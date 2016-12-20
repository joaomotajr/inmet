// ============================================
// 
// FlexImage.Viewer
// SimpleViewer.cs
// 28/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Accusoft.ImagXpressSdk;
using Accusoft.PrintProSdk;
using FlexImage.Core;
using FlexImage.WebService;
using Telerik.WinControls.UI;
using Accusoft.NotateXpressSdk;
using BackStyle = Accusoft.NotateXpressSdk.BackStyle;

#endregion

namespace FlexImage.Viewer
{
    public partial class SimpleViewer : UserControl
    {
        #region Delegates

        public delegate void InfoClickedDelegate(object sender, EventArgs e);

        public delegate void LogClickedDelegate(object sender, EventArgs e);

        #endregion

        //private readonly SignBussiness _sign;
        private readonly WSRepository _wsRepository = GenericSingleton<WSRepository>.GetInstance();
        
        private Double _dZoomFactor;
        private long _documentId;
        private string _extension;
        private string _filename;
        private bool _isFront;

        private bool _isCaptureMode;
        
        public SimpleViewer()
        {
            this.InitializeComponent();
            this.imagXpress1.Licensing.UnlockRuntime(1908224503, 373713104, 1341088068, 5785);

            this.uxImageXView1.MouseWheelCapture = true;
            //this._sign = new SignBussiness();

            this.printPro1.Licensing.UnlockRuntime(2052484501, 498091594, 1466175470, 7017);
            
            // set up toolbar defaults
            // These are set globally for all toolbars, they can also be set on a layer basis.
            notateXpress1.ToolbarDefaults.StampToolbarDefaults.Text = "Data Aprovação: " + DateTime.Now.ToString();
            notateXpress1.ToolbarDefaults.StampToolbarDefaults.TextFont = new Font("Arial", 14, FontStyle.Italic);
            notateXpress1.ToolbarDefaults.TextToolbarDefaults.TextFont = new Font("Arial", 14, FontStyle.Regular);

            notateXpress1.ToolbarDefaults.ImageToolbarDefaults.BackStyle = BackStyle.Transparent;
            notateXpress1.ToolbarDefaults.ImageToolbarDefaults.TransparentColor = Color.White;
            notateXpress1.ToolbarDefaults.ImageToolbarDefaults.PenWidth = 0;
            notateXpress1.ToolbarDefaults.ImageToolbarDefaults.ToolTipText = "Anotação de Imagens";
            notateXpress1.ToolbarDefaults.ImageToolbarDefaults.AutoSize = Accusoft.NotateXpressSdk.AutoSize.ToContents;

            string signFile= @"D:\Documentos\PESSOAIS\PESSOAIS\Assinatura\Assinatura.png";

            if (System.IO.File.Exists(signFile))
            {
                ImageX imageToolImage;
                imageToolImage = ImageX.FromFile(imagXpress1, signFile);
                IntPtr dibHandle = imageToolImage.ToHdib(true);
                notateXpress1.ToolbarDefaults.ImageToolbarDefaults.DibHandle = dibHandle;

                //resource cleanup
                if (imageToolImage != null)
                {
                    imageToolImage.Dispose();
                    imageToolImage = null;
                }
            }
        }

