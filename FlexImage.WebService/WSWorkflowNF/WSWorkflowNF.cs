using System;
using FlexImage.Core;
using System.Collections.Generic;
using FlexImage.WebService.WorkflowNFWebReference;

namespace FlexImage.WebService
{
    public class WSWorkflowNF
    {
        public WorkflowNfWSService ProxyWorkflowNf = new WorkflowNfWSService();

        private string endPoint = "";

        public string EndPoint
        {
            get { return endPoint; }
            set
            {
                endPoint = value;
                ProxyWorkflowNf.Url = endPoint + "WorkflowNfService";
            }
        }

        public savePreResponse SavePre(long typeId, preDocKeyDTO[] predockeys, long stationId, long userId)
        {
            var savepre1 = new savePre();

            savepre1.typeId = typeId;
            savepre1.typeIdSpecified = true;

            savepre1.stationId = stationId;
            savepre1.stationIdSpecified = true;

            savepre1.usrId = userId;
            savepre1.usrIdSpecified = true;

            savepre1.preDocKeysDTO = predockeys;
            var resp = new savePreResponse();
            resp = ProxyWorkflowNf.savePre(savepre1);
            return resp;
        }

        public deleteBatchNfResponse deleteBatchNf(long usrId, long stationId, long batchNfId)
        {
            var param = new deleteBatchNf();
            var resp = new deleteBatchNfResponse();

            param.batchNfId = batchNfId;
            param.batchNfIdSpecified = true;

            param.reason = "CANCELAMENTO MANUAL";

            param.stationId = stationId;
            param.stationIdSpecified = true;

            param.usrId = usrId;
            param.usrIdSpecified = true;
            
            resp = ProxyWorkflowNf.deleteBatchNf(param);

            return resp;
        }

        public deleteDocNfResponse deleteDocNf(long usrId, long stationId, long docNfId)
        {
            var deleteDocNf1 = new deleteDocNf();

            deleteDocNf1.docNfId = docNfId;
            deleteDocNf1.docNfIdSpecified = true;

            deleteDocNf1.stationId = stationId;
            deleteDocNf1.stationIdSpecified = true;

            deleteDocNf1.usrId = usrId;
            deleteDocNf1.usrIdSpecified = true;

            var resp = new deleteDocNfResponse();

            resp = ProxyWorkflowNf.deleteDocNf(deleteDocNf1);

            return resp;
        }


        public typeDTO[] GetHierarchycalTypes(long usrId)
        {
            var getHierarchyTypes1 = new getHierarchyTypes();
            typeDTO[] type;

            getHierarchyTypes1.usrId = usrId;
            getHierarchyTypes1.usrIdSpecified = true;

            type = ProxyWorkflowNf.getHierarchyTypes(getHierarchyTypes1);

            return type;
        }

        public keyDefDTO[] FindAllKeyDef()
        {
            var findAllKeyDef1 = new findAllKeyDef();
            keyDefDTO[] keydef;

            keydef = ProxyWorkflowNf.findAllKeyDef(findAllKeyDef1);

            return keydef;
        }

        public void classifyAndTypingService(long usrId, docKeyDTO[] keys, docNf docNf, int keyStroke, int time, long stationId)
        {
            var classifyAndTypingService1 = new classifyAndTypingService();
            var classifyAndTypingServiceResponse1 = new classifyAndTypingServiceResponse();

            try
            {
                classifyAndTypingService1.docKeys = keys;
                classifyAndTypingService1.docNf = docNf;

                classifyAndTypingService1.typingKeyStroke = keyStroke;
                classifyAndTypingService1.typingKeyStrokeSpecified = true;

                classifyAndTypingService1.typingTime = time;
                classifyAndTypingService1.typingTimeSpecified = true;

                classifyAndTypingService1.usrId = usrId;
                classifyAndTypingService1.usrIdSpecified = true;

                classifyAndTypingService1.stationId = stationId;
                classifyAndTypingService1.stationIdSpecified = true;

                classifyAndTypingServiceResponse1 =
                    ProxyWorkflowNf.classifyAndTypingService(classifyAndTypingService1);
            }
            catch (Exception e)
            {
                Alert.Exclamation("classifyAndTypingService::Error:" + e);
            }
        }

        public queueObject[] GetObjectsInQueue(long queueId, long userId)
        {
            var getObjectsInQueue1 = new getObjectsInQueue();
            queueObject[] response;

            getObjectsInQueue1.queueId = queueId;
            getObjectsInQueue1.queueIdSpecified = true;
            getObjectsInQueue1.usrId = userId;
            getObjectsInQueue1.usrIdSpecified = true;

            response = ProxyWorkflowNf.getObjectsInQueue(getObjectsInQueue1);

            return response;
        }

