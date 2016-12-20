// ============================================
// 
// FlexImage.WSRepository
// FlexImage.WSRepository.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.IO;
using FlexImage.Core;
using FlexImage.WebService.RepositoryWebReference;

#endregion

namespace FlexImage.WebService
{
    public class WSRepository
    {
        private readonly Log log = GenericSingleton<Log>.GetInstance();
        private readonly RepositoryService proxyRepository = new RepositoryService();
        private int _timeOut;

        private string securityToken = "";
        private long stationId;

        private long usrId;

        public int TimeOut
        {
            get { return this._timeOut; }
            set
            {
                this._timeOut = value;
                this.proxyRepository.Timeout = value;
            }
        }

        public string Url
        {
            set { this.proxyRepository.Url = value; }
        }

        public string SecurityToken
        {
            set { this.securityToken = value; }
        }

        public long UsrId
        {
            set { this.usrId = value; }
        }

        public long StationId
        {
            set { this.stationId = value; }
        }

        private string ResolvePath(enumPathServer path)
        {
            string ret = "";

            switch (path)
            {
                case enumPathServer.A2iATemplates:
                    {
                        ret = "templates_a2ia";
                        break;
                    }
                case enumPathServer.ABBYYTemplates:
                    {
                        ret = "templates_abbyy";
                        break;
                    }              
            }

            return ret;
        }

        public string Upload(string filename, long documentId, bool isBack, long txRate)
        {
            double bytesToRead;
            int offset = 0;

            //Read File into Array of Bytes
            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var length = (int) fileStream.Length; // get file length
            var buffer = new byte[length]; // create buffer         
            fileStream.Read(buffer, 0, buffer.Length);
            fileStream.Close();

            if (txRate != 0)
                bytesToRead = txRate;
            else
                bytesToRead = length;

            var totalNumberOfParts = (int) Math.Ceiling(length/bytesToRead);

            var hash = new Security.Hash();
            string hashFile = hash.SHA1File(filename);

            //Split Array of Bytes
            for (int partNumber = 1; partNumber <= totalNumberOfParts; partNumber++)
            {
                if ((length - offset) < txRate)
                    bytesToRead = length - offset;

                var destArray = new byte[(int) bytesToRead];
                Array.Copy(buffer, offset, destArray, 0, (int) bytesToRead);
                offset += (int) txRate;

                string extension = Path.GetExtension(filename).ToLower().Replace(".", "");

                this.log.Debug("WSUpload: " + partNumber + "/" + totalNumberOfParts);

                // Send part of file over the network
                if (!this.proxyRepository.upload(this.securityToken, this.usrId, true, destArray, documentId, true, isBack, extension,
                                                 length, true, hashFile, partNumber, true, totalNumberOfParts, true))
                {
                    this.log.Error("***** UPLOAD ERROR ******");
                    return "";
                }
            }

            return hashFile;
        }

        public string GetFileWithExtension(long documentId, string extension, bool isBack)
        {
            string newPath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "DOCS\\"),
                                          Files.GetFormattedPath(documentId.ToString()));

            string type = "F";
            if (isBack)
                type = "V";

            string filename = newPath + documentId + type + "." + extension;
            string filenameXml = newPath + documentId + type + ".xml";

            if (File.Exists(filename))
                File.Delete(filename);

            if (File.Exists(filenameXml))
                File.Delete(filenameXml);
                       

            fileDto fileDto = this.proxyRepository.getFileWithExtension(this.securityToken, this.usrId, true, documentId, true, isBack,
                                                                        extension);
            Directory.CreateDirectory(newPath);
            Files.ByteArrayToFile(filename, fileDto.byteArray);
            
            return filename;
        }

        public bool GetFileGeneric(enumPathServer pathServer, string filename, string localFileName)
        {
            try
            {
                string path = this.ResolvePath(pathServer);

                if (!File.Exists(localFileName))
                {
                    fileDto fileDto = this.proxyRepository.getFileGeneric(this.securityToken, this.usrId, true, path, filename);

                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(localFileName));
                    Files.ByteArrayToFile(localFileName, fileDto.byteArray);
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool UploadGeneric(enumPathServer pathServer, string filename)
        {
            //double bytesToRead;
            //int offset = 0;

            //Read File into Array of Bytes
            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var length = (int) fileStream.Length; // get file length
            var buffer = new byte[length]; // create buffer         
            fileStream.Read(buffer, 0, buffer.Length);
            fileStream.Close();

            string path = this.ResolvePath(pathServer);
            bool ret =  this.proxyRepository.uploadGeneric(this.securityToken, this.usrId, true, buffer, path, System.IO.Path.GetFileName(filename).ToLower());
            return ret;
        }

        public string[] DirList(enumPathServer pathServer)
        {
            string path = this.ResolvePath(pathServer);
            return this.proxyRepository.dirList(this.securityToken, this.usrId, true,  path);
        }

        public bool DeleteGeneric(string path, string fileName)        
        {
            return proxyRepository.deleteGeneric(securityToken, this.usrId, true, path, fileName);
        }

        public string GetPdfPages(long documentNfId, int startPage, int endPage )
        {
            string newPath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "DOCS\\"),
                                          Files.GetFormattedPath(documentNfId.ToString()));

            string type = "F";

            string filename = newPath + documentNfId + type + ".pdf";

            if (System.IO.File.Exists(filename))
                System.IO.File.Delete(filename);

            fileDto fileDto = proxyRepository.getPdfPages(securityToken, this.usrId, true, documentNfId, true, startPage, true, endPage, true);
            Directory.CreateDirectory(newPath);
            Files.ByteArrayToFile(filename, fileDto.byteArray);
         
            return filename;

        }
    }
}