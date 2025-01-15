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

namespace POS.Sal
{
    public partial class Sal_Qst_Balance : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dtacbal, dtusers, dtcur;
        public static int qq;
       // string typeno = "";
        SqlConnection con3 =BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
       // sqlConnection
        public Sal_Qst_Balance()
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
                string condc = cmb_class.SelectedIndex !=-1 ? " and a.su_class='" + cmb_class.SelectedValue + "' " : " ";
                string qstr = "select a.su_no d1,a.su_name d2,round(a.su_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.su_no=b.su_no and " + cond + "<='" + maskedTextBox2.Text.Replace("-", "") + "'),2) d3"
                            + " from customers a where round(a.su_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.su_no=b.su_no and " + cond + "<='" + maskedTextBox2.Text.Replace("-", "") + "'),2) <>0 "
                            + " and su_brno='" + BL.CLS_Session.brno + "'" + condc + " and a.su_no like '%" + txt_cuno.Text + "%'"
                            + " order by a.su_no;";
                 */
              //  string condft = (!string.IsNullOrEmpty(txt_cuno.Text) && !string.IsNullOrEmpty(txt_tosup.Text)) ? " and a.su_no between '" + txt_cuno.Text + "' and '" + txt_tosup.Text + "' " : "";

                string qstr;
                //if (cmb_cur.SelectedIndex == -1)
                //{
                    string cond = checkBox1.Checked ? "b.a_hdate" : "b.a_mdate";
                    string condc = cmb_salctr.SelectedIndex != -1 ? " and a.slcenter='" + cmb_salctr.SelectedValue + "' " : " ";
                    string condp = rad_posted.Checked ? " and a.posted=1 " : (rad_notpostd.Checked ? " and a.posted=0 " : " ");
                    string condf = txt_cuno.Text != "" ? txt_cuno.Text : "1";
                    string condt = txt_tosup.Text != "" ? txt_tosup.Text : "999999999";
                    string condft = " and a.ref between " + condf + " and " + condt + " ";

                    //if (!chk_openbalonly.Checked)
                    //{
                    qstr = "select a.slcenter d0,cast(a.ref as varchar) d1," + (checkBox1.Checked ? " (substring(a.invhdate,7,2) + '-' + substring(a.invhdate,5,2)+'-'+substring(a.invhdate,1,4)) " : " CONVERT(VARCHAR(10),CAST(a.invmdate as date), 105) ") + " dt,a.text d2,a.nettotal d3,isnull(a.invpaid,0) d4,round((select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.pu_no=b.pu_no and b.a_type in ('02')  where b.a_brno='" + BL.CLS_Session.brno + "' and a.branch=b.a_brno and a.slcenter=b.qst_sl and a.ref=b.qst_ref and b.a_mdate<='" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'),2)*-1 d5"
                                                       + ",0.00 d6,a.posted a_p,a.glser src from sales_hdr a where a.invpaid is not null and a.invpaid<>0 and (a.nettotal -  isnull(a.invpaid,0))<>0 "
                        //  + " and su_brno='" + BL.CLS_Session.brno + "'" + condc + " and a.su_no like '%" + txt_cuno.Text + "%'"
                               + " and a.branch='" + BL.CLS_Session.brno + "'" + condc + condft + ""
                               + " order by cast(a.ref as int);";
                    //}
                    //else
                    //{
                        //qstr = "select a.su_no d1,a.su_name d2,round(a.su_opnbal,2) d3"
                        //               + " from suppliers a where round(a.su_opnbal,2) <>0 "
                        //             //  + " and a.su_brno='" + BL.CLS_Session.brno + "'" + " and a.su_no like '%" + txt_cuno.Text + "%'"
                        //               + " and su_brno='" + BL.CLS_Session.brno + "'" + condc + condft + ""
                        //               + " order by cast(a.su_no as int);";

                    //}
                //}
                //else
                //{
                //    string cond = checkBox1.Checked ? "b.a_hdate" : "b.a_mdate";
                //    string condc = cmb_class.SelectedIndex != -1 ? " and a.su_class='" + cmb_class.SelectedValue + "' " : " ";
                //    string condp = rad_posted.Checked ? " and c.a_posted=1 " : (rad_notpostd.Checked ? " and c.a_posted=0 " : " ");



