using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace POS
{
    public partial class BaseForm : Form
    {
       // Button btn1;
        public BaseForm()
        {
            InitializeComponent();
          
        }

        private void button1_EnabledChanged(object sender, EventArgs e)
        {

        }

        public void BaseForm_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.TsIcon;
            ////////foreach (var button in this.Controls.OfType<Button>())
            ////////{
            ////////    if(button.Enabled==true)
            ////////        button.BackColor = ColorTranslator.FromHtml(BL.CLS_Session.cmp_color);
            ////////   // else
            ////////    //    button.BackColor = Color.Gainsboro;
            ////////   // button.EnabledChanged += button1_EnabledChanged;
            ////////}
            ////////foreach (var panel in this.Controls.OfType<Panel>())
            ////////{
            ////////    foreach (Control c in panel.Controls)
            ////////    {
            ////////        if (c is Button)
            ////////        {
            ////////           // btn1 = c;
            ////////            if (c.Enabled == true)
            ////////                c.BackColor = ColorTranslator.FromHtml(BL.CLS_Session.cmp_color);
            ////////          //  else
            ////////           //     c.BackColor = Color.Gainsboro;
            ////////           // (c as Button).TopColor = Color.Red;
            ////////        }
            ////////    }
            ////////}
            /*
            foreach (var panel in this.Controls.OfType<Panel>())
            {
                foreach (var button2 in panelite .OfType<Button>())
                {
                    button.BackColor = Color.Red;
                    // button.MouseLeave += createButton_MouseEnter;
                }
               // button.BackColor = Color.Red;
                // button.MouseLeave += createButton_MouseEnter;
            }
            */
            this.BackColor = ColorTranslator.FromHtml(BL.CLS_Session.cmp_color);
            this.ForeColor = ColorTranslator.FromHtml(BL.CLS_Session.fore_color);
           // this.BackColor = Color.FromArgb(244, 248, 249);

            if (BL.CLS_Session.is_bold.Equals("1"))
            {
                this.Font = new Font(BL.CLS_Session.font_t, float.Parse(BL.CLS_Session.font_s), FontStyle.Bold);
            }
            else
            {
                this.Font = new Font(BL.CLS_Session.font_t, float.Parse(BL.CLS_Session.font_s));
            }
            
        }

        private void BaseForm_Activated(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }
    }
}
