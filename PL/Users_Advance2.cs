using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS
{
    public partial class Users_Advance2 : BaseForm
    {
        
        BL.DAML daml = new BL.DAML();
        string usrid;
        public Users_Advance2(string uid)
        {
            usrid = uid;
            InitializeComponent();
        }

        private void Users_Advance2_Load(object sender, EventArgs e)
        {
            

            DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select user_name,slkey,pukey,stkey,acckey,trkey from users where user_name='" + usrid + "'");
            cmb_user.DataSource = dt;
            cmb_user.DisplayMember = "user_name";
            cmb_user.ValueMember = "user_name";



            var items = new[] { new { Text = "الفروع", Value = "01" }, new { Text = "مراكز البيع", Value = "02" }, new { Text = "مراكز الشراء", Value = "03" }, new { Text = "المخازن", Value = "04" }, new { Text = "الحسابات", Value = "05" }, new { Text = "القيود", Value = "06" }, new { Text = "مراكز التكلفة", Value = "07" } };
            cmb_task.DataSource = items;
            cmb_task.DisplayMember = "Text";
            cmb_task.ValueMember = "Value";
          //  cmb_task.SelectedIndex = -1;
            cmb_task_SelectedIndexChanged(sender, e);

         //   BL.CLS_Session.slkey = dt.Rows[0]["slkey"].ToString();
         //   BL.CLS_Session.pukey = dt.Rows[0]["pukey"].ToString();
         //   BL.CLS_Session.stkey = dt.Rows[0]["stkey"].ToString();
         //  BL.CLS_Session.acckey = dt.Rows[0]["acckey"].ToString();
         //  BL.CLS_Session.acckey = dt.Rows[0]["trkey"].ToString();
        }

        private void cmb_task_SelectedIndexChanged(object sender, EventArgs e)
        {
            string slkey2, acckey2, pukey2, stkey2,trkey2,brkey2,cstkey;
            DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select user_name,slkey,pukey,stkey,acckey,trkey,brkey,cstkey from users where user_name='" + usrid + "'");
            slkey2 = dt2.Rows[0][1].ToString();
            pukey2 = dt2.Rows[0][2].ToString();
            stkey2 = dt2.Rows[0][3].ToString();
            acckey2 = dt2.Rows[0][4].ToString();
            trkey2 = dt2.Rows[0][5].ToString();
            brkey2 = dt2.Rows[0][6].ToString();
            cstkey = dt2.Rows[0][7].ToString();

            try
            {
                DataTable dts = new DataTable();
                list_notgranted.Items.Clear();
                list_granted.Items.Clear();

                if (cmb_task.SelectedValue.Equals("02"))
                {

                    dts = daml.SELECT_QUIRY_only_retrn_dt("select sl_no,sl_name,sl_brno from  slcenters");

                    for (int i = 0; i < dts.Rows.Count; i++)
                    {


                        if (slkey2.Contains(dts.Rows[i][2].ToString() + dts.Rows[i][0]))
                        {
                            list_granted.Items.Add(dts.Rows[i][2].ToString() + dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        }
                        else
                        {
                            list_notgranted.Items.Add(dts.Rows[i][2].ToString() + dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        }
                    }


                    /*
                    for (int i2 = 0; i2 < list_notgranted.Items.Count; i2++)
                    {
                       // list_granted.Items.Add(list_notgranted.
                    }
                     * */
                }

                if (cmb_task.SelectedValue.Equals("01"))
                {

                    dts = daml.SELECT_QUIRY_only_retrn_dt("select br_no,br_name from branchs");

                    for (int i = 0; i < dts.Rows.Count; i++)
                    {

                        if (brkey2.Contains(dts.Rows[i][0].ToString()))
                        {
                           // list_granted.Items.Add(BL.CLS_Session.brno + dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                            list_granted.Items.Add(dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        }
                        else
                        {
                            list_notgranted.Items.Add(dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                         //   list_notgranted.Items.Add(BL.CLS_Session.brno + dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        }
                    }
                }

                if (cmb_task.SelectedValue.Equals("03"))
                {

                    dts = daml.SELECT_QUIRY_only_retrn_dt("select pu_no,pu_name,pu_brno from pucenters");

                    for (int i = 0; i < dts.Rows.Count; i++)
                    {
                        // list_notgranted.Items.Add(BL.CLS_Session.brno + dts.Rows[i][0] + "     " + dts.Rows[i][1]);

                        if (pukey2.Contains(dts.Rows[i][2].ToString() + dts.Rows[i][0]))
                        {
                            list_granted.Items.Add(dts.Rows[i][2].ToString() + dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        }
                        else
                        {
                            list_notgranted.Items.Add(dts.Rows[i][2].ToString() + dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        }
                    }
                }

                if (cmb_task.SelectedValue.Equals("04"))
                {

                    dts = daml.SELECT_QUIRY_only_retrn_dt("select wh_no,wh_name,wh_brno from whouses");

                    for (int i = 0; i < dts.Rows.Count; i++)
                    {
                        // list_notgranted.Items.Add(BL.CLS_Session.brno + dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        if (stkey2.Contains(dts.Rows[i][2].ToString() + dts.Rows[i][0]))
                        {
                            list_granted.Items.Add(dts.Rows[i][2].ToString() + dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        }
                        else
                        {
                            list_notgranted.Items.Add(dts.Rows[i][2].ToString() + dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        }
                    }
                }

                if (cmb_task.SelectedValue.Equals("05"))
                {

                  //  dts = daml.SELECT_QUIRY_only_retrn_dt("select a_key,a_name from accounts where len(a_key)=9 and a_brno='" + BL.CLS_Session.brno + "'");
                    dts = daml.SELECT_QUIRY_only_retrn_dt("select a_key,a_name from accounts where len(a_key)=9 ");

                    for (int i = 0; i < dts.Rows.Count; i++)
                    {
                        // list_notgranted.Items.Add(BL.CLS_Session.brno + dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        if (acckey2.Contains(dts.Rows[i][0].ToString()))
                        {
                            list_granted.Items.Add(dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        }
                        else
                        {
                            list_notgranted.Items.Add(dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        }
                    }
                }

                if (cmb_task.SelectedValue.Equals("06"))
                {

                    dts = daml.SELECT_QUIRY_only_retrn_dt("select tr_no," +( BL.CLS_Session.lang.Equals("E") ? "tr_ename" : "tr_name" )+ " tr_name from trtypes");

                    for (int i = 0; i < dts.Rows.Count; i++)
                    {
                        // list_notgranted.Items.Add(BL.CLS_Session.brno + dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        if (trkey2.Contains(dts.Rows[i][0].ToString()))
                        {
                            list_granted.Items.Add(dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        }
                        else
                        {
                            list_notgranted.Items.Add(dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        }
                    }
                }

                if (cmb_task.SelectedValue.Equals("07"))
                {

                    dts = daml.SELECT_QUIRY_only_retrn_dt("select cc_id," + (BL.CLS_Session.lang.Equals("E") ? "cc_lname" : "cc_name") + " tr_name from costcenters");

                    for (int i = 0; i < dts.Rows.Count; i++)
                    {
                        // list_notgranted.Items.Add(BL.CLS_Session.brno + dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        if (cstkey.Contains(dts.Rows[i][0].ToString()))
                        {
                            list_granted.Items.Add(dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        }
                        else
                        {
                            list_notgranted.Items.Add(dts.Rows[i][0] + "     " + dts.Rows[i][1]);
                        }
                    }
                }

              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                list_granted.Items.Add(list_notgranted.SelectedItem);
                list_notgranted.Items.Remove(list_notgranted.SelectedItem);
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
             try
            {
            list_notgranted.Items.Add(list_granted.SelectedItem);
            list_granted.Items.Remove(list_granted.SelectedItem);
            }
             catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i2 = 0; i2 < list_notgranted.Items.Count; i2++)
                {
                    list_granted.Items.Add(list_notgranted.Items[i2]);                   
                }
                list_notgranted.Items.Clear();
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i2 = 0; i2 < list_granted.Items.Count; i2++)
                {
                    list_notgranted.Items.Add(list_granted.Items[i2]);
                   
                }
                list_granted.Items.Clear();
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string slkey = "";
            if (cmb_task.SelectedValue.Equals("02"))
            {
                for (int i = 0; i < list_granted.Items.Count; i++)
                {
                    //list_notgranted.Items.Add(list_granted.Items[i]);
                    slkey =slkey + " " + list_granted.Items[i].ToString().Substring(0, list_granted.Items[i].ToString().IndexOf(' '));
                }
              //  MessageBox.Show(slkey);
                int res= daml.Insert_Update_Delete_retrn_int("update users set slkey='" + slkey + "' where user_name='" + cmb_user.Text + "'",false);
                BL.CLS_Session.slkey = slkey;
            }

            if (cmb_task.SelectedValue.Equals("01"))
            {
                for (int i = 0; i < list_granted.Items.Count; i++)
                {
                    //list_notgranted.Items.Add(list_granted.Items[i]);
                    slkey = slkey + " " + list_granted.Items[i].ToString().Substring(0, list_granted.Items[i].ToString().IndexOf(' '));
                }
               // MessageBox.Show(slkey);
                int res = daml.Insert_Update_Delete_retrn_int("update users set brkey='" + (string.IsNullOrEmpty(slkey) ? " 01" : slkey) + "' where user_name='" + cmb_user.Text + "'", false);
                BL.CLS_Session.brkey = slkey;
            }

            if (cmb_task.SelectedValue.Equals("03"))
            {
                for (int i = 0; i < list_granted.Items.Count; i++)
                {
                    //list_notgranted.Items.Add(list_granted.Items[i]);
                    slkey = slkey + " " + list_granted.Items[i].ToString().Substring(0, list_granted.Items[i].ToString().IndexOf(' '));
                }
              //  MessageBox.Show(slkey);
                int res = daml.Insert_Update_Delete_retrn_int("update users set pukey='" + slkey + "' where user_name='" + cmb_user.Text + "'", false);
                BL.CLS_Session.pukey = slkey;
            }

            if (cmb_task.SelectedValue.Equals("04"))
            {
                for (int i = 0; i < list_granted.Items.Count; i++)
                {
                    //list_notgranted.Items.Add(list_granted.Items[i]);
                    slkey = slkey + " " + list_granted.Items[i].ToString().Substring(0, list_granted.Items[i].ToString().IndexOf(' '));
                }
               // MessageBox.Show(slkey);
                int res = daml.Insert_Update_Delete_retrn_int("update users set stkey='" + slkey + "' where user_name='" + cmb_user.Text + "'", false);
                BL.CLS_Session.stkey = slkey;
            }

            if (cmb_task.SelectedValue.Equals("05"))
            {
                for (int i = 0; i < list_granted.Items.Count; i++)
                {
                    //list_notgranted.Items.Add(list_granted.Items[i]);
                    slkey = slkey + " " + list_granted.Items[i].ToString().Substring(0, list_granted.Items[i].ToString().IndexOf(' '));
                }
                // MessageBox.Show(slkey);
                int res = daml.Insert_Update_Delete_retrn_int("update users set acckey='" + slkey + "' where user_name='" + cmb_user.Text + "'", false);
                BL.CLS_Session.acckey = slkey;
            }

            if (cmb_task.SelectedValue.Equals("06"))
            {
                for (int i = 0; i < list_granted.Items.Count; i++)
                {
                    //list_notgranted.Items.Add(list_granted.Items[i]);
                    slkey = slkey + " " + list_granted.Items[i].ToString().Substring(0, list_granted.Items[i].ToString().IndexOf(' '));
                }
                // MessageBox.Show(slkey);
                int res = daml.Insert_Update_Delete_retrn_int("update users set trkey='" + slkey + "' where user_name='" + cmb_user.Text + "'", false);
                BL.CLS_Session.trkey = slkey;
            }

            if (cmb_task.SelectedValue.Equals("07"))
            {
                for (int i = 0; i < list_granted.Items.Count; i++)
                {
                    //list_notgranted.Items.Add(list_granted.Items[i]);
                    slkey = slkey + " " + list_granted.Items[i].ToString().Substring(0, list_granted.Items[i].ToString().IndexOf(' '));
                }
                // MessageBox.Show(slkey);
                int res = daml.Insert_Update_Delete_retrn_int("update users set cstkey='" + slkey + "' where user_name='" + cmb_user.Text + "'", false);
                BL.CLS_Session.cstkey = slkey;
            }

            MessageBox.Show(" تم حفظ بنجاح " + cmb_task.Text ,"", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
