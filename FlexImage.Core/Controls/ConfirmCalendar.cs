// ============================================
// 
// FlexImage.Core
// ConfirmCalendar.cs
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
    public partial class ConfirmCalendar : UserControl
    {
        private DateTime expectedDate;
        private string title = "";

        public ConfirmCalendar()
        {
            this.InitializeComponent();
        }

        public DateTime ExpectedDate
        {
            get { return this.expectedDate; }
            set
            {
                this.expectedDate = value;
                this.calendar1.SetDate(value);
            }
        }

        public DateTime SelectedDate
        {
            get { return this.calendar1.SelectionRange.Start; }
        }

        public string Title
        {
            get { return this.title; }
            set
            {
                this.labelTitulo.Text = value;
                this.title = value;
            }
        }


        public bool ConfirmDate()
        {
            DateTime date1 = this.calendar1.SelectionRange.Start;
            DateTime date2;

            try
            {
                date2 = Convert.ToDateTime(this.maskedEditBox1.Text);
            }
            catch
            {
                Alert.Exclamation("Data informada inválida!");
                this.maskedEditBox1.Focus();
                return false;
            }

            if (date1.ToShortDateString() != date2.ToShortDateString())
            {
                Alert.Exclamation("Confirmação de data divergente!");
                return false;
            }
            else
            {
                if (this.expectedDate.ToShortDateString() != date1.ToShortDateString())
                {
                    Alert.Exclamation("Atenção: A data prevista para este evento era [" +
                                      this.expectedDate.ToShortDateString() + "]");
                }
            }

            return true;
        }

        private void ConfirmCalendar_Load(object sender, EventArgs e)
        {
        }
    }
}