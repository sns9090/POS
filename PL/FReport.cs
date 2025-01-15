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
    public partial class FReport : BaseForm
    {
        Report rpt = new Report();
        public FReport()
        {
            InitializeComponent();
        }

        private void FastReport_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FR_Designer rptd = new FR_Designer(report1);
            // Report_Designer rptd = new Report_Designer();
            // rptd.designerControl1.Report = rpt;
            // rptd.Text = rptd.Text + report1.FileName;
            rptd.MdiParent = MdiParent;
            rptd.Show();
            // report1.Show();
            //  report1.Print();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Report_Viewer rptd = new Report_Viewer(report1);
            FR_Viewer rptv = new FR_Viewer(report1);
           // rpt.Preview = rptv.previewControl1;
           // rpt.Show();
            rptv.MdiParent = MdiParent;
            rptv.Show();

            //report1.Design();
        }
    }
}
