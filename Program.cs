using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Net.NetworkInformation;
using System.Globalization;

namespace POS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        

        static void Main()
        {
            //if (SystemInformation.TerminalServerSession)
            ////  if(Environment.GetEnvironmentVariable("SESSIONNAME").ToLower()  =="console")
            //{
            //    MessageBox.Show("غير مسموح تشغيل التطبيق عن طريق ريموت ديسكتوب","Alert",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            //    return;
            //}

            //try
            //{
            BL.EncDec ende = new BL.EncDec();
                SqlConnection con3 ;//= new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
                var lines2 = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\ServerSetting.txt");
                

                BL.CLS_Session.sqlserver = lines2.First().ToString();

                // BL.CLS_Session.sqldb = ConfigurationManager.AppSettings["logfilelocation"];
                BL.CLS_Session.sqluser = lines2.Skip(2).First().ToString();
                BL.CLS_Session.sqluserpass =ende.Decrypt(lines2.Skip(3).First().ToString(),true);
                BL.CLS_Session.sqlcontimout = lines2.Skip(4).First().ToString();

                var lines3 = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Color.txt");

                if (!string.IsNullOrEmpty(lines3.First().ToString()))
                    BL.CLS_Session.cmp_color = lines3.First().ToString();

                BL.CLS_Session.font_t = lines3.Skip(1).First().ToString();
                BL.CLS_Session.font_s = lines3.Skip(2).First().ToString();
                BL.CLS_Session.is_bold = lines3.Skip(3).First().ToString();
                BL.CLS_Session.fore_color = string.IsNullOrEmpty(lines3.Skip(4).First().ToString()) ? "#000000" : lines3.Skip(4).First().ToString();

                //BaseForm bsf = new BaseForm();
                //bsf.BaseForm_Load(null,null);

                Ping ping = new Ping();
                PingReply pingReply = ping.Send(BL.CLS_Session.sqlserver.StartsWith(".") ? "127.0.0.1" : (BL.CLS_Session.sqlserver.Contains(@"\") ? BL.CLS_Session.sqlserver.Substring(0, BL.CLS_Session.sqlserver.IndexOf(@"\")) :(BL.CLS_Session.sqlserver.Contains(@",") ? BL.CLS_Session.sqlserver.Substring(0, BL.CLS_Session.sqlserver.IndexOf(@",")) : BL.CLS_Session.sqlserver)));
               // MessageBox.Show((BL.CLS_Session.sqlserver.Contains(@"\") ? BL.CLS_Session.sqlserver.Substring(0, BL.CLS_Session.sqlserver.IndexOf(@"\")) : BL.CLS_Session.sqlserver));
                //if (pingReply.Status == IPStatus.Success)
                //{
                    //Server is alive
                    
                    if (!File.Exists(Environment.CurrentDirectory + @"\local.txt"))
                        con3 = new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + lines2.Skip(1).First().ToString() + ";User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=" + BL.CLS_Session.sqlcontimout + "");
                    else
                        con3 = new SqlConnection(@"Data Source=" + lines2.First().ToString() + @";AttachDbFilename=|DataDirectory|\DB\" + lines2.Skip(1).First().ToString() + ".mdf;User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=" + BL.CLS_Session.sqlcontimout + "");

                    try
                    {
                        con3.Open();
                        if (con3.State == ConnectionState.Open)
                            con3.Close();
                        else
                        {
                            MessageBox.Show("فشل الاتصال بقاعدة البيانات .. تاكد من اتصالك او قم باعادة تشغيل الجهاز" , "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                            // Application.Exit();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("فشل الاتصال بقاعدة البيانات .. تاكد من اتصالك او قم باعادة تشغيل الجهاز" + Environment.NewLine + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                        // Application.Exit();
                    }
                //}
                //else
                //{
                //    MessageBox.Show("سرفر البيانات غير متاح .. يرجى التاكد من الشبكة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

               // string licepath = @"C:\Windows\POSLICE.txt";
             //   string licepath = Directory.GetCurrentDirectory() + @"\licen.txt";
                string licepath = Environment.SystemDirectory.ToUpper().Replace(@"\SYSTEM32", "") + @"\PosKey.txt";
                if (!File.Exists(licepath))
                {


                    SqlDataAdapter d = new SqlDataAdapter("select comp_guid,guid_day,guid_dt from companys", con3);
                    DataTable dt = new DataTable();
                    d.Fill(dt);

                   // Lice ds = new Lice();
                   // ds.ShowDialog();

                    //string tcocmpday1 = ende.Decrypt(dt.Rows[0][0].ToString(), true).Substring(0, ende.Decrypt(dt.Rows[0][0].ToString(), true).IndexOf("///"));
                    //string tcocmpday2 = ende.Decrypt(dt.Rows[0][0].ToString(), true).Substring(ende.Decrypt(dt.Rows[0][0].ToString(), true).IndexOf("///") + 1, ende.Decrypt(dt.Rows[0][0].ToString(), true).IndexOf("//////"));
                    //string tcocmpday3 = ende.Decrypt(dt.Rows[0][0].ToString(), true).Substring(ende.Decrypt(dt.Rows[0][0].ToString(), true).IndexOf("//////") + 1, ende.Decrypt(dt.Rows[0][0].ToString(), true).Length);
                    string tcocmpday1 = ende.Decrypt(dt.Rows[0][0].ToString(), true);
                    string tcocmpday2 = ende.Decrypt(dt.Rows[0][1].ToString(), true);
                    string tcocmpday3 = ende.Decrypt(dt.Rows[0][2].ToString(), true);
                   
                  //  int tocomp = Convert.ToInt32(ende.Decrypt(dt.Rows[0][0].ToString(), true));
                  //  MessageBox.Show(tcocmpday1);
                  //  MessageBox.Show(tcocmpday2);
                  //  MessageBox.Show(tcocmpday3);
                  //  MessageBox.Show(Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false))).ToString());

                    int tocomp1 = Convert.ToInt32(tcocmpday1);
                    int tocomp2 = Convert.ToInt32(tcocmpday2);

                    BL.CLS_Session.daycount = (Convert.ToInt32(tcocmpday3) + tocomp2 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false)))).ToString();
                   // MessageBox.Show(Convert.ToInt32(DateTime.Now.AddDays(Convert.ToInt32(tcocmpday2)).ToString("yyyyMMdd", new CultureInfo("en-US", false))).ToString());
                   // MessageBox.Show((Convert.ToInt32(tcocmpday3) + tocomp2 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false)))).ToString());
                    
                    // if (tocomp1 <= 100)
                  //  if (tocomp1 <= 100 && Convert.ToInt32(DateTime.Now.AddDays(Convert.ToInt32(tcocmpday2)*-1).ToString("yyyyMMdd", new CultureInfo("en-US", false))) <= Convert.ToInt32(tcocmpday3))
                  //  if ( Convert.ToInt32(DateTime.Now.AddDays(Convert.ToInt32(tcocmpday2) * -1).ToString("yyyyMMdd", new CultureInfo("en-US", false))) <= Convert.ToInt32(tcocmpday3))
                    if (tocomp1 <= 50 || Convert.ToInt32(BL.CLS_Session.daycount) >= 0)
                    {
                        int tosav = tocomp1 + 1;
                        SqlCommand cmd = new SqlCommand("update companys set comp_guid='" + ende.Encrypt(tosav.ToString(), true) + "'", con3);

                        con3.Open();
                        cmd.ExecuteNonQuery();
                        con3.Close();
                        new Cybele.Thinfinity.VirtualUI().Start();
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new LOGIN());

                        //   BL.EncDec ende = new BL.EncDec();
                        // MessageBox.Show(ende.Encrypt("1", true).ToString());
                        // con3.Open();
                    }
                    else
                    {
                        MessageBox.Show("Trial vertion expired انتهت النسخة التجريبية", "alert تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Lice());
                    }
                    //File.Create(licepath);


                    ////TextWriter tw = new StreamWriter(licepath, true);
                    ////tw.WriteLine("Zph1oFdZmky22eDp4HO36wZO4IziZzXk");
                    ////tw.Close();
                    //File.WriteAllText(@"C:\Windows\lice.txt", "Zph1oFdZmky22eDp4HO36wZO4IziZzXk");
                }
                else
                {




                    ////String thisprocessname = Process.GetCurrentProcess().ProcessName;

                    ////if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1)
                    ////{
                    ////    MessageBox.Show("المعذرة هناك نسخة قيد التشغيل");
                    ////    return;
                    ////}

                   // BL.EncDec endc = new BL.EncDec();


                    string id = HardwareInfo.GetProcessorId() + "" + HardwareInfo.GetBIOSserNo() + "" + HardwareInfo.GetBoardProductId();
                    // ok  string id = HardwareInfo.GetHDDSerialNo();


                    var lines = File.ReadAllLines(licepath);
                    string lice = lines.First().ToString();

                    //string liceDec = endc.Decrypt(lice, true);
                    string liceDec2 = ende.Decrypt(ende.Decrypt(lice, true), true);
                    if (liceDec2.Substring(liceDec2.Length - 5,5).ToString().Equals("Light"))
                    {
                       // MessageBox.Show("it is light");
                        BL.CLS_Session.islight = true;
                    }

                    if (id == ende.Decrypt(ende.Decrypt(lice, true), true) || liceDec2.Substring(liceDec2.Length - 5, 5).ToString().Equals("Light"))
                    {
                        //Application.EnableVisualStyles();
                        //Application.SetCompatibleTextRenderingDefault(false);
                        //Application.Run(new SMSO());
                        new Cybele.Thinfinity.VirtualUI().Start();
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new LOGIN());
                        //Application.Run(new Form1());
                    }
                    else
                    {
                        //   MessageBox.Show(" النسخة غير مرخصة ", "تحذير ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Lice Missing الترخيص مفقود", "alert تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Lice());
                       // Application.Run(new Lice());
                    }

                }


                //ok
                //////////string licepath = @"C:\Windows\lice.txt";
                //////////if (File.Exists(licepath))
                //////////{
                //////////    //Application.EnableVisualStyles();
                //////////    //Application.SetCompatibleTextRenderingDefault(false);
                //////////    //Application.Run(new SMSO());

                //////////    Application.EnableVisualStyles();
                //////////    Application.SetCompatibleTextRenderingDefault(false);
                //////////    Application.Run(new MAIN());
                //////////}
                //////////else
                //////////{
                //////////    MessageBox.Show(" النسخة غير مرخصة ", "تحذير ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //////////}


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            //}
        }
    }
}
