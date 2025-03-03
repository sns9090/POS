﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;
using System.Globalization;
using GridPrintPreviewLib;
using Microsoft.Reporting.WinForms;

namespace POS.Cus
{
    public partial class Acc_Tran_QAK : BaseForm
    {
       // DataTable dtg;
        DataTable dthdr, dtdtl, dttrtyps, dtcolman, dtslctr;
        string a_ref, a_type, strdcash, strsndoq;
        bool isnew,isupdate,notposted;
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate dtv = new BL.Date_Validate();
        SqlConnection con =BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public Acc_Tran_QAK(string atype, string aref)
        {
            InitializeComponent();
          //  this.KeyPreview = true;
            a_ref = aref;
            a_type = atype;
            //this.KeyPreview = true;
            //this.KeyPress += new KeyPressEventHandler(Control_KeyPress);

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectNextControl(ActiveControl, true, true, true, true);
                e.Handled = true;
            }
        }

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

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            // if (e.Key == Key.Enter)


            // var selectedCell = dataGridView1.SelectedCells[0];
            // do something with selectedCell...


        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (dataGridView1.CurrentCell == dataGridView1[4, dataGridView1.CurrentRow.Index])
            {

                // dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];
                SendKeys.Send("{Down}");
                SendKeys.Send("{right 3}");
                //  dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index];
            }
             */
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Acc_Tran_Load(object sender, EventArgs e)
        {
            
           // dtg = dataGridView1.DataSource;
            
            // dataGridView1.Columns["SN"].ReadOnly = true;
            try
            {
                string tr1 = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
                //   dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('04','05','14','15')");
                cmb_type.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr1 + ") and tr_no in('05')");
                cmb_type.DisplayMember = "tr_name";
                cmb_type.ValueMember = "tr_no";
                cmb_type.SelectedIndex = 0;

                string sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";
                dtslctr= daml.SELECT_QUIRY_only_retrn_dt("select * from slcenters where sl_brno + sl_no in(" + sl + ") and sl_brno='" + BL.CLS_Session.brno + "'");
                cmb_salctr.DataSource = dtslctr;
                cmb_salctr.DisplayMember = "sl_name";
                cmb_salctr.ValueMember = "sl_no";
                cmb_salctr.SelectedIndex = 0;

                txt_mdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
                txt_hdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));

               

               // MessageBox.Show(dtv.convert_to_yyyyDDdd(txt_mdate.Text));
               // MessageBox.Show(dtv.convert_to_yyyyDDdd(BL.CLS_Session.start_dt));
               // MessageBox.Show(BL.CLS_Session.end_dt.Substring(6, 4) + BL.CLS_Session.end_dt.Substring(3, 2) + BL.CLS_Session.end_dt.Substring(0, 2));
                /*
                comboBox1.DisplayMember = "Text";
                comboBox1.ValueMember = "Value";

                comboBox1.Items.Add(new { Text = "قيد عام", Value = "01" });
                comboBox1.Items.Add(new { Text = "سند قبض", Value = "02" });
                comboBox1.Items.Add(new { Text = "سند صرف", Value = "03" });
                 */
