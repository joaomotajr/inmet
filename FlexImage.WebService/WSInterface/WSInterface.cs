using System;
using System.IO;
using System.Windows.Forms;
using FlexImage.Core;
using FlexImage.WebService.InterfaceWebReference;
using System.Xml;

namespace FlexImage.WebService
{    
    public delegate void ConnectionInfo(string text);

    public class WSInterface
    {
        public WSService ProxyInterface = new WSService();
                
        public event ConnectionInfo OnInfo;
        public event ConnectionInfo OnConnected;

        private readonly Log log = GenericSingleton<Log>.GetInstance();
        private readonly string[] server = new string[4];

        private string service = "";
        private string endPoint = "";

        public string EndPoint
        {
            get { return endPoint; }
            set { 
                endPoint = value;
                ProxyInterface.Url = endPoint + "InterfaceService";
            }
        }
        private string port = "";
        private string protocol = "";
        
        public bool SelectServer(string addr)
        {
            log.Info("SelectServer: " + addr);

            if (OnInfo != null)
            {
                OnInfo("Connecting server...");
            }

            string uri = addr + "InterfaceService?wsdl";

            if (CheckConnection(addr))
            {
                if (OnInfo != null)
                {
                    OnInfo("Connected.");
                }

                endPoint = addr;
                
                return true;
            }

            return false;
        }

        public bool SelectServer()
        {
            string fileName = Application.StartupPath + "\\CONFIG\\CONFIG.XML";
            string aux = "";

            if (!File.Exists(fileName))
            {
                //Gera arquivo Default
                WriteDefaultConfig(fileName);
            }

            XmlTextReader xtr = new XmlTextReader(fileName);
            xtr.WhitespaceHandling = WhitespaceHandling.None;
            XmlDocument xml = new XmlDocument();
            xml.Load(xtr);
            xtr.Close();

            System.Xml.XmlNode element = xml.SelectSingleNode("/ROOT/ENVIRONMENTS");

            if (element == null)
            {
                Alert.Exclamation("Tags <ENVIRONMENTS> não localizada no arquivo de configuração!");
                return false;
            }
            else
            {
                SelectEnvironment select = new SelectEnvironment(xml);
                select.ShowDialog();

                protocol = select.Protocol;
                server[0] = select.Server;
                port = select.Port;
                service = select.Service;

                Application.DoEvents();
            }

            if (service == null)
                return false;

            aux = service.Substring(service.Length - 1, 1);
            if (aux != "/")
                service = service + "/";

            for (int i = 0; i < 4; i++)
            {
                if (OnInfo != null)
                {
                    OnInfo("Connecting server: " + server[i]);
                }

                endPoint = protocol + "://" + server[i] + ":" + port + service;
                string uri = endPoint + "InterfaceService?wsdl";

                if (CheckConnection(endPoint))
                {
                    if (OnConnected != null)
                    {
                        OnConnected("Connected: " + server[i]);
                    }

                    return true;
                }
            }
         
            Alert.Exclamation("Não foi possível se conectar ao DataCenter!");
            return false;
        }

        private void WriteDefaultConfig(string filename)
        {
            string aux="";
            string dir = System.IO.Path.GetDirectoryName(filename);

            try
            {

                System.IO.Directory.CreateDirectory(dir);

                aux = aux + "<ROOT>\n";
                aux = aux + "	<ENVIRONMENTS>\n";
                aux = aux + "	   <ENVIRONMENT1>\n";
                aux = aux + "		<DEPLOY>FLEXDOC</DEPLOY>\n";
                aux = aux + "		<PROTOCOL>HTTP</PROTOCOL>\n";
                aux = aux + "		<SERVICE>/flexdoc/Service/</SERVICE>\n";
                aux = aux + "		<PORT>8080</PORT>\n";
                aux = aux + "		<SERVER>192.168.1.2</SERVER>\n";
                aux = aux + "	   </ENVIRONMENT1>\n";
                aux = aux + "	</ENVIRONMENTS>\n";
                aux = aux + "</ROOT>\n";
                
                // create a writer and open the file
                TextWriter tw = new StreamWriter(filename);

                // write a line of text to the file
                tw.WriteLine(aux);

                // close the stream
                tw.Close();
            }
            catch (Exception e)
            {
                Alert.Exclamation("InterfaceWS::WriteDefault:" + e.ToString());
            }
        }