                //    if (!chk_openbalonly.Checked)
                //    {
                //        qstr = "select a.su_no d1,a.su_name d2,round(a.su_opnfcy +(select isnull(sum((case when b.jddbcr='D' then b.jdfcamt else -b.jdfcamt end)),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.pu_no=b.pu_no and b.a_type not in ('06','16') " + condp + " where b.a_brno='" + BL.CLS_Session.brno + "' and a.su_no=b.su_no and " + cond + "<='" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'),2) d3"
                //                   + " from suppliers a  where round(a.su_opnfcy +(select isnull(sum((case when b.jddbcr='D' then b.jdfcamt else -b.jdfcamt end)),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.pu_no=b.pu_no and b.a_type not in ('06','16') " + condp + " where b.a_brno='" + BL.CLS_Session.brno + "' and a.su_no=b.su_no and " + cond + "<='" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'),2) <>0 "
                //                  // + " and a.su_cur='"+cmb_cur.SelectedValue+"' and a.su_brno='" + BL.CLS_Session.brno + "'" + condc + " and a.su_no like '%" + txt_cuno.Text + "%'"
                //                   + " and a.su_cur='" + cmb_cur.SelectedValue + "' and a.su_brno='" + BL.CLS_Session.brno + "'" + condc + condft + ""
                //                   + " order by cast(a.su_no as int);";
                //    }
                //    else
                //    {
                //        qstr = "select a.su_no d1,a.su_name d2,round(a.su_opnfcy,2) d3"
                //                       + " from suppliers a where round(a.su_opnfcy,2) <>0 "
                //                      // + " and a.su_cur='" + cmb_cur.SelectedValue + "' and a.su_brno='" + BL.CLS_Session.brno + "'" + " and a.su_no like '%" + txt_cuno.Text + "%'"
                //                       + " and a.su_cur='" + cmb_cur.SelectedValue + "' and a.su_brno='" + BL.CLS_Session.brno + "'" + condc + condft + ""
                //                       + " order by cast(a.su_no as int);";

                //    }
                
                
                //}
                
                dtacbal=daml.SELECT_QUIRY_only_retrn_dt(qstr);

