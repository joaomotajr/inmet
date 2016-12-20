// ============================================
// 
// FlexImage.WSQueue
// FlexImage.WSQueue.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using FlexImage.WebService.QueueWebReference;

#endregion

namespace FlexImage.WebService
{
    public class WSQueue
    {
        private readonly QueueFService proxyQueue = new QueueFService();
        private int _timeOut;

        private docQueueDto[] docQueueDto;

        private string securityToken = "";
        private long stationId;
        private long[] usrGroupsIds;

        private long usrId;

        public int TimeOut
        {
            get { return this._timeOut; }
            set
            {
                this._timeOut = value;
                this.proxyQueue.Timeout = value;
            }
        }


        public string Url
        {
            set { this.proxyQueue.Url = value; }
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

        public long[] UsrGroupsIds
        {
            get { return this.usrGroupsIds; }
            set { this.usrGroupsIds = value; }
        }

        public docQueueDto[] GetDocsInQueue(long queueId, int numObjs)
        {
            return this.proxyQueue.getDocsInQueue(this.securityToken, this.usrId, true, queueId, true, numObjs, true);
        }

        public robotQueueDto[] GetRobotInQueue()
        {
            return this.proxyQueue.getRobotInQueue(this.securityToken, this.usrId, true, this.stationId, true);
        }

        public void AbortDocsInQueue(long queueId, docQueueDto docQueue)
        {
            this.proxyQueue.abortDocsInQueue(this.securityToken, this.usrId, true, queueId, true, docQueue);
        }

        public void AbortTransInQueue(long queueId, transQueueDto trans)
        {
            this.proxyQueue.abortTransInQueue(this.securityToken, this.usrId, true, queueId, true, trans);
        }

        public void AbortBatchInQueue(long queueId, batchQueueDto batchQueue)
        {
            this.proxyQueue.abortBatchInQueue(this.securityToken, this.usrId, true, queueId, true, batchQueue);
        }


        public long GetAutomaticQueue()
        {
            return this.proxyQueue.getAutomaticQueue(this.securityToken, this.usrId, true, this.usrGroupsIds);
        }

        public batchQueueDto[] GetBatchInQueue(long siteId, long queueId, int numObjs)
        {
            return this.proxyQueue.getBatchInQueue(securityToken, usrId, true, siteId, true, queueId, true, numObjs,
                                                   true);
        }

        public transQueueDto[] GetTransInQueue(long siteId, long queueId, int numObjs)
        {
            return this.proxyQueue.getTransInQueue(this.securityToken, this.usrId, true, siteId, true, queueId, true, numObjs, true);
        }

        public queueMonitoringDto[] GetMonitoring()
        {
            return this.proxyQueue.getMonitoring(this.securityToken, this.usrId, true);
        }

        //=================================================
        //COMMON
        //=================================================

        public transQueueDto RefreshTrans(long transId)
        {
            return this.proxyQueue.refreshTrans(this.securityToken, this.usrId, true, transId, true);            
        }

        public docQueueDto[] GetDocsInQueueForSystem(long queueId, int numObjs)
        {
            return this.proxyQueue.getDocsInQueueForSystem(securityToken, this.usrId, true, queueId, true, numObjs, true);                
        }

        public docQueueDto[] GetDocsInQueueByWorkflow(long queueId, int numObjs, long workflowId)
        {
            return this.proxyQueue.getDocsInQueueByWorkflow(securityToken, usrId, true, queueId, true, workflowId, true, numObjs, true);
        }
    }
}