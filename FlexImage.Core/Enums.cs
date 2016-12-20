// ============================================
// 
// FlexImage.Core
// Enums.cs
// 27/08/2012
// cflavio
// 
// ============================================

namespace FlexImage.Core
{
    public enum enumScanType
    {
        Simul = 0,
        Twain = 1,
        ISIS = 2,
        Import = 3,
        Specialist = 4
    }

    public enum enumPathServer
    {
        A2iATemplates = 0,
        ABBYYTemplates = 1        
    }

    public enum enumSearchMode
    {
        FinancialDocs = 1,
        NonFinacialDocs = 2,
        Folder = 3,
        All = 4
    }

    public enum enumTypeId
    {
        NAO_CLASSIFICADO = 2,
        ENVELOPE_AS = 3,
        ENVELOPE_ATM = 4,
        MALOTE_PJ = 5,
        CAPA_CREDITO = 6,
        CAPA_DEBITO = 7,
        BORDERO_CUSTODIA = 8,
        FICHA_DEPOSITO = 9,
        CHEQUE = 10,
        VOUCHER = 11,
        AUTORIZACAO_DEBITO = 12,
        TITULO = 13,
        ARRECADACAO = 14,
        DARF_SB = 15,
        DARF_CB = 16,
        DARF_SIMPLES_SB = 17,
        DARF_SIMPLES_CB = 18,
        GPS_SB = 19,
        GPS_CB = 20,
        FGTS_SB = 21,
        FGTS_CB = 22,
        REGULARIZACAO_CREDITO = 23,
        REGULARIZACAO_DEBITO = 24,
        AVISO_CREDITO = 25,
        AVISO_DEBITO = 26,
        DOC = 27,
        TED = 28,
        TRANSFERENCIA_CC = 29,
        GARE_SB = 30,
        GARE_CB = 31,
        PAGTO_AVULSO_CARTAO = 32,
        TARIFA = 33,
        FDR = 34,
        BORDERO_TRUNCAGEM = 35,
        TVD = 36,
        ORDEM_PAGAMENTO = 37,
        CHEQUE_CUSTODIA = 38,
        NAO_CLASSIFICADO_NFIN = 100
    };

    public enum enumDocStatus
    {
        CREATED = 0,
        WAITING_RECAPTURE = 10,
        WAITING_JPG_CLASSIFIC = 20,
        WAITING_JPG_TYPING = 30,
        WAITING_JPG_DOUBLE_TYPING = 40,
        WAITING_JPG_VALID = 50,
        WAITING_JPG_FORMALISTIC = 60,
        WAITING_CLASSIFY1 = 100,
        WAITING_CLASSIFY2 = 110,
        WAITING_TYPING1 = 120,
        WAITING_TYPING2 = 130,
        WAITING_DOUBLE_TYPING1 = 140,
        WAITING_DOUBLE_TYPING2 = 150,
        WAITING_VALID1 = 200,
        WAITING_VALID2 = 210,
        WAITING_FORMALISTIC1 = 220,
        WAITING_FORMALISTIC2 = 230,
        WAITING_RECEIPT = 300,
        PENDING = 500,
        DELETED = 800
    };

    public enum enumQueue
    {
        CLASSIFY = 1,
        MOUNTING = 2,
        TYPING_ENVELOPE_AS = 3,
        TYPING_ENVELOPE_ATM = 4,
        TYPING_MALOTE_PJ = 5,
        TYPING_CAPA_CREDITO = 6,
        TYPING_CAPA_DEBITO = 7,
        TYPING_FICHA_DEPOSITO = 9,
        TYPING_CHEQUE = 10,
        TYPING_VOUCHER = 11,
        TYPING_AUTORIZACAO_DEBITO = 12,
        TYPING_TITULO = 13,
        TYPING_ARRECADACAO = 14,
        TYPING_DARF_SB = 15,
        TYPING_DARF_CB = 16,
        TYPING_DARF_SIMPLES_SB = 17,
        TYPING_DARF_SIMPLES_CB = 18,
        TYPING_GPS_SB = 19,
        TYPING_GPS_CB = 20,
        TYPING_FGTS_SB = 21,
        TYPING_FGTS_CB = 22,
        TYPING_REGULARIZACAO_CREDITO = 23,
        TYPING_REGULARIZACAO_DEBITO = 24,
        TYPING_AVISO_CREDITO = 25,
        TYPING_AVISO_DEBITO = 26,
        TYPING_DOC = 27,
        TYPING_TED = 28,
        TYPING_TRANSFERENCIA_CC = 29,
        TYPING_GARE_SB = 30,
        TYPING_GARE_CB = 31,
        TYPING_PAGTO_AVULSO_CARTAO = 32,
        TYPING_TARIFA = 33,
        TYPING_FDR = 34,
        TYPING_TVD = 36,
        TYPING_ORDEM_PAGAMENTO = 37,

