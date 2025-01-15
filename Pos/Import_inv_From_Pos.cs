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

namespace POS.Pos
{
    public partial class Import_inv_From_Pos : BaseForm
    {
        SqlConnection con2 = BL.DAML.con;
        SqlCommand myCommand;
        SqlConnection con = BL.DAML.con;
        BL.DAML daml = new BL.DAML();
        DataTable dtcntrs;
        BL.Date_Validate datval = new BL.Date_Validate();
        string textBox1, textBox2, textBox3, textBox4, textBox5, zzz;
        int count_hdr, count_dtl;
        public Import_inv_From_Pos()
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
            try
            {
                //WaitForm wf = new WaitForm("");
                //wf.MdiParent = MdiParent;
                //wf.Show();
                //Application.DoEvents();

                //button1.Text = "يرجى الانتظار";
                //button1.Enabled = false;

                //if (chk_all_itms.Checked)
                //{
                //    SqlCommand myCommand = new SqlCommand();
                //    //1 SqlDataReader myReader;

                //    myCommand.CommandType = CommandType.StoredProcedure;
                //    myCommand.Connection = con;
                //    myCommand.CommandText = "update_items";
                //    //2 myCommand.Parameters.Add("@NO_items_updated", OleDbType.Integer);

                //    myCommand.Parameters.AddWithValue("@server", textBox1);
                //    myCommand.Parameters.AddWithValue("@db", textBox2);
                //    // myCommand.Parameters.Add("@NO_items_updated", 0);

                //    myCommand.Parameters.Add("@NO_items_updated", 0);
                //    myCommand.Parameters.Add("@errstatus", 0);
                //    myCommand.Parameters.AddWithValue("@sl_man", chk_salman.Checked ? 1 : 0);
                //    myCommand.Parameters.AddWithValue("@br_no", BL.CLS_Session.brno);

                //    myCommand.Parameters["@NO_items_updated"].Direction = ParameterDirection.Output;
                //    myCommand.Parameters["@errstatus"].Direction = ParameterDirection.Output;
                //    try
                //    {
                //        con.Open();

                //        //3  myReader = myCommand.ExecuteReader();
                //        myCommand.ExecuteNonQuery();
                //        //Uncomment this line to return the proper output value.
                //        //myReader.Close();
                //        MessageBox.Show("Items Updated : " + myCommand.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //        // MessageBox.Show("Return Value : " + myCommand.Parameters["@errstatus"].Value);
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.ToString());
                //    }
                //    finally
                //    {
                //        con.Close();
                //    }
                //}
                //else
                //{

                //    SqlConnection con_src = new SqlConnection("Data Source=" + textBox1 + ";Initial Catalog=" + textBox2 + ";User id=" + textBox3 + ";Password=" + textBox4 + ";Connection Timeout=" + textBox5 + "");
                //    DataTable dtitm = new DataTable();
                //    DataTable dtitmbc = new DataTable();
                //    DataTable dtbrpric = new DataTable();
                //    DataTable dtpossal = new DataTable(); ;

                //    DateTime dtn = DateTime.Now.AddDays(-(Convert.ToInt32(txt_newp.Text)));
                //    // con_src.Open();
                //    //cmd1.Parameters.AddWithValue("@a23", DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false)));
                //    string dtms = DateTime.Now.AddDays(-(Convert.ToInt32(txt_newp.Text))).ToString("yyyyMMdd", new CultureInfo("en-US", false));

                //    SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM items where last_updt >= '" + dtms + "'", con_src);
                //    SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM items_bc a where exists(SELECT * FROM items b where a.item_no=b.item_no and b.last_updt >= '" + dtms + "')", con_src);
                //    SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM brprices a where a.branch='" + BL.CLS_Session.brno + "' and exists(SELECT * FROM items b where a.itemno=b.item_no and b.last_updt >= '" + dtms + "')", con_src);
                //    SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM pos_salmen where sl_brno='" + BL.CLS_Session.brno + "'", con_src);

