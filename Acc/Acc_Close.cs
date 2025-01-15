using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace POS.Acc
{
    public partial class Acc_Close : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        SqlConnection con = BL.DAML.con;
        DataTable dtmtagra,dtarbah;

        public Acc_Close()
        {
            InitializeComponent();
        }

        private void txt_code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    // var selectedCell = dataGridView1.SelectedCells[0];
                    // do something with selectedCell...
                    Search_All f4 = new Search_All("1", "Acc");
                    f4.ShowDialog();



                    txt_code.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                    /*
                   dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
                   dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
                   dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                    */



                }

                if (e.KeyCode == Keys.Enter)
                {

                    // button1_Click( sender,  e);
                    DataTable dta = daml.SELECT_QUIRY_only_retrn_dt("select a_name,round(a_opnbal,2) a_opnbal,a_key  from accounts where a_key='" + txt_code.Text + "' and a_brno='" + BL.CLS_Session.brno + "'");
                    if (dta.Rows.Count > 0)
                    {
                        txt_name.Text = dta.Rows[0][0].ToString();
                        txt_code.Text = dta.Rows[0][2].ToString();
                      //  txt_opnbal.Text = dta.Rows[0][1].ToString();
                    }
                    else
                    {
                        //    MessageBox.Show("الحساب غير موجود");
                        txt_name.Text = "";
                        txt_code.Text = "";
                        //  txt_code.Text = dt.Rows[0][2].ToString();
                      //  txt_opnbal.Text = "0";
                    }

                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtp = daml.SELECT_QUIRY_only_retrn_dt("select * from acc_hdr where a_posted=0");
                if (dtp.Rows.Count > 0)
                {
                  //  MessageBox.Show("يجب ترحيل جميع الحركات اولا");
                    MessageBox.Show("يجب ترحيل جميع الحركات اولا", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(txt_code.Text))
                {
                   // MessageBox.Show("يجب ادخال حساب الاقفال");
                    MessageBox.Show("يجب ادخال حساب الاقفال", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_code.Focus();
                    return;
                }

                else
                {
                    if (chk_mtagra.Checked)
                    {
                        mtagra_close();
                    }
                    else
                    {
                        if (chk_arbah.Checked)
                        {
                            arbah_close();
                        }
                        else
                        {
                            if (rad_tashqil.Checked)
                            {
                                tashqil_close();
                            }
                            else
                            {
                                mtagra_close();
                                arbah_close();
                                tashqil_close();
                            }
                        }
                    
                    }
                
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mtagra_close()
        {
            try
            {
               // string condtax = chk_tax.Checked ? " and a.acckind in ('1','2') " : " ";
             //   string cond = checkBox1.Checked ? "b.a_hdate" : "b.a_mdate";
              //  string condp = rad_posted.Checked ? " and c.a_posted=1 " : (rad_notpostd.Checked ? " and c.a_posted=0 " : " ");

                string qstr = " select a.a_key d1,a.a_name d2,round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no where a.a_key=b.a_key),4) d3 "
                            + " from accounts a where round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no where a.a_key=b.a_key),4) <> 0 "
                            + " and a.a_brno='" + BL.CLS_Session.brno + "' and a.a_type='2' and len(a.a_key)=9 " // +condtax + " and a.a_key like '" + textBox3.Text + "%'"
                            + " order by a.a_key;";
                dtmtagra = daml.SELECT_QUIRY_only_retrn_dt(qstr);
                if (dtmtagra.Rows.Count == 0)
                {
                    MessageBox.Show("لا يوجد حسابات متاجرة للاقفال", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(a_ref),0) from acc_hdr where a_type='01' and a_brno='" + BL.CLS_Session.brno + "'");
                double sum = 0;
                foreach (DataRow r1 in dtmtagra.Rows)
                {
                    sum = sum + Convert.ToDouble(r1[2]);
                }
                int ref_max = Convert.ToInt32(dt.Rows[0][0]) + 1;

                int exexcuted = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_brno,a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,usrid,jhsrc) VALUES('" + BL.CLS_Session.brno + "','01'," + ref_max + ",'" + datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt) + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(BL.CLS_Session.end_dt)) + "','قيد اقفال المتاجرة'," + (sum>0? sum : -sum) + "," + (dtmtagra.Rows.Count) + ",'" + BL.CLS_Session.UserName + "','Acc1')", false);

                int folio=1;
                using (SqlCommand cmd = new SqlCommand("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", con))
                {
                    cmd.Parameters.AddWithValue("@a0", BL.CLS_Session.brno);
                    cmd.Parameters.AddWithValue("@a1", "01");
                    cmd.Parameters.AddWithValue("@a2",  ref_max );
                    cmd.Parameters.AddWithValue("@a3", txt_code.Text);
                    cmd.Parameters.AddWithValue("@a4", "قيد اقفال المتاجرة");
                    // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                    cmd.Parameters.AddWithValue("@a5", sum < 0 ? -sum : 0);
                    // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                    cmd.Parameters.AddWithValue("@a6", sum > 0 ? sum : 0);
                    cmd.Parameters.AddWithValue("@a7", folio);
                    cmd.Parameters.AddWithValue("@a8", datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt));
                    cmd.Parameters.AddWithValue("@a9", datval.convert_to_yyyyDDdd(datval.convert_to_hdate(BL.CLS_Session.end_dt)));
                    cmd.Parameters.AddWithValue("@a10", (sum < 0 ? "C" : "D"));
  
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
                    folio = 2;
                }
                foreach (DataRow row in dtmtagra.Rows)
                {
                    // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", con))
                    {
                        cmd.Parameters.AddWithValue("@a0", BL.CLS_Session.brno);
                        cmd.Parameters.AddWithValue("@a1", "01");
                        cmd.Parameters.AddWithValue("@a2", ref_max);
                        cmd.Parameters.AddWithValue("@a3", row[0]);
                        cmd.Parameters.AddWithValue("@a4", "قيد اقفال المتاجرة");
                        // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                        cmd.Parameters.AddWithValue("@a5", Convert.ToDouble(row[2]) > 0 ? Convert.ToDouble(row[2]) : 0);
                        // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                        cmd.Parameters.AddWithValue("@a6", Convert.ToDouble(row[2]) < 0 ? Convert.ToDouble(row[2])*-1 : 0);
                        cmd.Parameters.AddWithValue("@a7", folio);
                        cmd.Parameters.AddWithValue("@a8", datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt));
                        cmd.Parameters.AddWithValue("@a9", datval.convert_to_yyyyDDdd(datval.convert_to_hdate(BL.CLS_Session.end_dt)));
                        cmd.Parameters.AddWithValue("@a10", (Convert.ToDouble(row[2]) > 0 ? "C" : "D"));
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
                        // lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);

                    }
                    folio++;
                }
                int exexcuted2 = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_amt =(select sum(a_damt) from acc_dtl where a_brno='" + BL.CLS_Session.brno + "' and a_type='01' and a_ref=" + ref_max + ") where a_brno='" + BL.CLS_Session.brno + "' and a_type='01' and a_ref=" + ref_max + "",false);

               // MessageBox.Show("تم اقفال المتاجرة");
                MessageBox.Show("تم اقفال المتاجرة", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void arbah_close()
        {
            try
            {
                // string condtax = chk_tax.Checked ? " and a.acckind in ('1','2') " : " ";
                //   string cond = checkBox1.Checked ? "b.a_hdate" : "b.a_mdate";
                //  string condp = rad_posted.Checked ? " and c.a_posted=1 " : (rad_notpostd.Checked ? " and c.a_posted=0 " : " ");

                //string qstr = " select a.a_key d1,a.a_name d2,round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type where a.a_key=b.a_key),2) d3 "
                //            + " from accounts a where round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type where a.a_key=b.a_key),2) <> 0 "
                //            + " and a_brno='" + BL.CLS_Session.brno + "' and a_type='3'" // +condtax + " and a.a_key like '" + textBox3.Text + "%'"
                //            + " order by a.a_key;";
                //dtmtagra = daml.SELECT_QUIRY_only_retrn_dt(qstr);
                string qstr = " select a.a_key d1,a.a_name d2,round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no where a.a_key=b.a_key),4) d3 "
                          + " from accounts a where round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no where a.a_key=b.a_key),4) <> 0 "
                          + " and a.a_brno='" + BL.CLS_Session.brno + "' and a.a_type='3' and len(a.a_key)=9 " // +condtax + " and a.a_key like '" + textBox3.Text + "%'"
                          + " order by a.a_key;";
                dtarbah = daml.SELECT_QUIRY_only_retrn_dt(qstr);
                if (dtarbah.Rows.Count == 0)
                {
                    MessageBox.Show("لا يوجد حسابات ارباح وخسائر للاقفال", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(a_ref),0) from acc_hdr where a_type='01' and a_brno='" + BL.CLS_Session.brno + "'");
                double sum = 0;
                foreach (DataRow r1 in dtarbah.Rows)
                {
                    sum = sum + Convert.ToDouble(r1[2]);
                }
                int ref_max = Convert.ToInt32(dt.Rows[0][0]) + 1;


                int exexcuted = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_brno,a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,usrid,jhsrc) VALUES('" + BL.CLS_Session.brno + "','01'," + ref_max + ",'" + datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt) + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(BL.CLS_Session.end_dt)) + "','قيد اقفال الارباح والخسائر'," + (sum > 0 ? sum : -sum) + "," + (dtarbah.Rows.Count) + ",'" + BL.CLS_Session.UserName + "','Acc1')", false);
                int folio = 1;
                using (SqlCommand cmd = new SqlCommand("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", con))
                {
                    cmd.Parameters.AddWithValue("@a0", BL.CLS_Session.brno);
                    cmd.Parameters.AddWithValue("@a1", "01");
                    cmd.Parameters.AddWithValue("@a2", ref_max);
                    cmd.Parameters.AddWithValue("@a3", txt_code.Text);
                    cmd.Parameters.AddWithValue("@a4", "قيد اقفال الارباح والخسائر");
                    // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                    cmd.Parameters.AddWithValue("@a5", sum < 0 ? -sum : 0);
                    // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                    cmd.Parameters.AddWithValue("@a6", sum > 0 ? sum : 0);
                    cmd.Parameters.AddWithValue("@a7", folio);
                    cmd.Parameters.AddWithValue("@a8", datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt));
                    cmd.Parameters.AddWithValue("@a9", datval.convert_to_yyyyDDdd(datval.convert_to_hdate(BL.CLS_Session.end_dt)));
                    cmd.Parameters.AddWithValue("@a10", (sum < 0 ? "C" : "D"));

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
                    folio = 2;
                }
                foreach (DataRow row in dtarbah.Rows)
                {
                    // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", con))
                    {
                        cmd.Parameters.AddWithValue("@a0", BL.CLS_Session.brno);
                        cmd.Parameters.AddWithValue("@a1", "01");
                        cmd.Parameters.AddWithValue("@a2", ref_max);
                        cmd.Parameters.AddWithValue("@a3", row[0]);
                        cmd.Parameters.AddWithValue("@a4", "قيد اقفال الارباح والخسائر");
                        // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                        cmd.Parameters.AddWithValue("@a5", Convert.ToDouble(row[2]) > 0 ? Convert.ToDouble(row[2]) : 0);
                        // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                        cmd.Parameters.AddWithValue("@a6", Convert.ToDouble(row[2]) < 0 ? Convert.ToDouble(row[2]) * -1 : 0);
                        cmd.Parameters.AddWithValue("@a7", folio);
                        cmd.Parameters.AddWithValue("@a8", datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt));
                        cmd.Parameters.AddWithValue("@a9", datval.convert_to_yyyyDDdd(datval.convert_to_hdate(BL.CLS_Session.end_dt)));
                        cmd.Parameters.AddWithValue("@a10", (Convert.ToDouble(row[2]) > 0 ? "C" : "D"));
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
                        // lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);

                    }
                    folio++;
                }
               // MessageBox.Show("تم اقفال الارباح والخسائر");
                int exexcuted2 = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_amt =(select sum(a_damt) from acc_dtl where a_brno='" + BL.CLS_Session.brno + "' and a_type='01' and a_ref=" + ref_max + ") where a_brno='" + BL.CLS_Session.brno + "' and a_type='01' and a_ref=" + ref_max + "", false);

                MessageBox.Show("تم اقفال الارباح والخسائر", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tashqil_close()
        {
            try
            {
                // string condtax = chk_tax.Checked ? " and a.acckind in ('1','2') " : " ";
                //   string cond = checkBox1.Checked ? "b.a_hdate" : "b.a_mdate";
                //  string condp = rad_posted.Checked ? " and c.a_posted=1 " : (rad_notpostd.Checked ? " and c.a_posted=0 " : " ");

                //string qstr = " select a.a_key d1,a.a_name d2,round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type where a.a_key=b.a_key),2) d3 "
                //            + " from accounts a where round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type where a.a_key=b.a_key),2) <> 0 "
                //            + " and a_brno='" + BL.CLS_Session.brno + "' and a_type='3'" // +condtax + " and a.a_key like '" + textBox3.Text + "%'"
                //            + " order by a.a_key;";
                //dtmtagra = daml.SELECT_QUIRY_only_retrn_dt(qstr);
                string qstr = " select a.a_key d1,a.a_name d2,round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no where a.a_key=b.a_key),4) d3 "
                          + " from accounts a where round(a.a_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no where a.a_key=b.a_key),4) <> 0 "
                          + " and a.a_brno='" + BL.CLS_Session.brno + "' and a.a_type='0' and len(a.a_key)=9 " // +condtax + " and a.a_key like '" + textBox3.Text + "%'"
                          + " order by a.a_key;";
                dtarbah = daml.SELECT_QUIRY_only_retrn_dt(qstr);

                if (dtarbah.Rows.Count == 0)
                {
                    MessageBox.Show("لا يوجد حسابات تشغيل للاقفال", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(a_ref),0) from acc_hdr where a_type='01' and a_brno='" + BL.CLS_Session.brno + "'");
                double sum = 0;
                foreach (DataRow r1 in dtarbah.Rows)
                {
                    sum = sum + Convert.ToDouble(r1[2]);
                }
                int ref_max = Convert.ToInt32(dt.Rows[0][0]) + 1;


                int exexcuted = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_brno,a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,usrid,jhsrc) VALUES('" + BL.CLS_Session.brno + "','01'," + ref_max + ",'" + datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt) + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(BL.CLS_Session.end_dt)) + "','قيد اقفال التشغيل'," + (sum > 0 ? sum : -sum) + "," + (dtarbah.Rows.Count) + ",'" + BL.CLS_Session.UserName + "','Acc1')", false);
                int folio = 1;
                using (SqlCommand cmd = new SqlCommand("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", con))
                {
                    cmd.Parameters.AddWithValue("@a0", BL.CLS_Session.brno);
                    cmd.Parameters.AddWithValue("@a1", "01");
                    cmd.Parameters.AddWithValue("@a2", ref_max);
                    cmd.Parameters.AddWithValue("@a3", txt_code.Text);
                    cmd.Parameters.AddWithValue("@a4", "قيد اقفال التشغيل");
                    // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                    cmd.Parameters.AddWithValue("@a5", sum < 0 ? -sum : 0);
                    // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                    cmd.Parameters.AddWithValue("@a6", sum > 0 ? sum : 0);
                    cmd.Parameters.AddWithValue("@a7", dtarbah.Rows.Count);
                    cmd.Parameters.AddWithValue("@a8", datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt));
                    cmd.Parameters.AddWithValue("@a9", datval.convert_to_yyyyDDdd(datval.convert_to_hdate(BL.CLS_Session.end_dt)));
                    cmd.Parameters.AddWithValue("@a10", (sum < 0 ? "C" : "D"));

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
                    folio = 2;
                }
                foreach (DataRow row in dtarbah.Rows)
                {
                    // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", con))
                    {
                        cmd.Parameters.AddWithValue("@a0", BL.CLS_Session.brno);
                        cmd.Parameters.AddWithValue("@a1", "01");
                        cmd.Parameters.AddWithValue("@a2", ref_max);
                        cmd.Parameters.AddWithValue("@a3", row[0]);
                        cmd.Parameters.AddWithValue("@a4", "قيد اقفال التشغيل");
                        // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                        cmd.Parameters.AddWithValue("@a5", Convert.ToDouble(row[2]) > 0 ? Convert.ToDouble(row[2]) : 0);
                        // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                        cmd.Parameters.AddWithValue("@a6", Convert.ToDouble(row[2]) < 0 ? Convert.ToDouble(row[2]) * -1 : 0);
                        cmd.Parameters.AddWithValue("@a7", folio);
                        cmd.Parameters.AddWithValue("@a8", datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt));
                        cmd.Parameters.AddWithValue("@a9", datval.convert_to_yyyyDDdd(datval.convert_to_hdate(BL.CLS_Session.end_dt)));
                        cmd.Parameters.AddWithValue("@a10", (Convert.ToDouble(row[2]) > 0 ? "C" : "D"));
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
                        // lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);

                    }
                    folio++;
                }
                // MessageBox.Show("تم اقفال الارباح والخسائر");
                int exexcuted2 = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_amt =(select sum(a_damt) from acc_dtl where a_brno='" + BL.CLS_Session.brno + "' and a_type='01' and a_ref=" + ref_max + ") where a_brno='" + BL.CLS_Session.brno + "' and a_type='01' and a_ref=" + ref_max + "", false);

                MessageBox.Show("تم اقفال التشغيل", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txt_code_Leave(object sender, EventArgs e)
        {
            try
            {
                DataTable dta = daml.SELECT_QUIRY_only_retrn_dt("select a_name,a_key  from accounts where a_key='" + txt_code.Text + "' and a_brno='" + BL.CLS_Session.brno + "'");
                if (dta.Rows.Count > 0)
                {
                    txt_name.Text = dta.Rows[0][0].ToString();
                    txt_code.Text = dta.Rows[0][1].ToString();
                    //  txt_opnbal.Text = dta.Rows[0][1].ToString();
                }
                else
                {
                    //    MessageBox.Show("الحساب غير موجود");
                    txt_name.Text = "";
                    txt_code.Text = "";
                    //  txt_code.Text = dt.Rows[0][2].ToString();
                    //  txt_opnbal.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
