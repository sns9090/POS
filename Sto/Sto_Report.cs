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

namespace POS.Sto
{
    public partial class Sto_Report : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dt,dt1,dt2, dtusers;
      //  DataTable dtt;
        public static int qq;
       // string typeno = "";
        SqlConnection con3 =BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public Sto_Report()
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
                string usrname = cmb_user.SelectedIndex != -1 ? " and usrid = '" + cmb_user.SelectedValue.ToString() + "' " : " ";
                string cond = checkBox1.Checked? "trhdate" : "trmdate" ;
                string cond1 = checkBox1.Checked ? "invhdate" : "invmdate";
                string condp = rad_posted.Checked ? " and posted=1 " :(rad_notpostd.Checked? " and posted=0 " :" ");

                string condf = txt_fno.Text != "" ? txt_fno.Text : "1";
                string condt = txt_tno.Text != "" ? txt_tno.Text : "999999999";
                string condft = " and ref between " + condf + " and " + condt + " ";
                string condfdt = cmb_exityear.Checked ? " > '" + datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt) + "' or " + cond + " < '" + datval.convert_to_yyyyDDdd(BL.CLS_Session.start_dt) + "'" : " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'";
                string condfdt1 = cmb_exityear.Checked ? " > '" + datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt) + "' or " + cond1 + " < '" + datval.convert_to_yyyyDDdd(BL.CLS_Session.start_dt) + "'" : " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'";
                string condft_tp =chk_sto_only.Checked? " and trtype in ('31','32','33','36') " : " ";
                string condft_tp1 =chk_sto_only.Checked? " and invtype not in ('04','05','14','15','06','07','16','17') " : " ";

               // string qstr = "select trtype a_type,ref a_ref,CONVERT(VARCHAR(10), CAST(trmdate as date), 105) a_mdate,CONVERT(VARCHAR(10), CAST(trhdate as date), 105) a_hdate,text a_text,amnttl a_amt,posted a_posted,trtype a_t from sto_hdr where  branch='" + BL.CLS_Session.brno + "' " + condp + usrname + condft + " and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'";
                string qstr = " select trtype a_type,cast(ref as varchar) a_ref,CONVERT(VARCHAR(10), CAST(trmdate as date), 105) a_mdate,(substring(trhdate,7,2) + '-' + substring(trhdate,5,2)+'-'+substring(trhdate,1,4)) a_hdate,text a_text,amnttl a_amt,posted a_posted,trtype a_t,trmdate dto,'' dptno from sto_hdr where  branch='" + BL.CLS_Session.brno + "' and isbrtrx=0 " + condp + usrname + condft + " and " + cond + condfdt + condft_tp + "";

                string qstr1 = " select invtype a_type,cast(ref as varchar) a_ref,CONVERT(VARCHAR(10), CAST(invmdate as date), 105) a_mdate,(substring(invhdate,7,2) + '-' + substring(invhdate,5,2)+'-'+substring(invhdate,1,4)) a_hdate,text a_text,invttl a_amt,posted a_posted,invtype a_t,invmdate dto,slcenter dptno from sales_hdr where  branch='" + BL.CLS_Session.brno + "' and 0=0 " + condp + usrname + condft + " and " + cond1 + condfdt1 + condft_tp1 + "";

                string qstr2 = " select invtype a_type,cast(ref as varchar) a_ref,CONVERT(VARCHAR(10), CAST(invmdate as date), 105) a_mdate,(substring(invhdate,7,2) + '-' + substring(invhdate,5,2)+'-'+substring(invhdate,1,4)) a_hdate,text a_text,invttl a_amt,posted a_posted,invtype a_t,invmdate dto,pucenter dptno from pu_hdr where  branch='" + BL.CLS_Session.brno + "' and 0=0 " + condp + usrname + condft + " and " + cond1 + condfdt1 + condft_tp1 + "";
               
                if (!string.IsNullOrEmpty(txt_desc.Text))
                {
                    qstr = qstr + " and text like '%" + txt_desc.Text + "%'";
                    qstr1 = qstr1 + " and text like '%" + txt_desc.Text + "%'";
                    qstr2 = qstr2 + " and text like '%" + txt_desc.Text + "%'";
                }

                if (!string.IsNullOrEmpty(txt_equl.Text))
                {
                  //  MessageBox.Show(comboBox2.SelectedValue.ToString());
                    qstr = qstr + " and amntlt " + comboBox2.Text.Substring(0,1) + txt_equl.Text;
                    qstr1 = qstr1 + " and amntlt " + comboBox2.Text.Substring(0, 1) + txt_equl.Text;
                    qstr2 = qstr2 + " and amntlt " + comboBox2.Text.Substring(0, 1) + txt_equl.Text;
                }

                dt = daml.SELECT_QUIRY_only_retrn_dt(qstr + " and trtype like '%" + cmb_type.SelectedValue + "%' order by trmdate");
                dt1 = daml.SELECT_QUIRY_only_retrn_dt(qstr1 + " and invtype like '%" + cmb_type.SelectedValue + "%' order by invmdate");
                dt2 = daml.SELECT_QUIRY_only_retrn_dt(qstr2 + " and invtype like '%" + cmb_type.SelectedValue + "%' order by invmdate");
                
                DataTable dt_new = new DataTable();
                dt_new.Merge(dt);
                dt_new.Merge(dt1);
                dt_new.Merge(dt2);
                // dt_new.AcceptChanges();

                // dt_new.AcceptChanges();
                dt_new.DefaultView.Sort = "dto asc";
                //  dt_new.DefaultView.Sort = "c3 desc";
                dt_new = dt_new.DefaultView.ToTable();



                DataRow dr = dt_new.NewRow();
                double sumopnd = 0;//, sumopnm = 0, sumtrd = 0, sumtrm = 0, sumttld = 0, sumttlm = 0;
                foreach (DataRow dtr in dt_new.Rows)
                {
                    sumopnd = sumopnd + Convert.ToDouble(dtr[5]);
                   // sumopnm = sumopnm + Convert.ToDouble(dtr[3]);
                   // sumtrd = sumtrd + Convert.ToDouble(dtr[4]);
                   // sumtrm = sumtrm + Convert.ToDouble(dtr[5]);
                  //  sumttld = sumttld + Convert.ToDouble(dtr[6]);
                   // sumttlm = sumttlm + Convert.ToDouble(dtr[7]);
                }
                dr[0] = "";
                dr[1] = "" ;
                dr[2] = "  -  -    ";
                dr[3] = "  -  -    ";
                dr[4] =  "الاجمالي";
                dr[5] = sumopnd;
                dr[6] = true;
                dr[7] = "";

                dt_new.Rows.Add(dr);

                dataGridView1.DataSource = dt_new;

                
                foreach (DataGridViewRow rw in dataGridView1.Rows)
                {
                    if (Convert.ToBoolean(rw.Cells[6].Value) == false)
                       // rw.DefaultCellStyle.BackColor = Color.Thistle;
                        rw.DefaultCellStyle.ForeColor = Color.Red;
                   // rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02")? "س قبض" : "س صرف";
                    rw.Cells[0].Value =string.IsNullOrEmpty(rw.Cells[0].Value.ToString())? "" : datval.validate_trtypes(rw.Cells[0].Value.ToString());
                }

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
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";

            var items = new[] { new { Text = "قيد عام", Value = "01" }, new { Text = "سند قبض", Value = "02" }, new { Text = "سند صرف", Value = "03" } };
            comboBox1.DataSource = items;
            comboBox1.SelectedIndex = -1;
            */
            string tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
            cmb_type.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('31','32','33','36','04','05','14','15','06','07','16','17')");
            cmb_type.DisplayMember = "tr_name";
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
                if (dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Equals("04") || dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Equals("05") || dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Equals("14") || dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Equals("15"))
                {
                    if (BL.CLS_Session.cmp_type.StartsWith("a"))
                    {
                        Sal.Sal_Tran_D_TF fs = new Sal.Sal_Tran_D_TF(dataGridView1.CurrentRow.Cells["dptno"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        fs.MdiParent = ParentForm;
                        if (BL.CLS_Session.formkey.Contains(fs.Tag.ToString()))
                            fs.Show();
                    }
                    else
                    {
                        Sal.Sal_Tran_D fs = new Sal.Sal_Tran_D(dataGridView1.CurrentRow.Cells["dptno"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        fs.MdiParent = ParentForm;
                        if (BL.CLS_Session.formkey.Contains(fs.Tag.ToString()))
                            fs.Show();
                    }
                }

                if (dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Equals("06") || dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Equals("07") || dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Equals("16") || dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Equals("17"))
                {
                    Pur.Pur_Tran_D fp = new Pur.Pur_Tran_D(dataGridView1.CurrentRow.Cells["dptno"].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    fp.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(fp.Tag.ToString()))
                        fp.Show();
                }

                if (dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Equals("33"))
                {
                    Sto_qty_Convert f4 = new Sto_qty_Convert("",dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                    f4.Show();
                }

                if (dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Equals("31") || (dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Equals("32")))
                {
                    if (BL.CLS_Session.sysno.Equals("9"))
                    {
                        Sto_InOut f4 = new Sto_InOut("", dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        f4.MdiParent = ParentForm;
                        if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                            f4.Show();
                    }
                    else
                    {
                        Sto_ImpExp f4 = new Sto_ImpExp("", dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        f4.MdiParent = ParentForm;
                        if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                            f4.Show();
                    }
                }
                if (dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Equals("45") || (dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Equals("46")))
                {
                    Sto_item_Combine f4 = new Sto_item_Combine("", dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    f4.MdiParent = ParentForm;
                    if (BL.CLS_Session.formkey.Contains(f4.Tag.ToString()))
                    f4.Show();
                }
                if (dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Equals("36"))
                {
                    Sto_Cost_Tran f4 = new Sto_Cost_Tran("", dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
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
                cmb_type.SelectedIndex = -1;
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

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int testInt = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["Column1"].Value) ? 1 : 0;
                datval.formate_dgv(this.dataGridView1, testInt);
            }
            catch { }
        }
    }
}
