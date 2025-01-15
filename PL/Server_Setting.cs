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
using System.Globalization;

namespace POS
{
    public partial class Server_Setting : BaseForm
    {
        SqlConnection con = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        BL.DAML daml = new BL.DAML();
        BL.EncDec endc = new BL.EncDec();

        ProgressFrm pfr = new ProgressFrm();

       // SqlConnection con_src=new SqlConnection(
        public void SqlCon_Open()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }

        //Method to close the connection
        public void SqlCon_Close()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        public Server_Setting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update server set server='" + textBox1.Text + "' , db='" + textBox2.Text + "' ,login='" + textBox3.Text + "' ,pass='" + endc.Encrypt(textBox4.Text, true) + "',tim_out='" + textBox5.Text + "',updt_itm=" + (chk_updtitm.Checked ? 1 : 0) + ",send_inv=" + (chk_sendinv.Checked ? 1 : 0) + ",updt_cust=" + (chk_ucst.Checked ? 1 : 0) + ",updt_stk=" + (chk_stk.Checked ? 1 : 0) + " ", con);
                
                if(con.State==ConnectionState.Closed)
                    con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Saved");
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                button1.Enabled = false;
                chk_updtitm.Enabled = false;
                chk_sendinv.Enabled = false;
                chk_stk.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            load_linked_servers();
        }

        private void Server_Setting_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from server", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            textBox1.Text = dt.Rows[0][0].ToString();
            textBox2.Text = dt.Rows[0][1].ToString();
            textBox3.Text = dt.Rows[0][2].ToString();
            textBox4.Text =endc.Decrypt(dt.Rows[0][3].ToString(),true);
            textBox5.Text = dt.Rows[0][4].ToString();
            chk_updtitm.Checked = Convert.ToBoolean(dt.Rows[0][5]) ? true : false;
            chk_sendinv.Checked = Convert.ToBoolean(dt.Rows[0][6]) ? true : false;
            chk_ucst.Checked = Convert.ToBoolean(dt.Rows[0][7]) ? true : false;
            chk_stk.Checked = Convert.ToBoolean(dt.Rows[0][8]) ? true : false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            button1.Enabled = false;
            chk_updtitm.Enabled = false;
            chk_sendinv.Enabled = false;
            chk_ucst.Enabled = false;
        }

        public void button2_Click(object sender, EventArgs e)
        {
            /*
            daml.SqlCon_Open();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@server", SqlDbType.NVarChar,50);
            param[0].Value = textBox1.Text;

            param[1] = new SqlParameter("@db", SqlDbType.NVarChar,50);
            param[1].Value = textBox2.Text;

            param[2] = new SqlParameter("@NO_items_updated", SqlDbType.Int, 30);
            param[2].Value = 999999;

            //param[3] = new SqlParameter("@Qte", SqlDbType.Int);
            //param[3].Value = Qte;

            //param[4] = new SqlParameter("@PRICE", SqlDbType.VarChar, 50);
            //param[4].Value = Price;

            //param[5] = new SqlParameter("@Img", SqlDbType.Image);
            //param[5].Value = img;

            daml.ExecuteCommand("update_items", param);
            daml.SqlCon_Close();
            MessageBox.Show("ok items");
             */
           



        //    String myConnString = @"User ID=sa;password=infocic;Initial Catalog=dbase;Data Source=RYD1-PC\INFOSOFT12";
           // SqlConnection myConnection = new SqlConnection(myConnString);
            SqlCommand myCommand = new SqlCommand();
            //1 SqlDataReader myReader;

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Connection = con;
            myCommand.CommandText = "update_items";
            //2 myCommand.Parameters.Add("@NO_items_updated", OleDbType.Integer);

            myCommand.Parameters.AddWithValue("@server", textBox1.Text);
            myCommand.Parameters.AddWithValue("@db", textBox2.Text);
           // myCommand.Parameters.Add("@NO_items_updated", 0);

            myCommand.Parameters.Add("@NO_items_updated", 0);
            myCommand.Parameters.Add("@errstatus", 0);

            myCommand.Parameters["@NO_items_updated"].Direction = ParameterDirection.Output;
            myCommand.Parameters["@errstatus"].Direction = ParameterDirection.Output;
            try
            {
                con.Open();

                //3  myReader = myCommand.ExecuteReader();
                myCommand.ExecuteNonQuery();
                //Uncomment this line to return the proper output value.
                //myReader.Close();
                MessageBox.Show("Items Updated : " + myCommand.Parameters["@NO_items_updated"].Value);
                // MessageBox.Show("Return Value : " + myCommand.Parameters["@errstatus"].Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            /*
              String myConnString =@"User ID=sa;password=infocic;Initial Catalog=dbase;Data Source=RYD1-PC\INFOSOFT12";
            SqlConnection myConnection = new SqlConnection(myConnString);
            SqlCommand myCommand = new SqlCommand();
           //1 SqlDataReader myReader;

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Connection = myConnection;
            myCommand.CommandText = "items_count_test";
            //2 myCommand.Parameters.Add("@NO_items_updated", OleDbType.Integer);
           
            myCommand.Parameters.Add("@NO_items_updated", 0);
            myCommand.Parameters.Add("@errstatus", 0);

            myCommand.Parameters["@NO_items_updated"].Direction = ParameterDirection.Output;
            myCommand.Parameters["@errstatus"].Direction = ParameterDirection.Output;
            try
            {
                myConnection.Open();

              //3  myReader = myCommand.ExecuteReader();
                myCommand.ExecuteNonQuery();
                //Uncomment this line to return the proper output value.
                //myReader.Close();
                MessageBox.Show("Return Value : " + myCommand.Parameters["@NO_items_updated"].Value);
               // MessageBox.Show("Return Value : " + myCommand.Parameters["@errstatus"].Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                myConnection.Close();
            }
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            pfr.progressBar1.Minimum = 0;
            progressBar1.Minimum = 0;
            //BL.DAML dml = new BL.DAML();
            daml.SqlCon_Open();

           // SqlCon_Open();
            SqlConnection con_srce = new SqlConnection("Data Source=" + textBox1.Text + ";Initial Catalog=" + textBox2.Text + ";User id=" + textBox3.Text + ";Password=" + textBox4.Text + ";");

            SqlDataAdapter da = new SqlDataAdapter("select * from items", con_srce);
            DataTable dt = new DataTable();

            da.Fill(dt);
           // MessageBox.Show(dt.Rows[0][1].ToString());
            progressBar1.Maximum = dt.Rows.Count - 1;
            pfr.progressBar1.Maximum = dt.Rows.Count - 1;

            pfr.Show();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string StrQuery = " MERGE items as t"
                                + " USING (select '" + dt.Rows[i][0].ToString() + "' as item_no, '" + dt.Rows[i][1].ToString() + "' as item_name, " + dt.Rows[i][2].ToString() + " as item_cost," + dt.Rows[i][3].ToString() + " as item_price,'" + dt.Rows[i][4].ToString() + "' as item_barcode," + dt.Rows[i][5].ToString() + " as item_unit," + dt.Rows[i][6].ToString() + " as item_obalance," + dt.Rows[i][7].ToString() + " as item_cbalance," + dt.Rows[i][8].ToString() + " as item_group,'" + dt.Rows[i][9].ToString() + "' as item_image," + dt.Rows[i][10].ToString() + " as item_req) as s"
                                + " ON T.item_no=S.item_no"
                                + " WHEN MATCHED THEN"
                                + " UPDATE SET T.item_price = S.item_price"
                                + " WHEN NOT MATCHED THEN"
                                + " INSERT VALUES(s.item_no, s.item_name, s.item_cost,s.item_price,s.item_barcode,s.item_unit,s.item_obalance,s.item_cbalance,s.item_group,s.item_image,s.item_req);";
                try
                {
                    //SqlConnection conn = new SqlConnection();

                    /*
                    using (SqlCommand comm = new SqlCommand(StrQuery, con))
                    {
                       // con.Open();
                        comm.ExecuteNonQuery();
                       // con.Close();
                       
                    }
                     */

                    daml.Insert_Update_Delete_retrn_int(StrQuery, false);

                    progressBar1.Value = i;
                    pfr.progressBar1.Value = i;


                }
                catch (Exception ex)
                {
                   // MessageBox.Show(ex.ToString());
                }
                finally
                {
                   
                }
            }
           // SqlCon_Close();
            daml.SqlCon_Close();
            MessageBox.Show("ok items");
            update_bc();
            pfr.Close();

        }

         private void update_bc()
         {
             progressBar1.Minimum = 0;
            daml.SqlCon_Open();
            SqlConnection con_srce = new SqlConnection("Data Source=" + textBox1.Text + ";Initial Catalog=" + textBox2.Text + ";User id=" + textBox3.Text + ";Password=" + textBox4.Text + ";");

            SqlDataAdapter da = new SqlDataAdapter("select * from items_bc", con_srce);
            DataTable dt = new DataTable();

            da.Fill(dt);
           // MessageBox.Show(dt.Rows[0][1].ToString());
            progressBar1.Maximum = dt.Rows.Count - 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string StrQuery = " MERGE items_bc as t"
                                + " USING (select '" + dt.Rows[i][0].ToString() + "' as item_no, '" + dt.Rows[i][1].ToString() + "' as barcode, " + dt.Rows[i][2].ToString() + " as pack," + dt.Rows[i][3].ToString() + " as pk_qty," + dt.Rows[i][4].ToString() + " as price,'" + dt.Rows[i][5].ToString() + "' as note," + dt.Rows[i][6].ToString() + " as pkorder) as s"
                                + " ON T.barcode=S.barcode"
                                + " WHEN MATCHED THEN"
                                + " UPDATE SET T.price = S.price"
                                + " WHEN NOT MATCHED THEN"
                                + " INSERT VALUES(s.item_no, s.barcode, s.pack, s.pk_qty, s.price, s.note,s.pkorder);";
                try
                {
                    //SqlConnection conn = new SqlConnection();

                    /*
                    using (SqlCommand comm = new SqlCommand(StrQuery, con))
                    {
                       // con.Open();
                        comm.ExecuteNonQuery();
                      //  con.Close();
                    }
                    */
                    daml.Insert_Update_Delete_retrn_int(StrQuery, false);

                    progressBar1.Value = i;
                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.ToString());
                }
                finally
                {

                }

            }
            daml.SqlCon_Close();
            MessageBox.Show("ok items_bc");
         }

         public void button4_Click(object sender, EventArgs e)
         {
            
             /*
             SqlConnection con_dest = new SqlConnection("Data Source=" + textBox1.Text + ";Initial Catalog=" + textBox2.Text + ";User id=" + textBox3.Text + ";Password=" + textBox4.Text + ";");

            // DataTable hdr = new DataTable();
            // DataTable dtl = new DataTable();

            // hdr = daml.SELECT_QUIRY("select * from pos_hdr");
            // dtl = daml.SELECT_QUIRY("select * from pos_dtl");

             using (SqlCommand cmd = new SqlCommand("update_pos_inv"))
             {

                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Connection = con_dest;
                 cmd.Parameters.AddWithValue("@pos_hdr_tb", daml.SELECT_QUIRY("select * from pos_hdr"));
                 cmd.Parameters.AddWithValue("@pos_dtl_tb", daml.SELECT_QUIRY("select * from pos_dtl"));
                 con_dest.Open();
                 cmd.ExecuteNonQuery();
                 con_dest.Close();
             }
              */
             /*
                 using (var command = new SqlCommand("update_pos_inv") { CommandType = CommandType.StoredProcedure })
                 {
                    // var dt = new DataTable(); //create your own data table
                     command.Connection = con_dest;
                     command.Parameters.Add(new SqlParameter("@pos_hdr_tb", hdr));
                     command.Parameters.Add(new SqlParameter("@pos_dtl_tb", dtl));
                    // SqlHelper.Exec(command);
                     con_dest.Open();
                     command.ExecuteNonQuery();
                     con_dest.Close();
                     MessageBox.Show("ok");
                 }
              */
             
            // int ii;
             try
             {
                 progressBar1.Minimum = 0;
                 // progressBar1.Maximum = 200;

                 //  for (ii = 0; ii <= 200; ii++)
                 //  {
                 //     progressBar1.Value = ii;
                 //  }
                 // daml.SqlCon_Open();
                 SqlConnection con_dest = new SqlConnection("Data Source=" + textBox1.Text + ";Initial Catalog=" + textBox2.Text + ";User id=" + textBox3.Text + ";Password=" + textBox4.Text + ";");

                 // SqlDataAdapter da = new SqlDataAdapter("select * from items_bc", daml.);
                 DataTable dt = new DataTable();

                 dt = daml.SELECT_QUIRY_only_retrn_dt("select * from pos_hdr");
                 // da.Fill(dt);
                 //  MessageBox.Show(Convert.ToDateTime(dt.Rows[0][4]).ToString("yyyy-MM-dd HH:mm:ss.fff"));






                 progressBar1.Maximum = dt.Rows.Count - 1;
                 for (int i = 0; i < dt.Rows.Count; i++)
                 {
                   //  MessageBox.Show(Convert.ToDateTime(dt.Rows[i][5]).ToString());
                     string StrQuery = " MERGE pos_hdr as t"
                                   //  + " USING (select '" + dt.Rows[i][0] + "' as brno, '" + dt.Rows[i][1] + "' as slno, " + dt.Rows[i][2] + " as ref," + dt.Rows[i][3] + " as contr, " + dt.Rows[i][4] + " as [type], '" + Convert.ToDateTime(dt.Rows[i][5]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' as date," + dt.Rows[i][6] + " as total," + dt.Rows[i][7] + " as count," + dt.Rows[i][8] + " as payed," + dt.Rows[i][9] + " as total_cost,'" + dt.Rows[i][10] + "' as saleman," + dt.Rows[i][11] + " as req_no," + dt.Rows[i][12] + " as cust_no," + dt.Rows[i][13] + " as discount," + dt.Rows[i][14] + " as net_total,'" + Convert.ToDateTime(dt.Rows[i][15]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' as sysdate," + dt.Rows[i][16] + " as gen_ref," + dt.Rows[i][17] + " as tax_amt) as s"
                                   //   + " USING (select '" + dt.Rows[i][0] + "' as brno, '" + dt.Rows[i][1] + "' as slno, " + dt.Rows[i][2] + " as ref," + dt.Rows[i][3] + " as contr, " + dt.Rows[i][4] + " as [type], convert(datetime,'" + Convert.ToDateTime(dt.Rows[i][5]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "') as date," + dt.Rows[i][6] + " as total," + dt.Rows[i][7] + " as count," + dt.Rows[i][8] + " as payed," + dt.Rows[i][9] + " as total_cost,'" + dt.Rows[i][10] + "' as saleman," + dt.Rows[i][11] + " as req_no," + dt.Rows[i][12] + " as cust_no," + dt.Rows[i][13] + " as discount," + dt.Rows[i][14] + " as net_total, convert(datetime,'" + Convert.ToDateTime(dt.Rows[i][15]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "') as sysdate," + dt.Rows[i][16] + " as gen_ref," + dt.Rows[i][17] + " as tax_amt) as s"
                                     + " USING (select '" + dt.Rows[i][0] + "' as brno, '" + dt.Rows[i][1] + "' as slno, " + dt.Rows[i][2] + " as ref," + dt.Rows[i][3] + " as contr, " + dt.Rows[i][4] + " as [type],'" + Convert.ToDateTime(dt.Rows[i][5]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "' as date," + dt.Rows[i][6] + " as total," + dt.Rows[i][7] + " as count," + dt.Rows[i][8] + " as payed," + dt.Rows[i][9] + " as total_cost,'" + dt.Rows[i][10] + "' as saleman," + dt.Rows[i][11] + " as req_no," + dt.Rows[i][12] + " as cust_no," + dt.Rows[i][13] + " as discount," + dt.Rows[i][14] + " as net_total,'" + Convert.ToDateTime(dt.Rows[i][15]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "' as sysdate," + dt.Rows[i][16] + " as gen_ref," + dt.Rows[i][17] + " as tax_amt," + dt.Rows[i][18] + " as dscper," + dt.Rows[i][19] + " as card_type," + dt.Rows[i][20] + " as card_amt," + dt.Rows[i][21] + " as rref," + dt.Rows[i][22] + " as rcontr) as s"
                                     + " ON t.brno=s.brno and T.slno=S.slno and T.ref=S.ref and T.contr=S.contr"
                                     + " WHEN MATCHED THEN"
                                     + " UPDATE SET T.total = S.total,T.discount = S.discount,T.net_total = S.net_total,t.tax_amt=s.tax_amt"
                                     + " WHEN NOT MATCHED THEN"
                                     + " INSERT VALUES(s.brno,s.slno, s.ref, s.contr,s.type,s.date,s.total,s.count,s.payed,s.total_cost,s.saleman,s.req_no,s.cust_no,s.discount,s.net_total,s.sysdate,s.gen_ref,s.tax_amt,s.dscper,s.card_type,s.card_amt,s.rref,s.rcontr);";
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

                 MessageBox.Show("ok sales_hd");
                 update_pos_dtl();
             }
             catch(Exception ex)
             { MessageBox.Show(ex.Message); }
              
             
         }

         private void update_pos_dtl()
         {
             
             try{
             progressBar1.Value = 0;
             progressBar1.Minimum = 0;
             // daml.SqlCon_Open();
             SqlConnection con_dest = new SqlConnection("Data Source=" + textBox1.Text + ";Initial Catalog=" + textBox2.Text + ";User id=" + textBox3.Text + ";Password=" + textBox4.Text + ";");

             // SqlDataAdapter da = new SqlDataAdapter("select * from items_bc", daml.);
             DataTable dt = new DataTable();

             dt = daml.SELECT_QUIRY_only_retrn_dt("select * from pos_dtl");
             // da.Fill(dt);
            // MessageBox.Show(Convert.ToDateTime(dt.Rows[0][4]).ToString("yyyy-MM-dd HH:mm:ss.fff"));
            // con_dest.Open();
             progressBar1.Maximum = dt.Rows.Count - 1;
             for (int i = 0; i < dt.Rows.Count; i++)
             {
                 string StrQuery = " MERGE pos_dtl as t"
                                 + " USING (select '" + dt.Rows[i][0].ToString() + "' as brno, '" + dt.Rows[i][1].ToString() + "' as slno, " + dt.Rows[i][2].ToString() + " as ref," + dt.Rows[i][3].ToString() + " as contr," + dt.Rows[i][4].ToString() + " as type,'" + dt.Rows[i][5].ToString() + "' as barcode,'" + dt.Rows[i][6].ToString() + "' as name,'" + dt.Rows[i][7].ToString() + "' as unit," + dt.Rows[i][8].ToString() + " as price," + dt.Rows[i][9].ToString() + " as qty," + dt.Rows[i][10].ToString() + " as cost," + dt.Rows[i][11].ToString() + " as is_req, '" + dt.Rows[i][12].ToString() + "' as itemno, " + dt.Rows[i][13].ToString() + " as srno," + dt.Rows[i][14].ToString() + " as pkqty," + dt.Rows[i][15].ToString() + " as discpc," + dt.Rows[i][16].ToString() + " as tax_id," + dt.Rows[i][17].ToString() + " as tax_amt," + dt.Rows[i][18].ToString() + " as rqty," + dt.Rows[i][19].ToString() + " as offr_amt) as s"
                                 + " ON t.brno=s.brno and T.slno=S.slno and T.ref=S.ref and T.contr=S.contr"
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
             MessageBox.Show("ok sales_dt");
             }
             catch (Exception ex)
             { MessageBox.Show(ex.Message); }
              
         }

         private void button5_Click(object sender, EventArgs e)
         {
             SqlConnection con_src = new SqlConnection("Data Source=" + textBox1.Text + ";Initial Catalog=" + textBox2.Text + ";User id=" + textBox3.Text + ";Password=" + textBox4.Text + ";Connection Timeout=" + textBox5.Text + "");

             SqlCommand cmd = new SqlCommand("delete FROM items", con);
            
             con_src.Open();
             con.Open();
             cmd.ExecuteNonQuery();

             cmd = new SqlCommand("SELECT * FROM items", con_src);

             SqlDataReader reader = cmd.ExecuteReader();
             // Create SqlBulkCopy
             SqlBulkCopy bulkData = new SqlBulkCopy(con);

             bulkData.DestinationTableName = "items";
             // Write data
             bulkData.WriteToServer(reader);
             // Close objects
             bulkData.Close();
             con.Close();
             con_src.Close();
             MessageBox.Show("items ok");

             SqlCommand cmd2 = new SqlCommand("delete FROM items_bc", con);

             con_src.Open();
             con.Open();
             cmd2.ExecuteNonQuery();

             cmd2 = new SqlCommand("SELECT * FROM items_bc", con_src);

             SqlDataReader reader2 = cmd2.ExecuteReader();
             // Create SqlBulkCopy
             SqlBulkCopy bulkData2 = new SqlBulkCopy(con);

             bulkData2.DestinationTableName = "items_bc";
             // Write data
             bulkData2.WriteToServer(reader2);
             // Close objects
             bulkData2.Close();
             con.Close();
             con_src.Close();
             MessageBox.Show("items_bc");
         }

         private void button6_Click(object sender, EventArgs e)
         {
             textBox1.Enabled = true;
             textBox2.Enabled = true;
             textBox3.Enabled = true;
             textBox4.Enabled = true;
             textBox5.Enabled = true;
             button1.Enabled = true;
             chk_updtitm.Enabled = true;
             chk_sendinv.Enabled = true;
             chk_ucst.Enabled = true;
             chk_stk.Enabled = true;
         }

         private void button7_Click(object sender, EventArgs e)
         {
             SqlConnection con_src = new SqlConnection("Data Source=" + textBox1.Text + ";Initial Catalog=" + textBox2.Text + ";User id=" + textBox3.Text + ";Password=" + textBox4.Text + ";Connection Timeout=" + textBox5.Text + "");

           

            // con_src.Open();
            
         

             SqlDataAdapter  da = new SqlDataAdapter("SELECT * FROM items", con_src);
             SqlDataAdapter dab = new SqlDataAdapter("SELECT * FROM items_bc", con_src);

             DataTable dt=new DataTable();
             DataTable dtb = new DataTable();

             da.Fill(dt);
             dab.Fill(dtb);

             using (SqlCommand cmd = new SqlCommand("update_items_table"))
             {

                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Connection = con;
                 cmd.Parameters.AddWithValue("@items_tb", dt);
                 cmd.Parameters.AddWithValue("@items_bc_tb", dtb);
                 con.Open();
                 cmd.ExecuteNonQuery();
                 con.Close();
             }
             
             




            
           //  con_src.Close();
             MessageBox.Show("ok");



           
         }

         private void load_linked_servers()
         {
            // IF EXISTS(SELECT * FROM sys.servers WHERE name = N'AccessDataSource')
            // EXEC master.sys.sp_dropserver 'AccessDataSource','droplogins'  
            // GO
             
             /*
             DataTable dt = daml.SELECT_QUIRY("SELECT name FROM sys.servers WHERE name = N'main'");

             //if (dataGridView1.Rows.Count>1)
             if (!string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
             {
               //  MessageBox.Show(dataGridView1.Rows[0].Cells[0].Value.ToString());
                 daml.SqlCon_Open();

                 daml.Exec_Query(@"EXEC master.sys.sp_dropserver 'main','droplogins'");

               //  daml.Exec_Query(@"EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'main',@useself=N'False',@locallogin=NULL,@rmtuser=N'" + textBox3.Text + "',@rmtpassword='" + textBox4.Text + "'");

                 daml.SqlCon_Close();
             }
             //else
             //{*/
                 daml.SqlCon_Open();

                 daml.Exec_Query_only(@"IF EXISTS(SELECT * FROM sys.servers WHERE name = N'main') EXEC master.sys.sp_dropserver 'main','droplogins'");

                 daml.Exec_Query_only(@"EXEC master.dbo.sp_addlinkedserver @server = N'main', @srvproduct=N'', @provider=N'SQLNCLI', @datasrc=N'" + textBox1.Text + "'");

                 daml.Exec_Query_only(@"EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'main',@useself=N'False',@locallogin=NULL,@rmtuser=N'" + textBox3.Text + "',@rmtpassword='" + textBox4.Text + "'");

                 daml.SqlCon_Close();

                 dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("EXEC master.dbo.sp_linkedservers");
             //}

         }
    }
}
