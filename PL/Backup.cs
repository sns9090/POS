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
    public partial class Backup : BaseForm
    {
        SqlConnection con = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        string path;
        string sql = "";
        private SqlCommand command;
        public Backup()
        {
            InitializeComponent();
        }

        private void Backup_Load(object sender, EventArgs e)
        {
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
            path = BL.CLS_Session.bkpdir;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(BL.CLS_Session.bkpdir))
                Directory.CreateDirectory(BL.CLS_Session.bkpdir);

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
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    //con.Open();
                    string baktime = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);

                    //sql = "BACKUP DATABASE " + cmbDb.Text + " TO DISK = '" + txtLocation.Text + "\\" + cmbDb.Text + "." + DateTime.Now.Ticks.ToString() + ".bak'";
                    sql = "BACKUP DATABASE " + database + " TO DISK = '" + path + "\\" + "Manual_" + BL.CLS_Session.sqldb + "_" + baktime + ".bak'";
                    command = new SqlCommand(sql, con);
                    command.CommandTimeout = 0;
                    command.ExecuteNonQuery();
                    con.Close();
                    //  con.Dispose();

                    MessageBox.Show(this, " successful backup complated  تم النسخ بنجاح " + "\n" + path + "\\" + "Manual_" + BL.CLS_Session.sqldb + "_" + baktime + ".bak", " Note تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                database = builder.InitialCatalog;

            

                // string database2 = builder.AttachDBFilename;

                try
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                   // con.Open();
                    string baktime = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);

                    //sql = "BACKUP DATABASE " + cmbDb.Text + " TO DISK = '" + txtLocation.Text + "\\" + cmbDb.Text + "." + DateTime.Now.Ticks.ToString() + ".bak'";
                   // sql = "BACKUP DATABASE " + database + " TO DISK = '" + path + "\\" + "Manual_" + database + "_" + baktime + ".bak'";
                    sql = "BACKUP DATABASE " + BL.CLS_Session.sqldb + " TO DISK = '" + path + "\\" + "Manual_" + BL.CLS_Session.sqldb + "_" + baktime + ".bak'";
                    command = new SqlCommand(sql, con);
                    command.CommandTimeout = 0;
                    command.ExecuteNonQuery();
                    con.Close();
                    //  con.Dispose();

                    MessageBox.Show(this, " successful backup complated  تم النسخ بنجاح " + "\n" + path + "\\" + "Manual_" + BL.CLS_Session.sqldb + "_" + baktime + ".bak", " Note تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
