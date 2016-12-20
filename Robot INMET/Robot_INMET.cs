// ============================================
// 
// Robot PreIndex
// INMET.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using FlexImage.Core;
using FlexImage.WebService;
using FlexImage.WebService.QueueNFWebReference;
using FlexImage.WebService.SecurityWebReference;
using System.Drawing;
using System.Xml;
using Telerik.WinControls.UI;
using System.Collections.Generic;
using FlexImage.Binarize;
using System.Diagnostics;
using System.Runtime.InteropServices;

#endregion

//Failed to run non-developer licenses in developer library
//http://knowledgebase.abbyy.com/article/650
//Solution
//Remove the Protection.Developer.dll file from the Bin folder 
//or just change its extension. If you ever need to work with 
//the developer license on the same machine, just put the file 
//back into the Bin folder or change its extension back to .dll.


namespace INMET
{
    public partial class Robot_INMET : Form
    {
        private readonly Log log = new Log();

        private readonly WSBasicTable _wsBasicTable = GenericSingleton<WSBasicTable>.GetInstance();
        private readonly WSManager _wsManager = GenericSingleton<WSManager>.GetInstance();
        private readonly WsProcessingNf _wsProcessingNf = GenericSingleton<WsProcessingNf>.GetInstance();
        private readonly WSDocumentNF _wsDocumentNf = GenericSingleton<WSDocumentNF>.GetInstance();
        private readonly WSQueueNF _wsQueueNf = GenericSingleton<WSQueueNF>.GetInstance();
        private readonly WSRepository _wsRepository = GenericSingleton<WSRepository>.GetInstance();
        private readonly WSSecurity _wsSecurity = GenericSingleton<WSSecurity>.GetInstance();
        private readonly WSCancelNF _wsCancelNF = GenericSingleton<WSCancelNF>.GetInstance();

        private readonly string controlFile = Directory.GetCurrentDirectory() + "\\CONFIG\\CONTROL.TXT";
        
        private docNfQueueDto doc = new docNfQueueDto();

        private ClassifyINMET classify;

        private long processed = 0;
        private long ignored = 0;
        private string[] productionStr = new string[60];
        private int[] produtionValue  = new int[60];

        private Stopwatch stopWatch = new Stopwatch();
        private string lastAlias = "";
        private string SN;
        private string robotMODE = "";

        private int countError = 0;

        public Robot_INMET()
        {
            this.InitializeComponent();
            uxProgressBar1.Maximum = 30;
            this.uxVersion.Text = "Build " + Assembly.GetExecutingAssembly().GetName().Version;
            this.timerConnection.Enabled = true;

            log.Debug("");
            log.Debug("");
            log.Debug("==============================================================================");
            log.Debug("");
            
            log.Debug("Init: " + this.uxVersion.Text);
             
        }
        
        private void timerConnection_Tick(object sender, EventArgs e)
        {
            log.Debug("Opening connection");

            this.timerConnection.Enabled = false;
            if (!this._wsManager.SelectServer())
            {
                if (this._wsManager.GetEndPoint() != "")
                    Alert.Exclamation("Erro na conexão com o DataCenter [" + this._wsManager.GetEndPoint() + "]");

                log.Debug("SelectServer:" + this._wsManager.GetEndPoint());

                Application.Exit();
                return;
            }

            log.Debug("Connection opened");

            lblServer.Text = this._wsManager.Ip + ":" + this._wsManager.Port;
            lblVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            lblINIT.Text = System.DateTime.Now.ToString();
            lblProcessed.Text = "0";
            lblIgnored.Text = "0";
            lblDPH.Text = "0";
            lblId.Text = "";
            lblTipo.Text = "";
            lblMode.Text = "";

            lblResult.BackColor = Color.White;

            string stationName = Network.GetComputerName();
            string ip = Network.GetLocalIP();
            lblIP.Text = ip;

            log.Debug("Before:Login");

            this.Login();

            log.Debug("Before:ReadParameters");

            this._wsBasicTable.ReadParameters();

            log.Debug("Before:GetAbbyySN");

            SN = GetAbbyySN();

            if (FlexImage.Core.Network.GetComputerName() == "HOME-JR" || FlexImage.Core.Network.GetComputerName() == "WIN-4RSENMS1GVD" )
                SN = "SWTD10100002923576437063";


            if (FlexImage.Core.Network.GetComputerName() == "SUPORTE001")
            {
                //SN = "SWTR10100004299253149846";
                SN = "SWTD10100002922743704307";
            }

            log.Debug("Abbyy SN:" + SN);

            lblSN.Text = SN;

            
            GetDoc();
        }
        
