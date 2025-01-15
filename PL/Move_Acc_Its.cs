using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS.PL
{
    public partial class Move_Acc_Its : BaseForm
    {
        SqlConnection con3 = BL.DAML.con;

        BL.DAML daml = new BL.DAML();

        string  tr, sl, pu, allmsg="";

        bool bacc = false, bits = false, bsup = false, bcus = false;

        DataTable dttrtyps;

        public Move_Acc_Its()
        {
            InitializeComponent();
        }

        private void Move_Acc_Its_Load(object sender, EventArgs e)
        {
            tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
            sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";
            pu = BL.CLS_Session.pukey.Replace(" ", "','").Remove(0, 2) + "'";
            // MessageBox.Show(tr);
            // cmb_salctr.Visible = false;
            dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from slcenters where sl_brno + sl_no in(" + sl + ")");
            cmb_salctr.DataSource = dttrtyps;
            cmb_salctr.DisplayMember = "sl_name";
            cmb_salctr.ValueMember = "sl_no";
            cmb_salctr.SelectedIndex = -1;

            dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from pucenters where pu_brno + pu_no in(" + pu + ")");
            cmb_puctr.DataSource = dttrtyps;
            cmb_puctr.DisplayMember = "pu_name";
            cmb_puctr.ValueMember = "pu_no";
            cmb_puctr.SelectedIndex = -1;

            //tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
            // MessageBox.Show(tr);
            // cmb_salctr.Visible = false;
            dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ")");
            cmb_type.DataSource = dttrtyps;
            cmb_type.DisplayMember = BL.CLS_Session.lang.Equals("E") ? "tr_ename" : "tr_name";
            cmb_type.ValueMember = "tr_no";
            cmb_type.SelectedIndex = -1;
           
            cmb_salctr.Select();

           // MessageBox.Show(tr);
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
              

                int pass = 0;
                if (Convert.ToInt32(DateTime.Now.Day) % 2 == 0)
                    pass = ((Convert.ToInt32(DateTime.Now.Year) + Convert.ToInt32(DateTime.Now.Month)) * 411) - (Convert.ToInt32(DateTime.Now.Day) * 114);
                else
                    pass = ((Convert.ToInt32(DateTime.Now.Year) + Convert.ToInt32(DateTime.Now.Month)) * 311) - (Convert.ToInt32(DateTime.Now.Day) * 113);

                // if (textBox1.Text == "sa735356688")
                if (!textBox1.Text.Trim().Equals(pass.ToString())) //if (Convert.ToInt32(textBox1.Text.Trim()) != pass)
                {
                    MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Password Error" : "كلمة السر خطا", "Error خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Focus();
                    return;
                }

                cmb_type_Leave(sender, e);

                if (string.IsNullOrEmpty(txt_oldcode.Text.Trim()) || string.IsNullOrEmpty(txt_newcode.Text.Trim()))
                {
                    MessageBox.Show("يجب ادخال ارقام الحسابات المطلوبة", "Error خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                button1.Text = "يرجى الانتظار";
                button1.Enabled = false;
                using (SqlCommand cmd = new SqlCommand("Move_Acc_To_Other"))
                {
                    //MessageBox.Show(txt_oldcode.Text);
                   // MessageBox.Show(txt_newcode.Text);
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con3;
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.AddWithValue("@br_no", BL.CLS_Session.brno);
                        cmd.Parameters.AddWithValue("@old_acc", txt_oldcode.Text.Trim());
                        cmd.Parameters.AddWithValue("@new_acc", txt_newcode.Text.Trim());

                        cmd.Parameters.AddWithValue("@ok_ac", rad_acc.Checked ? 1 : 0);
                        cmd.Parameters.AddWithValue("@ok_it", rad_its.Checked ? 1 : 0);
                        cmd.Parameters.AddWithValue("@ok_su", rad_sup.Checked ? 1 : 0);
                        cmd.Parameters.AddWithValue("@ok_cu", rad_cus.Checked ? 1 : 0);


                        cmd.Parameters.AddWithValue("@errstatus", 0);
                        cmd.Parameters["@errstatus"].Direction = ParameterDirection.Output;
                        if (con3.State == ConnectionState.Closed) con3.Open();
                        cmd.ExecuteNonQuery();
                        con3.Close();

                        // MessageBox.Show("Done Successfully " +cmd.Parameters["@errstatus"].Value + " " , " تم النقل بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        MessageBox.Show("تم نقل حركة الحساب بنجاح" + Environment.NewLine + Environment.NewLine, " تم النقل بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        button1.Enabled = true;
                        button1.Text = "نقل حركة الحساب";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + Environment.NewLine + cmd.Parameters["@errstatus"].Value, "Error خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        button1.Enabled = true;
                        button1.Text = "نقل حركة الحساب";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }
        }


        private void txt_oldcode_Leave(object sender, EventArgs e)
        {
            DataTable dtt;

            if (rad_acc.Checked)
            {
                dtt = daml.CMD_SELECT_QUIRY_only_retrn_dt("select a_name from accounts where a_key='" + txt_oldcode.Text + "' and len(a_key)=9");
                if (dtt.Rows.Count > 0)
                {
                    txt_oldname.Text = dtt.Rows[0][0].ToString();
                }
                else
                {
                    txt_oldcode.Text = "";
                    txt_oldname.Text = "";
                }
            }
            else if (rad_its.Checked)
            {
                dtt = daml.CMD_SELECT_QUIRY_only_retrn_dt("select item_name from items where item_no='" + txt_oldcode.Text + "'");
                if (dtt.Rows.Count > 0)
                {
                    txt_oldname.Text = dtt.Rows[0][0].ToString();
                }
                else
                {
                    txt_oldcode.Text = "";
                    txt_oldname.Text = "";
                }
            }
            else if (rad_sup.Checked)
            {
                dtt = daml.CMD_SELECT_QUIRY_only_retrn_dt("select su_name from suppliers where su_no='" + txt_oldcode.Text + "' and su_brno='"+BL.CLS_Session.brno+"'");
                if (dtt.Rows.Count > 0)
                {
                    txt_oldname.Text = dtt.Rows[0][0].ToString();
                }
                else
                {
                    txt_oldcode.Text = "";
                    txt_oldname.Text = "";
                }
            }
            else if (rad_cus.Checked)
            {
                dtt = daml.CMD_SELECT_QUIRY_only_retrn_dt("select cu_name from customers where cu_no='" + txt_oldcode.Text + "' and cu_brno='" + BL.CLS_Session.brno + "'");
                if (dtt.Rows.Count > 0)
                {
                    txt_oldname.Text = dtt.Rows[0][0].ToString();
                }
                else
                {
                    txt_oldcode.Text = "";
                    txt_oldname.Text = "";
                }
            }
            else
            {
                txt_oldcode.Text = "";
                txt_oldname.Text = "";
            }

        }

        private void txt_newcode_Leave(object sender, EventArgs e)
        {
            DataTable dtt;

            if (rad_acc.Checked)
            {
                dtt = daml.CMD_SELECT_QUIRY_only_retrn_dt("select a_name from accounts where a_key='" + txt_newcode.Text + "'  and len(a_key)=9");
                if (dtt.Rows.Count > 0)
                {
                    txt_newname.Text = dtt.Rows[0][0].ToString();
                }
                else
                {
                    txt_newcode.Text = "";
                    txt_newname.Text = "";
                }
            }
            else if (rad_its.Checked)
            {
                dtt = daml.CMD_SELECT_QUIRY_only_retrn_dt("select item_name from items where item_no='" + txt_newcode.Text + "'");
                if (dtt.Rows.Count > 0)
                {
                    txt_newname.Text = dtt.Rows[0][0].ToString();
                }
                else
                {
                    txt_newcode.Text = "";
                    txt_newname.Text = "";
                }
            }
            else if (rad_sup.Checked)
            {
                dtt = daml.CMD_SELECT_QUIRY_only_retrn_dt("select su_name from suppliers where su_no='" + txt_newcode.Text + "' and su_brno='" + BL.CLS_Session.brno + "'");
                if (dtt.Rows.Count > 0)
                {
                    txt_newname.Text = dtt.Rows[0][0].ToString();
                }
                else
                {
                    txt_newcode.Text = "";
                    txt_newname.Text = "";
                }
            }
            else if (rad_cus.Checked)
            {
                dtt = daml.CMD_SELECT_QUIRY_only_retrn_dt("select cu_name from customers where cu_no='" + txt_newcode.Text + "' and cu_brno='" + BL.CLS_Session.brno + "'");
                if (dtt.Rows.Count > 0)
                {
                    txt_newname.Text = dtt.Rows[0][0].ToString();
                }
                else
                {
                    txt_newcode.Text = "";
                    txt_newname.Text = "";
                }
            }
            else
            {
                txt_newcode.Text = "";
                txt_newname.Text = "";
            }
        }

        private void txt_oldcode_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_oldcode.Text) && string.IsNullOrEmpty(txt_newcode.Text))
                panel1.Enabled = true;
            else
                panel1.Enabled = false;

        }

        private void txt_newcode_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_oldcode.Text) && string.IsNullOrEmpty(txt_newcode.Text))
                panel1.Enabled = true;
            else
                panel1.Enabled = false;
        }

        private void txt_oldcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                txt_newcode.Focus();
      
            }
        }

        private void txt_newcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                button1.Focus();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int pass = 0;
                if (Convert.ToInt32(DateTime.Now.Day) % 2 == 0)
                    pass = ((Convert.ToInt32(DateTime.Now.Year) + Convert.ToInt32(DateTime.Now.Month)) * 411) - (Convert.ToInt32(DateTime.Now.Day) * 114);
                else
                    pass = ((Convert.ToInt32(DateTime.Now.Year) + Convert.ToInt32(DateTime.Now.Month)) * 311) - (Convert.ToInt32(DateTime.Now.Day) * 113);

                // if (textBox1.Text == "sa735356688")
                if (!textBox1.Text.Trim().Equals(pass.ToString())) //if (Convert.ToInt32(textBox1.Text.Trim()) != pass)
                {
                    MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Password Error" : "كلمة السر خطا", "Error خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Focus();
                    return;
                }

                cmb_type_Leave( sender,  e);
               // MessageBox.Show(allmsg);
               // MessageBox.Show(txt_newno.Text);
              //  MessageBox.Show(txt_oldno.Text);
               
                

                if (string.IsNullOrEmpty(txt_oldno.Text.Trim()) || string.IsNullOrEmpty(txt_newno.Text.Trim()) || string.IsNullOrEmpty(cmb_type.Text.Trim()))
                {
                    if (cmb_salctr.SelectedIndex==-1 &&(cmb_type.SelectedValue.Equals("04") || cmb_type.SelectedValue.Equals("05") || cmb_type.SelectedValue.Equals("14") || cmb_type.SelectedValue.Equals("15")))
                    {
                        MessageBox.Show("يجب ادخال الحقول المطلوبة", "Error خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (cmb_puctr.SelectedIndex==-1 &&(cmb_type.SelectedValue.Equals("06") || cmb_type.SelectedValue.Equals("07") || cmb_type.SelectedValue.Equals("16") || cmb_type.SelectedValue.Equals("17")))
                    {
                        MessageBox.Show("يجب ادخال الحقول المطلوبة", "Error خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("يجب ادخال الحقول المطلوبة", "Error خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                button1.Text = "يرجى الانتظار";
                button1.Enabled = false;
                using (SqlCommand cmd = new SqlCommand("Move_Tran_To_Other"))
               //using (SqlCommand cmd = new SqlCommand(" update acc_hdr set a_ref="+txt_newno.Text+" where a_ref="+txt_oldno.Text+" and a_type='"+cmb_type.SelectedValue+"' and a_brno='"+BL.CLS_Session.brno+"' and sl_no='"+cmb_salctr.SelectedValue+"' ;"
               // + " update acc_dtl set a_ref="+txt_newno.Text+" where a_ref="+txt_oldno.Text+" and a_type='"+cmb_type.SelectedValue+"' and a_brno='"+BL.CLS_Session.brno+"' and sl_no='"+cmb_salctr.SelectedValue+"' ;"
               // + " update sales_hdr set ref="+txt_newno.Text+" where ref="+txt_oldno.Text+" and invtype='"+cmb_type.SelectedValue+"' and branch='"+BL.CLS_Session.brno+"' and slcenter='"+cmb_salctr.SelectedValue+"' ;"
               // + " update sales_dtl set ref="+txt_newno.Text+" where ref="+txt_oldno.Text+" and invtype='"+cmb_type.SelectedValue+"' and branch='"+BL.CLS_Session.brno+"' and slcenter='"+cmb_salctr.SelectedValue+"' ;"))
				
                {
                    //MessageBox.Show(txt_oldcode.Text);
                    // MessageBox.Show(txt_newcode.Text);
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con3;
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.AddWithValue("@br_no", BL.CLS_Session.brno);
                        cmd.Parameters.AddWithValue("@sqlmsg1", allmsg);
				
                        cmd.Parameters.AddWithValue("@type_no", cmb_type.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@sl_no", cmb_salctr.SelectedIndex == -1 ? " " : cmb_salctr.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@pu_no", cmb_puctr.SelectedIndex == -1 ? " " : cmb_puctr.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@old_acc", txt_oldcode.Text.Trim());
                        cmd.Parameters.AddWithValue("@new_acc", txt_newcode.Text.Trim());

                        cmd.Parameters.AddWithValue("@ok_ac", bacc ? 1 : 0);
                        cmd.Parameters.AddWithValue("@ok_it", bits ? 1 : 0);
                        cmd.Parameters.AddWithValue("@ok_su", bsup ? 1 : 0);
                        cmd.Parameters.AddWithValue("@ok_cu", bcus ? 1 : 0);


                        cmd.Parameters.AddWithValue("@errstatus", 0);
                        cmd.Parameters["@errstatus"].Direction = ParameterDirection.Output;
                        if (con3.State == ConnectionState.Closed) con3.Open();
                        cmd.ExecuteNonQuery();
                        con3.Close();

                        // MessageBox.Show("Done Successfully " +cmd.Parameters["@errstatus"].Value + " " , " تم النقل بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        MessageBox.Show("تم تغيير رقم الحركة بنجاح" + Environment.NewLine + Environment.NewLine, " تم التغيير بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        button1.Enabled = true;
                        button1.Text = "تغيير رقم الحركة";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + Environment.NewLine + cmd.Parameters["@errstatus"].Value, "Error خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        button1.Enabled = true;
                        button1.Text = "تغيير رقم الحركة";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }
        }

        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmb_type_Leave(object sender, EventArgs e)
        {
            //try
            //{
                if (cmb_type.SelectedValue.Equals("04") || cmb_type.SelectedValue.Equals("05") || cmb_type.SelectedValue.Equals("14") || cmb_type.SelectedValue.Equals("15"))
                {
                   // cmb_salctr.SelectedIndex = -1;
                    cmb_puctr.SelectedIndex = -1;
                    cmb_salctr.Enabled = true;
                    cmb_puctr.Enabled = false;
                    bcus = true; bits = false; bsup = false; bacc = false;
                    allmsg = " update acc_hdr set a_ref=" + txt_newno.Text + " where a_ref=" + txt_oldno.Text + " and a_type='" + cmb_type.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "' and sl_no='" + cmb_salctr.SelectedValue + "' ;"
                   + " update acc_dtl set a_ref=" + txt_newno.Text + " where a_ref=" + txt_oldno.Text + " and a_type='" + cmb_type.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "' and sl_no='" + cmb_salctr.SelectedValue + "' ;"
                   + " update sales_hdr set ref=" + txt_newno.Text + " where ref=" + txt_oldno.Text + " and invtype='" + cmb_type.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "' ;"
                   + " update sales_dtl set ref=" + txt_newno.Text + " where ref=" + txt_oldno.Text + " and invtype='" + cmb_type.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "' ;";
                }

                else if (cmb_type.SelectedValue.Equals("06") || cmb_type.SelectedValue.Equals("07") || cmb_type.SelectedValue.Equals("16") || cmb_type.SelectedValue.Equals("17"))
                {
                    cmb_salctr.SelectedIndex = -1;
                  //  cmb_puctr.SelectedIndex = -1;
                    cmb_salctr.Enabled = false;
                    cmb_puctr.Enabled = true;
                    bcus = false; bits = false; bsup = true; bacc = false;
                    allmsg = " update acc_hdr set a_ref=" + txt_newno.Text + " where a_ref=" + txt_oldno.Text + " and a_type='" + cmb_type.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "' and pu_no='" + cmb_puctr.SelectedValue + "' ;"
                   + " update acc_dtl set a_ref=" + txt_newno.Text + " where a_ref=" + txt_oldno.Text + " and a_type='" + cmb_type.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "' and pu_no='" + cmb_puctr.SelectedValue + "' ;"
                   + " update pu_hdr set ref=" + txt_newno.Text + " where ref=" + txt_oldno.Text + " and invtype='" + cmb_type.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "' and pucenter='" + cmb_puctr.SelectedValue + "' ;"
                   + " update pu_dtl set ref=" + txt_newno.Text + " where ref=" + txt_oldno.Text + " and invtype='" + cmb_type.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "' and pucenter='" + cmb_puctr.SelectedValue + "' ;";
                }
                else if (cmb_type.SelectedValue.Equals("31") || cmb_type.SelectedValue.Equals("32") || cmb_type.SelectedValue.Equals("33") || cmb_type.SelectedValue.Equals("34") || cmb_type.SelectedValue.Equals("35") || cmb_type.SelectedValue.Equals("36") || cmb_type.SelectedValue.Equals("45") || cmb_type.SelectedValue.Equals("46"))
                {
                    cmb_salctr.SelectedIndex = -1;
                    cmb_puctr.SelectedIndex = -1;
                    cmb_salctr.Enabled = false;
                    cmb_puctr.Enabled = false;
                    bcus = false; bits = true; bsup = false; bacc = false;
                    allmsg = " update acc_hdr set a_ref=" + txt_newno.Text + " where a_ref=" + txt_oldno.Text + " and a_type='" + cmb_type.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "' ;"
                       + " update acc_dtl set a_ref=" + txt_newno.Text + " where a_ref=" + txt_oldno.Text + " and a_type='" + cmb_type.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "' ;"
                       + " update sto_hdr set ref=" + txt_newno.Text + " where ref=" + txt_oldno.Text + " and trtype='" + cmb_type.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "'  ;"
                       + " update sto_dtl set ref=" + txt_newno.Text + " where ref=" + txt_oldno.Text + " and trtype='" + cmb_type.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "'  ;";
                }
                else
                {
                    cmb_salctr.SelectedIndex = -1;
                    cmb_puctr.SelectedIndex = -1;
                    cmb_salctr.Enabled = false;
                    cmb_puctr.Enabled = false;
                    bcus = false; bits = false; bsup = false; bacc = true;
                    allmsg = " update acc_hdr set a_ref=" + txt_newno.Text + " where a_ref=" + txt_oldno.Text + " and a_type='" + cmb_type.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "' ;"
                  + " update acc_dtl set a_ref=" + txt_newno.Text + " where a_ref=" + txt_oldno.Text + " and a_type='" + cmb_type.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "' ;";

               }
            //}
            //catch { }
        }

        private void rad_acc_CheckedChanged(object sender, EventArgs e)
        {
            //if (rad_acc.Checked)
            //    panel2.Visible = true;
            //else
            //    panel2.Visible = false;
        }

        private void rad_cus_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
