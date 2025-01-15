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
    public partial class Acc_Balance : BaseForm
    {
        List<BL.CurrencyInfo> currencies = new List<BL.CurrencyInfo>();
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dtacbal, dtusers, dtcity, dtmustp,dtprt;
        public static int qq;
       // string typeno = "";
        string strt_dt;
        SqlConnection con3 =BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
       // sqlConnection
        public Acc_Balance()
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
                string condcs = chk_cs.Checked ? " a.cu_no=b.cu_no or (a.whno=b.su_no and ltrim(rtrim(b.su_no))<>'') " : " a.cu_no=b.cu_no ";
                string condcso = chk_cs.Checked ? " (a.cu_opnbal+isnull(s.su_opnbal,0)) " : " a.cu_opnbal ";
                string condcsoj = chk_cs.Checked ? " left join suppliers s on a.whno=s.su_no and a.cu_brno=s.su_brno " : "  ";

                string cond = checkBox1.Checked ? "b.a_hdate" : "b.a_mdate";
                string condc = cmb_class.SelectedIndex != -1 ? " and a.cu_class='" + cmb_class.SelectedValue + "' " : " ";
                string condcity = cmb_city.SelectedIndex != -1 ? " and a.cu_city='" + cmb_city.SelectedValue + "' " : " ";
                string condp = rad_posted.Checked ? " and c.a_posted=1 " : (rad_notpostd.Checked ? " and c.a_posted=0 " : " ");
                string qstr;
                string condft = (!string.IsNullOrEmpty(txt_cuno.Text) && !string.IsNullOrEmpty(txt_tosup.Text)) ? " and a.cu_no between '" + txt_cuno.Text + "' and '" + txt_tosup.Text + "' " : "";

                if (!chk_openbalonly.Checked)
                {
                    //qstr = "select a.cu_no d1,a.cu_name d2,round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and b.a_type not in ('04','14') " + condp + " where b.a_brno='" + BL.CLS_Session.brno + "' and a.cu_no=b.cu_no and " + cond + "<='" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'),2) d3"
                    //          + " ,a.cu_cntactp d4,a.cu_tel d5,'' d6 from customers a where round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and b.a_type not in ('04','14') " + condp + " where b.a_brno='" + BL.CLS_Session.brno + "' and a.cu_no=b.cu_no and " + cond + "<='" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'),2) <>0 "
                    //        //  + " and cu_brno='" + BL.CLS_Session.brno + "'" + condc + condcity + " and a.cu_no like '%" + txt_cuno.Text + "%'"
                    //         + " and cu_brno='" + BL.CLS_Session.brno + "'" + condc + condcity + condft + ""
                    //          + " order by cast(a.cu_no as int);";
                    qstr = "select a.cu_no d1,a.cu_name d2,(select " + condcso + " + round((select isnull(sum(a_damt-a_camt),0)),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and b.a_type not in ('04','14','06','16') " + condp + " where b.a_brno='" + BL.CLS_Session.brno + "' and (" + condcs + ") and " + cond + "<='" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "') d3"
                            + " ,a.cu_cntactp d4,a.cu_tel d5,'' d6,a.lastupdt mdate from customers a " + condcsoj + " where " + (chk_zerobl.Checked ? " 1=1 " : " (select " + condcso + " + round((select isnull(sum(a_damt-a_camt),0)),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and b.a_type not in ('04','14','06','16') " + condp + " where b.a_brno='" + BL.CLS_Session.brno + "' and (" + condcs + ") and " + cond + "<='" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "') <>0 ")
                        //  + " and cu_brno='" + BL.CLS_Session.brno + "'" + condc + condcity + " and a.cu_no like '%" + txt_cuno.Text + "%'"
                           + " and cu_brno='" + BL.CLS_Session.brno + "'" + condc + condcity + condft + ""
                            + " order by cast(a.cu_no as int);";
                }
                else
                {
                    //qstr = "select a.cu_no d1,a.cu_name d2,round(a.cu_opnbal ,2) d3,a.cu_cntactp d4,a.cu_tel d5,'' d6"
                    //             + " from customers a where round(a.cu_opnbal ,2) <>0 "
                    //           //  + " and a.cu_brno='" + BL.CLS_Session.brno + "'" + " and a.cu_no like '%" + txt_cuno.Text + "%'"
                    //             + " and a.cu_brno='" + BL.CLS_Session.brno + "'" + condc + condcity + condft + ""
                    //             + " order by cast(a.cu_no as int);";
                    qstr = "select a.cu_no d1,a.cu_name d2,round(" + condcso + " ,4) d3,a.cu_cntactp d4,a.cu_tel d5,'' d6,a.lastupdt mdate "
                                 + " from customers a  " + condcsoj + " where " + (chk_zerobl.Checked ? " 1=1 " :" round(" + condcso + " ,4) <>0 ")
                        //  + " and a.cu_brno='" + BL.CLS_Session.brno + "'" + " and a.cu_no like '%" + txt_cuno.Text + "%'"
                                 + " and a.cu_brno='" + BL.CLS_Session.brno + "'" + condc + condcity + condft + ""
                                 + " order by cast(a.cu_no as int);";
                }
                
                dtacbal=daml.SELECT_QUIRY_only_retrn_dt(qstr);
                for (int i = dtacbal.Rows.Count - 1; i >= 0; i--)
                {
                    if (chk_m.Checked && Convert.ToDouble(dtacbal.Rows[i][2]) > 0)
                    {
                        dtacbal.Rows.RemoveAt(i);
                    }

                    if (chk_d.Checked && Convert.ToDouble(dtacbal.Rows[i][2]) < 0)
                    {
                        dtacbal.Rows.RemoveAt(i);
                    }
                }

                dtprt = dtacbal.Copy();
                ////////foreach (DataRow dtr1 in dtacbal.Rows)
                ////////{
                ////////    // sum =sum + Convert.ToDouble(dtr[2]);
                ////////    // sum1 = sum1 + Convert.ToDouble(dtr[3]);
                ////////    dtmustp = daml.SELECT_QUIRY_only_retrn_dt("select round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where b.a_mdate <= DATEADD(day,-" + dtr1[4] + ",CAST('" + datval.convert_to_yyyyDDdd(strt_dt) + "' as date)) and a.cu_no=b.cu_no and a.cu_brno='" + BL.CLS_Session.brno + "' and b.a_type not in ('04','14')),2) from customers a where a.cu_no = '" + dtr1[0] + "'");
                ////////    dtr1[3] = dtmustp.Rows[0][0].ToString();
                ////////}
               // dtacbal.Rows.Add(row);
                DataRow dr = dtacbal.NewRow();
                double sum=0,sum1=0;
                foreach (DataRow dtr in dtacbal.Rows)
                {
                    sum =sum + Convert.ToDouble(dtr[2]);
                   // sum1 = sum1 + Convert.ToDouble(dtr[3]);
                  //  dtmustp = daml.SELECT_QUIRY_only_retrn_dt("select round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where b.a_mdate <= DATEADD(day,-" + dth.Rows[0][3].ToString() + ",CAST('" + datval.convert_to_yyyyDDdd(strt_dt) + "' as date)) and a.cu_no=b.cu_no and a.cu_brno='" + BL.CLS_Session.brno + "' and b.a_type not in ('04','14')),2) from customers a where a.cu_no = '" + aakey + "'");
                  //  dtr[] = dtmustp.Rows[0][0].ToString();
                  //  BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(Convert.ToDouble(dtr[5]) < 0 ? Convert.ToDouble(dtr[5]) * -1 : Convert.ToDouble(dtr[5])), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);
                  
                }

                foreach (DataRow dtr2 in dtprt.Rows)
                {
                   // sum = sum + Convert.ToDouble(dtr[2]);
                    // sum1 = sum1 + Convert.ToDouble(dtr[3]);
                    //  dtmustp = daml.SELECT_QUIRY_only_retrn_dt("select round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where b.a_mdate <= DATEADD(day,-" + dth.Rows[0][3].ToString() + ",CAST('" + datval.convert_to_yyyyDDdd(strt_dt) + "' as date)) and a.cu_no=b.cu_no and a.cu_brno='" + BL.CLS_Session.brno + "' and b.a_type not in ('04','14')),2) from customers a where a.cu_no = '" + aakey + "'");
                    //  dtr[] = dtmustp.Rows[0][0].ToString();
                    BL.ToWord toWord2 = new BL.ToWord(Convert.ToDecimal(Convert.ToDouble(dtr2[2]) < 0 ? Convert.ToDouble(dtr2[2]) * -1 : Convert.ToDouble(dtr2[2])), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);
                    dtr2[5] = BL.CLS_Session.lang.Equals("ع") ? toWord2.ConvertToArabic() : toWord2.ConvertToEnglish();

                }
                dr[0] = "";
                dr[1] = "الاجمالي";
                dr[2] = sum; dr[3] = "";
                ////////dr[3] = sum1;
                dr[4] = "";
                dr[5] = "";
                dr[6] = DateTime.Now.ToString("yyyyMMdd" ,new CultureInfo("en-US", false));

                dtacbal.Rows.Add(dr);


                dataGridView1.DataSource = dtacbal;

                if (BL.CLS_Session.sysno.ToUpper().Equals("T"))
                {
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        if (Convert.ToDouble(datval.convert_to_yyyyDDdd(Convert.ToDateTime((dgvr.Cells[6].Value.ToString().Substring(4, 2) + "/" + dgvr.Cells[6].Value.ToString().Substring(6, 2) + "/" + dgvr.Cells[6].Value.ToString().Substring(0, 4)), new CultureInfo("en-US", false)).AddDays(365).ToString())) < Convert.ToDouble(DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false))) && dgvr.Index < dataGridView1.Rows.Count - 1)
                        {
                            // MessageBox.Show(" العميل خارج الصيانة ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                          //  MessageBox.Show(Convert.ToDateTime((dgvr.Cells[6].Value.ToString().Substring(4, 2) + "/" + dgvr.Cells[6].Value.ToString().Substring(6, 2) + "/" + dgvr.Cells[6].Value.ToString().Substring(0, 4)), new CultureInfo("en-US", false)).AddDays(365).ToString());
                            dgvr.DefaultCellStyle.BackColor = Color.Goldenrod;
                        }
                        else
                            dgvr.DefaultCellStyle.BackColor = Color.White;
                    }
                }

               // dataGridView1.Rows.Add();
                //dataGridView1.Columns[1].Width = 500;
                //dataGridView1.Columns[2].Width = 200;
                //dataGridView1.Columns[0].Width = 200;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                dataGridView1.ClearSelection();


                
                

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SalesReport_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.TsIcon;
            if (BL.CLS_Session.islight)
            {
                button5.Visible = false;
                button4.Visible = false;
            }

            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Syria));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.UAE));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.SaudiArabia));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Tunisia));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Gold));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Yemen));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.nul));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Kuwait));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Qatar));
            /*
            maskedTextBox1.Text = DateTime.Now.ToString("yyyy-MM-01") + "05:00:00";

            DateTime dt = new DateTime();

            dt=DateTime.Now.AddDays(1);

           // maskedTextBox2.Text = DateTime.Now.ToString("yyyy-MM-dd") + "05:00:00";
            maskedTextBox2.Text = dt.ToString("yyyy-MM-dd") + "05:00:00";
             * */
           // string temy = BL.CLS_Session.end_dt;// DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
           // txt_tdate.Text = temy;
            txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US"));

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
                            // XcelApp.Cells[i + 6, j + 1] = "'" + dataGridView1.Rows[i].Cells[j].Value.ToString();
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
                workSheet.Cells[1, "B"] = BL.CLS_Session.cmp_name;
                workSheet.Cells[2, "B"] = BL.CLS_Session.brname;
                workSheet.Cells[3, "B"] = this.Text;

                workSheet.Range["A1", "C1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                workSheet.Range["A2", "C2"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                workSheet.Range["A3", "C3"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                workSheet.Range["A" + (dataGridView1.Rows.Count + 5).ToString(), "E" + (dataGridView1.Rows.Count + 5).ToString()].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                workSheet.Range["A5", "E5"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                // workSheet.Cells[2, "D"]

                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
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

                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Cus.rpt.Cust_Balance_rpt.rdlc";
              //  rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Acc.rpt.Acc_Balance_rpt.rdlc";

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[4];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
              //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                parameters[1] = new ReportParameter("mdate", txt_tdate.Text);
                parameters[2] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[3] = new ReportParameter("desc", "ارصدة العملاء حتى التاريخ الموضح");
               // parameters[4] = new ReportParameter("ttl", "ارصدة العملاء");
               // parameters[5] = new ReportParameter("nm", "اسم العميل");
              //  parameters[6] = new ReportParameter("no", "رقم العميل");
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

        private void button4_Click(object sender, EventArgs e)
        {
        //     qstr = "select a.cu_no d1,a.cu_name d2,round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and b.a_type not in ('04','14') " + condp + " where a.cu_no=b.cu_no and " + cond + "<='" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'),2) d3"
        //                      + " ,a.cu_cntactp d4,a.cu_tel d5 from customers a where round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and b.a_type not in ('04','14') " + condp + " where a.cu_no=b.cu_no and " + cond + "<='" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'),2) <>0 "
        //                      + " and cu_brno='" + BL.CLS_Session.brno + "'" + condc + condcity + " and a.cu_no like '%" + txt_cuno.Text + "%'"
        //                      + " order by cast(a.cu_no as int);";
        //        }
        //        else
        //        {
        //            qstr = "select a.cu_no d1,a.cu_name d2,round(a.cu_opnbal ,2) d3,a.cu_cntactp d4,a.cu_tel d5"
        //                         + " from customers a where round(a.cu_opnbal ,2) <>0 "
        //                         + " and a.cu_brno='" + BL.CLS_Session.brno + "'" + " and a.cu_no like '%" + txt_cuno.Text + "%'"
        //                         + " order by cast(a.cu_no as int);";
        //        }
                
        //        dtacbal=daml.SELECT_QUIRY_only_retrn_dt(qstr);
            /*
            string cond = checkBox1.Checked ? "b.a_hdate" : "b.a_mdate";
            string condc = cmb_class.SelectedIndex != -1 ? " and a.cu_class='" + cmb_class.SelectedValue + "' " : " ";
            string condcity = cmb_city.SelectedIndex != -1 ? " and a.cu_city='" + cmb_city.SelectedValue + "' " : " ";
            string condp = rad_posted.Checked ? " and c.a_posted=1 " : (rad_notpostd.Checked ? " and c.a_posted=0 " : " ");
            DataTable dt01 = daml.SELECT_QUIRY_only_retrn_dt("select a.cu_no c1,a.cu_name c2,round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and b.a_type not in ('04','14') " + condp + " where a.cu_no=b.cu_no and " + cond + "<='" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'),2) c3"
                              + " from customers a where round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and b.a_type not in ('04','14') " + condp + " where a.cu_no=b.cu_no and " + cond + "<='" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'),2) <>0 "
                              + " and cu_brno='" + BL.CLS_Session.brno + "'" + condc + condcity + " and a.cu_no like '%" + txt_cuno.Text + "%'"
                              + " order by cast(a.cu_no as int);");

            Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();


            DataSet ds1 = new DataSet();




            rf.reportViewer1.LocalReport.DataSources.Clear();
            rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt01));
             */
            Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();


            DataSet ds1 = new DataSet();

           // dtprt.Rows.RemoveAt(dtprt.Rows.Count);


            rf.reportViewer1.LocalReport.DataSources.Clear();
            rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("details", dtprt));
            //  rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("details", dtdtl));

            if (File.Exists(Directory.GetCurrentDirectory() + @"\reports\Cust_Balance_Dtl3_rpt.rdlc"))
                rf.reportViewer1.LocalReport.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Cust_Balance_Dtl3_rpt.rdlc";
            else
                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Cus.rpt.Cust_Balance_Dtl3_rpt.rdlc";

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

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Do you want to print" : "هل تريد فعلا طباعة كشوفات تفصيلية لكل الحسابات المعروضة بالتقرير", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                ProgressFrm pf = new ProgressFrm();
                pf.progressBar1.Minimum = 1;
                pf.progressBar1.Maximum = dataGridView1.RowCount;
                pf.Show();
                foreach (DataGridViewRow dtr in dataGridView1.Rows)
                {
                   // pf.progressBar1.Value = dtr.Index + 1;
                   // Acc_Statment_Exp ex = new Acc_Statment_Exp(dtr.Cells[0].Value.ToString());
                   // ex.SalesReport_Load(sender, e);
                   // ex.btn_prtdirct_Click(sender, e);
                   //// pf.progressBar1.Value = dtr.Index + 1;
                    if (!dtr.Cells[0].Value.ToString().Trim().Equals(""))
                    {
                        pf.progressBar1.Value = dtr.Index + 1;
                        Acc_Statment_Exp ex = new Acc_Statment_Exp(dtr.Cells[0].Value.ToString());
                        ex.SalesReport_Load(sender, e);
                        ex.Load_Statment(dtr.Cells[0].Value.ToString());
                        ex.btn_prtdirct_Click(sender, e);
                    }
                }
                // MessageBox.Show("ok");
                pf.Close();
            }
            else if (result == DialogResult.No)
            {
                //...
            }
            else
            {
                //...
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int testInt = dataGridView1.CurrentRow.DefaultCellStyle.BackColor == Color.Goldenrod ? 0 : 1;
                datval.formate_dgv(this.dataGridView1, testInt);
            }
            catch { }
        }
    }
}
