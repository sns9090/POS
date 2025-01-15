using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using Microsoft.Reporting.WinForms;
using System.Globalization;

namespace POS.Sup
{
    public partial class Acc_Statment_org_SU : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        //  DataTable dtt;
        double seso = 0;
        List<BL.CurrencyInfo> currencies = new List<BL.CurrencyInfo>();
        DataTable dth, dtcur, dtcust;
        public static int qq;
        string a_key;
        SqlConnection con3 = BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public Acc_Statment_org_SU(string akey)
        {
            InitializeComponent();

            a_key = akey;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            seso = 0;
            if (txt_code.Text == "")
            {
                MessageBox.Show("يجب ادخال رقم المورد", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_code.Focus();
                return;

            }


            // Load_Statment(txt_code.Text);
            try
            {
                if (txt_code.Text != "")
                {
                    dtcust = daml.SELECT_QUIRY_only_retrn_dt("select su_no from suppliers where su_brno='" + BL.CLS_Session.brno + "' and cmncode='" + txt_code.Text + "'");

                    string custrs = "";
                    foreach (DataRow rw in dtcust.Rows)
                    {
                        custrs = custrs + "'" + rw[0].ToString() + "',";
                    }
                    custrs = custrs.TrimEnd(',');

                    // txt_code.Text = a_key;
                    /*
                    string qstr2 = "select a_type c1,a_ref c2,CONVERT(VARCHAR(10), CAST(a_mdate as date), 105) c3,CONVERT(VARCHAR(10), CAST(a_mdate as date), 105) d1,a_text c4,round((case jddbcr when 'C' then a_camt else 0 end),2) c5,round((case jddbcr when 'D' then a_damt else 0 end),2) c6 from acc_dtl where a_key='" + aakey + "' and a_comp='01' order by a_mdate";
                     dth = daml.SELECT_QUIRY("select a_name,round(a_opnbal,2),a_key from accounts where a_key='" + aakey + "' and a_comp='01'");
                    dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);
                    */
                    string condt = cmb_type.SelectedIndex != -1 ? " and a.a_type = '" + cmb_type.SelectedValue.ToString() + "' " : " ";
                    string cond = checkBox1.Checked ? "b.a_hdate" : "b.a_mdate";
                    string condp = rad_posted.Checked ? " and b.a_posted=1 " : (rad_notpostd.Checked ? " and b.a_posted=0 " : " ");
                    string conddm = chk_d.Checked ? " and a.jddbcr='C' " : (chk_m.Checked ? " and a.jddbcr='D' " : " ");

                    string condcur = cmb_cur.SelectedIndex == -1 ? " round((case a.jddbcr when 'D' then a.a_damt else 0 end),2) c6,round((case a.jddbcr when 'C' then a.a_camt else 0 end),2) c7 " : " round((case a.jddbcr when 'D' then a.jdfcamt else 0 end),2) c6,round((case a.jddbcr when 'C' then a.jdfcamt else 0 end),2) c7 ";
                    string ccur = cmb_cur.SelectedIndex != -1 ? " and a.jdcurr='" + cmb_cur.SelectedValue + "' " : " ";

                   // string qstr2 = "select a.a_type c1,a.a_ref c2,CONVERT(VARCHAR(10), CAST(a.a_mdate as date), 105) c3,CONVERT(VARCHAR(10), CAST(a.a_hdate as date), 105) c9,a.a_text c4,round((case a.jddbcr when 'C' then a.a_camt else 0 end),2) c5,round((case a.jddbcr when 'D' then a.a_damt else 0 end),2) c6,b.a_posted,a.a_type a_t from acc_dtl a,acc_hdr b  where a.a_brno=b.a_brno and a.a_ref=b.a_ref and a.a_type=b.a_type " + condp + conddm + " and " + cond + " <= '" + txt_tdate.Text.Replace("-", "") + "' and a.su_no='" + txt_code.Text + "' and a.a_type not in('06','16') and a.a_brno='" + BL.CLS_Session.brno + "' and a.a_text like '%" + txt_desc.Text.Replace(" ", "%") + "%' order by a.a_mdate";
                   // string qstr2 = "select a.a_type c1,a.a_ref c2,CONVERT(VARCHAR(10), CAST(a.a_mdate as date), 105) c3,CONVERT(VARCHAR(10), CAST(a.a_hdate as date), 105) c9,a.a_text c4,round((case a.jddbcr when 'C' then a.a_camt else 0 end),2) c5,round((case a.jddbcr when 'D' then a.a_damt else 0 end),2) c6,b.a_posted,a.a_type a_t,b.jhsrc src,b.sl_no sl,b.pu_no pu from acc_dtl a,acc_hdr b  where a.a_brno=b.a_brno and a.a_ref=b.a_ref and a.a_type=b.a_type " + condp + conddm + condt + " and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and a.su_no='" + txt_code.Text + "' and a.a_type not in('06','16') and a.a_brno='" + BL.CLS_Session.brno + "' and a.a_text like '%" + txt_desc.Text.Replace(" ", "%") + "%' order by a.a_mdate,a.a_sysdate";
                    string qstr2 = "select a.a_type c1,cast(a.a_ref as varchar) c2,CONVERT(VARCHAR(10), CAST(a.a_mdate as date), 105) c3,(substring(a.a_hdate,7,2) + '-' + substring(a.a_hdate,5,2)+'-'+substring(a.a_hdate,1,4)) c4,a.a_text c5," + condcur + ",b.a_posted,a.a_type a_t,b.jhsrc src,b.sl_no sl,b.pu_no pu from acc_dtl a,acc_hdr b  where a.a_brno=b.a_brno and a.a_ref=b.a_ref and a.a_type=b.a_type and a.pu_no=b.pu_no " + ccur + condp + conddm + condt + " and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and a.su_no in (" + custrs + ") and a.a_type not in('06','16') and a.a_brno='" + BL.CLS_Session.brno + "' and a.a_text like '%" + txt_desc.Text.Replace(" ", "%") + "%' order by a.a_mdate,a.a_sysdate";

                    dth = daml.SELECT_QUIRY_only_retrn_dt("select su_name a_name,round(" + (cmb_cur.SelectedIndex == -1 ? " sum(su_opnbal) " : " sum(su_opnfcy) ") + ",2) a_opnbal from suppliers where cmncode in (" + txt_code.Text + ") and su_brno='" + BL.CLS_Session.brno + "' group by su_name," + (cmb_cur.SelectedIndex == -1 ? " su_opnbal " : " su_opnfcy ") + " ");
                 //   dth = daml.SELECT_QUIRY_only_retrn_dt("select su_name a_name,round((case when su_cur ='" + (cmb_cur.SelectedIndex != -1 ? " then su_opnfcy " :  "")  + "'  else su_opnbal),2) a_opnbal,su_no a_key from suppliers where su_no='" + txt_code.Text + "' and su_brno='" + BL.CLS_Session.brno + "'");
                   
                   // dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt(qstr2);
                    double sumop = 0;
                    foreach (DataRow dtro in dth.Rows)
                    {
                        // sumopnd = sumopnd + Convert.ToDouble(dtr[2]);
                        // sumopnm = sumopnm + Convert.ToDouble(dtr[3]);
                        // sumtrd = sumtrd + Convert.ToDouble(dtr[4]);
                        sumop = sumop + Convert.ToDouble(dtro[1]);
                        // sumttld = sumttld + Convert.ToDouble(dtr[6]);
                        //  sumttlm = sumttlm + Convert.ToDouble(dtr[7]);
                    }

                    if (dth.Rows.Count > 0)
                    {
                        DataTable dtn = daml.SELECT_QUIRY_only_retrn_dt(qstr2);
                        DataRow dr = dtn.NewRow();
                        double sumtrm = 0, sumttld = 0;
                        foreach (DataRow dtr in dtn.Rows)
                        {
                            // sumopnd = sumopnd + Convert.ToDouble(dtr[2]);
                            // sumopnm = sumopnm + Convert.ToDouble(dtr[3]);
                            // sumtrd = sumtrd + Convert.ToDouble(dtr[4]);
                            sumtrm = sumtrm + Convert.ToDouble(dtr[6]);
                            sumttld = sumttld + Convert.ToDouble(dtr[5]);
                            //  sumttlm = sumttlm + Convert.ToDouble(dtr[7]);
                        }
                        dr[0] = "";
                        dr[1] = "";
                        dr[2] = "";
                        dr[3] = "";
                        dr[4] = "الاجمالي";
                        dr[5] = sumttld;
                        dr[6] = sumtrm;
                        dr[7] = 0;
                       // dr[8] = 0;
                        dr[8] = true;
                        dr[9] = "";
                        dr[10] = "";
                        // dr[12] = "";
                        //dr[13] = "";
                        // dr[8] = null;

                        dtn.Rows.Add(dr);

                        dataGridView1.DataSource = dtn;
                        txt_name.Text = dth.Rows[0][0].ToString();
                       // txt_code.Text = dth.Rows[0][2].ToString();
                        txt_opnbal.Text = sumop.ToString(); //dth.Rows[0][1].ToString();
                    }

                    else
                    {
                        txt_name.Text = "";
                        // txt_code.Text = dt.Rows[0][2].ToString();
                        txt_code.Text = "";
                        txt_opnbal.Text = "";
                    }

                    for (int i = 0; i < dataGridView1.Rows.Count - 1 ; ++i)
                    {
                        if (i == 0)
                        {
                            seso = Convert.ToDouble(sumop) + Convert.ToDouble(dataGridView1.Rows[i].Cells["c6"].Value.ToString().Trim()) - Convert.ToDouble(dataGridView1.Rows[i].Cells["c7"].Value.ToString().Trim());
                            //dataGridView1.Rows[i].Cells["c8"].Value = seso > 0 ? seso.ToString() : 0.ToString();
                            dataGridView1.Rows[i].Cells["c8"].Value = seso > 0 ? seso.ToString() : seso.ToString();
                            //dataGridView1.Rows[i].Cells["c9"].Value = seso < 0 ? (seso * -1).ToString() : 0.ToString();
                        }
                        else
                        {
                            seso = seso + Convert.ToDouble(dataGridView1.Rows[i].Cells["c6"].Value.ToString().Trim()) - Convert.ToDouble(dataGridView1.Rows[i].Cells["c7"].Value.ToString().Trim());
                            //dataGridView1.Rows[i].Cells[0].Value = seso.ToString();
                            //dataGridView1.Rows[i].Cells["c8"].Value = seso > 0 ? seso.ToString() : 0.ToString();
                            dataGridView1.Rows[i].Cells["c8"].Value = seso > 0 ? seso.ToString() : seso.ToString();
                            //dataGridView1.Rows[i].Cells["c9"].Value = seso < 0 ? (seso * -1).ToString() : 0.ToString();
                        }
                    }


                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        if (Convert.ToBoolean(rw.Cells["a_p"].Value) == false && !Convert.ToString(rw.Cells["c1"].Value).Equals(""))
                            // rw.DefaultCellStyle.BackColor = Color.Thistle;
                            rw.DefaultCellStyle.ForeColor = Color.Red;
                        //  rw.Cells["c1"].Value = rw.Cells["a_t"].Value.ToString().Equals("01") ? "ق عام" : rw.Cells["a_t"].Value.ToString().Equals("02") ? "س قبض" : "س صرف";
                        if (!Convert.ToString(rw.Cells["c1"].Value).Equals(""))
                            rw.Cells["c1"].Value = datval.validate_trtypes(rw.Cells["c1"].Value.ToString());
                    }
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                    dataGridView1.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //a_key = "";



            //try
            //{

            //    /*
            //    string qstr = "select a.a_key a,a.a_name b,round(a.a_opnbal+isnull(sum(case b.jddbcr when 'D' then b.a_camt else -b.a_damt end),0),2) c"
            //               + " from accounts a left outer join acc_dtl b on a.a_comp=b.a_comp and a.a_key=b.a_key and a.glcurrency=b.jdcurr"
            //               + " and b.a_key<>''"
            //        // +" where a.glcomp='01' --and a.glopnbal<>0 --and b.jdcurr='' "
            //               + " where a.a_comp='01' and a.a_key like '"+ textBox3.Text + "%'"
            //               + " group by a.a_comp,a.a_key,a.a_name,a.a_opnbal"
            //               + " having a.a_opnbal+isnull(sum(case b.jddbcr when 'D' then b.a_camt else -b.a_damt end),0)<>0"
            //               + " order by a.a_name";
            //     */

            //  //  string qstr = "select a_key,a_name,round(glopnbal,2) from accounts where glkey='" + a_key + "' and glcomp='01'";
            //    string qstr2 = "select round((case jddbcr when 'D' then a_damt else - a_camt end),2) as a,a_text as b,a_mdate as c from acc_dtl where a_key='" + textBox3.Text + "' and a_comp='01' order by a_mdate";

            //    dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);


            //}

            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }

        private void SalesReport_Load(object sender, EventArgs e)
        {
            string tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
            //   dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('04','05','14','15')");
            cmb_type.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no not in('24','26','31','32','33','34','35','45','46')");
            cmb_type.DisplayMember = "tr_name";
            cmb_type.ValueMember = "tr_no";
            cmb_type.SelectedIndex = -1;

            dtcur = daml.SELECT_QUIRY_only_retrn_dt("select curcode,curname,curlname,currate from Crncy order by curcode");
            cmb_cur.DataSource = dtcur;
            cmb_cur.DisplayMember = "curname";
            cmb_cur.ValueMember = "curcode";
            cmb_cur.SelectedIndex = -1;
            //txt_tdate.Text = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US", false));
            Load_Statment(a_key);

            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Syria));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.UAE));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.SaudiArabia));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Tunisia));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Gold));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Yemen));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.nul));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Kuwait));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Qatar));

            txt_opn.Text = BL.CLS_Session.start_dt;
            txt_fdate.Text = BL.CLS_Session.start_dt;
            txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));// BL.CLS_Session.end_dt;


            // double sum = 0;
            // double seso = 0;
            // try
            // {

            //         textBox3.Text=a_key;

            // //string qstr = "select a.a_key a,a.a_name b,round(a.a_opnbal+isnull(sum(case b.jddbcr when 'D' then b.a_camt else -b.a_damt end),0),2) c"
            // //               + " from accounts a left outer join acc_dtl b on a.a_comp=b.a_comp and a.a_key=b.a_key and a.glcurrency=b.jdcurr"
            // //               + " and b.a_key<>''"
            // //        // +" where a.glcomp='01' --and a.glopnbal<>0 --and b.jdcurr='' "
            // //               + " where a.a_comp='01' and a.a_key like '"+ textBox3.Text + "%'"
            // //               + " group by a.a_comp,a.a_key,a.a_name,a.a_opnbal"
            // //               + " having a.a_opnbal+isnull(sum(case b.jddbcr when 'D' then b.a_camt else -b.a_damt end),0)<>0"
            // //               + " order by a.a_name";
            //     string qstr2 = "select round((case jddbcr when 'D' then a_damt else - a_camt end),2) as a,a_text as b,a_mdate as c from acc_dtl where a_key='" + a_key + "' and a_comp='01' order by a_mdate";


            //     DataTable dt = daml.SELECT_QUIRY("select a_name,round(a_opnbal,2),a_key from accounts where a_key='" + a_key + "' and a_comp='01'");
            //     dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

            //     txt_name.Text=dt.Rows[0][0].ToString();
            //     for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            //     {
            //         if (i == 0)
            //         {
            //             seso = Convert.ToDouble(dt.Rows[0][1]) + Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString().Trim());
            //             dataGridView1.Rows[i].Cells[0].Value = seso.ToString();
            //         }
            //         else
            //         {
            //             seso = seso + Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString().Trim());
            //             dataGridView1.Rows[i].Cells[0].Value = seso.ToString();
            //         }
            //         //GridView2.Rows[i].Cells[0].Text = Convert.ToString(seso);
            //       //  dataGridView1.Rows[i].Cells[3].Style[HtmlTextWriterStyle.Direction] = "rtl";
            //       //  dataGridView1.Rows[i].Cells[2].Style[HtmlTextWriterStyle.Direction] = "rtl";
            //     }


            // }

            // catch (Exception ex)
            // {
            //     MessageBox.Show(ex.Message);
            // }
            // /*
            // maskedTextBox1.Text = DateTime.Now.ToString("yyyy-MM-01") + "05:00:00";

            // DateTime dt = new DateTime();

            // dt=DateTime.Now.AddDays(1);

            //// maskedTextBox2.Text = DateTime.Now.ToString("yyyy-MM-dd") + "05:00:00";
            // maskedTextBox2.Text = dt.ToString("yyyy-MM-dd") + "05:00:00";
            //  * */
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            /*
            if (e.KeyCode == Keys.F5)
            {
                // int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                DilySalesReport.cc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                // if (selectedIndex > -1)
                // {
                //dataGridView1.Rows.RemoveAt(selectedIndex);
                //dataGridView1.Refresh(); // if needed


                // }
                SalesDtlReport sdtr = new SalesDtlReport();
                //MAIN mn = new MAIN();
                //sdtr.MdiParent = mn;

                sdtr.ShowDialog();


            }
             * */
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            
                // int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                DilySalesReport.cc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                // if (selectedIndex > -1)
                // {
                //dataGridView1.Rows.RemoveAt(selectedIndex);
                //dataGridView1.Refresh(); // if needed


                // }
                SalesDtlReport sdtr = new SalesDtlReport();
                //MAIN mn = new MAIN();
                //sdtr.MdiParent = mn;

                sdtr.ShowDialog();
            */


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                XcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    XcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
             * */

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            // dataGridView1_CellContentDoubleClick((object)sender, (DataGridViewCellEventArgs)e);
            /*
            DilySalesReport.cc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            // if (selectedIndex > -1)
            // {
            //dataGridView1.Rows.RemoveAt(selectedIndex);
            //dataGridView1.Refresh(); // if needed


            // }
            SalesDtlReport sdtr = new SalesDtlReport();
            //MAIN mn = new MAIN();
            //sdtr.MdiParent = mn;

            sdtr.ShowDialog();
             */
            //  dataGridView1_KeyDown((object)sender,(EventArgs) e);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (comboBox1.SelectedItem.ToString() == "محلي" || comboBox1.SelectedItem.ToString() == "Local")
            {
                typeno = "1";
            }
            else
            {
                if (comboBox1.SelectedItem.ToString() == "سفري" || comboBox1.SelectedItem.ToString() == "Out")
                {
                    typeno = "3";
                }
                else
                {
                    if (comboBox1.SelectedItem.ToString() == "على الحساب" || comboBox1.SelectedItem.ToString() == "On Acc")
                    {
                        typeno = "4";
                    }
                    else
                    {
                        typeno = "";
                    }
                }

            }
             */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            seso = seso != 0 || dataGridView1.RowCount > 1 ? seso : Convert.ToDouble(txt_opnbal.Text);
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
                //  DataSet ds1 = new DataSet();

                DataTable dt = new DataTable();
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    dt.Columns.Add(col.Name);
                    // dt.Columns.Add(col.HeaderText);
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataRow dRow = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    dt.Rows.Add(dRow);
                }

                // dataGridView2.DataSource = dt;

                rf.reportViewer1.LocalReport.DataSources.Clear();
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("stament_exp", dt));
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("account", dth));

               // rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Acc.rpt.Acc_Statment_Org_rpt.rdlc";
                if (File.Exists(Directory.GetCurrentDirectory() + @"\reports\Acc_Statment_Org_rpt.rdlc"))
                    rf.reportViewer1.LocalReport.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Acc_Statment_Org_rpt.rdlc";
                else
                    rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Acc.rpt.Acc_Statment_Org_rpt.rdlc";

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[8];

                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                // parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                // parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());
                BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(seso < 0 ? seso * -1 : seso), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);
                parameters[1] = new ReportParameter("a_toword", cmb_cur.SelectedIndex != -1 ? "" : toWord.ConvertToArabic());
                parameters[2] = new ReportParameter("cur_bal", seso.ToString());
                parameters[3] = new ReportParameter("todate", txt_tdate.Text);
                parameters[4] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[5] = new ReportParameter("fdate", txt_fdate.Text);
                parameters[6] = new ReportParameter("openbal", txt_opnbal.Text);
                //parameters[7] = new ReportParameter("userin", txt_user.Text);
                parameters[7] = new ReportParameter("userprint", BL.CLS_Session.UserName);
                rf.reportViewer1.LocalReport.SetParameters(parameters);
                // */
                //MAIN mn = new MAIN();
                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();
            }
            catch { }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                Acc_Tran f4 = new Acc_Tran(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                f4.MdiParent = ParentForm;
                f4.Show();


            }
        }

        private void dataGridView1_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {/*
            Acc_Tran f4 = new Acc_Tran(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
            f4.Show();
          */
        }

        private void Load_Statment(string aakey)
        {
            // double sum = 0;
            if (aakey != "")
            {
                try
                {

                    // txt_code.Text = a_key;
                    /*
                    string qstr2 = "select a_type c1,a_ref c2,CONVERT(VARCHAR(10), CAST(a_mdate as date), 105) c3,CONVERT(VARCHAR(10), CAST(a_mdate as date), 105) d1,a_text c4,round((case jddbcr when 'C' then a_camt else 0 end),2) c5,round((case jddbcr when 'D' then a_damt else 0 end),2) c6 from acc_dtl where a_key='" + aakey + "' and a_comp='01' order by a_mdate";
                     dth = daml.SELECT_QUIRY("select a_name,round(a_opnbal,2),a_key from accounts where a_key='" + aakey + "' and a_comp='01'");
                    dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);
                    */
                    string cond = checkBox1.Checked ? "b.a_hdate" : "b.a_mdate";
                    string condp = rad_posted.Checked ? " and b.a_posted=1 " : (rad_notpostd.Checked ? " and b.a_posted=0 " : " ");

                    string qstr2 = "select a.a_type c1,a.a_ref c2,CONVERT(VARCHAR(10), CAST(a.a_mdate as date), 105) c3,CONVERT(VARCHAR(10), CAST(a.a_hdate as date), 105) c9,a.a_text c4,round((case a.jddbcr when 'C' then a.a_camt else 0 end),2) c5,round((case a.jddbcr when 'D' then a.a_damt else 0 end),2) c6,b.a_posted,a.a_type a_t from acc_dtl a,acc_hdr b  where a.a_brno=b.a_brno and a.a_ref=b.a_ref and a.a_type=b.a_type and " + condp + " and " + cond + " <= '" + txt_tdate.Text.Replace("-", "") + "' and a.a_key='" + aakey + "' and a.a_brno='" + BL.CLS_Session.brno + "' order by a.a_mdate,a.a_sysdate";
                    dth = daml.SELECT_QUIRY_only_retrn_dt("select a_name,round(a_opnbal,2) a_opnbal,a_key from accounts where a_key='" + aakey + "' and a_brno='" + BL.CLS_Session.brno + "'");
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt(qstr2);

                    if (dth.Rows.Count > 0)
                    {
                        txt_name.Text = dth.Rows[0][0].ToString();
                        txt_code.Text = dth.Rows[0][2].ToString();
                        txt_opnbal.Text = dth.Rows[0][1].ToString();
                    }

                    else
                    {
                        txt_name.Text = "";
                        // txt_code.Text = dt.Rows[0][2].ToString();
                        txt_opnbal.Text = "";
                    }

                    for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    {
                        if (i == 0)
                        {
                            seso = Convert.ToDouble(dth.Rows[0][1]) - Convert.ToDouble(dataGridView1.Rows[i].Cells["c5"].Value.ToString().Trim()) + Convert.ToDouble(dataGridView1.Rows[i].Cells["c6"].Value.ToString().Trim());
                            dataGridView1.Rows[i].Cells["c8"].Value = seso > 0 ? seso.ToString() : 0.ToString();
                            dataGridView1.Rows[i].Cells["c7"].Value = seso < 0 ? (seso * -1).ToString() : 0.ToString();
                        }
                        else
                        {
                            seso = seso - Convert.ToDouble(dataGridView1.Rows[i].Cells["c5"].Value.ToString().Trim()) + Convert.ToDouble(dataGridView1.Rows[i].Cells["c6"].Value.ToString().Trim());
                            //dataGridView1.Rows[i].Cells[0].Value = seso.ToString();
                            dataGridView1.Rows[i].Cells["c8"].Value = seso > 0 ? seso.ToString() : 0.ToString();
                            dataGridView1.Rows[i].Cells["c7"].Value = seso < 0 ? (seso * -1).ToString() : 0.ToString();
                        }
                    }

                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        if (Convert.ToBoolean(rw.Cells["a_p"].Value) == false)
                            // rw.DefaultCellStyle.BackColor = Color.Thistle;
                            rw.DefaultCellStyle.ForeColor = Color.Red;
                        //  rw.Cells["c1"].Value = rw.Cells["a_t"].Value.ToString().Equals("01") ? "ق عام" : rw.Cells["a_t"].Value.ToString().Equals("02") ? "س قبض" : "س صرف";
                    }


                }

                catch (Exception ex)
                {
                    //  MessageBox.Show(ex.Message);
                }
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                //////// var selectedCell = dataGridView1.SelectedCells[0];
                //////// do something with selectedCell...
                //////Search_All f4 = new Search_All("7", "Sup");
                //////f4.ShowDialog();

                //////try
                //////{

                //////    txt_code.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                //////    txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                //////    //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                //////    /*
                //////   dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
                //////   dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
                //////   dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                //////    */


                //////}
                //////catch { }
            }

            if (e.KeyCode == Keys.Enter)
            {

                button1_Click(sender, e);


            }


        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            /*
                Acc_Tran f4 = new Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Trim(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString().Trim());
                f4.MdiParent = ParentForm;    
                f4.Show();
            */

            SendKeys.Send("{F9}");
        }

        private void dataGridView1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acc1"))
                {
                    Acc.Acc_Tran f4 = new Acc.Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString(), "");
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }


                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acct"))
                {
                    Acc.Acc_Tax_Tran f4 = new Acc.Acc_Tax_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString(), "");
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }
                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acct2"))
                {
                    Acc.Acc_Tax_Tran2 f4 = new Acc.Acc_Tax_Tran2(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString(), "");
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Sup2"))
                {
                    Sup.Acc_Tran f4 = new Sup.Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Sup1"))
                {
                    Sup.Acc_Tran_S f4 = new Sup.Acc_Tran_S(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Sal1") || dataGridView1.CurrentRow.Cells["src"].Value.Equals("Pos"))
                {
                    Sal.Sal_Tran_D f4 = new Sal.Sal_Tran_D(dataGridView1.CurrentRow.Cells["sl"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Sal2"))
                {
                    Sal.Sal_Tran_notax f4 = new Sal.Sal_Tran_notax(dataGridView1.CurrentRow.Cells["sl"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }
                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Pur1"))
                {
                    Pur.Pur_Tran_D f4 = new Pur.Pur_Tran_D(dataGridView1.CurrentRow.Cells["pu"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Pur2"))
                {
                    Pur.Pur_Tran_notax f4 = new Pur.Pur_Tran_notax(dataGridView1.CurrentRow.Cells["pu"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }
                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Supc"))
                {
                    Sup.Acc_Tran_Cur f4 = new Sup.Acc_Tran_Cur(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                //  maskedTextBox1.Text = DateTime.Now.ToString("yyyy-01-01", new CultureInfo("ar-SA", false));
                //  string temy2 =
                //  txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));
                txt_fdate.Text = datval.convert_to_hdate(txt_fdate.Text);
                txt_tdate.Text = datval.convert_to_hdate(txt_tdate.Text);
            }
            else
            {
                //  maskedTextBox1.Text = DateTime.Now.ToString("yyyy-01-01", new CultureInfo("en-US", false));
                //  string temy2 =
                // txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
                txt_fdate.Text = datval.convert_to_mdate(txt_fdate.Text);
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

        private void txt_fdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_tdate.Focus();
                txt_tdate.SelectAll();
            }
        }

        private void txt_tdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
        }

        private void txt_fdate_Enter(object sender, EventArgs e)
        {
            txt_fdate.SelectAll();
        }

        private void txt_code_DoubleClick(object sender, EventArgs e)
        {
            ////Search_All f4 = new Search_All("7", "Sup");
            ////f4.ShowDialog();

            ////try
            ////{

            ////    txt_code.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            ////    txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            ////    //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

            ////    /*
            ////   dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
            ////   dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
            ////   dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
            ////    */


            ////}
            ////catch { }
        }

        private void cmb_type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_type.SelectedIndex = -1;
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int testInt = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["a_p"].Value) ? 1 : 0;
                datval.formate_dgv(this.dataGridView1, testInt);
            }
            catch { }
        }
    }
}

