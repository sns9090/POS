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

namespace POS.Acc
{
    public partial class Acc_Report : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dt, dtusers;
      //  DataTable dtt;
        public static int qq;
       // string typeno = "";
        SqlConnection con3 =BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public Acc_Report()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                if (typeno == "")
                {
                    string xxx;
                    xxx = maskedTextBox1.Text.ToString();

                    string zzz;
                    zzz = maskedTextBox2.Text.ToString();
                    SqlDataAdapter da3 = new SqlDataAdapter("select [ref] ,[contr] ,[type],[date] ,[total] ,[count] ,[payed],[total_cost],[saleman] ,[req_no] ,[cust_no],[discount],[net_total] from sales_hdr where date between'" + xxx + "' and '" + zzz + "'", con3);
                    SqlDataAdapter da4 = new SqlDataAdapter("select sum(total),sum(total_cost),sum(discount),sum(net_total) from sales_hdr where date between'" + xxx + "' and '" + zzz + "'", con3);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3, "0");
                    da4.Fill(ds3, "1");
                    dataGridView1.DataSource = ds3.Tables["0"];
                    txt_total.Text = ds3.Tables[1].Rows[0][0].ToString();
                    txt_cost.Text = ds3.Tables[1].Rows[0][1].ToString();


                    textBox1.Text = ds3.Tables[1].Rows[0][2].ToString();
                    textBox2.Text = ds3.Tables[1].Rows[0][3].ToString();

                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    dtt = ds3.Tables["0"];
                }
                else
                {// and type = " + typeno + "
                    string xxx;
                    xxx = maskedTextBox1.Text.ToString();

                    string zzz;
                    zzz = maskedTextBox2.Text.ToString();
                    SqlDataAdapter da3 = new SqlDataAdapter("select [ref] ,[contr] ,[type],[date] ,[total] ,[count] ,[payed],[total_cost],[saleman] ,[req_no] ,[cust_no],[discount],[net_total] from sales_hdr where date between'" + xxx + "' and '" + zzz + "' and type = " + typeno + "", con3);
                    SqlDataAdapter da4 = new SqlDataAdapter("select sum(total),sum(total_cost),sum(discount),sum(net_total) from sales_hdr where date between'" + xxx + "' and '" + zzz + "' and type = " + typeno + "", con3);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3, "0");
                    da4.Fill(ds3, "1");
                    dataGridView1.DataSource = ds3.Tables["0"];
                    txt_total.Text = ds3.Tables[1].Rows[0][0].ToString();
                    txt_cost.Text = ds3.Tables[1].Rows[0][1].ToString();

                    textBox1.Text = ds3.Tables[1].Rows[0][2].ToString();
                    textBox2.Text = ds3.Tables[1].Rows[0][3].ToString();

                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    dtt = ds3.Tables["0"];

                }
                */
               // MessageBox.Show(datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt));
                if (!chk_notequal.Checked)
                {
                    string usrname = cmb_user.SelectedIndex != -1 ? " and usrid = '" + cmb_user.SelectedValue.ToString() + "' " : " ";
                    string cond = checkBox1.Checked ? "a_hdate" : "a_mdate";
                    string condp = rad_posted.Checked ? " and a_posted=1 " : (rad_notpostd.Checked ? " and a_posted=0 " : " ");

                    string condf = txt_fno.Text != "" ? txt_fno.Text : "1";
                    string condt = txt_tno.Text != "" ? txt_tno.Text : "999999999";
                   // string condft = " and " + (checkBox2.Checked ? "serial_no" : "a_ref") + " between " + condf + " and " + condt + " ";
                    string condft = checkBox2.Checked ? " and serial_no between '" + condf + "' and '" + condt + "' " : " and a_ref between " + condf + " and " + condt + " ";

                    string condfdt = cmb_exityear.Checked ? " > '" + datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt) + "' or " + cond + " < '" + datval.convert_to_yyyyDDdd(BL.CLS_Session.start_dt) + "'" : " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'";
                    //  string qstr = "select a_type a_type,a_ref a_ref,CONVERT(VARCHAR(10), CAST(a_mdate as date), 105) a_mdate,CONVERT(VARCHAR(10), CAST(a_hdate as date), 105) a_hdate,a_text a_text,a_amt a_amt,a_posted,a_type a_t from acc_hdr where jhsrc like 'Acc%' and a_brno='" + BL.CLS_Session.brno + "' " + condp + usrname + condft + " and " + cond + " between '" + txt_fdate.Text.Replace("-", "") + "' and '" + txt_tdate.Text.Replace("-", "") + "'";
                    string qstr = "select a_type a_type,a_ref a_ref,CONVERT(VARCHAR(10), CAST(a_mdate as date), 105) a_mdate,(substring(a_hdate,7,2) + '-' + substring(a_hdate,5,2)+'-'+substring(a_hdate,1,4)) a_hdate,a_text a_text,a_amt a_amt,a_posted,a_type a_t,jhsrc src,sl_no sl,pu_no pu   from acc_hdr where a_brno='" + BL.CLS_Session.brno + "' " + condp + usrname + condft + " and " + cond + condfdt + "";

                    if (!string.IsNullOrEmpty(txt_desc.Text))
                    {
                        qstr = qstr + " and a_text like '%" + txt_desc.Text + "%'";
                    }

                    if (!string.IsNullOrEmpty(txt_equl.Text))
                    {
                        //  MessageBox.Show(comboBox2.SelectedValue.ToString());
                        qstr = qstr + " and a_amt " + comboBox2.Text.Substring(0, 1) + txt_equl.Text;
                    }

                    dt = daml.SELECT_QUIRY_only_retrn_dt(qstr + " and a_type like '%" + cmb_type.SelectedValue + "%' order by a_mdate");

                    DataRow dr = dt.NewRow();
                    double sumopnd = 0;
                    foreach (DataRow dtr in dt.Rows)
                    {
                        sumopnd = sumopnd + Convert.ToDouble(dtr[5]);
                       
                    }

                    //dr[0] = "";
                   // dr[1] =0;
                    //dr[2] = "";
                    //dr[3] = "";
                    // dr[4] = "";
                    dr[4] = "الاجمالي";
                    dr[5] = Math.Round(sumopnd, 4);

                    dr[6] = true;
                    //dr[7] = "";
                    //dr[8] = "";
                    //dr[9] = "";
                    //dr[10] = "";
                    dt.Rows.Add(dr);

                    dataGridView1.DataSource = dt;


                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        if (Convert.ToBoolean(rw.Cells[6].Value) == false)
                            // rw.DefaultCellStyle.BackColor = Color.Thistle;
                            rw.DefaultCellStyle.ForeColor = Color.Red;
                        // rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02")? "س قبض" : "س صرف";
                        if (!string.IsNullOrEmpty(rw.Cells[1].Value.ToString()))
                        rw.Cells[0].Value = datval.validate_trtypes(rw.Cells[0].Value.ToString());
                    }

                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                    dataGridView1.ClearSelection();
                }
                else
                {
                    //string usrname = cmb_user.SelectedIndex != -1 ? " and usrid = '" + cmb_user.SelectedValue.ToString() + "' " : " ";
                    //string cond = checkBox1.Checked ? "a_hdate" : "a_mdate";
                    //string condp = rad_posted.Checked ? " and a_posted=1 " : (rad_notpostd.Checked ? " and a_posted=0 " : " ");

                    //string condf = txt_fno.Text != "" ? txt_fno.Text : "1";
                    //string condt = txt_tno.Text != "" ? txt_tno.Text : "999999999";
                    //string condft = " and a_ref between " + condf + " and " + condt + " ";
                    //string condfdt = cmb_exityear.Checked ? " > '" + datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt) + "' or " + cond + " < '" + datval.convert_to_yyyyDDdd(BL.CLS_Session.start_dt) + "'" : " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'";
                    //  string qstr = "select a_type a_type,a_ref a_ref,CONVERT(VARCHAR(10), CAST(a_mdate as date), 105) a_mdate,CONVERT(VARCHAR(10), CAST(a_hdate as date), 105) a_hdate,a_text a_text,a_amt a_amt,a_posted,a_type a_t from acc_hdr where jhsrc like 'Acc%' and a_brno='" + BL.CLS_Session.brno + "' " + condp + usrname + condft + " and " + cond + " between '" + txt_fdate.Text.Replace("-", "") + "' and '" + txt_tdate.Text.Replace("-", "") + "'";
                    string qstr = "select a.a_type a_type,a.a_ref a_ref,CONVERT(VARCHAR(10), CAST(a.a_mdate as date), 105) a_mdate,(substring(a.a_hdate,7,2) + '-' + substring(a.a_hdate,5,2)+'-'+substring(a.a_hdate,1,4)) a_hdate,a.a_text a_text,a.a_amt a_amt,a.a_posted,a.a_type a_t,a.jhsrc src,a.sl_no sl,a.pu_no pu from acc_hdr a join acc_dtl b on a.a_brno=b.a_brno and a.a_ref=b.a_ref and a.a_type=b.a_type and a.pu_no=b.pu_no and a.sl_no=b.sl_no where a.a_brno='" + BL.CLS_Session.brno + "' "
                                + " group by a.a_type,a.a_ref,a.a_mdate,a.a_hdate,a.a_text,a.a_amt,a.a_posted,a.jhsrc,a.sl_no,a.pu_no,a.a_sysdate "
                                + " HAVING (sum(b.a_damt)- sum(b.a_camt))<>0 "
                                + " order by a.a_sysdate ";
                    //if (!string.IsNullOrEmpty(txt_desc.Text))
                    //{
                    //    qstr = qstr + " and a_text like '%" + txt_desc.Text + "%'";
                    //}

                    //if (!string.IsNullOrEmpty(txt_equl.Text))
                    //{
                    //    //  MessageBox.Show(comboBox2.SelectedValue.ToString());
                    //    qstr = qstr + " and a_amt " + comboBox2.Text.Substring(0, 1) + txt_equl.Text;
                    //}

                    dt = daml.SELECT_QUIRY_only_retrn_dt(qstr);
                    DataRow dr = dt.NewRow();
                    double sumopnd = 0;
                    foreach (DataRow dtr in dt.Rows)
                    {
                        sumopnd = sumopnd + Convert.ToDouble(dtr[5]);

                    }

                   // dr[0] = "";
                  //  dr[1] = 0;
                    //dr[2] = "";
                    //dr[3] = "";
                   // dr[4] = "";
                    dr[4] = "الاجمالي";
                    dr[5] = Math.Round(sumopnd, 4);

                    dr[6] = true;
                    //dr[7] = "";
                    //dr[8] = "";
                    //dr[9] = "";
                    //dr[10] = "";
                    dt.Rows.Add(dr);

                    dataGridView1.DataSource = dt;


                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                       // if (Convert.ToBoolean(rw.Cells[6].Value) == false)
                            // rw.DefaultCellStyle.BackColor = Color.Thistle;
                        rw.DefaultCellStyle.BackColor = Color.LightCoral;
                      //  rw.DefaultCellStyle.ForeColor = Color.White;
                        // rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02")? "س قبض" : "س صرف";
                        if (!string.IsNullOrEmpty(rw.Cells[1].Value.ToString()))
                        rw.Cells[0].Value = datval.validate_trtypes(rw.Cells[0].Value.ToString());
                    }


                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                    dataGridView1.ClearSelection();


                }

               
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void SalesReport_Load(object sender, EventArgs e)
        {
            
            //comboBox1.DisplayMember = "Text";
            //comboBox1.ValueMember = "Value";

            //var items = new[] { new { Text = "قيد عام", Value = "01" }, new { Text = "سند قبض", Value = "02" }, new { Text = "سند صرف", Value = "03" } };
            //comboBox1.DataSource = items;
            //comboBox1.SelectedIndex = -1;
            string tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
            //   dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('04','05','14','15')");
            cmb_type.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no not in('24','26','33','34','35','45','46')");
            cmb_type.DisplayMember = BL.CLS_Session.lang.Equals("E") ? "tr_ename" : "tr_name";
            cmb_type.ValueMember = "tr_no";
            cmb_type.SelectedIndex = -1;

            dtusers = daml.SELECT_QUIRY_only_retrn_dt("select user_name,full_name from users");

            cmb_user.DataSource = dtusers;
            cmb_user.DisplayMember = "full_name";
            cmb_user.ValueMember = "user_name";
            cmb_user.SelectedIndex = -1;

            /*
            comboBox2.DisplayMember = "Text";
            comboBox2.ValueMember = "Value";
            var items2 = new[] { new { Text = ">", Value = ">" }, new { Text = "=", Value = "=" }, new { Text = "<", Value = "<" } };
            comboBox2.DataSource = items2;
            comboBox2.SelectedIndex = -1;
            */
            /*
            maskedTextBox1.Text = DateTime.Now.ToString("yyyy-MM-01") + "05:00:00";

            DateTime dt = new DateTime();

            dt=DateTime.Now.AddDays(1);

           // maskedTextBox2.Text = DateTime.Now.ToString("yyyy-MM-dd") + "05:00:00";
            maskedTextBox2.Text = dt.ToString("yyyy-MM-dd") + "05:00:00";
             * */
          //  string temy1 =
           // txt_fdate.Text = BL.CLS_Session.start_dt;
            txt_fdate.Text = DateTime.Now.AddSeconds((BL.CLS_Session.posatrtday) * -3600).ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            txt_tdate.Text = BL.CLS_Session.end_dt;
           // string temy2=
           // txt_tdate.Text = DateTime.Now.ToString("yyyy-12-31", new CultureInfo("en-US"));
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
                    XcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].Visible ==true? dataGridView1.Columns[i - 1].HeaderText : "";
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Visible ==true? dataGridView1.Rows[i].Cells[j].Value.ToString() : "";
                    }
                }
               // XcelApp.Cells[0, 0] = "123";
                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
             
           
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


                DataSet ds1 = new DataSet();




                rf.reportViewer1.LocalReport.DataSources.Clear();
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Acc.rpt.Acc_Report_rpt.rdlc";



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

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acc1"))
                {
                    Acc_Tran f4 = new Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(),"");
                    f4.MdiParent = ParentForm;
                    if(BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                    f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.ToString().StartsWith("Grd"))
                {
                    Acc_Tran_All f4 = new Acc_Tran_All(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(),"");
                    f4.MdiParent = ParentForm;
                   // if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acc2"))
                {
                    Acc_Tran_Q f4 = new Acc_Tran_Q(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                    f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acc3"))
                {
                    Acc_Tran_S f4 = new Acc_Tran_S(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                    f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acct"))
                {
                    Acc_Tax_Tran f4 = new Acc_Tax_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(),"");
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                    f4.Show();
                }
                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Acct2"))
                {
                    Acc.Acc_Tax_Tran2 f4 = new Acc.Acc_Tax_Tran2(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), "");
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Cus1"))
                {
                    Cus.Acc_Tran_Q f4 = new Cus.Acc_Tran_Q(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Cus2"))
                {
                    Sup.Acc_Tran f4 = new Sup.Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Sal1") || dataGridView1.CurrentRow.Cells["src"].Value.Equals("Pos"))
                {
                    if (BL.CLS_Session.cmp_type.StartsWith("a"))
                    {
                        Sal.Sal_Tran_D_TF f4 = new Sal.Sal_Tran_D_TF(dataGridView1.CurrentRow.Cells["sl"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        f4.MdiParent = ParentForm;
                        if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                            f4.Show();
                    }
                    else
                    {
                        Sal.Sal_Tran_D f4 = new Sal.Sal_Tran_D(dataGridView1.CurrentRow.Cells["sl"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        f4.MdiParent = ParentForm;
                        if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                            f4.Show();
                    }
                }
              
                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Sal2"))
                {
                    Sal.Sal_Tran_notax f4 = new Sal.Sal_Tran_notax(dataGridView1.CurrentRow.Cells["sl"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Sup1"))
                {
                    Sup.Acc_Tran_S f4 = new Sup.Acc_Tran_S(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Sup2"))
                {
                    Sup.Acc_Tran f4 = new Sup.Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Pur1"))
                {
                    Pur.Pur_Tran_D f4 = new Pur.Pur_Tran_D(dataGridView1.CurrentRow.Cells["pu"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Pur2"))
                {
                    Pur.Pur_Tran_notax f4 = new Pur.Pur_Tran_notax(dataGridView1.CurrentRow.Cells["pu"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }
                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Supc"))
                {
                    Sup.Acc_Tran_Cur f4 = new Sup.Acc_Tran_Cur(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }
             
            }
        }

        private void dataGridView1_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
           // SendKeys.Send("{F9}");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            Acc_Tran f4 = new Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
            f4.MdiParent = ParentForm;
            // ActiveMdiChild.WindowState = FormWindowState.Normal;
            f4.Show();
             */
            SendKeys.Send("{F9}");
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_type.SelectedIndex = -1;
            }

             
        }

        private void كشفحسابToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            Acc_Tran f4 = new Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
            f4.MdiParent = ParentForm;
            // ActiveMdiChild.WindowState = FormWindowState.Normal;
            f4.Show();
             */
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    // Add this
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    // Can leave these here - doesn't hurt
                    // dataGridView1.Rows[e.RowIndex].Selected = true;
                    dataGridView1.Focus();
                }
            }
            catch { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           // string temy1 =
            if (checkBox1.Checked)
            {
                txt_fdate.Text = DateTime.Now.ToString("01-01-yyyy", new CultureInfo("ar-SA", false));
                //  string temy2 =
                txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));
            }
            else
            {
                txt_fdate.Text = DateTime.Now.ToString("01-01-yyyy", new CultureInfo("en-US", false));
                //  string temy2 =
                txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            }
        }

        private void cmb_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_user.SelectedIndex = -1;
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

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                comboBox2.SelectedIndex = -1;
            }
            
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int testInt = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[6].Value) ? 1 : 0;
                datval.formate_dgv(this.dataGridView1, testInt);
            }
            catch { }
        }

        private void txt_fdate_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{Home}");
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
    }
}