        private void Login()
        {
            string userInternalId;
            string pwd;

            GetBDUser(out userInternalId, out pwd);

            lblMode.Text = robotMODE;
            lblMode.BackColor = Color.Green;
            
            var hash = new Security.Hash();

            //string pwd = this.uiPwd.Text.ToUpper();
            //string userInternalId = this.uiUser.Text.ToUpper();
            string stationName = Network.GetComputerName();
            string ip = Network.GetLocalIP();
            string hashStr = hash.SHA1String(pwd.ToUpper()).ToLower();

            //Verifica se SHA2 está habilitado
            string randomSaltKey = this._wsSecurity.GetRandomSaltKey(userInternalId);

            if (randomSaltKey != null)
                hashStr = hash.encryptSHA2(pwd, userInternalId, randomSaltKey);

            usrSession usrSession;
            usrSession = this._wsSecurity.Login(userInternalId, randomSaltKey + hashStr, stationName, ip);

            Console.WriteLine("Usr: " + userInternalId);
            Console.WriteLine("Pwd: " + pwd);
            Console.WriteLine("SaltKey " + randomSaltKey);
            Console.WriteLine("HashStr: " + hashStr);
            Console.WriteLine("Token: " + this._wsSecurity.SecurityToken.ToString());

            this._wsManager.SetSecurity(usrSession.securityToken, usrSession.usrId, usrSession.stationId);

            stopWatch.Reset();
            stopWatch.Start();
        }

        private void GetBDUser(out string user, out string pwd)
        {
            string fileName = Application.StartupPath + "\\CONFIG\\CONFIG.XML";

            if (!File.Exists(fileName))
            {
                user = "ROBOT_OCR";
                pwd = "FLEXDOC99";
                return;
            }

            var xtr = new XmlTextReader(fileName);
            xtr.WhitespaceHandling = WhitespaceHandling.None;
            var xml = new XmlDocument();
            xml.Load(xtr);
            xtr.Close();

            try
            {
                XmlNode el1 = xml.SelectSingleNode("/ROOT/BD/USER");
                user = el1.InnerText;
            }
            catch
            {
                user = "ROBOT_OCR";
            }

            try
            {
                XmlNode el2 = xml.SelectSingleNode("/ROOT/BD/PWD");
                pwd = el2.InnerText;
            }
            catch
            {
                pwd = "FLEXDOC99";
            }

            try
            {
                XmlNode el2 = xml.SelectSingleNode("/ROOT/MODE");
                robotMODE = el2.InnerText;
            }
            catch
            {
                robotMODE = "NORMAL";
            }

            return;
        }


        private string GetAbbyySN()
        {
            string fileName = Application.StartupPath + "\\CONFIG\\CONFIG.XML";
            
            if (!File.Exists(fileName))
            {
                return "";
            }

            var xtr = new XmlTextReader(fileName);
            xtr.WhitespaceHandling = WhitespaceHandling.None;
            var xml = new XmlDocument();
            xml.Load(xtr);
            xtr.Close();

            try
            {
                XmlNode el2 = xml.SelectSingleNode("/ROOT/ABBYY/SN");
                return el2.InnerText;                    
            }
            catch
            {
            }

            return "";
        }
        