        public void ZoomType(enumTypeId typeId)
        {
            switch (typeId)
            {
                case enumTypeId.ARRECADACAO:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.AUTORIZACAO_DEBITO:
                    {
                        this.DoZoomFitToWidth();
                        break;
                    }
                case enumTypeId.AVISO_CREDITO:
                    {
                        this.DoZoomFitToWidth();
                        break;
                    }
                case enumTypeId.AVISO_DEBITO:
                    {
                        this.DoZoomFitToWidth();
                        break;
                    }
                case enumTypeId.BORDERO_CUSTODIA:
                    {
                        this.DoZoomFitToWidth();
                        break;
                    }
                case enumTypeId.BORDERO_TRUNCAGEM:
                    {
                        this.DoZoomFitToWidth();
                        break;
                    }
                case enumTypeId.CAPA_CREDITO:
                    {
                        this.DoZoomFitToWidth();
                        break;
                    }
                case enumTypeId.CAPA_DEBITO:
                    {
                        this.DoZoomFitToWidth();
                        break;
                    }
                case enumTypeId.CHEQUE:
                    {
                        this.DoZoomFitToWidth();
                        break;
                    }
                case enumTypeId.CHEQUE_CUSTODIA:
                    {
                        this.DoZoomFitToWidth();
                        break;
                    }
                case enumTypeId.DARF_CB:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.DARF_SB:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.DARF_SIMPLES_CB:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.DARF_SIMPLES_SB:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.DOC:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.ENVELOPE_AS:
                    {
                        this.DoZoomFitToWidth();
                        break;
                    }
                case enumTypeId.ENVELOPE_ATM:
                    {
                        this.DoZoomFitToWidth();
                        break;
                    }
                case enumTypeId.FDR:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.FGTS_CB:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.FGTS_SB:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.FICHA_DEPOSITO:
                    {
                        this.DoZoomFitToWidth();
                        break;
                    }
                case enumTypeId.GARE_CB:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.GARE_SB:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.GPS_CB:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.GPS_SB:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.MALOTE_PJ:
                    {
                        this.DoZoomFitToWidth();
                        break;
                    }
                case enumTypeId.NAO_CLASSIFICADO:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.NAO_CLASSIFICADO_NFIN:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.ORDEM_PAGAMENTO:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.PAGTO_AVULSO_CARTAO:
                    {
                        this.DoZoomFitToWidth();
                        break;
                    }
                case enumTypeId.REGULARIZACAO_CREDITO:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.REGULARIZACAO_DEBITO:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.TARIFA:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.TED:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.TITULO:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.TRANSFERENCIA_CC:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.TVD:
                    {
                        this.DoZoomFitToHeight();
                        break;
                    }
                case enumTypeId.VOUCHER:
                    {
                        this.DoZoomFitToWidth();
                        break;
                    }
            }
        }

        private void ResetSystem()
        {
            Accusoft.NotateXpressSdk.LoadOptions lo;
            lo = new Accusoft.NotateXpressSdk.LoadOptions();

            lo.UnicodeMode = true;  // Set up NX for Unicode
            lo.AnnType = AnnotationType.NotateXpressXml;
            notateXpress1.Layers.SetLoadOptions(lo);

            notateXpress1.Layers.Clear();
            notateXpress1.ClientWindow = uxImageXView1.Handle;
            
            // turn on the built in toolbar
            notateXpress1.ToolbarDefaults.ToolbarActivated = false;
            //if (notateXpress1.Layers.Count > 0)  // make sure we have a layer on an image
            //    notateXpress1.Layers[0].Toolbar.Visible = true;

            //// pre start the ImagXpress toolbar
            //uxImageXView1.Toolbar.Activated = true;
            //uxImageXView1.Toolbar.Visible = false;

            notateXpress1.FontScaling = FontScaling.ResolutionY;
            notateXpress1.LineScaling = LineScaling.ResolutionY;
            notateXpress1.MultiLineEdit = true; // The more sophisticated editor.

            // make sure we have a layer to draw upon.
            if (notateXpress1.Layers.Count == 0)  // we could have a built in layer from the image.
                if (uxImageXView1.Image != null) // make sure we loaded an image
                    notateXpress1.Layers.Add(new Layer());
            
        }

