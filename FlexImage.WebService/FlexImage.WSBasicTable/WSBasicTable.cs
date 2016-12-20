// ============================================
// 
// FlexImage.WSBasicTable
// WSBasicTable.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using FlexImage.WebService.BasicTableWebReference;

#endregion

namespace FlexImage.WebService
{
    public class WSBasicTable
    {
        private readonly BasicTableService proxyBasicTable = new BasicTableService();
        private int _timeOut;

        private bool isParameterIntialized;
        private paramDto[] parameters;

        private string securityToken = "";
        private long stationId;

        private long usrId;

        public int TimeOut
        {
            get { return this._timeOut; }
            set
            {
                this._timeOut = value;
                this.proxyBasicTable.Timeout = value;
            }
        }


        public string Url
        {
            set { this.proxyBasicTable.Url = value; }
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

        public void ReadParameters()
        {
            this.parameters = this.proxyBasicTable.getAllParam();
            this.isParameterIntialized = true;
        }

        public string GetParameters(string paramName, string defaultValue)
        {
            string ret = "";
            bool paramExist = false;

            if (!this.isParameterIntialized)
                this.ReadParameters();

            if (this.parameters != null)
            {
                foreach (paramDto param in this.parameters)
                {
                    if (param.paramAlias == paramName)
                    {
                        ret = param.paramValue;
                        paramExist = true;
                        break;
                    }
                }
            }

            if (!paramExist)
            {
                //if (defaultValue == "")
                //FlexImage.Core.Alert.Exclamation("Parâmetro [" + paramName + "] não configurado no sistema!");
                //else
                ret = defaultValue;
            }

            return ret;
        }

        public byte[] GetOrganizationLogo()
        {
            return this.proxyBasicTable.getOrganizationLogo();
        }

        public workflowDto[] GetWorkflowByUsrGroups(long[] usrGroupIds)
        {
            return this.proxyBasicTable.getWorkflowByUsrGroups(this.securityToken, this.usrId, true, usrGroupIds);            
        }

        public workflowDto GetWorkflowById (long workflowId)
        {
            return this.proxyBasicTable.getWorkflowById(securityToken, this.usrId, true, workflowId, true);            
        }

        public typeDto[] GetTypesByWorkflowId(long workflowId)
        {
            return this.proxyBasicTable.getTypesByWorkflowId(this.securityToken, this.usrId, true, workflowId, true);
        }

        public reasonDto[] GetReasonsByReasonGroupAlias(string reasonGroupAlias)
        {
            return this.proxyBasicTable.getReasonsByReasonGroupAlias(this.securityToken, this.usrId, true, reasonGroupAlias);
        }

        public hwScanDto getHwScan(long hwScanId)
        {
            return this.proxyBasicTable.getHwScan(this.securityToken, this.usrId, true, hwScanId, true);
        }

        public queueDto[] getQueuesByUsrGroupsId(long[] usrGroupIds)
        {
            return this.proxyBasicTable.getQueuesByUsrGroupsId(this.securityToken, this.usrId, true, usrGroupIds);
        }

        public siteDto GetSiteById(long siteId)
        {
            return this.proxyBasicTable.getSiteById(this.securityToken, this.usrId, true, siteId, true);
        }

        public typeDto GetTypeByAlias(string typeAlias)
        {
            return this.proxyBasicTable.getTypeByAlias(this.securityToken, this.usrId, true, typeAlias);
        }

        public void SetParam(string paramAlias, string value)
        {
            this.proxyBasicTable.setParam(this.securityToken, paramAlias, value, this.usrId, true, this.stationId, true);
        }

        public void CreateParam(string paramGroupAlias, string paramGroupName, string paramAlias, string paramDesc,
                                int paramLogin, string paramValue)
        {
            this.proxyBasicTable.createParam(this.securityToken, paramGroupAlias, paramGroupName, paramAlias, paramDesc,
                                             paramLogin, true, paramValue, this.usrId, true, this.stationId, true);
        }

        public mediaTypeDto[] GetMediaType()
        {
            return this.proxyBasicTable.getMediaType(this.securityToken, this.usrId, true);
        }

        public long GetStationIdByName(string stationName)
        {
            return this.proxyBasicTable.getStationIdByName(securityToken, this.usrId, true, stationName);            
        }

        public string[] GetOcrTemplateList()
        {
            return this.proxyBasicTable.getOcrTemplateList(securityToken, this.usrId, true);            
        }

        public checkListDto[] GetCheckList(long workflowId)
        {
            return this.proxyBasicTable.getCheckList(securityToken, this.usrId, true, workflowId, true);
        }

        public clearingDto[] GetClearingList()
        {
            return this.proxyBasicTable.getClearingList(securityToken, this.usrId, true);
        }

        public bankDto[] GetBankList()
        {
            return this.proxyBasicTable.getBankList(securityToken, this.usrId, true);                    
        }

        public bool IsValidBranch(long bankId, long branchId, long clearingId)
        {
            return this.proxyBasicTable.isValidBranch(securityToken, this.usrId, true, bankId, true, branchId, true, clearingId, true);
        }
    }
}