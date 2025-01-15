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


namespace POS.Sup
{
    public partial class Suppliers : BaseForm
    {
        bool isupdate, isnew;
        DataTable dtunits, dtcur;
        BL.DAML dml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        string bc = "",bprinter,sup_id;
        string path = "0.png";
        SqlConnection con2 = BL.DAML.con;//new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        //public Item_Card(string itemno)
        public Suppliers(string supno)
        {
            sup_id = supno;
           // Form.ActiveForm.Refresh();

            InitializeComponent();
           
            txt_no.Enabled = false;
            txt_name.Enabled = false;
            txt_note.Enabled = false;
            txt_mobile.Enabled = false;
            txt_address.Enabled = false;
            txt_opnbal.Enabled = false;
            txt_openfcy.Enabled = false;
            cmb_cur.Enabled = false;
            cmb_class.Enabled = false;
            txt_maxbal.Enabled = false;
            txt_resperson.Enabled = false;
            txt_taxcode.Enabled = false;
            chk_priceshameltax.Enabled = false;
            cmb_city.Enabled = false;
            txt_period.Enabled = false;
            checkBox1.Enabled = false;
            txt_cmncode.Enabled = false;
            txt_assup.Enabled = false;

           // button1.Visible = false;
           // comboBox1.Enabled = false;
          //  comboBox3.Enabled = false;
            // button3.Visible = false;
          //  button6.Visible = false;
        //    checkBox1.Enabled = false;
         //   pictureBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isnew)
            {
            if (txt_no.Text != "" && txt_name.Text != "" && cmb_class.Text != "" )
            {
                MemoryStream ms = new MemoryStream();
               // pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] byteImage = ms.ToArray();
                //if (pictureBox1.Image == null)
                //{

                //}
                //else
                //{
                    
                //}
                
             //   SqlDataAdapter da2 = new SqlDataAdapter("select * from suppliers where (su_no ='" + txt_no.Text + "' and su_brno='" + BL.CLS_Session.brno + "') or (vndr_taxcode='" + txt_taxcode.Text + "' and vndr_taxcode<>'')", con2);
                SqlDataAdapter da2 = new SqlDataAdapter("select * from suppliers where su_no ='" + txt_no.Text + "'", con2);
                //  SqlDataAdapter da3 = new SqlDataAdapter("select * from items_bc where barcode ='" + txt_opnbal.Text + "'", con2);
                DataTable dt2 = new DataTable();
              //  DataTable dt3 = new DataTable();
                SqlDataAdapter da3 = new SqlDataAdapter("select * from suppliers where vndr_taxcode='" + txt_taxcode.Text + "' and vndr_taxcode<>''", con2);
                //  SqlDataAdapter da3 = new SqlDataAdapter("select * from items_bc where barcode ='" + txt_opnbal.Text + "'", con2);
                DataTable dt3 = new DataTable();

                da2.Fill(dt2);
                da3.Fill(dt3);

                if (dt3.Rows.Count > 0)
                {
                    MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Tax Id For supplier already exists" : "الرقم الضريبي للمورد موجود من سابق", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
               // da3.Fill(dt3);

                if (dt2.Rows.Count == 0 )
                {
                    // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
                    using (SqlCommand cmd1 = new SqlCommand("INSERT INTO suppliers ( su_brno,su_no, su_name, su_class, su_addrs, su_tel, su_opnbal, su_cntactp,su_crlmt, inactive,su_cur,su_city,vndr_taxcode,su_opnfcy,su_pymnt,cmncode,whno,is_shamel_tax,s_bulding_no, s_street, s_site_name, s_city, s_cuntry, s_postal_code, s_ex_postalcode, s_other_id,su_note) VALUES(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13,@a14,@a15,@a16,@a17,@a18,@a19,@a20,@a21,@a22,@a23,@a24,@a25,@a26,@a27)", con2))
                    {


                        cmd1.Parameters.AddWithValue("@a1", BL.CLS_Session.brno);
                        cmd1.Parameters.AddWithValue("@a2", txt_no.Text);
                        cmd1.Parameters.AddWithValue("@a3", txt_name.Text);
                        cmd1.Parameters.AddWithValue("@a4", cmb_class.SelectedValue);
                        cmd1.Parameters.AddWithValue("@a5", txt_address.Text);
                      //  cmd1.Parameters.AddWithValue("@a6", textBox6.Text);
                        cmd1.Parameters.AddWithValue("@a6", txt_mobile.Text);
                      //  cmd1.Parameters.AddWithValue("@a7", comboBox1.SelectedValue.ToString());
                        // cmd1.Parameters.AddWithValue("@a8", byteImage);
                        cmd1.Parameters.AddWithValue("@a7", string.IsNullOrEmpty(txt_opnbal.Text) ? 0 : Convert.ToDouble(txt_opnbal.Text));
                     //   if (checkBox1.Checked == true)
                     //   { cmd1.Parameters.AddWithValue("@a9", 1); }
                     //   else { cmd1.Parameters.AddWithValue("@a9", 0); }
                        cmd1.Parameters.AddWithValue("@a8", txt_resperson.Text);
                        cmd1.Parameters.AddWithValue("@a9", string.IsNullOrEmpty(txt_maxbal.Text) ? 0 : Convert.ToDouble(txt_maxbal.Text));
                        cmd1.Parameters.AddWithValue("@a10", checkBox1.Checked ? 1 : 0);
                        cmd1.Parameters.AddWithValue("@a11", cmb_cur.SelectedIndex==-1? "" : cmb_cur.SelectedValue);
                        cmd1.Parameters.AddWithValue("@a12", cmb_city.SelectedIndex == -1 ? "" : cmb_city.SelectedValue);
                        cmd1.Parameters.AddWithValue("@a13", txt_taxcode.Text);
                        cmd1.Parameters.AddWithValue("@a14", string.IsNullOrEmpty(txt_openfcy.Text) ? 0 : Convert.ToDouble(txt_openfcy.Text));
                        cmd1.Parameters.AddWithValue("@a15", txt_period.Text);
                        cmd1.Parameters.AddWithValue("@a16", txt_cmncode.Text);
                        cmd1.Parameters.AddWithValue("@a17", txt_assup.Text);
                        cmd1.Parameters.AddWithValue("@a18", chk_priceshameltax.Checked ? 1 : 0);
                        cmd1.Parameters.AddWithValue("@a19", txt_buldingno.Text);
                        cmd1.Parameters.AddWithValue("@a20", txt_street.Text);
                        cmd1.Parameters.AddWithValue("@a21", txt_site.Text);
                        cmd1.Parameters.AddWithValue("@a22", txt_city.Text);
                        cmd1.Parameters.AddWithValue("@a23", txt_cuntry.Text);
                        cmd1.Parameters.AddWithValue("@a24", txt_postalcode.Text);
                        cmd1.Parameters.AddWithValue("@a25", txt_expostal.Text);
                        cmd1.Parameters.AddWithValue("@a26", txt_otherid.Text);
                        cmd1.Parameters.AddWithValue("@a27", txt_note.Text);
                      //  cmd1.Parameters.AddWithValue("@a11", cmb_u2.Items.Count <= 0 ? 0 : Convert.ToInt32(cmb_u2.SelectedValue));
                       // cmd1.Parameters.AddWithValue("@a12", Convert.ToDecimal(txt_u2q.Text));
                       // cmd1.Parameters.AddWithValue("@a13", Convert.ToDouble(txt_u2p.Text));

                       // cmd1.Parameters.AddWithValue("@a14", cmb_u3.Items.Count <= 0 ? 0 : Convert.ToInt32(cmb_u3.SelectedValue));
                       // cmd1.Parameters.AddWithValue("@a15", Convert.ToDecimal(txt_u3q.Text));
                      //  cmd1.Parameters.AddWithValue("@a16", Convert.ToDouble(txt_u3p.Text));

                       // cmd1.Parameters.AddWithValue("@a17", cmb_u4.Items.Count <= 0 ? 0 : Convert.ToInt32(cmb_u4.SelectedValue));
                     //   cmd1.Parameters.AddWithValue("@a18", Convert.ToDecimal(txt_u4q.Text));
                      //  cmd1.Parameters.AddWithValue("@a19", Convert.ToDouble(txt_u4p.Text));
                        if (con2.State == ConnectionState.Closed)
                            con2.Open();

                        cmd1.ExecuteNonQuery();
                        con2.Close();
                        //  MessageBox.Show("add success", "ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_no.Enabled = false;
                        txt_name.Enabled = false;
                        txt_note.Enabled = false;
                        txt_mobile.Enabled = false;
                        txt_address.Enabled = false;
                        txt_opnbal.Enabled = false;
                        txt_openfcy.Enabled = false;
                        cmb_cur.Enabled = false;
                        cmb_class.Enabled = false;
                        cmb_city.Enabled = false;
                        txt_taxcode.Enabled = false;
                        chk_priceshameltax.Enabled = false;
                        txt_resperson.Enabled = false;
                        txt_maxbal.Enabled = false;
                        checkBox1.Enabled = false;

                        btn_del.Enabled = true;
                    //    checkBox1.Enabled = false;
                        btn_save.Enabled = false;
                        btn_savchng.Enabled = false;
                     //   pictureBox1.Enabled = false;
                        btn_add.Enabled = true;
                        btn_edit.Enabled = true;
                        btn_Undo.Enabled = false;
                        txt_period.Enabled = false;
                        txt_cmncode.Enabled = false;
                        txt_assup.Enabled = false;
                        btn_statmnt.Enabled = true;
                        btn_Find.Enabled = true;

                        txt_buldingno.Enabled = false;
                        txt_street.Enabled = false;
                        txt_site.Enabled = false;
                        txt_city.Enabled = false;
                        txt_cuntry.Enabled = false;
                        txt_postalcode.Enabled = false;
                        txt_expostal.Enabled = false;
                        txt_otherid.Enabled = false;
                     //   comboBox1.Enabled = false;
                     //   comboBox3.Enabled = false;

                        //if (dt3.Rows.Count == 0)
                        //{
                        /*
                            using (SqlCommand cmd2 = new SqlCommand("INSERT INTO items_bc (item_no, barcode, pack, pk_qty, price,pkorder) VALUES(@a11,@a22,@a33,@a44,@a55,@a66)", con2))
                            {


                                cmd2.Parameters.AddWithValue("@a11", txt_no.Text);
                                cmd2.Parameters.AddWithValue("@a22", txt_opnbal.Text);
                                cmd2.Parameters.AddWithValue("@a33", "1");
                                cmd2.Parameters.AddWithValue("@a44", "1");
                                cmd2.Parameters.AddWithValue("@a55", txt_address.Text);
                                cmd2.Parameters.AddWithValue("@a66", 1);
                                // cmd1.Parameters.AddWithValue("@a6", textBox6.Text);
                                //  cmd1.Parameters.AddWithValue("@a7", comboBox1.SelectedValue.ToString());
                                // cmd1.Parameters.AddWithValue("@a8", byteImage);
                                // cmd1.Parameters.AddWithValue("@a8", path);
                                // if (checkBox1.Checked == true)
                                // { cmd1.Parameters.AddWithValue("@a9", 1); }
                                //  else { cmd1.Parameters.AddWithValue("@a9", 0); }
                                con2.Open();
                                cmd2.ExecuteNonQuery();
                                con2.Close();
                           }
                        */
                        //}
                        //else
                        //{
                        //    MessageBox.Show("الباركود موجود من سابق", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //  //  con2.Close();
                        //}

                    }

                 //   dml.SqlCon_Open();
                    dml.Exec_Query_only("beld_acc_su_class_opnbal @sup_class = '" + cmb_class.SelectedValue + "', @branch_no = '" + BL.CLS_Session.brno + "'");
                   // dml.SqlCon_Close();
                    btn_add.Focus();
                }
                else { 
                    MessageBox.Show("المورد او الرقم الضريبي موجود من سابق", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con2.Close();
                }
            }
            else
            {
                MessageBox.Show("يرجى تعبئة البيانات كاملة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (BL.CLS_Session.up_editop == false)
            {
                txt_opnbal.ReadOnly = true;
                txt_openfcy.ReadOnly = true;
            }


            txt_find.Select();
           // var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\temp.txt");
            var lines2 = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\bprinter.txt");
            bprinter = lines2.First().ToString();


            string str2 = BL.CLS_Session.formkey;
            string whatever = str2.Substring(str2.IndexOf("D111") + 5, 3);
            if (whatever.Substring(0, 1).Equals("0"))
                btn_add.Visible = false;
            if (whatever.Substring(1, 1).Equals("0"))
                btn_edit.Visible = false;
            if (whatever.Substring(2, 1).Equals("0"))
                btn_del.Visible = false;

            DataTable dt = dml.SELECT_QUIRY_only_retrn_dt("select cl_no,cl_desc from suclass where cl_brno='" + BL.CLS_Session.brno + "'");
            // da.Fill(dt);


            cmb_class.DataSource = dt;
            // // metroComboBox3.DataSource = dt2;
            // //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // // metroComboBox3.DisplayMember = "a";
            cmb_class.ValueMember = "cl_no";
            cmb_class.DisplayMember = "cl_desc";

            DataTable dt2 = dml.SELECT_QUIRY_only_retrn_dt("select city_id,city_name from cites");
            // da.Fill(dt);
            /*
                        DataRow dr = dt2.NewRow();
                        dr["city_id"] = 0;
                        dr["city_name"] = "";//0

                        dt2.Rows.InsertAt(dr,0);
                        */
            cmb_city.DataSource = dt2;
            // // metroComboBox3.DataSource = dt2;
            // //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // // metroComboBox3.DisplayMember = "a";
            cmb_city.ValueMember = "city_id";
            cmb_city.DisplayMember = "city_name";

            //   cmb_city.Items.Insert(0, "< Select city >");
            cmb_city.SelectedIndex = -1;

            dtcur = dml.SELECT_QUIRY_only_retrn_dt("select curcode,curname,curlname,currate from Crncy order by curcode");
            cmb_cur.DataSource = dtcur;
            cmb_cur.DisplayMember = "curname";
            cmb_cur.ValueMember = "curcode";
            cmb_cur.SelectedIndex = -1;

            Fill_Data(sup_id);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //comboBox1.Items.Clear();

            txt_no.Enabled = true;
            txt_name.Enabled = true;
            txt_note.Enabled = true;
            txt_mobile.Enabled = true;
            txt_address.Enabled = true;
            txt_opnbal.Enabled = true;
            txt_openfcy.Enabled = true;
            cmb_cur.Enabled = true;
            txt_maxbal.Enabled = true;
            txt_resperson.Enabled = true;
            txt_taxcode.Enabled = true;
            chk_priceshameltax.Enabled = true;
            cmb_city.Enabled = true;
            checkBox1.Enabled = true;
            cmb_class.Enabled = true;
            txt_period.Enabled = true;
            btn_Undo.Enabled = true;
            txt_cmncode.Enabled = true;
            txt_assup.Enabled = true;
            txt_buldingno.Enabled = true;
            txt_street.Enabled = true;
            txt_site.Enabled = true;
            txt_city.Enabled = true;
            txt_cuntry.Enabled = true;
            txt_postalcode.Enabled = true;
            txt_expostal.Enabled = true;
            txt_otherid.Enabled = true;
           // comboBox3.Enabled = true;
           // checkBox1.Enabled = true;
           // comboBox1.Enabled = true;
          //  pictureBox1.Enabled = true;

            txt_name.Focus();

            txt_no.Text = "";
            txt_name.Text = "";
            txt_note.Text = "";
            txt_mobile.Text = "";
            txt_address.Text = "";
            txt_resperson.Text = "";
           // txt_
            txt_opnbal.Text = "0.00";
            txt_maxbal.Text = "0.00";
            txt_cmncode.Text = "";
            txt_taxcode.Text = "";
            txt_buldingno.Text = "";
            txt_street.Text = "";
            txt_site.Text = "";
            txt_city.Text = "";
            txt_cuntry.Text = "";
            txt_postalcode.Text = "";
            txt_expostal.Text = "";
            txt_otherid.Text = "";
           // textBox6.Text = "";
          //  comboBox1.SelectedIndex = -1;
           // cmb_class.SelectedIndex = -1;
            cmb_city.SelectedIndex = -1;
          //  pictureBox1.Image = Properties.Resources.background_button;



            btn_add.Enabled = false;
            btn_edit.Enabled = false;
            btn_Find.Enabled = false;
            btn_del.Enabled = false;
            btn_statmnt.Enabled = false;
            btn_save.Enabled = true;

           // SqlDataAdapter da = new SqlDataAdapter("select group_name,group_id from groups",con2);
////            DataTable dt = dml.SELECT_QUIRY_only_retrn_dt("select cl_no,cl_desc from suclass where cl_brno='"+BL.CLS_Session.brno+"'");
////           // da.Fill(dt);

           
////            cmb_class.DataSource = dt;
////           // // metroComboBox3.DataSource = dt2;
////           // //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
////           // // metroComboBox3.DisplayMember = "a";
////            cmb_class.ValueMember = "cl_no";
////            cmb_class.DisplayMember = "cl_desc";

////            DataTable dt2 = dml.SELECT_QUIRY_only_retrn_dt("select city_id,city_name from cites");
////            // da.Fill(dt);
/////*
////            DataRow dr = dt2.NewRow();
////            dr["city_id"] = 0;
////            dr["city_name"] = "";//0
          
////            dt2.Rows.InsertAt(dr,0);
////            */
////            cmb_city.DataSource = dt2;
////            // // metroComboBox3.DataSource = dt2;
////            // //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
////            // // metroComboBox3.DisplayMember = "a";
////            cmb_city.ValueMember = "city_id";
////            cmb_city.DisplayMember = "city_name";

////         //   cmb_city.Items.Insert(0, "< Select city >");
////            cmb_city.SelectedIndex = -1;

           // DataTable dtm = dml.SELECT_QUIRY_only_retrn_dt("select isnull(max(cast(su_no as int)),0)+1 from suppliers where su_brno='" + BL.CLS_Session.brno + "'");
            DataTable dtm = dml.SELECT_QUIRY_only_retrn_dt("select isnull(max(cast(su_no as int)),0)+1 from suppliers");
            txt_no.Text = dtm.Rows[0][0].ToString();
            isupdate = false;
            isnew = true;
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
            var frm2 = new Search_All("2", "Cus");
            frm2.ShowDialog();
            txt_find.Text = Convert.ToString(frm2.dataGridView1.CurrentRow.Cells[0].Value);
            txt_find.Focus();
            //this.Close();
            
            //Item_Srch its = new Item_Srch();
            ////its.Parent = this;
            //its.Show();
        }


        public void button4_Click(object sender, EventArgs e)
        {

           // 

            try
            {
                // dataGridView1.ReadOnly = false;
                var srr = new Search_All("7", "Acc");
                srr.ShowDialog();

                Fill_Data(srr.dataGridView1.CurrentRow.Cells["sn"].Value.ToString());
                //  dataGridView1_CellLeave(null, null);
                // total();

            }
            catch { }
        }

        private void Fill_Data(string cuno)
        {

            try
            {

                //  comboBox1.DataSource = null;
              //  cmb_class.DataSource = null;

                if (cuno != "")
                {


                    SqlDataAdapter da = new SqlDataAdapter("select * from suppliers where su_no=" + cuno + " and su_brno='" + BL.CLS_Session.brno + "'", con2);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count != 0)
                    {
                        txt_no.Text = dt.Rows[0]["su_no"].ToString();
                        txt_name.Text = dt.Rows[0]["su_name"].ToString();
                        txt_note.Text = dt.Rows[0]["su_note"].ToString();
                        txt_mobile.Text = dt.Rows[0]["su_tel"].ToString();
                        txt_address.Text = dt.Rows[0]["su_addrs"].ToString();
                        txt_opnbal.Text = dt.Rows[0]["su_opnbal"].ToString();
                        txt_resperson.Text = dt.Rows[0]["su_cntactp"].ToString();
                        txt_taxcode.Text = dt.Rows[0]["vndr_taxcode"].ToString();
                        txt_maxbal.Text = dt.Rows[0]["su_crlmt"].ToString();
                        checkBox1.Checked = Convert.ToBoolean(dt.Rows[0]["inactive"]) ? true : false;
                        txt_openfcy.Text = dt.Rows[0]["su_opnfcy"].ToString();
                        txt_period.Text = dt.Rows[0]["su_pymnt"].ToString();
                        cmb_cur.SelectedValue = dt.Rows[0]["su_cur"].ToString();
                        txt_cmncode.Text = dt.Rows[0]["cmncode"].ToString();
                        txt_assup.Text = dt.Rows[0]["whno"].ToString();
                        chk_priceshameltax.Checked = Convert.ToBoolean(dt.Rows[0]["is_shamel_tax"]) ? true : false;

                        txt_buldingno.Text = dt.Rows[0]["s_bulding_no"].ToString();
                        txt_street.Text = dt.Rows[0]["s_street"].ToString();
                        txt_site.Text = dt.Rows[0]["s_site_name"].ToString();
                        txt_city.Text = dt.Rows[0]["s_city"].ToString();
                        txt_cuntry.Text = dt.Rows[0]["s_cuntry"].ToString();
                        txt_postalcode.Text = dt.Rows[0]["s_postal_code"].ToString();
                        txt_expostal.Text = dt.Rows[0]["s_ex_postalcode"].ToString();
                        txt_otherid.Text = dt.Rows[0]["s_other_id"].ToString();
                        /*
                        if (dt.Rows[0][9] is DBNull)
                        {
                           // pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\simple-blue-opera-background-button.jpg");
                          // ok   pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\background-button.png");
                      //      pictureBox1.Image = Properties.Resources.background_button;
                            
                        }
                        else
                        {
                            //////byte[] image = (byte[])dt.Rows[0][9];
                            //////MemoryStream ms = new MemoryStream(image);
                            //////pictureBox1.Image = Image.FromStream(ms);

                            ////////pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + dt.Rows[0][9]);
                            ////////path = dt.Rows[0][9].ToString();
                            //if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + dt.Rows[0][9]))
                            //{
                            //    pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + dt.Rows[0][9]);
                               
                            //}
                            //else
                            //{
                            //    pictureBox1.Image = Properties.Resources.background_button;
                                
                            //}


                            //path = dt.Rows[0][9].ToString();

                        }
                         */
                        //if (Convert.ToInt32(dt.Rows[0][10]) == 1)
                        //{
                        //    checkBox1.Checked = true;
                        //}
                        //else { checkBox1.Checked = false; }

                        /*
                        string tmp = dt.Rows[0]["su_city"].ToString();
                        SqlDataAdapter da2 = new SqlDataAdapter("select city_name from cites where city_id=" + Convert.ToInt32(tmp) + "", con2);
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);
                        */
                        // comboBox1.Items.Clear();
                        //comboBox1.SelectedIndex = -1;

                        // comboBox1.SelectedValue = null;
                        // comboBox1.Items.Clear();


                        //   comboBox1.Items.Add(dt2.Rows[0][0].ToString());
                        //   comboBox1.SelectedIndex = comboBox1.FindStringExact(dt2.Rows[0][0].ToString());

                        //  string tmp2 = dt.Rows[0][11].ToString();
                        //  SqlDataAdapter da4 = new SqlDataAdapter("select group_name from groups where group_id=" + Convert.ToInt32(tmp) + "", con2);
                        //  DataTable dt4 = dml.SELECT_QUIRY_only_retrn_dt("select tax_name from taxs where tax_id=" + Convert.ToInt32(tmp2) + "");
                        //  da2.Fill(dt2);

                        // comboBox1.Items.Clear();
                        //comboBox1.SelectedIndex = -1;

                        // comboBox1.SelectedValue = null;
                        // comboBox1.Items.Clear();


                        //    comboBox3.Items.Add(dt4.Rows[0][0].ToString());
                        //    comboBox3.SelectedIndex = comboBox3.FindStringExact(dt4.Rows[0][0].ToString());

                        ////SqlDataAdapter da3 = new SqlDataAdapter("select city_id,city_name from cites", con2);
                        ////DataTable dt3 = new DataTable();
                        ////da3.Fill(dt3);

                        ////cmb_city.DataSource = dt3;
                        ////cmb_city.DisplayMember = "city_name";
                        ////cmb_city.ValueMember = "city_id";

                        ////if (string.IsNullOrEmpty(dt.Rows[0]["su_city"].ToString()))
                        ////    cmb_city.SelectedIndex = -1;
                        ////else
                        ////    cmb_city.SelectedValue = dt.Rows[0]["su_city"].ToString();

                        ////SqlDataAdapter da4 = new SqlDataAdapter("select cl_no,cl_desc from suclass where cl_brno=" + BL.CLS_Session.brno + "", con2);
                        ////DataTable dt4 = new DataTable();
                        ////da4.Fill(dt4);

                        ////cmb_class.DataSource = dt4;
                        ////cmb_class.DisplayMember = "cl_desc";
                        ////cmb_class.ValueMember = "cl_no";

                        ////if (string.IsNullOrEmpty(dt.Rows[0]["su_class"].ToString()))
                        ////    cmb_class.SelectedIndex = -1;
                        ////else
                        ////    cmb_class.SelectedValue = dt.Rows[0]["su_class"].ToString();
                        cmb_city.SelectedValue = dt.Rows[0]["su_city"].ToString();
                        cmb_class.SelectedValue = dt.Rows[0]["su_class"].ToString();
                        // comboBox2.Items.Add(dt3.Rows[0][0].ToString());
                        //  comboBox2.SelectedIndex = comboBox2.FindStringExact(dt3.Rows[0][1].ToString());
                        // comboBox2.SelectedValue = dt.Rows[0][5].ToString();
                        // comboBox1.Items.Clear();
                        //  comboBox1.Items.Add(dt.Rows[0][8].ToString());
                        //  comboBox1.SelectedIndex = comboBox1.FindStringExact(dt.Rows[0][8].ToString());
                        txt_find.Text = "";
                        btn_edit.Enabled = true;
                        //btn_bar.Enabled = true;
                        //button6.Enabled = true;
                        btn_del.Enabled = true;
                        btn_statmnt.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("لايوجد مورد بهذا الرقم", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_find.Text = "";
                    }
                }
                else
                {
                  //  MessageBox.Show("يرجى ادخال رقم المورد", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
              //  button4_Click((object)sender, (EventArgs)e);
                Fill_Data(txt_find.Text.Trim());
            }
        }

        public void button5_Click(object sender, EventArgs e)
        {
            bc = txt_opnbal.Text;
           // string ss = comboBox1.Text.ToString();
            string ss2 = cmb_class.Text.ToString();
          //  string ss3 = comboBox3.Text.ToString();
            txt_no.Enabled = false;
            txt_name.Enabled = true;
            txt_note.Enabled = true;
            txt_mobile.Enabled = true;
            txt_address.Enabled = true;
            txt_opnbal.Enabled = true;
            txt_openfcy.Enabled = true;
            cmb_cur.Enabled = true;
            txt_resperson.Enabled = true;
            txt_taxcode.Enabled = true;
            chk_priceshameltax.Enabled = true;
            txt_maxbal.Enabled = true;
            cmb_class.Enabled = true;
            cmb_city.Enabled = true;
            checkBox1.Enabled = true;
            txt_period.Enabled = true;
           // comboBox2.Enabled = true;
            btn_add.Enabled = false;
            btn_edit.Enabled = false;
            btn_del.Enabled = false;
            btn_save.Enabled = false;
            btn_savchng.Enabled = true;
            btn_Undo.Enabled = true;
            txt_cmncode.Enabled = true;
            txt_assup.Enabled = true;

            txt_buldingno.Enabled = true;
            txt_street.Enabled = true;
            txt_site.Enabled = true;
            txt_city.Enabled = true;
            txt_cuntry.Enabled = true;
            txt_postalcode.Enabled = true;
            txt_expostal.Enabled = true;
            txt_otherid.Enabled = true;
          //  checkBox1.Enabled = true;
         //   comboBox1.Enabled = true;
          //  comboBox3.Enabled = true;
          //  pictureBox1.Enabled = true;
            /*
            SqlDataAdapter da = new SqlDataAdapter("select group_id,group_name from groups", con2);
            DataTable dt = new DataTable();
            da.Fill(dt);


          //  comboBox1.DataSource = dt;
            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
          //  comboBox1.DisplayMember = "group_name";
          //  comboBox1.ValueMember = "group_id";

           // comboBox1.Items.Add(dt.Rows[0][0].ToString());
           // comboBox1.SelectedIndex = comboBox1.FindStringExact(ss);

            SqlDataAdapter da2 = new SqlDataAdapter("select unit_id,unit_name from units", con2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);


            cmb_class.DataSource = dt2;
            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
            cmb_class.DisplayMember = "unit_name";
            cmb_class.ValueMember = "unit_id";

            // comboBox1.Items.Add(dt.Rows[0][0].ToString());
            cmb_class.SelectedIndex = cmb_class.FindStringExact(ss2);

          //  SqlDataAdapter da2 = new SqlDataAdapter("select unit_id,unit_name from units", con2);
          //  DataTable dt2 = new DataTable();
          //  da2.Fill(dt2);


         //   comboBox3.DataSource = dml.SELECT_QUIRY_only_retrn_dt("select tax_id,tax_name from taxs");
            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
         //   comboBox3.DisplayMember = "tax_name";
         //   comboBox3.ValueMember = "tax_id";

            // comboBox1.Items.Add(dt.Rows[0][0].ToString());
         //   comboBox3.SelectedIndex = comboBox3.FindStringExact(ss3);

            if (BL.CLS_Session.up_ch_itmpr == false)
            {
               txt_address.ReadOnly = true;
            }
            */
            isupdate = true;
            isnew = false;
            btn_save.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
      //      MessageBox.Show(comboBox3.SelectedValue.ToString());
            if (txt_no.Text != "" && txt_name.Text != "" && cmb_class.Text != "")
            {
                MemoryStream ms = new MemoryStream();
     //           pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] byteImage = ms.ToArray();

               // con2.Open();
                // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
                using (SqlCommand cmd1 = new SqlCommand("update suppliers set  su_name=@a2, su_class=@a3, su_addrs=@a4, su_tel=@a5,su_cntactp=@a6,su_crlmt=@a7,su_opnbal=@a8,su_city=@a9,inactive=@a10,su_cur=@a11,su_opnfcy=@a12,su_pymnt=@a13,vndr_taxcode=@a14,cmncode=@a15,whno=@a16,is_shamel_tax=@a17,s_bulding_no=@a18, s_street=@a19, s_site_name=@a20, s_city=@a21, s_cuntry=@a22, s_postal_code=@a23, s_ex_postalcode=@a24, s_other_id=@a25,su_note=@a26 where su_no=@a1 and su_brno='" + BL.CLS_Session.brno + "'", con2))
                {


                    cmd1.Parameters.AddWithValue("@a1", txt_no.Text);
                    cmd1.Parameters.AddWithValue("@a2", txt_name.Text);
                    cmd1.Parameters.AddWithValue("@a3", cmb_class.SelectedValue);
                    cmd1.Parameters.AddWithValue("@a4", txt_address.Text);
                    cmd1.Parameters.AddWithValue("@a5", txt_mobile.Text);
                    cmd1.Parameters.AddWithValue("@a6", txt_resperson.Text);
                    cmd1.Parameters.AddWithValue("@a7", string.IsNullOrEmpty(txt_maxbal.Text) ? 0 : Convert.ToDouble(txt_maxbal.Text));
                   // cmd1.Parameters.AddWithValue("@a8", byteImage);
                    cmd1.Parameters.AddWithValue("@a8", string.IsNullOrEmpty(txt_opnbal.Text) ? 0 : Convert.ToDouble(txt_opnbal.Text));
                    cmd1.Parameters.AddWithValue("@a9", cmb_city.SelectedIndex == -1 ? "" : cmb_city.SelectedValue);
                    cmd1.Parameters.AddWithValue("@a10", checkBox1.Checked ? 1 : 0);
                    cmd1.Parameters.AddWithValue("@a11", cmb_cur.SelectedIndex == -1 ? "" : cmb_cur.SelectedValue);
                    cmd1.Parameters.AddWithValue("@a12",string.IsNullOrEmpty(txt_openfcy.Text)? 0 : Convert.ToDouble(txt_openfcy.Text));
                    cmd1.Parameters.AddWithValue("@a13", txt_period.Text);
                    cmd1.Parameters.AddWithValue("@a14", txt_taxcode.Text);
                    cmd1.Parameters.AddWithValue("@a15", txt_cmncode.Text);
                    cmd1.Parameters.AddWithValue("@a16", txt_assup.Text);
                    cmd1.Parameters.AddWithValue("@a17", chk_priceshameltax.Checked ? 1 : 0);
                    cmd1.Parameters.AddWithValue("@a18", txt_buldingno.Text);
                    cmd1.Parameters.AddWithValue("@a19", txt_street.Text);
                    cmd1.Parameters.AddWithValue("@a20", txt_site.Text);
                    cmd1.Parameters.AddWithValue("@a21", txt_city.Text);
                    cmd1.Parameters.AddWithValue("@a22", txt_cuntry.Text);
                    cmd1.Parameters.AddWithValue("@a23", txt_postalcode.Text);
                    cmd1.Parameters.AddWithValue("@a24", txt_expostal.Text);
                    cmd1.Parameters.AddWithValue("@a25", txt_otherid.Text);
                    cmd1.Parameters.AddWithValue("@a26", txt_note.Text);
          //          if (checkBox1.Checked == true)
                   //{ cmd1.Parameters.AddWithValue("@a9", 1); }
                   // else { cmd1.Parameters.AddWithValue("@a9", 0); }
       //             cmd1.Parameters.AddWithValue("@a10", comboBox3.SelectedValue.ToString());
                    if (con2.State == ConnectionState.Closed)
                        con2.Open();
                    cmd1.ExecuteNonQuery();
                    con2.Close();
                   // MessageBox.Show("modifed success", "ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_no.Enabled = false;
                    txt_name.Enabled = false;
                    txt_note.Enabled = false;
                    txt_mobile.Enabled = false;
                    txt_address.Enabled = false;
                    txt_opnbal.Enabled = false;
                    txt_openfcy.Enabled = false;
                    cmb_cur.Enabled = false;
                    cmb_class.Enabled = false;
                    cmb_city.Enabled = false;
                    txt_taxcode.Enabled = false;
                    chk_priceshameltax.Enabled = false;
                    txt_resperson.Enabled = false;
                    txt_maxbal.Enabled = false;
                    checkBox1.Enabled = false;
         //           comboBox3.Enabled = false;
        //            checkBox1.Enabled = false;
                    btn_save.Enabled = false;
                    btn_add.Enabled = true;
                    btn_del.Enabled = true;
                    btn_savchng.Enabled = false;
                    btn_edit.Enabled = true;
                    btn_Undo.Enabled = false;
                    txt_period.Enabled = false;
                    txt_cmncode.Enabled = false;
                    txt_assup.Enabled = false;
                    btn_statmnt.Enabled = true;
                    btn_Find.Enabled = true;

                    txt_buldingno.Enabled = false;
                    txt_street.Enabled = false;
                    txt_site.Enabled = false;
                    txt_city.Enabled = false;
                    txt_cuntry.Enabled = false;
                    txt_postalcode.Enabled = false;
                    txt_expostal.Enabled = false;
                    txt_otherid.Enabled = false;
          //          comboBox1.Enabled = false;
          //          pictureBox1.Enabled = false;
                    /*
                    using (SqlCommand cmd2 = new SqlCommand("update items_bc set barcode=@a11,price=@a22 where item_no=@a33 and barcode=@a44", con2))
                    {


                        cmd2.Parameters.AddWithValue("@a11", txt_opnbal.Text);
                        cmd2.Parameters.AddWithValue("@a22", txt_address.Text);
                        cmd2.Parameters.AddWithValue("@a33", txt_no.Text);
                        cmd2.Parameters.AddWithValue("@a44", bc);
                        //cmd2.Parameters.AddWithValue("@a55", textBox4.Text);
                        // cmd1.Parameters.AddWithValue("@a6", textBox6.Text);
                        //  cmd1.Parameters.AddWithValue("@a7", comboBox1.SelectedValue.ToString());
                        // cmd1.Parameters.AddWithValue("@a8", byteImage);
                        // cmd1.Parameters.AddWithValue("@a8", path);
                        // if (checkBox1.Checked == true)
                        // { cmd1.Parameters.AddWithValue("@a9", 1); }
                        //  else { cmd1.Parameters.AddWithValue("@a9", 0); }
                        con2.Open();
                        cmd2.ExecuteNonQuery();
                        con2.Close();
                    }
                  //  comboBox1.Items.Clear();
                     */
                }
               // dml.SqlCon_Open();
                dml.Exec_Query_only("beld_acc_su_class_opnbal @sup_class = '" + cmb_class.SelectedValue + "', @branch_no = '" + BL.CLS_Session.brno + "'");
               // dml.SqlCon_Close();
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(bprinter);
           
            // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            // string str = "SELECT hdr.ref, hdr.total, hdr.count, hdr.date, dtl.barcod, dtl.name, dtl.price, dtl.unit FROM hdr INNER JOIN dtl ON hdr.ref = dtl.ref WHERE (hdr.ref = (SELECT MAX(ref) AS Expr1 FROM  hdr AS hdr_1))";
            // SqlDataAdapter dacr = new SqlDataAdapter(str , con);

            if (txt_no.Text != "")
            {
                if (txt_maxbal.Text == "")
                {
                    txt_maxbal.Text = "1";
                    SqlDataAdapter da2 = new SqlDataAdapter("select * from items where item_no ='" + txt_no.Text + "'", con2);
                    //"item_no like '%" + textBox1.Text + "%' or item_name like '%"+textBox1.Text+"%'"
                    // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from hdr where ref=(select max(ref) from hdr)", con);
                    // DataSet1 ds = new DataSet1();
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    // dacr.Fill(ds, "dtl");
                    // dacr1.Fill(ds, "hdr");


                    Pos.rpt.BarcodeReport report = new Pos.rpt.BarcodeReport();
                    report.SetDataSource(dt2);
                    report.SetParameterValue("cmp_name", POS.BL.CLS_Session.cmp_name);
                    // report.ReportDefinition.Areas.

                    // crystalReportViewer1.ReportSource = report;

                    // crystalReportViewer1.Refresh();

                    report.PrintOptions.PrinterName = bprinter;




                    //int xxx = Convert.ToInt32(textBox8.Text);
                    //for (int i = 1; i <= xxx; i++)
                    //{
                        report.PrintToPrinter(1, true, 1, 1);
                        // report.PrintToPrinter(1, false, 1, 1);
                        txt_maxbal.Text = "";
                    //}
                    
                }
                else
                {
                    
                    SqlDataAdapter da2 = new SqlDataAdapter("select * from items where item_no ='" + txt_no.Text + "'", con2);
                    
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    // dacr.Fill(ds, "dtl");
                    // dacr1.Fill(ds, "hdr");


                    Pos.rpt.BarcodeReport report = new Pos.rpt.BarcodeReport();
                    report.SetDataSource(dt2);
                    // report.ReportDefinition.Areas.

                    // crystalReportViewer1.ReportSource = report;

                    // crystalReportViewer1.Refresh();

                    report.PrintOptions.PrinterName = bprinter;
                    int xxx = Convert.ToInt32(txt_maxbal.Text);
                    for (int i = 1; i <= xxx; i++)
                    {
                        report.PrintToPrinter(1, true, 1, 1);
                        // report.PrintToPrinter(1, false, 1, 1);
                        
                    }
                }
            }
            else

            {
                MessageBox.Show("يرجى اختار صنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
           // InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("EN"));
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
           // InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("EN"));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          //  txt_opnbal.Text = txt_no.Text;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (txt_no.Text == "")
            {
                MessageBox.Show("لا يوجد مورد لحذفة يرجى اختار المورد", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //SqlDataAdapter da10 = new SqlDataAdapter("select * from acc_dtl where su_no ='" + txt_no.Text + "' and a_brno='" + BL.CLS_Session.brno + "'", con2);

                //DataTable dt10 = new DataTable();
                //da10.Fill(dt10);
                //if (dt10.Rows.Count != 0 && (Convert.ToDouble(txt_opnbal.Text) != 0 || Convert.ToDouble(txt_openfcy.Text) != 0))
                //{
                DataTable dt1 = dml.SELECT_QUIRY_only_retrn_dt("select count(*) from acc_dtl where su_no='" + txt_no.Text + "' and a_brno='" + BL.CLS_Session.brno + "'");
                DataTable dt2 = dml.SELECT_QUIRY_only_retrn_dt("select count(*) from suppliers where su_no='" + txt_no.Text + "' and su_brno='" + BL.CLS_Session.brno + "' and su_opnbal<>0");

                if (Convert.ToInt32(dt1.Rows[0][0]) != 0 || Convert.ToDouble(txt_opnbal.Text) != 0 || Convert.ToInt32(dt1.Rows[0][0]) != 0 || Convert.ToDouble(txt_openfcy.Text) != 0)
                {
                    MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "can't delete Supplier have transaction or balance" : "لا يمكن حذف مورد تمت عليه حركة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("هل تريد حذف هذا المورد ؟", "Confirmation", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string xx = txt_no.Text.ToString();
                        
                        //...
                        using (SqlCommand cmd10 = new SqlCommand("delete from suppliers where su_no='" + xx + "' and su_brno='" + BL.CLS_Session.brno + "'", con2))
                        {


                            con2.Open();
                            cmd10.ExecuteNonQuery();
                            con2.Close();
                            MessageBox.Show("تم مسح المورد " + xx);
                            txt_no.Text = "";
                            txt_name.Text = "";
                            txt_note.Text = "";
                            txt_mobile.Text = "";
                            txt_address.Text = "";
                            txt_opnbal.Text = "";
                            cmb_class.SelectedIndex = -1;
                            cmb_city.SelectedIndex = -1;
                            txt_maxbal.Text = "";
                            txt_resperson.Text = "";
                            txt_taxcode.Text = "";
                            checkBox1.Checked = false;
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
            if (txt_find.Text != "")
            {

               
                SqlCommand cmd = new SqlCommand("Select_Product",con2);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "Select_Product";
                cmd.Parameters.Add("@no", SqlDbType.NVarChar, 16).Value = txt_find.Text;
                cmd.Parameters.Add("@barcode", SqlDbType.NVarChar, 16).Value = "333";
                

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

               if (dt.Rows.Count != 0)
                {
                    txt_no.Text = dt.Rows[0][0].ToString();
                    txt_name.Text = dt.Rows[0][1].ToString();
                    txt_mobile.Text = dt.Rows[0][2].ToString();
                    txt_address.Text = dt.Rows[0][3].ToString();
                    txt_opnbal.Text = dt.Rows[0][4].ToString();
                    cmb_class.Text = dt.Rows[0][5].ToString();
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
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select group_id,group_name from groups", con2);
            DataTable dt = new DataTable();
            da.Fill(dt);


            ////comboBox1.DataSource = dt;
            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
            ////comboBox1.DisplayMember = "group_name";
            ////comboBox1.ValueMember = "group_id";
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
               // pictureBox1.Image = Image.FromFile(ofd.FileName);
                path = Path.GetFileName(ofd.FileName);

                string fullpath = Path.GetDirectoryName(ofd.FileName);

               // MessageBox.Show(fullpath + "\\" + path + "\n" + "\n" + Directory.GetCurrentDirectory() + "\\images\\" + path);
                if (!File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + path))
                {
                    File.Copy(fullpath + "\\" + path , Directory.GetCurrentDirectory() + "\\images\\" + path, true);
                //    //  Console.WriteLine("The file exists.");
                }
                //else
                //{ 
                
                //}
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
            //Sto.Items_Bc fbc = new Sto.Items_Bc(txt_no.Text);
            //fbc.MdiParent = ParentForm;
            //fbc.Show();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_name.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_class.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_address.Focus();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_opnbal.Focus();
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_cur.Focus();
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    comboBox1.Focus();
            //}
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    comboBox3.Focus();
            //}
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
        //    /*
        //    SqlDataAdapter da = new SqlDataAdapter("select unit_id,unit_name from units", con2);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);


        //    comboBox2.DataSource = dt;
        //     */
        //    dtunits = dml.SELECT_QUIRY_only_retrn_dt("select unit_id,unit_name from units");
            

        //    var dv1 = new DataView();
        //    dv1 = dtunits.DefaultView;
        //    // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
        //  //  dv1.RowFilter = "unit_id not in(" + comboBox2.SelectedValue + ")";
        //    DataTable dtNew = dv1.ToTable();
        //    // MessageBox.Show(dtNew.Rows[0][1].ToString());
        //    cmb_class.DataSource = dtNew;

        //    // metroComboBox3.DataSource = dt2;
        //    //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
        //    // metroComboBox3.DisplayMember = "a";
        //    //  comboBox1.ValueMember = comboBox1.Text;
        //    cmb_class.DisplayMember = "unit_name";
        //    cmb_class.ValueMember = "unit_id";
        //    //comboBox1.Items.Clear();
        //    //comboBox1.Items.Add(dt.Rows[0][8].ToString());
        //    //comboBox1.SelectedIndex = comboBox1.FindStringExact(dt.Rows[0][8].ToString());
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_mobile.Focus();
            }
        }

        private void comboBox3_Enter(object sender, EventArgs e)
        {
            
          // // DataTable dt = dml.SELECT_QUIRY("select tax_id,tax_name from taxs");


          ////  dtunits =
          //  comboBox3.DataSource = dml.SELECT_QUIRY_only_retrn_dt("select tax_id,tax_name from taxs");
          //  // metroComboBox3.DataSource = dt2;
          //  //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
          //  // metroComboBox3.DisplayMember = "a";
          //  //  comboBox1.ValueMember = comboBox1.Text;
          //  comboBox3.DisplayMember = "tax_name";
          //  comboBox3.ValueMember = "tax_id";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_u2_Enter(object sender, EventArgs e)
        {
           // MessageBox.Show(comboBox2.SelectedValue.ToString());
          //  string qury =  ;
            /*
           cmb_u2.DataSource = dml.SELECT_QUIRY_only_retrn_dt("select unit_id,unit_name from units where unit_id not in(" + comboBox2.SelectedValue+")");
             */
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
            var dv1 = new DataView();
            dv1=dtunits.DefaultView;
            // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
            dv1.RowFilter = "unit_id not in(" + cmb_class.SelectedValue + ")";
            DataTable dtNew = dv1.ToTable();
            //// MessageBox.Show(dtNew.Rows[0][1].ToString());
            //cmb_u2.DataSource = dtNew;

            //cmb_u2.DisplayMember = "unit_name";
            //cmb_u2.ValueMember = "unit_id";
        }

        private void comboBox4_Enter(object sender, EventArgs e)
        {
           
        }

        private void cmb_u3_Enter(object sender, EventArgs e)
        {
       //     try{/*
       //     cmb_u3.DataSource = dml.SELECT_QUIRY_only_retrn_dt("select unit_id,unit_name from units where unit_id not in(" + comboBox2.SelectedValue + "," + cmb_u2.SelectedValue + ")");
       //*/
       //         var dv1 = new DataView();
       //         dv1 = dtunits.DefaultView;
       //         // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
       //         dv1.RowFilter = "unit_id not in(" + cmb_class.SelectedValue + "," + cmb_u2.SelectedValue + ")";
       //         DataTable dtNew = dv1.ToTable();
       //         // MessageBox.Show(dtNew.Rows[0][1].ToString());
       //         cmb_u3.DataSource = dtNew;

       //     cmb_u3.DisplayMember = "unit_name";
       //     cmb_u3.ValueMember = "unit_id";
       //      }
       //     catch { }
        }

        private void cmb_u4_Enter(object sender, EventArgs e)
        {
            try
            {
                /*
                cmb_u4.DataSource = dml.SELECT_QUIRY_only_retrn_dt("select unit_id,unit_name from units where unit_id not in(" + comboBox2.SelectedValue + "," + cmb_u2.SelectedValue + "," + cmb_u3.SelectedValue + ")");
                */
                //var dv1 = new DataView();
                //dv1 = dtunits.DefaultView;
                //// dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                //dv1.RowFilter = "unit_id not in(" + cmb_class.SelectedValue + "," + cmb_u2.SelectedValue + "," + cmb_u3.SelectedValue + ")";
                //DataTable dtNew = dv1.ToTable();
                //// MessageBox.Show(dtNew.Rows[0][1].ToString());
                //cmb_u4.DataSource = dtNew;

                //cmb_u4.DisplayMember = "unit_name";
                //cmb_u4.ValueMember = "unit_id";
                 
            }
            catch { }
        }

        private void cmb_u2_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete)
            //{
            //    cmb_u2.DataSource = null;
            //   // cmb_u2.SelectedValue = null;
            //}
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txt_u2q.Focus();
            //}
        }

        private void cmb_u3_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete)
            //{
            //    cmb_u3.DataSource = null;
            //    // cmb_u2.SelectedValue = null;
            //}
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txt_u3q.Focus();
            //}
        }

        private void cmb_u4_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete)
            //{
            //   // cmb_u4.Items.Clear();
            //    cmb_u4.DataSource = null;
            //}
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txt_u4q.Focus();
            //}
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
            //if (string.IsNullOrEmpty(txt_u3p.Text))
            //    txt_u3p.Text = "0";
        }

        private void txt_u2q_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txt_u2p.Focus();
            //}
        }

        private void txt_u3q_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txt_u3p.Focus();
            //}
        }

        private void txt_u4q_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txt_u4p.Focus();
            //}
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    cmb_u2.Focus();
            //}
        }

        private void txt_u2p_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    cmb_u3.Focus();
            //}
        }

        private void txt_u3p_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    cmb_u4.Focus();
            //}
        }

        private void cmb_city_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_city.SelectedIndex = -1;
            }
        }

        private void btn_Undo_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("هل تريد التراجع عن التغييرات", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {



                if (isupdate)
                {
                    Fill_Data(txt_find.Text.Trim());
                    // btn_Save_Click(sender, e);
                    /*
                    Fill_Data(cmb_salctr.SelectedValue.ToString(), comboBox1.SelectedValue.ToString(), txt_ref.Text);
                    btn_Save.Enabled = false;
                    btn_Add.Enabled = true;
                    btn_Undo.Enabled = false;
                    btn_Exit.Enabled = true;
                    btn_Find.Enabled = true;
                    btn_Edit.Enabled = true;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;
                    comboBox1.Enabled = false;
                    txt_ref.Enabled = false;
                    txt_remark.Enabled = false;
                    txt_key.Enabled = false;
                    */
                    foreach (Control ctrl in groupBox1.Controls)
                    {
                        ctrl.Enabled = false;
                    }
                    txt_find.Enabled = true;
                    btn_del.Enabled = true;
                     btn_add.Enabled = true;
                    btn_Find.Enabled = true;
                    btn_edit.Enabled = true;
                    btn_Undo.Enabled = false;
                    btn_statmnt.Enabled = true;
                }
                else
                {
                   // btn_save.Enabled = false;
                   // btn_add.Enabled = true;
                   // btn_Undo.Enabled = false;
                   //// btn_Exit.Enabled = true;
                   // btn_Find.Enabled = true;
                   // btn_edit.Enabled = false;

                    foreach (Control ctrl in groupBox1.Controls)
                    {
                        ctrl.Enabled = false;
                        if (ctrl is TextBox)
                            ctrl.Text = "";
                    }
                    //foreach (TextBox txt1 in groupBox1.Controls)
                    //{
                    //    txt1.Text = "";
                    //}
                    txt_opnbal.Text = "0.0";
                    txt_maxbal.Text = "0.0";

                    txt_find.Enabled = true;
                   // txt_find.Enabled = true;
                    btn_add.Enabled = true;
                    btn_save.Enabled = false;
                    btn_Find.Enabled = true;
                    btn_Undo.Enabled = false;
                    btn_statmnt.Enabled = true;
                   // btn_edit.Enabled = true;
                    /*
                    try
                    {
                        if (dataGridView1.Rows.Count > 0)
                        {
                            //  ((DataTable)dataGridView1.DataSource).Rows.Clear();
                            // dataGridView1.DataSource = null;
                            dataGridView1.Rows.Clear();
                            dataGridView1.Refresh();
                            //dataGridView1.DataSource = null;
                            //dataGridView1.Refresh();
                        }
                    }
                    catch { }
                    
                    dataGridView1.ReadOnly = true;

                    comboBox1.Enabled = false;
                    txt_ref.Enabled = false;
                    //  dataGridView1.AllowUserToAddRows = false;

                    comboBox1.SelectedIndex = -1;
                    txt_amt.Text = "0";
                    txt_desc.Text = "";
                     */

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
                    /*
                    try
                    {
                        if (dataGridView1.Rows.Count > 0)
                        {
                            ((DataTable)dataGridView1.DataSource).Rows.Clear();
                        }
                    }
                    catch { }
                    dataGridView1.AllowUserToAddRows = false;
                    */
                }


                // Acc_Tran_Load(sender,e);
                /*
                txt_mdate.Enabled = false;
                txt_hdate.Enabled = false;
                txt_desc.Enabled = false;
                txt_des.Enabled = false;
                txt_amt.Enabled = false;
                comboBox1.Enabled = false;
                cmb_salctr.Enabled = false;
                txt_ref.Enabled = false;
                */
                //...
                isnew = false;
                isupdate = false;
                btn_save.Enabled = false;
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

        private void btn_statmnt_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.formkey.Contains("D133"))
            {
                if (txt_no.Text != "")
                {
                    Acc_Statment_Exp f4a = new Acc_Statment_Exp(txt_no.Text);
                    f4a.MdiParent = MdiParent;
                    f4a.Show();
                }
            }
        }

        private void cmb_cur_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_cur.SelectedIndex = -1;
            }
            if (e.KeyCode == Keys.Enter)
            {
                txt_openfcy.Focus();
            }
        }

