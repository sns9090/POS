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
using Microsoft.Reporting.WinForms;
using System.Globalization;
//using CrystalDecisions.CrystalReports.Engine;
using System.Threading;


namespace POS.Sto
{
    public partial class Item_Card : BaseForm
    {
        public int from_pur=0;
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dtunits, dtunits2, dtunits3, dtunits4, dtunits5, dtcount;
        BL.DAML dml = new BL.DAML();
        string bc = "",bprinter,item,item_temp="";
        bool isnew,isupdate,ismoved=false;
        string path = "",strimgname="";
        SqlConnection con2 = BL.DAML.con;//new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        //public Item_Card(string itemno)
        public Item_Card(string it_no)
        {
            item = it_no;
           // Form.ActiveForm.Refresh();

            InitializeComponent();
           
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            cmb_dunit.Enabled = false;
            txt_staticcst.Enabled = false;
            chk_inactv.Enabled = false ;
            //textBox3.Enabled = false;
            cmb_type.Enabled = false;
            txt_price.Enabled = false;
            textBox5.Enabled = false;
            txt_ename.Enabled = false;
            cmb_unit.Enabled = false;
           // button1.Enabled = false;
            cmb_group.Enabled = false;
            cmb_sgroup.Enabled = false;
            cmb_tax.Enabled = false;
            // button3.Enabled = false;
          //  button6.Enabled = false;
            checkBox1.Enabled = false;
            pictureBox1.Enabled = false;
            pnl_qtys.Enabled = false;
            txt_price2.Enabled = false;
            txt_note.Enabled = false;
            txt_supno.Enabled = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (isnew)
            {
                if (Convert.ToDouble(txt_u2q.Text) == 0 || cmb_u2.Text == "")
                {
                    MessageBox.Show("يرجى ادخال الوحدة2 وشدها", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmb_u2.Focus();
                    return;
                }

                //if (string.IsNullOrEmpty(txt_supno.Text))
                //{
                //    MessageBox.Show("يرجى ادخال رقم المورد", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    txt_supno.Focus();
                //    return;
                //}



                if (textBox1.Text != "" && textBox2.Text != "" && textBox5.Text != "" && cmb_unit.Text != "" && cmb_group.Items.Count != 0 && cmb_u2.Text != "")
                {
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    byte[] byteImage = ms.ToArray();
                    //if (pictureBox1.Image == null)
                    //{

                    //}
                    //else
                    //{

                    //}

                    if (con2.State == ConnectionState.Closed)
                        con2.Open();
                    SqlDataAdapter da2 = new SqlDataAdapter("select * from items where item_no ='" + textBox1.Text.Trim() + "'", con2);
                    SqlDataAdapter da3 = new SqlDataAdapter("select * from items_bc where barcode ='" + textBox5.Text.Trim() + "'", con2);
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();

                    da2.Fill(dt2);
                    da3.Fill(dt3);

                    if (dt2.Rows.Count == 0 && dt3.Rows.Count == 0)
                    {
                        // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
                        using (SqlCommand cmd1 = new SqlCommand("INSERT INTO items (item_no, item_name, item_cost, item_price, item_barcode, item_unit,item_group,item_image,item_req,item_tax,unit2,uq2,unit2p,unit3,uq3,unit3p,unit4,uq4,unit4p,item_ename,supno,note,last_updt,price2,sgroup,inactive,static_cost,dunit) VALUES(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13,@a14,@a15,@a16,@a17,@a18,@a19,@a20,@a21,@a22,@a23,@a24,@a25,@a26,@a27,@a28)", con2))
                        {


                            cmd1.Parameters.AddWithValue("@a1", textBox1.Text.Trim());
                            cmd1.Parameters.AddWithValue("@a2", textBox2.Text.Trim());
                            cmd1.Parameters.AddWithValue("@a3", (cmb_type.SelectedIndex == 0 ? 1 : cmb_type.SelectedIndex == 1 ? 0 : 2));
                            cmd1.Parameters.AddWithValue("@a4", Convert.ToDouble(txt_price.Text));
                            cmd1.Parameters.AddWithValue("@a5", textBox5.Text.Trim());
                            //  cmd1.Parameters.AddWithValue("@a6", textBox6.Text);
                            cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(cmb_unit.SelectedValue));
                            cmd1.Parameters.AddWithValue("@a7", cmb_group.SelectedValue.ToString());
                            // cmd1.Parameters.AddWithValue("@a8", byteImage);
                            cmd1.Parameters.AddWithValue("@a8", strimgname);
                            if (checkBox1.Checked == true)
                            { cmd1.Parameters.AddWithValue("@a9", 1); }
                            else { cmd1.Parameters.AddWithValue("@a9", 0); }
                            cmd1.Parameters.AddWithValue("@a10", cmb_tax.SelectedValue.ToString());

                            cmd1.Parameters.AddWithValue("@a11", Convert.ToDouble(txt_u2q.Text) == 0 ? 0 : cmb_u2.SelectedIndex == -1 ? 0 : cmb_u2.SelectedValue);
                            cmd1.Parameters.AddWithValue("@a12", Convert.ToDecimal(txt_u2q.Text));
                            cmd1.Parameters.AddWithValue("@a13", Convert.ToDouble(txt_u2p.Text));

                            cmd1.Parameters.AddWithValue("@a14", Convert.ToDouble(txt_u3q.Text) == 0 ? 0 : cmb_u3.SelectedIndex == -1 ? 0 : cmb_u3.SelectedValue);
                            cmd1.Parameters.AddWithValue("@a15", Convert.ToDecimal(txt_u3q.Text));
                            cmd1.Parameters.AddWithValue("@a16", Convert.ToDouble(txt_u3p.Text));

                            cmd1.Parameters.AddWithValue("@a17", Convert.ToDouble(txt_u4q.Text) == 0 ? 0 : cmb_u4.SelectedIndex == -1 ? 0 : cmb_u4.SelectedValue);
                            cmd1.Parameters.AddWithValue("@a18", Convert.ToDecimal(txt_u4q.Text));
                            cmd1.Parameters.AddWithValue("@a19", Convert.ToDouble(txt_u4p.Text));
                            cmd1.Parameters.AddWithValue("@a20", txt_ename.Text);
                            cmd1.Parameters.AddWithValue("@a21", txt_supno.Text);
                            cmd1.Parameters.AddWithValue("@a22", txt_note.Text);
                            cmd1.Parameters.AddWithValue("@a23", DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false)));
                            cmd1.Parameters.AddWithValue("@a24", txt_price2.Text);
                            cmd1.Parameters.AddWithValue("@a25", (cmb_sgroup.SelectedIndex == -1 ? "" : cmb_sgroup.SelectedValue.ToString()));
                            cmd1.Parameters.AddWithValue("@a26", chk_inactv.Checked ? 1 : 0);
                            cmd1.Parameters.AddWithValue("@a27", txt_staticcst.Text);
                            cmd1.Parameters.AddWithValue("@a28", (cmb_dunit.SelectedIndex == -1 ? cmb_unit.SelectedValue : cmb_dunit.SelectedValue));

                            cmd1.ExecuteNonQuery();
                            con2.Close();
                            //  MessageBox.Show("add success", "ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Enabled = false;
                            textBox2.Enabled = false;
                            cmb_dunit.Enabled = false;
                            txt_staticcst.Enabled = false;
                            chk_inactv.Enabled = false;
                            //textBox3.Enabled = false;
                            cmb_type.Enabled = false;
                            txt_price.Enabled = false;
                            textBox5.Enabled = false;
                            txt_supno.Enabled = false;
                            txt_ename.Enabled = false;
                            cmb_unit.Enabled = false;
                            btn_del.Enabled = true;
                            btn_bar.Enabled = true;
                            checkBox1.Enabled = false;
                            btn_save.Enabled = false;
                            btn_savchng.Enabled = false;
                            pictureBox1.Enabled = false;
                            btn_add.Enabled = true;
                            atn_edit.Enabled = true;
                            cmb_group.Enabled = false;
                            cmb_sgroup.Enabled = false;
                            cmb_tax.Enabled = false;
                            pnl_qtys.Enabled = false;
                            txt_price2.Enabled = false;
                            txt_note.Enabled = false;
                            // txt_supno.Enabled = false;
                            btn_statmnt.Enabled = true;

                            btn_Undo.Enabled = false;
                            btn_Exit.Enabled = true;
                            btn_prtbar.Enabled = true;
                            txt_prtbar.Enabled = true;
                            btn_find.Enabled = true;
                            btn_opal.Enabled = true;
                            btn_cpal.Enabled = true;
                            if (cmb_type.SelectedIndex == 2)
                                btn_combin.Enabled = true;
                            else
                                btn_combin.Enabled = false;

                            //if (dt3.Rows.Count == 0)
                            //{
                            using (SqlCommand cmd2 = new SqlCommand("INSERT INTO items_bc (item_no, barcode, pack, pk_qty, price,pkorder,br_no,sl_no,min_price) VALUES(@a11,@a22,@a33,@a44,@a55,@a66,@br,@a77,@a88)", con2))
                            {


                                cmd2.Parameters.AddWithValue("@a11", textBox1.Text.Trim());
                                cmd2.Parameters.AddWithValue("@a22", textBox5.Text.Trim());
                                cmd2.Parameters.AddWithValue("@a33", Convert.ToInt32(cmb_unit.SelectedValue));
                                cmd2.Parameters.AddWithValue("@a44", "1");
                                cmd2.Parameters.AddWithValue("@a55", txt_price.Text);
                                cmd2.Parameters.AddWithValue("@a66", 1);
                                cmd2.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                                cmd2.Parameters.AddWithValue("@a77", BL.CLS_Session.sl_prices );
                                cmd2.Parameters.AddWithValue("@a88", txt_price2.Text);
                                // cmd1.Parameters.AddWithValue("@a6", textBox6.Text);
                                //  cmd1.Parameters.AddWithValue("@a7", comboBox1.SelectedValue.ToString());
                                // cmd1.Parameters.AddWithValue("@a8", byteImage);
                                // cmd1.Parameters.AddWithValue("@a8", path);
                                // if (checkBox1.Checked == true)
                                // { cmd1.Parameters.AddWithValue("@a9", 1); }
                                //  else { cmd1.Parameters.AddWithValue("@a9", 0); }
                                if (con2.State == ConnectionState.Closed)
                                    con2.Open();
                                cmd2.ExecuteNonQuery();
                                con2.Close();
                            }


                            //////using (SqlCommand cmd3 = new SqlCommand("INSERT INTO brprices (branch, slcenter, itemno, lprice1) VALUES(@a111,@a222,@a333,@a444)", con2))
                            //////{


                            //////    cmd3.Parameters.AddWithValue("@a111", BL.CLS_Session.brno);
                            //////    cmd3.Parameters.AddWithValue("@a222", " ");
                            //////    cmd3.Parameters.AddWithValue("@a333", textBox1.Text);
                            //////    cmd3.Parameters.AddWithValue("@a444", txt_price.Text);

                            //////    con2.Open();
                            //////    cmd3.ExecuteNonQuery();
                            //////    con2.Close();
                            //////}


                            //}
                            //else
                            //{
                            //    MessageBox.Show("الباركود موجود من سابق", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //  //  con2.Close();
                            //}

                        }


                        btn_add.Focus();

                        if (from_pur == 1)
                            this.Close();
                    }
                    else
                    {
                        MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Item or barcode already exist" : "الصنف او الباركود موجود من سابق", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con2.Close();
                    }
                }
                else
                {
                    MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "please fill all requird feilds" : "يرجى تعبئة البيانات كاملة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                button6_Click(sender, e);
            }
        }

        public void Item_Card_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.TsIcon;
            //if (BL.CLS_Session.temp_bcorno.Equals("bc"))
            //{
            //    label23.Text = BL.CLS_Session.lang.Equals("E") ? "By Barcode" : "بالباركود";
            //    BL.CLS_Session.temp_bcorno = "bc";
            //}
            //else
            //{
                label23.Text = BL.CLS_Session.lang.Equals("E") ? "By No" : "بالرقم";
                BL.CLS_Session.temp_bcorno = "no";
            //}

            if(BL.CLS_Session.islight)
            {
                btn_brprice.Visible = false;
                btn_irp.Visible = false;
                btn_combin.Visible = false;
                btn_mizan.Visible = false;
               // btn_bar.Visible = false;
            }
            if (!BL.CLS_Session.multi_bc)
            {
                btn_bar.Visible = false;
            }

            if (BL.CLS_Session.isshamltax.Equals("2"))
            {
                txt_bprice.Visible = true;
                label27.Visible = true;
            }

           // MessageBox.Show(BL.CLS_Session.dtunits.Rows.Count.ToString());
            if (BL.CLS_Session.up_editop == false)
            {
                btn_opal.Enabled = false;
            }

            if (BL.CLS_Session.price_type.Equals("2"))
                btn_brprice.Text = "اسعار المراكز";
            if (BL.CLS_Session.up_ch_itmpr == false)
            {
                txt_price.ReadOnly = true;
                btn_brprice.Enabled = false;
            }
           

