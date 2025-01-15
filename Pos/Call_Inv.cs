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
    public partial class Call_Inv : Form
    {
        public Call_Inv()
        {
            InitializeComponent();
        }

        private void Call_Inv_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //string s1, s2;
                //if (textBox1.Text.Contains("-"))
                //{
                //    s1 = textBox1.Text.Substring(0, textBox1.Text.IndexOf("-"));
                //    s2 = textBox1.Text.Substring(textBox1.Text.IndexOf("-") + 1);

                //    MessageBox.Show(s1);
                //    MessageBox.Show(s2);
                //}
                this.Close();
            }
        }
    }
}
