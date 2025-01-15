using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS.Acc
{
    public partial class Copy_Data : BaseForm
    {
        DataTable dtt, dtt2;
        //string a_slctr, a_ref, a_type, strcash, strrcash, strrcrdt, strcrdt, strdsc, strtax, strcashr, stwhno, stbrno, stsndoq, stccno;
        string dbname="";
        // SqlConnection con2 = BL.DAML.con;
        SqlConnection con3 = BL.DAML.con;
        //  BL.EncDec endc = new BL.EncDec();
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        public Copy_Data()
        {
            InitializeComponent();
        }

        private void Copy_Data_Load(object sender, EventArgs e)
        {
            txt_fmdate.Text = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            txt_tmdate.Text = DateTime.Now.AddDays(2).ToString("dd-MM-yyyy", new CultureInfo("en-US", false));



            dtt = daml.SELECT_QUIRY_only_retrn_dt("select server_name,server_id from server_links");
            cmb_salctr.DataSource = dtt;
            cmb_salctr.DisplayMember = "server_name";
            cmb_salctr.ValueMember = "server_id";
           // cmb_salctr.SelectedIndex = -1;

            //chk_as.Checked=Convert.ToBoolean(dtt.row)

            cmb_salctr.Select();
        }

        private void cmb_salctr_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void txt_fmdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_fmdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_fmdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    //  txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_fmdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_fmdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                        else
                        {
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                    }
                }

                txt_tmdate.Text = txt_fmdate.Text;
            }
            catch { }
        }

        private void txt_tmdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_tmdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_tmdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    // txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_tmdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        //  txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_tmdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                            //  txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
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

        private void cmb_salctr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_fmdate.Focus();
                // txt_mdate.Select();
                txt_fmdate.SelectionStart = 0;

            }
        }

        private void txt_fmdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                txt_tmdate.Focus();
                txt_tmdate.SelectionStart = 0;
                // txt_tmdate.SelectAll();
                // txt_hdate.SelectionStart = 0;
                //  txt_tmdate.SelectionLength = 0;

                // txt_tmdate.SelectAll();
            }
        }

        private void txt_tmdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                button2.Focus();
                // txt_hdate.SelectionStart = 0;
                //  txt_tmdate.SelectionLength = 0;

                // txt_tmdate.SelectAll();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (cmb_salctr.SelectedIndex == -1)
            {
                MessageBox.Show("يرجى اختيار الفرع اولا","تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_salctr.Select();
                return;
            }

            if ((Convert.ToDateTime(datval.convert_to_yyyy_MMddwith_dash(txt_tmdate.Text))  - Convert.ToDateTime(datval.convert_to_yyyy_MMddwith_dash(txt_fmdate.Text))).TotalDays>30)
            {
                MessageBox.Show(" لا يمكن ان تكون الفترة اكثر من 30 يوم", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);       
                return;
            }

            string whatdo = chk_as.Checked ? chk_as.Text + Environment.NewLine : "";
            whatdo = whatdo + (chk_cus.Checked ? chk_cus.Text + Environment.NewLine : "");
            whatdo = whatdo + (chk_sup.Checked ? chk_sup.Text + Environment.NewLine : "");
            whatdo = whatdo + (chk_its.Checked ? chk_its.Text + Environment.NewLine : "");
            whatdo = whatdo + (chk_ac.Checked ? chk_ac.Text + Environment.NewLine : "");
            whatdo = whatdo + (chk_sl.Checked ? chk_sl.Text + Environment.NewLine : "");
            whatdo = whatdo + (chk_pu.Checked ? chk_pu.Text + Environment.NewLine : "");
            whatdo = whatdo + (chk_st.Checked ? chk_st.Text + Environment.NewLine : "");
            whatdo = whatdo + (chk_posted.Checked ? "-------------------------" + Environment.NewLine + chk_posted.Text + Environment.NewLine : "-------------------------" + Environment.NewLine + "الحركات المرحلة والغير مرحلة" + Environment.NewLine);

            button2.Text = "يرجى الانتظار";
            button2.Enabled = false;
            using (SqlCommand cmd = new SqlCommand("copy_data_by_link"))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con3;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@fmdate", datval.convert_to_yyyyDDdd(txt_fmdate.Text));
                    cmd.Parameters.AddWithValue("@todate", datval.convert_to_yyyyDDdd(txt_tmdate.Text));
                    cmd.Parameters.AddWithValue("@link", "server" + cmb_salctr.SelectedValue);
                    cmd.Parameters.AddWithValue("@db_nam", dbname);

                    cmd.Parameters.AddWithValue("@ok_as", chk_as.Checked ? 1 : 0);
                    cmd.Parameters.AddWithValue("@ok_ac", chk_ac.Checked? 1 : 0);
                    cmd.Parameters.AddWithValue("@ok_sl", chk_sl.Checked ? 1 : 0);
                    cmd.Parameters.AddWithValue("@ok_pu", chk_pu.Checked ? 1 : 0);
                    cmd.Parameters.AddWithValue("@ok_st", chk_st.Checked ? 1 : 0);

                    cmd.Parameters.AddWithValue("@ok_it", chk_its.Checked ? 1 : 0);                   
                    cmd.Parameters.AddWithValue("@ok_su", chk_sup.Checked ? 1 : 0);
                    cmd.Parameters.AddWithValue("@ok_cu", chk_cus.Checked ? 1 : 0);
                    cmd.Parameters.AddWithValue("@ok_pstd", chk_posted.Checked ? 1 : 0);
                    cmd.Parameters.AddWithValue("@ok_dtl", chk_dtl.Checked ? 1 : 0);

                    cmd.Parameters.AddWithValue("@errstatus", 0);
                    cmd.Parameters["@errstatus"].Direction = ParameterDirection.Output;
                    if (con3.State == ConnectionState.Closed) con3.Open();
                    cmd.ExecuteNonQuery();
                    con3.Close();

                   // MessageBox.Show("Done Successfully " +cmd.Parameters["@errstatus"].Value + " " , " تم النقل بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    MessageBox.Show("تم نقل الملفات والحركات التالية بنجاح" + Environment.NewLine + Environment.NewLine + whatdo, " تم النقل بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    button2.Enabled = true;
                    button2.Text = "جلب البيانات";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + cmd.Parameters["@errstatus"].Value, "Error خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button2.Enabled = true;
                    button2.Text = "جلب البيانات";
                }
            }
        }

        private void cmb_salctr_Leave(object sender, EventArgs e)
        {
            dtt2 = daml.SELECT_QUIRY_only_retrn_dt("select * from server_links where server_id='" + cmb_salctr.SelectedValue + "'");

            if (dtt2.Rows.Count > 0)
            {
                dbname = dtt2.Rows[0]["db"].ToString();
                chk_as.Checked = Convert.ToBoolean(dtt2.Rows[0]["acc"]) ? true : false;
                chk_cus.Checked = Convert.ToBoolean(dtt2.Rows[0]["cus"]) ? true : false;
                chk_sup.Checked = Convert.ToBoolean(dtt2.Rows[0]["sup"]) ? true : false;
                chk_its.Checked = Convert.ToBoolean(dtt2.Rows[0]["its"]) ? true : false;
                chk_ac.Checked = Convert.ToBoolean(dtt2.Rows[0]["ac"]) ? true : false;
                chk_pu.Checked = Convert.ToBoolean(dtt2.Rows[0]["pu"]) ? true : false;
                chk_sl.Checked = Convert.ToBoolean(dtt2.Rows[0]["sl"]) ? true : false;
                chk_st.Checked = Convert.ToBoolean(dtt2.Rows[0]["st"]) ? true : false;
                chk_posted.Checked = Convert.ToBoolean(dtt2.Rows[0]["posted"]) ? true : false;
                chk_dtl.Checked = Convert.ToBoolean(dtt2.Rows[0]["dtl"]) ? true : false;
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_fmdate_Enter(object sender, EventArgs e)
        {
            txt_fmdate.SelectAll();
        }

        private void txt_tmdate_Enter(object sender, EventArgs e)
        {
            txt_tmdate.SelectAll();
        }
    }
}