                //    da1.Fill(dtitm);
                //    da2.Fill(dtitmbc);
                //    da3.Fill(dtbrpric);
                //    da4.Fill(dtpossal);
                //    //   dtitm = daml.SELECT_QUIRY_only_retrn_dt("SELECT * FROM items where last_updt >= '" + dtms + "'");
                //    //    dtitmbc = daml.SELECT_QUIRY_only_retrn_dt("SELECT * FROM items_bc a where exists(SELECT * FROM items b where a.item_no=b.item_no and b.last_updt >= '" + dtms + "')");
                //    //    dtbrpric = daml.SELECT_QUIRY_only_retrn_dt("SELECT * FROM brprices a where a.branch='" + BL.CLS_Session.brno + "' and exists(SELECT * FROM items b where a.itemno=b.item_no and b.last_updt >= '" + dtms + "')");
                //    //   dtpossal = daml.SELECT_QUIRY_only_retrn_dt("SELECT * FROM pos_salmen where sl_brno='" + BL.CLS_Session.brno + "'");


                //    using (SqlCommand cmd = new SqlCommand("update_items_table"))
                //    {

                //        cmd.CommandType = CommandType.StoredProcedure;
                //        cmd.Connection = con;
                //        cmd.Parameters.AddWithValue("@items_tb", dtitm);
                //        cmd.Parameters.AddWithValue("@items_bc_tb", dtitmbc);
                //        cmd.Parameters.AddWithValue("@brprices_tb", dtbrpric);
                //        cmd.Parameters.AddWithValue("@pos_salmen_tb", dtpossal);
                //        cmd.Parameters.AddWithValue("@sl_man", chk_salman.Checked ? 1 : 0);
                //        cmd.Parameters.AddWithValue("@NO_items_updated", 0);
                //        cmd.Parameters["@NO_items_updated"].Direction = ParameterDirection.Output;
                //        con.Open();
                //        cmd.ExecuteNonQuery();
                //        con.Close();

                //        MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                //    }

                //}
                ///*
                //try
                //{
                //    if (string.IsNullOrEmpty(txt_newp.Text) || txt_newp.Text.Equals("0") || string.IsNullOrEmpty(txt_code.Text))
                //    {
                //        MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Enter Item and New Price" : "ادخل الصنف والسعر الجديد", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        txt_newp.Focus();
                //        return;
                
                //    }
                //    using (SqlCommand cmd1 = new SqlCommand("update items set item_price=@a11 where item_barcode=@a33", con2))
                //    {


                //        cmd1.Parameters.AddWithValue("@a11", txt_newp.Text);
                //        cmd1.Parameters.AddWithValue("@a33", txt_code.Text);
                //        con2.Open();
                //        cmd1.ExecuteNonQuery();
                //        con2.Close();
                //    }
                //    using (SqlCommand cmd2 = new SqlCommand("update items_bc set price=@a22 where barcode=@a44", con2))
                //    {


                //        cmd2.Parameters.AddWithValue("@a22", txt_newp.Text);
                //        cmd2.Parameters.AddWithValue("@a44", txt_code.Text);
                //        con2.Open();
                //        cmd2.ExecuteNonQuery();
                //        con2.Close();
                //    }

                //    MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Item Price Changed" : "تم تعديل سعر الصنف", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                //    txt_code.Text = "";
                //    txt_name.Text = "";
                //    txt_oldp.Text = "";
                //    txt_newp.Text = "";
                //    //using (SqlCommand cmd3 = new SqlCommand("update brprices set lprice1=@a111 where itemno=@a333 and branch=@a444", con2))
                //    //{
                //    txt_code.Select();

                //    //    cmd3.Parameters.AddWithValue("@a111", Convert.ToDouble(txt_brprice.Text));
                //    //    //  cmd3.Parameters.AddWithValue("@a22", textBox4.Text);
                //    //    cmd3.Parameters.AddWithValue("@a333", textBox1.Text);
                //    //    cmd3.Parameters.AddWithValue("@a444", BL.CLS_Session.brno);

                //    //    con2.Open();
                //    //    cmd3.ExecuteNonQuery();
                //    //    con2.Close();
                //    //}
            
                //}

                //catch { }
                // */
                //button1.Enabled = true;
                //button1.Text = "تحديث الاصناف";
                //wf.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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



