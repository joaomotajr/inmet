// ============================================
// 
// FlexImage.WSCapture
// FlexImage.WSCapture.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using FlexImage.Core;
using FlexImage.WebService.CaptureWebReference;

#endregion

namespace FlexImage.WebService
{
    public class WSCapture
    {
        private readonly Log log = GenericSingleton<Log>.GetInstance();
        private readonly CaptureService proxyCapture = new CaptureService();
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
                this.proxyCapture.Timeout = value;
            }
        }


        public string Url
        {
            set { this.proxyCapture.Url = value; }
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

        public long OpenBatch(long workflowId, long siteId)
        {
            return this.proxyCapture.openBatchF(this.securityToken, workflowId, true, this.stationId, true, siteId, true,
                                                this.usrId, true);
        }

        public void insertDocument(long usrId, long batchId, string xml, long typeId, long documentId, int fileSize,
                                   int captSeq, string fileType, string ocrTextNumber, decimal ocrTextValue,
                                   string documentHashFile)
        {
            this.proxyCapture.insertDocument(this.securityToken, usrId, true, this.stationId, true, typeId, true,
                                             batchId, true, xml,
                                             documentId, true, fileSize, true, captSeq, true, fileType, ocrTextNumber,
                                             ocrTextValue, true, documentHashFile);
            return;
        }


        public void CloseBatch(long batchId, long siteId, long workflowId, int[] docsCanceled, int totDocs)
        {
            this.proxyCapture.closeBatchF(this.securityToken, batchId, true, this.stationId, true, siteId, true,
                                          workflowId, true,
                                          this.usrId, true, docsCanceled, totDocs, true);
        }

        public void SetBatchTransmitted(long batchId, long workflowId, int totFiles)
        {
            this.proxyCapture.setBatchFTransmitted(this.securityToken, batchId, true, workflowId, true, totFiles, true,
                                                   this.stationId,
                                                   true, this.usrId, true);
        }

        public long[] GetBatchFToRecapture()
        {
            return this.proxyCapture.getBatchFToRecapture(this.securityToken, this.usrId, true, this.stationId, true);
        }

        public documentDto[] GetDocumentFToRecapture(long batchId)
        {
            return this.proxyCapture.getDocumentFToRecapture(this.securityToken, this.usrId, true, batchId, true);
        }

        public void setDocFRecapture(long documentIdOld, long documentIdNew, long batchId)
        {
            this.proxyCapture.setDocFRecapture(this.securityToken, documentIdOld, true, documentIdNew, true, batchId,
                                               true,
                                               this.stationId, true, this.usrId, true);
        }
    }
}