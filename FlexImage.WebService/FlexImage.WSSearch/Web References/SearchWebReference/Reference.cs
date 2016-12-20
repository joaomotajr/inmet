﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5466
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.5466.
// 
#pragma warning disable 1591

namespace FlexImage.WebService.SearchWebReference {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SearchServiceSoapBinding", Namespace="http://impl.service.flexps.flexdoc.com.br/")]
    public partial class SearchService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback simpleSearchOperationCompleted;
        
        private System.Threading.SendOrPostCallback advancedSearchOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SearchService() {
            this.Url = global::FlexImage.WebService.Properties.Settings.Default.FlexImage_WSSearch_WSSearch_SearchService;
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
        public event simpleSearchCompletedEventHandler simpleSearchCompleted;
        
        /// <remarks/>
        public event advancedSearchCompletedEventHandler advancedSearchCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://service.flexps.flexdoc.com.br/", ResponseNamespace="http://service.flexps.flexdoc.com.br/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public genericSearchDto simpleSearch([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string token, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string text, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] int searchType, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool searchTypeSpecified, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] int startRec, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool startRecSpecified, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] int qtdeRows, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool qtdeRowsSpecified, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] long usrId, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool usrIdSpecified, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] long stationId, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool stationIdSpecified) {
            object[] results = this.Invoke("simpleSearch", new object[] {
                        token,
                        text,
                        searchType,
                        searchTypeSpecified,
                        startRec,
                        startRecSpecified,
                        qtdeRows,
                        qtdeRowsSpecified,
                        usrId,
                        usrIdSpecified,
                        stationId,
                        stationIdSpecified});
            return ((genericSearchDto)(results[0]));
        }
        
        /// <remarks/>
        public void simpleSearchAsync(string token, string text, int searchType, bool searchTypeSpecified, int startRec, bool startRecSpecified, int qtdeRows, bool qtdeRowsSpecified, long usrId, bool usrIdSpecified, long stationId, bool stationIdSpecified) {
            this.simpleSearchAsync(token, text, searchType, searchTypeSpecified, startRec, startRecSpecified, qtdeRows, qtdeRowsSpecified, usrId, usrIdSpecified, stationId, stationIdSpecified, null);
        }
        