                  //  txt_code.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                  //  txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                    /*
                   dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
                   dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
                   dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                    */



                }
                if (e.KeyCode == Keys.Enter)
                {


                    //if (!string.IsNullOrEmpty(txt_code.Text))
                    //{

                    //    string condi = label8.Text.Equals("الباركود") ? " and b.barcode='" + txt_code.Text + "'" : " and i.item_no='" + txt_code.Text + "'";
                    //    // select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img from items i join items_bc b on b.item_no=i.item_no and i.item_no='" + textBox3.Text + "' join taxs t on i.item_tax=t.tax_id", con2);
                    //    SqlDataAdapter da23 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img from items i join items_bc b on b.item_no=i.item_no " + condi + " join taxs t on i.item_tax=t.tax_id", con2);
                    //    DataTable dt = new DataTable();
                    //    da23.Fill(dt);

                    //    if (dt.Rows.Count == 1)
                    //    {
                    //        txt_name.Text = dt.Rows[0][1].ToString();
                    //        txt_oldp.Text = dt.Rows[0][3].ToString();
                    //        txt_newp.Select();
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("ادخل رقم الصنف");

                    //}

                }
            }
            catch
            { }


                
        }

        private void Price_Chang_Load(object sender, EventArgs e)
        {
            if (BL.CLS_Session.ismain)
            {
                string sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";
                label5.Text = "اسم المركز";
                dtcntrs = daml.SELECT_QUIRY_only_retrn_dt("select srcst,sl_name from slcenters where sl_brno='" + BL.CLS_Session.brno + "' and sl_brno + sl_no in(" + sl + ") ");
                cmb_ctrno.DataSource = dtcntrs;
                cmb_ctrno.DisplayMember = "sl_name";
                cmb_ctrno.ValueMember = "srcst";
               // cmb_ctrno.SelectedIndex = -1;
            }
            else
            {
                dtcntrs = daml.SELECT_QUIRY_only_retrn_dt("select contr_id,contr_name from contrs where c_brno='" + BL.CLS_Session.brno + "'");
                cmb_ctrno.DataSource = dtcntrs;
                cmb_ctrno.DisplayMember = "contr_name";
                cmb_ctrno.ValueMember = "contr_id";
                cmb_ctrno.SelectedIndex = -1;
            }
            //MessageBox.Show("poslink" + BL.CLS_Session.brno + BL.CLS_Session.contr_id);
            txt_fmdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            txt_tmdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

           
           
           // txt_code.Select();
            SqlDataAdapter da = new SqlDataAdapter("select * from server", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            textBox1 = dt.Rows[0][0].ToString();
            textBox2 = dt.Rows[0][1].ToString();
            textBox3 = dt.Rows[0][2].ToString();
            textBox4 = dt.Rows[0][3].ToString();
            textBox5 = dt.Rows[0][4].ToString();
            cmb_ctrno.Select();

        }

        private void txt_newp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               // button1.Select();
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
            button2.Enabled = false;
            button2.Text = "يرجى الانتظار";

            WaitForm wf = new WaitForm("");
            wf.MdiParent = MdiParent;
            wf.Show();
            Application.DoEvents();

            CultureInfo culture = new CultureInfo("en-US");
            DateTime tempDate = Convert.ToDateTime(datval.convert_to_yyyy_MMddwith_dash(txt_tmdate.Text) + " 04:59:59.999", culture).AddHours(24);
            string  yy1 = tempDate.ToString("dd-MM-yyyy", new CultureInfo("en-US"));
            string xx1 = txt_fmdate.Text;

            string condt = string.IsNullOrEmpty(cmb_ctrno.Text) ? " " : BL.CLS_Session.ismain ? " and b.slno<>'" + cmb_ctrno.SelectedValue.ToString() + "' " : " and b.contr=" + cmb_ctrno.SelectedValue.ToString() + " ";
            string toprint = "";
            myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Connection = con;
            ////SqlCommand myCommand = new SqlCommand();
            //////1 SqlDataReader myReader;

            ////myCommand.CommandType = CommandType.StoredProcedure;
            ////myCommand.Connection = con;
            ////myCommand.CommandText = "import_inv_from_pos";
            //////2 myCommand.Parameters.Add("@NO_items_updated", OleDbType.Integer);

            ////myCommand.Parameters.AddWithValue("@whr_condn", " b.[date] between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condt + "");
            ////myCommand.Parameters.AddWithValue("@poslink", "poslink" + BL.CLS_Session.brno + BL.CLS_Session.contr_id);
            ////// myCommand.Parameters.Add("@NO_items_updated", 0);

            ////myCommand.Parameters.Add("@no_of_invoices", 0);
            ////myCommand.Parameters.Add("@errstatus", 0);
            ////// myCommand.Parameters.AddWithValue("@sl_man", chk_salman.Checked ? 1 : 0);
            ////// myCommand.Parameters.AddWithValue("@br_no", BL.CLS_Session.brno);

            ////myCommand.Parameters["@no_of_invoices"].Direction = ParameterDirection.Output;
            ////myCommand.Parameters["@errstatus"].Direction = ParameterDirection.Output;
            ////try
            ////{
            ////    if (con.State == ConnectionState.Closed)
            ////        con.Open();
            ////    // con.Open();

            ////    //3  myReader = myCommand.ExecuteReader();
            ////    myCommand.ExecuteNonQuery();
            ////    //Uncomment this line to return the proper output value.
            ////    //myReader.Close();
            ////    MessageBox.Show("Invocies Updated : " + myCommand.Parameters["@no_of_invoices"].Value, "عدد الفواتير اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            ////    // MessageBox.Show("Return Value : " + myCommand.Parameters["@errstatus"].Value);
            ////}
            ////catch (Exception ex)
            ////{
            ////    MessageBox.Show(ex.Message);
            ////}
            ////finally
            ////{
            ////    con.Close();
            ////}

            if (!string.IsNullOrEmpty(cmb_ctrno.Text))
            {
                
                //if (con.State == ConnectionState.Open)
                //    con.Close();

              //  myCommand = new SqlCommand();
                //1 SqlDataReader myReader;

                //myCommand.CommandType = CommandType.StoredProcedure;
                //myCommand.Connection = con;
                myCommand.CommandText = BL.CLS_Session.ismain ? "import_inv_from_branch" : "import_inv_from_pos";
                //2 myCommand.Parameters.Add("@NO_items_updated", OleDbType.Integer);

                myCommand.Parameters.AddWithValue("@whr_condn", " b.[date] between '" + datval.convert_to_yyyyDDdd(xx1) + " 05:00:00.000' and '" + datval.convert_to_yyyyDDdd(yy1) + " 05:00:00.000' " + condt + "");
                if(BL.CLS_Session.ismain)
                    myCommand.Parameters.AddWithValue("@brlink", cmb_ctrno.SelectedValue.ToString().Trim() );
                else
                    myCommand.Parameters.AddWithValue("@poslink", "poslink" + BL.CLS_Session.brno + cmb_ctrno.SelectedValue.ToString());
                // myCommand.Parameters.Add("@NO_items_updated", 0);

                myCommand.Parameters.Add("@no_of_invoices", 0);
                myCommand.Parameters.Add("@errstatus", 0);
                // myCommand.Parameters.AddWithValue("@sl_man", chk_salman.Checked ? 1 : 0);
                // myCommand.Parameters.AddWithValue("@br_no", BL.CLS_Session.brno);

                myCommand.Parameters["@no_of_invoices"].Direction = ParameterDirection.Output;
                myCommand.Parameters["@errstatus"].Direction = ParameterDirection.Output;
                try
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                   // con.Open();

                    //3  myReader = myCommand.ExecuteReader();
                    myCommand.ExecuteNonQuery();
                    //Uncomment this line to return the proper output value.
                    //myReader.Close();
                  //  wf.Close();
                    MessageBox.Show("Invocies Updated : " + myCommand.Parameters["@no_of_invoices"].Value, "عدد الفواتير اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    // MessageBox.Show("Return Value : " + myCommand.Parameters["@errstatus"].Value);
                }
                catch (Exception ex)
                {
                  //  wf.Close();
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    wf.Close();
                    con.Close();
                    button2.Text = "جلب الفواتير";
                    button2.Enabled = true;
                }
            }
            else
            {
                //if (con.State == ConnectionState.Open)
                //    con.Close();

               // SqlCommand myCommand = new SqlCommand();
               
                foreach (DataRow dtr in dtcntrs.Rows)
                {
                  //  myCommand = new SqlCommand();
                    //1 SqlDataReader myReader;
                   // MessageBox.Show(dtr[0].ToString());
                  //  myCommand.CommandType = CommandType.StoredProcedure;
                  //  myCommand.Connection = con;
                    myCommand.CommandText = BL.CLS_Session.ismain ? "import_inv_from_branch" : "import_inv_from_pos";
                    //2 myCommand.Parameters.Add("@NO_items_updated", OleDbType.Integer);

                    myCommand.Parameters.AddWithValue("@whr_condn", " b.[date] between '" + datval.convert_to_yyyyDDdd(xx1) + " 05:00:00.000' and '" + datval.convert_to_yyyyDDdd(yy1) + " 05:00:00.000' ");
                    if (BL.CLS_Session.ismain)
                        myCommand.Parameters.AddWithValue("@brlink", cmb_ctrno.SelectedValue.ToString().Trim());
                    else
                        myCommand.Parameters.AddWithValue("@poslink", "poslink" + BL.CLS_Session.brno + cmb_ctrno.SelectedValue.ToString());
                    // myCommand.Parameters.Add("@NO_items_updated", 0);

                    myCommand.Parameters.Add("@no_of_invoices", 0);
                    myCommand.Parameters.Add("@errstatus", 0);
                    // myCommand.Parameters.AddWithValue("@sl_man", chk_salman.Checked ? 1 : 0);
                    // myCommand.Parameters.AddWithValue("@br_no", BL.CLS_Session.brno);

                    myCommand.Parameters["@no_of_invoices"].Direction = ParameterDirection.Output;
                    myCommand.Parameters["@errstatus"].Direction = ParameterDirection.Output;
                    try
                    {
                        if (con.State == ConnectionState.Closed)
                        con.Open();

                        //3  myReader = myCommand.ExecuteReader();
                        myCommand.ExecuteNonQuery();

                        toprint = toprint + Environment.NewLine + dtr[1].ToString() + " : " + myCommand.Parameters["@no_of_invoices"].Value.ToString();
                        //Uncomment this line to return the proper output value.
                        //myReader.Close();
                       //// MessageBox.Show("Invocies Updated : " + myCommand.Parameters["@no_of_invoices"].Value, "عدد الفواتير اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        // MessageBox.Show("Return Value : " + myCommand.Parameters["@errstatus"].Value);
                    }

                    catch (Exception ex)
                    {
                      //  MessageBox.Show(ex.Message + Environment.NewLine + dtr[1].ToString());
                      // wf.Close();
                       toprint = toprint + Environment.NewLine + dtr[1].ToString() + " : " + "0";
                    }
                    finally
                    {
                        wf.Close();
                        con.Close();
                        button2.Text = "جلب الفواتير";
                        button2.Enabled = true;
                    }
                }

               // wf.Close();
                MessageBox.Show("Invocies Updated : " + Environment.NewLine + toprint, "عدد الفواتير اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                       
            }
            

            //button2.Text = "يرجى الانتظار";
            //button2.Enabled = false;
            //try
            //{
            //    //WaitForm wf = new WaitForm("");
            //    //wf.MdiParent = MdiParent;
            //    //wf.Show();
            //    //Application.DoEvents();

            //    CultureInfo culture = new CultureInfo("en-US",false);
            //    DateTime tempDate = Convert.ToDateTime(datval.convert_to_yyyy_MMddwith_dash(txt_tmdate.Text) + " 05:00:00.000", culture).AddHours(24);
            //    zzz = tempDate.ToString("dd-MM-yyyy", new CultureInfo("en-US",false));

            //   // MessageBox.Show(zzz);
            //    progressBar1.Minimum = 0;
            //    // progressBar1.Maximum = 200;

            //    //  for (ii = 0; ii <= 200; ii++)
            //    //  {
            //    //     progressBar1.Value = ii;
            //    //  }
            //    // daml.SqlCon_Open();
            //    SqlConnection con_dest = new SqlConnection("Data Source=" + textBox1 + ";Initial Catalog=" + textBox2 + ";User id=" + textBox3 + ";Password=" + textBox4 + ";Connection Timeout=" + textBox5 + "");

            //    // SqlDataAdapter da = new SqlDataAdapter("select * from items_bc", daml.);
            //    DataTable dt = new DataTable();

            //    dt = daml.SELECT_QUIRY_only_retrn_dt("select * from pos_hdr where date between '" + datval.convert_to_yyyy_MMddwith_dash(txt_fmdate.Text) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000'");
            //    // da.Fill(dt);
            //    //  MessageBox.Show(Convert.ToDateTime(dt.Rows[0][4]).ToString("yyyy-MM-dd HH:mm:ss.fff"));

            //    if (dt.Rows.Count == 0)
            //    {
            //        MessageBox.Show("لا يوجد فواتير في المدئ المختار","",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //        return;
            //    }


            //    progressBar1.Visible = true;
            //    count_hdr = dt.Rows.Count;
            //    progressBar1.Maximum = dt.Rows.Count - 1;
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        //  MessageBox.Show(Convert.ToDateTime(dt.Rows[i][5]).ToString());
            //        string StrQuery = " MERGE pos_hdr as t"
            //            //  + " USING (select '" + dt.Rows[i][0] + "' as brno, '" + dt.Rows[i][1] + "' as slno, " + dt.Rows[i][2] + " as ref," + dt.Rows[i][3] + " as contr, " + dt.Rows[i][4] + " as [type], '" + Convert.ToDateTime(dt.Rows[i][5]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' as date," + dt.Rows[i][6] + " as total," + dt.Rows[i][7] + " as count," + dt.Rows[i][8] + " as payed," + dt.Rows[i][9] + " as total_cost,'" + dt.Rows[i][10] + "' as saleman," + dt.Rows[i][11] + " as req_no," + dt.Rows[i][12] + " as cust_no," + dt.Rows[i][13] + " as discount," + dt.Rows[i][14] + " as net_total,'" + Convert.ToDateTime(dt.Rows[i][15]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' as sysdate," + dt.Rows[i][16] + " as gen_ref," + dt.Rows[i][17] + " as tax_amt) as s"
            //            //   + " USING (select '" + dt.Rows[i][0] + "' as brno, '" + dt.Rows[i][1] + "' as slno, " + dt.Rows[i][2] + " as ref," + dt.Rows[i][3] + " as contr, " + dt.Rows[i][4] + " as [type], convert(datetime,'" + Convert.ToDateTime(dt.Rows[i][5]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "') as date," + dt.Rows[i][6] + " as total," + dt.Rows[i][7] + " as count," + dt.Rows[i][8] + " as payed," + dt.Rows[i][9] + " as total_cost,'" + dt.Rows[i][10] + "' as saleman," + dt.Rows[i][11] + " as req_no," + dt.Rows[i][12] + " as cust_no," + dt.Rows[i][13] + " as discount," + dt.Rows[i][14] + " as net_total, convert(datetime,'" + Convert.ToDateTime(dt.Rows[i][15]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "') as sysdate," + dt.Rows[i][16] + " as gen_ref," + dt.Rows[i][17] + " as tax_amt) as s"
            //                        + " USING (select '" + dt.Rows[i][0] + "' as brno, '" + dt.Rows[i][1] + "' as slno, " + dt.Rows[i][2] + " as ref," + dt.Rows[i][3] + " as contr, " + dt.Rows[i][4] + " as [type],'" + Convert.ToDateTime(dt.Rows[i][5]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "' as date," + dt.Rows[i][6] + " as total," + dt.Rows[i][7] + " as count," + dt.Rows[i][8] + " as payed," + dt.Rows[i][9] + " as total_cost,'" + dt.Rows[i][10] + "' as saleman," + dt.Rows[i][11] + " as req_no," + dt.Rows[i][12] + " as cust_no," + dt.Rows[i][13] + " as discount," + dt.Rows[i][14] + " as net_total,'" + Convert.ToDateTime(dt.Rows[i][15]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "' as sysdate," + dt.Rows[i][16] + " as gen_ref," + dt.Rows[i][17] + " as tax_amt," + dt.Rows[i][18] + " as dscper," + dt.Rows[i][19] + " as card_type," + dt.Rows[i][20] + " as card_amt," + dt.Rows[i][21] + " as rref," + dt.Rows[i][22] + " as rcontr) as s"
            //                        + " ON t.brno=s.brno and T.slno=S.slno and T.ref=S.ref and T.contr=S.contr"
            //                        + " WHEN MATCHED THEN"
            //                        + " UPDATE SET T.total = S.total,T.discount = S.discount,T.net_total = S.net_total,t.tax_amt=s.tax_amt"
            //                        + " WHEN NOT MATCHED THEN"
            //                        + " INSERT VALUES(s.brno,s.slno, s.ref, s.contr,s.type,s.date,s.total,s.count,s.payed,s.total_cost,s.saleman,s.req_no,s.cust_no,s.discount,s.net_total,s.sysdate,s.gen_ref,s.tax_amt,s.dscper,s.card_type,s.card_amt,s.rref,s.rcontr);";
            //        //try
            //        //{
            //        //SqlConnection conn = new SqlConnection();


            //        using (SqlCommand comm = new SqlCommand(StrQuery, con_dest))
            //        {
            //            if (con_dest.State != ConnectionState.Open)
            //            {
            //                con_dest.Open();
            //            }
            //            comm.ExecuteNonQuery();

            //        }

            //        progressBar1.Value = i;
            //        // daml.Insert_Update_Delete(StrQuery, false);

            //        //    }
            //        //    catch (Exception ex)
            //        //    {
            //        //         MessageBox.Show(ex.ToString());
            //        //    }
            //        //    finally
            //        //    {

            //        //    }

            //    }
            //    // daml.SqlCon_Close();
            //    if (con_dest.State == ConnectionState.Open)
            //    {
            //        con_dest.Close();
            //    }

            //   // MessageBox.Show("ok sales_hd");
            //    update_pos_dtl();

            //    button2.Enabled = true;
            //    button2.Text = "ارسال الفواتير";
            //    //wf.Close();
            //}
            //catch (Exception ex)
            //{ MessageBox.Show(ex.Message); }

          
        }
        private void update_pos_dtl()
        {

            try
            {
                progressBar1.Value = 0;
                progressBar1.Minimum = 0;
                // daml.SqlCon_Open();
                SqlConnection con_dest = new SqlConnection("Data Source=" + textBox1 + ";Initial Catalog=" + textBox2 + ";User id=" + textBox3 + ";Password=" + textBox4 + ";Connection Timeout=" + textBox5 + "");

                // SqlDataAdapter da = new SqlDataAdapter("select * from items_bc", daml.);
                DataTable dt = new DataTable();

                dt = daml.SELECT_QUIRY_only_retrn_dt("select * from pos_dtl a join pos_hdr b on a.brno=b.brno and a.slno=b.slno and a.ref=b.ref and a.contr=b.contr where b.date between '" + datval.convert_to_yyyy_MMddwith_dash(txt_fmdate.Text) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000'");
                // da.Fill(dt);
                // MessageBox.Show(Convert.ToDateTime(dt.Rows[0][4]).ToString("yyyy-MM-dd HH:mm:ss.fff"));
                // con_dest.Open();
                count_dtl = dt.Rows.Count;
                progressBar1.Maximum = dt.Rows.Count - 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string StrQuery = " MERGE pos_dtl as t"
                                    + " USING (select '" + dt.Rows[i][0].ToString() + "' as brno, '" + dt.Rows[i][1].ToString() + "' as slno, " + dt.Rows[i][2].ToString() + " as ref," + dt.Rows[i][3].ToString() + " as contr," + dt.Rows[i][4].ToString() + " as type,'" + dt.Rows[i][5].ToString() + "' as barcode,'" + dt.Rows[i][6].ToString() + "' as name,'" + dt.Rows[i][7].ToString() + "' as unit," + dt.Rows[i][8].ToString() + " as price," + dt.Rows[i][9].ToString() + " as qty," + dt.Rows[i][10].ToString() + " as cost," + dt.Rows[i][11].ToString() + " as is_req, '" + dt.Rows[i][12].ToString() + "' as itemno, " + dt.Rows[i][13].ToString() + " as srno," + dt.Rows[i][14].ToString() + " as pkqty," + dt.Rows[i][15].ToString() + " as discpc," + dt.Rows[i][16].ToString() + " as tax_id," + dt.Rows[i][17].ToString() + " as tax_amt," + dt.Rows[i][18].ToString() + " as rqty," + dt.Rows[i][19].ToString() + " as offr_amt) as s"
                                    + " ON t.brno=s.brno and T.slno=S.slno and T.ref=S.ref and T.contr=S.contr and t.srno=s.srno"
                                    + " WHEN MATCHED THEN"
                                    + " UPDATE SET T.price = S.price,T.qty=S.qty,t.itemno=s.itemno,t.srno=s.srno"
                                    + " WHEN NOT MATCHED THEN"
                                    + " INSERT VALUES(s.brno,s.slno, s.ref, s.contr,s.type,s.barcode,s.name,s.unit,s.price,s.qty,s.cost,s.is_req,s.itemno,s.srno,s.pkqty,s.discpc,s.tax_id,s.tax_amt,s.rqty,s.offr_amt);";
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
              //  MessageBox.Show("Sales invoces sent : " + count_hdr.ToString() + "\n\r Items sent : " + count_dtl.ToString(), "عدد الفواير والاصناف المرسلة", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              //  progressBar1.Visible = false;
                send_del_h();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

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

        private void txt_contr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_fmdate.Focus();
                // txt_hdate.SelectionStart = 0;
                txt_fmdate.SelectionLength = 0;
            }
        }

        private void cmb_ctrno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_fmdate.Focus();
                
            }
        }
    }
}
