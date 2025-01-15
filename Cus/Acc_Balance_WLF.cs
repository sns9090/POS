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

namespace POS.Cus
{
    public partial class Acc_Balance_WLF : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dtacbal, dtusers, dtcity, dtmustp;
        public static int qq;
       // string typeno = "";
        string strt_dt;
        SqlConnection con3 =BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
       // sqlConnection
        public Acc_Balance_WLF()
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
                strt_dt = checkBox1.Checked ? DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false)) : DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            
                /*
                if (typeno == "")
                {
                    string xxx;
                    xxx = maskedTextBox1.Text.ToString();

                    string zzz;
                    zzz = maskedTextBox2.Text.ToString();
                    SqlDataAdapter da3 = new SqlDataAdapter("select [ref] ,[company] ,[type],[date] ,[total] ,[count] ,[payed],[total_cost],[saleman] ,[req_no] ,[cust_no],[discount],[net_total] from sales_hdr where date between'" + xxx + "' and '" + zzz + "'", con3);
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
                    SqlDataAdapter da3 = new SqlDataAdapter("select [ref] ,[company] ,[type],[date] ,[total] ,[count] ,[payed],[total_cost],[saleman] ,[req_no] ,[cust_no],[discount],[net_total] from sales_hdr where date between'" + xxx + "' and '" + zzz + "' and type = " + typeno + "", con3);
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
                /*
                string qstr = "select a.a_key as a,a.a_name as b,round(a.a_opnbal+isnull(sum(iif (b.jddbcr='D', b.a_camt, -b.a_damt)),0),2) as c from accounts a"
                                                    +" left outer join acc_dtl b on a.a_comp=b.a_comp and a.a_key=b.a_key and b.a_key<>'' where a.a_comp='01' and a.a_key like '0%'"
                                                    +" group by a.a_key,a.a_name,a.a_opnbal"
                                                    +" having a.a_opnbal + isnull(sum(iif(jddbcr='D',a_camt,-(a_damt))),0)<>0"
                                                    +" order by a.a_name";
                 * */
                /*
                string qstr ="select a.a_key as a,a.a_name as b,round(a.a_opnbal+isnull(sum(case b.jddbcr when 'D' then b.a_camt else -b.a_damt end),0),2) as c from accounts a"
                                                     +" left outer join acc_dtl b on a.a_comp=b.a_comp and a.a_key=b.a_key and b.a_key<>'' where a.a_comp='01' and a.a_key like '0%'"
                                                     +" group by a.a_key,a.a_name,a.a_opnbal"
                                                     +" having a.a_opnbal + isnull(sum(case b.jddbcr when 'D' then b.a_camt else -b.a_damt end),0)<>0"
                                                     +" order by a.a_name";
                 * */
                /*
                string qstr = "select a.a_key a,a.a_name b,round(a.a_opnbal+isnull(sum(case b.jddbcr when 'D' then b.a_camt else -b.a_damt end),0),2) c"
                           + " from accounts a left outer join acc_dtl b on a.a_comp=b.a_comp and a.a_key=b.a_key and a.glcurrency=b.jdcurr"
                           + " and b.a_key<>''"
                    // +" where a.glcomp='01' --and a.glopnbal<>0 --and b.jdcurr='' "
                           + " where a.a_comp='01' and a.a_key like '"+ textBox3.Text + "%'"
                           + " group by a.a_comp,a.a_key,a.a_name,a.a_opnbal"
                           + " having a.a_opnbal+isnull(sum(case b.jddbcr when 'D' then b.a_camt else -b.a_damt end),0)<>0"
                           + " order by a.a_name";
                 */
                /*
                string cond = checkBox1.Checked ? "b.a_hdate" : "b.a_mdate";
                string condc = cmb_class.SelectedIndex !=-1 ? " and a.cu_class='" + cmb_class.SelectedValue + "' " : " ";
                string qstr = "select a.cu_no d1,a.cu_name d2,round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.cu_no=b.cu_no and " + cond + "<='" + maskedTextBox2.Text.Replace("-", "") + "'),2) d3"
                            + " from customers a where round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.cu_no=b.cu_no and " + cond + "<='" + maskedTextBox2.Text.Replace("-", "") + "'),2) <>0 "
                            + " and cu_brno='" + BL.CLS_Session.brno + "'" + condc + " and a.cu_no like '%" + txt_cuno.Text + "%'"
                            + " order by a.cu_no;";
                 */
                string cond = checkBox1.Checked ? "b.a_hdate" : "b.a_mdate";
                string condc = cmb_class.SelectedIndex != -1 ? " and a.cu_class='" + cmb_class.SelectedValue + "' " : " ";
                string condcity = cmb_city.SelectedIndex != -1 ? " and a.cu_city='" + cmb_city.SelectedValue + "' " : " ";
                string condp = rad_posted.Checked ? " and c.a_posted=1 " : (rad_notpostd.Checked ? " and c.a_posted=0 " : " ");
                string qstr;

                if (!chk_openbalonly.Checked)
                {
                    qstr = "select a.cu_no d1,a.cu_name d2,round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and b.a_type not in ('04','14') " + condp + " where b.a_brno='" + BL.CLS_Session.brno + "' and a.cu_no=b.cu_no and " + cond + "<='" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'),4) d3"
                              + " ,0.00 d4,cu_pymnt d5,'' d6 from customers a where round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and b.a_type not in ('04','14') " + condp + " where b.a_brno='" + BL.CLS_Session.brno + "' and a.cu_no=b.cu_no and " + cond + "<='" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'),4) <>0 "
                              + " and cu_brno='" + BL.CLS_Session.brno + "'" + condc + condcity + " and a.cu_no like '%" + txt_cuno.Text + "%'"
                              + " order by cast(a.cu_no as int);";
                }
                else
                {
                    qstr = "select a.cu_no d1,a.cu_name d2,round(a.cu_opnbal ,4) d3,0.00 d4,cu_pymnt d5,'' d6"
                                 + " from customers a where round(a.cu_opnbal ,4) <>0 "
                                 + " and a.cu_brno='" + BL.CLS_Session.brno + "'" + " and a.cu_no like '%" + txt_cuno.Text + "%'"
                                 + " order by cast(a.cu_no as int);";
                }
                
                dtacbal=daml.SELECT_QUIRY_only_retrn_dt(qstr);

                foreach (DataRow dtr1 in dtacbal.Rows)
                {
                   // sum =sum + Convert.ToDouble(dtr[2]);
                   // sum1 = sum1 + Convert.ToDouble(dtr[3]);
                    dtmustp = daml.SELECT_QUIRY_only_retrn_dt("select round(a.cu_opnbal +(select isnull(sum(a_damt),0) from acc_dtl b where b.a_mdate <= DATEADD(day,-" + dtr1[4] + ",CAST('" + datval.convert_to_yyyyDDdd(strt_dt) + "' as date)) and a.cu_no=b.cu_no and a.cu_brno='" + BL.CLS_Session.brno + "' and b.a_type not in ('04','14')) - (select isnull(sum(a_camt),0) from acc_dtl b where a.cu_no=b.cu_no and a.cu_brno='" + BL.CLS_Session.brno + "' and b.a_type not in ('04','14')),4) from customers a where a.cu_no = '" + dtr1[0] + "'");
                    dtr1[3] = Convert.ToDouble(dtmustp.Rows[0][0])>0? dtmustp.Rows[0][0].ToString():"0.00";
                    dtr1[5] = "اكثر من" + " " + dtr1[4] +" " +"يوم";
                  //  dtr1[5] =  dtr1[4] + " " + "يوم";
                }
               // dtacbal.Rows.Add(row);
                DataRow dr = dtacbal.NewRow();
                double sum=0,sum1=0;
                foreach (DataRow dtr in dtacbal.Rows)
                {
                    sum =sum + Convert.ToDouble(dtr[2]);
                    sum1 = sum1 + Convert.ToDouble(dtr[3]);
                  //  dtmustp = daml.SELECT_QUIRY_only_retrn_dt("select round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where b.a_mdate <= DATEADD(day,-" + dth.Rows[0][3].ToString() + ",CAST('" + datval.convert_to_yyyyDDdd(strt_dt) + "' as date)) and a.cu_no=b.cu_no and a.cu_brno='" + BL.CLS_Session.brno + "' and b.a_type not in ('04','14')),2) from customers a where a.cu_no = '" + aakey + "'");
                  //  dtr[] = dtmustp.Rows[0][0].ToString();
                }
                dr[0] = "";
                dr[1] = "الاجمالي";
                dr[2] = sum;
                dr[3] = sum1;

                dtacbal.Rows.Add(dr);

                dataGridView1.DataSource = dtacbal;

               // dataGridView1.Rows.Add();
                //dataGridView1.Columns[1].Width = 500;
                //dataGridView1.Columns[2].Width = 200;
                //dataGridView1.Columns[0].Width = 200;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                dataGridView1.ClearSelection();


                for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                {
                    if (chk_m.Checked && Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) > 0)
                    {
                        dataGridView1.Rows.RemoveAt(i);
                    }

                    if (chk_d.Checked && Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) < 0)
                    {
                        dataGridView1.Rows.RemoveAt(i);
                    }
                }
                

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SalesReport_Load(object sender, EventArgs e)
        {
            /*
            maskedTextBox1.Text = DateTime.Now.ToString("yyyy-MM-01") + "05:00:00";

            DateTime dt = new DateTime();

            dt=DateTime.Now.AddDays(1);

           // maskedTextBox2.Text = DateTime.Now.ToString("yyyy-MM-dd") + "05:00:00";
            maskedTextBox2.Text = dt.ToString("yyyy-MM-dd") + "05:00:00";
             * */
            string temy = BL.CLS_Session.end_dt;// DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            txt_tdate.Text = temy;

            dtusers = daml.SELECT_QUIRY_only_retrn_dt("select cl_no,cl_desc from cuclass where cl_brno='"+BL.CLS_Session.brno+"'");

            cmb_class.DataSource = dtusers;
            cmb_class.DisplayMember = "cl_desc";
            cmb_class.ValueMember = "cl_no";
            cmb_class.SelectedIndex = -1;

            dtcity = daml.SELECT_QUIRY_only_retrn_dt("select city_id,city_name from cites");

            cmb_city.DataSource = dtcity;
            cmb_city.DisplayMember = "city_name";
            cmb_city.ValueMember = "city_id";
            cmb_city.SelectedIndex = -1;
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
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("details", dtacbal));
              //  rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("details", dtdtl));

                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Cus.rpt.Cus_Balance_rpt.rdlc";

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[4];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
              //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                parameters[1] = new ReportParameter("mdate", txt_tdate.Text);
                parameters[2] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[3] = new ReportParameter("desc", "ارصدة العملاء حتى التاريخ الموضح");

                rf.reportViewer1.LocalReport.SetParameters(parameters);

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();
            }
            catch { }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                Acc_Statment_Exp f4a = new Acc_Statment_Exp(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                f4a.MdiParent = MdiParent;
                f4a.Show();
            }

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
            {
                Cus.Customers ac = new Cus.Customers(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                ac.MdiParent = ParentForm;
                ac.Show();
            }
        }

        private void dataGridView1_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Acc_Statment f4a = new Acc_Statment(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            //f4a.Show();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {/*
            Acc_Statment_Exp f4a = new Acc_Statment_Exp(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            f4a.MdiParent = MdiParent;
            f4a.Show();
          */
        }

        private void كشفحسابToolStripMenuItem_Click(object sender, EventArgs e)
        {/*
            Acc_Statment_Exp f4a = new Acc_Statment_Exp(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            f4a.MdiParent = MdiParent;
            f4a.Show();
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
            if (checkBox1.Checked)
            {
              //  maskedTextBox1.Text = DateTime.Now.ToString("yyyy-01-01", new CultureInfo("ar-SA", false));
                //  string temy2 =
                txt_tdate.Text = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("ar-SA", false));
            }
            else
            {
              //  maskedTextBox1.Text = DateTime.Now.ToString("yyyy-01-01", new CultureInfo("en-US", false));
                //  string temy2 =
                txt_tdate.Text = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US", false));
            }
        }

        private void cmb_class_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_class.SelectedIndex = -1;
            }
        }

        private void txt_cuno_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmb_city_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_city.SelectedIndex = -1;
            }
        }

        private void chk_m_CheckedChanged(object sender, EventArgs e)
        {

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
    }
}