        private void txt_openfcy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_resperson.Focus();
            }
        }

        private void txt_maxbal_TextChanged(object sender, EventArgs e)
        {
           // datval.ValidateText_numric(txt_maxbal);
            //if (string.IsNullOrEmpty(txt_maxbal.Text))
            //    txt_maxbal.Text = "0";
        }

        private void txt_opnbal_TextChanged(object sender, EventArgs e)
        {
            //datval.ValidateText_float(txt_opnbal);
            //if (string.IsNullOrEmpty(txt_opnbal.Text))
            //    txt_opnbal.Text = "0";
        }

        private void txt_openfcy_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_openfcy.Text))
            //    txt_openfcy.Text = "0";
        }

        private void txt_period_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_period.Text))
                txt_period.Text = "0";
        }

        private void txt_opnbal_Validating(object sender, CancelEventArgs e)
        {
            datval.ValidateText_float(txt_opnbal);
        }

        private void txt_openfcy_Validating(object sender, CancelEventArgs e)
        {
            datval.ValidateText_float(txt_openfcy);
        }

        private void txt_maxbal_Validating(object sender, CancelEventArgs e)
        {
            datval.ValidateText_float(txt_maxbal);
        }

        private void txt_name_Enter(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Suppliers_Shown(object sender, EventArgs e)
        {
            if (!BL.CLS_Session.formkey.Contains(this.Tag.ToString()))
            {
                this.Close();
            }
        }
        
    }
}

