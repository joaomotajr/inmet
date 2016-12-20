using FlexImage.Core;
using FlexImage.WebService.MonitorWebReference;

namespace FlexImage.WebService
{
    public class WSMonitor
    {
        public WorkflowMonitorWSService ProxyMonitor = new WorkflowMonitorWSService();

        private string endPoint = "";

        public string EndPoint
        {
            get { return endPoint; }
            set
            {
                endPoint = value;
                ProxyMonitor.Url = endPoint + "WorkflowMonitorService";
            }
        }
        
        public getMonitorByWorkflowIdResponse getMonitorByWorkflowId(long workflowId)
        {
            var param = new getMonitorByWorkflowId();
            var response = new getMonitorByWorkflowIdResponse();

            param.workflowId = workflowId;
            param.workflowIdSpecified = true;

            response = ProxyMonitor.getMonitorByWorkflowId(param);
            return response;
        }

        public workflowMonitorDTO[] getWorkflowsMonitor()
        {
            var param = new getWorkflowsMonitor();
            workflowMonitorDTO[] response;

            response = ProxyMonitor.getWorkflowsMonitor(param);
            return response;
        }

        public phaseDTO[] findPhaseByWorkflowId(long workflowId)
        {
            var param = new findPhaseByWorkflowId();
            phaseDTO[] response;

            param.workflowId = workflowId;
            param.workflowIdSpecified = true;

            response = ProxyMonitor.findPhaseByWorkflowId(param);
            return response;
        }

        public batchNfMonitorDTO[] findBatchNfMonitorBySiteId(long siteId)
        {
            var param = new findBatchNfMonitorBySiteId();

            batchNfMonitorDTO[] response;

            param.siteId = siteId;
            param.siteIdSpecified = true;

            response = ProxyMonitor.findBatchNfMonitorBySiteId(param);

            return response;
        }

        public getMonitorByWorkflowIdResponse GetWorkflowSyntheticData(long workflowId)
        {
            var response = new getMonitorByWorkflowIdResponse();
            var param = new getMonitorByWorkflowId();

            param.workflowId = workflowId;
            param.workflowIdSpecified = true;

            response = ProxyMonitor.getMonitorByWorkflowId(param);
            return response;
        }

    }
}