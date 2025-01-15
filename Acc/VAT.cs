using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.Reporting.WinForms;

namespace POS.Acc
{
    public partial class VAT : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dtp;
        double taxper;
        public VAT()
        {
            InitializeComponent();
        }

        private void VAT_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(16);
            dataGridView1.Rows[0].Cells[0].Value = BL.CLS_Session.lang.Equals("E") ? "Taxable domestic sales at the basic rate" : "المبيعات المحلية الخاضعة للضريبة بالنسبة الاساسية";
            dataGridView1.Rows[1].Cells[0].Value = BL.CLS_Session.lang.Equals("E") ? "Citizens Sales (Private Health Services and Private Private Education)" : "مبيعات المواطنين(الخدمات الصحية الخاصةوالتعليم الاهلي الخاص)";
            dataGridView1.Rows[2].Cells[0].Value = BL.CLS_Session.lang.Equals("E") ? "Domestic sales subject to zero percent" : "المبيعات المحلية الخاضعة لنسبة الصفر بالمائه";
            dataGridView1.Rows[3].Cells[0].Value = BL.CLS_Session.lang.Equals("E") ? "Export at zero percent" : "التصدير بنسبة الصفر بالمائه";
            dataGridView1.Rows[4].Cells[0].Value = BL.CLS_Session.lang.Equals("E") ? "Exempt sales" : "المبيعات المعفاة";
            dataGridView1.Rows[5].Cells[0].Value = BL.CLS_Session.lang.Equals("E") ? "Total sales" : "اجمالي المبيعات";

            dataGridView1.Rows[6].Cells[0].Value = BL.CLS_Session.lang.Equals("E") ? "Domestic purchases subject to tax at the basic rate" : "المشتريات المحلية الخاضعة للضريبة بالنسبة الاساسية";
            dataGridView1.Rows[7].Cells[0].Value = BL.CLS_Session.lang.Equals("E") ? "Imports subject to tax and paid at customs" : "الواردات الخاضعة للضريبة ومدفوعة في الجمارك";
            dataGridView1.Rows[8].Cells[0].Value = BL.CLS_Session.lang.Equals("E") ? "Taxable imports through reverse charge" : "الواردات الخاضعة للضريبة من خلال الاحتساب العكسي";
            dataGridView1.Rows[9].Cells[0].Value = BL.CLS_Session.lang.Equals("E") ? "Zero-rated purchases" : "المشتريات الخاضعة لنسبة الصفر بالمائه";
            dataGridView1.Rows[10].Cells[0].Value = BL.CLS_Session.lang.Equals("E") ? "Exempt purchases" : "المشتريات المعفاة";
            dataGridView1.Rows[11].Cells[0].Value = BL.CLS_Session.lang.Equals("E") ? "total purchases" : "اجمالي المشتريات";
            dataGridView1.Rows[12].Cells[0].Value = BL.CLS_Session.lang.Equals("E") ? "Total VAT due for the selected tax period" : "اجمالي ضريبة القيمة المضافة المستحقة عن الفترة الضريبية المختارة";
            dataGridView1.Rows[13].Cells[0].Value = BL.CLS_Session.lang.Equals("E") ? "Corrections from previous periods" : "تصحيحات من الفترات السابقة";
            dataGridView1.Rows[14].Cells[0].Value = BL.CLS_Session.lang.Equals("E") ? "VAT carried forward to previous periods" : "ضريبة القيمة المضافة التي تم ترحيلها الفترات السابقة";
            dataGridView1.Rows[15].Cells[0].Value = BL.CLS_Session.lang.Equals("E") ? "net tax payable" : "صافي الضريبة المستحقة";

