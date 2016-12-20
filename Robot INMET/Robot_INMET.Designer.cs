namespace INMET
{
    partial class Robot_INMET
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerConnection = new System.Windows.Forms.Timer(this.components);
            this.timerProc = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.uxVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.uxInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.uxProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTipo = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSN = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblProcessed = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDPH = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblINIT = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.simpleViewer1 = new FlexImage.Viewer.SimpleViewer();
            this.lblIgnored = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timerConnection
            // 
            this.timerConnection.Interval = 1000;
            this.timerConnection.Tick += new System.EventHandler(this.timerConnection_Tick);
            // 
            // timerProc
            // 
            this.timerProc.Interval = 1000;
            this.timerProc.Tick += new System.EventHandler(this.timerProc_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxVersion,
            this.uxInfo,
            this.uxProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 567);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(739, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // uxVersion
            // 
            this.uxVersion.Name = "uxVersion";
            this.uxVersion.Size = new System.Drawing.Size(42, 17);
            this.uxVersion.Text = "-------";
            // 
            // uxInfo
            // 
            this.uxInfo.Name = "uxInfo";
            this.uxInfo.Size = new System.Drawing.Size(22, 17);
            this.uxInfo.Text = "     ";
            // 
            // uxProgressBar1
            // 
            this.uxProgressBar1.Name = "uxProgressBar1";
            this.uxProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblIgnored);
            this.groupBox1.Controls.Add(this.lblMode);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.lblResult);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblTipo);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblIP);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lblVersion);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblServer);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblSN);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.lblProcessed);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblDPH);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblINIT);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 366);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(738, 198);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // lblMode
            // 
            this.lblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMode.Location = new System.Drawing.Point(635, 142);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(72, 22);
            this.lblMode.TabIndex = 22;
            this.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(602, 146);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "Mode:";
            // 
            // lblResult
            // 
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult.Location = new System.Drawing.Point(87, 142);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(26, 22);
            this.lblResult.TabIndex = 20;
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Result:";
            // 
            // lblTipo
            // 
            this.lblTipo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTipo.Location = new System.Drawing.Point(362, 81);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(191, 22);
            this.lblTipo.TabIndex = 18;
            this.lblTipo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(293, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Tipo:";
            // 
            // lblIP
            // 
            this.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIP.Location = new System.Drawing.Point(87, 111);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(191, 22);
            this.lblIP.TabIndex = 16;
            this.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 115);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "IP:";
            // 
            // lblVersion
            // 
            this.lblVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVersion.Location = new System.Drawing.Point(362, 16);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(191, 22);
            this.lblVersion.TabIndex = 14;
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(293, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Version:";
            // 
            // lblServer
            // 
            this.lblServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblServer.Location = new System.Drawing.Point(87, 81);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(191, 22);
            this.lblServer.TabIndex = 12;
            this.lblServer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Server:";
            // 
            // lblSN
            // 
            this.lblSN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSN.Location = new System.Drawing.Point(87, 16);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(191, 22);
            this.lblSN.TabIndex = 10;
            this.lblSN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "S/N:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::INMET.Properties.Resources.LOGOTIPO_OFICIAL_INMET___Pequeno;
            this.pictureBox1.Location = new System.Drawing.Point(605, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(102, 90);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // lblProcessed
            // 
            this.lblProcessed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProcessed.Location = new System.Drawing.Point(362, 139);
            this.lblProcessed.Name = "lblProcessed";
            this.lblProcessed.Size = new System.Drawing.Size(191, 22);
            this.lblProcessed.TabIndex = 7;
            this.lblProcessed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(293, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Docs Proc.:";
            // 
            // lblDPH
            // 
            this.lblDPH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDPH.Location = new System.Drawing.Point(362, 110);
            this.lblDPH.Name = "lblDPH";
            this.lblDPH.Size = new System.Drawing.Size(191, 22);
            this.lblDPH.TabIndex = 5;
            this.lblDPH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(293, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Docs/h:";
            // 
            // lblINIT
            // 
            this.lblINIT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblINIT.Location = new System.Drawing.Point(87, 48);
            this.lblINIT.Name = "lblINIT";
            this.lblINIT.Size = new System.Drawing.Size(191, 22);
            this.lblINIT.TabIndex = 3;
            this.lblINIT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Init Time:";
            // 
            // lblId
            // 
            this.lblId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblId.Location = new System.Drawing.Point(362, 48);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(191, 22);
            this.lblId.TabIndex = 1;
            this.lblId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(293, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // simpleViewer1
            // 
            this.simpleViewer1.BackColor = System.Drawing.Color.White;
            this.simpleViewer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.simpleViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.simpleViewer1.Filename = null;
            this.simpleViewer1.IsCaptureMode = false;
            this.simpleViewer1.Location = new System.Drawing.Point(0, 0);
            this.simpleViewer1.Name = "simpleViewer1";
            this.simpleViewer1.Size = new System.Drawing.Size(739, 363);
            this.simpleViewer1.TabIndex = 11;
            // 
            // lblIgnored
            // 
            this.lblIgnored.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIgnored.Location = new System.Drawing.Point(362, 165);
            this.lblIgnored.Name = "lblIgnored";
            this.lblIgnored.Size = new System.Drawing.Size(191, 22);
            this.lblIgnored.TabIndex = 23;
            this.lblIgnored.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(293, 169);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Docs Igno.:";
            // 
            // Robot_INMET
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 589);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.simpleViewer1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Robot_INMET";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Robot INMET - Cadernetas";
            this.Load += new System.EventHandler(this.Robot_INMET_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerConnection;
        private System.Windows.Forms.Timer timerProc;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel uxVersion;
        private System.Windows.Forms.ToolStripStatusLabel uxInfo;
        private System.Windows.Forms.ToolStripProgressBar uxProgressBar1;
        private FlexImage.Viewer.SimpleViewer simpleViewer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblProcessed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDPH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblINIT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblSN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblIgnored;
    }
}

