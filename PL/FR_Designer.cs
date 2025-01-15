using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastReport;

namespace POS
{
    public partial class FR_Designer : BaseForm
    {
        public FR_Designer(Report rpt)
        {
            InitializeComponent();

            designerControl1.Report = rpt;

            designerControl1.RefreshLayout();
        }

        private void FR_Designer_Load(object sender, EventArgs e)
        {
          //  designerControl1.ShowMainMenu = false;
            //this.Size.Width=1074;
            //this.Size.Height = 626;
            this.Text = "مصمم التقرير";
        }
    }
}
