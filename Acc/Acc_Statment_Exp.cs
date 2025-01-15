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
//using POS.BL;

namespace POS.Acc
{
    public partial class Acc_Statment_Exp : BaseForm
    {
        List<BL.CurrencyInfo> currencies = new List<BL.CurrencyInfo>();

        double seso = 0;
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dth;
        public static int qq;
        string a_key,acc,conkey;
        SqlConnection con3 =BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public Acc_Statment_Exp(string akey)
        {
            InitializeComponent();

            a_key = akey.Trim();
           // 
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        public void button1_Click(object sender, EventArgs e)
        {
            seso = 0;
            if (txt_code.Text == "")
            {
                MessageBox.Show("يجب ادخال رقم الحساب", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_code.Focus();
                return;

            }
       // if(txt_code.Text !="")
            Load_Statment(txt_code.Text);
            /*
            if (txt_code.Text != "")
            {
                try
                {

         
                    string cond = checkBox1.Checked ? "b.a_hdate" : "b.a_mdate";
                    string condp = rad_posted.Checked ? " and b.a_posted=1 " : (rad_notpostd.Checked ? " and b.a_posted=0 " : " ");
                    string conddm = chk_d.Checked ? " and a.jddbcr='C' " : (chk_m.Checked ? " and a.jddbcr='D' " : " ");
                    //  string condtx = txt_desc.Text;

                    //   string qstr2 = "select a.a_type c1, a.a_ref c2, CONVERT(VARCHAR(10), CAST(a.a_mdate as date), 105) c3, (substring(a.a_hdate,7,2) + '-' + substring(a.a_hdate,5,2)+'-'+substring(a.a_hdate,1,4)) c4,a.a_text c5,round((case a.jddbcr when 'D' then a.a_damt else - a.a_camt end),2) c6,b.a_posted,a.a_type a_t from acc_dtl a,acc_hdr b where a.a_brno=b.a_brno and a.a_ref=b.a_ref and a.a_type=b.a_type " + condp + conddm + " and " + cond + " <= '" + txt_tdate.Text.Replace("-", "") + "' and a.a_key='" + aakey + "' and a.a_brno='" + BL.CLS_Session.brno + "' and a.a_text like '%" + txt_desc.Text.Replace(" ", "%") + "%' order by a.a_mdate";
                    string qstr2 = "select a.a_type c1, a.a_ref c2, CONVERT(VARCHAR(10), CAST(a.a_mdate as date), 105) c3, (substring(a.a_hdate,7,2) + '-' + substring(a.a_hdate,5,2)+'-'+substring(a.a_hdate,1,4)) c4,a.a_text c5,round((case a.jddbcr when 'D' then a.a_damt else - a.a_camt end),2) c6,b.a_posted,a.a_type a_t from acc_dtl a,acc_hdr b where a.a_brno=b.a_brno and a.a_ref=b.a_ref and a.a_type=b.a_type " + condp + conddm + " and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and a.a_key='" + txt_code.Text + "' and a.a_brno='" + BL.CLS_Session.brno + "' and a.a_text like '%" + txt_desc.Text.Replace(" ", "%") + "%' order by a.a_mdate";

                    dth = daml.SELECT_QUIRY_only_retrn_dt("select a_name,round(a_opnbal,2) a_opnbal,a_key  from accounts where a_key='" + txt_code.Text + "' and a_brno='" + BL.CLS_Session.brno + "'");
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt(qstr2);
                    if (dth.Rows.Count > 0)
                    {
                        txt_name.Text = dth.Rows[0][0].ToString();
                        txt_code.Text = dth.Rows[0][2].ToString();
                        txt_opnbal.Text = dth.Rows[0][1].ToString();
                    }
                    else
                    {
                        //    MessageBox.Show("الحساب غير موجود");
                        txt_name.Text = "";
                        txt_code.Text = "";
                        //  txt_code.Text = dt.Rows[0][2].ToString();
                        txt_opnbal.Text = "0";
                    }
               

                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        if (Convert.ToBoolean(rw.Cells["a_p"].Value) == false)
                            // rw.DefaultCellStyle.BackColor = Color.Thistle;
                            rw.DefaultCellStyle.ForeColor = Color.Red;
                        // rw.Cells["c1"].Value = rw.Cells["a_t"].Value.ToString().Equals("01") ? "ق عام" : rw.Cells["a_t"].Value.ToString().Equals("02") ? "س قبض" : "س صرف";
                        rw.Cells["c1"].Value = datval.validate_trtypes(rw.Cells["c1"].Value.ToString());
                    }



                    dataGridView1_calculate();
                    paintg();

                    dataGridView1.ClearSelection();
                


                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            */
              //  Load_Statment(txt_code.Text);
             
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

        public void SalesReport_Load(object sender, EventArgs e)
        {
           
            acc =!string.IsNullOrEmpty(BL.CLS_Session.acckey) ? BL.CLS_Session.acckey.Replace(" ", "','").Remove(0, 2) + "'" : " ";
            conkey = !string.IsNullOrEmpty(BL.CLS_Session.acckey) ? " and a_key in(" + acc + ")" : " ";
          //  MessageBox.Show(acc);
          //  MessageBox.Show(conkey);
            string tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
            //   dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('04','05','14','15')");
            cmb_type.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no not in('24','26','33','34','35','45','46')");
            cmb_type.DisplayMember = BL.CLS_Session.lang.Equals("E") ? "tr_ename" : "tr_name";
            cmb_type.ValueMember = "tr_no";
            cmb_type.SelectedIndex = -1;

            try
            {
                if (!BL.CLS_Session.formkey.Contains("A133"))
                {
                   // this.Close();
                }
                else
                {

                    txt_fdate.Text = BL.CLS_Session.start_dt;
                    txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));// BL.CLS_Session.end_dt;


                    txt_opn.Text = BL.CLS_Session.start_dt;

                    // txt_code.Text = a_key;
                   // Load_Statment(a_key);

                    //  MessageBox.Show("-" + a_key + "-");
                    // button1_Click(sender, e);
                    // txt_tdate.Text = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US", false));

                    // Load_Statment(a_key);

                    currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Syria));
                    currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.UAE));
                    currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.SaudiArabia));
                    currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Tunisia));
                    currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Gold));
                    currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Yemen));
                    currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.nul));
                    currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Kuwait));
                    currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Qatar));

                    txt_fdate.Text = BL.CLS_Session.start_dt;
                    txt_tdate.Text = BL.CLS_Session.end_dt;


                    txt_opn.Text = BL.CLS_Session.start_dt;

                    //paintg();
                    // button1_Click(null, null);

                    dataGridView1.ClearSelection();

                    // Load_Statment(a_key);
                    // button1_Click(sender, e);
                    // button1_Click(sender, e);

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
            }
            catch { }
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
                            //XcelApp.Cells[i + 6, j + 1] = "'" + dataGridView1.Rows[i].Cells[j].Value.ToString();
                            XcelApp.Cells[i + 6, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString().StartsWith("0") && dataGridView1.Rows[i].Cells[j].Value.ToString().Length > 1 ? "'" + dataGridView1.Rows[i].Cells[j].Value.ToString() : dataGridView1.Rows[i].Cells[j].Value.ToString();
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
                workSheet.Cells[1, "D"] = BL.CLS_Session.cmp_name;
                workSheet.Cells[2, "D"] = BL.CLS_Session.brname;
                workSheet.Cells[3, "D"] = this.Text;

                workSheet.Range["C1", "E1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                workSheet.Range["C2", "E2"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                workSheet.Range["C3", "E3"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

               // workSheet.Range["A" + (dataGridView1.Rows.Count + 5).ToString(), "G" + (dataGridView1.Rows.Count + 5).ToString()].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                workSheet.Range["A5", "G5"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                // workSheet.Cells[2, "D"]

                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
            //////if (dataGridView1.Rows.Count > 0)
            //////{
            //////    Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
            //////    XcelApp.Application.Workbooks.Add(Type.Missing);



            //////    for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            //////    {
            //////        if(dataGridView1.Columns[i-1].Visible == true)
            //////            XcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            //////        //else
                    
            //////      //  XcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].Visible == true ? dataGridView1.Columns[i - 1].HeaderText : "";
            //////    }

            //////    for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //////    {
            //////        for (int j = 0; j < dataGridView1.Columns.Count; j++)
            //////        {
            //////            if (dataGridView1.Columns[j].Visible == true)
            //////                XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
            //////           // XcelApp.Cells[i + 2, j + 1] = dataGridView1.Columns[j].Visible == true ? dataGridView1.Rows[i].Cells[j].Value.ToString() : "";
            //////        }
            //////    }
            //////   // XcelApp.Cells[0, 0] = "123";
            //////    XcelApp.Columns.AutoFit();
            //////    XcelApp.Visible = true;
            //////}

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
            //            XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
            //        }
            //    }
            //    XcelApp.Columns.AutoFit();
            //    XcelApp.Visible = true;
            //}
             
           
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
            seso = seso != 0 || dataGridView1.RowCount>0? seso : Convert.ToDouble(txt_opnbal.Text);
            //if (dataGridView1.Rows.Count == 0)
            //    return;
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

                if (File.Exists(Directory.GetCurrentDirectory() + @"\reports\Acc_Statment_Exp_rpt.rdlc"))
                {
                    rf.reportViewer1.LocalReport.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Acc_Statment_Exp_rpt.rdlc";
                }
                else
                {
                    rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Acc.rpt.Acc_Statment_Exp_rpt.rdlc";
                }
                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
                
                ReportParameter[] parameters = new ReportParameter[12];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);


                BL.ToWord toWord;
                
                if(seso!=0)
                   toWord = new BL.ToWord(Convert.ToDecimal(seso < 0 ? seso * -1 : seso), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);
                else
                    toWord = new BL.ToWord(Convert.ToDecimal(Convert.ToDecimal(txt_opnbal.Text) < 0 ? Convert.ToDecimal(txt_opnbal.Text) * -1 : Convert.ToDecimal(txt_opnbal.Text)), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);
                    // txtEnglishWord.Text = toWord.ConvertToEnglish();
                   // label7.Text = toWord.ConvertToArabic();
                
               
               // parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                parameters[1] = new ReportParameter("a_toword", toWord.ConvertToArabic());
                parameters[2] = new ReportParameter("cur_bal", seso.ToString());
                parameters[3] = new ReportParameter("todate", txt_tdate.Text);
                parameters[4] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[5] = new ReportParameter("fdate", txt_fdate.Text);
                parameters[6] = new ReportParameter("openbal", txt_opnbal.Text);
                parameters[7] = new ReportParameter("userprint", BL.CLS_Session.UserName);
                parameters[8] = new ReportParameter("comp_ename", BL.CLS_Session.cmp_ename);
                parameters[9] = new ReportParameter("comp_taxid", BL.CLS_Session.tax_no);
                parameters[10] = new ReportParameter("comp_adress", BL.CLS_Session.dtcomp.Rows[0]["city"].ToString() + " - " + BL.CLS_Session.dtcomp.Rows[0]["site_name"].ToString() + " - " + BL.CLS_Session.dtcomp.Rows[0]["street"].ToString());
                parameters[11] = new ReportParameter("comp_rg", BL.CLS_Session.dtcomp.Rows[0]["ownr_mob"].ToString());
                rf.reportViewer1.LocalReport.SetParameters(parameters);
               // */
                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();
            }
            catch { }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            Acc_Tran f4 = new Acc_Tran(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
            f4.Show();
             */
        }

        public void Load_Statment(string aakey)
        {
            /*
           // double sum = 0;
            double seso = 0;
            try
            {

               // txt_code.Text = a_key;
                string qstr2 = "select round((case jddbcr when 'D' then a_damt else 0 end),2) as f,round((case jddbcr when 'C' then a_camt else 0 end),2) as e,a_text as d,CONVERT(VARCHAR(10), CAST(a_mdate as date), 105) as c,a_type a,a_ref b from acc_dtl where a_key='" + aakey + "' and a_comp='01' order by a_mdate";
                DataTable dt = daml.SELECT_QUIRY("select a_name,round(a_opnbal,2),a_key from accounts where a_key='" + aakey + "' and a_comp='01'");
                dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

                txt_name.Text = dt.Rows[0][0].ToString();
                txt_code.Text=dt.Rows[0][2].ToString();
                txt_opnbal.Text=dt.Rows[0][1].ToString();
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    if (i == 0)
                    {
                        seso = Convert.ToDouble(dt.Rows[0][1]) + Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString().Trim()) - Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value.ToString().Trim());
                        dataGridView1.Rows[i].Cells[0].Value = seso > 0 ? seso.ToString() : 0.ToString();
                        dataGridView1.Rows[i].Cells[1].Value = seso < 0 ? seso.ToString() : 0.ToString();
                    }
                    else
                    {
                        seso = seso + Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString().Trim()) - Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value.ToString().Trim());
                        //dataGridView1.Rows[i].Cells[0].Value = seso.ToString();
                        dataGridView1.Rows[i].Cells[0].Value = seso > 0 ? seso.ToString() : 0.ToString();
                        dataGridView1.Rows[i].Cells[1].Value = seso < 0 ? seso.ToString() : 0.ToString();
                    }
                }


            }
            
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
            }
            */
          //  double seso = 0;
           // MessageBox.Show(datval.convert_to_yyyyDDdd(txt_tdate.Text));
            seso = 0;
            if (aakey != "")
            {
                try
                {
                    if (!txt_fdate.Text.Equals(BL.CLS_Session.start_dt))
                        txt_opn.Text = txt_fdate.Text;
                    // txt_code.Text = a_key;
                    /*
                    string qstr2 = "select a_type c1, a_ref c2, CONVERT(VARCHAR(10), CAST(a_mdate as date), 105) c3, CONVERT(VARCHAR(10), CAST(a_hdate as date), 105) c4,a_text c5,round((case jddbcr when 'D' then a_damt else - a_camt end),2) c6 from acc_dtl where a_key='" + aakey + "' and a_comp='01' order by a_mdate";
                    dth = daml.SELECT_QUIRY("select a_name,round(a_opnbal,2) a_opnbal,a_key  from accounts where a_key='" + aakey + "' and a_comp='01'");
                    dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);
                    */
                    string condt = cmb_type.SelectedIndex != -1 ? " and a.a_type = '" + cmb_type.SelectedValue.ToString() + "' " : " ";
                    string cond = checkBox1.Checked ? "b.a_hdate" : "b.a_mdate";
                    string condp = rad_posted.Checked ? " and b.a_posted=1 " : (rad_notpostd.Checked ? " and b.a_posted=0 " : " ");
                    string conddm = chk_d.Checked ? " and a.jddbcr='C' " : (chk_m.Checked ? " and a.jddbcr='D' " : " ");
                  //  string condtx = txt_desc.Text;

                 //   string qstr2 = "select a.a_type c1, a.a_ref c2, CONVERT(VARCHAR(10), CAST(a.a_mdate as date), 105) c3, (substring(a.a_hdate,7,2) + '-' + substring(a.a_hdate,5,2)+'-'+substring(a.a_hdate,1,4)) c4,a.a_text c5,round((case a.jddbcr when 'D' then a.a_damt else - a.a_camt end),2) c6,b.a_posted,a.a_type a_t from acc_dtl a,acc_hdr b where a.a_brno=b.a_brno and a.a_ref=b.a_ref and a.a_type=b.a_type " + condp + conddm + " and " + cond + " <= '" + txt_tdate.Text.Replace("-", "") + "' and a.a_key='" + aakey + "' and a.a_brno='" + BL.CLS_Session.brno + "' and a.a_text like '%" + txt_desc.Text.Replace(" ", "%") + "%' order by a.a_mdate";
                    string qstr2 = "select a.a_type c1, a.a_ref c2, CONVERT(VARCHAR(10), CAST(a.a_mdate as date), 105) c3, (substring(a.a_hdate,7,2) + '-' + substring(a.a_hdate,5,2)+'-'+substring(a.a_hdate,1,4)) c4,a.a_text c5,round((case a.jddbcr when 'D' then a.a_damt else - a.a_camt end),4) c6,b.a_posted,a.a_type a_t,b.jhsrc src,b.sl_no sl,b.pu_no pu  from acc_dtl a,acc_hdr b where a.a_brno=b.a_brno and a.a_ref=b.a_ref and a.a_type=b.a_type and a.sl_no=b.sl_no and a.pu_no=b.pu_no " + condp + conddm + condt + " and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and a.a_key='" + aakey + "' and a.a_brno='" + BL.CLS_Session.brno + "' and a.a_text like '%" + txt_desc.Text.Replace(" ", "%") + "%' order by a.a_mdate,a.a_sysdate";

                   // dth = daml.SELECT_QUIRY_only_retrn_dt("select a_name,(round(a_opnbal,2) + (select isnull(sum(b.a_camt-b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "')) a_opnbal,a_key  from accounts where a_key='" + aakey + "' and a_brno='" + BL.CLS_Session.brno + "'" + conkey + "");
                    dth = daml.SELECT_QUIRY_only_retrn_dt("select a.a_name,round((a.a_opnbal + (select isnull(sum(b.a_damt-b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' where a.a_key=b.a_key)),4) a_opnbal,a.a_key  from accounts a where a.a_key='" + aakey + "' and a.a_brno='" + BL.CLS_Session.brno + "'" + conkey + "");
                   
                    if(dth.Rows.Count>0)
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt(qstr2);

                    if (dth.Rows.Count > 0)
                    {
                        txt_name.Text = dth.Rows[0][0].ToString();
                        txt_code.Text = dth.Rows[0][2].ToString();
                        txt_opnbal.Text = dth.Rows[0][1].ToString();
                    }
                    else
                    {
                        //    MessageBox.Show("الحساب غير موجود");
                        txt_name.Text = "";
                        txt_code.Text = "";
                        //  txt_code.Text = dt.Rows[0][2].ToString();
                        txt_opnbal.Text = "0";
                      //  dataGridView1.Rows.Clear();
                    }
                    /*
                    double seso = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    {
                        if (i == 0)
                        {
                            seso = Convert.ToDouble(txt_opnbal.Text) + Convert.ToDouble(dataGridView1.Rows[i].Cells["c6"].Value.ToString().Trim());
                            dataGridView1.Rows[i].Cells["c7"].Value = Convert.ToDouble(seso);
                        }
                        else
                        {
                            seso = seso + Convert.ToDouble(dataGridView1.Rows[i].Cells["c6"].Value.ToString().Trim());
                            dataGridView1.Rows[i].Cells["c7"].Value = Convert.ToDouble(seso);
                        }
                    }
                    */
                    
                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        if (Convert.ToBoolean(rw.Cells["a_p"].Value) == false)
                            // rw.DefaultCellStyle.BackColor = Color.Thistle;
                            rw.DefaultCellStyle.ForeColor = Color.Red;
                       // rw.Cells["c1"].Value = rw.Cells["a_t"].Value.ToString().Equals("01") ? "ق عام" : rw.Cells["a_t"].Value.ToString().Equals("02") ? "س قبض" : "س صرف";
                        rw.Cells["c1"].Value = datval.validate_trtypes(rw.Cells["c1"].Value.ToString());

                        if (rw.Index == 0)
                        {
                            seso = Convert.ToDouble(txt_opnbal.Text) + Convert.ToDouble(rw.Cells["c6"].Value.ToString().Trim());
                            rw.Cells["c7"].Value = Math.Round(seso, 4).ToString();// Convert.ToDouble(seso);
                        }
                        else
                        {
                            seso = seso + Convert.ToDouble(rw.Cells["c6"].Value.ToString().Trim());
                            rw.Cells["c7"].Value = Math.Round(seso, 4).ToString();// Convert.ToDouble(seso);

                        }
                    }

                    
                        

                    

                  //  dataGridView1_calculate();
                   // paintg();

                    dataGridView1.ClearSelection();
                    // dataGridView1.Refresh();
                    /*
                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        if (Convert.ToBoolean(rw.Cells["a_p"].Value) == false)
                            // rw.DefaultCellStyle.BackColor = Color.Thistle;
                            rw.DefaultCellStyle.ForeColor = Color.Red;
                        rw.Cells["c1"].Value = rw.Cells["a_t"].Value.ToString().Equals("01") ? "ق عام" : rw.Cells["a_t"].Value.ToString().Equals("02") ? "س قبض" : "س صرف";
                    }
                   */
                    //////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    //////{
                    //////    if (i == 0)
                    //////    {
                    //////        seso = Convert.ToDouble(dt.Rows[0][1]) + Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString().Trim());
                    //////        dataGridView1.Rows[i].Cells[0].Value = seso.ToString();
                    //////    }
                    //////    else
                    //////    {
                    //////        seso = seso + Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString().Trim());
                    //////        dataGridView1.Rows[i].Cells[0].Value = seso.ToString();
                    //////    }
                    //////}


                }

                catch (Exception ex)
                {
                      MessageBox.Show(ex.Message);
                }
            }


        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    // var selectedCell = dataGridView1.SelectedCells[0];
                    // do something with selectedCell...
                    Search_All f4 = new Search_All("1", "Acc");
                    f4.ShowDialog();



                    txt_code.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                    /*
                   dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
                   dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
                   dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                    */



                }

                if (e.KeyCode == Keys.Enter)
                {

                    // button1_Click( sender,  e);
                    DataTable dta = daml.SELECT_QUIRY_only_retrn_dt("select a_name,round(a_opnbal,2) a_opnbal,a_key  from accounts where a_key='" + txt_code.Text + "' and a_brno='" + BL.CLS_Session.brno + "'" + conkey + "");
                    if (dta.Rows.Count > 0)
                    {
                        txt_name.Text = dta.Rows[0][0].ToString();
                        txt_code.Text = dta.Rows[0][2].ToString();
                        txt_opnbal.Text = dta.Rows[0][1].ToString();
                        button1_Click(sender, e);

                    }
                    else
                    {
                        //    MessageBox.Show("الحساب غير موجود");
                        txt_name.Text = "";
                        txt_code.Text = "";
                        //  txt_code.Text = dt.Rows[0][2].ToString();
                        txt_opnbal.Text = "0";
                    }

                }
            }
            catch { }
         
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            /*
                Acc_Tran f4 = new Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Trim(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString().Trim());
                f4.MdiParent = MdiParent;    
                f4.Show();
            */
            SendKeys.Send("{F9}");
            
        }

        private void dataGridView1_KeyDown_1(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F9)
            //{
            //    Acc.Acc_Tran f4 = new Acc.Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Trim(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString().Trim());
            //    f4.MdiParent = MdiParent;
            //    f4.Show();


            //}
            if (e.KeyCode == Keys.F9)
            {
                //if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acc1"))
                //{
                //    Acc_Tran f4 = new Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                //    f4.MdiParent = ParentForm;
                //    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                //        f4.Show();
                //}

                //if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acc2"))
                //{
                //    Acc_Tran_Q f4 = new Acc_Tran_Q(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                //    f4.MdiParent = ParentForm;
                //    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                //        f4.Show();
                //}

                //if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acc3"))
                //{
                //    Acc_Tran_S f4 = new Acc_Tran_S(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                //    f4.MdiParent = ParentForm;
                //    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                //        f4.Show();
                //}

                //if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acct"))
                //{
                //    Acc_Tax_Tran f4 = new Acc_Tax_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                //    f4.MdiParent = ParentForm;
                //    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                //        f4.Show();
                //}
                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acc1"))
                {
                    Acc_Tran f4 = new Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString(),"");
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.ToString().StartsWith("Grd"))
                {
                   // Acc_Tran_All f4 = new Acc_Tran_All(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), "");
                    Acc_Tran_All f4 = new Acc_Tran_All(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString(), "");
                    f4.MdiParent = ParentForm;
                    // if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                    f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acc2"))
                {
                    Acc_Tran_Q f4 = new Acc_Tran_Q(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acc3"))
                {
                    Acc_Tran_S f4 = new Acc_Tran_S(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acct"))
                {
                    Acc_Tax_Tran f4 = new Acc_Tax_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString(),"");
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }
                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acct2"))
                {
                    Acc_Tax_Tran2 f4 = new Acc_Tax_Tran2(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString(), "");
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Cus1"))
                {
                    Cus.Acc_Tran_Q f4 = new Cus.Acc_Tran_Q(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Cus2"))
                {
                    Sup.Acc_Tran f4 = new Sup.Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Sal1") || dataGridView1.CurrentRow.Cells["src"].Value.Equals("Pos"))
                {
                    if (BL.CLS_Session.cmp_type.StartsWith("a"))
                    {
                        Sal.Sal_Tran_D_TF f4 = new Sal.Sal_Tran_D_TF(dataGridView1.CurrentRow.Cells["sl"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString());
                        f4.MdiParent = ParentForm;
                        if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                            f4.Show();
                    }
                    else
                    {
                        Sal.Sal_Tran_D f4 = new Sal.Sal_Tran_D(dataGridView1.CurrentRow.Cells["sl"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString());
                        f4.MdiParent = ParentForm;
                        if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                            f4.Show();
                    }
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Sal2"))
                {
                    Sal.Sal_Tran_notax f4 = new Sal.Sal_Tran_notax(dataGridView1.CurrentRow.Cells["sl"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString());
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

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Sup2"))
                {
                    Sup.Acc_Tran f4 = new Sup.Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["c2"].Value.ToString());
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

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
           

        }

        private void txt_opnbal_TextChanged(object sender, EventArgs e)
        {
           
                 
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //double seso = 0;
            //for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            //{
            //    if (i == 0)
            //    {
            //        seso = Convert.ToDouble(txt_opnbal.Text) + Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString().Trim());
            //        dataGridView1.Rows[i].Cells[0].Value = seso.ToString();
            //    }
            //    else
            //    {
            //        seso = seso + Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString().Trim());
            //        dataGridView1.Rows[i].Cells[0].Value = seso.ToString();
            //    }
            //}
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //double seso = 0;
            //for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            //{
            //    if (i == 0)
            //    {
            //        seso = Convert.ToDouble(txt_opnbal.Text) +  Convert.ToDouble(dataGridView1.Rows[i].Cells["c6"].Value.ToString().Trim());
            //        dataGridView1.Rows[i].Cells["c7"].Value = seso.ToString();
            //    }
            //    else
            //    {
            //        seso = seso + Convert.ToDouble(dataGridView1.Rows[i].Cells["c6"].Value.ToString().Trim());
            //        dataGridView1.Rows[i].Cells["c7"].Value =  seso.ToString();
            //    }
            //}

           // dataGridView1_calculate();
           // paintg();

         //   dataGridView1_calculate();
            /*
            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(rw.Cells["a_p"].Value) == false)
                    // rw.DefaultCellStyle.BackColor = Color.Thistle;
                    rw.DefaultCellStyle.ForeColor = Color.Red;
                rw.Cells["c1"].Value = rw.Cells["a_t"].Value.ToString().Equals("01") ? "ق عام" : rw.Cells["a_t"].Value.ToString().Equals("02") ? "س قبض" : "س صرف";
            }
             */
        }
        private void dataGridView1_calculate()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                if (i == 0)
                {
                    seso = Convert.ToDouble(txt_opnbal.Text) + Convert.ToDouble(dataGridView1.Rows[i].Cells["c6"].Value.ToString().Trim());
                    dataGridView1.Rows[i].Cells["c7"].Value = Convert.ToDouble(seso);
                }
                else
                {
                    seso = seso + Convert.ToDouble(dataGridView1.Rows[i].Cells["c6"].Value.ToString().Trim());
                    dataGridView1.Rows[i].Cells["c7"].Value = Convert.ToDouble(seso);

                }
   
            }
           
        }

        private void paintg()
        {
            
            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
               // rw.Cells["c1"].Value = rw.Cells["a_t"].Value.ToString().Equals("01") ? "ق عام" : rw.Cells["a_t"].Value.ToString().Equals("02") ? "س قبض" : "س صرف";
                // if (Convert.ToBoolean(rw.Cells["a_p"].Value) == false)
                //     rw.DefaultCellStyle.ForeColor = Color.Red;
                rw.DefaultCellStyle.ForeColor = Convert.ToBoolean(rw.Cells["a_p"].Value) == false ? Color.Red : Color.Black;

            }
             
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(txt_opnbal.Text), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);
               // txtEnglishWord.Text = toWord.ConvertToEnglish();
                label7.Text = toWord.ConvertToArabic();
            }
            catch (Exception ex)
            {
              //  txtEnglishWord.Text = String.Empty;
                label7.Text = String.Empty;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
              //  maskedTextBox1.Text = DateTime.Now.ToString("yyyy-01-01", new CultureInfo("ar-SA", false));
                //  string temy2 =
                txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));
                txt_fdate.Text = DateTime.Now.ToString("01-01-yyyy", new CultureInfo("ar-SA", false));
            }
            else
            {
              //  maskedTextBox1.Text = DateTime.Now.ToString("yyyy-01-01", new CultureInfo("en-US", false));
                //  string temy2 =
                txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
                txt_fdate.Text = DateTime.Now.ToString("01-01-yyyy", new CultureInfo("en-US", false));
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
               // SendKeys.Send("{Home}");
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
            Search_All f4 = new Search_All("1", "Acc");
            f4.ShowDialog();

            try
            {

                txt_code.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                /*
               dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
               dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
               dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                */


            }
            catch { }
        }

        private void cmb_type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_type.SelectedIndex = -1;
            }
        }

        private void Acc_Statment_Exp_Activated(object sender, EventArgs e)
        {
            // this.WindowState = FormWindowState.Normal;
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

        private void txt_code_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // var selectedCell = dataGridView1.SelectedCells[0];
                // do something with selectedCell...
                Search_All f4 = new Search_All("1", "Acc");
                f4.ShowDialog();

                try
                {

                    txt_code.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();

                }
                catch { }
            }
        }

        public void btn_prtdirct_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.Rows.Count == 0)
            //    return;

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

                //rf.reportViewer1.LocalReport.DataSources.Clear();
                //rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("stament_exp", dt));
                //rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("account", dth));

                LocalReport report = new LocalReport();
                report.ReportEmbeddedResource = "POS.Acc.rpt.Acc_Statment_Exp_rpt.rdlc";
                // report.DataSources.Add(new ReportDataSource("DataSet1", getYourDatasource()));
                // report.DataSources.Add(new ReportDataSource("sdt", dt));
                report.DataSources.Add(new ReportDataSource("stament_exp", dt));
                report.DataSources.Add(new ReportDataSource("account", dth));

                //if (BL.CLS_Session.lang.Equals("ع"))
                //    rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Acc.rpt.Acc_Statment_Exp_rpt.rdlc";
                //else
                //    rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Acc.rpt.Acc_Statment_Exp_en_rpt.rdlc";
                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[7];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);


                BL.ToWord toWord;

                if (seso != 0)
                    toWord = new BL.ToWord(Convert.ToDecimal(seso < 0 ? seso * -1 : seso), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);
                else
                    toWord = new BL.ToWord(Convert.ToDecimal(Convert.ToDecimal(txt_opnbal.Text) < 0 ? Convert.ToDecimal(txt_opnbal.Text) * -1 : Convert.ToDecimal(txt_opnbal.Text)), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);
                // txtEnglishWord.Text = toWord.ConvertToEnglish();
                // label7.Text = toWord.ConvertToArabic();


                // parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
              //  parameters[1] = new ReportParameter("a_toword", toWord.ConvertToArabic());
              //  parameters[2] = new ReportParameter("cur_bal", seso != 0 ? seso.ToString() : txt_opnbal.Text);
                parameters[1] = new ReportParameter("a_toword", BL.CLS_Session.lang.Equals("ع") ? toWord.ConvertToArabic() : toWord.ConvertToEnglish());
                parameters[2] = new ReportParameter("cur_bal", seso != 0 ? seso.ToString() : txt_opnbal.Text);
                parameters[3] = new ReportParameter("todate", txt_tdate.Text);
                parameters[4] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[5] = new ReportParameter("fdate", txt_fdate.Text);
                parameters[6] = new ReportParameter("openbal", txt_opnbal.Text);

                report.SetParameters(parameters);
                ////////BL.PrintReport.pagetype = "Acc";
                ////////BL.PrintReport.PrintToPrinter(report);
                BL.Print_Rdlc_Direct.Print(report, 1, "");

                //rf.reportViewer1.LocalReport.SetParameters(parameters);
                //// */
                //rf.reportViewer1.RefreshReport();
                //rf.MdiParent = ParentForm;
                //rf.Show();
            }
            catch { }
        }

        private void Acc_Statment_Exp_Shown(object sender, EventArgs e)
        {
            if (!BL.CLS_Session.formkey.Contains(this.Tag.ToString()))
            {
                this.Close();
            }
            else
            {
                Load_Statment(a_key);
            }
        }
    }
}
