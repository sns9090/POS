using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS
{
    public partial class Search_by_No_Copy : Form
    {
        DataTable dtbrs;
        string lang, comp_log, compyr_log, user_log, branch_log, is_rmbr, is_rmbr_pass;
        string ser, uid, pass, timout;
       // public SqlConnection con4;
        SqlConnection con3=BL.DAML.con;
        public string dbco,brco;
        BL.DAML daml = new BL.DAML();
        BL.EncDec ende = new BL.EncDec();
        string skey;
        public Search_by_No_Copy(string key)
        {
            InitializeComponent();
            skey = key;
        }

        private void Search_by_No_Load(object sender, EventArgs e)
        {

           // cmb_user.Enabled = false;
           // txt_pass.Enabled = false;
           // cmb_brno.Enabled = false;

           // var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\ServerSetting.txt");
            var lines_login = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\last_login.txt");

           // var linescontr = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\temp.txt");
           // BL.CLS_Session.contr_id = linescontr.First().ToString();
           // BL.CLS_Session.ctrno = linescontr.First().ToString();

            comp_log = lines_login.First().ToString();
            compyr_log = lines_login.Skip(1).First().ToString();
            user_log = lines_login.Skip(2).First().ToString();
            branch_log = lines_login.Skip(3).First().ToString();
           


           // con3 = new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + BL.CLS_Session.sqldb + ";User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=" + BL.CLS_Session.sqlcontimout + "");
            //  con3 = new SqlConnection(@"Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=|DataDirectory|\DBL\dbmstr.mdf");

            Fillcombo();
               





            if (skey == "01")
            {/*
                comboBox1.DisplayMember = "Text";
                comboBox1.ValueMember = "Value";

                var items = new[] { new { Text = "قيد عام", Value = "01" }, new { Text = "سند قبض", Value = "02" }, new { Text = "سند صرف", Value = "03" } };
                comboBox1.DataSource = items;
                comboBox1.SelectedIndex = -1;
              */
            }
            if (skey == "02")
            {/*
                comboBox1.DisplayMember = "Text";
                comboBox1.ValueMember = "Value";

                var items = new[] { new { Text = "سند قبض", Value = "02" }};
                comboBox1.DataSource = items;
                comboBox1.SelectedIndex = 0;
              */
                textBox1.Select();
            }
            if (skey == "03")
            {/*
                comboBox1.DisplayMember = "Text";
                comboBox1.ValueMember = "Value";

                var items = new[] { new { Text = "سند صرف", Value = "03" } };
                comboBox1.DataSource = items;
                comboBox1.SelectedIndex = 0;
              * */
                textBox1.Select();
            }

            if (skey == "04")
            {
                var stn = new Sal.Sal_Tran_notax("","","");
                cmb_salctr.Visible = true;

              //  comboBox1.Items.Add(new { Text = "نسخ من عرض سعر", Value = "24" });
                /*
                DataTable dtsal = daml.SELECT_QUIRY_only_retrn_dt("Select * from slcenters where sl_brno=" + BL.CLS_Session.brno);
                cmb_salctr.DataSource = dtsal;
                cmb_salctr.DisplayMember = "sl_name";
                cmb_salctr.ValueMember = "sl_no";
                cmb_salctr.SelectedIndex = 0;
                */
                //comboBox1.DisplayMember = "Text";
                //comboBox1.ValueMember = "Value";

                //var items = new[] { new { Text = "مبيعات نقدية", Value = "04" }, new { Text = "مبيعات اجله", Value = "05" }, new { Text = "مرتجع نقدي", Value = "14" }, new { Text = "مرتجع اجل", Value = "15" } };
                //comboBox1.DataSource = items;
                //comboBox1.SelectedIndex = -1;
              //  comboBox1.DataSource = stn.comboBox1.DataSource;
            }
            if (skey == "07")
            {
                var stn = new Pur.Pur_Tran_D("", "", "");
                cmb_salctr.Visible = true;
            }

            if (skey == "30")
            {
                var stn = new Sto.Sto_ImpExp("", "", "");
                cmb_salctr.Visible = false;
                label9.Visible = false;
            }

           // comboBox1.FlatStyle = FlatStyle.Flat;
        }

        void Fillcombo()
        {
            SqlDataAdapter da;
            if (!File.Exists(Environment.CurrentDirectory + @"\local.txt"))
                da = new SqlDataAdapter("select comp_id,comp_name from dbmstr.dbo.companys", con3);
            else
                da = new SqlDataAdapter("select comp_id,comp_name from companys", con3);

            DataTable dt = new DataTable();

            da.Fill(dt);
            // cmb_company.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select comp_id,comp_name from companys");
            cmb_company.DataSource = dt;
            // comboBox1.ValueMember = "username";
            cmb_company.DisplayMember = "comp_name";
            cmb_company.ValueMember = "comp_id";
            cmb_company.SelectedValue = comp_log;
            

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                this.Close();

            }


        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox1.Focus();
            }
        }

        private void cmb_salctr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_type.Focus();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBox1.Checked && checkBox1.Text.Equals("برقم فاتورة المورد"))
            //    txt_sno.Visible = true;
            //else
            //    txt_sno.Visible = false;

            textBox1.Select();
        }

        private void txt_sno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                this.Close();


            }
        }

        private void cmb_year_Enter(object sender, EventArgs e)
        {
            SqlDataAdapter da;
            if (!File.Exists(Environment.CurrentDirectory + @"\local.txt"))
                da = new SqlDataAdapter("select comp_id,yrcode from dbmstr.dbo.years where comp_id='" + cmb_company.SelectedValue + "' order by yrcode DESC", con3);
            else
                da = new SqlDataAdapter("select comp_id,yrcode from years where comp_id='" + cmb_company.SelectedValue + "' order by yrcode DESC", con3);

            DataTable dt = new DataTable();

            da.Fill(dt);
            cmb_year.DataSource = dt;
            // cmb_year.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select comp_id,yrcode from years where comp_id='"+cmb_company.SelectedValue+"'");
            // comboBox1.ValueMember = "username";
            cmb_year.DisplayMember = "yrcode";
            cmb_year.ValueMember = "yrcode";
            cmb_year.SelectedValue = compyr_log;

           // SendKeys.Send("{Enter}");
        }

        private void cmb_year_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_brno.Focus();
            }
            //if (e.KeyCode == Keys.Enter)
            //{
            //    try
            //    {
                  
            //        dtbrs = daml.SELECT_QUIRY_only_retrn_dt("select * from branchs");
            //        cmb_brno.DataSource = dtbrs;
            //        cmb_brno.DisplayMember = "br_name";
            //        cmb_brno.ValueMember = "br_no";
            //        //  cmb_brno.SelectedIndex = 0;
            //        cmb_brno.SelectedValue = branch_log;
                   
            //        cmb_company.Enabled = false;
            //        cmb_year.Enabled = false;



            //        cmb_brno.Enabled = true;
                  
            //        cmb_brno.Focus();
            //    }
            //    catch { }
            }

        private void cmb_brno_Enter(object sender, EventArgs e)
        {
            
                try
                {

                    dtbrs = daml.SELECT_QUIRY_only_retrn_dt("select * from branchs");
                    cmb_brno.DataSource = dtbrs;
                    cmb_brno.DisplayMember = "br_name";
                    cmb_brno.ValueMember = "br_no";
                    //  cmb_brno.SelectedIndex = 0;
                    cmb_brno.SelectedValue = branch_log;

                    cmb_company.Enabled = false;
                    cmb_year.Enabled = false;



                    cmb_brno.Enabled = true;

                    cmb_brno.Focus();
                }
                catch { }
        
        }

        private void cmb_company_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_year.Focus();
            }
        }

        private void cmb_brno_Leave(object sender, EventArgs e)
        {
            dbco = "db" + cmb_company.SelectedValue + "y" + cmb_year.SelectedValue + "";
            brco = cmb_brno.SelectedValue.ToString() ;
            //con4 = new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + "db" + cmb_company.SelectedValue + "y" + cmb_year.SelectedValue + ";User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=" + BL.CLS_Session.sqlcontimout + "");
        }

        private void cmb_brno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmb_salctr.Visible == true)
                    cmb_salctr.Focus();
                else
                    cmb_type.Focus();
            }
        }
        
    }
}
