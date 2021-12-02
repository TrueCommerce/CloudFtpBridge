using CloudFtpBridge.Core.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace CloudFtpBridge.Infrastructure.FTP
{
    public class Receive
    {
        public void Download()
        {
            string[] files = GetFileListing();
            if (files != null)
            {
                foreach (string file in files)
                {
                    ReceiveFiles(file);
                }
            }
        }

        private void ReceiveFiles(string file)
        {
            try
            {
                string uri = "ftp://" + FtpUrl + "/" + Dir + "/" + file;

                Uri ftpServerUri = new Uri(uri);

                if (ftpServerUri.Scheme != Uri.UriSchemeFtp)
                {
                    return;
                }
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpUrl + "/" + dir + "/" + file));

                #region Set credentials and settings
                reqFTP.Credentials = new NetworkCredential(user, pass);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Proxy = null;
                reqFTP.UsePassive = Passive;
                #endregion

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream responseStream = response.GetResponseStream();
                FileStream writeStream;

                if (File.Exists(localDestDir + "\\" + file) == false)
                {
                    writeStream = new FileStream(localDestDir + "\\" + file, FileMode.Create);
                }
                else
                {
                    Trace.TraceInformation("File existed prefixing a GUID.");
                    writeStream = new FileStream(localDestDir + "\\" + Guid.NewGuid().ToString() + "_" + file, FileMode.Create);
                }

                responseStream.CopyTo(writeStream);
                writeStream.Close();
                response.Close();
                responseStream.Close();

                if (EnableArchive)
                    File.Copy(localDestDir + file, localDestDir + "\\Archive\\" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + file);
                #region Perform Delete

                if (DelFileAfterDownload == true)
                {
                    try
                    {
                        Trace.TraceInformation("Attempting Delete");
                        reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + FtpUrl + "/" + Dir + "/" + file));
                        reqFTP.Credentials = new NetworkCredential(User, Pass);
                        reqFTP.KeepAlive = false;
                        reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                        reqFTP.UseBinary = true;
                        reqFTP.Proxy = null;
                        reqFTP.UsePassive = Passive;
                        FtpWebResponse responsedel = (FtpWebResponse)reqFTP.GetResponse();
                        responsedel.Close();
                        Trace.TraceInformation("Delete Successful");
                    }
                    catch (Exception x)
                    {
                        Trace.TraceInformation("Security Permissions or VAN moves file. Could not delete file from remote FTP server. Full exception: " + x.Message);
                    }
                }
                #endregion


            }
            catch (Exception ex)
            {
                Trace.TraceInformation("Receive File failed Exception ex: " + ex.Message);
            }
        }

        public string[] GetFileListing()
        {
            StringBuilder result = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;
            try
            {
                FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + FtpUrl + "/" + dir + "/"));
                #region FTP Settings
                ftpReq.UseBinary = true;
                ftpReq.Credentials = new NetworkCredential(User, Pass);
                ftpReq.Method = WebRequestMethods.Ftp.ListDirectory;
                ftpReq.Proxy = null;
                ftpReq.KeepAlive = true;
                ftpReq.UsePassive = Passive;
                ftpReq.Timeout = 600000;
                #endregion

                response = ftpReq.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();

                while (line != null)
                {
                    if (!line.StartsWith("."))//ignore directories
                    {
                        if (!string.IsNullOrEmpty(FTPPrefixes))//get files with specific prefix
                        {
                            if (FTPPrefixes.Contains("|"))//if multiple prefixes
                            {
                                string[] listPrefixes = FTPPrefixes.Split('|');
                                foreach (string prefix in listPrefixes)
                                {
                                    if (!string.IsNullOrEmpty(prefix) & line.StartsWith(prefix))
                                    {
                                        result.Append(line);
                                        result.Append("\n");
                                        break;
                                    }
                                }
                            }
                            else if (line.StartsWith(FTPPrefixes))//if only one
                            {
                                result.Append(line);
                                result.Append("\n");
                            }
                        }
                        else
                        {
                            result.Append(line);
                            result.Append("\n");
                        }
                    }
                    line = reader.ReadLine();

                }

                if (result == null || result.ToString() == "")
                {
                    return null;
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                return result.ToString().Split('\n');
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
                Trace.TraceInformation(ex.Message);

                return null;
            }

        }

        #region Properties
        private string ftpUrl = "";
        private string user = "";
        private string pass = "";
        private string dir = "";
        private string localDestDir = "";
        private bool delFileAfterDownload = true;
        private bool Passive = false;
        private string ftpPrefixes = "";
        private bool enableArch = false;

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

        public bool DelFileAfterDownload
        {
            get { return delFileAfterDownload; }
            set { delFileAfterDownload = value; }
        }

        public bool SetPassive
        {
            get { return Passive; }
            set { Passive = value; }
        }

        public string FTPPrefixes
        {
            get { return ftpPrefixes; }
            set { ftpPrefixes = value; }
        }

        public bool EnableArchive
        {
            get { return enableArch; }
            set { enableArch = value; }
        }
        #endregion
    }
}
