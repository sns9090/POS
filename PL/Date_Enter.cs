using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace POS
{
    public partial class Date_Enter : BaseForm
    {
        BL.Date_Validate datval = new BL.Date_Validate();
        string content;
        public Date_Enter(string val)
        {
            InitializeComponent();
            content = val;
        }

        private void Date_Enter_Load(object sender, EventArgs e)
        {
            txt_date.Text = content;
        }

        private void txt_per01_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_date.Text.Replace("-", "").Length == 8)
                    this.Close();
                else
                {
                    txt_date.Text = "";
                    this.Close();
                }
            }
        }

        private void txt_date_Validating(object sender, CancelEventArgs e)
        {
           // datval.txtdate_Validating(txt_date, e);
        }

        private void txt_date_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            //DateTime rs;

            //CultureInfo ci = new CultureInfo("en-US", false);

            //if (!DateTime.TryParseExact(txt_date.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            //{

            //    MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //   // e.Cancel = true;
            //    txt_date.Text = "";
            //    txt_date.Focus();

            //}
        }

        private void txt_date_TextChanged(object sender, EventArgs e)
        {
          //  string sch = ;
            if (txt_date.Text.Replace("-", "").Length == 8)
            {
                DateTime rs;

                CultureInfo ci = new CultureInfo("en-US", false);

                if (!DateTime.TryParseExact(txt_date.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
                {

                    MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // e.Cancel = true;
                    txt_date.Text = "";
                    txt_date.Focus();

                }
            }
        }
    }
}
