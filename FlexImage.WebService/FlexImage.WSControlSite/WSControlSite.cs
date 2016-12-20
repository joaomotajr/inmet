using System;
using FlexImage.Core;
using FlexImage.WebService.ControlSiteWebReference;

namespace FlexImage.WebService
{
    public class WSControlSite
    {
        public SiteControlWSService ProxyControlSite = new SiteControlWSService();

        private string endPoint = "";

        public string EndPoint
        {
            get { return endPoint; }
            set
            {
                endPoint = value;
                ProxyControlSite.Url = endPoint + "SiteControlService";
            }
        }

        public bool IsSiteControlOpen(long siteId)
        {
            var checkSiteControl1 = new checkSiteControl();
            var checkSiteControlResponse1 = new checkSiteControlResponse();

            checkSiteControl1.siteId = siteId;
            checkSiteControl1.siteIdSpecified = true;

            try
            {
                checkSiteControlResponse1 = ProxyControlSite.checkSiteControl(checkSiteControl1);
                if (checkSiteControlResponse1.@return != 0)
                    return true;
            }
            catch (Exception e)
            {
                Alert.Exclamation("DoClose::Error:" + e);
            }

            return false;
        }

        public DateTime GetSuggestedOpenDay()
        {
            var getSuggestedCloseDay1 = new getSuggestedCloseDay();
            var getSuggestedCloseDayResponse1 = new getSuggestedCloseDayResponse();

            var getSuggestedOpenDay1 = new getSuggestedOpenDay();
            var getSuggestedOpenDayResponse1 = new getSuggestedOpenDayResponse();

            try
            {
                getSuggestedOpenDayResponse1 = ProxyControlSite.getSuggestedOpenDay(getSuggestedOpenDay1);
            }
            catch (Exception e)
            {
                Alert.Exclamation("DoClose::Error:" + e);
                return DateTime.Now;
            }
            return getSuggestedOpenDayResponse1.@return;
        }

        public bool DoOpen(DateTime dt, long siteId, long userId, long stationId)
        {
            var openSiteControl1 = new openSiteControl();
            var openSiteControlResponse1 = new openSiteControlResponse();

            try
            {
                openSiteControl1.siteId = siteId;
                openSiteControl1.siteIdSpecified = true;
                openSiteControl1.controlDate = dt;
                openSiteControl1.controlDateSpecified = true;
                openSiteControl1.usrId = userId;
                openSiteControl1.usrIdSpecified = true;
                openSiteControl1.stationId = stationId;
                openSiteControl1.stationIdSpecified = true;
                openSiteControlResponse1 = ProxyControlSite.openSiteControl(openSiteControl1);
            }
            catch (Exception e)
            {
                Alert.Exclamation("SiteControlWS::DoOpen::Error:" + e);
                return false;
            }

            return true;
        }

        public bool DoClose(DateTime dt, long siteId, long userId, long stationId)
        {
            var closeSiteControl1 = new closeSiteControl();
            var closeSiteControlResponse1 = new closeSiteControlResponse();

            try
            {
                closeSiteControl1.siteId = siteId;
                closeSiteControl1.siteIdSpecified = true;
                closeSiteControl1.controlDate = dt;
                closeSiteControl1.controlDateSpecified = true;
                closeSiteControl1.usrId = userId;
                closeSiteControl1.usrIdSpecified = true;
                closeSiteControl1.stationId = stationId;
                closeSiteControl1.stationIdSpecified = true;
                closeSiteControlResponse1 = ProxyControlSite.closeSiteControl(closeSiteControl1);
            }
            catch (Exception e)
            {
                Alert.Exclamation("SiteControlWS::DoClose::Error:" + e);
                return false;
            }

            return true;
        }
    }
}