        public string Filename
        {
            get { return this._filename; }
            set
            {
                this.ClearImage();

                this._filename = value;

                if (this._filename != null)
                {
                    if (!_isCaptureMode)
                    {
                        string aux = Path.GetFileNameWithoutExtension(Path.GetFileName(this._filename));

                        if (aux.Contains("F"))
                            this._isFront = true;
                        else
                            this._isFront = false;
             
                        if (aux != null)
                            aux = aux.Replace("F", "").Replace("V", "").Replace("B","");

                        if (aux.Contains("."))
                            aux = Path.GetFileNameWithoutExtension(aux);

                        _documentId = Convert.ToInt64(aux);
                    }

                    var extension = Path.GetExtension(this._filename);
                    if (extension != null)
                        this._extension = extension.ToUpper();

                    if (File.Exists(this._filename))
                    {
                        this.uxImageXView1.Visible = true;
                        try
                        {
                            if (this._extension != ".P7S")
                            {
                                //Documento não está assinado
                                this.uxImageXView1.Image = ImageX.FromFile(this.imagXpress1, value, 1);
                            }
                            //else
                            //{
                            //    //Documento está assinado digitalmente
                            //    this.uxMenuCertificate.Enabled = true;
                            //    this.uxImageXView1.Image = ImageX.FromByteArray(this.imagXpress1,
                            //                                                    this._sign.RemoveSignatures(value));
                            //}
                        }
                        catch (Exception e)
                        {
                            Alert.Exclamation("Error: DocumentViewer: Set_Filename: " + value + "/" + e);
                        }
                        //this.uxImageXView1.Refresh();
                        //this.uxImageXView1.ZoomToFit(ZoomToFitType.FitWidth);
                        this._dZoomFactor = this.uxImageXView1.ZoomFactor;

                        this.ResetSystem();

                        this.uxMenuEmail.Enabled = true;
                        this.uxMenuFitToPage.Enabled = true;
                        this.uxMenuFitToWidth.Enabled = true;
                        this.uxMenuInfo.Enabled = true;
                        
                        
                        this.uxMenuLog.Enabled = true;
                        this.uxMenuNegate.Enabled = true;
                        this.uxMenuPrint.Enabled = true;
                        this.uxMenuRotateLeft.Enabled = true;
                        this.uxMenuRotateRight.Enabled = true;
                        this.uxMenuSave.Enabled = true;
                        this.uxMenuZoomIn.Enabled = true;
                        this.uxMenuZoomOut.Enabled = true;

                        this.uxMenuStamp.Enabled = true;

                        //if (this._isFront)
                            this.uxMenuBack.Enabled = true;

                        if (this._extension != "JPG")
                            this.uxMenuJPG.Enabled = true;
                        else
                            this.uxMenuJPG.Enabled = false;

                    }
                }
            }
        }

        public bool IsCaptureMode
        {
            get { return this._isCaptureMode; }
            set
            {
                this._isCaptureMode = value;

                if (_isCaptureMode)
                {
                    uxMenuInfo.Visible = false;
                    uxMenuBack.Visible = false;
                    uxMenuCertificate.Visible = false;
                    uxMenuEmail.Visible = false;
                    uxMenuJPG.Visible = false;
                    uxMenuLog.Visible = false;
                    uxMenuPrint.Visible = false;
                    uxMenuSave.Visible = false;
                }
            }
        }

        public event InfoClickedDelegate EvInfoClicked;

        public void ApplyMask(enumTypeId typeId)
        {
            switch (typeId)
            {
                case (enumTypeId.CHEQUE):
                    {
                        //Máscara clientes (Abaixo assinatura)
                        var maskAss = new Rectangle(690, 352, 631, 117);
                        this.processor1.Image = this.uxImageXView1.Image;
                        this.processor1.SetRegion(maskAss, RegionType.Rectangle);
                        this.processor1.EnableRegion = true;
                        this.processor1.Ripple(13, 13, RippleDirection.Both);
                        this.uxImageXView1.Image = this.processor1.Image;
                        break;
                    }
            }
        }

        public void ClearImage()
        {
            this._extension = "";
            this._filename = "";

            // Dispose the ImageX object if the ImageXView has one
            try
            {
                if (this.uxImageXView1.Image != null)
                {
                    this.uxImageXView1.Image.Dispose();
                    this.uxImageXView1.Image = null;
                }
            }
            catch
            {
            }

            this.uxMenuBack.Enabled = false;
            this.uxMenuEmail.Enabled = false;
            this.uxMenuFitToPage.Enabled = false;
            this.uxMenuFitToWidth.Enabled = false;
            this.uxMenuInfo.Enabled = false;
            this.uxMenuJPG.Enabled = false;
            this.uxMenuLog.Enabled = false;
            this.uxMenuNegate.Enabled = false;
            this.uxMenuPrint.Enabled = false;
            this.uxMenuRotateLeft.Enabled = false;
            this.uxMenuRotateRight.Enabled = false;
            this.uxMenuSave.Enabled = false;
            this.uxMenuZoomIn.Enabled = false;
            this.uxMenuZoomOut.Enabled = false;
            this.uxMenuStamp.Enabled = false;

            this.uxMenuCertificate.Enabled = false;
            this.uxImageXView1.Image = null;

        }

        public void SetInfoXml(XmlDocument doc)
        {
            for (int i = this.uxGridViewInfo.Rows.Count - 1; i >= 0; i--)
                this.uxGridViewInfo.Rows.RemoveAt(i);

            XmlElement root = doc.DocumentElement;
            if (root != null)
            {
                XmlNodeList nodeList = root.SelectNodes("*");
                if (nodeList != null)
                {
                    foreach (XmlNode node in nodeList)
                        this.uxGridViewInfo.Rows.Add(node.Name, node.InnerText);
                }
            }
        }

