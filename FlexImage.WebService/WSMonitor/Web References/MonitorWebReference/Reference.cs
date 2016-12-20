﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5448
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.5448.
// 
#pragma warning disable 1591

namespace FlexImage.WebService.MonitorWebReference {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="WorkflowMonitorServiceBinding", Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")]
    public partial class WorkflowMonitorWSService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback findBatchNfMonitorBySiteIdOperationCompleted;
        
        private System.Threading.SendOrPostCallback findPhaseByWorkflowIdOperationCompleted;
        
        private System.Threading.SendOrPostCallback getMonitorByWorkflowIdOperationCompleted;
        
        private System.Threading.SendOrPostCallback getWorkflowsMonitorOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WorkflowMonitorWSService() {
            this.Url = global::FlexImage.WebService.Properties.Settings.Default.WSMonitor_MonitorWebReference_WorkflowMonitorWSService;
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
        public event findBatchNfMonitorBySiteIdCompletedEventHandler findBatchNfMonitorBySiteIdCompleted;
        
        /// <remarks/>
        public event findPhaseByWorkflowIdCompletedEventHandler findPhaseByWorkflowIdCompleted;
        
        /// <remarks/>
        public event getMonitorByWorkflowIdCompletedEventHandler getMonitorByWorkflowIdCompleted;
        
        /// <remarks/>
        public event getWorkflowsMonitorCompletedEventHandler getWorkflowsMonitorCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlArrayAttribute("findBatchNfMonitorBySiteIdResponse", Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public batchNfMonitorDTO[] findBatchNfMonitorBySiteId([System.Xml.Serialization.XmlElementAttribute("findBatchNfMonitorBySiteId", Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")] findBatchNfMonitorBySiteId findBatchNfMonitorBySiteId1) {
            object[] results = this.Invoke("findBatchNfMonitorBySiteId", new object[] {
                        findBatchNfMonitorBySiteId1});
            return ((batchNfMonitorDTO[])(results[0]));
        }
        
        /// <remarks/>
        public void findBatchNfMonitorBySiteIdAsync(findBatchNfMonitorBySiteId findBatchNfMonitorBySiteId1) {
            this.findBatchNfMonitorBySiteIdAsync(findBatchNfMonitorBySiteId1, null);
        }
        
        /// <remarks/>
        public void findBatchNfMonitorBySiteIdAsync(findBatchNfMonitorBySiteId findBatchNfMonitorBySiteId1, object userState) {
            if ((this.findBatchNfMonitorBySiteIdOperationCompleted == null)) {
                this.findBatchNfMonitorBySiteIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindBatchNfMonitorBySiteIdOperationCompleted);
            }
            this.InvokeAsync("findBatchNfMonitorBySiteId", new object[] {
                        findBatchNfMonitorBySiteId1}, this.findBatchNfMonitorBySiteIdOperationCompleted, userState);
        }
        
        private void OnfindBatchNfMonitorBySiteIdOperationCompleted(object arg) {
            if ((this.findBatchNfMonitorBySiteIdCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findBatchNfMonitorBySiteIdCompleted(this, new findBatchNfMonitorBySiteIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlArrayAttribute("findPhaseByWorkflowIdResponse", Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public phaseDTO[] findPhaseByWorkflowId([System.Xml.Serialization.XmlElementAttribute("findPhaseByWorkflowId", Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")] findPhaseByWorkflowId findPhaseByWorkflowId1) {
            object[] results = this.Invoke("findPhaseByWorkflowId", new object[] {
                        findPhaseByWorkflowId1});
            return ((phaseDTO[])(results[0]));
        }
        
        /// <remarks/>
        public void findPhaseByWorkflowIdAsync(findPhaseByWorkflowId findPhaseByWorkflowId1) {
            this.findPhaseByWorkflowIdAsync(findPhaseByWorkflowId1, null);
        }
        
        /// <remarks/>
        public void findPhaseByWorkflowIdAsync(findPhaseByWorkflowId findPhaseByWorkflowId1, object userState) {
            if ((this.findPhaseByWorkflowIdOperationCompleted == null)) {
                this.findPhaseByWorkflowIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindPhaseByWorkflowIdOperationCompleted);
            }
            this.InvokeAsync("findPhaseByWorkflowId", new object[] {
                        findPhaseByWorkflowId1}, this.findPhaseByWorkflowIdOperationCompleted, userState);
        }
        
        private void OnfindPhaseByWorkflowIdOperationCompleted(object arg) {
            if ((this.findPhaseByWorkflowIdCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findPhaseByWorkflowIdCompleted(this, new findPhaseByWorkflowIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("getMonitorByWorkflowIdResponse", Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")]
        public getMonitorByWorkflowIdResponse getMonitorByWorkflowId([System.Xml.Serialization.XmlElementAttribute("getMonitorByWorkflowId", Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")] getMonitorByWorkflowId getMonitorByWorkflowId1) {
            object[] results = this.Invoke("getMonitorByWorkflowId", new object[] {
                        getMonitorByWorkflowId1});
            return ((getMonitorByWorkflowIdResponse)(results[0]));
        }
        
        /// <remarks/>
        public void getMonitorByWorkflowIdAsync(getMonitorByWorkflowId getMonitorByWorkflowId1) {
            this.getMonitorByWorkflowIdAsync(getMonitorByWorkflowId1, null);
        }
        
        /// <remarks/>
        public void getMonitorByWorkflowIdAsync(getMonitorByWorkflowId getMonitorByWorkflowId1, object userState) {
            if ((this.getMonitorByWorkflowIdOperationCompleted == null)) {
                this.getMonitorByWorkflowIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetMonitorByWorkflowIdOperationCompleted);
            }
            this.InvokeAsync("getMonitorByWorkflowId", new object[] {
                        getMonitorByWorkflowId1}, this.getMonitorByWorkflowIdOperationCompleted, userState);
        }
        
        private void OngetMonitorByWorkflowIdOperationCompleted(object arg) {
            if ((this.getMonitorByWorkflowIdCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getMonitorByWorkflowIdCompleted(this, new getMonitorByWorkflowIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlArrayAttribute("getWorkflowsMonitorResponse", Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public workflowMonitorDTO[] getWorkflowsMonitor([System.Xml.Serialization.XmlElementAttribute("getWorkflowsMonitor", Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")] getWorkflowsMonitor getWorkflowsMonitor1) {
            object[] results = this.Invoke("getWorkflowsMonitor", new object[] {
                        getWorkflowsMonitor1});
            return ((workflowMonitorDTO[])(results[0]));
        }
        
        /// <remarks/>
        public void getWorkflowsMonitorAsync(getWorkflowsMonitor getWorkflowsMonitor1) {
            this.getWorkflowsMonitorAsync(getWorkflowsMonitor1, null);
        }
        
        /// <remarks/>
        public void getWorkflowsMonitorAsync(getWorkflowsMonitor getWorkflowsMonitor1, object userState) {
            if ((this.getWorkflowsMonitorOperationCompleted == null)) {
                this.getWorkflowsMonitorOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetWorkflowsMonitorOperationCompleted);
            }
            this.InvokeAsync("getWorkflowsMonitor", new object[] {
                        getWorkflowsMonitor1}, this.getWorkflowsMonitorOperationCompleted, userState);
        }
        
        private void OngetWorkflowsMonitorOperationCompleted(object arg) {
            if ((this.getWorkflowsMonitorCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getWorkflowsMonitorCompleted(this, new getWorkflowsMonitorCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")]
    public partial class findBatchNfMonitorBySiteId {
        
        private long siteIdField;
        
        private bool siteIdFieldSpecified;
        
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
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")]
    public partial class getWorkflowsMonitor {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")]
    public partial class phaseMonitorDTO {
        
        private long phaseIdField;
        
        private bool phaseIdFieldSpecified;
        
        private string phaseNameField;
        
        private int phaseOrderField;
        
        private bool phaseOrderFieldSpecified;
        
        private long totalDocsField;
        
        private bool totalDocsFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long phaseId {
            get {
                return this.phaseIdField;
            }
            set {
                this.phaseIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool phaseIdSpecified {
            get {
                return this.phaseIdFieldSpecified;
            }
            set {
                this.phaseIdFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string phaseName {
            get {
                return this.phaseNameField;
            }
            set {
                this.phaseNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int phaseOrder {
            get {
                return this.phaseOrderField;
            }
            set {
                this.phaseOrderField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool phaseOrderSpecified {
            get {
                return this.phaseOrderFieldSpecified;
            }
            set {
                this.phaseOrderFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long totalDocs {
            get {
                return this.totalDocsField;
            }
            set {
                this.totalDocsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool totalDocsSpecified {
            get {
                return this.totalDocsFieldSpecified;
            }
            set {
                this.totalDocsFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")]
    public partial class workflowMonitorDTO {
        
        private phaseMonitorDTO[] phasesMonitorField;
        
        private long workflowIdField;
        
        private bool workflowIdFieldSpecified;
        
        private string workflowNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("phasesMonitor", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public phaseMonitorDTO[] phasesMonitor {
            get {
                return this.phasesMonitorField;
            }
            set {
                this.phasesMonitorField = value;
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
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string workflowName {
            get {
                return this.workflowNameField;
            }
            set {
                this.workflowNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")]
    public partial class getMonitorByWorkflowIdResponse {
        
        private workflowMonitorDTO returnField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public workflowMonitorDTO @return {
            get {
                return this.returnField;
            }
            set {
                this.returnField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")]
    public partial class getMonitorByWorkflowId {
        
        private long workflowIdField;
        
        private bool workflowIdFieldSpecified;
        
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")]
    public partial class phaseDTO {
        
        private string phaseAliasField;
        
        private long phaseIdField;
        
        private bool phaseIdFieldSpecified;
        
        private string phaseNameField;
        
        private int phaseOrderField;
        
        private bool phaseOrderFieldSpecified;
        
        private int phaseVisibleField;
        
        private bool phaseVisibleFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string phaseAlias {
            get {
                return this.phaseAliasField;
            }
            set {
                this.phaseAliasField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long phaseId {
            get {
                return this.phaseIdField;
            }
            set {
                this.phaseIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool phaseIdSpecified {
            get {
                return this.phaseIdFieldSpecified;
            }
            set {
                this.phaseIdFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string phaseName {
            get {
                return this.phaseNameField;
            }
            set {
                this.phaseNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int phaseOrder {
            get {
                return this.phaseOrderField;
            }
            set {
                this.phaseOrderField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool phaseOrderSpecified {
            get {
                return this.phaseOrderFieldSpecified;
            }
            set {
                this.phaseOrderFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int phaseVisible {
            get {
                return this.phaseVisibleField;
            }
            set {
                this.phaseVisibleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool phaseVisibleSpecified {
            get {
                return this.phaseVisibleFieldSpecified;
            }
            set {
                this.phaseVisibleFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")]
    public partial class findPhaseByWorkflowId {
        
        private long workflowIdField;
        
        private bool workflowIdFieldSpecified;
        
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://webservice.service.ws.mdi.flexdoc.com.br/")]
    public partial class batchNfMonitorDTO {
        
        private long batchIdField;
        
        private bool batchIdFieldSpecified;
        
        private int batchStatusField;
        
        private bool batchStatusFieldSpecified;
        
        private int batchTotalDocsField;
        
        private bool batchTotalDocsFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long batchId {
            get {
                return this.batchIdField;
            }
            set {
                this.batchIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool batchIdSpecified {
            get {
                return this.batchIdFieldSpecified;
            }
            set {
                this.batchIdFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int batchStatus {
            get {
                return this.batchStatusField;
            }
            set {
                this.batchStatusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool batchStatusSpecified {
            get {
                return this.batchStatusFieldSpecified;
            }
            set {
                this.batchStatusFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int batchTotalDocs {
            get {
                return this.batchTotalDocsField;
            }
            set {
                this.batchTotalDocsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool batchTotalDocsSpecified {
            get {
                return this.batchTotalDocsFieldSpecified;
            }
            set {
                this.batchTotalDocsFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void findBatchNfMonitorBySiteIdCompletedEventHandler(object sender, findBatchNfMonitorBySiteIdCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findBatchNfMonitorBySiteIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal findBatchNfMonitorBySiteIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public batchNfMonitorDTO[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((batchNfMonitorDTO[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void findPhaseByWorkflowIdCompletedEventHandler(object sender, findPhaseByWorkflowIdCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findPhaseByWorkflowIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal findPhaseByWorkflowIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public phaseDTO[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((phaseDTO[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void getMonitorByWorkflowIdCompletedEventHandler(object sender, getMonitorByWorkflowIdCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getMonitorByWorkflowIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getMonitorByWorkflowIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public getMonitorByWorkflowIdResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((getMonitorByWorkflowIdResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void getWorkflowsMonitorCompletedEventHandler(object sender, getWorkflowsMonitorCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getWorkflowsMonitorCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getWorkflowsMonitorCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public workflowMonitorDTO[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((workflowMonitorDTO[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591