            if (BL.CLS_Session.sysno == "1")
            {
                btn_irp.Visible = false;
                btn_brprice.Visible = false;
                btn_combin.Visible = false;
                btn_cpal.Visible = false;
                btn_opal.Visible = false;
                panel3.Visible = false;
                btn_bar.Visible = false;
                label15.Text = "Arabic Name";
                btn_mizan.Visible = false;
                panel2.Visible = false;
            }
            textBox7.Select();
           // var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\temp.txt");
            var lines2 = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\bprinter.txt");
            bprinter = lines2.First().ToString();

            string str2 = BL.CLS_Session.formkey;
            string whatever = str2.Substring(str2.IndexOf("E111") + 5, 3);
            if (whatever.Substring(0, 1).Equals("0"))
                btn_add.Visible = false;
            if (whatever.Substring(1, 1).Equals("0"))
                atn_edit.Visible = false;
            if (whatever.Substring(2, 1).Equals("0"))
                btn_del.Visible = false;
            dtunits = dml.SELECT_QUIRY_only_retrn_dt("select unit_id,unit_name from units");
            cmb_unit.DataSource = dtunits;
            cmb_unit.DisplayMember = "unit_name";
            cmb_unit.ValueMember = "unit_id";
           // cmb_unit.SelectedIndex = -1;

            dtunits2 = dml.SELECT_QUIRY_only_retrn_dt("select unit_id,unit_name from units");
            cmb_u2.DataSource = dtunits2;
            cmb_u2.DisplayMember = "unit_name";
            cmb_u2.ValueMember = "unit_id";
            cmb_u2.SelectedIndex = -1;

            dtunits3 = dml.SELECT_QUIRY_only_retrn_dt("select unit_id,unit_name from units");
            cmb_u3.DataSource = dtunits3;
            cmb_u3.DisplayMember = "unit_name";
            cmb_u3.ValueMember = "unit_id";
            cmb_u3.SelectedIndex = -1;

            dtunits4 = dml.SELECT_QUIRY_only_retrn_dt("select unit_id,unit_name from units");
            cmb_u4.DataSource = dtunits4;
            cmb_u4.DisplayMember = "unit_name";
            cmb_u4.ValueMember = "unit_id";
            cmb_u4.SelectedIndex = -1;

            dtunits5 = dml.SELECT_QUIRY_only_retrn_dt("select unit_id,unit_name from units");
            cmb_dunit.DataSource = dtunits5;
            cmb_dunit.DisplayMember = "unit_name";
            cmb_dunit.ValueMember = "unit_id";
            cmb_dunit.SelectedIndex = -1;

          


            cmb_tax.DataSource = dml.SELECT_QUIRY_only_retrn_dt("select tax_id,tax_name from taxs");
            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
            cmb_tax.DisplayMember = "tax_name";
            cmb_tax.ValueMember = "tax_id";


          //  SqlDataAdapter da = new SqlDataAdapter("select group_id,group_name from groups", con2);
          //  DataTable dt = new DataTable();
          //  da.Fill(dt);


            cmb_group.DataSource = dml.SELECT_QUIRY_only_retrn_dt("select group_id,group_name from groups where group_pid is null");
            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
            cmb_group.DisplayMember = "group_name";
            cmb_group.ValueMember = "group_id";

            cmb_sgroup.DataSource = dml.SELECT_QUIRY_only_retrn_dt("select group_id,group_name from groups where group_pid is not null");
            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
            cmb_sgroup.DisplayMember = "group_name";
            cmb_sgroup.ValueMember = "group_id";

