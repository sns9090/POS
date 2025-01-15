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
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using MetroFramework;
//using CrystalDecisions.CrystalReports.Engine;
using System.Globalization;
//using System.IO.Ports;


namespace POS.Pos
{
    public partial class Resturant_ReSales2 : Form
    {
        SqlDataAdapter dacr1, dacr, dacr2;
        DataTable dtn;
        DataSet1 ds;

        Pos.Add_Tameen at=new Add_Tameen();
        decimal price1 = 0, total1 = 0;
       // SerialPort sp;
        string sl_brno, sl_id, sl_name, sl_password, slpmaxdisc, maxitmdsc,txt="",tameen,taminn;
        bool sl_chgqty, sl_chgprice, sl_delline, sl_delinv, sl_return, sl_admin, sl_alowdisc, sl_alowexit, alwreprint, alwitmdsc, sl_inactive;

        Pos.Cust_Screen_R cssc;
        BL.DAML daml = new BL.DAML();
        SqlConnection con2 = BL.DAML.con;//new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
       // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        int ccc,cur_row,typeno=1,refmax,rqmax;
        string prttype=null,cmp_name="",comp_id="",printer_nam="";
        string[] lines_prt=null;
       // string path = Directory.GetCurrentDirectory()+ @"\printers.txt";

       // string[] lines_prt = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\printers.txt");
       // string[] lines_prt = new string[] { };
       // string[] lines_prt = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\printers.txt");
        

       // StringBuilder input = new StringBuilder();
        int flag = 1; 

        public Resturant_ReSales2()
        {
            InitializeComponent();
            //flowLayoutPanel1.AutoScroll = false;

            //flowLayoutPanel2.AutoScroll = false;

        }

        private void creat_btns()
        {
            try
            {
                if (checkBox2.Checked == false)
                {
                    SqlDataAdapter da = new SqlDataAdapter("select " + (BL.CLS_Session.lang.Equals("E") ? " item_ename" : " item_name") + "  as item_name,item_no,item_image,item_price from items where item_group=" + ccc + " and inactive=0 order by cast(item_no as bigint)", con2);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "0");

                    int cunt = ds.Tables["0"].Rows.Count;


                    //   pictureBox1.Image = Image.FromStream(ms);


                    flowLayoutPanel2.Controls.Clear();
                    for (int i = 0; i < cunt; i++)
                    {
                        //Button b = new Button();
                        //b.Name = i.ToString();
                        //b.Text = "btn : " + i.ToString();
                        //b.Width = 100;
                        //b.Height = 100;
                        //b.Click += new EventHandler(b_click);
                        //flowLayoutPanel2.Controls.Add(b);

                        Button b = new Button();
                        b.Name = ds.Tables["0"].Rows[i][1].ToString();
                        b.Text = ds.Tables["0"].Rows[i][0].ToString() + "\n" + ds.Tables["0"].Rows[i][3].ToString();
                        //*****  b.TextAlign = ContentAlignment.BottomCenter;
                        b.FlatStyle = FlatStyle.Standard;



                        //  b.Margin = new Padding(100, 100, 100, 0);

                        if (ds.Tables["0"].Rows[i][2] is DBNull)
                        {
                            // b.Image = null;

                            // b.Image= ContentAlignment.MiddleRight; 
                            //  b.Image = Properties.Resources.background_button;
                            b.BackgroundImage = Properties.Resources.background_button;
                            b.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        else
                        {
                            //////byte[] image = (byte[])ds.Tables["0"].Rows[i][2];
                            //////MemoryStream ms = new MemoryStream(image);
                            //////// b.Image = Image.FromStream(ms);
                            //////// b.Image = null;
                            ////////                     b.BackgroundImage = Image.FromStream(ms);
                            /////////                   b.BackgroundImageLayout = ImageLayout.Stretch;


                            //////Image img = Image.FromStream(ms);
                            ////////  devBtn = new Button();

                            //////b.BackgroundImage = img;
                            // See if this file exists in the same directory.
                            if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + ds.Tables["0"].Rows[i][2]))
                            {
                                b.BackgroundImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + ds.Tables["0"].Rows[i][2]);
                                b.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                b.BackgroundImage = Properties.Resources.background_button;
                                b.BackgroundImageLayout = ImageLayout.Stretch;
                            }

                            // b.Size = new Size((img.Width + 5), (img.Height + 5));

                            // b.Top = positionTOP;
                        }
                        /*
                        byte[] data = (byte[])ds.Tables["0"].Rows[i][2];
                        using (MemoryStream ms = new MemoryStream(data))
                        {
                            //here you get the image and assign it to the button.
                            Image image = new Bitmap(ms);
                        }
                        */



                        // b.Image = new Bitmap(ms);
                        b.Width = 102;
                        b.Height = 65;
                        // b.Top = 10;
                        b.Font = new Font("Arial", 11, FontStyle.Bold);
                        b.Click += new EventHandler(b_click);
                        flowLayoutPanel2.Controls.Add(b);


                    }
                }
                else
                {
                    SqlDataAdapter da = new SqlDataAdapter("select " + (BL.CLS_Session.lang.Equals("E") ? " item_ename" : " item_name") + " as item_name,item_no from items where item_group=" + ccc + " order by cast(item_no as bigint)", con2);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "0");

                    int cunt = ds.Tables["0"].Rows.Count;


                    //   pictureBox1.Image = Image.FromStream(ms);