               // dtacbal.Rows.Add(row);
                DataRow dr = dtacbal.NewRow();
                double sum=0,sum1=0,sum2=0,sum3=0;
                foreach (DataRow dtr in dtacbal.Rows)
                {
                    sum =sum + Convert.ToDouble(dtr[4]);
                    sum1 = sum1 + Convert.ToDouble(dtr[5]);
                    sum2 = sum2 + Convert.ToDouble(dtr[6]);
                    //sum3 = sum3 + Convert.ToDouble(dtr[7]);
                }
               // dr[1] = 0;
                dr[3] = "الاجمالي";
                dr[4] = sum;
                dr[5] = sum1;
                dr[6] = sum2;
                dr[8] = true;

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
                    if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["a_p"].Value) == false)
                        // rw.DefaultCellStyle.BackColor = Color.Thistle;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;

                    dataGridView1.Rows[i].Cells["d6"].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells["d3"].Value) - Convert.ToDouble(dataGridView1.Rows[i].Cells["d4"].Value) - Convert.ToDouble(dataGridView1.Rows[i].Cells["d5"].Value);
                    //if (chk_m.Checked && Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) > 0 )
                    //{
                    //    dataGridView1.Rows.RemoveAt(i);
                    //}

                    //if (chk_d.Checked && Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) < 0)
                    //{
                    //    dataGridView1.Rows.RemoveAt(i);
                    //}
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SalesReport_Load(object sender, EventArgs e)
        {
            if (BL.CLS_Session.islight)
            {
                button5.Visible = false;
            }
            /*
            maskedTextBox1.Text = DateTime.Now.ToString("yyyy-MM-01") + "05:00:00";

            DateTime dt = new DateTime();

            dt=DateTime.Now.AddDays(1);

           // maskedTextBox2.Text = DateTime.Now.ToString("yyyy-MM-dd") + "05:00:00";
            maskedTextBox2.Text = dt.ToString("yyyy-MM-dd") + "05:00:00";
             * */
           // string  temy = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            txt_tdate.Text = BL.CLS_Session.end_dt;// DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US",false));
            string sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";
            cmb_salctr.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select * from slcenters where sl_brno + sl_no in(" + sl + ") and sl_brno='" + BL.CLS_Session.brno + "'");
            cmb_salctr.DisplayMember = "sl_name";
            cmb_salctr.ValueMember = "sl_no";
            cmb_salctr.SelectedIndex = -1;

            //dtcur = daml.SELECT_QUIRY_only_retrn_dt("select curcode,curname,curlname,currate from Crncy order by curcode");
            //cmb_cur.DataSource = dtcur;
            //cmb_cur.DisplayMember = "curname";
            //cmb_cur.ValueMember = "curcode";
            //cmb_cur.SelectedIndex = -1;
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
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    dt.Rows.Add(dRow);
                }




                rf.reportViewer1.LocalReport.DataSources.Clear();
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
              //  rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("details", dtdtl));

                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sal.rpt.Sal_Qst_Report_rpt.rdlc";

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
               // parameters[1] = new ReportParameter("fmdate", txt_fdate.Text);
                parameters[1] = new ReportParameter("tmdate", txt_tdate.Text);
                parameters[2] = new ReportParameter("br_name", BL.CLS_Session.brname);

                rf.reportViewer1.LocalReport.SetParameters(parameters);

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
                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Sal1") || dataGridView1.CurrentRow.Cells["src"].Value.Equals("Pos"))
                {
                    if (BL.CLS_Session.cmp_type.StartsWith("a"))
                    {
                        Sal.Sal_Tran_D_TF f4 = new Sal.Sal_Tran_D_TF(dataGridView1.CurrentRow.Cells[0].Value.ToString(), "04", dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        f4.MdiParent = ParentForm;
                        if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                            f4.Show();
                    }
                    else
                    {
                        Sal.Sal_Tran_D f4 = new Sal.Sal_Tran_D(dataGridView1.CurrentRow.Cells[0].Value.ToString(), "04", dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        f4.MdiParent = ParentForm;
                        if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                            f4.Show();
                    }
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Sal2"))
                {
                    Sal.Sal_Tran_notax f4 = new Sal.Sal_Tran_notax(dataGridView1.CurrentRow.Cells[0].Value.ToString(), "04", dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }


            }
            if (e.KeyCode == Keys.F7)
            {
                Sal.Sal_Qst_Statment_Exp f4a = new Sal.Sal_Qst_Statment_Exp(dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[0].Value.ToString());
                f4a.MdiParent = MdiParent;
                f4a.Show();
            }

            //if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
            //{
            //    Sup.Suppliers ac = new Sup.Suppliers(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            //    ac.MdiParent = ParentForm;
            //    ac.Show();
            //}
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
            SendKeys.Send("{F9}");
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
                txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));
            }
            else
            {
              //  maskedTextBox1.Text = DateTime.Now.ToString("yyyy-01-01", new CultureInfo("en-US", false));
                //  string temy2 =
                txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            }
        }

        private void cmb_class_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete)
            //{
            //    cmb_class.SelectedIndex = -1;
            //}
        }

        private void txt_cuno_KeyDown(object sender, KeyEventArgs e)
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

        private void cmb_cur_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_cur.SelectedIndex = -1;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //DialogResult result = MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Do you want to print" : "هل تريد فعلا طباعة كشوفات تفصيلية لكل الحسابات المعروضة بالتقرير", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            //if (result == DialogResult.Yes)
            //{
            //    ProgressFrm pf = new ProgressFrm();
            //    pf.progressBar1.Minimum = 1;
            //    pf.progressBar1.Maximum = dataGridView1.RowCount;
            //    pf.Show();
            //    foreach (DataGridViewRow dtr in dataGridView1.Rows)
            //    {
            //        pf.progressBar1.Value = dtr.Index + 1;
            //      //  Acc_Statment_Exp ex = new Acc_Statment_Exp(dtr.Cells[0].Value.ToString());
            //        ex.SalesReport_Load(sender, e);
            //        ex.btn_prtdirct_Click(sender, e);
            //       // pf.progressBar1.Value = dtr.Index + 1;
            //    }
            //    // MessageBox.Show("ok");
            //    pf.Close();
            //}
            //else if (result == DialogResult.No)
            //{
            //    //...
            //}
            //else
            //{
            //    //...
            //}
        }

        private void cmb_salctr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_salctr.SelectedIndex = -1;
            }
        }
    }
}
