// ============================================
// 
// FlexImage.Core
// StandardForm.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using Telerik.WinControls.UI;

#endregion

namespace FlexImage.Core
{
    public partial class StandardForm : RadForm
    {
        public StandardForm()
        {
            this.InitializeComponent();
            this.radLabelElement1.Text = "";
            this.radLabelElement2.Text = "";
            this.radLabelElement3.Text = "";
            this.radLabelElement4.Text = "";
            this.radLabelElement5.Text = "";
        }

        public void SetToolBarType(ToolbarStandard.toolbarType type)
        {
            this.toolbarStandard1.SetToolbarType(type);
        }

        public void toolbarStandard1_OnExitClicked(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        public void toolbarStandard1_OnHelpedClicked(object sender, EventArgs e)
        {
            Alert.Exclamation("Não implementado!");
        }

        public void toolbarStandard1_OnPrintClicked(object sender, EventArgs e)
        {
            Alert.Exclamation("Não implementado!");
        }

        private void StandardForm_Resize(object sender, EventArgs e)
        {
//            toolbarStandard1.Width = this.Width;
        }

        private void StandardForm_Load(object sender, EventArgs e)
        {
        }
    }
}