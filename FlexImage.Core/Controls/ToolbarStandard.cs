// ============================================
// 
// FlexImage.Core
// ToolbarStandard.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.Windows.Forms;

#endregion

namespace FlexImage.Core
{
    public delegate void ButtonClickDelegateHandler(object sender, EventArgs e);

    public partial class ToolbarStandard : UserControl
    {
        #region toolbarType enum

        public enum toolbarType
        {
            toolbarMinimum,
            toolbarMount,
            toolbarConfirm,
            toolbarCapture,
            toolbarReCapture,
            toolbarStandard,
            toolbarClassify,
            toolbarTyping,
            toolbarTypingCTD,
            toolbarFormalistic,
            toolbarPZ,
            toolbarAprov1,
            toolbarAprov2,
            toolbarSearch,
            toolbarIndex,
            toolbarCancel,
            toolbarReceipt,
            toolbarPending,
            toolbarEdit
        };

        #endregion

        private int ct = 100;

        public ToolbarStandard()
        {
            this.InitializeComponent();
            this.Clear();
        }

        public event ButtonClickDelegateHandler OnCaptureClicked;
        public event ButtonClickDelegateHandler OnConfirmClicked;
        public event ButtonClickDelegateHandler OnRefreshClicked;
        public event ButtonClickDelegateHandler OnSearchClicked;
        public event ButtonClickDelegateHandler OnCancelClicked;
        public event ButtonClickDelegateHandler OnReClassifyClicked;

        public event ButtonClickDelegateHandler OnMarkClicked;
        public event ButtonClickDelegateHandler OnAccountClicked;

        public event ButtonClickDelegateHandler OnRecaptureClicked;
        public event ButtonClickDelegateHandler OnRequestJPGClicked;

        public event ButtonClickDelegateHandler OnPrintClicked;
        public event ButtonClickDelegateHandler OnHelpedClicked;
        public event ButtonClickDelegateHandler OnExitClicked;

        private void Clear()
        {
            this.buttonCapture.Visible = false;
            this.buttonConfirm.Visible = false;
            this.buttonRefresh.Visible = false;
            this.buttonSearch.Visible = false;
            this.buttonCancel.Visible = false;
            this.buttonReClassify.Visible = false;

            this.buttonAccount.Visible = false;
            this.buttonMark.Visible = false;

            this.buttonRecapture.Visible = false;
            this.buttonRequestJPG.Visible = false;

            this.buttonPrint.Visible = false;
            this.buttonHelp.Visible = false;
            this.buttonExit.Visible = false;
        }

