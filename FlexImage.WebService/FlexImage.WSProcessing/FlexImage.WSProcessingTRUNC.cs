// ============================================
// 
// FlexImage.WSProcessing
// FlexImage.WSProcessingTRUNC.cs
// 26/08/2012
// cflavio
// 
// ============================================

namespace FlexImage.WebService
{
    public partial class WSProcessing
    {
        public void processTypingBorderoTrunc(long documentId, long batchId, long typeId, int batchTotalDocs,
                                              long workflowId, string xml, string hashXml, string documentNumber,
                                              decimal value, int typingTime, int keyStroke, string descTypingLog)
        {
            this.proxyProcessing.processTypingBorderoTrunc(this.securityToken, documentId, true, batchId, true, typeId,
                                                           true, batchTotalDocs, true, workflowId, true, xml, hashXml,
                                                           documentNumber, value, true, typingTime, true, keyStroke,
                                                           true, descTypingLog, this.usrId, true, this.stationId, true);
        }

        public void processTypingCmc7Trunc(long documentId, long batchId, long typeId, int batchTotalDocs,
                                           long workflowId, string xml, string hashXml, string documentNumber,
                                           decimal value, int typingTime, int keyStroke, string descTypingLog)
        {
            this.proxyProcessing.processTypingCmc7Trunc(this.securityToken, documentId, true, batchId, true, typeId,
                                                        true, batchTotalDocs, true, workflowId, true, xml, hashXml,
                                                        documentNumber, value, true, typingTime, true, keyStroke, true,
                                                        descTypingLog, this.usrId, true, this.stationId, true);
        }

        public void processTypingValTrunc(long documentId, long batchId, long typeId, int batchTotalDocs,
                                          long workflowId, string xml, string hashXml, string documentNumber,
                                          decimal value, int typingTime, int keyStroke, string descTypingLog)
        {
            this.proxyProcessing.processTypingValTrunc(this.securityToken, documentId, true, batchId, true, typeId, true,
                                                       batchTotalDocs, true, workflowId, true, xml, hashXml,
                                                       documentNumber, value, true, typingTime, true, keyStroke, true,
                                                       descTypingLog, this.usrId, true, this.stationId, true);
        }
    }
}