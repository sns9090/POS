using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS.Pos
{
    public partial class Add_Tameen : BaseForm
    {
        //string tamin, tamin_not;
        public Add_Tameen()
        {
            InitializeComponent();

            //if (!string.IsNullOrEmpty(tam))
            //txt_tameemamt.Text = tam;

            //if (!string.IsNullOrEmpty(tamnot))
            //txt_tameennot.Text = tamnot;
        }

        private void Add_Tameen_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_tameennot_Enter(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
        }

        private void txt_tameemamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txt_tameemamt_ImeModeChanged(object sender, EventArgs e)
        {

        }

        private void txt_tameemamt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_tameennot.Focus();
            }
        }
    }
}
