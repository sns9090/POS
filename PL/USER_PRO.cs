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



namespace POS
{
    public partial class USER_PRO : BaseForm
    {

        BL.Date_Validate datavd = new BL.Date_Validate();
        bool isupdate, isnew;
        BL.EncDec ende = new BL.EncDec();
        SqlConnection con2 = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        //public Item_Card(string itemno)
        DataTable dtslctr;
        BL.DAML daml = new BL.DAML();
        public USER_PRO()
        {

           // Form.ActiveForm.Refresh();

            InitializeComponent();

            txt_userid.Enabled = false;
            txt_username.Enabled = false;
            txt_password.Enabled = false;
            //textBox3.Enabled = false;
            //textBox4.Enabled = false;
            //textBox5.Enabled = false;
            //textBox6.Enabled = false;
            btn_save.Enabled = false;
            comboBox1.Enabled = false;
            cmb_salctr.Enabled = false;
            // button3.Enabled = false;
            button6.Enabled = false;
            chk_admin.Enabled = false;
            chk_pricemodify.Enabled = false;
            chk_edit.Enabled = false;
            chk_suspend.Enabled = false;
            chk_itmpr.Enabled = false;
            chk_post.Enabled = false;
            chk_item_desc.Enabled = false;
            txt_item_desc.Enabled = false;
            chk_inv_desc.Enabled = false;
            txt_inv_desc.Enabled = false;
            chk_edit_other.Enabled = false;
            chk_chang_date.Enabled = false;
            chk_icost_sal.Enabled = false;
            chk_inactive.Enabled = false;
            chk_stopserach.Enabled = false;
            chk_showwin.Enabled = false;
            chk_priclowcost.Enabled = false;
           // pictureBox1.Enabled = false;
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
            if (txt_password.Text.Trim().Length < 4)
            {
                MessageBox.Show("يجب ان يكون طول كلمة السر 4 ارقام او اكثر", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_password.Focus();
                return;
            }

            
            if (txt_userid.Text != "" && txt_username.Text != ""  && comboBox1.Text !="")
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

                con2.Open();
                SqlDataAdapter da2 = new SqlDataAdapter("select * from users where user_name ='" + txt_userid.Text + "'", con2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count == 0)
                {
                    // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
                    using (SqlCommand cmd1 = new SqlCommand("INSERT INTO users (user_name, full_name, password, user_type, modify_price, language,use_sal_edt,use_dsc,post_btn,chng_date,modfy_othr_tr,item_desc,inv_desc,shw_sal_icost,inactive,show_win,suspend_inv,belw_bal,sl_no,prislwcst,up_stopsrch) VALUES(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13,@a14,@a15,@a16,@a17,@a18,@a19,@a20,@a21)", con2))
                    {


                        cmd1.Parameters.AddWithValue("@a1", txt_userid.Text);
                        cmd1.Parameters.AddWithValue("@a2", txt_username.Text);
                        cmd1.Parameters.AddWithValue("@a3", ende.Encrypt(txt_password.Text, true));
                       // cmd1.Parameters.AddWithValue("@a3", Convert.ToInt32(textBox3.Text));
                       // cmd1.Parameters.AddWithValue("@a4", Convert.ToInt32(textBox4.Text));
                       // cmd1.Parameters.AddWithValue("@a5", textBox5.Text);
                      //  cmd1.Parameters.AddWithValue("@a6", textBox6.Text);
                       
                       // cmd1.Parameters.AddWithValue("@a8", byteImage);
                       // cmd1.Parameters.AddWithValue("@a8", path);
                        if (chk_admin.Checked == true)
                        { cmd1.Parameters.AddWithValue("@a4", 1); }
                        else { cmd1.Parameters.AddWithValue("@a4", 0); }

                        if (chk_pricemodify.Checked == true)
                        { cmd1.Parameters.AddWithValue("@a5", 1); }
                        else { cmd1.Parameters.AddWithValue("@a5", 0); }

                        cmd1.Parameters.AddWithValue("@a6", comboBox1.Text.ToString().Equals("English")? "E" : "ع");

                        if (chk_edit.Checked == true)
                        { cmd1.Parameters.AddWithValue("@a7", 1); }
                        else { cmd1.Parameters.AddWithValue("@a7", 0); }

                        if (chk_suspend.Checked == true)
                        { cmd1.Parameters.AddWithValue("@a8", 1); }
                        else { cmd1.Parameters.AddWithValue("@a8", 0); }

                        if (chk_post.Checked == true)
                        { cmd1.Parameters.AddWithValue("@a9", 1); }
                        else { cmd1.Parameters.AddWithValue("@a9", 0); }

                        if (chk_chang_date.Checked == true)
                        { cmd1.Parameters.AddWithValue("@a10", 1); }
                        else { cmd1.Parameters.AddWithValue("@a10", 0); }

                        if (chk_edit_other.Checked == true)
                        { cmd1.Parameters.AddWithValue("@a11", 1); }
                        else { cmd1.Parameters.AddWithValue("@a11", 0); }

                        if (chk_item_desc.Checked == true)
                        { cmd1.Parameters.AddWithValue("@a12", txt_item_desc.Text); }
                        else { cmd1.Parameters.AddWithValue("@a12", 0); }

                        if (chk_inv_desc.Checked == true)
                        { cmd1.Parameters.AddWithValue("@a13", txt_inv_desc.Text); }
                        else { cmd1.Parameters.AddWithValue("@a13", 0); }

                        if (chk_icost_sal.Checked == true)
                        { cmd1.Parameters.AddWithValue("@a14", 1); }
                        else { cmd1.Parameters.AddWithValue("@a14", 0); }

                        if (chk_inactive.Checked == true)
                        { cmd1.Parameters.AddWithValue("@a15", 1); }
                        else { cmd1.Parameters.AddWithValue("@a15", 0); }

                        if (chk_showwin.Checked == true)
                        { cmd1.Parameters.AddWithValue("@a16", 1); }
                        else { cmd1.Parameters.AddWithValue("@a16", 0); }

                        if (chk_suspend.Checked == true)
                        { cmd1.Parameters.AddWithValue("@a17", 1); }
                        else { cmd1.Parameters.AddWithValue("@a17", 0); }

                        if (chk_edit.Checked == true)
                        { cmd1.Parameters.AddWithValue("@a18", 1); }
                        else { cmd1.Parameters.AddWithValue("@a18", 0); }

                        cmd1.Parameters.AddWithValue("@a19", cmb_salctr.SelectedIndex == -1 ? "01" : cmb_salctr.SelectedValue);

                        if (chk_priclowcost.Checked == true)
                        { cmd1.Parameters.AddWithValue("@a20", 1); }
                        else { cmd1.Parameters.AddWithValue("@a20", 0); }

                        if (chk_stopserach.Checked == true)
                        { cmd1.Parameters.AddWithValue("@a21", 1); }
                        else { cmd1.Parameters.AddWithValue("@a21", 0); }

                        cmd1.ExecuteNonQuery();
                        con2.Close();
                      //  MessageBox.Show("add success", "ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_userid.Enabled = false;
                        txt_username.Enabled = false;
                        txt_password.Enabled = false;
                      //  textBox3.Enabled = false;
                      //  textBox4.Enabled = false;
                      //  textBox5.Enabled = false;
                      //  textBox6.Enabled = false;
                        chk_admin.Enabled = false;
                        chk_pricemodify.Enabled = false;
                        chk_edit.Enabled = false;
                        btn_save.Enabled = false;
                      //  pictureBox1.Enabled = false;
                        btn_add.Enabled = true;
                        btn_edit.Enabled = true;
                        btn_find.Enabled = true;
                        comboBox1.Enabled = false;
                        cmb_salctr.Enabled = false;
                        chk_suspend.Enabled = false;
                        chk_post.Enabled = false;
                        chk_itmpr.Enabled = false;
                        chk_item_desc.Enabled = false;
                        txt_item_desc.Enabled = false;
                        chk_inv_desc.Enabled = false;
                        txt_inv_desc.Enabled = false;
                        chk_edit_other.Enabled = false;
                        chk_chang_date.Enabled = false;
                        chk_icost_sal.Enabled = false;
                        chk_inactive.Enabled = false;
                        chk_stopserach.Enabled = false;
                        chk_showwin.Enabled = false;
                        chk_priclowcost.Enabled = false;
                        button7.Enabled = true;
                       // button7.Enabled = false;
                        button9.Enabled = true;

                    }
                }
                else { 
                    MessageBox.Show("الصنف موجود من سابق", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con2.Close();
                }
            }
            else
            {
                MessageBox.Show("يرجى تعبئة البيانات كاملة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Item_Card_Load(object sender, EventArgs e)
        {
            string sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";
            // MessageBox.Show(sl);
            dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select * from slcenters where sl_brno + sl_no in(" + sl + ") and sl_brno='" + BL.CLS_Session.brno + "'");
            cmb_salctr.DataSource = dtslctr;
            cmb_salctr.DisplayMember = "sl_name";
            cmb_salctr.ValueMember = "sl_no";
            cmb_salctr.SelectedIndex = -1;
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            isnew = true;
            isupdate = false;
           // SqlDataAdapter da1 = new SqlDataAdapter("select isnull(max(user_name),0) from users", con2);
           // DataTable dt1 = new DataTable();
          //  da1.Fill(dt1);
            //comboBox1.Items.Clear();
           // txt_userid.Text=dt1.Rows[0][0].ToString();
            txt_userid.Enabled = true;
           // txt_u
            txt_username.Enabled = true;
            txt_password.Enabled = true;
          //  textBox3.Enabled = true;
         //   textBox4.Enabled = true;
          //  textBox5.Enabled = true;
           // textBox6.Enabled = true;
            chk_admin.Enabled = true;
            chk_pricemodify.Enabled = true;
            chk_edit.Enabled = true;
            comboBox1.Enabled = true;
            cmb_salctr.Enabled = true;
            chk_suspend.Enabled = true;
            chk_itmpr.Enabled = true;
            chk_post.Enabled = true;
            chk_inv_desc.Enabled = true;
            chk_edit_other.Enabled = true;
            chk_chang_date.Enabled = true;
            chk_item_desc.Enabled = true;
            chk_icost_sal.Enabled = true;
            chk_inactive.Enabled = true;
            chk_stopserach.Enabled = true;
            chk_showwin.Enabled = true;
            chk_priclowcost.Enabled = true;
            button7.Enabled = false;
            button9.Enabled = false;
           // chk_item_desc.Enabled = false;
           // txt_item_desc.Enabled = false;
           // chk_inv_desc.Enabled = false;
           // txt_inv_desc.Enabled = false;
          //  chk_edit_other.Enabled = false;
           // chk_chang_date.Enabled = false;

          //  pictureBox1.Enabled = true;

            txt_userid.Focus();

            txt_username.Text = "";
            txt_password.Text = "";
           // textBox3.Text = "";
           // textBox4.Text = "";
          //  textBox5.Text = "";
          //  textBox6.Text = "";
            comboBox1.SelectedIndex = 0;// -1;
          //  pictureBox1.Image = Properties.Resources.background_button;



            btn_add.Enabled = false;
            btn_edit.Enabled = false;
            btn_save.Enabled = true;


            string str2 = BL.CLS_Session.formkey;
            string whatever = str2.Substring(str2.IndexOf("F120") + 5, 3);
            if (whatever.Substring(0, 1).Equals("0"))
                btn_add.Visible = false;
            if (whatever.Substring(1, 1).Equals("0"))
                btn_edit.Visible = false;
            if (whatever.Substring(2, 1).Equals("0"))
                btn_del.Visible = false;

           // SqlDataAdapter da = new SqlDataAdapter("select group_name,group_id from groups",con2);
           // DataTable dt = new DataTable();
           // da.Fill(dt);

           
           // comboBox1.DataSource = dt;
           // // metroComboBox3.DataSource = dt2;
           // //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
           // // metroComboBox3.DisplayMember = "a";
           //// comboBox1.ValueMember = "group_id";
           // comboBox1.DisplayMember = "group_name";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            var frm3 = new USER_Srch(this);
            //  frm2.Owner = this;
            //-//-//  frm2.MdiParent = Application.OpenForms["MAIN"];
            frm3.ShowDialog();
            load_data(frm3.dataGridView1.CurrentRow.Cells[0].Value.ToString());
            textBox7.Focus();
             */
            Search_All f4 = new Search_All("8", "Usr");
            f4.ShowDialog();

            try
            {

               load_data(f4.dataGridView1.CurrentRow.Cells[0].Value.ToString());
               txt_find.Focus();
                //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                /*
               dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
               dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
               dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                */


            }
            catch { }
            //this.Close();
            
            //Item_Srch its = new Item_Srch();
            ////its.Parent = this;
            //its.Show();
        }
        

        public void button4_Click(object sender, EventArgs e)
        {
            load_data(txt_find.Text);
            txt_find.Text = "";
        }
        private void load_data( string userid)
        {
            try
            {
              //  comboBox1.DataSource = null;

                if (userid != "")
                {


                    SqlDataAdapter da = new SqlDataAdapter("select * from users where user_name='" + userid.Trim() + "'", con2);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count != 0)
                    {
                        txt_userid.Text = dt.Rows[0][0].ToString();
                        txt_username.Text = dt.Rows[0][1].ToString();
                        txt_password.Text = ende.Decrypt(dt.Rows[0][2].ToString(), true);
                        //   textBox3.Text = dt.Rows[0][2].ToString();
                        //  textBox4.Text = dt.Rows[0][3].ToString();
                        //  textBox5.Text = dt.Rows[0][4].ToString();
                        //  textBox6.Text = dt.Rows[0][5].ToString();
                        if (Convert.ToInt32(dt.Rows[0][3]) == 1)
                        {
                            chk_admin.Checked = true;
                        }
                        else { chk_admin.Checked = false; }

                        if (Convert.ToInt32(dt.Rows[0][4]) == 1)
                        {
                            chk_pricemodify.Checked = true;
                        }
                        else { chk_admin.Checked = false; }

                        comboBox1.Text = dt.Rows[0][5].ToString().Equals("E")? "English" : "عربي";

                        if (Convert.ToInt32(dt.Rows[0][6]) == 1)
                        {
                            chk_edit.Checked = true;
                        }
                        else { chk_edit.Checked = false; }

                        if (Convert.ToInt32(dt.Rows[0][7]) == 1)
                        {
                            chk_suspend.Checked = true;
                        }
                        else { chk_suspend.Checked = false; }

                        if (Convert.ToInt32(dt.Rows[0][8]) == 1)
                        {
                            chk_itmpr.Checked = true;
                        }
                        else { chk_itmpr.Checked = false; }

                        if (Convert.ToInt32(dt.Rows[0][10]) == 1)
                        {
                            chk_post.Checked = true;
                        }
                        else { chk_post.Checked = false; }
                        ///////////////////////////////////////

                        if (Convert.ToInt32(dt.Rows[0]["chng_date"]) == 1)
                        {
                            chk_chang_date.Checked = true;
                        }
                        else { chk_chang_date.Checked = false; }

                        if (Convert.ToInt32(dt.Rows[0]["modfy_othr_tr"]) == 1)
                        {
                            chk_edit_other.Checked = true;
                        }
                        else { chk_edit_other.Checked = false; }

                        if (Convert.ToDouble(dt.Rows[0]["item_desc"]) > 0)
                        {
                            chk_item_desc.Checked = true;
                            txt_item_desc.Text = dt.Rows[0]["item_desc"].ToString();
                            txt_item_desc.Enabled = false;
                        }
                        else
                        {
                            chk_item_desc.Checked = false;
                            // txt_item_desc.Text = "0";
                        }

                        if (Convert.ToDouble(dt.Rows[0]["inv_desc"]) > 0)
                        {
                            chk_inv_desc.Checked = true;
                            txt_inv_desc.Text = dt.Rows[0]["inv_desc"].ToString();
                            txt_inv_desc.Enabled = false;
                        }
                        else
                        {
                            chk_inv_desc.Checked = false;
                            //  chk_inv_desc.Text = "0";
                        }

                        if (Convert.ToInt32(dt.Rows[0]["shw_sal_icost"]) == 1)
                        {
                            chk_icost_sal.Checked = true;
                        }
                        else { chk_icost_sal.Checked = false; }

                        if (Convert.ToInt32(dt.Rows[0]["inactive"]) == 1)
                        {
                            chk_inactive.Checked = true;
                        }
                        else { chk_inactive.Checked = false; }

                        if (Convert.ToInt32(dt.Rows[0]["show_win"]) == 1)
                        {
                            chk_showwin.Checked = true;
                        }
                        else { chk_showwin.Checked = false; }

                        if (Convert.ToInt32(dt.Rows[0]["suspend_inv"]) == 1)
                        {
                            chk_suspend.Checked = true;
                        }
                        else { chk_suspend.Checked = false; }

                        if (Convert.ToInt32(dt.Rows[0]["belw_bal"]) == 1)
                        {
                            chk_edit.Checked = true;
                        }
                        else { chk_edit.Checked = false; }


                        cmb_salctr.SelectedValue = dt.Rows[0]["sl_no"];

                        if (Convert.ToBoolean(dt.Rows[0]["prislwcst"]) == true)
                        {
                            chk_priclowcost.Checked = true;
                        }
                        else { chk_priclowcost.Checked = false; }

                        if (Convert.ToBoolean(dt.Rows[0]["up_stopsrch"]) == true)
                        {
                            chk_stopserach.Checked = true;
                        }
                        else { chk_stopserach.Checked = false; }

                        //if (dt.Rows[0][9] is DBNull)
                        //{
                        //   // pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\simple-blue-opera-background-button.jpg");
                        //  // ok   pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\background-button.png");
                        //  //  pictureBox1.Image = Properties.Resources.background_button;

                        //}
                        //else
                        //{
                        //    //////byte[] image = (byte[])dt.Rows[0][9];
                        //    //////MemoryStream ms = new MemoryStream(image);
                        //    //////pictureBox1.Image = Image.FromStream(ms);

                        //    ////////pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + dt.Rows[0][9]);
                        //    ////////path = dt.Rows[0][9].ToString();
                        //    if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + dt.Rows[0][9]))
                        //    {
                        //    //    pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + dt.Rows[0][9]);

                        //    }
                        //    else
                        //    {
                        //    //    pictureBox1.Image = Properties.Resources.background_button;

                        //    }


                        //    //path = dt.Rows[0][9].ToString();

                        //}



                        // string tmp = dt.Rows[0][8].ToString();
                        // SqlDataAdapter da2 = new SqlDataAdapter("select group_name from groups where group_id=" + Convert.ToInt32(tmp) + "", con2);
                        // DataTable dt2 = new DataTable();
                        // da2.Fill(dt2);

                        // // comboBox1.Items.Clear();
                        // //comboBox1.SelectedIndex = -1;

                        //// comboBox1.SelectedValue = null;
                        //// comboBox1.Items.Clear();


                        // comboBox1.Items.Add(dt2.Rows[0][0].ToString());
                        // comboBox1.SelectedIndex = comboBox1.FindStringExact(dt2.Rows[0][0].ToString());
                        // // comboBox1.Items.Clear();
                        // //  comboBox1.Items.Add(dt.Rows[0][8].ToString());
                        // //  comboBox1.SelectedIndex = comboBox1.FindStringExact(dt.Rows[0][8].ToString());
                       
                        if (!txt_userid.Text.Equals("1"))
                        {
                            button7.Enabled = true;
                        }
                        else
                        {
                            button7.Enabled = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("لايوجد مستخدم بهذا الرقم", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_find.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("يرجى ادخال رقم المستخدم", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                button4_Click((object)sender, (EventArgs)e);             
            }
        }

        public void button5_Click(object sender, EventArgs e)
        {
            isnew = false;
            isupdate = true;
            string ss = comboBox1.Text.ToString();
            txt_userid.Enabled = false;
            txt_username.Enabled = true;
            txt_password.Enabled = true;
           // textBox3.Enabled = true;
          //  textBox4.Enabled = true;
          //  textBox5.Enabled = true;
          //  textBox6.Enabled = true;
            btn_add.Enabled = false;
            btn_edit.Enabled = false;
            btn_find.Enabled = false;
            button6.Enabled = true;
            chk_admin.Enabled = true;
            chk_pricemodify.Enabled = true;
            chk_edit.Enabled = true;
            comboBox1.Enabled = true;
            cmb_salctr.Enabled = true;
            chk_suspend.Enabled = true;
            chk_itmpr.Enabled = true;
            chk_post.Enabled = true;
            chk_item_desc.Enabled = true;
            txt_item_desc.Enabled = true;
            chk_inv_desc.Enabled = true;
            txt_inv_desc.Enabled = true;
            chk_edit_other.Enabled = true;
            chk_chang_date.Enabled = true;
            chk_icost_sal.Enabled = true;
            chk_inactive.Enabled = true;
            chk_stopserach.Enabled = true;
            chk_showwin.Enabled = true;
            chk_priclowcost.Enabled = true;
           /// pictureBox1.Enabled = true;

           // SqlDataAdapter da = new SqlDataAdapter("select group_id,group_name from groups", con2);
           // DataTable dt = new DataTable();
           // da.Fill(dt);


           // comboBox1.DataSource = dt;
           // // metroComboBox3.DataSource = dt2;
           // //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
           // // metroComboBox3.DisplayMember = "a";
           // //  comboBox1.ValueMember = comboBox1.Text;
           // comboBox1.DisplayMember = "group_name";
           // comboBox1.ValueMember = "group_id";

           //// comboBox1.Items.Add(dt.Rows[0][0].ToString());
           // comboBox1.SelectedIndex = comboBox1.FindStringExact(ss);
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txt_password.Text.Trim().Length < 4)
            {
                MessageBox.Show("يجب ان يكون طول كلمة السر 4 ارقام او اكثر", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_password.Focus();
                return;

            }
            if (txt_userid.Text != "" && txt_username.Text != "" && comboBox1.Text != "")
            {
             //   MemoryStream ms = new MemoryStream();
             ////   pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
             //   byte[] byteImage = ms.ToArray();

                con2.Open();
                // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
                using (SqlCommand cmd1 = new SqlCommand("update users set  full_name=@a2, password=@a3, user_type=@a4, modify_price=@a5, language=@a6,use_sal_edt=@a7,use_dsc=@a8,ch_itmpr=@a9,post_btn=@a10,chng_date=@a11,modfy_othr_tr=@a12,item_desc=@a13,inv_desc=@a14,shw_sal_icost=@a15,inactive=@a16,show_win=@a17,suspend_inv=@a18,belw_bal=@a19,sl_no=@a20,prislwcst=@a21,up_stopsrch=@a22 where user_name='" + txt_userid.Text + "'", con2))
                {


                    cmd1.Parameters.AddWithValue("@a2", txt_username.Text);
                   // cmd1.Parameters.AddWithValue("@a2", textBox2.Text);
                    cmd1.Parameters.AddWithValue("@a3", ende.Encrypt(txt_password.Text, true));

                    if (chk_admin.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a4", 1); }
                    else { cmd1.Parameters.AddWithValue("@a4", 0); }

                    if (chk_pricemodify.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a5", 1); }
                    else { cmd1.Parameters.AddWithValue("@a5", 0); }


                   // cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(textBox4.Text));
                   // cmd1.Parameters.AddWithValue("@a5", textBox5.Text);
                   // cmd1.Parameters.AddWithValue("@a6", textBox6.Text);
                    cmd1.Parameters.AddWithValue("@a6", comboBox1.Text.ToString().Equals("English") ? "E" : "ع");
                   // cmd1.Parameters.AddWithValue("@a8", byteImage);
                    //cmd1.Parameters.AddWithValue("@a8", path);
                    //if (chk_admin.Checked == true)
                    //{ cmd1.Parameters.AddWithValue("@a9", 1); }
                    //else { cmd1.Parameters.AddWithValue("@a9", 0); }

                    if (chk_edit.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a7", 1); }
                    else { cmd1.Parameters.AddWithValue("@a7", 0); }

                    if (chk_suspend.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a8", 1); }
                    else { cmd1.Parameters.AddWithValue("@a8", 0); }

                    if (chk_itmpr.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a9", 1); }
                    else { cmd1.Parameters.AddWithValue("@a9", 0); }

                    if (chk_post.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a10", 1); }
                    else { cmd1.Parameters.AddWithValue("@a10", 0); }

                    if (chk_chang_date.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a11", 1); }
                    else { cmd1.Parameters.AddWithValue("@a11", 0); }

                    if (chk_edit_other.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a12", 1); }
                    else { cmd1.Parameters.AddWithValue("@a12", 0); }

                    if (chk_item_desc.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a13", txt_item_desc.Text); }
                    else { cmd1.Parameters.AddWithValue("@a13", 0); }

                    if (chk_inv_desc.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a14", txt_inv_desc.Text); }
                    else { cmd1.Parameters.AddWithValue("@a14", 0); }

                    if (chk_icost_sal.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a15", 1); }
                    else { cmd1.Parameters.AddWithValue("@a15", 0); }

                    if (chk_inactive.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a16", 1); }
                    else { cmd1.Parameters.AddWithValue("@a16", 0); }

                    if (chk_showwin.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a17", 1); }
                    else { cmd1.Parameters.AddWithValue("@a17", 0); }

                    if (chk_suspend.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a18", 1); }
                    else { cmd1.Parameters.AddWithValue("@a18", 0); }

                    if (chk_edit.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a19", 1); }
                    else { cmd1.Parameters.AddWithValue("@a19", 0); }

                    cmd1.Parameters.AddWithValue("@a20", cmb_salctr.SelectedIndex == -1 ? "01" : cmb_salctr.SelectedValue);

                    if (chk_priclowcost.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a21", 1); }
                    else { cmd1.Parameters.AddWithValue("@a21", 0); }

                    if (chk_stopserach.Checked == true)
                    { cmd1.Parameters.AddWithValue("@a22", 1); }
                    else { cmd1.Parameters.AddWithValue("@a22", 0); }

                    cmd1.ExecuteNonQuery();
                    con2.Close();
                   // MessageBox.Show("modifed success", "ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_userid.Enabled = false;
                    txt_username.Enabled = false;
                    txt_password.Enabled = false;
                  //  textBox3.Enabled = false;
                  //  textBox4.Enabled = false;
                  //  textBox5.Enabled = false;
                  //  textBox6.Enabled = false;
                    chk_admin.Enabled = false;
                    chk_pricemodify.Enabled = false;
                    chk_edit.Enabled = false;
                    btn_save.Enabled = false;
                    btn_add.Enabled = true;
                    btn_find.Enabled = true;
                    button6.Enabled = false;
                    btn_edit.Enabled = true;
                    comboBox1.Enabled = false;
                    cmb_salctr.Enabled = false;
                    chk_suspend.Enabled = false;
                    chk_itmpr.Enabled = false;
                    chk_post.Enabled = false;
                    chk_item_desc.Enabled = false;
                    txt_item_desc.Enabled = false;
                    chk_inv_desc.Enabled = false;
                    txt_inv_desc.Enabled = false;
                    chk_edit_other.Enabled = false;
                    chk_chang_date.Enabled = false;
                    chk_icost_sal.Enabled = false;
                    chk_inactive.Enabled = false;
                    chk_stopserach.Enabled = false;
                    chk_showwin.Enabled = false;
                    chk_priclowcost.Enabled = false;
                    button7.Enabled = true;
                    button9.Enabled = true;
                 //   pictureBox1.Enabled = false;

                  //  comboBox1.Items.Clear();
                }
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {

           
            // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            // string str = "SELECT hdr.ref, hdr.total, hdr.count, hdr.date, dtl.barcod, dtl.name, dtl.price, dtl.unit FROM hdr INNER JOIN dtl ON hdr.ref = dtl.ref WHERE (hdr.ref = (SELECT MAX(ref) AS Expr1 FROM  hdr AS hdr_1))";
            // SqlDataAdapter dacr = new SqlDataAdapter(str , con);

            //if (textBox1.Text != "")
            //{
            //   if (textBox8.Text == "")
            //    {
            //       textBox8.Text = "1";
            //        SqlDataAdapter da2 = new SqlDataAdapter("select * from items where item_no ='" + textBox1.Text + "'", con2);
            //        //"item_no like '%" + textBox1.Text + "%' or item_name like '%"+textBox1.Text+"%'"
            //        // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from hdr where ref=(select max(ref) from hdr)", con);
            //        // DataSet1 ds = new DataSet1();
            //        DataTable dt2 = new DataTable();
            //        da2.Fill(dt2);
            //        // dacr.Fill(ds, "dtl");
            //        // dacr1.Fill(ds, "hdr");


            //        reports.BarcodeReport report = new reports.BarcodeReport();
            //        report.SetDataSource(dt2);
            //        // report.ReportDefinition.Areas.

            //        // crystalReportViewer1.ReportSource = report;

            //        // crystalReportViewer1.Refresh();

            //        report.PrintOptions.PrinterName = "Barcode";




            //        //int xxx = Convert.ToInt32(textBox8.Text);
            //        //for (int i = 1; i <= xxx; i++)
            //        //{
            //            report.PrintToPrinter(1, true, 1, 1);
            //            // report.PrintToPrinter(1, false, 1, 1);
            //        //    textBox8.Text = "";
            //        //}
                    
            //    }
            //    else
            //    {
                    
            //        SqlDataAdapter da2 = new SqlDataAdapter("select * from items where item_no ='" + textBox1.Text + "'", con2);
                    
            //        DataTable dt2 = new DataTable();
            //        da2.Fill(dt2);
            //        // dacr.Fill(ds, "dtl");
            //        // dacr1.Fill(ds, "hdr");


            //        reports.BarcodeReport report = new reports.BarcodeReport();
            //        report.SetDataSource(dt2);
            //        // report.ReportDefinition.Areas.

            //        // crystalReportViewer1.ReportSource = report;

            //        // crystalReportViewer1.Refresh();

            //        report.PrintOptions.PrinterName = "Barcode";
            //      //  int xxx = Convert.ToInt32(textBox8.Text);
            //        //for (int i = 1; i <= xxx; i++)
            //        //{
            //        //    report.PrintToPrinter(1, true, 1, 1);
            //        //    // report.PrintToPrinter(1, false, 1, 1);
                        
            //        //}
            //    }
            //}
            //else

            //{
            //    MessageBox.Show("يرجى اختار صنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
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
           // textBox5.Text = textBox1.Text;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (txt_userid.Text == "")
            {
                MessageBox.Show("لا يوجد صنف لحذفة يرجى اختار صنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlDataAdapter da10 = new SqlDataAdapter("select * from users where user_name ='" + txt_userid.Text + "'", con2);

                DataTable dt10 = new DataTable();
                da10.Fill(dt10);
               // if (dt10.Rows[0][0].ToString() == "1")
                if (dt10.Rows[0][0].ToString() == "admin")
                {
                    MessageBox.Show("لا يمكن حذف المستخدم الرئيسي", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                       DialogResult result = MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Do you want to dalete" : "هل تريد المسح", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                       if (result == DialogResult.Yes)
                       {
                           string xx = txt_username.Text.ToString();
                           con2.Open();
                           // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
                           using (SqlCommand cmd10 = new SqlCommand("delete from users where user_name='" + txt_userid.Text + "'", con2))
                           {



                               cmd10.ExecuteNonQuery();
                               con2.Close();
                               MessageBox.Show("تم مسح الصنف " + xx ,"",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                               txt_userid.Text = "";
                               txt_username.Text = "";
                               txt_password.Text = "";
                               //textBox3.Text = "";
                               //textBox4.Text = "";
                               //textBox5.Text = "";
                               //textBox6.Text = "";
                               // comboBox1.Items.Clear();
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
                }
            }
            //}
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //if (textBox7.Text != "")
            //{

               
            //    SqlCommand cmd = new SqlCommand("Select_Product",con2);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    //cmd.CommandText = "Select_Product";
            //    cmd.Parameters.Add("@no", SqlDbType.NVarChar, 16).Value = textBox7.Text;
            //    cmd.Parameters.Add("@barcode", SqlDbType.NVarChar, 16).Value = "333";
                

            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);

            //   if (dt.Rows.Count != 0)
            //    {
            //        txt_username.Text = dt.Rows[0][0].ToString();
            //        txt_password.Text = dt.Rows[0][1].ToString();
            //        //textBox3.Text = dt.Rows[0][2].ToString();
            //        //textBox4.Text = dt.Rows[0][3].ToString();
            //        //textBox5.Text = dt.Rows[0][4].ToString();
            //        //textBox6.Text = dt.Rows[0][5].ToString();
            //   }
            //    else
            //   { 
            //       MessageBox.Show("لايوجد صنف بهذا الرقم", "", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            //   }

            //}
            //else
            //{
            //    MessageBox.Show("يرجى ادخال رقم صنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            //SqlDataAdapter da = new SqlDataAdapter("select group_id,group_name from groups", con2);
            //DataTable dt = new DataTable();
            //da.Fill(dt);


            //comboBox1.DataSource = dt;
            //// metroComboBox3.DataSource = dt2;
            ////comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            //// metroComboBox3.DisplayMember = "a";
            ////  comboBox1.ValueMember = comboBox1.Text;
            //comboBox1.DisplayMember = "group_name";
            //comboBox1.ValueMember = "group_id";
            ////comboBox1.Items.Clear();
            ////comboBox1.Items.Add(dt.Rows[0][8].ToString());
            ////comboBox1.SelectedIndex = comboBox1.FindStringExact(dt.Rows[0][8].ToString());

        }

        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory() +"\\images"  ;
            ofd.Filter = "ملفات الصور |*.JPG; *.PNG; *.GIF; *.BMP";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
             //   pictureBox1.Image = Image.FromFile(ofd.FileName);
              //  path = Path.GetFileName(ofd.FileName);
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

        private void button7_Click_1(object sender, EventArgs e)
        {
            User_advance ua = new User_advance(txt_userid.Text);
            ua.MdiParent = MdiParent;
            ua.Show();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_userid.Text))
            {
                MessageBox.Show("يرجى اختيار المستخدم اولا", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Users_Advance2 ua = new Users_Advance2(txt_userid.Text);
                ua.MdiParent = MdiParent;
                ua.Show();
            }
        }

        private void chk_item_desc_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_item_desc.Checked)
                txt_item_desc.Enabled = true;
            else
                txt_item_desc.Enabled = false;
        }

        private void txt_item_desc_TextChanged(object sender, EventArgs e)
        {
            if (txt_item_desc.Text == "" || Convert.ToDouble(txt_item_desc.Text)> 99.99)
                txt_item_desc.Text = "0";
            datavd.ValidateText(txt_item_desc);

        }

        private void txt_inv_desc_TextChanged(object sender, EventArgs e)
        {
            if (txt_inv_desc.Text == "" || Convert.ToDouble(txt_inv_desc.Text) > 99.99)
                txt_inv_desc.Text = "0";

            datavd.ValidateText(txt_inv_desc);
           // datavd.textBox_Validating();
        }

        private void chk_inv_desc_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_inv_desc.Checked)
                txt_inv_desc.Enabled = true;
            else
                txt_inv_desc.Enabled = false;

           
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Undo_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "are you sure to undo?" : "هل تريد التراجع عن التغييرات", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {



                if (isupdate)
                {
                    load_data(txt_find.Text.Trim());
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
                    btn_find.Enabled = true;
                    btn_edit.Enabled = true;
                    btn_Undo.Enabled = false;
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
                   // txt_opnbal.Text = "0.0";
                   // txt_maxbal.Text = "0.0";

                    txt_find.Enabled = true;
                    // txt_find.Enabled = true;
                    btn_add.Enabled = true;
                    btn_save.Enabled = false;
                    btn_find.Enabled = true;
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

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cmb_salctr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_salctr.SelectedIndex = -1;
            }
        }
        
    }
}

