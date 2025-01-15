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

namespace POS.Pos
{
    public partial class CTR_FRM : BaseForm
    {
        CurrencyManager cm;

        int flag1;
        BL.DAML daml = new BL.DAML();
        DataTable dt;
        List<BL.CurrencyInfo> currencies = new List<BL.CurrencyInfo>();

        SqlConnection con = BL.DAML.con;//new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        public CTR_FRM()
        {
            InitializeComponent();

            txt_cid.Enabled = false;
            cmb_slctr.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
            using (SqlCommand cmd = new SqlCommand("update contrs set contr_name=@a2,msg1=@m1,msg2=@m2,c_slno=@sl,[usecd]=@ucd,[portno]=@pn,[cdmsg]=@cm,[print_type]=@prtt,[print_rtn]=@prtn,[is_tuch]=@ts,[impitm]=@ii,[is_cofi]=@ic,[is_small]=@is where c_brno='" + txt_brno.Text + "' and  c_slno='" + cmb_slctr.SelectedValue + "' and contr_id=" + txt_cid.Text + "", con))
            {

                //cboCurrency.SelectedValue
                cmd.Parameters.AddWithValue("@a2", txt_cnam.Text);
              //  cmd.Parameters.AddWithValue("@a3", textBox3.Text);
              //  cmd.Parameters.AddWithValue("@a4", textBox4.Text);
                cmd.Parameters.AddWithValue("@m1", txt_msg1.Text);
                cmd.Parameters.AddWithValue("@m2", txt_msg2.Text);
                cmd.Parameters.AddWithValue("@sl", cmb_slctr.SelectedValue);
                cmd.Parameters.AddWithValue("@ucd", chk_screenuse.Checked? 1 : 0);
                cmd.Parameters.AddWithValue("@pn", txt_portno.Text);
                cmd.Parameters.AddWithValue("@cm", txt_cdtext.Text);
                cmd.Parameters.AddWithValue("@prtt", cmb_prttyp.SelectedIndex);
                cmd.Parameters.AddWithValue("@prtn", chk_printrtn.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@ts", chk_tuchscrin.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@ii", chk_impitm.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@ic", chk_iscofi.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@is", chk_smallscrn.Checked ? 1 : 0);
             //   cmd.Parameters.AddWithValue("@on", textBox8.Text);
             //   cmd.Parameters.AddWithValue("@cur", cboCurrency.SelectedValue);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("modifed success تم التعديل", "ok", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void CMP_FRM_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select sl_no,sl_name from slcenters where sl_brno='" + BL.CLS_Session.brno + "'");
                cmb_slctr.DataSource = dt2;
                cmb_slctr.DisplayMember = "sl_name";
                cmb_slctr.ValueMember = "sl_no";

                // button1.Text = "تعديل";
                //SqlDataAdapter da = new SqlDataAdapter("select c_brno,c_slno,contr_id,contr_name,[msg1],[msg2],[usecd],[portno],[cdmsg],[print_type] from contrs where c_brno='" + BL.CLS_Session.brno + "'", con);
                SqlDataAdapter da = new SqlDataAdapter("select contr_id from contrs where c_brno='" + BL.CLS_Session.brno + "'", con);

                dt = new DataTable();
                da.Fill(dt);

                txt_t.DataBindings.Add("Text", dt, "contr_id");
                cm = (CurrencyManager)BindingContext[dt];
                txt_mov.Text = (cm.Position + 1) + "/" + (dt.Rows.Count);
                Fill_Data(txt_t.Text);

                txt_cid.Text = dt.Rows[0][2].ToString();
                txt_cnam.Text = dt.Rows[0][3].ToString();
                //   textBox3.Text = dt.Rows[0][2].ToString();
                //   textBox4.Text = dt.Rows[0][3].ToString();
                txt_brno.Text = dt.Rows[0][0].ToString();
                txt_msg1.Text = dt.Rows[0][4].ToString();
                txt_msg2.Text = dt.Rows[0][5].ToString();
                txt_cdtext.Text = dt.Rows[0]["cdmsg"].ToString();
                txt_portno.Text = dt.Rows[0]["portno"].ToString();

                if (Convert.ToInt32((string.IsNullOrEmpty(dt.Rows[0]["print_type"].ToString()) ? 0 : dt.Rows[0]["print_type"])) == 0)
                    cmb_prttyp.SelectedIndex = 0;
                else
                    cmb_prttyp.SelectedIndex = 1;

                //  cmb_slctr.SelectedValue = dt.Rows[0][0].ToString();

                if (Convert.ToBoolean(dt.Rows[0]["usecd"]) == true)
                    chk_screenuse.Checked = true;
                else
                    chk_screenuse.Checked = false;

                if (Convert.ToBoolean(dt.Rows[0]["print_rtn"]) == true)
                    chk_printrtn.Checked = true;
                else
                    chk_printrtn.Checked = false;
                //  textBox8.Text = dt.Rows[0][7].ToString();
                /*
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Syria));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.UAE));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.SaudiArabia));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Tunisia));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Gold));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Yemen));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.nul));

                cboCurrency.DataSource = currencies;

                cboCurrency.SelectedIndex =Convert.ToInt32(dt.Rows[0][8]);
                */
                // comboBox1_DropDownClosed(null, null);




                // cmb_slctr_fill();
            }
            catch { };
        }

