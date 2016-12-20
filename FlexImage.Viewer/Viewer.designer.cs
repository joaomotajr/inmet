namespace FlexImage.Viewer
{
    partial class Viewer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Don't forget to dispose IX
                //

                if (imagXpress1 != null)
                {
                    imagXpress1.Dispose();
                    imagXpress1 = null;
                }

                if (processor1 != null)
                {
                    processor1.Dispose();
                    processor1 = null;
                }
                if (uxImageXView1 != null)
                {
                    uxImageXView1.Dispose();
                    uxImageXView1 = null;
                }

                this.pdfViewer1.Dispose();
    
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Viewer));
            this.menuPrint = new System.Windows.Forms.ToolStripButton();
            this.menuEmail = new System.Windows.Forms.ToolStripButton();
            this.menuBurn = new System.Windows.Forms.ToolStripButton();
            this.menuOpenWidth = new System.Windows.Forms.ToolStripButton();
            this.menuItem16 = new System.Windows.Forms.ToolStripButton();
            this.menuItem1 = new System.Windows.Forms.ToolStripButton();
            this.uxmenu1 = new System.Windows.Forms.ToolStrip();
            this.uxMenuZoomIn = new System.Windows.Forms.ToolStripButton();
            this.uxMenuFitToPage = new System.Windows.Forms.ToolStripButton();
            this.uxMenuFitToWidth = new System.Windows.Forms.ToolStripButton();
            this.uxMenuZoomOut = new System.Windows.Forms.ToolStripButton();
            this.uxMenuRotateLeft = new System.Windows.Forms.ToolStripButton();
            this.uxMenuRotateRight = new System.Windows.Forms.ToolStripButton();
            this.uxMenuNegate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.uxMenuInfo = new System.Windows.Forms.ToolStripButton();
            this.uxMenuLog = new System.Windows.Forms.ToolStripButton();
            this.uxMenuSave = new System.Windows.Forms.ToolStripButton();
            this.uxMenuExport = new System.Windows.Forms.ToolStripButton();
            this.uxMenuPrint = new System.Windows.Forms.ToolStripButton();
            this.uxMenuEmail = new System.Windows.Forms.ToolStripButton();
            this.uxMenuCertificate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.uxMenuPreviousPage = new System.Windows.Forms.ToolStripButton();
            this.uxMenuGotoPage = new System.Windows.Forms.ToolStripButton();
            this.uxMenuNextPage = new System.Windows.Forms.ToolStripButton();
            this.uxImageXView1 = new Accusoft.ImagXpressSdk.ImageXView(this.components);
            this.processor1 = new Accusoft.ImagXpressSdk.Processor(this.components);
            this.imagXpress1 = new Accusoft.ImagXpressSdk.ImagXpress(this.components);
            this.uxGridViewInfo = new Telerik.WinControls.UI.RadGridView();
            this.uxGridViewLog = new Telerik.WinControls.UI.RadGridView();
            this.pdfViewer1 = new FlexImage.PDFView.PDFViewer();
            this.menuItem9 = new System.Windows.Forms.ToolStripButton();
            this.uxmenu1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxGridViewInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxGridViewInfo.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxGridViewLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxGridViewLog.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // menuPrint
            // 
            this.menuPrint.Enabled = false;
            this.menuPrint.Name = "menuPrint";
            this.menuPrint.Size = new System.Drawing.Size(23, 23);
            this.menuPrint.Text = "Zoom-";
            // 
            // menuEmail
            // 
            this.menuEmail.Enabled = false;
            this.menuEmail.Name = "menuEmail";
            this.menuEmail.Size = new System.Drawing.Size(23, 23);
            this.menuEmail.Text = "E-mail";
            // 
            // menuBurn
            // 
            this.menuBurn.Enabled = false;
            this.menuBurn.Name = "menuBurn";
            this.menuBurn.Size = new System.Drawing.Size(23, 23);
            this.menuBurn.Text = "Burn";
            // 
            // menuOpenWidth
            // 
            this.menuOpenWidth.Enabled = false;
            this.menuOpenWidth.Name = "menuOpenWidth";
            this.menuOpenWidth.Size = new System.Drawing.Size(23, 23);
            this.menuOpenWidth.Text = "Open With ";
            // 
            // menuItem16
            // 
            this.menuItem16.Name = "menuItem16";
            this.menuItem16.Size = new System.Drawing.Size(23, 23);
            this.menuItem16.Text = "Help";
            // 
            // menuItem1
            // 
            this.menuItem1.Name = "menuItem1";
            this.menuItem1.Size = new System.Drawing.Size(23, 23);
            this.menuItem1.Text = "menuItem1";
            // 
            // uxmenu1
            // 
            this.uxmenu1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uxmenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxMenuZoomIn,
            this.uxMenuFitToPage,
            this.uxMenuFitToWidth,
            this.uxMenuZoomOut,
            this.uxMenuRotateLeft,
            this.uxMenuRotateRight,
            this.uxMenuNegate,
            this.toolStripSeparator2,
            this.uxMenuInfo,
            this.uxMenuLog,
            this.uxMenuSave,
            this.uxMenuExport,
            this.uxMenuPrint,
            this.uxMenuEmail,
            this.uxMenuCertificate,
            this.toolStripSeparator1,
            this.uxMenuPreviousPage,
            this.uxMenuGotoPage,
            this.uxMenuNextPage});
            this.uxmenu1.Location = new System.Drawing.Point(0, 418);
            this.uxmenu1.Name = "uxmenu1";
            this.uxmenu1.Size = new System.Drawing.Size(613, 25);
            this.uxmenu1.TabIndex = 0;
            this.uxmenu1.Text = "menu1";
            // 
            // uxMenuZoomIn
            // 
            this.uxMenuZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uxMenuZoomIn.Image = global::FlexImage.Viewer.Resources.Zoom_In;
            this.uxMenuZoomIn.Name = "uxMenuZoomIn";
            this.uxMenuZoomIn.Size = new System.Drawing.Size(23, 22);
            this.uxMenuZoomIn.Text = "Zoom +";
            this.uxMenuZoomIn.ToolTipText = "Zoom +";
            this.uxMenuZoomIn.Click += new System.EventHandler(this.MenuZoomInClick);
            // 
            // uxMenuFitToPage
            // 
            this.uxMenuFitToPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uxMenuFitToPage.Image = global::FlexImage.Viewer.Resources.Zoom_fit;
            this.uxMenuFitToPage.Name = "uxMenuFitToPage";
            this.uxMenuFitToPage.Size = new System.Drawing.Size(23, 22);
            this.uxMenuFitToPage.Text = "Zoom Fit";
            this.uxMenuFitToPage.ToolTipText = "Ajustar à Página";
            this.uxMenuFitToPage.Click += new System.EventHandler(this.MenuZoomToFitClick);
            // 
            // uxMenuFitToWidth
            // 
            this.uxMenuFitToWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uxMenuFitToWidth.Image = global::FlexImage.Viewer.Resources.Swap;
            this.uxMenuFitToWidth.Name = "uxMenuFitToWidth";
            this.uxMenuFitToWidth.Size = new System.Drawing.Size(23, 22);
            this.uxMenuFitToWidth.Text = "Swap";
            this.uxMenuFitToWidth.ToolTipText = "Ajustar à Largura";
            this.uxMenuFitToWidth.Click += new System.EventHandler(this.UxMenuFitToWidthClick);
            // 
            // uxMenuZoomOut
            // 
            this.uxMenuZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uxMenuZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("uxMenuZoomOut.Image")));
            this.uxMenuZoomOut.Name = "uxMenuZoomOut";
            this.uxMenuZoomOut.Size = new System.Drawing.Size(23, 22);
            this.uxMenuZoomOut.Text = "Zoom -";
            this.uxMenuZoomOut.ToolTipText = "Zoom -";
            this.uxMenuZoomOut.Click += new System.EventHandler(this.MenuZoomOutClick);
            // 
            // uxMenuRotateLeft
            // 
            this.uxMenuRotateLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uxMenuRotateLeft.Image = global::FlexImage.Viewer.Resources.RotateLeft;
            this.uxMenuRotateLeft.Name = "uxMenuRotateLeft";
            this.uxMenuRotateLeft.Size = new System.Drawing.Size(23, 22);
            this.uxMenuRotateLeft.Text = "Rotate Left";
            this.uxMenuRotateLeft.ToolTipText = "Girar para Esquerda";
            this.uxMenuRotateLeft.Click += new System.EventHandler(this.MenuRotateLeftClick);
            // 
            // uxMenuRotateRight
            // 
            this.uxMenuRotateRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uxMenuRotateRight.Image = global::FlexImage.Viewer.Resources.RotateRight;
            this.uxMenuRotateRight.Name = "uxMenuRotateRight";
            this.uxMenuRotateRight.Size = new System.Drawing.Size(23, 22);
            this.uxMenuRotateRight.Text = "Rotate Right";
            this.uxMenuRotateRight.ToolTipText = "Girar para Direita";
            this.uxMenuRotateRight.Click += new System.EventHandler(this.MenuZoomRightClick);
            // 
            // uxMenuNegate
            // 
            this.uxMenuNegate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uxMenuNegate.Image = global::FlexImage.Viewer.Resources.Negate;
            this.uxMenuNegate.Name = "uxMenuNegate";
            this.uxMenuNegate.Size = new System.Drawing.Size(23, 22);
            this.uxMenuNegate.Text = "Negate";
            this.uxMenuNegate.ToolTipText = "Negativo";
            this.uxMenuNegate.Click += new System.EventHandler(this.MenuNegateClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // uxMenuInfo
            // 
            this.uxMenuInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uxMenuInfo.Image = global::FlexImage.Viewer.Resources.Info;
            this.uxMenuInfo.Name = "uxMenuInfo";
            this.uxMenuInfo.Size = new System.Drawing.Size(23, 22);
            this.uxMenuInfo.Text = "Info";
            this.uxMenuInfo.ToolTipText = "Dados do Documento";
            this.uxMenuInfo.Click += new System.EventHandler(this.MenuInfoClick);
            // 
            // uxMenuLog
            // 
            this.uxMenuLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uxMenuLog.Image = global::FlexImage.Viewer.Resources.Log;
            this.uxMenuLog.Name = "uxMenuLog";
            this.uxMenuLog.Size = new System.Drawing.Size(23, 22);
            this.uxMenuLog.Text = "Log";
            this.uxMenuLog.ToolTipText = "Log do Documento";
            this.uxMenuLog.Click += new System.EventHandler(this.MenuLogClick);
            // 
            // uxMenuSave
            // 
            this.uxMenuSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uxMenuSave.Image = global::FlexImage.Viewer.Resources.Save;
            this.uxMenuSave.Name = "uxMenuSave";
            this.uxMenuSave.Size = new System.Drawing.Size(23, 22);
            this.uxMenuSave.Text = "Save";
            this.uxMenuSave.ToolTipText = "Gravar Imagem";
            this.uxMenuSave.Click += new System.EventHandler(this.MenuSaveClick);
            // 
            // uxMenuExport
            // 
            this.uxMenuExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uxMenuExport.Image = ((System.Drawing.Image)(resources.GetObject("uxMenuExport.Image")));
            this.uxMenuExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uxMenuExport.Name = "uxMenuExport";
            this.uxMenuExport.Size = new System.Drawing.Size(23, 22);
            this.uxMenuExport.Text = "Import images to the PDF file format";
            this.uxMenuExport.Click += new System.EventHandler(this.UxMenuExportClick);
            // 
            // uxMenuPrint
            // 
            this.uxMenuPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uxMenuPrint.Image = global::FlexImage.Viewer.Resources.Printer;
            this.uxMenuPrint.Name = "uxMenuPrint";
            this.uxMenuPrint.Size = new System.Drawing.Size(23, 22);
            this.uxMenuPrint.Text = "Print";
            this.uxMenuPrint.ToolTipText = "Imprimir imagem";
            this.uxMenuPrint.Click += new System.EventHandler(this.MenuPrintClick);
            // 
            // uxMenuEmail
            // 
            this.uxMenuEmail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uxMenuEmail.Image = global::FlexImage.Viewer.Resources.Mail;
            this.uxMenuEmail.Name = "uxMenuEmail";
            this.uxMenuEmail.Size = new System.Drawing.Size(23, 22);
            this.uxMenuEmail.Text = "Email";
            this.uxMenuEmail.ToolTipText = "Enviar imagem por email";
            this.uxMenuEmail.Click += new System.EventHandler(this.MenuEmailClick);
            // 
            // uxMenuCertificate
            // 
            this.uxMenuCertificate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uxMenuCertificate.Image = global::FlexImage.Viewer.Resources.Certificate;
            this.uxMenuCertificate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uxMenuCertificate.Name = "uxMenuCertificate";
            this.uxMenuCertificate.Size = new System.Drawing.Size(23, 22);
            this.uxMenuCertificate.Text = "Certificado Digital";
            this.uxMenuCertificate.Click += new System.EventHandler(this.UxMenuCertificateClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // uxMenuPreviousPage
            // 
            this.uxMenuPreviousPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uxMenuPreviousPage.Image = ((System.Drawing.Image)(resources.GetObject("uxMenuPreviousPage.Image")));
            this.uxMenuPreviousPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uxMenuPreviousPage.Name = "uxMenuPreviousPage";
            this.uxMenuPreviousPage.Size = new System.Drawing.Size(23, 22);
            this.uxMenuPreviousPage.Text = "Página Anterior";
            this.uxMenuPreviousPage.Click += new System.EventHandler(this.UxMenuPreviousPageClick);
            // 
            // uxMenuGotoPage
            // 
            this.uxMenuGotoPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.uxMenuGotoPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uxMenuGotoPage.Name = "uxMenuGotoPage";
            this.uxMenuGotoPage.Size = new System.Drawing.Size(28, 22);
            this.uxMenuGotoPage.Text = "0/0";
            this.uxMenuGotoPage.Click += new System.EventHandler(this.UxMenuGotoPageClick);
            // 
            // uxMenuNextPage
            // 
            this.uxMenuNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uxMenuNextPage.Image = ((System.Drawing.Image)(resources.GetObject("uxMenuNextPage.Image")));
            this.uxMenuNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uxMenuNextPage.Name = "uxMenuNextPage";
            this.uxMenuNextPage.Size = new System.Drawing.Size(23, 22);
            this.uxMenuNextPage.Text = "Próxima Página";
            this.uxMenuNextPage.Click += new System.EventHandler(this.UxMenuNextPageClick);
            // 
            // uxImageXView1
            // 
            this.uxImageXView1.BackColor = System.Drawing.Color.White;
            this.uxImageXView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxImageXView1.Location = new System.Drawing.Point(0, 0);
            this.uxImageXView1.Name = "uxImageXView1";
            this.uxImageXView1.Size = new System.Drawing.Size(613, 418);
            this.uxImageXView1.TabIndex = 1;
            // 
            // uxGridViewInfo
            // 
            this.uxGridViewInfo.Location = new System.Drawing.Point(0, 240);
            this.uxGridViewInfo.Name = "uxGridViewInfo";
            this.uxGridViewInfo.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            // 
            // 
            // 
            this.uxGridViewInfo.RootElement.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.uxGridViewInfo.Size = new System.Drawing.Size(613, 99);
            this.uxGridViewInfo.TabIndex = 4;
            this.uxGridViewInfo.Text = "radGridView1";
            this.uxGridViewInfo.Visible = false;
            // 
            // uxGridViewLog
            // 
            this.uxGridViewLog.Location = new System.Drawing.Point(0, 338);
            this.uxGridViewLog.Name = "uxGridViewLog";
            this.uxGridViewLog.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            // 
            // 
            // 
            this.uxGridViewLog.RootElement.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.uxGridViewLog.Size = new System.Drawing.Size(613, 80);
            this.uxGridViewLog.TabIndex = 5;
            this.uxGridViewLog.Text = "radGridView1";
            this.uxGridViewLog.Visible = false;
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.AllowBookmarks = true;
            this.pdfViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfViewer1.FileName = null;
            this.pdfViewer1.Location = new System.Drawing.Point(0, 0);
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.Size = new System.Drawing.Size(613, 418);
            this.pdfViewer1.TabIndex = 6;
            this.pdfViewer1.UseXPDF = true;
            // 
            // menuItem9
            // 
            this.menuItem9.Image = ((System.Drawing.Image)(resources.GetObject("menuItem9.Image")));
            this.menuItem9.Name = "menuItem9";
            this.menuItem9.Size = new System.Drawing.Size(23, 23);
            this.menuItem9.Text = "Zoom+";
            // 
            // Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pdfViewer1);
            this.Controls.Add(this.uxImageXView1);
            this.Controls.Add(this.uxGridViewLog);
            this.Controls.Add(this.uxGridViewInfo);
            this.Controls.Add(this.uxmenu1);
            this.Name = "Viewer";
            this.Size = new System.Drawing.Size(613, 443);
            this.Load += new System.EventHandler(this.FlexImgLoad);
            this.uxmenu1.ResumeLayout(false);
            this.uxmenu1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxGridViewInfo.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxGridViewInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxGridViewLog.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxGridViewLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton menuItem9;
        private System.Windows.Forms.ToolStripButton menuPrint;
        private System.Windows.Forms.ToolStripButton menuEmail;
        private System.Windows.Forms.ToolStripButton menuBurn;
        private System.Windows.Forms.ToolStripButton menuOpenWidth;
        private System.Windows.Forms.ToolStripButton menuItem16;
        private System.Windows.Forms.ToolStripButton menuItem1;
        private System.Windows.Forms.ToolStrip uxmenu1;
        private System.Windows.Forms.ToolStripButton uxMenuZoomIn;
        private System.Windows.Forms.ToolStripButton uxMenuFitToPage;
        private System.Windows.Forms.ToolStripButton uxMenuZoomOut;
        private System.Windows.Forms.ToolStripButton uxMenuRotateLeft;
        private System.Windows.Forms.ToolStripButton uxMenuRotateRight;
        private System.Windows.Forms.ToolStripButton uxMenuNegate;
        private System.Windows.Forms.ToolStripButton uxMenuFitToWidth;
        private System.Windows.Forms.ToolStripButton uxMenuInfo;
        private System.Windows.Forms.ToolStripButton uxMenuLog;
        private System.Windows.Forms.ToolStripButton uxMenuSave;
        private System.Windows.Forms.ToolStripButton uxMenuPrint;
        private System.Windows.Forms.ToolStripButton uxMenuEmail;
        private Accusoft.ImagXpressSdk.ImageXView uxImageXView1;
        private Accusoft.ImagXpressSdk.Processor processor1;
        private Accusoft.ImagXpressSdk.ImagXpress imagXpress1;
        private Telerik.WinControls.UI.RadGridView uxGridViewInfo;
        private Telerik.WinControls.UI.RadGridView uxGridViewLog;
        private System.Windows.Forms.ToolStripButton uxMenuPreviousPage;
        private System.Windows.Forms.ToolStripButton uxMenuGotoPage;
        private System.Windows.Forms.ToolStripButton uxMenuNextPage;
        private FlexImage.PDFView.PDFViewer pdfViewer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton uxMenuExport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton uxMenuCertificate;
        
    }
}
