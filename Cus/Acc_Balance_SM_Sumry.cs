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
    public partial class Acc_Balance_SM_Sumry : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dtacbal ,dtusers,dtcity;
        public static int qq;
       // string typeno = "";
        SqlConnection con3 =BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
       // sqlConnection
        public Acc_Balance_SM_Sumry()
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
                string condc = cmb_class.SelectedIndex !=-1 ? " and a.cu_class='" + cmb_class.SelectedValue + "' " : " ";
                string qstr = "select a.cu_no d1,a.cu_name d2,round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.cu_no=b.cu_no and " + cond + "<='" + maskedTextBox2.Text.Replace("-", "") + "'),2) d3"
                            + " from customers a where round(a.cu_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.cu_no=b.cu_no and " + cond + "<='" + maskedTextBox2.Text.Replace("-", "") + "'),2) <>0 "
                            + " and cu_brno='" + BL.CLS_Session.brno + "'" + condc + " and a.cu_no like '%" + txt_cuno.Text + "%'"
                            + " order by a.cu_no;";
                 */
              //  string cond = checkBox1.Checked ? "b.a_hdate" : "b.a_mdate";
                string condmn = cmb_mndop.SelectedIndex == -1 ? " " : " and s.sp_id='"+cmb_mndop.SelectedValue+"' ";
                string cond = checkBox1.Checked ? "b.a_hdate" : "b.a_mdate";
                string condcn = string.IsNullOrEmpty(txt_cuno.Text) ? " " : " and a.cu_no='" + txt_cuno.Text + "' ";
                string condc = cmb_class.SelectedIndex != -1 ? " and a.cu_class='" + cmb_class.SelectedValue + "' " : " ";
                string condcity = cmb_city.SelectedIndex != -1 ? " and a.cu_city='" + cmb_city.SelectedValue + "' " : " ";
                string condp = rad_posted.Checked ? " and c.a_posted=1 " : (rad_notpostd.Checked ? " and c.a_posted=0 " : " ");
              //  string conddc = chk_m.Checked ? " having round((select isnull(a.cu_opnbal+sum(isnull(case when b.a_type not in ('04','14') then b.a_damt else 0 end,0)-isnull(case when b.a_type not in ('04','14') then b.a_camt else 0 end ,0)),0)),2)<0 " : (chk_d.Checked ? " round((select isnull(a.cu_opnbal+sum(isnull(case when b.a_type not in ('04','14') then b.a_damt else 0 end,0)-isnull(case when b.a_type not in ('04','14') then b.a_camt else 0 end ,0)),0)),2)>0 " : " having round((select isnull(a.cu_opnbal+sum(isnull(case when b.a_type not in ('04','14') then b.a_damt else 0 end,0)-isnull(case when b.a_type not in ('04','14') then b.a_camt else 0 end ,0)),0)),2)<>0 ");
                string cnd = " from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no where a.cu_brno=b.a_brno and a.cu_no=b.cu_no and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and a.cu_brno='" + BL.CLS_Session.brno + "' " + condc + condcity + condp ;
                string cndd = " from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no where a.cu_brno=b.a_brno and a.cu_no=b.cu_no and " + cond + " < '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and a.cu_brno='" + BL.CLS_Session.brno + "' " + condc + condcity + condp;

                string conddc = chk_m.Checked ? " having (select round((select a.cu_opnbal+isnull(sum(isnull(case when b.a_type not in ('04','14') then b.a_damt else 0 end,0)-isnull(case when b.a_type not in ('04','14') then b.a_camt else 0 end ,0)),0)),4) " + cndd + ") + (select round((select isnull(sum(isnull(case when b.a_type not in ('04','14') then b.a_damt else 0 end,0)-isnull(case when b.a_type not in ('04','14') then b.a_camt else 0 end ,0)),0)),4) " + cnd + ")<0 " : (chk_d.Checked ? " having (select round((select a.cu_opnbal+isnull(sum(isnull(case when b.a_type not in ('04','14') then b.a_damt else 0 end,0)-isnull(case when b.a_type not in ('04','14') then b.a_camt else 0 end ,0)),0)),4) " + cndd + ") + (select round((select isnull(sum(isnull(case when b.a_type not in ('04','14') then b.a_damt else 0 end,0)-isnull(case when b.a_type not in ('04','14') then b.a_camt else 0 end ,0)),0)),4) " + cnd + ")>0 " : " ");
                // string qstr = "select a.cu_no d1,a.cu_name d2,a.cu_opnbal d3,round(( isnull(sum(b.a_camt),0)),2) d4,round(( isnull(sum(b.a_damt),0)),2) d5,round(( isnull(a.cu_opnbal+sum(b.a_damt-b.a_camt),0)),2) d6,round(( isnull(max(iif(b.a_camt>0 ,b.a_camt,0)),0)),2) d7,( isnull(max(iif(b.a_camt>0 ,b.a_mdate,'##-##-####')),'##-##-####')) d8 from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and b.a_type not in ('04','14') right Outer join customers a on a.cu_brno=b.a_brno and a.cu_no=b.cu_no where a.cu_no<>'' " + condc + condcity + condp + " group by a.cu_no ,a.cu_name ,a.cu_opnbal " + conddc + "";
               // string qstr = "select a.cu_no d1,a.cu_name d2,a.cu_opnbal d3,round((isnull(sum(case when b.a_type in ('04','14') then b.a_camt + b.a_damt else 0 end),0)),2) d4,round(( isnull(sum(case when b.a_type in ('05','15') then b.a_camt + b.a_damt else 0 end),0)),2) d5,round(( isnull(sum(case when b.a_type in ('04','05','14','15') then b.a_camt + b.a_damt else 0 end),0)),2) d6,round(( isnull(sum(case when b.a_type not in ('04','05','14','15') then b.a_camt + b.a_damt else 0 end),0)),2) d7,round((select isnull(a.cu_opnbal+sum(isnull(case when b.a_type not in ('04','14') then b.a_damt else 0 end,0)-isnull(case when b.a_type not in ('04','14') then b.a_camt else 0 end ,0)),0)),2) d8 from customers a left outer join acc_dtl b  inner join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no on a.cu_brno=b.a_brno and a.cu_no=b.cu_no where " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and a.cu_brno='" + BL.CLS_Session.brno + "' " + condc + condcity + condp + condcn + " group by a.cu_no ,a.cu_name ,a.cu_opnbal " + conddc + " order by cast(a.cu_no as int)";
               // string qstr = "select a.cu_no d1,a.cu_name d2,a.cu_opnbal d3,round((isnull(sum(case when b.a_type in ('04','14') then b.a_camt + b.a_damt else 0 end),0)),2) d4,round(( isnull(sum(case when b.a_type in ('05','15') then b.a_camt + b.a_damt else 0 end),0)),2) d5,round(( isnull(sum(case when b.a_type in ('04','05','14','15') then b.a_camt + b.a_damt else 0 end),0)),2) d6,round(( isnull(sum(case when b.a_type not in ('04','05','14','15') then b.a_camt + b.a_damt else 0 end),0)),2) d7,round((select isnull(a.cu_opnbal+sum(isnull(case when b.a_type not in ('04','14') then b.a_damt else 0 end,0)-isnull(case when b.a_type not in ('04','14') then b.a_camt else 0 end ,0)),0)),2) d8,s.sp_name d9 from customers a left outer join sal_men s on s.sp_brno=a.cu_brno and s.sp_id=a.cu_salman left outer join acc_dtl b  inner join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no on a.cu_brno=b.a_brno and a.cu_no=b.cu_no where " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and a.cu_brno='" + BL.CLS_Session.brno + "' " + condc + condcity + condp + condcn + " group by a.cu_no ,a.cu_name ,a.cu_opnbal,s.sp_name " + conddc + " order by cast(a.cu_no as int)";
                string qstr = "select a.cu_no d1,a.cu_name d2,(select round((select a.cu_opnbal+isnull(sum(isnull(case when b.a_type not in ('04','14') then b.a_damt else 0 end,0)-isnull(case when b.a_type not in ('04','14') then b.a_camt else 0 end ,0)),0)),4) " + cndd + ") d3,(select round((isnull(sum(case when b.a_type in ('04','14') then b.a_camt + b.a_damt else 0 end),0)),4) " + cnd + ") d4,(select round(( isnull(sum(case when b.a_type in ('05','15') then b.a_camt + b.a_damt else 0 end),0)),4) " + cnd + ") d5,(select round(( isnull(sum(case when b.a_type in ('04','05','14','15') then b.a_camt + b.a_damt else 0 end),0)),4) " + cnd + ") d6,(select round(( isnull(sum(case when b.a_type not in ('04','05','14','15') then b.a_camt + b.a_damt else 0 end),0)),4) " + cnd + ") d7,(select round((select a.cu_opnbal+isnull(sum(isnull(case when b.a_type not in ('04','14') then b.a_damt else 0 end,0)-isnull(case when b.a_type not in ('04','14') then b.a_camt else 0 end ,0)),0)),4) " + cndd + ") + (select round((select isnull(sum(isnull(case when b.a_type not in ('04','14') then b.a_damt else 0 end,0)-isnull(case when b.a_type not in ('04','14') then b.a_camt else 0 end ,0)),0)),4) " + cnd + ") d8,s.sp_name d9 from customers a " + (cmb_mndop.SelectedIndex == -1 ? "left" : "") + " join sal_men s on s.sp_brno=a.cu_brno and s.sp_id=a.cu_salman " + condmn + " where 1=1 " + condcn + " group by a.cu_no ,a.cu_name ,a.cu_opnbal,a.cu_brno,s.sp_name " + conddc + " order by cast(a.cu_no as int)";
          
                dtacbal=daml.SELECT_QUIRY_only_retrn_dt(qstr);

               // dtacbal.Rows.Add(row);
                
                DataRow dr = dtacbal.NewRow();
                double sum = 0, sum2 = 0, sum3 = 0, sum4 = 0, sum6 = 0, sum7 = 0;
                foreach (DataRow dtr in dtacbal.Rows)
                {
                    sum2 = sum2 + Convert.ToDouble(dtr[2]);
                    sum3 = sum3 + Convert.ToDouble(dtr[3]);
                    sum4 = sum4 + Convert.ToDouble(dtr[4]);
                    sum = sum + Convert.ToDouble(dtr[5]);
                    sum6 = sum6 + Convert.ToDouble(dtr[6]);
                    sum7 = sum7 + Convert.ToDouble(dtr[7]);
                }
                dr[0] = "";
                dr[1] = "الاجمالي";
                dr[2] = sum2;
                dr[3] = sum3;
                dr[4] = sum4;
                dr[5] = sum;
                dr[6] = sum6;
                dr[7] = sum7;

                dtacbal.Rows.Add(dr);
                

               

               // dataGridView1.Rows.Add();
                //dataGridView1.Columns[1].Width = 500;
                //dataGridView1.Columns[2].Width = 200;
                //dataGridView1.Columns[0].Width = 200;
               // dataGridView1.ClearSelection();

                ////foreach (DataRow dtr in dtacbal.Rows)
                ////{
                  
                
                ////    //if (chk_m.Checked && Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) > 0)
                ////    //{
                ////    //    dataGridView1.Rows.RemoveAt(i);
                ////    //}

                ////    //if (chk_d.Checked && Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) < 0)
                ////    //{
                ////    //    dataGridView1.Rows.RemoveAt(i);
                ////    //}
                ////    string toval = dtr["d8"].ToString().Substring(6, 2) + "-" + dtr["d8"].ToString().Substring(4, 2) + "-" + dtr["d8"].ToString().Substring(0, 4);


                ////    dtr["d8"] = toval;
                ////}
                dataGridView1.DataSource = dtacbal;
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
           
            /*
            maskedTextBox1.Text = DateTime.Now.ToString("yyyy-MM-01") + "05:00:00";

            DateTime dt = new DateTime();

            dt=DateTime.Now.AddDays(1);

           // maskedTextBox2.Text = DateTime.Now.ToString("yyyy-MM-dd") + "05:00:00";
            maskedTextBox2.Text = dt.ToString("yyyy-MM-dd") + "05:00:00";
             * */
           // string temy = BL.CLS_Session.end_dt;// DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
           // txt_tdate.Text = temy;
            txt_fdate.Text = BL.CLS_Session.start_dt;
            txt_tdate.Text = BL.CLS_Session.end_dt;

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

            DataTable dt3 = daml.SELECT_QUIRY_only_retrn_dt("select sp_id,sp_name from sal_men where sp_brno='" + BL.CLS_Session.brno + "'");
            // da.Fill(dt);


            cmb_mndop.DataSource = dt3;

            cmb_mndop.ValueMember = "sp_id";
            cmb_mndop.DisplayMember = "sp_name";
            cmb_mndop.SelectedIndex = -1;
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

                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Cus.rpt.Cus_Balance_SM_Sumry_rpt.rdlc";

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[4];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
              //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                parameters[1] = new ReportParameter("mdate", txt_tdate.Text);
                parameters[2] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[3] = new ReportParameter("desc", "نشاط العملاء والمندوبين حتى التاريخ ");

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
            if (e.KeyCode == Keys.F8)
            {
                Search_All f4 = new Search_All("5", "Cus");
                f4.ShowDialog();



                txt_cuno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
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

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
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

        private void txt_tdate_Leave_1(object sender, EventArgs e)
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

        private void cmb_mndop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_mndop.SelectedIndex = -1;
            }
        }
    }
}