        private void button3_Click(object sender, EventArgs e)
        {/*
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox4.Text = folderDlg.SelectedPath + @"\";
               // Environment.SpecialFolder root = folderDlg.RootFolder;
            }
          */
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
           // txtNumber_TextChanged(null, null);
        }

        private void cmb_slctr_fill()
        {
           

        }

        private void designToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FastReport.Report rpt2 = new FastReport.Report();

                if (flag1 == 1 || numericUpDown1.Focus()==true)
                { //rpt2.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt.frx");
                    if (numericUpDown1.Value == 1)
                        rpt2.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt.frx");
                    else
                        rpt2.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt" + numericUpDown1.Value + ".frx");
                }
                else
                    rpt2.Load(Environment.CurrentDirectory + @"\reports\Pos_RSale_rpt.frx");
                // rpt2.SetParameterValue("br_name", BL.CLS_Session.brname);
                // rpt2.RegisterData(dt3, "items");

                //rpt.PrintSettings.ShowDialog = false;
                //rpt.PrintSettings.Printer = bprinter;
                //rpt.PrintSettings.Copies = Convert.ToInt32(txt_count.Text);

                //rpt.Print();
                // rpt2.Design();

                FR_Designer rptd = new FR_Designer(rpt2);
                // Report_Designer rptd = new Report_Designer();
                // rptd.designerControl1.Report = rpt;
                // rptd.Text = rptd.Text + report1.FileName;
                rptd.MdiParent = MdiParent;
                rptd.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lbl_Sales_Enter(object sender, EventArgs e)
        {
            flag1 = 1;
        }

        private void lbl_RSales_Enter(object sender, EventArgs e)
        {
            lbl_RSales.Focus();
            flag1 = 2;
        }

        private void lbl_Sales_MouseHover(object sender, EventArgs e)
        {
            flag1 = 1;
        }

        private void lbl_RSales_MouseHover(object sender, EventArgs e)
        {
            lbl_RSales.Focus();
            flag1 = 2;
        }

