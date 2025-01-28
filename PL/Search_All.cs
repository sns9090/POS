using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

namespace POS
{
    public partial class Search_All : Form
    {
      //  int fir = 0;
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        string strkey,condition,cond1,usrid;
        SqlConnection con = BL.DAML.con;//new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public Search_All(string  key,string cond)
        {
           
            InitializeComponent();
            strkey = key;
            condition = cond;
            
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (msg.WParam.ToInt32() == (int)Keys.Enter)
            {
                SendKeys.Send("{Tab}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            usrid = BL.CLS_Session.up_edit_othr ? " " : " and usrid = '" + BL.CLS_Session.UserName + "' ";
            this.Icon = Properties.Resources.TsIcon;
           // MessageBox.Show(BL.CLS_Session.cstkey.Replace(" ", "','").Remove(0, 2) + "'");
           // string ckey = BL.CLS_Session.cstkey.Replace(" ", "','").Remove(0, 2) + "'";
            string ckey1 = !string.IsNullOrEmpty(BL.CLS_Session.cstkey) ? BL.CLS_Session.cstkey.Replace(" ", "','").Remove(0, 2) + "'" : " ";
            string ckey2 = !string.IsNullOrEmpty(BL.CLS_Session.cstkey) ? " and cc_no in(" + ckey1 + ") " : " ";

            if (BL.CLS_Session.temp_lang.Equals("en"))
            {
                chk_enar.Checked=true;
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("EN"));
            }
            else
            {
                chk_enar.Checked = false;
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
            }
            this.Size = condition.Equals("Pos") ? new System.Drawing.Size(626, 500) : new System.Drawing.Size(626, 280);
           // if (condition.Equals("Pos"))
              //  button1_Click_1(sender, e);
           // MessageBox.Show(condition);

            if (BL.CLS_Session.islight)
            {
                button1.Visible = false;
            }
           // dataGridView1.CurrentRow.Cells[0].Value = null;
            //dataGridView1.
            /*
            listView1.Columns.Add("key", 150);
            listView1.Columns.Add("name", 300);

            listView2.Columns.Add("key", 150);
            listView2.Columns.Add("name", 300);
           */

            //if (BL.CLS_Session.is_bold.Equals("1"))
            //{
            //    this.Font = new Font(BL.CLS_Session.font_t, float.Parse(BL.CLS_Session.font_s)-1, FontStyle.Bold);
            //}
            //else
            //{
            //    this.Font = new Font(BL.CLS_Session.font_t, float.Parse(BL.CLS_Session.font_s)-1);
            //}

            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;

           // InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
            //switch (BL.CLS_Session.lang)
            //{
            //   // case "ع": Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar"); break;
            //  //  case "E": Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en"); break;
            //    case "ع":
            //        InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
            //        break;
            //    case "E":
            //        InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("EN"));
            //        break;
            //}

            switch (strkey)
            {
                case "prntr":
                    textBox1.Visible = false;
                    //dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select wh_no 'رقم المخزن',wh_name 'اسم المخزن' from whouses where wh_brno='" + BL.CLS_Session.brno + "' ");
                    foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                    {
                        //  MessageBox.Show();
                        
                        dataGridView1.Rows.Add(printer);
                    }
                    dataGridView1.AllowUserToAddRows = false;
                    break;

                case "3":
                   // textBox1.Visible = false;
                    this.Text =BL.CLS_Session.lang.Equals("E")? "Trns. View" : "استعراض الحركات";
                   // dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select top 100 a_type " + (BL.CLS_Session.lang.Equals("ع") ? "'النوع'" : "'Type'") + " ,a_ref " + (BL.CLS_Session.lang.Equals("ع") ? "'الرقم'" : "'Ref No'") + " ,CONVERT(VARCHAR(10), CAST(a_mdate as date), 105) " + (BL.CLS_Session.lang.Equals("ع") ? "'التاريخ'" : "'Date'") + "  ,a_text " + (BL.CLS_Session.lang.Equals("ع") ? "'الوصف'" : "'Description'") + " ,round(a_amt,2) " + (BL.CLS_Session.lang.Equals("ع") ? "'المبلغ'" : "'Amount'") + " ,a_type a_t from acc_hdr where a_brno='" + BL.CLS_Session.brno + "' and jhsrc='" + condition + "' " + (ckey2.Trim().Equals("") ? "" : ckey2) + " and a_text like '%" + textBox1.Text.Replace(" ", "%") + "%' order by a_mdate desc");
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select top 100 a_type " + (BL.CLS_Session.lang.Equals("ع") ? "'النوع'" : "'Type'") + " ,a_ref " + (BL.CLS_Session.lang.Equals("ع") ? "'الرقم'" : "'Ref No'") + " ,CONVERT(VARCHAR(10), CAST(a_mdate as date), 105) " + (BL.CLS_Session.lang.Equals("ع") ? "'التاريخ'" : "'Date'") + "  ,a_text " + (BL.CLS_Session.lang.Equals("ع") ? "'الوصف'" : "'Description'") + " ,round(a_amt,2) " + (BL.CLS_Session.lang.Equals("ع") ? "'المبلغ'" : "'Amount'") + " ,a_type a_t from acc_hdr where a_brno='" + BL.CLS_Session.brno + "' and jhsrc='" + condition + "'  and a_text like '%" + textBox1.Text.Replace(" ", "%") + "%' order by a_mdate desc");
                    dataGridView1.Columns[3].Width = 150;
                    dataGridView1.Columns[0].Width = 75;
                    dataGridView1.Columns[2].Width = 100;
                    dataGridView1.Columns[1].Width = 75;
                    dataGridView1.Columns["a_t"].Visible = false;
                    dataGridView1.RightToLeft = BL.CLS_Session.lang.Equals("E") ? RightToLeft.No : RightToLeft.Yes;
                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                     //   rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02") ? "س قبض" : "س صرف";

                        rw.Cells[0].Value = datval.validate_trtypes(rw.Cells[0].Value.ToString());
                    }

                    break;

                case "4":
                    textBox1.Visible = false;
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select wh_no 'رقم المخزن',wh_name 'اسم المخزن' from whouses where wh_brno='" + BL.CLS_Session.brno + "' ");

                    break;

                case "cur":
                    textBox1.Visible = false;
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select curcode 'رمز العملة',curname 'اسم العملة' from crncy ");
                    dataGridView1.Columns[0].FillWeight = 30;
                    dataGridView1.Columns[1].FillWeight = 70;         
                    dataGridView1.RightToLeft = RightToLeft.Yes;
                    break;
               
                case "2-2":
                    textBox1.Text = condition;
                    break;


                case "6":
                    //   textBox1.Visible = false;
                    string sle = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";
                   // string usrid = BL.CLS_Session.up_edit_othr ? " " : " and usrid = '" + BL.CLS_Session.UserName + "' ";
                    this.Text = this.Text = BL.CLS_Session.lang.Equals("E") ? "Sales View" : "استعراض المبيعات";
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select top 100 slcenter " + (BL.CLS_Session.lang.Equals("ع") ? "'مركز البيع'" : "'Sal Center'") + " ,invtype " + (BL.CLS_Session.lang.Equals("ع") ? "'النوع'" : "'Type'") + ",ref " + (BL.CLS_Session.lang.Equals("ع") ? "'الرقم'" : "'Ref No'") + ",CONVERT(VARCHAR(10), CAST(invmdate as date), 105) " + (BL.CLS_Session.lang.Equals("ع") ? "'التاريخ'" : "'Date'") + " ," + (checkBox1.Checked ? "remarks 'الملاحضات' " : "case when custno='' then text else cu_name end " + (BL.CLS_Session.lang.Equals("ع") ? "'الوصف'" : "'Description'") + "") + ",round(nettotal,2) " + (BL.CLS_Session.lang.Equals("ع") ? "'المبلغ'" : "'Amount'") + ",invtype a_t from sales_hdr left join customers on sales_hdr.custno=customers.cu_no and sales_hdr.branch=customers.cu_brno where branch='" + BL.CLS_Session.brno + "' " + usrid + " and branch + slcenter in(" + sle + ") and (" + (checkBox1.Checked ? "remarks+text+note2+note3+cust_mobil" : "text+remarks+note2+note3+cust_mobil") + " like '%" + textBox1.Text.Replace(" ", "%") + "%' or chkno like '%" + textBox1.Text + "%') order by released desc");
                    dataGridView1.Columns[0].Width = 35;
                    dataGridView1.Columns[1].Width = 50;
                    dataGridView1.Columns[2].Width = 55;
                    dataGridView1.Columns[3].Width = 65;
                    dataGridView1.Columns[4].Width = 180;
                    // dataGridView1.Columns[0].Width = 75;
                    dataGridView1.Columns[5].Width = 60;
                    // dataGridView1.Columns[1].Width = 75;
                    dataGridView1.Columns["a_t"].Visible = false;
                    dataGridView1.RightToLeft = BL.CLS_Session.lang.Equals("E") ? RightToLeft.No : RightToLeft.Yes;
                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        //   rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02") ? "س قبض" : "س صرف";

                        rw.Cells[1].Value = datval.validate_trtypes(rw.Cells[1].Value.ToString());
                    }
                    break;

                case "6-6":
                    //   textBox1.Visible = false;
                    string sles = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";
                    this.Text = this.Text = BL.CLS_Session.lang.Equals("E") ? "Sales View" : "استعراض المبيعات";
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select slcenter " + (BL.CLS_Session.lang.Equals("ع") ? "'مركز البيع'" : "'Sal Center'") + " ,invtype " + (BL.CLS_Session.lang.Equals("ع") ? "'النوع'" : "'Type'") + ",ref " + (BL.CLS_Session.lang.Equals("ع") ? "'الرقم'" : "'Ref No'") + ",CONVERT(VARCHAR(10), CAST(invmdate as date), 105) " + (BL.CLS_Session.lang.Equals("ع") ? "'التاريخ'" : "'Date'") + " ," + (checkBox1.Checked ? "remarks 'الملاحضات' " : "case when custno='' then text else cu_name end " + (BL.CLS_Session.lang.Equals("ع") ? "'الوصف'" : "'Description'") + "") + ",round(nettotal,2) " + (BL.CLS_Session.lang.Equals("ع") ? "'المبلغ'" : "'Amount'") + ",invtype a_t from sales_hdr left join customers on sales_hdr.custno=customers.cu_no and sales_hdr.branch=customers.cu_brno where branch='" + BL.CLS_Session.brno + "' and branch + slcenter in(" + sles + ") " + condition + " and (" + (checkBox1.Checked ? "remarks+text+note2+note3+cust_mobil" : "text+remarks+note2+note3+cust_mobil") + " like '%" + textBox1.Text.Replace(" ", "%") + "%' or chkno like '%" + textBox1.Text + "%') order by released desc");
                    dataGridView1.Columns[0].Width = 35;
                    dataGridView1.Columns[1].Width = 50;
                    dataGridView1.Columns[2].Width = 55;
                    dataGridView1.Columns[3].Width = 65;
                    dataGridView1.Columns[4].Width = 180;
                    // dataGridView1.Columns[0].Width = 75;
                    dataGridView1.Columns[5].Width = 60;
                    // dataGridView1.Columns[1].Width = 75;
                    dataGridView1.Columns["a_t"].Visible = false;
                    dataGridView1.RightToLeft = BL.CLS_Session.lang.Equals("E") ? RightToLeft.No : RightToLeft.Yes;
                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        //   rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02") ? "س قبض" : "س صرف";

                        rw.Cells[1].Value = datval.validate_trtypes(rw.Cells[1].Value.ToString());
                    }
                    break;

