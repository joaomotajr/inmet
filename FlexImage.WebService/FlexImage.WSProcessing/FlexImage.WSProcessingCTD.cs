// ============================================
// 
// FlexImage.WSProcessing
// FlexImage.WSProcessingCTD.cs
// 26/08/2012
// cflavio
// 
// ============================================

namespace FlexImage.WebService
{
    public partial class WSProcessing
    {
        public void processDoubleTypingDataBoaCtd(long documentId, long batchId, long typeId, int batchTotalDocs,
                                                  long workflowId, string xml, string hashXml, string documentNumber,
                                                  decimal value, int typingTime, int keyStroke, string descTypingLog)
        {
            this.proxyProcessing.processDoubleTypingDataBoaCtd(this.securityToken, documentId, true, batchId, true,
                                                               typeId, true, batchTotalDocs, true, workflowId, true, xml,
                                                               hashXml, documentNumber, value, true, typingTime, true,
                                                               keyStroke, true, descTypingLog, this.usrId, true,
                                                               this.stationId, true);
        }

        public void processDoubleTypingValCtd(long documentId, long batchId, long typeId, int batchTotalDocs,
                                              long workflowId, string xml, string hashXml, string documentNumber,
                                              decimal value, int typingTime, int keyStroke, string descTypingLog)
        {
            this.proxyProcessing.processDoubleTypingValCtd(this.securityToken, documentId, true, batchId, true, typeId,
                                                           true, batchTotalDocs, true, workflowId, true, xml, hashXml,
                                                           documentNumber, value, true, typingTime, true, keyStroke,
                                                           true, descTypingLog, this.usrId, true, this.stationId, true);
        }

        public void processTypingBorderoCtd(long documentId, long batchId, long typeId, int batchTotalDocs,
                                            long workflowId, string xml, string hashXml, string documentNumber,
                                            decimal value, int typingTime, int keyStroke, string descTypingLog)
        {
            this.proxyProcessing.processTypingBorderoCtd(this.securityToken, documentId, true, batchId, true, typeId,
                                                         true, batchTotalDocs, true, workflowId, true, xml, hashXml,
                                                         documentNumber, value, true, typingTime, true, keyStroke, true,
                                                         descTypingLog, this.usrId, true, this.stationId, true);
        }

        public void processTypingCmc7Ctd(long documentId, long batchId, long typeId, int batchTotalDocs, long workflowId,
                                         string xml, string hashXml, string documentNumber, decimal value,
                                         int typingTime, int keyStroke, string descTypingLog)
        {
            this.proxyProcessing.processTypingCmc7Ctd(this.securityToken, documentId, true, batchId, true, typeId, true,
                                                      batchTotalDocs, true, workflowId, true, xml, hashXml,
                                                      documentNumber, value, true, typingTime, true, keyStroke, true,
                                                      descTypingLog, this.usrId, true, this.stationId, true);
        }

        public void processTypingCpfCnpjCtd(long documentId, long batchId, long typeId, int batchTotalDocs,
                                            long workflowId, string xml, string hashXml, string documentNumber,
                                            decimal value, int typingTime, int keyStroke, string descTypingLog)
        {
            this.proxyProcessing.processTypingCpfCnpjCtd(this.securityToken, documentId, true, batchId, true, typeId,
                                                         true, batchTotalDocs, true, workflowId, true, xml, hashXml,
                                                         documentNumber, value, true, typingTime, true, keyStroke, true,
                                                         descTypingLog, this.usrId, true, this.stationId, true);
        }

        public void processTypingDataBoaCtd(long documentId, long batchId, long typeId, int batchTotalDocs,
                                            long workflowId, string xml, string hashXml, string documentNumber,
                                            decimal value, int typingTime, int keyStroke, string descTypingLog)
        {
            this.proxyProcessing.processTypingDataBoaCtd(this.securityToken, documentId, true, batchId, true, typeId,
                                                         true, batchTotalDocs, true, workflowId, true, xml, hashXml,
                                                         documentNumber, value, true, typingTime, true, keyStroke, true,
                                                         descTypingLog, this.usrId, true, this.stationId, true);
        }

        public void processTypingValCtd(long documentId, long batchId, long typeId, int batchTotalDocs, long workflowId,
                                        string xml, string hashXml, string documentNumber, decimal value, int typingTime,
                                        int keyStroke, string descTypingLog)
        {
            this.proxyProcessing.processTypingValCtd(this.securityToken, documentId, true, batchId, true, typeId, true,
                                                     batchTotalDocs, true, workflowId, true, xml, hashXml,
                                                     documentNumber, value, true, typingTime, true, keyStroke, true,
                                                     descTypingLog, this.usrId, true, this.stationId, true);
        }
    }
}