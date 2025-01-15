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
    public partial class Pos_note : BaseForm
    {
        string txt = "";
        public Pos_note(string tx)
        {
            InitializeComponent();
            txt = tx;
        }

        private void Pos_note_Load(object sender, EventArgs e)
        {
            textBox1.Text = txt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
        }
    }
}
