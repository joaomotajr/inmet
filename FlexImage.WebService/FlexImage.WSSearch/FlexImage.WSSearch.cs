// ============================================
// 
// FlexImage.WSSearch
// FlexImage.WSSearch.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using FlexImage.WebService.SearchWebReference;

#endregion

namespace FlexImage.WebService
{
    public class WSSearch
    {
        private readonly SearchService proxySearch = new SearchService();
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
                this.proxySearch.Timeout = value;
            }
        }


        public string Url
        {
            set { this.proxySearch.Url = value; }
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

        public genericSearchDto SimpleSearch(string text, int searchType, int startRec, int qtdeRows)
        {
            return this.proxySearch.simpleSearch(this.securityToken, text, searchType, true, startRec, true, qtdeRows,
                                                 true, this.usrId,
                                                 true, this.stationId, true);
        }

        public genericSearchDto AdvancedSearch(long batchId, long docId, long typeId, keyDto[] keys, int searchType,
                                               int start, int rows)
        {
            bool batchIdSpecified = (batchId > 0);
            bool docIdSpecified = (docId > 0);
            bool typeIdSpecfied = (typeId > 0);
            return this.proxySearch.advancedSearch(this.securityToken, batchId, batchIdSpecified, docId, docIdSpecified,
                                                   typeId,
                                                   typeIdSpecfied, keys, searchType, true, start, true, rows, true,
                                                   this.usrId,
                                                   true, this.stationId, true);
        }
    }
}