        OCR_A = 45,

        VALIDATION = 50,
        PZ1 = 60,
        PZ2 = 61,
        PZ3 = 62,
        FORM_SIGN = 70,
        FORM_NOM = 71,
        FORM_DATE = 72,
        FORM_VAL = 73,
        FORM_BACK = 74,
        APR1 = 80,
        APR2 = 81,
        APR3 = 82,
        APR4 = 83,

        CTD_BORDERO = 201,
        CTD_CMC7 = 202,
        CTD_VAL = 203,
        CTD_CPF_CNPJ = 204,
        CTD_DATA_BOA = 205,
        CTD_DOUBLE_VAL = 211,
        CTD_DOUBLE_DATA_BOA = 212,

        TRUNC_BORDERO = 35,
        TRUNC_CMC7 = 215,
        TRUNC_VAL = 220,

        OCR = 330,

        OCRFIELDS = 505,

        REPROCESS_DOCUMENTNF_CADERNETAS = 700
    };

    public enum enumBatchStatus
    {
        BATCH_OPEN = 1,
        BATCH_PROCESSING1 = 10,
        BATCH_PROCESSING2 = 20,
        BATCH_RECAPTURE = 30,
        BATCH_CLASSIFY = 40,
        BATCH_TYPING = 50,
        BATCH_DOUBLE_TYPING = 60,
        BATCH_CONSOLID = 70,
        BATCH_MOUNTING_MANUAL = 80,
        BATCH_MOUNTED = 100,
        BATCH_PENDING = 400,
        BATCH_FINISHED = 500,
        BATCH_DELETED = 800
    };

    public enum enumReadType
    {
        Manual = 0,
        Automatic = 1
    };

    public enum enumChqCropType
    {
        CHEQUE_SIGNATURE = 1,
        CHEQUE_PAYEE = 2,
        CHEQUE_DATE = 3,
        CHEQUE_LARCAR = 4,
        CHEQUE_BACK = 5,
        CHEQUE_CPFCPNJ = 6,
        CHEQUE_DATABOA = 7,
        CHEQUE_CMC7
    };

    public enum enumExceptionAuthType
    {
        RECONHECIMENTO = 0,
        CONFIRMACAO = 1,
        SIMPLES = 2,
        SUPERIOR = 3,
        DUPLA = 4,
        TRIPLA = 5
    };

    public enum enumExceptionType
    {
        INFORMATION = 0,
        ALERT = 1,
        MANDATORY = 2,
        WARNING = 3,
        ERROR = 4,
        FATAL = 5
    };

    public enum enumTypeOperation
    {
        CAPA = 0,
        RECURSO = 1,
        COMPROMISSO = 2,
        HIBRIDO = 3,
        NAO_IDENTIFICADO = 5
    };
    
    public enum enumParascript
    {
        CI_PROP_VALUE_FIELD_AMOUNT =               0x00000001,
        CI_PROP_VALUE_FIELD_PAYEE_LINE =           0x00000002,
        CI_PROP_VALUE_FIELD_DATE =                 0x00000004,
        CI_PROP_VALUE_FIELD_SIGNATURE =            0x00000008,
        CI_PROP_VALUE_FIELD_PAYOR_BLOCK =          0x00000010,
        CI_PROP_VALUE_FIELD_CHECK_NUMBER =         0x00000020,
        CI_PROP_VALUE_FIELD_MICR_LINE =            0x00000040,
        CI_PROP_VALUE_FIELD_CHECKSUM =             0x00000080,
        CI_PROP_VALUE_FIELD_AMOUNT_VERIFICATION =  0x00000100,
        CI_PROP_VALUE_FIELD_PAYOR_ID =             0x00000200,
        CI_PROP_VALUE_FIELD_PLACE =                0x00000400,
        CI_PROP_VALUE_FIELD_TOP_LINE =             0x00000800
    }
}