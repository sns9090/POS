using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace POS
{
    public partial class Open_New_Yr : BaseForm
    {
        SqlConnection con = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        string path;
        string sql = "";
        private SqlCommand command;
        BL.DAML daml = new BL.DAML();
        public Open_New_Yr()
        {
            InitializeComponent();
        }

        private void Backup_Load(object sender, EventArgs e)
        {
            lbl_newyr.Text = (Convert.ToInt32(BL.CLS_Session.sqldb.Substring(5, 4)) + 1).ToString();
            ////ok if inside app dir
            ////////path = Directory.GetCurrentDirectory() + @"\Backup";
            ////////textBox1.Text = path;
            

            ////////if (!Directory.Exists(path))
            ////////{
            ////////    Directory.CreateDirectory(path);
            ////////}

            /*
            SqlDataAdapter da = new SqlDataAdapter("select bkp_dir from company", con);

            DataTable dt = new DataTable();
            da.Fill(dt);
            path = dt.Rows[0][0].ToString();
             
            textBox1.Text = path;
            */
            textBox1.Text = BL.CLS_Session.bkpdir;
           // textBox1.Text = new DirectoryInfo(Environment.CurrentDirectory).Parent.FullName;
            path = textBox1.Text;// BL.CLS_Session.bkpdir;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(BL.CLS_Session.bkpdir))
                Directory.CreateDirectory(BL.CLS_Session.bkpdir);

            int pass = 0;
                if (Convert.ToInt32(DateTime.Now.Day) % 2 == 0)
                    pass = ((Convert.ToInt32(DateTime.Now.Year) + Convert.ToInt32(DateTime.Now.Month)) * 411) - (Convert.ToInt32(DateTime.Now.Day) * 114);
                else
                    pass = ((Convert.ToInt32(DateTime.Now.Year) + Convert.ToInt32(DateTime.Now.Month)) * 311) - (Convert.ToInt32(DateTime.Now.Day) * 113);

               // if (textBox1.Text == "sa735356688")
                if (textBox2.Text.Trim().Equals(pass.ToString()))
                {
                    WaitForm wf = new WaitForm("جاري فتح السنة .. يرجى الانتظار ...");
                    wf.MdiParent = MdiParent;
                    wf.Show();
                    Application.DoEvents();

                    string database;
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

                    string server = builder.DataSource;
                    // database = builder.InitialCatalog;

                    // if (builder.InitialCatalog.Trim() == "")
                    if (File.Exists(Environment.CurrentDirectory + @"\local.txt"))
                    {
                        //////  database = (builder.AttachDBFilename.Replace(@"|DataDirectory|\DB\","")).Replace(".mdf","");
                        //////  database =Directory.GetCurrentDirectory()+ @"\DB\DBASE.mdf";
                        //////string sourcePath = Directory.GetCurrentDirectory() + @"\DB\DBASE.mdf";
                        //////string targetPath = Directory.GetCurrentDirectory() + @"\Backup\DBASE.mdf";
                        //////File.Copy(sourcePath, targetPath);
                        ////try
                        ////{
                        ////    database = "[" + Directory.GetCurrentDirectory() + @"\DB\DBASE.mdf]";
                        ////    // MessageBox.Show(database);
                        ////    if (con.State == ConnectionState.Closed)
                        ////        con.Open();
                        ////    //con.Open();
                        ////    string baktime = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);

                        ////    //sql = "BACKUP DATABASE " + cmbDb.Text + " TO DISK = '" + txtLocation.Text + "\\" + cmbDb.Text + "." + DateTime.Now.Ticks.ToString() + ".bak'";
                        ////    sql = "BACKUP DATABASE " + database + " TO DISK = '" + path + "\\" + "Manual_" + "DBASE" + "_" + baktime + ".bak'";
                        ////    command = new SqlCommand(sql, con);
                        ////    command.CommandTimeout = 60;
                        ////    command.ExecuteNonQuery();
                        ////    con.Close();
                        ////    //  con.Dispose();

                        ////    MessageBox.Show(this, " successful backup complated  تم النسخ بنجاح " + "\n" + path + "\\" + "Manual_" + "DBASE" + "_" + baktime + ".bak", " Note تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        ////}
                        ////catch (Exception ex)
                        ////{
                        ////    MessageBox.Show(ex.Message);
                        ////}
                        try
                        {
                            database = "[" + Directory.GetCurrentDirectory() + @"\DB\" + BL.CLS_Session.sqldb + ".mdf]";
                            // MessageBox.Show(database);
                           
                            //con.Open();
                            string baktime = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);

                            //sql = "BACKUP DATABASE " + cmbDb.Text + " TO DISK = '" + txtLocation.Text + "\\" + cmbDb.Text + "." + DateTime.Now.Ticks.ToString() + ".bak'";


                            // sql = "BACKUP DATABASE " + database + " TO DISK = '" + path + "\\" + "Manual_" + BL.CLS_Session.sqldb + "_" + baktime + ".bak'";
                            string newdb = "db" + BL.CLS_Session.sqldb.Substring(2, 2) + "y" + lbl_newyr.Text;

                            DataTable dtdb = daml.SELECT_QUIRY_only_retrn_dt("select * from sys.sysdatabases where NAME='" + newdb + "'");
                            if (File.Exists(Environment.CurrentDirectory + @"\DB\" + newdb + ".mdf") || dtdb.Rows.Count > 0)
                            {
                                wf.Close();
                                MessageBox.Show("db exists قاعدة البيانات موجودة", " Note تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                sql = "BACKUP DATABASE " + BL.CLS_Session.sqldb + " TO DISK = '" + path + "\\" + "OpenYear_" + BL.CLS_Session.sqldb + "_" + baktime + ".bak';"
                                    + @"RESTORE DATABASE " + newdb + " "
                                    + @" FROM DISK ='" + path + "\\" + "OpenYear_" + BL.CLS_Session.sqldb + "_" + baktime + ".bak'"
                                    + @" WITH RECOVERY ,
                                         MOVE 'db01y2020' TO '" + Environment.CurrentDirectory + @"\DB\" + newdb + ".mdf', "
                                    + @" MOVE 'db01y2020_log' TO '" + Environment.CurrentDirectory + @"\DB\" + newdb + "_log.ldf'; "
                                    // + " exec " + newdb + ".dbo.formate_db; ";
                                   + "delete from " + newdb + ".dbo.sales_hdr;delete from " + newdb + ".dbo.d_sales_hdr;delete from " + newdb + ".dbo.d_sales_dtl;"
                                   + "delete from " + newdb + ".dbo.pu_hdr;delete from " + newdb + ".dbo.d_pu_hdr;delete from " + newdb + ".dbo.d_pu_dtl;"
                                   + "delete from " + newdb + ".dbo.pos_hdr;delete from " + newdb + ".dbo.d_pos_hdr;delete from " + newdb + ".dbo.d_pos_dtl;"
                                   + "delete from " + newdb + ".dbo.salofr_hdr;delete from " + newdb + ".dbo.sto_hdr;delete from " + newdb + ".dbo.acc_hdr;"
                                   + "delete from " + newdb + ".dbo.d_acc_hdr;delete from " + newdb + ".dbo.d_acc_dtl;"
                                   + "delete from " + newdb + ".dbo.pos_esend;"
                                   + "delete from " + newdb + ".dbo.whbins; update " + newdb + ".dbo.items set item_cbalance=0;"
                                   + "update " + newdb + ".dbo.accounts set a_opnbal=0,a_curbal=0;"
                                   + "update " + newdb + ".dbo.suppliers set su_opnbal=0,su_opnfcy=0,su_curbal=0;"
                                   + "update " + newdb + ".dbo.customers set cu_opnbal=0,cf_opnfcy=0,cu_curbal=0;"
                                   + "EXEC " + newdb + ".[dbo].[move_tr_serial] @old_db = '" + BL.CLS_Session.sqldb + "',@move_serial = "+(checkBox1.Checked? 1 : 0 )+";"

                                   + "INSERT INTO dbmstr.dbo.years (comp_id,yrcode,calstart,calend, per01, per02, per03, per04, per05, per06, per07, per08, per09, per10, per11, per12) VALUES('" + BL.CLS_Session.sqldb.Substring(2, 2) + "','" + lbl_newyr.Text + "','" + lbl_newyr.Text + "0101','" + lbl_newyr.Text + "1231','" + lbl_newyr.Text + "0101','" + lbl_newyr.Text + "0201','" + lbl_newyr.Text + "0301','" + lbl_newyr.Text + "0401','" + lbl_newyr.Text + "0501','" + lbl_newyr.Text + "0601','" + lbl_newyr.Text + "0701','" + lbl_newyr.Text + "0801','" + lbl_newyr.Text + "0901','" + lbl_newyr.Text + "1001','" + lbl_newyr.Text + "1101','" + lbl_newyr.Text + "1201');";

                                command = new SqlCommand(sql, con);
                                command.CommandTimeout = 0;
                                if (con.State == ConnectionState.Closed)
                                    con.Open();
                                command.ExecuteNonQuery();
                                con.Close();
                                //  con.Dispose();

                                wf.Close();
                                MessageBox.Show(this, " successful Year Opened  تم فتح السنة بنجاح " + "\n" + newdb + "", " Note تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                        catch (Exception ex)
                        {
                            wf.Close();
                            MessageBox.Show(ex.Message);
                        }

                    }
                    else
                    {
                        // database = builder.InitialCatalog;



                        // string database2 = builder.AttachDBFilename;

                        try
                        {
                            
                            // con.Open();
                            string baktime = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);

                            //sql = "BACKUP DATABASE " + cmbDb.Text + " TO DISK = '" + txtLocation.Text + "\\" + cmbDb.Text + "." + DateTime.Now.Ticks.ToString() + ".bak'";
                            // sql = "BACKUP DATABASE " + database + " TO DISK = '" + path + "\\" + "Manual_" + database + "_" + baktime + ".bak'";

                            // sql = "BACKUP DATABASE " + BL.CLS_Session.sqldb + " TO DISK = '" + path + "\\" + "Manual_" + BL.CLS_Session.sqldb + "_" + baktime + ".bak'";

                            string newdb = "db" + BL.CLS_Session.sqldb.Substring(2, 2) + "y" + lbl_newyr.Text;

                            DataTable dtdb = daml.SELECT_QUIRY_only_retrn_dt("select * from sys.sysdatabases where NAME='" + newdb + "'");
                            if (File.Exists(Environment.CurrentDirectory + @"\DB\" + newdb + ".mdf") || dtdb.Rows.Count > 0)
                            {
                                wf.Close();
                                MessageBox.Show("db exists قاعدة البيانات موجودة", " Note تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                sql = "BACKUP DATABASE " + BL.CLS_Session.sqldb + " TO DISK = '" + path + "\\" + "OpenYear_" + BL.CLS_Session.sqldb + "_" + baktime + ".bak';"
                                    + @"RESTORE DATABASE " + newdb + " "
                                    + @" FROM DISK ='" + path + "\\" + "OpenYear_" + BL.CLS_Session.sqldb + "_" + baktime + ".bak'"
                                    + @" WITH RECOVERY ,
                                         MOVE 'db01y2020' TO '" + Environment.CurrentDirectory + @"\DB\" + newdb + ".mdf', "
                                    + @" MOVE 'db01y2020_log' TO '" + Environment.CurrentDirectory + @"\DB\" + newdb + "_log.ldf'; "
                                    // + " exec " + newdb + ".dbo.formate_db; ";
                                   + "delete from " + newdb + ".dbo.sales_hdr;delete from " + newdb + ".dbo.d_sales_hdr;delete from " + newdb + ".dbo.d_sales_dtl;"
                                   + "delete from " + newdb + ".dbo.pu_hdr;delete from " + newdb + ".dbo.d_pu_hdr;delete from " + newdb + ".dbo.d_pu_dtl;"
                                   + "delete from " + newdb + ".dbo.pos_hdr;delete from " + newdb + ".dbo.d_pos_hdr;delete from " + newdb + ".dbo.d_pos_dtl;"
                                   + "delete from " + newdb + ".dbo.salofr_hdr;delete from " + newdb + ".dbo.sto_hdr;delete from " + newdb + ".dbo.acc_hdr;"
                                   + "delete from " + newdb + ".dbo.d_acc_hdr;delete from " + newdb + ".dbo.d_acc_dtl;"
                                   + "delete from " + newdb + ".dbo.pos_esend;"
                                   + "delete from " + newdb + ".dbo.whbins; update " + newdb + ".dbo.items set item_cbalance=0;"
                                   + "update " + newdb + ".dbo.accounts set a_opnbal=0,a_curbal=0;"
                                   + "update " + newdb + ".dbo.suppliers set su_opnbal=0,su_opnfcy=0,su_curbal=0;"
                                   + "update " + newdb + ".dbo.customers set cu_opnbal=0,cf_opnfcy=0,cu_curbal=0;"
                                   + "EXEC " + newdb + ".[dbo].[move_tr_serial] @old_db = '" + BL.CLS_Session.sqldb + "',@move_serial = " + (checkBox1.Checked ? 1 : 0) + ";"


                                   + "INSERT INTO dbmstr.dbo.years (comp_id,yrcode,calstart,calend, per01, per02, per03, per04, per05, per06, per07, per08, per09, per10, per11, per12) VALUES('" + BL.CLS_Session.sqldb.Substring(2, 2) + "','" + lbl_newyr.Text + "','" + lbl_newyr.Text + "0101','" + lbl_newyr.Text + "1231','" + lbl_newyr.Text + "0101','" + lbl_newyr.Text + "0201','" + lbl_newyr.Text + "0301','" + lbl_newyr.Text + "0401','" + lbl_newyr.Text + "0501','" + lbl_newyr.Text + "0601','" + lbl_newyr.Text + "0701','" + lbl_newyr.Text + "0801','" + lbl_newyr.Text + "0901','" + lbl_newyr.Text + "1001','" + lbl_newyr.Text + "1101','" + lbl_newyr.Text + "1201');";

                                command = new SqlCommand(sql, con);
                                command.CommandTimeout = 0;
                                if (con.State == ConnectionState.Closed)
                                    con.Open();
                                command.ExecuteNonQuery();
                                con.Close();
                                //  con.Dispose();
                                wf.Close();
                                MessageBox.Show(this, " Year opened successfully  تم فتح السنة بنجاح " + "\n" + newdb + "", " Note تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }

                        }
                        catch (Exception ex)
                        {
                            wf.Close();
                            MessageBox.Show(ex.Message);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Password Error كلمة السر خطا", " Note تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
