using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS
{
    public partial class WaitForm : Form
    {
        string message = "";
        public WaitForm(string msg)
        {
          
                
            InitializeComponent();
            message = msg;
            //this.StartPosition = FormStartPosition.Manual;
            //foreach (var scrn in Screen.AllScreens)
            //{
            //    if (scrn.Bounds.Contains(this.Location))
            //    {
            //        this.Location = new Point(scrn.Bounds.Left - this.Width, scrn.Bounds.Top);
            //        return;
            //    }
            //}
            //this.ControlBox = false;
            this.Location = new Point(0, 0);
        }

        private void WaitForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(message))
                label1.Text = message;
            else
                label1.Text = BL.CLS_Session.lang.Equals("E") ? "Please Wait ...    " : "يرجى الانتظار ...    ";
            
        }
    }
}
