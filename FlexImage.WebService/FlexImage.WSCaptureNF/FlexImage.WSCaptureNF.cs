// ============================================
// 
// FlexImage.WSCaptureNF
// FlexImage.WSCaptureNF.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using FlexImage.Core;
using FlexImage.WebService.CaptureNFWebReference;

#endregion

namespace FlexImage.WebService
{
    public class WSCaptureNF
    {
        private readonly Log log = GenericSingleton<Log>.GetInstance();
        private readonly CaptureNfService proxyCaptureNF = new CaptureNfService();
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
                this.proxyCaptureNF.Timeout = value;
            }
        }


        public string Url
        {
            set { this.proxyCaptureNF.Url = value; }
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

        public void insertDocumentNF(long usrId, long workflowId, long batchId, long typeId, long virtualFolderId,
                                     long documentId, int fileSize, int captSeq, string fileType, int totalPages,
                                     int origin, string originalFileName, string hash)
        {
            this.proxyCaptureNF.insertDocumentNf(this.securityToken, usrId, true, this.stationId, true, documentId, true,
                                                 typeId, true,
                                                 virtualFolderId, true, batchId, true, origin, true, fileSize, true,
                                                 captSeq,
                                                 true, fileType, totalPages, true, originalFileName, hash);
            return;
        }

        public long OpenBatchNF(long workflowId, long siteId)
        {
            return this.proxyCaptureNF.openBatchNf(this.securityToken, workflowId, true, this.stationId, true, siteId,
                                                   true, this.usrId,
                                                   true);
        }

        public void CloseBatchNF(long batchNFId, long siteId, long workflowId, int[] docsCanceled)
        {
            this.proxyCaptureNF.closeBatchNf(this.securityToken, batchNFId, true, this.stationId, true, siteId, true,
                                             workflowId, true,
                                             this.usrId, true, docsCanceled);
        }

        public bool SetBatchTransmittedNF(long batchNFId, long workflowId, int totFiles)
        {
            try
            {
                this.proxyCaptureNF.setBatchNfTransmitted(this.securityToken, batchNFId, true, workflowId, true,
                                                          this.stationId, true,
                                                          this.usrId, true);
            }
            catch (Exception e)
            {
                this.log.Error("SetBatchTransmitted: " + batchNFId + " : " + e);
                return false;
            }
            return true;
        }

        public long[] GetBatchNFToRecapture()
        {
            return this.proxyCaptureNF.getBatchNfToRecapture(this.securityToken, this.usrId, true, this.stationId, true);
        }

        public documentNfDto[] GetDocumentNFToRecapture(long batchNFId)
        {
            return this.proxyCaptureNF.getDocumentNfToRecapture(this.securityToken, this.usrId, true, batchNFId, true);
        }

        public void setDocNFRecapture(long documentNFIdOld, long documentNFIdNew, long batchNFId)
        {
            this.proxyCaptureNF.setDocNfRecapture(this.securityToken, documentNFIdOld, true, documentNFIdNew, true,
                                                  batchNFId,
                                                  true, this.stationId, true, this.usrId, true);
        }
    }
}