        private void timerProc_Tick(object sender, EventArgs e)
        {
            uxProgressBar1.Value = uxProgressBar1.Value + 1;

            int minToReconnect = 60; //60 minutos

            if (uxProgressBar1.Value == uxProgressBar1.Maximum)
            {
                if (stopWatch.ElapsedMilliseconds > minToReconnect * 60 * 1000)
                {
                    this.Login();
                }

                uxProgressBar1.Value = 0;
                timerProc.Enabled = false;
                timerProc.Stop();
                GetDoc();
            }
        }
        
        private void GetDoc()
        {
            log.Debug("");
            log.Debug("-----------------------------------------------------");
            log.Debug("");
            log.Debug("Getting Doc: " + robotMODE);

            docNfQueueDto[] docNfQueueDto;

            docNfQueueDto = this._wsQueueNf.GetDocsNfInQueue((long)enumQueue.OCRFIELDS, 1);
        
            if (docNfQueueDto == null)
            {
                simpleViewer1.ClearImage();
                log.Debug("Nenhum documento na fila de OCR Fields");
                timerProc.Enabled = true;
                timerProc.Start();
                return;
            }

            this.doc = docNfQueueDto[0];
            uxInfo.Text = "Doc: " + doc.documentNfId + " / Batch: " + doc.batchNfId;

            lblResult.BackColor = Color.White;
            lblId.Text = this.doc.documentNfId.ToString();
            
            string alias = doc.typeAlias;

            //Normaliza Alias para corrigir erro de classificação e reprocessamento

            alias = alias.Replace("ContraCapa", "Caderneta");
            alias = alias.Replace("Capa", "Caderneta");
            alias = alias.Replace("Detalhe", "Caderneta");
            alias = alias.Replace("Equipamentos", "Caderneta");

            lblTipo.Text = alias;

            int minSize = Convert.ToInt32(_wsBasicTable.GetParameters("MIN_SIZE_BLANK_PAGE", "10000"));

            //if (lastAlias != alias)
            //{
                log.Debug("Initializing Classifier: " + alias);
                classify = null;
                
                classify = new ClassifyINMET(SN, alias);
                log.Debug("Classifier initialized");
                lastAlias = alias;
            //}
            
            try
            {
                log.Debug("Iniciando o processamento OCR do arquivo : " + docNfQueueDto[0].documentNfId.ToString());
       
                string filename = this._wsRepository.GetFileWithExtension(docNfQueueDto[0].documentNfId, docNfQueueDto[0].documentNfFileType, false);

                bool isCapa = _wsDocumentNf.IsCapa(docNfQueueDto[0].documentNfId, docNfQueueDto[0].batchNfId);

                //==================================================================

                if (isCapa)
                {

                    if (alias == "923Jan1987Caderneta" ||
                        alias == "922AJan1987Caderneta" ||
                        alias == "922Dez1981Caderneta" ||
                        alias == "922Jul1980Caderneta" ||
                        alias == "922BNov1994Caderneta" ||
                        alias == "922CRev2001Caderneta" ||
                        alias == "922CAbr1998Caderneta")
                    {

                        //Checa modelo da capa
                        string recType = classify._oAbbyy.RecognizeType(docNfQueueDto[0].documentNfId, 
                            docNfQueueDto[0].batchNfId, filename, alias, docNfQueueDto[0].batchNfTotalDocs, docNfQueueDto[0].workflowId, isCapa);

                        //Checa modelo novamente com aplicação de Rotate
                        if (recType == "")
                        {
                            recType = classify._oAbbyy.RecognizeType(docNfQueueDto[0].documentNfId,
                            docNfQueueDto[0].batchNfId, filename, alias, docNfQueueDto[0].batchNfTotalDocs, docNfQueueDto[0].workflowId, false);
                        }

                        //Se erro conhecido 922 /923 Jan 1987 Cancela o lote e gera lista para recaptura
                        if (recType != "" && recType.Replace("Caderneta", "").Replace("_", "") != alias.Replace("Caderneta", "") && (alias == "922AJan1987Caderneta" || alias == "923Jan1987Caderneta"))
                        {
                            log.Debug("Capa Divergente do tipo reconhecido! : " + recType + " / " + alias);
                            
                            string destDir = Application.StartupPath + "\\ERR_Type\\";                                                      

                            _wsCancelNF.CancelBatchNF(doc.batchNfId, "Erro na classificação: Original: [" + alias + "] Correto:  [" + recType + "]");

                            File.AppendAllText(destDir + "logErrType.txt", "Lote: " + doc.batchNfId.ToString() + "; Docto: " + docNfQueueDto[0].documentNfId + " Origem: [" + alias + "] Sugerido  [" + recType + "]" + Environment.NewLine);

                            if (!System.IO.Directory.Exists(destDir))
                                System.IO.Directory.CreateDirectory(destDir);

                            string destFileName = destDir + "\\" + System.IO.Path.GetFileName(filename);

                            System.IO.File.Copy(filename, destFileName, true);
                        }
                        //Se erro para demais modelos analisar
                        else if (recType != "" && recType.Replace("Caderneta", "").Replace("_", "") != alias.Replace("Caderneta", ""))
                        {
                            log.Debug("Capa Divergente do tipo reconhecido! : " + recType + " / " + alias);

                            string destDir = Application.StartupPath + "\\ERR_Type\\";

                            if (!System.IO.Directory.Exists(destDir))
                                System.IO.Directory.CreateDirectory(destDir);

                            string destFileName = destDir + "\\" + System.IO.Path.GetFileName(filename);

                            System.IO.File.Copy(filename, destFileName, true);

                            File.AppendAllText(destDir + "logErrType.txt", "Lote: " + doc.batchNfId.ToString() + "; Docto: " + docNfQueueDto[0].documentNfId + " Origem: [" + alias + "] Sugerido  [" + recType + "]" + Environment.NewLine);

                        }

                        //==================================================================

                        log.Debug("ReInitializing Classifier: " + alias);
                        classify = null;

                        classify = new ClassifyINMET(SN, alias);
                        log.Debug("Classifier ReInitialized");
                        lastAlias = alias;
                    }

                    //==================================================================
                
                }

                ////Trava para não processar documentos splitados errados(segundo Danilo, q tbm pediu pra retirar em nossos tests)
                //BinarizeNF obin = new BinarizeNF();

                //string binarizedFile = filename.ToUpper().Replace(".JPG", ".TIF");
                //obin.BinarizeFile(filename, binarizedFile, 0);
                //FileInfo fi = new FileInfo(binarizedFile);

                //log.Debug("FileName:" + binarizedFile + " - File:" + fi.Length + "/" + minSize);
                
                //if (fi.Length < minSize) //Se for menor que X bytes deve ser página em branco. Neste caso pula doc.
                //{
                //    log.Debug("Erro: Página em branco! : Tamanho: " + fi.Length.ToString());

                //    Application.DoEvents();

                //    uxProgressBar1.Value = uxProgressBar1.Maximum - 1;
                //    timerProc.Enabled = true;
                //    timerProc.Start();

                //    return;
                //}

                log.Debug("Before ProcessDoc: " + filename);

                simpleViewer1.Filename = filename;
                Application.DoEvents();
                
                //Trava provissório para não processar a 921, pois depende de ajustes no wkf
               // if (!alias.Contains("921"))
                //{
                    ProcessDoc(docNfQueueDto[0].documentNfId, docNfQueueDto[0].batchNfId, filename, alias, docNfQueueDto[0].batchNfTotalDocs, docNfQueueDto[0].workflowId, isCapa);
                //}
                //else
                //{
                  //  log.Debug("Tipo em stand by");
                  //  lblIgnored.Text = (Convert.ToInt32(lblIgnored.Text) + 1).ToString();
                  //  Application.DoEvents();

                   // uxProgressBar1.Value = uxProgressBar1.Maximum - 1;
                   // timerProc.Enabled = true;
                   // timerProc.Start();

                   // return;
               // }
                
                lblProcessed.Text = (Convert.ToInt32(lblProcessed.Text) + 1).ToString();
                int min = System.DateTime.Now.Minute;

                string aux = System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute;

                if (aux != productionStr[min])
                {
                    productionStr[min] = aux;
                    produtionValue[min] = 0;
                }

                produtionValue[min]++;

                int dph = 0;
                for(int i=0;i<60;i++)
                {
                    dph = dph + produtionValue[i];
                }

                if (dph > 17)
                {
                    lblDPH.Text = dph.ToString();
                }

                lblDPH.Text = dph.ToString();

            }
            catch (Exception ex)
            {
                this.log.Error("ERROR1: " + ex);
                            
            }

            Application.DoEvents();

            uxProgressBar1.Value = uxProgressBar1.Maximum - 1;
            timerProc.Enabled = true;
            timerProc.Start();
        }

