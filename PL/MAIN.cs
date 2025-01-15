using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using CustomUIControls;
using System.Net;
using Ionic.Zip;
//using FastReport;

namespace POS
{
    public partial class MAIN : Form
    {
        TaskbarNotifier taskbarNotifier1;

        string usrlan = "";
        BL.DAML daml = new BL.DAML();
        BL.EncDec encd = new BL.EncDec();
        SqlConnection con3 = BL.DAML.con;//
        int count = 0, fcount = 0, ii = 0;
        private static MAIN frm;
     //   public int scount = 0,rscount=0;

        /*
        static void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm = null;
        }

        public static MAIN getMainForm
        {
            get
            {
                if (frm == null)
                {
                    frm = new MAIN();
                    frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                }
                return frm;
            }
          
        }
        */
        public MAIN()
        {
            /*
            switch (BL.CLS_Session.lang)
            {
                case "ع": Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar"); break;
                case "E": Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en"); break;

            }
           // Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            
           
             InitializeComponent();
           

            if (frm == null)
                frm = this;
            */
            switch (BL.CLS_Session.lang)
            {
                case "ع": Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar"); break;
                case "E": Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en"); break;

            }
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

       
        private void MAIN_Load(object sender, EventArgs e)
        {
            if (!BL.CLS_Session.oneslserial)
                تغييرنوعالفاتورةToolStripMenuItem.Enabled = false;
           // DateTime creation = File.GetCreationTime(Environment.CurrentDirectory + @"\TreeSoft.exe");
            DateTime creation = File.GetLastWriteTime(Environment.CurrentDirectory + @"\TreeSoft.exe");
           // MessageBox.Show(creation.ToString("yyyyMMdd", new CultureInfo("en-US", false)));
           // MessageBox.Show(DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false)));

           // if (creation.AddDays(365).ToString("yyyyMMdd", new CultureInfo("en-US", false)).Equals(DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false))))
            if (Convert.ToDouble(creation.AddDays(365).ToString("yyyyMMdd", new CultureInfo("en-US", false))) <= Convert.ToDouble(DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false))))
            {
                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Very Old Version just to update" : "النسخة قديمة جدا يجب تحديثها", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            try
            {
                this.Icon = Properties.Resources.TsIcon;
            string path = Environment.SystemDirectory.ToUpper().Replace(@"\SYSTEM32", "") + @"\PosKey.txt";
            if (!File.Exists(path))
            {
              //  linkLabel2.Visible = true;

                if (Convert.ToInt32(BL.CLS_Session.daycount) <= 3)
                    MessageBox.Show(BL.CLS_Session.lang.Equals("E")? "Not Licend Copy": "يرجى تفعيـل النسخـة" , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

              // linkLabel2.Text = "عدد الايام المتبقية لانتهاء الترخيص" + " " + BL.CLS_Session.daycount;
            }
            else
            {
               // linkLabel2.Visible = true;
                linkLabel2.Text = BL.CLS_Session.lang.Equals("E") ? "Licend Copy" : "نسخة مسجلة"; 
                linkLabel2.LinkColor = Color.Green;
            }
            
           //// string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
           // string filepath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
           // DirectoryInfo d = new DirectoryInfo(filepath);

           // foreach (var file in d.GetFiles("*.txt"))
           // {
           //    // Directory.Move(file.FullName, filepath + "\\TextFiles\\" + file.Name);
           //     MessageBox.Show(file.Name);
           // }
            if (Directory.Exists(Environment.CurrentDirectory + "\\scripts"))
                Directory.Delete(Environment.CurrentDirectory + "\\scripts", true);

            string[] files = Directory.GetFiles(Environment.CurrentDirectory, "ECRLOG*.txt");

            foreach (string file in files)
            {
               // File.Copy(file, "....");
               // MessageBox.Show(file);
                File.Delete(file);
            }

            string w_file = "TreeSoft.exe";
            string w_directory = Directory.GetCurrentDirectory();

            DateTime c3 = File.GetLastWriteTime(System.IO.Path.Combine(w_directory, w_file));
            // RTB_info.AppendText("Program created at: " + c3.ToString());
           // MessageBox.Show("Program created at: " + c3.ToString("dd-MM-yyyy", new CultureInfo("en-US", false)));
            label3.Text = "Build : " + c3.ToString("yy.MM.dd", new CultureInfo("en-US", false));

            statusStrip1.BackColor = ColorTranslator.FromHtml(BL.CLS_Session.cmp_color);
            menuStrip1.BackColor = ColorTranslator.FromHtml(BL.CLS_Session.cmp_color);
            label1.BackColor = ColorTranslator.FromHtml(BL.CLS_Session.cmp_color);
            label2.BackColor = ColorTranslator.FromHtml(BL.CLS_Session.cmp_color);
            //foreach (ToolStripMenuItem mnit in menuStrip1)
            //{
            //    mnit.BackColor = ColorTranslator.FromHtml(BL.CLS_Session.cmp_color);
            //}
            panel1.BackColor = ColorTranslator.FromHtml(BL.CLS_Session.cmp_color);
            panel2.BackColor = ColorTranslator.FromHtml(BL.CLS_Session.cmp_color);

           // ToolStripMenuItem newitem = new ToolStripMenuItem();
           // newitem.Text = "&New";
            //+"     " + DateTime.Now.ToString("dddd", new CultureInfo("ar-SA", false)) ;
            // DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("ar-AE"))
          //  toolStripStatusLabel1.Text = "     " + System.DateTime.Today.DayOfWeek + "     " + DateTime.Now.ToString("dd-MM-yyyy م", new CultureInfo("en-US", false));
            toolStripStatusLabel1.Text = "     " + (BL.CLS_Session.lang.Equals("ع") ? DateTime.Now.ToString("dd-MM-yyyy م", new CultureInfo("en-US", false)) : DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false)) + " M");
            toolStripStatusLabel5.Text = "     " + (BL.CLS_Session.lang.Equals("ع") ? DateTime.Now.ToString("dddd", new CultureInfo("ar-SA", false)) : DateTime.Now.ToString("dddd", new CultureInfo("en-US", false)));
            toolStripStatusLabel2.Text = "     " + (BL.CLS_Session.lang.Equals("ع") ? DateTime.Now.ToString("dd-MM-yyyy هـ", new CultureInfo("ar-SA", false)) : DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false)) + " H");
            toolStripSplitButton1.Text = toolStripSplitButton1.Text + " :   Company=" + BL.CLS_Session.sqldb.Remove(0,2).Remove(2,5) + "   Branch="+BL.CLS_Session.brno + "   ";
           
              //  MessageBox.Show(BL.CLS_Session.formkey);
                List<ToolStripMenuItem> myItems = GetItems(this.menuStrip1);
                foreach (var item in myItems)
                {
                  //  string sitem = string.IsNullOrEmpty(item.Tag.ToString()) ? "XXX" : item.Tag.ToString();
                    if (BL.CLS_Session.formkey.Contains(item.Tag.ToString()))
                   // if (BL.CLS_Session.formkey.Contains(sitem))
                    {
                        item.Visible = true;
                    }
                    else
                    {
                        item.Visible = false;
                    }

                    item.BackColor = ColorTranslator.FromHtml(BL.CLS_Session.cmp_color);

                    if (BL.CLS_Session.mnu_type.Equals("1") && !item.Tag.Equals("T100"))
                        item.Visible = false;
                }
           

            if (File.Exists(Environment.CurrentDirectory + @"\background.png"))
            {
                this.BackgroundImage = new Bitmap(Environment.CurrentDirectory + @"\background.png");
               // MessageBox.Show(Environment.CurrentDirectory + @"\background.jpg");
               // Image myimage = new Bitmap(Environment.CurrentDirectory + "\background.jpg");
              ////  if (BL.CLS_Session.sysno == "0" || BL.CLS_Session.sysno == "10" || BL.CLS_Session.sysno == "9" || BL.CLS_Session.sysno.Equals("2") || BL.CLS_Session.sysno.Equals("e") || BL.CLS_Session.sysno.Equals("l"))
              ////  this.BackgroundImage = new Bitmap(Environment.CurrentDirectory + @"\background.png");
               // this.BackgroundImage = "";
            }
            //MessageBox.Show(BL.CLS_Session.formkey);
           

            //var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\temp.txt");


           //
            if (BL.CLS_Session.iscashir.Equals("1"))
            { panel2.Visible = true; panel1.Visible = false; }
            else if (BL.CLS_Session.iscashir.Equals("2"))
            { panel1.Visible = true; panel2.Visible = false; }

           // if (!BL.CLS_Session.contr_id.Equals("1"))
           // { panel2.Visible = true; panel1.Visible = false; }
           // else


            if (BL.CLS_Session.cmp_type.Equals("b") || BL.CLS_Session.cmp_type.Equals("p"))
            {
                اصلاحالمبيعاتوالقيودالمحاسبيةToolStripMenuItem.Visible = false;
                الحساباتToolStripMenuItem.Visible = false;
                المبيعاتToolStripMenuItem2.Visible = false;
                العملاءToolStripMenuItem.Visible = false;
                الـمـشترواتtoolStripMenuItem1.Visible = false;
                toolStripMenuItem1.Visible = false;
                التقاريرToolStripMenuItem.Visible = false;
                ترحيلالحركاتToolStripMenuItem.Visible = false;
                فكترحيلالحركاتToolStripMenuItem.Visible = false;
                نقلارصدةمنسنةماضيةToolStripMenuItem.Visible = false;
                بناءارصدةالاصنافوالتكاليفToolStripMenuItem.Visible = false;
                جلبالمبيعاتمنالفروعToolStripMenuItem.Visible = false;
                تكوينالمبيعاتوالقيودالمحاسبيةToolStripMenuItem.Visible = false;
                مبيعاتمطعمToolStripMenuItem.Visible = false;
                مرتجعاتلمسToolStripMenuItem.Visible = false;
               // مبيعاتلمس600800ToolStripMenuItem.Visible = false;
               // تعديلمبيعاتلمس800600ToolStripMenuItem.Visible = false;

                if (BL.CLS_Session.iscofi)
                {
                    المبيعاتToolStripMenuItem1.Visible = false;
                    المرتجعاتToolStripMenuItem.Visible = false;
                    if (0==1)
                    {
                        مبيعاتمطعمToolStripMenuItem.Visible = false;
                        مرتجعاتلمسToolStripMenuItem.Visible = false;

                       // if (BL.CLS_Session.formkey.Contains("S125"))
                       // مبيعاتلمس600800ToolStripMenuItem.Visible = true;
                       // if (BL.CLS_Session.formkey.Contains("S126"))
                       // تعديلمبيعاتلمس800600ToolStripMenuItem.Visible = true;
                    }
                    else
                    {
                        if (BL.CLS_Session.formkey.Contains("S123"))
                        مبيعاتمطعمToolStripMenuItem.Visible = true;
                        if (BL.CLS_Session.formkey.Contains("S124"))
                        مرتجعاتلمسToolStripMenuItem.Visible = true;

                       // مبيعاتلمس600800ToolStripMenuItem.Visible = false;
                       // تعديلمبيعاتلمس800600ToolStripMenuItem.Visible = false;
                    }
                }

            }
            else if (BL.CLS_Session.cmp_type.Equals("s") || BL.CLS_Session.sysno.Equals("ac"))
            {
                if (BL.CLS_Session.cmp_type.Equals("s"))
                {
                    الحساباتToolStripMenuItem.Visible = false;
                }
                else if (BL.CLS_Session.sysno.Equals("ac"))
                {
                    الحساباتToolStripMenuItem.Visible = true;
                    المبيعاتToolStripMenuItem2.Visible = false;
                        الحركاتToolStripMenuItem.Visible = false;
                        العملاءToolStripMenuItem.Visible = false;
                }
               // المبيعاتToolStripMenuItem2.Visible = false;
                //العملاءToolStripMenuItem.Visible = false;
                الـمـشترواتtoolStripMenuItem1.Visible = false;
                toolStripMenuItem1.Visible = false;
                التقاريرToolStripMenuItem.Visible = false;
                ترحيلالحركاتToolStripMenuItem.Visible = false;
                فكترحيلالحركاتToolStripMenuItem.Visible = false;
                نقلارصدةمنسنةماضيةToolStripMenuItem.Visible = false;
                بناءارصدةالاصنافوالتكاليفToolStripMenuItem.Visible = false;
                جلبالمبيعاتمنالفروعToolStripMenuItem.Visible = false;
                تكوينالمبيعاتوالقيودالمحاسبيةToolStripMenuItem.Visible = false;
                مبيعاتمطعمToolStripMenuItem.Visible = false;
                مرتجعاتلمسToolStripMenuItem.Visible = false;
                الحركاتToolStripMenuItem.Visible = false;
                button1.Visible = false;
                button23.Visible = true;
            }

            if (BL.CLS_Session.sysno == "0" || BL.CLS_Session.sysno == "10" || BL.CLS_Session.sysno == "9" || BL.CLS_Session.sysno.Equals("2"))
            {
                عرضسعرخاصToolStripMenuItem.Visible = false;
            }
            if (BL.CLS_Session.sysno == "1")
            {
                foreach (ToolStripMenuItem tsmi in menuStrip1.Items)
                {
                    tsmi.Visible = false;
                }
                العملاتToolStripMenuItem.Visible = false; الجملالشائعةToolStripMenuItem.Visible = false;
                تفصيلحركاتالمبيعاتToolStripMenuItem.Visible = false; عمولاتمندوبيالبيعToolStripMenuItem.Visible = false; ملخصالمبيعاتاليوميةوالشهريةToolStripMenuItem.Visible = false;
                ملخصمبيعاتالفروعToolStripMenuItem.Visible = false; ملخصمبيعاتالمجموعاتToolStripMenuItem.Visible = false; ملخصمبيعاتالعمـــلاءToolStripMenuItem.Visible = false; الاصنافالاكثروالاقلبيعاToolStripMenuItem.Visible = false;
                تفصيلحركاتالعملاءToolStripMenuItem.Visible = false; كشفحسابمعالعمرالزمنيToolStripMenuItem.Visible = false; كشفحسابعميلبالرقمالموحدToolStripMenuItem.Visible = false; ارصدةالعملاءمعالمبالغالمستحقةToolStripMenuItem.Visible = false; ملخصحركاتالعملاءToolStripMenuItem.Visible = false; عمولاتمندوبيالتحصيلToolStripMenuItem.Visible = false; تعميرديونالعملاءToolStripMenuItem.Visible = false; اعمـــارديــونالمـــوردينToolStripMenuItem.Visible = false;
                مراجعةسنداتالصرفوالاستلامبينالفروعToolStripMenuItem.Visible = false; كشفحركةصنفسنواتسابقةToolStripMenuItem.Visible = false; ارصدةالاصناففيالمخازنToolStripMenuItem.Visible = false; ارصدةالاصناففيالفروعToolStripMenuItem.Visible = false; قيمةوارصدةمخزونالمجموعاتToolStripMenuItem.Visible = false; ارصدةوقيمةمخزونالفروعToolStripMenuItem.Visible = false; الاصنافالراكدةوالمتحركةToolStripMenuItem.Visible = false;
                مناديبالتحصيلToolStripMenuItem.Visible = false;
                العروضوالتخفيضاتToolStripMenuItem.Visible = false;
                عرضسعرToolStripMenuItem.Visible = false;
                المبيعاتToolStripMenuItem2.Visible = true;
                تسجيلالخروجToolStripMenuItem.Visible = true;
                الـمـلـفــــاتToolStripMenuItem.Visible = false;
                    مبيعاتToolStripMenuItem.Visible = false;
                    //مبيعاتبلاضريبةToolStripMenuItem.Visible = false;
                        مراجعةحركاتالمبيعتToolStripMenuItem.Visible = false;
                        ملخصالمبيعاتمعالربحToolStripMenuItem.Visible = false;
                            قائمةفواتيرالعملاءمعالربحToolStripMenuItem.Visible = false;
                            ملخصمبيعاتالاصنافToolStripMenuItem1.Visible = false;
                            التقاريرToolStripMenuItem.Visible = true;
               // التقاريرToolStripMenuItem2
                التقريرالشهريToolStripMenuItem.Visible = false;
               // التقاريرToolStripMenuItem2.Visible = false;
                مراجعةحركةالمخازنToolStripMenuItem.Visible = false;
                قيمةالمخزونToolStripMenuItem.Visible = false;
                المخازنToolStripMenuItem.Visible = false;
                كرتالصنفToolStripMenuItem.Visible = false;
                الاصنافبروToolStripMenuItem.Visible = false;
                استيرادالاصنافمناكسلToolStripMenuItem.Visible = false;

                العملاءToolStripMenuItem.Visible = true;
                ملفالتصنيفToolStripMenuItem.Visible = false;
                المدنToolStripMenuItem.Visible = false;
                الحركاتToolStripMenuItem1.Visible = false;
                مراجعةالحركاتToolStripMenuItem1.Visible = false;
                    كشفحسابToolStripMenuItem3.Visible = false;
                    ارصدةالعملاءToolStripMenuItem.Visible = false;

                ملفToolStripMenuItem.Visible = true;
                المستخدمينToolStripMenuItem.Visible = false;
                  //  صنفجديدToolStripMenuItem.Visible = false;
                 //   اعدادالطابعاتالاساسيةToolStripMenuItem.Visible = false;
                        اعدادطابعةالباركودToolStripMenuItem.Visible = false;
                  //      اعدادالطابعاتالاضافيةToolStripMenuItem.Visible = false;
                        //اعداداتالسرفرToolStripMenuItem.Visible = false;
                        الجردToolStripMenuItem.Visible = false;
                        كشفحسابسنواتسابقةToolStripMenuItem.Visible = false;
                        ارصدةالعملاءواخردفعةToolStripMenuItem.Visible = false;
                        قيمةالمخزونالافتتاحيToolStripMenuItem.Visible = false;
                        تعديلسعرصنفToolStripMenuItem.Visible = false;
                        panel1.Visible = false;

            }
            if (BL.CLS_Session.sysno == "2")
            {
                الحركاتToolStripMenuItem.Visible = false;
                عرضسعرخاصToolStripMenuItem.Visible = false;
            }
            if (BL.CLS_Session.sysno == "e" || BL.CLS_Session.sysno == "E")
            {
                الحساباتToolStripMenuItem.Enabled = false;
                toolStripSplitButton2.Enabled = false;
                button3.Enabled = false; button4.Enabled = false; //button12.Visible = false; button13.Visible = false;
                button17.Enabled = false;
                قبضمنفاتورةمقسطةToolStripMenuItem.Enabled = false;
                سدادفاتورةاجلةبسندToolStripMenuItem.Enabled = false;
            }
            if (BL.CLS_Session.sysno == "s" || BL.CLS_Session.sysno == "S")
            {
                الحساباتToolStripMenuItem.Enabled = false;
                toolStripSplitButton2.Enabled = false;
                button3.Enabled = false; button4.Enabled = false; //button12.Visible = false; button13.Visible = false;
                الـمـشترواتtoolStripMenuItem1.Enabled = false;
                toolStripMenuItem1.Enabled = false;
               // button3.Enabled = false; button4.Enabled = false;
                button11.Enabled = false; button13.Enabled = false;
                button17.Enabled = false;
                قبضمنفاتورةمقسطةToolStripMenuItem.Enabled = false;
                سدادفاتورةاجلةبسندToolStripMenuItem.Enabled = false;
            }

