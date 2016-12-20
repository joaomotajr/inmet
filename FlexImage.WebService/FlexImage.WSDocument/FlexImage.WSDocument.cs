// ============================================
// 
// FlexImage.WSDocument
// FlexImage.WSDocument.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using FlexImage.WebService.DocumentWebReference;

#endregion

namespace FlexImage.WebService
{
    public class WSDocument
    {
        private readonly DocumentService proxyDocument = new DocumentService();
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
                this.proxyDocument.Timeout = value;
            }
        }


        public string Url
        {
            set { this.proxyDocument.Url = value; }
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

        public long[] getDocsId(int cache)
        {
            long[] ret = this.proxyDocument.getDocsId(this.securityToken, this.usrId, true, cache, true);
            return ret;
        }

        public void SetJpgDocumentFTransmitted(long documentId, long batchId, string filename, int fileSize,
                                               string fileType, string hashFile)
        {
            this.proxyDocument.setJpgDocumentFTransmittedFront(this.securityToken, documentId, true, batchId, true,
                                                               fileSize, true,
                                                               fileType, hashFile, this.usrId, true, this.stationId,
                                                               true);
        }

        public void SetJpgDocumentFTransmittedBack(long documentId, long batchId, string filename, int fileSize,
                                               string fileType, string hashFile)
        {
            this.proxyDocument.setJpgDocumentFTransmittedBack(this.securityToken, documentId, true, batchId, true,
                                                               fileSize, true,
                                                               fileType, hashFile, this.usrId, true, this.stationId,
                                                               true);
        }

        public void setJpgDocumentFTransmittedBackDetached(long documentId, long batchId, string filename, int fileSize,
                                                           string fileType, string documentHashFile)
        {
            this.proxyDocument.setJpgDocumentFTransmittedBackDetached(this.securityToken, documentId, true, batchId,
                                                                      true,
                                                                      fileSize, true, fileType, documentHashFile,
                                                                      this.usrId, true,
                                                                      this.stationId, true);
        }

        public void SetTifDocumentFTransmittedBackDetached(long documentId, long batchId, string filename, int fileSize,
                                                   string fileType, string documentHashFileBack)
        {
            this.proxyDocument.setTifDocumentFTransmittedBackDetached(this.securityToken, documentId, true, batchId, true,
                                                              fileSize, true,
                                                              fileType, documentHashFileBack, this.usrId, true,
                                                              this.stationId, true);
        }

        public void EditDocument(long documentId, string newDocumentXml, string oldDocumentXml, string documentNumber,
                                 decimal documentValue)
        {
            this.proxyDocument.editDocument(this.securityToken, documentId, true, newDocumentXml, oldDocumentXml,
                                            documentNumber, documentValue, true, this.usrId, true, this.stationId, true);
        }

        public documentDto GetDocument(long documentId)
        {
            return this.proxyDocument.getDocument(this.securityToken, this.usrId, true, documentId, true);
        }

        public void RequestJpgDetached(long documentId, bool isFront)
        {
            if (isFront)
                this.proxyDocument.requestJpgDetached(this.securityToken, documentId, true, this.usrId, true,
                                                      this.stationId, true);
            else
                this.proxyDocument.requestJpgBackDetached(this.securityToken, documentId, true, this.usrId, true,
                                                          this.stationId, true);

            return;
        }

        public void SetJpgTransmittedDetached(long documentId, long batchId, int documentFileSizeFront,
                                              string documentFileTypeFront, string documentHashFile, bool isFront)
        {
            if (isFront)
            {
                this.proxyDocument.setJpgTransmittedFrontDetached(this.securityToken, documentId, true, batchId, true,
                                                                  documentFileSizeFront, true, documentFileTypeFront,
                                                                  documentHashFile, this.usrId, true, this.stationId,
                                                                  true);
            }
            else
            {
                proxyDocument.setJpgDocumentFTransmittedBackDetached(securityToken, documentId, true, batchId, true, documentFileSizeFront, true, documentFileTypeFront, documentHashFile, usrId, true, stationId, true);
            }
            return;
        }

        public void RequestTifBackDetached(long documentId)
        {
            this.proxyDocument.requestTifBackDetached(this.securityToken, documentId, true, this.usrId, true,
                                                      this.stationId, true);
            return;
        }

        public void AckException(long documentId, long exceptionId, string[] arrayUsr)
        {
            this.proxyDocument.ackException(this.securityToken, documentId, true, exceptionId, true, arrayUsr,
                                            this.usrId, true,
                                            this.stationId, true);
            return;
        }

        public documentLogDto[] GetDocumentLog(long documentId)
        {
            return this.proxyDocument.getDocumentLog(this.securityToken, this.usrId, true, documentId, true);
        }

        public void Recapture(long documentId, long batchId, string reason)
        {
            this.proxyDocument.recapture(this.securityToken, documentId, true, batchId, true, reason, this.usrId, true,
                                         this.stationId, true);
        }

        public void Reclassify(long documentId, long batchId, int batchTotalDocs, long workflowId)
        {
            this.proxyDocument.reclassify(this.securityToken, documentId, true, batchId, true, batchTotalDocs, true,
                                          workflowId, true, this.usrId, true, this.stationId, true);
        }

        public void RequestJPG(long documentId)
        {
            this.proxyDocument.requestJpg(this.securityToken, documentId, true, this.usrId, true, this.stationId, true);
        }

        public void RequestJPGBack(long documentId)
        {
            this.proxyDocument.requestJpgBack(this.securityToken, documentId, true, this.usrId, true, this.stationId, true);
        }
 
        public documentDto[] GetPendingBySite(long siteId)
        {
            return this.proxyDocument.getPendingBySite(securityToken, this.usrId, true, siteId, true);
        }

        public void MountTrans(long workflowId, long batchId, long[] documentIds, long documentCoverId)
        {
            this.proxyDocument.mountTrans(securityToken, batchId, true, workflowId, true, documentIds, documentCoverId, true, usrId, true, stationId, true);
        }

        public long[] GetBatchFToReceipt(long siteId)
        {
            return this.proxyDocument.getBatchFToReceipt(securityToken, this.usrId, true, siteId, true);
        }

        public documentDto[] GetReceiptByBatch(long batchId)
        {
            return this.proxyDocument.getReceiptByBatch(securityToken, this.usrId, true, batchId, true);
        }

        public string PrintReceipt(long documentId)
        {
            return this.proxyDocument.printReceipt(securityToken, documentId, true, usrId, true, stationId, true);
        }

        public void AckPending(long pendingId, long documentId)
        {
            this.proxyDocument.ackPending(securityToken, pendingId, true, documentId, true, usrId, true, stationId, true);
            return;
        }

        
    }
}