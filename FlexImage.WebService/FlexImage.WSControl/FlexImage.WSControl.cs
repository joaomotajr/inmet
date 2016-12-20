// ============================================
// 
// FlexImage.WSControl
// FlexImage.WSControl.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using FlexImage.WebService.ControlWebReference;

#endregion

namespace FlexImage.WebService
{
    public class WSControl
    {
        private readonly ControlService proxyControl = new ControlService();
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
                this.proxyControl.Timeout = value;
            }
        }


        public string Url
        {
            set { this.proxyControl.Url = value; }
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

        public bool IsControlOpen()
        {
            bool ret = this.proxyControl.checkControl(this.securityToken, this.usrId, true);
            return ret;
        }

        public string GetControlNextOpenDay()
        {
            string dt = this.proxyControl.getControlNextOpenDay(this.securityToken, this.usrId, true);
            return dt;
        }

        public string GetCurrentControl()
        {
            string dt = this.proxyControl.getCurrentControl(this.securityToken, this.usrId, true);
            return dt;
        }

        public bool OpenControl(DateTime dt)
        {
            try
            {
                this.proxyControl.openControl(this.securityToken, dt, true, this.usrId, true, this.stationId, true);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public string GetControlSuggestedCloseDay()
        {
            string dt = this.proxyControl.getControlSuggestedCloseDay(this.securityToken, this.usrId, true);
            return dt;
        }

        public void CloseControl(DateTime dt)
        {
            this.proxyControl.closeControl(this.securityToken, dt, true, this.usrId, true, this.stationId, true);
            return;
        }

        public void CloseSiteControl(long siteId, DateTime dt)
        {
            this.proxyControl.closeSiteControl(this.securityToken, siteId, true, dt, true, this.usrId, true,
                                               this.stationId, true);
            return;
        }

        public bool OpenSiteControl(long siteId, DateTime dt)
        {
            try
            {
                this.proxyControl.openSiteControl(this.securityToken, siteId, true, dt, true, this.usrId, true,
                                                  this.stationId, true);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool IsSiteOpen(long siteId)
        {
            return this.proxyControl.checkSiteMov(this.securityToken, this.usrId, true, siteId, true);
        }

        private void teste()
        {
            //proxyControl.checkControl
            //proxyControl.closeControl
            //proxyControl.findAllControl
            //proxyControl.getControl
            //proxyControl.getControlNextOpenDay
            //proxyControl.getControlSuggestedCloseDay
            //proxyControl.getCurrentControl
            //proxyControl.getOpenedControlDate
            //proxyControl.openControl


            //proxyControl.checkSiteMov
            //proxyControl.checkSitesOpened            
            //proxyControl.findAllSiteControl
            //proxyControl.getSiteControlBySiteId
            //proxyControl.getSiteControlNextOpenDay
            //proxyControl.openSiteControl          
        }
    }
}