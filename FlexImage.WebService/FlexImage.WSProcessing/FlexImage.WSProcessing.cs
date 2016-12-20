// ============================================
// 
// FlexImage.WSProcessing
// FlexImage.WSProcessing.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using FlexImage.WebService.ProcessingWebReference;

#endregion

namespace FlexImage.WebService
{
    public partial class WSProcessing
    {
        private readonly ProcessingFService proxyProcessing = new ProcessingFService();
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
                this.proxyProcessing.Timeout = value;
            }
        }

        public string Url
        {
            set { this.proxyProcessing.Url = value; }
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


        //=================================================
        //CLASSIFY
        //=================================================

        public void ProcessClassify(long documentId, long batchId, int batchTotalDocs, long workflowId, long typeId,
                                    string documentXml)
        {
            this.proxyProcessing.processClassify(this.securityToken, documentId, true, batchId, true, batchTotalDocs,
                                                 true,
                                                 workflowId, true, typeId, true, documentXml, this.usrId, true,
                                                 this.stationId, true);
            return;
        }

        //=================================================
        //TYPING
        //=================================================

        public void ProcessTyping(long documentId, long batchId, long typeId, int batchTotalDocs, long workflowId,
                                  string xml, string documentHashXml, string documentNumber, decimal documentValue,
                                  int typingTime, int keystroke, string descTypingLog)
        {
            this.proxyProcessing.processTyping(this.securityToken, documentId, true, batchId, true, typeId, true,
                                               batchTotalDocs,
                                               true, workflowId, true, xml, documentHashXml, documentNumber,
                                               documentValue,
                                               true, typingTime, true, keystroke, true, descTypingLog, this.usrId, true,
                                               this.stationId,
                                               true);
            return;
        }

        
        //=====================================
        // Prova Zero1
        //=====================================

        
        public void MarkTransInDiff1(long transId, long workflowId)
        {
            this.proxyProcessing.markTransInDiff1(this.securityToken, transId, true, workflowId, true, this.usrId, true,
                                                  this.stationId,
                                                  true);
            return;
        }

        public void approveTransInDiff1(long transId, long workflowId)
        {
            this.proxyProcessing.approveTransInDiff1(this.securityToken, transId, true, workflowId, true, this.usrId,
                                                     true, this.stationId,
                                                     true);
            return;
        }

        //=====================================
        // Prova Zero2
        //=====================================
                
        public void MarkTransInDiff2(long transId, long workflowId)
        {
            this.proxyProcessing.markTransInDiff2(this.securityToken, transId, true, workflowId, true, this.usrId, true,
                                                  this.stationId,
                                                  true);
            return;
        }

        public void approveTransInDiff2(long transId, long workflowId)
        {
            this.proxyProcessing.approveTransInDiff2(this.securityToken, transId, true, workflowId, true, this.usrId,
                                                     true, this.stationId,
                                                     true);
            return;
        }


        //=====================================
        // Prova Zero3
        //=====================================

        public void approveTransInDiff3(long transId, long workflowId)
        {
            this.proxyProcessing.approveTransInDiff3(this.securityToken, transId, true, workflowId, true, this.usrId,
                                                     true, this.stationId,
                                                     true);
            return;
        }

        //=====================================
        // Formalística
        //=====================================

        public void ApproveForm(long documentId, long transId, long workflowId, long queueId)
        {
            this.proxyProcessing.approveForm(this.securityToken, documentId, true, transId, true, workflowId, true,
                                             queueId, true,
                                             this.usrId, true, this.stationId, true);
        }

        public void MarkForm(long documentId, long transId, long workflowId, long queueId)
        {
            this.proxyProcessing.markForm(this.securityToken, documentId, true, transId, true, workflowId, true, queueId,
                                          true,
                                          this.usrId, true, this.stationId, true);
        }

        //=====================================
        // Aprov1
        //=====================================

        public void ApproveTransInApprove1(long transId, long workflowId)
        {
            this.proxyProcessing.approveTransInApprove1(this.securityToken, transId, true, workflowId, true, this.usrId,
                                                        true,
                                                        this.stationId, true);
        }

        public void MarkTransInApprove1(long transId, long workflowId)
        {
            this.proxyProcessing.markTransInApprove1(this.securityToken, transId, true, workflowId, true, this.usrId,
                                                     true, this.stationId,
                                                     true);
        }

        //public void UpdateDocumentInApprove1(long documentId, string documentXml, string documentNumber,
        //                                     decimal newDocumentValue, decimal oldDocumentValue)
        //{
        //    proxyProcessing.updateDocumentInApprove1(securityToken, documentId, true, documentXml, documentNumber,
        //                                             newDocumentValue, true, oldDocumentValue, true, usrId, true,
        //                                             stationId, true);
        //}

        //=====================================
        // Aprov2
        //=====================================

        public void ApproveTransInApprove2(long transId, long workflowId)
        {
            this.proxyProcessing.approveTransInApprove2(this.securityToken, transId, true, workflowId, true, this.usrId,
                                                        true,
                                                        this.stationId, true);
        }

        //public void UpdateDocumentInApprove2(long documentId, string documentXml, string documentNumber,
        //                                     decimal newDocumentValue, decimal oldDocumentValue)
        //{
        //    proxyProcessing.updateDocumentInApprove2(securityToken, documentId, true, documentXml, documentNumber,
        //                                             newDocumentValue, true, oldDocumentValue, true, usrId, true,
        //                                             stationId, true);
        //}


        //=====================================
        // Aprov3
        //=====================================

        public void ApproveTransInApprove3(long transId, long workflowId)
        {
            this.proxyProcessing.approveTransInApprove3(this.securityToken, transId, true, workflowId, true, this.usrId,
                                                        true,
                                                        this.stationId, true);
        }

        public void MarkTransInApprove3(long transId, long workflowId)
        {
            this.proxyProcessing.markTransInApprove3(this.securityToken, transId, true, workflowId, true, this.usrId,
                                                     true, this.stationId,
                                                     true);
        }

        //public void UpdateDocumentInApprove3(long documentId, string documentXml, string documentNumber,
        //                                     decimal newDocumentValue, decimal oldDocumentValue)
        //{
        //    proxyProcessing.updateDocumentInApprove3(securityToken, documentId, true, documentXml, documentNumber,
        //                                             newDocumentValue, true, oldDocumentValue, true, usrId, true,
        //                                             stationId, true);
        //}


        //=====================================
        // Aprov4
        //=====================================

        public void ApproveTransInApprove4(long transId, long workflowId)
        {
            this.proxyProcessing.approveTransInApprove4(this.securityToken, transId, true, workflowId, true, this.usrId,
                                                        true, this.stationId, true);
        }

        //public void UpdateDocumentInApprove4(long documentId, string documentXml, string documentNumber,
        //                                     decimal newDocumentValue, decimal oldDocumentValue)
        //{
        //    proxyProcessing.updateDocumentInApprove4(securityToken, documentId, true, documentXml, documentNumber,
        //                                             newDocumentValue, true, oldDocumentValue, true, usrId, true,
        //                                             stationId, true);
        //}


        public void ProcessOcrA(long documentId, long batchId, int batchTotalDocs, long workflowId, string xml, string documentHashXml, string documentNumber, decimal documentValue)
        {
            this.proxyProcessing.processOcrA(securityToken, this.usrId, true, documentId, true, batchId, true, batchTotalDocs, true, workflowId, true, xml, documentHashXml, documentNumber, documentValue, true, stationId, true);
        }
    }
}