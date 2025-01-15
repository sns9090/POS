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
    public partial class Sal_Report_sp : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dt, dtusers,dtcolman;
      //  DataTable dtt;
        public static int qq;
       // string typeno = "";
        SqlConnection con3 =BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public Sal_Report_sp()
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

               

                if (chk_all.Checked)
                {
                    string usrname = cmb_user.SelectedIndex != -1 ? " and b.usrid = '" + cmb_user.SelectedValue.ToString() + "' " : " ";
                    string cond = checkBox1.Checked ? "b.invhdate" : "b.invmdate";

                    string condf = txt_fno.Text != "" ? txt_fno.Text : "1";
                    string condt = txt_tno.Text != "" ? txt_tno.Text : "999999999";
                    string condft = " and b.ref between " + condf + " and " + condt + " ";

                    string condp = rad_posted.Checked ? " and b.posted=1 " : (rad_notpostd.Checked ? " and b.posted=0 " : " ");
                    string condcol = rad_posted.Checked ? " and b.posted=1 " : (rad_notpostd.Checked ? " and b.posted=0 " : " ");

                    // string qstr = "select a.a_type a_type,cast(a.a_ref as varchar) a_ref,CONVERT(VARCHAR(10), CAST(a.a_mdate as date), 105) a_mdate,CONVERT(VARCHAR(10), CAST(a.a_hdate as date), 105) a_hdate,c.cu_name a_text,a.a_amt a_amt, round((a.a_amt * " + Math.Round(Convert.ToDouble(dtcolman.Rows[0]["sp_percent"]), 2) + "/100),2) per,a.a_posted,a.a_type a_t,a.jhsrc src from acc_hdr a left join acc_dtl b on a.a_brno=b.a_brno and a.a_type=b.a_type and a.a_ref=b.a_ref join customers c on b.cu_no=c.cu_name where a.jhsrc like 'Cus%' and a.a_brno='" + BL.CLS_Session.brno + "' " + condp + usrname + condft + " and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and a.jhcustno='" + cmb_colman.SelectedValue + "'";
                   // string qstr = "select a.sp_id sp_id,a.sp_name sp_name,(select isnull(sum(b.a_amt),0) from acc_hdr b where a.sp_id=b.jhcustno and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' ) sp_all,(select isnull(sum(b.a_amt * " + Math.Round(Convert.ToDouble(dtcolman.Rows[0]["sp_percent"]), 2) + "/100),0) from acc_hdr b where a.sp_id=b.jhcustno and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' ) sp_per from col_men a join acc_hdr b on a.sp_id=b.jhcustno where b.jhsrc like 'Cus%' and b.a_brno='" + BL.CLS_Session.brno + "' " + condp + usrname + condft + " ";
                    string qstr = "select a.sp_id sp_id,a.sp_name sp_name, round( isnull((sum(case when invtype in ('04','05') then (b.nettotal-b.tax_amt_rcvd) else -(b.nettotal-b.tax_amt_rcvd) end)) ,0),2) sp_all,round((isnull((sum(case when invtype in ('04','05') then (b.nettotal-b.tax_amt_rcvd) else -(b.nettotal-b.tax_amt_rcvd) end)),0) * " + Math.Round(Convert.ToDouble(dtcolman.Rows[0]["sp_percent"]), 2) + "/100),2) sp_per from sal_men a join sales_hdr b on a.sp_id=b.slcode where " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and b.branch='" + BL.CLS_Session.brno + "' " + condp + usrname + condft + " ";

                    //if (!string.IsNullOrEmpty(txt_desc.Text))
                    //{
                    //    qstr = qstr + " and a_text like '%" + txt_desc.Text + "%'";
                    //}

                    if (!string.IsNullOrEmpty(txt_equl.Text))
                    {
                        //  MessageBox.Show(comboBox2.SelectedValue.ToString());
                        qstr = qstr + " and b.nettotal " + comboBox2.Text.Substring(0, 1) + txt_equl.Text;
                    }

                    dt = daml.SELECT_QUIRY_only_retrn_dt(qstr + " and b.invtype like '%" + comboBox1.SelectedValue + "%' group by a.sp_id , a.sp_name");

                    DataRow dr = dt.NewRow();
                    double sum = 0, sum2 = 0;
                    foreach (DataRow dtr in dt.Rows)
                    {
                        sum = sum + Convert.ToDouble(dtr[2]);
                        sum2 = sum2 + Convert.ToDouble(dtr[3]);
                    }
                    dr[0] = "";
                    dr[1] = "الاجمالي";
                    dr[2] = sum;
                    dr[3] = sum2;
                  //  dr[4] = "الاجمالي";
                    //dr[5] = sum;
                    //dr[6] = sum2;
                    //dr[7] = true;
                    //dr[8] = "";
                    //dr[9] = "";
                    // dr[2] = " ";
                    dt.Rows.Add(dr);

                    dataGridView2.DataSource = dt;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                    dataGridView2.ClearSelection();
                
                }
                else
                {
                    if (cmb_colman.SelectedIndex == -1)
                    {
                        MessageBox.Show("يرجى اختيار بائع", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmb_colman.Focus();
                        return;
                    }
                    /*
                    DataView dv1 = dtcolman.DefaultView;
                    // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                    dv1.RowFilter = "sp_id ='" + cmb_colman.SelectedValue + "'";
                    DataTable dtper = dv1.ToTable();
                    */
                    DataRow[] dtrvat = dtcolman.Select("sp_id ='" + cmb_colman.SelectedValue + "'");

                    string usrname = cmb_user.SelectedIndex != -1 ? " and a.usrid = '" + cmb_user.SelectedValue.ToString() + "' " : " ";
                    string cond = checkBox1.Checked ? "a.invhdate" : "a.invmdate";

                    string condf = txt_fno.Text != "" ? txt_fno.Text : "1";
                    string condt = txt_tno.Text != "" ? txt_tno.Text : "999999999";
                    string condft = " and a.ref between " + condf + " and " + condt + " ";

                    string condp = rad_posted.Checked ? " and a.posted=1 " : (rad_notpostd.Checked ? " and a.posted=0 " : " ");
                    string condcol = rad_posted.Checked ? " and a.posted=1 " : (rad_notpostd.Checked ? " and a.posted=0 " : " ");

                    // string qstr = "select a.a_type a_type,cast(a.a_ref as varchar) a_ref,CONVERT(VARCHAR(10), CAST(a.a_mdate as date), 105) a_mdate,CONVERT(VARCHAR(10), CAST(a.a_hdate as date), 105) a_hdate,c.cu_name a_text,a.a_amt a_amt, round((a.a_amt * " + Math.Round(Convert.ToDouble(dtcolman.Rows[0]["sp_percent"]), 2) + "/100),2) per,a.a_posted,a.a_type a_t,a.jhsrc src from acc_hdr a left join acc_dtl b on a.a_brno=b.a_brno and a.a_type=b.a_type and a.a_ref=b.a_ref join customers c on b.cu_no=c.cu_name where a.jhsrc like 'Cus%' and a.a_brno='" + BL.CLS_Session.brno + "' " + condp + usrname + condft + " and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and a.jhcustno='" + cmb_colman.SelectedValue + "'";
                   // string qstr = "select a.invtype a_type,cast(a.ref as varchar) a_ref,CONVERT(VARCHAR(10), CAST(a.invmdate as date), 105) a_mdate,CONVERT(VARCHAR(10), CAST(a.invhdate as date), 105) a_hdate,a.text a_text,(a.nettotal-a.tax_amt_rcvd) a_amt, round(((a.nettotal-a.tax_amt_rcvd) * " + Math.Round(Convert.ToDouble(dtcolman.Rows[0]["sp_percent"]), 2) + "/100),2) per,a.posted a_posted,a.invtype a_t,glser src,slcenter sl from sales_hdr a where a.branch='" + BL.CLS_Session.brno + "' " + condp + usrname + condft + " and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and a.slcode='" + cmb_colman.SelectedValue + "'";
                    string qstr = "select a.invtype a_type,cast(a.ref as varchar) a_ref,CONVERT(VARCHAR(10), CAST(a.invmdate as date), 105) a_mdate,(substring(a.invhdate,7,2) + '-' + substring(a.invhdate,5,2)+'-'+substring(a.invhdate,1,4)) a_hdate,a.text a_text,round((case when invtype in ('04','05') then (a.nettotal-a.tax_amt_rcvd) else -(a.nettotal-a.tax_amt_rcvd) end),2) a_amt, ((case when invtype in ('04','05') then round(((a.nettotal-a.tax_amt_rcvd)),2) else -round(((a.nettotal-a.tax_amt_rcvd)),2) end) * " + Math.Round(Convert.ToDouble(dtrvat[0][4]), 2) + "/100) per,a.posted a_posted,a.invtype a_t,glser src,slcenter sl from sales_hdr a where a.branch='" + BL.CLS_Session.brno + "' " + condp + usrname + condft + " and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and a.slcode='" + cmb_colman.SelectedValue + "'";

                    //if (!string.IsNullOrEmpty(txt_desc.Text))
                    //{
                    //    qstr = qstr + " and a_text like '%" + txt_desc.Text + "%'";
                    //}

                    if (!string.IsNullOrEmpty(txt_equl.Text))
                    {
                        //  MessageBox.Show(comboBox2.SelectedValue.ToString());
                        qstr = qstr + " and a.nettotal " + comboBox2.Text.Substring(0, 1) + txt_equl.Text;
                    }

                    dt = daml.SELECT_QUIRY_only_retrn_dt(qstr + " and a.invtype like '%" + comboBox1.SelectedValue + "%'  order by a.invmdate");

                    DataRow dr = dt.NewRow();
                    double sum = 0, sum2 = 0;
                    foreach (DataRow dtr in dt.Rows)
                    {
                        sum = sum + Convert.ToDouble(dtr[5]);
                        sum2 = sum2 + Convert.ToDouble(dtr[6]);
                    }
                    dr[0] = "";
                    dr[1] = "";
                    dr[2] = "";
                    dr[3] = "";
                    dr[4] = "الاجمالي";
                    dr[5] = sum;
                    dr[6] = sum2;
                    dr[7] = true;
                    dr[8] = "";
                    dr[9] = "";
                    // dr[2] = " ";
                    dt.Rows.Add(dr);

                    dataGridView1.DataSource = dt;


                    // foreach (DataGridViewRow rw in dataGridView1.Rows)
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {

                        if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[7].Value) == false)
                            // rw.DefaultCellStyle.BackColor = Color.Thistle;
                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        // rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02")? "س قبض" : "س صرف";
                        dataGridView1.Rows[i].Cells[0].Value = !string.IsNullOrEmpty(dataGridView1.Rows[i].Cells[0].Value.ToString()) ? datval.validate_trtypes(dataGridView1.Rows[i].Cells[0].Value.ToString()) : " ";
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
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";

            var items = new[] { new { Text = "قيد عام", Value = "01" }, new { Text = "سند قبض", Value = "02" }, new { Text = "سند صرف", Value = "03" } };
            comboBox1.DataSource = items;
            comboBox1.SelectedIndex = -1;


            dtusers = daml.SELECT_QUIRY_only_retrn_dt("select user_name,full_name from users");

            cmb_user.DataSource = dtusers;
            cmb_user.DisplayMember = "full_name";
            cmb_user.ValueMember = "user_name";
            cmb_user.SelectedIndex = -1;


            dtcolman = daml.SELECT_QUIRY_only_retrn_dt("select * from sal_men where sp_brno='" + BL.CLS_Session.brno + "'");

            cmb_colman.DataSource = dtcolman;
            cmb_colman.DisplayMember = "sp_name";
            cmb_colman.ValueMember = "sp_id";
            cmb_colman.SelectedIndex = -1;

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
          ////  string temy1 =
          //  txt_fdate.Text = DateTime.Now.ToString("yyyy-01-01", new CultureInfo("en-US"));

          // // string temy2=
          //  txt_tdate.Text = DateTime.Now.ToString("yyyy-12-31", new CultureInfo("en-US"));
            txt_fdate.Text = BL.CLS_Session.start_dt;

            // string temy2=
            txt_tdate.Text = BL.CLS_Session.end_dt;
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
            if (dataGridView1.Visible == true)
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                    XcelApp.Application.Workbooks.Add(Type.Missing);



                    for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                    {
                        // XcelApp.Cells[5, i] = dataGridView1.Columns[i - 1].Visible == true ? dataGridView1.Columns[i - 1].HeaderText : "";
                        if (dataGridView1.Columns[i - 1].Visible == true)
                            XcelApp.Cells[6, i] = "'" + dataGridView1.Columns[i - 1].HeaderText;
                        else
                            XcelApp.Cells[6, i] = null;
                    }

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            // XcelApp.Cells[i + 6, j + 1] = dataGridView1.Rows[i].Cells[j].Visible == true ? dataGridView1.Rows[i].Cells[j].Value.ToString() : "";
                            if (dataGridView1.Columns[j].Visible == true)
                                //XcelApp.Cells[i + 7, j + 1] = "'" + dataGridView1.Rows[i].Cells[j].Value.ToString();
                                XcelApp.Cells[i + 7, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString().StartsWith("0") && dataGridView1.Rows[i].Cells[j].Value.ToString().Length > 1 ? "'" + dataGridView1.Rows[i].Cells[j].Value.ToString() : dataGridView1.Rows[i].Cells[j].Value.ToString();
                            else
                                XcelApp.Cells[i + 7, j + 1] = null;
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
                    workSheet.Cells[2, "E"] = BL.CLS_Session.cmp_name;
                    workSheet.Cells[3, "E"] = BL.CLS_Session.brname;
                    workSheet.Cells[4, "E"] = this.Text + "  " + "من" + "  " + txt_fdate.Text + "  " + "الى" + "  " + txt_tdate.Text;

                    workSheet.Range["D2", "F2"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                    workSheet.Range["D3", "F3"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                    workSheet.Range["D4", "F4"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                    workSheet.Range["E" + (dataGridView1.Rows.Count + 6).ToString(), "G" + (dataGridView1.Rows.Count + 6).ToString()].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                    workSheet.Range["A6", "G6"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                    // workSheet.Cells[2, "D"]

                    XcelApp.Columns.AutoFit();
                    XcelApp.Visible = true;
                }
            }
            else
            {

                if (dataGridView2.Rows.Count > 0)
                {
                    Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                    XcelApp.Application.Workbooks.Add(Type.Missing);



                    for (int i = 1; i < dataGridView2.Columns.Count + 1; i++)
                    {
                        // XcelApp.Cells[5, i] = dataGridView1.Columns[i - 1].Visible == true ? dataGridView1.Columns[i - 1].HeaderText : "";
                        if (dataGridView2.Columns[i - 1].Visible == true)
                            XcelApp.Cells[6, i] = "'" + dataGridView2.Columns[i - 1].HeaderText;
                        else
                            XcelApp.Cells[6, i] = null;
                    }

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView2.Columns.Count; j++)
                        {
                            // XcelApp.Cells[i + 6, j + 1] = dataGridView1.Rows[i].Cells[j].Visible == true ? dataGridView1.Rows[i].Cells[j].Value.ToString() : "";
                            if (dataGridView2.Columns[j].Visible == true)
                                //XcelApp.Cells[i + 7, j + 1] = "'" + dataGridView2.Rows[i].Cells[j].Value.ToString();
                                XcelApp.Cells[i + 7, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString().StartsWith("0") && dataGridView1.Rows[i].Cells[j].Value.ToString().Length > 1 ? "'" + dataGridView1.Rows[i].Cells[j].Value.ToString() : dataGridView1.Rows[i].Cells[j].Value.ToString();
                            else
                                XcelApp.Cells[i + 7, j + 1] = null;
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
                    workSheet.Cells[2, "B"] = BL.CLS_Session.cmp_name;
                    workSheet.Cells[3, "B"] = BL.CLS_Session.brname;
                    workSheet.Cells[4, "B"] = this.Text + "  " + "من" + "  " + txt_fdate.Text + "  " + "الى" + "  " + txt_tdate.Text;

                    workSheet.Range["B2", "C2"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                    workSheet.Range["B3", "C3"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                    workSheet.Range["B4", "C4"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                    workSheet.Range["B" + (dataGridView1.Rows.Count + 6).ToString(), "D" + (dataGridView1.Rows.Count + 6).ToString()].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                    workSheet.Range["A6", "D6"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                    // workSheet.Cells[2, "D"]

                    XcelApp.Columns.AutoFit();
                    XcelApp.Visible = true;
                }
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
            if (chk_all.Checked)
            {
                if (dataGridView2.Rows.Count == 0)
                    return;
                /*
                Reports.Report_Form rrf = new Reports.Report_Form("DR", dtt);
                rrf.MdiParent = ParentForm;
                rrf.Show();
                 */
                try
                {
                    Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();

                    DataTable dt3 = new DataTable();
                    int cn = 1;
                    foreach (DataGridViewColumn col in dataGridView2.Columns)
                    {
                        /*
                        dt.Columns.Add(col.Name);
                        // dt.Columns.Add(col.HeaderText);
                         */
                        dt3.Columns.Add("c" + cn.ToString());
                        cn++;
                    }

                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        DataRow dRow = dt3.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dRow[cell.ColumnIndex] = cell.Value;
                        }
                        dt3.Rows.Add(dRow);
                    }

                    //  DataSet ds1 = new DataSet();




                    rf.reportViewer1.LocalReport.DataSources.Clear();
                    rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt3));
                    rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sal.rpt.Sal_Report_sp_all_rpt.rdlc";



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
            else
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

                    DataTable dt2 = new DataTable();
                    int cn = 1;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        /*
                        dt.Columns.Add(col.Name);
                        // dt.Columns.Add(col.HeaderText);
                         */
                        dt2.Columns.Add("c" + cn.ToString());
                        cn++;
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        DataRow dRow = dt2.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dRow[cell.ColumnIndex] = cell.Value;
                        }
                        dt2.Rows.Add(dRow);
                    }

                    //  DataSet ds1 = new DataSet();




                    rf.reportViewer1.LocalReport.DataSources.Clear();
                    rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt2));
                    rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sal.rpt.Sal_Report_sp_rpt.rdlc";



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
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Sal1") || dataGridView1.CurrentRow.Cells["src"].Value.Equals("Pos"))
                {
                    if (BL.CLS_Session.cmp_type.StartsWith("a"))
                    {
                        Sal.Sal_Tran_D_TF f4 = new Sal.Sal_Tran_D_TF(dataGridView1.CurrentRow.Cells["sl"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_ref"].Value.ToString());
                        f4.MdiParent = ParentForm;
                        if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                            f4.Show();
                    }
                    else
                    {
                        Sal.Sal_Tran_D f4 = new Sal.Sal_Tran_D(dataGridView1.CurrentRow.Cells["sl"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_ref"].Value.ToString());
                        f4.MdiParent = ParentForm;
                        if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                            f4.Show();
                    }
                }

                if (dataGridView1.CurrentRow.Cells["src"].Value.Equals("Sal2"))
                {
                    Sal.Sal_Tran_notax f4 = new Sal.Sal_Tran_notax(dataGridView1.CurrentRow.Cells["sl"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_ref"].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                        f4.Show();
                }


            }
        }

        private void dataGridView1_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
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
                comboBox1.SelectedIndex = -1;
            }

             
        }

        private void كشفحسابToolStripMenuItem_Click(object sender, EventArgs e)
        {/*
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

        private void cmb_colman_SelectedIndexChanged(object sender, EventArgs e)
        {
         //dataGridView1.Rows.Clear();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_all.Checked)
            {
                dataGridView1.Visible = false;
                dataGridView2.Visible = true;
            }
            else
            {
                dataGridView2.Visible = false;
                dataGridView1.Visible = true;
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
            }
        }
    }
}