                    flowLayoutPanel2.Controls.Clear();
                    for (int i = 0; i < cunt; i++)
                    {
                        //Button b = new Button();
                        //b.Name = i.ToString();
                        //b.Text = "btn : " + i.ToString();
                        //b.Width = 100;
                        //b.Height = 100;
                        //b.Click += new EventHandler(b_click);
                        //flowLayoutPanel2.Controls.Add(b);

                        Button b = new Button();
                        b.Name = ds.Tables["0"].Rows[i][1].ToString();
                        b.Text = ds.Tables["0"].Rows[i][0].ToString() + "\n" + ds.Tables["0"].Rows[i][3].ToString();
                        //*****  b.TextAlign = ContentAlignment.BottomCenter;
                        b.FlatStyle = FlatStyle.Standard;



                        //  b.Margin = new Padding(100, 100, 100, 0);

                        
                            // b.Image = null;

                            // b.Image= ContentAlignment.MiddleRight; 
                            //  b.Image = Properties.Resources.background_button;
                            b.BackgroundImage = Properties.Resources.background_button;
                            b.BackgroundImageLayout = ImageLayout.Stretch;
                       
                        /*
                        byte[] data = (byte[])ds.Tables["0"].Rows[i][2];
                        using (MemoryStream ms = new MemoryStream(data))
                        {
                            //here you get the image and assign it to the button.
                            Image image = new Bitmap(ms);
                        }
                        */



                        // b.Image = new Bitmap(ms);
                        b.Width = 102;
                        b.Height = 65;
                        // b.Top = 10;
                        b.Font = new Font("Arial", 11, FontStyle.Bold);
                        b.Click += new EventHandler(b_click);
                        flowLayoutPanel2.Controls.Add(b);


                    }
                
                
                }
            }
            catch (Exception ex)
            {
               MetroMessageBox.Show(this,ex.Message);
            }


        }
        private void creat_groups()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select " + (BL.CLS_Session.lang.Equals("E") ? " group_desc" : " group_name") + " as group_name,group_id,group_img from groups where inactive=0 and group_pid is null order by group_order", con2);
                DataSet ds = new DataSet();
                da.Fill(ds, "0");

                int cunt = ds.Tables["0"].Rows.Count;

                flowLayoutPanel1.Controls.Clear();
                for (int i = 0; i < cunt; i++)
                {
                    //Button b = new Button();
                    //b.Name = i.ToString();
                    //b.Text = "btn : " + i.ToString();
                    //b.Width = 100;
                    //b.Height = 100;
                    //b.Click += new EventHandler(b_click);
                    //flowLayoutPanel2.Controls.Add(b);

                    Button b = new Button();
                    b.Name = ds.Tables["0"].Rows[i][1].ToString();
                    b.Text = ds.Tables["0"].Rows[i][0].ToString();
                    b.Width = 100;
                    b.Height = 50;
                    b.Font = new Font("Arial", 11, FontStyle.Bold);

                    b.FlatStyle = FlatStyle.Standard;
                  //  b.Image = Properties.Resources.background_button;
                    if (ds.Tables["0"].Rows[i][2] is DBNull)
                    {
                        b.Image = Properties.Resources.background_button;
                    }
                    else
                    {
                        byte[] image = (byte[])ds.Tables["0"].Rows[i][2];
                        MemoryStream ms = new MemoryStream(image);
                        b.Image = Image.FromStream(ms);
                    }
                        
                    // b.AutoSize = true;
                    b.Click += new EventHandler(b2_click);
                    // listBox1.Controls.Add(b);
                    flowLayoutPanel1.Controls.Add(b);


                }
            }
            catch (Exception ex)
            {
               MetroMessageBox.Show(this,ex.Message);
            }


        }

        void b_click(object sender, EventArgs e)
        {
            SqlDataAdapter da23;

            Button b = sender as Button;
            // label1.Text = b.Text;

            // label1.Text = b.Name;

            try
            {
                SqlDataAdapter dachk = new SqlDataAdapter("select count(*) from  items_bc where item_no='" + b.Name + "'", con2);
                DataTable dtchk = new DataTable();
                dachk.Fill(dtchk);
                if (Convert.ToInt32(dtchk.Rows[0][0]) > 1)
                {
                    Pos.Size_Select fr = new Pos.Size_Select(b.Name.ToString());
                    // fr.MdiParent = this;
                    fr.ShowDialog();
                    // fr.val;
                    da23 = new SqlDataAdapter("select a.[item_no],([" + (BL.CLS_Session.lang.Equals("E") ? "item_ename" : "item_name") + "] +' - ' + '"+fr.nam+"')as item_name,[item_curcost],b.[price],[barcode],[item_unit],[item_req] from  items a join items_bc b on a.item_no=b.item_no where b.barcode='" + fr.bar + "'", con2);
                }
                else
                {
                    da23 = new SqlDataAdapter("select [item_no],[" + (BL.CLS_Session.lang.Equals("E") ? "item_ename" : "item_name") + "] as item_name,[item_curcost],[item_price],[item_barcode],[item_unit],[item_req] from  items where item_no='" + b.Name + "'", con2);
                }
                DataSet ds23 = new DataSet();
                da23.Fill(ds23, "0");

                if (ds23.Tables["0"].Rows.Count == 1)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                    row.Cells[1].Value = ds23.Tables["0"].Rows[0][0];
                    row.Cells[0].Value = ds23.Tables["0"].Rows[0][4];
                    row.Cells[2].Value = ds23.Tables["0"].Rows[0][1];
                    row.Cells[3].Value = ds23.Tables["0"].Rows[0][5];
                    if (row.Cells[4].Value == null)
                    {
                        row.Cells[4].Value = 1;
                    }
                    row.Cells[5].Value = ds23.Tables["0"].Rows[0][3];
                    row.Cells[6].Value = ds23.Tables["0"].Rows[0][2];
                    row.Cells[7].Value = ds23.Tables["0"].Rows[0][6];
                    row.Cells[8].Value = ds23.Tables["0"].Rows[0][3];
                    dataGridView1.Rows.Add(row);
                }

                dataGridView1.ClearSelection();//If you want
                int nRowIndex = dataGridView1.Rows.Count - 2;
                // int nColumnIndex = 3;

                // dataGridView1.Rows[nRowIndex].Selected = true;
                // dataGridView1.Rows[nRowIndex].Cells[nColumnIndex].Selected = true;

                //In case if you want to scroll down as well.
                dataGridView1.FirstDisplayedScrollingRowIndex = nRowIndex;
                // dataGridView1.Rows[nRowIndex].Selected = true;
                total();
                ////if (BL.CLS_Session.use_cd)
                ////    disply(Convert.ToDecimal(dataGridView1.CurrentRow.Cells[5].Value), total1);

                hight();

                dataGridView1.Select();

                cur_row = dataGridView1.Rows.Count-2;

                dataGridView1.Rows[cur_row].Selected = true;
                total();

                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex - 1].Cells[4];
               //dataGridView1.SelectedRows = dataGridView1.Rows[count - 2].Selected;
            }
            catch (Exception ex)
            {
               MetroMessageBox.Show(this,ex.Message);
            }
        }



        void b2_click(object sender, EventArgs e)
        {
            Button c = sender as Button;
            ccc = Convert.ToInt32 (c.Name);

            creat_btns();

            
        }
        void permitions()
        {
            BL.CLS_LOGIN curlg = new BL.CLS_LOGIN();
            DataTable dt = curlg.GetCurUserF();

            if (Convert.ToBoolean(dt.Rows[0][4]) == false)
            {
                foreach (DataGridViewColumn dc in dataGridView1.Columns)
                {
                    // if (dc.Index.Equals(5) || dc.Index.Equals(1))
                    if (dc.Index.Equals(5))
                    {
                        dc.ReadOnly = true;
                    }
                    else
                    {
                        dc.ReadOnly = true;
                    }
                }
            }
            
            //}
        }

        private void set_permition(string salid)
        {
            if ((salid.Equals("0") || salid.Equals("")) && BL.CLS_Session.is_sal_login == false)
            {
                //  BL.CLS_Session.is_sal_login = false;
                this.Close();
            }
            else
            {
                // BL.CLS_Session.is_sal_login = true;
                //BL.CLS_Session.dtsalman = daml.SELECT_QUIRY_only_retrn_dt("select *  from pos_salmen where sl_id=" + salid + " and sl_brno='" + BL.CLS_Session.brno + "'");


                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_chgqty"]) == false)
                    dataGridView1.Columns[4].ReadOnly = true;
                else
                    dataGridView1.Columns[4].ReadOnly = false;

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_chgprice"]) == false)
                    dataGridView1.Columns[5].ReadOnly = true;
                else
                    dataGridView1.Columns[5].ReadOnly = false;

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_delline"]) == false)
                    sl_delline = false;
                else
                    sl_delline = true;

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_delinv"]) == false)
                {
                    sl_delinv = false;
                    //// btn_del.Enabled = false;
                }
                else
                {
                    sl_delinv = true;
                    ////  btn_del.Enabled = true;
                }
                // sl_delinv = false;
                //   else
                // sl_delinv = true;

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_alowdisc"]) == false)
                    sl_alowdisc = false;
                else
                    sl_alowdisc = true;


                slpmaxdisc = BL.CLS_Session.dtsalman.Rows[0]["slpmaxdisc"].ToString();

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_alowexit"]) == false)
                {
                    sl_alowexit = false;
                    //// btn_Exit.Enabled = false;
                }
                else
                {
                    sl_alowexit = true;
                    //// btn_Exit.Enabled = true;
                }

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["alwreprint"]) == false)
                    Print_btn.Enabled = false;
                else
                    Print_btn.Enabled = true;

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["alwitmdsc"]) == false)
                    alwitmdsc = false;
                else
                    alwitmdsc = true;

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_alwcrdit"]) == false)
                    chk_agel.Enabled = false;
                else
                    chk_agel.Enabled = true;

                ////if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["slpalowopdr"]) == false)
                ////    btn_opndr.Enabled = false;
                ////else
                ////    btn_opndr.Enabled = true;

                maxitmdsc = BL.CLS_Session.dtsalman.Rows[0]["maxitmdsc"].ToString();
                lblsalid.Text = BL.CLS_Session.dtsalman.Rows[0]["sl_id"].ToString();
                lbluser.Text = BL.CLS_Session.dtsalman.Rows[0]["sl_name"].ToString();


                int form2Count = Application.OpenForms.OfType<RESALES_D_TCH>().Count();

                if (form2Count > Convert.ToInt32(BL.CLS_Session.dtsalman.Rows[0]["scr_open"]))
                    this.Close();
                //  this.Refresh();
                //  dataGridView1_RowEnter( null,  null);


            }

        }

        public int scrollValue = 1;
        public int ScrollValue
        {
            get
            {


                return scrollValue;
            }
            set
            {
                scrollValue = value;

                if (scrollValue < flowLayoutPanel1.VerticalScroll.Minimum)
                {
                    scrollValue = flowLayoutPanel1.VerticalScroll.Minimum;
                }
                if (scrollValue > flowLayoutPanel1.VerticalScroll.Maximum)
                {
                    scrollValue = flowLayoutPanel1.VerticalScroll.Maximum;
                }

                flowLayoutPanel1.VerticalScroll.Value = scrollValue;
                flowLayoutPanel1.PerformLayout();

            }
        }
       

        private void UpClick(object sender, EventArgs e)
        {
           // ScrollValue -= flowLayoutPanel1.VerticalScroll.LargeChange;
            try
            {

                flowLayoutPanel1.VerticalScroll.Value = VerticalScroll.Minimum;
                flowLayoutPanel1.VerticalScroll.Value = VerticalScroll.Minimum;
                // flowLayoutPanel2.VerticalScroll.Value -= VerticalScroll.LargeChange * 56;
                // flowLayoutPanel2.VerticalScroll.Value -= VerticalScroll.LargeChange * 56;
                //  flowLayoutPanel2.VerticalScroll.Value -= VerticalScroll.LargeChange * 33;

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

        }

        private void DownClick(object sender, EventArgs e)
        {
          //  ScrollValue += flowLayoutPanel1.VerticalScroll.LargeChange;
            // ScrollValue2 += flowLayoutPanel2.VerticalScroll.LargeChange;
            try
            {
                flowLayoutPanel1.VerticalScroll.Value += VerticalScroll.LargeChange * 57;
                flowLayoutPanel1.VerticalScroll.Value += VerticalScroll.LargeChange * 57;
                // flowLayoutPanel2.VerticalScroll.Value = VerticalScroll.Maximum;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        public int scrollValue2 = 1;
        public int ScrollValue2
        {
            get
            {


                return scrollValue2;
            }
            set
            {
                scrollValue2 = value;

                if (scrollValue2 < flowLayoutPanel2.VerticalScroll.Minimum)
                {
                    scrollValue2 = flowLayoutPanel2.VerticalScroll.Minimum;
                }
                if (scrollValue2 > flowLayoutPanel2.VerticalScroll.Maximum)
                {
                    scrollValue2 = flowLayoutPanel2.VerticalScroll.Maximum;
                }

                flowLayoutPanel2.VerticalScroll.Value = scrollValue2;
                flowLayoutPanel2.PerformLayout();

            }
        }
        
        private void UpClick2(object sender, EventArgs e)
        {
           // ScrollValue2 -= flowLayoutPanel2.VerticalScroll.LargeChange;
            try
            {

                flowLayoutPanel2.VerticalScroll.Value = VerticalScroll.Minimum;
                flowLayoutPanel2.VerticalScroll.Value = VerticalScroll.Minimum;
               // flowLayoutPanel2.VerticalScroll.Value -= VerticalScroll.LargeChange * 56;
               // flowLayoutPanel2.VerticalScroll.Value -= VerticalScroll.LargeChange * 56;
                //  flowLayoutPanel2.VerticalScroll.Value -= VerticalScroll.LargeChange * 33;

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

        }

        private void DownClick2(object sender, EventArgs e)
        {
           // ScrollValue2 += flowLayoutPanel2.VerticalScroll.LargeChange;
            try
            {
                flowLayoutPanel2.VerticalScroll.Value += VerticalScroll.LargeChange * 57;
                flowLayoutPanel2.VerticalScroll.Value += VerticalScroll.LargeChange * 57;
                // flowLayoutPanel2.VerticalScroll.Value = VerticalScroll.Maximum;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }
        private void Resturant_sales_Load(object sender, EventArgs e)
        {
            cssc = new Pos.Cust_Screen_R();
            if (Screen.AllScreens.Length > 1 && BL.CLS_Session.use_cd)
            {

                cssc.StartPosition = FormStartPosition.Manual;
                // cssc.Bounds = Screen.AllScreens[1].Bounds;
                cssc.Location = Screen.AllScreens[1].WorkingArea.Location;
                Screen screen = Screen.FromControl(cssc);
                cssc.WindowState = FormWindowState.Maximized;
                cssc.FormBorderStyle = FormBorderStyle.None;
                cssc.TopMost = true;
                cssc.Show();
                //  itc.Location = Screen.AllScreens[1].WorkingArea.Location;
                // itc.WindowState = FormWindowState.Maximized;
            }

            this.BackColor = ColorTranslator.FromHtml(BL.CLS_Session.cmp_color);
            ////if (BL.CLS_Session.use_cd && SerialPort.GetPortNames().ToList().Contains("COM" + BL.CLS_Session.port_no) && !BL.CLS_Session.isopprt)
            ////{
            ////    BL.CLS_Session.sp = new SerialPort("COM" + BL.CLS_Session.port_no);
            ////   // sp = new SerialPort("COM" + BL.CLS_Session.port_no, 9600, Parity.None, 8, StopBits.One);
            ////    BL.CLS_Session.sp.Open();

            ////    //Clear the Display
            ////    BL.CLS_Session.sp.Write(new byte[] { 0x0C }, 0, 1);
            ////    BL.CLS_Session.sp.Write(BL.CLS_Session.cd_msg);
            ////    BL.CLS_Session.isopprt = true;

            ////    //sp.Close();
            ////}
            DataTable dt3 = daml.SELECT_QUIRY_only_retrn_dt("select c_slno,contr_id from contrs");
            BL.CLS_Session.slno = dt3.Rows[0][0].ToString();
          //  BL.CLS_Session.ctrno = dt3.Rows[0][1].ToString();
            lblcashir.Text = BL.CLS_Session.contr_id;

            button5.BackColor = Color.GreenYellow;
            //button6.BackColor= default(Color);
            //button4.BackColor = default(Color);
            dataGridView1.Columns[2].ReadOnly = true;
            //////if (BL.CLS_Session.up_changprice)
            //////    dataGridView1.Columns[5].ReadOnly = false;
            //////else
            //////    dataGridView1.Columns[5].ReadOnly = true;
           // flowLayoutPanel1.Controls.OfType<HScrollBar>().First().Width = 20;
           // permitions();
            //////////foreach (DataGridViewColumn dc in dataGridView1.Columns)
            //////////{
            //////////    // if (dc.Index.Equals(5) || dc.Index.Equals(1))
            //////////    if (dc.Index.Equals(5))
            //////////    {
            //////////        dc.ReadOnly = false;
            //////////    }
            //////////    else
            //////////    {
            //////////        dc.ReadOnly = true;
            //////////    }
            //////////}

            if (con2.State == ConnectionState.Closed)
            {
                con2.Open();
            }
            else
            {
               // con2.Open();
            }
            //  creat_btns();
           // var lines = File.ReadAllLines(Directory.GetCurrentDirectory()+@"\temp.txt");
            var lines2 = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\prttype.txt");
           // string[] lines_prt = File.ReadAllLines(path + @"printers.txt");
           // MessageBox.Show(path.ToString());
           // var lines_prt2 = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\printers.txt");

           
           lines_prt = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\printers.txt");

           PrinterSettings settings = new PrinterSettings();
           printer_nam = settings.PrinterName;

            //lbluser.Text = lines.First().ToString();
            lbluser.Text = BL.CLS_Session.UserName;

            /*
            pass.Text = lines.Skip(1).First().ToString();
            lblcashir.Text = lines.Skip(2).First().ToString();
            cmp_name = lines.Skip(4).First().ToString();

            prttype = lines2.First().ToString();
            comp_id = lines.Skip(8).First().ToString();
            */
            prttype = lines2.First().ToString();
            try
            {
                
                DateTime dt = DateTime.Now;
                string xxx = dt.ToString("yyyy-MM-dd");
                lbldate.Text = xxx;
                //SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Documents and Settings\sa\My Documents\Visual Studio 2008\Projects\POS\POS\DB.mdf;Integrated Security=True;User Instance=True");
                //SqlDataAdapter da = new SqlDataAdapter("select * from items", con);
                //DataSet ds = new DataSet();
                //da.Fill(ds, "0");

                // dataGridView1.CurrentCell = dataGridView1[0, 0];
               // dataGridView1.Focus();
                // dataGridView1.CurrentCell = dataGridView1[0, 0];
                //dataGridView1.BeginEdit(true);
                //   dataGridView1.Rows[0].Cells[0].Value = ds.Tables["0"].Rows[0][0];
            }
            catch (System.Exception ex)
            {
               MetroMessageBox.Show(this,ex.Message);
            }

            DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref),0) as eee from pos_hdr where contr=" + (lblcashir.Text.Equals("") ? BL.CLS_Session.ctrno : lblcashir.Text) + "");
            lblref.Text = dt2.Rows[0][0].ToString();
            refmax = Convert.ToInt32(dt2.Rows[0][0]);
            creat_groups();
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                /// int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                if (cur_row > -2 && dataGridView1.Rows[cur_row].Cells[0].Value != null)
                {
                    dataGridView1.Rows.RemoveAt(cur_row);
                  //       dataGridView1.Refresh();

                    //int a = Convert.ToInt32(dataGridView1.Rows[cur_row].Cells[4].Value);
                    //if (a > 1)
                    //{
                    //    a = a - 1;
                    //    dataGridView1.Rows[cur_row].Cells[4].Value = a.ToString();
                    //}
                    //else
                    //{
                    //    dataGridView1.Rows.RemoveAt(cur_row);
                    //}
                    // if needed
                    // if needed
                }
            }
            catch (Exception ex)
            {
               MetroMessageBox.Show(this,ex.Message);
            }

            ////////try
            ////////{
            ////////    int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            ////////    if (selectedIndex > -1)
            ////////    {
            ////////        dataGridView1.Rows.RemoveAt(selectedIndex);
            ////////        dataGridView1.Refresh();
            ////////        // dataGridView1.CurrentCell = dataGridView1.Rows[selectedIndex+1].Cells[0];
            ////////        // if needed
            ////////    }
            ////////}
            ////////catch (Exception ex)
            ////////{
            ////////    MessageBox.Show(ex.Message);
            ////////}

            total();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ////try
            ////{
            ////    int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            ////    // int selectedIndex = dataGridView1.CurrentRow.Index;

            ////    if (selectedIndex > -1 && dataGridView1.Rows[selectedIndex].Cells[0].Value != null)
            ////    {
            ////        // dataGridView1.Rows.RemoveAt(selectedIndex);
            ////        // dataGridView1.Refresh();
            ////        int a = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells[4].Value);
            ////        a = a + 1;
            ////        dataGridView1.Rows[selectedIndex].Cells[4].Value = a.ToString();
            ////        // if needed
            ////    }
            ////}
            ////catch (Exception ex)
            ////{
            ////    MessageBox.Show(ex.Message);
            ////}

            try
            {
               // int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                // int selectedIndex = dataGridView1.CurrentRow.Index;
               // cur_row = dataGridView1.CurrentCell.RowIndex;
                if (cur_row > -2 && dataGridView1.Rows[cur_row].Cells[0].Value != null)
                {
                    // dataGridView1.Rows.RemoveAt(selectedIndex);
                    // dataGridView1.Refresh();
                    int a = Convert.ToInt32(dataGridView1.Rows[cur_row].Cells[4].Value);
                    a = a + 1;
                    dataGridView1.Rows[cur_row].Cells[4].Value = a.ToString();
                    dataGridView1.Rows[cur_row].Cells[8].Value = Convert.ToInt32(dataGridView1.Rows[cur_row].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[cur_row].Cells[5].Value);
                    // if needed
                }
            }
            catch (Exception ex)
            {
               MetroMessageBox.Show(this,ex.Message);
            }
           
           


            total();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
               /// int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                if (cur_row > -2 && dataGridView1.Rows[cur_row].Cells[0].Value != null)
                {
                    // dataGridView1.Rows.RemoveAt(selectedIndex);
                    // dataGridView1.Refresh();
                    //////////int a = Convert.ToInt32(dataGridView1.Rows[cur_row].Cells[4].Value);
                    //////////if (a > 1)
                    //////////{
                    //////////    a = a - 1;
                    //////////    dataGridView1.Rows[cur_row].Cells[4].Value = a.ToString();
                    //////////}
                    //////////else
                    //////////{
                    //////////    button1_Click(sender, e);
                    //////////}

                    int a = Convert.ToInt32(dataGridView1.Rows[cur_row].Cells[4].Value);
                    if (a > 1)
                    {
                        a = a - 1;
                        dataGridView1.Rows[cur_row].Cells[4].Value = a.ToString();
                        dataGridView1.Rows[cur_row].Cells[8].Value = Convert.ToInt32(dataGridView1.Rows[cur_row].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[cur_row].Cells[5].Value);
                    }
                    else
                    {
                        dataGridView1.Rows.RemoveAt(cur_row);
                    }
                    // if needed
                    // if needed
                }
            }
            catch (Exception ex)
            {
               MetroMessageBox.Show(this,ex.Message);
            }

            total();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                textBox1.Text = textBox1.Text + button31.Text;
            }
            if(flag==2)
            {
                txt_desc.Text = txt_desc.Text + button31.Text;
            }


        }

        private void button40_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                textBox1.Text = "";
            }
            if (flag == 2)
            {
                txt_desc.Text = "";
            }
           // textBox1.Text = "";
        }

        private void total()
        {


            try
            {

                double sum = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                    dataGridView1.Rows[i].Cells[8].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value);
                }
                label1.Text = sum.ToString();
                total1 = Convert.ToDecimal(label1.Text) - Convert.ToDecimal(string.IsNullOrEmpty(txt_desc.Text) ? "0" : txt_desc.Text);

                double sumcost = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    sumcost += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                }
                textBox2.Text = sumcost.ToString();

                double sum1 = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    sum1 += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                }
                label9.Text = sum1.ToString();


                DataTable dt = new DataTable();
                int cn = 1;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    /*
                    dt.Columns.Add(col.Name);
                    // dt.Columns.Add(col.HeaderText);
                     */
                    dt.Columns.Add("c" + cn.ToString());
                    cn++;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        DataRow dRow = dt.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dRow[cell.ColumnIndex] = cell.Value;
                        }
                        dt.Rows.Add(dRow);
                    }
                }

               // Pos.Cust_Screen_R pct = new Cust_Screen_R();
                cssc.dataGridView1.DataSource=dt;
                cssc.dataGridView1.ClearSelection();
                cssc.dataGridView1.Rows[cur_row].Selected = true;
                cssc.textBox2.Text = sum1.ToString();
                cssc.textBox1.Text = sum.ToString();
               // pct.Show();
               
            }
            catch (Exception ex)
            {
             //  MetroMessageBox.Show(this,ex.Message);
            }

        }

        private void button32_Click(object sender, EventArgs e)
        {
            //textBox1.Text = textBox1.Text + button32.Text;
            if (flag == 1)
            {
                textBox1.Text = textBox1.Text + button32.Text;
            }
            if (flag == 2)
            {
                txt_desc.Text = txt_desc.Text + button32.Text;
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            //textBox1.Text = textBox1.Text + button33.Text;
            if (flag == 1)
            {
                textBox1.Text = textBox1.Text + button33.Text;
            }
            if (flag == 2)
            {
                txt_desc.Text = txt_desc.Text + button33.Text;
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            //textBox1.Text = textBox1.Text + button34.Text;
            if (flag == 1)
            {
                textBox1.Text = textBox1.Text + button34.Text;
            }
            if (flag == 2)
            {
                txt_desc.Text = txt_desc.Text + button34.Text;
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            //textBox1.Text = textBox1.Text + button35.Text;
            if (flag == 1)
            {
                textBox1.Text = textBox1.Text + button35.Text;
            }
            if (flag == 2)
            {
                txt_desc.Text = txt_desc.Text + button35.Text;
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            //textBox1.Text = textBox1.Text + button36.Text;
            if (flag == 1)
            {
                textBox1.Text = textBox1.Text + button36.Text;
            }
            if (flag == 2)
            {
                txt_desc.Text = txt_desc.Text + button36.Text;
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            //textBox1.Text = textBox1.Text + button37.Text;
            if (flag == 1)
            {
                textBox1.Text = textBox1.Text + button37.Text;
            }
            if (flag == 2)
            {
                txt_desc.Text = txt_desc.Text + button37.Text;
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            //textBox1.Text = textBox1.Text + button38.Text;
            if (flag == 1)
            {
                textBox1.Text = textBox1.Text + button38.Text;
            }
            if (flag == 2)
            {
                txt_desc.Text = txt_desc.Text + button38.Text;
            }
        }
        
        private void button39_Click(object sender, EventArgs e)
        {
            //textBox1.Text = textBox1.Text + button39.Text;
            if (flag == 1)
            {
                textBox1.Text = textBox1.Text + button39.Text;
            }
            if (flag == 2)
            {
                txt_desc.Text = txt_desc.Text + button39.Text;
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                if (textBox1.Text != "" && textBox1.Text != "0")
                {
                    textBox1.Text = textBox1.Text + button0.Text;
                }
                else
                {
                    textBox1.Text = "0";
                }
            }

            if (flag == 2)
            {

                if (txt_desc.Text != "" && txt_desc.Text != "0")
                {
                    txt_desc.Text = txt_desc.Text + button0.Text;
                }
                else
                {
                    txt_desc.Text = "";
                }
            
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {
                double pay = Convert.ToDouble(textBox1.Text);
                //double remain = Convert.ToDouble(label1.Text) - pay;
                double remain = Convert.ToDouble(label1.Text) - Convert.ToDouble(string.IsNullOrEmpty(txt_desc.Text) ? "0" : txt_desc.Text) - pay;

                if (remain > 0)
                {

                    
                    label5.Text = remain.ToString()+"   " + "المتبقي للزبون";
                   // label4.BackColor = Color.Pink;
                    label5.BackColor = Color.Pink;
                }
                else
                {
                   // label4.Text = "";
                    label5.Text = remain.ToString()+"   " + "المتبقي للزبون";
                   // label4.BackColor = Color.GreenYellow;
                    label5.BackColor = Color.GreenYellow;
                }
            }
            else
            {
              //  label4.Text = "المتبقي على الزبون";
              //  label5.Text = label1.Text + "   " + "المتبقي عالزبون";
                label5.Text = (Convert.ToDouble(label1.Text) - Convert.ToDouble(string.IsNullOrEmpty(txt_desc.Text) ? "0" : txt_desc.Text)).ToString() + "   " + "المتبقي للزبون";
               
               // label4.BackColor = Color.Pink;
                label5.BackColor = Color.Pink;
            }




            total();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
           


            // int nRow = dataGridView1.CurrentCell.RowIndex;
            //if (nRow < dataGridView1.RowCount)
            //{
            //    dataGridView1.Rows[nRow].Selected = false;
            //    dataGridView1.Rows[++nRow].Selected = true;
            //}

        }
        private void Save_Only(object sender, EventArgs e)
        {

           
            double sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                if (dataGridView1.Rows[i].Cells[7].Value != null)
                {
                    sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value);
                }
            }
            sum = sum - Convert.ToDouble(string.IsNullOrEmpty(txt_desc.Text) ? "0" : txt_desc.Text);

            string s1, s2;
            if (txt_invbar.Text.Contains("-"))
            {
                s1 = txt_invbar.Text.Substring(0, txt_invbar.Text.IndexOf("-"));
                s2 = txt_invbar.Text.Substring(txt_invbar.Text.IndexOf("-") + 1);
            }
            else
            {
                s1 = BL.CLS_Session.contr_id;
                s2 = txt_invbar.Text;
            }
            /*
            if (sum > 0)
            {
                SqlDataAdapter da = new SqlDataAdapter("select isnull(max(req_no),0) from pos_hdr", con2);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int req_max = Convert.ToInt32(dt.Rows[0][0]);
                
                
                using (SqlCommand cmd1 = new SqlCommand("INSERT INTO pos_hdr(brno,slno,contr,type,date,total,count,payed,total_cost,saleman,req_no,cust_no,discount,net_total) VALUES(@br,@sl,@ctr,@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a11,@a12)", con2))
                {
                    cmd1.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                    cmd1.Parameters.AddWithValue("@sl", BL.CLS_Session.slno);
                    cmd1.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);

                    cmd1.Parameters.AddWithValue("@a0", typeno);

                    cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                    // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                    cmd1.Parameters.AddWithValue("@a2", Convert.ToDouble(label1.Text));
                    cmd1.Parameters.AddWithValue("@a3", Convert.ToDouble(label9.Text));
                    cmd1.Parameters.AddWithValue("@a4", Convert.ToDouble(textBox1.Text));
                    cmd1.Parameters.AddWithValue("@a5", Convert.ToDouble(textBox2.Text));
                   // cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                    cmd1.Parameters.AddWithValue("@a7", lbluser.Text);
                    cmd1.Parameters.AddWithValue("@a8", req_max + 1);
                    cmd1.Parameters.AddWithValue("@a9", Convert.ToInt32(label3.Text));
                   // cmd1.Parameters.AddWithValue("@a10", comp_id);
                    cmd1.Parameters.AddWithValue("@a11", 0);
                    cmd1.Parameters.AddWithValue("@a12", Convert.ToDouble(label1.Text));
                    cmd1.ExecuteNonQuery();
                }
            }
            else
            {
             * */

            DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref),0),isnull(max(req_no),0) from pos_hdr where [contr]=" + BL.CLS_Session.ctrno + " and [brno]= '" + BL.CLS_Session.brno + "' ");
            refmax = Convert.ToInt32(dt2.Rows[0][0]);// = Convert.ToString(dt2.Rows[0][0]);
            rqmax = Convert.ToInt32(dt2.Rows[0][1]);
            using (SqlCommand cmd1 = new SqlCommand("INSERT INTO pos_hdr(brno,slno,ref,contr,type,date,total,count,payed,total_cost,saleman,req_no,cust_no,discount,net_total,tax_amt,dscper,card_type,card_amt,note,taxfree_amt,mobile,rref,rcontr) VALUES(@br,@sl,@rf,@ctr,@a0,@a1,@a2,@a3,@a4,@a5,@a7,@a8,@a9,@a11,@a12,@a13,@a14,@a15,@a16,@a17,@a18,@a19,@a20,@a21)", con2))
                {
                    cmd1.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                    cmd1.Parameters.AddWithValue("@sl", BL.CLS_Session.slno);
                    cmd1.Parameters.AddWithValue("@rf", refmax + 1);
                    cmd1.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);
                    cmd1.Parameters.AddWithValue("@a0",(chk_agel.Checked && !string.IsNullOrEmpty(txt_custno.Text))? 2 : 0);

                    cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                    // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                    cmd1.Parameters.AddWithValue("@a2", Convert.ToDouble(label1.Text));
                    cmd1.Parameters.AddWithValue("@a3", Convert.ToDouble(label9.Text));
                    cmd1.Parameters.AddWithValue("@a4", (chk_iscard.Checked ? 0 :Convert.ToDouble(textBox1.Text)));
                    cmd1.Parameters.AddWithValue("@a5", Convert.ToDouble(textBox2.Text));
                  //  cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                    cmd1.Parameters.AddWithValue("@a7", lblsalid.Text);
                    cmd1.Parameters.AddWithValue("@a8", rqmax + 1 );// sum);
                   // cmd1.Parameters.AddWithValue("@a9", Convert.ToInt32(label3.Text));
                   //// cmd1.Parameters.AddWithValue("@a10", comp_id);
                   // cmd1.Parameters.AddWithValue("@a11", 0);
                   // cmd1.Parameters.AddWithValue("@a12", string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : Convert.ToDouble(label1.Text));
                    cmd1.Parameters.AddWithValue("@a9",string.IsNullOrEmpty(txt_custno.Text)? 0 : Convert.ToInt32(txt_custno.Text));
                    // cmd1.Parameters.AddWithValue("@a10", comp_id);
                    cmd1.Parameters.AddWithValue("@a11", Convert.ToDouble(string.IsNullOrEmpty(txt_desc.Text) ? "0" : txt_desc.Text));
                    cmd1.Parameters.AddWithValue("@a12", (Convert.ToDouble(label1.Text) - Convert.ToDouble(string.IsNullOrEmpty(txt_desc.Text) ? "0" : txt_desc.Text)));
                    cmd1.Parameters.AddWithValue("@a13", string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : Math.Round(((Convert.ToDouble(label1.Text) - Convert.ToDouble(string.IsNullOrEmpty(txt_desc.Text) ? "0" : txt_desc.Text)) * Convert.ToDouble(BL.CLS_Session.tax_per) / (100 + Convert.ToDouble(BL.CLS_Session.tax_per))), 4));
                    cmd1.Parameters.AddWithValue("@a14", Convert.ToDouble(string.IsNullOrEmpty(txt_dscper.Text) ? "0" : txt_dscper.Text));
                    cmd1.Parameters.AddWithValue("@a15", (chk_iscard.Checked? "1" : "0"));
                    cmd1.Parameters.AddWithValue("@a16", (chk_iscard.Checked ? textBox1.Text : "0"));
                    cmd1.Parameters.AddWithValue("@a17", txt_custnam.Text);
                    cmd1.Parameters.AddWithValue("@a18", 0);
                    cmd1.Parameters.AddWithValue("@a19", txt_temp.Text);
                    cmd1.Parameters.AddWithValue("@a20", s2);
                    cmd1.Parameters.AddWithValue("@a21", s1);

                    cmd1.ExecuteNonQuery();
                }
            if (!string.IsNullOrEmpty(txt_driver.Text) || button22.BackColor == Color.GreenYellow || !string.IsNullOrEmpty(txt) || !string.IsNullOrEmpty(at.txt_tameemamt.Text))
            {
                using (SqlCommand cmd1 = new SqlCommand("INSERT INTO pos_hdr_r(dbrno,dslno,dref,dcontr,dtype,driver_id,driver_nam,dnote,dtameen,tameen_note) VALUES(@br,@sl,@rf,@ctr,@a0,@a1,@a2,@a3,@a4,@a5)", con2))
                {
                    cmd1.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                    cmd1.Parameters.AddWithValue("@sl", BL.CLS_Session.slno);
                    cmd1.Parameters.AddWithValue("@rf", refmax + 1);
                    cmd1.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);
                    cmd1.Parameters.AddWithValue("@a0", (chk_agel.Checked && !string.IsNullOrEmpty(txt_custno.Text)) ? 2 : 0);

                    cmd1.Parameters.AddWithValue("@a1", txt_driver.Text); //ToString("yyyy-MM-dd");
                    // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                    cmd1.Parameters.AddWithValue("@a2", txt_drivernam.Text);
                    cmd1.Parameters.AddWithValue("@a3", txt);
                    cmd1.Parameters.AddWithValue("@a4", string.IsNullOrEmpty(at.txt_tameemamt.Text) ? "0" : at.txt_tameemamt.Text);
                    cmd1.Parameters.AddWithValue("@a5", at.txt_tameennot.Text);

                    cmd1.ExecuteNonQuery();
                }
            }

            //}
            txt = "";
            int srno = 1, reff = Convert.ToInt32(lblref.Text) + 1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {/*
                // SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from DB.dbo.sales_hdr", con2);
                SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from sales_hdr", con2);
                DataSet dss = new DataSet();
                daa.Fill(dss, "0");
                */
                if (!row.IsNewRow && row.Cells[0].Value != null || !row.IsNewRow && row.Cells[4].Value != null || !row.IsNewRow && row.Cells[5].Value != null)
                {
                    // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO pos_dtl(brno,slno,ref,contr,type, barcode, name, unit, price, qty, cost,is_req,itemno,srno,tax_amt) VALUES(@br,@sl,@rf,@ctr,@r0,@c1,@c2,@c3,@c4,@c5,@c6,@c8,@c10,@sn,@ta)", con2))
                    {
                        cmd.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                        cmd.Parameters.AddWithValue("@sl", BL.CLS_Session.slno);
                        cmd.Parameters.AddWithValue("@rf", refmax + 1);
                        cmd.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);

                       // cmd.Parameters.AddWithValue("@r1", reff);
                        cmd.Parameters.AddWithValue("@r0", (chk_agel.Checked && !string.IsNullOrEmpty(txt_custno.Text)) ? 2 : 0);

                        cmd.Parameters.AddWithValue("@c1", row.Cells[0].Value);
                        cmd.Parameters.AddWithValue("@c2", row.Cells[2].Value.ToString().Length > 100 ? row.Cells[2].Value.ToString().Substring(0, 100) : row.Cells[2].Value);
                        cmd.Parameters.AddWithValue("@c3", row.Cells[3].Value);
                        cmd.Parameters.AddWithValue("@c4", row.Cells[5].Value);
                        cmd.Parameters.AddWithValue("@c5", row.Cells[4].Value);
                        cmd.Parameters.AddWithValue("@c6", row.Cells[6].Value);
                      //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                        if (row.Cells[7].Value != null)
                        { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                       // cmd.Parameters.AddWithValue("@c9", comp_id);
                     //   cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                        cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                        cmd.Parameters.AddWithValue("@sn", srno);
                        cmd.Parameters.AddWithValue("@ta", string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : Math.Round((Convert.ToDouble(row.Cells[8].Value) * Convert.ToDouble(BL.CLS_Session.tax_per) / (100 + Convert.ToDouble(BL.CLS_Session.tax_per))),4));
                        cmd.ExecuteNonQuery();

                        //lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);

                    }
                    srno++;
                }
            }
          //  lblref.Text = reff.ToString();
            lblref.Text = (refmax + 1).ToString();
            refmax++;
            chk_iscard.Checked = false;
          //  txt_desc.Text = "";
            label1.Text = "0";
            txt_custno.Text = "";
            txt_invbar.Text = "";
            txt_custnam.Text = "";
            txt_driver.Text = "";
            chk_agel.Checked = false;
            at.txt_tameemamt.Text = "";
            at.txt_tameennot.Text = "";
            txt_dscper.Text = "0";
            txt_temp.Text = "";
            button6_Click(sender, e);
            txt_driver_Leave(sender, e);
            total();
        }


        private void SavePrint_Click(object sender, EventArgs e)
        {
            total();
            total();

            if (string.IsNullOrEmpty(txt_invbar.Text))
            {
                MessageBox.Show((BL.CLS_Session.lang.Equals("E") ? "must enter sale invoice number when return" : "يجب ادخال رقم فاتورة البيع عند الارجاع"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_invbar.Focus();
                return;
            }

            if (button22.BackColor == Color.GreenYellow)
                textBox1.Text = label1.Text;

            if (textBox1.Text.Equals(""))
                textBox1.Text = (Convert.ToDouble(label1.Text) - (txt_desc.Text.Equals("") ? 0 : Convert.ToDouble(txt_desc.Text))).ToString();
            else
            {
                if(Convert.ToDouble(textBox1.Text)==0)
                    textBox1.Text = (Convert.ToDouble(label1.Text) - (txt_desc.Text.Equals("") ? 0 : Convert.ToDouble(txt_desc.Text))).ToString();// label1.Text;
            }
          //  Save_btn_Click(sender, e);
            try
            {
                //if (checkBox1.Checked == false)
                
                //{
                if (textBox1.Text != "" && dataGridView1.Rows.Count > 1)
                {



                    if (Convert.ToDouble(textBox1.Text) >= (Convert.ToDouble(label1.Text) - Convert.ToDouble(string.IsNullOrEmpty(txt_desc.Text) ? "0" : txt_desc.Text)))
                    {
                      //////////  double sum = 0;
                      //////////  for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                      //////////  {
                      //////////      if (dataGridView1.Rows[i].Cells[7].Value != null)
                      //////////      {
                      //////////          sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value);
                      //////////      }
                      //////////  }

                      //////////  if (sum > 0)
                      //////////  {
                      //////////      SqlDataAdapter da = new SqlDataAdapter("select isnull(max(req_no),0) from sales_hdr", con2);
                      //////////      DataTable dt = new DataTable();
                      //////////      da.Fill(dt);
                      //////////      int req_max = Convert.ToInt32(dt.Rows[0][0]);

                      //////////      using (SqlCommand cmd1 = new SqlCommand("INSERT INTO sales_hdr (type,date,total, count,payed,total_cost,contr,saleman,req_no,cust_no) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)", con2))
                      //////////      {
                                
                      //////////          cmd1.Parameters.AddWithValue("@a0", typeno);
                                 

                      //////////          cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                      //////////          // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                      //////////          cmd1.Parameters.AddWithValue("@a2", Convert.ToInt32(label1.Text));
                      //////////          cmd1.Parameters.AddWithValue("@a3", Convert.ToInt32(label9.Text));
                      //////////          cmd1.Parameters.AddWithValue("@a4", Convert.ToInt32(textBox1.Text));
                      //////////          cmd1.Parameters.AddWithValue("@a5", Convert.ToInt32(textBox2.Text));
                      //////////          cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                      //////////          cmd1.Parameters.AddWithValue("@a7", lbluser.Text);
                      //////////          cmd1.Parameters.AddWithValue("@a8", req_max + 1);
                      //////////          cmd1.Parameters.AddWithValue("@a9", Convert.ToInt32(label3.Text)); 
                      //////////          cmd1.ExecuteNonQuery();
                      //////////      }
                      //////////  }
                      //////////  else
                      //////////  {
                      //////////      using (SqlCommand cmd1 = new SqlCommand("INSERT INTO sales_hdr (type,date,total, count,payed,total_cost,contr,saleman,cust_no) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a9)", con2))
                      //////////      {

                      //////////          cmd1.Parameters.AddWithValue("@a0", typeno);

                                
                      //////////          //        cmd1.Parameters.AddWithValue("@a0", 4);
                                  

                      //////////          cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                      //////////          // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                      //////////          cmd1.Parameters.AddWithValue("@a2", Convert.ToInt32(label1.Text));
                      //////////          cmd1.Parameters.AddWithValue("@a3", Convert.ToInt32(label9.Text));
                      //////////          cmd1.Parameters.AddWithValue("@a4", Convert.ToInt32(textBox1.Text));
                      //////////          cmd1.Parameters.AddWithValue("@a5", Convert.ToInt32(textBox2.Text));
                      //////////          cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                      //////////          cmd1.Parameters.AddWithValue("@a7", lbluser.Text);
                      //////////         // cmd1.Parameters.AddWithValue("@a8", req_max + 1);
                      //////////          cmd1.Parameters.AddWithValue("@a9", Convert.ToInt32(label3.Text)); 
                      //////////          cmd1.ExecuteNonQuery();
                      //////////      }

                      //////////  }


                      //////////  foreach (DataGridViewRow row in dataGridView1.Rows)
                      //////////  {
                      //////////      // SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from DB.dbo.sales_hdr", con2);
                      //////////      SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from sales_hdr", con2);
                      //////////      DataSet dss = new DataSet();
                      //////////      daa.Fill(dss, "0");

                      //////////      if (!row.IsNewRow && row.Cells[0].Value != null)
                      //////////      {
                      //////////          // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                      //////////          using (SqlCommand cmd = new SqlCommand("INSERT INTO sales_dtl (ref,type, barcode, name, unit, price, qty, cost,contr,is_req) VALUES(@r1,@r0,@c1,@c2,@c3,@c4,@c5,@c6,@c7,@c8)", con2))
                      //////////          {
                      //////////              cmd.Parameters.AddWithValue("@r1", dss.Tables[0].Rows[0][0]);
                      //////////              cmd.Parameters.AddWithValue("@r0", typeno);

                                   
                      //////////              cmd.Parameters.AddWithValue("@c1", row.Cells[0].Value);
                      //////////              cmd.Parameters.AddWithValue("@c2", row.Cells[2].Value);
                      //////////              cmd.Parameters.AddWithValue("@c3", row.Cells[3].Value);
                      //////////              cmd.Parameters.AddWithValue("@c4", row.Cells[5].Value);
                      //////////              cmd.Parameters.AddWithValue("@c5", row.Cells[4].Value);
                      //////////              cmd.Parameters.AddWithValue("@c6", row.Cells[6].Value);
                      //////////              cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                      //////////              if (row.Cells[7].Value != null)
                      //////////              { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                      //////////              cmd.ExecuteNonQuery();

                      //////////              lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);

                      //////////          }
                      //////////      }
                      //////////  }

                      //////////  //DialogResult result = MessageBox.Show("تم حفظ الفاتورة ....... هل تريد طباعة الفاتورة؟", "تاكيد", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                      //////////  //if (result == DialogResult.Yes)
                      //////////  //{
                      //////////  //    Print_btn_Click(sender, e);
                      //////////  //}
                      //////////  //else if (result == DialogResult.No)
                      //////////  //{
                      //////////  //    //...
                      //////////  //}
                      //////////  //else
                      //////////  //{
                      //////////  //    //...
                      //////////  //} 
                      //////////  //Print_btn_Click(sender, e);
                        


                      ////////// // using (rest demo = new Form1())

                        
                        

                      //////////  dataGridView1.Rows.Clear();
                      //////////  // MessageBox.Show(label5.Text);
                      //////////  total();
                      //////////  textBox1.Text = "";
                      //////////  // dataGridView1.Rows.Add(100);
                      //////////  // dataGridView1.TabIndex = 0;
                      //////////  // dataGridView1.Focus();
                      //////////  // textBox1.Text = "";
                      //////////  // textBox1.BackColor = Color.White;

                      //////////  //  Report1 report1 = new Report1();
                      //////////  //    report1.Show();



                      ////////////  Run();

                        total();
                        Save_Only(sender, e);
                        dataGridView1.Rows.Clear();
                        // MessageBox.Show(label5.Text);
                       // total();
                        textBox1.Text = "";
                        txt_desc.Text = "";
                        // dataGridView1.Rows.Add(100);
                        // dataGridView1.TabIndex = 0;
                        // dataGridView1.Focus();
                        // textBox1.Text = "";
                        // textBox1.BackColor = Color.White;

                        //  Report1 report1 = new Report1();
                        //    report1.Show();



                        //  Run();


                       // button5_Click((object)sender, (EventArgs)e);

                        Print_btn_Click(sender, e);
                    }
                    else
                    {
                        if (label12.Text != "." && Convert.ToDouble(textBox1.Text) >= 0)
                            
                        {
                            // textBox1.Text = "0";
                            //////////double sum = 0;
                            //////////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                            //////////{
                            //////////    if (dataGridView1.Rows[i].Cells[7].Value != null)
                            //////////    {
                            //////////        sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value);
                            //////////    }
                            //////////}

                            //////////if (sum > 0)
                            //////////{
                            //////////    SqlDataAdapter da = new SqlDataAdapter("select isnull(max(req_no),0) from sales_hdr", con2);
                            //////////    DataTable dt = new DataTable();
                            //////////    da.Fill(dt);
                            //////////    int req_max = Convert.ToInt32(dt.Rows[0][0]);

                            //////////    using (SqlCommand cmd1 = new SqlCommand("INSERT INTO sales_hdr (type,date,total, count,payed,total_cost,contr,saleman,req_no,cust_no) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)", con2))
                            //////////    {

                            //////////        cmd1.Parameters.AddWithValue("@a0", typeno);


                            //////////        cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                            //////////        // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                            //////////        cmd1.Parameters.AddWithValue("@a2", Convert.ToInt32(label1.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a3", Convert.ToInt32(label9.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a4", Convert.ToInt32(textBox1.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a5", Convert.ToInt32(textBox2.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a7", lbluser.Text);
                            //////////        cmd1.Parameters.AddWithValue("@a8", req_max + 1);
                            //////////        cmd1.Parameters.AddWithValue("@a9", Convert.ToInt32(label3.Text));
                            //////////        cmd1.ExecuteNonQuery();
                            //////////    }
                            //////////}
                            //////////else
                            //////////{
                            //////////    using (SqlCommand cmd1 = new SqlCommand("INSERT INTO sales_hdr (type,date,total, count,payed,total_cost,contr,saleman,cust_no) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a9)", con2))
                            //////////    {

                            //////////        cmd1.Parameters.AddWithValue("@a0", typeno);


                            //////////        //        cmd1.Parameters.AddWithValue("@a0", 4);


                            //////////        cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                            //////////        // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                            //////////        cmd1.Parameters.AddWithValue("@a2", Convert.ToInt32(label1.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a3", Convert.ToInt32(label9.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a4", Convert.ToInt32(textBox1.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a5", Convert.ToInt32(textBox2.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a7", lbluser.Text);
                            //////////        // cmd1.Parameters.AddWithValue("@a8", req_max + 1);
                            //////////        cmd1.Parameters.AddWithValue("@a9", Convert.ToInt32(label3.Text));
                            //////////        cmd1.ExecuteNonQuery();
                            //////////    }

                            //////////}


                            //////////foreach (DataGridViewRow row in dataGridView1.Rows)
                            //////////{
                            //////////    // SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from DB.dbo.sales_hdr", con2);
                            //////////    SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from sales_hdr", con2);
                            //////////    DataSet dss = new DataSet();
                            //////////    daa.Fill(dss, "0");

                            //////////    if (!row.IsNewRow && row.Cells[0].Value != null)
                            //////////    {
                            //////////        // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                            //////////        using (SqlCommand cmd = new SqlCommand("INSERT INTO sales_dtl (ref,type, barcode, name, unit, price, qty, cost,contr,is_req) VALUES(@r1,@r0,@c1,@c2,@c3,@c4,@c5,@c6,@c7,@c8)", con2))
                            //////////        {
                            //////////            cmd.Parameters.AddWithValue("@r1", dss.Tables[0].Rows[0][0]);
                            //////////            cmd.Parameters.AddWithValue("@r0", typeno);


                            //////////            cmd.Parameters.AddWithValue("@c1", row.Cells[0].Value);
                            //////////            cmd.Parameters.AddWithValue("@c2", row.Cells[2].Value);
                            //////////            cmd.Parameters.AddWithValue("@c3", row.Cells[3].Value);
                            //////////            cmd.Parameters.AddWithValue("@c4", row.Cells[5].Value);
                            //////////            cmd.Parameters.AddWithValue("@c5", row.Cells[4].Value);
                            //////////            cmd.Parameters.AddWithValue("@c6", row.Cells[6].Value);
                            //////////            cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                            //////////            if (row.Cells[7].Value != null)
                            //////////            { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                            //////////            cmd.ExecuteNonQuery();

                            //////////            lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);

                            //////////        }
                            //////////    }
                            //////////}


                            //DialogResult result = MessageBox.Show("تم حفظ الفاتورة ....... هل تريد طباعة الفاتورة؟", "تاكيد", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                            //if (result == DialogResult.Yes)
                            //{
                            //    Print_btn_Click(sender, e);
                            //}
                            //else if (result == DialogResult.No)
                            //{
                            //    //...
                            //}
                            //else
                            //{
                            //    //...
                            //} 
                            //Print_btn_Click(sender, e);



                            // using (rest demo = new Form1())
                            total();
                            Save_Only(sender, e);


                            dataGridView1.Rows.Clear();
                            // MessageBox.Show(label5.Text);
                           // total();
                            textBox1.Text = "";
                            txt_desc.Text = "";
                            // dataGridView1.Rows.Add(100);
                            // dataGridView1.TabIndex = 0;
                            // dataGridView1.Focus();
                            // textBox1.Text = "";
                            // textBox1.BackColor = Color.White;

                            //  Report1 report1 = new Report1();
                            //    report1.Show();



                            //  Run();


                           // button5_Click((object)sender, (EventArgs)e);

                            Print_btn_Click(sender, e);
                            txt_desc.Text = "";
                        }
                        else
                        {

                           MetroMessageBox.Show(this,"يجب دفع المبلغ كاملا", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // textBox1.Text = "";
                            //textBox1.Focus();
                        }
                    }
                }
                else
                {
                   MetroMessageBox.Show(this,"يرجى ادخال مبلغ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //textBox1.Text = "";
                    //textBox1.Focus();
                }

                
            }
            //    }

            //    else
            //    {
            //        if (textBox1.Text != "")
            //        {
            //            if (Convert.ToInt32(textBox1.Text) >= Convert.ToInt32(label1.Text))
            //            {
            //                SqlDataAdapter da = new SqlDataAdapter("select isnull(max(req_no),0) from sales_hdr", con2);
            //                DataTable dt = new DataTable();
            //                da.Fill(dt);
            //                int req_max = Convert.ToInt32(dt.Rows[0][0]);
            //                // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.sales_hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
            //                using (SqlCommand cmd1 = new SqlCommand("INSERT INTO sales_hdr (type,date,total, count,payed,total_cost,contr,saleman,req_no) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8)", con2))
            //                {

            //                    cmd1.Parameters.AddWithValue("@a0", 1);
            //                    cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
            //                    // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
            //                    cmd1.Parameters.AddWithValue("@a2", Convert.ToInt32(label1.Text));
            //                    cmd1.Parameters.AddWithValue("@a3", Convert.ToInt32(label9.Text));
            //                    cmd1.Parameters.AddWithValue("@a4", Convert.ToInt32(textBox1.Text));
            //                    cmd1.Parameters.AddWithValue("@a5", Convert.ToInt32(textBox2.Text));
            //                    cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
            //                    cmd1.Parameters.AddWithValue("@a7", lbluser.Text);
            //                    cmd1.Parameters.AddWithValue("@a8", req_max+1);
            //                    cmd1.ExecuteNonQuery();
            //                }


            //                foreach (DataGridViewRow row in dataGridView1.Rows)
            //                {
            //                    // SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from DB.dbo.sales_hdr", con2);
            //                    SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from sales_hdr", con2);
            //                    DataSet dss = new DataSet();
            //                    daa.Fill(dss, "0");

            //                    if (!row.IsNewRow && row.Cells[0].Value != null)
            //                    {
            //                        // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
            //                        using (SqlCommand cmd = new SqlCommand("INSERT INTO sales_dtl (ref,type, barcode, name, unit, price, qty, cost,contr) VALUES(@r1,@r0,@c1,@c2,@c3,@c4,@c5,@c6,@c7)", con2))
            //                        {
            //                            cmd.Parameters.AddWithValue("@r1", dss.Tables[0].Rows[0][0]);
            //                            cmd.Parameters.AddWithValue("@r0", 1);
            //                            cmd.Parameters.AddWithValue("@c1", row.Cells[0].Value);
            //                            cmd.Parameters.AddWithValue("@c2", row.Cells[2].Value);
            //                            cmd.Parameters.AddWithValue("@c3", row.Cells[3].Value);
            //                            cmd.Parameters.AddWithValue("@c4", row.Cells[5].Value);
            //                            cmd.Parameters.AddWithValue("@c5", row.Cells[4].Value);
            //                            cmd.Parameters.AddWithValue("@c6", row.Cells[6].Value);
            //                            cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
            //                            cmd.ExecuteNonQuery();

            //                            lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);

            //                        }
            //                    }
            //                }
            //                dataGridView1.Rows.Clear();
            //                // MessageBox.Show(label5.Text);
            //                total();
            //                textBox1.Text = "";
            //                // dataGridView1.Rows.Add(100);
            //                // dataGridView1.TabIndex = 0;
            //                // dataGridView1.Focus();
            //                // textBox1.Text = "";
            //                // textBox1.BackColor = Color.White;

            //                //  Report1 report1 = new Report1();
            //                //    report1.Show();


            //                SqlDataAdapter dacr = new SqlDataAdapter("select * from sales_dtl where ref=(select max(ref) from sales_dtl)", con2);
            //                SqlDataAdapter dacr1 = new SqlDataAdapter("select * from sales_hdr where ref=(select max(ref) from sales_hdr)", con2);
            //                DataSet1 ds = new DataSet1();

            //                dacr.Fill(ds, "sales_dtl");
            //                dacr1.Fill(ds, "sales_hdr");


            //               // reports.SalesReport report = new reports.SalesReport();
            //                reports.SalesReport_req report=new reports.SalesReport_req();
            //                //  CrystalReport1 report = new CrystalReport1();
            //                report.SetDataSource(ds);

            //                //    crystalReportViewer1.ReportSource = report;

            //                //  crystalReportViewer1.Refresh();

            //                report.PrintOptions.PrinterName = "pos";

            //                report.PrintToPrinter(1, false, 0, 0);


            //            }
            //            else
            //            {
            //                MessageBox.Show("يجب دفع المبلغ كاملا", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                // textBox1.Text = "";
            //                //textBox1.Focus();
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("يرجى ادخال مبلغ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            //textBox1.Text = "";
            //            //textBox1.Focus();
            //        }



            //    }


            //}
            catch (Exception ex)
            {
               MetroMessageBox.Show(this,ex.Message);
            }
            

        }
        private void hight()
        {
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    row.Height = 30;
            //}
            try
            {
                int x = dataGridView1.RowCount;
                dataGridView1.Rows[x - 2].Height = 40;
            }
            catch { }

        }
        

        //Control focusedCtrl;
        ////Enter event handler for all your TextBoxes
        //private void TextBoxesEnter(object sender, EventArgs e)
        //{
        //    focusedCtrl = sender as TextBox;
        //}
        //Click event handler for your btnNum1
        
        private void button4_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
               // textBox1.Text = null;
                textBox1.Text = textBox1.Text + button31.Text;
              //  return;
            }
            if (flag == 2)
            {
               // textBox2.Text.Remove(0, textBox2.Text.Length);
                txt_desc.Text = txt_desc.Text + button31.Text;
              //  return;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            flag = 1;
           // input.Remove(0, input.Length);
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            flag = 2;        //when select textbox2,change   flag =2
           // input.Remove(0, input.Length);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            //cur_row = dataGridView1.CurrentCell.RowIndex;
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cur_row = dataGridView1.CurrentCell.RowIndex;

                if (dataGridView1.Columns[e.ColumnIndex].Name == "Del")
                {
                    dataGridView1.Rows.RemoveAt(cur_row);
                    total();
                }
            }
            catch (Exception ex)
            {
              // MetroMessageBox.Show(this,ex.ToString());
            }
        }

        private void print_ref()
        {
            if (lblref.Text != "")
            {
                int toprt = Convert.ToInt32(lblref.Text);


                SqlDataAdapter dacr = new SqlDataAdapter("select * from pos_dtl where ref=" + toprt, con2);
                SqlDataAdapter dacr1 = new SqlDataAdapter("select * from pos_hdr where ref=" + toprt, con2);
                SqlDataAdapter chk = new SqlDataAdapter("select sum(isnull(is_req,0)) from pos_dtl where ref=" + toprt, con2);

                DataSet1 ds = new DataSet1();

                ds.Tables["dtl"].Clear();
                ds.Tables["hdr"].Clear();
                ds.Tables["sum"].Clear();

                dacr.Fill(ds, "dtl");
                dacr1.Fill(ds, "hdr");
                chk.Fill(ds, "sum");

               // label5.Text = (Convert.ToDouble(ds.Tables["hdr"].Rows[0]["net_total"]) - Convert.ToDouble(ds.Tables["hdr"].Rows[0]["net_total"])).ToString() + "   " + "المتبقي للزبون";
                // DataTable dtchk = new DataTable();
                //// dtchk.Clear();

                // chk.Fill(dtchk);

                // var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\temp.txt");
                // string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\printers.txt");

                CrystalDecisions.CrystalReports.Engine.ReportDocument report1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                if (File.Exists(Directory.GetCurrentDirectory() + @"\reports\SalesReport.rpt"))
                {
                    string filePath = Directory.GetCurrentDirectory() + @"\reports\SalesReport.rpt";
                    report1.Load(filePath);
                    report1.SetDataSource(ds);
                    report1.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                    report1.SetParameterValue("br_name", BL.CLS_Session.brname);
                    report1.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
                    report1.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                    report1.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
                    // Use a tab to indent each line of the file.
                    // Console.WriteLine("\t" + line);
                    foreach (string line in lines_prt)
                    {
                        report1.PrintOptions.PrinterName = line;

                        report1.PrintToPrinter(1, false, 0, 0);
                        // report.PrintToPrinter(0,true, 1, 1);

                    }
                    report1.Close();
                }
                else
                {
                    rpt.SalesReport report = new rpt.SalesReport();
                    // report.SetParameterValue("Comp_Name", cmp_name);

                    // report.DataDefinition.FormulaFields["Comp_Name"].Text = " '" + cmp_name + "'";
                    report.SetDataSource(ds);
                    report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                    report.SetParameterValue("br_name", BL.CLS_Session.brname);
                    report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
                    report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                    report.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());

                    //  CrystalReport1 report = new CrystalReport1();
                    //  report.SetDataSource(ds);

                    //  report.SetParameterValue("Comp_Name", cmp_name);
                    //    crystalReportViewer1.ReportSource = report;

                    //  crystalReportViewer1.Refresh();
                    /*
                    report.PrintOptions.PrinterName = "pos";

                    report.PrintToPrinter(1, false, 0, 0);
                    // report.PrintToPrinter(0, true, 1, 1);
                    report.Close();
                    // report.Dispose();
                     * */

                    foreach (string line in lines_prt)
                    {
                        // Use a tab to indent each line of the file.
                        // Console.WriteLine("\t" + line);


                        report.PrintOptions.PrinterName = line;

                        report.PrintToPrinter(1, false, 0, 0);
                        // report.PrintToPrinter(0,true, 1, 1);

                        // report.Dispose();
                    }
                    report.Close();
                }
                //   crystalReportViewer.ReportSource = reportDocument; 


                //////// if (ds.Tables["sum"].Rows.Count > 0 && Convert.ToInt32(ds.Tables["sum"].Rows[0][0]) > 0)
                ////// if (Convert.ToInt32(ds.Tables["sum"].Rows[0][0]) > 0)
                ////// {
                //////     rpt.SalesReport_req report = new rpt.SalesReport_req();
                //////    // report.SetParameterValue("Comp_Name", cmp_name);
                //////    // report.DataDefinition.FormulaFields["Comp_Name"].Text = " '" + cmp_name + "'";

                //////     //  CrystalReport1 report = new CrystalReport1();
                //////    // report.SetDataSource(ds);
                //////    // report.SetParameterValue("Comp_Name", cmp_name);
                //////     //    crystalReportViewer1.ReportSource = report;

                //////     //  crystalReportViewer1.Refresh();

                //////     foreach (string line in lines_prt)
                //////     {
                //////         report.SetDataSource(ds);
                //////         report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                //////         report.SetParameterValue("br_name", BL.CLS_Session.brname);
                //////         report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
                //////         report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                //////         // Use a tab to indent each line of the file.
                //////        // Console.WriteLine("\t" + line);
                //////         report.PrintOptions.PrinterName = line;

                //////         report.PrintToPrinter(1, false, 0, 0);
                //////         // report.PrintToPrinter(0,true, 1, 1);
                //////         report.Close();
                //////         // report.Dispose();
                //////     }
                //////   //  report.PrintOptions.PrinterName = "pos";

                //////   //  report.PrintToPrinter(1, false, 0, 0);
                //////     // report.PrintToPrinter(0,true, 1, 1);
                //////   //  report.Close();
                //////     // report.Dispose();
                ////// }
                ////// else
                ////// {
                //////     // reports.SalesReport report = new reports.SalesReport();

                //////     rpt.SalesReport report = new rpt.SalesReport();
                //////    // report.SetParameterValue("Comp_Name", cmp_name);

                //////    // report.DataDefinition.FormulaFields["Comp_Name"].Text = " '" + cmp_name + "'";



                //////     //  CrystalReport1 report = new CrystalReport1();
                //////   //  report.SetDataSource(ds);

                //////   //  report.SetParameterValue("Comp_Name", cmp_name);
                //////     //    crystalReportViewer1.ReportSource = report;

                //////     //  crystalReportViewer1.Refresh();
                //////     /*
                //////     report.PrintOptions.PrinterName = "pos";

                //////     report.PrintToPrinter(1, false, 0, 0);
                //////     // report.PrintToPrinter(0, true, 1, 1);
                //////     report.Close();
                //////     // report.Dispose();
                //////      * */

                //////    foreach (string line in lines_prt)
                //////    {
                //////        // Use a tab to indent each line of the file.
                //////        // Console.WriteLine("\t" + line);
                //////        report.SetDataSource(ds);
                //////        report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                //////        report.SetParameterValue("br_name", BL.CLS_Session.brname);
                //////        report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
                //////        report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());

                //////        report.PrintOptions.PrinterName = line;

                //////        report.PrintToPrinter(1, false, 0, 0);
                //////        // report.PrintToPrinter(0,true, 1, 1);
                //////        report.Close();
                //////        // report.Dispose();
                //////    }
                //////}

                if (prttype == "1")
                {
                    ////////PRT_DIALOG prtd = new PRT_DIALOG();
                    ////////prtd.ShowDialog();
                    new PRT_DIALOG(lblref.Text).ShowDialog();
                }
            }
            else
            {
                MetroMessageBox.Show(this, "لا يوجد فاتورة");
            }


            /*
            BL.CLS_PRINT clsprt = new BL.CLS_PRINT();

            clsprt.Run();
             * */

            /*
            if (lblref.Text == "")
            {
                MessageBox.Show("لا توجد فاتورة للطباعة");
            }
            else
            {
                Run();
            }
             * */
           
        }

        private void print_ref_fr()
        {
            if (string.IsNullOrEmpty(lblref.Text))
            {
                MessageBox.Show("لا يوجد فاتورة للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty] from pos_dtl where ref=(select max(ref) from pos_dtl)", con2);
          //  string chktyp1 = isocu ? " r_pos_hdr " : " pos_hdr ";
          //  string chktyp2 = isocu ? " r_pos_dtl " : " pos_dtl ";
            // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from pos_hdr where ref=(select max(ref) from pos_hdr)", con2);//" + lblref.Text + ""
             dacr1 = new SqlDataAdapter("select a.[brno],a.[slno],a.[ref],a.[contr],a.[type],FORMAT (a.[date], 'dd-MM-yyyy hh:mm:ss tt', 'en-us') date,a.[total],a.[count],a.[payed],a.[total_cost],b.sl_name [saleman],a.[req_no],a.[cust_no],a.[discount],a.[net_total],a.[sysdate],a.[gen_ref],a.[tax_amt],a.[card_amt],a.[note],a.[mobile] from pos_hdr a join pos_salmen b on a.saleman=b.sl_id where a.ref=" + lblref.Text + " and a.[contr]=" + BL.CLS_Session.ctrno + " and  a.[brno]= '" + BL.CLS_Session.brno + "' ", con2);
             dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty],[offr_amt] from pos_dtl where ref=" + lblref.Text + " and [contr]=" + BL.CLS_Session.ctrno + " and  [brno]= '" + BL.CLS_Session.brno + "' order by srno ", con2);
             dacr2 = new SqlDataAdapter("select dnote,dtameen,tameen_note from pos_hdr_r where dref=" + lblref.Text + " and dcontr=" + BL.CLS_Session.ctrno + " and  dbrno= '" + BL.CLS_Session.brno + "' ", con2);
            
            ds = new DataSet1();
            dtn = new DataTable();
            dacr1.Fill(ds, "hdr");
            dacr.Fill(ds, "dtl");
            dacr2.Fill(dtn);

            if (ds.Tables["hdr"].Rows.Count == 0)
            {
                MessageBox.Show("لا يوجد فاتورة للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FastReport.Report rpt = new FastReport.Report();

           // if (isocu == false)
            rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_RSale_rpt.frx");
          //  else
          //      rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_Ocu_rpt.frx");
            //  rpt.SetParameterValue("br_name", BL.CLS_Session.brname);
            rpt.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
            rpt.SetParameterValue("Br_Name", BL.CLS_Session.brname);
            //rpt.SetParameterValue("inv_bar", ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString());
            rpt.SetParameterValue("inv_bar", ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString());
            rpt.SetParameterValue("Msg1", BL.CLS_Session.msg1.ToString());
            rpt.SetParameterValue("Msg2", BL.CLS_Session.msg2.ToString());
            rpt.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
            rpt.SetParameterValue("tax_type", BL.CLS_Session.autotax ? "3" : BL.CLS_Session.isshamltax.Equals("2") ? "2" : "0");
            rpt.SetParameterValue("dnote", dtn.Rows.Count > 0 ? dtn.Rows[0][0].ToString() : "");
            rpt.SetParameterValue("dtameen", dtn.Rows.Count > 0 ? dtn.Rows[0][1].ToString() : "");
            rpt.SetParameterValue("tameen_note", dtn.Rows.Count > 0 ? dtn.Rows[0][2].ToString() : "");
            //rpt.SetParameterValue("type", ds.Tables["hdr"].Rows[0][4].ToString());
            rpt.SetParameterValue("type", ds.Tables["hdr"].Rows[0][4].ToString());
            //string dtt = Convert.ToDateTime(ds.Tables["hdr"].Rows[0]["date"].ToString()).ToString("yyyy-MM-ddTHH:mm:ssZ", new CultureInfo("en-US", false));
            ////////var tlvs = new GenerateQrCode.GenerateQrCodeTLV(
            ////////              BL.CLS_Session.cmp_ename,
            ////////               BL.CLS_Session.tax_no,
            ////////               dtt,
            ////////               ds.Tables["hdr"].Rows[0]["net_total"].ToString(),
            ////////              Math.Round(Convert.ToDouble(ds.Tables["hdr"].Rows[0]["tax_amt"].ToString()), 2).ToString());

            ////////rpt.SetParameterValue("qr", Convert.ToBase64String(tlvs.GetQRCode()));
            //string tlvs = new BL.qr_ar().GenerateQrCodeTLV(BL.CLS_Session.cmp_name, BL.CLS_Session.tax_no, dtt, Convert.ToDouble(ds.Tables["hdr"].Rows[0]["net_total"].ToString()), Convert.ToDouble(Math.Round(Convert.ToDouble(ds.Tables["hdr"].Rows[0]["tax_amt"].ToString()), 2).ToString()));
            //rpt.SetParameterValue("qr", tlvs);
            string dtt = Convert.ToDateTime(ds.Tables["hdr"].Rows[0]["date"].ToString()).ToString("yyyy-MM-ddTHH:mm:ssZ", new CultureInfo("en-US", false));
            var tlvs = new GenerateQrCode.GenerateQrCodeTLV(
                          BL.CLS_Session.cmp_ename,
                           BL.CLS_Session.tax_no,
                           dtt,
                           ds.Tables["hdr"].Rows[0]["net_total"].ToString(),
                          Math.Round(Convert.ToDouble(ds.Tables["hdr"].Rows[0]["tax_amt"].ToString()), 2).ToString());

            rpt.SetParameterValue("qr", Convert.ToBase64String(tlvs.GetQRCode()));
            rpt.RegisterData(ds.Tables["hdr"], "pos_hdr");
            rpt.RegisterData(ds.Tables["dtl"], "pos_dtl");
            // rpt.PrintSettings.ShowDialog = false;
            // rpt.PrintSettings.Copies =Convert.ToInt32(txt_count.Text);

            // rpt.Print();

            int iii = 0;
            foreach (string line in lines_prt)
            {
              //  MessageBox.Show(iii.ToString());
                rpt.PrintSettings.Printer = iii==0 ? printer_nam : line;// "pos";
                rpt.PrintSettings.ShowDialog = false;
                // FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                // rpt.PrintSettings.Copies =Convert.ToInt32(txt_count.Text);
                rpt.Print();
                iii++;
            }

           // print_subprntr();

            if (prttype == "1")
            {
                ////////PRT_DIALOG prtd = new PRT_DIALOG();
                ////////prtd.ShowDialog();
                new PRT_DIALOG(lblref.Text).ShowDialog();
            }
        }

        private void print_subprntr()
        {
           // DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("SELECT printer FROM groups where printer<>'' and inactive=0 group by printer");
            DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select g.printer from pos_dtl join items b on  pos_dtl.itemno=b.item_no join groups g on b.item_group=g.group_id and g.printer<>'' and g.inactive=0 where ref=" + ds.Tables["hdr"].Rows[0][2].ToString() + " and [contr]=" + BL.CLS_Session.ctrno + " and [brno]= '" + BL.CLS_Session.brno + "' group by g.printer");
            foreach (DataRow dtr in dt.Rows)
            {
               //  dacr1 = new SqlDataAdapter("select a.[brno],a.[slno],a.[ref],a.[contr],a.[type],FORMAT (a.[date], 'dd-MM-yyyy hh:mm:ss tt', 'en-us') date,a.[total],a.[count],a.[payed],a.[total_cost],b.sl_name [saleman],a.[req_no],a.[cust_no],a.[discount],a.[net_total],a.[sysdate],a.[gen_ref],a.[tax_amt],a.[card_amt],a.[note],a.[mobile] from pos_hdr a join pos_salmen b on a.saleman=b.sl_id where a.ref=" + lblref.Text + " and a.[contr]=" + BL.CLS_Session.ctrno + " and  a.[brno]= '" + BL.CLS_Session.brno + "' ", con2);
                 dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty],[offr_amt] from pos_dtl join items b on  pos_dtl.itemno=b.item_no join groups g on b.item_group=g.group_id and g.printer='" + dtr[0].ToString() + "' where ref=" + lblref.Text + " and [contr]=" + BL.CLS_Session.ctrno + " and  [brno]= '" + BL.CLS_Session.brno + "' ", con2);
                // dacr2 = new SqlDataAdapter("select dnote,dtameen,tameen_note from pos_hdr_r where dref=" + lblref.Text + " and dcontr=" + BL.CLS_Session.ctrno + " and  dbrno= '" + BL.CLS_Session.brno + "' ", con2);

                ds = new DataSet1();
                dtn = new DataTable();
                dacr1.Fill(ds, "hdr");
                dacr.Fill(ds, "dtl");
                dacr2.Fill(dtn);

                if (ds.Tables["hdr"].Rows.Count == 0)
                {
                   // MessageBox.Show("لا يوجد فاتورة للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FastReport.Report rpt = new FastReport.Report();

                // if (isocu == false)
                rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_sub_rpt.frx");
                //  else
                //      rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_Ocu_rpt.frx");
                //  rpt.SetParameterValue("br_name", BL.CLS_Session.brname);
                rpt.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                rpt.SetParameterValue("Br_Name", BL.CLS_Session.brname);
                //rpt.SetParameterValue("inv_bar", ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString());
                rpt.SetParameterValue("inv_bar", ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString());
                rpt.SetParameterValue("Msg1", BL.CLS_Session.msg1.ToString());
                rpt.SetParameterValue("Msg2", BL.CLS_Session.msg2.ToString());
                rpt.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
                rpt.SetParameterValue("tax_type", BL.CLS_Session.autotax ? "3" : BL.CLS_Session.isshamltax.Equals("2") ? "2" : "0");
                rpt.SetParameterValue("dnote", dtn.Rows.Count > 0 ? dtn.Rows[0][0].ToString() : "");
                rpt.SetParameterValue("dtameen", dtn.Rows.Count > 0 ? dtn.Rows[0][1].ToString() : "");
                rpt.SetParameterValue("tameen_note", dtn.Rows.Count > 0 ? dtn.Rows[0][2].ToString() : "");

                rpt.RegisterData(ds.Tables["hdr"], "pos_hdr");
                rpt.RegisterData(ds.Tables["dtl"], "pos_dtl");
                // rpt.PrintSettings.ShowDialog = false;
                // rpt.PrintSettings.Copies =Convert.ToInt32(txt_count.Text);

                // rpt.Print();

                //int iii = 0;
                //foreach (string line in lines_prt)
                //{
                    //  MessageBox.Show(iii.ToString());
                    rpt.PrintSettings.Printer = dtr[0].ToString();// "pos";
                    rpt.PrintSettings.ShowDialog = false;
                    // FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                    // rpt.PrintSettings.Copies =Convert.ToInt32(txt_count.Text);
                    rpt.Print();
                    //iii++;
                //}

            }
        }

        private void Print_btn_Click(object sender, EventArgs e)
        {
           
            ////SqlDataAdapter dacr = new SqlDataAdapter("select * from sales_dtl where ref=(select max(ref) from sales_dtl)", con2);
            ////SqlDataAdapter dacr1 = new SqlDataAdapter("select * from sales_hdr where ref=(select max(ref) from sales_hdr)", con2);
            ////SqlDataAdapter chk = new SqlDataAdapter("select sum(isnull(is_req,0)) from sales_dtl where ref=(select max(ref) from sales_dtl)", con2);
            ////
            if (BL.CLS_Session.prnt_type.Equals("0"))
                print_ref();
            else
                print_ref_fr();
            
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            total();
            total();

            if (string.IsNullOrEmpty(txt_invbar.Text))
            {
                MessageBox.Show((BL.CLS_Session.lang.Equals("E") ? "must enter sale invoice number when return" : "يجب ادخال رقم فاتورة البيع عند الارجاع"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_invbar.Focus();
                return;
            }

            if (button22.BackColor == Color.GreenYellow)
                textBox1.Text = label1.Text;
            //  Save_btn_Click(sender, e);
            if (textBox1.Text.Equals(""))
                textBox1.Text = (Convert.ToDouble(label1.Text) - (txt_desc.Text.Equals("") ? 0 : Convert.ToDouble(txt_desc.Text))).ToString();
            else
            {
                if (Convert.ToDouble(textBox1.Text) == 0)
                    textBox1.Text = (Convert.ToDouble(label1.Text) - (txt_desc.Text.Equals("") ? 0 : Convert.ToDouble(txt_desc.Text))).ToString();
            }
            try
            {
                total();
                //if (checkBox1.Checked == false)

                //{
                if (textBox1.Text != "" && dataGridView1.Rows.Count > 1 )
                {



                    if (Convert.ToDouble(textBox1.Text) >= (Convert.ToDouble(label1.Text) - Convert.ToDouble(string.IsNullOrEmpty(txt_desc.Text) ? "0" : txt_desc.Text)))
                    {
                        //////////  double sum = 0;
                        //////////  for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                        //////////  {
                        //////////      if (dataGridView1.Rows[i].Cells[7].Value != null)
                        //////////      {
                        //////////          sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value);
                        //////////      }
                        //////////  }

                        //////////  if (sum > 0)
                        //////////  {
                        //////////      SqlDataAdapter da = new SqlDataAdapter("select isnull(max(req_no),0) from sales_hdr", con2);
                        //////////      DataTable dt = new DataTable();
                        //////////      da.Fill(dt);
                        //////////      int req_max = Convert.ToInt32(dt.Rows[0][0]);

                        //////////      using (SqlCommand cmd1 = new SqlCommand("INSERT INTO sales_hdr (type,date,total, count,payed,total_cost,contr,saleman,req_no,cust_no) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)", con2))
                        //////////      {

                        //////////          cmd1.Parameters.AddWithValue("@a0", typeno);


                        //////////          cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                        //////////          // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                        //////////          cmd1.Parameters.AddWithValue("@a2", Convert.ToInt32(label1.Text));
                        //////////          cmd1.Parameters.AddWithValue("@a3", Convert.ToInt32(label9.Text));
                        //////////          cmd1.Parameters.AddWithValue("@a4", Convert.ToInt32(textBox1.Text));
                        //////////          cmd1.Parameters.AddWithValue("@a5", Convert.ToInt32(textBox2.Text));
                        //////////          cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                        //////////          cmd1.Parameters.AddWithValue("@a7", lbluser.Text);
                        //////////          cmd1.Parameters.AddWithValue("@a8", req_max + 1);
                        //////////          cmd1.Parameters.AddWithValue("@a9", Convert.ToInt32(label3.Text)); 
                        //////////          cmd1.ExecuteNonQuery();
                        //////////      }
                        //////////  }
                        //////////  else
                        //////////  {
                        //////////      using (SqlCommand cmd1 = new SqlCommand("INSERT INTO sales_hdr (type,date,total, count,payed,total_cost,contr,saleman,cust_no) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a9)", con2))
                        //////////      {

                        //////////          cmd1.Parameters.AddWithValue("@a0", typeno);


                        //////////          //        cmd1.Parameters.AddWithValue("@a0", 4);


                        //////////          cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                        //////////          // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                        //////////          cmd1.Parameters.AddWithValue("@a2", Convert.ToInt32(label1.Text));
                        //////////          cmd1.Parameters.AddWithValue("@a3", Convert.ToInt32(label9.Text));
                        //////////          cmd1.Parameters.AddWithValue("@a4", Convert.ToInt32(textBox1.Text));
                        //////////          cmd1.Parameters.AddWithValue("@a5", Convert.ToInt32(textBox2.Text));
                        //////////          cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                        //////////          cmd1.Parameters.AddWithValue("@a7", lbluser.Text);
                        //////////         // cmd1.Parameters.AddWithValue("@a8", req_max + 1);
                        //////////          cmd1.Parameters.AddWithValue("@a9", Convert.ToInt32(label3.Text)); 
                        //////////          cmd1.ExecuteNonQuery();
                        //////////      }

                        //////////  }


                        //////////  foreach (DataGridViewRow row in dataGridView1.Rows)
                        //////////  {
                        //////////      // SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from DB.dbo.sales_hdr", con2);
                        //////////      SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from sales_hdr", con2);
                        //////////      DataSet dss = new DataSet();
                        //////////      daa.Fill(dss, "0");

                        //////////      if (!row.IsNewRow && row.Cells[0].Value != null)
                        //////////      {
                        //////////          // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                        //////////          using (SqlCommand cmd = new SqlCommand("INSERT INTO sales_dtl (ref,type, barcode, name, unit, price, qty, cost,contr,is_req) VALUES(@r1,@r0,@c1,@c2,@c3,@c4,@c5,@c6,@c7,@c8)", con2))
                        //////////          {
                        //////////              cmd.Parameters.AddWithValue("@r1", dss.Tables[0].Rows[0][0]);
                        //////////              cmd.Parameters.AddWithValue("@r0", typeno);


                        //////////              cmd.Parameters.AddWithValue("@c1", row.Cells[0].Value);
                        //////////              cmd.Parameters.AddWithValue("@c2", row.Cells[2].Value);
                        //////////              cmd.Parameters.AddWithValue("@c3", row.Cells[3].Value);
                        //////////              cmd.Parameters.AddWithValue("@c4", row.Cells[5].Value);
                        //////////              cmd.Parameters.AddWithValue("@c5", row.Cells[4].Value);
                        //////////              cmd.Parameters.AddWithValue("@c6", row.Cells[6].Value);
                        //////////              cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                        //////////              if (row.Cells[7].Value != null)
                        //////////              { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                        //////////              cmd.ExecuteNonQuery();

                        //////////              lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);

                        //////////          }
                        //////////      }
                        //////////  }

                        //////////  //DialogResult result = MessageBox.Show("تم حفظ الفاتورة ....... هل تريد طباعة الفاتورة؟", "تاكيد", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                        //////////  //if (result == DialogResult.Yes)
                        //////////  //{
                        //////////  //    Print_btn_Click(sender, e);
                        //////////  //}
                        //////////  //else if (result == DialogResult.No)
                        //////////  //{
                        //////////  //    //...
                        //////////  //}
                        //////////  //else
                        //////////  //{
                        //////////  //    //...
                        //////////  //} 
                        //////////  //Print_btn_Click(sender, e);



                        ////////// // using (rest demo = new Form1())




                        //////////  dataGridView1.Rows.Clear();
                        //////////  // MessageBox.Show(label5.Text);
                        //////////  total();
                        //////////  textBox1.Text = "";
                        //////////  // dataGridView1.Rows.Add(100);
                        //////////  // dataGridView1.TabIndex = 0;
                        //////////  // dataGridView1.Focus();
                        //////////  // textBox1.Text = "";
                        //////////  // textBox1.BackColor = Color.White;

                        //////////  //  Report1 report1 = new Report1();
                        //////////  //    report1.Show();



                        ////////////  Run();

                        total();
                        Save_Only(sender, e);
                        dataGridView1.Rows.Clear();
                        // MessageBox.Show(label5.Text);
                       // total();
                        textBox1.Text = "";
                        txt_desc.Text = "";
                        txt_dscper.Text = "0";
                        // dataGridView1.Rows.Add(100);
                        // dataGridView1.TabIndex = 0;
                        // dataGridView1.Focus();
                        // textBox1.Text = "";
                        // textBox1.BackColor = Color.White;

                        //  Report1 report1 = new Report1();
                        //    report1.Show();



                        //  Run();


                      //  button5_Click((object)sender, (EventArgs)e);

                        // Print_btn_Click(sender, e);
                    }
                    else
                    {
                        if (label12.Text != "." && Convert.ToInt32(textBox1.Text) >= 0)
                        {
                            // textBox1.Text = "0";
                            //////////double sum = 0;
                            //////////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                            //////////{
                            //////////    if (dataGridView1.Rows[i].Cells[7].Value != null)
                            //////////    {
                            //////////        sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value);
                            //////////    }
                            //////////}

                            //////////if (sum > 0)
                            //////////{
                            //////////    SqlDataAdapter da = new SqlDataAdapter("select isnull(max(req_no),0) from sales_hdr", con2);
                            //////////    DataTable dt = new DataTable();
                            //////////    da.Fill(dt);
                            //////////    int req_max = Convert.ToInt32(dt.Rows[0][0]);

                            //////////    using (SqlCommand cmd1 = new SqlCommand("INSERT INTO sales_hdr (type,date,total, count,payed,total_cost,contr,saleman,req_no,cust_no) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)", con2))
                            //////////    {

                            //////////        cmd1.Parameters.AddWithValue("@a0", typeno);


                            //////////        cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                            //////////        // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                            //////////        cmd1.Parameters.AddWithValue("@a2", Convert.ToInt32(label1.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a3", Convert.ToInt32(label9.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a4", Convert.ToInt32(textBox1.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a5", Convert.ToInt32(textBox2.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a7", lbluser.Text);
                            //////////        cmd1.Parameters.AddWithValue("@a8", req_max + 1);
                            //////////        cmd1.Parameters.AddWithValue("@a9", Convert.ToInt32(label3.Text));
                            //////////        cmd1.ExecuteNonQuery();
                            //////////    }
                            //////////}
                            //////////else
                            //////////{
                            //////////    using (SqlCommand cmd1 = new SqlCommand("INSERT INTO sales_hdr (type,date,total, count,payed,total_cost,contr,saleman,cust_no) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a9)", con2))
                            //////////    {

                            //////////        cmd1.Parameters.AddWithValue("@a0", typeno);


                            //////////        //        cmd1.Parameters.AddWithValue("@a0", 4);


                            //////////        cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                            //////////        // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                            //////////        cmd1.Parameters.AddWithValue("@a2", Convert.ToInt32(label1.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a3", Convert.ToInt32(label9.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a4", Convert.ToInt32(textBox1.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a5", Convert.ToInt32(textBox2.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                            //////////        cmd1.Parameters.AddWithValue("@a7", lbluser.Text);
                            //////////        // cmd1.Parameters.AddWithValue("@a8", req_max + 1);
                            //////////        cmd1.Parameters.AddWithValue("@a9", Convert.ToInt32(label3.Text));
                            //////////        cmd1.ExecuteNonQuery();
                            //////////    }

                            //////////}


                            //////////foreach (DataGridViewRow row in dataGridView1.Rows)
                            //////////{
                            //////////    // SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from DB.dbo.sales_hdr", con2);
                            //////////    SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from sales_hdr", con2);
                            //////////    DataSet dss = new DataSet();
                            //////////    daa.Fill(dss, "0");

                            //////////    if (!row.IsNewRow && row.Cells[0].Value != null)
                            //////////    {
                            //////////        // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                            //////////        using (SqlCommand cmd = new SqlCommand("INSERT INTO sales_dtl (ref,type, barcode, name, unit, price, qty, cost,contr,is_req) VALUES(@r1,@r0,@c1,@c2,@c3,@c4,@c5,@c6,@c7,@c8)", con2))
                            //////////        {
                            //////////            cmd.Parameters.AddWithValue("@r1", dss.Tables[0].Rows[0][0]);
                            //////////            cmd.Parameters.AddWithValue("@r0", typeno);


                            //////////            cmd.Parameters.AddWithValue("@c1", row.Cells[0].Value);
                            //////////            cmd.Parameters.AddWithValue("@c2", row.Cells[2].Value);
                            //////////            cmd.Parameters.AddWithValue("@c3", row.Cells[3].Value);
                            //////////            cmd.Parameters.AddWithValue("@c4", row.Cells[5].Value);
                            //////////            cmd.Parameters.AddWithValue("@c5", row.Cells[4].Value);
                            //////////            cmd.Parameters.AddWithValue("@c6", row.Cells[6].Value);
                            //////////            cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                            //////////            if (row.Cells[7].Value != null)
                            //////////            { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                            //////////            cmd.ExecuteNonQuery();

                            //////////            lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);

                            //////////        }
                            //////////    }
                            //////////}


                            //DialogResult result = MessageBox.Show("تم حفظ الفاتورة ....... هل تريد طباعة الفاتورة؟", "تاكيد", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                            //if (result == DialogResult.Yes)
                            //{
                            //    Print_btn_Click(sender, e);
                            //}
                            //else if (result == DialogResult.No)
                            //{
                            //    //...
                            //}
                            //else
                            //{
                            //    //...
                            //} 
                            //Print_btn_Click(sender, e);



                            // using (rest demo = new Form1())

                            Save_Only(sender, e);


                            dataGridView1.Rows.Clear();
                            // MessageBox.Show(label5.Text);
                            total();
                            textBox1.Text = "";
                            txt_desc.Text = "";
                            // dataGridView1.Rows.Add(100);
                            // dataGridView1.TabIndex = 0;
                            // dataGridView1.Focus();
                            // textBox1.Text = "";
                            // textBox1.BackColor = Color.White;

                            //  Report1 report1 = new Report1();
                            //    report1.Show();



                            //  Run();


                           // button5_Click((object)sender, (EventArgs)e);

                            //Print_btn_Click(sender, e);
                        }
                        else
                        {

                           MetroMessageBox.Show(this,"يجب دفع المبلغ كاملا", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // textBox1.Text = "";
                            //textBox1.Focus();
                        }
                    }
                }
                else
                {
                   MetroMessageBox.Show(this,"يرجى ادخال مبلغ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //textBox1.Text = "";
                    //textBox1.Focus();
                }


            }
            catch (Exception ex)
            {
               MetroMessageBox.Show(this,ex.Message);
            }


        }



        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        private DataTable LoadSalesData()
        {
            // Create a new DataSet and read sales data file 
            ////////    data.xml into the first DataTable.
            //////DataSet dataSet = new DataSet();
            //////dataSet.ReadXml(@"..\..\data.xml");
            //////return dataSet.Tables[0];


            //DataTable dtb = rs.select_report(textBox1.Text);

            //return dtb;
           // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            
                int toprt = Convert.ToInt32(lblref.Text);

                SqlDataAdapter dacr = new SqlDataAdapter("SELECT pos_hdr.ref, pos_hdr.contr, pos_hdr.date, pos_hdr.total, pos_hdr.count, pos_hdr.payed, pos_hdr.saleman, pos_hdr.req_no, pos_dtl.barcode, pos_dtl.name, pos_dtl.price, pos_dtl.qty FROM pos_hdr CROSS JOIN pos_dtl where pos_hdr.ref=" + toprt + " and pos_dtl.ref=" + toprt, con2);
                // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from sales_hdr where ref=" + toprt, con2);
                // SqlDataAdapter chk = new SqlDataAdapter("select sum(isnull(is_req,0)) from sales_dtl where ref=" + toprt, con2);

                DataTable dt = new DataTable();

                dacr.Fill(dt);
                // dacr1.Fill(ds, "sales_hdr");
                // DataSet ds = new DataSet();
                // da.Fill(ds, "0");
                return dt;
            
            
        }
        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {
           
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.27in</PageWidth>
                <PageHeight>11.69in</PageHeight>
                <MarginTop>0.25in</MarginTop>
                <MarginLeft>0.25in</MarginLeft>
                <MarginRight>0.25in</MarginRight>
                <MarginBottom>0.25in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }
        // Create a local report for Report.rdlc, load the data,
        //    export the report to an .emf file, and print it.
        private void Run()
        {
            LocalReport report = new LocalReport();
            report.ReportPath = @"reports\Sales_direct.rdlc";
            report.DataSources.Add(
            new ReportDataSource("DataSet1", LoadSalesData()));
            Export(report);
            Print();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.Value == null &&(dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[8]))
                dataGridView1.CurrentCell.Value = 0;
            
            total();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //typeno = 4;
            //CUST_SEARCH cs = new CUST_SEARCH();
            //cs.StartPosition = FormStartPosition.CenterScreen;
            //cs.ShowDialog();

            //if (cs.dataGridView1.Rows.Count>0)
            //{
            //    label12.Text = cs.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //    label3.Text = cs.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //    button4.BackColor = Color.GreenYellow;
            //    //button5.BackColor = default(Color);
            //    // button6.BackColor = default(Color);
            //    //  button5.BackColor = SystemColors.Control;
            //    // button6.BackColor = SystemColors.Control;
            //    button5.UseVisualStyleBackColor = true;
            //    button6.UseVisualStyleBackColor = true;
            //}
            //else
            //{
            //    button5_Click((object)sender, (EventArgs)e);
            //}

            
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            typeno = 1;
            button5.BackColor = Color.GreenYellow;
           // button6.BackColor = default(Color);
           // button4.BackColor = default(Color);

           // button6.BackColor = SystemColors.Control;
           // button4.BackColor = SystemColors.Control;
            button6.UseVisualStyleBackColor = true;
            button22.UseVisualStyleBackColor = true;
            label3.Text = "0";
            label12.Text = ".";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            typeno = 4;
            button6.BackColor = Color.GreenYellow;
           // button5.BackColor = default(Color);
           // button4.BackColor = default(Color);
            //button5.BackColor = SystemColors.Control;
           // button4.BackColor = SystemColors.Control;
            button5.UseVisualStyleBackColor = true;
            button22.UseVisualStyleBackColor = true;
            label3.Text = "0";
            label12.Text = ".";

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                // int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                // int selectedIndex = dataGridView1.CurrentRow.Index;
                // cur_row = dataGridView1.CurrentCell.RowIndex;
                if (cur_row > -2 && dataGridView1.Rows[cur_row].Cells[0].Value != null)
                {
                    // dataGridView1.Rows.RemoveAt(selectedIndex);
                    // dataGridView1.Refresh();
                    int a = Convert.ToInt32(dataGridView1.Rows[cur_row].Cells[4].Value);
                    a = a + 2;
                    dataGridView1.Rows[cur_row].Cells[4].Value = a.ToString();
                    dataGridView1.Rows[cur_row].Cells[8].Value = Convert.ToInt32(dataGridView1.Rows[cur_row].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[cur_row].Cells[5].Value);
                    // if needed
                }
            }
            catch (Exception ex)
            {
               MetroMessageBox.Show(this,ex.Message);
            }




            total();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                // int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                // int selectedIndex = dataGridView1.CurrentRow.Index;
                // cur_row = dataGridView1.CurrentCell.RowIndex;
                if (cur_row > -2 && dataGridView1.Rows[cur_row].Cells[0].Value != null)
                {
                    // dataGridView1.Rows.RemoveAt(selectedIndex);
                    // dataGridView1.Refresh();
                    int a = Convert.ToInt32(dataGridView1.Rows[cur_row].Cells[4].Value);
                    a = a + 3;
                    dataGridView1.Rows[cur_row].Cells[4].Value = a.ToString();
                    dataGridView1.Rows[cur_row].Cells[8].Value = Convert.ToInt32(dataGridView1.Rows[cur_row].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[cur_row].Cells[5].Value);
                    // if needed
                }
            }
            catch (Exception ex)
            {
               MetroMessageBox.Show(this,ex.Message);
            }




            total();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                /// int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                if (cur_row > -2 && dataGridView1.Rows[cur_row].Cells[0].Value != null)
                {
                    // dataGridView1.Rows.RemoveAt(selectedIndex);
                    // dataGridView1.Refresh();
                    //////////int a = Convert.ToInt32(dataGridView1.Rows[cur_row].Cells[4].Value);
                    //////////if (a > 1)
                    //////////{
                    //////////    a = a - 1;
                    //////////    dataGridView1.Rows[cur_row].Cells[4].Value = a.ToString();
                    //////////}
                    //////////else
                    //////////{
                    //////////    button1_Click(sender, e);
                    //////////}

                    int a = Convert.ToInt32(dataGridView1.Rows[cur_row].Cells[4].Value);
                    if (a > 1)
                    {
                        a = a - 2;
                        if (a > 0)
                        {
                            dataGridView1.Rows[cur_row].Cells[4].Value = a.ToString();
                            dataGridView1.Rows[cur_row].Cells[8].Value = Convert.ToInt32(dataGridView1.Rows[cur_row].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[cur_row].Cells[5].Value);
                        }
                        else
                        {
                            dataGridView1.Rows.RemoveAt(cur_row);
                        }
                    }
                    else
                    {
                        
                            dataGridView1.Rows.RemoveAt(cur_row);
                        
                        
                    }
                    // if needed
                    // if needed
                }
            }
            catch (Exception ex)
            {
               MetroMessageBox.Show(this,ex.Message);
            }

            total();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                /// int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                if (cur_row > -2 && dataGridView1.Rows[cur_row].Cells[0].Value != null)
                {
                    // dataGridView1.Rows.RemoveAt(selectedIndex);
                    // dataGridView1.Refresh();
                    //////////int a = Convert.ToInt32(dataGridView1.Rows[cur_row].Cells[4].Value);
                    //////////if (a > 1)
                    //////////{
                    //////////    a = a - 1;
                    //////////    dataGridView1.Rows[cur_row].Cells[4].Value = a.ToString();
                    //////////}
                    //////////else
                    //////////{
                    //////////    button1_Click(sender, e);
                    //////////}

                    int a = Convert.ToInt32(dataGridView1.Rows[cur_row].Cells[4].Value);
                    if (a > 1)
                    {
                        a = a - 3;
                        if (a > 0)
                        {
                            dataGridView1.Rows[cur_row].Cells[4].Value = a.ToString();
                            dataGridView1.Rows[cur_row].Cells[8].Value = Convert.ToInt32(dataGridView1.Rows[cur_row].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[cur_row].Cells[5].Value);
                        }
                        else
                        {
                            dataGridView1.Rows.RemoveAt(cur_row);
                        }
                    }
                    else
                    {
                        
                            dataGridView1.Rows.RemoveAt(cur_row);
                        
                    }
                    // if needed
                    // if needed
                }
            }
            catch (Exception ex)
            {
               MetroMessageBox.Show(this,ex.Message);
            }

            total();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                textBox1.Text = "10";
            }
            if (flag == 2)
            {
                txt_desc.Text = "10";
            }
           
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                textBox1.Text = "50";
            }
            if (flag == 2)
            {
                txt_desc.Text = "50";
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                textBox1.Text = "100";
            }
            if (flag == 2)
            {
                txt_desc.Text = "100";
            }
        }

        private void disply(decimal price, decimal total)
        {
            try
            {

                //  SerialPort sp = new SerialPort("COM10", 9600, Parity.None, 8, StopBits.One);
                //in my case COM4  is the RJ11 connector port
                //POS to Line display connector is RJ11 (Like modem connector)
                //  sp.Open();

                //Clear the Display
                BL.CLS_Session.sp.Write(new byte[] { 0x0C }, 0, 1);
                BL.CLS_Session.sp.Write(price == 0 ? "________________" : "PRICE = " + string.Format("{0:0.00}", price));

                //Goto Bottem Line 
                BL.CLS_Session.sp.Write(new byte[] { 0x0A, 0x0D }, 0, 2);
                BL.CLS_Session.sp.Write("TOTAL = " + string.Format("{0:0.00}", total));
                /*
                Thread.Sleep(3000);
                //Here it will sleep for 3 sec and then excecute again 

                //Clear the Display
                
                sp.Write(new byte[] { 0x0C }, 0, 1);
                sp.Write("WELCOME");

                //Goto Bottem Line 
                sp.Write(new byte[] { 0x0A, 0x0D }, 0, 2);
                sp.Write("TO OUR MARKET");
                 * */
                // sp.Close();

            }
            catch { }

        }

        private void Resturant_sales_Shown(object sender, EventArgs e)
        {
            // try
            //   {
            //  MessageBox.Show(dts.Rows.Count.ToString());
            //  Pos.SALES_D smn = new SALES_D();
            int form2Count = Application.OpenForms.OfType<SALES_D_TCH>().Count();
            //  MessageBox.Show(form2Count.ToString());

            if (form2Count > Convert.ToInt32(BL.CLS_Session.dtsalman.Rows.Count == 0 ? 1 : BL.CLS_Session.dtsalman.Rows[0]["scr_open"]))
            {
                this.Close();
            }
            else
            {

                if (BL.CLS_Session.is_sal_login == false || BL.CLS_Session.dtsalman.Rows.Count == 0)
                {
                    Pos.Salmen_LogIn salm = new Salmen_LogIn();
                    //salm.MdiParent = MdiParent;
                    salm.ShowDialog();
                    set_permition(salm.txt_salman.Text);
                    this.WindowState = FormWindowState.Maximized;
                    dataGridView1.Select();
                }
                else
                {
                    set_permition(BL.CLS_Session.dtsalman.Rows[0]["sl_id"].ToString());
                    dataGridView1.Select();
                }
            }
            // else
            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string per = (100 * (Convert.ToDouble(string.IsNullOrEmpty(txt_desc.Text) ? "0" : txt_desc.Text)) / Convert.ToDouble(label1.Text)).ToString();

            //  if ((Convert.ToDouble(per) > Convert.ToDouble(BL.CLS_Session.inv_dsc)))
            if ((Convert.ToDouble(per) > Convert.ToDouble(slpmaxdisc)))
            {
                // MessageBox.Show("تجاوزت الخصم المسموح لك");
                // MetroMessageBox.Show(this, "يرجى ادخال مبلغ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MetroMessageBox.Show(this, "تجاوزت الخصم المسموح لك", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_desc.Text = "0";
                // txt_dscper.Text = "0";
                txt_desc.SelectAll();
                // txt_desper.Text = "0";
            }
            textBox1_TextChanged(sender, e);
        }

        private void txt_desc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox1.Focus();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SavePrint.Focus();
            }
        }

        private void txt_dscper_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_dscper.Text))
                txt_dscper.Text = "0";

            txt_desc.Text = (Math.Round(((Convert.ToDouble(txt_dscper.Text) * Convert.ToDouble(label1.Text)) / 100), 2)).ToString();
            // total();
            //   string per = (100 * (Convert.ToDouble(txt_desc.Text)) / Convert.ToDouble(txt_total.Text)).ToString();

            //  if ((Convert.ToDouble(per) > Convert.ToDouble(BL.CLS_Session.inv_dsc)))
            if ((Convert.ToDouble(txt_dscper.Text) > Convert.ToDouble(slpmaxdisc)))
            {
                // MessageBox.Show("تجاوزت الخصم المسموح لك");
                MessageBox.Show("تجاوزت الخصم المسموح لك", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_dscper.Text = "0";
                txt_desc.Text = "0";
                txt_dscper.SelectAll();
                // txt_desper.Text = "0";
            }
            total();
        }

        private void Resturant_sales_FormClosing(object sender, FormClosingEventArgs e)
        {
            BL.CLS_Session.is_sal_login = false;
            cssc.Close();
        }

        private void chk_iscard_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_iscard.Checked)
            {
                textBox1.Text = label1.Text;
                textBox1.ReadOnly = true;
            }
            else
            {
                textBox1.Text = "";
                textBox1.ReadOnly = false;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bool ism = this.WindowState == FormWindowState.Maximized ? true : false;
            Pos.Items_Update iu = new Pos.Items_Update();
           // iu.MdiParent = MdiParent;
            iu.ShowDialog();
            this.WindowState = ism ? FormWindowState.Maximized : FormWindowState.Normal;
          //  dataGridView1.Focus();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            dataGridView1.Select();
           
            if (!dataGridView1.CurrentRow.IsNewRow)
            {
                Pos.Add_Select fr = new Pos.Add_Select();
                // fr.MdiParent = this;
                fr.ShowDialog();
                dataGridView1.CurrentRow.Cells[2].Value = dataGridView1.CurrentRow.Cells[2].Value + " " + fr.val;
            }
        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {
            total();
           
        }

        private void button21_Click(object sender, EventArgs e)
        {

        }

        private void txt_custnam_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_custnam_Click(object sender, EventArgs e)
        {
           // txt_custnam.Text = "";
            try
            {
                Search_All f4 = new Search_All("5", "Cus");
                f4.ShowDialog();
                txt_custno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txt_custnam.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txt_temp.Text = f4.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                // txt_taxid.Text = f4.dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
            catch { }
        }

        private void txt_drivernam_Click(object sender, EventArgs e)
        {
            try
            {
                Search_All f4 = new Search_All("5-r", "Cus");
                f4.ShowDialog();
                txt_driver.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txt_drivernam.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                //  txt_temp.Text = f4.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                // txt_taxid.Text = f4.dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
            catch { }
        }

        private void txt_custno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
               // txt_custnam.Text = "";
                try
                {
                    Search_All f4 = new Search_All("5", "Cus");
                    f4.ShowDialog();
                    txt_custno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_custnam.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txt_temp.Text = f4.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                   // txt_taxid.Text = f4.dataGridView1.CurrentRow.Cells[5].Value.ToString();
                }
                catch { }

            }
        }

        private void txt_custno_Leave(object sender, EventArgs e)
        {
            DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select cu_no ,cu_name ,cl_acc,cu_tel,vndr_taxcode from customers a join cuclass b on a.cu_class=b.cl_no  where a.inactive=0 and a.cu_brno='" + BL.CLS_Session.brno + "' and cu_no='" + txt_custno.Text + "'");

            // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

            if (dt2.Rows.Count > 0)
            {
                txt_custnam.Text = dt2.Rows[0][1].ToString();
                txt_custno.Text = dt2.Rows[0][0].ToString();
                txt_temp.Text = dt2.Rows[0][3].ToString();
              //  txt_taxid.Text = dt2.Rows[0]["vndr_taxcode"].ToString();
            }
            else
            {
                txt_custnam.Text = "";
                txt_custno.Text = "";
                txt_temp.Text = "";
              //  txt_crlmt.Text = "";
            }
        }

        private void txt_custno_TextChanged(object sender, EventArgs e)
        {
            if (txt_custno.Text.Trim().Equals(""))
            {
                txt_custnam.ReadOnly = false;
                chk_agel.Checked = false;
            }
            else
                txt_custnam.ReadOnly = true;
        }

        private void txt_custnam_Enter(object sender, EventArgs e)
        {
            if (txt_custno.Text.Trim().Equals(""))
                txt_custnam.ReadOnly = false;
            else
                txt_custnam.ReadOnly = true;
        }

        private void txt_driver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                // txt_custnam.Text = "";
                try
                {
                    Search_All f4 = new Search_All("5-r", "Cus");
                    f4.ShowDialog();
                    txt_driver.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_drivernam.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                  //  txt_temp.Text = f4.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    // txt_taxid.Text = f4.dataGridView1.CurrentRow.Cells[5].Value.ToString();
                }
                catch { }

            }
        }

        private void txt_driver_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_driver.Text))
                button22_Click(sender, e);
            else
                button5_Click(sender, e);
        }

        private void txt_driver_Leave(object sender, EventArgs e)
        {
            DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select sp_id ,sp_name from pos_drivers a where a.sp_brno='" + BL.CLS_Session.brno + "' and sp_id='" + txt_driver.Text + "'");

            // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

            if (dt2.Rows.Count > 0)
            {
                txt_driver.Text = dt2.Rows[0][0].ToString();
                txt_drivernam.Text = dt2.Rows[0][1].ToString();
               // txt_temp.Text = dt2.Rows[0][3].ToString();
                //  txt_taxid.Text = dt2.Rows[0]["vndr_taxcode"].ToString();
            }
            else
            {
                txt_driver.Text = "";
                txt_drivernam.Text = "";
                //txt_temp.Text = "";
                //  txt_crlmt.Text = "";
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            typeno = 5;
            button22.BackColor = Color.GreenYellow;
            // button6.BackColor = default(Color);
            // button4.BackColor = default(Color);

            // button6.BackColor = SystemColors.Control;
            // button4.BackColor = SystemColors.Control;
            button6.UseVisualStyleBackColor = true;
            button5.UseVisualStyleBackColor = true;
            label3.Text = "0";
            label12.Text = ".";
           // txt_drivernam_Click( sender,  e);
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 4 || dataGridView1.CurrentCell.ColumnIndex == 5) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
            //if (e.Control is DataGridViewTextBoxEditingControl)
            //{
            //    DataGridViewTextBoxEditingControl tb = e.Control as DataGridViewTextBoxEditingControl;
            //    tb.KeyDown -= dataGridView1_KeyDown;
            //    tb.KeyDown += dataGridView1_KeyDown;
            //}
        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btn_note_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount>1)
            {
                Pos.Pos_note pn = new Pos_note(txt);
                pn.ShowDialog();
                txt = pn.textBox1.Text;
            }

        }

        private void chk_agel_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_agel.Checked)
            {
                typeno = 3;
                if(string.IsNullOrEmpty(txt_custno.Text))
                txt_custnam_Click(sender, e);
            }
            else
                typeno = 5; 
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //at = new Add_Tameen();
            at.ShowDialog();
            

        }

        private void button24_Click(object sender, EventArgs e)
        {
            Pos.SalesReport_tm sr = new Pos.SalesReport_tm();
           // sr.MdiParent = this;
            sr.ShowDialog();
        }
        /*
        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }
         * */

        //public static void Main1(string[] args)
        //{
        //    using (Form1 demo = new Form1())
        //    {
        //        demo.Run();
        //    }
        //}

        

        

        
            

           
        
    }
}



