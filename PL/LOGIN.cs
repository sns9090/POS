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
using System.Threading;
using System.Diagnostics;
using System.Globalization;
//using MetroFramework;
using System.Text.RegularExpressions;
using Microsoft.Win32;




namespace POS
{
    public partial class LOGIN : BaseForm
    {
       // string aaa = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
       // string saif = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        DataTable dtbrs;
        string lang, comp_log, compyr_log, user_log, branch_log, is_rmbr, is_rmbr_pass,onlinedb;
        string ser, uid, pass, timout;
        SqlConnection con;

        BL.EncDec ende = new BL.EncDec();
        MAIN main = new MAIN();
      //  string slno;
        SqlConnection con3 ;//= new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        

         //SqlConnection con3 = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\DB.mdf;Integrated Security=True;User Instance=True");
       // SqlConnection con3 = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Documents and Settings\sa\My Documents\Visual Studio 2008\Projects\POS\POS\DB.mdf;Integrated Security=True;User Instance=True");

      //  SqlConnection con3 = new SqlConnection(@"Data Source=25.153.8.46;User ID=sa;Password=infocic;Connect Timeout=30");
        string progFiles = @"C:\Windows\System32";

        public LOGIN()
        {
            var lines2 = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\last_login.txt");
            lang = lines2.Skip(6).First().ToString();
            switch (lines2.Skip(6).First().ToString())
            {
                case "ع": Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar"); break;
                case "E": Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en"); break;

            }
            BL.CLS_Session.lang = lines2.Skip(6).First().ToString();
           // InitializeComponent();
            InitializeComponent();

           
        }