                case "10":
                    //   textBox1.Visible = false;
                    string sl = BL.CLS_Session.pukey.Replace(" ", "','").Remove(0, 2) + "'";
                    this.Text = this.Text = BL.CLS_Session.lang.Equals("E") ? "Purchase View" : "استعراض المشتريات";
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select top 100 pucenter " + (BL.CLS_Session.lang.Equals("ع") ? "'مركز الشراء'" : "'Sal Center'") + " ,invtype " + (BL.CLS_Session.lang.Equals("ع") ? "'النوع'" : "'Type'") + ",ref " + (BL.CLS_Session.lang.Equals("ع") ? "'الرقم'" : "'Ref No'") + ",CONVERT(VARCHAR(10), CAST(invmdate as date), 105) " + (BL.CLS_Session.lang.Equals("ع") ? "'التاريخ'" : "'Date'") + " ,case when supno='' then text else su_name end " + (BL.CLS_Session.lang.Equals("ع") ? "'الوصف'" : "'Description'") + ",round(nettotal,2) " + (BL.CLS_Session.lang.Equals("ع") ? "'المبلغ'" : "'Amount'") + ",invtype a_t from pu_hdr left join suppliers on pu_hdr.supno=suppliers.su_no and pu_hdr.branch=suppliers.su_brno where branch='" + BL.CLS_Session.brno + "' and text like '%" + textBox1.Text.Replace(" ", "%") + "%' and branch + pucenter in(" + sl + ") order by released desc");
                   // dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select top 100 pucenter 'مركز الشراء' ,invtype 'النوع',ref 'الرقم',CONVERT(VARCHAR(10), CAST(invmdate as date), 105) 'التاريخ' ,case when supno='' then text else su_name end 'الوصف',round(nettotal,2) 'المبلغ',invtype a_t from pu_hdr left join suppliers on pu_hdr.supno=suppliers.su_no and pu_hdr.branch=suppliers.su_brno where branch='" + BL.CLS_Session.brno + "' and text like '%" + textBox1.Text.Replace(" ", "%") + "%' and branch + pucenter in(" + sl + ") order by invmdate desc");
                    dataGridView1.Columns[0].Width = 35;
                    dataGridView1.Columns[1].Width = 50;
                    dataGridView1.Columns[2].Width = 55;
                    dataGridView1.Columns[3].Width = 65;
                    dataGridView1.Columns[4].Width = 180;
                    // dataGridView1.Columns[0].Width = 75;
                    dataGridView1.Columns[5].Width = 60;
                    // dataGridView1.Columns[1].Width = 75;
                    dataGridView1.Columns["a_t"].Visible = false;
                    dataGridView1.RightToLeft = BL.CLS_Session.lang.Equals("E") ? RightToLeft.No : RightToLeft.Yes;
                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        //   rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02") ? "س قبض" : "س صرف";

                        rw.Cells[1].Value = datval.validate_trtypes(rw.Cells[1].Value.ToString());
                    }
                    break;

                case "16":
                    //   textBox1.Visible = false;
                    this.Text = BL.CLS_Session.lang.Equals("ع") ?"استعراض عروض الاسعار" : "Price Offers Browsing";

