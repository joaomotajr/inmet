// ============================================
// 
// FlexImage.WSSecurity
// FlexImage.WSSecurity.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using FlexImage.Core;
using FlexImage.WebService.SecurityWebReference;

#endregion

namespace FlexImage.WebService
{
    public class WSSecurity
    {
        private readonly SecurityService proxySecurity = new SecurityService();
        private int _timeOut;

        private string securityToken = "";
        private long stationId;
        private long[] usrGroupsIds;

        private long usrId;
        public usrSession usrSession;

        public int TimeOut
        {
            get { return this._timeOut; }
            set
            {
                this._timeOut = value;
                this.proxySecurity.Timeout = value;
            }
        }

        public string Url
        {
            set { this.proxySecurity.Url = value; }
        }

        public string SecurityToken
        {
            set { this.securityToken = value; }
            get { return this.securityToken; }
        }

        public long UsrId
        {
            set { this.usrId = value; }
            get { return this.usrId; }
        }

        public long StationId
        {
            set { this.stationId = value; }
            get { return this.stationId; }
        }

        public long[] UsrGroupsIds
        {
            get { return this.usrGroupsIds; }
            set { this.usrGroupsIds = value; }
        }

        public usrSession Login(string userName, string hashPassword, string stationName, string ip)
        {
            try
            {
                this.usrSession = this.proxySecurity.login(userName, hashPassword, stationName, ip);
            }
            catch (Exception e)
            {
                return this.usrSession;
            }

            if (this.usrSession.usrGroupIds != null)
                this.usrGroupsIds = ConvertNullable.ConvertNullableLongArrayToLongArray(this.usrSession.usrGroupIds);
            this.stationId = this.usrSession.stationId;
            this.usrId = this.usrSession.usrId;
            this.securityToken = this.usrSession.securityToken;

            return this.usrSession;
        }

        public moduleDto[] findModuleByUsrGroupsId(long[] usrGroupId)
        {
            moduleDto[] modules = this.proxySecurity.findModuleByUsrGroupsId(this.securityToken, this.usrId, true, usrGroupId);
            return modules;
        }

        public string GetModulesXML(long[] usrGroupId)
        {
            string xml = this.proxySecurity.getModulesXML(this.securityToken, this.usrId, true, usrGroupId);
            return xml;
        }

        public bool AuthenticUsr(string usrName, string usrPassword)
        {
            return this.proxySecurity.authenticUsr(this.securityToken, usrName, usrPassword);
        }

        public string GetRandomSaltKey(string usrInternalId)
        {
            return this.proxySecurity.getRandomSaltKey(usrInternalId);
        }
    }
}