        /// <remarks/>
        public void simpleSearchAsync(string token, string text, int searchType, bool searchTypeSpecified, int startRec, bool startRecSpecified, int qtdeRows, bool qtdeRowsSpecified, long usrId, bool usrIdSpecified, long stationId, bool stationIdSpecified, object userState) {
            if ((this.simpleSearchOperationCompleted == null)) {
                this.simpleSearchOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsimpleSearchOperationCompleted);
            }
            this.InvokeAsync("simpleSearch", new object[] {
                        token,
                        text,
                        searchType,
                        searchTypeSpecified,
                        startRec,
                        startRecSpecified,
                        qtdeRows,
                        qtdeRowsSpecified,
                        usrId,
                        usrIdSpecified,
                        stationId,
                        stationIdSpecified}, this.simpleSearchOperationCompleted, userState);
        }
        
        private void OnsimpleSearchOperationCompleted(object arg) {
            if ((this.simpleSearchCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.simpleSearchCompleted(this, new simpleSearchCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://service.flexps.flexdoc.com.br/", ResponseNamespace="http://service.flexps.flexdoc.com.br/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public genericSearchDto advancedSearch(
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string token, 
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] long batchId, 
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool batchIdSpecified, 
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] long docId, 
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool docIdSpecified, 
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] long typeId, 
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool typeIdSpecified, 
                    [System.Xml.Serialization.XmlElementAttribute("keysDto", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] keyDto[] keysDto, 
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] int searchType, 
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool searchTypeSpecified, 
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] int start, 
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool startSpecified, 
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] int rows, 
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool rowsSpecified, 
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] long usrId, 
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool usrIdSpecified, 
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] long stationId, 
                    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool stationIdSpecified) {
            object[] results = this.Invoke("advancedSearch", new object[] {
                        token,
                        batchId,
                        batchIdSpecified,
                        docId,
                        docIdSpecified,
                        typeId,
                        typeIdSpecified,
                        keysDto,
                        searchType,
                        searchTypeSpecified,
                        start,
                        startSpecified,
                        rows,
                        rowsSpecified,
                        usrId,
                        usrIdSpecified,
                        stationId,
                        stationIdSpecified});
            return ((genericSearchDto)(results[0]));
        }
        
        /// <remarks/>
        public void advancedSearchAsync(
                    string token, 
                    long batchId, 
                    bool batchIdSpecified, 
                    long docId, 
                    bool docIdSpecified, 
                    long typeId, 
                    bool typeIdSpecified, 
                    keyDto[] keysDto, 
                    int searchType, 
                    bool searchTypeSpecified, 
                    int start, 
                    bool startSpecified, 
                    int rows, 
                    bool rowsSpecified, 
                    long usrId, 
                    bool usrIdSpecified, 
                    long stationId, 
                    bool stationIdSpecified) {
            this.advancedSearchAsync(token, batchId, batchIdSpecified, docId, docIdSpecified, typeId, typeIdSpecified, keysDto, searchType, searchTypeSpecified, start, startSpecified, rows, rowsSpecified, usrId, usrIdSpecified, stationId, stationIdSpecified, null);
        }
        
        /// <remarks/>
        public void advancedSearchAsync(
                    string token, 
                    long batchId, 
                    bool batchIdSpecified, 
                    long docId, 
                    bool docIdSpecified, 
                    long typeId, 
                    bool typeIdSpecified, 
                    keyDto[] keysDto, 
                    int searchType, 
                    bool searchTypeSpecified, 
                    int start, 
                    bool startSpecified, 
                    int rows, 
                    bool rowsSpecified, 
                    long usrId, 
                    bool usrIdSpecified, 
                    long stationId, 
                    bool stationIdSpecified, 
                    object userState) {
            if ((this.advancedSearchOperationCompleted == null)) {
                this.advancedSearchOperationCompleted = new System.Threading.SendOrPostCallback(this.OnadvancedSearchOperationCompleted);
            }
            this.InvokeAsync("advancedSearch", new object[] {
                        token,
                        batchId,
                        batchIdSpecified,
                        docId,
                        docIdSpecified,
                        typeId,
                        typeIdSpecified,
                        keysDto,
                        searchType,
                        searchTypeSpecified,
                        start,
                        startSpecified,
                        rows,
                        rowsSpecified,
                        usrId,
                        usrIdSpecified,
                        stationId,
                        stationIdSpecified}, this.advancedSearchOperationCompleted, userState);
        }
        
        private void OnadvancedSearchOperationCompleted(object arg) {
            if ((this.advancedSearchCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.advancedSearchCompleted(this, new advancedSearchCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.flexps.flexdoc.com.br/")]
    public partial class genericSearchDto {
        
        private string queryTimeField;
        
        private genericResultSearchDto[] resultsField;
        
        private int rowsField;
        
        private bool rowsFieldSpecified;
        
        private int startField;
        
        private bool startFieldSpecified;
        
        private int totalResultField;
        
        private bool totalResultFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string queryTime {
            get {
                return this.queryTimeField;
            }
            set {
                this.queryTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("results", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public genericResultSearchDto[] results {
            get {
                return this.resultsField;
            }
            set {
                this.resultsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int rows {
            get {
                return this.rowsField;
            }
            set {
                this.rowsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool rowsSpecified {
            get {
                return this.rowsFieldSpecified;
            }
            set {
                this.rowsFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int start {
            get {
                return this.startField;
            }
            set {
                this.startField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool startSpecified {
            get {
                return this.startFieldSpecified;
            }
            set {
                this.startFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int totalResult {
            get {
                return this.totalResultField;
            }
            set {
                this.totalResultField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool totalResultSpecified {
            get {
                return this.totalResultFieldSpecified;
            }
            set {
                this.totalResultFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.flexps.flexdoc.com.br/")]
    public partial class genericResultSearchDto {
        
        private string fileTypeField;
        
        private long idField;
        
        private bool idFieldSpecified;
        
        private string resultField;
        
        private string segmentField;
        
        private typeDto typeDtoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string fileType {
            get {
                return this.fileTypeField;
            }
            set {
                this.fileTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool idSpecified {
            get {
                return this.idFieldSpecified;
            }
            set {
                this.idFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string result {
            get {
                return this.resultField;
            }
            set {
                this.resultField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string segment {
            get {
                return this.segmentField;
            }
            set {
                this.segmentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public typeDto typeDto {
            get {
                return this.typeDtoField;
            }
            set {
                this.typeDtoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.flexps.flexdoc.com.br/")]
    public partial class typeDto {
        
        private keyDefDto[] keyDefDtoListField;
        
        private string typeAliasField;
        
        private long typeIdField;
        
        private bool typeIdFieldSpecified;
        
        private string typeNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("keyDefDtoList", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public keyDefDto[] keyDefDtoList {
            get {
                return this.keyDefDtoListField;
            }
            set {
                this.keyDefDtoListField = value;
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
        public string typeName {
            get {
                return this.typeNameField;
            }
            set {
                this.typeNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.flexps.flexdoc.com.br/")]
    public partial class keyDefDto {
        
        private long keyDefIdField;
        
        private bool keyDefIdFieldSpecified;
        
        private int keyDefMandatoryField;
        
        private bool keyDefMandatoryFieldSpecified;
        
        private string keyDefNameField;
        
        private long typeIdField;
        
        private bool typeIdFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long keyDefId {
            get {
                return this.keyDefIdField;
            }
            set {
                this.keyDefIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool keyDefIdSpecified {
            get {
                return this.keyDefIdFieldSpecified;
            }
            set {
                this.keyDefIdFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int keyDefMandatory {
            get {
                return this.keyDefMandatoryField;
            }
            set {
                this.keyDefMandatoryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool keyDefMandatorySpecified {
            get {
                return this.keyDefMandatoryFieldSpecified;
            }
            set {
                this.keyDefMandatoryFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string keyDefName {
            get {
                return this.keyDefNameField;
            }
            set {
                this.keyDefNameField = value;
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
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.flexps.flexdoc.com.br/")]
    public partial class keyDto {
        
        private long keyDefIdField;
        
        private bool keyDefIdFieldSpecified;
        
        private string keyDefNameField;
        
        private string keyValueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long keyDefId {
            get {
                return this.keyDefIdField;
            }
            set {
                this.keyDefIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool keyDefIdSpecified {
            get {
                return this.keyDefIdFieldSpecified;
            }
            set {
                this.keyDefIdFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string keyDefName {
            get {
                return this.keyDefNameField;
            }
            set {
                this.keyDefNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string keyValue {
            get {
                return this.keyValueField;
            }
            set {
                this.keyValueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void simpleSearchCompletedEventHandler(object sender, simpleSearchCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class simpleSearchCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal simpleSearchCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public genericSearchDto Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((genericSearchDto)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void advancedSearchCompletedEventHandler(object sender, advancedSearchCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class advancedSearchCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal advancedSearchCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public genericSearchDto Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((genericSearchDto)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591