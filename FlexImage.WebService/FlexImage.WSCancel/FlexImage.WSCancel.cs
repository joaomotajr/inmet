// ============================================
// 
// FlexImage.WSCancel
// FlexImage.WSCancel.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using FlexImage.WebService.CancelWebReference;

#endregion

namespace FlexImage.WebService
{
    public class WSCancel
    {
        private readonly CancelService proxyCancel = new CancelService();
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
                this.proxyCancel.Timeout = value;
            }
        }


        public string Url
        {
            set { this.proxyCancel.Url = value; }
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

        public void CancelBatch(long batchId, string reason)
        {
            this.proxyCancel.cancelBatch(this.securityToken, batchId, true, reason, this.usrId, true, this.stationId,
                                         true);
        }

        public void CancelDocument(long documentId, string reason)
        {
            this.proxyCancel.cancelDocument(this.securityToken, documentId, true, reason, this.usrId, true,
                                            this.stationId, true);
        }

        public void CancelTrans(long transId, string reason)
        {
            this.proxyCancel.cancelTransaction(this.securityToken, transId, true, reason, this.usrId, true,
                                               this.stationId, true);
        }
    }
}