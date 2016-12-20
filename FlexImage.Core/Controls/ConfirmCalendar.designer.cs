namespace FlexImage.Core
{
    partial class ConfirmCalendar
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.button1 = new Telerik.WinControls.UI.RadButton();
            this.maskedEditBox1 = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.label1 = new Telerik.WinControls.UI.RadLabel();
            this.calendar1 = new System.Windows.Forms.MonthCalendar();
            this.labelTitulo = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.button1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelTitulo)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(0, 0);
            this.button1.TabIndex = 14;
            // 
            // maskedEditBox1
            // 
            this.maskedEditBox1.Location = new System.Drawing.Point(153, 223);
            this.maskedEditBox1.Mask = "99/99/9999";
            this.maskedEditBox1.Name = "maskedEditBox1";
            this.maskedEditBox1.Size = new System.Drawing.Size(68, 20);
            this.maskedEditBox1.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(33, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Confirmação da Data:";
            // 
            // calendar1
            // 
            this.calendar1.Location = new System.Drawing.Point(22, 49);
            this.calendar1.Name = "calendar1";
            this.calendar1.TabIndex = 9;
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = false;
            this.labelTitulo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.ForeColor = System.Drawing.Color.White;
            this.labelTitulo.Location = new System.Drawing.Point(0, 3);
            this.labelTitulo.Name = "labelTitulo";
            // 
            // 
            // 
            this.labelTitulo.RootElement.ForeColor = System.Drawing.Color.White;
            this.labelTitulo.Size = new System.Drawing.Size(275, 34);
            this.labelTitulo.TabIndex = 13;
            this.labelTitulo.Text = "  ";
            this.labelTitulo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConfirmCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelTitulo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.maskedEditBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.calendar1);
            this.Name = "ConfirmCalendar";
            this.Size = new System.Drawing.Size(275, 268);
            this.Load += new System.EventHandler(this.ConfirmCalendar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.button1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelTitulo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton button1;
        private Telerik.WinControls.UI.RadMaskedEditBox maskedEditBox1;
        private Telerik.WinControls.UI.RadLabel label1;
        private System.Windows.Forms.MonthCalendar calendar1;
        private Telerik.WinControls.UI.RadLabel labelTitulo;
    }
}
