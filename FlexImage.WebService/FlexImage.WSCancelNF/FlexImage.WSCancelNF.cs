// ============================================
// 
// FlexImage.WSCancelNF
// FlexImage.WSCancelNF.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using FlexImage.WebService.CancelNFWebReference;

#endregion

namespace FlexImage.WebService
{
    public class WSCancelNF
    {
        private readonly CancelNfService proxyCancelNF = new CancelNfService();
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
                this.proxyCancelNF.Timeout = value;
            }
        }


        public string Url
        {
            set { this.proxyCancelNF.Url = value; }
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

        public void CancelBatchNF(long batchNFId, string reason)
        {
            this.proxyCancelNF.cancelBatchNf(this.securityToken, batchNFId, true, reason, this.usrId, true,
                                             this.stationId, true);
        }

        public void CancelDocumentNF(long documentNFId, string reason)
        {
            this.proxyCancelNF.cancelDocumentNf(this.securityToken, documentNFId, true, reason, this.usrId, true,
                                                this.stationId, true);
        }
    }
}