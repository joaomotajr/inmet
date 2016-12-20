// ============================================
// 
// FlexImage.WSQueueNF
// FlexImage.QueueNF.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using FlexImage.WebService.QueueNFWebReference;

#endregion

namespace FlexImage.WebService
{
    public class WSQueueNF
    {
        private readonly QueueNfService proxyQueueNF = new QueueNfService();
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
                this.proxyQueueNF.Timeout = value;
            }
        }

        public string Url
        {
            set { this.proxyQueueNF.Url = value; }
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

        public long[] UsrGroupsIds { get; set; }

        public docNfQueueDto[] GetDocsNfInQueue(long queueId, int numObjs)
        {
            return this.proxyQueueNF.getDocsNfInQueue(this.securityToken, this.usrId, true, queueId, true, numObjs, true);
        }

        //public void AbortDocsInQueue(long queueId, docQueueDto docQueue)
        //{
        //    proxyQueue.abortDocsInQueue(securityToken, usrId, true, queueId, true, docQueue);
        //}

        //public long GetAutomaticQueue()
        //{
        //    return proxyQueue.getAutomaticQueue(securityToken, this.usrGroupsIds);
        //}
    }
}