// ============================================
// 
// FlexImage.WSDocumentNF
// FlexImage.WSDocumentNF.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using FlexImage.WebService.DocumentNFWebReference;

#endregion

namespace FlexImage.WebService
{
    public class WSDocumentNF
    {
        private readonly DocumentNfService proxyDocumentNF = new DocumentNfService();
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
                this.proxyDocumentNF.Timeout = value;
            }
        }


        public string Url
        {
            set { this.proxyDocumentNF.Url = value; }
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

        public long[] GetDocsNFId(int cache)
        {
            long[] ret = this.proxyDocumentNF.getDocsNfId(this.securityToken, this.usrId, true, cache, true);
            return ret;
        }

        public documentNfDto[] GetListDocsForBkp(long docInitial, int length)
        {
            return this.proxyDocumentNF.getListDocsForBkp(this.securityToken, this.usrId, true, docInitial, true, length, true);
        }

        public documentNfDto[] GetListDocsForBkpByDate(System.DateTime dataInicial, System.DateTime dataFinal)
        {
            return this.proxyDocumentNF.getListDocsForBkpByDate(this.securityToken, this.usrId, true, dataInicial, true, dataFinal, true);
        }

        public documentNfDto[] GetListDocsForBkp(long typeId, long docInitial)
        {
            return this.proxyDocumentNF.getListDocsForBkpByType(this.securityToken, this.usrId, true, typeId, true, docInitial, true);
        }

        public long InsertPredocument(long typeId, preDocKeyDto[] preDocKeyDtoList)
        {
            return this.proxyDocumentNF.insertPredocument(this.securityToken, this.usrId, true, typeId, true, preDocKeyDtoList);
        }

        public void IndexDocumentNf(long typeId, long documentNfid, docKeyDto[] dockeys, bool autoIndexed)
        {
            proxyDocumentNF.indexDocumentNf(securityToken, documentNfid, true, typeId, true, dockeys, usrId, true, stationId, true, autoIndexed);
        }

        public long OpenBackups(string desc, long mediaTypeId)
        {
            return this.proxyDocumentNF.openBackups(this.securityToken, this.usrId, true, desc, mediaTypeId, true);
        }

        public void SetDocumentBackuped(long documentNfId, long backupId)
        {
            this.proxyDocumentNF.setDocumentBackuped(this.securityToken, this.usrId, true, documentNfId, true, backupId, true);
        }

        public void CloseBackups(long backupId, int totalDocs, int totalBytes)
        {
            this.proxyDocumentNF.closeBackups(this.securityToken, this.usrId, true, backupId, true, totalDocs, true, totalBytes, true);
        }

        public long GetOpenBackups()
        {
            return this.proxyDocumentNF.getOpenBackup(this.securityToken, this.usrId, true);
        }

        public documentNfDto GetDocumentNf(long documentNfId)
        {
            return this.proxyDocumentNF.getDocumentNf(securityToken, this.usrId, true, documentNfId, true);
        }

        public bool IsCapa(long documentNfId, long batchNfId)
        {
            return this.proxyDocumentNF.isCapa(this.securityToken, this.usrId, true, documentNfId, true, batchNfId, true);
        }
    }
}