        private void RecognizeType()
        {
        }

        private void ProcessDoc(long docId, long batchNfId, string filename, string alias, int batchNfTotalDocs, long workflowId, bool isCapa)
        {
            
            Application.DoEvents();
            log.Debug("ProcessDoc:" + docId + " - " + filename + " - " + alias);

            string fileNamePreProcessed = "";

            if (System.IO.Path.GetExtension(filename).ToUpper().Contains("JPG"))
                fileNamePreProcessed = filename.Replace(".jpg", ".preprocessed.jpg");
            else
                fileNamePreProcessed = filename.Replace(".tif", ".preprocessed.tif");

            log.Debug("Preprocessed Filename: " + fileNamePreProcessed);

            if (System.IO.File.Exists(fileNamePreProcessed))
                File.Delete(fileNamePreProcessed);

            log.Debug("BeforeProcessImage:" + filename);

            if (classify._oAbbyy == null)
                log.Error("Objeto ABBYY não instanciado");

            string exportFile = classify._oAbbyy.ProcessImage(filename, "XML", ref fileNamePreProcessed, alias, Application.StartupPath, isCapa, false, batchNfId);
            string xml = "";

            if (exportFile == "RETRY")
            {
                exportFile = classify._oAbbyy.ProcessImage(filename, "XML", ref fileNamePreProcessed, alias, Application.StartupPath, isCapa, true, batchNfId);
            }

            if (exportFile == "")
            {
                countError++;
                if (countError > 40)
                {
                    MessageBox.Show("Trava de segurança processamento nulo. Lote atual: [" + batchNfId + "]");
                    Application.Exit();
                }
            }
            else
                countError = 0;
            
            if (System.IO.File.Exists(exportFile))
                xml = System.IO.File.ReadAllText(exportFile);

            if (xml.Length > 100)
                lblResult.BackColor = Color.Green;
            else
                lblResult.BackColor = Color.Yellow;

            log.Debug("Uploading File: " + fileNamePreProcessed + " - Size XML: " + xml.Length.ToString());

            //Realiza Upload Img corrigida (JPG)
            if (System.IO.File.Exists(fileNamePreProcessed))
            {
                log.Debug("Uploading Image: " + fileNamePreProcessed);
                _wsRepository.Upload(fileNamePreProcessed, docId, false, 999999999);
            }

            log.Debug("Updating BD");
            
            //Grava dados
            _wsProcessingNf.ProcessOcrFieldCaderneta(docId, batchNfId, batchNfTotalDocs, workflowId, xml);

            log.Debug("ProcessDoc finished: Batch:" + batchNfId + " - Doc:" + docId.ToString());
            log.Debug(".....................................................................");

            Application.DoEvents();
        }

        private void Robot_INMET_Load(object sender, EventArgs e)
        {

        }
    }
}
