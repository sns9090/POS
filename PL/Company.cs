using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Globalization;

namespace POS
{
    public partial class Company : BaseForm
    {
        List<BL.CurrencyInfo> currencies = new List<BL.CurrencyInfo>();
        BL.EncDec ende = new BL.EncDec();
        SqlConnection con;//= new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        SqlConnection con2 = BL.DAML.con;
        string imgpath = "", fullpath = "", oldfilnam, imgpathset="";
        string fontt, fonts, isbold;
        BL.Date_Validate datval = new BL.Date_Validate();
        //float fonts;
       // bool isbold;

        private SqlConnection sqlConnection = null;

        private SqlDataAdapter sqlDataAdapter = null;

        private SqlCommandBuilder sqlCommandBuilder = null;

        private DataTable dataTable = null;

        private BindingSource bindingSource = null;


        public Company()
        {
            InitializeComponent();
            fill_combo_rtype();
        //    textBox1.Enabled = false;
            // textBox2.Enabled = false;
        }
        private void fill_combo_rtype()
        {
            Dictionary<string, string> schem = new Dictionary<string, string>()
           {
               {"رقم السجل التجاري","CRN"},
               {"رخصة البلدية","MOM"},
               {"رخصة الموارد البشرية","MLS"},
               {"رخصة وزارة الاستثمار","SAG"},
               {"معرف اخر","OTH"}
           };
            cmb_rtype.DataSource = new BindingSource(schem, null);
            cmb_rtype.DisplayMember = "Key";
            cmb_rtype.ValueMember = "Value";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
              int pass = 0;
                if (Convert.ToInt32(DateTime.Now.Day) % 2 == 0)
                    pass = ((Convert.ToInt32(DateTime.Now.Year) + Convert.ToInt32(DateTime.Now.Month)) * 411) - (Convert.ToInt32(DateTime.Now.Day) * 114);
                else
                    pass = ((Convert.ToInt32(DateTime.Now.Year) + Convert.ToInt32(DateTime.Now.Month)) * 311) - (Convert.ToInt32(DateTime.Now.Day) * 113);

               // if (textBox1.Text == "sa735356688")
                //if (Convert.ToInt32(textBox1.Text.Trim()) == pass)
                if (!txt_pass.Text.Trim().Equals(pass.ToString()) && txt_pass.Enabled)
                {
                    MessageBox.Show("PassWord Error", " Note تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            if (con.State == ConnectionState.Closed)
            con.Open();
            // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
            using (SqlCommand cmd = new SqlCommand("update companys set comp_name=@a3,bkp_dir=@a4,msg1=@m1,msg2=@m2,ownr_mob=@on,currency=@cur, tax_no=@tax,img_path=@img,sal_mins_bal=@smb,is_shaml_tax=@sh_tax,is_cashir=@iscashir,is_dory_mstmr=@isdom,cotype=@cotp,br_or_sl_price=@brsl,auto_itmno=@ai,item_start_with=@isw,tax_percent=@taxp,auto_backup=@ab,auto_bak_time=@abt,auto_tax=@atax,auto_pos_update=@apu,is_sections=@issec,mizan_sart=@mizs,mizan_itemlen=@mizil,mizan_pricelen=@mizpl,multibc=@mbc,mada_type=@mtyp,mada_porttyp=@mptyp,mada_portno=@mpn,sar_wzn=@sw,must_cost=@mc,is_taqseet=@itq,comp_ename=@cmpen,is_einvoice=@einv,notaxchang=@ntc,must_equal=@meq,msrof_qid=@msq,show_qty_srch=@sqs,bulding_no=@bulding_no,street=@street,site_name=@site_name,city=@city,cuntry=@cuntry,postal_code=@postal_code,ex_postalcode=@ex_postalcode,other_id=@other_id,allw_zerop=@allw_zerop,cmp_schem=@cmp_schem,whats_msg=@whats_msg,einv_p2_sync_actv=@einv_p2_sync_actv,einv_p2_sync_tim=@einv_p2_sync_tim,min_start=@min_start,min_mref=@min_mref  where comp_id='" + txt_cmpid.Text + "'", con))
            {

                //cboCurrency.SelectedValue
               // cmd.Parameters.AddWithValue("@a2", textBox2.Text);
                cmd.Parameters.AddWithValue("@a3", txt_cmpnam.Text);
                cmd.Parameters.AddWithValue("@a4", txt_bak.Text);
                cmd.Parameters.AddWithValue("@m1", txt_address.Text);
                cmd.Parameters.AddWithValue("@m2", txt_slmsg.Text);
                cmd.Parameters.AddWithValue("@on", txt_mob.Text);
                cmd.Parameters.AddWithValue("@cur", cboCurrency.SelectedValue);
                cmd.Parameters.AddWithValue("@tax", txt_taxcode.Text);
                cmd.Parameters.AddWithValue("@img", !string.IsNullOrEmpty(imgpathset) ? imgpathset : oldfilnam);
                cmd.Parameters.AddWithValue("@smb",(BL.CLS_Session.islight? 0 : chk_nobal.Checked ? 1 : 0));
                //iscashir
                //sh_tax
                cmd.Parameters.AddWithValue("@sh_tax", rad_without.Checked ? 1 :  2 );
                cmd.Parameters.AddWithValue("@iscashir", cmb_shortcut.SelectedIndex);
                cmd.Parameters.AddWithValue("@isdom", rd_dory.Checked ? 1 : 2);
                cmd.Parameters.AddWithValue("@cotp", (rd_m.Checked && ch_trdfrt.Checked)? "a" :(rd_m.Checked && !ch_trdfrt.Checked)? "m" : rd_p.Checked? "p":rd_b.Checked? "b": "s");
                cmd.Parameters.AddWithValue("@brsl", rd_brpr.Checked ? 1 : 2);
                //cmd.Parameters.AddWithValue("@ai",(BL.CLS_Session.islight? "0": chk_itemauto.Checked ? "1" : "0"));
                cmd.Parameters.AddWithValue("@ai", chk_itemauto.Checked ? "1" : "0");
                cmd.Parameters.AddWithValue("@isw", txt_isw.Text);
                cmd.Parameters.AddWithValue("@taxp", txt_taxper.Text);
                cmd.Parameters.AddWithValue("@ab", chk_enableab.Checked? 1 : 0);
                cmd.Parameters.AddWithValue("@abt", cmb_time.Text);
                cmd.Parameters.AddWithValue("@einv_p2_sync_actv", chk_einv_p2_sync.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@einv_p2_sync_tim", cmb_einv_p2_time.Text);
                cmd.Parameters.AddWithValue("@atax", chk_autotax.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@apu", chk_autoposup.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@issec", chk_issections.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@mizs", txt_itmstart.Text);
                cmd.Parameters.AddWithValue("@mizil", txt_itmln.Text);
                cmd.Parameters.AddWithValue("@mizpl", txt_itmprl.Text);
                cmd.Parameters.AddWithValue("@mbc", chk_multibc.Checked ? 1 : 0);
                //@mtyp,mada_porttyp=@mptyp,mada_portno=@mpn
                cmd.Parameters.AddWithValue("@mtyp", cmb_madatyp.SelectedIndex);
                cmd.Parameters.AddWithValue("@mptyp", cmb_porttyp.SelectedIndex);
                cmd.Parameters.AddWithValue("@mpn", txt_port.Text);
                cmd.Parameters.AddWithValue("@sw", cmb_wznsar.SelectedIndex);
                cmd.Parameters.AddWithValue("@mc", chk_mustcost.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@itq", chk_istaqsit.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@cmpen", txt_compename.Text);
                cmd.Parameters.AddWithValue("@einv", chk_einv.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@ntc", chk_nochngtax.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@meq", chk_mustequal.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@msq", chk_msrofqid.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@sqs", chk_showqtysrch.Checked ? 1 : 0);

                cmd.Parameters.AddWithValue("@bulding_no", txt_buldingno.Text);
                cmd.Parameters.AddWithValue("@street", txt_street.Text);
                cmd.Parameters.AddWithValue("@site_name", txt_site.Text);
                cmd.Parameters.AddWithValue("@city", txt_city.Text);
                cmd.Parameters.AddWithValue("@cuntry", txt_cuntry.Text);
                cmd.Parameters.AddWithValue("@postal_code", txt_postalcode.Text);
                cmd.Parameters.AddWithValue("@ex_postalcode", txt_expostal.Text);
                cmd.Parameters.AddWithValue("@other_id", txt_otherid.Text);
                cmd.Parameters.AddWithValue("@allw_zerop", chk_zerop.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@cmp_schem", cmb_rtype.SelectedValue);
                cmd.Parameters.AddWithValue("@whats_msg", txt_wmsg.Text);
                cmd.Parameters.AddWithValue("@min_start",ende.Encrypt(datval.convert_to_yyyyDDdd(txt_mdate.Text),true));
                cmd.Parameters.AddWithValue("@min_mref", txt_mref.Text);
                cmd.ExecuteNonQuery();
               // con.Close();
              //  MessageBox.Show("modifed success تم التعديل", "ok", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            using (SqlCommand cmd = new SqlCommand("update years set calstart=@sd,calend=@ed,per01=@p1, per02=@p2, per03=@p3, per04=@p4, per05=@p5, per06=@p6, per07=@p7, per08=@p8, per09=@p9, per10=@p10, per11=@p11, per12=@p12 where yrcode='" + BL.CLS_Session.sqldb.Substring(5, 4) + "' and comp_id='" + BL.CLS_Session.sqldb.Substring(2, 2) + "'", con))
            {

                cmd.Parameters.AddWithValue("@sd", txt_start.Text.Replace("-",""));
                cmd.Parameters.AddWithValue("@ed", txt_end.Text.Replace("-",""));
                cmd.Parameters.AddWithValue("@p1", datval.convert_to_yyyyDDdd(txt_per01.Text));
                cmd.Parameters.AddWithValue("@p2", datval.convert_to_yyyyDDdd(txt_per02.Text));
                cmd.Parameters.AddWithValue("@p3", datval.convert_to_yyyyDDdd(txt_per03.Text));
                cmd.Parameters.AddWithValue("@p4", datval.convert_to_yyyyDDdd(txt_per04.Text));
                cmd.Parameters.AddWithValue("@p5", datval.convert_to_yyyyDDdd(txt_per05.Text));
                cmd.Parameters.AddWithValue("@p6", datval.convert_to_yyyyDDdd(txt_per06.Text));
                cmd.Parameters.AddWithValue("@p7", datval.convert_to_yyyyDDdd(txt_per07.Text));
                cmd.Parameters.AddWithValue("@p8", datval.convert_to_yyyyDDdd(txt_per08.Text));
                cmd.Parameters.AddWithValue("@p9", datval.convert_to_yyyyDDdd(txt_per09.Text));
                cmd.Parameters.AddWithValue("@p10", datval.convert_to_yyyyDDdd(txt_per10.Text));
                cmd.Parameters.AddWithValue("@p11", datval.convert_to_yyyyDDdd(txt_per11.Text));
                cmd.Parameters.AddWithValue("@p12", datval.convert_to_yyyyDDdd(txt_per12.Text));
                cmd.ExecuteNonQuery();
                con.Close();

                File.WriteAllText(Directory.GetCurrentDirectory() + @"\Color.txt", textBox1.Text + Environment.NewLine + fontt + Environment.NewLine + fonts + Environment.NewLine + isbold + Environment.NewLine + txt_forcolor.Text);

               // sqlDataAdapter.Update(dataTable);

                MessageBox.Show("modifed success تم التعديل", "ok", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            using (SqlCommand cmd = new SqlCommand("update taxs set tax_percent=@txp where tax_id=1 ", con2))
            {
                if (con2.State == ConnectionState.Closed)
                con2.Open();
                cmd.Parameters.AddWithValue("@txp", txt_taxper.Text);
               // cmd.Parameters.AddWithValue("@ed", txt_end.Text.Replace("-",""));
              
                cmd.ExecuteNonQuery();
                con2.Close();

               // File.WriteAllText(Directory.GetCurrentDirectory() + @"\Color.txt", textBox1.Text + Environment.NewLine + fontt + Environment.NewLine + fonts + Environment.NewLine + isbold);
               // MessageBox.Show("modifed success تم التعديل", "ok", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            foreach (Control ctr in panel1.Controls)
            {
                ctr.Enabled = false;
            }
           // txt_cmpid.Enabled = false;
            button1.Enabled = false;
            button4.Enabled = true;

            panel5.Enabled = false;
            panel6.Enabled = false;
            panel7.Enabled = false;
            panel8.Enabled = false;
            panel9.Enabled = false;
            panel10.Enabled = false;
            ch_trdfrt.Enabled = false;
            chk_multibc.Enabled = false;
            chk_enableab.Enabled = false;
            cmb_time.Enabled = false;
            cmb_einv_p2_time.Enabled = false;
            chk_einv_p2_sync.Enabled = false;
           // cmb_einv_p2_time.Enabled = false;
            chk_einv.Enabled = false;
            chk_nochngtax.Enabled = false;
            chk_mustequal.Enabled = false;
            chk_zerop.Enabled = false;
            chk_msrofqid.Enabled = false;
            chk_showqtysrch.Enabled = false;
            txt_buldingno.Enabled = false;
            txt_street.Enabled = false;
            txt_site.Enabled = false;
            txt_city.Enabled = false;
            txt_cuntry.Enabled = false;
            txt_postalcode.Enabled = false;
            txt_expostal.Enabled = false;
            txt_otherid.Enabled = false;
            txt_wmsg.Enabled = false;
            cmb_rtype.Enabled = false;
            txt_mdate.Enabled = false;
            txt_mref.Enabled = false;
        }

        private void CMP_FRM_Load(object sender, EventArgs e)
        {

            if (BL.CLS_Session.sysno == "1")
            {
                tabControl1.Visible = false;
            }
            var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\ServerSetting.txt");
            var lines3 = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Color.txt");
            textBox1.Text = lines3.First().ToString();
            txt_forcolor.Text = lines3.Skip(4).First().ToString();

            fontt = BL.CLS_Session.font_t;
            fonts =BL.CLS_Session.font_s;
           // BL.CLS_Session.font_s = lines.Skip(3).First().ToString();
            isbold = BL.CLS_Session.is_bold;

           // BL.CLS_Session.sqlserver = lines.First().ToString();
            // BL.CLS_Session.sqldb = ConfigurationManager.AppSettings["logfilelocation"];
           // BL.CLS_Session.sqluser = lines.Skip(2).First().ToString();
           // BL.CLS_Session.sqluserpass = lines.Skip(3).First().ToString();
          //  BL.CLS_Session.sqlcontimout = lines.Skip(4).First().ToString();

            if (!File.Exists(Environment.CurrentDirectory + @"\local.txt"))
                con = new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + lines.Skip(1).First().ToString() + ";User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=" + BL.CLS_Session.sqlcontimout + "");
            else
                con = new SqlConnection(@"Data Source=" + BL.CLS_Session.sqlserver + @";AttachDbFilename=|DataDirectory|\DB\" + BL.CLS_Session.sqldb + ".mdf;User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=" + BL.CLS_Session.sqlcontimout + "");
             
            // button1.Text = "تعديل";
            SqlDataAdapter da = new SqlDataAdapter("select a.[comp_id],a.[comp_name],a.[bkp_dir],a.[msg1],a.[msg2],a.[ownr_mob],a.[currency],b.calstart startdate,b.calend enddate,a.tax_no,a.img_path img_path,a.sal_mins_bal sal_mins_bal,a.is_shaml_tax is_shaml_tax,a.is_cashir,a.is_dory_mstmr,b.per01, b.per02, b.per03, b.per04, b.per05, b.per06, b.per07, b.per08, b.per09, b.per10, b.per11, b.per12,a.cotype,a.br_or_sl_price,a.auto_itmno,a.item_start_with,a.tax_percent,a.auto_backup,a.auto_bak_time,a.auto_tax,a.auto_pos_update,a.is_sections,a.mizan_sart,a.mizan_itemlen,a.mizan_pricelen,a.multibc,a.mada_type,a.mada_porttyp,a.mada_portno,a.sar_wzn,a.must_cost,a.ts_custno,is_taqseet,a.comp_ename,a.is_einvoice,a.notaxchang,a.must_equal,a.msrof_qid,a.one_sal_srial,a.show_qty_srch,a.bulding_no, a.street, a.site_name, a.city, a.cuntry, a.postal_code, a.ex_postalcode, a.other_id,a.allw_zerop,a.cmp_schem,a.whats_msg,a.einv_p2_sync_actv,a.einv_p2_sync_tim,a.min_start,a.min_mref,a.is_einv_p2,a.[z_cert_pem],a.[z_key_pem],a.[z_secret_txt],a.[z_csr_csr] from companys a,years b where a.[comp_id]=b.[comp_id] and b.yrcode='" + BL.CLS_Session.sqldb.Substring(5, 4) + "' and a.comp_id='" + BL.CLS_Session.cmp_id + "'", con);

            DataTable dt = new DataTable();
            da.Fill(dt);
           // textBox1.Text = dt.Rows[0][0].ToString();
          //  textBox2.Text = dt.Rows[0][1].ToString();
            txt_cmpnam.Text = dt.Rows[0][1].ToString();
            txt_bak.Text = dt.Rows[0][2].ToString();
            txt_cmpid.Text = dt.Rows[0][0].ToString();
            txt_address.Text = dt.Rows[0][3].ToString();
            txt_slmsg.Text = dt.Rows[0][4].ToString();
            txt_mob.Text = dt.Rows[0][5].ToString();
            txt_taxper.Text = dt.Rows[0]["tax_percent"].ToString();
            //,a.mizan_sart,a.mizan_itemlen,a.mizan_pricelen
            txt_itmstart.Text = dt.Rows[0]["mizan_sart"].ToString();
            txt_itmln.Text = dt.Rows[0]["mizan_itemlen"].ToString();
            txt_itmprl.Text = dt.Rows[0]["mizan_pricelen"].ToString();

            txt_buldingno.Text = dt.Rows[0]["bulding_no"].ToString();
            txt_street.Text = dt.Rows[0]["street"].ToString();
            txt_site.Text = dt.Rows[0]["site_name"].ToString();
            txt_city.Text = dt.Rows[0]["city"].ToString();
            txt_cuntry.Text = dt.Rows[0]["cuntry"].ToString();
            txt_postalcode.Text = dt.Rows[0]["postal_code"].ToString();
            txt_expostal.Text = dt.Rows[0]["ex_postalcode"].ToString();
            txt_otherid.Text = dt.Rows[0]["other_id"].ToString();

            txt_zcert.Text = dt.Rows[0]["z_cert_pem"].ToString(); 
            txt_zkey.Text = dt.Rows[0]["z_key_pem"].ToString();
            txt_zsecrt.Text = dt.Rows[0]["z_secret_txt"].ToString();
            txt_csr.Text = dt.Rows[0]["z_csr_csr"].ToString();

            txt_wmsg.Text = string.IsNullOrEmpty(dt.Rows[0]["whats_msg"].ToString()) ? txt_wmsg.Text : dt.Rows[0]["whats_msg"].ToString();

            //dt.ToString("yyyy-MM-dd", new CultureInfo("en-US", false));
            //txt_start.Text =Convert.ToDateTime(dt.Rows[0][7]).ToString("yyyy-MM-dd", new CultureInfo("en-US", false));
            //txt_end.Text = Convert.ToDateTime(dt.Rows[0][8]).ToString("yyyy-MM-dd", new CultureInfo("en-US", false));
            txt_start.Text = dt.Rows[0][7].ToString().Substring(0, 4) + "-" + dt.Rows[0][7].ToString().Substring(4, 2) + "-" + dt.Rows[0][7].ToString().Substring(6, 2);
            txt_end.Text = dt.Rows[0][8].ToString().Substring(0, 4) + "-" + dt.Rows[0][8].ToString().Substring(4, 2) + "-" + dt.Rows[0][8].ToString().Substring(6, 2);

            txt_taxcode.Text = dt.Rows[0][9].ToString();
            txt_compename.Text = dt.Rows[0]["comp_ename"].ToString();
            txt_mref.Text = dt.Rows[0]["min_mref"].ToString();

            pictureBox1.ImageLocation = Directory.GetCurrentDirectory() + "\\" + dt.Rows[0]["img_path"].ToString();
            oldfilnam = dt.Rows[0]["img_path"].ToString();
            chk_nobal.Checked = dt.Rows[0]["sal_mins_bal"].ToString().Equals("1")? true : false ;

            if (dt.Rows[0]["is_shaml_tax"].ToString().Equals("1"))
                rad_without.Checked = true;
            else 
                rad_with.Checked = true;

            if (Convert.ToBoolean(dt.Rows[0]["is_sections"]))
                chk_issections.Checked = true;
            else
                chk_issections.Checked = false;

            if (Convert.ToBoolean(dt.Rows[0]["multibc"]))
                chk_multibc.Checked = true;
            else
                chk_multibc.Checked = false;

            if (Convert.ToBoolean(dt.Rows[0]["is_einv_p2"]))
                chk_einv2.Checked = true;
            else
                chk_einv2.Checked = false;

            //is_cashir
            if (dt.Rows[0]["is_cashir"].ToString().Equals("0"))
                cmb_shortcut.SelectedIndex = 0;
            else if(dt.Rows[0]["is_cashir"].ToString().Equals("1"))
                cmb_shortcut.SelectedIndex = 1;
            else
                cmb_shortcut.SelectedIndex = 2;

            if(dt.Rows[0]["is_dory_mstmr"].ToString().Equals("1"))
                rd_dory.Checked = true;
            else
                rd_mstmr.Checked = true;

            if (dt.Rows[0]["br_or_sl_price"].ToString().Equals("1"))
                rd_brpr.Checked = true;
            else
                rd_slpr.Checked = true;

            if (Convert.ToBoolean( dt.Rows[0]["auto_backup"]))
                chk_enableab.Checked = true;
            else
                chk_enableab.Checked = false;

            if (Convert.ToBoolean(dt.Rows[0]["auto_tax"]))
                chk_autotax.Checked = true;
            else
                chk_autotax.Checked = false;

            if (Convert.ToBoolean(dt.Rows[0]["auto_pos_update"]))
                chk_autoposup.Checked = true;
            else
                chk_autoposup.Checked = false;

            if (Convert.ToBoolean(dt.Rows[0]["is_taqseet"]))
                chk_istaqsit.Checked = true;
            else
                chk_istaqsit.Checked = false;

            if (Convert.ToBoolean(dt.Rows[0]["is_einvoice"]))
                chk_einv.Checked = true;
            else
                chk_einv.Checked = false;

            if (Convert.ToBoolean(dt.Rows[0]["notaxchang"]))
                chk_nochngtax.Checked = true;
            else
                chk_nochngtax.Checked = false;

            if (Convert.ToBoolean(dt.Rows[0]["einv_p2_sync_actv"]))
                chk_einv_p2_sync.Checked = true;
            else
                chk_einv_p2_sync.Checked = false;

            cmb_time.SelectedIndex =Convert.ToInt32(dt.Rows[0]["auto_bak_time"])==0? 23 : Convert.ToInt32(dt.Rows[0]["auto_bak_time"]) - 1;
            cmb_einv_p2_time.SelectedIndex = Convert.ToInt32(dt.Rows[0]["einv_p2_sync_tim"]) == 0 ? 23 : Convert.ToInt32(dt.Rows[0]["einv_p2_sync_tim"]) - 1;

            cmb_madatyp.SelectedIndex = Convert.ToInt32(dt.Rows[0]["mada_type"]);
            cmb_porttyp.SelectedIndex = Convert.ToInt32(dt.Rows[0]["mada_porttyp"]);
            txt_port.Text = dt.Rows[0]["mada_portno"].ToString();
            cmb_wznsar.SelectedIndex = Convert.ToInt32(dt.Rows[0]["sar_wzn"]);
            cmb_rtype.SelectedValue = dt.Rows[0]["cmp_schem"].ToString();
            rd_m.Checked = (dt.Rows[0]["cotype"].ToString().Trim().Equals("m") || dt.Rows[0]["cotype"].ToString().Trim().Equals("a")) ? true : false;
            ch_trdfrt.Checked = dt.Rows[0]["cotype"].ToString().Trim().Equals("a") ? true : false;
            rd_p.Checked = dt.Rows[0]["cotype"].ToString().Trim().Equals("p") ? true : false;
            rd_b.Checked = dt.Rows[0]["cotype"].ToString().Trim().Equals("b") ? true : false;
            rd_s.Checked = dt.Rows[0]["cotype"].ToString().Trim().Equals("s") ? true : false;

            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Syria));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.UAE));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.SaudiArabia));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Tunisia));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Gold));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Yemen));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.nul));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Kuwait));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Qatar));

            cboCurrency.DataSource = currencies;

            cboCurrency.SelectedIndex =Convert.ToInt32(dt.Rows[0][6]);

            txt_per01.Text = datval.convert_to_ddMMyyyy_dash(dt.Rows[0]["per01"].ToString());
            txt_per02.Text = datval.convert_to_ddMMyyyy_dash(dt.Rows[0]["per02"].ToString());
            txt_per03.Text = datval.convert_to_ddMMyyyy_dash(dt.Rows[0]["per03"].ToString());
            txt_per04.Text = datval.convert_to_ddMMyyyy_dash(dt.Rows[0]["per04"].ToString());
            txt_per05.Text = datval.convert_to_ddMMyyyy_dash(dt.Rows[0]["per05"].ToString());
            txt_per06.Text = datval.convert_to_ddMMyyyy_dash(dt.Rows[0]["per06"].ToString());
            txt_per07.Text = datval.convert_to_ddMMyyyy_dash(dt.Rows[0]["per07"].ToString());
            txt_per08.Text = datval.convert_to_ddMMyyyy_dash(dt.Rows[0]["per08"].ToString());
            txt_per09.Text = datval.convert_to_ddMMyyyy_dash(dt.Rows[0]["per09"].ToString());
            txt_per10.Text = datval.convert_to_ddMMyyyy_dash(dt.Rows[0]["per10"].ToString());
            txt_per11.Text = datval.convert_to_ddMMyyyy_dash(dt.Rows[0]["per11"].ToString());
            txt_per12.Text = datval.convert_to_ddMMyyyy_dash(dt.Rows[0]["per12"].ToString());

            if (Convert.ToBoolean( dt.Rows[0]["auto_itmno"])==true)
                chk_itemauto.Checked = true;
            else
                chk_itemauto.Checked = false;

            txt_isw.Text = dt.Rows[0]["item_start_with"].ToString();

            if (Convert.ToBoolean(dt.Rows[0]["must_cost"]) == true)
                chk_mustcost.Checked = true;
            else
                chk_mustcost.Checked = false;

            if (Convert.ToBoolean(dt.Rows[0]["must_equal"]) == true)
                chk_mustequal.Checked = true;
            else
                chk_mustequal.Checked = false;

            if (Convert.ToBoolean(dt.Rows[0]["msrof_qid"]) == true)
                chk_msrofqid.Checked = true;
            else
                chk_msrofqid.Checked = false;

            if (Convert.ToBoolean(dt.Rows[0]["one_sal_srial"]) == true)
                chk_onesalserial.Checked = true;
            else
                chk_onesalserial.Checked = false;

            if (Convert.ToBoolean(dt.Rows[0]["show_qty_srch"]) == true)
                chk_showqtysrch.Checked = true;
            else
                chk_showqtysrch.Checked = false;

            if (Convert.ToBoolean(dt.Rows[0]["allw_zerop"]))
                chk_zerop.Checked = true;
            else
                chk_zerop.Checked = false;

            foreach (Control ctr in panel1.Controls)
            {
                ctr.Enabled = false;
            }

            txt_tscustno.Text = dt.Rows[0]["ts_custno"].ToString();
            txt_mdate.Text =datval.convert_to_ddMMyyyy_dash(ende.Decrypt(dt.Rows[0]["min_start"].ToString(),true));
           // txt_cmpid.Enabled = false;
           // button1.Enabled = true;

           // comboBox1_DropDownClosed(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txt_bak.Text = folderDlg.SelectedPath + @"\";
               // Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
           // txtNumber_TextChanged(null, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Control ctr in panel1.Controls)
            {
                ctr.Enabled = true;
            }
            txt_cmpid.Enabled = false;
            button1.Enabled = true;
            button4.Enabled = false;
            panel4.Enabled = false;
            panel5.Enabled = true;
            panel6.Enabled = true;
            panel7.Enabled = true;
            panel8.Enabled = true;
            panel9.Enabled = true;
            panel10.Enabled = true;
            ch_trdfrt.Enabled = true;
            chk_multibc.Enabled = true;
            chk_enableab.Enabled = true;
            cmb_time.Enabled = true;
            cmb_einv_p2_time.Enabled = true;
            chk_einv_p2_sync.Enabled = true;
            chk_mustcost.Enabled = true;
           // chk_einv.Enabled = true;
            chk_nochngtax.Enabled = true;
            chk_mustequal.Enabled = true;
            chk_zerop.Enabled = true;
            chk_msrofqid.Enabled = true;
            chk_showqtysrch.Enabled = true;
            txt_buldingno.Enabled = true;
            txt_street.Enabled = true;
            txt_site.Enabled = true;
            txt_city.Enabled = true;
            txt_cuntry.Enabled = true;
            txt_postalcode.Enabled = true;
            txt_expostal.Enabled = true;
            txt_otherid.Enabled = true;
            if(BL.CLS_Session.whatsappactv)
            txt_wmsg.Enabled = true;
            cmb_rtype.Enabled = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory() +"\\images"  ;
            ofd.Filter = "ملفات الصور |*.JPG; *.PNG; *.GIF; *.BMP";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //pictureBox1.Image = Image.FromFile(ofd.FileName);
                pictureBox1.Image = Image.FromFile(ofd.FileName);
               // imgpath = Path.GetFileName(ofd.FileName);
                string fileext = Path.GetExtension(ofd.FileName);
                fullpath = Path.GetDirectoryName(ofd.FileName);
                MessageBox.Show(fileext);
                imgpath = Path.GetFileName(ofd.FileName);
                // MessageBox.Show(fullpath + "\\" + path + "\n" + "\n" + Directory.GetCurrentDirectory() + "\\images\\" + path);
               // if (!File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + imgpath))
                //if (!File.Exists(Directory.GetCurrentDirectory() + "\\LOGO" + fileext))
                //{
                   // File.Copy(fullpath + "\\" + imgpath, Directory.GetCurrentDirectory() + "\\images\\" + imgpath, true);
                    File.Copy(fullpath + "\\" + imgpath, Directory.GetCurrentDirectory() + "\\LOGO" + fileext, true);
                    imgpathset = "LOGO" + fileext;
                    //    //  Console.WriteLine("The file exists.");
                //}
                 
            }
        }

        private void txt_mob_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            this.BackColor = colorDialog1.Color;
            textBox1.Text = "#" + (colorDialog1.Color.ToArgb() & 0x00FFFFFF).ToString("X6");
          //  BL.CLS_Session.cmp_color = "#" + (colorDialog1.Color.ToArgb() & 0x00FFFFFF).ToString("X6");

          //  BaseForm bsf = new BaseForm();
          //  bsf.BaseForm_Load( sender,  e);
         MessageBox.Show("سيتم تتطبيق تغييرات اللون عند تشغيل التطبيق مرة اخرى", "ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button6_Click(object sender, EventArgs e)
        {
           

            fontDialog1.ShowDialog();
          

            //if(fontDialog1.sele
         //   textBox1.Font = fontDialog1.Font;

            isbold = fontDialog1.Font.Bold? "1":"0";
            fontt = fontDialog1.Font.FontFamily.Name;
            fonts  =fontDialog1.Font.Size.ToString();

           // MessageBox.Show(isbold.ToString());
           // MessageBox.Show(fontt);
           //MessageBox.Show(fonts.ToString());

            if (isbold.Equals("1"))
                this.Font = new Font(fontt,float.Parse(fonts), FontStyle.Bold);
            else
                this.Font = new Font(fontt, float.Parse(fonts));
            MessageBox.Show("سيتم تتطبيق تغييرات اللون عند التشغيل مرة اخرى", "ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {
            //fontDialog1.Font.Bold = BL.CLS_Session.is_bold.Equals("1") ? true : false;
            //fontDialog1.Font.FontFamily.Name = BL.CLS_Session.font_t;
            //fontDialog1.Font.Size = float.Parse(BL.CLS_Session.font_s);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            BL.CLS_Session.cmp_color = "#BDBDDF";// "#C1C8D2";
            BL.CLS_Session.fore_color = "#000000";
            //244; 248; 249

           // colorDialog1.ShowDialog();
            this.BackColor = ColorTranslator.FromHtml(BL.CLS_Session.cmp_color);
            this.ForeColor = ColorTranslator.FromHtml(BL.CLS_Session.fore_color);
            textBox1.Text = BL.CLS_Session.cmp_color;
            txt_forcolor.Text = BL.CLS_Session.fore_color;
           /// fontDialog1.ShowDialog();


            //if(fontDialog1.sele
            //   textBox1.Font = fontDialog1.Font;

            isbold = "1";
            fontt = "Tahoma";
            fonts = "9";

            // MessageBox.Show(isbold.ToString());
            // MessageBox.Show(fontt);
            //MessageBox.Show(fonts.ToString());

            
             this.Font = new Font(fontt, float.Parse(fonts), FontStyle.Bold);
          
          //  MessageBox.Show("سيتم تتطبيق تغييرات اللون عند التشغيل مرة اخرى", "ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            EmailSetting es = new EmailSetting();
           // es.MdiParent = MdiParent;
            es.ShowDialog();
        }

        private void txtbox_Validating(MaskedTextBox txtbx, CancelEventArgs e)
        {
            DateTime rs;

            CultureInfo ci = new CultureInfo("en-US", false);

            if (!DateTime.TryParseExact(txtbx.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            {

                MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                txtbx.Focus();

            }
            if (Convert.ToInt32(datval.convert_to_yyyyDDdd(txtbx.Text)) < Convert.ToInt32(datval.convert_to_yyyyDDdd(BL.CLS_Session.start_dt)) || Convert.ToInt32(datval.convert_to_yyyyDDdd(txtbx.Text)) > Convert.ToInt32(datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt)))
            {

                MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;

            }
        }

        private void txt_per01_Validating(object sender, CancelEventArgs e)
        {
            txtbox_Validating(txt_per01,e);
        }

        private void txt_per02_Validating(object sender, CancelEventArgs e)
        {
            txtbox_Validating(txt_per02, e);
        }

        private void txt_per03_Validating(object sender, CancelEventArgs e)
        {
            txtbox_Validating(txt_per03, e);
        }

        private void txt_per04_Validating(object sender, CancelEventArgs e)
        {
            txtbox_Validating(txt_per04, e);
        }

        private void txt_per05_Validating(object sender, CancelEventArgs e)
        {
            txtbox_Validating(txt_per05, e);
        }

        private void txt_per06_Validating(object sender, CancelEventArgs e)
        {
            txtbox_Validating(txt_per06, e);
        }

        private void txt_per07_Validating(object sender, CancelEventArgs e)
        {
            txtbox_Validating(txt_per07, e);
        }

        private void txt_per08_Validating(object sender, CancelEventArgs e)
        {
            txtbox_Validating(txt_per08, e);
        }

        private void txt_per09_Validating(object sender, CancelEventArgs e)
        {
            txtbox_Validating(txt_per09, e);
        }

        private void txt_per10_Validating(object sender, CancelEventArgs e)
        {
            txtbox_Validating(txt_per10, e);
        }

        private void txt_per11_Validating(object sender, CancelEventArgs e)
        {
            txtbox_Validating(txt_per11, e);
        }

        private void txt_per12_Validating(object sender, CancelEventArgs e)
        {
            txtbox_Validating(txt_per12, e);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            button5_Click(sender, e);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            button6_Click(sender, e);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            button7_Click(sender, e);
        }

        private void rad_without_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_without.Checked)
                chk_autotax.Enabled = true;
            else
            {
                chk_autotax.Enabled = false;
                chk_autotax.Checked = false;
            }

        }

        private void btn_forcolor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            this.ForeColor = colorDialog1.Color;
            txt_forcolor.Text = "#" + (colorDialog1.Color.ToArgb() & 0x00FFFFFF).ToString("X6");
        }

        private void rd_p_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label55_Click(object sender, EventArgs e)
        {

        }

        private void label61_DoubleClick(object sender, EventArgs e)
        {
            if (!txt_mdate.Enabled && chk_enableab.Enabled)
            { //txt_mdate.Enabled = false; txt_mref.Enabled = false; txt_pass.Enabled = false; 
                //   }
                //   else
                // { 
                txt_mdate.Enabled = true; txt_mref.Enabled = true; txt_pass.Enabled = true;
            }
        }

        private void label61_Click(object sender, EventArgs e)
        {

        }

        private void txt_mdate_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"ZatkaCSID.exe");
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value == null || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[0].Value.ToString()))
                dataGridView1.CurrentRow.Cells[0].Value = BL.CLS_Session.cmp_id;
                //dataGridView1.CurrentRow.Cells[0].Value = (Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString().Length == 1 ? "0" + (Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString() : (Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage7"])//your specific tabname
            {
                // your stuff
                sqlConnection = BL.DAML.con;
                sqlDataAdapter = new SqlDataAdapter("select comp_id,fld_tag,fld_name, fld_desc, fld_edesc,fld_note from dbmstr.dbo.comp_chang_fild where comp_id= '" + BL.CLS_Session.cmp_id + "' ", sqlConnection);
                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                dataTable = new DataTable();

                sqlDataAdapter.Fill(dataTable);


                bindingSource = new BindingSource();

                bindingSource.DataSource = dataTable;


                dataGridView1.DataSource = bindingSource;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                sqlDataAdapter.Update(dataTable);
                MessageBox.Show("تم حفظ التعديلات", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                tabControl1_SelectedIndexChanged( sender,  e);

            }

            catch (Exception exceptionObj)
            {

                MessageBox.Show(exceptionObj.Message.ToString());

            }
        }
    }
}
