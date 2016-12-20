// ============================================
// 
// FlexImage.WSManager
// FlexImage.WSManager.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using FlexImage.Core;

#endregion

namespace FlexImage.WebService
{
    public delegate void ConnectionInfo(string text);

    public class WSManager
    {
        private readonly Log log = new Log();

        public readonly WSBasicTable WsBasicTable = GenericSingleton<WSBasicTable>.GetInstance();
        public readonly WSCaptureNF WsCaptureNf = GenericSingleton<WSCaptureNF>.GetInstance();
        public readonly WSDocumentNF WsDocumentNf = GenericSingleton<WSDocumentNF>.GetInstance();
        public readonly WsProcessingNf WsProcessingNf = GenericSingleton<WsProcessingNf>.GetInstance();
        public readonly WSQueueNF WsQueueNf = GenericSingleton<WSQueueNF>.GetInstance();
        public readonly WSRepository WsRepository = GenericSingleton<WSRepository>.GetInstance();
        public readonly WSSecurity WsSecurity = GenericSingleton<WSSecurity>.GetInstance();
        public readonly WSMonitoring WsMonitoring = GenericSingleton<WSMonitoring>.GetInstance();
        public readonly WSCancelNF WsCancelNf = GenericSingleton<WSCancelNF>.GetInstance();

        private string _endPoint = "";
        private string _ip = "";
        private string _password = "";
        private string _port = "";

        private string _protocol = "";
        private string _securityToken = "";

        public string Protocol
        {
            get { return this._protocol; }
            set { this._protocol = value; }
        }

        public string Ip
        {
            get { return this._ip; }
            set { this._ip = value; }
        }

        public string Port
        {
            get { return this._port; }
            set { this._port = value; }
        }

        public string Password
        {
            get { return this._password; }
            set { this._password = value; }
        }

        public long StationId { get; set; }

        public long UsrId { get; set; }

        public string SecurityToken
        {
            get { return this._securityToken; }
            set { this._securityToken = value; }
        }

        public event ConnectionInfo OnConnected;
        public event ConnectionInfo OnInfo;


        public string GetEndPoint()
        {
            return this._endPoint;
        }

        public void SetSecurity(string securityToken, long usrId, long stationId)
        {
            this.SecurityToken = securityToken;
            this.UsrId = usrId;
            this.StationId = stationId;

            this.WsMonitoring.SecurityToken = securityToken;
            this.WsBasicTable.SecurityToken = securityToken;
            this.WsRepository.SecurityToken = securityToken;
            this.WsSecurity.SecurityToken = securityToken;
            this.WsQueueNf.SecurityToken = securityToken;
            this.WsDocumentNf.SecurityToken = securityToken;
            this.WsCaptureNf.SecurityToken = securityToken;
            this.WsProcessingNf.SecurityToken = securityToken;
            this.WsCancelNf.SecurityToken = securityToken;

            this.UsrId = usrId;
            this.WsMonitoring.UsrId = usrId;
            this.WsBasicTable.UsrId = usrId;
            this.WsRepository.UsrId = usrId;
            this.WsSecurity.UsrId = usrId;
            this.WsQueueNf.UsrId = usrId;
            this.WsDocumentNf.UsrId = usrId;
            this.WsCaptureNf.UsrId = usrId;
            this.WsProcessingNf.UsrId = usrId;
            this.WsCancelNf.UsrId = usrId;
            
            this.StationId = stationId;
            this.WsMonitoring.StationId = stationId;
            this.WsBasicTable.StationId = stationId;
            this.WsRepository.StationId = stationId;
            this.WsSecurity.StationId = stationId;
            this.WsQueueNf.StationId = stationId;
            this.WsCaptureNf.StationId = stationId;
            this.WsDocumentNf.StationId = stationId;
            this.WsProcessingNf.StationId = stationId;
            this.WsCancelNf.StationId = stationId;            
        }

        public void SetEndPoint(string endPoint)
        {
            log.Debug("SetEndpoint Begin:" + endPoint);

            this._endPoint = endPoint;
            this.WsMonitoring.Url = endPoint + "MonitoringService?wsdl";
            this.WsBasicTable.Url = endPoint + "BasicTableService?wsdl";
            this.WsRepository.Url = endPoint + "RepositoryService?wsdl";
            this.WsSecurity.Url = endPoint + "SecurityService?wsdl";
            this.WsQueueNf.Url = endPoint + "QueueNfService?wsdl";
            this.WsDocumentNf.Url = endPoint + "DocumentNfService?wsdl";
            this.WsCaptureNf.Url = endPoint + "CaptureNfService?wsdl";
            this.WsProcessingNf.Url = endPoint + "ProcessingNfService?wsdl";
            this.WsCancelNf.Url = endPoint + "CancelNfService?wsdl";
            
            //TIMEOUT
            int t1 = 5*60*1000; //5 minutos
            int t2 = 10*60*1000; //10 minutos
            int t3 = 30*1000; //30 segs

            this.WsMonitoring.TimeOut = t1;
            this.WsBasicTable.TimeOut = t1;
            this.WsSecurity.TimeOut = t1;
            this.WsQueueNf.TimeOut = t1;
            this.WsDocumentNf.TimeOut = t1;
            this.WsCaptureNf.TimeOut = t1;
            this.WsProcessingNf.TimeOut = t1;
            this.WsRepository.TimeOut = t2;
            this.WsCancelNf.TimeOut = t2;

            log.Debug("SetEndpoint End");
        }

        public bool SelectServer()
        {
            log.Debug("Begin:SelectServer");

            var selectEnv = new SelectEnvironment();

            //if (selectEnv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            if (!string.IsNullOrEmpty(selectEnv.Service))
            {
                string endPoint = selectEnv.Protocol + "://" + selectEnv.Server + ":" + selectEnv.Port + selectEnv.Service;
                if (endPoint.Substring(endPoint.Length - 1, 1) != "/")
                    endPoint = endPoint + "/";

                log.Debug("SelectServer:EndPoint:" + endPoint);

                this.SetEndPoint(endPoint);

                this._protocol = selectEnv.Protocol;
                this._ip = selectEnv.Server;
                this._port = selectEnv.Port;

                log.Debug("WsMonitoring:" + this.WsMonitoring.Url.ToString());
                string ret = this.WsMonitoring.Ping();

                log.Debug("Ping:" + ret);

                if (ret != "")
                {
                    log.Debug("OnConnected");

                    if (this.OnConnected != null)
                        this.OnConnected(endPoint);

                    selectEnv.Close();
                    return true;
                }
            }
            
            selectEnv.Close();

            log.Debug("End:SelectServer");


            return false;
        }
    }
}