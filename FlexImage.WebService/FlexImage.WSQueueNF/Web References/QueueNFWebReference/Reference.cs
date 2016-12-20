﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace FlexImage.WebService.QueueNFWebReference {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.79.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="QueueNfServiceSoapBinding", Namespace="http://impl.service.flexps.flexdoc.com.br/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(genericQueueDto))]
    public partial class QueueNfService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getDocsNfInQueueOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public QueueNfService() {
            this.Url = global::FlexImage.WebService.Properties.Settings.Default.FlexImage_WSQueueNF_QueueNFWebReference_QueueNfService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event getDocsNfInQueueCompletedEventHandler getDocsNfInQueueCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://service.flexps.flexdoc.com.br/", ResponseNamespace="http://service.flexps.flexdoc.com.br/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public docNfQueueDto[] getDocsNfInQueue([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string token, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] long usrId, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool usrIdSpecified, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] long queueId, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool queueIdSpecified, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] int numObjs, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool numObjsSpecified) {
            object[] results = this.Invoke("getDocsNfInQueue", new object[] {
                        token,
                        usrId,
                        usrIdSpecified,
                        queueId,
                        queueIdSpecified,
                        numObjs,
                        numObjsSpecified});
            return ((docNfQueueDto[])(results[0]));
        }
        
        /// <remarks/>
        public void getDocsNfInQueueAsync(string token, long usrId, bool usrIdSpecified, long queueId, bool queueIdSpecified, int numObjs, bool numObjsSpecified) {
            this.getDocsNfInQueueAsync(token, usrId, usrIdSpecified, queueId, queueIdSpecified, numObjs, numObjsSpecified, null);
        }
        
        /// <remarks/>
        public void getDocsNfInQueueAsync(string token, long usrId, bool usrIdSpecified, long queueId, bool queueIdSpecified, int numObjs, bool numObjsSpecified, object userState) {
            if ((this.getDocsNfInQueueOperationCompleted == null)) {
                this.getDocsNfInQueueOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetDocsNfInQueueOperationCompleted);
            }
            this.InvokeAsync("getDocsNfInQueue", new object[] {
                        token,
                        usrId,
                        usrIdSpecified,
                        queueId,
                        queueIdSpecified,
                        numObjs,
                        numObjsSpecified}, this.getDocsNfInQueueOperationCompleted, userState);
        }
        
        private void OngetDocsNfInQueueOperationCompleted(object arg) {
            if ((this.getDocsNfInQueueCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getDocsNfInQueueCompleted(this, new getDocsNfInQueueCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.79.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.flexps.flexdoc.com.br/")]
    public partial class docNfQueueDto : genericQueueDto {
        
        private long batchNfIdField;
        
        private bool batchNfIdFieldSpecified;
        
        private int batchNfPriorityField;
        
        private bool batchNfPriorityFieldSpecified;
        
        private int batchNfTotalDocsField;
        
        private bool batchNfTotalDocsFieldSpecified;
        
        private int documentNfCaptureSeqField;
        
        private bool documentNfCaptureSeqFieldSpecified;
        
        private string documentNfFileTypeField;
        
        private long documentNfIdField;
        
        private bool documentNfIdFieldSpecified;
        
        private long siteIdField;
        
        private bool siteIdFieldSpecified;
        
        private string typeAliasField;
        
        private long typeIdField;
        
        private bool typeIdFieldSpecified;
        
        private long usrIndexIdField;
        
        private bool usrIndexIdFieldSpecified;
        
        private long workflowIdField;
        
        private bool workflowIdFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long batchNfId {
            get {
                return this.batchNfIdField;
            }
            set {
                this.batchNfIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool batchNfIdSpecified {
            get {
                return this.batchNfIdFieldSpecified;
            }
            set {
                this.batchNfIdFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int batchNfPriority {
            get {
                return this.batchNfPriorityField;
            }
            set {
                this.batchNfPriorityField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool batchNfPrioritySpecified {
            get {
                return this.batchNfPriorityFieldSpecified;
            }
            set {
                this.batchNfPriorityFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int batchNfTotalDocs {
            get {
                return this.batchNfTotalDocsField;
            }
            set {
                this.batchNfTotalDocsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool batchNfTotalDocsSpecified {
            get {
                return this.batchNfTotalDocsFieldSpecified;
            }
            set {
                this.batchNfTotalDocsFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int documentNfCaptureSeq {
            get {
                return this.documentNfCaptureSeqField;
            }
            set {
                this.documentNfCaptureSeqField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool documentNfCaptureSeqSpecified {
            get {
                return this.documentNfCaptureSeqFieldSpecified;
            }
            set {
                this.documentNfCaptureSeqFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string documentNfFileType {
            get {
                return this.documentNfFileTypeField;
            }
            set {
                this.documentNfFileTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long documentNfId {
            get {
                return this.documentNfIdField;
            }
            set {
                this.documentNfIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool documentNfIdSpecified {
            get {
                return this.documentNfIdFieldSpecified;
            }
            set {
                this.documentNfIdFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long siteId {
            get {
                return this.siteIdField;
            }
            set {
                this.siteIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool siteIdSpecified {
            get {
                return this.siteIdFieldSpecified;
            }
            set {
                this.siteIdFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string typeAlias {
            get {
                return this.typeAliasField;
            }
            set {
                this.typeAliasField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long typeId {
            get {
                return this.typeIdField;
            }
            set {
                this.typeIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool typeIdSpecified {
            get {
                return this.typeIdFieldSpecified;
            }
            set {
                this.typeIdFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long usrIndexId {
            get {
                return this.usrIndexIdField;
            }
            set {
                this.usrIndexIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool usrIndexIdSpecified {
            get {
                return this.usrIndexIdFieldSpecified;
            }
            set {
                this.usrIndexIdFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long workflowId {
            get {
                return this.workflowIdField;
            }
            set {
                this.workflowIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool workflowIdSpecified {
            get {
                return this.workflowIdFieldSpecified;
            }
            set {
                this.workflowIdFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(docNfQueueDto))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.79.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.flexps.flexdoc.com.br/")]
    public partial class genericQueueDto {
        
        private System.DateTime dateAddInQueueField;
        
        private bool dateAddInQueueFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime dateAddInQueue {
            get {
                return this.dateAddInQueueField;
            }
            set {
                this.dateAddInQueueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool dateAddInQueueSpecified {
            get {
                return this.dateAddInQueueFieldSpecified;
            }
            set {
                this.dateAddInQueueFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.79.0")]
    public delegate void getDocsNfInQueueCompletedEventHandler(object sender, getDocsNfInQueueCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.79.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getDocsNfInQueueCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getDocsNfInQueueCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public docNfQueueDto[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((docNfQueueDto[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591