        public void SetToolbarType(toolbarType type)
        {
            this.Clear();
            switch (type)
            {
                case (toolbarType.toolbarMinimum):
                    {
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = 0;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct*1;
                        break;
                    }

                case (toolbarType.toolbarMount):
                    {
                        this.buttonConfirm.Visible = true;
                        this.buttonConfirm.Left = 0;
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = this.ct * 1;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct * 2;
                        break;
                    }

                case (toolbarType.toolbarConfirm):
                    {
                        this.buttonConfirm.Visible = true;
                        this.buttonConfirm.Left = 0;
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = this.ct*1;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct*2;
                        break;
                    }

                case (toolbarType.toolbarStandard):
                    {
                        this.buttonConfirm.Visible = true;
                        this.buttonConfirm.Left = 0;
                        this.buttonPrint.Visible = true;
                        this.buttonPrint.Left = this.ct*1;
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = this.ct*2;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct*3;
                        break;
                    }
                case (toolbarType.toolbarCapture):
                    {
                        this.buttonCapture.Visible = true;
                        this.buttonCapture.Left = 0;
                        this.buttonConfirm.Visible = true;
                        this.buttonConfirm.Left = this.ct*1;
                        this.buttonCancel.Visible = true;
                        this.buttonCancel.Left = this.ct*2;
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = this.ct*3;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct*4;
                        break;
                    }

                case (toolbarType.toolbarReCapture):
                    {
                        this.buttonRefresh.Visible = true;
                        this.buttonRefresh.Left = 0;
                        this.buttonRecapture.Visible = true;
                        this.buttonRecapture.Left = this.ct*1;
                        this.buttonConfirm.Visible = true;
                        this.buttonConfirm.Left = this.ct*2;
                        this.buttonCancel.Visible = true;
                        this.buttonCancel.Left = this.ct*3;
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = this.ct*4;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct*5;
                        break;
                    }

                case (toolbarType.toolbarTyping):
                    {
                        this.buttonConfirm.Visible = true;
                        this.buttonConfirm.Left = 0;
                        this.buttonRequestJPG.Visible = true;
                        this.buttonRequestJPG.Left = this.ct*1;
                        this.buttonRecapture.Visible = true;
                        this.buttonRecapture.Left = this.ct*2;
                        this.buttonReClassify.Visible = true;
                        this.buttonReClassify.Left = this.ct*3;
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = this.ct*4;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct*5;
                        break;
                    }

                case (toolbarType.toolbarTypingCTD):
                    {
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = 0;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct*1;
                        break;
                    }


                case (toolbarType.toolbarFormalistic):
                    {
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = 0;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct*1;
                        break;
                    }

                case (toolbarType.toolbarClassify):
                    {
                        this.buttonConfirm.Visible = true;
                        this.buttonConfirm.Left = 0;
                        this.buttonRequestJPG.Visible = true;
                        this.buttonRequestJPG.Left = this.ct*1;
                        this.buttonRecapture.Visible = true;
                        this.buttonRecapture.Left = this.ct*2;
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = this.ct*3;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct*4;
                        break;
                    }

                case (toolbarType.toolbarIndex):
                    {
                        this.buttonConfirm.Visible = true;
                        this.buttonConfirm.Left = 0;
                        this.buttonCancel.Visible = true;
                        this.buttonCancel.Left = this.ct*1;
                        //buttonRequestJPG.Visible = true;
                        //buttonRequestJPG.Left = ct * 2;
                        this.buttonRecapture.Visible = true;
                        this.buttonRecapture.Left = this.ct*2;
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = this.ct*3;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct*4;
                        break;
                    }


                case (toolbarType.toolbarPZ):
                    {
                        this.buttonConfirm.Visible = true;
                        this.buttonConfirm.Left = 0;
                        this.buttonMark.Visible = true;
                        this.buttonMark.Left = this.ct*1;
                        this.buttonCancel.Visible = true;
                        this.buttonCancel.Left = this.ct*2;
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = this.ct*3;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct*4;
                        break;
                    }

                case (toolbarType.toolbarAprov1):
                    {
                        this.buttonConfirm.Visible = true;
                        this.buttonConfirm.Left = 0;
                        this.buttonMark.Visible = true;
                        this.buttonMark.Left = this.ct * 1;
                        this.buttonAccount.Visible = true;
                        this.buttonAccount.Left = this.ct * 2;
                        this.buttonCancel.Visible = true;
                        this.buttonCancel.Left = this.ct * 3;
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = this.ct * 4;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct * 5;
                        break;
                    }

                case (toolbarType.toolbarAprov2):
                    {
                        this.buttonConfirm.Visible = true;
                        this.buttonConfirm.Left = 0;
                        this.buttonAccount.Visible = true;
                        this.buttonAccount.Left = this.ct*1;
                        this.buttonCancel.Visible = true;
                        this.buttonCancel.Left = this.ct*2;
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = this.ct*3;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct*4;
                        break;
                    }

                case (toolbarType.toolbarReceipt):
                    {
                        this.buttonRefresh.Visible = true;
                        this.buttonRefresh.Left = 0;
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = this.ct*1;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct*2;
                        break;
                    }

                case (toolbarType.toolbarPending):
                    {
                        this.buttonRefresh.Visible = true;
                        this.buttonRefresh.Left = 0;
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = this.ct * 1;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct * 2;
                        break;
                    }

                case (toolbarType.toolbarSearch):
                    {
                        this.buttonSearch.Visible = true;
                        this.buttonSearch.Left = 0;
                        this.buttonPrint.Visible = true;
                        this.buttonPrint.Left = this.ct*1;
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = this.ct*2;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct*3;
                        break;
                    }

                case (toolbarType.toolbarCancel):
                    {
                        this.buttonCancel.Visible = true;
                        this.buttonCancel.Left = 0;
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = this.ct*1;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct*2;
                        break;
                    }

                case (toolbarType.toolbarEdit):
                    {
                        this.buttonSearch.Visible = true;
                        this.buttonSearch.Left = 0;
                        this.buttonHelp.Visible = true;
                        this.buttonHelp.Left = this.ct*1;
                        this.buttonExit.Visible = true;
                        this.buttonExit.Left = this.ct*2;
                        break;
                    }
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (this.OnExitClicked != null)
                this.OnExitClicked(sender, e);
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            if (this.OnHelpedClicked != null)
                this.OnHelpedClicked(sender, e);
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (this.OnPrintClicked != null)
                this.OnPrintClicked(sender, e);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (this.OnSearchClicked != null)
                this.OnSearchClicked(sender, e);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            if (this.OnRefreshClicked != null)
                this.OnRefreshClicked(sender, e);
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (this.OnConfirmClicked != null)
                this.OnConfirmClicked(sender, e);
        }

        private void buttonCapture_Click(object sender, EventArgs e)
        {
            if (this.OnCaptureClicked != null)
                this.OnCaptureClicked(sender, e);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (this.OnCancelClicked != null)
                this.OnCancelClicked(sender, e);
        }

        private void ToolbarStandard_Resize(object sender, EventArgs e)
        {
            this.buttonBackground.Width = this.Width;
        }

        private void buttonRecapture_Click(object sender, EventArgs e)
        {
            if (this.OnRecaptureClicked != null)
                this.OnRecaptureClicked(sender, e);
        }

        private void buttonRequestJPG_Click(object sender, EventArgs e)
        {
            if (this.OnRequestJPGClicked != null)
                this.OnRequestJPGClicked(sender, e);
        }

        private void buttonReClassify_Click(object sender, EventArgs e)
        {
            if (this.OnReClassifyClicked != null)
                this.OnReClassifyClicked(sender, e);
        }

        private void buttonMark_Click(object sender, EventArgs e)
        {
            if (this.OnMarkClicked != null)
                this.OnMarkClicked(sender, e);
        }

        private void buttonAccount_Click(object sender, EventArgs e)
        {
            if (this.OnAccountClicked != null)
                this.OnAccountClicked(sender, e);
        }
    }
}