            dtp = daml.SELECT_QUIRY_only_retrn_dt("select * from taxperiods");
            cmb_prd.DataSource = dtp;
            cmb_prd.DisplayMember = "prd_name";
            cmb_prd.ValueMember = "prd_no";
           // cmb_prd.SelectedIndex = -1;
        }

        private void cmb_prd_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmb_prd.SelectedIndex != -1)
            //{
            //    /*
            //    DataRow[] res = dtp.Select("prd_no =" + cmb_prd.SelectedValue + "");
            //    txt_fdate.Text = datval.convert_to_ddDDyyyy(res[0][4].ToString());
            //    txt_tdate.Text = datval.convert_to_ddDDyyyy(res[0][5].ToString());
            //    */
            //    var dv1 = new DataView();
            //    dv1 = dtp.DefaultView;
            //    // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
            //    dv1.RowFilter = "prd_no =" + cmb_prd.SelectedValue + "";
            //    DataTable dtNew = dv1.ToTable();

            //    txt_fdate.Text = datval.convert_to_ddDDyyyy(dtNew.Rows[0][4].ToString());
            //    txt_tdate.Text = datval.convert_to_ddDDyyyy(dtNew.Rows[0][5].ToString());
            //}
        }

        private void cmb_prd_Leave(object sender, EventArgs e)
        {
            if (cmb_prd.SelectedIndex != -1)
            {
                /*
                DataRow[] res = dtp.Select("prd_no =" + cmb_prd.SelectedValue + "");
                txt_fdate.Text = datval.convert_to_ddDDyyyy(res[0][4].ToString());
                txt_tdate.Text = datval.convert_to_ddDDyyyy(res[0][5].ToString());
                */
                var dv1 = new DataView();
                dv1 = dtp.DefaultView;
                // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                dv1.RowFilter = "prd_no =" + cmb_prd.SelectedValue + "";
                DataTable dtNew = dv1.ToTable();

                txt_fdate.Text = dtNew.Rows[0][4].ToString().Substring(6, 2) + "-" + dtNew.Rows[0][4].ToString().Substring(4, 2) + "-" + dtNew.Rows[0][4].ToString().Substring(0, 4);
                txt_tdate.Text = dtNew.Rows[0][5].ToString().Substring(6, 2) + "-" + dtNew.Rows[0][5].ToString().Substring(4, 2) + "-" + dtNew.Rows[0][5].ToString().Substring(0, 4);
               // txt_fdate.Enabled = false;
               // txt_tdate.Enabled = false;
                dtp = daml.SELECT_QUIRY_only_retrn_dt("select isnull(tax_percent,0) from taxperiods where prd_no=" + cmb_prd.SelectedValue + "");
                taxper = Convert.ToDouble(dtp.Rows[0][0]);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txt_fdate.Text = datval.convert_to_hdate(txt_fdate.Text);
                //  string temy2 =
                txt_tdate.Text = datval.convert_to_hdate(txt_tdate.Text);
            }
            else
            {
                txt_fdate.Text =datval.convert_to_mdate( txt_fdate.Text);
                //  string temy2 =
                txt_tdate.Text = datval.convert_to_mdate(txt_tdate.Text);
            }
        }

        private void txt_fdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_fdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_fdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    // txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_fdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_fdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                        else
                        {
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                    }
                }
            }
            catch { }
        }

        private void txt_tdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_tdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_tdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    // txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_tdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_tdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                        else
                        {
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                    }
                }
            }
            catch { }
        }

        private void cmb_prd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_fdate.Focus();
                // SendKeys.Send("{Home}");
                txt_fdate.SelectAll();
            }
        }

        private void txt_fdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_tdate.Focus();
                // SendKeys.Send("{Home}");
                txt_tdate.SelectAll();
            }
        }

        private void txt_tdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
                // SendKeys.Send("{Home}");
                //txt_tdate.SelectAll();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string condallbr = chk_allbr.Checked ? " " : " and c.a_brno='"+BL.CLS_Session.brno+"' ";
            string condallbr2 = chk_allbr.Checked ? " " : " and a.a_brno='" + BL.CLS_Session.brno + "' ";
            string condallbr3 = chk_allbr.Checked ? " " : " and branch='" + BL.CLS_Session.brno + "' ";
            string cond = checkBox1.Checked ? "c.a_hdate" : "c.a_mdate";
            string cond3 = checkBox1.Checked ? "invhdate" : "invmdate";

            string qur = "select a.a_key ,(select round(isnull(sum(b.a_camt - b.a_damt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'  where b.taxcatId<>16 and  a.a_key=b.a_key " + condallbr + ") qty from accounts a where a.acckind=1 " + condallbr2 + "";
            DataTable dtm = daml.SELECT_QUIRY_only_retrn_dt(qur);

            string qur1 = "select round(isnull(sum(case when invtype in ('04','05') then taxfree_amt else -taxfree_amt end),0),4) from sales_hdr where " + cond3 + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + condallbr3 + "";
            DataTable dtm1 = daml.SELECT_QUIRY_only_retrn_dt(qur1);


            string qur2 = "select a.a_key ,(select round(isnull(sum(b.a_damt - b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'  where b.taxcatId<>10 and ltrim(rtrim(c.jhcurr))='' and a.a_key=b.a_key " + condallbr + ") qty from accounts a where a.acckind=2 " + condallbr2 + "";
            DataTable dtm2 = daml.SELECT_QUIRY_only_retrn_dt(qur2);

            string qur22 = "select round(isnull(sum(case when invtype in ('06','07') then taxfree_amt else -taxfree_amt end),0),4) from pu_hdr where ltrim(rtrim(cur))='' and " + cond3 + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + condallbr3 + "";
            DataTable dtm22 = daml.SELECT_QUIRY_only_retrn_dt(qur22);

            string qur227 = "select round(isnull(sum(case when invtype in ('06','07') then etax else -etax end),0),4) from pu_hdr where ltrim(rtrim(cur))<>'' and " + cond3 + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + condallbr3 + "";
            DataTable dtm227 = daml.SELECT_QUIRY_only_retrn_dt(qur227);

            double sums = 0, sump = 0, sump7 = 0;
            foreach (DataRow row in dtm.Rows)
            {
                sums = sums + Convert.ToDouble(row[1]);
            }

            foreach (DataRow rowp in dtm2.Rows)
            {
                sump = sump + Convert.ToDouble(rowp[1]);
            }

            foreach (DataRow rowp in dtm227.Rows)
            {
                sump7 = sump7 + Convert.ToDouble(rowp[0]);
            }

            dataGridView1.Rows[0].Cells[3].Value = Math.Round(sums, 4).ToString();// dtm.Rows[0][1].ToString(); //Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value) - Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value);
            dataGridView1.Rows[6].Cells[3].Value = Math.Round(sump, 4).ToString();// dtm2.Rows[0][1].ToString();// Convert.ToDouble(dataGridView1.Rows[6].Cells[1].Value) - Convert.ToDouble(dataGridView1.Rows[6].Cells[2].Value);
            dataGridView1.Rows[7].Cells[3].Value = Math.Round(sump7, 4).ToString();

            dataGridView1.Rows[0].Cells[1].Value = BL.CLS_Session.isshamltax.Equals("2") ? Math.Round((Convert.ToDouble(sums) * ((100 ) / taxper)),4).ToString() : Math.Round((Convert.ToDouble(sums) * ((100 / taxper))),4).ToString();
            dataGridView1.Rows[6].Cells[1].Value = BL.CLS_Session.isshamltax.Equals("2") ? Math.Round((Convert.ToDouble(sump) * ((100 ) / taxper)),4).ToString() : Math.Round((Convert.ToDouble(sump) * ((100 / taxper))),4).ToString();
            dataGridView1.Rows[7].Cells[1].Value = BL.CLS_Session.isshamltax.Equals("2") ? Math.Round((Convert.ToDouble(sump7) * ((100) / taxper)), 4).ToString() : Math.Round((Convert.ToDouble(sump7) * ((100 / taxper))), 4).ToString();


            dataGridView1.Rows[15].Cells[0].Value = (Convert.ToDouble(dataGridView1.Rows[0].Cells[3].Value) - Convert.ToDouble(dataGridView1.Rows[6].Cells[3].Value) - Convert.ToDouble(dataGridView1.Rows[7].Cells[3].Value)) < 0 ? BL.CLS_Session.lang.Equals("E") ? "net tax retrieved" : "صافي الضريبة المستردة" : BL.CLS_Session.lang.Equals("E") ? "net tax payable" : "صافي الضريبة المستحقة";
            dataGridView1.Rows[15].Cells[3].Value = Convert.ToDouble(dataGridView1.Rows[0].Cells[3].Value) - Convert.ToDouble(dataGridView1.Rows[6].Cells[3].Value) - Convert.ToDouble(dataGridView1.Rows[7].Cells[3].Value);

            dataGridView1.Rows[4].Cells[1].Value = dtm1.Rows[0][0].ToString();
           // dataGridView1.Rows[5].Cells[1].Value = (Convert.ToDouble(dtm1.Rows[0][0]) + Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value)).ToString();
            dataGridView1.Rows[10].Cells[1].Value = dtm22.Rows[0][0].ToString();
           // dataGridView1.Rows[11].Cells[1].Value = (Convert.ToDouble(dtm22.Rows[0][0]) + Convert.ToDouble(dataGridView1.Rows[6].Cells[1].Value)).ToString();

            dataGridView1.Rows[dataGridView1.Rows.Count - 16].DefaultCellStyle.BackColor = Color.PaleTurquoise;
            dataGridView1.Rows[dataGridView1.Rows.Count - 10].DefaultCellStyle.BackColor = Color.PaleTurquoise;

            dataGridView1.Rows[dataGridView1.Rows.Count - 11].DefaultCellStyle.BackColor = Color.Lavender;
            dataGridView1.Rows[dataGridView1.Rows.Count - 5].DefaultCellStyle.BackColor = Color.Lavender;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                   // if(string.IsNullOrEmpty(row.Cells[i].Value.ToString()))
                        if (row.Cells[i].Value==null)
                        row.Cells[i].Value="0.0000";
                    //String header = dataGridView1.Columns[i].HeaderText;
                    //String cellText = row.Cells[i].Value;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;
            /*
          Reports.Report_Form rrf = new Reports.Report_Form("DR", dtt);
          rrf.MdiParent = ParentForm;
          rrf.Show();
           */
            try
            {
                Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();

                /*
                DataSet ds1 = new DataSet();

                */
                DataTable dt = new DataTable();
                int cn = 1;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    /*
                    dt.Columns.Add(col.Name);
                    // dt.Columns.Add(col.HeaderText);
                     * */
                    dt.Columns.Add("c" + cn.ToString());
                    cn++;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataRow dRow = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] =cell.Value ;
                    }
                    dt.Rows.Add(dRow);
                }


                rf.reportViewer1.LocalReport.DataSources.Clear();
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Acc.rpt.Vat_Report_rpt.rdlc";



                // ReportParameter[] parameters = new ReportParameter[2];
                // parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                // parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));

                // rf.reportViewer1.LocalReport.SetParameters(parameters);
                ReportParameter[] parameters = new ReportParameter[4];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                parameters[1] = new ReportParameter("fmdate", txt_fdate.Text);
                parameters[2] = new ReportParameter("tmdate", txt_tdate.Text);
                parameters[3] = new ReportParameter("br_name", BL.CLS_Session.brname);

                rf.reportViewer1.LocalReport.SetParameters(parameters);

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();

                //rf.reportViewer1.RefreshReport();
                //rf.MdiParent = ParentForm;
                //rf.Show();
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.Rows.Count > 0)
            //{
            //    Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
            //    XcelApp.Application.Workbooks.Add(Type.Missing);

            //    for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            //    {
            //        XcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            //    }

            //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //    {
            //        for (int j = 0; j < dataGridView1.Columns.Count; j++)
            //        {
            //            XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j] == dataGridView1.Rows[i].Cells[0] ? "'" + dataGridView1.Rows[i].Cells[j].Value.ToString() : dataGridView1.Rows[i].Cells[j].Value.ToString();
            //        }
            //    }
            //    XcelApp.Columns.AutoFit();
            //    XcelApp.Visible = true;
            //}
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    foreach (DataGridViewCell cell in row.Cells)
            //    {
            //        //do operations with cell
            //        if (string.IsNullOrEmpty(cell.Value.ToString()))
            //            cell.Value = "-";
            //    }
            //}

            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                XcelApp.Application.Workbooks.Add(Type.Missing);



                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    // XcelApp.Cells[5, i] = dataGridView1.Columns[i - 1].Visible == true ? dataGridView1.Columns[i - 1].HeaderText : "";
                    if (dataGridView1.Columns[i - 1].Visible == true)
                        XcelApp.Cells[5, i] = "'" + dataGridView1.Columns[i - 1].HeaderText;
                    else
                        XcelApp.Cells[5, i] = null;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        //  XcelApp.Cells[i + 6, j + 1] = dataGridView1.Columns[j].Visible == true ? dataGridView1.Rows[i].Cells[j].Value.ToString() : "";
                        if (dataGridView1.Columns[j].Visible == true)
                            XcelApp.Cells[i + 6, j + 1] = "'" + dataGridView1.Rows[i].Cells[j].Value.ToString();
                        else
                            XcelApp.Cells[i + 6, j + 1] = null;
                    }
                }
                // XcelApp.Cells[0, 0] = "123";

                Microsoft.Office.Interop.Excel._Worksheet workSheet = XcelApp.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;

                // workSheet.Rows.Insert();
                /*
                workSheet.Cells[1, "H"] = "ID Number";
                workSheet.Cells[1, "J"] = "Current Balance";

                var row = 1;
                foreach (var acct in bankAccounts)
                {
                    row++;
                    workSheet.Cells[row, "H"] = acct.ID;
                    workSheet.Cells[row, "J"] = acct.Balance;
                }
                */
                workSheet.Cells[3, "B"] = " الاقرار الضريبي" +  " من" + txt_fdate.Text + " الى" + txt_tdate.Text; ;
                workSheet.Cells[1, "B"] = BL.CLS_Session.cmp_name;
                workSheet.Cells[2, "B"] = BL.CLS_Session.brname;
                // workSheet.Cells[3, "B"] = this.Text + "من" + txt_fdate.Text + " الى" + txt_tdate.Text;

                workSheet.Range["B1", "C1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                workSheet.Range["B2", "C2"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                workSheet.Range["B3", "C3"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                // workSheet.Range["A" + (dataGridView1.Rows.Count + 5).ToString(), "D" + (dataGridView1.Rows.Count + 5).ToString()].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                workSheet.Range["A5", "D5"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                workSheet.Range["A6", "D6"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatReport2);

                workSheet.Range["A12", "D12"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatReport2);

                workSheet.Range["A21", "D21"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatReport2);
                // workSheet.Cells[2, "D"]

                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }

        }

        private void cmb_prd_Enter(object sender, EventArgs e)
        {
            dtp = daml.SELECT_QUIRY_only_retrn_dt("select * from taxperiods");
            cmb_prd.DataSource = dtp;
            cmb_prd.DisplayMember = "prd_name";
            cmb_prd.ValueMember = "prd_no";
        }
    }
}