        public searchDocumentsResponse SearchDocuments(string query, int page, long userId, long stationId)
        {
            var searchDocuments1 = new searchDocuments();
            searchDocumentsResponse response;

            searchDocuments1.query = query;
            searchDocuments1.usrId = userId;
            searchDocuments1.usrIdSpecified = true;

            searchDocuments1.page = page;
            searchDocuments1.pageSpecified = true;

            searchDocuments1.stationId = stationId;
            searchDocuments1.stationIdSpecified = true;

            searchDocuments1.findType = 0;
            searchDocuments1.findTypeSpecified = true;

            response = ProxyWorkflowNf.searchDocuments(searchDocuments1);
            return response;
        }

        public getBatchNfDetailsResponse GetBatchNfDetails(long batchId)
        {
            var param = new getBatchNfDetails();
            var response = new getBatchNfDetailsResponse();

            param.batchNfId = batchId;
            param.batchNfIdSpecified = true;

            response = ProxyWorkflowNf.getBatchNfDetails(param);

            return response;
        }

        public getBPMLoginAddressResponse getBPMLoginAddress()
        {
            var param = new getBPMLoginAddress();
            var response = new getBPMLoginAddressResponse();

            response = ProxyWorkflowNf.getBPMLoginAddress(param);

            return response;
        }

        public uploadDocOfTemplateResponse uploadDocOfTemplate(long typeId, long workflowId, long siteId, long stationId, long userId)
        {
            var param = new uploadDocOfTemplate();
            var response = new uploadDocOfTemplateResponse();

            param.siteId = siteId;
            param.siteIdSpecified = true;

            param.stationId = stationId;
            param.stationIdSpecified = true;

            param.typeId = typeId;
            param.typeIdSpecified = true;

            param.usrId = userId;
            param.usrIdSpecified = true;

            param.workflowId = workflowId;
            param.workflowIdSpecified = true;

            response = ProxyWorkflowNf.uploadDocOfTemplate(param);

            return response;
        }

        public bool resetQueues(long stationId, long userId)
        {
            var param = new resetQueues();
            var response = new resetQueuesResponse();

            param.stationId = stationId;
            param.stationIdSpecified = true;

            param.usrId = userId;
            param.usrIdSpecified = true;

            ProxyWorkflowNf.resetQueues(param);

            return true;
        }

        public docRepoDTO[] getDocsByFolderId(long folderId)
        {
            var param = new getDocsByFolderId();
            docRepoDTO[] response;

            param.typeId = folderId;
            param.typeIdSpecified = true;
            
            response = ProxyWorkflowNf.getDocsByFolderId(param);
                       
            return response;
        }

        public preDocumentDTO[] getPreDocByKey(string word)
        {
            var param = new getPreDocByKey();
            param.word = word;

            preDocumentDTO[] response = ProxyWorkflowNf.getPreDocByKey(param);

            return response;
        }

        public long[] getDocsForOCR(int totalDocsRequested)
        {
            var param = new getDocsForOCR();
            param.docsRequested = totalDocsRequested;
            param.docsRequestedSpecified = true;

            long[] response = ProxyWorkflowNf.getDocsForOCR(param);
            return response;
        }


        public void SetDocOCR(long docId, string OCRtext)
        {
            var param = new setDocOCR();
            param.docNfId = docId;
            param.docNfIdSpecified = true;
            param.documentNfOcr = OCRtext;

            setDocOCRResponse response = ProxyWorkflowNf.setDocOCR(param);
            return;
        }

        public long[] GetDocsForBackup(long maxSize)
        {
            var param = new getDocForBackup();
            param.max = maxSize;
            param.maxSpecified = true;

            long[] docs = ProxyWorkflowNf.getDocForBackup(param);

            return docs;
        }

        public getBatchNfDetailsResponse LoadBatch(long batchId)
        {
            List<docNfDTO> obj = new List<docNfDTO>();

            var response = new getBatchNfDetailsResponse();
            getBatchNfDetails param = new getBatchNfDetails();

            param.batchNfId = batchId;
            param.batchNfIdSpecified = true;

            response = ProxyWorkflowNf.getBatchNfDetails(param);

            return response ;
        }

        public getDocsInBoxResponse GetDocsInBox(long boxId, long stationId, long usrId)
        {
            var response = new getDocsInBoxResponse();
            getDocsInBox param = new getDocsInBox();

            param.boxId = Convert.ToInt32( boxId);
            param.boxIdSpecified = true;

            param.stationId = stationId;
            param.stationIdSpecified = true;

            param.usrId = usrId;
            param.usrIdSpecified = true;

            response = ProxyWorkflowNf.getDocsInBox(param);

            return response;
        }

        //public getBoxInfoResponse GetBoxInfo(long boxId, long stationId, long usrId)
        //{
        //    var response = new getBoxInfoResponse();
        //    getBoxInfo param = new getBoxInfo();

        //    param.boxId = Convert.ToInt32(boxId);
        //    param.boxIdSpecified = true;

        //    param.stationId = stationId;
        //    param.stationIdSpecified = true;

        //    param.usrId = usrId;
        //    param.usrIdSpecified = true;

        //    response = ProxyWorkflowNf.getBoxInfo(param);

        //    return response;
        //}
    }
}