        public void SetLog(string xml)
        {
        }

        private void FlexImgLoad(object sender, EventArgs e)
        {
            this.uxImageXView1.Height = this.Size.Height - this.uxmenu1.Size.Height;
            this.uxGridViewInfo.Height = this.uxImageXView1.Height;
            this.uxGridViewLog.Height = this.uxImageXView1.Height;

            this.InitGridInfo();
            this.InitGridLog();
        }

        private GridViewDataColumn CreateFilesTemplateViewColumn(GridViewDataColumn col, string fieldName,
                                                                 string headerText, Type dataType)
        {
            if (col != null)
            {
                col.UniqueName = col.FieldName = fieldName;
                col.HeaderText = headerText;
                col.ReadOnly = true;
                col.DataType = dataType;
                col.IsVisible = true;
                return col;
            }
            return null;
        }

        private void InitGridLog()
        {
            List<GridViewColumn> columnsTemplate;

            columnsTemplate = new List<GridViewColumn>();
            GridViewDataColumn col;

            col = this.CreateFilesTemplateViewColumn(new GridViewTextBoxColumn(), "TimeStamp", "Data/Hora",
                                                     typeof (DateTime));
            col.Width = 75;
            col.AllowResize = true;
            columnsTemplate.Add(col);

            col = this.CreateFilesTemplateViewColumn(new GridViewTextBoxColumn(), "User", "Usuário", typeof(string));
            col.Width = 75;
            col.AllowResize = true;
            columnsTemplate.Add(col);

            col = this.CreateFilesTemplateViewColumn(new GridViewTextBoxColumn(), "Station", "Estação", typeof(string));
            col.Width = 75;
            col.AllowResize = true;
            columnsTemplate.Add(col);
            
            col = this.CreateFilesTemplateViewColumn(new GridViewTextBoxColumn(), "Action", "Ação", typeof (string));
            col.Width = 450;
            col.AllowResize = true;
            columnsTemplate.Add(col);

            this.uxGridViewInfo.TableElement.BeginUpdate();
            for (int i = 0; i < columnsTemplate.Count; i++)
                this.uxGridViewLog.MasterTemplate.Columns.Add((GridViewDataColumn) columnsTemplate[i]);

            this.uxGridViewLog.MasterTemplate.AllowColumnChooser = false;
            this.uxGridViewLog.MasterTemplate.AllowDragToGroup = false;
            this.uxGridViewLog.MasterTemplate.AllowEditRow = false;
            this.uxGridViewLog.MasterTemplate.AllowDeleteRow = false;
            this.uxGridViewLog.MasterTemplate.AllowAddNewRow = false;
            this.uxGridViewLog.MasterTemplate.EnableSorting = true;
            this.uxGridViewLog.MasterTemplate.EnableGrouping = false;
            this.uxGridViewLog.MasterTemplate.EnableFiltering = false;
            this.uxGridViewLog.MasterTemplate.ShowGroupedColumns = false;
            this.uxGridViewLog.MasterTemplate.ShowRowHeaderColumn = false;

            this.uxGridViewLog.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            this.uxGridViewLog.TableElement.RowHeight = 35;
            this.uxGridViewLog.TableElement.EndUpdate();
        }

        private void InitGridInfo()
        {
            List<GridViewColumn> columnsTemplate = new List<GridViewColumn>();
            GridViewDataColumn col;

            col = this.CreateFilesTemplateViewColumn(new GridViewTextBoxColumn(), "Campo", "Campo", typeof (string));
            col.Width = 150;
            col.AllowResize = true;
            columnsTemplate.Add(col);

            col = this.CreateFilesTemplateViewColumn(new GridViewTextBoxColumn(), "Descrição", "Descrição",
                                                     typeof (string));
            col.Width = 300;
            col.AllowResize = true;
            columnsTemplate.Add(col);

            this.uxGridViewInfo.TableElement.BeginUpdate();
            for (int i = 0; i < columnsTemplate.Count; i++)
                this.uxGridViewInfo.MasterTemplate.Columns.Add((GridViewDataColumn) columnsTemplate[i]);

            this.uxGridViewInfo.MasterTemplate.AllowColumnChooser = false;
            this.uxGridViewInfo.MasterTemplate.AllowDragToGroup = false;
            this.uxGridViewInfo.MasterTemplate.AllowEditRow = false;
            this.uxGridViewInfo.MasterTemplate.AllowDeleteRow = false;
            this.uxGridViewInfo.MasterTemplate.AllowAddNewRow = false;
            this.uxGridViewInfo.MasterTemplate.EnableSorting = true;
            this.uxGridViewInfo.MasterTemplate.EnableGrouping = false;
            this.uxGridViewInfo.MasterTemplate.EnableFiltering = false;
            this.uxGridViewInfo.MasterTemplate.ShowGroupedColumns = false;
            this.uxGridViewInfo.MasterTemplate.ShowRowHeaderColumn = false;

            this.uxGridViewInfo.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.uxGridViewInfo.TableElement.RowHeight = 35;
            this.uxGridViewInfo.TableElement.EndUpdate();
        }

