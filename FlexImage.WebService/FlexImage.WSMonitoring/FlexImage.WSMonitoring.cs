// ============================================
// 
// FlexImage.WSMonitoring
// FlexImage.WSMonitoring.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using FlexImage.Core;
using FlexImage.WebService.MonitoringWebReference;

#endregion

namespace FlexImage.WebService
{
    public class WSMonitoring
    {
        private readonly Log log = new Log();

        private readonly MonitoringService proxyMonitoring = new MonitoringService();
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
                this.proxyMonitoring.Timeout = value;
            }
        }


        public string Url
        {
            get { return this.proxyMonitoring.Url; }
            set { this.proxyMonitoring.Url = value; }
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

        public string Ping()
        {
            string ret = "";
            try
            {
                ret = this.proxyMonitoring.ping();
            }
            catch (Exception e)
            {
            }

            return ret;
        }
    }
}