                   // dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select top 100 slcenter " + (BL.CLS_Session.lang.Equals("ع") ? "'مركز البيع'" : "'Sale Center'") + " ,invtype " + (BL.CLS_Session.lang.Equals("ع") ? "'النوع'" : "'Type'") + ",ref " + (BL.CLS_Session.lang.Equals("ع") ? "'الرقم'" : "'Ref. No'") + ",CONVERT(VARCHAR(10), CAST(invmdate as date), 105) " + (BL.CLS_Session.lang.Equals("ع") ? "'التاريخ'" : "'Date'") + " ,text " + (BL.CLS_Session.lang.Equals("ع") ? "'الوصف'" : "'Describtion'") + " ,round(nettotal,2) " + (BL.CLS_Session.lang.Equals("ع") ? "'المبلغ'" : "'Amount'") + ",invtype a_t from salofr_hdr where branch='" + BL.CLS_Session.brno + "'  and invtype='24' and text like '%" + textBox1.Text.Replace(" ", "%") + "%' order by invmdate desc");
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select top 100 slcenter " + (BL.CLS_Session.lang.Equals("ع") ? "'مركز البيع'" : "'Sale Center'") + " ,invtype " + (BL.CLS_Session.lang.Equals("ع") ? "'النوع'" : "'Type'") + ",ref " + (BL.CLS_Session.lang.Equals("ع") ? "'الرقم'" : "'Ref. No'") + ",CONVERT(VARCHAR(10), CAST(invmdate as date), 105) " + (BL.CLS_Session.lang.Equals("ع") ? "'التاريخ'" : "'Date'") + " ," + (checkBox1.Checked ? "remarks 'الملاحضات' " : "text 'الوصف'") + " ,round(nettotal,2) " + (BL.CLS_Session.lang.Equals("ع") ? "'المبلغ'" : "'Amount'") + ",invtype a_t from salofr_hdr where branch='" + BL.CLS_Session.brno + "' "+usrid+"  and invtype='24' and " + (checkBox1.Checked ? "remarks" : "text") + " like '%" + textBox1.Text.Replace(" ", "%") + "%' order by released desc");   
                    dataGridView1.Columns[0].Width = 35;
                    dataGridView1.Columns[1].Width = 50;
                    dataGridView1.Columns[2].Width = 55;
                    dataGridView1.Columns[3].Width = 65;
                    dataGridView1.Columns[4].Width = 180;
                    // dataGridView1.Columns[0].Width = 75;
                    dataGridView1.Columns[5].Width = 60;
                    // dataGridView1.Columns[1].Width = 75;
                    dataGridView1.Columns["a_t"].Visible = false;
                    textBox1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                    dataGridView1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        //   rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02") ? "س قبض" : "س صرف";

