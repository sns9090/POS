using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Pos
{
    public partial class Pos_Tafseel : BaseForm
    {
        double tax_per = BL.CLS_Session.tax_per, oval;
        List<BL.CurrencyInfo> currencies = new List<BL.CurrencyInfo>();
        // public Sal_Tran_D frm;
        // DataTable dtg;
        // DataTable dt2,dtunits;
        string inv_t, inv_r, inv_s;

        DataTable dthdr, dtdtl, dt222, dtunits, dtsal, dtvat, dttrtyps, dtslctr, dtcolman, dtexits, dttodel, dtcust;//,dtbind;
        // int count = 0;
        int ref_max, cnt = 0, cntto = 0, temp = 0, jrdacc, print_count = 0, ccc, cur_row, typeno = 1, refmax, rqmax;
        string a_slctr, a_ref, a_type, strcash, strdcash, strrcash, strrcrdt, strcrdt, strdsc, strtax, strcashr, stwhno, strsndoq, stcstcode, stslcust, stmkhzon, cardacc, otheracc, freetax = "";
        double card_amt = 0, other_amt = 0;
        bool isnew, isupdate, notposted, shameltax, isfeched, aqd_temp, is_printd = false, is_cash = false;
        string sl_brno, sl_id, sl_name, sl_password, slpmaxdisc, maxitmdsc, reftosend, printer_nam = "";
        bool sl_chgqty, sl_chgprice, sl_delline, sl_delinv, sl_return, sl_admin, sl_alowdisc, sl_alowexit, alwreprint, alwitmdsc, sl_inactive, slalwdesc;
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate dtv = new BL.Date_Validate();
        SqlConnection con = BL.DAML.con;//new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        BindingSource bindsrc = new BindingSource();
        public Pos_Tafseel(string slctr, string atype, string aref)
        {
            InitializeComponent();
            //  this.KeyPreview = true;
            a_ref = aref;
            a_type = atype;
            a_slctr = slctr;
            //this.KeyPreview = true;
            //this.KeyPress += new KeyPressEventHandler(Control_KeyPress);

            // backgroundWorker1.DoWork += backgroundWorker1_DoWork;

        }

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        SelectNextControl(ActiveControl, true, true, true, true);
        //        e.Handled = true;
        //    }
        //}

        private const int CP_DISABLE_CLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CP_DISABLE_CLOSE_BUTTON;
                return cp;
            }
        }
        //public Pos_Tafseel()
        //{
        //    InitializeComponent();
        //}

        private void Pos_Tafseel_Load(object sender, EventArgs e)
        {
            if (!BL.CLS_Session.formkey.Contains("B121"))
            {
                //  this.Close();
            }
            else
            {
                PrinterSettings settings = new PrinterSettings();
                printer_nam = settings.PrinterName;

                // this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Syria));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.UAE));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.SaudiArabia));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Tunisia));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Gold));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Yemen));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.nul));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Kuwait));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Qatar));

                //// this.dataGridView1.CellValidated += new DataGridViewCellEventHandler(dataGridView1_CellValidated);


                // dtunits = daml.SELECT_QUIRY_only_retrn_dt("select * from units");
                // dtvat = daml.SELECT_QUIRY_only_retrn_dt("select * from taxs");
                // // MessageBox.Show(dtv.convert_to_ddDDyyyy("20180526"));

                // // dtg = dataGridView1.DataSource;

                // // dataGridView1.Columns["SN"].ReadOnly = true;
                try
                {


                    txt_mdate.Text = DateTime.Now.AddSeconds((BL.CLS_Session.posatrtday) * -3600).ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
                    txt_hdate.Text = DateTime.Now.AddSeconds((BL.CLS_Session.posatrtday) * -3600).ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));

                    if (BL.CLS_Session.is_einv_p2 && Convert.ToDouble(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) >= Convert.ToDouble(BL.CLS_Session.einv_p2_date))
                        btn_zatkasnd.Enabled = true;

                    //  dataGridView1.TopLeftHeaderCell.Value = "م";


                    string tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
                    // MessageBox.Show(tr);
                    ////dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('04','05','14','15')");
                    ////cmb_type.DataSource = dttrtyps;
                    ////cmb_type.DisplayMember = BL.CLS_Session.lang.Equals("E") ? "tr_ename" : "tr_name";
                    ////cmb_type.ValueMember = "tr_no";
                    ////cmb_type.SelectedIndex = -1;

                    string sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";
                    // MessageBox.Show(sl);
                    dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select * from slcenters where sl_brno + sl_no in(" + sl + ") and sl_brno='" + BL.CLS_Session.brno + "'");
                    cmb_salctr.DataSource = dtslctr;
                    cmb_salctr.DisplayMember = "sl_name";
                    cmb_salctr.ValueMember = "sl_no";


                    //dtcolman = daml.SELECT_QUIRY_only_retrn_dt("select * from sal_men where sp_brno='" + BL.CLS_Session.brno + "'");

                    //cmb_salman.DataSource = dtcolman;
                    //cmb_salman.DisplayMember = "sp_name";
                    //cmb_salman.ValueMember = "sp_id";
                    //cmb_salman.SelectedIndex = -1;

                    //dtexits = daml.SELECT_QUIRY_only_retrn_dt("select code, cname, address, tel FROM sltrans");

                    //cmb_exits.DataSource = dtexits;
                    //cmb_exits.DisplayMember = "cname";
                    //cmb_exits.ValueMember = "code";
                    //cmb_exits.SelectedIndex = -1;

                    //cmb_salman.Enabled = false;
                    //// cmb_salman.SelectedIndex = -1;
                    cmb_type.Enabled = false;
                    txt_custno.Enabled = false;
                    txt_mobile.Enabled = false;
                    txt_itemno.Enabled = false;
                    txt_remark.Enabled = false;
                    cmb_salctr.Enabled = false;
                   // txt_ref.Enabled = false;
                    txt_hdate.Enabled = false;
                    txt_mdate.Enabled = false;
                    txt_desc.Enabled = false;
                    txt_nqd.Enabled = false;
                    txt_card.Enabled = false;
                    panel7.Enabled = false; panel2.Enabled = false;
                    //txt_ccno.Enabled = false;
                    //btn_payment.Enabled = false;
                    //txt_aqd.Enabled = false;
                    //txt_paid.Enabled = false;
                    //cmb_exits.Enabled = false;
                    //txt_remark.Enabled = false;
                    //txt_note2.Enabled = false;
                    //txt_note3.Enabled = false;
                    //txt_mpay.Enabled = false;
                    //txt_mobile.Enabled = false;
                    //txt_reref.Enabled = false;
                    //txt_key.Enabled = false;
                    //txt_custno.Enabled = false;
                    //txt_des.Enabled = false;
                    //txt_desper.Enabled = false;
                    //dataGridView1.ReadOnly = true;

                    /*
                    if (string.IsNullOrEmpty(a_ref))
                        //  Fill_Data(a_slctr, a_type, a_ref);
                        Fill_Data(txt_s.Text, txt_t.Text, txt_r.Text);
                    else
                        Fill_Data(a_slctr, a_type, a_ref);
                    */

                    //  dataGridView1.AllowUserToAddRows = false;
                    //dataGridView1.Select();
                    // dataGridView1.BeginEdit(true);
                    // dataGridView1.Rows.Add(10);

                    // this.Owner.Enabled = false;

                    //  Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                    string str2 = BL.CLS_Session.formkey;
                    string whatever = str2.Substring(str2.IndexOf("B121") + 5, 3);
                    if (whatever.Substring(0, 1).Equals("0"))
                        btn_Add.Visible = false;
                    if (whatever.Substring(1, 1).Equals("0"))
                        btn_Edit.Visible = false;
                    if (whatever.Substring(2, 1).Equals("0"))
                        btn_Del.Visible = false;

                    if (BL.CLS_Session.up_us_post == false)
                        btn_Post.Visible = false;
                }
                catch { }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddDays(1);
            //chk_nodup.Checked = true;
            tax_per = BL.CLS_Session.tax_per;

            //btn_Rfrsh.Enabled = false;
            //btn_qbl.Enabled = false;
            //btn_tali.Enabled = false;
            //chk_usebarcode.Enabled = true;

            txt_taxid.Enabled = true;
            txt_taxid.Text = "";
            txt_custno.Text = "";
            freetax = "";
            //   txt_custno.Text = "";
            txt_custnam.Text = "";
            txt_remark.Text = "";
            // chk_suspend.Checked = false;
            txt_total.Text = "0";
            //txt_des.Text = "0";
            //txt_desper.Text = "0";
            //txt_net.Text = "0";
            txt_tax.Text = "0";
            //txt_taxfree.Text = "0";
            //txt_paid.Text = "0";
            //txt_aqd.Text = "";
            //txt_reref.Text = "";
            //  txt_key.Text = BL.CLS_Session.brcash;
            //  load_key();
            // dthdr.Rows.Clear();
            // dtdtl.Rows.Clear();
            isnew = true;
            isupdate = false;
            is_printd = false;
            btn_Save.Enabled = true;
            btn_zatkasnd.Enabled = false;
            btn_Add.Enabled = false;
            btn_Undo.Enabled = true;
            btn_Exit.Enabled = false;
            btn_Find.Enabled = false;
            btn_Del.Enabled = false;
            btn_prtdirct.Enabled = false;
            btn_Print.Enabled = false;
            btn_Edit.Enabled = false;
            btn_Post.Enabled = false;
            txt_nqd.Enabled = true;
            txt_card.Enabled = true;
            txt_itemno.Enabled = true;
            // chk_nodup.Enabled = true;
            cmb_type.Enabled = true;

            //if (BL.CLS_Session.up_suspend == false)
            //    chk_suspend.Enabled = false;
            //else
            //    chk_suspend.Enabled = true;

            //if (BL.CLS_Session.isshamltax.Equals("2"))
            //{
            //    chk_shaml_tax.Checked = true;
            //    chk_shaml_tax.Enabled = BL.CLS_Session.notxchng ? false : true;
            //}
            //else
            //{
            //    chk_shaml_tax.Checked = false;
            //    chk_shaml_tax.Enabled = BL.CLS_Session.notxchng ? false : true;
            //}

            cmb_type.SelectedIndex = 0;

            cmb_salctr.Enabled = true;
            cmb_salctr.SelectedIndex = 0;

            //cmb_salman.Enabled = true;
            //cmb_salman.SelectedIndex = -1;


            //txt_ref.Enabled = false;
            if (BL.CLS_Session.up_chang_dt && !BL.CLS_Session.is_einv)
            {
                txt_hdate.Enabled = true;
                txt_mdate.Enabled = true;
            }
            else
            {
                txt_hdate.Enabled = false;
                txt_mdate.Enabled = false;
            }
            txt_desc.Enabled = true;
            //txt_ccno.Enabled = true;
            //btn_payment.Enabled = true;
            //txt_aqd.Enabled = true;
            //txt_paid.Enabled = true;
            //cmb_exits.Enabled = true;
            txt_remark.Enabled = true;
            //txt_note2.Enabled = true;
            //txt_note3.Enabled = true;
            //txt_mpay.Enabled = true;
            txt_mobile.Enabled = true;
            //txt_reref.Enabled = true;
            //txt_key.Enabled = true;
            txt_custno.Enabled = true;
            //txt_des.Enabled = true;
            //txt_desper.Enabled = true;
            txt_user.Text = BL.CLS_Session.UserName;
            panel7.Enabled = true; panel2.Enabled = true;
            cmb_type.Focus();
        }

        private void txt_mdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_mdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_mdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_mdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_mdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                            txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                        else
                        {
                            txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                    }
                }
            }
            catch { }
        }
        private string convert_to_mdate(string input)
        {
            input = (txt_hdate.Text.Replace("-", "").Substring(4, 4)) + "-" + txt_hdate.Text.Replace("-", "").Substring(2, 2) + "-" + txt_hdate.Text.Replace("-", "").Substring(0, 2);

            DateTime dt = Convert.ToDateTime(input, new CultureInfo("ar-SA", false));
            //  DateTime dt = Convert.ToDateTime(txt_mdate.Text.ToString("yyyy-MM-dd"), new CultureInfo("en-US"));
            // txt_hdate.Text = dt.ToString("ddMMyyyy", new CultureInfo("ar-SA"));

            input = dt.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

            return input;

        }

        private string convert_to_hdate(string input)
        {
            input = (txt_mdate.Text.Replace("-", "").Trim().Substring(4, 4)) + "-" + txt_mdate.Text.Replace("-", "").Substring(2, 2) + "-" + txt_mdate.Text.Replace("-", "").Substring(0, 2);

            DateTime dt = Convert.ToDateTime(input, new CultureInfo("en-US", false));
            //  DateTime dt = Convert.ToDateTime(txt_mdate.Text.ToString("yyyy-MM-dd"), new CultureInfo("en-US"));
            // txt_hdate.Text = dt.ToString("ddMMyyyy", new CultureInfo("ar-SA"));

            input = dt.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));

            return input;

        }

        private void txt_hdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr2 = txt_hdate.Text.ToString().Replace("-", "").Trim();
                if (datestr2.Length == 0)
                {
                    txt_hdate.Text = DateTime.Now.ToString("dd", new CultureInfo("ar-SA", false)) + DateTime.Now.ToString("MM", new CultureInfo("ar-SA", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("ar-SA", false));
                    txt_mdate.Text = convert_to_mdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr2.Length == 2)
                    {
                        txt_hdate.Text = datestr2 + DateTime.Now.ToString("MM", new CultureInfo("ar-SA", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("ar-SA", false));
                        txt_mdate.Text = convert_to_mdate(txt_hdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr2.Length == 4)
                        {
                            txt_hdate.Text = datestr2 + DateTime.Now.ToString("yyyy", new CultureInfo("ar-SA", false));
                            txt_mdate.Text = convert_to_mdate(txt_hdate.Text.ToString());
                        }
                        else
                        {
                            txt_mdate.Text = convert_to_mdate(txt_hdate.Text.ToString());
                        }
                    }
                }
            }
            catch { }
        }

        private void txt_desc_Enter(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("ar-SA", false));
        }

        private void txt_tol_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (btn_Post.Enabled == true)
            {
                if (BL.CLS_Session.oneslserial && 0 == 1)
                    cmb_type.Enabled = true;
                else
                    cmb_type.Enabled = false;
                // chk_nodup.Enabled = true;
                btn_zatkasnd.Enabled = false;

                notposted = true;
                isnew = false;
                isupdate = true;
                btn_Save.Enabled = true;
                btn_Add.Enabled = false;
                btn_Undo.Enabled = true;
                btn_Exit.Enabled = false;
                btn_Find.Enabled = false;
                btn_Del.Enabled = false;
                btn_prtdirct.Enabled = false;
                btn_Print.Enabled = false;
                btn_Edit.Enabled = false;
                btn_Post.Enabled = false;
                // cmb_salman.Enabled = true;
                txt_taxid.Enabled = true;
                //  chk_nodup.Enabled = true;
                // cmb_salman.SelectedIndex = -1;
                //txt_ref.Enabled = false;
                if (BL.CLS_Session.up_chang_dt && !BL.CLS_Session.is_einv)
                {
                    txt_hdate.Enabled = true;
                    txt_mdate.Enabled = true;
                }
                else
                {
                    txt_hdate.Enabled = false;
                    txt_mdate.Enabled = false;
                }
                // chk_nodup.Checked = true;
                txt_desc.Enabled = true;

                txt_remark.Enabled = true;

                txt_mobile.Enabled = true;

                txt_custno.Enabled = true; txt_itemno.Enabled = true; txt_nqd.Enabled = true; txt_card.Enabled = true;

                panel7.Enabled = true; panel2.Enabled = true;
            }
            else
            {
                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Can't edit posted trans. " : "لا يمكن تعديل حركة مرحلة", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void cmb_type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (txt_mdate.Enabled == true)
                {
                    txt_mdate.Focus();
                    // txt_mdate.Select();
                    txt_mdate.SelectionStart = 0;
                    //   txt_mdate.SelectionLength = 0;// txt_mdate.Text.Length;  
                }
                else
                {
                    txt_desc.Focus();
                }
            }
        }

        private void cmb_type_Leave(object sender, EventArgs e)
        {

            try
            {
                // MessageBox.Show("يجب التاكد من نوع الفاتورة قبل الحفظ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // if (string.IsNullOrEmpty(txt_desc.Text))
                txt_desc.Text = cmb_type.Text + " - " + cmb_salctr.Text;
                // cmb_salctr_Leave(null, null);



            }
            catch { }
        }

        private void txt_desc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_custno.Focus();
            }
        }

        private void txt_custno_Leave(object sender, EventArgs e)
        {
            load_key();
        }

        private void load_key()
        {
            DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select cu_no ,cu_name ,cl_acc,cu_crlmt,vndr_taxcode,cu_alwcr,c_bulding_no,  c_street, c_site_name, c_city, c_cuntry, c_postal_code, c_ex_postalcode, c_other_id,cu_kind,cu_addrs,cu_tel from customers a join cuclass b on a.cu_class=b.cl_no  where a.inactive=0 and a.cu_brno='" + BL.CLS_Session.brno + "' and cu_no='" + txt_custno.Text + "'");

            // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

            if (dt2.Rows.Count > 0)
            {
                txt_custnam.Text = dt2.Rows[0][1].ToString();
                //  txt_temp.Text = dt2.Rows[0][2].ToString();
                // txt_crlmt.Text = dt2.Rows[0][3].ToString();
                txt_taxid.Text = dt2.Rows[0]["vndr_taxcode"].ToString();
                txt_mobile.Text = dt2.Rows[0]["cu_tel"].ToString();
                is_cash = Convert.ToBoolean(dt2.Rows[0]["cu_alwcr"]) ? true : false;
                dtcust = dt2;
                freetax = dt2.Rows[0]["cu_kind"].ToString();
            }
            else
            {
                txt_custnam.Text = "";
                txt_custno.Text = "";
                // txt_temp.Text = "";
                // txt_crlmt.Text = "";
                // txt_mobile.Text =!string.IsNullOrEmpty(txt_custno.Text)? txt_mobile.Text : "";
                is_cash = false;
                dtcust = null;
                // freetax = dt.Rows.Count > 0 && freetax.Equals("1") ? freetax : "";
            }
            //    txt_code.Text = dth.Rows[0][2].ToString();
            //   txt_opnbal.Text = dth.Rows[0][1].ToString();
            total();
        }

        private void total()
        {
            try
            {
                txt_total.Text = (Math.Round(((Convert.ToDouble(txt_qty.Text) * Convert.ToDouble(txt_price.Text)) - Convert.ToDouble(txt_discont.Text)), 2)).ToString();
                txt_tax.Text = (Math.Round(((Convert.ToDouble(txt_total.Text) * 3) / 23), 4)).ToString();
                txt_baqi.Text = (Math.Round((Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_wasel.Text)), 2)).ToString();
            }
            catch { }
        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {
            if ((Math.Round((Convert.ToDouble(txt_nqd.Text) + Convert.ToDouble(txt_card.Text)), 2)) > Convert.ToDouble(txt_total.Text))
                MessageBox.Show("لا يمكن دفع مبلغ اكبر من قيمة الفاتورة", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                txt_wasel.Text = (Math.Round((Convert.ToDouble(txt_nqd.Text) + Convert.ToDouble(txt_card.Text)), 2)).ToString();
                total();
            }
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {

        }

        private void btn_Undo_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Do you want to undo" : "هل تريد التراجع عن التغييرات", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {



                if (isupdate)
                {
                    isupdate = false;
                    // btn_Save_Click(sender, e);
                    //  Fill_Data(cmb_salctr.SelectedValue.ToString(), cmb_type.SelectedValue.ToString(), txt_ref.Text);
                    btn_Save.Enabled = false;
                    btn_Add.Enabled = true;
                    btn_Undo.Enabled = false;
                    btn_Exit.Enabled = true;
                    btn_Find.Enabled = true;
                    btn_Edit.Enabled = true;
                    // dataGridView1.ReadOnly = true;
                    //  dataGridView1.AllowUserToAddRows = false;
                    cmb_type.Enabled = false;
                    //  cmb_salman.Enabled = false;
                   // txt_ref.Enabled = false;
                    txt_remark.Enabled = false;
                    //  txt_note2.Enabled = false;
                    //  txt_note3.Enabled = false;
                    //  txt_mpay.Enabled = false;
                    txt_mobile.Enabled = false;
                    //  txt_reref.Enabled = false;
                    //  txt_key.Enabled = false;
                    txt_custno.Enabled = false;
                    //  chk_suspend.Enabled = false;
                    //  button1.Enabled = true;
                    panel2.Enabled = false; panel7.Enabled = false;
                }
                else
                {
                    isnew = false;
                    btn_Save.Enabled = false;
                    btn_Add.Enabled = true;
                    btn_Undo.Enabled = false;
                    btn_Exit.Enabled = true;
                    btn_Find.Enabled = true;
                    btn_Edit.Enabled = false;
                    panel2.Enabled = false; panel7.Enabled = false;
                    // chk_suspend.Enabled = false;
                    // button1.Enabled = true;

                    //try
                    //{
                    //    if (dataGridView1.Rows.Count > 0)
                    //    {
                    //        //  ((DataTable)dataGridView1.DataSource).Rows.Clear();
                    //        // dataGridView1.DataSource = null;
                    //        dataGridView1.Rows.Clear();
                    //        dataGridView1.Refresh();
                    //        //dataGridView1.DataSource = null;
                    //        //dataGridView1.Refresh();
                    //    }
                    //}
                    //catch { }

                    //dataGridView1.ReadOnly = true;

                    cmb_type.Enabled = false;
                    // chk_suspend.Enabled = false;
                    //txt_ref.Enabled = false;
                    //  dataGridView1.AllowUserToAddRows = false;

                    cmb_type.SelectedIndex = -1;
                    // txt_amt.Text = "0";
                    txt_desc.Text = "";
                    freetax = "";
                    // cmb_salman.Enabled = false;
                    // cmb_salman.SelectedIndex = -1;
                    /*
                    if (dataGridView1.Rows.Count-1 > 0)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                dataGridView1.Rows.Remove(row);
                            }

                        }
                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                    }
                     */
                    //try
                    //{
                    //    if (dataGridView1.Rows.Count > 0)
                    //    {
                    //        ((DataTable)dataGridView1.DataSource).Rows.Clear();
                    //    }
                    //}
                    //catch { }
                    //dataGridView1.AllowUserToAddRows = false;
                    txt_remark.Enabled = false;
                }


                // Acc_Tran_Load(sender,e);

                txt_mdate.Enabled = false;
                txt_hdate.Enabled = false;
                txt_desc.Enabled = false;
                txt_nqd.Enabled = false;
                txt_card.Enabled = false;
                txt_custno.Enabled = false;
                txt_taxid.Enabled = false;
                txt_mobile.Enabled = false;
                //txt_ccno.Enabled = false;
                //btn_payment.Enabled = false;
                //txt_aqd.Enabled = false;
                //txt_paid.Enabled = false;
                //cmb_exits.Enabled = false;
                //txt_des.Enabled = false;
                //txt_desper.Enabled = false;
                //txt_amt.Enabled = false;
                cmb_type.Enabled = false;
                cmb_salctr.Enabled = false;
                //txt_ref.Enabled = false;
                //btn_Rfrsh.Enabled = true;
                //btn_qbl.Enabled = true;
                //btn_tali.Enabled = true;
                btn_Print.Enabled = true;
                btn_prtdirct.Enabled = true;
                chk_shaml_tax.Enabled = false;
                btn_zatkasnd.Enabled = true;
                //...
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

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            if (sl_alowexit)
            {
                BL.CLS_Session.is_sal_login = false;
                //  BL.CLS_Session.dtsalman.Rows.Clear();
                // sp.Close();
                BL.CLS_Session.scount = BL.CLS_Session.scount - 1;
                this.Close();

            }
        }

        private void btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                isfeched = false;
                var sr = new Search_All("6t", "Sal");
                // sr.checkBox1.Visible = true;
                sr.ShowDialog();

                Fill_Data(sr.dataGridView1.CurrentRow.Cells[0].Value.ToString(), sr.dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), sr.dataGridView1.CurrentRow.Cells[2].Value.ToString());
                total();
            }
            catch { }
        }

        private void Fill_Data(string slctr, string atype, string aref)
        {
            //throw new NotImplementedException();
            DataTable dtt = daml.SELECT_QUIRY_only_retrn_dt("select * from pos_tafseel where slno='" + slctr + "' and contr="+BL.CLS_Session.contr_id+" and ref="+aref+" ");
            if (dtt.Rows.Count > 0)
            {
                txt_ref.Text = dtt.Rows[0]["ref"].ToString();
                cmb_salctr.SelectedValue = dtt.Rows[0]["slno"].ToString();
                txt_mdate.Text = isnew || isupdate ? txt_mdate.Text : dtt.Rows[0]["mdate"].ToString().Substring(6, 2) + dtt.Rows[0]["mdate"].ToString().Substring(4, 2) + dtt.Rows[0]["mdate"].ToString().Substring(0, 4);
                txt_hdate.Text = isnew || isupdate ? txt_hdate.Text : dtt.Rows[0]["hdate"].ToString().Substring(6, 2) + dtt.Rows[0]["hdate"].ToString().Substring(4, 2) + dtt.Rows[0]["hdate"].ToString().Substring(0, 4);
                txt_custno.Text = dtt.Rows[0]["cust_no"].ToString();
                txt_custnam.Text = dtt.Rows[0]["cust_nam"].ToString();
                txt_desc.Text = dtt.Rows[0]["dscrp"].ToString();
                txt_note.Text = dtt.Rows[0]["remark"].ToString();
                txt_taxid.Text = dtt.Rows[0]["cust_taxid"].ToString();
                txt_mobile.Text = dtt.Rows[0]["cust_mobil"].ToString();
                chk_shaml_tax.Checked =Convert.ToBoolean( dtt.Rows[0]["is_shamel_tax"])? true: false;
                txt_itemno.Text = dtt.Rows[0]["item_no"].ToString();
                txt_itemname.Text = dtt.Rows[0]["item_name"].ToString();
                txt_tol.Text = dtt.Rows[0]["tol"].ToString();
                txt_ktf.Text = dtt.Rows[0]["ktf"].ToString();
                txt_yd.Text = dtt.Rows[0]["yd"].ToString();
                txt_rqba.Text = dtt.Rows[0]["rqbh"].ToString();
                txt_sdr.Text = dtt.Rows[0]["sdr"].ToString();
                txt_wese.Text = dtt.Rows[0]["wse"].ToString();
                txt_wseyd1.Text = dtt.Rows[0]["wseyd1"].ToString();
                txt_wseyd2.Text = dtt.Rows[0]["wseyd2"].ToString();
                txt_wseyd3.Text = dtt.Rows[0]["wseyd3"].ToString();
                txt_tolkbk1.Text = dtt.Rows[0]["tolkbk1"].ToString();
                txt_tolkbk2.Text = dtt.Rows[0]["tolkbk2"].ToString();
                txt_asflthob.Text = dtt.Rows[0]["asflthob"].ToString();
                txt_qomash.Text = dtt.Rows[0]["qmashtyp"].ToString();
                cmb_thoptyp.SelectedIndex = Convert.ToInt32( dtt.Rows[0]["thoptyp"]);
                chk_kmkabi.Checked = Convert.ToBoolean(dtt.Rows[0]["mkis"]) ? true : false;
                chk_mmkabi.Checked = Convert.ToBoolean(dtt.Rows[0]["mmkhfi"]) ? true : false;
                chk_imkabi.Checked = Convert.ToBoolean(dtt.Rows[0]["madi"]) ? true : false;
                cmb_jebtyp.SelectedIndex = Convert.ToInt32(dtt.Rows[0]["gyptyp"]);
                chk_qlm.Checked = Convert.ToBoolean(dtt.Rows[0]["qlm"]) ? true : false;
                chk_jwal.Checked = Convert.ToBoolean(dtt.Rows[0]["jwal"]) ? true : false;
                cmb_sdrtyp.SelectedIndex = Convert.ToInt32(dtt.Rows[0]["sdrtyp"]);
                cmb_rqbatyp.SelectedIndex = Convert.ToInt32(dtt.Rows[0]["rqbhtyp"]);
                txt_qty.Text = dtt.Rows[0]["qty"].ToString();
                txt_discont.Text = dtt.Rows[0]["discont"].ToString();
                txt_price.Text = dtt.Rows[0]["price"].ToString();
                txt_tax.Text = dtt.Rows[0]["tax"].ToString();
                txt_total.Text = dtt.Rows[0]["total"].ToString();
                txt_nqd.Text = dtt.Rows[0]["nqd"].ToString();
                txt_card.Text = dtt.Rows[0]["card"].ToString();
                txt_wasel.Text = dtt.Rows[0]["wasel"].ToString();
                txt_baqi.Text = dtt.Rows[0]["baqi"].ToString();
                dateTimePicker1.Text = isnew || isupdate ? txt_mdate.Text : dtt.Rows[0]["tslimdate"].ToString().Substring(6, 2) + "-" + dtt.Rows[0]["tslimdate"].ToString().Substring(4, 2) + "-"  + dtt.Rows[0]["tslimdate"].ToString().Substring(0, 4);// dtt.Rows[0]["tslimdate"].ToString();
                txt_note.Text = dtt.Rows[0]["note"].ToString();
                chk_tasleem.Checked = Convert.ToBoolean(dtt.Rows[0]["tmtslim"]) ? true : false;
                btn_Post.Enabled = Convert.ToBoolean(dtt.Rows[0]["qlm"]) ? false : true;






                //txt_custnam.Text = dtt.Rows[0]["cust_nam"].ToString();
                total();
                btn_Edit.Enabled = true;
            }
            else
            {
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            //try
            //{
            if (isnew)
            {
                DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref),0),isnull(max(req_no),0) from pos_hdr where [contr]=" + BL.CLS_Session.ctrno + " and [brno]= '" + BL.CLS_Session.brno + "' ");
                refmax = Convert.ToInt32(dt2.Rows[0][0]);// = Convert.ToString(dt2.Rows[0][0]);
                rqmax = Convert.ToInt32(dt2.Rows[0][1]);

                using (SqlCommand cmd1 = new SqlCommand("INSERT INTO pos_hdr(brno,slno,ref,contr,type,date,total,count,payed,total_cost,saleman,req_no,cust_no,discount,net_total,tax_amt,dscper,card_type,card_amt,note,taxfree_amt,mobile,rref,rcontr) VALUES(@br,@sl,@rf,@ctr,@a0,@a1,@a2,@a3,@a4,@a5,@a7,@a8,@a9,@a11,@a12,@a13,@a14,@a15,@a16,@a17,@a18,@a19,@a20,@a21)", con))
                {
                    cmd1.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                    cmd1.Parameters.AddWithValue("@sl", BL.CLS_Session.sl_no);
                    cmd1.Parameters.AddWithValue("@rf", refmax + 1);
                    cmd1.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);
                    cmd1.Parameters.AddWithValue("@a0", (cmb_type.SelectedIndex == 0 ? 1 : 0));

                    cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                    // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                    cmd1.Parameters.AddWithValue("@a2", Convert.ToDouble(txt_total.Text));
                    cmd1.Parameters.AddWithValue("@a3", Convert.ToDouble(1));
                    cmd1.Parameters.AddWithValue("@a4", (0 == 1 ? 0 : Convert.ToDouble(txt_total.Text)));
                    cmd1.Parameters.AddWithValue("@a5", Convert.ToDouble(0));
                    //  cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                    cmd1.Parameters.AddWithValue("@a7", txt_salid.Text);
                    cmd1.Parameters.AddWithValue("@a8", rqmax + 1);// sum);
                    // cmd1.Parameters.AddWithValue("@a9", Convert.ToInt32(label3.Text));
                    //// cmd1.Parameters.AddWithValue("@a10", comp_id);
                    // cmd1.Parameters.AddWithValue("@a11", 0);
                    // cmd1.Parameters.AddWithValue("@a12", string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : Convert.ToDouble(label1.Text));
                    cmd1.Parameters.AddWithValue("@a9", string.IsNullOrEmpty(txt_custno.Text) ? 0 : Convert.ToInt32(txt_custno.Text));
                    // cmd1.Parameters.AddWithValue("@a10", comp_id);
                    cmd1.Parameters.AddWithValue("@a11", Convert.ToDouble(txt_discont.Text));
                    cmd1.Parameters.AddWithValue("@a12", (Convert.ToDouble(txt_total.Text)));
                    cmd1.Parameters.AddWithValue("@a13", (Convert.ToDouble(txt_tax.Text)));
                    cmd1.Parameters.AddWithValue("@a14", (Convert.ToDouble(txt_dscper.Text)));
                    cmd1.Parameters.AddWithValue("@a15", (Convert.ToDouble(txt_card.Text) > 0 ? 1 : 0));
                    cmd1.Parameters.AddWithValue("@a16", (Convert.ToDouble(txt_card.Text)));
                    cmd1.Parameters.AddWithValue("@a17", txt_note.Text);
                    cmd1.Parameters.AddWithValue("@a18", 0);
                    cmd1.Parameters.AddWithValue("@a19", txt_mobile.Text);
                    cmd1.Parameters.AddWithValue("@a20", 0);
                    cmd1.Parameters.AddWithValue("@a21", 0);

                    if (con.State == ConnectionState.Closed) con.Open();
                    cmd1.ExecuteNonQuery();
                  //  con.Close();
                }
                using (SqlCommand cmd1 = new SqlCommand(@"INSERT INTO pos_tafseel(brno, slno, ref, contr, type, mdate, hdate, cust_no, cust_nam, dscrp, remark, cust_taxid, cust_mobil, is_shamel_tax, item_no, item_name, tol, ktf, yd, rqbh, sdr, wse, wseyd1, wseyd2, wseyd3, tolkbk1, tolkbk2, asflthob, 
                                                                              qmashtyp, thoptyp, mkis, mmkhfi, madi, gyptyp, qlm, jwal, sdrtyp, rqbhtyp, qty, price,discont, tax, total,nqd,card, wasel, baqi, tslimdate, note, tmtslim, crtdate,updtdate) "
                                                                        + "  VALUES('" + BL.CLS_Session.brno + "', '" + cmb_salctr.SelectedValue + "', " + (refmax + 1) + ", " + BL.CLS_Session.contr_id + ", " + (cmb_type.SelectedIndex == 0 ? 1 : 0) + ", '" + dtv.convert_to_yyyyDDdd(txt_mdate.Text) + "', '" + dtv.convert_to_yyyyDDdd(txt_hdate.Text) + "', '" + txt_custno.Text + "', '" + txt_custnam.Text + "', '" + txt_desc.Text + "', '" + txt_remark.Text + "', '" + txt_taxid.Text + "', '" + txt_mobile.Text + "', 1, '" + txt_itemno.Text + "','" + txt_itemname.Text + "', " + txt_tol.Text + ", " + txt_ktf.Text + ", " + txt_yd.Text + ", " + txt_rqba.Text + ", " + txt_sdr.Text + "," + txt_wese.Text + ", " + txt_wseyd1.Text + ", " + txt_wseyd2.Text + ", " + txt_wseyd3.Text + ", " + txt_tolkbk1.Text + ", " + txt_tolkbk2.Text + ", " + txt_asflthob.Text + ", "
                                                                        + "         '" + txt_qomash.Text + "', " + cmb_thoptyp.SelectedIndex + ", " + (chk_kmkabi.Checked ? 1 : 0) + ", " + (chk_mmkabi.Checked ? 1 : 0) + ", " + (chk_imkabi.Checked ? 1 : 0) + ", " + cmb_jebtyp.SelectedIndex + ", " + (chk_qlm.Checked ? 1 : 0) + ", " + (chk_jwal.Checked ? 1 : 0) + ", " + cmb_sdrtyp.SelectedIndex + ", " + cmb_rqbatyp.SelectedIndex + ", " + txt_qty.Text + ", " + txt_price.Text + "," + txt_discont.Text + ", " + txt_tax.Text + ", " + txt_total.Text + "," + txt_nqd.Text + "," + txt_card.Text + ", " + txt_wasel.Text + ", " + txt_baqi.Text + ", '" + dtv.convert_to_yyyyDDdd(dateTimePicker1.Text) + "', '" + txt_note.Text + "', " + (chk_tasleem.Checked ? 1 : 0) + ", getdate(), getdate())", con))
                {


                    if (con.State == ConnectionState.Closed) con.Open();
                    cmd1.ExecuteNonQuery();
                   // con.Close();
                }

                // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                using (SqlCommand cmd = new SqlCommand("INSERT INTO pos_dtl(brno,slno,ref,contr,type, barcode, name, unit, price, qty, cost,is_req,itemno,srno,tax_amt) VALUES(@br,@sl,@rf,@ctr,@r0,@c1,@c2,@c3,@c4,@c5,@c6,@c8,@c10,@sn,@ta)", con))
                {
                    cmd.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                    cmd.Parameters.AddWithValue("@sl", BL.CLS_Session.slno);
                    cmd.Parameters.AddWithValue("@rf", refmax + 1);
                    cmd.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);

                    // cmd.Parameters.AddWithValue("@r1", reff);
                    cmd.Parameters.AddWithValue("@r0", (cmb_type.SelectedIndex == 0 ? 1 : 0));

                    cmd.Parameters.AddWithValue("@c1", txt_itemno.Text);
                    cmd.Parameters.AddWithValue("@c2", txt_itemname.Text);
                    cmd.Parameters.AddWithValue("@c3", 1);
                    cmd.Parameters.AddWithValue("@c4", txt_price.Text);
                    cmd.Parameters.AddWithValue("@c5", txt_qty.Text);
                    cmd.Parameters.AddWithValue("@c6", 0);
                    //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                    //if (row.Cells[7].Value != null)
                    cmd.Parameters.AddWithValue("@c8", 0);
                    // cmd.Parameters.AddWithValue("@c9", comp_id);
                    //   cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                    cmd.Parameters.AddWithValue("@c10", txt_itemno.Text);
                    cmd.Parameters.AddWithValue("@sn", 1);
                    cmd.Parameters.AddWithValue("@ta", txt_tax.Text);

                    if (con.State == ConnectionState.Closed) con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    txt_ref.Text = (refmax + 1).ToString();
                    

                    //lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);

                }
            }
            else
            {
                using (SqlCommand cmd1 = new SqlCommand("update pos_hdr set total=@a2,payed=@a4,saleman=@a7,cust_no=@a9,discount=@a11,net_total=@a12,tax_amt=@a13,dscper=@a14,card_type=@a15,card_amt=@a16,note=@a17,mobile=@a19 where slno='" + cmb_salctr.SelectedValue + "' and contr=" + BL.CLS_Session.contr_id + " and ref=" + txt_ref.Text + " ", con))
                {
                   //// cmd1.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                   ///// cmd1.Parameters.AddWithValue("@sl", BL.CLS_Session.sl_no);
                   //// cmd1.Parameters.AddWithValue("@rf", refmax + 1);
                  ////  cmd1.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);
                   //// cmd1.Parameters.AddWithValue("@a0", (cmb_type.SelectedIndex == 0 ? 1 : 0));

                   //// cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                    // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                    cmd1.Parameters.AddWithValue("@a2", Convert.ToDouble(txt_total.Text));
                   // cmd1.Parameters.AddWithValue("@a3", Convert.ToDouble(1));
                    cmd1.Parameters.AddWithValue("@a4", (0 == 1 ? 0 : Convert.ToDouble(txt_total.Text)));
                   //// cmd1.Parameters.AddWithValue("@a5", Convert.ToDouble(0));
                    //  cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                    cmd1.Parameters.AddWithValue("@a7", txt_salid.Text);
                   //// cmd1.Parameters.AddWithValue("@a8", rqmax + 1);// sum);
                    // cmd1.Parameters.AddWithValue("@a9", Convert.ToInt32(label3.Text));
                    //// cmd1.Parameters.AddWithValue("@a10", comp_id);
                    // cmd1.Parameters.AddWithValue("@a11", 0);
                    // cmd1.Parameters.AddWithValue("@a12", string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : Convert.ToDouble(label1.Text));
                    cmd1.Parameters.AddWithValue("@a9", string.IsNullOrEmpty(txt_custno.Text) ? 0 : Convert.ToInt32(txt_custno.Text));
                    // cmd1.Parameters.AddWithValue("@a10", comp_id);
                    cmd1.Parameters.AddWithValue("@a11", Convert.ToDouble(txt_discont.Text));
                    cmd1.Parameters.AddWithValue("@a12", (Convert.ToDouble(txt_total.Text)));
                    cmd1.Parameters.AddWithValue("@a13", (Convert.ToDouble(txt_tax.Text)));
                    cmd1.Parameters.AddWithValue("@a14", (Convert.ToDouble(txt_dscper.Text)));
                    cmd1.Parameters.AddWithValue("@a15", (Convert.ToDouble(txt_card.Text) > 0 ? 1 : 0));
                    cmd1.Parameters.AddWithValue("@a16", (Convert.ToDouble(txt_card.Text)));
                    cmd1.Parameters.AddWithValue("@a17", txt_note.Text);
                   //// cmd1.Parameters.AddWithValue("@a18", 0);
                    cmd1.Parameters.AddWithValue("@a19", txt_mobile.Text);
                   //// cmd1.Parameters.AddWithValue("@a20", 0);
                   //// cmd1.Parameters.AddWithValue("@a21", 0);

                    if (con.State == ConnectionState.Closed) con.Open();
                    cmd1.ExecuteNonQuery();
                  //  con.Close();
                }
                using (SqlCommand cmd1 = new SqlCommand(@"update pos_tafseel set mdate='" + dtv.convert_to_yyyyDDdd(txt_mdate.Text) + "', hdate='" + dtv.convert_to_yyyyDDdd(txt_hdate.Text) + "', cust_no='"+txt_custno.Text+"', cust_nam='"+txt_custnam.Text+"', dscrp='"+txt_desc.Text+"', remark='"+txt_remark.Text+"', cust_taxid='"+txt_taxid.Text+"', cust_mobil='"+txt_mobile.Text+"', item_no='"+txt_itemno.Text+"', item_name='"+txt_itemname.Text+"', tol="+txt_tol.Text+", ktf="+txt_ktf.Text+", yd="+txt_yd.Text+", rqbh="+txt_rqba.Text+", sdr="+txt_sdr.Text+", wse="+txt_wese.Text+", wseyd1="+txt_wseyd1.Text+", wseyd2="+txt_wseyd2.Text+", wseyd3="+txt_wseyd3.Text+", tolkbk1="+txt_tolkbk1.Text+", tolkbk2="+txt_tolkbk2.Text+", asflthob="+txt_asflthob.Text+","
                                                                             + " qmashtyp='" + txt_qomash.Text + "', thoptyp=" + cmb_thoptyp.SelectedIndex + ", mkis=" + (chk_kmkabi.Checked ? 1 : 0) + ", mmkhfi=" + (chk_mmkabi.Checked ? 1 : 0) + ", madi=" + (chk_imkabi.Checked ? 1 : 0) + ", gyptyp=" + cmb_jebtyp.SelectedIndex + ", qlm=" + (chk_qlm.Checked ? 1 : 0) + ", jwal=" + (chk_jwal.Checked ? 1 : 0) + ", sdrtyp=" + cmb_sdrtyp.SelectedIndex + ", rqbhtyp=" + cmb_rqbatyp.SelectedIndex + ", qty=" + txt_qty.Text + ", price=" + txt_price.Text + ",discont=" + txt_discont.Text + ", tax=" + txt_tax.Text + ", total=" + txt_total.Text + ",nqd=" + txt_nqd.Text + ",card=" + txt_card.Text + ", wasel=" + txt_wasel.Text + ", baqi=" + txt_baqi.Text + ", tslimdate='" + dtv.convert_to_yyyyDDdd(dateTimePicker1.Text) + "', note='" + txt_note.Text + "', tmtslim=" + (chk_tasleem.Checked ? 1 : 0) + ",updtdate=getdate() where slno='" + cmb_salctr.SelectedValue + "' and contr=" + BL.CLS_Session.contr_id + " and ref=" + txt_ref.Text + " ", con))
                                                                        //+ "  VALUES('" + BL.CLS_Session.brno + "', '" + cmb_salctr.SelectedValue + "', " + (refmax + 1) + ", " + BL.CLS_Session.contr_id + ", " + (cmb_type.SelectedIndex == 0 ? 1 : 0) + ", '" + dtv.convert_to_yyyyDDdd(txt_mdate.Text) + "', '" + dtv.convert_to_yyyyDDdd(txt_hdate.Text) + "', '" + txt_custno.Text + "', '" + txt_custnam.Text + "', '" + txt_desc.Text + "', '" + txt_remark.Text + "', '" + txt_taxid.Text + "', '" + txt_mobile.Text + "', 1, '" + txt_itemno.Text + "','" + txt_itemname.Text + "', " + txt_tol.Text + ", " + txt_ktf.Text + ", " + txt_yd.Text + ", " + txt_rqba.Text + ", " + txt_sdr.Text + "," + txt_wese.Text + ", " + txt_wseyd1.Text + ", " + txt_wseyd2.Text + ", " + txt_wseyd3.Text + ", " + txt_tolkbk1.Text + ", " + txt_tolkbk2.Text + ", " + txt_asflthob.Text + ", "
                                                                        //+ "         '" + txt_qomash.Text + "', " + cmb_thoptyp.SelectedIndex + ", " + (chk_kmkabi.Checked ? 1 : 0) + ", " + (chk_mmkabi.Checked ? 1 : 0) + ", " + (chk_imkabi.Checked ? 1 : 0) + ", " + cmb_jebtyp.SelectedIndex + ", " + (chk_qlm.Checked ? 1 : 0) + ", " + (chk_jwal.Checked ? 1 : 0) + ", " + cmb_sdrtyp.SelectedIndex + ", " + cmb_rqbatyp.SelectedIndex + ", " + txt_qty.Text + ", " + txt_price.Text + "," + txt_discont.Text + ", " + txt_tax.Text + ", " + txt_total.Text + "," + txt_nqd.Text + "," + txt_card.Text + ", " + txt_wasel.Text + ", " + txt_baqi.Text + ", '" + dtv.convert_to_yyyyDDdd(dateTimePicker1.Text) + "', '" + txt_note.Text + "', " + (chk_tasleem.Checked ? 1 : 0) + ", getdate(), getdate())", con))
                {


                    if (con.State == ConnectionState.Closed) con.Open();
                    cmd1.ExecuteNonQuery();
                   // con.Close();
                }

                // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                using (SqlCommand cmd = new SqlCommand("update pos_dtl set  barcode=@c1, name=@c2, price=@c4, qty=@c5,itemno=@c10,tax_amt=@ta  where slno='" + cmb_salctr.SelectedValue + "' and contr=" + BL.CLS_Session.contr_id + " and ref=" + txt_ref.Text + " ", con))
                {
                    ////cmd.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                    ////cmd.Parameters.AddWithValue("@sl", BL.CLS_Session.slno);
                    ////cmd.Parameters.AddWithValue("@rf", refmax + 1);
                    ////cmd.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);

                    // cmd.Parameters.AddWithValue("@r1", reff);
                   //// cmd.Parameters.AddWithValue("@r0", (cmb_type.SelectedIndex == 0 ? 1 : 0));

                    cmd.Parameters.AddWithValue("@c1", txt_itemno.Text);
                    cmd.Parameters.AddWithValue("@c2", txt_itemname.Text);
                   //// cmd.Parameters.AddWithValue("@c3", 1);
                    cmd.Parameters.AddWithValue("@c4", txt_price.Text);
                    cmd.Parameters.AddWithValue("@c5", txt_qty.Text);
                  ////  cmd.Parameters.AddWithValue("@c6", 0);
                    //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                    //if (row.Cells[7].Value != null)
                  ////  cmd.Parameters.AddWithValue("@c8", 0);
                    // cmd.Parameters.AddWithValue("@c9", comp_id);
                    //   cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                    cmd.Parameters.AddWithValue("@c10", txt_itemno.Text);
                   // cmd.Parameters.AddWithValue("@sn", 1);
                    cmd.Parameters.AddWithValue("@ta", txt_tax.Text);

                    if (con.State == ConnectionState.Closed) con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                   // txt_ref.Text = (refmax + 1).ToString();


                    //lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);

                }
            }

            isnew = false;
            isupdate = false;
            btn_Save.Enabled = false;
            btn_Add.Enabled = true;
            btn_Undo.Enabled = false;
            btn_Exit.Enabled = true;
            btn_Find.Enabled = true;
            btn_Edit.Enabled = true;
            btn_prtdirct.Enabled = true;
            btn_Print.Enabled = true;
            btn_Post.Enabled = true;
            //  txt_custno.Enabled = false;
            txt_taxid.Enabled = false;
            txt_itemno.Enabled = false;
            txt_desc.Enabled = false;
            txt_nqd.Enabled = false;
            txt_card.Enabled = false;
            cmb_salctr.Enabled = false;
            // dataGridView1.ReadOnly = true;
            //  dataGridView1.AllowUserToAddRows = false;
            cmb_type.Enabled = false;
            //  cmb_salman.Enabled = false;
            //txt_ref.Enabled = false;
            txt_remark.Enabled = false;
            //  txt_note2.Enabled = false;
            //  txt_note3.Enabled = false;
            //  txt_mpay.Enabled = false;
            txt_mobile.Enabled = false;
            //  txt_reref.Enabled = false;
            //  txt_key.Enabled = false;
            txt_custno.Enabled = false;
            //  chk_suspend.Enabled = false;
            //  button1.Enabled = true;
            panel2.Enabled = false; panel7.Enabled = false;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //}


        }

        private void txt_itemno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    // var selectedCell = dataGridView1.SelectedCells[0];
                    // do something with selectedCell...
                    Search_All f4 = new Search_All("2", "Sto");
                    f4.ShowDialog();



                    txt_itemno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_itemname.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                    /*
                   dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
                   dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
                   dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                    */



                }

                if (e.KeyCode == Keys.Enter)
                {

                    button1_Click(sender, e);


                }
                /*
                if (e.KeyCode == Keys.Enter)
                {
                    if (e.KeyCode == Keys.Enter)
                    {

                        button1_Click(sender, e);


                    }
                    // button1_Click( sender,  e);
                    DataTable dta = daml.SELECT_QUIRY_only_retrn_dt("select item_name,round(item_obalance,2) a_opnbal,item_no  from items where item_no='" + txt_code.Text + "'");
                    if (dta.Rows.Count > 0)
                    {
                        txt_name.Text = dta.Rows[0][0].ToString();
                        txt_code.Text = dta.Rows[0][2].ToString();
                        txt_opnbal.Text = dta.Rows[0][1].ToString();
                    }
                    else
                    {
                        //    MessageBox.Show("الحساب غير موجود");
                        txt_name.Text = "";
                        txt_code.Text = "";
                        //  txt_code.Text = dt.Rows[0][2].ToString();
                        txt_opnbal.Text = "0";
                    }

                }
                 * */
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_itemno.Text == "")
            {
                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Enter Item NO" : "يجب ادخال رقم الصنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_itemno.Focus();
                return;

            }
            // if(txt_code.Text !="")
            //Load_Statment(txt_code.Text);
        }

        private void txt_itemno_Leave(object sender, EventArgs e)
        {
            try
            {
                DataTable dta = daml.SELECT_QUIRY_only_retrn_dt("select item_name,round(item_obalance,2) a_opnbal,item_no  from items where item_cost=2 and item_no='" + txt_itemno.Text + "'");
                if (dta.Rows.Count > 0)
                {
                    txt_itemname.Text = dta.Rows[0][0].ToString();
                    txt_itemno.Text = dta.Rows[0][2].ToString();
                    //  txt_opnbal.Text = dta.Rows[0][1].ToString();
                }
                else
                {
                    //    MessageBox.Show("الحساب غير موجود");
                    txt_itemname.Text = "";
                    txt_itemno.Text = "";
                    //  txt_code.Text = dt.Rows[0][2].ToString();
                    //  txt_opnbal.Text = "0";
                }

                //if (isnew)
                //    fill_items();
            }
            catch { }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                isfeched = false;
                // dataGridView1.ReadOnly = false;
                var fsn = new Search_by_No("04");

                DataTable dttp = dttrtyps.Copy();

                DataRow dtt = dttp.NewRow();
                dtt[0] = "24"; dtt[1] = "نسخ من عرض سعر"; dtt[2] = ""; dtt[3] = "";
                //  dttp.Rows.Add(new { Text = "نسخ من عرض سعر", Value = "24" });
                dttp.Rows.Add(dtt);
                DataRow dtt2 = dttp.NewRow();
                dtt2[0] = "37"; dtt2[1] = "نسخ من طلب بيع"; dtt2[2] = ""; dtt2[3] = "";
                //  dttp.Rows.Add(new { Text = "نسخ من عرض سعر", Value = "24" });
                dttp.Rows.Add(dtt2);

                fsn.comboBox1.DataSource = dttp;
                fsn.comboBox1.DisplayMember = "tr_name";
                fsn.comboBox1.ValueMember = "tr_no";



                fsn.cmb_salctr.DataSource = dtslctr;
                fsn.cmb_salctr.DisplayMember = "sl_name";
                fsn.cmb_salctr.ValueMember = "sl_no";
                fsn.checkBox1.Visible = true;
                fsn.checkBox1.Text = "برقم العقد";

                fsn.ShowDialog();
                aqd_temp = fsn.checkBox1.Checked ? true : false;
                Fill_Data(fsn.cmb_salctr.SelectedValue.ToString(), fsn.comboBox1.SelectedValue.ToString(), fsn.textBox1.Text);
                //  dataGridView1_CellLeave(null, null);
                if (!dthdr.Rows[0]["glser"].ToString().Equals("Pos"))
                    total();

            }
            catch { }
            //  dataGridView1.Enabled = true;
        }

        private void Pos_Tafseel_Shown(object sender, EventArgs e)
        {
            if (Convert.ToInt32(dtv.convert_to_yyyyDDdd(DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false)))) > Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.end_dt)) || Convert.ToInt32(dtv.convert_to_yyyyDDdd(DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false)))) < Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.start_dt)))
            {
                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Date Out of Years" : "لا يمكن ادخال حركة خارج السنة المالية", "Error خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
            // try
            //   {
            //  MessageBox.Show(dts.Rows.Count.ToString());
            //  Pos.SALES_D smn = new SALES_D();
            int form2Count;
            if (BL.CLS_Session.istuch)
                form2Count = Application.OpenForms.OfType<Pos_Tafseel>().Count();
            else
                form2Count = Application.OpenForms.OfType<Pos_Tafseel>().Count();
            //  MessageBox.Show(form2Count.ToString());

            if (form2Count > Convert.ToInt32(BL.CLS_Session.dtsalman.Rows.Count == 0 ? 1 : BL.CLS_Session.dtsalman.Rows[0]["scr_open"]))
            {
                BL.CLS_Session.scount = BL.CLS_Session.scount - 1;
                this.Close();
            }
            else
            {

                if (BL.CLS_Session.is_sal_login == false || BL.CLS_Session.dtsalman.Rows.Count == 0)
                {
                    Pos.Salmen_LogIn salm = new Salmen_LogIn();
                    //salm.MdiParent = MdiParent;
                    salm.ShowDialog();
                    set_permition(salm.txt_salman.Text);
                    // dataGridView1.Select();
                }
                else
                {
                    set_permition(BL.CLS_Session.dtsalman.Rows[0]["sl_id"].ToString());
                    // dataGridView1.Select();
                }
            }
            // else
            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void set_permition(string salid)
        {
            try
            {

                if ((salid.Equals("0") || salid.Equals("")) && BL.CLS_Session.is_sal_login == false)
                {
                    //  BL.CLS_Session.is_sal_login = false;
                    this.Close();
                }
                else
                {
                    // BL.CLS_Session.is_sal_login = true;
                    //BL.CLS_Session.dtsalman = daml.SELECT_QUIRY_only_retrn_dt("select *  from pos_salmen where sl_id=" + salid + " and sl_brno='" + BL.CLS_Session.brno + "'");


                    if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_chgqty"]) == false)
                    {
                        // dataGridView1.Columns[4].ReadOnly = true;
                        //btn_mins.Enabled = false;
                        // btn_plus.Enabled = false;
                    }
                    else
                    {
                        //dataGridView1.Columns[4].ReadOnly = false;
                        //btn_mins.Enabled = true;
                        //btn_plus.Enabled = true;
                    }

                    //if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_chgprice"]) == false)
                    //    txt_price.Enabled = true;
                    //else
                    //    txt_price.Enabled = false;

                    if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_delline"]) == false)
                    {
                        //sl_delline = false;
                        // btn_delitem.Enabled = false;
                    }
                    else
                    {
                        // sl_delline = true;
                        // btn_delitem.Enabled = true;
                    }

                    if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_delinv"]) == false)
                    {
                        // sl_delinv = false;
                        // btn_del.Enabled = false;
                    }
                    else
                    {
                        //  sl_delinv = true;
                        // btn_del.Enabled = true;
                    }
                    // sl_delinv = false;
                    //   else
                    // sl_delinv = true;

                    ////if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_alowdisc"]) == false)
                    ////    sl_alowdisc = false;
                    ////else
                    ////    sl_alowdisc = true;


                      slpmaxdisc = BL.CLS_Session.dtsalman.Rows[0]["slpmaxdisc"].ToString();
                    ////  slalwdesc = Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_alowdisc"]);

                    if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_alowexit"]) == false)
                    {
                        sl_alowexit = false;
                        btn_Exit.Enabled = false;
                    }
                    else
                    {
                        sl_alowexit = true;
                        btn_Exit.Enabled = true;
                    }

                    //if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["alwreprint"]) == false)
                    //    btn_prt.Enabled = false;
                    //else
                    //    btn_prt.Enabled = true;

                    //if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["alwitmdsc"]) == false)
                    //    alwitmdsc = false;
                    //else
                    //    alwitmdsc = true;

                    ////if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_alwcrdit"]) == false)
                    ////{
                    ////    txt_custno.Enabled = false;
                    ////   // chk_agel.Enabled = false;
                    ////}
                    ////else
                    ////{
                    ////    txt_custno.Enabled = true;
                    ////   // chk_agel.Enabled = true;
                    ////}

                    //if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["slpalowopdr"]) == false)
                    //    btn_opndr.Enabled = false;
                    //else
                    //    btn_opndr.Enabled = true;

                    if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["frcrplinv"]) == false)
                        btn_Find.Enabled = false;
                    else
                        btn_Find.Enabled = true;

                    ////if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["catch_thief"]) == false)
                    ////{
                    ////    button5.Enabled = false;
                    ////    btn_resrv.Enabled = false;
                    ////}
                    ////else
                    ////{
                    ////    button5.Enabled = true;
                    ////    btn_resrv.Enabled = true;
                    ////}

                    ////if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sold_once"]) == false)
                    ////    checkBox1.Enabled = false;
                    ////else
                    ////    checkBox1.Enabled = true;


                    ////maxitmdsc = BL.CLS_Session.dtsalman.Rows[0]["maxitmdsc"].ToString();
                    txt_salid.Text = BL.CLS_Session.dtsalman.Rows[0]["sl_id"].ToString();
                    txt_salnam.Text = BL.CLS_Session.dtsalman.Rows[0]["sl_name"].ToString();


                    int form2Count = Application.OpenForms.OfType<Pos_Tafseel>().Count();

                    if (form2Count > Convert.ToInt32(BL.CLS_Session.dtsalman.Rows[0]["scr_open"]))
                        this.Close();
                    //  this.Refresh();
                    //  dataGridView1_RowEnter( null,  null);


                }
            }
            catch { this.Close(); }
            // this.WindowState = FormWindowState.Maximized; 
        }

        private void txt_price_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_discont.Text))
                txt_discont.Text = "0";

            double td = Math.Round(Convert.ToDouble(txt_discont.Text), 2);
            txt_discont.Text = td.ToString();

            string per = (100 * (Convert.ToDouble(txt_discont.Text)) / Convert.ToDouble(txt_total.Text)).ToString();

            //  if ((Convert.ToDouble(per) > Convert.ToDouble(BL.CLS_Session.inv_dsc)))
            if ((Convert.ToDouble(per) > Convert.ToDouble(slpmaxdisc)))
            {
                // MessageBox.Show("تجاوزت الخصم المسموح لك");
                MessageBox.Show("تجاوزت الخصم المسموح لك", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_discont.Text = "0";
                txt_dscper.Text = "0";
                txt_discont.SelectAll();
                // txt_desper.Text = "0";
            }

            txt_dscper.Text = (Math.Round((100 * (Convert.ToDouble(txt_discont.Text)) / Convert.ToDouble(txt_total.Text)), 2)).ToString();

            // total();

           // total();

            total();
        }

        private void txt_total_Leave(object sender, EventArgs e)
        {
            txt_price.Text = (Math.Round((Convert.ToDouble(txt_total.Text) / Convert.ToDouble(txt_qty.Text)), 4)).ToString();
            total();
        }

        private void txt_total_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_total_Leave(sender, e);
                SendKeys.Send("{TAB}");
            }
        }

        private void txt_tol_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txt_wasel_TextChanged(object sender, EventArgs e)
        {
           // txt_nqd.Text = txt_wasel.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cus.Customers cust = new Cus.Customers("");
            cust.MdiParent = this.MdiParent;
            cust.Show();
        }

        private void txt_ref_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //SendKeys.Send("{TAB}");
                Fill_Data(cmb_salctr.SelectedValue.ToString(), "", txt_ref.Text);
            }
        }

        private void btn_prtdirct_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.prnt_type.Equals("0"))
                print_ref();
            else
                print_ref_fr(false);
        }

        private void print_ref()
        {
            if (string.IsNullOrEmpty(txt_ref.Text))
            {
                MessageBox.Show("لا يوجد فاتورة للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty] from pos_dtl where ref=(select max(ref) from pos_dtl)", con2);

            // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from pos_hdr where ref=(select max(ref) from pos_hdr)", con2);//" + lblref.Text + ""
            SqlDataAdapter dacr1 = new SqlDataAdapter("select a.[brno],a.[slno],a.[ref],a.[contr],a.[type],a.[date],a.[total],a.[count],a.[payed],a.[total_cost],b.sl_name [saleman],a.[req_no],a.[cust_no],a.[discount],a.[net_total],a.[sysdate],a.[gen_ref],a.[tax_amt],a.[card_amt],a.[note],a.[mobile] from pos_hdr a join pos_salmen b on a.saleman=b.sl_id where a.ref=" + txt_ref.Text + " and a.[contr]=" + BL.CLS_Session.ctrno + " and  a.[brno]= '" + BL.CLS_Session.brno + "' ", con);
            SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty],[offr_amt] from pos_dtl where ref=" + txt_ref.Text + " and [contr]=" + BL.CLS_Session.ctrno + " and  [brno]= '" + BL.CLS_Session.brno + "' ", con);

            DataSet1 ds = new DataSet1();

            dacr1.Fill(ds, "hdr");
            dacr.Fill(ds, "dtl");

            if (ds.Tables["hdr"].Rows.Count == 0)
            {
                MessageBox.Show("لا يوجد فاتورة للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CrystalDecisions.CrystalReports.Engine.ReportDocument report1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            if (File.Exists(Directory.GetCurrentDirectory() + (string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? @"\reports\SalesReport1nt.rpt" : BL.CLS_Session.isshamltax.Equals("1") ? @"\reports\SalesReport112.rpt" : @"\reports\SalesReport111.rpt")))
            {
                string filePath = Directory.GetCurrentDirectory() + (string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? @"\reports\SalesReport1nt.rpt" : BL.CLS_Session.isshamltax.Equals("1") ? @"\reports\SalesReport112.rpt" : @"\reports\SalesReport111.rpt");
                report1.Load(filePath);

                // rpt.SalesReport111 report = new rpt.SalesReport111();

                //  CrystalReport1 report = new CrystalReport1();
                report1.SetDataSource(ds);

                //    crystalReportViewer1.ReportSource = report;

                //  crystalReportViewer1.Refresh();
                report1.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                report1.SetParameterValue("br_name", BL.CLS_Session.brname);
                report1.SetParameterValue("inv_bar", "*" + ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString() + "*");
                report1.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                report1.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
                report1.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
                // report.SetParameterValue("inv_bar", );
                //  report.SetParameterValue("picpath", Directory.GetCurrentDirectory() + "\\" + BL.CLS_Session.comp_logo);


               // foreach (string line in lines_prt)
                if(1==1)
                {
                    //report1.PrintOptions.PrinterName = line;

                    //report1.PrintToPrinter(1, false, 0, 0);
                    // report.PrintToPrinter(0,true, 1, 1);
                    report1.PrintOptions.PrinterName = printer_nam;// "pos";

                    report1.PrintToPrinter(1, false, 0, 0);

                }


                //report1.PrintOptions.PrinterName = printer_nam;// "pos";

                //report1.PrintToPrinter(1, false, 0, 0);
                report1.Close();
            }
            else
            {
                rpt.SalesReport111 report = new rpt.SalesReport111();

                //  CrystalReport1 report = new CrystalReport1();
                report.SetDataSource(ds);

                //    crystalReportViewer1.ReportSource = report;

                //  crystalReportViewer1.Refresh();
                report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                report.SetParameterValue("br_name", BL.CLS_Session.brname);
                report.SetParameterValue("inv_bar", "*" + ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString() + "*");
                report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
                report.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
                // report.SetParameterValue("inv_bar", );
                //  report.SetParameterValue("picpath", Directory.GetCurrentDirectory() + "\\" + BL.CLS_Session.comp_logo);

               // foreach (string line in lines_prt)
                if(1==1)
                {
                    //report1.PrintOptions.PrinterName = line;

                    //report1.PrintToPrinter(1, false, 0, 0);
                    // report.PrintToPrinter(0,true, 1, 1);
                    report.PrintOptions.PrinterName = printer_nam;// "pos";

                    report.PrintToPrinter(1, false, 0, 0);

                }

                report.PrintOptions.PrinterName = printer_nam;// "pos";

                report.PrintToPrinter(1, false, 0, 0);
                report.Close();
            }

        }
        private void print_ref_fr(bool vio)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_ref.Text))
                {
                    MessageBox.Show("لا يوجد فاتورة للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               // if (Convert.ToDouble(dtv.convert_to_yyyyDDdd(Convert.ToDateTime((BL.CLS_Session.minstart.Substring(4, 2) + "/" + BL.CLS_Session.minstart.Substring(6, 2) + "/" + BL.CLS_Session.minstart.Substring(0, 4)), new CultureInfo("en-US", false)).AddDays(365).ToString())) < Convert.ToDouble(DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false))))
                TimeSpan span = DateTime.Now.Subtract(Convert.ToDateTime((BL.CLS_Session.minstart.Substring(4, 2) + "/" + BL.CLS_Session.minstart.Substring(6, 2) + "/" + BL.CLS_Session.minstart.Substring(0, 4)), new CultureInfo("en-US", false)));
                if (Convert.ToDouble(span.Days) > 365)
                {
                    MessageBox.Show(" بيانات الصيانه مفقودة يرجى التواصل مع الدعم الفني لتحديثها ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty] from pos_dtl where ref=(select max(ref) from pos_dtl)", con2);
                string chktyp1 = 0==1 ? " r_pos_hdr " : " pos_hdr ";
                string chktyp2 = 0==1 ? " r_pos_dtl " : " pos_dtl ";
                // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from pos_hdr where ref=(select max(ref) from pos_hdr)", con2);//" + lblref.Text + ""
                SqlDataAdapter dacr1 = new SqlDataAdapter("select a.[brno],a.[slno],a.[ref],a.[contr],a.[type],FORMAT (a.[date], 'dd-MM-yyyy hh:mm:ss tt', 'en-us') date,a.[total],a.[count],a.[payed],a.[total_cost],b.sl_name [saleman],a.[req_no],a.[cust_no],a.[discount],a.[net_total],a.[sysdate],a.[gen_ref],a.[tax_amt],a.[card_amt],a.[note],a.[mobile] from " + chktyp1 + " a join pos_salmen b on a.saleman=b.sl_id where a.ref=" + txt_ref.Text + " and a.[contr]=" + BL.CLS_Session.ctrno + " and  a.[brno]= '" + BL.CLS_Session.brno + "' ", con);
                SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit_name] [unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty],[offr_amt] from " + chktyp2 + " join units on " + chktyp2 + ".[unit]=units.unit_id  where ref=" + txt_ref.Text + " and [contr]=" + BL.CLS_Session.ctrno + " and  [brno]= '" + BL.CLS_Session.brno + "' order by srno ", con);

                DataSet1 ds = new DataSet1();
                // DataSet2 ds = new DataSet2();

                // hdTable.TableName = "hdr";
                // ds.Tables.Add(hdTable);
                // dtTable.TableName = "dtl";
                // ds.Tables.Add(dtTable);
                // MessageBox.Show(hdTable.Rows.Count.ToString());
                // ds.Tables["hdr"].Equals(hdTable.Copy());
                // ds.Tables.Add(hdTable);
                // ds.Tables["dtl"].Equals(dtTable.Copy());
                //ds.Tables.Add(dtTable);
                dacr1.Fill(ds, "hdr");
                dacr.Fill(ds, "dtl");
                DataTable dtcust = daml.SELECT_QUIRY_only_retrn_dt("select cu_no ,cu_name ,cl_acc,cu_crlmt,vndr_taxcode,cu_alwcr,c_bulding_no,  c_street, c_site_name, c_city, c_cuntry, c_postal_code, c_ex_postalcode, c_other_id,cu_kind,cu_addrs from customers a join cuclass b on a.cu_class=b.cl_no  where a.inactive=0 and a.cu_brno='" + BL.CLS_Session.brno + "' and cu_no='" + ds.Tables["hdr"].Rows[0]["cust_no"].ToString() + "'");

                // MessageBox.Show(ds.Tables["hdr"].Rows.Count.ToString());
                // if (ds.Tables["hdr"].Rows.Count == 0)
                if (ds.Tables["hdr"].Rows.Count == 0)
                {
                    MessageBox.Show("لا يوجد فاتورة للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FastReport.Report rpt = new FastReport.Report();

                if (1==1)
                {
                    if (numericUpDown1.Value == 1)
                        rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt.frx");
                    else
                        rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt" + numericUpDown1.Value + ".frx");
                }
                else
                    rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_Ocu_rpt.frx");
                //  rpt.SetParameterValue("br_name", BL.CLS_Session.brname);
                rpt.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                rpt.SetParameterValue("Br_Name", BL.CLS_Session.brname);
                //rpt.SetParameterValue("inv_bar", ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString());
                rpt.SetParameterValue("inv_bar", ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString());
                rpt.SetParameterValue("Msg1", BL.CLS_Session.msg1.ToString());
                rpt.SetParameterValue("Msg2", BL.CLS_Session.msg2.ToString());
                rpt.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
                rpt.SetParameterValue("tax_type", BL.CLS_Session.autotax ? "3" : BL.CLS_Session.isshamltax.Equals("2") ? "2" : "0");
                rpt.SetParameterValue("tax_id", BL.CLS_Session.tax_no);
                rpt.SetParameterValue("type", ds.Tables["hdr"].Rows[0][4].ToString());

                BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(txt_total.Text), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);

                //  MessageBox.Show(toWord.ConvertToArabic());
                rpt.SetParameterValue("a_toword", BL.CLS_Session.lang.Equals("E") ? toWord.ConvertToEnglish() : toWord.ConvertToArabic());

                if (dtcust.Rows.Count > 0)
                {
                    rpt.SetParameterValue("cust_no", dtcust.Rows[0]["cu_no"].ToString());
                    rpt.SetParameterValue("cust_taxid", dtcust.Rows[0]["vndr_taxcode"].ToString());
                }

                string dtt = Convert.ToDateTime(ds.Tables["hdr"].Rows[0]["date"].ToString()).ToString("yyyy-MM-ddTHH:mm:ssZ", new CultureInfo("en-US", false));
                var tlvs = new GenerateQrCode.GenerateQrCodeTLV(
                              BL.CLS_Session.cmp_ename,
                               BL.CLS_Session.tax_no,
                               dtt,
                               ds.Tables["hdr"].Rows[0]["net_total"].ToString(),
                              Math.Round(Convert.ToDouble(ds.Tables["hdr"].Rows[0]["tax_amt"].ToString()), 2).ToString());

                if (BL.CLS_Session.is_einv_p2 && 1==1)
                {
                    DataTable dtqrc = daml.SELECT_QUIRY_only_retrn_dt("select qr_code from pos_esend where ref=" + txt_ref.Text + "");
                    rpt.SetParameterValue("qr", dtqrc.Rows[0][0].ToString());
                }
                else
                    rpt.SetParameterValue("qr", Convert.ToBase64String(tlvs.GetQRCode()));

                ////////string tlvs = new BL.qr_ar().GenerateQrCodeTLV(BL.CLS_Session.cmp_name, BL.CLS_Session.tax_no, dtt, Convert.ToDouble(ds.Tables["hdr"].Rows[0]["net_total"].ToString()), Convert.ToDouble(Math.Round(Convert.ToDouble(ds.Tables["hdr"].Rows[0]["tax_amt"].ToString()), 2).ToString()));
                ////////rpt.SetParameterValue("qr", tlvs);

                rpt.RegisterData(ds.Tables["hdr"], "pos_hdr");
                rpt.RegisterData(ds.Tables["dtl"], "pos_dtl");
                // rpt.PrintSettings.ShowDialog = false;
                // rpt.PrintSettings.Copies =Convert.ToInt32(txt_count.Text);

                // rpt.Print();


                //foreach (string line in lines_prt)
                if (vio==false)
                {
                    if (numericUpDown1.Value == 1)
                        rpt.PrintSettings.Printer = printer_nam;// "pos";
                    rpt.PrintSettings.ShowDialog = numericUpDown1.Value == 1 ? false : true;
                    // FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                    // rpt.PrintSettings.Copies =Convert.ToInt32(txt_count.Text);
                    rpt.Print();
                }
                else
                {
                    FR_Viewer rptd = new FR_Viewer(rpt);

                    rptd.MdiParent = MdiParent;
                    rptd.Show();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            print_ref_fr(true);
        }

        private void btn_Post_Click(object sender, EventArgs e)
        {

            using (SqlCommand cmd = new SqlCommand("update pos_tafseel set posted=1 where slno='" + cmb_salctr.SelectedValue + "' and contr=" + BL.CLS_Session.contr_id + " and ref=" + txt_ref.Text + " ", con))
            {
               // cmd.Parameters.AddWithValue("@br", BL.CLS_Session.brno);

                if (con.State == ConnectionState.Closed) con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                btn_Post.Enabled = false;
                //lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);
            }
        }
    }
}

        
    

