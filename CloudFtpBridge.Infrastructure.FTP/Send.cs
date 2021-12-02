using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace CloudFtpBridge.Infrastructure.FTP
{
    internal class Send
    {
        public void SendFiles()
        {
            if (!localDestDir.EndsWith("\\"))
                localDestDir = localDestDir + "\\";

            string[] files = Directory.GetFiles(localDestDir);

            if (files != null)
            {
                try
                {
                    var filesOnFtp = GetFileListing();
                    foreach (string fileName in files)
                    {
                        try
                        {
                            string temp = Path.GetFileName(fileName);
                            SendToFtp(localDestDir, temp, filesOnFtp);

                            if (!Directory.Exists(localDestDir + "Archive"))
                                Directory.CreateDirectory(localDestDir + "Archive");

                            File.Move(fileName, localDestDir + "Archive\\" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + temp);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceInformation("Exception occurred in Send: " + e.Message);
                }
            }
        }

        public void SendToFtp(string localFilePath, string fileName, List<string> filesOnFtp)
        {
            bool fileExist = false;
            FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create("ftp://" + ftpUrl + ":" + Port.ToString() + "/" + dir + "/" + fileName);

            //Check if file exists on FTP if so set fileExist to true
            if (filesOnFtp != null)
            {
                foreach (string f in filesOnFtp)
                {
                    if (fileName == f)
                    {
                        fileExist = true;
                        Trace.TraceInformation("File exists on FTP server. We will append to the file.");
                    }
                }
            }

            if (fileExist)
               ftpReq.Method = WebRequestMethods.Ftp.AppendFile;
            else
               ftpReq.Method = WebRequestMethods.Ftp.UploadFile;

            ftpReq.EnableSsl = UseFtps;
            ftpReq.UsePassive = Passive;
            ftpReq.UseBinary = true;
            ftpReq.KeepAlive = false;
            ftpReq.Timeout = 600000;

            ftpReq.Credentials = new NetworkCredential(user, pass);

            StreamReader fileStream = new StreamReader(localDestDir + fileName);
            byte[] fileContents = Encoding.UTF8.GetBytes(fileStream.ReadToEnd());
            fileStream.Close();
            ftpReq.ContentLength = fileContents.Length;

            Stream requestStream = ftpReq.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)ftpReq.GetResponse();
            response.Close();

            Trace.TraceInformation("Upload was successful: " + fileName);
        }

        public List<string> GetFileListing()
        {
            string uri = "ftp://" + ftpUrl + ":" + Port.ToString() + "/" + dir + "/";
            List<string> files = new List<string>();
            WebResponse response = null;
            StreamReader reader = null;
            try
            {
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(uri));
                reqFTP.UseBinary = false;
                reqFTP.Credentials = new NetworkCredential(user, pass);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.Proxy = null;
                reqFTP.KeepAlive = true;
                reqFTP.UsePassive = Passive;
                reqFTP.Timeout = 600000;
                reqFTP.EnableSsl = UseFtps;

                response = reqFTP.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    files.Add(line);
                    line = reader.ReadLine();
                }

                if (files == null || files.Count == 0)
                {
                    Trace.TraceInformation("No files to send.");
                    return null;
                }
                return files;
            }
            catch (Exception ex)
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                Trace.TraceInformation("Exception in Send: " + ex.Message);
                return null;
            }
        }


        #region Properties
        private string ftpUrl = "";
        private string user = "";
        private string pass = "";
        private string dir = "";
        private string localDestDir = "";
        private bool Passive = false;

        public string FtpUrl
        {
            get { return ftpUrl; }
            set { ftpUrl = value; }
        }

        public string User
        {
            get { return user; }
            set { user = value; }
        }

        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }

        public string Dir
        {
            get { return dir; }
            set { dir = value; }
        }

        public string LocalDestDir
        {
            get { return localDestDir; }
            set { localDestDir = value; }
        }

        public bool UsePassive
        {
            get { return Passive; }
            set { Passive = value; }
        }

        public bool UseFtps { get; set; } = false;
        public int Port { get; set; } = 21;

        #endregion
    }
}