            if (BL.CLS_Session.UserID.Equals("admin"))
            {
                المستخدمينToolStripMenuItem.Text =BL.CLS_Session.lang.Equals("E")? "Users File" : "ملف المستخدمين";
                الكاشيرToolStripMenuItem.Visible = true;
            }
            else
            {
                المستخدمينToolStripMenuItem.Text = BL.CLS_Session.lang.Equals("E") ? "Change Password" : "تغيير كلمة السر";
                الكاشيرToolStripMenuItem.Visible = false;
            }
            BL.CLS_Session.dtunits = daml.SELECT_QUIRY_only_retrn_dt("select * from units");


          
                // MessageBox.Show(BL.CLS_Session.lang);

            //switch (lang)
            //{
            //    case "ع": Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar"); break;
            //    case "E": Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en"); break;

            //}
            /*  Menu mn = new Menu();
                mn.MdiParent = Application.OpenForms["MAIN"];
                mn.Show();
             */

            //menuStrip1.Enabled = false;
           // LOGIN log = new LOGIN();
           // log.MdiParent = this;
            // SALES sales = new SALES();
           // log.MdiParent = this;

           // MAIN mn = new MAIN();
           // log.ShowDialog();

           // LOGIN frm = new LOGIN();
           // frm.Parent = mn;
           
           // frm.ShowDialog();
           // this.Hide();
           // this.Hide();
            
           // this.menuStrip1.Items.Clear();
            //SALES sales = new SALES();
            //sales.ShowDialog();
           // WaitForm wf = new WaitForm("جاري تجهيز الملفات .......: " + BL.CLS_Session.UserName);
            WaitForm wf = new WaitForm(BL.CLS_Session.lang.Equals("E") ? "Please wait during Processing files ..." : "جاري تجهيز الملفات ...");
            wf.MdiParent = this;
            wf.Show();
            Application.DoEvents();
           // DoWelcome();
            wf.Close();

