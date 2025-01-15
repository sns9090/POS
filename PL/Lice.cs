using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.IO;
using System.Data.SqlClient;
using System.Globalization;


namespace POS
{
    public partial class Lice : BaseForm
    {
        int countt = 0;
        string id = "";
        string str = "";
        SqlConnection con3;
        BL.EncDec ende = new BL.EncDec();

        BL.Date_Validate datval = new BL.Date_Validate();
        public Lice()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ok
            //////var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            //////ManagementObjectCollection mbsList = mbs.Get();
            
            //////foreach (ManagementObject mo in mbsList)
            //////{
            //////    id = mo["ProcessorId"].ToString();
            //////    break;
            //////}

           // id = HardwareInfo.GetProcessorId();
            id = HardwareInfo.GetProcessorId() + "" + HardwareInfo.GetBIOSserNo() + "" + HardwareInfo.GetBoardProductId();
            label1.Text = ende.Encrypt(id, true);// id.ToString();
            textBox2.Text = ende.Encrypt(id, true);// id.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          //  string to dec
           
           // textBox1.Text = id + "AAAAAAAA";
            textBox1.Text = ende.Encrypt(id, true);
            str = textBox1.Text;

        }
       

        private void button3_Click(object sender, EventArgs e)
        {

            textBox1.Text = ende.Decrypt(str, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text.Trim().Length <= 10)
                {
                    int pass = 0;
                    var lines2 = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\ServerSetting.txt");
                    con3 = new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + lines2.Skip(1).First().ToString() + ";User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=" + BL.CLS_Session.sqlcontimout + "");
                    if (Convert.ToInt32(DateTime.Now.Day) % 2 == 0)
                        pass = ((Convert.ToInt32(DateTime.Now.Year) + Convert.ToInt32(DateTime.Now.Month)) * 411) - (Convert.ToInt32(DateTime.Now.Day) * 114);
                    else
                        pass = ((Convert.ToInt32(DateTime.Now.Year) + Convert.ToInt32(DateTime.Now.Month)) * 311) - (Convert.ToInt32(DateTime.Now.Day) * 113);

                    if (Convert.ToInt32(textBox3.Text.Trim()) == pass)
                    {
                      //  SqlCommand cmd = new SqlCommand("update companys set comp_guid='CSKraNCRfT4=" + (txt_Days.Text.Equals("") ? "'" : ende.Encrypt("///", true) + ende.Encrypt(txt_Days.Text, true) + ende.Encrypt("//////", true) + ende.Encrypt(datval.convert_to_yyyyDDdd(DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false))), true) + "'"), con3);
                        SqlCommand cmd = new SqlCommand("update companys set comp_guid='rlUnpygPz9k=',guid_day='" + ende.Encrypt(Convert.ToInt32(txt_Days.Text) > 0 ? txt_Days.Text : "7", true) + "',guid_dt='" + ende.Encrypt(datval.convert_to_yyyyDDdd(DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false))), true) + "'", con3);
                        
                       // MessageBox.Show("CSKraNCRfT4=" + (txt_Days.Text.Equals("") ? "" : ende.Encrypt("///", true) + ende.Encrypt(txt_Days.Text, true) + ende.Encrypt("//////", true) + ende.Encrypt(datval.convert_to_yyyyDDdd(DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false))), true) + ""));
                        con3.Open();
                        cmd.ExecuteNonQuery();
                        con3.Close();
                        MessageBox.Show("it is OK تم التسجيل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Application.Restart();
                    }
                    else
                    {
                        MessageBox.Show("Error خطا", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (textBox3.Text != "")
                    {
                        //   string path = Directory.GetCurrentDirectory() + @"\licen.txt";// @"C:\Windows\POSLICE.txt";
                        string path = Environment.SystemDirectory.ToUpper().Replace(@"\SYSTEM32", "") + @"\PosKey.txt";
                        if (!File.Exists(path))
                        {
                            // Create a file to write to.
                            using (StreamWriter sw = File.CreateText(path))
                            {
                                sw.WriteLine(textBox3.Text);
                                MessageBox.Show("it is OK تم التسجيل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                // sw.WriteLine("And");
                                //sw.WriteLine("Welcome");
                                Application.Restart();
                            }
                        }
                        else if (File.Exists(path))
                        {
                            File.Delete(path);
                            using (StreamWriter wt = File.CreateText(path))
                            {
                                wt.WriteLine(textBox3.Text);
                                MessageBox.Show("it is OK تم التسجيل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                // sw.WriteLine("And");
                                //sw.WriteLine("Welcome");
                                Application.Restart();
                            }
                        }
                    }

                    else
                    {
                        MessageBox.Show("Error خطا", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Lice_Click(object sender, EventArgs e)
        {
            countt = countt + 1;

            if (countt > 5)
                txt_Days.Visible = true;
           // MessageBox.Show("OK");
        }
    }
}
