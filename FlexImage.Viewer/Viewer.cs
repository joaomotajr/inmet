// ============================================
// 
// FlexImage.Viewer
// Viewer.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Accusoft.ImagXpressSdk;
using FlexImage.Core;
using Telerik.WinControls.UI;

#endregion

namespace FlexImage.Viewer
{
    public partial class Viewer : UserControl
    {
        #region Delegates

        public delegate void InfoClickedDelegate(object sender, EventArgs e);

        public delegate void LogClickedDelegate(object sender, EventArgs e);

        public delegate void SwapClickedDelegate(object sender, EventArgs e);

        #endregion

        //private int columnNum;
        private Double _dZoomFactor;
        private string _extension;
        private string _filename;

        private bool _isPdf;
        //private int pageNumber;

        //public event LogClickedDelegate evLogClicked;


        public Viewer()
        {
            this.InitializeComponent();
            this.imagXpress1.Licensing.UnlockRuntime(1908224503, 373713104, 1341088068, 5785);
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
                    if (File.Exists(this._filename))
                    {
                        var s = Path.GetExtension(value);
                        if (s != null)
                            this._extension = s.ToUpper();
                        this._isPdf = this._extension.Contains("PDF");

                        if (!this._isPdf)
                        {
                            this.pdfViewer1.Visible = false;
                            this.uxImageXView1.Visible = true;

                            try
                            {
                                this.uxImageXView1.Image = ImageX.FromFile(this.imagXpress1, value, 1);
                            }
                            catch (Exception e)
                            {
                                Alert.Exclamation("Error: DocumentViewer: Set_Filename: " + value + "/" + e);
                            }

                            if (this.uxImageXView1.Image.PageCount > 1)
                            {
                                this.uxImageXView1.Image.Page = 1;
                                this.uxMenuPreviousPage.Visible = true;
                                this.uxMenuGotoPage.Visible = true;
                                this.uxMenuNextPage.Visible = true;
                            }

                            this.uxImageXView1.AllowUpdate = false;
                            this.DoZoomToFitBest();
                            this.uxImageXView1.AllowUpdate = true;
                        }
                        else
                        {
                            var fi = new FileInfo(this._filename);
                            if (fi.Length != 0)
                            {
                                this.pdfViewer1.FileName = this._filename;
                                this.uxImageXView1.Visible = false;
                                this.pdfViewer1.Visible = true;

                                if (this.pdfViewer1.GetPageCount() > 1)
                                {
                                    this.uxMenuPreviousPage.Visible = true;
                                    this.uxMenuGotoPage.Visible = true;
                                    this.uxMenuNextPage.Visible = true;
                                }
                            }
                        }
                        this.CheckPagesBounds();
                    }
                }
                Application.DoEvents();
            }
        }

        public event InfoClickedDelegate EvInfoClicked;

        public void GotoPage(int p)
        {
            if (this._isPdf)
                this.pdfViewer1.DoGotoPage(p);
        }

        public int GetTotalPages()
        {
            int tot = 0;
            if (this._isPdf)
                tot = this.pdfViewer1.GetPageCount();
            return tot;
        }

        public string ExportToTif(string file, int page)
        {
            string ret = "";

            if (this._isPdf)
                ret = this.pdfViewer1.ExportToTif(file, page);
            return ret;
        }


        public string[] ExplodeTif(string file, string targetDir)
        {
            string[] ret;
            ret = this.pdfViewer1.ExplodeTif(file, targetDir);
            return ret;
        }

        public string[] ExplodeJpgSeq(string file, string destPath, int page)
        {
            string[] ret;
            ret = this.pdfViewer1.ExplodeJPGSeq(file, destPath, page);
            return ret;
        }

        //public event SwapClickedDelegate evSwapClicked;
        //public event LogClickedDelegate evLogClicked;
        //public event InfoClickedDelegate evInfoClicked;

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

            this.pdfViewer1.FileName = "";

            this.uxMenuPreviousPage.Visible = false;
            this.uxMenuGotoPage.Visible = false;
            this.uxMenuNextPage.Visible = false;
        }

        public void SetInfoXml(string xmlString)
        {
            for (int i = this.uxGridViewInfo.Rows.Count - 1; i >= 0; i--)
                this.uxGridViewInfo.Rows.RemoveAt(i);

            var doc = new XmlDocument();
            doc.LoadXml(xmlString);
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
            col.UniqueName = col.FieldName = fieldName;
            col.HeaderText = headerText;
            col.ReadOnly = true;
            col.DataType = dataType;
            col.IsVisible = true;
            return col;
        }

        private void InitGridLog()
        {
            List<GridViewColumn> columnsTemplate;

            columnsTemplate = new List<GridViewColumn>();
            GridViewDataColumn col;

            col = this.CreateFilesTemplateViewColumn(new GridViewTextBoxColumn(), "TimeStamp", "Data/Hora",
                                                     typeof(DateTime));
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

            col = this.CreateFilesTemplateViewColumn(new GridViewTextBoxColumn(), "Action", "Ação", typeof(string));
            col.Width = 450;
            col.AllowResize = true;
            columnsTemplate.Add(col);

            this.uxGridViewLog.TableElement.BeginUpdate();
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
            if (!this._isPdf)
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
            else
                this.pdfViewer1.DoRotateLeft();
        }

        private void MenuZoomRightClick(object sender, EventArgs e)
        {
            if (!this._isPdf)
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
            else
                this.pdfViewer1.DoRotateRight();
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
                this.uxGridViewInfo.Visible = true;
            }
            this.uxGridViewLog.Visible = false;

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
            }
            this.uxGridViewInfo.Visible = false;

            if (this.EvInfoClicked != null)
                this.EvInfoClicked(sender, e);
        }

        private void MenuSaveClick(object sender, EventArgs e)
        {
            if (!this._isPdf)
            {
            }
            else
            {
                string newFilename = "C:\\" + Path.GetFileName(this._filename);
                if (Alert.InputBox("Nome do Arquivo:", "Salvar PDF", ref newFilename) == DialogResult.OK)
                    this.pdfViewer1.DoSave(newFilename);
            }
        }

        private void MenuPrintClick(object sender, EventArgs e)
        {
            if (!this._isPdf)
            {
            }
            else
                this.pdfViewer1.DoPrint();
        }

        private void MenuEmailClick(object sender, EventArgs e)
        {
        }

        private void DoZoomIn()
        {
            if (!this._isPdf)
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
            else
                this.pdfViewer1.DoZoomIn();
        }

        private void DoZoomToFitBest()
        {
            if (!this._isPdf)
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
            else
                this.pdfViewer1.DoFitToPage();
        }

        private void DoZoomOut()
        {
            if (!this._isPdf)
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
            else
                this.pdfViewer1.DoZoomOut();
        }

        private void UxMenuPreviousPageClick(object sender, EventArgs e)
        {
            if (!this._isPdf)
                this.uxImageXView1.Image.Page = this.uxImageXView1.Image.Page - 1;
            else
                this.pdfViewer1.DoPreviousPage();
            this.CheckPagesBounds();
        }

        private void UxMenuGotoPageClick(object sender, EventArgs e)
        {
            string value = "1";
            if (Alert.InputBox("Página", "Número da Página", ref value) == DialogResult.OK)
            {
                int i = Convert.ToInt16(value);
                if (!this._isPdf)
                {
                    if ((i > 0) && (i < this.uxImageXView1.Image.PageCount))
                        this.uxImageXView1.Image.Page = i;
                }
                else
                {
                    if ((i > 0) && (i < this.pdfViewer1.GetPageCount()))
                        this.pdfViewer1.DoGotoPage(i);
                }
                this.CheckPagesBounds();
            }
        }

        private void UxMenuNextPageClick(object sender, EventArgs e)
        {
            if (!this._isPdf)
            {
                this.uxImageXView1.Image.Page = this.uxImageXView1.Image.Page + 1;
                this.CheckPagesBounds();
            }
            else
                this.pdfViewer1.DoNextPage();
        }

        private void CheckPagesBounds()
        {
            int tot;
            int page;

            if (!this._isPdf)
            {
                tot = this.uxImageXView1.Image.PageCount;
                page = this.uxImageXView1.Image.Page;
            }
            else
            {
                tot = this.pdfViewer1.GetPageCount();
                page = this.pdfViewer1.GetCurrentPage();
            }

            this.uxMenuGotoPage.Text = page + @"/" + tot;

            if (page == 1)
                this.uxMenuPreviousPage.Enabled = false;
            else
                this.uxMenuPreviousPage.Enabled = true;

            if (page == tot)
                this.uxMenuNextPage.Enabled = false;
            else
                this.uxMenuNextPage.Enabled = true;
        }

        private void UxMenuFitToWidthClick(object sender, EventArgs e)
        {
            if (!this._isPdf)
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
            else
                this.pdfViewer1.DoFitToWidth();
        }

        private void UxMenuExportClick(object sender, EventArgs e)
        {
            if (!this._isPdf)
            {
            }
            else
                this.pdfViewer1.DoExport();
        }

        private void UxMenuCertificateClick(object sender, EventArgs e)
        {
        }
    }
}