        private void MenuZoomInClick(object sender, EventArgs e)
        {
            this.DoZoomIn();
        }

        private void MenuZoomToFitClick(object sender, EventArgs e)
        {
            this.DoZoomToFitBest();
        }

        private void MenuZoomOutClick(object sender, EventArgs e)
        {
            this.DoZoomOut();
        }

        private void MenuRotateLeftClick(object sender, EventArgs e)
        {
            try
            {
                this.processor1.Image = this.uxImageXView1.Image.Copy();
                this.processor1.Rotate(+90);
                this.uxImageXView1.Image = this.processor1.Image;
            }
            catch (ImagXpressException eX)
            {
                MessageBox.Show(eX.Message + @" " + eX.Source);
            }
        }

        private void MenuZoomRightClick(object sender, EventArgs e)
        {
            try
            {
                this.processor1.Image = this.uxImageXView1.Image.Copy();
                this.processor1.Rotate(-90);
                this.uxImageXView1.Image = this.processor1.Image;
            }
            catch (ImagXpressException eX)
            {
                MessageBox.Show(eX.Message + @" " + eX.Source);
            }
        }

        private void MenuNegateClick(object sender, EventArgs e)
        {
            try
            {
                this.processor1.Image = this.uxImageXView1.Image.Copy();
                this.processor1.Negate();
                this.uxImageXView1.Image = this.processor1.Image;
            }
            catch (ImagXpressException eX)
            {
                MessageBox.Show(eX.Message + @" " + eX.Source);
            }
        }

        private void MenuInfoClick(object sender, EventArgs e)
        {
            if (this.uxGridViewInfo.Visible)
            {
                this.uxImageXView1.Visible = true;
                this.uxGridViewInfo.Visible = false;
            }
            else
            {
                this.uxImageXView1.Visible = false;
                this.uxGridViewLog.Visible = false;
                this.uxGridViewInfo.Visible = true;

                this.ShowInfo();
            }

            if (this.EvInfoClicked != null)
                this.EvInfoClicked(sender, e);
        }

        private void MenuLogClick(object sender, EventArgs e)
        {
            if (this.uxGridViewLog.Visible)
            {
                this.uxImageXView1.Visible = true;
                this.uxGridViewLog.Visible = false;
            }
            else
            {
                this.uxImageXView1.Visible = false;
                this.uxGridViewLog.Visible = true;
                this.uxGridViewInfo.Visible = false;

                this.ShowLog();
            }

            if (this.EvInfoClicked != null)
                this.EvInfoClicked(sender, e);
        }

        private void ShowLog()
        {
            //this.uxGridViewLog.Rows.Clear();

            //if (this._documentId == 0)
            //{
            //}

            //documentLogDto[] logArray = this._wsDocument.GetDocumentLog(this._documentId);

            //foreach (documentLogDto item in logArray)
            //{
            //    GridViewRowInfo rowInfo = this.uxGridViewLog.Rows.AddNew();
            //    rowInfo.Cells[0].Value = item.logDate;
            //    rowInfo.Cells[1].Value = item.logUsr;
            //    rowInfo.Cells[2].Value = item.logStation;
            //    rowInfo.Cells[3].Value = item.log;
            //    //rowInfo.Cells[4].Value = item.logDescription
            //    rowInfo.Height = 40;
            //}
        }