        private bool CheckConnection(string url)
        {

            System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();

            var pingRequest = new ping();
            var pingResponse = new pingResponse();

            try
            {
                ProxyInterface.Url = url + "InterfaceService";

                pingResponse = ProxyInterface.ping(pingRequest);

                if (pingResponse.@return.ToString() != "")
                    return true;
            }
            catch (Exception e)
            {
                log.Error("CheckConnection:" + e + url);
            }

            return false;
        }

        //public long GetNewBatch(int batchType, long workflowId)
        //{
        //    var openBatch = new openBatch();
        //    var openBatchResponse = new openBatchResponse();

        //    openBatch.batchType = batchType;
        //    openBatch.batchTypeSpecified = true;
        //    openBatch.siteId = wsLogin.UserLogin.@return.siteId;
        //    openBatch.siteIdSpecified = true;
        //    openBatch.stationId = wsSecurity.StationId;
        //    openBatch.stationIdSpecified = true;
        //    openBatch.usrId = wsSecurity.UsrId;
        //    openBatch.usrIdSpecified = true;
        //    openBatch.workflowId = workflowId;
        //    openBatch.workflowIdSpecified = true;

        //    openBatchResponse = ProxyInterface.openBatch(openBatch);

        //    return openBatchResponse.@return;
        //}

        //public void SetTransmitted(TransmissionQueueDTO obj, long size)
        //{
        //    var setDocumentSent1 = new setDocumentSent();

        //    setDocumentSent1.documentId = obj.DocumentId;
        //    setDocumentSent1.documentIdSpecified = true;
        //    setDocumentSent1.queueTransmissionId = obj.QueueTransmissionId;
        //    setDocumentSent1.queueTransmissionIdSpecified = true;
        //    setDocumentSent1.queueTransmissionType = (int)(obj.QueueTransmissionType);
        //    setDocumentSent1.queueTransmissionTypeSpecified = true;
        //    setDocumentSent1.size = size;
        //    setDocumentSent1.sizeSpecified = true;

        //    try
        //    {
        //        ProxyInterface.setDocumentSent(setDocumentSent1);
        //        log.Debug("Set Transmitted: Document_id [" + obj.DocumentId + "]");
        //    }
        //    catch (Exception er)
        //    {
        //        log.Debug("Error: SetTransmitted: [" + obj.DocumentId + "] - " + er.Message);
        //    }
        //}

        public long[] GetDocsNumber()
        {
            var getDocsNumber1 = new getDocsNumber();

            getDocsNumber1.qt = 10;
            getDocsNumber1.qtSpecified = true;
            long[] arrayId = ProxyInterface.getDocsNumber(getDocsNumber1);

            return arrayId;
        }

        public bool Upload(long usrId, long stationId, long workflowId, long batchId, string xml, long docId, long size, int captSeq,
                           string fileType, int totalPages)
        {

            try
            {
                var param = new txDocF();

                param.batchId = batchId;
                param.batchIdSpecified = true;

                param.stationId = stationId;
                param.stationIdSpecified = true;

                param.captureSeq = captSeq;
                param.captureSeqSpecified = true;

                param.docId = docId;
                param.docIdSpecified = true;

                param.fileSizeFront = size;
                param.fileSizeFrontSpecified = true;

                param.pages = totalPages;
                param.pagesSpecified = true;

                param.usrId = usrId;
                param.usrIdSpecified = true;

                param.workflowId = workflowId;
                param.workflowIdSpecified = true;

                param.fileTypeFront = fileType.ToUpper();
                param.ocr = xml;

                txDocFResponse resp = new txDocFResponse();

                resp = ProxyInterface.txDocF(param);

                log.Debug("Upload: Document inserted into BD: " + batchId + "/" + docId + "/" + usrId);
            }
            catch (Exception e)
            {
                log.Error("Upload Error: " + batchId + "/" + docId + "/" + usrId + ":" + e.ToString());
                return false;
            }

            return true;
        }