            load_data(item);
            isnew = false;
            isupdate = false;
        }

        public void button3_Click(object sender, EventArgs e)
        {
            

            btn_combin.Enabled = false;
           // btn_combin.Enabled = true;
            btn_find.Enabled = false;
            btn_opal.Enabled = false;
            btn_cpal.Enabled = false;
            btn_prtbar.Enabled = false;
            txt_prtbar.Enabled = false;
            txt_note.Enabled = true;
            item_temp = textBox1.Text;
            isnew = true;
            isupdate=false;
            btn_Undo.Enabled = true;
            //comboBox1.Items.Clear();
            cmb_unit.SelectedIndex = 0;
            pnl_qtys.Enabled = true;
            cmb_u2.Enabled = true;
            cmb_u2.SelectedIndex = -1;
            txt_u2q.Enabled = true;
            txt_u2p.Enabled = true;
            cmb_u3.Enabled = true;
            cmb_u3.SelectedIndex = -1;
            txt_u3q.Enabled = true;
            txt_u3p.Enabled = true;
            cmb_u4.Enabled = true;
            cmb_u4.SelectedIndex = -1;
            cmb_dunit.SelectedIndex = -1;
            txt_u4q.Enabled = true;
            txt_u4p.Enabled = true;
           // foreach (Control ctr in pnl_qtys.Controls)
           //     ctr.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            cmb_dunit.Enabled = true;
            txt_staticcst.Enabled = true;
            chk_inactv.Enabled = true;
            //textBox3.Enabled = true;
            cmb_type.Enabled = true;
            cmb_type.SelectedIndex = 0;
            txt_price.Enabled = true;
            textBox5.Enabled = true;
            txt_supno.Enabled = true;
            txt_ename.Enabled = true;
            cmb_unit.Enabled = true;
            txt_price2.Enabled = true;

            cmb_tax.Enabled = true;
            cmb_tax.SelectedIndex = 0;
            checkBox1.Enabled = true;
           // cmb_group.SelectedIndex = 0;
            cmb_group.Enabled = true;

          //  cmb_group.SelectedIndex = 0;
            cmb_sgroup.Enabled = true;

            pictureBox1.Enabled = true;

            textBox1.Focus();

            textBox1.Text = "";
            textBox2.Text = "";
          //  textBox3.Text = "0";
            txt_price.Text = "0";
            textBox5.Text = "";
            txt_ename.Text = "";
            txt_curbal.Text = "0.00";
            txt_curcost.Text = "0.00";
            txt_lastp.Text = "0.00";
            txt_staticcst.Text = "0.00";
            txt_u2q.Text = "0.00";
            txt_u2p.Text = "0.00";
           // textBox6.Text = "";
          //  comboBox1.SelectedIndex = -1;
          //  comboBox2.SelectedIndex = -1;
            pictureBox1.Image = Properties.Resources.background_button;



            btn_add.Enabled = false;
            atn_edit.Enabled = false;
            btn_del.Enabled = false;
            btn_save.Enabled = true;
            btn_Exit.Enabled = false;

            if (BL.CLS_Session.autoitemno)
            {
                DataTable dtau = dml.SELECT_QUIRY_only_retrn_dt("SELECT isnull(max(dbo.get_numbers_from_string(item_no)+1),1) from items where len(item_no)<9");
                textBox1.Text = BL.CLS_Session.item_sw + dtau.Rows[0][0].ToString();
                textBox2.Focus();
            }

            SendKeys.Send("{end}");
            /*
            var dv1 = new DataView();
            dv1 = dtunits.DefaultView;
            // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
            //  dv1.RowFilter = "unit_id not in(" + comboBox2.SelectedValue + ")";
            DataTable dtNew = dv1.ToTable();
            // MessageBox.Show(dtNew.Rows[0][1].ToString());
            comboBox2.DataSource = dtNew;

            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
            comboBox2.DisplayMember = "unit_name";
            comboBox2.ValueMember = "unit_id";
             */
           // comboBox2.SelectedIndex = -1;

           // SqlDataAdapter da = new SqlDataAdapter("select group_name,group_id from groups",con2);
           // DataTable dt = new DataTable();
           // da.Fill(dt);

           
           // comboBox1.DataSource = dt;
           // // metroComboBox3.DataSource = dt2;
           // //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
           // // metroComboBox3.DisplayMember = "a";
           //// comboBox1.ValueMember = "group_id";
           // comboBox1.DisplayMember = "group_name";
            isnew = true;
            isupdate = false;
            cmb_u2_Enter( sender,  e);
            cmb_u2_Leave( sender,  e);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            

          

            /*
            var frm2 = new Item_Srch(this);
          //  frm2.Owner = this;
           //-//-//  frm2.MdiParent = Application.OpenForms["MAIN"];
            frm2.ShowDialog();
            textBox7.Focus();
            */
            var frm2 = new Search_All("2", "Sto");
            frm2.ShowDialog();
            try
            {

                load_data(frm2.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                textBox7.Focus();
                //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                /*
               dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
               dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
               dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                */


            }
            catch { }
            //textBox7.Text = Convert.ToString(frm2.dataGridView1.CurrentRow.Cells[0].Value);
            //textBox7.Focus();
            //this.Close();
            
            //Item_Srch its = new Item_Srch();
            ////its.Parent = this;
            //its.Show();
        }
        

        public void button4_Click(object sender, EventArgs e)
        {
            load_data(textBox7.Text);
            textBox7.Focus();
            
            //////////try
            //////////{
            //////////   // comboBox1.DataSource = null;
            //////////   // comboBox2.DataSource = null;

            //////////    if (textBox7.Text != "")
            //////////    {


            //////////        SqlDataAdapter da = new SqlDataAdapter("select * from items where item_no='" + textBox7.Text.Trim() + "' or item_barcode='" + textBox7.Text.Trim() + "'", con2);
            //////////        DataTable dt = new DataTable();
            //////////        da.Fill(dt);
            //////////        if (dt.Rows.Count != 0)
            //////////        {
            //////////            textBox1.Text = dt.Rows[0][0].ToString();
            //////////            textBox2.Text = dt.Rows[0][1].ToString();
            //////////            textBox3.Text = dt.Rows[0][2].ToString();
            //////////            textBox4.Text = dt.Rows[0][3].ToString();
            //////////            textBox5.Text = dt.Rows[0][4].ToString();
            //////////            txt_ename.Text = dt.Rows[0]["item_ename"].ToString();

            //////////            if (dt.Rows[0][9] is DBNull)
            //////////            {
            //////////               // pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\simple-blue-opera-background-button.jpg");
            //////////              // ok   pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\background-button.png");
            //////////                pictureBox1.Image = Properties.Resources.background_button;
                            
            //////////            }
            //////////            else
            //////////            {
            //////////                //////byte[] image = (byte[])dt.Rows[0][9];
            //////////                //////MemoryStream ms = new MemoryStream(image);
            //////////                //////pictureBox1.Image = Image.FromStream(ms);

            //////////                ////////pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + dt.Rows[0][9]);
            //////////                ////////path = dt.Rows[0][9].ToString();
            //////////                if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + dt.Rows[0][9]))
            //////////                {
            //////////                    pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + dt.Rows[0][9]);
                               
            //////////                }
            //////////                else
            //////////                {
            //////////                    pictureBox1.Image = Properties.Resources.background_button;
                                
            //////////                }


            //////////                //path = dt.Rows[0][9].ToString();

            //////////            }
            //////////            if (Convert.ToInt32(dt.Rows[0][10]) == 1)
            //////////            {
            //////////                checkBox1.Checked = true;
            //////////            }
            //////////            else { checkBox1.Checked = false; }

            //////////            /*
            //////////            string tmp = dt.Rows[0][8].ToString();
            //////////            SqlDataAdapter da2 = new SqlDataAdapter("select group_name from groups where group_id=" + Convert.ToInt32(tmp) + "", con2);
            //////////            DataTable dt2 = new DataTable();
            //////////            da2.Fill(dt2);

            //////////            // comboBox1.Items.Clear();
            //////////            //comboBox1.SelectedIndex = -1;
                        
            //////////           // comboBox1.SelectedValue = null;
            //////////           // comboBox1.Items.Clear();


            //////////            comboBox1.Items.Add(dt2.Rows[0][0].ToString());
            //////////            comboBox1.SelectedIndex = comboBox1.FindStringExact(dt2.Rows[0][0].ToString());
            //////////            */
            //////////            cmb_group.SelectedValue = dt.Rows[0][8].ToString();
            //////////            /*
            //////////            string tmp2 = dt.Rows[0][11].ToString();
            //////////          //  SqlDataAdapter da4 = new SqlDataAdapter("select group_name from groups where group_id=" + Convert.ToInt32(tmp) + "", con2);
            //////////            DataTable dt4 = dml.SELECT_QUIRY_only_retrn_dt("select tax_name from taxs where tax_id=" + Convert.ToInt32(tmp2) + "");
            //////////          //  da2.Fill(dt2);

            //////////            // comboBox1.Items.Clear();
            //////////            //comboBox1.SelectedIndex = -1;

            //////////            // comboBox1.SelectedValue = null;
            //////////            // comboBox1.Items.Clear();


            //////////            comboBox3.Items.Add(dt4.Rows[0][0].ToString());
            //////////            comboBox3.SelectedIndex = comboBox3.FindStringExact(dt4.Rows[0][0].ToString());
            //////////            */
            //////////            cmb_tax.SelectedValue = dt.Rows[0][11].ToString();
            //////////            /*
            //////////            SqlDataAdapter da3 = new SqlDataAdapter("select unit_id,unit_name from units where unit_id=" + dt.Rows[0][5].ToString() + "", con2);
            //////////            DataTable dt3 = new DataTable();
            //////////            da3.Fill(dt3);

            //////////            comboBox2.DataSource = dt3;
            //////////            comboBox2.DisplayMember = "unit_name";
            //////////            comboBox2.ValueMember = "unit_id";
            //////////            */
            //////////            cmb_unit.SelectedValue = dt.Rows[0][5].ToString();

            //////////            cmb_u2.SelectedValue = dt.Rows[0]["unit2"].ToString();
            //////////            cmb_u3.SelectedValue = dt.Rows[0]["unit3"].ToString();
            //////////            cmb_u4.SelectedValue = dt.Rows[0]["unit4"].ToString();

            //////////            txt_u2q.Text = dt.Rows[0]["uq2"].ToString();
            //////////            txt_u3q.Text = dt.Rows[0]["uq3"].ToString();
            //////////            txt_u4q.Text = dt.Rows[0]["uq4"].ToString();

            //////////            txt_u2p.Text = dt.Rows[0]["unit2p"].ToString();
            //////////            txt_u3p.Text = dt.Rows[0]["unit3p"].ToString();
            //////////            txt_u4p.Text = dt.Rows[0]["unit4p"].ToString();
            //////////           // comboBox2.Items.Add(dt3.Rows[0][0].ToString());
            //////////          //  comboBox2.SelectedIndex = comboBox2.FindStringExact(dt3.Rows[0][1].ToString());
            //////////           // comboBox2.SelectedValue = dt.Rows[0][5].ToString();
            //////////            // comboBox1.Items.Clear();
            //////////            //  comboBox1.Items.Add(dt.Rows[0][8].ToString());
            //////////            //  comboBox1.SelectedIndex = comboBox1.FindStringExact(dt.Rows[0][8].ToString());
            //////////            SqlDataAdapter da4s = new SqlDataAdapter("select su_no,su_name from suppliers where su_no='" + dt.Rows[0]["supno"].ToString() + "' and su_brno='"+BL.CLS_Session.brno+"'", con2);
            //////////            DataTable dt4s = new DataTable();
            //////////            da4s.Fill(dt4s);
            //////////            if (dt4s.Rows.Count >0)
            //////////            {
            //////////                txt_supno.Text = dt4s.Rows[0][0].ToString();
            //////////                txt_supname.Text = dt4s.Rows[0][1].ToString();
            //////////            }
            //////////            else
            //////////            {
            //////////                txt_supname.Text = "";
            //////////                txt_supno.Text = "";
            //////////            }
            //////////            textBox7.Text = "";
            //////////            atn_edit.Enabled = true;
            //////////            btn_bar.Enabled = true;
            //////////            btn_statmnt.Enabled = true;
            //////////            btn_del.Enabled = true;
            //////////        }
            //////////        else
            //////////        {
            //////////            MessageBox.Show("لايوجد صنف بهذا الرقم", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //////////            textBox7.Text = "";
            //////////        }
            //////////    }
            //////////    else
            //////////    {
            //////////        MessageBox.Show("يرجى ادخال رقم صنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //////////    }

            //////////}
            //////////catch (Exception ex)
            //////////{
            //////////    MessageBox.Show(ex.Message);
            //////////}
        }

        private void load_data(string key)
        {
            if (key != "")
            {
               // DataTable dti;
                try
                {
                    //  comboBox1.DataSource = null;
                    //  comboBox2.DataSource = null;
                    SqlDataAdapter da,da2;
                   // string quiry = "";
                    if (label23.Text.Equals("بالرقم"))
                    {
                        //da = new SqlDataAdapter("select aa.*,cast(item_curcost AS decimal(10,2)) as cst,[dbo].[get_lastprice_for_item] ('" + BL.CLS_Session.brno + "','" + key.Trim() + "',0) lastp from items aa where (aa.item_no='" + key.Trim() + "' or aa.item_barcode='" + key.Trim() + "')", con2);
                       // da = new SqlDataAdapter("select aa.*,cast(item_curcost AS decimal(10,2)) as cst from items aa where (aa.item_no='" + key.Trim() + "' or aa.item_barcode='" + key.Trim() + "')", con2);
                        da = new SqlDataAdapter("select aa.*," + (BL.CLS_Session.up_sal_icost? " cast(item_curcost AS decimal(10,2)) " : " 0.00 " ) + " as cst,[dbo].[get_item_bal] ('" + key.Trim() + "') ttqty from items aa where aa.item_no='" + key.Trim() + "'", con2);                
                       // da = new SqlDataAdapter("select aa.*,cast(item_curcost AS decimal(10,2)) as cst,0.00 ttqty from items aa where aa.item_no='" + key.Trim() + "'", con2);                
                        da2 = new SqlDataAdapter("exec [dbo].[sp_get_lastprice_for_item] @comp1='" + BL.CLS_Session.brno + "',@lbarcode1='" + key.Trim() + "',@glact1=0 ", con2);
                      
                        //new Thread(() =>
                        //{
                        //    dti = new DataTable();
                        //    using (SqlCommand cmd = new SqlCommand("[dbo].[get_item_bal] ('" + key.Trim() + "')", BL.DAML.con))
                        //    {
                               
                        //      //  cmd.CommandType = CommandType.StoredProcedure;
                        //       // cmd.Connection = con2;
                        //       // cmd.Parameters.AddWithValue("@items_tran_tb", dti);
                        //        // con.Open();
                        //        if (BL.DAML.con.State == ConnectionState.Closed)
                        //            BL.DAML.con.Open();
                        //        dti.Load(cmd.ExecuteReader());
                        //        BL.DAML.con.Close();
                        //        txt_curbal.Text=dti.Rows[0][0].ToString();
                        //    }
                        //}).Start();

                    }
                    else
                    {
                        da = new SqlDataAdapter("select aa.*," + (BL.CLS_Session.up_sal_icost ? " cast(item_curcost AS decimal(10,2)) " : " 0.00 ") + " as cst,[dbo].[get_item_bal] (bb.item_no) ttqty from items aa,items_bc bb where aa.item_no=bb.item_no and bb.barcode='" + key.Trim() + "'", con2);
                      //  da = new SqlDataAdapter("select aa.*,cast(item_curcost AS decimal(10,2)) as cst,0.00 ttqty from items aa,items_bc bb where aa.item_no=bb.item_no and bb.barcode='" + key.Trim() + "'", con2);
                        da2 = new SqlDataAdapter("exec [dbo].[sp_get_lastprice_for_item] @comp1='" + BL.CLS_Session.brno + "',@lbarcode1='" + key.Trim() + "',@glact1=1 ", con2);

                        //new Thread(() =>
                        //{
                        //    dti = new DataTable();
                        //    using (SqlCommand cmd = new SqlCommand("[dbo].[get_item_bal] ('" + key.Trim() + "')", BL.DAML.con))
                        //    {

                        //      //  cmd.CommandType = CommandType.StoredProcedure;
                        //       // cmd.Connection = con2;
                        //        // cmd.Parameters.AddWithValue("@items_tran_tb", dti);
                        //        // con.Open();
                        //        if (BL.DAML.con.State == ConnectionState.Closed)
                        //            BL.DAML.con.Open();
                        //        dti.Load(cmd.ExecuteReader());
                        //        BL.DAML.con.Close();
                        //        txt_curbal.Text = dti.Rows[0][0].ToString();
                        //    }
                        //}).Start();
                    }
                  

                  //  SqlDataAdapter da = new SqlDataAdapter("select aa.*,bb.lprice1 from items aa,brprices bb where aa.item_no=bb.itemno and bb.branch='" + BL.CLS_Session.brno + "' and (aa.item_no='" + key.Trim() + "' or aa.item_barcode='" + key.Trim() + "')", con2);
                 //   SqlDataAdapter da = new SqlDataAdapter("select aa.*,cast(item_curcost AS decimal(10,2)) as cst from items aa where (aa.item_no='" + key.Trim() + "' or aa.item_barcode='" + key.Trim() + "')", con2);
                   
                    DataTable dt = new DataTable();
                    DataTable dt2 = new DataTable();
                    da.Fill(dt);
                    da2.Fill(dt2);
                    if (dt.Rows.Count != 0)
                    {
                        textBox1.Text = dt.Rows[0][0].ToString();
                        textBox2.Text = dt.Rows[0][1].ToString();
                       // textBox3.Text = dt.Rows[0][2].ToString();
                        txt_price.Text = dt.Rows[0][3].ToString();
                        textBox5.Text = dt.Rows[0][4].ToString();
                        txt_note.Text = dt.Rows[0]["note"].ToString();

                        txt_ename.Text = dt.Rows[0]["item_ename"].ToString();
                        txt_price2.Text = dt.Rows[0]["price2"].ToString();
                       // txt_curbal.Text = dt.Rows[0]["item_cbalance"].ToString();
                        txt_curbal.Text =Convert.ToBoolean(dt.Rows[0]["item_cost"])? dt.Rows[0]["ttqty"].ToString(): "0.00";
                        txt_curcost.Text =BL.CLS_Session.up_sal_icost ? Convert.ToBoolean(dt.Rows[0]["item_cost"])? dt.Rows[0]["cst"].ToString(): "0.00" : "0.00";
                       // txt_lastp.Text = dt.Rows[0]["lastp"].ToString();
                        txt_lastp.Text =BL.CLS_Session.up_sal_icost ? Convert.ToBoolean(dt.Rows[0]["item_cost"])? dt2.Rows[0][0].ToString():"0.00" : "0.00";
                        cmb_type.SelectedIndex = dt.Rows[0]["item_cost"].ToString().Equals("1") ? 0 :dt.Rows[0]["item_cost"].ToString().Equals("2") ?2: 1;
                        if (dt.Rows[0]["item_cost"].ToString().Equals("2"))
                            btn_combin.Enabled = true;
                        else
                            btn_combin.Enabled = false;

                        strimgname = dt.Rows[0][9].ToString();

                        if (dt.Rows[0][9] is DBNull)
                        {
                            // pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\simple-blue-opera-background-button.jpg");
                            // ok   pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\background-button.png");
                            pictureBox1.Image = Properties.Resources.background_button;

                        }
                        else
                        {
                            //////byte[] image = (byte[])dt.Rows[0][9];
                            //////MemoryStream ms = new MemoryStream(image);
                            //////pictureBox1.Image = Image.FromStream(ms);

                            ////////pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + dt.Rows[0][9]);
                            ////////path = dt.Rows[0][9].ToString();
                            if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + dt.Rows[0][9]))
                            {
                                pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + dt.Rows[0][9]);

                            }
                            else
                            {
                                pictureBox1.Image = Properties.Resources.background_button;

                            }


                            //path = dt.Rows[0][9].ToString();

                        }
                        if (Convert.ToInt32(dt.Rows[0][10]) == 1)
                        {
                            checkBox1.Checked = true;
                        }
                        else { checkBox1.Checked = false; }

                        if (Convert.ToInt32(dt.Rows[0]["inactive"]) == 1)
                        {
                            chk_inactv.Checked = true;
                        }
                        else { chk_inactv.Checked = false; }

                        /*
                        string tmp = dt.Rows[0][8].ToString();
                        SqlDataAdapter da2 = new SqlDataAdapter("select group_name from groups where group_id=" + Convert.ToInt32(tmp) + "", con2);
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);

                        // comboBox1.Items.Clear();
                        //comboBox1.SelectedIndex = -1;

                        // comboBox1.SelectedValue = null;
                        // comboBox1.Items.Clear();


                        comboBox1.Items.Add(dt2.Rows[0][0].ToString());
                        comboBox1.SelectedIndex = comboBox1.FindStringExact(dt2.Rows[0][0].ToString());
                        */
                       
                        cmb_unit.SelectedValue = dt.Rows[0][5].ToString();
                        cmb_group.SelectedValue = dt.Rows[0][8].ToString();
                        cmb_sgroup.SelectedValue = dt.Rows[0]["sgroup"].ToString();
                        cmb_dunit.SelectedValue = dt.Rows[0]["dunit"].ToString();
                        /*
                         * 
                        string tmp2 = dt.Rows[0][11].ToString();
                        //  SqlDataAdapter da4 = new SqlDataAdapter("select group_name from groups where group_id=" + Convert.ToInt32(tmp) + "", con2);
                        DataTable dt4 = dml.SELECT_QUIRY_only_retrn_dt("select tax_name from taxs where tax_id=" + Convert.ToInt32(tmp2) + "");
                        //  da2.Fill(dt2);

                        // comboBox1.Items.Clear();
                        //comboBox1.SelectedIndex = -1;

                        // comboBox1.SelectedValue = null;
                        // comboBox1.Items.Clear();


                        comboBox3.Items.Add(dt4.Rows[0][0].ToString());
                        comboBox3.SelectedIndex = comboBox3.FindStringExact(dt4.Rows[0][0].ToString());
                        */
                        cmb_tax.SelectedValue = dt.Rows[0][11].ToString();
                        /*
                        SqlDataAdapter da3 = new SqlDataAdapter("select unit_id,unit_name from units where unit_id=" + dt.Rows[0][5].ToString() + "", con2);
                        DataTable dt3 = new DataTable();
                        da3.Fill(dt3);

                        comboBox2.DataSource = dt3;
                        comboBox2.DisplayMember = "unit_name";
                        comboBox2.ValueMember = "unit_id";

                        comboBox2.SelectedValue = dt3.Rows[0][0].ToString();
                        */
                       

                        cmb_u2.SelectedValue = dt.Rows[0]["unit2"].ToString();
                        cmb_u3.SelectedValue = dt.Rows[0]["unit3"].ToString();
                        cmb_u4.SelectedValue = dt.Rows[0]["unit4"].ToString();

                        txt_u2q.Text = dt.Rows[0]["uq2"].ToString();
                        txt_u3q.Text = dt.Rows[0]["uq3"].ToString();
                        txt_u4q.Text = dt.Rows[0]["uq4"].ToString();

                        txt_u2p.Text = dt.Rows[0]["unit2p"].ToString();
                        txt_u3p.Text = dt.Rows[0]["unit3p"].ToString();
                        txt_u4p.Text = dt.Rows[0]["unit4p"].ToString();

                        txt_price2.Text = dt.Rows[0]["price2"].ToString();
                        txt_staticcst.Text =BL.CLS_Session.up_sal_icost ? dt.Rows[0]["static_cost"].ToString(): "0.00";
                       // txt_curbal.Text = dt.Rows[0]["item_cbalance"].ToString();
                       // txt_curbal.Text = dt.Rows[0]["ttqty"].ToString();
                       // txt_curcost.Text = dt.Rows[0]["cst"].ToString();
                        cmb_type.SelectedIndex = dt.Rows[0]["item_cost"].ToString().Equals("1") ? 0 :dt.Rows[0]["item_cost"].ToString().Equals("2") ? 2:1;
                        if (dt.Rows[0]["item_cost"].ToString().Equals("2"))
                            btn_combin.Enabled = true;
                        else
                            btn_combin.Enabled = false;
                        // comboBox2.Items.Add(dt3.Rows[0][0].ToString());
                        //  comboBox2.SelectedIndex = comboBox2.FindStringExact(dt3.Rows[0][1].ToString());
                        // comboBox2.SelectedValue = dt.Rows[0][5].ToString();
                        // comboBox1.Items.Clear();
                        //  comboBox1.Items.Add(dt.Rows[0][8].ToString());
                        //  comboBox1.SelectedIndex = comboBox1.FindStringExact(dt.Rows[0][8].ToString());
                        SqlDataAdapter da4s = new SqlDataAdapter("select su_no,su_name from suppliers where su_no='" + dt.Rows[0]["supno"].ToString() + "' and su_brno='" + BL.CLS_Session.brno + "'", con2);
                        DataTable dt4s = new DataTable();
                        da4s.Fill(dt4s);
                        if (dt4s.Rows.Count > 0)
                        {
                            txt_supno.Text = dt4s.Rows[0][0].ToString();
                            txt_supname.Text = dt4s.Rows[0][1].ToString();
                        }
                        else
                        {
                            txt_supname.Text = "";
                            txt_supno.Text = "";
                        }
                        textBox7.Text = "";
                        atn_edit.Enabled = true;
                        btn_bar.Enabled = true;
                        btn_statmnt.Enabled = true;
                        btn_del.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(BL.CLS_Session.lang.Equals("E")?"No Item Like this no.":"لايوجد صنف بهذا الرقم", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox7.Text = "";
                    }

                    txt_price_Leave(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                //  MessageBox.Show("يرجى ادخال رقم المورد", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click((object)sender, (EventArgs)e);
            }
            if (e.KeyCode == Keys.F8)
            {
                var frm2 = new Search_All("2", "Sto");
                frm2.ShowDialog();
                try
                {

                    load_data(frm2.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    textBox7.Focus();
                }
                catch { }
            }
        }

        public void button5_Click(object sender, EventArgs e)
        {
            //dtcount = dml.SELECT_QUIRY_only_retrn_dt("getTotalCount");
            //if (Convert.ToInt32(dtcount.Rows[0][0]) == 0)
            //{
            //}
            //else
            //{

            //}

            if (cmb_type.SelectedIndex == 2)
                btn_combin.Enabled = true;
            else
                btn_combin.Enabled = false;

            btn_prtbar.Enabled = false;
            txt_prtbar.Enabled = false;
            btn_find.Enabled = false;
            btn_opal.Enabled = false;
            btn_cpal.Enabled = false;

            isnew = false;
            isupdate=true;
            btn_Exit.Enabled = false;
            btn_Undo.Enabled = true;
           // bc = textBox5.Text;
           // string ss = cmb_group.Text.ToString();
           // string ss2 = cmb_unit.Text.ToString();
           // string ss3 = cmb_tax.Text.ToString();
            txt_price2.Enabled = true;
            textBox1.Enabled = false;
            textBox2.Enabled = true;
            cmb_dunit.Enabled = true;
            txt_staticcst.Enabled = true;
            chk_inactv.Enabled = true;
           // textBox3.Enabled = true;
            txt_price.Enabled = true;
            txt_supno.Enabled = true;
            textBox5.Enabled = false;
            txt_ename.Enabled = true;
           // comboBox2.Enabled = true;
            btn_add.Enabled = false;
            atn_edit.Enabled = false;
            btn_del.Enabled = false;
            btn_save.Enabled = false;
            btn_savchng.Enabled = true;
            checkBox1.Enabled = true;
            cmb_group.Enabled = true;
            cmb_sgroup.Enabled = true;
            cmb_tax.Enabled = true;
            pictureBox1.Enabled = true;
            pnl_qtys.Enabled = true;
            if (!string.IsNullOrEmpty(cmb_u2.Text.ToString()))
            {
                cmb_u2.Enabled = false;
                txt_u2q.Enabled = false;
            }
            else
            {
                cmb_u2.Enabled = true;
                txt_u2q.Enabled = true;
            }

            if (!string.IsNullOrEmpty(cmb_u3.Text.ToString()))
            {
                cmb_u3.Enabled = false;
                txt_u3q.Enabled = false;
            }
            else
            {
                cmb_u3.Enabled = true;
                txt_u3q.Enabled = true;
            }

            if (!string.IsNullOrEmpty(cmb_u4.Text.ToString()))
            {
                cmb_u4.Enabled = false;
                txt_u4q.Enabled = false;
            }
            else
            {
                cmb_u4.Enabled = true;
                txt_u4q.Enabled = true;
            }

            txt_note.Enabled = true;
            /*
            SqlDataAdapter da = new SqlDataAdapter("select group_id,group_name from groups", con2);
            DataTable dt = new DataTable();
            da.Fill(dt);


            cmb_group.DataSource = dt;
            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
            cmb_group.DisplayMember = "group_name";
            cmb_group.ValueMember = "group_id";

           // comboBox1.Items.Add(dt.Rows[0][0].ToString());
            cmb_group.SelectedIndex = cmb_group.FindStringExact(ss);

            SqlDataAdapter da2 = new SqlDataAdapter("select unit_id,unit_name from units", con2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);


            cmb_unit.DataSource = dt2;
            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
            cmb_unit.DisplayMember = "unit_name";
            cmb_unit.ValueMember = "unit_id";

            // comboBox1.Items.Add(dt.Rows[0][0].ToString());
           // cmb_unit.SelectedIndex = cmb_unit.FindStringExact(ss2);

          //  SqlDataAdapter da2 = new SqlDataAdapter("select unit_id,unit_name from units", con2);
          //  DataTable dt2 = new DataTable();
          //  da2.Fill(dt2);


            cmb_tax.DataSource = dml.SELECT_QUIRY_only_retrn_dt("select tax_id,tax_name from taxs");
            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
            cmb_tax.DisplayMember = "tax_name";
            cmb_tax.ValueMember = "tax_id";

            // comboBox1.Items.Add(dt.Rows[0][0].ToString());
            cmb_tax.SelectedIndex = cmb_tax.FindStringExact(ss3);
            */
            if (BL.CLS_Session.up_ch_itmpr == false)
            {
               txt_price.ReadOnly = true;
               btn_brprice.Enabled = false;
            }

            dtcount = dml.SELECT_QUIRY_only_retrn_dt("getTotalCount @itemno = '"+textBox1.Text+"'");
            if (Convert.ToInt32(dtcount.Rows[0][0]) == 0 || BL.CLS_Session.multi_bc)
            {
                cmb_unit.Enabled = true;
                cmb_u2.Enabled = true;
                txt_u2q.Enabled = true;
                txt_u2p.Enabled = true;
                cmb_u3.Enabled = true;
                txt_u3q.Enabled = true;
                txt_u3p.Enabled = true;
                cmb_u4.Enabled = true;
                txt_u4q.Enabled = true;
                txt_u4p.Enabled = true;
                cmb_type.Enabled = true;
                textBox5.Enabled = true;
               // cmb_type.Enabled = true;
                ismoved = false;
            }
            else
            {
                ismoved = true;
                //cmb_unit.Enabled = false;
                //cmb_u2.Enabled = false;
                //txt_u2q.Enabled = true;
                //txt_u2p.Enabled = true;
                //cmb_u3.Enabled = true;
                //txt_u3q.Enabled = true;
                //txt_u3p.Enabled = true;
                //cmb_u4.Enabled = true;
                //txt_u4q.Enabled = true;
                //txt_u4p.Enabled = true;
            }
            isnew = false;
            isupdate = true;
            btn_save.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(strimgname);

            ////if (string.IsNullOrEmpty(txt_supno.Text))
            ////{
            ////    MessageBox.Show("يرجى ادخال رقم المورد", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////    txt_supno.Focus();
            ////    return;
            ////}
            if (textBox1.Text != "" && textBox2.Text != "" && txt_price.Text != "" && textBox5.Text != "" && cmb_unit.Text != "" && cmb_u2.Text != "")
            {
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] byteImage = ms.ToArray();

                if (con2.State == ConnectionState.Closed)
                con2.Open();
                // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
                using (SqlCommand cmd1 = new SqlCommand("update items set  item_name=@a2, item_cost=@a3, item_price=@a4, item_barcode=@a5,item_unit=@a6,item_group=@a7,item_image=@a8,item_req=@a9,item_tax=@a10,item_ename=@a20,supno=@a21,unit2=@u2,uq2=@uq2,unit2p=@u2p,unit3=@u3,uq3=@uq3,unit3p=@u3p,unit4=@u4,uq4=@uq4,unit4p=@u4p,note=@not,last_updt=@lu,price2=@p2,sgroup=@sg,inactive=@ia,static_cost=@stc,dunit=@dunit where item_no=@a1", con2))
                {


                    cmd1.Parameters.AddWithValue("@a1", textBox1.Text.Trim());
                    cmd1.Parameters.AddWithValue("@a2", textBox2.Text.Trim());
                    cmd1.Parameters.AddWithValue("@a3", (cmb_type.SelectedIndex == 0 ? 1 : cmb_type.SelectedIndex == 1 ? 0 : 2));
                    cmd1.Parameters.AddWithValue("@a4", Convert.ToDouble(txt_price.Text));
                    cmd1.Parameters.AddWithValue("@a5", textBox5.Text.Trim());
                    cmd1.Parameters.AddWithValue("@a6", cmb_unit.SelectedValue.ToString());
                    cmd1.Parameters.AddWithValue("@a7", cmb_group.SelectedValue.ToString());
                   // cmd1.Parameters.AddWithValue("@a8", byteImage);
                    cmd1.Parameters.AddWithValue("@a8", strimgname);
                    if (checkBox1.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a9", 1); }
                    else { cmd1.Parameters.AddWithValue("@a9", 0); }
                    cmd1.Parameters.AddWithValue("@a10", cmb_tax.SelectedValue.ToString());
                    cmd1.Parameters.AddWithValue("@a20", txt_ename.Text);
                    cmd1.Parameters.AddWithValue("@a21", txt_supno.Text);

                    cmd1.Parameters.AddWithValue("@u2", Convert.ToDouble(txt_u2q.Text) == 0 ? 0 : cmb_u2.SelectedIndex == -1 ? 0 : cmb_u2.SelectedValue);
                    cmd1.Parameters.AddWithValue("@uq2", Convert.ToDouble(txt_u2q.Text));
                    cmd1.Parameters.AddWithValue("@u2p", Convert.ToDouble(txt_u2p.Text));

                    cmd1.Parameters.AddWithValue("@u3", Convert.ToDouble(txt_u3q.Text) == 0? 0 : cmb_u3.SelectedIndex == -1 ? 0 : cmb_u3.SelectedValue);
                    cmd1.Parameters.AddWithValue("@uq3", Convert.ToDouble(txt_u3q.Text));
                    cmd1.Parameters.AddWithValue("@u3p", Convert.ToDouble(txt_u3p.Text));

                    cmd1.Parameters.AddWithValue("@u4", Convert.ToDouble(txt_u4q.Text) == 0 ? 0 : cmb_u4.SelectedIndex == -1 ? 0 : cmb_u4.SelectedValue);
                    cmd1.Parameters.AddWithValue("@uq4", Convert.ToDouble(txt_u4q.Text));
                    cmd1.Parameters.AddWithValue("@u4p", Convert.ToDouble(txt_u4p.Text));
                    cmd1.Parameters.AddWithValue("@not", txt_note.Text);
                    cmd1.Parameters.AddWithValue("@lu", DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false)));
                    cmd1.Parameters.AddWithValue("@p2", txt_price2.Text);
                    cmd1.Parameters.AddWithValue("@sg", (cmb_sgroup.SelectedIndex==-1 ? "" : cmb_sgroup.SelectedValue.ToString()));
                    cmd1.Parameters.AddWithValue("@ia", chk_inactv.Checked? 1 : 0 );
                    cmd1.Parameters.AddWithValue("@stc", txt_staticcst.Text);
                    cmd1.Parameters.AddWithValue("@dunit", (cmb_dunit.SelectedIndex == -1 ? cmb_unit.SelectedValue : cmb_dunit.SelectedValue));
                    cmd1.ExecuteNonQuery();
                    con2.Close();
                   // MessageBox.Show("modifed success", "ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    cmb_dunit.Enabled = false;
                    txt_staticcst.Enabled = false;
                    chk_inactv.Enabled = false;
                   // textBox3.Enabled = false;
                    txt_price.Enabled = false;
                    textBox5.Enabled = false;
                    txt_supno.Enabled = false;
                    txt_ename.Enabled = false;
                    cmb_unit.Enabled = false;
                    cmb_tax.Enabled = false;
                    checkBox1.Enabled = false;
                    btn_save.Enabled = false;
                    btn_add.Enabled = true;
                    btn_del.Enabled = true;
                    btn_bar.Enabled = true;
                    btn_savchng.Enabled = false;
                    atn_edit.Enabled = true;
                    cmb_group.Enabled = false;
                    cmb_sgroup.Enabled = false;
                    pictureBox1.Enabled = false;
                    pnl_qtys.Enabled = false;
                    txt_price2.Enabled = false;
                    txt_note.Enabled = false;
                    btn_Undo.Enabled = false;
                    btn_Exit.Enabled = true;
                    btn_prtbar.Enabled = true;
                    txt_prtbar.Enabled = true;
                    btn_find.Enabled = true;
                    btn_opal.Enabled = true;
                    btn_cpal.Enabled = true;
                    cmb_type.Enabled = false;

                    if (cmb_type.SelectedIndex == 2)
                        btn_combin.Enabled = true;
                    else
                        btn_combin.Enabled = false;

                    using (SqlCommand cmd2 = new SqlCommand("update items_bc set price=@a22,barcode=@a55,pack=@a44,min_price=@a88 where br_no=@br and sl_no=@a66 and item_no=@a33 and pkorder=1", con2))
                    {


                      //  cmd2.Parameters.AddWithValue("@a11", textBox5.Text);
                        cmd2.Parameters.AddWithValue("@a22", txt_price.Text);
                        cmd2.Parameters.AddWithValue("@a33", textBox1.Text.Trim());
                        cmd2.Parameters.AddWithValue("@a44", Convert.ToInt32(cmb_unit.SelectedValue));
                        cmd2.Parameters.AddWithValue("@a55", textBox5.Text.Trim());
                        cmd2.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                        cmd2.Parameters.AddWithValue("@a66",BL.CLS_Session.sl_prices);
                        cmd2.Parameters.AddWithValue("@a88", txt_price2.Text);
                        // cmd1.Parameters.AddWithValue("@a6", textBox6.Text);
                        //  cmd1.Parameters.AddWithValue("@a7", comboBox1.SelectedValue.ToString());
                        // cmd1.Parameters.AddWithValue("@a8", byteImage);
                        // cmd1.Parameters.AddWithValue("@a8", path);
                        // if (checkBox1.Checked == true)
                        // { cmd1.Parameters.AddWithValue("@a9", 1); }
                        //  else { cmd1.Parameters.AddWithValue("@a9", 0); }
                        if (con2.State == ConnectionState.Closed)
                        con2.Open();
                        cmd2.ExecuteNonQuery();
                        con2.Close();
                    }

                   
                    //////using (SqlCommand cmd3 = new SqlCommand("update brprices set lprice1=@a111 where itemno=@a333 and branch=@a444", con2))
                    //////{


                    //////    cmd3.Parameters.AddWithValue("@a111", Convert.ToDouble(txt_price2.Text));
                    //////  //  cmd3.Parameters.AddWithValue("@a22", textBox4.Text);
                    //////    cmd3.Parameters.AddWithValue("@a333", textBox1.Text);
                    //////    cmd3.Parameters.AddWithValue("@a444", BL.CLS_Session.brno);
                     
                    //////    con2.Open();
                    //////    cmd3.ExecuteNonQuery();
                    //////    con2.Close();
                    //////}
                     
                  //  comboBox1.Items.Clear();
                }
            }
            else
            {
                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "please fill all requird feilds" : "يرجى تعبئة البيانات كاملة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(bprinter);
           
            // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            // string str = "SELECT hdr.ref, hdr.total, hdr.count, hdr.date, dtl.barcod, dtl.name, dtl.price, dtl.unit FROM hdr INNER JOIN dtl ON hdr.ref = dtl.ref WHERE (hdr.ref = (SELECT MAX(ref) AS Expr1 FROM  hdr AS hdr_1))";
            // SqlDataAdapter dacr = new SqlDataAdapter(str , con);
            if (rd_gen.Checked)
            {
                if (!string.IsNullOrEmpty(txt_prtbar.Text) && !string.IsNullOrEmpty(textBox1.Text))
                {
                   // if (txt_prtbar.Text == "")
                    //{
                        int xxx = Convert.ToInt32(string.IsNullOrEmpty(txt_prtbar.Text)? "1" : txt_prtbar.Text);
                        SqlDataAdapter da2 = new SqlDataAdapter("select * from items where item_no ='" + textBox1.Text + "'", con2);
                        //"item_no like '%" + textBox1.Text + "%' or item_name like '%"+textBox1.Text+"%'"
                        // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from hdr where ref=(select max(ref) from hdr)", con);
                        // DataSet1 ds = new DataSet1();
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);
                        // dacr.Fill(ds, "dtl");
                        // dacr1.Fill(ds, "hdr");

                        if (File.Exists(Directory.GetCurrentDirectory() + @"\reports\BarcodeReports1.rpt"))
                        {
                            CrystalDecisions.CrystalReports.Engine.ReportDocument report = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                            string filePath = Directory.GetCurrentDirectory() + @"\reports\BarcodeReports1.rpt";
                            report.Load(filePath);

                            report.SetDataSource(dt2);
                            report.SetParameterValue("cmp_name", POS.BL.CLS_Session.cmp_name);
                            // report.ReportDefinition.Areas.

                            // crystalReportViewer1.ReportSource = report;

                            // crystalReportViewer1.Refresh();

                            report.PrintOptions.PrinterName = bprinter;




                            //int xxx = Convert.ToInt32(textBox8.Text);
                            //for (int i = 1; i <= xxx; i++)
                            //{
                            // report.PrintToPrinter(1, true, 1, 1);
                           // report.PrintToPrinter(1, false, 0, 0);
                            report.PrintToPrinter(xxx, true, 1, 1);
                            report.Close();
                            // report.PrintToPrinter(1, false, 1, 1);
                            txt_prtbar.Text = "";
                            //}
                        }
                        else
                        {
                            
                            Sto.rpt.BarcodeReports report = new Sto.rpt.BarcodeReports();
                            report.SetDataSource(dt2);
                            report.SetParameterValue("cmp_name", POS.BL.CLS_Session.cmp_name);
                            // report.ReportDefinition.Areas.

                            // crystalReportViewer1.ReportSource = report;

                            // crystalReportViewer1.Refresh();

                            report.PrintOptions.PrinterName = bprinter;




                            //int xxx = Convert.ToInt32(textBox8.Text);
                            //for (int i = 1; i <= xxx; i++)
                            //{
                            // report.PrintToPrinter(1, true, 1, 1);
                          //  report.PrintToPrinter(1, false, 0, 0);
                            report.PrintToPrinter(xxx, true, 1, 1);
                            report.Close();
                            // report.PrintToPrinter(1, false, 1, 1);
                            txt_prtbar.Text = "";
                             
                        }

                    //}
                    //else
                    //{

                        //SqlDataAdapter da2 = new SqlDataAdapter("select * from items where item_no ='" + textBox1.Text + "'", con2);

                        //DataTable dt2 = new DataTable();
                        //da2.Fill(dt2);
                        //// dacr.Fill(ds, "dtl");
                        //// dacr1.Fill(ds, "hdr");


                        //Sto.rpt.BarcodeReports report = new Sto.rpt.BarcodeReports();
                        //report.SetDataSource(dt2);
                        //report.SetParameterValue("cmp_name", POS.BL.CLS_Session.cmp_name);
                        //// report.ReportDefinition.Areas.

                        //// crystalReportViewer1.ReportSource = report;

                        //// crystalReportViewer1.Refresh();

                        //report.PrintOptions.PrinterName = bprinter;
                        //int xxx = Convert.ToInt32(txt_prtbar.Text);
                        //// for (int i = 1; i <= xxx; i++)
                        //// {
                        //// report.PrintToPrinter(1, true, 1, 1);
                        //report.PrintToPrinter(xxx, true, 1, 1);
                        //// 
                        //// report.PrintToPrinter(1, false, 1, 1);

                        //// }
                        //report.Close();
                        //txt_prtbar.Text = "";
                    //}
                }
                else
                {
                    Sto.Print_Barcode_FR bp = new Sto.Print_Barcode_FR(textBox1.Text);
                    // bp.MdiParent = this;
                    bp.ShowDialog();

                  //  MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "please select item" : "يرجى اختار صنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    SqlDataAdapter da3 = new SqlDataAdapter("select * from items where item_no ='" + textBox1.Text + "'", con2);

                    DataTable dt3 = new DataTable();
                    da3.Fill(dt3);

                    Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();

                   // LocalReport report = new LocalReport();
                  //  report.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Barcode.rdlc";
                    rf.reportViewer1.LocalReport.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Barcode.rdlc";
                   // report.DataSources.Add(new ReportDataSource("dts", dt3));
                    rf.reportViewer1.LocalReport.DataSources.Clear();
                    rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dts", dt3));
                    // rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("sdt", dt));

                    ReportParameter[] parameters = new ReportParameter[2];
                    parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                    //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                    //  parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());
                    parameters[1] = new ReportParameter("br_name", BL.CLS_Session.brname);

                   

                    //report.SetParameters(parameters);
                    rf.reportViewer1.LocalReport.SetParameters(parameters);

                  //  BL.PrintReport.pagetype = "Sal";

                   // BL.PrintReport.PrintToPrinter(report);
                    rf.reportViewer1.RefreshReport();
                    rf.MdiParent = ParentForm;
                    rf.Show();
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
           //  InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("EN"));
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("EN"));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox5.Text = textBox1.Text;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "No Item selected" : "لا يوجد صنف لحذفة يرجى اختار صنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlDataAdapter da10 = new SqlDataAdapter("select count(*) from sales_dtl where itemno ='" + textBox1.Text + "'", con2);
                SqlDataAdapter da11 = new SqlDataAdapter("select count(*) from pu_dtl where itemno ='" + textBox1.Text + "'", con2);
                SqlDataAdapter da12 = new SqlDataAdapter("select count(*) from sto_dtl where itemno ='" + textBox1.Text + "'", con2);
                SqlDataAdapter da13 = new SqlDataAdapter("select count(*) from salofr_dtl where itemno ='" + textBox1.Text + "'", con2);
                SqlDataAdapter da14 = new SqlDataAdapter("select isnull(sum(qty+openbal),0) from whbins where item_no ='" + textBox1.Text + "'", con2);
                DataTable dt10 = new DataTable();
                DataTable dt11 = new DataTable();
                DataTable dt12 = new DataTable();
                DataTable dt13 = new DataTable();
                DataTable dt14 = new DataTable();
                da10.Fill(dt10);
                da11.Fill(dt11);
                da12.Fill(dt12);
                da13.Fill(dt13);
                da14.Fill(dt14);
               // if (dt10.Rows.Count != 0)
                if (Convert.ToInt32(dt10.Rows[0][0]) != 0 || Convert.ToInt32(dt11.Rows[0][0]) != 0 || Convert.ToInt32(dt12.Rows[0][0]) != 0 || Convert.ToInt32(dt13.Rows[0][0]) != 0 || Convert.ToDouble(dt14.Rows[0][0]) != 0)
                {
                    MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Can't delete item had trans. or balance" : "لا يمكن حذف صنف تمت عليه حركة او رصيد", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "do you want to delete?" : "هل تريد حذف " + textBox1.Text + " فعلا", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        string xx = textBox1.Text.ToString();
                        
                        //...
                        using (SqlCommand cmd10 = new SqlCommand("delete from items where item_no='" + xx + "'", con2))
                        {

                            if(con2.State==ConnectionState.Closed)
                            con2.Open();
                            cmd10.ExecuteNonQuery();
                            con2.Close();
                            MessageBox.Show("تم مسح الصنف " + xx ,"",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                            textBox1.Text = "";
                            textBox2.Text = "";
                            //textBox3.Text = "0";
                            txt_price.Text = "";
                            textBox5.Text = "";
                            cmb_unit.Text = "";
                            //comboBox1.Items.Clear();
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        //...
                    }
                    else
                    {
                        //...
                    } 
                   
                    // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
                   
                }
            }
            //}
        }

        private void button9_Click(object sender, EventArgs e)
        {
            /*
            if (textBox7.Text != "")
            {

               
                SqlCommand cmd = new SqlCommand("Select_Product",con2);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "Select_Product";
                cmd.Parameters.Add("@no", SqlDbType.NVarChar, 16).Value = textBox7.Text;
                cmd.Parameters.Add("@barcode", SqlDbType.NVarChar, 16).Value = "333";
                

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

               if (dt.Rows.Count != 0)
                {
                    textBox1.Text = dt.Rows[0][0].ToString();
                    textBox2.Text = dt.Rows[0][1].ToString();
                    textBox3.Text = dt.Rows[0][2].ToString();
                    textBox4.Text = dt.Rows[0][3].ToString();
                    textBox5.Text = dt.Rows[0][4].ToString();
                    cmb_unit.Text = dt.Rows[0][5].ToString();
               }
                else
               { 
                   MessageBox.Show("لايوجد صنف بهذا الرقم", "", MessageBoxButtons.OK, MessageBoxIcon.Error); 
               }

            }
            else
            {
                MessageBox.Show("يرجى ادخال رقم صنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
             */
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
           
            //comboBox1.Items.Clear();
            //comboBox1.Items.Add(dt.Rows[0][8].ToString());
            //comboBox1.SelectedIndex = comboBox1.FindStringExact(dt.Rows[0][8].ToString());

        }

        private void button10_Click(object sender, EventArgs e)
        {
            
          //  MessageBox.Show(path +"\n"+Directory.GetCurrentDirectory() + "\\images");
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory() +"\\images"  ;
            ofd.Filter = "ملفات الصور |*.JPG; *.PNG; *.GIF; *.BMP";
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //pictureBox1.Image = Image.FromFile(ofd.FileName);
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                path = Path.GetFileName(ofd.FileName);

                string fullpath = Path.GetDirectoryName(ofd.FileName);

               

               // MessageBox.Show(fullpath + "\\" + path + "\n" + "\n" + Directory.GetCurrentDirectory() + "\\images\\" + path);
                if (!File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + path))
                {
                    strimgname = DateTime.Now.ToString("yyyyMMdd_HHmmss_", new CultureInfo("en-US", false)) + path;
                    File.Copy(fullpath + "\\" + path, Directory.GetCurrentDirectory() + "\\images\\" + strimgname, true);
                //    //  Console.WriteLine("The file exists.");
                }
                else
                {
                    strimgname = path;
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            button10_Click(sender, e);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Items_Bc fbc = new Items_Bc(textBox1.Text,Convert.ToDouble(txt_curcost.Text));
            //fbc.MdiParent = ParentForm;
            fbc.ShowDialog();
            load_data(fbc.txt_itemno.Text);
            textBox7.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
            if (e.Control && e.KeyCode == Keys.Insert)
            {
                if (item_temp != "")
                {
                    try
                    {
                        //  comboBox1.DataSource = null;
                        //  comboBox2.DataSource = null;




                      //  SqlDataAdapter da = new SqlDataAdapter("select aa.*,bb.lprice1 from items aa,brprices bb where aa.item_no=bb.itemno and bb.branch='" + BL.CLS_Session.brno + "' and (aa.item_no='" + item_temp.Trim() + "' or aa.item_barcode='" + item_temp.Trim() + "')", con2);
                        SqlDataAdapter da = new SqlDataAdapter("select aa.*,cast(item_curcost AS decimal(10,2)) as cst from items aa where (aa.item_no='" + item_temp.Trim() + "' or aa.item_barcode='" + item_temp.Trim() + "')", con2);
                       
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count != 0)
                        {
                            textBox1.Text = dt.Rows[0][0].ToString();
                            textBox2.Text = dt.Rows[0][1].ToString();
                           // textBox3.Text = dt.Rows[0][2].ToString();
                            txt_price.Text = dt.Rows[0][3].ToString();
                            textBox5.Text = dt.Rows[0][4].ToString();
                            txt_note.Text = dt.Rows[0]["note"].ToString();
                            txt_ename.Text = dt.Rows[0]["item_ename"].ToString();
                            txt_price2.Text = dt.Rows[0]["price2"].ToString();
                            cmb_type.SelectedIndex = dt.Rows[0]["item_cost"].ToString().Equals("1") ? 0 : 1;
                            txt_curbal.Text = dt.Rows[0]["item_cbalance"].ToString();
                            txt_curcost.Text =BL.CLS_Session.up_sal_icost ? dt.Rows[0]["cst"].ToString() : "0.00";
                            strimgname = dt.Rows[0][9].ToString();

                            if (dt.Rows[0][9] is DBNull)
                            {
                                // pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\simple-blue-opera-background-button.jpg");
                                // ok   pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\background-button.png");
                                pictureBox1.Image = Properties.Resources.background_button;

                            }
                            else
                            {
                                //////byte[] image = (byte[])dt.Rows[0][9];
                                //////MemoryStream ms = new MemoryStream(image);
                                //////pictureBox1.Image = Image.FromStream(ms);

                                ////////pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + dt.Rows[0][9]);
                                ////////path = dt.Rows[0][9].ToString();
                                if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + dt.Rows[0][9]))
                                {
                                    pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + dt.Rows[0][9]);

                                }
                                else
                                {
                                    pictureBox1.Image = Properties.Resources.background_button;

                                }


                                //path = dt.Rows[0][9].ToString();

                            }
                            if (Convert.ToInt32(dt.Rows[0][10]) == 1)
                            {
                                checkBox1.Checked = true;
                            }
                            else { checkBox1.Checked = false; }


                            /*
                            string tmp = dt.Rows[0][8].ToString();
                            SqlDataAdapter da2 = new SqlDataAdapter("select group_name from groups where group_id=" + Convert.ToInt32(tmp) + "", con2);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);

                            // comboBox1.Items.Clear();
                            //comboBox1.SelectedIndex = -1;

                            // comboBox1.SelectedValue = null;
                            // comboBox1.Items.Clear();


                            comboBox1.Items.Add(dt2.Rows[0][0].ToString());
                            comboBox1.SelectedIndex = comboBox1.FindStringExact(dt2.Rows[0][0].ToString());
                            */

                            cmb_unit.SelectedValue = dt.Rows[0][5].ToString();
                            cmb_group.SelectedValue = dt.Rows[0][8].ToString();
                            cmb_sgroup.SelectedValue = dt.Rows[0]["sgroup"].ToString();
                            /*
                            string tmp2 = dt.Rows[0][11].ToString();
                            //  SqlDataAdapter da4 = new SqlDataAdapter("select group_name from groups where group_id=" + Convert.ToInt32(tmp) + "", con2);
                            DataTable dt4 = dml.SELECT_QUIRY_only_retrn_dt("select tax_name from taxs where tax_id=" + Convert.ToInt32(tmp2) + "");
                            //  da2.Fill(dt2);

                            // comboBox1.Items.Clear();
                            //comboBox1.SelectedIndex = -1;

                            // comboBox1.SelectedValue = null;
                            // comboBox1.Items.Clear();


                            comboBox3.Items.Add(dt4.Rows[0][0].ToString());
                            comboBox3.SelectedIndex = comboBox3.FindStringExact(dt4.Rows[0][0].ToString());
                            */
                            cmb_tax.SelectedValue = dt.Rows[0][11].ToString();
                            /*
                            SqlDataAdapter da3 = new SqlDataAdapter("select unit_id,unit_name from units where unit_id=" + dt.Rows[0][5].ToString() + "", con2);
                            DataTable dt3 = new DataTable();
                            da3.Fill(dt3);

                            comboBox2.DataSource = dt3;
                            comboBox2.DisplayMember = "unit_name";
                            comboBox2.ValueMember = "unit_id";

                            comboBox2.SelectedValue = dt3.Rows[0][0].ToString();
                            */


                            cmb_u2.SelectedValue = dt.Rows[0]["unit2"].ToString();
                            cmb_u3.SelectedValue = dt.Rows[0]["unit3"].ToString();
                            cmb_u4.SelectedValue = dt.Rows[0]["unit4"].ToString();

                            txt_u2q.Text = dt.Rows[0]["uq2"].ToString();
                            txt_u3q.Text = dt.Rows[0]["uq3"].ToString();
                            txt_u4q.Text = dt.Rows[0]["uq4"].ToString();

                            txt_u2p.Text = dt.Rows[0]["unit2p"].ToString();
                            txt_u3p.Text = dt.Rows[0]["unit3p"].ToString();
                            txt_u4p.Text = dt.Rows[0]["unit4p"].ToString();

                            txt_price2.Text = dt.Rows[0]["price2"].ToString();
                            txt_curbal.Text = dt.Rows[0]["item_cbalance"].ToString();
                            txt_curcost.Text =BL.CLS_Session.up_sal_icost ? dt.Rows[0]["cst"].ToString() : "0.00";
                            cmb_type.SelectedIndex = dt.Rows[0]["item_cost"].ToString().Equals("1") ? 0 : 1;
                            // comboBox2.Items.Add(dt3.Rows[0][0].ToString());
                            //  comboBox2.SelectedIndex = comboBox2.FindStringExact(dt3.Rows[0][1].ToString());
                            // comboBox2.SelectedValue = dt.Rows[0][5].ToString();
                            // comboBox1.Items.Clear();
                            //  comboBox1.Items.Add(dt.Rows[0][8].ToString());
                            //  comboBox1.SelectedIndex = comboBox1.FindStringExact(dt.Rows[0][8].ToString());
                            SqlDataAdapter da4s = new SqlDataAdapter("select su_no,su_name from suppliers where su_no='" + dt.Rows[0]["supno"].ToString() + "' and su_brno='" + BL.CLS_Session.brno + "'", con2);
                            DataTable dt4s = new DataTable();
                            da4s.Fill(dt4s);
                            if (dt4s.Rows.Count > 0)
                            {
                                txt_supno.Text = dt4s.Rows[0][0].ToString();
                                txt_supname.Text = dt4s.Rows[0][1].ToString();
                            }
                            else
                            {
                                txt_supname.Text = "";
                                txt_supno.Text = "";
                            }
                            textBox7.Text = "";
                            //atn_edit.Enabled = true;
                            //btn_bar.Enabled = true;
                            //btn_statmnt.Enabled = true;
                            //btn_del.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "No Item Like this no." : "لايوجد صنف بهذا الرقم", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox7.Text = "";
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    //  MessageBox.Show("يرجى ادخال رقم المورد", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //comboBox2.Focus();
                txt_ename.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_price.Focus();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox5.Focus();
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_group.Focus();
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_group.Focus();
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_sgroup.Focus();
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    button3.Focus();
            //}
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            /*
            SqlDataAdapter da = new SqlDataAdapter("select unit_id,unit_name from units", con2);
            DataTable dt = new DataTable();
            da.Fill(dt);


            comboBox2.DataSource = dt;
             */
           
            

           
            //comboBox1.Items.Clear();
            //comboBox1.Items.Add(dt.Rows[0][8].ToString());
            //comboBox1.SelectedIndex = comboBox1.FindStringExact(dt.Rows[0][8].ToString());
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_price.Focus();
            }
        }

        private void comboBox3_Enter(object sender, EventArgs e)
        {
            
           // DataTable dt = dml.SELECT_QUIRY("select tax_id,tax_name from taxs");


          //  dtunits =
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           // cmb_dunit.SelectedIndex = -1;
           
        }

        private void cmb_u2_Enter(object sender, EventArgs e)
        {
           // MessageBox.Show(comboBox2.SelectedValue.ToString());
          //  string qury =  ;
           
           cmb_u2.DataSource = dml.SELECT_QUIRY_only_retrn_dt("select unit_id,unit_name from units where unit_id not in(" + cmb_unit.SelectedValue+")");
           cmb_u2.DisplayMember = "unit_name";
           cmb_u2.ValueMember = "unit_id";
  
         //   DataRow[] dtr = dtunits.Select("unit_id not in(" + comboBox2.SelectedValue + ")");
            //DataTable dttemp = new DataTable();
            //foreach (DataRow row in dtr)
            //{
            //    dttemp.ImportRow(row);
            //}

          //  cmb_u2.DataSource = dtunits
            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
            /*
            try
            {
                var dv1 = new DataView();
                dv1 = dtunits.DefaultView;
                // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                dv1.RowFilter = "unit_id not in(" + comboBox2.SelectedValue + ")";
                DataTable dtNew = dv1.ToTable();
                // MessageBox.Show(dtNew.Rows[0][1].ToString());
                cmb_u2.DataSource = dtNew;

                cmb_u2.DisplayMember = "unit_name";
                cmb_u2.ValueMember = "unit_id";
            }
            catch { }
             */
            /*
            cmb_u2.DataSource = dtunits;
            cmb_u2.DisplayMember = "unit_name";
            cmb_u2.ValueMember = "unit_id";

            cmb_u2.Items.Remove(comboBox2.SelectedValue);
             */
        }

        private void comboBox4_Enter(object sender, EventArgs e)
        {
           
        }

        private void cmb_u3_Enter(object sender, EventArgs e)
        {
            /*
            try{
          //  cmb_u3.DataSource = dml.SELECT_QUIRY_only_retrn_dt("select unit_id,unit_name from units where unit_id not in(" + comboBox2.SelectedValue + "," + cmb_u2.SelectedValue + ")");
       
                var dv1 = new DataView();
                dv1 = dtunits.DefaultView;
                // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                dv1.RowFilter = "unit_id not in(" + comboBox2.SelectedValue + "," + cmb_u2.SelectedValue + ")";
                DataTable dtNew = dv1.ToTable();
                // MessageBox.Show(dtNew.Rows[0][1].ToString());
                cmb_u3.DataSource = dtNew;

            cmb_u3.DisplayMember = "unit_name";
            cmb_u3.ValueMember = "unit_id";
             }
            catch { }
             * */
            
            cmb_u3.DataSource = dml.SELECT_QUIRY_only_retrn_dt("select unit_id,unit_name from units where unit_id not in(" + cmb_unit.SelectedValue + "," + (cmb_u2.SelectedIndex != -1 ? cmb_u2.SelectedValue : 0) + ")");
            cmb_u3.DisplayMember = "unit_name";
            cmb_u3.ValueMember = "unit_id";
             
            /*
            cmb_u3.DataSource = dtunits;
            cmb_u3.DisplayMember = "unit_name";
            cmb_u3.ValueMember = "unit_id";
            cmb_u3.Items.Remove(comboBox2.SelectedValue);
            cmb_u3.Items.Remove(cmb_u2.SelectedValue);
             */
        }

        private void cmb_u4_Enter(object sender, EventArgs e)
        {
            /*
            try
            {
                
               // cmb_u4.DataSource = dml.SELECT_QUIRY_only_retrn_dt("select unit_id,unit_name from units where unit_id not in(" + comboBox2.SelectedValue + "," + cmb_u2.SelectedValue + "," + cmb_u3.SelectedValue + ")");
                
                var dv1 = new DataView();
                dv1 = dtunits.DefaultView;
                // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                dv1.RowFilter = "unit_id not in(" + comboBox2.SelectedValue + "," + cmb_u2.SelectedValue + "," + cmb_u3.SelectedValue + ")";
                DataTable dtNew = dv1.ToTable();
                // MessageBox.Show(dtNew.Rows[0][1].ToString());
                cmb_u4.DataSource = dtNew;

                cmb_u4.DisplayMember = "unit_name";
                cmb_u4.ValueMember = "unit_id";
                 
            }
            catch { }
             * */
            
            cmb_u4.DataSource = dml.SELECT_QUIRY_only_retrn_dt("select unit_id,unit_name from units where unit_id not in(" + cmb_unit.SelectedValue + "," + (cmb_u2.SelectedIndex != -1 ? cmb_u2.SelectedValue : 0) + "," + (cmb_u3.SelectedIndex != -1 ? cmb_u3.SelectedValue : 0) + ")");
            cmb_u4.DisplayMember = "unit_name";
            cmb_u4.ValueMember = "unit_id";
             
            /*
            cmb_u4.DataSource = dtunits;
            cmb_u4.DisplayMember = "unit_name";
            cmb_u4.ValueMember = "unit_id";
            cmb_u4.Items.Remove(comboBox2.SelectedValue);
            cmb_u4.Items.Remove(cmb_u2.SelectedValue);
            cmb_u4.Items.Remove(cmb_u3.SelectedValue);
            */
        }

        private void cmb_u2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
               // cmb_u2.DataSource = null;
                cmb_u2.SelectedIndex = -1;
               // cmb_u2.SelectedValue = null;
            }
            if (e.KeyCode == Keys.Enter)
            {
                txt_u2q.Focus();
            }
        }

        private void cmb_u3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
               // cmb_u3.DataSource = null;
                cmb_u3.SelectedIndex = -1;
                // cmb_u2.SelectedValue = null;
            }
            if (e.KeyCode == Keys.Enter)
            {
                txt_u3q.Focus();
            }
        }

        private void cmb_u4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
               // cmb_u4.Items.Clear();
                cmb_u4.SelectedIndex = -1;
                //  cmb_u4.DataSource = null;
            }
            if (e.KeyCode == Keys.Enter)
            {
                txt_u4q.Focus();
            }
        }

        private void txt_u2q_TextChanged(object sender, EventArgs e)
        {
            //if(string.IsNullOrEmpty(txt_u2q.Text))
            //    txt_u2q.Text="0";
        }

        private void txt_u3q_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_u3q.Text))
            //    txt_u3q.Text = "0";
        }

        private void txt_u4q_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_u4q.Text))
            //    txt_u4q.Text = "0";
        }

        private void txt_u2p_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_u2p.Text))
            //    txt_u2p.Text = "0";
        }

        private void txt_u3p_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_u3p.Text))
            //    txt_u3p.Text = "0";
        }

        private void txt_u4p_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_u4p.Text))
            //    txt_u4p.Text = "0";
        }

        private void txt_u2q_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_u2p.Focus();
            }
        }

        private void txt_u3q_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_u3p.Focus();
            }
        }

        private void txt_u4q_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_u4p.Focus();
            }
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_u2.Focus();
            }
        }

        private void txt_u2p_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               // cmb_u3.Focus();
                txt_supno.Focus();
            }
        }

        private void txt_u3p_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_u4.Focus();
            }
        }

        private void btn_prt_Click(object sender, EventArgs e)
        {
            DataTable dt = dml.SELECT_QUIRY_only_retrn_dt("select item_no c1,item_name c2,item_image c3 from items");
            
          //  MessageBox.Show(new Uri(Application.StartupPath + @"\Logo.jpg").AbsoluteUri);
         //   MessageBox.Show(("file:///" + Application.StartupPath + @"\Logo.jpg").Replace(@"\", "/"));
            try
            {
                Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();
                //  DataSet ds1 = new DataSet();

                foreach (DataRow row in dt.Rows)
                {
                    row[2] = new Uri(Application.StartupPath + @"\images\" + row[2]).AbsoluteUri;
                }
                //DataTable dt = new DataTable();
                //int cn = 1;
                //foreach (DataGridViewColumn col in dataGridView1.Columns)
                //{
                //    /*
                //    dt.Columns.Add(col.Name);
                //    // dt.Columns.Add(col.HeaderText);
                //     */
                //    dt.Columns.Add("c" + cn.ToString());
                //    cn++;
                //}

                //foreach (DataGridViewRow row in dataGridView1.Rows)
                //{
                //    DataRow dRow = dt.NewRow();
                //    foreach (DataGridViewCell cell in row.Cells)
                //    {
                //        dRow[cell.ColumnIndex] = cell.Value;

                //        if (cell.ColumnIndex == 2)
                //        {
                //            DataRow[] res = dtunits.Select("unit_id ='" + dRow[2] + "'");
                //            //   row[2] = res[0][1];
                //            dRow[cell.ColumnIndex] = res[0][1];
                //        }
                //    }
                //    dt.Rows.Add(dRow);
                //}




                rf.reportViewer1.LocalReport.DataSources.Clear();
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("sdt", dt));
                // rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("account", dth));

                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sto.rpt.All_P_rpt.rdlc";

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                //  parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());
                parameters[1] = new ReportParameter("br_name", BL.CLS_Session.brname);
                //parameters[2] = new ReportParameter("type", cmb_type.Text);
                //parameters[3] = new ReportParameter("ref", txt_ref.Text);
                //parameters[4] = new ReportParameter("date", txt_mdate.Text);
                //parameters[5] = new ReportParameter("cust", txt_custnam.Text + "  " + txt_custno.Text);

                //parameters[6] = new ReportParameter("desc", txt_desc.Text);
                //parameters[7] = new ReportParameter("tax_id", BL.CLS_Session.tax_no);
                //parameters[8] = new ReportParameter("total", txt_total.Text);
                //parameters[9] = new ReportParameter("descount", txt_des.Text);
                //parameters[10] = new ReportParameter("tax", txt_tax.Text);
                //parameters[11] = new ReportParameter("nettotal", txt_net.Text);


                //BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(txt_net.Text), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);

                ////  MessageBox.Show(toWord.ConvertToArabic());
                //parameters[12] = new ReportParameter("a_toword", toWord.ConvertToArabic());

                 parameters[2] = new ReportParameter("img",new Uri(Application.StartupPath + @"\Logo.jpg").AbsoluteUri);
                //parameters[13] = new ReportParameter("img", ("file:///" + Application.StartupPath + @"\Logo.jpg").Replace(@"\", "/"));
                rf.reportViewer1.LocalReport.EnableExternalImages = true;
                rf.reportViewer1.LocalReport.SetParameters(parameters);
                // */

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btn_Undo_Click(object sender, EventArgs e)
        {
             DialogResult result = MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "are you sure to undo?" : "هل تريد التراجع عن التغييرات", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
             if (result == DialogResult.Yes)
             {



                 if (isnew)
                 {
                     textBox1.Enabled = false;
                     textBox2.Enabled = false;
                     cmb_dunit.Enabled = false;
                     txt_staticcst.Enabled = false;
                     chk_inactv.Enabled = false;
                     //textBox3.Enabled = false;
                     cmb_type.Enabled = false;
                     txt_price.Enabled = false;
                     textBox5.Enabled = false;
                     txt_ename.Enabled = false;
                     cmb_unit.Enabled = false;
                     // button1.Enabled = false;
                     cmb_group.Enabled = false;
                     cmb_sgroup.Enabled = false;
                     cmb_tax.Enabled = false;
                     // button3.Enabled = false;
                     //  button6.Enabled = false;
                     checkBox1.Enabled = false;
                     pictureBox1.Enabled = false;
                     pnl_qtys.Enabled = false;
                     txt_price2.Enabled = false;
                     btn_Undo.Enabled = false;
                     btn_Exit.Enabled = true;
                     btn_add.Enabled = true;
                     btn_del.Enabled = false;
                     btn_savchng.Enabled = false;
                     atn_edit.Enabled = false;
                     btn_save.Enabled = false;
                     btn_prtbar.Enabled = true;
                     txt_prtbar.Enabled = true;
                     txt_supno.Enabled = false;
                     txt_note.Enabled = false;
                     btn_find.Enabled = true;
                     btn_opal.Enabled = true;
                     btn_cpal.Enabled = true;
                     textBox7.Focus();
                 }
                 else
                 {
                     textBox1.Enabled = false;
                     textBox2.Enabled = false;
                     cmb_dunit.Enabled = false;
                     txt_staticcst.Enabled = false;
                     chk_inactv.Enabled = false;
                     //textBox3.Enabled = false;
                     cmb_type.Enabled = false;
                     txt_price.Enabled = false;
                     textBox5.Enabled = false;
                     txt_ename.Enabled = false;
                     cmb_unit.Enabled = false;
                     // button1.Enabled = false;
                     cmb_group.Enabled = false;
                     cmb_sgroup.Enabled = false;
                     cmb_tax.Enabled = false;
                     // button3.Enabled = false;
                     //  button6.Enabled = false;
                     checkBox1.Enabled = false;
                     pictureBox1.Enabled = false;
                     pnl_qtys.Enabled = false;
                     txt_price2.Enabled = false;
                     btn_Undo.Enabled = false;
                     btn_Exit.Enabled = true;
                     btn_add.Enabled = true;
                     btn_del.Enabled = false;
                     btn_savchng.Enabled = false;
                     btn_prtbar.Enabled = true;
                     txt_prtbar.Enabled = true;
                     txt_supno.Enabled = false;
                     txt_note.Enabled = false;
                     btn_find.Enabled = true;
                     btn_opal.Enabled = true;
                     btn_cpal.Enabled = true;
                     load_data(textBox1.Text);
                     textBox7.Focus();
                     btn_save.Enabled = false;
                 }
                 isnew = false;
                 isupdate = false;
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

        private void txt_ename_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_unit.Focus();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Sto.Opn_Bal_Items sto = new Sto.Opn_Bal_Items(textBox1.Text, textBox2.Text);
                //sto.MdiParent=ParentForm;
                sto.ShowDialog();
               // load_data(textBox1.Text);
            }
            catch { }
        }

        private void txt_custno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
               // txt_name.Text = "";
                try
                {
                    Search_All f4 = new Search_All("7", "Sup");
                    f4.ShowDialog();
                    txt_supno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_supname.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                }
                catch { }

            }
            
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
            {
                Sup.Suppliers ac = new Sup.Suppliers(txt_supno.Text);
                ac.MdiParent = ParentForm;
                ac.Show();
            }

            if (e.KeyCode == Keys.Enter)
            {
                txt_custno_Leave(sender, e);

                if (from_pur == 1)
                    btn_save.Select();
            }
        }

        private void txt_custno_Leave(object sender, EventArgs e)
        {
            DataTable dt2 = dml.SELECT_QUIRY_only_retrn_dt("select su_no ,su_name  from suppliers where su_brno='" + BL.CLS_Session.brno + "' and su_no='" + txt_supno.Text + "'");


            // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

            if (dt2.Rows.Count > 0)
            {
                txt_supname.Text = dt2.Rows[0][1].ToString();
               // txt_temp.Text = dt2.Rows[0][2].ToString();
               // txt_crlmt.Text = dt2.Rows[0][3].ToString();
            }
            else
            {
                txt_supname.Text = "";
                txt_supno.Text = "";
               // txt_crlmt.Text = "";
            }
        }

        private void btn_statmnt_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.formkey.Contains("E133"))
            {
                Sto.Sto_ItemStmt_Exp f4a = new Sto.Sto_ItemStmt_Exp(textBox1.Text);
                f4a.MdiParent = ParentForm;
                f4a.Show();
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                Sto.Sto_CurBal_Items f4a = new Sto.Sto_CurBal_Items(textBox1.Text, textBox2.Text);
                f4a.MdiParent = ParentForm;
                f4a.Show();
            }
            catch { }
        }

        private void txt_price_TextChanged(object sender, EventArgs e)
        {
            try{
                if (BL.CLS_Session.isshamltax.Equals("2"))
                    txt_priceper.Text = Math.Round(((Convert.ToDouble(txt_bprice.Text) - Convert.ToDouble(txt_staticcst.Text)) / Convert.ToDouble(txt_staticcst.Text)) * 100, 2)<0? "0" : (Math.Round(((Convert.ToDouble(txt_bprice.Text) - Convert.ToDouble(txt_staticcst.Text)) / Convert.ToDouble(txt_staticcst.Text)) * 100, 2)).ToString();
                else
                    txt_priceper.Text = Math.Round(((Convert.ToDouble(txt_price.Text) - Convert.ToDouble(txt_staticcst.Text)) / Convert.ToDouble(txt_staticcst.Text)) * 100, 2)<0? "0" : (Math.Round(((Convert.ToDouble(txt_price.Text) - Convert.ToDouble(txt_staticcst.Text)) / Convert.ToDouble(txt_staticcst.Text)) * 100, 2)).ToString();
            //if (isnew)
            //    txt_price2.Text = txt_price.Text;
            ////if (string.IsNullOrEmpty(txt_price.Text))
            ////    txt_price.Text = "0";
            //try
            //{
            //   // if (txt_price.Enabled && (isnew || isupdate))
            //    txt_bprice.Text = (Math.Round((Convert.ToDouble(txt_price.Text) - Convert.ToDouble(txt_price.Text) / ((100 + Convert.ToDouble(BL.CLS_Session.tax_per)) / Convert.ToDouble(BL.CLS_Session.tax_per))), 2)).ToString();
            }
            catch {
                txt_priceper.Text = "0";
            }
            txt_price_Leave(null, null);


        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Sto.Mizan_Expoert rs = new Sto.Mizan_Expoert();
            rs.MdiParent = MdiParent;
            rs.Show();
        }

        private void cmb_sgroup_Enter(object sender, EventArgs e)
        {
            try
            {
                cmb_sgroup.DataSource = dml.SELECT_QUIRY_only_retrn_dt("select group_id,group_name from groups where group_pid='" + cmb_group.SelectedValue + "' and group_pid is not null");
                // metroComboBox3.DataSource = dt2;
                //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
                // metroComboBox3.DisplayMember = "a";
                //  comboBox1.ValueMember = comboBox1.Text;
                cmb_sgroup.DisplayMember = "group_name";
                cmb_sgroup.ValueMember = "group_id";
                cmb_sgroup.SelectedIndex = -1;
            }
            catch { }
        }

        private void cmb_sgroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_sgroup.SelectedIndex = -1;
            }
            if (e.KeyCode == Keys.Enter)
            {
                cmb_tax.Focus();
            }
        }

        private void txt_price2_TextChanged(object sender, EventArgs e)
        {
            ////if (string.IsNullOrEmpty(txt_price2.Text))
            ////    txt_price2.Text = "0";
        }

        private void label23_Click(object sender, EventArgs e)
        {
            if (label23.Text.Equals("بالرقم") || label23.Text.Equals("By No"))
            {
                label23.Text =BL.CLS_Session.lang.Equals("E")? "By Barcode" : "بالباركود";
                BL.CLS_Session.temp_bcorno = "bc";
            }
            else
            {
                label23.Text = BL.CLS_Session.lang.Equals("E") ? "By No" : "بالرقم";
                BL.CLS_Session.temp_bcorno = "no";
            }
        }

        private void btn_combin_Click(object sender, EventArgs e)
        {
            dtcount = dml.SELECT_QUIRY_only_retrn_dt("getTotalCount @itemno = '"+textBox1.Text+"'");
            if (Convert.ToInt32(dtcount.Rows[0][0]) == 0)
            {
                Items_Combin itmc = new Items_Combin(textBox1.Text, textBox2.Text, false);
                //itmc.MdiParent = MdiParent;
                itmc.ShowDialog();
            }
            else
            {
                Items_Combin itmc = new Items_Combin(textBox1.Text, textBox2.Text, true);
                //itmc.MdiParent = MdiParent;
                itmc.ShowDialog();
            }
        }

        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmb_type.SelectedIndex == 2)
            //    btn_combin.Enabled = true;
            //else
            //    btn_combin.Enabled = false;
        }

        private void btn_brprice_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                Br_Prices itmc = new Br_Prices(textBox1.Text, textBox2.Text, txt_price.Text, textBox5.Text);
                //itmc.MdiParent = MdiParent;
                itmc.ShowDialog();
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            SqlDataAdapter da22 = new SqlDataAdapter("select * from items where item_no ='" + textBox1.Text.Trim() + "'", con2);
            DataTable dt22 = new DataTable();

            da22.Fill(dt22);

            if (dt22.Rows.Count > 0)
            {
                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Item or barcode already exist" : "الصنف او الباركود موجود من سابق", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();   
            }

            textBox1.Text = textBox1.Text.ToUpper();
        }

        private void btn_irp_Click(object sender, EventArgs e)
        {
            Items_Altr itmc = new Items_Altr(textBox1.Text, textBox2.Text, false);
            //itmc.MdiParent = MdiParent;
            itmc.ShowDialog();
        }

        private void txt_u4p_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_supno.Focus();
            }
        }

        private void cmb_sgroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_u2_Leave(object sender, EventArgs e)
        {
            if (cmb_u2.SelectedIndex != -1 && (string.IsNullOrEmpty(txt_u2q.Text) || Convert.ToDouble(txt_u2q.Text) == 0))
            {
               // DataTable dttemp = dml.SELECT_QUIRY_only_retrn_dt("select isnull(unit_qty,0) from units where unit_id = " + cmb_u2.SelectedValue + "");
              //  txt_u2q.Text = dttemp.Rows[0][0].ToString();
                DataRow[] dtr = BL.CLS_Session.dtunits.Select("unit_id = " + cmb_u2.SelectedValue + "");
                txt_u2q.Text = dtr[0][4].ToString();
            }
           //DataTable dttemp = new DataTable();
            //foreach (DataRow row in dtr)
            //{
            //    dttemp.ImportRow(row);
            //}
        }

        private void txt_u2p_Enter(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txt_u2p.Text)==0 || string.IsNullOrEmpty(txt_u2p.Text))
                txt_u2p.Text = (Convert.ToDouble(txt_u2q.Text) * Convert.ToDouble(txt_price.Text)).ToString();
            //else
            //    txt_u2p.Text = "0";
        }

        private void txt_u3p_Enter(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txt_u3p.Text) == 0 || string.IsNullOrEmpty(txt_u3p.Text))
                txt_u3p.Text = (Convert.ToDouble(txt_u3q.Text) * Convert.ToDouble(txt_price.Text)).ToString();
            //else
            //    txt_u3p.Text = "0";
        }

        private void txt_u4p_Enter(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txt_u4p.Text) == 0 || string.IsNullOrEmpty(txt_u4p.Text))
                txt_u4p.Text = (Convert.ToDouble(txt_u4q.Text) * Convert.ToDouble(txt_price.Text)).ToString();
            //else
            //    txt_u4p.Text = "0";
        }

        private void cmb_u3_Leave(object sender, EventArgs e)
        {
            if (cmb_u3.SelectedIndex != -1 && (string.IsNullOrEmpty(txt_u3q.Text) || Convert.ToDouble(txt_u3q.Text) == 0))
            {
               // DataTable dttemp = dml.SELECT_QUIRY_only_retrn_dt("select isnull(unit_qty,0) from units where unit_id = " + cmb_u3.SelectedValue + "");
               // txt_u3q.Text = dttemp.Rows[0][0].ToString();
                DataRow[] dtr = BL.CLS_Session.dtunits.Select("unit_id = " + cmb_u3.SelectedValue + "");
                txt_u3q.Text = dtr[0][4].ToString();
            }
        }

        private void cmb_u4_Leave(object sender, EventArgs e)
        {
            if (cmb_u4.SelectedIndex != -1 && (string.IsNullOrEmpty(txt_u4q.Text) || Convert.ToDouble(txt_u4q.Text) == 0))
            {
              // DataTable dttemp = dml.SELECT_QUIRY_only_retrn_dt("select isnull(unit_qty,0) from units where unit_id = " + cmb_u4.SelectedValue + "");
              //  txt_u4q.Text = dttemp.Rows[0][0].ToString();
                DataRow[] dtr = BL.CLS_Session.dtunits.Select("unit_id = " + cmb_u4.SelectedValue + "");
                txt_u4q.Text = dtr[0][4].ToString();
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox5.Text))
                textBox5.Text=textBox1.Text;
            SqlDataAdapter da22 = new SqlDataAdapter("select * from items where item_barcode ='" + textBox5.Text.Trim() + "'", con2);
            DataTable dt22 = new DataTable();

            da22.Fill(dt22);

            if (dt22.Rows.Count > 0 && isnew)
            {
                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? " Barcode already exist" : "  الباركود موجود من سابق", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
            }

           // textBox5.Text = textBox5.Text.ToUpper();

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
        }

        private void txt_price_Validating(object sender, CancelEventArgs e)
        {
            datval.ValidateText(txt_price);
        }

        private void txt_u2p_Validating(object sender, CancelEventArgs e)
        {
            datval.ValidateText(txt_u2p);
        }

        private void txt_u3p_Validating(object sender, CancelEventArgs e)
        {
            datval.ValidateText(txt_u3p);
        }

        private void txt_u4p_Validating(object sender, CancelEventArgs e)
        {
            datval.ValidateText(txt_u4p);
        }

        private void txt_price2_Validating(object sender, CancelEventArgs e)
        {
            datval.ValidateText(txt_price2);
        }

        private void txt_u2q_Validating(object sender, CancelEventArgs e)
        {
            datval.ValidateText(txt_u2q);
        }

        private void txt_u3q_Validating(object sender, CancelEventArgs e)
        {
            datval.ValidateText(txt_u3q);
        }

        private void txt_u4q_Validating(object sender, CancelEventArgs e)
        {
            datval.ValidateText(txt_u4q);
        }

        private void Item_Card_Shown(object sender, EventArgs e)
        {
            if (from_pur == 1)
                button3_Click(sender, e);

            if (!BL.CLS_Session.cmp_type.Equals("m"))
            {
                this.Close();
            }

            if (!BL.CLS_Session.formkey.Contains(this.Tag.ToString()))
            {
                this.Close();
            }
           
        }

        private void txt_bprice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_price.Enabled && txt_bprice.Focused && (isnew || isupdate))
                  txt_price.Text = (Math.Round((Convert.ToDouble(txt_bprice.Text) + (Convert.ToDouble(txt_bprice.Text) * Convert.ToDouble(BL.CLS_Session.tax_per) / 100)), 2)).ToString();
            }
            catch { }
        }

        private void txt_bprice_Validating(object sender, CancelEventArgs e)
        {
            datval.ValidateText(txt_bprice);
        }

        private void txt_bprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox5.Focus();
            }
        }


        private void txt_price_Leave(object sender, EventArgs e)
        {
            try
            {
                // if (txt_price.Enabled && (isnew || isupdate))
                txt_bprice.Text = (Math.Round((Convert.ToDouble(txt_price.Text) - Convert.ToDouble(txt_price.Text) / ((100 + Convert.ToDouble(BL.CLS_Session.tax_per)) / Convert.ToDouble(BL.CLS_Session.tax_per))), 2)).ToString();
            }
            catch {
               
            }
        }

        private void txt_staticcst_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (BL.CLS_Session.isshamltax.Equals("2"))
                    txt_priceper.Text = Math.Round(((Convert.ToDouble(txt_bprice.Text) - Convert.ToDouble(txt_staticcst.Text)) / Convert.ToDouble(txt_staticcst.Text)) * 100, 2) < 0 ? "0" : (Math.Round(((Convert.ToDouble(txt_bprice.Text) - Convert.ToDouble(txt_staticcst.Text)) / Convert.ToDouble(txt_staticcst.Text)) * 100, 2)).ToString();
                else
                    txt_priceper.Text = Math.Round(((Convert.ToDouble(txt_price.Text) - Convert.ToDouble(txt_staticcst.Text)) / Convert.ToDouble(txt_staticcst.Text)) * 100, 2) < 0 ? "0" : (Math.Round(((Convert.ToDouble(txt_price.Text) - Convert.ToDouble(txt_staticcst.Text)) / Convert.ToDouble(txt_staticcst.Text)) * 100, 2)).ToString();
            }
            catch {
                txt_priceper.Text = "0";
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("EN"));
        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cmb_dunit_Enter(object sender, EventArgs e)
        {
            cmb_dunit.DataSource = dml.SELECT_QUIRY_only_retrn_dt("select unit_id,unit_name from units where unit_id in(" + cmb_unit.SelectedValue + "," + (cmb_u2.SelectedIndex == -1 ? 0 : cmb_u2.SelectedValue) + "," + (cmb_u3.SelectedIndex == -1 ? 0 : cmb_u3.SelectedValue) + "," + (cmb_u4.SelectedIndex == -1 ? 0 : cmb_u4.SelectedValue) + ") order by unit_order");
            cmb_dunit.DisplayMember = "unit_name";
            cmb_dunit.ValueMember = "unit_id";
        }

        private void cmb_u2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_u3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_u4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_unit_Leave(object sender, EventArgs e)
        {
            cmb_dunit_Enter(sender, e);
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void txt_supname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_supno_TextChanged(object sender, EventArgs e)
        {

        }
        
    }
}

