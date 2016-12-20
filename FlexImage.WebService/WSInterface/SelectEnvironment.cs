using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Xml;
using Telerik.WinControls.UI;
using FlexImage.Core;
using System.IO;

namespace FlexImage.WebService
{
    public partial class SelectEnvironment : Telerik.WinControls.UI.RadForm
    {
        private string deploy;
        private string protocol;
        private string server;
        private string port;
        private string service;
        string controlFile = System.IO.Directory.GetCurrentDirectory() + "\\CONFIG\\CONTROL.TXT";      

        public string Protocol
        {
            get { return protocol; }
            set { protocol = value; }
        }
        
        public string Server
        {
            get { return server; }
            set { server = value; }
        }

        public string Port
        {
            get { return port; }
            set { port = value; }
        }
        
        public string Service
        {
            get { return service; }
            set { service = value; }
        }
        
        public string Deploy
        {
            get { return deploy; }
            set { deploy = value; }
        }

        //Constructor
        public SelectEnvironment(XmlDocument xml)
        {
            InitializeComponent();

            radGridView1.RowCount = 0;
            radGridView1.Columns.Add("Deploy", "Deploy");
            radGridView1.Columns.Add("Protocol", "Protocol");
            radGridView1.Columns.Add("Server", "Server");
            radGridView1.Columns.Add("Port", "Port");
            radGridView1.Columns.Add("Service", "Service");
            
            radGridView1.AllowAddNewRow = false;
            radGridView1.AllowDeleteRow = false;
            radGridView1.AllowEditRow = false;

            radGridView1.Columns["Deploy"].Width = 160;
            radGridView1.Columns["Protocol"].Width = 50;
            radGridView1.Columns["Server"].Width = 130;
            radGridView1.Columns["Port"].Width = 50;
            radGridView1.Columns["Service"].Width = 110;
            
            for (int i = 0; i < 99; i++)
            {                
                System.Xml.XmlNode element;
                element = xml.SelectSingleNode("/ROOT/ENVIRONMENTS/ENVIRONMENT" + i + "/DEPLOY");
                if (element != null)
                {
                    GridViewRowInfo rowInfo = radGridView1.Rows.AddNew();

                    rowInfo.Cells["deploy"].Value = element.InnerText;

                    element = xml.SelectSingleNode("/ROOT/ENVIRONMENTS/ENVIRONMENT" + i + "/PROTOCOL");
                    if (element != null) rowInfo.Cells["Protocol"].Value = element.InnerText;

                    element = xml.SelectSingleNode("/ROOT/ENVIRONMENTS/ENVIRONMENT" + i + "/SERVER");
                    if (element != null) rowInfo.Cells["Server"].Value = element.InnerText;

                    element = xml.SelectSingleNode("/ROOT/ENVIRONMENTS/ENVIRONMENT" + i + "/PORT");
                    if (element != null) rowInfo.Cells["Port"].Value = element.InnerText;

                    element = xml.SelectSingleNode("/ROOT/ENVIRONMENTS/ENVIRONMENT" + i + "/SERVICE");
                    if (element != null) rowInfo.Cells["Service"].Value = element.InnerText;
                }
            }

            string lastDeploy = IniFile.IniReadValue(controlFile, "DEPLOY", "LASTDEPLOY");

            for (int i = 0; i < radGridView1.RowCount; i++)
            {
                if (radGridView1.Rows[i].Cells["deploy"].Value.ToString() == lastDeploy)
                {
                    radGridView1.Rows[i].IsSelected = true;
                    radGridView1.Rows[i].IsCurrent = true;
                }
                else
                    radGridView1.Rows[i].IsSelected = false;
            }

            if (radGridView1.RowCount == 1)
            {
                //ConfirmSelection();
            }

        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            ConfirmSelection();
        }

        private void ConfirmSelection()
        {
            int totRows = radGridView1.RowCount;

            for (int i = 0; i < totRows; i++)
            {
                if (radGridView1.Rows[i].IsSelected)
                {
                    deploy = radGridView1.Rows[i].Cells["deploy"].Value.ToString();
                    protocol = radGridView1.Rows[i].Cells["protocol"].Value.ToString();
                    server = radGridView1.Rows[i].Cells["server"].Value.ToString();
                    port = radGridView1.Rows[i].Cells["port"].Value.ToString();
                    service = radGridView1.Rows[i].Cells["service"].Value.ToString();

                    string controlFile = System.IO.Directory.GetCurrentDirectory() + "\\CONFIG\\CONTROL.TXT";            
                    IniFile.IniWriteValue(controlFile, "DEPLOY", "LASTDEPLOY", deploy);

                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void radGridView1_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            ConfirmSelection();
        }

        private void SelectEnvironment_Load(object sender, EventArgs e)
        {

        }

        private void radGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ConfirmSelection();
        }

        private void WriteDefaultConfig(string filename)
        {
            string aux = "";
            string dir = System.IO.Path.GetDirectoryName(filename);

            try
            {

                System.IO.Directory.CreateDirectory(dir);

                aux = aux + "<ROOT>\n";
                aux = aux + "	<ENVIRONMENTS>\n";
                aux = aux + "	   <ENVIRONMENT1>\n";
                aux = aux + "		<DEPLOY>FLEXDOC</DEPLOY>\n";
                aux = aux + "		<PROTOCOL>HTTP</PROTOCOL>\n";
                aux = aux + "		<SERVICE>/flexdoc/Service/</SERVICE>\n";
                aux = aux + "		<PORT>8080</PORT>\n";
                aux = aux + "		<SERVER>192.168.1.2</SERVER>\n";
                aux = aux + "	   </ENVIRONMENT1>\n";
                aux = aux + "	</ENVIRONMENTS>\n";
                aux = aux + "</ROOT>\n";

                // create a writer and open the file
                TextWriter tw = new StreamWriter(filename);

                // write a line of text to the file
                tw.WriteLine(aux);

                // close the stream
                tw.Close();
            }
            catch (Exception e)
            {
                Alert.Exclamation("InterfaceWS::WriteDefault:" + e.ToString());
            }
        }

    }
}