        private void ShowInfo()
        {
            //this.uxGridViewInfo.Rows.Clear();

            //documentDto docDto = this._wsDocument.GetDocument(this._documentId);

            //var doc = new XmlDocument();
            //doc.LoadXml(docDto.documentXml);

            //XmlNodeList nodes = doc.SelectNodes("/XMLDATA");

            //if (nodes != null && nodes.Count!=0)
            //{
            //    XmlNodeList childs = nodes[0].ChildNodes;

            //    foreach (XmlNode child in childs)
            //    {
            //        GridViewRowInfo rowInfo = this.uxGridViewInfo.Rows.AddNew();
            //        rowInfo.Cells[0].Value = child.Name;
            //        rowInfo.Cells[1].Value = child.InnerText;
            //        rowInfo.Height = 40;
            //    }
            //}
        }

        private void MenuSaveClick(object sender, EventArgs e)
        {
            //var saveFileDialog1 = new SaveFileDialog
            //                          {
            //                              Filter =
            //                                  @"Image files (*." + this._extension + @")|*." + this._extension +
            //                                  @"|All files (*.*)|*.*",
            //                              FilterIndex = 1,
            //                              RestoreDirectory = true
            //                          };

            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        File.Copy(_filename, saveFileDialog1.FileName);
            //    }
            //    catch (Exception ex)
            //    {
            //        Alert.Exclamation(ex.Message);
            //    }
            //}

            this.saveImage();
        }

        private void MenuPrintClick(object sender, EventArgs e)
        {
            // Note: all of the dimensions in this demo are in twips.
            try
            {
                Accusoft.PrintProSdk.Printer printer;  // generic Printer object, this is a real printer or a file depending on the user's selection
                Accusoft.PrintProSdk.PrintJob job;     // object representation of a print job

                /* Prompt the user for the desired printer.
                 * The dialog can be circumvented by specifying a printer name as a string
                 * or by specifying only the PrintPro object to select the default printer. */
                printer = Accusoft.PrintProSdk.Printer.SelectPrinter(printPro1, true);

                
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                if (printer != null)
                {
                    /* Create a PrintJob object for the above-selected Printer. If the printer is not specified, PrintPRO
                     * will just use Windows's default printer. */
                    job = new Accusoft.PrintProSdk.PrintJob(printer);
                    job.Name = "FlexImage - Doc [" + _documentId + "]";

                    /* Print all of the information on the page */
                    PrintPage(job);

                    /* Finish the print job to end the current page and print the document */
                    job.Finish();

                }
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        void PrintPage(Accusoft.PrintProSdk.PrintJob job)
        {
            // Note: all of the dimensions in this demo are in twips.
            System.Drawing.Image picture = System.Drawing.Image.FromFile(_filename);

            System.Drawing.Font font;
            System.Drawing.Rectangle bounds;

            font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold);

            /* Print a banner at the top of the page, white on black. */
            job.Draw.BackStyle = Accusoft.PrintProSdk.BackStyle.Opaque;
            job.Draw.ForeColor = System.Drawing.Color.Black;
            job.Draw.BackColor = System.Drawing.Color.White;
            job.Draw.TextAligned(
                new System.Drawing.RectangleF(1000, job.TopMargin, job.PaperWidth - 2000, 1100 - job.TopMargin),
                " Documento [" + _documentId + "] ", font, Accusoft.PrintProSdk.Alignment.CenterJustifyMiddle);

            /* The following code demonstrates how PrintPRO can print text aligned inside
             * a rectangular area. First a box is drawn then text drawn aligned inside
             * the box.
             */
            job.Draw.Width = 1;
            job.Draw.BackStyle = Accusoft.PrintProSdk.BackStyle.Transparent;
            job.Draw.ForeColor = System.Drawing.Color.Black;
            font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);

            /* Print a sample image onto the page using a .NET Image. There is also a PrintDib() method for printing
             * bitmaps from ImagXpress or other components.
             *
             * Note: Since "aspect" is true, the source and source size parameters are assumed to be (0,0) and the
             * size of the original image, respectively. */

            float w = uxImageXView1.Image.Width;
            float h = uxImageXView1.Image.Height;

            int x2 = 0;
            int y2 = 0;

            float ratio = w/h;

            if ( w > h )
            {
                x2 = 6000;
                y2 = Convert.ToInt32(x2/ratio);
            }
            else
            {
                y2 = 10000;
                x2 = Convert.ToInt32(y2 * ratio);
            }

            job.PrintImage(picture, new System.Drawing.PointF(1640, 1640), new System.Drawing.SizeF(x2, y2), true);

            this.ShowLog();
            
            font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            job.LeftMargin = 1440;
            job.Draw.CurrentX = 1440;
            job.Draw.CurrentY = y2+1850;
            job.Draw.Text("Dados do Documento", font);
            font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Regular);