                        rw.Cells[1].Value = datval.validate_trtypes(rw.Cells[1].Value.ToString());
                    }
                    break;

                case "37":
                    //   textBox1.Visible = false;
                    this.Text = BL.CLS_Session.lang.Equals("ع") ? "استعراض طلبيات البيع" : "Sale Orders Browsing";
                    // dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select top 100 slcenter " + (BL.CLS_Session.lang.Equals("ع") ? "'مركز البيع'" : "'Sale Center'") + " ,invtype " + (BL.CLS_Session.lang.Equals("ع") ? "'النوع'" : "'Type'") + ",ref " + (BL.CLS_Session.lang.Equals("ع") ? "'الرقم'" : "'Ref. No'") + ",CONVERT(VARCHAR(10), CAST(invmdate as date), 105) " + (BL.CLS_Session.lang.Equals("ع") ? "'التاريخ'" : "'Date'") + " ,text " + (BL.CLS_Session.lang.Equals("ع") ? "'الوصف'" : "'Describtion'") + " ,round(nettotal,2) " + (BL.CLS_Session.lang.Equals("ع") ? "'المبلغ'" : "'Amount'") + ",invtype a_t from salofr_hdr where branch='" + BL.CLS_Session.brno + "'  and invtype='24' and text like '%" + textBox1.Text.Replace(" ", "%") + "%' order by invmdate desc");
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select top 100 slcenter " + (BL.CLS_Session.lang.Equals("ع") ? "'مركز البيع'" : "'Sale Center'") + " ,invtype " + (BL.CLS_Session.lang.Equals("ع") ? "'النوع'" : "'Type'") + ",ref " + (BL.CLS_Session.lang.Equals("ع") ? "'الرقم'" : "'Ref. No'") + ",CONVERT(VARCHAR(10), CAST(invmdate as date), 105) " + (BL.CLS_Session.lang.Equals("ع") ? "'التاريخ'" : "'Date'") + " ," + (checkBox1.Checked ? "remarks 'الملاحضات' " : "text 'الوصف'") + " ,round(nettotal,2) " + (BL.CLS_Session.lang.Equals("ع") ? "'المبلغ'" : "'Amount'") + ",invtype a_t from salofr_hdr where branch='" + BL.CLS_Session.brno + "' "+usrid+"  and invtype='37' and " + (checkBox1.Checked ? "remarks" : "text") + " like '%" + textBox1.Text.Replace(" ", "%") + "%' order by released desc");
                    dataGridView1.Columns[0].Width = 35;
                    dataGridView1.Columns[1].Width = 50;
                    dataGridView1.Columns[2].Width = 55;
                    dataGridView1.Columns[3].Width = 65;
                    dataGridView1.Columns[4].Width = 180;
                    // dataGridView1.Columns[0].Width = 75;
                    dataGridView1.Columns[5].Width = 60;
                    // dataGridView1.Columns[1].Width = 75;
                    dataGridView1.Columns["a_t"].Visible = false;
                    textBox1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                    dataGridView1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        //   rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02") ? "س قبض" : "س صرف";

                        rw.Cells[1].Value = datval.validate_trtypes(rw.Cells[1].Value.ToString());
                    }
                    break;

                case "17":
                    //   textBox1.Visible = false;
                    this.Text = this.Text = BL.CLS_Session.lang.Equals("E") ? "Orders View" : "استعراض طلبيات الشراء";
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select top 100 slcenter 'مركز الشراء' ,invtype 'النوع',ref 'الرقم',CONVERT(VARCHAR(10), CAST(invmdate as date), 105) 'التاريخ' ,text 'الوصف',round(nettotal,2) 'المبلغ',invtype a_t from salofr_hdr where branch='" + BL.CLS_Session.brno + "' and invtype='26' and text like '%" + textBox1.Text.Replace(" ", "%") + "%' order by released desc");
                   dataGridView1.Columns[0].Width = 35;
                    dataGridView1.Columns[1].Width = 50;
                    dataGridView1.Columns[2].Width = 55;
                    dataGridView1.Columns[3].Width = 65;
                    dataGridView1.Columns[4].Width = 180;
                    // dataGridView1.Columns[0].Width = 75;
                    dataGridView1.Columns[5].Width = 60;
                    // dataGridView1.Columns[1].Width = 75;
                    dataGridView1.Columns["a_t"].Visible = false;
                    dataGridView1.RightToLeft = RightToLeft.Yes;
                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        //   rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02") ? "س قبض" : "س صرف";

                        rw.Cells[1].Value = datval.validate_trtypes(rw.Cells[1].Value.ToString());
                    }
                    break;

                case "30":
                    //   textBox1.Visible = false;
                    this.Text = "استعراض سندات الصرف والاستلام";
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select top 100 whno 'مخزن', trtype 'النوع',ref 'الرقم',CONVERT(VARCHAR(10), CAST(trmdate as date), 105) 'التاريخ' ,text 'الوصف',round(amnttl,2) 'المبلغ',trtype a_t from sto_hdr where branch='" + BL.CLS_Session.brno + "' and trtype in ('31','32') and isbrtrx <> 1 and text like '%" + textBox1.Text.Replace(" ", "%") + "%' order by trmdate desc");
                    dataGridView1.Columns[0].Width = 35;
                    dataGridView1.Columns[1].Width = 50;
                    dataGridView1.Columns[2].Width = 55;
                    dataGridView1.Columns[3].Width = 65;
                    dataGridView1.Columns[4].Width = 180;
                    // dataGridView1.Columns[0].Width = 75;
                    dataGridView1.Columns[5].Width = 60;
                    // dataGridView1.Columns[1].Width = 75;
                    dataGridView1.Columns["a_t"].Visible = false;
                    dataGridView1.RightToLeft = RightToLeft.Yes;
                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        //   rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02") ? "س قبض" : "س صرف";

                        rw.Cells[1].Value = datval.validate_trtypes(rw.Cells[1].Value.ToString());
                    }
                    break;

                case "30-br":
                    //   textBox1.Visible = false;
                    this.Text = "استعراض سندات الصرف والاستلام";
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select top 100 whno 'مخزن', trtype 'النوع',ref 'الرقم',CONVERT(VARCHAR(10), CAST(trmdate as date), 105) 'التاريخ' ,text 'الوصف',round(amnttl,2) 'المبلغ',trtype a_t from sto_hdr where branch='" + BL.CLS_Session.brno + "' and trtype in ('32','31') and isbrtrx = 1 and text like '%" + textBox1.Text.Replace(" ", "%") + "%' order by trmdate desc");
                    dataGridView1.Columns[0].Width = 35;
                    dataGridView1.Columns[1].Width = 50;
                    dataGridView1.Columns[2].Width = 55;
                    dataGridView1.Columns[3].Width = 65;
                    dataGridView1.Columns[4].Width = 180;
                    // dataGridView1.Columns[0].Width = 75;
                    dataGridView1.Columns[5].Width = 60;
                    // dataGridView1.Columns[1].Width = 75;
                    dataGridView1.Columns["a_t"].Visible = false;
                    dataGridView1.RightToLeft = RightToLeft.Yes;
                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        //   rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02") ? "س قبض" : "س صرف";

                        rw.Cells[1].Value = datval.validate_trtypes(rw.Cells[1].Value.ToString());
                    }
                    break;
                case "33":
                    //   textBox1.Visible = false;
                    this.Text = "استعراض سندات التحويلات";
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select top 100 whno 'من مخزن', towhno 'الى مخزن',ref 'الرقم',CONVERT(VARCHAR(10), CAST(trmdate as date), 105) 'التاريخ' ,text 'الوصف',round(amnttl,2) 'المبلغ',trtype a_t from sto_hdr where branch='" + BL.CLS_Session.brno + "' and trtype in ('33') and text like '%" + textBox1.Text.Replace(" ", "%") + "%' order by trmdate desc");
                    dataGridView1.Columns[0].Width = 35;
                    dataGridView1.Columns[1].Width = 50;
                    dataGridView1.Columns[2].Width = 55;
                    dataGridView1.Columns[3].Width = 65;
                    dataGridView1.Columns[4].Width = 180;
                    // dataGridView1.Columns[0].Width = 75;
                    dataGridView1.Columns[5].Width = 60;
                    // dataGridView1.Columns[1].Width = 75;
                    dataGridView1.Columns["a_t"].Visible = false;
                    dataGridView1.RightToLeft = RightToLeft.Yes;
                    /*
                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        //   rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02") ? "س قبض" : "س صرف";

                        rw.Cells[1].Value = datval.validate_trtypes(rw.Cells[1].Value.ToString());
                    }
                     */
                    break;

                case "36":
                    //   textBox1.Visible = false;
                    this.Text = "استعراض سندات تسوية تكلفة الاصناف";
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select top 100 whno 'مخزن', trtype 'النوع',ref 'الرقم',CONVERT(VARCHAR(10), CAST(trmdate as date), 105) 'التاريخ' ,text 'الوصف',round(amnttl,2) 'المبلغ',trtype a_t from sto_hdr where branch='" + BL.CLS_Session.brno + "' and trtype in ('36') and isbrtrx <> 1 and text like '%" + textBox1.Text.Replace(" ", "%") + "%' order by trmdate desc");
                    dataGridView1.Columns[0].Width = 35;
                    dataGridView1.Columns[1].Width = 50;
                    dataGridView1.Columns[2].Width = 55;
                    dataGridView1.Columns[3].Width = 65;
                    dataGridView1.Columns[4].Width = 180;
                    // dataGridView1.Columns[0].Width = 75;
                    dataGridView1.Columns[5].Width = 60;
                    // dataGridView1.Columns[1].Width = 75;
                    dataGridView1.Columns["a_t"].Visible = false;
                    dataGridView1.RightToLeft = RightToLeft.Yes;
                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        //   rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02") ? "س قبض" : "س صرف";

                        rw.Cells[1].Value = datval.validate_trtypes(rw.Cells[1].Value.ToString());
                    }
                    break;

                case "45":
                    //   textBox1.Visible = false;
                    this.Text = "استعراض حركات تركيب وفك الاصناف";
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select top 100 whno 'من مخزن', towhno 'الى مخزن',ref 'الرقم',CONVERT(VARCHAR(10), CAST(trmdate as date), 105) 'التاريخ' ,text 'الوصف',round(amnttl,2) 'المبلغ',trtype a_t from sto_hdr where branch='" + BL.CLS_Session.brno + "' and trtype in ('45','46') and text like '%" + textBox1.Text.Replace(" ", "%") + "%' order by trmdate desc");
                    dataGridView1.Columns[0].Width = 35;
                    dataGridView1.Columns[1].Width = 50;
                    dataGridView1.Columns[2].Width = 55;
                    dataGridView1.Columns[3].Width = 65;
                    dataGridView1.Columns[4].Width = 180;
                    // dataGridView1.Columns[0].Width = 75;
                    dataGridView1.Columns[5].Width = 60;
                    // dataGridView1.Columns[1].Width = 75;
                    dataGridView1.Columns["a_t"].Visible = false;
                    dataGridView1.RightToLeft = RightToLeft.Yes;
                    /*
                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        //   rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02") ? "س قبض" : "س صرف";

                        rw.Cells[1].Value = datval.validate_trtypes(rw.Cells[1].Value.ToString());
                    }
                     */
                    break;

                case "8":
                //    SqlDataAdapter da2 = new SqlDataAdapter("select user_name,full_name from users where user_name like '%" + textBox1.Text + "%' or full_name like '%" + textBox1.Text + "%'", con);
            //"item_no like '%" + textBox1.Text + "%' or item_name like '%"+textBox1.Text+"%'"
            // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from hdr where ref=(select max(ref) from hdr)", con);
            // DataSet1 ds = new DataSet1();
          //  DataTable dt2 = new DataTable();
          //  da2.Fill(dt2);
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select user_name 'اسم المستخدم',full_name 'الاسم الكامل' from users where user_name like '%" + textBox1.Text + "%' or full_name like '%" + textBox1.Text + "%'");
                    dataGridView1.Columns[0].Width = 150;
                    dataGridView1.RightToLeft = RightToLeft.Yes;
                   break;

                case "8-1":
                   //    SqlDataAdapter da2 = new SqlDataAdapter("select user_name,full_name from users where user_name like '%" + textBox1.Text + "%' or full_name like '%" + textBox1.Text + "%'", con);
                   //"item_no like '%" + textBox1.Text + "%' or item_name like '%"+textBox1.Text+"%'"
                   // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from hdr where ref=(select max(ref) from hdr)", con);
                   // DataSet1 ds = new DataSet1();
                   //  DataTable dt2 = new DataTable();
                   //  da2.Fill(dt2);
                   dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select sl_id 'رقم البائع',sl_name 'اسم البائع' from pos_salmen where sl_name like '%" + textBox1.Text + "%' and sl_brno ='"+BL.CLS_Session.brno+"'");
                   dataGridView1.Columns[0].Width = 150;
                   dataGridView1.RightToLeft = RightToLeft.Yes;
                   break;

                case "chkb":
                   //    SqlDataAdapter da2 = new SqlDataAdapter("select user_name,full_name from users where user_name like '%" + textBox1.Text + "%' or full_name like '%" + textBox1.Text + "%'", con);
                   //"item_no like '%" + textBox1.Text + "%' or item_name like '%"+textBox1.Text+"%'"
                   // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from hdr where ref=(select max(ref) from hdr)", con);
                   // DataSet1 ds = new DataSet1();
                   //  DataTable dt2 = new DataTable();
                   //  da2.Fill(dt2);
                   textBox1.Visible = false;
                   dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select a.barcode 'الباركود',b.unit_name 'الوحدة' ,a.pk_qty 'الشد' , a.price 'السعر' from items_bc a,units b where a.pack=b.unit_id and sl_no='"+BL.CLS_Session.sl_prices+"' and a.item_no ='" + condition + "' order by a.pkorder");
                   //  dataGridView1.Columns[0].Width = 150;
                   dataGridView1.RightToLeft = RightToLeft.Yes;
                   break;

                case "chkalter":
                   //    SqlDataAdapter da2 = new SqlDataAdapter("select user_name,full_name from users where user_name like '%" + textBox1.Text + "%' or full_name like '%" + textBox1.Text + "%'", con);
                   //"item_no like '%" + textBox1.Text + "%' or item_name like '%"+textBox1.Text+"%'"
                   // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from hdr where ref=(select max(ref) from hdr)", con);
                   // DataSet1 ds = new DataSet1();
                   //  DataTable dt2 = new DataTable();
                   //  da2.Fill(dt2);
                   textBox1.Visible = false;
                   dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select a.rplitemno 'رقم الصنف',b.item_name 'اسم الصنف'  , b.item_price 'السعر',b.item_cbalance " + (BL.CLS_Session.lang.Equals("ع") ? "'الرصيد'" : "'Balance'") + "  from items_altr a,items b where a.rplitemno=b.item_no and a.itemno ='" + condition + "' order by a.iorder");
                   //dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select a.rplitemno 'رقم الصنف',b.item_name 'اسم الصنف'  , b.item_price 'السعر',a.iorder 'ترتيب الاهمية'  from items_altr a,items b where a.rplitemno=b.item_no and a.itemno ='" + condition + "' and (a.rplitemno like '%" + textBox1.Text + "%' or b.item_name like '%" + textBox1.Text + "%') order by a.iorder");
                   //  dataGridView1.Columns[0].Width = 150;
                   dataGridView1.Columns[1].Width = 250;
                   dataGridView1.RightToLeft = RightToLeft.Yes;
                   break;

                case "Acc-1":
                   //    SqlDataAdapter da2 = new SqlDataAdapter("select user_name,full_name from users where user_name like '%" + textBox1.Text + "%' or full_name like '%" + textBox1.Text + "%'", con);
                   //"item_no like '%" + textBox1.Text + "%' or item_name like '%"+textBox1.Text+"%'"
                   // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from hdr where ref=(select max(ref) from hdr)", con);
                   // DataSet1 ds = new DataSet1();
                   //  DataTable dt2 = new DataTable();
                   //  da2.Fill(dt2);
                   dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select a_key " + (BL.CLS_Session.lang.Equals("ع") ? "'رقم الحساب'" : "'Account No'") + ",a_name " + (BL.CLS_Session.lang.Equals("ع") ? "'اسم الحساب'" : "'Account Name'") + ",acckind acckind from accounts");
                   dataGridView1.Columns[0].Width = 150;
                   dataGridView1.Columns[2].Visible = false;
                   dataGridView1.RightToLeft = RightToLeft.Yes;
                   break;
                
               
            }
            dataGridView1.ClearSelection();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection(@"Data Source=RYD1-PC\INFOSOFT12;" +
            //                                      "Initial Catalog=DBASE;" +
            //                                      "User id=sa;" +
            //                                      "Password=infocic;");
           
                switch (strkey)
                {
                    case "1":
                        string tr = !string.IsNullOrEmpty(BL.CLS_Session.acckey) ? BL.CLS_Session.acckey.Replace(" ", "','").Remove(0, 2) + "'" : " ";
                        string conkey = !string.IsNullOrEmpty(BL.CLS_Session.acckey) ? " and a_key in(" + tr + ")" : " ";
                        //  SqlDataAdapter da = new SqlDataAdapter("select a_key 'رقم الحساب',a_name 'اسم الحساب' from accounts where a_active=1 and a_key + a_name like '%" + textBox1.Text.Replace(" ","%") + "%' and a_brno='"+BL.CLS_Session.brno+"'", con);
                        DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select a_key " + (BL.CLS_Session.lang.Equals("ع") ? "'رقم الحساب'" : "'Account No'") + ",a_name " + (BL.CLS_Session.lang.Equals("ع") ? "'اسم الحساب'" : "'Account Name'") + ",acckind acckind from accounts where a_active=1 and accontrol=0 and a_key + a_name like '%" + textBox1.Text.Replace(" ", "%") + "%' and a_brno='" + BL.CLS_Session.brno + "'" + conkey + "");
                        // da.Fill(dt);

                        dataGridView1.DataSource = dt;
                        dataGridView1.Columns[0].Width = 150;
                        dataGridView1.Columns[2].Visible = false;
                        textBox1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                        dataGridView1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;

                        /*
                        foreach (DataRow row in dt.Rows)
                        {
                            ListViewItem item = new ListViewItem(row[0].ToString());
                            for (int i = 1; i < dt.Columns.Count; i++)
                            {
                                item.SubItems.Add(row[i].ToString());
                            }
                            listView1.Items.Add(item);
                        }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dt.Rows[i];
                            ListViewItem listitem = new ListViewItem(dr[0].ToString().PadLeft(3));
                            listitem.SubItems.Add("| " + dr[1].ToString().PadLeft(3));
                            //listitem.SubItems.Add("| " + dr[2].ToString());
                            //listitem.SubItems.Add("| " + dr[3].ToString());
                            //listitem.SubItems.Add("| " + dr[4].ToString());
                            //listitem.SubItems.Add("| " + dr[5].ToString());
                            listView2.Items.Add(listitem);
                        }
                        */
                        break;

                    case "Grd":

                        dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select Item_no رقم_الصنف,Item_Name اسم_الصنف,item_price سعر_البيع,item_Barcode i_b,item_unit ui from items where Item_no + item_Name  like '%" + textBox1.Text.Replace(" ", "%") + "%'");
                        

                        
                        dataGridView1.Columns[0].FillWeight = 30;
                        dataGridView1.Columns[1].FillWeight = 40;
                        dataGridView1.Columns[2].FillWeight = 15;
                        dataGridView1.Columns[3].Visible = false;
                        dataGridView1.Columns[4].Visible = false;
                       // dataGridView1.Columns[4].FillWeight = 15;
                        textBox1.RightToLeft = RightToLeft.Yes;
                        dataGridView1.RightToLeft = RightToLeft.Yes;

                        break;


                    case "Acc-1":
                       // string tr = !string.IsNullOrEmpty(BL.CLS_Session.acckey) ? BL.CLS_Session.acckey.Replace(" ", "','").Remove(0, 2) + "'" : " ";
                       // string conkey = !string.IsNullOrEmpty(BL.CLS_Session.acckey) ? " and a_key in(" + tr + ")" : " ";
                        //  SqlDataAdapter da = new SqlDataAdapter("select a_key 'رقم الحساب',a_name 'اسم الحساب' from accounts where a_active=1 and a_key + a_name like '%" + textBox1.Text.Replace(" ","%") + "%' and a_brno='"+BL.CLS_Session.brno+"'", con);
                        DataTable dta = daml.SELECT_QUIRY_only_retrn_dt("select a_key " + (BL.CLS_Session.lang.Equals("ع") ? "'رقم الحساب'" : "'Account No'") + ",a_name " + (BL.CLS_Session.lang.Equals("ع") ? "'اسم الحساب'" : "'Account Name'") + ",acckind acckind from accounts where  a_key + a_name like '%" + textBox1.Text.Replace(" ", "%") + "%'");
                        // da.Fill(dt);

                        dataGridView1.DataSource = dta;
                        dataGridView1.Columns[0].Width = 150;
                        dataGridView1.Columns[2].Visible = false;
                        textBox1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                        dataGridView1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;

                        
                        
                        break;

                    case "1-1":
                        string tr2 = !string.IsNullOrEmpty(BL.CLS_Session.acckey) ? BL.CLS_Session.acckey.Replace(" ", "','").Remove(0, 2) + "'" : " ";
                        string conkey2 = !string.IsNullOrEmpty(BL.CLS_Session.acckey) ? " and a_key in(" + tr2 + ")" : " ";
                        //  SqlDataAdapter da = new SqlDataAdapter("select a_key 'رقم الحساب',a_name 'اسم الحساب' from accounts where a_active=1 and a_key + a_name like '%" + textBox1.Text.Replace(" ","%") + "%' and a_brno='"+BL.CLS_Session.brno+"'", con);
                        DataTable dta1 = daml.SELECT_QUIRY_only_retrn_dt("select a_key " + (BL.CLS_Session.lang.Equals("ع") ? "'رقم الحساب'" : "'Account No'") + ",a_name " + (BL.CLS_Session.lang.Equals("ع") ? "'اسم الحساب'" : "'Account Name'") + " from accounts where a_active=1 and accontrol=1 and a_key + a_name like '%" + textBox1.Text.Replace(" ", "%") + "%' and a_brno='" + BL.CLS_Session.brno + "'" + conkey2 + "");
                        // da.Fill(dt);

                        dataGridView1.DataSource = dta1;
                        dataGridView1.Columns[0].Width = 150;
                        textBox1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                        dataGridView1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                        /*
                        foreach (DataRow row in dta1.Rows)
                        {
                            ListViewItem item = new ListViewItem(row[0].ToString());
                            for (int i = 1; i < dta1.Columns.Count; i++)
                            {
                                item.SubItems.Add(row[i].ToString());
                            }
                            listView1.Items.Add(item);
                        }

                        for (int i = 0; i < dta1.Rows.Count; i++)
                        {
                            DataRow dr = dta1.Rows[i];
                            ListViewItem listitem = new ListViewItem(dr[0].ToString().PadLeft(3));
                            listitem.SubItems.Add("| " + dr[1].ToString().PadLeft(3));
                            //listitem.SubItems.Add("| " + dr[2].ToString());
                            //listitem.SubItems.Add("| " + dr[3].ToString());
                            //listitem.SubItems.Add("| " + dr[4].ToString());
                            //listitem.SubItems.Add("| " + dr[5].ToString());
                            listView2.Items.Add(listitem);
                        }
                        */
                        break;

                    case "M":
                        //  textBox1.Visible = false;
                        dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select cc_id 'رقم مركز التكلفة',cc_name 'اسم مركز التكلفة' from costcenters where cc_id + cc_name like '%" + textBox1.Text.Replace(" ", "%") + "%' and cc_brno='" + BL.CLS_Session.brno + "' ");
                        dataGridView1.Columns[0].FillWeight = 30;
                        dataGridView1.Columns[1].FillWeight = 70;
                        textBox1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                        dataGridView1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                        break;

                    case "SC":
                        //  textBox1.Visible = false;
                        dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select sc_code 'رقم القسم /القطاع',sc_name 'اسم القسم / القطاع' from sctions where sc_code + sc_name like '%" + textBox1.Text.Replace(" ", "%") + "%' and sc_brno='" + BL.CLS_Session.brno + "' ");
                        dataGridView1.Columns[0].FillWeight = 30;
                        dataGridView1.Columns[1].FillWeight = 70;
                        textBox1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                        dataGridView1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                        break;

                    case "2":
                        if (textBox1.Text.Length >= 3)
                        {
                            if(BL.CLS_Session.showqtysrch && !condition.Equals("Pos"))
                                cond1 =  ",[dbo].[get_item_bal_by_whno] (a.item_no,'" + condition + "','" + BL.CLS_Session.brno + "') 'الرصيد' ";
                            else
                                cond1 = ",'' 'الرصيد' ";
                           // SqlDataAdapter da2 = new SqlDataAdapter("select item_no " + (BL.CLS_Session.lang.Equals("ع") ? "'رقم الصنف'" : "'Item No'") + ",item_name " + (BL.CLS_Session.lang.Equals("ع") ? "'اسم الصنف'" : "'Item Name'") + ",item_price " + (BL.CLS_Session.lang.Equals("ع") ? "'سعر البيع'" : "'Sale Price'") + ",item_barcode i_b,item_cbalance " + (BL.CLS_Session.lang.Equals("ع") ? "'الرصيد'" : "'Balance'") + " from items where inactive=0 and item_no + item_name + item_ename like '%" + textBox1.Text.Replace(" ", "%") + "%'" + (condition.Equals("Sto2") ? " and item_cost=2 " : "") + "", con);
                          //  SqlDataAdapter da2 = new SqlDataAdapter("select item_no " + (BL.CLS_Session.lang.Equals("ع") ? "'رقم الصنف'" : "'Item No'") + ",item_name " + (BL.CLS_Session.lang.Equals("ع") ? "'اسم الصنف'" : "'Item Name'") + ",item_price " + (BL.CLS_Session.lang.Equals("ع") ? "'سعر البيع'" : "'Sale Price'") + ",item_barcode i_b from items where inactive=0 and item_no + item_name + item_ename like '%" + textBox1.Text.Replace(" ", "%") + "%'" + (condition.Equals("Sto2") ? " and item_cost=2 " : "") + "", con);
                            SqlDataAdapter da2 = new SqlDataAdapter("select a.item_no " + (BL.CLS_Session.lang.Equals("ع") ? "'رقم الصنف'" : "'Item No'") + "," + (BL.CLS_Session.lang.Equals("E")? "a.item_ename ": "a.item_name ") +  (BL.CLS_Session.lang.Equals("ع") ? "'اسم الصنف'" : "'Item Name'") + ",b.price " + (BL.CLS_Session.lang.Equals("ع") ? "'سعر البيع'" : "'Sale Price'") + ",b.barcode i_b "+cond1+" from items a join items_bc b on a.item_no=b.item_no and a.item_barcode=b.barcode and b.sl_no='" + BL.CLS_Session.sl_prices + "' where a.inactive=0 and a.item_no + a.item_name + a.item_ename like '%" + textBox1.Text.Replace(" ", "%") + "%'" + (condition.Equals("Sto2") ? " and a.item_cost=2 " : "") + "", con);
                            // SqlDataAdapter da2 = new SqlDataAdapter("select item_no " + (BL.CLS_Session.lang.Equals("ع") ? "'رقم الصنف'" : "'Item No'") + ",item_name " + (BL.CLS_Session.lang.Equals("ع") ? "'اسم الصنف'" : "'Item Name'") + ",item_price " + (BL.CLS_Session.lang.Equals("ع") ? "'سعر البيع'" : "'Sale Price'") + ",item_barcode i_b,item_cbalance " + (BL.CLS_Session.lang.Equals("ع") ? "'الرصيد'" : "'Balance'") + " from items where inactive=0 and item_no + item_name + item_ename like '%" + textBox1.Text + "%'" + (condition.Equals("Sto2") ? " and item_cost=2 " : "") + "", con);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);

                            dataGridView1.DataSource = dt2;
                            dataGridView1.Columns[0].FillWeight = 30;
                            dataGridView1.Columns[1].FillWeight = 55;
                            dataGridView1.Columns[2].FillWeight = 15;
                            dataGridView1.Columns[3].Visible = false;
                          //  dataGridView1.Columns[4].FillWeight = 15;
                            dataGridView1.Columns[4].Visible =BL.CLS_Session.showqtysrch ?true : false ;
                            dataGridView1.Columns[4].FillWeight = 15;

                            textBox1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                            dataGridView1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                           // dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 
                        }
                        break;

                    case "2-2":
                        //item_no " + (BL.CLS_Session.lang.Equals("ع") ? "'رقم الصنف'" : "'Item No'") + ",item_name " + (BL.CLS_Session.lang.Equals("ع") ? "'اسم الصنف'" : "'Item Name'") + ",item_price " + (BL.CLS_Session.lang.Equals("ع") ? "'سعر البيع'" : "'Sale Price'") + ",item_barcode i_b
                        //  SqlDataAdapter da22 = new SqlDataAdapter("select item_no 'رقم الصنف',item_name 'اسم الصنف',item_price 'سعر البيع',item_barcode i_b from items where item_no + item_name like '%" + textBox1.Text.Replace(" ", "%") + "%'", con);
                        if (BL.CLS_Session.showqtysrch && !condition.Equals("Pos"))
                            cond1 = ",[dbo].[get_item_bal_by_whno] (a.item_no,'" + condition + "','" + BL.CLS_Session.brno + "') 'الرصيد' ";
                        else
                            cond1 = ",'' 'الرصيد' ";
                        SqlDataAdapter da22 = new SqlDataAdapter("select a.item_no " + (BL.CLS_Session.lang.Equals("ع") ? "'رقم الصنف'" : "'Item No'") + "," + (BL.CLS_Session.lang.Equals("E") ? "item_ename " : "item_name ") + (BL.CLS_Session.lang.Equals("ع") ? "'اسم الصنف'" : "'Item Name'") + ",item_price " + (BL.CLS_Session.lang.Equals("ع") ? "'سعر البيع'" : "'Sale Price'") + ",item_barcode i_b " + cond1 + " from items a where inactive=0 and a.item_no + item_name + item_ename like '%" + textBox1.Text.Replace(" ", "%") + "%'", con);

                        DataTable dt22 = new DataTable();
                        da22.Fill(dt22);

                        dataGridView1.DataSource = dt22;
                        dataGridView1.Columns[0].FillWeight = 30;
                        dataGridView1.Columns[1].FillWeight = 55;
                        dataGridView1.Columns[2].FillWeight = 15;
                        dataGridView1.Columns[3].Visible = false;
                      //  dataGridView1.Columns[4].FillWeight = 15;
                         dataGridView1.Columns[4].Visible =BL.CLS_Session.showqtysrch ?true : false ;
                         dataGridView1.Columns[4].FillWeight = 15;

                        textBox1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                        dataGridView1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                        break;

                    case "5":
                        SqlDataAdapter da3 = new SqlDataAdapter("select cu_no as " + (BL.CLS_Session.lang.Equals("ع") ? "'رقم العميل'" : "'Customer No'") + ",cu_name as " + (BL.CLS_Session.lang.Equals("ع") ? "'اسم العميل'" : "'Customer No'") + ",cu_tel as " + (BL.CLS_Session.lang.Equals("ع") ? "'رقم الهاتف'" : "'Tel. No.'") + ",cl_acc,cu_no as cn,vndr_taxcode,cu_disc,cu_kind from customers a join cuclass b on a.cu_class=b.cl_no  where a.inactive=0 and a.cu_brno='" + BL.CLS_Session.brno + "' and cu_no + cu_name + cu_tel + vndr_taxcode like '%" + textBox1.Text.Replace(" ", "%") + "%' ", con); //order by cast(cu_no as int)
                        DataTable dt3 = new DataTable();
                        da3.Fill(dt3);

                        dataGridView1.DataSource = dt3;

                        dataGridView1.Columns[3].Visible = false;
                        dataGridView1.Columns[4].Visible = false;
                        dataGridView1.Columns[5].Visible = false;
                        dataGridView1.Columns[6].Visible = false;
                        dataGridView1.Columns[7].Visible = false;
                        dataGridView1.Columns[0].FillWeight = 20;
                        dataGridView1.Columns[1].FillWeight = 50;
                        dataGridView1.Columns[2].FillWeight = 30;
                        textBox1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                        dataGridView1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                        break;

                    case "5-r":
                        SqlDataAdapter da3r = new SqlDataAdapter("select sp_id as " + (BL.CLS_Session.lang.Equals("ع") ? "'رقم الموصل'" : "'Customer No'") + ",sp_name as " + (BL.CLS_Session.lang.Equals("ع") ? "'اسم الموصل'" : "'Customer No'") + " from pos_drivers a where a.sp_brno='" + BL.CLS_Session.brno + "' and sp_id + sp_name like '%" + textBox1.Text.Replace(" ", "%") + "%' order by cast(sp_id as int)", con);
                        DataTable dt3r = new DataTable();
                        da3r.Fill(dt3r);

                        dataGridView1.DataSource = dt3r;

                        //dataGridView1.Columns[3].Visible = false;
                        //dataGridView1.Columns[4].Visible = false;
                        //dataGridView1.Columns[5].Visible = false;
                        dataGridView1.Columns[0].FillWeight = 20;
                        dataGridView1.Columns[1].FillWeight = 50;
                        //dataGridView1.Columns[2].FillWeight = 30;
                        textBox1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                        dataGridView1.RightToLeft = BL.CLS_Session.lang.Equals("ع") ? RightToLeft.Yes : RightToLeft.No;
                        break;

                    case "6":
                        Form7_Load(null, null);
                        break;

                    case "7":
                        SqlDataAdapter da7 = new SqlDataAdapter("select su_no as " + (BL.CLS_Session.lang.Equals("ع") ? "'رقم المورد'" : "'Supplier No'") + ",su_name as " + (BL.CLS_Session.lang.Equals("ع") ? "'اسم المورد'" : "'Supplier No'") + ",su_no as sn,cl_acc,vndr_taxcode from suppliers a join suclass b on a.su_class=b.cl_no  where a.inactive=0 and a.su_brno='" + BL.CLS_Session.brno + "' and su_no + su_name + vndr_taxcode like '%" + textBox1.Text.Replace(" ", "%") + "%' order by cast(su_no as int)", con);
                        DataTable dt7 = new DataTable();
                        da7.Fill(dt7);

                        dataGridView1.DataSource = dt7;

                        dataGridView1.Columns[2].Visible = false;
                        dataGridView1.Columns[3].Visible = false;
                        dataGridView1.Columns[4].Visible = false;
                        dataGridView1.Columns[0].Width = 100;
                        dataGridView1.RightToLeft = RightToLeft.Yes;
                        break;

                    case "10":
                        Form7_Load(null, null);
                        break;

                    case "16":
                        Form7_Load(null, null);
                        break;

                    case "3":
                        Form7_Load(null, null);
                        break;
                    //case "chkalter":
                    //    Form7_Load(null, null);
                    //    break;

                    //case "5":
                    //  //  textBox1.Visible = false;
                    //    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select * from items ");

                    //    break;
                    /*
                case "9":
                    SqlDataAdapter da9 = new SqlDataAdapter("select su_no as 'رقم المورد',su_name as 'اسم المورد',su_no as cn,cl_acc from suppliers a join cuclass b on a.su_class=b.cl_no  where a.inactive=0 and a.su_brno='" + BL.CLS_Session.brno + "' and su_no + su_name like '%" + textBox1.Text.Replace(" ", "%") + "%'", con);
                    DataTable dt9 = new DataTable();
                    da9.Fill(dt9);

                    dataGridView1.DataSource = dt9;

                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[3].Visible = false;
                    dataGridView1.Columns[0].Width = 100;
                    dataGridView1.RightToLeft = RightToLeft.Yes;
                    break;
                    */

                }
            

            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.KeyCode == Keys.Tab)
                //{
                //   // SendKeys.Send("{DOWN}");
                //    //  dataGridView1.Select();
                //      this.Close();
                //    //   dataGridView1.Select();
                //    //   dataGridView1.Rows[0].Selected = true;
                //}

                if (e.KeyCode == Keys.Down)
                {
                    // var selectedCell = dataGridView1.SelectedCells[0];
                    // do something with selectedCell...



                    //dataGridView1.Select();
                    //dataGridView1.CurrentCell = dataGridView1[0, 0];
                    dataGridView1.Focus();
                    dataGridView1.Rows[0].Selected = true;
                    // dataGridView1.CurrentCell = dataGridView1[0, 0];
                }

            }
            catch { }
       
            //if (e.KeyCode == Keys.Enter)
            //{
            //    // var selectedCell = dataGridView1.SelectedCells[0];
            //    // do something with selectedCell...


            //    try
            //    {
            //        this.Hide();
            //    }
            //    catch { }
            //}
            //if (e.KeyCode == Keys.Enter)
           
           // KeyDown_Enter(sender, e);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    // var selectedCell = dataGridView1.SelectedCells[0];
            //    // do something with selectedCell...


            //    try
            //    {
            //        this.Hide();
            //    }
            //    catch { }
            //}
            // if (e.KeyCode == Keys.Enter)
            if (e.KeyCode == Keys.Tab)
            {
                // var selectedCell = dataGridView1.SelectedCells[0];
                // do something with selectedCell...
               // dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.CurrentRow.Index - 1];
              //  this.Close();

                try
               {
                 //  SendKeys.Send("{UP}");
                 //  this.Close();
                   ////////int rowindex = dataGridView1.CurrentRow.Index;
                   //////////  MessageBox.Show(rowindex.ToString());
                   //////////if (dataGridView1.CurrentCell.Equals(dataGridView1[0, 0]))
                   ////////if (rowindex != 0)
                   ////////{
                   ////////    //   fir = 1;
                   ////////    dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.CurrentRow.Index - 1];
                   ////////    // dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.CurrentRow.Index != 0 ? dataGridView1.CurrentRow.Index - 1 : 0];                       
                   ////////    this.Close();
                   ////////    //  //  dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.CurrentRow.Index - 1];                       
                   ////////    //    this.Close();
                   ////////}
                   ////////else
                   ////////{
                   ////////    //   fir = 0;
                   ////////    // dataGridView1.Rows[0].Selected = true;
                   ////////    dataGridView1.CurrentCell = dataGridView1[0, 0];
                      // SendKeys.Send("{UP}");
                       this.Close();
                   ////////}
                }
                catch { }
             }
          //  KeyDown_Enter(sender, e);
        }

        private void KeyDown_Enter(object sender, KeyEventArgs e)
        {
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //listView1.Columns.Add("key", 70);
            //listView1.Columns.Add("name", 70);
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
          //  this.Close();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
           
        }
        public bool ClosedByXButtonOrAltF4 { get; private set; }
        private const int SC_CLOSE = 0xF060;
        private const int WM_SYSCOMMAND = 0x0112;
        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == WM_SYSCOMMAND && msg.WParam.ToInt32() == SC_CLOSE)
                ClosedByXButtonOrAltF4 = true;
            base.WndProc(ref msg);
        }
        protected override void OnShown(EventArgs e)
        {
            ClosedByXButtonOrAltF4 = false;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (ClosedByXButtonOrAltF4)
                ////// MessageBox.Show("Closed by X or Alt+F4");
                {
                    //dataGridView1.ClearSelection();
                    dataGridView1.DataSource = null;
                }
            }
            catch { }
           // else
           //    MessageBox.Show("Closed by calling Close()");
        }

        private void Search_All_FormClosing(object sender, FormClosingEventArgs e)
        {
          //  if (string.Equals((sender as Button).Name, @"CloseButton"))
            ////if (msg
            ////{
                
            ////}
            ////else
            ////{
            ////    dataGridView1.CurrentRow.Cells[0].Value = null;
            ////}
        }

        private void Search_All_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.Close();
            //}
        }

        private void dataGridView1_Enter(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
                dataGridView1.Rows[0].Selected = true;
            else
                this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
                dataGridView1.Columns[4].HeaderText = "الملاحضات";
            else
                dataGridView1.Columns[4].HeaderText = "الوصف";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {


            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                XcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    if (dataGridView1.Columns[i - 1].Visible == true)
                    {
                        XcelApp.Cells[1, i] = "'" + dataGridView1.Columns[i - 1].HeaderText;
                    }
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Visible == true)
                            XcelApp.Cells[i + 2, j + 1] = "'" + dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }

                }
                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
           
        }

        private void chk_enar_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_enar.Checked)
            {
                BL.CLS_Session.temp_lang = "en";
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("EN"));
                textBox1.Focus();
            }
            else
            {
                BL.CLS_Session.temp_lang = "ar";
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
                textBox1.Focus();
            }
        }

    }
}
