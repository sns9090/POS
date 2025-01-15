using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace POS.Acc
{
    public partial class Acc_Update : BaseForm
    {
        SqlConnection con2 = BL.DAML.con;
        SqlConnection con = BL.DAML.con;
        BL.EncDec endc = new BL.EncDec();
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        string textBox1, textBox2, textBox3, textBox4, textBox5, zzz;
        int count_hdr, count_dtl;
       // DataTable dts;
        public Acc_Update()
        {
            InitializeComponent();
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


        public void button1_Click(object sender, EventArgs e)
        {

        }

        private void txt_code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    // var selectedCell = dataGridView1.SelectedCells[0];
                    // do something with selectedCell...
                    Search_All f4 = new Search_All("2", "Sto");
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


                    if (!string.IsNullOrEmpty(txt_code.Text))
                    {

                        string condi = label8.Text.Equals("الباركود") ? " and b.barcode='" + txt_code.Text + "'" : " and i.item_no='" + txt_code.Text + "'";
                        // select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img from items i join items_bc b on b.item_no=i.item_no and i.item_no='" + textBox3.Text + "' join taxs t on i.item_tax=t.tax_id", con2);
                        SqlDataAdapter da23 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img from items i join items_bc b on b.item_no=i.item_no " + condi + " join taxs t on i.item_tax=t.tax_id", con2);
                        DataTable dt = new DataTable();
                        da23.Fill(dt);

                        if (dt.Rows.Count == 1)
                        {
                            txt_name.Text = dt.Rows[0][1].ToString();
                            txt_oldp.Text = dt.Rows[0][3].ToString();
                            txt_newp.Select();
                        }
                    }
                    else
                    {
                        MessageBox.Show("ادخل رقم الصنف");

                    }

                }
            }
            catch
            { }


                
        }

        private void Price_Chang_Load(object sender, EventArgs e)
        {
            txt_fmdate.Text = DateTime.Now.AddDays(-3).ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            txt_tmdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

           
           
           // txt_code.Select();
            SqlDataAdapter da = new SqlDataAdapter("select * from server", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            textBox1 = dt.Rows[0][0].ToString();
            textBox2 = dt.Rows[0][1].ToString();
            textBox3 = dt.Rows[0][2].ToString();
            textBox4 =endc.Decrypt(dt.Rows[0][3].ToString(),true);
            textBox5 = dt.Rows[0][4].ToString();

           

        }

        private void txt_newp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Select();
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            if (label8.Text.Equals("الباركود"))
                label8.Text = "رقم الصنف";
            else
                label8.Text = "الباركود";
        }

        public void button2_Click(object sender, EventArgs e)
        {
         

            button2.Text = "يرجى الانتظار";
            button2.Enabled = false;
            try
            {
                //WaitForm wf = new WaitForm("");
                //wf.MdiParent = MdiParent;
                //wf.Show();
                //Application.DoEvents();

                CultureInfo culture = new CultureInfo("en-US",false);
                DateTime tempDate = Convert.ToDateTime(datval.convert_to_yyyy_MMddwith_dash(txt_tmdate.Text) + " 05:00:00.000", culture).AddDays(1);
                zzz = tempDate.ToString("dd-MM-yyyy", new CultureInfo("en-US",false));

               // MessageBox.Show(zzz);
                progressBar1.Minimum = 0;
                // progressBar1.Maximum = 200;

                //  for (ii = 0; ii <= 200; ii++)
                //  {
                //     progressBar1.Value = ii;
                //  }
                // daml.SqlCon_Open();
                SqlConnection con_dest = new SqlConnection("Data Source=" + textBox1 + ";Initial Catalog=" + textBox2 + ";User id=" + textBox3 + ";Password=" + textBox4 + ";Connection Timeout=" + textBox5 + "");

                // SqlDataAdapter da = new SqlDataAdapter("select * from items_bc", daml.);
                DataTable dt = new DataTable();
                DataTable dtdtl = new DataTable();
                
                DataTable dts = new DataTable();
                DataTable dtp = new DataTable();

                dt = daml.SELECT_QUIRY_only_retrn_dt("select * from acc_hdr where a_type in('04','05','14','15','06','07','16','17') and a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "'");
                dtdtl = daml.SELECT_QUIRY_only_retrn_dt("select * from acc_dtl where a_type in('04','05','14','15','06','07','16','17') and a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "'");

                dts = daml.SELECT_QUIRY_only_retrn_dt("select * from sales_hdr where invmdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "'");
                dtp = daml.SELECT_QUIRY_only_retrn_dt("select * from pu_hdr where invmdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "'");
                
                // da.Fill(dt);
                //  MessageBox.Show(Convert.ToDateTime(dt.Rows[0][4]).ToString("yyyy-MM-dd HH:mm:ss.fff"));

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("لا يوجد حركات في المدئ المختار","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    button2.Enabled = true;
                    button2.Text = "ارسال الحركات";
                    return;
                }

                if (chk_fast.Checked)
                {
                   
                    using (SqlCommand cmd = new SqlCommand("update_acc_inv"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con_dest;
                        cmd.CommandTimeout = 0;
                       // cmd.Parameters.AddWithValue("@br_no", BL.CLS_Session.price_type.Equals("1") ? BL.CLS_Session.brno : BL.CLS_Session.sl_no);
                        cmd.Parameters.AddWithValue("@acc_hdr_tb", dt);
                        cmd.Parameters.AddWithValue("@acc_dtl_tb", dtdtl);

                        cmd.Parameters.AddWithValue("@sales_hdr_tb", dts);
                        cmd.Parameters.AddWithValue("@pu_hdr_tb", dtp);

                      //  cmd.Parameters.AddWithValue("@NO_items_updated", 0);
                      //  cmd.Parameters["@NO_items_updated"].Direction =ParameterDirection.Output;
                        if (con_dest.State == ConnectionState.Closed) con_dest.Open();
                        cmd.ExecuteNonQuery();
                        con_dest.Close();

                      //  MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        MessageBox.Show("Trans. Updated : " + dt.Rows.Count.ToString(), "عدد الحركات اللتي تم ارسالها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    }
                    button2.Enabled = true;
                    button2.Text = "ارسال الحركات";
                }
                else
                {
                    progressBar1.Visible = true;
                    count_hdr = dt.Rows.Count;
                    progressBar1.Maximum = dt.Rows.Count - 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //  MessageBox.Show(Convert.ToDateTime(dt.Rows[i][5]).ToString());
                        string StrQuery = " MERGE pos_hdr as t"
                            //  + " USING (select '" + dt.Rows[i][0] + "' as brno, '" + dt.Rows[i][1] + "' as slno, " + dt.Rows[i][2] + " as ref," + dt.Rows[i][3] + " as contr, " + dt.Rows[i][4] + " as [type], '" + Convert.ToDateTime(dt.Rows[i][5]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' as date," + dt.Rows[i][6] + " as total," + dt.Rows[i][7] + " as count," + dt.Rows[i][8] + " as payed," + dt.Rows[i][9] + " as total_cost,'" + dt.Rows[i][10] + "' as saleman," + dt.Rows[i][11] + " as req_no," + dt.Rows[i][12] + " as cust_no," + dt.Rows[i][13] + " as discount," + dt.Rows[i][14] + " as net_total,'" + Convert.ToDateTime(dt.Rows[i][15]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' as sysdate," + dt.Rows[i][16] + " as gen_ref," + dt.Rows[i][17] + " as tax_amt) as s"
                            //   + " USING (select '" + dt.Rows[i][0] + "' as brno, '" + dt.Rows[i][1] + "' as slno, " + dt.Rows[i][2] + " as ref," + dt.Rows[i][3] + " as contr, " + dt.Rows[i][4] + " as [type], convert(datetime,'" + Convert.ToDateTime(dt.Rows[i][5]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "') as date," + dt.Rows[i][6] + " as total," + dt.Rows[i][7] + " as count," + dt.Rows[i][8] + " as payed," + dt.Rows[i][9] + " as total_cost,'" + dt.Rows[i][10] + "' as saleman," + dt.Rows[i][11] + " as req_no," + dt.Rows[i][12] + " as cust_no," + dt.Rows[i][13] + " as discount," + dt.Rows[i][14] + " as net_total, convert(datetime,'" + Convert.ToDateTime(dt.Rows[i][15]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "') as sysdate," + dt.Rows[i][16] + " as gen_ref," + dt.Rows[i][17] + " as tax_amt) as s"
                                        + " USING (select '" + dt.Rows[i][0] + "' as brno, '" + dt.Rows[i][1] + "' as slno, " + dt.Rows[i][2] + " as ref," + dt.Rows[i][3] + " as contr, " + dt.Rows[i][4] + " as [type],'" + Convert.ToDateTime(dt.Rows[i][5]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "' as date," + dt.Rows[i][6] + " as total," + dt.Rows[i][7] + " as count," + dt.Rows[i][8] + " as payed," + dt.Rows[i][9] + " as total_cost,'" + dt.Rows[i][10] + "' as saleman," + dt.Rows[i][11] + " as req_no," + dt.Rows[i][12] + " as cust_no," + dt.Rows[i][13] + " as discount," + dt.Rows[i][14] + " as net_total,'" + Convert.ToDateTime(dt.Rows[i][15]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "' as sysdate," + dt.Rows[i][16] + " as gen_ref," + dt.Rows[i][17] + " as tax_amt," + dt.Rows[i][18] + " as dscper," + dt.Rows[i][19] + " as card_type," + dt.Rows[i][20] + " as card_amt," + dt.Rows[i][21] + " as rref," + dt.Rows[i][22] + " as rcontr,isnull(" + dt.Rows[i][23] + ",0)  as taxfree_amt,isnull('" + dt.Rows[i][24] + "','') as note,isnull('" + dt.Rows[i][25] + "','') as mobile) as s"
                                        + " ON t.brno=s.brno and T.slno=S.slno and T.ref=S.ref and T.contr=S.contr"
                                        + " WHEN MATCHED THEN"
                                        + " UPDATE SET T.total = S.total,T.discount = S.discount,T.net_total = S.net_total,t.tax_amt=s.tax_amt"
                                        + " WHEN NOT MATCHED THEN"
                                        + " INSERT VALUES(s.brno,s.slno, s.ref, s.contr,s.type,s.date,s.total,s.count,s.payed,s.total_cost,s.saleman,s.req_no,s.cust_no,s.discount,s.net_total,s.sysdate,s.gen_ref,s.tax_amt,s.dscper,s.card_type,s.card_amt,s.rref,s.rcontr,s.taxfree_amt,s.note,s.mobile);";
                        //try
                        //{
                        //SqlConnection conn = new SqlConnection();


                        using (SqlCommand comm = new SqlCommand(StrQuery, con_dest))
                        {
                            comm.CommandTimeout = 0;
                            if (con_dest.State != ConnectionState.Open)
                            {
                                con_dest.Open();
                            }
                            comm.ExecuteNonQuery();

                        }

                        progressBar1.Value = i;
                        // daml.Insert_Update_Delete(StrQuery, false);

                        //    }
                        //    catch (Exception ex)
                        //    {
                        //         MessageBox.Show(ex.ToString());
                        //    }
                        //    finally
                        //    {

                        //    }

                    }
                    // daml.SqlCon_Close();
                    if (con_dest.State == ConnectionState.Open)
                    {
                        con_dest.Close();
                    }

                    // MessageBox.Show("ok sales_hd");
                    update_pos_dtl();

                    button2.Enabled = true;
                    button2.Text = "ارسال الفواتير";
                    //wf.Close();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message);
            button2.Enabled = true;
            button2.Text = "ارسال الفواتير";
            }

        
        }
        private void update_pos_dtl()
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

        private void txt_fmdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_tmdate.Focus();
                // txt_hdate.SelectionStart = 0;
                txt_tmdate.SelectionLength = 0;
            }
        }

        private void txt_tmdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               // txt_tmdate.Focus();
                // txt_hdate.SelectionStart = 0;
               // txt_tmdate.SelectionLength = 0;
                button2.Focus();
            }
        }

        private void txt_fmdate_Enter(object sender, EventArgs e)
        {
            txt_fmdate.Focus();
            txt_fmdate.SelectAll();
        }

        private void txt_tmdate_Enter(object sender, EventArgs e)
        {
            txt_tmdate.Focus();
            txt_tmdate.SelectAll();
        }

        private void txt_fmdate_Validating(object sender, CancelEventArgs e)
        {
            DateTime rs;

            CultureInfo ci = new CultureInfo("en-US", false);

            if (!DateTime.TryParseExact(this.txt_fmdate.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            {

                MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                txt_fmdate.Focus();

            }
            //if (Convert.ToInt32(datval.convert_to_yyyyDDdd(txt_fmdate.Text)) < Convert.ToInt32(datval.convert_to_yyyyDDdd(BL.CLS_Session.start_dt)) || Convert.ToInt32(datval.convert_to_yyyyDDdd(txt_fmdate.Text)) > Convert.ToInt32(datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt)))
            //{

            //    MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    e.Cancel = true;

            //}
        }

        private void txt_tmdate_Validating(object sender, CancelEventArgs e)
        {
            DateTime rs;

            CultureInfo ci = new CultureInfo("en-US", false);

            if (!DateTime.TryParseExact(this.txt_tmdate.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            {

                MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                txt_tmdate.Focus();

            }
        }


        private void send_del_h()
        {

            try
            {
                CultureInfo culture = new CultureInfo("en-US", false);
                DateTime tempDate = Convert.ToDateTime(datval.convert_to_yyyy_MMddwith_dash(txt_tmdate.Text) + " 05:00:00.000", culture).AddHours(24);
                zzz = tempDate.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

                // MessageBox.Show(zzz);
               
                // progressBar1.Maximum = 200;

                //  for (ii = 0; ii <= 200; ii++)
                //  {
                //     progressBar1.Value = ii;
                //  }
                // daml.SqlCon_Open();
                SqlConnection con_dest = new SqlConnection("Data Source=" + textBox1 + ";Initial Catalog=" + textBox2 + ";User id=" + textBox3 + ";Password=" + textBox4 + ";Connection Timeout=" + textBox5 + "");

                // SqlDataAdapter da = new SqlDataAdapter("select * from items_bc", daml.);
                DataTable dt = new DataTable();

                dt = daml.SELECT_QUIRY_only_retrn_dt("select * from d_pos_hdr where date between '" + datval.convert_to_yyyy_MMddwith_dash(txt_fmdate.Text) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000'");
                // da.Fill(dt);
                //  MessageBox.Show(Convert.ToDateTime(dt.Rows[0][4]).ToString("yyyy-MM-dd HH:mm:ss.fff"));
               
                if (dt.Rows.Count == 0)
                {
                   // MessageBox.Show("لا يوجد فواتير في المدئ المختار", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


               
               
              
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //  MessageBox.Show(Convert.ToDateTime(dt.Rows[i][5]).ToString());
                    string StrQuery = " MERGE d_pos_hdr as t"
                        //  + " USING (select '" + dt.Rows[i][0] + "' as brno, '" + dt.Rows[i][1] + "' as slno, " + dt.Rows[i][2] + " as ref," + dt.Rows[i][3] + " as contr, " + dt.Rows[i][4] + " as [type], '" + Convert.ToDateTime(dt.Rows[i][5]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' as date," + dt.Rows[i][6] + " as total," + dt.Rows[i][7] + " as count," + dt.Rows[i][8] + " as payed," + dt.Rows[i][9] + " as total_cost,'" + dt.Rows[i][10] + "' as saleman," + dt.Rows[i][11] + " as req_no," + dt.Rows[i][12] + " as cust_no," + dt.Rows[i][13] + " as discount," + dt.Rows[i][14] + " as net_total,'" + Convert.ToDateTime(dt.Rows[i][15]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' as sysdate," + dt.Rows[i][16] + " as gen_ref," + dt.Rows[i][17] + " as tax_amt) as s"
                        //   + " USING (select '" + dt.Rows[i][0] + "' as brno, '" + dt.Rows[i][1] + "' as slno, " + dt.Rows[i][2] + " as ref," + dt.Rows[i][3] + " as contr, " + dt.Rows[i][4] + " as [type], convert(datetime,'" + Convert.ToDateTime(dt.Rows[i][5]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "') as date," + dt.Rows[i][6] + " as total," + dt.Rows[i][7] + " as count," + dt.Rows[i][8] + " as payed," + dt.Rows[i][9] + " as total_cost,'" + dt.Rows[i][10] + "' as saleman," + dt.Rows[i][11] + " as req_no," + dt.Rows[i][12] + " as cust_no," + dt.Rows[i][13] + " as discount," + dt.Rows[i][14] + " as net_total, convert(datetime,'" + Convert.ToDateTime(dt.Rows[i][15]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "') as sysdate," + dt.Rows[i][16] + " as gen_ref," + dt.Rows[i][17] + " as tax_amt) as s"
                                   + " USING (select '" + dt.Rows[i][0] + "' as brno, '" + dt.Rows[i][1] + "' as slno, " + dt.Rows[i][2] + " as ref," + dt.Rows[i][3] + " as contr, " + dt.Rows[i][4] + " as [type],'" + Convert.ToDateTime(dt.Rows[i][5]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "' as date," + dt.Rows[i][6] + " as total," + dt.Rows[i][7] + " as count," + dt.Rows[i][8] + " as payed," + dt.Rows[i][9] + " as total_cost,'" + dt.Rows[i][10] + "' as saleman," + dt.Rows[i][11] + " as req_no," + dt.Rows[i][12] + " as cust_no," + dt.Rows[i][13] + " as discount," + dt.Rows[i][14] + " as net_total,'" + Convert.ToDateTime(dt.Rows[i][15]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "' as sysdate," + dt.Rows[i][16] + " as gen_ref," + dt.Rows[i][17] + " as tax_amt," + dt.Rows[i][18] + " as o_ref) as s"
                                   + " ON t.brno=s.brno and T.slno=S.slno and T.ref=S.ref and T.contr=S.contr"
                                   + " WHEN MATCHED THEN"
                                   + " UPDATE SET T.total = S.total,T.discount = S.discount,T.net_total = S.net_total,t.tax_amt=s.tax_amt"
                                   + " WHEN NOT MATCHED THEN"
                                   + " INSERT VALUES(s.brno,s.slno, s.ref, s.contr,s.type,s.date,s.total,s.count,s.payed,s.total_cost,s.saleman,s.req_no,s.cust_no,s.discount,s.net_total,s.sysdate,s.gen_ref,s.tax_amt,s.o_ref);";
                    //try
                    //{
                    //SqlConnection conn = new SqlConnection();


                    using (SqlCommand comm = new SqlCommand(StrQuery, con_dest))
                    {
                        if (con_dest.State != ConnectionState.Open)
                        {
                            con_dest.Open();
                        }
                        comm.ExecuteNonQuery();

                    }

                   
                    // daml.Insert_Update_Delete(StrQuery, false);

                    //    }
                    //    catch (Exception ex)
                    //    {
                    //         MessageBox.Show(ex.ToString());
                    //    }
                    //    finally
                    //    {

                    //    }

                }
                // daml.SqlCon_Close();
                if (con_dest.State == ConnectionState.Open)
                {
                    con_dest.Close();
                }

                // MessageBox.Show("ok sales_hd");
                update_d_pos_dtl();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }
        private void update_d_pos_dtl()
        {

            try
            {
               
                // daml.SqlCon_Open();
                SqlConnection con_dest = new SqlConnection("Data Source=" + textBox1 + ";Initial Catalog=" + textBox2 + ";User id=" + textBox3 + ";Password=" + textBox4 + ";Connection Timeout=" + textBox5 + "");

                // SqlDataAdapter da = new SqlDataAdapter("select * from items_bc", daml.);
                DataTable dt = new DataTable();

                dt = daml.SELECT_QUIRY_only_retrn_dt("select * from d_pos_dtl a join d_pos_hdr b on a.brno=b.brno and a.slno=b.slno and a.ref=b.ref and a.contr=b.contr where b.date between '" + datval.convert_to_yyyy_MMddwith_dash(txt_fmdate.Text) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000'");
                // da.Fill(dt);
                // MessageBox.Show(Convert.ToDateTime(dt.Rows[0][4]).ToString("yyyy-MM-dd HH:mm:ss.fff"));
                // con_dest.Open();

              //  MessageBox.Show(dt.Rows.Count.ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string StrQuery = " MERGE d_pos_dtl as t"
                                    + " USING (select '" + dt.Rows[i][0].ToString() + "' as brno, '" + dt.Rows[i][1].ToString() + "' as slno, " + dt.Rows[i][2].ToString() + " as ref," + dt.Rows[i][3].ToString() + " as contr," + dt.Rows[i][4].ToString() + " as type,'" + dt.Rows[i][5].ToString() + "' as barcode,'" + dt.Rows[i][6].ToString() + "' as name,'" + dt.Rows[i][7].ToString() + "' as unit," + dt.Rows[i][8].ToString() + " as price," + dt.Rows[i][9].ToString() + " as qty," + dt.Rows[i][10].ToString() + " as cost," + dt.Rows[i][11].ToString() + " as is_req, '" + dt.Rows[i][12].ToString() + "' as itemno, " + dt.Rows[i][13].ToString() + " as srno," + dt.Rows[i][14].ToString() + " as pkqty," + dt.Rows[i][15].ToString() + " as discpc," + dt.Rows[i][16].ToString() + " as tax_id," + dt.Rows[i][17].ToString() + " as tax_amt) as s"
                                    + " ON t.brno=s.brno and T.slno=S.slno and T.ref=S.ref and T.contr=S.contr and t.srno=s.srno"
                                    + " WHEN MATCHED THEN"
                                    + " UPDATE SET T.price = S.price,T.qty=S.qty,t.itemno=s.itemno,t.srno=s.srno"
                                    + " WHEN NOT MATCHED THEN"
                                    + " INSERT VALUES(s.brno,s.slno, s.ref, s.contr,s.type,s.barcode,s.name,s.unit,s.price,s.qty,s.cost,s.is_req,s.itemno,s.srno,s.pkqty,s.discpc,s.tax_id,s.tax_amt);";
                    //try
                    //{
                    //SqlConnection conn = new SqlConnection();


                    using (SqlCommand comm = new SqlCommand(StrQuery, con_dest))
                    {
                        if (con_dest.State != ConnectionState.Open)
                        {
                            con_dest.Open();
                        }
                        comm.ExecuteNonQuery();

                    }
                   
                    // daml.Insert_Update_Delete(StrQuery, false);

                    //    }
                    //    catch (Exception ex)
                    //    {
                    //         MessageBox.Show(ex.ToString());
                    //    }
                    //    finally
                    //    {

                    //    }

                }
                // daml.SqlCon_Close();
                if (con_dest.State == ConnectionState.Open)
                {
                    con_dest.Close();
                }
                MessageBox.Show("Sales invoces sent : " + count_hdr.ToString() + "\n\r Items sent : " + count_dtl.ToString(), "عدد الفواير والاصناف المرسلة", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                progressBar1.Visible = false;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }

        private void chk_all_itms_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_all_itms.Checked)
                chk_offers.Checked = true;
        }
    }
}