            for (int i = 0; i < uxGridViewInfo.Rows.Count; i++)
            {
                string aux = uxGridViewInfo.Rows[i].Cells[0].Value + " : " + uxGridViewInfo.Rows[i].Cells[1].Value;
                job.Draw.Text(aux, font);
            }

            font = new System.Drawing.Font("Arial", 8);
            job.Draw.CurrentY = job.PrintHeight - job.Draw.TextHeight(font, "This page");
            job.Draw.Text("Informação Confidencial - " + Network.GetComputerName() + " - " + Network.GetLocalIP() + " - " + System.DateTime.Now.ToString(), font);
        }

        private void MenuEmailClick(object sender, EventArgs e)
        {
        }

        private void DoZoomIn()
        {
            this._dZoomFactor += 0.05;
            try
            {
                this.uxImageXView1.ZoomFactor = Convert.ToSingle(this._dZoomFactor);
            }
            catch (ImagXpressException eX)
            {
                MessageBox.Show(eX.Message + @" " + eX.Source);
            }
        }

        public void DoZoomToFitBest()
        {
            try
            {
                this.uxImageXView1.ZoomToFit(ZoomToFitType.FitBest);
                this._dZoomFactor = this.uxImageXView1.ZoomFactor;
            }
            catch (ImagXpressException eX)
            {
                MessageBox.Show(eX.Message + @" " + eX.Source);
            }
        }

        public void DoZoomFitToHeight()
        {
            try
            {
                this.uxImageXView1.ZoomToFit(ZoomToFitType.FitHeight);
                this._dZoomFactor = this.uxImageXView1.ZoomFactor;
            }
            catch (ImagXpressException eX)
            {
                MessageBox.Show(eX.Message + @" " + eX.Source);
            }
        }

        public void DoZoomFitToWidth()
        {
            try
            {
                this.uxImageXView1.ZoomToFit(ZoomToFitType.FitWidth);
                this._dZoomFactor = this.uxImageXView1.ZoomFactor;
            }
            catch (ImagXpressException eX)
            {
                MessageBox.Show(eX.Message + @" " + eX.Source);
            }
        }

        private void DoZoomOut()
        {
            this._dZoomFactor -= 0.05;
            try
            {
                this.uxImageXView1.ZoomFactor = Convert.ToSingle(this._dZoomFactor);
            }
            catch (ImagXpressException eX)
            {
                MessageBox.Show(eX.Message + @" " + eX.Source);
            }
        }


        private void UxMenuFitToWidthClick(object sender, EventArgs e)
        {
            try
            {
                this.uxImageXView1.ZoomToFit(ZoomToFitType.FitWidth);
                this._dZoomFactor = this.uxImageXView1.ZoomFactor;
            }
            catch (ImagXpressException eX)
            {
                MessageBox.Show(eX.Message + @" " + eX.Source);
            }
        }

        private void UxMenuExportClick(object sender, EventArgs e)
        {
        }

        private void UxMenuJpgClick(object sender, EventArgs e)
        {

            //uxMenuJPG.Enabled = false;

            //documentDto currentDoc = this._wsDocument.GetDocument(this._documentId);

            //if (currentDoc.documentFileTypeFront == "JPG")
            //{
            //    string filename = this._wsRepository.GetFileWithExtension(this._documentId, "JPG", this._isFront);
            //    this.Filename = filename;
            //}
            //else
            //{
            //    //Solicita JPG avulso
            //    try
            //    {
            //        this._wsDocument.RequestJpgDetached(this._documentId, this._isFront);
            //    }
            //    catch (Exception ex)
            //    {
            //        if (ex.Message == "br.com.flexdoc.flexps.exception.BusinessException: business.jpgback.requestAlreadyExists")
            //            Alert.Information("Solicitação de JPG está em processamento!");
            //        else
            //            Alert.Information(ex.ToString());
            //    }
            //}
        }

