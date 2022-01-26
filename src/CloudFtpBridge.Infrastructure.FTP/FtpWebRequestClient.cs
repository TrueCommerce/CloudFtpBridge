using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace CloudFtpBridge.Infrastructure.FTP
{
    public class FtpWebRequestClient
    {
        public FtpWebRequestClient(string ftpUrl, string ftpPath, int ftpPort, string userName, string password, ILogger<FTPFileSystem> logger,
            bool passiveFtp = false, bool useFtps = false)
        {
            FtpUrl = ftpUrl;
            Dir = ftpPath;
            Port = ftpPort;
            User = userName;
            Pass = password;
            UsePassive = passiveFtp;
            UseFtps = useFtps;
            Logger = logger;
        }

        public void SendToFtp(string fileName, Stream fromStream)
        {
            bool fileExist = false;

            if (!FilesOnFtpCaptured)
                FilesOnFtp = GetListOfFiles();

            MemoryStream memStream = new MemoryStream();
            fromStream.CopyTo(memStream);

            FtpWebRequest ftpReq = CreateRequest(fileName);

            //Check if file exists on FTP if so set fileExist to true
            if (FilesOnFtp != null && FilesOnFtp.Count >0)
            {
                foreach (string f in FilesOnFtp)
                {
                    if (fileName == f)
                    {
                        fileExist = true;
                        Logger.LogInformation("FtpWebRequest: File exists on FTP server. We will append to the file.");
                    }
                }
            }

            ftpReq.Method = fileExist ? WebRequestMethods.Ftp.AppendFile : WebRequestMethods.Ftp.UploadFile;
            ftpReq.Credentials = new NetworkCredential(User, Pass);

            byte[] fileContents = memStream.ToArray();
            ftpReq.ContentLength = fileContents.Length;

            Stream requestStream = ftpReq.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)ftpReq.GetResponse();
            response.Close();

            Logger.LogInformation("FtpWebRequest: Upload was successful: " + fileName);
        }
        public void Delete(string fileName)
        {
            try
            {
                Logger.LogInformation("FtpWebRequest: Attempting Delete for file: " + fileName);
                var reqFtp = CreateRequest(fileName);
                reqFtp.Method = WebRequestMethods.Ftp.DeleteFile;
                var respDel = (FtpWebResponse)reqFtp.GetResponse();
                respDel.Close();

                Logger.LogInformation("FtpWebRequest: Delete Successful");
            }
            catch (Exception x)
            {
                Logger.LogError("FtpWebRequest: Security, Permissions, or VAN moves file. Could not delete file from remote FTP server. File name: " + fileName+" Full exception: " + x.Message);
            }
        }

        public List<string> GetListOfFiles()
        {
            List<string> files = new List<string>();
            WebResponse response = null;
            StreamReader reader = null;
            try
            {
                var reqFtp = CreateRequest();
                reqFtp.Method = WebRequestMethods.Ftp.ListDirectory;
                response = reqFtp.GetResponse();

                reader = new StreamReader(response.GetResponseStream());

                string file = reader.ReadLine();
                while (file != null)
                {
                    files.Add(file);
                    file = reader.ReadLine();
                }

                reader.Close();
                response.Close();
                FilesOnFtpCaptured = true;
            }
            catch (Exception ex)
            {
                reader?.Close();
                response?.Close();
                Logger.LogError("FtpWebRequest: Exception in Getting Directory List: " + ex.Message);
            }
            
            return files;
        }

        private FtpWebRequest CreateRequest(string fileName = "")
        {
            var reqFtp = (FtpWebRequest)WebRequest.Create(new Uri(CreateUrl(fileName)));
            reqFtp.UseBinary = false;
            reqFtp.Credentials = new NetworkCredential(User, Pass);
            reqFtp.Proxy = null;
            reqFtp.KeepAlive = false;
            reqFtp.UsePassive = UsePassive;
            reqFtp.Timeout = 600000;
            reqFtp.EnableSsl = UseFtps;

            return reqFtp;
        }

        private string CreateUrl(string fileName)
        {
            string path;
            
            var url = FtpUrl.EndsWith("/") ? FtpUrl.Remove(FtpUrl.Length - 1) : FtpUrl;

            if (Dir.StartsWith("/") || Dir.EndsWith("/"))
            {
                path = Dir;

                if (path.StartsWith("/"))
                {
                    path = Dir.Substring(1);
                }

                if (path.EndsWith("/"))
                {
                    path = path.Remove(path.Length - 1);
                }
            }
            else
            {
                path = Dir;
            }

            return "ftp://" + url + ":" + Port.ToString() + "/" + path + "/" + fileName;
        }

        public Stream GetFile(string file)
        {
            try
            {
                Logger.LogInformation("FtpWebRequest: Attempting to receive file: " + file);
                string uri = CreateUrl(file);

                Uri ftpServerUri = new Uri(uri);

                if (ftpServerUri.Scheme != Uri.UriSchemeFtp)
                {
                    return null;
                }

                var reqFtp = CreateRequest(file);
                reqFtp.Method = WebRequestMethods.Ftp.DownloadFile;
                FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();


                Stream responseStream = new MemoryStream();

                response.GetResponseStream().CopyTo(responseStream);

                responseStream.Seek(0, SeekOrigin.Begin);

                response.Close();

                Logger.LogInformation("FtpWebRequest: File has been received successfully.");
                return responseStream;
            }
            catch (Exception ex)
            {
                Logger.LogError("FtpWebRequest: Get File failed Exception: " + ex.Message);
                return null;
            }
        }

        public void Rename(string oldFileName, string newFileName)
        {
            try
            {
                Logger.LogInformation("FtpWebRequest: Attempting to rename file from "+ oldFileName +" to " + newFileName);
                var reqFtp = CreateRequest(oldFileName);
                reqFtp.Method = WebRequestMethods.Ftp.Rename;
                reqFtp.RenameTo = newFileName;
                FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
                Logger.LogInformation("FtpWebRequest: File rename successful");
                response.Close();
            }
            catch (Exception e)
            {
                Logger.LogError("FtpWebRequest: Rename failed Exception: " + e.Message);
            }
        }

        public bool FileExists(string fileName)
        {
            if (!FilesOnFtpCaptured)
            {
                FilesOnFtp = GetListOfFiles();
                FilesOnFtpCaptured = true;
            }

            return FilesOnFtp.Any(fileName.Equals);
        }

        #region FTP Properties
        public string FtpUrl { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string Dir { get; set; }
        public string LocalDestDir { get; set; } = string.Empty;
        public bool EnableArchive { get; set; } = false;
        public bool UsePassive { get; set; }
        public bool UseFtps { get; set; }
        public int Port { get; set; } = 21;
        public List<string> FilesOnFtp { get; set; }
        public bool FilesOnFtpCaptured { get; set; }
        public ILogger<FTPFileSystem> Logger { get; set; }

        #endregion
    }
}
