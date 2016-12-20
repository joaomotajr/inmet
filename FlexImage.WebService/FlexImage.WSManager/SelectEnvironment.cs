// ============================================
// 
// FlexImage.WSManager
// SelectEnvironment.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using FlexImage.Core;
using Telerik.WinControls.UI;

#endregion

namespace FlexImage.WebService
{
    public partial class SelectEnvironment : RadForm
    {
        private readonly string controlFile = Directory.GetCurrentDirectory() + "\\CONFIG\\CONTROL.TXT";
        private string deploy;
        private string port;
        private string protocol;
        private string server;
        private string service;

        public SelectEnvironment()
        {
            this.InitializeComponent();
            this.Visible = false;
            this.ProcessXML();
        }

        public string Protocol
        {
            get { return this.protocol; }
            set { this.protocol = value; }
        }

        public string Server
        {
            get { return this.server; }
            set { this.server = value; }
        }

        public string Port
        {
            get { return this.port; }
            set { this.port = value; }
        }

        public string Service
        {
            get { return this.service; }
            set { this.service = value; }
        }

        public string Deploy
        {
            get { return this.deploy; }
            set { this.deploy = value; }
        }

        //Constructor

        private void ProcessXML()
        {
            string fileName = Application.StartupPath + "\\CONFIG\\CONFIG.XML";
            string aux = "";

            if (!File.Exists(fileName))
            {
                //Gera arquivo Default
                this.WriteDefaultConfig(fileName);
            }

            var xtr = new XmlTextReader(fileName);
            xtr.WhitespaceHandling = WhitespaceHandling.None;
            var xml = new XmlDocument();
            xml.Load(xtr);
            xtr.Close();

            string lastDeploy = IniFile.IniReadValue(this.controlFile, "CONNECTION", "LASTCONNECTION");
            bool bypass = false;

            try
            {
                XmlNode el2 = xml.SelectSingleNode("/ROOT/BYPASS");
                if (el2.InnerText == "1")
                    bypass = true;
            }
            catch
            {
            }

            XmlNode el = xml.SelectSingleNode("/ROOT/ENVIRONMENTS");

            if (el == null)
            {
                Alert.Exclamation("Tags <ENVIRONMENTS> não localizada no arquivo de configuração!");
                return;
            }

            this.radGridView1.RowCount = 0;
            this.radGridView1.Columns.Add("Deploy", "Deploy");
            this.radGridView1.Columns.Add("Protocol", "Protocol");
            this.radGridView1.Columns.Add("Server", "Server");
            this.radGridView1.Columns.Add("Port", "Port");
            this.radGridView1.Columns.Add("Service", "Service");

            this.radGridView1.AllowAddNewRow = false;
            this.radGridView1.AllowDeleteRow = false;
            this.radGridView1.AllowEditRow = false;

            this.radGridView1.Columns["Deploy"].Width = 160;
            this.radGridView1.Columns["Protocol"].Width = 50;
            this.radGridView1.Columns["Server"].Width = 130;
            this.radGridView1.Columns["Port"].Width = 50;
            this.radGridView1.Columns["Service"].Width = 110;

            for (int i = 0; i < 99; i++)
            {
                el = xml.SelectSingleNode("/ROOT/ENVIRONMENTS/ENVIRONMENT" + i + "/DEPLOY");
                if (el != null)
                {
                    GridViewRowInfo rowInfo = this.radGridView1.Rows.AddNew();

                    rowInfo.Cells["deploy"].Value = el.InnerText;

                    el = xml.SelectSingleNode("/ROOT/ENVIRONMENTS/ENVIRONMENT" + i + "/PROTOCOL");
                    if (el != null)
                        rowInfo.Cells["Protocol"].Value = el.InnerText;

                    el = xml.SelectSingleNode("/ROOT/ENVIRONMENTS/ENVIRONMENT" + i + "/SERVER");
                    if (el != null)
                        rowInfo.Cells["Server"].Value = el.InnerText;

                    el = xml.SelectSingleNode("/ROOT/ENVIRONMENTS/ENVIRONMENT" + i + "/PORT");
                    if (el != null)
                        rowInfo.Cells["Port"].Value = el.InnerText;

                    el = xml.SelectSingleNode("/ROOT/ENVIRONMENTS/ENVIRONMENT" + i + "/SERVICE");
                    if (el != null)
                        rowInfo.Cells["Service"].Value = el.InnerText;
                }
            }

            for (int i = 0; i < this.radGridView1.RowCount; i++)
            {
                if (this.radGridView1.Rows[i].Cells["deploy"].Value.ToString() == lastDeploy)
                {
                    this.radGridView1.Rows[i].IsSelected = true;
                    this.radGridView1.Rows[i].IsCurrent = true;
                }
                else
                    this.radGridView1.Rows[i].IsSelected = false;
            }

            if ((bypass) && (lastDeploy != ""))
                this.ConfirmSelection();
            else
            {
                this.ShowDialog();
                //this.Visible = true;
            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            this.ConfirmSelection();
        }

        private void ConfirmSelection()
        {
            int totRows = this.radGridView1.RowCount;

            for (int i = 0; i < totRows; i++)
            {
                if (this.radGridView1.Rows[i].IsSelected)
                {
                    this.deploy = this.radGridView1.Rows[i].Cells["deploy"].Value.ToString();
                    this.protocol = this.radGridView1.Rows[i].Cells["protocol"].Value.ToString();
                    this.server = this.radGridView1.Rows[i].Cells["server"].Value.ToString();
                    this.port = this.radGridView1.Rows[i].Cells["port"].Value.ToString();
                    this.service = this.radGridView1.Rows[i].Cells["service"].Value.ToString();

                    string controlFile = Directory.GetCurrentDirectory() + "\\CONFIG\\CONTROL.TXT";
                    IniFile.IniWriteValue(controlFile, "CONNECTION", "LASTCONNECTION", this.deploy);
                    this.Visible = false;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void radGridView1_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            this.ConfirmSelection();
        }

        private void SelectEnvironment_Load(object sender, EventArgs e)
        {
        }

        private void radGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.ConfirmSelection();
        }

        private void WriteDefaultConfig(string filename)
        {
            string aux = "";
            string dir = Path.GetDirectoryName(filename);

            try
            {
                Directory.CreateDirectory(dir);

                aux = aux + "<ROOT>\n";
                aux = aux + "<ENVIRONMENTS>\n";
                aux = aux + "<ENVIRONMENT1>\n";
                aux = aux + "<DEPLOY>FLEXDOC</DEPLOY>\n";
                aux = aux + "<PROTOCOL>HTTP</PROTOCOL>\n";
                aux = aux + "<SERVICE>/flexpsService/</SERVICE>\n";
                aux = aux + "<PORT>8180</PORT>\n";
                aux = aux + "<SERVER>192.168.1.2</SERVER>\n";
                aux = aux + "</ENVIRONMENT1>\n";
                aux = aux + "</ENVIRONMENTS>\n";

                aux = aux + "<ABBYY>";
                aux = aux + "<SN>SWTD10100002922743704307</SN>";
                aux = aux + "<SN2>SWTD10100002923576437063</SN2>";                
                aux = aux + "<DLLPATH>C:\\Program Files (x86)\\ABBYY SDK\\10\\FlexiCapture Engine\\Bin\\FCEngine.dll</DLLPATH>";
                aux = aux + "</ABBYY>";
                
                aux = aux + "</ROOT>\n";

                // create a writer and open the file
                TextWriter tw = new StreamWriter(filename);

                // write a line of text to the file
                tw.Write(aux);

                // close the stream
                tw.Close();
            }
            catch (Exception e)
            {
                Alert.Exclamation("InterfaceWS::WriteDefault:" + e);
            }
        }

        private void SelectEnvironment_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F10) && e.Control)
                Process.Start("explorer.exe", Directory.GetCurrentDirectory());
        }
    }
}