        private void cmb_prttyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_prttyp.SelectedIndex == 0)
            {
                lbl_Sales.Visible = false;
                lbl_RSales.Visible = false;
            }
            else
            {
                lbl_Sales.Visible = true;
                lbl_RSales.Visible = true;
            }

        }

        private void lbl_Sales_DoubleClick(object sender, EventArgs e)
        {
            designToolStripMenuItem_Click(sender, e);
        }

        private void lbl_RSales_DoubleClick(object sender, EventArgs e)
        {
            lbl_RSales.Focus();
            flag1 = 2;
            designToolStripMenuItem_Click(sender, e);
        }

        private void txt_cid_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btn_first_Click(object sender, EventArgs e)
        {
            cm.Position = dt.Rows.Count - 1;
            txt_mov.Text = (cm.Position + 1) + "/" + (dt.Rows.Count);
            Fill_Data(txt_t.Text);
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            cm.Position += 1;
            txt_mov.Text = (cm.Position + 1) + "/" + (dt.Rows.Count);
            Fill_Data(txt_t.Text);
        }

        private void btn_prvs_Click(object sender, EventArgs e)
        {
            cm.Position -= 1;
            txt_mov.Text = (cm.Position + 1) + "/" + (dt.Rows.Count);
            Fill_Data(txt_t.Text);
        }

        private void btn_last_Click(object sender, EventArgs e)
        {
            cm.Position = 0;
            txt_mov.Text = (cm.Position + 1) + "/" + (dt.Rows.Count);
            Fill_Data(txt_t.Text);
        }

        private void txt_t_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Fill_Data(string txts)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select c_brno,c_slno,contr_id,contr_name,[msg1],[msg2],[usecd],[portno],[cdmsg],[print_type],[print_rtn],[is_tuch],[impitm],[is_cofi],[is_small] from contrs where c_brno='" + BL.CLS_Session.brno + "' and contr_id=" + txts + " ", con);

                DataTable dt1 = new DataTable();
                da.Fill(dt1);



                txt_cid.Text = dt1.Rows[0][2].ToString();
                txt_cnam.Text = dt1.Rows[0][3].ToString();
                //   textBox3.Text = dt.Rows[0][2].ToString();
                //   textBox4.Text = dt.Rows[0][3].ToString();
                txt_brno.Text = dt1.Rows[0][0].ToString();
                txt_msg1.Text = dt1.Rows[0][4].ToString();
                txt_msg2.Text = dt1.Rows[0][5].ToString();
                txt_cdtext.Text = dt1.Rows[0]["cdmsg"].ToString();
                txt_portno.Text = dt1.Rows[0]["portno"].ToString();

                if (Convert.ToInt32((string.IsNullOrEmpty(dt1.Rows[0]["print_type"].ToString()) ? 0 : dt1.Rows[0]["print_type"])) == 0)
                    cmb_prttyp.SelectedIndex = 0;
                else
                    cmb_prttyp.SelectedIndex = 1;

                //  cmb_slctr.SelectedValue = dt.Rows[0][0].ToString();

                if (Convert.ToBoolean(dt1.Rows[0]["usecd"]) == true)
                    chk_screenuse.Checked = true;
                else
                    chk_screenuse.Checked = false;

                if (Convert.ToBoolean(dt1.Rows[0]["print_rtn"]) == true)
                    chk_printrtn.Checked = true;
                else
                    chk_printrtn.Checked = false;

                if (Convert.ToBoolean(dt1.Rows[0]["is_tuch"]) == true)
                    chk_tuchscrin.Checked = true;
                else
                    chk_tuchscrin.Checked = false;

                if (Convert.ToBoolean(dt1.Rows[0]["impitm"]) == true)
                    chk_impitm.Checked = true;
                else
                    chk_impitm.Checked = false;

                if (Convert.ToBoolean(dt1.Rows[0]["is_cofi"]) == true)
                    chk_iscofi.Checked = true;
                else
                    chk_iscofi.Checked = false;

                if (Convert.ToBoolean(dt1.Rows[0]["is_small"]) == true)
                    chk_smallscrn.Checked = true;
                else
                    chk_smallscrn.Checked = false;
            }
            catch { }

        }

        private void lbl_Sales_Click(object sender, EventArgs e)
        {
            flag1 = 1;
            FastReport.Report rpt2 = new FastReport.Report();
            rpt2.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt.frx");
        }

        private void lbl_RSales_Click(object sender, EventArgs e)
        {
            flag1 = 2;
            FastReport.Report rpt2 = new FastReport.Report();
            rpt2.Load(Environment.CurrentDirectory + @"\reports\Pos_RSale_rpt.frx");
        }

    }
}
