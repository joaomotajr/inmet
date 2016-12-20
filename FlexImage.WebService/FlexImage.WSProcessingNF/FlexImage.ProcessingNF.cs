// ============================================
// 
// FlexImage.WSProcessingNF
// FlexImage.ProcessingNF.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using FlexImage.WebService.ProcessingNFWebReference;

#endregion

namespace FlexImage.WebService
{
    public class WsProcessingNf
    {
        private readonly ProcessingNfService _proxyProcessingNf = new ProcessingNfService();

        private string _securityToken = "";
        private long _stationId;

        private int _timeOut;
        private long _usrId;

        public int TimeOut
        {
            get { return this._timeOut; }
            set
            {
                this._timeOut = value;
                this._proxyProcessingNf.Timeout = value;
            }
        }


        public string Url
        {
            set { this._proxyProcessingNf.Url = value; }
        }

        public string SecurityToken
        {
            set { this._securityToken = value; }
        }

        public long UsrId
        {
            set { this._usrId = value; }
        }

        public long StationId
        {
            set { this._stationId = value; }
        }

        public void ProcessIndex(long documentNfId, long typeId, docKeyDto[] keys, long batchNfId, int batchNfTotalDocs,
                                 long workflowId)
        {
            this._proxyProcessingNf.processIndex(this._securityToken, documentNfId, true, typeId, true, keys, batchNfId,
                                                 true,
                                                 batchNfTotalDocs, true, workflowId, true, this._usrId, true,
                                                 this._stationId, true);
        }

        public void ProcessPreIndex(long documentNfId, long predocumentId, long batchNfId, int batchNfTotalDocs,
                                    long workflowId)
        {
            this._proxyProcessingNf.processPreIndex(this._securityToken, documentNfId, true, predocumentId, true,
                                                    batchNfId, true,
                                                    batchNfTotalDocs, true, workflowId, true, this._usrId, true,
                                                    this._stationId, true);
        }

        public void ProcessOcr(long documentNfId, long batchNfId, int batchNfTotalDocs, long workflowId, string fileType, string hashFile, int fileSize)
        {
            this._proxyProcessingNf.processOcr(this._securityToken, documentNfId, true, batchNfId, true,
                                               batchNfTotalDocs, true, workflowId, true, fileType, fileSize, true, hashFile, this._usrId, true, this._stationId, true);
        }

        public void ProcessOcrFieldCaderneta(long documentNfId, long batchNfId, int batchNfTotalDocs, long workflowId, string xml)
        {
            this._proxyProcessingNf.processOcrFieldCaderneta(this._securityToken, documentNfId, true, batchNfId, true, batchNfTotalDocs, true, workflowId, true, xml, this._usrId, true, this._stationId, true);
        }

        public void ReprocessBatchNf(long batchNfId, long documentNfId)
        {
            this._proxyProcessingNf.reprocessBatchNf(batchNfId, true, documentNfId, true);
        }
    }
}