        private void UxMenuBackClick(object sender, EventArgs e)
        {
            //if (this._isFront)
            //{

            //    documentDto d = _wsDocument.GetDocument(this._documentId);

            //    if (d.documentFileTypeBack != null)
            //    {
            //        string filename = this._wsRepository.GetFileWithExtension(this._documentId,d.documentFileTypeBack.Replace(".", ""), true);
            //        this.Filename = filename;
            //        this._isFront = false;
            //    }
            //    else
            //    {
            //        //Solicita Verso avulso
            //        try
            //        {
            //            this._wsDocument.RequestTifBackDetached(this._documentId);
            //            Alert.Information("Solicitação de Verso realizada!");
            //        }
            //        catch (Exception ex)
            //        {
            //            if (ex.Message == "br.com.flexdoc.flexps.exception.BusinessException: business.tifback.requestAlreadyExists")
            //                Alert.Information("Solicitação de Verso está em processamento!");
            //            else
            //                Alert.Information(ex.ToString());
            //        }
            //    }

            //}
            //else
            //{
            //    string filename = this._wsRepository.GetFileWithExtension(this._documentId, this._extension.Replace(".", ""), false);
            //    this.Filename = filename;                 
            //    this._isFront = true;                
            //}
        }

        private void UxMenuCertificateClick(object sender, EventArgs e)
        {
            //var sign = new SignBussiness();
            //if (SignWrappers.IsSigned(this._filename))
            //    sign.DisplayCertificate(this._filename);
            //else
            //    Alert.Information("Documento não está assinado digitalmente!");
        }

        public void EnableRequestJpgButton(bool value)
        {
            this.uxMenuJPG.Enabled = value;
        }

        private void uxMenuStamp_Click(object sender, EventArgs e)
        {
            // reminder:  NotateXpress Toolbars are configured per layer
            Layer currentLayer;

            currentLayer = notateXpress1.Layers.Selected();

            bool flag = !currentLayer.Toolbar.Visible;

            notateXpress1.ToolbarDefaults.ToolbarActivated = flag;
            if (flag)
            {
                currentLayer.Toolbar.Visible = flag;
            }
            else
            {
                saveImage();
            }

        }


        private void BrandAnnotation()
        {
            try
            {
                //brand annotations on current image at 24-bit
                notateXpress1.Layers.Brand(24);

                //delete annotations because now they've been branded in and we don't
                //need a duplicate of selectable annotations
                notateXpress1.Layers.Clear();

                // make sure we have a layer to draw upon.
                notateXpress1.Layers.Add(new Layer());

                // turn on the built in toolbar
                notateXpress1.ToolbarDefaults.ToolbarActivated = true;
                notateXpress1.Layers[0].Toolbar.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured in the Demo. " + ex.Message);
            }
        }

        private void saveImage()
        {

            Accusoft.NotateXpressSdk.SaveOptions so;
            so = new Accusoft.NotateXpressSdk.SaveOptions();

            so.AnnType = AnnotationType.ImagingForWindows; // aka Wang, aka Kodak
            notateXpress1.ImagXpressSave = true;
            notateXpress1.Layers.SetSaveOptions(so);
        
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            // A reduced list of supported file formats
            saveFileDialog1.FileName = "";
            saveFileDialog1.Title = "Please save the new image";

            System.String strDefaultImageFilter = "Windows Bitmap (*.BMP)|*.bmp" +
                                                  "|JPEG (*.JPG)|*.jpg" +
                                                  "|Portable Network Graphics (*.PNG)|*.png" +
                                                  "|Tagged Image Format G4 (*.TIFF)|*.tif;*.tiff";

            Accusoft.ImagXpressSdk.SaveOptions soOpts = new Accusoft.ImagXpressSdk.SaveOptions();

            saveFileDialog1.Filter = strDefaultImageFilter;
            saveFileDialog1.FilterIndex = 4;  // default to tiff

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                this.BrandAnnotation();

                if (saveFileDialog1.FilterIndex == 1) // bmp
                {
                    soOpts.Format = ImageXFormat.Bmp;
                    soOpts.Bmp.Compression = Compression.Rle;
                }
                else if (saveFileDialog1.FilterIndex == 2) // jpg
                {
                    soOpts.Format = ImageXFormat.Jpeg;
                }
                else if (saveFileDialog1.FilterIndex == 3) // png
                {
                    soOpts.Format = ImageXFormat.Png;
                }
                else if (saveFileDialog1.FilterIndex == 4) // tiff G4
                {
                    soOpts.Format = ImageXFormat.Tiff;
                    soOpts.Tiff.Compression = Compression.Group4;
                    soOpts.Tiff.MultiPage = false;
                }

                processor1 = new Processor(imagXpress1, uxImageXView1.Image);
                processor1.Image.Save(saveFileDialog1.FileName, soOpts);
            }
        }

        

    }
}