        public long GetNewBatch(int batchType, long workflowId, long siteId, long stationId, long userId)
        {
            var openBatch = new openBatch();
            var openBatchResponse = new openBatchResponse();

            openBatch.batchType = batchType;
            openBatch.batchTypeSpecified = true;
            openBatch.siteId = siteId;
            openBatch.siteIdSpecified = true;
            openBatch.stationId = stationId;
            openBatch.stationIdSpecified = true;
            openBatch.usrId = userId;
            openBatch.usrIdSpecified = true;
            openBatch.workflowId = workflowId;
            openBatch.workflowIdSpecified = true;

            openBatchResponse = ProxyInterface.openBatch(openBatch);

            return openBatchResponse.@return;
        }


        public bool SetBatchTransmitted(long batchId, long workflowId, int totFiles, long stationId)
        {
            bool ret = false;

            try
            {
                var setBatchTransmitted1 = new setBatchTransmitted();
                var setBatchTransmittedResponse1 = new setBatchTransmittedResponse();

                setBatchTransmitted1.batchId = batchId;
                setBatchTransmitted1.batchIdSpecified = true;

                setBatchTransmitted1.workflowId = workflowId;
                setBatchTransmitted1.workflowIdSpecified = true;

                setBatchTransmitted1.totalDocs = totFiles;
                setBatchTransmitted1.totalDocsSpecified = true;

                setBatchTransmitted1.stationId = stationId;
                setBatchTransmitted1.stationIdSpecified = true;

                log.Debug("Setting Batch Transmitted: Batch_id [BatchId:" + batchId + "/WorkflowId:" + workflowId + "/TotFiles:"+ totFiles + "/StationId:"+ stationId + "]");
                setBatchTransmittedResponse1 = ProxyInterface.setBatchTransmitted(setBatchTransmitted1);
                log.Debug("Response Batch Transmitted: Batch_id [" + batchId + "]: " + setBatchTransmittedResponse1.ToString());

                ret = true;
            }
            catch (Exception e)
            {
                log.Error("Error: SetBatchTransmitted: " + e.ToString());
            }
            return ret;
        }

        public bool UploadFile(string fileName, double txRate, string destDirectory)
        {
            var doUpload1 = new doUpload();
            var doUploadResponse1 = new doUploadResponse();

            try
            {
                if (!File.Exists(fileName))
                {
                    MessageBox.Show("UploadFile::Upload: File [" + fileName + "] not found");
                    return false;
                }

                double bytesToRead;
                int offset = 0;

                //Read File into Array of Bytes
                var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                var length = (int)fileStream.Length; // get file length
                var buffer = new byte[length]; // create buffer         
                fileStream.Read(buffer, 0, buffer.Length);
                fileStream.Close();

                if (txRate != 0)
                {
                    bytesToRead = txRate;
                }
                else
                {
                    bytesToRead = length;
                }

                var totalNumberOfParts = (int)Math.Ceiling(length / bytesToRead);

                Crypto.Hash hash = new Crypto.Hash();

                //Set WS Parameters
                doUpload1.fileName = Path.GetFileName(fileName);
                doUpload1.hash = hash.SHA1File(fileName);
                doUpload1.totalParts = totalNumberOfParts;
                doUpload1.dirOut = destDirectory.Replace("\\", "/");

                //Split Array of Bytes
                for (int partNumber = 1; partNumber <= totalNumberOfParts; partNumber++)
                {
                    if ((length - offset) < txRate)
                        bytesToRead = length - offset;

                    var destArray = new byte[(int)bytesToRead];
                    Array.Copy(buffer, offset, destArray, 0, (int)bytesToRead);
                    offset += (int)txRate;

                    // Send part of file over the network
                    doUpload1.part = partNumber;
                    doUpload1.bytes = destArray;
                    doUploadResponse1 = ProxyInterface.doUpload(doUpload1);
                }
            }
            catch (Exception e)
            {
                log.Error("UploadFile: " + e.ToString());
                return false;
            }

            return true;
        }