        public void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(BL.CLS_Session.sqldb);
            if (cmb_company.Text == "" || cmb_year.Text == "" || cmb_user.Text == "" || cmb_brno.Text == "")
            {
                MessageBox.Show(lang.Equals("E") ? "please enter all requied fields" : "يجب اختيار كل الحقول المطلوبة", lang.Equals("E") ? "Error" : "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            
            }
            try
            {
             //   BL.DAML daml = new BL.DAML();
              //  SqlConnection con4 = new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + BL.CLS_Session.sqldb + ";User id=" + BL.CLS_Session.sqluser + ";Password=sa123456;Connection Timeout=60");
        

                // SqlDataAdapter da3 = new SqlDataAdapter("select * from DB.dbo.users where username ='" + textBox1.Text + "' and password='" + textBox2.Text + "'", con3);
                // SqlDataAdapter da4 = new SqlDataAdapter("select [comp_id],[comp_name],[bkp_dir],[msg1],[msg2],[ownr_mob],[currency],startdate,enddate from companys", BL.DAML.con);
                //select a.[comp_id],a.[comp_name],a.[bkp_dir],a.[msg1],a.[msg2],a.[ownr_mob],a.[currency],b.calstart,b.calend from companys a,years b where a.[comp_id]=b.[comp_id]
               // daml.Exec_Query_only("update dbmstr.dbo.companys set min_start='" + ende.Encrypt(DateTime.Now.ToString("20200101", new CultureInfo("en-US", false)), true) + "' where comp_id='" + BL.CLS_Session.cmp_id + "' and min_start is null ");

                SqlDataAdapter da4 = new SqlDataAdapter("select a.[comp_id],a.[comp_name],a.[bkp_dir],a.[msg1],a.[msg2],a.[ownr_mob],a.[currency],b.calstart startdate,b.calend enddate,a.tax_no tax_no,a.img_path img,a.sw_no sw_no,a.sal_mins_bal sal_nobal,b.yrcode,a.is_cashir,a.is_shaml_tax,a.is_dory_mstmr,b.per01, b.per02, b.per03, b.per04, b.per05, b.per06, b.per07, b.per08, b.per09, b.per10, b.per11, b.per12,a.cotype,a.br_or_sl_price,a.auto_itmno,a.item_start_with,a.tax_percent,a.auto_backup,a.auto_bak_time,a.auto_tax,a.auto_pos_update,a.is_sections,a.mizan_sart,a.mizan_itemlen,a.mizan_pricelen,a.multibc,a.mada_type,a.mada_porttyp,a.mada_portno,a.sar_wzn,a.down,a.must_cost,a.is_main,a.is_taqseet,a.comp_ename,a.is_einvoice,a.notaxchang,a.must_equal,a.pos_start_day,a.msrof_qid,a.one_sal_srial,a.show_qty_srch,a.bulding_no, a.street, a.site_name, a.city, a.cuntry, a.postal_code, a.ex_postalcode, a.other_id,a.edit_itm,a.row_hight,a.allw_zerop,a.is_einv_p2,a.einv_p2_date,a.is_production,a.[whatsapp_actv],a.[whatsapp_tokn],a.[cmp_schem],a.whats_msg,a.[einv_p2_sync_actv],a.[einv_p2_sync_tim],a.min_start,a.min_lastlgin,a.[z_cert_pem],a.[z_key_pem],a.[z_secret_txt] from companys a,years b where a.[comp_id]=b.[comp_id] and a.[comp_id]='" + cmb_company.SelectedValue + "' and b.yrcode='" + cmb_year.SelectedValue + "'", con3);
                DataSet ds3 = new DataSet();
                da4.Fill(ds3, "1");
              //  BL.CLS_Session.sqldb = "db" + ds3.Tables["1"].Rows[0]["comp_id"] + "y" + ds3.Tables["1"].Rows[0]["yrcode"];

                SqlDataAdapter da3 = new SqlDataAdapter("select * from users where user_name ='" + cmb_user.SelectedValue + "' and password='" + ende.Encrypt(txt_pass.Text, true) + "' and inactive=0", BL.DAML.con);
                
                SqlDataAdapter da5 = new SqlDataAdapter("select * from branchs where br_no='" + cmb_brno.SelectedValue + "'", BL.DAML.con);

                SqlDataAdapter da6 = new SqlDataAdapter("select tr_no,tr_abriv,tr_ename from trtypes", BL.DAML.con);

               // SqlDataAdapter da9 = new SqlDataAdapter("select comp_id, fld_tag, fld_name, fld_desc, fld_edesc, fld_note from comp_chang_fild where comp_id='" + cmb_company.SelectedValue + "'", con3);
                SqlDataAdapter da9 = new SqlDataAdapter("select comp_id, fld_tag, fld_name, fld_desc, fld_edesc, fld_note from comp_chang_fild ", con3);

                SqlDataAdapter da7 = new SqlDataAdapter("select contr_id,msg1,msg2,usecd,portno,cdmsg,c_slno,print_type,print_rtn,is_tuch,impitm,is_cofi,is_small from contrs where c_brno='" + cmb_brno.SelectedValue + "' and contr_id=" + BL.CLS_Session.contr_id + "", BL.DAML.con);

                SqlDataAdapter dads = new SqlDataAdapter("select * from descs", BL.DAML.con);

                da3.Fill(ds3, "0");
                da5.Fill(ds3, "2");
                da6.Fill(ds3, "3");
                da9.Fill(ds3, "9");
                da7.Fill(ds3, "4");
                dads.Fill(ds3, "dtds");
               
                /*
                string lang = ds3.Tables["0"].Rows[0][5].ToString();
                if (lang == "English")
                {
                    //  Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar");
                    //mn.Hide();
                    //mn.Show();
                    //this.companyols.Clear();
                    //mn.englishToolStripMenuItem_Click(sender, e);

                    //InitializeComponent();
                }
                else
                {
                    //  Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");

                    //mn.Hide();
                    //mn.Show();
                    //this.companyols.Clear();
                    //mn.englishToolStripMenuItem_Click(sender, e);

                    //this.companyols.Clear();
                    //InitializeComponent();
                }
                */
               // MessageBox.Show(ende.Decrypt(ds3.Tables["0"].Rows[0]["password"].ToString(), true).Trim().Length.ToString());
                if (ds3.Tables["0"].Rows.Count >= 1 && ende.Decrypt(ds3.Tables["0"].Rows[0]["password"].ToString(), true).Trim().Length>=4)
                {
                    String thisprocessname = Process.GetCurrentProcess().ProcessName;

                   // if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1 && (Convert.ToString(ds3.Tables["1"].Rows[0]["cotype"]).Equals("p") || Convert.ToString(ds3.Tables["1"].Rows[0]["cotype"]).Equals("b")))
                    if (0>1)
                    {
                        MessageBox.Show("لا يمكن تشغيل اكثر من نسخة في نظام الكاشير","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                       // return;
                        Application.Exit();
                    }
                    if (ds3.Tables["4"].Rows.Count == 0)
                    {
                        MessageBox.Show("يجب تعريف الكاشير بالرئيسي اولا", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // return;
                        Application.Exit();
                    }

                    BL.CLS_Session.temp_bcorno = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["multibc"]) ? "bc" : "no";
                    BL.CLS_Session.cmp_ename = ds3.Tables["1"].Rows[0]["comp_ename"].ToString();
                    BL.CLS_Session.is_einv = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["is_einvoice"]);
                    BL.CLS_Session.whatsappactv = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["whatsapp_actv"]);
                    BL.CLS_Session.whatsapptokn = ds3.Tables["1"].Rows[0]["whatsapp_tokn"].ToString();
                    BL.CLS_Session.whatsappmsg = ds3.Tables["1"].Rows[0]["whats_msg"].ToString();
                    BL.CLS_Session.einv_p2_syncactv = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["einv_p2_sync_actv"]);
                    BL.CLS_Session.einv_p2_synctim = ds3.Tables["1"].Rows[0]["einv_p2_sync_tim"].ToString();
                    BL.CLS_Session.minstart =ende.Decrypt(ds3.Tables["1"].Rows[0]["min_start"].ToString(),true);
                    BL.CLS_Session.minlastlgin = ds3.Tables["1"].Rows[0]["min_lastlgin"].ToString();
                    BL.CLS_Session.cmpschem = ds3.Tables["1"].Rows[0]["cmp_schem"].ToString();
                    BL.CLS_Session.notxchng = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["notaxchang"]);
                    BL.CLS_Session.mustequal = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["must_equal"]);
                    BL.CLS_Session.msrofqid = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["msrof_qid"]);
                    BL.CLS_Session.oneslserial = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["one_sal_srial"]);
                    BL.CLS_Session.showqtysrch = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["show_qty_srch"]);
                    BL.CLS_Session.edititm = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["edit_itm"]);
                    BL.CLS_Session.is_einv_p2 = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["is_einv_p2"]);
                    BL.CLS_Session.is_production = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["is_production"]);
                    BL.CLS_Session.allwzerop = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["allw_zerop"]);
                    BL.CLS_Session.rowhight = Convert.ToString(ds3.Tables["1"].Rows[0]["row_hight"]);
                    BL.CLS_Session.einv_p2_date = Convert.ToString(ds3.Tables["1"].Rows[0]["einv_p2_date"]);
                    BL.CLS_Session.posatrtday = Convert.ToInt32(ds3.Tables["1"].Rows[0]["pos_start_day"]);

                    BL.CLS_Session.z_certpem = ds3.Tables["1"].Rows[0]["z_cert_pem"].ToString();
                    BL.CLS_Session.z_keypem = ds3.Tables["1"].Rows[0]["z_key_pem"].ToString();
                    BL.CLS_Session.z_secrettxt = ds3.Tables["1"].Rows[0]["z_secret_txt"].ToString();

                    BL.CLS_Session.dtyrper = ds3.Tables["1"];
                    BL.CLS_Session.dtdescs = ds3.Tables["dtds"];
                    BL.CLS_Session.use_cd = Convert.ToBoolean(ds3.Tables["4"].Rows[0]["usecd"]);
                    BL.CLS_Session.istuch = Convert.ToBoolean(ds3.Tables["4"].Rows[0]["is_tuch"]);
                    BL.CLS_Session.imp_itm = Convert.ToBoolean(ds3.Tables["4"].Rows[0]["impitm"]);
                    BL.CLS_Session.port_no = ds3.Tables["4"].Rows[0]["portno"].ToString();
                    BL.CLS_Session.printrtn = Convert.ToBoolean(ds3.Tables["4"].Rows[0]["print_rtn"]);
                    BL.CLS_Session.sl_no = ds3.Tables["4"].Rows[0]["c_slno"].ToString();
                    BL.CLS_Session.cd_msg = ds3.Tables["4"].Rows[0]["cdmsg"].ToString();
                    BL.CLS_Session.prnt_type = string.IsNullOrEmpty(ds3.Tables["4"].Rows[0]["print_type"].ToString()) ? "0" : ds3.Tables["4"].Rows[0]["print_type"].ToString();// ds3.Tables["4"].Rows[0]["print_type"].ToString();
                    BL.CLS_Session.iscofi = Convert.ToBoolean(ds3.Tables["4"].Rows[0]["is_cofi"]);
                    BL.CLS_Session.nocahopen = Convert.ToBoolean(ds3.Tables["4"].Rows[0]["is_small"]);
                    BL.CLS_Session.dttrtype = ds3.Tables["3"];
                    BL.CLS_Session.dtbranch = ds3.Tables["2"];
                    BL.CLS_Session.dtfieldnm = ds3.Tables["9"];
                  //  DataRow[] dtr = dtbrs.Select("br_no='" + cmb_brno.SelectedValue + "'");
                   // MessageBox.Show(ds3.Tables["2"].Rows[0][7].ToString());
                    BL.CLS_Session.brcash = ds3.Tables["2"].Rows[0][7].ToString();
                    BL.CLS_Session.brstkin = ds3.Tables["2"].Rows[0]["stkinacc"].ToString();
                    BL.CLS_Session.brstkout = ds3.Tables["2"].Rows[0]["stkoutacc"].ToString();
                   // BL.CLS_Session.contr_id = ds3.Tables["4"].Rows[0][0].ToString();

                     SqlDataAdapter da8 = new SqlDataAdapter("select * from pos_salmen where sl_id=999999999", BL.DAML.con);
                     DataTable dt8 = new DataTable();
                     da8.Fill(dt8);
                     BL.CLS_Session.dtsalman = dt8;

                     BL.CLS_Session.dtcomp = ds3.Tables["1"];
                    /*
                    if (Convert.ToBoolean(ds3.Tables["0"].Rows[0][3]) == false)
                    {
                    
                        // mn.menuStrip1.Items.Clear();
                        // MAIN.getMainForm.ملفToolStripMenuItem.Visible = false;
                        //////MAIN.getMainForm.المستخدمينToolStripMenuItem.Visible = false;
                        //////MAIN.getMainForm.التقاريرToolStripMenuItem1.Visible = false;
                        //////MAIN.getMainForm.التقاريرToolStripMenuItem.Visible = false;
                        //////MAIN.getMainForm.مرتجعاتلمسToolStripMenuItem.Visible = false;
                        //////MAIN.getMainForm.التقريراليوميToolStripMenuItem2.Visible = false;
                        //////MAIN.getMainForm.المرتجعاتToolStripMenuItem.Visible = false;
                        //////MAIN.getMainForm.الكاشيرToolStripMenuItem.Visible = false;
                        //////MAIN.getMainForm.صنفجديدToolStripMenuItem.Visible = false;
                        //////MAIN.getMainForm.العملاءToolStripMenuItem.Visible = false;
                        //////MAIN.getMainForm.ملفالدليلالمحاسبيToolStripMenuItem.Visible = false;
                        //////MAIN.getMainForm.اعدادالطابعاتالاضافيةToolStripMenuItem.Visible = false;
                        //////MAIN.getMainForm.اعدادالطابعاتالاساسيةToolStripMenuItem.Visible = false;
                        //////MAIN.getMainForm.englishToolStripMenuItem.Visible = false;
                        // main_frm.ملفToolStripMenuItem.Visible = false;

                        //main_frm.ملفToolStripMenuItem.Visible = false;
                        //main_frm.menuStrip1.Refresh();

                        //    FRM_MAIN.getMainForm.العملاءToolStripMenuItem.Enabled = true;
                        //    FRM_MAIN.getMainForm.المستخدمونToolStripMenuItem.Enabled = true;
                        //    FRM_MAIN.getMainForm.إنشاءنسخةاحتياطيةToolStripMenuItem.Enabled = true;
                        //    FRM_MAIN.getMainForm.استعادةنسخةمحفوظةToolStripMenuItem.Enabled = true;
                        //    FRM_MAIN.getMainForm.المستخدمونToolStripMenuItem.Visible = true;
                        //    Program.SalesMan = Dt.Rows[0]["FullName"].ToString();
                        //    this.Close();

                        //else if (Dt.Rows[0][2].ToString() == "عادي")
                        //{
                        //    FRM_MAIN.getMainForm.المنتوجاتToolStripMenuItem.Enabled = true;
                        //    FRM_MAIN.getMainForm.العملاءToolStripMenuItem.Enabled = true;
                        //    FRM_MAIN.getMainForm.المستخدمونToolStripMenuItem.Visible = false;
                        //    FRM_MAIN.getMainForm.إنشاءنسخةاحتياطيةToolStripMenuItem.Enabled = true;
                        //    FRM_MAIN.getMainForm.استعادةنسخةمحفوظةToolStripMenuItem.Enabled = true;
                        //    Program.SalesMan = Dt.Rows[0]["FullName"].ToString();
                        //    this.Close();
                        //}


                    }
                    else
                    {

                    }
                    //else
                    //{
                    //    MessageBox.Show("Login failed !");
                    //}

                    // var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\temp.txt");

                    //  lang = lines.Skip(6).First().ToString();



                    */


                    BL.CLS_Session.up_pricelowcost = Convert.ToBoolean(ds3.Tables["0"].Rows[0]["prislwcst"]);
                    BL.CLS_Session.lang = ds3.Tables["0"].Rows[0][5].ToString();
                    BL.CLS_Session.sl_prices = ds3.Tables["0"].Rows[0]["sl_no"].ToString();
                    BL.CLS_Session.UseEdit = ds3.Tables["0"].Rows[0][6].ToString();
                    BL.CLS_Session.up_editop =Convert.ToBoolean(ds3.Tables["0"].Rows[0]["user_type"]);
                    BL.CLS_Session.up_stopsrch = Convert.ToBoolean(ds3.Tables["0"].Rows[0]["up_stopsrch"]);
                    BL.CLS_Session.up_suspend = Convert.ToBoolean(ds3.Tables["0"].Rows[0]["suspend_inv"]);
                    BL.CLS_Session.up_belwbal = Convert.ToBoolean(ds3.Tables["0"].Rows[0]["belw_bal"]);
                    BL.CLS_Session.autoitemno = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["auto_itmno"]);
                    BL.CLS_Session.issections = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["is_sections"]);
                    BL.CLS_Session.multi_bc = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["multibc"]);
                    BL.CLS_Session.mizansart = Convert.ToString(ds3.Tables["1"].Rows[0]["mizan_sart"]);
                    BL.CLS_Session.mizanitemlen = Convert.ToInt32(ds3.Tables["1"].Rows[0]["mizan_itemlen"]);
                    BL.CLS_Session.mizanpricelen = Convert.ToInt32(ds3.Tables["1"].Rows[0]["mizan_pricelen"]);
                    BL.CLS_Session.mizantype = Convert.ToInt32(ds3.Tables["1"].Rows[0]["sar_wzn"]);
                    BL.CLS_Session.mustcost = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["must_cost"]);
                    BL.CLS_Session.ismain = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["is_main"]);
                    BL.CLS_Session.is_taqsit = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["is_taqseet"]);
                    BL.CLS_Session.system_down = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["down"]);
                    BL.CLS_Session.item_sw = ds3.Tables["1"].Rows[0]["item_start_with"].ToString();
                    BL.CLS_Session.tax_per =Convert.ToDouble(ds3.Tables["1"].Rows[0]["tax_percent"]);
                    BL.CLS_Session.cmp_name = ds3.Tables["1"].Rows[0][1].ToString();
                    BL.CLS_Session.mnu_type = ds3.Tables["1"].Rows[0]["sal_nobal"].ToString();
                    BL.CLS_Session.cmp_type = ds3.Tables["1"].Rows[0]["cotype"].ToString();
                    BL.CLS_Session.comp_logo = ds3.Tables["1"].Rows[0]["img"].ToString();
                    BL.CLS_Session.bkpdir = ds3.Tables["1"].Rows[0][2].ToString();
                    BL.CLS_Session.autobak = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["auto_backup"]);
                    BL.CLS_Session.autotax = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["auto_tax"]);
                    BL.CLS_Session.autobaktime = ds3.Tables["1"].Rows[0]["auto_bak_time"].ToString();
                    BL.CLS_Session.autoposupdat = Convert.ToBoolean(ds3.Tables["1"].Rows[0]["auto_pos_update"]);
                   // BL.CLS_Session.cmp_name = ds3.Tables["1"].Rows[0][2].ToString();
                    BL.CLS_Session.cmp_id = ds3.Tables["1"].Rows[0][0].ToString();
                    BL.CLS_Session.price_type = ds3.Tables["1"].Rows[0]["br_or_sl_price"].ToString();
                    BL.CLS_Session.sysno = ds3.Tables["1"].Rows[0]["sw_no"].ToString();
                    BL.CLS_Session.sal_nobal = Convert.ToBoolean(ds3.Tables["0"].Rows[0]["belw_bal"])? "1":"0"; //ds3.Tables["1"].Rows[0]["sal_nobal"].ToString();
                    BL.CLS_Session.UserID = ds3.Tables["0"].Rows[0][0].ToString();
                    BL.CLS_Session.up_sal_icost = Convert.ToBoolean(ds3.Tables["0"].Rows[0]["shw_sal_icost"]);
                    BL.CLS_Session.iscashir = Convert.ToString(ds3.Tables["1"].Rows[0]["is_cashir"]);
                    BL.CLS_Session.isshamltax = Convert.ToString(ds3.Tables["1"].Rows[0]["is_shaml_tax"]);
                    BL.CLS_Session.is_dorymost = Convert.ToString(ds3.Tables["1"].Rows[0]["is_dory_mstmr"]);
                    BL.CLS_Session.UserName = cmb_user.SelectedValue.ToString();
                   // BL.CLS_Session.UserID = ds3.Tables["1"].Rows[0][0].ToString();
                    BL.CLS_Session.msg1 = ds3.Tables["4"].Rows[0][1].ToString();
                    BL.CLS_Session.msg2 = ds3.Tables["4"].Rows[0][2].ToString();
                    BL.CLS_Session.tax_no = ds3.Tables["1"].Rows[0]["tax_no"].ToString();
                    //BL.CLS_Session.start_dt = Convert.ToDateTime(ds3.Tables["1"].Rows[0][7]).ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
                    //BL.CLS_Session.end_dt = Convert.ToDateTime(ds3.Tables["1"].Rows[0][8]).ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
                    BL.CLS_Session.start_dt = ds3.Tables["1"].Rows[0][7].ToString().Substring(6, 2) + "-" + ds3.Tables["1"].Rows[0][7].ToString().Substring(4, 2) + "-" + ds3.Tables["1"].Rows[0][7].ToString().Substring(0, 4);
                    BL.CLS_Session.end_dt   = ds3.Tables["1"].Rows[0][8].ToString().Substring(6, 2) + "-" + ds3.Tables["1"].Rows[0][8].ToString().Substring(4, 2) + "-" + ds3.Tables["1"].Rows[0][8].ToString().Substring(0, 4);

                    BL.CLS_Session.ownrmob = ds3.Tables["1"].Rows[0][5].ToString();
                    BL.CLS_Session.up_changprice =Convert.ToBoolean(ds3.Tables["0"].Rows[0][4]);
                    BL.CLS_Session.up_use_dsc = Convert.ToBoolean(ds3.Tables["0"].Rows[0][7]);
                    BL.CLS_Session.up_ch_itmpr = Convert.ToBoolean(ds3.Tables["0"].Rows[0][8]);
                    BL.CLS_Session.up_us_post = Convert.ToBoolean(ds3.Tables["0"].Rows[0][10]);
                    BL.CLS_Session.formkey = ds3.Tables["0"].Rows[0][9].ToString();
                    BL.CLS_Session.slkey = ds3.Tables["0"].Rows[0]["slkey"].ToString();
                    BL.CLS_Session.pukey = ds3.Tables["0"].Rows[0]["pukey"].ToString();
                    BL.CLS_Session.stkey = ds3.Tables["0"].Rows[0]["stkey"].ToString();
                    BL.CLS_Session.acckey = ds3.Tables["0"].Rows[0]["acckey"].ToString();
                    BL.CLS_Session.trkey = ds3.Tables["0"].Rows[0]["trkey"].ToString();
                    BL.CLS_Session.brkey = ds3.Tables["0"].Rows[0]["brkey"].ToString();
                    BL.CLS_Session.cstkey = ds3.Tables["0"].Rows[0]["cstkey"].ToString();

                    BL.CLS_Session.up_chang_dt = Convert.ToBoolean(ds3.Tables["0"].Rows[0]["chng_date"]);
                    BL.CLS_Session.showwin = Convert.ToBoolean(ds3.Tables["0"].Rows[0]["show_win"]);
                    BL.CLS_Session.up_edit_othr = Convert.ToBoolean(ds3.Tables["0"].Rows[0]["modfy_othr_tr"]);
                    BL.CLS_Session.item_dsc = ds3.Tables["0"].Rows[0]["item_desc"].ToString();
                    BL.CLS_Session.inv_dsc = ds3.Tables["0"].Rows[0]["inv_desc"].ToString();

                    BL.CLS_Session.cur = ds3.Tables["1"].Rows[0][6].ToString();
                    BL.CLS_Session.brno = cmb_brno.SelectedValue.ToString();

                    //MessageBox.Show(cmb_brno.SelectedValue.ToString());

                    BL.CLS_Session.brname = cmb_brno.Text.ToString();


                    string[] lineslog = { cmb_company.SelectedValue.ToString(), cmb_year.SelectedValue.ToString(), cmb_user.SelectedValue.ToString(), cmb_brno.SelectedValue.ToString(), chk_rp.Checked ? "1" : "0", ende.Encrypt(txt_pass.Text, true), BL.CLS_Session.lang };

                    File.WriteAllLines(Directory.GetCurrentDirectory() + @"\last_login.txt", lineslog);

                    /*
                    string[] lines = { comboBox1.Text, textBox2.Text, ds3.Tables["1"].Rows[0][0].ToString(), ds3.Tables["1"].Rows[0][1].ToString(), ds3.Tables["1"].Rows[0][2].ToString(), ds3.Tables["0"].Rows[0][0].ToString(), ds3.Tables["0"].Rows[0][5].ToString(), ds3.Tables["0"].Rows[0][3].ToString(), ds3.Tables["1"].Rows[0][4].ToString() };

                    File.WriteAllLines(Directory.GetCurrentDirectory() + @"\temp.txt", lines);
                    */
                    // MAIN mn = new MAIN();
                    // mn.Show();

                    // this.Hide();

                  
                    // mn.englishToolStripMenuItem_Click(sender, e);

                    // mn.set_permision(sender, e);
                   // mn.englishToolStripMenuItem_Click(sender, e);
                    // mn.englishToolStripMenuItem_Click(sender, e);
                    /*
                    Process process = Process.Start(progFiles+ @"\osk.exe");
                    // Wait one second.
                    
                    // End notepad.
                    process.Kill();
                    */
                    MAIN mn = new MAIN();
                   
                    mn.Show();

                    
                    mn.set_permision(sender, e);
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(con3.ConnectionString);
                   // mn.Text = mn.Text + "     -     " + BL.CLS_Session.cmp_name + "     -     " + BL.CLS_Session.sqldb.Substring(5, 4) + "     -     " + BL.CLS_Session.brname + "     -     " + BL.CLS_Session.UserName; //builder.InitialCatalog.ToString().Substring(6, 4);
                     
                    mn.Text = "TreeSoft Systems : "+( BL.CLS_Session.islight ? "Light" : "Pro") + "    :::    " +  BL.CLS_Session.cmp_name + "   ||   " + BL.CLS_Session.sqldb.Substring(5, 4) + "   ||   " + BL.CLS_Session.brname + "   ||   " + BL.CLS_Session.UserName; //builder.InitialCatalog.ToString().Substring(6, 4);

                 //   mn.Text = BL.CLS_Session.islight ? "TreeSoft Systems : Light" + "      ::      " + BL.CLS_Session.cmp_name + "     -     " + BL.CLS_Session.sqldb.Substring(5, 4) + "     -     " + BL.CLS_Session.brname + "     -     " + BL.CLS_Session.UserName : this.Text; //builder.InitialCatalog.ToString().Substring(6, 4);
                   

                   // mn.toolStripStatusLabel4.Text = BL.CLS_Session.cmp_name + "     -     " + BL.CLS_Session.sqldb.Substring(5, 4) + "     -     " + BL.CLS_Session.brname + "     -     " + BL.CLS_Session.UserName; //builder.InitialCatalog.ToString().Substring(6, 4);
                 
                    this.Hide();

                }
                else if (ds3.Tables["0"].Rows.Count >= 1 && ende.Decrypt(ds3.Tables["0"].Rows[0]["password"].ToString(), true).Trim().Length<4)
                {
                    MessageBox.Show(this, lang.Equals("E") ? "Must be define password according E-Invoicing" : "يمنع دخول النظام بدون كلمة سر طبقا للمتطلبات ومواصفات الفوترة الالكترونية", lang.Equals("E") ? "Error" : "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    Chang_UsrPass ch = new Chang_UsrPass(ds3.Tables["0"].Rows[0]["user_name"].ToString());
                    ch.ShowDialog();
                }
                else
                {
                    
                   //MetroMessageBox.Show(this,"اسم المستخدم او كلمة السر خطا", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show(this, lang.Equals("E") ? "Username or Passeord Error" : "اسم المستخدم او كلمة السر خطا", lang.Equals("E") ? "Error" : "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_pass.Text = "";
                    //Application.Exit();
                     
                 
                }


            }
            catch (Exception ex)
            {
               // MetroMessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

               MessageBox.Show(ex.ToString());
            //    MessageBox.Show(this, lang.Equals("E") ? "Username or Password Error" : "كلمة السر خطا", lang.Equals("E")?"Error":"تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
             //   txt_pass.Text = "";
            }


        }

        void Fillcombo()
        {
            /*
            SqlCommand sc = new SqlCommand("select username from users", con3);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("username", typeof(string));
           // dt.Columns.Add("contactname", typeof(string));
            dt.Load(reader);

            comboBox1.ValueMember = "username";
           // comboBox1.DisplayMember = "contactname";
            comboBox1.DataSource = dt;

            con3.Close();*/
            /*
            SqlDataAdapter d = new SqlDataAdapter("select comp_id,comp_name from users", con3);
            DataTable dt = new DataTable();

            d.Fill(dt);
            */
            SqlDataAdapter d = new SqlDataAdapter("select comp_id,comp_name from companys", con3);
            DataTable dt = new DataTable();

            d.Fill(dt);
           // cmb_company.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select comp_id,comp_name from companys");
            cmb_company.DataSource = dt;
            // comboBox1.ValueMember = "username";
            cmb_company.DisplayMember = "comp_name";
            cmb_company.ValueMember = "comp_id";
            cmb_company.SelectedValue = comp_log;
            /*
            SqlDataAdapter d = new SqlDataAdapter("select username from users", con3);
            DataTable dt = new DataTable();

            d.Fill(dt);

            comboBox1.DataSource = dt;
            // comboBox1.ValueMember = "username";
            comboBox1.DisplayMember = "username";
            comboBox1.ValueMember = "username";
            */
           // comboBox1.SelectedText =string.IsNullOrEmpty(BL.CLS_Session.UserName) ? "" : BL.CLS_Session.UserName;

        }





        private void LOGIN_Load(object sender, EventArgs e)
        {
            try
            {
                if(File.Exists(@".\sp_01-11-2021.sql"))
                   File.Delete(@".\sp_01-11-2021.sql");

                RegistryKey rg = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                rg.SetValue("iCalendarType", "2");
                rg.SetValue("iDate", "0");
                rg.SetValue("NumShape", "1");
                rg.SetValue("sDate", "-");
                rg.SetValue("sShortDate", "dd-MM-yyyy");
                //rg.SetValue("LocaleName", "en-US");
                rg.SetValue("sLongDate", "dddd, MMMM dd, yyyy");
                // rg.SetValue("sLongDate", "dd-mm-yyyy");

                //RegistryKey rg1 = Registry.Users.OpenSubKey(@"S-1-5-21-2478951088-3800772498-1979442883-1001\Control Panel\International", true);
                //rg1.SetValue("iCalendarType", "2");
                //rg1.SetValue("iDate", "0");
                //rg1.SetValue("NumShape", "1");
                //rg1.SetValue("sDate", "-");
                //rg1.SetValue("sShortDate", "dd-MM-yyyy");
                ////rg.SetValue("LocaleName", "en-US");
                //rg1.SetValue("sLongDate", "dddd, MMMM dd, yyyy");


                // string w_file = "POS.exe";
                // string w_directory = Directory.GetCurrentDirectory();

                // DateTime c3 = File.GetLastWriteTime(System.IO.Path.Combine(w_directory, w_file));
                //// RTB_info.AppendText("Program created at: " + c3.ToString());
                // MessageBox.Show("Program created at: " + c3.ToString("dd-MM-yyyy", new CultureInfo("en-US", false)));

                cmb_user.Enabled = false;
                txt_pass.Enabled = false;
                cmb_brno.Enabled = false;

                var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\ServerSetting.txt");
                var lines_login = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\last_login.txt");

                var linescontr = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\temp.txt");
                BL.CLS_Session.contr_id = linescontr.First().ToString();
                BL.CLS_Session.ctrno = linescontr.First().ToString();

                comp_log = lines_login.First().ToString();
                compyr_log = lines_login.Skip(1).First().ToString();

                user_log = lines_login.Skip(2).First().ToString();
                branch_log = lines_login.Skip(3).First().ToString();
                is_rmbr = lines_login.Skip(4).First().ToString();
                is_rmbr_pass = lines_login.Skip(5).First().ToString();

                if (is_rmbr.Equals("1"))
                    chk_rp.Checked = true;
                BL.CLS_Session.sqlserver = lines.First().ToString();
                // BL.CLS_Session.sqldb = ConfigurationManager.AppSettings["logfilelocation"];
                BL.CLS_Session.sqluser = lines.Skip(2).First().ToString();
                BL.CLS_Session.sqluserpass = ende.Decrypt(lines.Skip(3).First().ToString(), true);
                BL.CLS_Session.sqlcontimout = lines.Skip(4).First().ToString();
                onlinedb = lines.Skip(1).First().ToString();


                ser = lines.First().ToString();
                //textBox2.Text = lines.Skip(1).First().ToString();
                uid = lines.Skip(2).First().ToString();
                pass = ende.Decrypt(lines.Skip(3).First().ToString(), true);
                timout = lines.Skip(4).First().ToString();

                //Trusted_Connection=True;
                // con = new SqlConnection("Data Source=" + lines.First().ToString() + ";Initial Catalog=master;User id=" + lines.Skip(2).First().ToString() + ";Password=" + lines.Skip(3).First().ToString() + ";Connection Timeout=" + lines.Skip(4).First().ToString() + "");
                // con = new SqlConnection("Server=" + lines.First().ToString() + ";Database=master;Trusted_Connection=True;");

                con3 = new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + lines.Skip(1).First().ToString() + ";User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=" + BL.CLS_Session.sqlcontimout + "");
                //  con3 = new SqlConnection(@"Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=|DataDirectory|\DBL\dbmstr.mdf");


                /*
                 lbluser.Text = lines.First().ToString();
                 lbluser.Text = BL.CLS_Session.UserName;

            
                pass.Text = lines.Skip(1).First().ToString();
                lblcashir.Text = lines.Skip(2).First().ToString();
                cmp_name = lines.Skip(4).First().ToString();
                */
                //BL.EncDec ende = new BL.EncDec();

                //SqlDataAdapter d = new SqlDataAdapter("select test from company", con3);
                //DataTable dt = new DataTable();
                //d.Fill(dt);

                //int tocomp = Convert.ToInt32(ende.Decrypt(dt.Rows[0][0].ToString(),true));
                //if (tocomp <= 100)
                //{
                //    int tosav = tocomp + 1;
                //    SqlCommand cmd = new SqlCommand("update company set test='" + ende.Encrypt(tosav.ToString(), true) + "'", con3);

                //    con3.Open();
                //    cmd.ExecuteNonQuery();
                //    con3.Close();

                // //   BL.EncDec ende = new BL.EncDec();
                //   // MessageBox.Show(ende.Encrypt("1", true).ToString());
                //    // con3.Open();
                //}
                Fillcombo();
                // SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(con3.ConnectionString);
                //  BL.CLS_Session.sqlserver = builder.DataSource;
                //  BL.CLS_Session.yrcode = "db" + cmb_company.SelectedValue + "y" + cmb_year.SelectedValue + "";
                /*
                BL.CLS_Session.sqlserver = ConfigurationManager.AppSettings["server"];
               // BL.CLS_Session.sqldb = ConfigurationManager.AppSettings["logfilelocation"];
                BL.CLS_Session.sqluser = ConfigurationManager.AppSettings["user"];
                BL.CLS_Session.sqluserpass = ConfigurationManager.AppSettings["pass"];
                 */
                //  cmb_company.Focus();
                // cmb_br_fill();
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // this.Close();
            Application.Exit();
          
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1_Click(sender, e);
            }
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            
           // string progFiles = @"C:\Program Files\Common Files\Microsoft Shared\ink";
          //  string progFiles = @"C:\Windows\System32";
           // string keyboardPath = Path.Combine(progFiles, "TabTip.exe");
            /*
            string keyboardPath = Path.Combine(progFiles, "osk.exe");
            Process.Start(keyboardPath);
             */
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void LOGIN_FormClosed(object sender, FormClosedEventArgs e)
        {
            MAIN MN = new MAIN();
            MN.Close();
        }

        private void LOGIN_FormClosing(object sender, FormClosingEventArgs e)
        {
            MAIN MN = new MAIN();
            MN.Close();
        }

        private void cmb_br_fill()
        {
            BL.DAML daml = new BL.DAML();
            DataTable dt3 = daml.SELECT_QUIRY_only_retrn_dt("select comp_name from company");
            DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select br_no,br_name,cashser from branchs");
            cmb_brno.DataSource = dt2;
            cmb_brno.DisplayMember = "br_name";
            cmb_brno.ValueMember = "br_no";
            cmb_brno.SelectedValue = branch_log;
           // cmb_brno.SelectedIndex = 0;
            this.Text = this.Text + "  -  " + dt3.Rows[0][0].ToString();
            BL.CLS_Session.brcash = dt2.Rows[0][2].ToString();

        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_pass.Focus();
               // DataTable dtb = daml.SELECT_QUIRY_only_retrn_dt("select brkey from users where username='" + comboBox1.Text + "'");


            }
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            
           
        }

        private void cmb_brno_Leave(object sender, EventArgs e)
        {

            try
            {
                //if (e.KeyCode == Keys.Enter)
                //{
                    BL.DAML daml = new BL.DAML();
                    DataTable dtu = daml.SELECT_QUIRY_only_retrn_dt("select user_name,full_name from users where inactive=0 and brkey like '% " + cmb_brno.SelectedValue + "%'");

                    if (dtu.Rows.Count > 0)
                    {

                        // cmb_user.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select user_name,full_name from users");
                        cmb_user.DataSource = dtu;
                        // comboBox1.ValueMember = "username";
                        cmb_user.DisplayMember = "full_name";
                        cmb_user.ValueMember = "user_name";
                        cmb_user.SelectedValue = user_log;
                        // BL.CLS_Session.yrcode = "db" + cmb_company.SelectedValue + "y" + cmb_year.SelectedValue + "";


                        cmb_company.Enabled = false;
                        cmb_year.Enabled = false;

                        cmb_user.Enabled = true;
                        txt_pass.Enabled = true;
                        cmb_brno.Enabled = true;

                        cmb_user.Focus();
                    }
                    else
                    {
                        //BL.DAML dal = new BL.DAML();
                        // daml.ExecuteCommand_sp_with_param("create_admin_user", null);
                        // Application.Restart();
                        cmb_user.DataSource = null;
                        cmb_user.Focus();
                    }
                //}
            }
            catch { }
        }

        private void cmb_brno_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void cmb_company_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               

                cmb_year.Focus();
                // DataTable dtb = daml.SELECT_QUIRY_only_retrn_dt("select brkey from users where username='" + comboBox1.Text + "'");
            }
        }

        private void cmb_year_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fill_br();
            }
        }

        private void fill_br()
        {
            try
            {
                BL.CLS_Session.sqldb = onlinedb.Equals("dbmstr") ? "db" + cmb_company.SelectedValue + "y" + cmb_year.SelectedValue + "" : onlinedb;

                BL.DAML daml = new BL.DAML();
                //// DataTable dt1 = daml.SELECT_QUIRY_only_retrn_dt("select brkey from users where user_name ='" + cmb_user.SelectedValue + "'");
                ////  BL.CLS_Session.brkey = dt1.Rows[0][0].ToString();
                ////  string sl = BL.CLS_Session.brkey.Replace(" ", "','").Remove(0, 2) + "'";
                //  MessageBox.Show(sl);
                // dtbrs = daml.SELECT_QUIRY_only_retrn_dt("select * from branchs where br_no in(" + sl + ")");
                dtbrs = daml.SELECT_QUIRY_only_retrn_dt("select * from branchs");
                cmb_brno.DataSource = dtbrs;
                cmb_brno.DisplayMember = "br_name";
                cmb_brno.ValueMember = "br_no";
                //  cmb_brno.SelectedIndex = 0;
                cmb_brno.SelectedValue = branch_log;
                //}
                //catch { }


                //SqlDataAdapter d = new SqlDataAdapter("select comp_id,comp_name from users", con3);
                //DataTable dt = new DataTable();

                //d.Fill(dt);
                // MessageBox.Show("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + BL.CLS_Session.sqldb + ";User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=" + BL.CLS_Session.sqlcontimout + "");
                // BL.DAML daml = new BL.DAML();

                ////DataTable dtu = daml.SELECT_QUIRY_only_retrn_dt("select user_name,full_name from users where inactive=0");

                ////if (dtu.Rows.Count > 0)
                ////{

                ////   // cmb_user.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select user_name,full_name from users");
                ////    cmb_user.DataSource = dtu;
                ////    // comboBox1.ValueMember = "username";
                ////    cmb_user.DisplayMember = "full_name";
                ////    cmb_user.ValueMember = "user_name";
                ////    cmb_user.SelectedValue = user_log;
                ////    // BL.CLS_Session.yrcode = "db" + cmb_company.SelectedValue + "y" + cmb_year.SelectedValue + "";


                cmb_company.Enabled = false;
                cmb_year.Enabled = false;



                cmb_brno.Enabled = true;
                cmb_user.Enabled = true;
                txt_pass.Enabled = true;
                ////    cmb_user.Focus();
                ////}
                ////else
                ////{
                ////    BL.DAML dal = new BL.DAML();
                ////    dal.ExecuteCommand_sp_with_param("create_admin_user", null);
                ////    Application.Restart();
                ////}

                // DataTable dtb = daml.SELECT_QUIRY_only_retrn_dt("select brkey from users where username='" + comboBox1.Text + "'");

                //  cmb_brno.Enabled = true;
                cmb_brno.Focus();
            }
            catch { } 
        }

        private void cmb_year_Enter(object sender, EventArgs e)
        {
            SqlDataAdapter d = new SqlDataAdapter("select comp_id,yrcode from years where comp_id='" + cmb_company.SelectedValue + "' order by yrcode DESC", con3);
            DataTable dt = new DataTable();

            d.Fill(dt);
            cmb_year.DataSource = dt;
            // cmb_year.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select comp_id,yrcode from years where comp_id='"+cmb_company.SelectedValue+"'");
            // comboBox1.ValueMember = "username";
            cmb_year.DisplayMember = "yrcode";
            cmb_year.ValueMember = "yrcode";
            cmb_year.SelectedValue = compyr_log;

           // SendKeys.Send("{Enter}");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmb_brno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BL.DAML daml = new BL.DAML();
                    DataTable dtu = daml.SELECT_QUIRY_only_retrn_dt("select user_name,full_name from users where inactive=0 and brkey like '% "+ cmb_brno.SelectedValue +"%'");

                    if (dtu.Rows.Count > 0)
                    {

                        // cmb_user.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select user_name,full_name from users");
                        cmb_user.DataSource = dtu;
                        // comboBox1.ValueMember = "username";
                        cmb_user.DisplayMember = "full_name";
                        cmb_user.ValueMember = "user_name";
                        cmb_user.SelectedValue = user_log;
                        // BL.CLS_Session.yrcode = "db" + cmb_company.SelectedValue + "y" + cmb_year.SelectedValue + "";


                        cmb_company.Enabled = false;
                        cmb_year.Enabled = false;

                        cmb_user.Enabled = true;
                        txt_pass.Enabled = true;
                        cmb_brno.Enabled = true;

                        cmb_user.Focus();
                    }
                    else
                    {
                        //BL.DAML dal = new BL.DAML();
                       // daml.ExecuteCommand_sp_with_param("create_admin_user", null);
                       // Application.Restart();
                        cmb_user.DataSource = null;
                        cmb_user.Focus();
                    }
                }
            }
            catch { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://tree-soft.com/");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://tree-soft.com/");
        }

        private void cmb_brno_Enter(object sender, EventArgs e)
        {
           // cmb_year_KeyDown(null, null);
           // SendKeys.Send("{Enter}");
        }

        private void cmb_year_Leave(object sender, EventArgs e)
        {
            
            /*
            string w_file = "TreeSoft.exe";
            string w_directory = Directory.GetCurrentDirectory();

            DateTime c3 = File.GetLastWriteTime(System.IO.Path.Combine(w_directory, w_file));
            // RTB_info.AppendText("Program created at: " + c3.ToString());
            // MessageBox.Show("Program created at: " + c3.ToString("dd-MM-yyyy", new CultureInfo("en-US", false)));
            string tochk = c3.ToString("yyyyMMdd", new CultureInfo("en-US", false));

           SqlDataAdapter dac = new SqlDataAdapter("select update_date from dbmstr.dbo.years where comp_id='" + cmb_company.SelectedValue + "' and yrcode='"+cmb_year.SelectedValue+"'", con);
           DataTable dtc = new DataTable();

           dac.Fill(dtc);

          // MessageBox.Show(dtc.Rows[0][0].ToString());
          // MessageBox.Show(tochk);

           if (!dtc.Rows[0][0].ToString().Equals(tochk))
           {

               // MessageBox.Show("exec db" + cmb_company.SelectedValue + "y" + cmb_year.SelectedValue + ".dbo.create_messing_columns;");

               // string commandText = "exec db" + cmb_company.SelectedValue + "y" + cmb_year.SelectedValue + ".dbo.create_messing_columns;";

               //// using (SqlConnection conn = new SqlConnection(con3))
               // using (SqlCommand cmd = new SqlCommand(commandText, con3))
               // {
               //     con3.Open();
               //     cmd.ExecuteNonQuery();
               //     con3.Close();
               // }

               //DirectoryInfo d = new DirectoryInfo(Directory.GetCurrentDirectory() + @"\scripts\");//Assuming Test is your Folder
               //FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
               //string str = "";

               string[] fileArray = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\scripts");
               //  foreach (FileInfo file in Files)
               foreach (string i in fileArray)
               {
                   //  str = str + ", " + file.Name;
                   // str = str + ", " + i;
                   // MessageBox.Show(str);
                   // MessageBox.Show(i);
                   string result = Path.GetFileName(i);

                   string connection = @"Data Source=" + ser + ";Initial Catalog=" + (result.StartsWith("dbm") ? "dbmstr" : "db" + cmb_company.SelectedValue + "y" + cmb_year.SelectedValue + "") + ";User ID=" + uid + ";Password=" + pass + ";Connection Timeout=" + timout + "";
                   // FileInfo file = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + \C:\Users\SSS\Documents\db2030.sql");
                   //  FileInfo file = new FileInfo(@"C:\Users\SSS\Documents\db2030.sql");
                   string script = File.ReadAllText(i);
                   // script = script.Replace("db01y2018", appPathDB).Replace("db01y2030", appPathLog);
                   ////  script = script.Replace("db01y2018", "db01y2030").Replace("AS" + Environment.NewLine + "BEGIN", Environment.NewLine + "WITH ENCRYPTION " + Environment.NewLine + " AS" + Environment.NewLine + "BEGIN"); ;
                   // split script on GO command

                   IEnumerable<string> commandStrings = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                   SqlConnection conn = new SqlConnection(connection);

                   try
                   {
                       conn.Open();

                       foreach (string commandString in commandStrings)
                       {
                           if (commandString.Trim() != "")
                           {
                               using (var command = new SqlCommand(commandString, conn))
                               {
                                   command.ExecuteNonQuery();
                               }
                           }
                       }
                       conn.Close();
                   }
                   catch (Exception ex) { 
                       //MessageBox.Show(ex.Message); 
                       conn.Close(); }
               }

             //  MessageBox.Show("done");
               string commandText = "update dbmstr.dbo.years set update_date='" + tochk + "' where comp_id='" + cmb_company.SelectedValue + "' and yrcode='" + cmb_year.SelectedValue + "'";

               //// using (SqlConnection conn = new SqlConnection(con3))
                using (SqlCommand cmd = new SqlCommand(commandText, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
           }
             */
        }

        private void txt_pass_Enter(object sender, EventArgs e)
        {
            if (is_rmbr.Equals("1"))
                txt_pass.Text = ende.Decrypt(is_rmbr_pass, true);

        }

        private void cmb_company_Enter(object sender, EventArgs e)
        {
           // SendKeys.Send("{Enter}");
        }

        private void cmb_user_Enter(object sender, EventArgs e)
        {
            //SendKeys.Send("{Enter}");
        }

        private void chk_lang_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_lang.Checked)
               Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            else
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar"); 

            // this.companyols.Clear();
            // this.menuStrip1.Items.Clear();

             InitializeComponent();
        }

        private void cmb_brno_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void cmb_brno_MouseUp(object sender, MouseEventArgs e)
        {
            //fill_br();
        }

        private void cmb_brno_MouseMove(object sender, MouseEventArgs e)
        {
           // fill_br();
        }

        private void cmb_year_MouseLeave(object sender, EventArgs e)
        {
            //fill_br();
        }       
        
    }
}
