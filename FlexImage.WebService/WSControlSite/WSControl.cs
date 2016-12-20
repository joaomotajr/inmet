using System;
using FlexImage.Core;
using FlexImage.WebService.ControlWebReference;

namespace FlexImage.WebService
{
    public class WSControl
    {
        public ControlWSService ProxyControl = new ControlWSService();

        private string endPoint = "";

        public string EndPoint
        {
            get { return endPoint; }
            set
            {
                endPoint = value;
                ProxyControl.Url = endPoint + "ControlService";
            }
        }


        public bool IsControlOpen()
        {
            var checkControl1 = new checkControl();
            var checkControlResponse1 = new checkControlResponse();
            try
            {
                checkControlResponse1 = ProxyControl.checkControl(checkControl1);
                if (checkControlResponse1.@return)
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
                getSuggestedOpenDayResponse1 = ProxyControl.getSuggestedOpenDay(getSuggestedOpenDay1);
            }
            catch (Exception e)
            {
                Alert.Exclamation("GetSuggestedOpenDay::Error:" + e);
                return DateTime.Now;
            }
            return getSuggestedOpenDayResponse1.@return;
        }

        public DateTime GetCurrentControl()
        {
            var getCurrentControl1 = new getCurrentControl();
            var getCurrentControlResponse1 = new getCurrentControlResponse();

            try
            {
                getCurrentControlResponse1 = ProxyControl.getCurrentControl(getCurrentControl1);
            }
            catch (Exception e)
            {
                Alert.Exclamation("GetCurrentControl::Error:" + e);
                return DateTime.Now;
            }
            return getCurrentControlResponse1.@return;
        }

        public bool DoOpen(DateTime dt, long userId, long stationId)
        {
            var openControl1 = new openControl();
            var openControlResponse1 = new openControlResponse();

            try
            {
                openControl1.controlDate = dt;
                openControl1.controlDateSpecified = true;
                openControl1.usrId = userId;
                openControl1.usrIdSpecified = true;
                openControl1.stationId = stationId;
                openControl1.stationIdSpecified = true;
                openControlResponse1 = ProxyControl.openControl(openControl1);
            }
            catch (Exception e)
            {
                Alert.Exclamation("ControlWS::DoOpen::Error:" + e);
                return false;
            }

            return true;
        }

        public bool DoClose(DateTime dt, long userId, long stationId)
        {
            var closeControl1 = new closeControl();
            var closeControlResponse1 = new closeControlResponse();

            try
            {
                closeControl1.controlDate = dt;
                closeControl1.controlDateSpecified = true;
                closeControl1.usrId = userId;
                closeControl1.usrIdSpecified = true;
                closeControl1.stationId = stationId;
                closeControl1.stationIdSpecified = true;
                closeControlResponse1 = ProxyControl.closeControl(closeControl1);
            }
            catch (Exception e)
            {
                Alert.Exclamation("ControlWS::DoClose::Error:" + e);
                return false;
            }

            return true;
        }
    }
}