            DataTable dtchkmsg = daml.SELECT_QUIRY_only_retrn_dt("select count(*) from notify where isopen=0 and usrnotify='" + BL.CLS_Session.UserID + "'");
            if (Convert.ToInt32(dtchkmsg.Rows[0][0]) > 0)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.Text = "يوجد رسائل غير مقروءة .. انقر نقرا مزدوج لعرضها";
               // notifyIcon1.Icon = SystemIcons.Exclamation;
                notifyIcon1.BalloonTipTitle = "تنبيه";
                notifyIcon1.BalloonTipText = "يوجد لديك رسائل جديدة .. انقر لعرضها";
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.ShowBalloonTip(3000);
              //  notifyIcon1.Visible = false;
            }

            using (SqlCommand cmd = new SqlCommand("dbmstr.dbo.login_sec"))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con3;
                cmd.CommandTimeout = 0;
                if (con3.State == ConnectionState.Closed) con3.Open();
                cmd.ExecuteNonQuery();
                con3.Close();
            }

            //taskbarNotifier1 = new TaskbarNotifier();
            //taskbarNotifier1.MdiParent = this;
            //taskbarNotifier1.SetBackgroundBitmap(new Bitmap(Properties.Resources.skin), Color.FromArgb(255, 0, 255));
            //taskbarNotifier1.SetCloseBitmap(new Bitmap(Properties.Resources.close), Color.FromArgb(255, 0, 255), new Point(179, 8));
            //taskbarNotifier1.TitleRectangle = new Rectangle(50, 9, 100, 25);
            //taskbarNotifier1.ContentRectangle = new Rectangle(8, 41, 180, 68);
            //// taskbarNotifier1.TitleClick += new EventHandler(TitleClick);
            //taskbarNotifier1.ContentClick += new EventHandler(ContentClick);
            //// taskbarNotifier1.CloseClick += new EventHandler(CloseClick);

            //taskbarNotifier1.CloseClickable = true;// checkBoxCloseClickable.Checked;
            //taskbarNotifier1.TitleClickable = false; // checkBoxTitleClickable.Checked;
            //taskbarNotifier1.ContentClickable = true;// checkBoxContentClickable.Checked;
            //taskbarNotifier1.EnableSelectionRectangle = false;// checkBoxSelectionRectangle.Checked;
            //taskbarNotifier1.KeepVisibleOnMousOver = true;// checkBoxKeepVisibleOnMouseOver.Checked;	// Added Rev 002
            //taskbarNotifier1.ReShowOnMouseOver = true;// checkBoxReShowOnMouseOver.Checked;			// Added Rev 002
            //taskbarNotifier1.Show(BL.CLS_Session.UserName + " مرحبا ", " عزيزي العميل شكرا لاستخدامك برنامجنا .. كما نرحب بكل مقترحاتكم بالنقر على هذه الرسالة ", 500, 3000, 2000);
            //// taskbarNotifier1.Close();
            
            if (BL.CLS_Session.mnu_type.Equals("1") && (!BL.CLS_Session.islight || !BL.CLS_Session.sysno.ToUpper().Equals("S") || !BL.CLS_Session.sysno.ToUpper().Equals("E") || !BL.CLS_Session.sysno.ToUpper().Equals("L")))
            {
              //  menuStrip1.Visible = false;
                User_Menu um = new User_Menu(BL.CLS_Session.UserID);
                um.MdiParent = this;
               //// um.Location = new System.Drawing.Point(0, 0);
                //um.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - a.Width, 
                //           Screen.PrimaryScreen.WorkingArea.Height - a.Height);
                //Get screen resolution
                // Rectangle scr = Screen.PrimaryScreen.Bounds;

                // Calculate location (etc. 1366 Width - form size...)
                //oooookkkkkkkkkkkkkkkkkkkk um.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - um.Width, Screen.PrimaryScreen.WorkingArea.Top); 
                ////oooookkkkkkk 
                um.Location = new Point(Screen.PrimaryScreen.Bounds.Right - um.Width - 55, 0);
                um.Show();
            }
            else
            {
               // menuStrip1.Visible = t;
               // um.Close();
            
            }
            if (!BL.CLS_Session.iscofi)
            {
                ملخصحركةالموصلينToolStripMenuItem.Visible = false;
                ملفالاضافاتToolStripMenuItem.Visible = false;
                ملفالموصلينToolStripMenuItem.Visible = false;
                مراجعةمبالغالتامينToolStripMenuItem.Visible = false;
            }

            if (!BL.CLS_Session.issections)
            {
                ارباحوخسائرالقسمالقطاعToolStripMenuItem.Visible = false;
                اقساموقطاعاتToolStripMenuItem.Visible = false;
            }

            if (BL.CLS_Session.ismain)
            {
                ارسالالحركاتToolStripMenuItem.Visible = false;
                // اقساموقطاعاتToolStripMenuItem.Visible = false;
            }
            else
            {
                جلبالبياناتمنالفروعToolStripMenuItem.Visible = false;
            }
            if (BL.CLS_Session.islight || BL.CLS_Session.sysno.ToUpper().Equals("L") || BL.CLS_Session.sysno.ToUpper().Equals("E") || BL.CLS_Session.sysno.ToUpper().Equals("S"))
            {
                استيرادالعملاءمناكسلToolStripMenuItem.Enabled = false; استيرادالموردينمناكسلToolStripMenuItem.Enabled = false;
                طرقالدفعالتطبيقاتToolStripMenuItem.Enabled = false;
                ملخصنشاطالعملاءوالمندوبينToolStripMenuItem.Enabled = false;
                ارسالالحركاتToolStripMenuItem.Enabled = false;
                مراجعةحركاتالمشترياتومصاريفهاToolStripMenuItem.Enabled = false;
                جلبالبياناتمنالفروعToolStripMenuItem.Enabled = false;
                نقلحركةحساباوصتفToolStripMenuItem.Enabled = false;
                ميزانمراجعةموردينToolStripMenuItem.Enabled = false;
                ميزانمراجعةعملاءToolStripMenuItem.Enabled = false;
                مراجعةاسعارالاصنافوالباركوداتToolStripMenuItem.Enabled = false;
                سنداتاستلاموصرفبينالشركاتToolStripMenuItem.Enabled = false;
                قبضمنفاتورةمقسطةToolStripMenuItem.Enabled = false;
                كشفحسابمراجعةالعقودToolStripMenuItem.Enabled = false;
              //  MessageBox.Show("it is light");
                // BL.CLS_Session.islight = true;
                مراجعةسنداتالتحويلبينالمخازنToolStripMenuItem.Enabled = false;
               // تقريراستعراضالاصنافToolStripMenuItem.Visible = false;
                ارباحوخسائرالقسمالقطاعToolStripMenuItem.Enabled = false;
                اقساموقطاعاتToolStripMenuItem.Enabled = false;
               // الفروعToolStripMenuItem.Visible = false;
                العملاتToolStripMenuItem.Enabled = false;
                الجملالشائعةToolStripMenuItem.Enabled = false;
                اخالالارصدةالافتتاحيةToolStripMenuItem.Enabled = false;
                مراكزالتكلفةToolStripMenuItem.Enabled = false;
                تفصيلحركاتالحساباتToolStripMenuItem.Enabled = false;
                كشفحسابToolStripMenuItem2.Enabled = false;
                كشفحسابسنواتسابقةToolStripMenuItem2.Enabled = false;
                ميزانمراجعةحركاتToolStripMenuItem.Enabled = false;
                قائمةالدخلToolStripMenuItem.Enabled = false;
                كشفحسابمراكزالتكلفةToolStripMenuItem.Enabled = false;
                ارصدةمراكزالتكلفةToolStripMenuItem.Enabled = false;
                قائمةدخلمراكزالتكلفةToolStripMenuItem.Enabled = false;
                الموازناتالتقديريةToolStripMenuItem.Enabled = false;
                التدفقاتالنقديةToolStripMenuItem.Enabled = false;
               // مراجعةالحركاتالضريبيةToolStripMenuItem.Visible = false;
               // تقريرالاقرارالضريبيToolStripMenuItem.Visible = false;
                //الفتراتالضريبيةToolStripMenuItem.Visible = false;
               // القيودالضريبيةToolStripMenuItem.Visible = false;
                ////////////////////////////////////////////////////
               // الـمـلـفــــاتToolStripMenuItem.Visible = false;
                ملخصمبيعاتالمجموعاتToolStripMenuItem1.Enabled = false;
                مندوبيالبيعToolStripMenuItem.Enabled = false;
                المخرجينToolStripMenuItem.Enabled = false;
                العروضوالتخفيضاتToolStripMenuItem.Enabled = false;
                //مبيعاتبلاضريبةToolStripMenuItem.Visible = false;
                تفصيلحركاتالمبيعاتToolStripMenuItem.Enabled = false;
                قائمةفواتيرالعملاءمعالربحToolStripMenuItem.Enabled = false;
                ملخصمبيعاتالاصنافToolStripMenuItem1.Enabled = false;
                عمولاتمندوبيالبيعToolStripMenuItem.Enabled = false;
                ملخصالمبيعاتاليوميةوالشهريةToolStripMenuItem.Enabled = false;
                ملخصمبيعاتالفروعToolStripMenuItem.Enabled = false;
                ملخصمبيعاتالمجموعاتToolStripMenuItem.Enabled = false;
                ملخصمبيعاتالعمـــلاءToolStripMenuItem.Enabled = false;
                ملخصمبيعاتاصنافالموردToolStripMenuItem.Enabled = false;
                الاصنافالاكثروالاقلبيعاToolStripMenuItem.Enabled = false;
               // مراجعةحركاتالمبيعاتوضرايبهاToolStripMenuItem.Visible = false;
                ملفالتصنيفToolStripMenuItem.Enabled = false;
                مقارنةالمبيعاتبالتكلفةToolStripMenuItem.Enabled = false;
                متابعةالفواتيرالمقسطةToolStripMenuItem.Enabled = false;
                كشفحسابفاتورةمقسطةToolStripMenuItem.Enabled = false;
                ///////////////////////////////////////////////////////

                المبيعاتاليوميةوالشهريةToolStripMenuItem.Enabled = false;
                ملخصالكاشيراتوالبائعينToolStripMenuItem.Enabled = false;
                قائمةفواتيرصنفToolStripMenuItem.Enabled = false;
                ملخصمبيعاتالاصنافToolStripMenuItem.Enabled = false;
               // مبيعاتالاصنافToolStripMenuItem.Enabled = false;
                جلبالمبيعاتمنالفروعToolStripMenuItem.Enabled = false;
                تصفيرتسلسلالطلباتToolStripMenuItem.Enabled = false;
                اعدادالطابعاتالاضافيةToolStripMenuItem1.Enabled = false;
                اعادةطباعةفاتورةToolStripMenuItem.Enabled = false;
                /////////////////////////////////////////////////////
                المدنToolStripMenuItem.Enabled = false;
                مناديبالتحصيلToolStripMenuItem.Enabled = false;
                تفصيلحركاتالعملاءToolStripMenuItem.Enabled = false;
                كشفحسابToolStripMenuItem3.Enabled = false;
                كشفحسابسنواتسابقةToolStripMenuItem.Enabled = false;
                كشفحسابمعالعمرالزمنيToolStripMenuItem.Enabled = false;
                كشفحسابعميلبالرقمالموحدToolStripMenuItem.Enabled = false;
                ارصدةالعملاءمعالمبالغالمستحقةToolStripMenuItem.Enabled = false;
                ملخصحركاتالعملاءToolStripMenuItem.Enabled = false;
                ارصدةالعملاءواخردفعةToolStripMenuItem.Enabled = false;
                عمولاتمندوبيالتحصيلToolStripMenuItem.Enabled = false;
                تعميرديونالعملاءToolStripMenuItem.Enabled = false;
                اعمـــارديــونالمـــوردينToolStripMenuItem.Enabled = false;
                /////////////////////////////////////////////////
                //toolStripMenuItem2.Visible = false;
                مشترياتمعالاصنافToolStripMenuItem.Enabled = false;
                مشترياتبدونضريبةToolStripMenuItem.Enabled = false;
                تفصيلحركاتالمشترياتToolStripMenuItem.Enabled = false;
                ملخصشراءالاصنافToolStripMenuItem.Enabled = false;
                ملخصالمشترياتمعسعرالبيعToolStripMenuItem.Enabled = false;
                قائمةفواتيرالموردToolStripMenuItem.Enabled = false;
                ملخصمشترياتالفــــروعToolStripMenuItem.Enabled = false;
                ملخصمشترياتالمجموعاتToolStripMenuItem.Enabled = false;
                ملخصمشترياتالموردينToolStripMenuItem.Enabled = false;
                //مراجعةحركاتالمشترياتوضرايبهاToolStripMenuItem.Visible = false;
                ////////////////////////////////////////////////////////////
                ملفتصنيفالموردينToolStripMenuItem.Enabled = false;
                قيدعامعملاتToolStripMenuItem.Enabled = false;
                تفصيلحركاتالموردينToolStripMenuItem.Enabled = false;
                toolStripMenuItem15.Enabled = false;
                كشفحسابسنواتسابقةToolStripMenuItem1.Enabled = false;
                كشفحسابموردبالرقمالموحدToolStripMenuItem.Enabled = false;
                ارصدةالموردينمعالمبالغالمستحقةToolStripMenuItem.Enabled = false;
                ارصدةالموردينواخردفعةToolStripMenuItem.Enabled = false;
                ملخصحركــاتالمــوردينToolStripMenuItem.Enabled = false;

                //////////////////////////////////////////////
                المخازنToolStripMenuItem.Enabled = false;
                استيرادالاصنافمناكسلToolStripMenuItem.Enabled = false;
               // تعديلسعرصنفToolStripMenuItem.Visible = false;
                تحويلكميةاصنافToolStripMenuItem.Enabled = false;
                تركيباوانتاجصنفToolStripMenuItem.Enabled = false;
                سنداتاستلاموصرفبينالفروعToolStripMenuItem.Enabled = false;
                تفصيلحركةالمخازنToolStripMenuItem.Enabled = false;
                مراجعةسنداتالصرفوالاستلامبينالفروعToolStripMenuItem.Enabled = false;
                تفصلالحركاتبينالفروعToolStripMenuItem.Enabled = false;
                كشفحركةصنفسنواتسابقةToolStripMenuItem.Enabled = false;
                ارصدةالاصناففيالمخازنToolStripMenuItem.Enabled = false;
                ارصدةالاصناففيالفروعToolStripMenuItem.Enabled = false;
                قيمةوارصدةمخزونالمجموعاتToolStripMenuItem.Enabled = false;
                ارصدةوقيمةمخزونالفروعToolStripMenuItem.Enabled = false;
                الاصنافالراكدةوالمتحركةToolStripMenuItem.Enabled = false;
                /////////////////////////////////////////////////////
               // button7.Visible = false;
                button17.Enabled = false;
            }
            if (BL.CLS_Session.is_einv_p2)
               تجهيزوارسالالفواتيرللزكاةToolStripMenuItem.Visible = true;
            else
               تجهيزوارسالالفواتيرللزكاةToolStripMenuItem.Visible = false;

            if (BL.CLS_Session.sysno.Equals("9"))
                التسويةToolStripMenuItem.Text = "سندات ادخال واخراج";

            if (panel1.Visible)
                button18.Select();
            else
                button19.Select();
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        void ContentClick(object obj, EventArgs ea)
        {
            // MessageBox.Show("Content was Clicked");
           // SendMail sd = new SendMail();
            About sd = new About();
            sd.MdiParent = this;
            sd.Show();
        }

        public void DoWelcome()
        {
          //  System.Threading.Thread.Sleep(1000);
            try
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Connection = BL.DAML.con;
                myCommand.CommandText = "create_messing_columns";
                BL.DAML.con.Open();
                myCommand.ExecuteNonQuery();
                BL.DAML.con.Close();
                System.Threading.Thread.Sleep(1000);
            }
            catch { }
        }
        /*
        protected override void OnKeyPress(KeyPressEventArgs ex)
        {
            string xo = ex.KeyChar.ToString();

          //  if (xo == "Control & q") //You pressed "q" key on the keyboard
          // if (((Control.ModifierKeys & Keys.Control) == Keys.Control) && xo == "A")
          //  if (((Control.ModifierKeys & Keys.Control) == Keys.Control) && ex.KeyChar == 'A')
          //  if (ex.KeyChar ==  (char)Keys.ControlKey && ex.KeyChar == 'Q')
            //if (ex.KeyChar == (char)Keys.ControlKey && ex.KeyChar == (char)Keys.N)
            //{
            //    LOGIN f2 = new LOGIN();
            //    f2.Show();
            //}
        }
        */
        public static List<ToolStripMenuItem> GetItems(MenuStrip menuStrip)
        {
            List<ToolStripMenuItem> myItems = new List<ToolStripMenuItem>();
            foreach (ToolStripMenuItem i in menuStrip.Items)
            {
                GetMenuItems(i, myItems);
            }
            return myItems;
        }

        private static void GetMenuItems(ToolStripMenuItem item, List<ToolStripMenuItem> items)
        {
            items.Add(item);
            foreach (ToolStripItem i in item.DropDownItems)
            {
                if (i is ToolStripMenuItem)
                {
                    GetMenuItems((ToolStripMenuItem)i, items);
                }
            }
        }

       
        private void المبيعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
           // SALES f = new SALES();
           // f.ShowDialog();
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // taskbarNotifier1.Close();
            if (Application.OpenForms.Count <= 2)
            {
                DialogResult result = MessageBox.Show("Do you want to backup now هل تريد اخذ نسخة الان", "تنبيه", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                if (result == DialogResult.Yes)
                {
                    Backup bk = new Backup();
                    bk.MdiParent = this;
                    bk.button1.Visible = false;
                    bk.Show();
                    bk.button1_Click(sender, e);

                   // daml.Exec_Query_only("update tobld set id=1");
                  //  taskbarNotifier1.Close();
                    Application.Exit();
                    // bk.MdiParent = this;
                    // bk.Show();
                    // Application.Exit();

                }
                else if (result == DialogResult.No)
                {
                    // if (this.ActiveMdiChild != null)
                    // int fcont = Application.OpenForms.Count;


                    //  this.ActiveMdiChild.Close();
                  //  daml.Exec_Query_only("update tobld set id=1");
                  //  taskbarNotifier1.Close();
                      Application.Exit();

                    //...
                }
                else
                {
                    //...
                } 
             //   Application.Exit();
            }
            else
            {
                MessageBox.Show("هناك شاشات مفتوحة", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            
            //LOGIN ln = new LOGIN();
            //ln.MdiParent = this;
            //ln.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void جديدToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void صنفجديدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            FRM_USERS_LIST ful = new FRM_USERS_LIST();
             
            ful.MdiParent = this;
            ful.Show();
             * */
            //Set_Form stf = new Set_Form();
            //stf.MdiParent=this;
            //stf.Show();
        }

        private void الدعمالفنيToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //count++;

            //if ((count % 2) == 0)
            //    System.Diagnostics.Process.Start(@"AD.exe");
            //else
            //{
            //    System.Diagnostics.Process.Start(@"TV.exe");
            //    //if ((count % 3) == 0)
            //    //    System.Diagnostics.Process.Start(@"AA.exe");
            //    //else
            //    //    System.Diagnostics.Process.Start(@"AD.exe");
            //}

            
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {/**
            test1 t = new test1();
            t.Show();
          * */
            /*
            Form1 f1 = new Form1();
            f1.Show();
             **/
        }

        private void التقريراليوميToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void تنفيذToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void المبيعاتToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ////////DataTable dtch = daml.SELECT_QUIRY_only_retrn_dt("select last_login from contrs");
            //////////MessageBox.Show(dtch.Rows[0][0].ToString());
            //////////MessageBox.Show(DateTime.Now.ToString());
            ////////if (Convert.ToDateTime(dtch.Rows[0][0]) < DateTime.Now)
            ////////{
            if (BL.CLS_Session.istuch)
            {
                Pos.SALES_D_TCH sales = new Pos.SALES_D_TCH();
                // SALES sales = new SALES();
                sales.MdiParent = this;
                sales.Show();
            }
            else
            {
                Pos.SALES_D_TCH sales = new Pos.SALES_D_TCH();
                // SALES sales = new SALES();
                sales.MdiParent = this;
                sales.Show();
            }
            ////////}
            ////////else
            ////////{
            ////////MessageBox.Show("Date Time Error","",MessageBoxButtons.OK,MessageBoxIcon.Error);
            ////////}
        }

        private void التقريراليوميToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           /* Pos.DilySales sr = new Pos.DilySales();
            sr.MdiParent = this;
            sr.Show();
            */
            if (BL.CLS_Session.iscofi)
            {
                Pos.DilySalesReport_r sr = new Pos.DilySalesReport_r();
                sr.MdiParent = this;
                sr.Show();
            }
            else
            {
                Pos.DilySalesReport sr = new Pos.DilySalesReport();
                sr.MdiParent = this;
                sr.Show();
            }
        }

        private void كرتالصنفToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //ITEMS ites = new ITEMS();
            //ites.MdiParent = this;
            //ites.Show();
        }

        private void تصديرالاصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //ItemsExpert ie = new ItemsExpert();
            //ie.MdiParent = this;
            //ie.Show();
        }

        private void البحثعنصنفToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //itmSearch ser = new itmSearch();
            //ser.MdiParent = this;
            //ser.Show();
        }

        private void المرتجعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////////DataTable dtch = daml.SELECT_QUIRY_only_retrn_dt("select last_login from contrs");
            //////////MessageBox.Show(dtch.Rows[0][0].ToString());
            //////////MessageBox.Show(DateTime.Now.ToString());
            ////////if (Convert.ToDateTime(dtch.Rows[0][0]) < DateTime.Now)
            ////////{
            //Pos.RESALES11 rs = new Pos.RESALES11();
           
            if (BL.CLS_Session.istuch)
            {
                Pos.RESALES_D_TCH sales = new Pos.RESALES_D_TCH();
                // SALES sales = new SALES();
                sales.MdiParent = this;
                sales.Show();
            }
            else
            {
                Pos.RESALES_D_TCH sales = new Pos.RESALES_D_TCH();
                // SALES sales = new SALES();
                sales.MdiParent = this;
                sales.Show();
            }
            ////////}
            ////////else
            ////////{
            ////////    MessageBox.Show("Date Time Error", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////////}
        }

        private void التقريرالشهريToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.iscofi)
            {
                Pos.Sales_Report_r sr = new Pos.Sales_Report_r();
                sr.MdiParent = this;
                sr.Show();
            }
            else
            {
                Pos.Sales_Report sr = new Pos.Sales_Report();
                sr.MdiParent = this;
                sr.Show();
            }
        }

        private void المستخدمينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //USERS us = new USERS();
            //us.MdiParent = this;
            //us.Show();
            if (BL.CLS_Session.UserID.Equals("admin"))
            {
                USER_PRO us = new USER_PRO();
                us.MdiParent = this;
                us.Show();
            }
            else
            {
                Chang_UsrPass us = new Chang_UsrPass(BL.CLS_Session.UserID);
                us.MdiParent = this;
                us.Show();
            }
            
            //Login_Test us = new Login_Test();
            //us.MdiParent = this;
            //us.Show();
        }

        private void تعريفصنفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
           // Item_Card it = new Item_Card(null);
            Sto.Item_Card it = new Sto.Item_Card("");
            it.MdiParent = this;
            it.Show();
        }

        private void التسويةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            RECV_frm rr = new RECV_frm();
            rr.MdiParent = this;
            rr.Show()*/
            if (BL.CLS_Session.sysno.Equals("9"))
            {
                Sto.Sto_InOut rr = new Sto.Sto_InOut("", "", "");
                rr.MdiParent = this;
                rr.Show();
            }
            else
            {
                Sto.Sto_ImpExp rr = new Sto.Sto_ImpExp("", "", "");
                rr.MdiParent = this;
                rr.Show();
            }
        }

        private void تفاصيلفاتوةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Order_dtl ot = new Order_dtl();
            //ot.MdiParent=this;
            //ot.Show();
        }

        private void حذفصنفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            Del_Pro dp = new Del_Pro();
            dp.MdiParent = this;
            dp.Show();
             * */
            Sto.Sto_qty_Convert dp = new Sto.Sto_qty_Convert("","","");
            dp.MdiParent = this;
            dp.Show();
        }

        private void التقريرالشهريToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void مبيعاتمطعمToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////DataTable dtch = daml.SELECT_QUIRY_only_retrn_dt("select last_login from contrs");
            //////MessageBox.Show(dtch.Rows[0][0].ToString());
            //////MessageBox.Show(DateTime.Now.ToString());
            ////if (Convert.ToDateTime(dtch.Rows[0][0]) < DateTime.Now)
            ////{
            Pos.Resturant_sales dp = new Pos.Resturant_sales();
            dp.MdiParent = this;
            dp.Show();
            //// }
            ////else
            ////{
            ////MessageBox.Show("Date Time Error","",MessageBoxButtons.OK,MessageBoxIcon.Error);
            ////}

        }

        private void المجموعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Groups gr = new Sto.Groups();
            gr.MdiParent = this;
            gr.Show();
        }

        private void المجموعاتتستToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Group_Test gr = new Group_Test();
            //gr.MdiParent = this;
            //gr.Show();
        }

        private void orderdetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Pos.ORDER_DETAILS or = new Pos.ORDER_DETAILS();
            //or.MdiParent = this;
            //or.Show();
        }


        private void chek()
        { 
        

        
        }

        private void الوحداتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            PL.FRM_PRODUCTS_LIST crd = new PL.FRM_PRODUCTS_LIST();
            crd.MdiParent = this;
            crd.Show();
            */
            Sto.Units crd = new Sto.Units();
            crd.MdiParent = this;
            crd.Show();
        }

        private void الاصنافبروToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.FRM_PRODUCTS crd2 = new Sto.FRM_PRODUCTS();
            crd2.MdiParent = this;
            crd2.Show();
        }

        private void مرتجعاتلمسToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //// DataTable dtch = daml.SELECT_QUIRY_only_retrn_dt("select last_login from contrs");
            //////MessageBox.Show(dtch.Rows[0][0].ToString());
            //////MessageBox.Show(DateTime.Now.ToString());
            ////if (Convert.ToDateTime(dtch.Rows[0][0]) < DateTime.Now)
            ////{
            if (BL.CLS_Session.is_einv)
            {
                Pos.Resturant_ReSales2 dp = new Pos.Resturant_ReSales2();
                dp.MdiParent = this;
                dp.Show();
            }
            else
            {
                Pos.Resturant_ReSales dp = new Pos.Resturant_ReSales("", "", "");
                dp.MdiParent = this;
                dp.Show();
            }
            ////}
            ////else
            ////{
            ////    MessageBox.Show("Date Time Error", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////}
        }

        private void المخازنToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Whouses dp = new Sto.Whouses();
            dp.MdiParent = this;
            dp.Show();
        }

        private void حولToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(" 0534275924 : " + "شركة حلول التقنية  تلفون", "حول", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            //FReport ab = new FReport();
            //ab.MdiParent = this;
            //ab.Show();

            About ab = new About();
            ab.MdiParent = this;
            ab.Show();
        }

        private void مبيعاتالاصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.ItemsDilySalesReport idr = new Pos.ItemsDilySalesReport();
            idr.MdiParent = this;
            idr.Show();
            
        }

        private void اعادةطباعةفاتورةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.RE_PRT_FRM prt = new Pos.RE_PRT_FRM();
            prt.MdiParent = this;
            prt.Show();
        }

        private void اضافةعميلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PL.FRM_CUSTOMERS cust = new PL.FRM_CUSTOMERS();
            Cus.Customers cust = new Cus.Customers("");
            cust.MdiParent = this;
            cust.Show();
        }

        private void ملخصمبيعاتالاصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Pos.RangItemsDilySalesReport rd = new Pos.RangItemsDilySalesReport();
            //rd.MdiParent = this;
            //rd.Show();
            Pos.Pos_Report_items idr = new Pos.Pos_Report_items();
            idr.MdiParent = this;
            idr.Show();
        }

        private void كشفحسابToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.custDilySalesReport cdr = new Pos.custDilySalesReport();
            cdr.MdiParent = this;
            cdr.Show();
        }

        private void كشفحسابمنالىToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.custRangDilySalesReport crdr = new Pos.custRangDilySalesReport();
            crdr.MdiParent = this;
            crdr.Show();
        }

        private void الحركاتToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {

            switch (Thread.CurrentThread.CurrentUICulture.IetfLanguageTag)
            {
                case "en": Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar"); break;
                case "ar": Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en"); break;

            }
            
           // this.companyols.Clear();
           // this.menuStrip1.Items.Clear();
            
           // InitializeComponent();
            set_permision(sender, e);
           
           
        }

        private void اخذنسخةاحتياطيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Backup bk = new Backup();
           // Open_New_Yr bk = new Open_New_Yr();
            bk.MdiParent=this;
            bk.Show();
        }

        private void مبيعاتلمس600800ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////DataTable dtch = daml.SELECT_QUIRY_only_retrn_dt("select last_login from contrs");
            //////MessageBox.Show(dtch.Rows[0][0].ToString());
            //////MessageBox.Show(DateTime.Now.ToString());
            ////if (Convert.ToDateTime(dtch.Rows[0][0]) < DateTime.Now)
            ////{
            //Pos.Resturant_sales_800_600 rs = new Pos.Resturant_sales_800_600();
            //rs.MdiParent = this;
            //rs.Show();
            //// }
            ////else
            ////{
            ////    MessageBox.Show("Date Time Error", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////}
        }

        private void ملفToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void الكاشيرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Company rs = new Company();
            rs.MdiParent = this;
            rs.Show();
        }

        private void مبيعاتلمسبلاصورToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Pos.Resturant_sales_NoImage rs = new Pos.Resturant_sales_NoImage();
            //rs.MdiParent = this;
            //rs.Show();

        }

        public void تسجيلالخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int scount = BL.CLS_Session.mnu_type.Equals("1") ? 3 : 2;
            if (Application.OpenForms.Count <= scount)
            {
                DataTable dtch = daml.SELECT_QUIRY_only_retrn_dt("select last_login from contrs");
                //MessageBox.Show(dtch.Rows[0][0].ToString());
                //MessageBox.Show(DateTime.Now.ToString());
                if (DateTime.Now > Convert.ToDateTime(dtch.Rows[0][0]))
                {
                    BL.CLS_Session.dtsalman = null;
                    daml.Exec_Query_only("update contrs set last_login=getdate()");
                    
                    LOGIN rs = new LOGIN();
                    ////rs.MdiParent = this;
                    rs.ShowDialog();
                    this.Close();
                }
                else
                {
                    BL.CLS_Session.dtsalman = null;
                    LOGIN rs = new LOGIN();
                    ////rs.MdiParent = this;
                    rs.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                 DialogResult result = MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Do You Want To LogOut? .. Maybe Lost Not Saved Data" : "هناك شاشات مفتوحة .. ستفقد المعلومات التي لم تقم بحفضها. هل تريد الخروج فعلا ؟", "تحذير", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                 if (result == DialogResult.Yes)
                 {
                     /*
                     List<Form> openForms = new List<Form>();

                     foreach (Form f in Application.OpenForms)
                         openForms.Add(f);

                     foreach (Form f in openForms)
                     {
                         if (f.Name != "LOGIN" || f.Name != "MAIN")
                             f.Close();
                     }
                     */
                     // MessageBox.Show("هناك شاشات مفتوحة", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     BL.CLS_Session.dtsalman = null;
                     LOGIN rs = new LOGIN();
                     ////rs.MdiParent = this;
                     rs.ShowDialog();
                     this.Close();

                     ////List<Form> openForms = new List<Form>();

                     ////foreach (Form f in Application.OpenForms)
                     ////    openForms.Add(f);

                     ////foreach (Form f in openForms)
                     ////{
                     ////    if (f.Name != "LOGIN")
                     ////        f.Close();
                     ////}
                 }
                 else if (result == DialogResult.No)
                 {
                     //...
                 }
                 else
                 {
                     //...
                 }
            }
            /*
            this.Hide();
            LOCK mn = new LOCK();
           // mn.MdiParent = this;
            mn.ShowDialog();
            */
           // Application.Restart();
            //LOGIN mn = new LOGIN();
            //mn.ShowDialog();
            
          //  Application.Restart();
           // set_permision(sender, e);
           // this.Hide();
        }

        private void اعدادالطابعاتالاساسيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void اعدادالطابعاتالاضافيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void اختبارToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Pos.Resturant_sales_Test rs = new Pos.Resturant_sales_Test();
            //rs.MdiParent = this;
            //rs.Show();

            //TreeTest
            //TreeTest rs = new TreeTest();
            //rs.MdiParent = this;
            //rs.Show();
        }

        private void ملفالدليلالمحاسبيToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Accounts rs = new Acc.Accounts();
            rs.MdiParent = this;
            rs.Show();
        }

        //public void lang_select(object sender, EventArgs e)
        //{

        //    switch (Thread.CurrentThread.CurrentUICulture.IetfLanguageTag)
        //    {
        //        case "en": Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar"); break;
        //        case "ar": Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en"); break;

        //    }
           
        //    InitializeComponent();


        //}

        public void set_permision(object sender, EventArgs e)
        { /*
            //menuStrip1.Enabled = true;
            var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\temp.txt");

           // usrlan = lines.Skip(6).First().ToString();
            usrlan = BL.CLS_Session.lang;

            bool usrbol =Convert.ToBoolean(lines.Skip(7).First());
            bool usredit = Convert.ToBoolean(BL.CLS_Session.UseEdit);
            //SqlDataAdapter da3 = new SqlDataAdapter("select user_type from users where user_id =" + Convert.ToInt32(usr) + "" , con3);

            //DataTable dt3 = new DataTable();

            //da3.Fill(dt3);

            //if (Convert.ToBoolean(dt3.Rows[0][0]) == false)

            //{
            if (usrlan == "E")
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                this.companyols.Clear();
                InitializeComponent();
                //set_permision(sender, e);
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar");
                this.companyols.Clear();
                InitializeComponent();
            }
            if (usrbol == false)
            {
                // mn.menuStrip1.Items.Clear();
                // MAIN.getMainForm.ملفToolStripMenuItem.Visible = false;
                MAIN.getMainForm.المستخدمينToolStripMenuItem.Visible = false;
                MAIN.getMainForm.التقاريرToolStripMenuItem1.Visible = false;
                MAIN.getMainForm.التقاريرToolStripMenuItem.Visible = false;
               
                MAIN.getMainForm.التقريراليوميToolStripMenuItem2.Visible = false;
               // MAIN.getMainForm.المرتجعاتToolStripMenuItem.Visible = false;
                MAIN.getMainForm.الكاشيرToolStripMenuItem.Visible = false;
                MAIN.getMainForm.صنفجديدToolStripMenuItem.Visible = false;
                MAIN.getMainForm.العملاءToolStripMenuItem.Visible = false;
                //MAIN.getMainForm.ملفالدليلالمحاسبيToolStripMenuItem.Visible = false;
                MAIN.getMainForm.اعدادالطابعاتالاضافيةToolStripMenuItem.Visible = false;
                MAIN.getMainForm.اعدادالطابعاتالاساسيةToolStripMenuItem.Visible = false;
               // MAIN.getMainForm.englishToolStripMenuItem.Visible = false;

                // main_frm.ملفToolStripMenuItem.Visible = false;
            }
            else
            {
                MAIN.getMainForm.المستخدمينToolStripMenuItem.Visible = true;
                MAIN.getMainForm.التقاريرToolStripMenuItem1.Visible = true;
                MAIN.getMainForm.التقاريرToolStripMenuItem.Visible = true;
                
                MAIN.getMainForm.التقريراليوميToolStripMenuItem2.Visible = true;
               // MAIN.getMainForm.المرتجعاتToolStripMenuItem.Visible = true;
                MAIN.getMainForm.الكاشيرToolStripMenuItem.Visible = true;
                MAIN.getMainForm.صنفجديدToolStripMenuItem.Visible = true;
                MAIN.getMainForm.العملاءToolStripMenuItem.Visible = true;
                //MAIN.getMainForm.ملفالدليلالمحاسبيToolStripMenuItem.Visible = true;
                MAIN.getMainForm.اعدادالطابعاتالاضافيةToolStripMenuItem.Visible = true;
                MAIN.getMainForm.اعدادالطابعاتالاساسيةToolStripMenuItem.Visible = true;
               // MAIN.getMainForm.englishToolStripMenuItem.Visible = true;

            }

            if (usredit == false)
            {
                MAIN.getMainForm.مرتجعاتلمسToolStripMenuItem.Visible = false;
            }
            else
            {
                MAIN.getMainForm.مرتجعاتلمسToolStripMenuItem.Visible = true;
            }

            */
           
           // menuStrip1.Refresh();
        }

        private void تقريرتجربةToolStripMenuItem_Click(object sender, EventArgs e)
        {



            SqlDataAdapter dacr1 = new SqlDataAdapter("select * from sales_hdr where company=" + BL.CLS_Session.cmp_id, con3);
                   

                    DataSet1 ds = new DataSet1();

                   
                    ds.Tables["hdr"].Clear();
                   

                   
                    dacr1.Fill(ds, "hdr");
                    

                    //DataSet ds = new DataSet();



                    //dacr.Fill(ds, "dtl");
                    //dacr1.Fill(ds, "hdr");
                    //chk.Fill(ds, "sum");

                    // DataTable dtchk = new DataTable();
                    //// dtchk.Clear();

                    // chk.Fill(dtchk);


                    Pos.rpt.Dily_Sales_Report report = new Pos.rpt.Dily_Sales_Report();
                        //  CrystalReport1 report = new CrystalReport1();
                        report.SetDataSource(ds);
                        report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                        //report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                       // report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());

                        //    crystalReportViewer1.ReportSource = report;
                        RPT.FRM_RPT_PRODUCT prt = new RPT.FRM_RPT_PRODUCT();

                        prt.crystalReportViewer1.ReportSource = report;

                        prt.ShowDialog();

                        //  crystalReportViewer1.Refresh();

                        // report.PrintOptions.PrinterName = "pos";

                        // report.PrintToPrinter(1, false, 0, 0);
                        // report.PrintToPrinter(0,true, 1, 1);
                        report.Close();
                        // report.Dispose();
                  
               
        }

        private void ادارةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagmentQuery mgq = new ManagmentQuery();
            mgq.MdiParent = this;
            mgq.Show();
        }

        private void اعداداتالسرفرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Server_Setting mgq = new Server_Setting();
            mgq.MdiParent = this;
            mgq.Show();
        }

        private void اعداداتالاتصالبالسرفرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.ismain)
            {
                Server_Links mgq = new Server_Links();
                mgq.MdiParent = this;
                mgq.Show();
            }
            else
            {
                Server_Setting mgq = new Server_Setting();
                mgq.MdiParent = this;
                mgq.Show();
            }
           
        }

        private void تعديلمبيعاتلمس800600ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //// DataTable dtch = daml.SELECT_QUIRY_only_retrn_dt("select last_login from contrs");
            //////MessageBox.Show(dtch.Rows[0][0].ToString());
            //////MessageBox.Show(DateTime.Now.ToString());
            ////if (Convert.ToDateTime(dtch.Rows[0][0]) < DateTime.Now)
            ////{
            //Pos.Resturant_ReSales_800_600 frf = new Pos.Resturant_ReSales_800_600();
            // frf.MdiParent = this;
            // frf.Show();
            ////}
            ////else
            ////{
            ////    MessageBox.Show("Date Time Error", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////}
        }

        private void قيمةالمخزونToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Sto.Sto_Whbins fr = new Sto.Sto_Whbins();
            // fr.MdiParent = this;
            // fr.Show();
            ////Sto.Sto_Whbins_New fr = new Sto.Sto_Whbins_New();
            ////fr.MdiParent = this;
            ////fr.Show();
            Sto.Sto_Whbins_Org fr = new Sto.Sto_Whbins_Org();
            fr.MdiParent = this;
            fr.Show();
        }

        private void اعدادطابعةالباركودToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // SetBarcPrt
             SetBarcPrt br = new SetBarcPrt();
             br.MdiParent = this;
             br.Show();
        }

        private void الحركاتToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void الدليلالمحاسبيToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Acc.Acc_Card ac = new Acc.Acc_Card(null);
            ac.MdiParent = this;
            ac.Show();
        }

        private void قيدعامToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_Tran f6 = new Acc.Acc_Tran("", "","");
            f6.MdiParent = this;
            f6.Show();
        }

        private void مراجعةالحركاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_Report f6 = new Acc.Acc_Report();
            f6.MdiParent = this;
            f6.Show();
        }

        private void ارصدةالحساباتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_Balance f6 = new Acc.Acc_Balance();
            f6.MdiParent = this;
            f6.Show();
        }

        private void كشفحسابToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Acc.Acc_Statment_Exp f6 = new Acc.Acc_Statment_Exp("");
            f6.MdiParent = this;
            f6.Show();
        }

        private void كشفحسابToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Acc.Acc_Statment_org f6 = new Acc.Acc_Statment_org("");
            f6.MdiParent = this;
            f6.Show();
        }

        private void ميزانالمراجعةToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Acc.Acc_Mizan f6 = new Acc.Acc_Mizan();
            f6.MdiParent = this;
            f6.Show();
        }

        private void الحساباتالختاميةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_Final_Report f6 = new Acc.Acc_Final_Report();
            f6.MdiParent = this;
            f6.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.formkey.Contains("A121"))
            {
                Acc.Acc_Tran f6 = new Acc.Acc_Tran("", "","");
                f6.MdiParent = this;
                f6.Show();
            }
            
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
           // toolTip1.Show("هلا",button1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.Icon = SystemIcons.Exclamation;
            notifyIcon1.BalloonTipTitle = "Balloon Tip Title";
            notifyIcon1.BalloonTipText = "Balloon Tip Text.";
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.ShowBalloonTip(1000);
        }

        private void MAIN_VisibleChanged(object sender, EventArgs e)
        {
           
        }

        private void الضرايبToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Taxs f6 = new Sto.Taxs();
            f6.MdiParent = this;
            f6.Show();
        }

        private void الفروعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Branchs f6 = new Branchs();
            f6.MdiParent = this;
            f6.Show();
        }

        private void مركزالبيعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.SLcenters f6 = new Sal.SLcenters();
            f6.MdiParent = this;
            f6.Show();
        }

        private void ملفالكاشيرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(BL.CLS_Session.cmp_type.StartsWith("m"))
            {
                //Pos.Cashirs rs = new Pos.Cashirs();
                //rs.MdiParent = this;
                //rs.Show();
                Pos.CTR_FRM rs = new Pos.CTR_FRM();
                rs.MdiParent = this;
                rs.Show();  
            }
            else
            {
                Pos.CTR_FRM rs = new Pos.CTR_FRM();
                rs.MdiParent = this;
                rs.Show();      
            }
        }

        private void سندقبضToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Acc.Acc_Tran_Q rs = new Acc.Acc_Tran_Q("","");
            rs.MdiParent = this;
            rs.Show();
        }

        private void سندصرفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_Tran_S rs = new Acc.Acc_Tran_S("", "");
            rs.MdiParent = this;
            rs.Show();
        }

        private void مراكزالبيعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.SLcenters rs = new Sal.SLcenters();
            rs.MdiParent = this;
            rs.Show();
        }

        private void مراكزالشراءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pur.PUcenters rs = new Pur.PUcenters();
            rs.MdiParent = this;
            rs.Show();
        }

        private void اخالالارصدةالافتتاحيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_opnbal rs = new Acc.Acc_opnbal();
            rs.MdiParent = this;
            rs.Show();
        }

        private void مبيعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.cmp_type.StartsWith("a"))
            {
                Sal.Sal_Tran_D_TF rs = new Sal.Sal_Tran_D_TF("", "", "");
                rs.MdiParent = this;
                rs.Show();
              
               //  Type t = Type.GetType("Branchs");
               // Form rs = (Form) Activator.CreateInstance(t);
               // string nullstr="";
               /* var rs = Activator.CreateInstance(Type.GetType("POS.Sal.Sal_Tran_D_TF"),) as Form;
               // rs=new Form()
               // Form tmp = (Form)Activator.CreateInstance(typelist[i]);
                rs.MdiParent = this;
                rs.Show();*/
            }
            else
            {
                //if (BL.CLS_Session.tax_no.Equals(""))
                //{
                //    Sal.Sal_Tran_notax rs = new Sal.Sal_Tran_notax("", "", "");
                //    rs.MdiParent = this;
                //    rs.Show();
                //}
                //else
                //{
                    Sal.Sal_Tran_D rs = new Sal.Sal_Tran_D("", "", "");
                    rs.MdiParent = this;
                    rs.Show();
                //}
            }
        }

        private void مبيعاتبلاضريبةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Tran_notax rs = new Sal.Sal_Tran_notax("", "", "");
            rs.MdiParent = this;
            rs.Show();
        }

        private void ملفالتصنيفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Cclass rs = new Cus.Cclass();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ملفتصنيفالموردينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sup.Sclass rs = new Sup.Sclass();
            rs.MdiParent = this;
            rs.Show();
        }

        private void المدنToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Cites rs = new Cus.Cites();
            rs.MdiParent = this;
            rs.Show();
        }

        private void سنداتالقبضToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Tran_Q rs = new Cus.Acc_Tran_Q("","");
            rs.MdiParent = this;
            rs.Show();
        }

        private void قيدعامToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cus.Acc_Tran rs = new Cus.Acc_Tran("", "");
            rs.MdiParent = this;
            rs.Show();
        }

        private void ارصدةالعملاءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Balance rs = new Cus.Acc_Balance();
            rs.MdiParent = this;
            rs.Show();
        }

        private void كشفحسابمراجعةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Statment_Exp f6 = new Cus.Acc_Statment_Exp("");
            f6.MdiParent = this;
            f6.Show();
        }

        private void كشفحسابToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Cus.Acc_Statment_org f6 = new Cus.Acc_Statment_org("");
            f6.MdiParent = this;
            f6.Show();
        }

        private void مراجعةالحركاتToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cus.Acc_Report f6 = new Cus.Acc_Report();
            f6.MdiParent = this;
            f6.Show();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Sup.Suppliers cust = new Sup.Suppliers("");
            cust.MdiParent = this;
            cust.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Sup.Acc_Tran_S rs = new Sup.Acc_Tran_S("", "");
            rs.MdiParent = this;
            rs.Show();
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            Sup.Acc_Tran rs = new Sup.Acc_Tran("", "");
            rs.MdiParent = this;
            rs.Show();
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            Sup.Acc_Report f6 = new Sup.Acc_Report();
            f6.MdiParent = this;
            f6.Show();
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            Sup.Acc_Statment_org f6 = new Sup.Acc_Statment_org("");
            f6.MdiParent = this;
            f6.Show();
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            Sup.Acc_Statment_Exp f6 = new Sup.Acc_Statment_Exp("");
            f6.MdiParent = this;
            f6.Show();
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            Sup.Acc_Balance rs = new Sup.Acc_Balance();
            rs.MdiParent = this;
            rs.Show();
        }

        private void مراجعةحركاتالمبيعتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Report rs = new Sal.Sal_Report();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ملخصالمبيعاتمعالربحToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Report_Sal_WR rs = new Sal.Sal_Report_Sal_WR();
            rs.MdiParent = this;
            rs.Show();
        }

        private void قائمةفواتيرالعملاءمعالربحToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Report_Cus_WR rs = new Sal.Sal_Report_Cus_WR();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ملخصمبيعاتالاصنافToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Sal.Sal_Report_items_WR rs = new Sal.Sal_Report_items_WR();
            rs.MdiParent = this;
            rs.Show();
        }

        private void عرضسعرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.cmp_type.StartsWith("a"))
            {
                Sal.Sal_Ofr_D_TF rs = new Sal.Sal_Ofr_D_TF("", "", "");
                rs.MdiParent = this;
                rs.Show();
            }
            else
            {
                Sal.Sal_Ofr_Tran_D rs = new Sal.Sal_Ofr_Tran_D("", "", "");
                rs.MdiParent = this;
                rs.Show();
            }
        }

        private void عرضسعرخاصToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Sal.Sal_Ofr_Tran_E2 rs = new Sal.Sal_Ofr_Tran_E2("", "", "");
            Sal.Sal_Ord_Tran_D rs = new Sal.Sal_Ord_Tran_D("", "", "");
            rs.MdiParent = this;
            rs.Show();
        }

        private void مراجعةعروضالاسعارToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Ofr_Report rs = new Sal.Sal_Ofr_Report();
            rs.MdiParent = this;
            rs.Show();
        }

        private void MAIN_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            //  if (xo == "Control & q") //You pressed "q" key on the keyboard
            // if (((Control.ModifierKeys & Keys.Control) == Keys.Control) && xo == "A")
            //  if (((Control.ModifierKeys & Keys.Control) == Keys.Control) && ex.KeyChar == 'A')
            //  if (ex.KeyChar ==  (char)Keys.ControlKey && ex.KeyChar == 'Q')
            //if (ex.KeyChar == (char)Keys.ControlKey && ex.KeyChar == (char)Keys.N)
            //{
            //    LOGIN f2 = new LOGIN();
            //    f2.Show();
            //}
            int ox = e.KeyValue;
            if (e.Alt && e.KeyCode == Keys.A)
            {
                LOGIN searchForm = new LOGIN();
                searchForm.Show();
            }
             */
            //int i=1;
            
           // fcount = Application.OpenForms.Count ;
           
            if (e.KeyCode == Keys.F12)
            {
                try
                {
                    int nn = 0;
                    while (nn < this.MdiChildren.Length)
                    {
                        //if (this.MdiChildren[n].Name == FormName)
                        //{
                        //    return n;
                        //}
                        nn++;
                    }
                  //  MessageBox.Show((nn-1).ToString());
                    //  return -1;

                    if (ii <= nn - 1)
                    {
                        // this.ActivateMdiChild(this.MdiChildren[ii]);
                       // MessageBox.Show((ii).ToString());
                        this.MdiChildren[ii].Activate();
                        ii++;
                    }
                    else
                    {
                        ii = 0;
                       // this.MdiChildren[ii].Activate();
                    }
                }
                catch { ii = 0; }
              //  this.MdiChildren[GetMdiChildID("SALES_D")].Activate();
            }

            if (e.KeyCode == Keys.Escape)
            {
                try
                {
                    if (ActiveMdiChild.Tag.Equals("S121") || ActiveMdiChild.Tag.Equals("S122"))
                        MessageBox.Show("لا يمكن الخروج من هذه الشاشة بالطريقة اللتي استخدمتها","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    else
                        this.ActiveMdiChild.Close();
                        
                }
                catch { }
            }

            if (e.KeyCode == Keys.F10)
            {
                var hl = new Abrivs();
                hl.MdiParent = this;
                hl.Show();
            }

            //else
            //{
            //    if (e.KeyCode == Keys.F11 && string.IsNullOrEmpty(this.ActiveMdiChild.Tag.ToString()))
            //    {
            //        this.ActiveMdiChild.Close();
            //    }
            //}
        }
        private int GetMdiChildID(string FormName)
        {
            int n = 0;
            while (n < this.MdiChildren.Length)
            {
                if (this.MdiChildren[n].Name == FormName)
                {
                    return n;
                }
                n++;
            }
            return -1;
        }

        private void مشترياتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pur.Pur_Tran_D rs = new Pur.Pur_Tran_D("", "", "");
            rs.MdiParent = this;
            rs.Show();
        }

        private void مشترياتبدونضريبةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pur.Pur_Tran_notax rs = new Pur.Pur_Tran_notax("", "", "");
            rs.MdiParent = this;
            rs.Show();
        }

        private void طلبيةشراءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pur.Pur_Ord_Tran_D rs = new Pur.Pur_Ord_Tran_D("", "", "");
            rs.MdiParent = this;
            rs.Show();
        }

        private void مراجعةحركاتالمشترياتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pur.Pur_Report rs = new Pur.Pur_Report();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ملخصشراءالاصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pur.Pur_Report_items_WR rs = new Pur.Pur_Report_items_WR();
            rs.MdiParent = this;
            rs.Show();
        }

        private void قائمةفواتيرالموردToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pur.Pur_Report_Sup_WR rs = new Pur.Pur_Report_Sup_WR();
            rs.MdiParent = this;
            rs.Show();
        }

        private void مراجعةحركةالمخازنToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Sto_Report rs = new Sto.Sto_Report();
            rs.MdiParent = this;
            rs.Show();
        }

        private void كشفحركةصنفToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Sto.Sto_ItemStmt_Exp rs = new Sto.Sto_ItemStmt_Exp("");
            rs.MdiParent = this;
            rs.Show();
        }

        private void التقريراليوميToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void الفتراتالضريبيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // Periods
            Acc.Periods rs = new Acc.Periods();
            rs.MdiParent = this;
            rs.Show();
        }

        private void تقريرالاقرارالضريبيToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.VAT rs = new Acc.VAT();
            rs.MdiParent = this;
            rs.Show();
        }

        private void تصفيرتسلسلالطلباتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.Set_Form stf = new Pos.Set_Form();
            stf.MdiParent = this;
            stf.Show();
        }

        private void اعدادالطابعاتالاساسيةToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetMainPrt rs = new SetMainPrt();
            
            rs.MdiParent = this;
            rs.Show();
        }

        private void اعدادالطابعاتالاضافيةToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetAddPrt rs = new SetAddPrt();
            rs.MdiParent = this;
            rs.Show();
        }

        private void قيودوسنداتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnPost_All rs = new UnPost_All("acc");
            rs.MdiParent = this;
            rs.Text = rs.Text + " " + "حسابات";
            rs.Show();
        }

        private void مبيعاتToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UnPost_All rs = new UnPost_All("sal");
            rs.MdiParent = this;
            rs.Text = rs.Text + " " + "مبيعات";
            rs.Show();
        }

        private void مشترياتToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UnPost_All rs = new UnPost_All("pu");
            rs.MdiParent = this;
            rs.Text = rs.Text + " " + "مشتروات";
            rs.Show();
        }

        private void حركةمخزنيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnPost_All rs = new UnPost_All("sto");
            rs.MdiParent = this;
            rs.Text = rs.Text + " " + "مخازن";
            rs.Show();
        }

        private void استيرادالاصنافمناكسلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Import_From_Xls st = new Sto.Import_From_Xls();
            st.MdiParent = this;
            st.Show();
        }

        private void القيودالضريبيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_Tax_Tran act = new Acc.Acc_Tax_Tran("","","");
           // Acc.Acc_Tax_Tran_All act = new Acc.Acc_Tax_Tran_All("", "");
            act.MdiParent = this;
            act.Show();
        }

      

        private void toolStripSplitButton2_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                string salno, typeno, refno;
                /*
                Form activeChild = this.ActiveMdiChild;

                // If there is an active child form, find the active control, which  
                // in this example should be a RichTextBox.  
                if (activeChild != null)
                {
                    try
                    {
                        RichTextBox theBox = (RichTextBox)activeChild.ActiveControl;
                        if (theBox != null)
                        {
                            // Put the selected text on the Clipboard.  
                            Clipboard.SetDataObject(theBox.SelectedText);

                        }
                    }
                    catch
                    {
                        MessageBox.Show("You need to select a RichTextBox.");
                    }
                }
                */
                if (this.ActiveMdiChild is Sto.Sto_ImpExp_Br)
                {
                    // salno = ((Cus.Acc_Tran)this.ActiveMdiChild).cmb_salctr.SelectedValue.ToString();
                  ////  typeno = ((Sto.Sto_ImpExp_Br)this.ActiveMdiChild).cmb_type.SelectedValue.ToString();
                    refno = ((Sto.Sto_ImpExp_Br)this.ActiveMdiChild).txt_jrdacc.Text.ToString();

                    Acc_Tran_All f6 = new Acc_Tran_All("01", refno, "");
                    f6.MdiParent = this;
                    f6.Show();
                }
                if (this.ActiveMdiChild is Cus.Acc_Tran)
                {
                   // salno = ((Cus.Acc_Tran)this.ActiveMdiChild).cmb_salctr.SelectedValue.ToString();
                    typeno = ((Cus.Acc_Tran)this.ActiveMdiChild).comboBox1.SelectedValue.ToString();
                    refno = ((Cus.Acc_Tran)this.ActiveMdiChild).txt_ref.Text.ToString();

                    Acc_Tran_All f6 = new Acc_Tran_All(typeno, refno, "");
                    f6.MdiParent = this;
                    f6.Show();
                }
                if (this.ActiveMdiChild is Cus.Acc_Tran_Q)
                {
                  //  salno = ((Cus.Acc_Tran_Q)this.ActiveMdiChild).cmb_salctr.SelectedValue.ToString();
                    typeno = ((Cus.Acc_Tran_Q)this.ActiveMdiChild).comboBox1.SelectedValue.ToString();
                    refno = ((Cus.Acc_Tran_Q)this.ActiveMdiChild).txt_ref.Text.ToString();

                    Acc_Tran_All f6 = new Acc_Tran_All(typeno, refno, "");
                    f6.MdiParent = this;
                    f6.Show();
                }
                if (this.ActiveMdiChild is Sup.Acc_Tran)
                {
                    // salno = ((Cus.Acc_Tran)this.ActiveMdiChild).cmb_salctr.SelectedValue.ToString();
                    typeno = ((Sup.Acc_Tran)this.ActiveMdiChild).comboBox1.SelectedValue.ToString();
                    refno = ((Sup.Acc_Tran)this.ActiveMdiChild).txt_ref.Text.ToString();

                    Acc_Tran_All f6 = new Acc_Tran_All(typeno, refno, "");
                    f6.MdiParent = this;
                    f6.Show();
                }
                if (this.ActiveMdiChild is Sup.Acc_Tran_S)
                {
                    //  salno = ((Cus.Acc_Tran_Q)this.ActiveMdiChild).cmb_salctr.SelectedValue.ToString();
                    typeno = ((Sup.Acc_Tran_S)this.ActiveMdiChild).comboBox1.SelectedValue.ToString();
                    refno = ((Sup.Acc_Tran_S)this.ActiveMdiChild).txt_ref.Text.ToString();

                    Acc_Tran_All f6 = new Acc_Tran_All(typeno, refno, "");
                    f6.MdiParent = this;
                    f6.Show();
                }
                if (this.ActiveMdiChild is Acc.Acc_Tax_Tran)
                {
                    //  salno = ((Cus.Acc_Tran_Q)this.ActiveMdiChild).cmb_salctr.SelectedValue.ToString();
                    typeno = ((Acc.Acc_Tax_Tran)this.ActiveMdiChild).comboBox1.SelectedValue.ToString();
                    refno = ((Acc.Acc_Tax_Tran)this.ActiveMdiChild).txt_ref.Text.ToString();

                    Acc_Tran_All f6 = new Acc_Tran_All(typeno, refno, "");
                    f6.MdiParent = this;
                    f6.Show();
                }
                if (this.ActiveMdiChild is Sal.Sal_Tran_D)
                {
                    salno = ((Sal.Sal_Tran_D)this.ActiveMdiChild).cmb_salctr.SelectedValue.ToString();
                    typeno = ((Sal.Sal_Tran_D)this.ActiveMdiChild).cmb_type.SelectedValue.ToString();
                    refno = ((Sal.Sal_Tran_D)this.ActiveMdiChild).txt_ref.Text.ToString();

                    Acc_Tran_All f6 = new Acc_Tran_All(typeno, refno, salno );
                    f6.MdiParent = this;
                    // foreach(Control cn in f6.panel1)
                    // {
                    //  f6.panel1.Enabled = false;
                    //  }
                    // f6.btn_Exit.Enabled = true;
                    f6.Show();
                }
                if (this.ActiveMdiChild is Sal.Sal_Tran_D_TF)
                {
                    salno = ((Sal.Sal_Tran_D_TF)this.ActiveMdiChild).cmb_salctr.SelectedValue.ToString();
                    typeno = ((Sal.Sal_Tran_D_TF)this.ActiveMdiChild).cmb_type.SelectedValue.ToString();
                    refno = ((Sal.Sal_Tran_D_TF)this.ActiveMdiChild).txt_ref.Text.ToString();

                    Acc_Tran_All f6 = new Acc_Tran_All(typeno, refno, salno);
                    f6.MdiParent = this;
                    // foreach(Control cn in f6.panel1)
                    // {
                    //  f6.panel1.Enabled = false;
                    //  }
                    // f6.btn_Exit.Enabled = true;
                    f6.Show();
                }
                if (this.ActiveMdiChild is Sal.Sal_Tran_notax)
                {
                    salno = ((Sal.Sal_Tran_notax)this.ActiveMdiChild).cmb_salctr.SelectedValue.ToString();
                    typeno = ((Sal.Sal_Tran_notax)this.ActiveMdiChild).cmb_type.SelectedValue.ToString();
                    refno = ((Sal.Sal_Tran_notax)this.ActiveMdiChild).txt_ref.Text.ToString();

                    Acc_Tran_All f6 = new Acc_Tran_All(typeno, refno, salno);
                    f6.MdiParent = this;
                    f6.Show();
                }

                if (this.ActiveMdiChild is Pur.Pur_Tran_D)
                {
                    salno = ((Pur.Pur_Tran_D)this.ActiveMdiChild).cmb_salctr.SelectedValue.ToString();
                    typeno = ((Pur.Pur_Tran_D)this.ActiveMdiChild).cmb_type.SelectedValue.ToString();
                    refno = ((Pur.Pur_Tran_D)this.ActiveMdiChild).txt_ref.Text.ToString();

                    Acc_Tran_All f6 = new Acc_Tran_All(typeno, refno, salno);
                    f6.MdiParent = this;
                    f6.Show();
                }
                if (this.ActiveMdiChild is Pur.Pur_Tran_notax)
                {
                    salno = ((Pur.Pur_Tran_notax)this.ActiveMdiChild).cmb_salctr.SelectedValue.ToString();
                    typeno = ((Pur.Pur_Tran_notax)this.ActiveMdiChild).cmb_type.SelectedValue.ToString();
                    refno = ((Pur.Pur_Tran_notax)this.ActiveMdiChild).txt_ref.Text.ToString();

                    Acc_Tran_All f6 = new Acc_Tran_All(typeno, refno, salno);
                    f6.MdiParent = this;
                    f6.Show();
                }

                if (this.ActiveMdiChild is Sto.Sto_InOut)
                {
                    //  salno = ((Cus.Acc_Tran_Q)this.ActiveMdiChild).cmb_salctr.SelectedValue.ToString();
                    typeno = ((Sto.Sto_InOut)this.ActiveMdiChild).cmb_type.SelectedValue.ToString();
                    refno = ((Sto.Sto_InOut)this.ActiveMdiChild).txt_ref.Text.ToString();

                    DataTable dt = daml.CMD_SELECT_QUIRY_only_retrn_dt("select stkjvno from sto_hdr where trtype='" + typeno + "' and ref=" + refno + " and branch='" + BL.CLS_Session.brno + "'");
                    double dtr=Convert.ToInt32(dt.Rows[0][0].ToString());
                    if (dtr > 0)
                    {
                        Acc_Tran_All f6 = new Acc_Tran_All("01", dtr.ToString(), "");
                        f6.MdiParent = this;
                        f6.Show();
                    }
                    else
                    {
                        Acc_Tran_All f6 = new Acc_Tran_All(typeno, refno, "");
                        f6.MdiParent = this;
                        f6.Show();
                    }
                }

                
            //////////notifyIcon1.Visible = true;
            //////////notifyIcon1.Icon = SystemIcons.Exclamation;
            //////////notifyIcon1.BalloonTipTitle = "تنويه";
            //////////notifyIcon1.BalloonTipText = "يعرض القيد المحاسبي للحركة اللتي بالشاشة";
            //////////notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            //////////notifyIcon1.ShowBalloonTip(1000);
               
            }
            catch { }
        }

        private void ابتداءالجردToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.GRD_Start rs = new Sto.GRD_Start();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ادخالنتائجالجردمنملفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.GRD_frm_file rs = new Sto.GRD_frm_file();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ادخالنتائجالجردToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Sto.GRD_Enter rs = new Sto.GRD_Enter();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ملفاتToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void طباعةفروقاتالجردToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  GRD_Result_Print
            Sto.GRD_Result_Print rs = new Sto.GRD_Result_Print();
            rs.MdiParent = this;
            rs.Show();
        }

        private void اعتمادالجردToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.GRD_End rs = new Sto.GRD_End();
            rs.MdiParent = this;
            rs.Show();
        }

        private void قيودالاقفالToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_Close rs = new Acc.Acc_Close();
            rs.MdiParent = this;
            rs.Show();
        }

        private void تعديلسعرصنفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Price_Chang rs = new Sto.Price_Chang();
            rs.MdiParent = this;
            rs.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.cmp_type.Equals("m"))
            {
                if (BL.CLS_Session.formkey.Contains("A124"))
                {
                    Acc.Acc_Tax_Tran2 f6 = new Acc.Acc_Tax_Tran2("", "", "");
                    f6.MdiParent = this;
                    f6.Show();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            notifyIcon1.Visible = true;
            notifyIcon1.Icon = SystemIcons.Exclamation;
            notifyIcon1.BalloonTipTitle = "Balloon Tip Title";
            notifyIcon1.BalloonTipText = "Balloon Tip Text.";
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.ShowBalloonTip(1000);
             * */
            if (BL.CLS_Session.cmp_type.Equals("m"))
            {
                if (BL.CLS_Session.formkey.Contains("A133"))
                {
                    Acc.Acc_Statment_Exp f6 = new Acc.Acc_Statment_Exp("");
                    f6.MdiParent = this;
                    f6.Show();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.formkey.Contains("S121"))
            {
                if (BL.CLS_Session.istuch)
                {
                    Pos.SALES_D_TCH sales = new Pos.SALES_D_TCH();
                    // SALES sales = new SALES();
                    sales.MdiParent = this;
                    sales.Show();
                }
                else
                {
                    Pos.SALES_D_TCH sales = new Pos.SALES_D_TCH();
                    // SALES sales = new SALES();
                    sales.MdiParent = this;
                    sales.Show();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
           

            if (BL.CLS_Session.formkey.Contains("S122"))
            {
                if (BL.CLS_Session.istuch)
                {
                    Pos.RESALES_D_TCH sales = new Pos.RESALES_D_TCH();
                    // SALES sales = new SALES();
                    sales.MdiParent = this;
                    sales.Show();
                }
                else
                {
                    Pos.RESALES_D_TCH sales = new Pos.RESALES_D_TCH();
                    // SALES sales = new SALES();
                    sales.MdiParent = this;
                    sales.Show();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.formkey.Contains("E11A") && BL.CLS_Session.cmp_type.Equals("m"))
            {
                Sto.Price_Chang rs = new Sto.Price_Chang();
                rs.MdiParent = this;
                rs.Show();
            }
        }

        private void مراجعةالحركاتالضريبيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_Tax_Report f6 = new Acc.Acc_Tax_Report();
            f6.MdiParent = this;
            f6.Show();
        }

        private void ملفالبائعينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.Pos_Sal_Men f6 = new Pos.Pos_Sal_Men();
            f6.MdiParent = this;
            f6.Show();
        }

        private void كشفحسابسنواتسابقةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Statment_All f6 = new Cus.Acc_Statment_All("");
            f6.MdiParent = this;
            f6.Show();
        }

        private void كشفحسابسنواتسابقةToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Sup.Acc_Statment_All f6 = new Sup.Acc_Statment_All("");
            f6.MdiParent = this;
            f6.Show();
        }

        private void كشفحسابسنواتسابقةToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Acc.Acc_Statment_All f6 = new Acc.Acc_Statment_All("");
            f6.MdiParent = this;
            f6.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Server_Setting st = new Server_Setting();
            //st.button2_Click( sender, e);
            Pos.Items_Update iu = new Pos.Items_Update();
            iu.MdiParent = this;
            iu.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Server_Setting st = new Server_Setting();
           // st.button4_Click( sender,  e);
        }

        private void بناءارصدةالاصنافوالتكاليفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Items_Bld iu = new Sto.Items_Bld();
            iu.MdiParent = this;
            iu.Show();
        }

        private void قيمةالمخزونالافتتاحيToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Sto_Whbins_o fr = new Sto.Sto_Whbins_o();
            fr.MdiParent = this;
            fr.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.formkey.Contains("B121") && !BL.CLS_Session.sysno.Equals("ac"))
            {
                if (BL.CLS_Session.cmp_type.StartsWith("a"))
                {
                    Sal.Sal_Tran_D_TF rs = new Sal.Sal_Tran_D_TF("", "", "");
                    rs.MdiParent = this;
                    rs.Show();
                }
                else
                {
                    Sal.Sal_Tran_D rs = new Sal.Sal_Tran_D("", "", "");
                    rs.MdiParent = this;
                    rs.Show();
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.formkey.Contains("P121") && !BL.CLS_Session.sysno.Equals("ac"))
            {
                Pur.Pur_Tran_D f6 = new Pur.Pur_Tran_D("", "", "");
                f6.MdiParent = this;
                f6.Show();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.formkey.Contains("C133") && !BL.CLS_Session.sysno.Equals("ac"))
            {
                if (this.ActiveMdiChild is Sal.Sal_Tran_D)
                {
                    Cus.Acc_Statment_Exp f6 = new Cus.Acc_Statment_Exp(((Sal.Sal_Tran_D)this.ActiveMdiChild).txt_custno.Text);
                    f6.MdiParent = this;
                    f6.Show();
                }
                else
                {
                    if (this.ActiveMdiChild is Sal.Sal_Tran_notax)
                    {
                        Cus.Acc_Statment_Exp f6 = new Cus.Acc_Statment_Exp(((Sal.Sal_Tran_notax)this.ActiveMdiChild).txt_custno.Text);
                        f6.MdiParent = this;
                        f6.Show();
                    }
                    else
                    {
                        Cus.Acc_Statment_Exp f6 = new Cus.Acc_Statment_Exp("");
                        f6.MdiParent = this;
                        f6.Show();
                    }
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.formkey.Contains("D133") && !BL.CLS_Session.sysno.Equals("ac"))
            {
                if (this.ActiveMdiChild is Pur.Pur_Tran_D)
                {
                    Sup.Acc_Statment_Exp f6 = new Sup.Acc_Statment_Exp(((Pur.Pur_Tran_D)this.ActiveMdiChild).txt_custno.Text);
                    f6.MdiParent = this;
                    f6.Show();
                }
                else
                {
                    if (this.ActiveMdiChild is Pur.Pur_Tran_notax)
                    {
                        Sup.Acc_Statment_Exp f6 = new Sup.Acc_Statment_Exp(((Pur.Pur_Tran_notax)this.ActiveMdiChild).txt_custno.Text);
                        f6.MdiParent = this;
                        f6.Show();
                    }
                    else
                    {
                        if (this.ActiveMdiChild is Pur.Pur_Tran_D_WI)
                        {
                            Sup.Acc_Statment_Exp f6 = new Sup.Acc_Statment_Exp(((Pur.Pur_Tran_D_WI)this.ActiveMdiChild).txt_custno.Text);
                            f6.MdiParent = this;
                            f6.Show();
                        }
                        else
                        {
                            Sup.Acc_Statment_Exp f6 = new Sup.Acc_Statment_Exp("");
                            f6.MdiParent = this;
                            f6.Show();
                        }
                    }
                }
                //Sup.Acc_Statment_Exp f6 = new Sup.Acc_Statment_Exp("");
                //f6.MdiParent = this;
                //f6.Show();
            }
        }

        private void قائمةالدخلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_QD_Report f6 = new Acc.Acc_QD_Report();
            f6.MdiParent = this;
            f6.Show();
        }

        private void اخفاءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (panel1.Visible == true)
            {
                panel1.Visible = false;
            }

            if (panel2.Visible == true)
            {
                panel2.Visible = false;
            }
        }

        private void ارصدةالعملاءواخردفعةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Balance_WLP rs = new Cus.Acc_Balance_WLP();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ارصدةالموردينواخردفعةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sup.Acc_Balance_WLP rs = new Sup.Acc_Balance_WLP();
            rs.MdiParent = this;
            rs.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process p =  System.Diagnostics.Process.Start("calc.exe");
            //p.WaitForInputIdle();
            //NativeWindow
           // Environment.SystemDirectory.ToUpper().Replace(@"\SYSTEM32", "") + @"\PosKey.txt";
           //  string progFiles = @"C:\Windows\System32";
            string progFiles = Environment.SystemDirectory;
            // string keyboardPath = Path.Combine(progFiles, "TabTip.exe");
            
            string calculater = Path.Combine(progFiles, "calc.exe");
            Process.Start(calculater);

        }

        private void button15_Click(object sender, EventArgs e)
        {
           // string progFiles = @"C:\Windows\System32";
            string progFiles = Environment.SystemDirectory;
            // string keyboardPath = Path.Combine(progFiles, "TabTip.exe");

            string calculater = Path.Combine(progFiles, "calc.exe");
            Process.Start(calculater);
        }

        private void مراكزالتكلفةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.CostCnters acc = new Acc.CostCnters();
            acc.MdiParent = this;
            acc.Show();
        }

        private void كشفحسابمراكزالتكلفةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.CC_Statment_Exp f6 = new Acc.CC_Statment_Exp("");
            f6.MdiParent = this;
            f6.Show();
        }

        private void ارصدةمراكزالتكلفةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.CC_Balance f6 = new Acc.CC_Balance();
            f6.MdiParent = this;
            f6.Show();
        }

        private void قائمةدخلمراكزالتكلفةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.CC_Final_Report f6 = new Acc.CC_Final_Report();
            f6.MdiParent = this;
            f6.Show();
        }

        private void مراجعةطلبياتالشراءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pur.Pur_Ofr_Report rs = new Pur.Pur_Ofr_Report();
            rs.MdiParent = this;
            rs.Show();
        }

        private void مندوبيالبيعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Salmen rs = new Sal.Salmen();
            rs.MdiParent = this;
            rs.Show();
        }

        private void مناديبالتحصيلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Colmen rs = new Cus.Colmen();
            rs.MdiParent = this;
            rs.Show();
        }

        private void عمولاتمندوبيالتحصيلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Report_col rs = new Cus.Acc_Report_col();
            rs.MdiParent = this;
            rs.Show();

        }

        private void عمولاتمندوبيالبيعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Report_sp rs = new Sal.Sal_Report_sp();
            rs.MdiParent = this;
            rs.Show();
        }

        private void نقلارصدةمنسنةماضيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_Copy_Olddb rs = new Acc.Acc_Copy_Olddb();
            rs.MdiParent = this;
            rs.Show();
        }

        private void كشفحركةصنفسنواتسابقةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Sto_ItemStmt_All rs = new Sto.Sto_ItemStmt_All("");
            rs.MdiParent = this;
            rs.Show();
        }

        private void العملاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Crncy rs = new Acc.Crncy();
            rs.MdiParent = this;
            rs.Show();
        }

        private void قيدعامعملاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sup.Acc_Tran_Cur rs = new Sup.Acc_Tran_Cur("", "");
            rs.MdiParent = this;
            rs.Show();
        }

        private void ملخصالمبيعاتاليوميةوالشهريةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Report_DM rs = new Sal.Sal_Report_DM();
            rs.MdiParent = this;
            rs.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.cmp_type.Equals("m") && !BL.CLS_Session.sysno.Equals("ac"))
            {
                if (BL.CLS_Session.formkey.Contains("E111"))
                {
                    Sto.Item_Card it = new Sto.Item_Card("");
                    it.MdiParent = this;
                    it.Show();
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Notify it = new Notify();
            it.MdiParent = this;
            it.Show();
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            Notify it = new Notify();
            it.MdiParent = this;
            it.Show();
            notifyIcon1.Visible = false;
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
           
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Notify it = new Notify();
            it.MdiParent = this;
            it.Show();
            notifyIcon1.Visible = false;
        }

        private void قيودعامةوسنداتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Post_All rs = new Post_All("acc");
            rs.MdiParent = this;
            rs.Text = rs.Text + " " + "الحسابات";
            rs.Show();
        }

        private void حركاتالمبيعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Post_All rs = new Post_All("sal");
            rs.MdiParent = this;
            rs.Text = rs.Text + " " + "المبيعات";
            rs.Show();
        }

        private void حركاتالمشترياتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Post_All rs = new Post_All("pu");
            rs.MdiParent = this;
            rs.Text = rs.Text + " " + "المشتروات";
            rs.Show();
        }

        private void الحركاتالمخزنيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Post_All rs = new Post_All("sto");
            rs.MdiParent = this;
            rs.Text = rs.Text + " " + "المخازن";
            rs.Show();
        }

        private void قائمةفواتيرصنفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.MenuSalesItemReport rs = new Pos.MenuSalesItemReport();
            rs.MdiParent = this;
            rs.Show();
        }

        private void المبيعاتاليوميةوالشهريةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.PosSal_Report_DM rs = new Pos.PosSal_Report_DM();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ملخصالكاشيراتوالبائعينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.SumSalesItemReport rs = new Pos.SumSalesItemReport();
            rs.MdiParent = this;
            rs.Show();
        }

        private void الجملالشائعةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pubtext rs = new Pubtext();
            rs.MdiParent = this;
            rs.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            backup_db();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            backup_db();
        }
        private void backup_db()
        {

            if (!Directory.Exists(BL.CLS_Session.bkpdir))
                Directory.CreateDirectory(BL.CLS_Session.bkpdir);
            
            DialogResult result = MessageBox.Show("Do you want to backup now هل تريد اخذ نسخة الان", "Alert تنبيه", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
            {
                if (File.Exists(Environment.CurrentDirectory + @"\local.txt"))
                {
                    WaitForm wf = new WaitForm("جاري عمل نسخة احتياطية .. يرجى الانتظار ...");
                    wf.MdiParent = this;
                    wf.Show();
                    Application.DoEvents();
                    // DoWelcome();

                    // string fulbath = "";
                    try
                    {
                        string sql, database;
                        SqlCommand command;

                        database = "[" + Directory.GetCurrentDirectory() + @"\DB\" + BL.CLS_Session.sqldb + ".mdf]";
                        // MessageBox.Show(database);
                        if (con3.State == ConnectionState.Closed)
                            con3.Open();
                        //con.Open();
                        string baktime = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);

                        //sql = "BACKUP DATABASE " + cmbDb.Text + " TO DISK = '" + txtLocation.Text + "\\" + cmbDb.Text + "." + DateTime.Now.Ticks.ToString() + ".bak'";
                        sql = "BACKUP DATABASE " + database + " TO DISK = '" + BL.CLS_Session.bkpdir + "\\" + "Manual_" + BL.CLS_Session.sqldb + "_" + baktime + ".bak';";
                        command = new SqlCommand(sql, con3);
                        command.CommandTimeout = 0;
                        command.ExecuteNonQuery();
                        con3.Close();
                        //  con.Dispose();
                        wf.Close();
                        MessageBox.Show(this, " successful backup complated  تم النسخ بنجاح " + "\n" + BL.CLS_Session.bkpdir + "\\" + "Manual_" + BL.CLS_Session.sqldb + "_" + baktime + ".bak", " Note تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        wf.Close();
                    }
                    finally
                    {
                        con3.Close();

                    }
                }
                else
                {
                    WaitForm wf = new WaitForm("جاري عمل نسخة احتياطية .. يرجى الانتظار ...");
                    wf.MdiParent = this;
                    wf.Show();
                    Application.DoEvents();
                    // DoWelcome();

                    // string fulbath = "";
                    SqlCommand myCommand = new SqlCommand();
                    //1 SqlDataReader myReader;

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Connection = con3;
                    myCommand.CommandTimeout = 0;
                    myCommand.CommandText = "dbmstr.dbo.backup_db";
                    //2 myCommand.Parameters.Add("@NO_items_updated", OleDbType.Integer);

                    myCommand.Parameters.AddWithValue("@dbname", BL.CLS_Session.sqldb);
                    myCommand.Parameters.AddWithValue("@bkppath", BL.CLS_Session.bkpdir);
                    // myCommand.Parameters.Add("@NO_items_updated", 0);
                   // myCommand.Parameters.AddWithValue("@my_pass", "Sa@654321");

                    myCommand.Parameters.Add("@file_name_crtd", SqlDbType.VarChar, 256);
                    myCommand.Parameters.Add("@errstatus", 0);
                    //  myCommand.Parameters.AddWithValue("@sl_man", chk_salman.Checked ? 1 : 0);
                    //  myCommand.Parameters.AddWithValue("@br_no", BL.CLS_Session.brno);

                    myCommand.Parameters["@file_name_crtd"].Direction = ParameterDirection.Output;
                    myCommand.Parameters["@errstatus"].Direction = ParameterDirection.Output;
                    try
                    {
                        if (con3.State == ConnectionState.Closed)
                            con3.Open();

                        //3  myReader = myCommand.ExecuteReader();
                        myCommand.ExecuteNonQuery();
                        //Uncomment this line to return the proper output value.
                        con3.Close();
                        wf.Close();
                        MessageBox.Show("Backup Done -- تم النسخ : \n\n" + myCommand.Parameters["@file_name_crtd"].Value.ToString(), "Alert تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        // MessageBox.Show("Return Value : " + myCommand.Parameters["@errstatus"].Value);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        wf.Close();
                    }
                    finally
                    {
                        con3.Close();

                    }
                }
                // Backup bk = new Backup();
                // bk.MdiParent = this;
                // bk.button1.Visible = false;
                // bk.Show();
                // bk.button1_Click(sender, e);

                // // daml.Exec_Query_only("update tobld set id=1");

                //// Application.Exit();
                // // bk.MdiParent = this;
                // // bk.Show();
                // // Application.Exit();
                // bk.Close();
            }
            else if (result == DialogResult.No)
            {
                // if (this.ActiveMdiChild != null)
                // int fcont = Application.OpenForms.Count;


                //  this.ActiveMdiChild.Close();
                //  daml.Exec_Query_only("update tobld set id=1");
                // Application.Exit();

                //...
            }
            else
            {
                //...
            }
            //   Application.Exit();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.cmp_type.Equals("m"))
            {
                button16_Click(sender, e);
            }
        }

        private void كشفحسابمعالعمرالزمنيToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Statment_Exp_Life f6 = new Cus.Acc_Statment_Exp_Life("");
            f6.MdiParent = this;
            f6.Show();
        }

        private void تعميرديونالعملاءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Balances_Life f6 = new Cus.Acc_Balances_Life("");
            f6.MdiParent = this;
            f6.Show();
        }

        private void تركيباوانتاجصنفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Sto_item_Combine sto = new Sto.Sto_item_Combine("","","");
            sto.MdiParent = this;
            sto.Show();
        }

        private void ارصدةالعملاءمعالمبالغالمستحقةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Balance_WLF rs = new Cus.Acc_Balance_WLF();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ارصدةالموردينمعالمبالغالمستحقةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sup.Acc_Balance_WLF rs = new Sup.Acc_Balance_WLF();
            rs.MdiParent = this;
            rs.Show();
        }

        private void teamViwerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"TV.exe");
        }

        private void anyDeskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(File.Exists(@"C:\Program Files (x86)\AnyDesk\AnyDesk.exe"))
              System.Diagnostics.Process.Start(@"C:\Program Files (x86)\AnyDesk\AnyDesk.exe");
            else
              System.Diagnostics.Process.Start(@"AD.exe");
        }

        private void ammyyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"AA.exe");
        }

        private void تفصيلحركاتالحساباتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_Report_Dtl f6 = new Acc.Acc_Report_Dtl();
            f6.MdiParent = this;
            f6.Show();
        }

        private void العروضوالتخفيضاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Items_Offer rs = new Sal.Sal_Items_Offer();
            rs.MdiParent = this;
            rs.Show();
        }

        private void جلبالمبيعاتمنالفروعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.Import_inv_From_Pos rs = new Pos.Import_inv_From_Pos();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ملخصحركاتالعملاءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Balance_Sumry rs = new Cus.Acc_Balance_Sumry();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ملخصحركــاتالمــوردينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sup.Acc_Balance_Sumry rs = new Sup.Acc_Balance_Sumry();
            rs.MdiParent = this;
            rs.Show();
        }

        private void سنداتاستلاموصرفبينالفروعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Sto_ImpExp_Br rr = new Sto.Sto_ImpExp_Br("", "", "");
            rr.MdiParent = this;
            rr.Show();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            taskbarNotifier1.CloseClickable = true;// checkBoxCloseClickable.Checked;
            taskbarNotifier1.TitleClickable = false; // checkBoxTitleClickable.Checked;
            taskbarNotifier1.ContentClickable = true;// checkBoxContentClickable.Checked;
            taskbarNotifier1.EnableSelectionRectangle = false;// checkBoxSelectionRectangle.Checked;
            taskbarNotifier1.KeepVisibleOnMousOver = true;// checkBoxKeepVisibleOnMouseOver.Checked;	// Added Rev 002
            taskbarNotifier1.ReShowOnMouseOver = false;// checkBoxReShowOnMouseOver.Checked;			// Added Rev 002
            taskbarNotifier1.Show("مرحبا", "عزيزي العميل شكرا لاستخدامك برنامج تريسوفت النظام المحاسبي المميز ", 500, 3000, 500);
        }

        private void مراجعةسنداتالصرفوالاستلامبينالفروعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Sto_Report_Br rs = new Sto.Sto_Report_Br();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ملخصمبيعاتالفروعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Report_Br rs = new Sal.Sal_Report_Br();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ملخصمبيعاتالمجموعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Report_Gr rs = new Sal.Sal_Report_Gr();
            rs.MdiParent = this;
            rs.Show();
           
        }

        private void ملخصمبيعاتالعمـــلاءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Report_Cs rs = new Sal.Sal_Report_Cs();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ملخصمشترياتالموردينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pur.Pur_Report_Su rs = new Pur.Pur_Report_Su();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ملخصمشترياتالفــــروعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pur.Pur_Report_Br rs = new Pur.Pur_Report_Br();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ملخصمشترياتالمجموعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pur.Pur_Report_Gr rs = new Pur.Pur_Report_Gr();
            rs.MdiParent = this;
            rs.Show();
        }

        private void قيمةوارصدةمخزونالمجموعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Sto_Whbins_Gr rs = new Sto.Sto_Whbins_Gr();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ارصدةوقيمةمخزونالفروعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Sto_Whbins_Br rs = new Sto.Sto_Whbins_Br();
            rs.MdiParent = this;
            rs.Show();
        }

        private void MAIN_Shown(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false))) >= Convert.ToDouble(encd.Decrypt(BL.CLS_Session.minlastlgin, true)))
                {
                   daml.Exec_Query_only("update dbmstr.dbo.companys set min_lastlgin='" + encd.Encrypt(DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false)), true) + "' where comp_id='" + BL.CLS_Session.cmp_id + "'");
                }
                else
                {
                    MessageBox.Show(" يرجى التاكد من تاريخ الجهاز ", " خطا بالتاريخ ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    //this.Close();
                }

                // MessageBox.Show(BL.CLS_Session.dtcomp.Rows[0]["cuntry"].ToString());
                Thread ab2 = new Thread(() => thread_get_ts_msg());// new Thread(new ThreadStart(tc.thread1(cmb_type.SelectedValue.ToString(), txt_ref.Text.ToString(), cmb_salctr.SelectedValue.ToString())));
                ab2.Start();

                //  timer1.Start();
              //  if (BL.CLS_Session.autobak && !BL.CLS_Session.islight)
                 if(BL.CLS_Session.autobak)
                    bak_timer.Start();

            if (BL.CLS_Session.einv_p2_syncactv)
                sync_timer.Start();

                timer_update.Start();
                // daml.SqlCon_Close();
            }
            catch { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            taskbarNotifier1 = new TaskbarNotifier();
           // taskbarNotifier1.MdiParent = this;
            taskbarNotifier1.SetBackgroundBitmap(new Bitmap(Properties.Resources.skin), Color.FromArgb(255, 0, 255));
            taskbarNotifier1.SetCloseBitmap(new Bitmap(Properties.Resources.close), Color.FromArgb(255, 0, 255), new Point(179, 8));
            taskbarNotifier1.TitleRectangle = new Rectangle(50, 9, 100, 25);
            taskbarNotifier1.ContentRectangle = new Rectangle(8, 41, 180, 68);
            // taskbarNotifier1.TitleClick += new EventHandler(TitleClick);
            taskbarNotifier1.ContentClick += new EventHandler(ContentClick);
            // taskbarNotifier1.CloseClick += new EventHandler(CloseClick);

            taskbarNotifier1.CloseClickable = true;// checkBoxCloseClickable.Checked;
            taskbarNotifier1.TitleClickable = false; // checkBoxTitleClickable.Checked;
            taskbarNotifier1.ContentClickable = true;// checkBoxContentClickable.Checked;
            taskbarNotifier1.EnableSelectionRectangle = false;// checkBoxSelectionRectangle.Checked;
            taskbarNotifier1.KeepVisibleOnMousOver = true;// checkBoxKeepVisibleOnMouseOver.Checked;	// Added Rev 002
            taskbarNotifier1.ReShowOnMouseOver = true;// checkBoxReShowOnMouseOver.Checked;			// Added Rev 002
            taskbarNotifier1.Show(BL.CLS_Session.UserName + (BL.CLS_Session.lang.Equals("E")?" Welcome " : " مرحبا "),BL.CLS_Session.lang.Equals("E")?" Thank you for selecting our app." : " عزيزي العميل شكرا لاستخدامك برنامج تريسوفت .. كما نرحب بكل مقترحاتكم بالنقر على هذه الرسالة ", 500, 3000, 2000);
           // taskbarNotifier1.MdiParent = null;
            timer1.Stop();
        }

        private void ارصدةالاصناففيالمخازنToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Sto_Whbins_Wh rs = new Sto.Sto_Whbins_Wh();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ارصدةالاصناففيالفروعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Sto_Whbins_Brb rs = new Sto.Sto_Whbins_Brb();
            rs.MdiParent = this;
            rs.Show();
        }

        private void ملخصالمشترياتمعسعرالبيعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pur.Pur_Report_Sal_WR rs = new Pur.Pur_Report_Sal_WR();
            rs.MdiParent = this;
            rs.Show();
        }

        private void كشفحسابعميلبالرقمالموحدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Statment_org_U f6 = new Cus.Acc_Statment_org_U("");
            f6.MdiParent = this;
            f6.Show();
        }

        private void تكوينالمبيعاتوالقيودالمحاسبيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.Pos_Sal_Cmbin f6 = new Pos.Pos_Sal_Cmbin();
            f6.MdiParent = this;
            f6.Show();
        }

        private void كشفحسابموردبالرقمالموحدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sup.Acc_Statment_org_SU f6 = new Sup.Acc_Statment_org_SU("");
            f6.MdiParent = this;
            f6.Show();
        }

        private void تفصيلحركاتالعملاءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Report_Dtl f6 = new Cus.Acc_Report_Dtl();
            f6.MdiParent = this;
            f6.Show();
        }

        private void تفصيلحركاتالموردينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sup.Acc_Report_Dtl f6 = new Sup.Acc_Report_Dtl();
            f6.MdiParent = this;
            f6.Show();
        }

        private void تفصيلحركاتالمبيعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Report_Dtl f6 = new Sal.Sal_Report_Dtl();
            f6.MdiParent = this;
            f6.Show();
        }

        private void تفصيلحركاتالمشترياتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pur.Pur_Report_Dtl f6 = new Pur.Pur_Report_Dtl();
            f6.MdiParent = this;
            f6.Show();
        }

        private void الاصنافالراكدةوالمتحركةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Sto_Whbins_RM fr = new Sto.Sto_Whbins_RM();
            fr.MdiParent = this;
            fr.Show();
        }

        private void الاصنافالاكثروالاقلبيعاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Report_items_Sal_Mor_No fr = new Sal.Sal_Report_items_Sal_Mor_No();
            fr.MdiParent = this;
            fr.Show();
        }

        private void مشترياتمعالاصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pur.Pur_Tran_D_WI rs = new Pur.Pur_Tran_D_WI("", "", "");
            rs.MdiParent = this;
            rs.Show();
        }

        private void الموازناتالتقديريةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_bdgt_report f6 = new Acc.Acc_bdgt_report();
            f6.MdiParent = this;
            f6.Show();
        }

        private void التدفقاتالنقديةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_cashinout_report f6 = new Acc.Acc_cashinout_report();
            f6.MdiParent = this;
            f6.Show();
        }

        private void ميزانمراجعةحركاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_Mizan_Tran f6 = new Acc.Acc_Mizan_Tran();
            f6.MdiParent = this;
            f6.Show();
        }

        private void طباعةالباركودToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Sto.Print_Barcode bp = new Sto.Print_Barcode();
            //bp.ShowDialog();
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            if (!BL.CLS_Session.sysno.Equals("ac"))
            {
                Sto.Print_Barcode_FR bp = new Sto.Print_Barcode_FR("");
                bp.MdiParent = this;
                bp.Show();
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Sto.Print_Barcode_FR bp = new Sto.Print_Barcode_FR("");
            bp.MdiParent = this;
            bp.Show();
        }

        private void تفصيلحركةالمخازنToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Sto_Report_Dtl f6 = new Sto.Sto_Report_Dtl();
            f6.MdiParent = this;
            f6.Show();
        }

        private void تفصلالحركاتبينالفروعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Sto_Report_Br_Dtl f6 = new Sto.Sto_Report_Br_Dtl();
            f6.MdiParent = this;
            f6.Show();
        }

        private void مراجعةحركاتالمبيعاتوضرايبهاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Tax_Report rs = new Sal.Sal_Tax_Report();
            rs.MdiParent = this;
            rs.Show();
        }

        private void مراجعةحركاتالمشترياتوضرايبهاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pur.Pur_Tax_Report rs = new Pur.Pur_Tax_Report();
            rs.MdiParent = this;
            rs.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void ملخصمبيعاتاصنافالموردToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Report_SupItems_WR rs = new Sal.Sal_Report_SupItems_WR();
            rs.MdiParent = this;
            rs.Show();
        }

        private void تسويةتكلفةصنفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Sto_Cost_Tran rr = new Sto.Sto_Cost_Tran("", "", "");
            rr.MdiParent = this;
            rr.Show();
        }

        private void المخرجينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Trans rr = new Sal.Trans();
            rr.MdiParent = this;
            rr.Show();
        }

        private void bak_timer_Tick(object sender, EventArgs e)
        {
          //  MessageBox.Show(DateTime.Now.Hour.ToString());
            if (Convert.ToInt32(BL.CLS_Session.autobaktime) == Convert.ToInt32(DateTime.Now.Hour.ToString()) && (BL.CLS_Session.sqlserver.StartsWith(".") || BL.CLS_Session.sqlserver.StartsWith(Environment.MachineName.ToString()) || BL.CLS_Session.sqlserver.StartsWith("localhost") || BL.CLS_Session.sqlserver.StartsWith("127.0.0.1")))
            {
                Thread ab = new Thread(() => thread_bakup());// new Thread(new ThreadStart(tc.thread1(cmb_type.SelectedValue.ToString(), txt_ref.Text.ToString(), cmb_salctr.SelectedValue.ToString())));
                ab.Start();
               
            }

           
           
        }
        public void thread_synceinv(string auto)
        {
            try
            {
                sync_timer.Stop();
                Pos.SalesReport_ToSend sbc = new Pos.SalesReport_ToSend(auto);
                sbc.button7_Click(null, null);
                Thread.Sleep(4000000);
                sync_timer.Start();
            }
            catch { }

        }

        public void thread_bakup()
        {
            if (!File.Exists(BL.CLS_Session.bkpdir + "Auto_Bak_" + BL.CLS_Session.sqldb + "_" + DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false)) + ".bak" ))
            {
                bak_timer.Stop();
                SqlConnection con4 = BL.DAML.con;
                try
                {
                    if (!Directory.Exists(BL.CLS_Session.bkpdir))
                        Directory.CreateDirectory(BL.CLS_Session.bkpdir);
                    //  MessageBox.Show(BL.CLS_Session.bkpdir + "Auto_Bak_" + BL.CLS_Session.sqldb + "_" + DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false)) + ".bak");


                   
                    //1 SqlDataReader myReader;
                    if (!File.Exists(Environment.CurrentDirectory + @"\local.txt"))
                    {
                        SqlCommand myCommand = new SqlCommand();
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.Connection = con4;
                        myCommand.CommandTimeout = 0;
                        myCommand.CommandText = "dbmstr.dbo.Auto_backup_db";
                        //2 myCommand.Parameters.Add("@NO_items_updated", OleDbType.Integer);

                        myCommand.Parameters.AddWithValue("@dbname", BL.CLS_Session.sqldb);
                        myCommand.Parameters.AddWithValue("@bkppath", BL.CLS_Session.bkpdir);
                        // myCommand.Parameters.Add("@NO_items_updated", 0);

                        myCommand.Parameters.Add("@file_name_crtd", SqlDbType.VarChar, 256);
                        myCommand.Parameters.Add("@errstatus", 0);
                        //  myCommand.Parameters.AddWithValue("@sl_man", chk_salman.Checked ? 1 : 0);
                        //  myCommand.Parameters.AddWithValue("@br_no", BL.CLS_Session.brno);

                        myCommand.Parameters["@file_name_crtd"].Direction = ParameterDirection.Output;
                        myCommand.Parameters["@errstatus"].Direction = ParameterDirection.Output;

                        if (con4.State == ConnectionState.Closed)
                            con4.Open();

                        //3  myReader = myCommand.ExecuteReader();
                        myCommand.ExecuteNonQuery();
                        con4.Close();
                        //Uncomment this line to return the proper output value.
                        //myReader.Close();
                        // wf.Close();
                        //// MessageBox.Show("Backup Done -- تم النسخ : \n\n" + myCommand.Parameters["@file_name_crtd"].Value.ToString(), "Alert تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        // MessageBox.Show("Return Value : " + myCommand.Parameters["@errstatus"].Value);

                    }
                    else
                    {
                        string sql, database;
                        SqlCommand command;

                        database = "[" + Directory.GetCurrentDirectory() + @"\DB\" + BL.CLS_Session.sqldb + ".mdf]";
                        // MessageBox.Show(database);
                       // if (con3.State == ConnectionState.Closed)
                        //    con3.Open();
                        //con.Open();
                        string baktime = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);

                        //sql = "BACKUP DATABASE " + cmbDb.Text + " TO DISK = '" + txtLocation.Text + "\\" + cmbDb.Text + "." + DateTime.Now.Ticks.ToString() + ".bak'";
                        sql = "BACKUP DATABASE " + database + " TO DISK = '" + BL.CLS_Session.bkpdir + "\\" + "Auto_" + BL.CLS_Session.sqldb + "_" + baktime + ".bak'";
                        command = new SqlCommand(sql, con4);
                        command.CommandTimeout = 0;
                        if (con4.State == ConnectionState.Closed)
                            con4.Open();
                        command.ExecuteNonQuery();
                        con4.Close();
                        //  con.Dispose();
                      
                      //  MessageBox.Show(this, " successful backup complated  تم النسخ بنجاح " + "\n" + BL.CLS_Session.bkpdir + "\\" + "Manual_" + BL.CLS_Session.sqldb + "_" + baktime + ".bak", " Note تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    }
                }
                // catch (Exception ex) {
                //    MessageBox.Show( ex.Message);
                catch { con4.Close(); }
                // bak_timer.Stop();
                Thread.Sleep(4000000);
                bak_timer.Start();
            
            }
        
        }

        public void thread_get_ts_msg()
        {
           
            try
            {
                SqlDataAdapter dat = new SqlDataAdapter("select * from ts_msg where inactive=0", BL.DAML.con_ts);
                DataTable dtt= new DataTable();
                dat.Fill(dtt);

                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    //  MessageBox.Show(Convert.ToDateTime(dt.Rows[i][5]).ToString());
                    string StrQuery = " MERGE ts_msg as t"
                        + " USING (select " + dtt.Rows[i][0] + " as id, '" + dtt.Rows[i][1] + "' as ar_text, '" + dtt.Rows[i][2] + "' as en_text,'" + Convert.ToDateTime(dtt.Rows[i][3]).ToString("yyyy-MM-dd", new CultureInfo("en-US", false)) + "' as date, " +(Convert.ToBoolean(dtt.Rows[i][4])? "1" : "0" )+ " as inactive) as s"
                                    + " ON t.id=s.id "
                                  //  + " WHEN MATCHED THEN"
                                  //  + " UPDATE SET T.total = S.total,T.discount = S.discount,T.net_total = S.net_total,t.tax_amt=s.tax_amt"
                                    + " WHEN NOT MATCHED THEN"
                                    + " INSERT VALUES( s.id, s.ar_text, s.en_text, s.date, s.inactive);";
                    //try
                    //{
                    //SqlConnection conn = new SqlConnection();


                    using (SqlCommand comm = new SqlCommand(StrQuery, BL.DAML.con))
                    {
                        if (BL.DAML.con.State != ConnectionState.Open)
                        {
                            BL.DAML.con.Open();
                        }
                        comm.ExecuteNonQuery();
                        BL.DAML.con.Close();
                    }
                }

                SqlDataAdapter dat2 = new SqlDataAdapter("select * from ts_msg where inactive=0", BL.DAML.con);
                DataTable dtt2 = new DataTable();
                dat2.Fill(dtt2);
                if (dtt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dtt2.Rows.Count; i++)
                    {
                        PL.Ts_Msg ts = new PL.Ts_Msg();
                        ts.MdiParent = MdiParent;
                        ts.textBox1.Text = dtt2.Rows[i][1].ToString();
                        ts.label2.Text = dtt2.Rows[i][3].ToString();
                        ts.label3.Text = dtt2.Rows[i][0].ToString();

                        ts.ShowDialog();
                    }
                }

                SqlDataAdapter dattx = new SqlDataAdapter("select * from customers where vndr_taxcode='" + BL.CLS_Session.tax_no + "'", BL.DAML.con_ts);
                DataTable dtttx = new DataTable();
                dattx.Fill(dtttx);
                if (dtttx.Rows.Count > 0)// && Convert.ToBoolean(dtttx.Rows[0]["inactive"]))
                {
                    BL.CLS_Session.system_down = Convert.ToBoolean(dtttx.Rows[0]["inactive"]);
                    string StrQuery;
                    if (!File.Exists(Environment.CurrentDirectory + @"\local.txt"))
                        StrQuery = "update dbmstr.dbo.companys set ts_custno='" + dtttx.Rows[0]["cu_no"].ToString() + "' , down=" + (Convert.ToBoolean(dtttx.Rows[0]["inactive"]) ? "1" : "0") + "";
                    else
                        StrQuery = "update companys set ts_custno='" + dtttx.Rows[0]["cu_no"].ToString() + "' , down=" + (Convert.ToBoolean(dtttx.Rows[0]["inactive"]) ? "1" : "0") + "";

                    using (SqlCommand comm = new SqlCommand(StrQuery, BL.DAML.con))
                    {
                        if (BL.DAML.con.State != ConnectionState.Open)
                        {
                            BL.DAML.con.Open();
                        }
                        comm.ExecuteNonQuery();
                        BL.DAML.con.Close();
                    }
                }
                else
                {
                    string StrQuery;
                   // string StrQuery;
                   // if (!File.Exists(Environment.CurrentDirectory + @"\local.txt"))
                    StrQuery = "INSERT INTO customers([cu_no],[cu_brno],[cu_name],[cu_class],[cf_fcy],[vndr_taxcode]) VALUES((select isnull(max(cast(cu_no as int)),0) + 1 from customers) ,'01','" + BL.CLS_Session.cmp_name + "','01','   ','" + BL.CLS_Session.tax_no + "' )";
                   // else
                    //    StrQuery = "update companys set down=" + dtttx.Rows[0]["inactive"] + "";

                    using (SqlCommand comm = new SqlCommand(StrQuery, BL.DAML.con_ts))
                    {
                        if (BL.DAML.con_ts.State != ConnectionState.Open)
                            BL.DAML.con_ts.Open();
                        
                        comm.ExecuteNonQuery();
                        BL.DAML.con_ts.Close();
                    }
                }

               // if (Convert.ToBoolean(dtttx.Rows[0]["inactive"]) || BL.CLS_Session.system_down)
                if (BL.CLS_Session.system_down)
                {
                    System.Threading.Timer t = new System.Threading.Timer(TimerCallback, null, 0, 10000);
                    

                    MessageBox.Show("Please Contact Technical Support" + "   " +"يرجى التواصل مع الدعم الفني", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //MAIN mn = new MAIN();
                    //mn.menuStrip1.Visible = false;
                    //mn.panel1.Visible = false;
                    //mn.panel2.Visible = false;
                    Application.Exit();
                   // return;
                }
            }
           // catch (Exception ex) { MessageBox.Show(ex.Message); }
            catch  {}
        }

        private static void TimerCallback(Object o) {
            Application.Exit();
        }
           

        private void مقارنةالمبيعاتبالتكلفةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Report_Sal_WC rs = new Sal.Sal_Report_Sal_WC();
            rs.MdiParent = this;
            rs.Show();
        }

        private void timer_update_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "     " + (BL.CLS_Session.lang.Equals("ع") ? DateTime.Now.ToString("dd-MM-yyyy م", new CultureInfo("en-US", false)) : DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false)) + " M");
            toolStripStatusLabel2.Text = "     " + (BL.CLS_Session.lang.Equals("ع") ? DateTime.Now.ToString("dd-MM-yyyy هـ", new CultureInfo("ar-SA", false)) : DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false)) + " H");
            toolStripStatusLabel5.Text = "     " + (BL.CLS_Session.lang.Equals("ع") ? DateTime.Now.ToString("dddd", new CultureInfo("ar-SA", false)) : DateTime.Now.ToString("dddd", new CultureInfo("en-US", false)));
        }

        private void اقساموقطاعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.DepCnters acc = new Acc.DepCnters();
            acc.MdiParent = this;
            acc.Show();
        }

        private void ارباحوخسائرالقسمالقطاعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_Final_SC_Report f6 = new Acc.Acc_Final_SC_Report();
            f6.MdiParent = this;
            f6.Show();
        }

        private void تقريراستعراضالاصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.FRM_PRODUCTS fr = new Sto.FRM_PRODUCTS();
            fr.MdiParent = this;
            fr.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            ////MessageBox.Show("سيبداء البرنامج بتحميل التحديث الجديد .. يمكنك مواصلة العمل سيتم اخبارك عند الانتهاء من التحميل", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            ////Thread a = new Thread(() => thread1());// new Thread(new ThreadStart(tc.thread1(cmb_type.SelectedValue.ToString(), txt_ref.Text.ToString(), cmb_salctr.SelectedValue.ToString())));
            ////a.Start(); 

            //UpdateProgress up = new UpdateProgress();
            //up.MdiParent =this;
            //up.Show();

            DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("SELECT name,format(create_date,'dd-MM-yyyy') cdate FROM sys.databases where name='dbmstr'");
         //   MessageBox.Show(dt.Rows[0][1].ToString(),"Sub. Date",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            MessageBox.Show(Directory.GetCreationTime(Environment.CurrentDirectory).ToString("dd-MM-yyyy"), "Sub. Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void thread1()
        {
            var client = new WebClient();

            try
            {
                // System.Threading.Thread.Sleep(5000);
                File.Delete(@".\AppUpdater.exe");
                client.DownloadFile("https://www.dropbox.com/s/h98qc645q3xb268/update.zip?dl=1", @"update.zip");
              //  string zipPath = @".\update.zip";
              //  string extractPath = @".\";


               // ZipFile zip = new ZipFile(Application.StartupPath + zipPath);
               // zip.ExtractAll(extractPath, ExtractExistingFileAction.OverwriteSilently);

               // File.Delete(@".\update.zip");
               // Process.Start(@".\AppUpdater.exe");
               // this.Close();
                MessageBox.Show("نجح في تحميل التحديث","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            }
            catch
            {
               // Process.Start("AppUpdater.exe");
               // this.Close();
                MessageBox.Show("فشل في تحميل التحديث","",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void تحديثToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UpdateProgress up = new UpdateProgress();
            //up.MdiParent = this;
            //up.Show();

            Process.Start(@".\App_Updater.exe");
            
        }

        private void مراجعةسنداتالتحويلبينالمخازنToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Sto_Report_Convrt rs = new Sto.Sto_Report_Convrt();
            rs.MdiParent = this;
            rs.Show();
        }

        private void قبضمنفاتورةمقسطةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Tran_QFK rs = new Cus.Acc_Tran_QFK("", "");
            rs.MdiParent = this;
            rs.Show();
            
        }

        private void متابعةالفواتيرالمقسطةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Qst_Balance rs = new Sal.Sal_Qst_Balance();
            rs.MdiParent = this;
            rs.Show();
        }

        private void كشفحسابفاتورةمقسطةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sal.Sal_Qst_Statment_Exp f4a = new Sal.Sal_Qst_Statment_Exp("", "");
            f4a.MdiParent = this;
            f4a.Show();
        }

        private void كشفحسابمراجعةالعقودToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Statment_Exp_Aqd f6 = new Cus.Acc_Statment_Exp_Aqd("");
            f6.MdiParent = this;
            f6.Show();
        }

        private void سنداتاستلاموصرفبينالشركاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Sto.Sto_Cnvrt_Comp f6 = new Sto.Sto_Cnvrt_Comp("","","");
            //f6.MdiParent = this;
            //f6.Show();
            Sto.Sto_InOut rr = new Sto.Sto_InOut("", "", "");
            rr.MdiParent = this;
            rr.Show();
        }

        private void مراجعةاسعارالاصنافوالباركوداتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sto.Items_Rivew fr = new Sto.Items_Rivew();
            fr.MdiParent = this;
            fr.Show();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {

        }

        private void ملفالاضافاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.Adds fr = new Pos.Adds();
            fr.MdiParent = this;
            fr.Show();
        }

        private void ملفالموصلينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.Pos_Drivers fr = new Pos.Pos_Drivers();
            fr.MdiParent = this;
            fr.Show();
        }

        private void ملخصحركةالموصلينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.SalesReport_m sr = new Pos.SalesReport_m();
            sr.MdiParent = this;
            sr.Show();
        }

        private void مراجعةمبالغالتامينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.SalesReport_tm sr = new Pos.SalesReport_tm();
            sr.MdiParent = this;
            sr.Show();
        }

        private void ملخصمبيعاتالمجموعاتToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Pos.Pos_Report_Gr rs = new Pos.Pos_Report_Gr();
            //Pos.Pos_Report_App rs = new Pos.Pos_Report_App();
            rs.MdiParent = this;
            rs.Show();
        }

        private void اصلاحالمبيعاتوالقيودالمحاسبيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.Pos_Sal_Cmbin_Rp f6 = new Pos.Pos_Sal_Cmbin_Rp();
            f6.MdiParent = this;
            f6.Show();
        }

        private void ارسالالحركاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_Update f6 = new Acc.Acc_Update();
            f6.MdiParent = this;
            f6.Show();
        }

        private void ميزانمراجعةعملاءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Mizan f6 = new Cus.Acc_Mizan();
            f6.MdiParent = this;
            f6.Show();
        }

        private void ميزانمراجعةموردينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sup.Acc_Mizan f6 = new Sup.Acc_Mizan();
            f6.MdiParent = this;
            f6.Show();
        }

        private void مراجعةحركاتالمشترياتومصاريفهاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pur.Pur_Report_Ms rs = new Pur.Pur_Report_Ms();
            rs.MdiParent = this;
            rs.Show();
        }

        private void جلبالبياناتمنالفروعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Copy_Data f6= new Acc.Copy_Data();
            f6.MdiParent = this;
            f6.Show();
        }

        private void نقلحركةحساباوصتفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PL.Move_Acc_Its f6 = new PL.Move_Acc_Its();
            f6.MdiParent = this;
            f6.Show();
        }

        private void ملخصنشاطالعملاءوالمندوبينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Balance_SM_Sumry rs = new Cus.Acc_Balance_SM_Sumry();
            rs.MdiParent = this;
            rs.Show();
        }

        private void فتحسنةجديدةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open_New_Yr bk = new Open_New_Yr();
            bk.MdiParent = this;
            bk.Show();
        }

        private void ادخالالجرديدوياToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Sto.Grd_Entry rs = new Sto.Grd_Entry();
            //rs.MdiParent = this;
            //rs.Show();
        }

        private void تغييرنوعالفاتورةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.oneslserial)
            {
                MessageBox.Show("يجب اغلاق جميع الشاشات قبل البدء بهذه العملية", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Sal.Sal_Chang_Type rs = new Sal.Sal_Chang_Type();
                rs.MdiParent = this;
                rs.Show();
            }
            else
                MessageBox.Show("لا يمكن استخدام هذه الميزة الا اذا كان التسلسل موحد للمبيعات", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void سدادفاتورةاجلةبسندToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Acc_Tran_QAK rs = new Cus.Acc_Tran_QAK("", "");
            rs.MdiParent = this;
            rs.Show();
        }

        private void استيرادالموردينمناكسلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Import_SC_From_Xls rs = new Cus.Import_SC_From_Xls("Sup");
            rs.MdiParent = this;
            rs.Show();
        }

        private void استيرادالعملاءمناكسلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cus.Import_SC_From_Xls rs = new Cus.Import_SC_From_Xls("Cust");
            rs.MdiParent = this;
            rs.Show();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Pos.Items_Update iu = new Pos.Items_Update();
            iu.MdiParent = this;
            iu.label4.Visible = false;
            iu.label6.Visible = false;
            iu.txt_fmdate.Visible = false;
            iu.txt_tmdate.Visible = false;
            iu.button2.Visible = false;
            iu.chk_offers.Visible = false;
            iu.chk_salman.Visible = false;
            iu.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://tree-soft.com/");
        }

        private void تجهيزوارسالالفواتيرللزكاةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.SalesReport_ToSend ts = new Pos.SalesReport_ToSend("");
            ts.MdiParent = this;
            ts.Show();
        }

        private void القيودوالمصاريفالضريبيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_Tax_Tran2 act = new Acc.Acc_Tax_Tran2("", "", "");
            // Acc.Acc_Tax_Tran_All act = new Acc.Acc_Tax_Tran_All("", "");
            act.MdiParent = this;
            act.Show();
        }

        private void قائمةفواتيرعميلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.MenuSalesCustReport rs = new Pos.MenuSalesCustReport();
            rs.MdiParent = this;
            rs.Show();
        }

        private void sync_timer_Tick(object sender, EventArgs e)
        {
           // MessageBox.Show("test sync");
            if (Convert.ToInt32(BL.CLS_Session.einv_p2_synctim) == Convert.ToInt32(DateTime.Now.Hour.ToString()) && BL.CLS_Session.einv_p2_syncactv)
            {
                Thread abc = new Thread(() => thread_synceinv("auto"));// new Thread(new ThreadStart(tc.thread1(cmb_type.SelectedValue.ToString(), txt_ref.Text.ToString(), cmb_salctr.SelectedValue.ToString())));
                abc.Start();

            }
            
        }

        private void ملخصمبيعاتالتطبيقاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.Pos_Report_App rs = new Pos.Pos_Report_App();
            rs.MdiParent = this;
            rs.Show();
        }

        private void اعمـــارديــونالمـــوردينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sup.Acc_Balances_Life f6 = new Sup.Acc_Balances_Life("");
            f6.MdiParent = this;
            f6.Show();
        }

        private void طرقالدفعالتطبيقاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos.Apps rs = new Pos.Apps();
            rs.MdiParent = this;
            rs.Show();
        }

        private void الميزانيةالعموميةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acc.Acc_Final_Report_mm f6 = new Acc.Acc_Final_Report_mm();
            f6.MdiParent = this;
            f6.Show();
        } 
    }
}