/*
                comboBox1.DisplayMember = "Text";
                comboBox1.ValueMember = "Value";

                var items = new[] { new { Text = "قيد عام", Value = "01" }, new { Text = "سند قبض", Value = "02" }, new { Text = "سند صرف", Value = "03" } };
                comboBox1.DataSource = items;
                comboBox1.SelectedIndex = -1;
                */

                string tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
                // MessageBox.Show(tr);
                dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('02')");

                comboBox1.DataSource = dttrtyps;
                comboBox1.DisplayMember = "tr_name";
                comboBox1.ValueMember = "tr_no";
                comboBox1.SelectedIndex = -1;

                dtcolman = daml.SELECT_QUIRY_only_retrn_dt("select * from col_men where sp_brno='"+BL.CLS_Session.brno+"'");

                cmb_colman.DataSource = dtcolman;
                cmb_colman.DisplayMember = "sp_name";
                cmb_colman.ValueMember = "sp_id";
                cmb_colman.SelectedIndex = -1;

                comboBox1.Enabled = false;
                cmb_colman.Enabled = false;
                txt_ref.Enabled = false;
                txt_hdate.Enabled = false;
                txt_mdate.Enabled = false;
                txt_desc.Enabled = false;
                cmb_salctr.Enabled = false;
                //cmb_type.Enabled = false;
                txt_fno.Enabled = false;
                txt_custno.Enabled = false;
                txt_bookno.Enabled = false;
                txt_key.Enabled = false;
                txt_amt.Enabled = false;
                txt_discnt.Enabled = false;
               

                Fill_Data(a_type, a_ref);
                //  dataGridView1.AllowUserToAddRows = false;
                //dataGridView1.Select();
                // dataGridView1.BeginEdit(true);
                // dataGridView1.Rows.Add(10);
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();

                foreach (DataRow dtr in BL.CLS_Session.dtdescs.Rows)
                {
                    MyCollection.Add(BL.CLS_Session.lang.Equals("E") ? dtr[2].ToString() : dtr[1].ToString());
                }
                txt_desc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txt_desc.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txt_desc.AutoCompleteCustomSource = MyCollection;
                // this.Owner.Enabled = false;

                //  Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                string str2 = BL.CLS_Session.formkey;
                string whatever = str2.Substring(str2.IndexOf("C124") + 5, 3);
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

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
           


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show((dataGridView1.Rows.Count-1).ToString());

            // dataGridView1.CurrentRow.Cells[2].Value=ConvertNumeralsToArabic(dataGridView1.CurrentRow.Cells[2].Value.ToString());
        }

        //private void Control_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    //your code

        public static string ConvertNumeralsToArabic(string input)
        {

            return input = input.Replace('٠', '0')

                        .Replace('۱', '1')

                        .Replace('۲', '2')

                        .Replace('۳', '3')

                        .Replace('٤', '4')

                        .Replace('۵', '5')

                        .Replace('٦', '6')

                        .Replace('٧', '7')

                        .Replace('٨', '8')

                        .Replace('٩', '9');

        }

        private void dataGridView1_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
          

           
            //try
            //{
            //    if (dataGridView1.CurrentRow.Cells[2].Value==null)
            //    {
            //        dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
            //    }
            //}
            //catch { }

            // dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //dataGridView1.Rows[e.RowIndex].Cells["SN"].Value = (e.RowIndex + 1).ToString();
            //  dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
        }

        private void Add_btn_Click(object sender, EventArgs e)
        {

        }

        private void txt_amt_KeyPress(object sender, KeyPressEventArgs e)
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



            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            //{
            //    e.Handled = true;
            //}
        }

        private void txt_amt_TextChanged(object sender, EventArgs e)
        {
            //if (System.Text.RegularExpressions.Regex.IsMatch(txt_amt.Text, "  ^ [0-9]"))
            //{
            //    txt_amt.Text = "0.0";
            //}

            if (txt_amt.Text.Trim() == "")
            {
                txt_amt.Text = "0";
            }

           // txt_damt.Text = txt_amt.Text;
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

        private void txt_mdate_Validating(object sender, CancelEventArgs e)
        {
            DateTime rs;

            CultureInfo ci = new CultureInfo("en-US", false);

            if (!DateTime.TryParseExact(this.txt_mdate.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            {

                MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                txt_mdate.Focus();
            }
            if (Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) < Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.start_dt)) || Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) > Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.end_dt)))
            {

                MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;

            }

            //if (Convert.ToInt32(txt_mdate.Text.Substring(6, 4) + txt_mdate.Text.Substring(3, 2) + txt_mdate.Text.Substring(0, 2)) < Convert.ToInt32(BL.CLS_Session.start_dt.Substring(6, 4) + BL.CLS_Session.start_dt.Substring(3, 2) + BL.CLS_Session.start_dt.Substring(0, 2)) || Convert.ToInt32(txt_mdate.Text.Substring(6, 4) + txt_mdate.Text.Substring(3, 2) + txt_mdate.Text.Substring(0, 2)) > Convert.ToInt32(BL.CLS_Session.end_dt.Substring(6, 4) + BL.CLS_Session.end_dt.Substring(3, 2) + BL.CLS_Session.end_dt.Substring(0, 2)))
            //{

            //    MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    e.Cancel = true;

            //}
        }

        private void txt_hdate_Validating(object sender, CancelEventArgs e)
        {
            DateTime rs;

            CultureInfo ci = new CultureInfo("ar-SA", false);

            if (!DateTime.TryParseExact(this.txt_hdate.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            {

                MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                txt_hdate.Focus();

            }
            if (Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_hdate.Text)) < Convert.ToInt32(dtv.convert_to_yyyyDDdd(dtv.convert_to_hdate(BL.CLS_Session.start_dt))) || Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_hdate.Text)) > Convert.ToInt32(dtv.convert_to_yyyyDDdd(dtv.convert_to_hdate(BL.CLS_Session.end_dt))))
            {

                MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;

            }
            //if (Convert.ToInt32(txt_hdate.Text.Substring(6, 4) + txt_hdate.Text.Substring(3, 2) + txt_hdate.Text.Substring(0, 2)) < Convert.ToInt32(convert_to_hdate(BL.CLS_Session.start_dt).Substring(6, 4) + convert_to_hdate(BL.CLS_Session.start_dt).Substring(3, 2) + convert_to_hdate(BL.CLS_Session.start_dt).Substring(0, 2)) || Convert.ToInt32(txt_hdate.Text.Substring(6, 4) + txt_hdate.Text.Substring(3, 2) + txt_hdate.Text.Substring(0, 2)) > Convert.ToInt32(convert_to_hdate(BL.CLS_Session.end_dt).Substring(6, 4) + convert_to_hdate(BL.CLS_Session.end_dt).Substring(3, 2) + convert_to_hdate(BL.CLS_Session.end_dt).Substring(0, 2)))
            //{

            //    MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    e.Cancel = true;

            //}

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // MessageBox.Show(comboBox1.SelectedValue.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.Owner.Enabled = true;
            this.Close();
        }

        private void txt_amt_KeyUp(object sender, KeyEventArgs e)
        {
            //if (!(e.KeyCode == Keys.Back))
            //{
            //    string text = txt_amt.Text.Replace(",", "");
            //    if (text.Length % 3 == 0)
            //    {
            //        txt_amt.Text += ",";
            //        txt_amt.SelectionStart = txt_amt.Text.Length;
            //    }
            //}
        }

        private void txt_mdate_Enter(object sender, EventArgs e)
        {
            // TextBox textBox = (TextBox)sender;
            //   txt_mdate.SelectAll();

            //  txt_mdate.SelectionStart = 0;
            // txt_mdate.SelectionLength = txt_mdate.Text.Length;
            txt_mdate.Focus();
            txt_mdate.SelectAll();
        }

        private void txt_hdate_Enter(object sender, EventArgs e)
        {
            // TextBox textBox = (TextBox)sender;
            // txt_hdate.SelectAll();
            //txt_hdate.SelectionStart = 0;
            //txt_hdate.SelectionLength = txt_hdate.Text.Length;

            txt_hdate.Focus();
            txt_hdate.SelectAll();
        }

        private void Add_btn_Click_1(object sender, EventArgs e)
        {
            
            txt_mdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            txt_hdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));
            
           // dthdr.Rows.Clear();
           // dtdtl.Rows.Clear();
            btn_Rfrsh.Enabled = false;
            txt_key.Text = BL.CLS_Session.brcash;
            load_key();

            isnew = true;
            isupdate = false;
            btn_Save.Enabled = true;
            btn_Add.Enabled = false;
            btn_Undo.Enabled = true;
            btn_Exit.Enabled = false;
            btn_Find.Enabled = false;
            btn_Del.Enabled = false;
            btn_Edit.Enabled = false;
            btn_Post.Enabled = false;
            btn_Print.Enabled = false;
            txt_user.Text = BL.CLS_Session.UserName;
           // comboBox1.Enabled = true;
            if (dttrtyps.Rows.Count > 0)
                comboBox1.SelectedIndex = 0;
            txt_ref.Enabled = false;
            if (BL.CLS_Session.up_chang_dt)
            {
                txt_hdate.Enabled = true;
                txt_mdate.Enabled = true;
            }
            else
            {
                txt_hdate.Enabled = false;
                txt_mdate.Enabled = false;
            }
            cmb_colman.Enabled = true;
            cmb_colman.SelectedValue = -1;
            txt_desc.Enabled = true;
            cmb_salctr.Enabled = true;
           // cmb_type.Enabled = true;
            txt_fno.Enabled = true;
            txt_custno.Enabled = true;
            txt_bookno.Enabled = true;
            txt_key.Enabled = true;
           // txt_amt.Enabled = true;
            txt_discnt.Enabled = true;
            btn_keyserch.Enabled = true;
           // dataGridView1.Enabled = true;
        
           // DataGridView dtg = new DataGridView();
          //  dataGridView1.Rows.Clear();
           // dataGridView1.DataSource = dtg;
           
          //  dataGridView1.AllowUserToAddRows = true;
            //if (dataGridView1.Rows.Count-1 > 0)
            //{
            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        if (!row.IsNewRow)
            //        {
            //            dataGridView1.Rows.Remove(row);
            //        }

            //    }
            //    //dataGridView1.CurrentRow.Cells[0].Value = "";
            //    //dataGridView1.CurrentRow.Cells[1].Value = "";
            //    //dataGridView1.CurrentRow.Cells[2].Value = "";
            //    //dataGridView1.CurrentRow.Cells[3].Value = "0";
            //    //dataGridView1.CurrentRow.Cells[4].Value = "0";
            //    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            //}
            /*
            if (dataGridView1.Rows.Count >= 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        dataGridView1.Rows.Remove(row);
                    }

                }
              //  dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
             */
           

           // comboBox1.Focus();
            if (txt_mdate.Enabled == true)
                txt_mdate.Focus();
            else
                txt_custno.Focus();

            txt_ref.Text = "";
            txt_desc.Text = "سداد فاتورة اجل";
            txt_bookno.Text = "";
            txt_amt.Text = "0";
            txt_discnt.Text = "0";
             
            //dataGridView1.Rows.Clear();
            //dataGridView1.Refresh();
           // dataGridView1.AllowUserToAddRows = true;
        }

        private void btn_Undo_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("هل تريد التراجع عن التغييرات", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                
              

                if (isupdate)
                {
                    isupdate = false;
                   // btn_Save_Click(sender, e);
                    Fill_Data(comboBox1.SelectedValue.ToString(), txt_ref.Text);
                    btn_Save.Enabled = false;
                    btn_Add.Enabled = true;
                    btn_Undo.Enabled = false;
                    btn_Exit.Enabled = true;
                    btn_Find.Enabled = true;
                    btn_Edit.Enabled = true;
                    
                    comboBox1.Enabled = false;
                    cmb_colman.Enabled = false;
                    txt_ref.Enabled = false;

                    
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
                    
                    comboBox1.Enabled = false;
                    cmb_colman.Enabled = false;
                    txt_ref.Enabled = false;
                  //  dataGridView1.AllowUserToAddRows = false;

                    comboBox1.SelectedIndex = -1;
                    cmb_colman.SelectedIndex = -1;
                    txt_amt.Text = "0";
                    txt_discnt.Text = "0";
                    txt_desc.Text = "";
                    txt_bookno.Text = "";

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
                   

                }

                
               // Acc_Tran_Load(sender,e);

                txt_mdate.Enabled = false;
                txt_hdate.Enabled = false;
                txt_desc.Enabled = false;
                cmb_salctr.Enabled = false;
              //  cmb_type.Enabled = false;
                txt_fno.Enabled = false;
                txt_custno.Enabled = false;
                txt_bookno.Enabled = false;
                txt_key.Enabled = false;
                txt_amt.Enabled = false;
                txt_discnt.Enabled = false;
                comboBox1.Enabled = false;
                cmb_colman.Enabled = false;
                txt_ref.Enabled = false;
                btn_keyserch.Enabled = false;
                btn_Rfrsh.Enabled = true;

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

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) > Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.end_dt)) || Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) < Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.start_dt)))
            {
                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Date Out of Years" : "لا يمكن ادخال حركة خارج السنة المالية", "Error خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                //if (dataGridView1.RowCount <= 1)
                //{
                //    MessageBox.Show("لا يوجد مدخلات", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                cmb_salctr_Leave(sender, e);
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("يرجى اختيار نوع القيد","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }

                if (txt_key.Text == "" || txt_name.Text == "")
                {
                    MessageBox.Show("يرجى ادخال حساب","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txt_key.Focus();
                    return;
                }
                if (txt_fno.Text == "" || txt_fno.Text == null)
                {
                    MessageBox.Show("يجب ادخال رقم الفاتورة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_fno.Focus();
                    return;
                }

                string mdate, hdate;
                mdate = txt_mdate.Text.Replace("-", "").Substring(4, 4) + txt_mdate.Text.Replace("-", "").Substring(2, 2) + txt_mdate.Text.Replace("-", "").Substring(0, 2);
                hdate = txt_hdate.Text.Replace("-", "").Substring(4, 4) + txt_hdate.Text.Replace("-", "").Substring(2, 2) + txt_hdate.Text.Replace("-", "").Substring(0, 2);

                DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(a_ref)," + BL.CLS_Session.dtbranch.Rows[0]["jv_" + comboBox1.SelectedValue + ""] + ") from acc_hdr where a_type='" + comboBox1.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "'");

                int ref_max = Convert.ToInt32(dt.Rows[0][0]) + 1;



                if (isnew && string.IsNullOrEmpty(txt_ref.Text))
                {
                    daml.SqlCon_Open();
                    int exexcuted = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_brno,a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,mainkey,usrid,jhsrc,jhcustno,serial_no,jhlccrttl) VALUES('" + BL.CLS_Session.brno + "','" + comboBox1.SelectedValue + "'," + ref_max + ",'" + mdate + "','" + hdate + "','" + txt_desc.Text + "'," + txt_amt.Text + "," + (0) + ",'" + txt_key.Text + "','" + txt_user.Text + "','Cus3','" + (cmb_colman.SelectedIndex != -1 ? cmb_colman.SelectedValue.ToString() : "") + "','" + txt_bookno.Text + "'," + txt_discnt.Text + ")", false);
                    daml.SqlCon_Close();
                    if (exexcuted > 0)
                    {
                        txt_ref.Text = ref_max.ToString();
                        txt_ref.Enabled = false;
                        txt_hdate.Enabled = false;
                        txt_mdate.Enabled = false;
                        txt_desc.Enabled = false;
                        cmb_salctr.Enabled = false;
                       // cmb_type.Enabled = false;
                        txt_fno.Enabled = false;
                        txt_custno.Enabled = false;
                        txt_bookno.Enabled = false;
                        txt_key.Enabled = false;
                        txt_amt.Enabled = false;
                        txt_discnt.Enabled = false;
                      //  dataGridView1.ReadOnly = true;
                        comboBox1.Enabled = false;
                        cmb_colman.Enabled = false;
                        isnew = false;
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    daml.SqlCon_Open();
                    int exexcuted = daml.Insert_Update_Delete_retrn_int("update acc_hdr set lastupdt=getdate(),a_mdate='" + mdate + "', a_hdate='" + hdate + "',a_text='" + txt_desc.Text + "',a_amt=" + txt_amt.Text + ",a_entries=" + 0 + ",mainkey='" + txt_key.Text + "',usrid='" + txt_user.Text + "',jhcustno='" + (cmb_colman.SelectedIndex != -1 ? cmb_colman.SelectedValue.ToString() : "") + "',serial_no='" + txt_bookno.Text + "',jhlccrttl=" + txt_discnt.Text + " where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "'", false);
                    exexcuted = exexcuted + daml.Insert_Update_Delete_retrn_int("delete from acc_dtl where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "' and a_brno='"+ BL.CLS_Session.brno +"'", false);
                    daml.SqlCon_Close();
                    if (exexcuted > 0)
                    {
                      //  txt_ref.Text = ref_max.ToString();
                        txt_ref.Enabled = false;
                        txt_hdate.Enabled = false;
                        txt_mdate.Enabled = false;
                        txt_desc.Enabled = false;
                        cmb_salctr.Enabled = false;
                        //cmb_type.Enabled = false;
                        txt_fno.Enabled = false;
                        txt_custno.Enabled = false;
                        txt_bookno.Enabled = false;
                        txt_key.Enabled = false;
                        txt_amt.Enabled = false;
                        txt_discnt.Enabled = false;
                        txt_discnt.Enabled = false;
                      //  dataGridView1.ReadOnly = true;
                    }
                    else
                    {

                    }

                }

                using (SqlCommand cmd = new SqlCommand("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", con))
                {
                    cmd.Parameters.AddWithValue("@a0", BL.CLS_Session.brno);
                    cmd.Parameters.AddWithValue("@a1", comboBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@a2", (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)));
                    cmd.Parameters.AddWithValue("@a3", txt_key.Text);
                    cmd.Parameters.AddWithValue("@a4", txt_desc.Text + " " + txt_fno.Text);
                    // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                    cmd.Parameters.AddWithValue("@a5", 0);
                    // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                    cmd.Parameters.AddWithValue("@a6",  Convert.ToDouble(txt_amt.Text));
                    cmd.Parameters.AddWithValue("@a7", 1);
                    cmd.Parameters.AddWithValue("@a8", mdate);
                    cmd.Parameters.AddWithValue("@a9", hdate);
                    cmd.Parameters.AddWithValue("@a10", "D");
                  //  cmd.Parameters.AddWithValue("@a11", cmb_salctr.SelectedValue);
                   // cmd.Parameters.AddWithValue("@a11", "D");
                    //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                    //if (row.Cells[7].Value != null)
                    // { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                    // cmd.Parameters.AddWithValue("@c9", comp_id);
                    // cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                    //con.Open();
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    //con.Close();
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                //if (Convert.ToDouble(txt_discnt.Text) > 0)
                //{
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,qst_ref,qst_sl,cu_no) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13)", con))
                    {
                        cmd.Parameters.AddWithValue("@a0", BL.CLS_Session.brno);
                        cmd.Parameters.AddWithValue("@a1", comboBox1.SelectedValue);
                        cmd.Parameters.AddWithValue("@a2", (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)));
                        cmd.Parameters.AddWithValue("@a3", strdcash);
                        cmd.Parameters.AddWithValue("@a4", "سداد فاتورة اجلة رقم" + " " + txt_fno.Text);
                        // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                        cmd.Parameters.AddWithValue("@a5", Convert.ToDouble(txt_amt.Text));
                        // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                        cmd.Parameters.AddWithValue("@a6", 0);
                        cmd.Parameters.AddWithValue("@a7", 2);
                        cmd.Parameters.AddWithValue("@a8", mdate);
                        cmd.Parameters.AddWithValue("@a9", hdate);
                        cmd.Parameters.AddWithValue("@a10", "C");
                        cmd.Parameters.AddWithValue("@a11", Convert.ToInt32(txt_fno.Text));
                        cmd.Parameters.AddWithValue("@a12", cmb_salctr.SelectedValue);
                        cmd.Parameters.AddWithValue("@a13", txt_custno.Text);
                        // cmd.Parameters.AddWithValue("@a11", "D");
                        //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                        //if (row.Cells[7].Value != null)
                        // { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                        // cmd.Parameters.AddWithValue("@c9", comp_id);
                        // cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                        //con.Open();
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                        //con.Close();
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }

                    //using (SqlCommand cmd = new SqlCommand("update acc_dtl set match=1 where a_type='05' and a_brno='" + BL.CLS_Session.brno + "' and cu_no='" + txt_custno.Text + "' and a_ref="+txt_fno.Text+"", con))
                    //{
                       
                    //    if (con.State != ConnectionState.Open)
                    //    {
                    //        con.Open();
                    //    }
                    //    cmd.ExecuteNonQuery();
                    //    //con.Close();
                    //    if (con.State == ConnectionState.Open)
                    //    {
                    //        con.Close();
                    //    }
                    //}
                //}

               
                
                btn_Save.Enabled = false;
                btn_Add.Enabled = true;
                btn_Exit.Enabled = true;
                btn_Find.Enabled = true;
                btn_Undo.Enabled = false;
                btn_Del.Enabled = true;
                btn_Edit.Enabled = true;
                btn_keyserch.Enabled = false;
                btn_Print.Enabled = true;
                if (notposted)
                { btn_Post.Enabled = true; }
                isnew = false;
                isupdate = false;
                cmb_colman.Enabled = false;
                
                btn_Rfrsh.Enabled = true;
                btn_Rfrsh_Click(sender, e); //to print

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (txt_user.Text != BL.CLS_Session.UserName && BL.CLS_Session.up_edit_othr == false)
            {
                MessageBox.Show("لا تملك صلاحية تعديل حركة شخص اخر", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (!dthdr.Rows[0][8].ToString().Equals("Cus3"))
                {
                    MessageBox.Show("لا يمكن تعديل القيد من هذه الشاشة","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;

                }
                btn_Rfrsh_Click(sender, e);
                if (btn_Post.Enabled == true)
                {
                    btn_Rfrsh.Enabled = false;
                    notposted = true;
                    isnew = false;
                    isupdate = true;
                    btn_Save.Enabled = true;
                    btn_Add.Enabled = false;
                    btn_Undo.Enabled = true;
                    btn_Exit.Enabled = false;
                    btn_Find.Enabled = false;
                    btn_Del.Enabled = false;
                    btn_Edit.Enabled = false;
                    btn_Post.Enabled = false;
                    cmb_colman.Enabled = true;
                    btn_Print.Enabled = false;
                    txt_ref.Enabled = false;
                    if (BL.CLS_Session.up_chang_dt)
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
                    cmb_salctr.Enabled = true;
                   // cmb_type.Enabled = true;
                    txt_fno.Enabled = true;
                    txt_custno.Enabled = true;
                    txt_bookno.Enabled = true;
                    txt_key.Enabled = true;
                    txt_amt.Enabled = true;
                    txt_discnt.Enabled = true;
                    btn_keyserch.Enabled = true;
                    txt_user.Text = BL.CLS_Session.UserName;
                    // dataGridView1.Enabled = true;
                   
                }
                else
                {
                    MessageBox.Show("لا يمكن تعديل حركة مرحلة", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            btn_Rfrsh_Click(sender, e);
            if (!dthdr.Rows[0][8].ToString().Equals("Cus3"))
            {
                MessageBox.Show("لا يمكن حذف القيد من هذه الشاشة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (btn_Post.Enabled == true)
            {
            DialogResult result = MessageBox.Show("هل تريد المسح", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
               
                try
                {
                    daml.SqlCon_Open();
                    ////if (dthdr.Rows[0][8].ToString().Equals("SL"))
                    ////{
                    ////    daml.Insert_Update_Delete_retrn_int("update sales_hdr set gen_ref=0 where gen_ref=" + txt_ref.Text, false);

                    ////}
                    int exexcuted = daml.Insert_Update_Delete_retrn_int("delete from acc_hdr where a_posted=0 and a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "'", false);
                    // exexcuted = exexcuted + daml.Insert_Update_Delete("delete from acc_dtl where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "'", false);
                    daml.SqlCon_Close();

                    if (exexcuted > 0)
                    {
                        MessageBox.Show("تم الحذف بنجاح", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        btn_Del.Enabled = false;
                      
                        
                        txt_ref.Text = "";
                        txt_desc.Text = "";
                        txt_bookno.Text = "";
                        txt_amt.Text = "";
                        txt_discnt.Text = "";
                        comboBox1.SelectedIndex = -1;

                      
                    }
                    else
                    {
                        MessageBox.Show("لم يتم الحذف", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
            else
            {
                MessageBox.Show("لا يمكن تعديل حركة مرحلة", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                var sr = new Search_All("3", "Cus3");
                sr.ShowDialog();

               // Fill_Data(sr.dataGridView1.CurrentRow.Cells[0].Value.ToString(), sr.dataGridView1.CurrentRow.Cells[1].Value.ToString());
                Fill_Data(sr.dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), sr.dataGridView1.CurrentRow.Cells[1].Value.ToString());
            }
            catch { }
        }

        private void Fill_Data(string atype, string aref)
        {
            try
            {
              //  string mdate, hdate;
               // mdate = txt_mdate.Text.Replace("-", "").Substring(4, 4) + txt_mdate.Text.Replace("-", "").Substring(2, 2) + txt_mdate.Text.Replace("-", "").Substring(0, 2);
              //  hdate = txt_hdate.Text.Replace("-", "").Substring(4, 4) + txt_hdate.Text.Replace("-", "").Substring(2, 2) + txt_hdate.Text.Replace("-", "").Substring(0, 2);


                dthdr = daml.SELECT_QUIRY_only_retrn_dt("select * from acc_hdr where a_type='" + atype + "' and a_ref=" + aref + " and a_brno='" + BL.CLS_Session.brno + "' and jhsrc='Cus3'");
                dtdtl = daml.SELECT_QUIRY_only_retrn_dt("select * from acc_dtl aa where aa.a_type='" + atype + "' and aa.a_ref=" + aref + " and aa.a_brno='" + BL.CLS_Session.brno + "' and aa.a_folio=2 order by aa.a_folio");
                
                // load_key();
                 if (dthdr.Rows[0]["mainkey"].ToString().Length > 0 )
                 {
                     txt_key.Text = dthdr.Rows[0]["mainkey"].ToString();
                     txt_custno.Text = dtdtl.Rows[0]["cu_no"].ToString();

                     DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select a_name from accounts where a_key='" + txt_key.Text + "' and a_brno='" + BL.CLS_Session.brno + "'");
                     DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select cu_name from customers where cu_no='" + txt_custno.Text + "' and cu_brno='" + BL.CLS_Session.brno + "'");
                     // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);
                     txt_name.Text = dt.Rows[0][0].ToString();
                     txt_custnam.Text = dt2.Rows[0][0].ToString();

                     comboBox1.SelectedValue = dthdr.Rows[0][1];
                     cmb_colman.SelectedValue = dthdr.Rows[0]["jhcustno"];
                     txt_ref.Text =isnew?"": dthdr.Rows[0][2].ToString();
                     txt_mdate.Text = dthdr.Rows[0][3].ToString().Substring(6, 2) + dthdr.Rows[0][3].ToString().Substring(4, 2) + dthdr.Rows[0][3].ToString().Substring(0, 4);
                     txt_hdate.Text = dthdr.Rows[0][4].ToString().Substring(6, 2) + dthdr.Rows[0][4].ToString().Substring(4, 2) + dthdr.Rows[0][4].ToString().Substring(0, 4);
                     txt_desc.Text = dthdr.Rows[0][5].ToString();
                     txt_amt.Text = dthdr.Rows[0][6].ToString();
                     txt_discnt.Text = dthdr.Rows[0]["jhlccrttl"].ToString();
                     txt_user.Text = dthdr.Rows[0]["usrid"].ToString();
                     txt_bookno.Text = dthdr.Rows[0]["serial_no"].ToString();
                     

                     if (Convert.ToBoolean(dthdr.Rows[0][14]) == true)
                     {
                         btn_Post.Enabled = false;

                     }
                     else { btn_Post.Enabled = true; }

                     btn_Del.Enabled = true;
                     btn_Edit.Enabled = true;
                     // txt_ref.Text = dthdr.Rows[0][2].ToString();

                     //dataGridView1.DataSource = dtdtl;
                     //dataGridView1.DataSource = dataGridView1.DataSource;
                     //dthdr.Rows.Clear();
                     //dtdtl.Rows.Clear();

                     txt_fno.Text = dtdtl.Rows[0]["qst_ref"].ToString();
                     txt_fno_Leave(null, null);
                 }
                 else
                 {
                     MessageBox.Show("لا يمكن فتح الحركة من هذه الشاشة يرجى فتحها من شاشة قيد عام");
                   //  return;
                 }

                 check_system(dthdr.Rows[0][8].ToString()); 
              
            }
            catch { }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                var fsn = new Search_by_No("02");


                fsn.comboBox1.DataSource = dttrtyps;
                fsn.comboBox1.DisplayMember = "tr_name";
                fsn.comboBox1.ValueMember = "tr_no";

                fsn.ShowDialog();

                

                Fill_Data(fsn.comboBox1.SelectedValue.ToString(), fsn.textBox1.Text);
            }
            catch { }
          //  dataGridView1.Enabled = true;
        }

        private void check_system(string tochek)
        {
            if (tochek.StartsWith("Sal"))
                txt_system.Text = "المبيعات";

            if (tochek.StartsWith("Acc"))
                txt_system.Text = "الحسابات";

            if (tochek.StartsWith("Cus"))
                txt_system.Text = "العملاء";

            if (tochek.StartsWith("Sup"))
                txt_system.Text = "الموردين";

            if (tochek.StartsWith("Pos"))
                txt_system.Text = "نقاط البيع";

            if (tochek.StartsWith("Pay"))
                txt_system.Text = "المشتريات";
        }

        private void btn_Post_Click(object sender, EventArgs e)
        {
            //if (Convert.ToDouble(txt_damt.Text) != Convert.ToDouble(txt_amt.Text) || Convert.ToDouble(txt_camt.Text) != Convert.ToDouble(txt_amt.Text))
            //{
            //    MessageBox.Show("لا يمكن ترحيل حركة غير متزنة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            try
                {
                    daml.SqlCon_Open();
                    int exexcuted = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_posted=1 where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "'", false);
                    // exexcuted = exexcuted + daml.Insert_Update_Delete("delete from acc_dtl where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "'", false);
                    daml.SqlCon_Close();

                    if (exexcuted > 0)
                    {
                        MessageBox.Show("تم الترحيل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        btn_Post.Enabled = false;
                    }
                    else
                    {
                         MessageBox.Show("فشل في الترحيل", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            catch { }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            Acc_Statment_Exp f4a = new Acc_Statment_Exp(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                f4a.MdiParent = ParentForm;
                f4a.Show();
            */
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                //System.Windows.Forms.Control.SelectNextControl();
                //   SendKeys.Send("{TAB}");
                if (txt_mdate.Enabled == true)
                {
                    txt_mdate.Focus();
                    // txt_mdate.Select();
                    //  txt_mdate.SelectionStart = 0;
                    //   txt_mdate.SelectionLength = 0;// txt_mdate.Text.Length;  
                }
                else
                {
                    txt_desc.Focus();
                }
            }
        }

        private void txt_mdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_hdate.Focus();
            }
        }

        private void txt_hdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_bookno.Focus();
            }
        }

        private void txt_desc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_amt.Focus();
            }
        }

        private void txt_amt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
            }
        }

        private void btn_Rfrsh_Click(object sender, EventArgs e)
        {
            try
            {
                Fill_Data(comboBox1.SelectedValue.ToString(), txt_ref.Text);
            }
            catch { }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
        //    GridPrintDocument doc = new GridPrintDocument(this.dataGridView1, this.dataGridView1.Font, true);
        //    doc.DocumentName = "Preview Test";
        //    doc.DrawCellBox = true;
        //    PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
        //    printPreviewDialog.ClientSize = new Size(1000, 800);
        //    printPreviewDialog.Location = new System.Drawing.Point(29, 29);
        //    printPreviewDialog.Name = "Print Preview Dialog";
        //    printPreviewDialog.UseAntiAlias = true;
        //    printPreviewDialog.Document = doc;
        //    printPreviewDialog.ShowDialog();
        //    doc.Dispose();
        //    doc = null;
            try
            {
                Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();

                //SqlConnection con = new SqlConnection(@"Data Source=RYD1-PC\INFOSOFT12;" +
                //                                    "Initial Catalog=DBASE;" +
                //                                    "User id=sa;" +
                //                                    "Password=infocic;");

                //SqlDataAdapter da1 = new SqlDataAdapter("select * from accounts where a_key='010201001'", con);
                //SqlDataAdapter da2 = new SqlDataAdapter("select * from acc_hdr where a_ref=7", con);
                //SqlDataAdapter da3 = new SqlDataAdapter("SELECT acc_dtl.a_key, accounts.a_name, acc_dtl.a_text, acc_dtl.a_camt, acc_dtl.a_damt"
                //                                       + " FROM acc_dtl INNER JOIN"
                //                                       + " accounts ON acc_dtl.a_key = accounts.a_key"
                //                                       + " where acc_dtl.a_key='010201001'", con);

                DataSet ds1 = new DataSet();

                // ds1.Tables["accounts"]=dthdr;
                // ds1.Tables["accounts"] = dthdr;
                //  da3.Fill(ds1, "details");

                // this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds1));

                //ReportParameter[] parameters = new ReportParameter[1];
                //parameters[0] = new ReportParameter("comp_name", "hi");

                //rf.reportViewer1.LocalReport.SetParameters(parameters);

                // rf.reportViewer1.LocalReport.SetParameters("comp_name", BL.CLS_Session.cmp_name);

                // rf.reportViewer1.LocalReport.Refresh();


                rf.reportViewer1.LocalReport.DataSources.Clear();
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("acc_hdr", dthdr));
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("details", dtdtl));

                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Acc.rpt.Acc_Tran_rpt.rdlc";

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[4];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                parameters[2] = new ReportParameter("aType", comboBox1.Text);
                parameters[3] = new ReportParameter("br_name", BL.CLS_Session.brname);
                rf.reportViewer1.LocalReport.SetParameters(parameters);

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();
            }
            catch { }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void txt_camt_TextChanged(object sender, EventArgs e)
        {
            //if (Convert.ToDouble(txt_camt.Text) > Convert.ToDouble(txt_amt.Text))
            //{
            //    MessageBox.Show("تجاوز مبلغ القيد", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
        }

        private void txt_damt_TextChanged(object sender, EventArgs e)
        {
            //if (Convert.ToDouble(txt_damt.Text) > Convert.ToDouble(txt_amt.Text))
            //{
            //    MessageBox.Show("تجاوز مبلغ القيد", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
        }

        private void نسخToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void حذفالصفToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
        }

        private void كشفحسابToolStripMenuItem_Click(object sender, EventArgs e)
        {
             try
            {
            // dataGridView1_CellDoubleClick( null,  null);
                //Acc_Statment_Exp f4a = new Acc_Statment_Exp(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                //f4a.MdiParent = ParentForm;
                //f4a.Show();
            }
            catch { }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            

        }

        private void txt_key_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

               // button1_Click(sender, e);
               // dataGridView1.Focus();


            }

            if (e.KeyCode == Keys.F8)
            {
                txt_name.Text = "";
                try
                {
                    Search_All f4 = new Search_All("1", "Acc");
                    f4.ShowDialog();
                    txt_key.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                }
                catch { }

            }
        }

        private void load_key()
        {
            DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select a_name from accounts where a_key='" + txt_key.Text + "' and a_brno='" + BL.CLS_Session.brno + "'");
            // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

            if (dt.Rows.Count > 0)
                txt_name.Text = dt.Rows[0][0].ToString();
            else
                txt_name.Text = "";
            //if (txt_key.Text.Length > 0)
            //{
            //    DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select a_name from accounts where a_key='" + txt_key.Text + "' and a_brno='01'");
            //    // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);


            //    txt_name.Text = dt.Rows[0][0].ToString();
            //}
            //else
            //{
            //    MessageBox.Show("لا يمكن فتح الحركة من هذه الشاشة");
            //    return;
            //}
            //    txt_code.Text = dth.Rows[0][2].ToString();
             //   txt_opnbal.Text = dth.Rows[0][1].ToString();
            
        }

        private void btn_keyserch_Click(object sender, EventArgs e)
        {
            try
            {
                Search_All f4 = new Search_All("1", "Acc");
                f4.ShowDialog();
                txt_key.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            catch { }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
           
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txt_key_Leave(object sender, EventArgs e)
        {
            load_key();
        }

        private void cmb_colman_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_colman.SelectedIndex = -1;
            }

        }

        private void txt_amt_Leave(object sender, EventArgs e)
        {
           
        }

        private void txt_bookno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_custno.Focus();
            }
        }

        private void txt_bookno_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.Handled = !(char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar));
            if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                if (e.KeyChar >= '0' && e.KeyChar <= '9')
                    e.Handled = false;
                else
                    e.Handled = true;
                //  e.Handled = true;
            }
        }

        private void txt_discnt_TextChanged(object sender, EventArgs e)
        {
            if (txt_discnt.Text.Trim() == "")
            {
                txt_discnt.Text = "0";
            }
        }

        private void cmb_salctr_Leave(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dr = dtslctr.Select("sl_no = '" + cmb_salctr.SelectedValue + "'");
                strsndoq = dr[0][3].ToString();

                DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select b.cl_acc from cuclass b join customers a on a.cu_class=b.cl_no  where  a.cu_brno='" + BL.CLS_Session.brno + "' and cu_no='" + txt_custno.Text + "'");
                strdcash = dt.Rows[0][0].ToString();
                // strdcash = dr[0]["dp_diffser"].ToString();
            }
            catch { }
        }

        private void txt_fno_Leave(object sender, EventArgs e)
        {

            try
            {
                DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select * from sales_hdr where 0=0 and invtype='" + cmb_type.SelectedValue + "' and slcenter='" + cmb_salctr.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "' and ref=" + txt_fno.Text + "");
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("الفاتورة غير موجودة بالنظام", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_fno.Text = "";
                    return;
                }
                else
                {
                    txt_custno.Text = dt.Rows[0]["custno"].ToString();
                    DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select cu_name from customers where cu_no='" + txt_custno.Text + "' and cu_brno='" + BL.CLS_Session.brno + "'");
                    txt_custnam.Text = dt2.Rows[0][0].ToString();

                    txt_text.Text = dt.Rows[0]["text"].ToString();
                    txt_remain.Text = (Convert.ToDouble(dt.Rows[0]["nettotal"]) - Convert.ToDouble(dt.Rows[0]["invpaid"])).ToString();
                    txt_amt.Text = dt.Rows[0]["nettotal"].ToString();
                }
            }
            catch(Exception ex) {
               // MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btn_statmnt_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.formkey.Contains("B13S"))
            {
                Sal.Sal_Qst_Statment_Exp f4a = new Sal.Sal_Qst_Statment_Exp(txt_fno.Text, cmb_salctr.SelectedValue.ToString());
                f4a.MdiParent = MdiParent;
                f4a.Show();
            }
            
        }

        private void Acc_Tran_QFK_Shown(object sender, EventArgs e)
        {
            if (!BL.CLS_Session.formkey.Contains(this.Tag.ToString()))
            {
                this.Close();
            }
        }

        private void txt_fno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txt_text.Focus();
                }

                if (e.KeyCode == Keys.F8)
                {
                    var sr = new Search_All("6-6", " and custno='" + txt_custno.Text + "' and invtype='" + cmb_type.SelectedValue + "'");
                    sr.checkBox1.Visible = true;
                    sr.ShowDialog();
                    txt_fno.Text = sr.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txt_amt.Text = sr.dataGridView1.CurrentRow.Cells[5].Value.ToString();
                }
            }
            catch { }
        }

        private void cmb_type_Leave(object sender, EventArgs e)
        {

        }

        private void txt_custno_Leave(object sender, EventArgs e)
        {
            try
            {
                DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select cu_name from customers where cu_no='" + txt_custno.Text + "' and cu_brno='" + BL.CLS_Session.brno + "'");
                txt_custnam.Text = dt2.Rows[0][0].ToString();
                txt_fno.Focus();
                SendKeys.Send("{F8}");
            }
            catch { }
        }

        private void txt_custno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_fno.Focus();
            }

            if (e.KeyCode == Keys.F8)
            {
                txt_name.Text = "";
                try
                {
                    Search_All f4 = new Search_All("5", "Cus");
                    f4.ShowDialog();
                    txt_custno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_custnam.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                }
                catch { }

            }
        }
    }
}