        public void CloseBatch(long batchId, long stationId, long siteId, long workflowId, long usrId,
                               int[] docsCanceled, int totDocs)
        {
            var closeBatch = new closeBatch();
            var closeBatchResponse = new closeBatchResponse();

            closeBatch.batchId = batchId;
            closeBatch.batchIdSpecified = true;
            closeBatch.docsCanceled = docsCanceled;
            closeBatch.siteId = siteId;
            closeBatch.siteIdSpecified = true;
            closeBatch.stationId = stationId;
            closeBatch.stationIdSpecified = true;
            closeBatch.totalDocs = totDocs;
            closeBatch.totalDocsSpecified = true;
            closeBatch.usrId = usrId;
            closeBatch.usrIdSpecified = true;
            closeBatch.workflowId = workflowId;
            closeBatch.workflowIdSpecified = true;

            closeBatchResponse = ProxyInterface.closeBatch(closeBatch);
        }

        public string GetFile(long docId)
        {
            string newPath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "DOCS\\"),
                                          Files.GetFormattedPath(docId.ToString()));
            Directory.CreateDirectory(newPath);
            string filename = newPath + docId + "F.PDF";

            //Verifica se arquivo já está em Cache Local ou é necessário download
            if (!File.Exists(filename))
            {
                //Realiza download a partir do servidor
                var getfile1 = new getFile();
                var response = new getFileResponse();

                getfile1.documentId = docId;
                getfile1.documentIdSpecified = true;

                response = ProxyInterface.getFile(getfile1);

                if (response.@return.bytes == null)
                {
                    //Arquivo não foi encontrado no servidor
                    return "";
                }

                Files.ByteArrayToFile(filename, response.@return.bytes);
            }

            return filename;
        }

        public checklistDTO[] findChecklistByWorkflowId(long workflowId)
        {
            var param = new findChecklistByWorkflowId();
            checklistDTO[] response;

            param.workflowId = workflowId;
            param.workflowIdSpecified = true;

            response = ProxyInterface.findChecklistByWorkflowId(param);
            return response;
        }

        public paramDTO[] GetParams()
        {
            paramDTO[] response;

            var getParams1 = new getParams();
            response = ProxyInterface.getParams(getParams1);
            return response;
        }

        public reportDTO[] GetReports()
        {
            var param1 = new getReports();
            reportDTO[] response;

            response = ProxyInterface.getReports(param1);
            return response;
        }

        public confirmRecaptureResponse ConfirmRecapture(long stationId, long docId, long usrId, string ext)
        {

            confirmRecapture param = new confirmRecapture();
            param.docId = docId;
            param.docIdSpecified = true;

            param.stationId = stationId;
            param.stationIdSpecified = true;

            param.usrId = usrId;
            param.usrIdSpecified = true;

            param.imageExtension = ext;

            confirmRecaptureResponse response = ProxyInterface.confirmRecapture(param);

            return response;

        }


        //Action Print: 240
        public usrLogResponse UsrLog(int action, long usrId, long stationId, string desc)
        {
            usrLog param = new usrLog();

            param.action = action;
            param.actionSpecified = true;

            param.desc = desc;

            param.stationId = stationId;
            param.stationIdSpecified = true;

            param.usrId = usrId;
            param.usrIdSpecified = true;

            usrLogResponse response = ProxyInterface.usrLog(param);

            return response;

        }

        public documentnfLogResponse DocumentNfLog(int action, long usrId, long stationId, string desc, long docId)
        {
            documentnfLog param = new documentnfLog();

            param.action = action;
            param.actionSpecified = true;

            param.desc = desc;

            param.stationId = stationId;
            param.stationIdSpecified = true;

            param.usrId = usrId;
            param.usrIdSpecified = true;

            param.docNfId = docId;
            param.docNfIdSpecified = true;

            documentnfLogResponse response = ProxyInterface.documentnfLog(param);

            return response;

        }
    }
}