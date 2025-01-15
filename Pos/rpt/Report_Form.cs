using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.OleDb;
using System.IO;

namespace POS.Reports
{
    public partial class Report_Form : BaseForm
    {
       // OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Directory.GetCurrentDirectory() + @"\db\db.accdb; Jet OLEDB:Database Password=123456;");

        string condition;
        DataTable dt;

        public Report_Form(string cond, DataTable dt1)
        {
            InitializeComponent();
            condition = cond;
            //en = endd ;
            dt = dt1;
        }

        private void Report_Form_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.sms_bdy' table. You can move, or remove it, as needed.
          //  this.sms_bdyTableAdapter.Fill(this.dataSet1.sms_bdy);
            // TODO: This line of code loads data into the 'dataSet1.sms_bdy' table. You can move, or remove it, as needed.
            //this.sms_bdyTableAdapter.Fill(this.dataSet1.sms_bdy);
            // TODO: This line of code loads data into the 'dataSet1.sms_bdy' table. You can move, or remove it, as needed.
           // this.sms_bdyTableAdapter.Fill(this.dataSet1.sms_bdy);
            // TODO: This line of code loads data into the 'dataSet1.sms_bdy' table. You can move, or remove it, as needed.
          // this.sms_bdyTableAdapter.Fill(this.dataSet1.sms_bdy);
            // TODO: This line of code loads data into the 'dataSet1.sms_bdy' table. You can move, or remove it, as needed.
          //  this.sms_bdyTableAdapter.Fill(this.dataSet1.sms_bdy);
            // TODO: This line of code loads data into the 'dataSet1.sms_bdy' table. You can move, or remove it, as needed.
           // this.sms_bdyTableAdapter.Fill(this.dataSet1.sms_bdy);
            // TODO: This line of code loads data into the 'dataSet1.sms_bdy' table. You can move, or remove it, as needed.
           // this.sms_bdyTableAdapter.Fill(this.dataSet1.sms_bdy);
            //MessageBox.Show(st + "         " + en);
         //   Rng_Repo rng = new Rng_Repo();
            // TODO: This line of code loads data into the 'dataSet1.sms_bdy' table. You can move, or remove it, as needed.
            // MessageBox.Show(Convert.ToDateTime(en).ToString());
           // MessageBox.Show(Convert.ToDateTime(en).AddSeconds(86399).ToString());
          //  
          //  this.sms_bdyTableAdapter.FillBy1(this.dataSet1.sms_bdy);

            //ReportDataSource datasource = new ReportDataSource("sms_bdy", load_data(st,en));
            //this.reportViewer1.LocalReport.DataSources.Clear();
            //this.reportViewer1.LocalReport.DataSources.Add(DataTable1);
            //this.reportViewer1.RefreshReport();
           // String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", st);
           // String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", en);
           // this.sms_bdyTableAdapter
          //  this.reportViewer1.Clear();
           // this.sms_bdyTableAdapter.Fill(dataSet1.sms_bdy);
          //  DataSet1 ds1 = new DataSet1();

          //  MessageBox.Show(ds1.Tables[0].Rows[0][0].ToString());

         // this.sms_bdyTableAdapter.FillBy(this.dataSet1.DataTable1);
            //ReportDataSource datasource = new ReportDataSource("DataTable1",);
          //  this.reportViewer1.LocalReport.DataSources.Add(datasource);
           // this.reportViewer1.RefreshReport();


            //this.reportViewer1.RefreshReport();
            /*
            ReportDataSource reportDataSource1 = new ReportDataSource("DataSet1", dt);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            if (condition == "DR")
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = @"POS.reports.DailyReport.rdlc";
            }
            else
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = @"SEND_SMS_OK.Reports.sms_bdy.rdlc";
            }
            */

            switch (condition)
            {
                case "DR":
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = @"POS.reports.DailyReport.rdlc";
                    break;

                case "AR":
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = @"SEND_SMS_OK.Reports.sms_bdy.rdlc";
                    break;

            }

            this.reportViewer1.RefreshReport();




           // this.reportViewer1.RefreshReport();
          //  this.reportViewer1.RefreshReport();
           
        }

        //private  DataTable load_data(string s1, string e1)
        //{

                //string st11 = s1;
                //string e11 = e1;
                //Select sms_hd.sms_id, sms_hd.sms_date,sms_hd.sms_sndr,sms_hd.sms_rcvr,sms_dt.sms_text from sms_hd,sms_dt where sms_hd.sms_id=sms_dt.sms_id and sms_hd.sms_date like '*21-07-2015*'
                // OleDbDataAdapter da = new OleDbDataAdapter("Select sms_hd.sms_id as a, sms_hd.sms_date as b,sms_hd.sms_sndr as c,sms_hd.sms_rcvr as d,sms_dt.sms_text as e from sms_hd,sms_dt where sms_hd.sms_id=sms_dt.sms_id and sms_hd.sms_date like '%" + maskedTextBox1.Text.Replace(" ", "") + "%'", con);
                // OleDbDataAdapter da = new OleDbDataAdapter("Select sms_hd.sms_id as a, sms_hd.sms_date as b,sms_hd.sms_sndr as c,sms_hd.sms_rcvr as d,sms_dt.sms_text as e from sms_hd,sms_dt where sms_hd.sms_id=sms_dt.sms_id and sms_hd.sms_date between #" + dateTimePicker1.Text + " 00:00:00# and #" + dateTimePicker2.Text + " 23:59:59#", con);
                //// OleDbDataAdapter da = new OleDbDataAdapter("Select sms_id as a, sms_date as b,sms_sndr as c,sms_rcvr as d,sms_text as e from sms_bdy where sms_date between #" + dateTimePicker1.Text + " 00:00:00# and #" + dateTimePicker2.Text + " 23:59:59#", con);
                //OleDbDataAdapter da = new OleDbDataAdapter("Select sms_id as a, sms_date as b,sms_sndr as c,sms_rcvr as d,sms_text as e from sms_bdy where sms_date between #" + s1 + "# and #" + e1 + "#", con);
                ////DataTable dt = new DataTable();
                //DataSet1 ds1 = new DataSet1();
                //da.Fill(ds1,"sms_bdy");

                //return ds1.Tables["sms_bdy"];
           
        //}
    }
}
