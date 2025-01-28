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
using System.Drawing.Printing;
using System.Globalization;
using Microsoft.Reporting.WinForms;
using System.Drawing.Imaging;
using System.IO.Ports;
//using CrystalDecisions.CrystalReports.Engine;
using FastReport;
using System.Threading;
using System.Media;

using ZatcaIntegrationSDK;
using ZatcaIntegrationSDK.BLL;
using ZatcaIntegrationSDK.HelperContracts;

namespace POS.Pos
{
    public partial class RESALES_D_TCH : Form
    {
        SoundPlayer player = new SoundPlayer(@"Alert.wav");
        Pos.Cust_Screen cssc;
        double oval;// = Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value);
        int confirmd = 1;
        string[] lines_prt = null;
        decimal price1 = 0, total1 = 0;
        BL.EncDec endc = new BL.EncDec();
        // SerialPort sp;
        public DataTable dts = null;
        string sl_brno, sl_id, sl_name, sl_password, slpmaxdisc, maxitmdsc,reftosend;
        bool sl_chgqty, sl_chgprice, sl_delline, sl_delinv, sl_return, sl_admin, sl_alowdisc, sl_alowexit, alwreprint, alwitmdsc, sl_inactive, slalwdesc;
        bool connec_with_ser = true,prt=true;
        // public bool is_sal_login = false;
        //  public bool isout=false;
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        //double sum = 0;
        // SqlConnection con2 = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\DB.mdf;Integrated Security=True;User Instance=True");
        // SqlConnection con2 = new SqlConnection(@"Data Source=25.153.8.46;User ID=sa;Password=infocic;Connect Timeout=30");
        SqlConnection con2 = BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        DataTable dttax, dtcards,serset, dtunits;
        string cmp_name = "", comp_id = "", printer_nam = "";
        // SqlConnection con2 = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Documents and Settings\sa\My Documents\Visual Studio 2008\Projects\POS\POS\DB.mdf;Integrated Security=True;User Instance=True");

       // Thread a;
        public RESALES_D_TCH()
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


        private void SALES_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = true;

            dtunits = daml.SELECT_QUIRY_only_retrn_dt("select * from units");
            if (BL.CLS_Session.mizantype.ToString().Equals("1"))
                dataGridView1.Columns[4].DefaultCellStyle.Format = "N3";

            timer1.Start();
            cssc = new Pos.Cust_Screen();

            if (Screen.AllScreens.Length > 1 && Application.OpenForms.OfType<Pos.Cust_Screen>().Count() == 0)
            {
                cssc.StartPosition = FormStartPosition.Manual;
                cssc.Bounds = Screen.AllScreens[1].Bounds;
                cssc.WindowState = FormWindowState.Maximized;
                cssc.FormBorderStyle = FormBorderStyle.None;
                cssc.TopMost = true;
                cssc.Show();
                //  itc.Location = Screen.AllScreens[1].WorkingArea.Location;
                // itc.WindowState = FormWindowState.Maximized;
            }


            //Screen[] screen = Screen.AllScreens;

           // Sto.Item_Card itc= new Sto.Item_Card("");

            //itc.ShowDialog();

            //itc.Location = Screen.AllScreens[1].WorkingArea.Location;


            //Cust_Screen itc = new Cust_Screen();
            //if (Screen.AllScreens.Length > 1)
            //{
            //    itc.StartPosition = FormStartPosition.Manual;
            //    itc.Bounds = Screen.AllScreens[1].Bounds;
            //    itc.Show();
            //  //  itc.Location = Screen.AllScreens[1].WorkingArea.Location;
            //   // itc.WindowState = FormWindowState.Maximized;
            //}

            if (BL.CLS_Session.autoposupdat)
            {
                lbl_constat.Visible = true;
            }
            txt_total.BackColor = this.BackColor;
            txt_remain.BackColor = this.BackColor;
            txt_tax.BackColor = this.BackColor;

            FastReport.Utils.Config.ReportSettings.ShowProgress = false;

            lines_prt = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\printers.txt");
            // var min=new MAIN();
            if (BL.CLS_Session.scount == 0 || Application.OpenForms.OfType<RESALES_D_TCH>().Count() == 1)
                BL.CLS_Session.scount = 1;
            else
                BL.CLS_Session.scount = BL.CLS_Session.scount + 1;

            this.Text = this.Text + "  " + BL.CLS_Session.scount.ToString();

            dtcards = daml.SELECT_QUIRY_only_retrn_dt("select * from pos_cards");
            cmb_cards.DataSource = dtcards;
            cmb_cards.DisplayMember = BL.CLS_Session.lang.Equals("E") ? "card_edesc" : "card_desc";
            cmb_cards.ValueMember = "card_id";

            dataGridView1.TopLeftHeaderCell.Value = "م";
            if (BL.CLS_Session.use_cd && SerialPort.GetPortNames().ToList().Contains("COM" + BL.CLS_Session.port_no) && !BL.CLS_Session.isopprt)
            {
                BL.CLS_Session.sp = new SerialPort("COM" + BL.CLS_Session.port_no);
                // sp = new SerialPort("COM" + BL.CLS_Session.port_no, 9600, Parity.None, 8, StopBits.One);
                BL.CLS_Session.sp.Open();

                //Clear the Display
                BL.CLS_Session.sp.Write(new byte[] { 0x0C }, 0, 1);
                BL.CLS_Session.sp.Write(BL.CLS_Session.cd_msg);
                BL.CLS_Session.isopprt = true;
                //sp.Close();
                // DataTable dtcards = new DataTable();

            }

            this.BackColor = ColorTranslator.FromHtml(BL.CLS_Session.cmp_color);

            dttax = daml.SELECT_QUIRY_only_retrn_dt("select * from taxs");

            DataTable dt3 = daml.SELECT_QUIRY_only_retrn_dt("select c_slno,contr_id from contrs");
            BL.CLS_Session.slno = dt3.Rows[0][0].ToString();
            // BL.CLS_Session.ctrno = dt3.Rows[0][1].ToString();

            PrinterSettings settings = new PrinterSettings();
            printer_nam = settings.PrinterName;
            // dataGridView1.Rows.Add(25);
            //  dataGridView1.ReadOnly = false;
            // dataGridView1.Columns[1].ReadOnly = true;
            //  dataGridView1.Columns[2].Frozen = true;
            //  dataGridView1.Columns[2].ReadOnly = true;
            // dataGridView1.BeginEdit(true);   
            // this.dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;

            //   dataGridView1.Rows.Add(100);
            // dataGridView1.ReadOnly = true;
            dataGridView1.Columns[2].DefaultCellStyle.BackColor = Color.Lavender;// Color.SeaShell;
            dataGridView1.Columns[8].DefaultCellStyle.BackColor = Color.Lavender;

            // dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            //foreach (DataGridViewColumn dc in dataGridView1.Columns)
            //{
            //    if (dc.Index.Equals(2) || dc.Index.Equals(3))
            //    {
            //        dc.ReadOnly = true;
            //    }
            //    else
            //    {
            //        dc.ReadOnly = false;
            //    }
            //}

            // dataGridView1.Columns[4].ReadOnly = true;
            // dataGridView1.Columns[4].Frozen = true; //always visible
            // dataGridView1.ReadOnly = true;

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;

            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[8].ReadOnly = true;
            dataGridView1.Columns[15].ReadOnly = true;

            //  dataGridView1.Columns[4].ReadOnly = false;
            /*
           var lines = File.ReadAllLines(@"temp.txt");
          // var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\temp.txt");
           
           lbluser.Text = lines.First().ToString();
           pass.Text = lines.Skip(1).First().ToString();
           lblcashir.Text = lines.Skip(2).First().ToString();

           cmp_name = lines.Skip(4).First().ToString();
           comp_id = lines.Skip(8).First().ToString();
           */
            txt_salid.Text = BL.CLS_Session.UserName;
            // pass.Text = lines.Skip(1).First().ToString();
            txt_cashir.Text = BL.CLS_Session.ctrno;

            cmp_name = BL.CLS_Session.cmp_name;
            //  comp_id = lines.Skip(8).First().ToString();
            try
            {
                con2.Open();
                DateTime dt = DateTime.Now;
                string xxx = dt.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
                txt_date.Text = xxx;
                //SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Documents and Settings\sa\My Documents\Visual Studio 2008\Projects\POS\POS\DB.mdf;Integrated Security=True;User Instance=True");
                //SqlDataAdapter da = new SqlDataAdapter("select * from items", con);
                //DataSet ds = new DataSet();
                //da.Fill(ds, "0");

                // dataGridView1.CurrentCell = dataGridView1[0, 0];
               // dataGridView1.Focus();
                txt_invbar.Focus();
                // dataGridView1.CurrentCell = dataGridView1[0, 0];
                //dataGridView1.BeginEdit(true);
                //   dataGridView1.Rows[0].Cells[0].Value = ds.Tables["0"].Rows[0][0];
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            // lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);

            // dataGridView1.Focus();

            serset = daml.SELECT_QUIRY_only_retrn_dt("select * from server");

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
                {
                    dataGridView1.Columns[4].ReadOnly = true;
                    btn_mins.Enabled = false;
                    btn_plus.Enabled = false;
                }
                else
                {
                    dataGridView1.Columns[4].ReadOnly = false;
                    btn_mins.Enabled = true;
                    btn_plus.Enabled = true;
                }

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_chgprice"]) == false)
                    dataGridView1.Columns[5].ReadOnly = true;
                else
                    dataGridView1.Columns[5].ReadOnly = false;

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_delline"]) == false)
                {
                    sl_delline = false;
                    btn_delitem.Enabled = false;
                }
                else
                {
                    sl_delline = true;
                    btn_delitem.Enabled = true;
                }

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_delinv"]) == false)
                {
                    sl_delinv = false;
                    btn_del.Enabled = false;
                }
                else
                {
                    sl_delinv = true;
                    btn_del.Enabled = true;
                }
                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_return"]) == false)
                {
                    BL.CLS_Session.is_sal_login = false;
                    //  BL.CLS_Session.dtsalman.Rows.Clear();
                    // sp.Close();
                    BL.CLS_Session.scount = BL.CLS_Session.scount - 1;
                    this.Close();
                }
                
                // sl_delinv = false;
                //   else
                // sl_delinv = true;

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_alowdisc"]) == false)
                    sl_alowdisc = false;
                else
                    sl_alowdisc = true;


                slpmaxdisc = BL.CLS_Session.dtsalman.Rows[0]["slpmaxdisc"].ToString();
                slalwdesc = Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_alowdisc"]);

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_alowexit"]) == false)
                {
                    sl_alowexit = false;
                    btn_Exit.Enabled = false;
                }
                else
                {
                    sl_alowexit = true;
                    btn_Exit.Enabled = true;
                }

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["alwreprint"]) == false)
                    btn_prt.Enabled = false;
                else
                    btn_prt.Enabled = true;

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["alwitmdsc"]) == false)
                    alwitmdsc = false;
                else
                    alwitmdsc = true;

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_alwcrdit"]) == false)
                {
                    txt_custno.Enabled = false;
                    chk_agel.Enabled = false;
                }
                else
                {
                    txt_custno.Enabled = true;
                    chk_agel.Enabled = true;
                }

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["slpalowopdr"]) == false)
                    btn_opndr.Enabled = false;
                else
                    btn_opndr.Enabled = true;

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["frcrplinv"]) == false)
                    btn_Find.Enabled = false;
                else
                    btn_Find.Enabled = true;

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["catch_thief"]) == false)
                {
                    button5.Enabled = false;
                    btn_resrv.Enabled = false;
                }
                else
                {
                    button5.Enabled = true;
                    btn_resrv.Enabled = true;
                }

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sold_once"]) == false)
                    checkBox1.Enabled = false;
                else
                    checkBox1.Enabled = true;

                maxitmdsc = BL.CLS_Session.dtsalman.Rows[0]["maxitmdsc"].ToString();
                txt_salid.Text = BL.CLS_Session.dtsalman.Rows[0]["sl_id"].ToString();
                txt_salnam.Text = BL.CLS_Session.dtsalman.Rows[0]["sl_name"].ToString();


                int form2Count = Application.OpenForms.OfType<RESALES_D_TCH>().Count();

                if (form2Count > Convert.ToInt32(BL.CLS_Session.dtsalman.Rows[0]["scr_open"]))
                    this.Close();
                //  this.Refresh();
                //  dataGridView1_RowEnter( null,  null);


            }
            txt_invbar.Select();
           // this.WindowState = FormWindowState.Maximized; 
        }

        private void lbldate_Click(object sender, EventArgs e)
        {

        }


        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int rowindex, columnindex;

                if (e.KeyCode == Keys.F8 && e.Modifiers != Keys.Control && dataGridView1.CurrentRow.IsNewRow)
                {
                    
                    rowindex = dataGridView1.CurrentCell.RowIndex;
                    columnindex = dataGridView1.CurrentCell.ColumnIndex;

                    Search_All itm = new Search_All("2", "Pos");
                    itm.ShowDialog();

                    SqlDataAdapter da23 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_curcost,b.price as item_price,i.item_barcode as  item_barcode,i.item_unit pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,1 pkqty,i.item_tax i_tax,i.item_image img,b.min_price mp from items i join items_bc b on i.item_no=b.item_no and i.item_barcode=b.barcode and b.sl_no='" + BL.CLS_Session.sl_prices + "' join taxs t on i.item_tax=t.tax_id  and i.item_no='" + itm.dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() + "' where i.inactive=0", con2);

                    DataSet ds23 = new DataSet();
                    da23.Fill(ds23, "0");

                    if (ds23.Tables["0"].Rows.Count > 0)
                    {
                       
                        //var matchedRow = dataGridView1.Rows.OfType<DataGridViewRow>()
                        //  .FirstOrDefault(row => row.Cells[0].Value != null &&
                        //                       row.Cells[0].Value.Equals(ds23.Tables["0"].Rows[0][0]));
                        ////var matchedRow = dataGridView1.Rows.OfType<DataGridViewRow>()
                        ////       .FirstOrDefault(row => row.Cells[0].Value != null && row.Cells[15].Value == null &&
                        ////                            row.Cells[0].Value.Equals(ds23.Tables["0"].Rows[0][0]));
                        var matchedRow = dataGridView1.Rows.OfType<DataGridViewRow>()
                                .FirstOrDefault(row => row.Cells[0].Value != null && //row.Cells[15].Value == null &&
                                                     row.Cells[0].Value.ToString().Trim().Equals(ds23.Tables["0"].Rows[0][4].ToString().Trim()));
                        //// if (matchedRow != null && matchedRow.Cells[15].Value == null)
                        if (matchedRow != null && BL.CLS_Session.imp_itm)
                        {
                            double qty = (double)matchedRow.Cells[4].Value + 1;
                            //  double price = (double)matchedRow.Cells[3].Value;
                            matchedRow.Cells[4].Value = qty;
                            matchedRow.Cells[8].Value = (Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) - ((Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) * Convert.ToDouble(matchedRow.Cells[6].Value) / 100);
                            matchedRow.Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(matchedRow.Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) - ((Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) * Convert.ToDouble(matchedRow.Cells[6].Value)) / 100) : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(matchedRow.Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) - ((Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) * Convert.ToDouble(matchedRow.Cells[6].Value)) / 100) : (Convert.ToDouble(matchedRow.Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(matchedRow.Cells[9].Value)) * ((Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) - ((Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) * Convert.ToDouble(matchedRow.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                            matchedRow.DefaultCellStyle.BackColor = Color.PaleGreen;
                            total();
                           // dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                            btnDown(matchedRow);
                            chk_qty_offer(matchedRow);
                          //  dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                            total();
                           // return;
                        }


                        if (Convert.ToDouble(ds23.Tables["0"].Rows[0][3]) <= 0)
                        {
                            player.Play();
                            MessageBox.Show("الصنف ليس لديه سعر", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //  dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                            textBox3.Text = "";
                            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                            return;
                        }

                        DataGridViewRow row1 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                        row1.DefaultCellStyle.BackColor = Color.White;
                        row1.Cells[0].Value = ds23.Tables["0"].Rows[0][4];
                        row1.Cells[1].Value = ds23.Tables["0"].Rows[0][0];
                        row1.Cells[2].Value = ds23.Tables["0"].Rows[0][1];
                        row1.Cells[3].Value = ds23.Tables["0"].Rows[0][5];
                        //DataView dv1 = dtunits.DefaultView;
                        ////  dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                        //dv1.RowFilter = "unit_id in('" + ds23.Tables["0"].Rows[0][5] + "')";
                        //DataTable dtNew = dv1.ToTable();
                        //dcombo.DataSource = dtNew;
                        //dcombo.DisplayMember = "unit_name";
                        //dcombo.ValueMember = "unit_id";
                        //row1.Cells[3] = dcombo;
                        //row1.Cells[3].Value = ds23.Tables["0"].Rows[0][5];
                        //dcombo.FlatStyle = FlatStyle.Flat;

                        row1.Cells[9].Value = ds23.Tables["0"].Rows[0][11];
                        row1.Cells[10].Value = ds23.Tables["0"].Rows[0]["pkqty"];
                        row1.Cells[11].Value = ds23.Tables["0"].Rows[0]["i_tax"];
                        row1.Cells[13].Value = ds23.Tables["0"].Rows[0]["img"];
                        row1.Cells[14].Value = ds23.Tables["0"].Rows[0]["mp"];

                        if (row1.Cells[4].Value == null)
                        {
                            row1.Cells[4].Value = 1;
                        }
                        if (row1.Cells[5].Value == null)
                        {
                            row1.Cells[5].Value = ds23.Tables["0"].Rows[0][3];
                        }
                        if (row1.Cells[6].Value == null)
                        {
                            row1.Cells[6].Value = 0;
                        }
                        //  row.Cells[5].Value = ds23.Tables["0"].Rows[0][3];
                        row1.Cells[7].Value = ds23.Tables["0"].Rows[0][2];
                        row1.Cells[8].Value = Convert.ToDouble(row1.Cells[4].Value) * Convert.ToDouble(row1.Cells[5].Value);

                        if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + ds23.Tables[0].Rows[0][9]))
                        {
                            pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + ds23.Tables[0].Rows[0][9]);

                        }
                        else
                        {
                            pictureBox1.Image = Properties.Resources.background_button;

                        }

                        //  DataRow[] dtrvat = dttax.Select("tax_id =" + row.Cells[11].Value + "");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";

                        //row.Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : (Convert.ToDouble(row.Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(row.Cells[9].Value)) * ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) - ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) * Convert.ToDouble(row.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                        row1.Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(row1.Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(row1.Cells[5].Value) * Convert.ToDouble(row1.Cells[4].Value)) - ((Convert.ToDouble(row1.Cells[5].Value) * Convert.ToDouble(row1.Cells[4].Value)) * Convert.ToDouble(row1.Cells[6].Value)) / 100) : (Convert.ToDouble(row1.Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(row1.Cells[9].Value)) * ((Convert.ToDouble(row1.Cells[5].Value) * Convert.ToDouble(row1.Cells[4].Value)) - ((Convert.ToDouble(row1.Cells[5].Value) * Convert.ToDouble(row1.Cells[4].Value)) * Convert.ToDouble(row1.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();


                        dataGridView1.Rows.Add(row1);

                        //DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                        ////  r.Cells[2] = dcombo;
                        //DataView dv1 = dtunits.DefaultView;
                        ////  dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                        //dv1.RowFilter = "unit_id in('" + row1.Cells[3].Value + "')";
                        //DataTable dtNew = dv1.ToTable();
                        //dcombo.DataSource = dtNew;
                        //dcombo.DisplayMember = "unit_name";
                        //dcombo.ValueMember = "unit_id";
                        //row1.Cells[3] = dcombo;
                        //row1.Cells[3].Value = dtNew.Rows[0][0];

                        ////  dataGridView1[2, r.Index] = dcombo;
                        //// dataGridView1[2, r.Index].Value = dtNew.Rows[0][1];
                        //// dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                        //dcombo.FlatStyle = FlatStyle.Flat;
                        chk_qty_offer(row1);

                        DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                        //  r.Cells[2] = dcombo;
                        DataView dv1 = dtunits.DefaultView;
                        //  dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                        dv1.RowFilter = "unit_id in('" + row1.Cells[3].Value.ToString() + "')";
                        DataTable dtNew = dv1.ToTable();
                        dcombo.DataSource = dtNew;
                        dcombo.DisplayMember = "unit_name";
                        dcombo.ValueMember = "unit_id";

                        row1.Cells[3] = dcombo;
                        row1.Cells[3].Value = dtNew.Rows[0][0];

                        //  dataGridView1[2, r.Index] = dcombo;
                        // dataGridView1[2, r.Index].Value = dtNew.Rows[0][1];
                        // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                        dcombo.FlatStyle = FlatStyle.Flat;

                        total();
                        //////dataGridView1.Rows[rowindex].Cells[0].Value = ds23.Tables["0"].Rows[0][0];
                        //////dataGridView1.Rows[rowindex].Cells[2].Value = ds23.Tables["0"].Rows[0][1];
                        //////dataGridView1.Rows[rowindex].Cells[3].Value = ds23.Tables["0"].Rows[0][5];
                        //////if (dataGridView1.Rows[rowindex].Cells[4].Value == null)
                        //////{
                        //////    dataGridView1.Rows[rowindex].Cells[4].Value = 1;
                        //////}
                        //////dataGridView1.Rows[rowindex].Cells[5].Value = ds23.Tables["0"].Rows[0][3];
                        //////dataGridView1.Rows[rowindex].Cells[6].Value = ds23.Tables["0"].Rows[0][2];

                        // label1.Text = rowindex.ToString();
                        // passes the value through the constructor to the 
                        //   second form.
                        //   MySecondForm f2 = new MySecondForm(firstColumnValue);
                        //  f2.Show();
                        //if (dataGridView1.Rows[rowindex].Cells[columnindex + 3].Value != null)
                        //{
                        //label3.Text = (dataGridView1.Rows.Count - 1).ToString();

                        ////////double sum1 = 0;
                        ////////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                        ////////{
                        ////////    sum1 += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                        ////////}
                        ////////label3.Text = sum1.ToString();
                        //////////}
                        //////////else
                        //////////{

                        //////////}

                        //////////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                        //////////{

                        ////////double sum = 0;
                        ////////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                        ////////{
                        ////////    sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                        ////////}
                        ////////label5.Text = sum.ToString();


                        ////////double sumcost = 0;
                        ////////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                        ////////{
                        ////////    sumcost += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                        ////////}
                        ////////textBox2.Text = sumcost.ToString();

                        total();
                        if (BL.CLS_Session.use_cd)
                            //  disply(Convert.ToDecimal(row.Cells[5].Value), total1);
                            disply(Convert.ToDecimal(ds23.Tables["0"].Rows[0][3]), total1);

                        cssc.txt_iname.Text = ds23.Tables["0"].Rows[0][1].ToString();
                        cssc.txt_iprice.Text = ds23.Tables["0"].Rows[0][3].ToString();
                        cssc.txt_total.Text = total1.ToString();

                        // sum += Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[columnindex + 3].Value);
                        // sum += Convert.ToDouble(ds2.Tables["0"].Rows[0][2]);
                        // }
                        // label5.Text = sum.ToString();



                        // dataGridView1.CurrentCell = dataGridView1.Rows[rowindex + 1].Cells[0];



                        // if ((string)dataGridView1.CurrentRow.Cells[0].Value=="")
                        // if (dataGridView1.SelectedCells==null)


                        //  }
                       // DataGridViewRow dtr;
                       // dataGridView1.Rows.(dtr);
                        dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                        dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                        dataGridView1.Select();
                       // this.dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.NewRowIndex];

                    }
                    /*

                    //Item_Srch_G itm = new Item_Srch_G();

                    //this.dataGridView1.CurrentRow.Cells[0].Value = itm.dataGridView1.CurrentRow.Cells[0].Value;

                    //  dataGridView1.BeginEdit(true);
                    //itm.ShowDialog();
                    textBox3.Select();
                    // Item_Srch_G itm = new Item_Srch_G();
                    // itm.ShowDialog();
                    //
                    Search_All itm = new Search_All("2", "Pos");
                    itm.ShowDialog();

                    // dataGridView1.BeginEdit(true);

                    //DataGridViewCell cell = dataGridView1.CurrentRow.Cells[0];
                    //dataGridView1.CurrentCell = cell;
                    //dataGridView1.BeginEdit(true);

                    //   this.dataGridView1.CurrentRow.Cells[0].Value = itm.dataGridView1.CurrentRow.Cells[0].Value;

                    //////textBox1.Text = itm.dataGridView1.CurrentRow.Cells[0].Value.ToString() ;
                    //////textBox3_KeyUp( sender,  e);
                    // dataGridView1.Rows.Add(itm.dataGridView1.CurrentRow.Cells[0].Value);
                    // dataGridView1_KeyUp( sender,  e);
                    //this.txtCustomerID.Text = frm.dgvCustomers.CurrentRow.Cells[0].Value.ToString();
                    //this.txtFirstName.Text = frm.dgvCustomers.CurrentRow.Cells[1].Value.ToString();
                    //this.txtLastName.Text = frm.dgvCustomers.CurrentRow.Cells[2].Value.ToString();
                    //this.txtTel.Text = frm.dgvCustomers.CurrentRow.Cells[3].Value.ToString();
                    //this.txtEmail.Text = frm.dgvCustomers.CurrentRow.Cells[4].Value.ToString();



                    textBox3.Text = itm.dataGridView1.CurrentRow.Cells[0].Value.ToString();


                    // textBox1.Focus();
                    */
                }

                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F8)
                {
                    textBox3.Select();
                    Search_All f4 = new Search_All("chkalter", dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    f4.ShowDialog();
                    try
                    {
                        textBox3.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        //  dataGridView1.CurrentRow.Cells[0].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                        SendKeys.Send("{ENTER}");
                    }
                    catch { }
                }


                if (e.KeyCode == Keys.Delete)
                {
                    int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                    if (selectedIndex > -1 && sl_delline == true)
                    {
                        dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.IsNewRow ? selectedIndex - 1 : selectedIndex);
                        dataGridView1.Refresh();
                        total();
                        if (dataGridView1.Rows.Count <= 1)
                            btn_Exit.Enabled = sl_alowexit ? true : false;

                        dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                       // dataGridView1.Rows[dataGridView1.NewRowIndex].Selected = true;
                        dataGridView1.CurrentCell =dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];// dataGridView1.Rows[selectedIndex+1].Cells[0];
                        // if needed

                        cssc.txt_iname.Text = "";
                        cssc.txt_iprice.Text = "0.00";
                        cssc.txt_total.Text = total1.ToString();
                    }

                }

                //if (e.KeyCode == Keys.Space)
                //{

                //    txt_recv.Focus();
                //}
            }
            catch { }
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {



        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (e.KeyChar == 13)
                {
                    if (txt_recv.Text == "")
                    {
                        // dataGridView1.Focus();
                        int chk2 = dataGridView1.RowCount - 1;
                        dataGridView1.Focus();
                        // dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex]
                        dataGridView1.Rows[chk2].Cells[0].Selected = true;
                    }
                    else
                    {
                        //  double pay = Convert.ToDouble(textBox1.Text);
                        double remain = Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_desc.Text) - Convert.ToDouble(txt_recv.Text);
                        lbl_mustpay.Text = remain.ToString();
                        //button1_Click((object)sender, (EventArgs)e);
                        button1.Focus();
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            total();
            if (string.IsNullOrEmpty(txt_invbar.Text) && BL.CLS_Session.is_einv)
            {
                MessageBox.Show("يجب ادخال رقم فاتورة البيع في حال الارجاع", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_invbar.Focus();
                return;
            }

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

            total();
            txt_recv.Text = lbl_mustpay.Text;
            button1.Enabled = false;
            if (Convert.ToDouble(lbl_mustpay.Text) <= 0)
            {
                MessageBox.Show("مبلغ الفاتورة غير صحيح", "تحدذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1.Enabled = true;
               // btn_Save.Enabled = true;
                return;
            }

            
            DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref),0) as eee from pos_hdr where [contr]=" + BL.CLS_Session.ctrno + " and [brno]= '" + BL.CLS_Session.brno + "' ");
            txt_ref.Text = Convert.ToString(dt2.Rows[0][0]);
            //try
            //{
            // MessageBox.Show(Directory.GetCurrentDirectory() + "\\" + BL.CLS_Session.comp_logo);
            // MessageBox.Show();
            int invref = Convert.ToInt32(txt_ref.Text) + 1;

            if (dataGridView1.Rows[0].Cells[3].Value != null)
            {
               // if (string.IsNullOrEmpty(txt_custno.Text) ? ((Convert.ToDouble(txt_recv.Text) + Convert.ToDouble(txt_cardpay.Text)) >= (BL.CLS_Session.isshamltax.Equals("2") ? (Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_desc.Text)) : (Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_desc.Text) + Convert.ToDouble(txt_tax.Text)))): 1 != 0)
               // if (string.IsNullOrEmpty(txt_custno.Text) && !chk_agel.Checked ? ((Convert.ToDouble(txt_recv.Text) + Convert.ToDouble(txt_cardpay.Text)) >= (BL.CLS_Session.isshamltax.Equals("2") ? (Convert.ToDouble(lbl_mustpay.Text)) : (Convert.ToDouble(lbl_mustpay.Text)))) : 1 != 0)
                if (Convert.ToDouble(txt_recv.Text) + Convert.ToDouble(txt_cardpay.Text) >= Convert.ToDouble(lbl_mustpay.Text) && (string.IsNullOrEmpty(txt_custno.Text) || !chk_agel.Checked))
                {
                    //  if (((Convert.ToDouble(txt_recv.Text) + Convert.ToDouble(txt_cardpay.Text)) > (BL.CLS_Session.isshamltax.Equals("2") ? (Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_desc.Text) + 500) : (Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_desc.Text) + Convert.ToDouble(txt_tax.Text) + 500))))
                    if (((Convert.ToDouble(txt_recv.Text) + Convert.ToDouble(txt_cardpay.Text)) > (BL.CLS_Session.isshamltax.Equals("2") ? (Convert.ToDouble(lbl_mustpay.Text) + 500) : (Convert.ToDouble(lbl_mustpay.Text) + 500))))
                    {
                        MessageBox.Show("المبلغ المستلم كبير جدا .. يرجى ادخال مبلغ صحيح","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        button1.Enabled = true;
                        txt_recv.Focus();
                        return;
                    }
                    // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.sales_hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
                    using (SqlCommand cmd1 = new SqlCommand("INSERT INTO pos_hdr (brno,slno,ref,contr,type,date,total,count,payed,total_cost,saleman,cust_no,discount,net_total,tax_amt,dscper,card_type,card_amt,note,taxfree_amt,mobile,rref,rcontr) VALUES(@br,@sl,@refhd,@ctr,@a0,@a1,@a2,@a3,@a4,@a5,@a8,@a9,@a10,@a11,@a12,@a13,@a14,@a15,@a16,@a17,@a18,@a19,@a20)", con2))
                    {
                        cmd1.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                        cmd1.Parameters.AddWithValue("@sl", BL.CLS_Session.slno);
                        cmd1.Parameters.AddWithValue("@refhd", invref);
                        cmd1.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);

                        cmd1.Parameters.AddWithValue("@a0", !string.IsNullOrEmpty(txt_custno.Text) && chk_agel.Checked ? 2 : 0);
                        cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                        // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                        cmd1.Parameters.AddWithValue("@a2", (BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(txt_total.Text) + Convert.ToDouble(txt_tax.Text)) : Convert.ToDouble(txt_total.Text)));
                        cmd1.Parameters.AddWithValue("@a3", Convert.ToDouble(label3.Text));
                        cmd1.Parameters.AddWithValue("@a4", Convert.ToDouble(txt_recv.Text));
                        cmd1.Parameters.AddWithValue("@a5", Convert.ToDouble(textBox2.Text));
                        //  cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                        //  cmd1.Parameters.AddWithValue("@a7", comp_id);
                        cmd1.Parameters.AddWithValue("@a8", txt_salid.Text);
                        cmd1.Parameters.AddWithValue("@a9", string.IsNullOrEmpty(txt_custno.Text) ? "0" : txt_custno.Text);
                        cmd1.Parameters.AddWithValue("@a10", Convert.ToDouble(txt_desc.Text));
                        cmd1.Parameters.AddWithValue("@a11", BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_desc.Text) + Convert.ToDouble(txt_tax.Text)) : (Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_desc.Text)));
                        cmd1.Parameters.AddWithValue("@a12", string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : Convert.ToDouble(txt_tax.Text));
                        cmd1.Parameters.AddWithValue("@a13", Convert.ToDouble(txt_dscper.Text));
                        cmd1.Parameters.AddWithValue("@a14", Convert.ToDouble(txt_cardpay.Text) > 0 ? cmb_cards.SelectedValue : 0);
                        cmd1.Parameters.AddWithValue("@a15", Convert.ToDouble(txt_cardpay.Text));
                        cmd1.Parameters.AddWithValue("@a16",  txt_custnam.Text );
                        cmd1.Parameters.AddWithValue("@a17", Convert.ToDouble(txt_taxfree.Text));
                        cmd1.Parameters.AddWithValue("@a18", txt_mobil.Text);
                        cmd1.Parameters.AddWithValue("@a19", s2);
                        cmd1.Parameters.AddWithValue("@a20", s1);
                        //  cmd1.Parameters.AddWithValue("@a16", string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : (BL.CLS_Session.isshamltax.Equals("1") ? 1 : BL.CLS_Session.isshamltax.Equals("2") ? 2 : 0));

                        if (con2.State == ConnectionState.Closed)
                            con2.Open();
                        cmd1.ExecuteNonQuery();
                    }


                    int srno = 1, reff = Convert.ToInt32(txt_ref.Text) + 1;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from DB.dbo.sales_hdr", con2);


                        if (!row.IsNewRow && row.Cells[0].Value != null && !row.Cells[0].Value.ToString().Trim().Equals("") && row.Cells[1].Value != null && !row.Cells[1].Value.ToString().Trim().Equals("") && row.Cells[2].Value != null && !row.Cells[2].Value.ToString().Trim().Equals("") && Convert.ToDouble(row.Cells[4].Value) != 0)
                        {
                            // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO pos_dtl (brno,slno,contr,ref,type, barcode, name, unit, price, qty, cost,is_req,itemno,srno,pkqty,discpc,tax_id,tax_amt,offr_amt) VALUES(@br,@sl,@ctr,@r1,@r0,@c1,@c2,@c3,@c4,@c5,@c6,@c9,@c10,@sn,@pk,@c11,@c12,@c13,@c14)", con2))
                            {
                                cmd.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                                cmd.Parameters.AddWithValue("@sl", BL.CLS_Session.slno);
                                //cmd.Parameters.AddWithValue("@refdt", invref);
                                cmd.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);

                                cmd.Parameters.AddWithValue("@r1", reff);
                                cmd.Parameters.AddWithValue("@r0", !string.IsNullOrEmpty(txt_custno.Text) && chk_agel.Checked ? 2 : 0);
                                cmd.Parameters.AddWithValue("@c1", row.Cells[0].Value);
                                cmd.Parameters.AddWithValue("@c2", row.Cells[2].Value.ToString().Length > 100 ? row.Cells[2].Value.ToString().Substring(0,100) : row.Cells[2].Value);
                                cmd.Parameters.AddWithValue("@c3", row.Cells[3].Value);
                                cmd.Parameters.AddWithValue("@c4", row.Cells[5].Value);
                                cmd.Parameters.AddWithValue("@c5", row.Cells[4].Value);
                                cmd.Parameters.AddWithValue("@c6", row.Cells[7].Value);
                                // cmd.Parameters.AddWithValue("@c7", lblcashir.Text);
                                // cmd.Parameters.AddWithValue("@c8", comp_id);
                                cmd.Parameters.AddWithValue("@c9", 0);
                                cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                                cmd.Parameters.AddWithValue("@sn", srno);
                                cmd.Parameters.AddWithValue("@pk", row.Cells[10].Value);
                                cmd.Parameters.AddWithValue("@c11", row.Cells[6].Value);
                                cmd.Parameters.AddWithValue("@c12", row.Cells[11].Value);
                                cmd.Parameters.AddWithValue("@c13", string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : Math.Round(Convert.ToDouble(row.Cells[12].Value), 4));
                                cmd.Parameters.AddWithValue("@c14",row.Cells[15].Value==null? 0 : row.Cells[15].Value);

                                if (con2.State == ConnectionState.Closed)
                                    con2.Open();
                                cmd.ExecuteNonQuery();



                            }
                            srno++;
                        }
                    }
                    dataGridView1.Rows.Clear();
                    // dataGridView1.Rows.Add(100);
                    // dataGridView1.TabIndex = 0;
                    txt_recv.Text = "0";
                    txt_desc.Text = "0";
                    txt_dscper.Text = "0";
                    txt_cardpay.Text = "0";

                    dataGridView1.Focus();
                    txt_ref.Text = reff.ToString();
                    reftosend = reff.ToString();

                    if (BL.CLS_Session.is_einv_p2)
                        create_einv_p2(reff.ToString());

                    if (prt)
                    {
                        DialogResult result = MessageBox.Show("هل تريد طباعة الفاتورة", "تاكيد الطباعة", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            if (BL.CLS_Session.prnt_type.Equals("0"))
                                print_ref();
                            else
                                print_ref_fr();
                        }
                        else if (result == DialogResult.No)
                        {
                            //...
                            if (!BL.CLS_Session.nocahopen)
                            btn_update_Click(sender, e);
                            // btn_Save.Enabled = true;

                        }
                        else
                        {
                            //...
                            // btn_Save.Enabled = true;

                        }
                    }
                    btn_Exit.Enabled = sl_alowexit ? true : false;
                    // textBox1.BackColor = Color.White;
                    txt_custno.Text = "";
                    txt_custnam.Text = "";
                    txt_invbar.Text = "";
                    //  Report1 report1 = new Report1();
                    //    report1.Show();
                    txt_mobil.Visible = false;
                    lbl_mobil.Visible = false;
                    chk_payed.Checked = false;
                    chk_agel.Checked = false;
                    button1.Enabled = true;
                    if (BL.CLS_Session.autoposupdat)
                    {
                        Thread a = new Thread(() => thread2(reftosend));
                        a.Start();
                       // a.Abort();
                    }

                    if (connec_with_ser)
                    {
                       // lbl_constat.Visible = true;
                        lbl_constat.BackColor = Color.Lime;
                    }
                    else
                    {
                       // lbl_constat.Visible = true;
                        lbl_constat.BackColor = Color.Red;
                    }

                }
                else
                {
                    MessageBox.Show("يجب دفع المبلغ كاملا", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // MessageBox.Show("يجب دفع المبلغ كاملا");
                    txt_recv.Text = "";
                    txt_recv.Focus();
                }
            }
            else
            {
                MessageBox.Show("لا يوجد اصناف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //  MessageBox.Show("لا يوجد اصناف");
            }
            //////label11.Text = "OK";
            ////////sum = 0;
            ////////dataGridView1.Focus();
            ////////dataGridView1.Rows.Clear();



            //////dataGridView1.Rows.Clear();
            //////dataGridView1.Rows.Add(12);
            //////dataGridView1.Focus();

            // dataGridView1.CurrentCell = dataGridView1[0, 0];
            //dataGridView1.Refresh();
            //dataGridView1.DataSource=null;

            //dataGridView1.TabIndex = 0;

            /* 
              dataGridView1.Rows[0].Cells[0].Value = "";
             ////DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[1].Clone();

             dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
             //  dataGridView1.Focus();
             //  

             //dataGridView1.CurrentCell = 

             // dataGridView1.Rows[1].Cells[0].Value.ToString()="";
             textBox1.BackColor = Color.White;
             */
            //Report1 report1 = new Report1();
            //report1.Show();


            // textBox3.Focus();

            // dataGridView1.Rows.Add(100);
            // dataGridView1.Rows[0].Cells[0].Selected = true;


            // dataGridView1.Refresh();
            // dataGridView1.Focus();

            //}



            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}


        }
        public void thread1(string inv)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("send_pos_inv"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;
                    cmd.Parameters.AddWithValue("@reff", inv);
                    cmd.Parameters.AddWithValue("@dbname", serset.Rows[0][1].ToString());
                    con2.Open();
                    cmd.ExecuteNonQuery();
                    con2.Close();
                }
            }
            catch { }
        }
        private void thread2(string inv)
        {
            try
            {
                DataTable dthdr = daml.SELECT_QUIRY_only_retrn_dt("select * from pos_hdr where ref=" + reftosend + "");
                DataTable dtdtl = daml.SELECT_QUIRY_only_retrn_dt("select * from pos_dtl where ref=" + reftosend + "");

                SqlConnection con_dest = new SqlConnection("Data Source=" + serset.Rows[0][0].ToString() + ";Initial Catalog=" + serset.Rows[0][1].ToString() + ";User id=" + serset.Rows[0][2].ToString() + ";Password=" + endc.Decrypt(serset.Rows[0][3].ToString(),true) + ";Connection Timeout=" + serset.Rows[0][4].ToString() + "");


                for (int i = 0; i < dthdr.Rows.Count; i++)
                {
                    //  MessageBox.Show(Convert.ToDateTime(dt.Rows[i][5]).ToString());
                    string StrQuery = " MERGE pos_hdr as t"
                        //  + " USING (select '" + dt.Rows[i][0] + "' as brno, '" + dt.Rows[i][1] + "' as slno, " + dt.Rows[i][2] + " as ref," + dt.Rows[i][3] + " as contr, " + dt.Rows[i][4] + " as [type], '" + Convert.ToDateTime(dt.Rows[i][5]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' as date," + dt.Rows[i][6] + " as total," + dt.Rows[i][7] + " as count," + dt.Rows[i][8] + " as payed," + dt.Rows[i][9] + " as total_cost,'" + dt.Rows[i][10] + "' as saleman," + dt.Rows[i][11] + " as req_no," + dt.Rows[i][12] + " as cust_no," + dt.Rows[i][13] + " as discount," + dt.Rows[i][14] + " as net_total,'" + Convert.ToDateTime(dt.Rows[i][15]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' as sysdate," + dt.Rows[i][16] + " as gen_ref," + dt.Rows[i][17] + " as tax_amt) as s"
                        //   + " USING (select '" + dt.Rows[i][0] + "' as brno, '" + dt.Rows[i][1] + "' as slno, " + dt.Rows[i][2] + " as ref," + dt.Rows[i][3] + " as contr, " + dt.Rows[i][4] + " as [type], convert(datetime,'" + Convert.ToDateTime(dt.Rows[i][5]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "') as date," + dt.Rows[i][6] + " as total," + dt.Rows[i][7] + " as count," + dt.Rows[i][8] + " as payed," + dt.Rows[i][9] + " as total_cost,'" + dt.Rows[i][10] + "' as saleman," + dt.Rows[i][11] + " as req_no," + dt.Rows[i][12] + " as cust_no," + dt.Rows[i][13] + " as discount," + dt.Rows[i][14] + " as net_total, convert(datetime,'" + Convert.ToDateTime(dt.Rows[i][15]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "') as sysdate," + dt.Rows[i][16] + " as gen_ref," + dt.Rows[i][17] + " as tax_amt) as s"
                                    + " USING (select '" + dthdr.Rows[i][0] + "' as brno, '" + dthdr.Rows[i][1] + "' as slno, " + dthdr.Rows[i][2] + " as ref," + dthdr.Rows[i][3] + " as contr, " + dthdr.Rows[i][4] + " as [type],'" + Convert.ToDateTime(dthdr.Rows[i][5]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "' as date," + dthdr.Rows[i][6] + " as total," + dthdr.Rows[i][7] + " as count," + dthdr.Rows[i][8] + " as payed," + dthdr.Rows[i][9] + " as total_cost,'" + dthdr.Rows[i][10] + "' as saleman," + dthdr.Rows[i][11] + " as req_no," + dthdr.Rows[i][12] + " as cust_no," + dthdr.Rows[i][13] + " as discount," + dthdr.Rows[i][14] + " as net_total,'" + Convert.ToDateTime(dthdr.Rows[i][15]).ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US", false)) + "' as sysdate," + dthdr.Rows[i][16] + " as gen_ref," + dthdr.Rows[i][17] + " as tax_amt," + dthdr.Rows[i][18] + " as dscper," + dthdr.Rows[i][19] + " as card_type," + dthdr.Rows[i][20] + " as card_amt," + dthdr.Rows[i][21] + " as rref," + dthdr.Rows[i][22] + " as rcontr,isnull(" + dthdr.Rows[i][23] + ",0)  as taxfree_amt,isnull('" + dthdr.Rows[i][24] + "','') as note,isnull('" + dthdr.Rows[i][25] + "','') as mobile) as s"
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
                        if (con_dest.State != ConnectionState.Open)
                        {
                            con_dest.Open();
                           
                        }                  
                         comm.ExecuteNonQuery();
                         connec_with_ser = true;
                    }
                }
                for (int i = 0; i < dtdtl.Rows.Count; i++)
                {
                    string StrQuery = " MERGE pos_dtl as t"
                                    + " USING (select '" + dtdtl.Rows[i][0].ToString() + "' as brno, '" + dtdtl.Rows[i][1].ToString() + "' as slno, " + dtdtl.Rows[i][2].ToString() + " as ref," + dtdtl.Rows[i][3].ToString() + " as contr," + dtdtl.Rows[i][4].ToString() + " as type,'" + dtdtl.Rows[i][5].ToString() + "' as barcode,'" + dtdtl.Rows[i][6].ToString() + "' as name,'" + dtdtl.Rows[i][7].ToString() + "' as unit," + dtdtl.Rows[i][8].ToString() + " as price," + dtdtl.Rows[i][9].ToString() + " as qty," + dtdtl.Rows[i][10].ToString() + " as cost," + dtdtl.Rows[i][11].ToString() + " as is_req, '" + dtdtl.Rows[i][12].ToString() + "' as itemno, " + dtdtl.Rows[i][13].ToString() + " as srno," + dtdtl.Rows[i][14].ToString() + " as pkqty," + dtdtl.Rows[i][15].ToString() + " as discpc," + dtdtl.Rows[i][16].ToString() + " as tax_id," + dtdtl.Rows[i][17].ToString() + " as tax_amt," + dtdtl.Rows[i][18].ToString() + " as rqty," + dtdtl.Rows[i][19].ToString() + " as offr_amt) as s"
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
                }

                
               
            }
            catch { connec_with_ser = false; }
        } 


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Item_Srch sss = new Item_Srch(this);
            // sss.ShowDialog();
        }

        private void dataGridView1_KeyPress_1(object sender, KeyPressEventArgs e)
        {

            // dataGridView1_KeyUp((object) sender, (KeyEventArgs) e);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.KeyCode.Equals(Keys.Up))
                //{
                //    moveUp();
                //}
                //if (e.KeyCode.Equals(Keys.Down))
                //{
                //    moveDown();
                //}
                //e.Handled = true;

                int rowindex, columnindex;
               // rowindex = dataGridView1.CurrentRow.IsNewRow ? dataGridView1.CurrentCell.RowIndex - 1 : dataGridView1.CurrentCell.RowIndex;
                rowindex = dataGridView1.CurrentCell.RowIndex;
                columnindex = dataGridView1.CurrentCell.ColumnIndex;

                if (e.KeyCode == Keys.Space || e.KeyCode == Keys.F6)
                {
                    DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                    ////if (dataGridView1.CurrentRow.Cells[0].Value == null || dataGridView1.CurrentRow.Cells[0].Value == null)
                    ////{
                    ////    MessageBox.Show("لا يوجد صنف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ////    return;
                    ////}

                    // columnindex = columnindex + 1;
                    SqlDataAdapter da23;
                    string firstColumnValue3 = dataGridView1.Rows[rowindex].Cells[1].Value.ToString();
                    string baknam = "";
                    //   string nextColumnValue = dataGridView1.Rows[rowindex].Cells[columnindex+1].Value.ToString();
                    DataTable dtchkbar = daml.SELECT_QUIRY_only_retrn_dt("select count(*) from items_bc where item_no='" + firstColumnValue3 + "'");
                    if (Convert.ToInt32(dtchkbar.Rows[0][0]) > 1)
                    {
                        Search_All ns = new Search_All("chkb", firstColumnValue3);
                        ns.ShowDialog();
                        firstColumnValue3 = ns.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        baknam = ns.dataGridView1.CurrentRow.Cells[1].Value.ToString().StartsWith("عرض") ?  ns.dataGridView1.CurrentRow.Cells[1].Value.ToString() : "";
                        //da23 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_curcost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img,b.min_price mp from items i join items_bc b on b.item_no=i.item_no and b.barcode='" + firstColumnValue3 + "' b.sl_no='" + BL.CLS_Session.sl_prices + "' join taxs t on i.item_tax=t.tax_id where i.inactive=0", con2);
                        da23 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_curcost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img,b.min_price mp from items i join items_bc b on b.item_no=i.item_no and b.barcode='" + firstColumnValue3 + "' join taxs t on i.item_tax=t.tax_id where i.inactive=0", con2);

                    }
                    else
                    {
                        //  string value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
                        //string currentcell = dataGridView1.CurrentCell.Value.ToString();
                        // string firstColumnValue = dataGridView1.Rows[0].Cells[0].Value.ToString();
                        //string firstColumnValue =Convert.ToString(dataGridView1.CurrentCell

                        //SqlDataAdapter da2 = new SqlDataAdapter("select * from items where item_no='" + firstColumnValue + "' or item_no='" + nextColumnValue + "'", con2);
                        //  SqlDataAdapter da2 = new SqlDataAdapter("select * from DB.dbo.items where item_no='" + firstColumnValue + "'", con2);



                        // SqlDataAdapter da23 = new SqlDataAdapter("select i.*,t.tax_percent from items i, taxs t where i.item_tax=t.tax_id and i.item_no='" + firstColumnValue3 + "'", con2);
                        da23 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_curcost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img,b.min_price mp from items i join items_bc b on b.item_no=i.item_no and i.item_no='" + firstColumnValue3 + "' join taxs t on i.item_tax=t.tax_id where i.inactive=0", con2);
                    }
                    DataSet ds23 = new DataSet();
                    da23.Fill(ds23, "0");

                    if (ds23.Tables["0"].Rows.Count == 1)
                    {
                        if (Convert.ToDouble(ds23.Tables["0"].Rows[0][3]) <= 0)
                        {
                            player.Play();
                            MessageBox.Show("الصنف ليس لديه سعر", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                            return;
                        }

                        dataGridView1.Rows[rowindex].Cells[0].Value = ds23.Tables["0"].Rows[0]["item_barcode"];
                        dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToDouble(ds23.Tables["0"].Rows[0]["pkqty"]) > 1 ? ds23.Tables["0"].Rows[0][1].ToString().Trim() + " * " + Math.Round(Convert.ToDecimal(ds23.Tables["0"].Rows[0]["pkqty"]), 0).ToString().Trim() + " " + baknam  : ds23.Tables["0"].Rows[0][1].ToString().Trim();// ds23.Tables["0"].Rows[0][1];
                        dataGridView1.Rows[rowindex].Cells[3].Value = ds23.Tables["0"].Rows[0][5];
                        DataView dv1 = dtunits.DefaultView;
                        dv1.RowFilter = "unit_id in('" + ds23.Tables["0"].Rows[0][5] + "')";
                        DataTable dtNew = dv1.ToTable();
                        
                        // MessageBox.Show(dtNew.Rows[0][1].ToString());
                        // dataGridView2.DataSource = dtNew;
                        /*
                        dcombo.DataSource = dal.SelectData_query("select * from orpacking where pack_id in('" + dt2.Rows[0][1] + "','" + dt2.Rows[0][2] + "','" + dt2.Rows[0][4] + "')", null);
                        dcombo.DisplayMember = "pkname";
                        dcombo.ValueMember = "pack_id";
                        */
                        //ممتاز

                        dcombo.DataSource = dtNew;
                        dcombo.DisplayMember = "unit_name";
                        dcombo.ValueMember = "unit_id";

                        //  dcombo.DisplayIndex = 0;

                        dataGridView1.Rows[rowindex].Cells[3] = dcombo;
                        dataGridView1.Rows[rowindex].Cells[3].Value = dtNew.Rows[0][0];// ds23.Tables["0"].Rows[0][5];

                        dcombo.FlatStyle = FlatStyle.Flat;
                        if (dataGridView1.Rows[rowindex].Cells[4].Value == null)
                        {
                            dataGridView1.Rows[rowindex].Cells[4].Value = Convert.ToDouble("1");
                        }
                        //else
                        //{
                        //    dataGridView1.Rows[rowindex].Cells[4].Value = ds23.Tables["0"].Rows[0][3];
                        //}
                        
                        dataGridView1.Rows[rowindex].Cells[5].Value = ds23.Tables["0"].Rows[0][3];
                        
                        if (dataGridView1.Rows[rowindex].Cells[6].Value == null)
                        {
                            dataGridView1.Rows[rowindex].Cells[6].Value = 0;
                        }
                        //   dataGridView1.Rows[rowindex].Cells[5].Value = ds23.Tables["0"].Rows[0][3];
                        dataGridView1.Rows[rowindex].Cells[7].Value = ds23.Tables["0"].Rows[0][2];
                        dataGridView1.Rows[rowindex].Cells[9].Value = ds23.Tables["0"].Rows[0][11];
                        dataGridView1.Rows[rowindex].Cells[10].Value = ds23.Tables["0"].Rows[0]["pkqty"];
                        dataGridView1.Rows[rowindex].Cells[11].Value = ds23.Tables["0"].Rows[0]["i_tax"];
                        dataGridView1.Rows[rowindex].Cells[13].Value = ds23.Tables["0"].Rows[0]["img"];
                        dataGridView1.Rows[rowindex].Cells[14].Value = ds23.Tables["0"].Rows[0]["mp"];
                        dataGridView1.Rows[rowindex].Cells[15].Value = null;// ds23.Tables["0"].Rows[0]["ofr"];
                        dataGridView1.Rows[rowindex].Cells[8].Value = Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value);


                        if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + ds23.Tables[0].Rows[0][9]))
                        {
                            pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + ds23.Tables[0].Rows[0][9]);

                        }
                        else
                        {
                            pictureBox1.Image = Properties.Resources.background_button;

                        }
                        //   DataRow[] dtrvat = dttax.Select("tax_id =" + dataGridView1.CurrentRow.Cells[11].Value + "");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";

                        dataGridView1.Rows[rowindex].Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                        //DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                        ////  r.Cells[2] = dcombo;
                        //DataView dv1 = dtunits.DefaultView;
                        ////  dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                        //dv1.RowFilter = "unit_id in('" + dataGridView1.Rows[rowindex].Cells[3].Value + "')";
                        //DataTable dtNew = dv1.ToTable();
                        //dcombo.DataSource = dtNew;
                        //dcombo.DisplayMember = "unit_name";
                        //dcombo.ValueMember = "unit_id";
                        //dataGridView1.Rows[rowindex].Cells[3] = dcombo;
                        //dataGridView1.Rows[rowindex].Cells[3].Value = dtNew.Rows[0][0];

                        ////  dataGridView1[2, r.Index] = dcombo;
                        //// dataGridView1[2, r.Index].Value = dtNew.Rows[0][1];
                        //// dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                        //dcombo.FlatStyle = FlatStyle.Flat;
                        chk_qty_offer(dataGridView1.Rows[rowindex]);

                        // label1.Text = rowindex.ToString();
                        // passes the value through the constructor to the 
                        //   second form.
                        //   MySecondForm f2 = new MySecondForm(firstColumnValue);
                        //  f2.Show();
                        //if (dataGridView1.Rows[rowindex].Cells[columnindex + 3].Value != null)
                        //{
                        //label3.Text = (dataGridView1.Rows.Count - 1).ToString();

                        ////double sum1 = 0;
                        ////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                        ////{
                        ////    sum1 += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                        ////}
                        ////label3.Text = sum1.ToString();
                        //////}
                        //////else
                        //////{

                        //////}

                        //////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                        //////{

                        ////double sum = 0;
                        ////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                        ////{
                        ////    sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                        ////}
                        ////label5.Text = sum.ToString();


                        ////double sumcost = 0;
                        ////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                        ////{
                        ////    sumcost += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                        ////}
                        ////textBox2.Text = sumcost.ToString();



                        total();
                        if (BL.CLS_Session.use_cd)
                            disply(Convert.ToDecimal(dataGridView1.Rows[rowindex].Cells[5].Value), total1);

                        cssc.txt_iname.Text = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
                        cssc.txt_iprice.Text = dataGridView1.Rows[rowindex].Cells[5].Value.ToString();
                        cssc.txt_total.Text = total1.ToString();
                        // sum += Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[columnindex + 3].Value);
                        // sum += Convert.ToDouble(ds2.Tables["0"].Rows[0][2]);
                        // }
                        // label5.Text = sum.ToString();
                        //  rowindex = rowindex + 1;


                        // dataGridView1.CurrentCell = dataGridView1.Rows[rowindex + 1].Cells[0];



                        // if ((string)dataGridView1.CurrentRow.Cells[0].Value=="")
                        // if (dataGridView1.SelectedCells==null)


                        //  }

                    }



                    else
                    {

                        player.Play();
                        MessageBox.Show("الصنف غير موجود", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[1];
                        // dataGridView1.Rows[rowindex].Cells[1].Value = null;
                        dataGridView1.Rows.RemoveAt(rowindex);
                        dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                        // label3.Text = (dataGridView1.Rows.Count - 2).ToString();

                    }



                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (dataGridView1.CurrentCell.Value != null)
                    {
                        dataGridView1_KeyUp(sender, e);
                    }
                    else
                    {
                        // int i = dataGridView1.CurrentRow.Index;
                        //MessageBox.Show(i.ToString());
                        if (dataGridView1.RowCount > 1)
                        {
                            dataGridView1.ClearSelection();

                            if (slalwdesc || Convert.ToDouble(slpmaxdisc) > 0)
                            {
                                txt_desc.Enabled = true;
                                txt_dscper.Enabled = true;
                                txt_desc.Focus();
                                total();
                            }
                            else
                            {
                                txt_desc.Enabled = false;
                                txt_dscper.Enabled = false;
                                txt_recv.Focus();
                                total();
                            }
                        }
                    }


                }
                else
                {

                }

            }
            catch { }

        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {
           
            //try
            //{

                //if (dataGridView1.RowCount > 2)
                //{
                //    if (dataGridView1.CurrentRow.Cells[0].Value == dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)
                //    {
                //        dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value = Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value) + 1;
                //        dataGridView1.Rows[e.RowIndex - 1].Cells[8].Value = (Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value) / 100);// + Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value);
                //        dataGridView1.Rows[e.RowIndex - 1].Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value)) / 100) : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                //        total();
                //    }
                //}
            //}
            //catch { }
            
            /*
            if (dataGridView1.CurrentCell == dataGridView1[4, dataGridView1.CurrentRow.Index] || dataGridView1.CurrentCell == dataGridView1[5, dataGridView1.CurrentRow.Index] || dataGridView1.CurrentCell == dataGridView1[6, dataGridView1.CurrentRow.Index])
            {
                //  dataGridView1.CurrentRow.Cells[7].Value = ((Convert.ToDouble(filteredData.Rows[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)).ToString();
                dataGridView1.CurrentRow.Cells[8].Value = (Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) / 100);// + Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value);
                dataGridView1.CurrentRow.Cells[12].Value = (Convert.ToDouble(dataGridView1.CurrentRow.Cells["tax_percent"].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.CurrentRow.Cells["tax_percent"].Value)) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

            }
          */
            if (BL.CLS_Session.use_cd)
                disply(0, total1);

            cssc.txt_iname.Text = "";
            cssc.txt_iprice.Text = "0.00";
            cssc.txt_total.Text = total1.ToString();

            total();
        }

        private void dataGridView1_Move(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //int chk5 = dataGridView1.RowCount - 1;
            // dataGridView1.Rows.Add();
            /*
            Item_Srch_G itm = new Item_Srch_G();

            this.dataGridView1.CurrentRow.Cells[0].Value = itm.dataGridView1.CurrentRow.Cells[0].Value;

            itm.ShowDialog();
             */

            //DataGridViewCell cell = dataGridView1.CurrentRow.Cells[0];
            //dataGridView1.CurrentCell = cell;
            //dataGridView1.BeginEdit(true);
            // dataGridView1.Rows.Add(1);
            // this.dataGridView1.CurrentRow.Cells[0].Value = itm.dataGridView1.CurrentRow.Cells[0].Value;


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //  textBox3_KeyUp((object) sender, (KeyEventArgs) e);
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            // rechk:

            if (e.KeyCode == Keys.Enter)
            {
                
                //var matchedRow = dataGridView1.Rows.OfType<DataGridViewRow>()
                //           .FirstOrDefault(row => row.Cells[0].Value != null &&
                //                                row.Cells[0].Value.Equals(dataGridView1.CurrentRow.Cells[0].Value));
                ////// if (matchedRow != null && matchedRow.Cells[15].Value == null)
                //if (matchedRow != null)
                //{
                //    int qty = (int)matchedRow.Cells[4].Value + 1;
                //    //  double price = (double)matchedRow.Cells[3].Value;
                //    matchedRow.Cells[4].Value = qty;
                //    matchedRow.Cells[8].Value = (Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) - ((Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) * Convert.ToDouble(matchedRow.Cells[6].Value) / 100);
                //    matchedRow.DefaultCellStyle.BackColor = Color.PaleGreen;
                //    total();
                //    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                //    btnDown(matchedRow);
                //    chk_qty_offer(matchedRow);
                //}

                // SqlDataAdapter da23 = new SqlDataAdapter("select i.*,t.tax_percent from items i, taxs t where i.item_tax=t.tax_id and i.item_no='" + textBox3.Text + "'", con2);

               // SqlDataAdapter da23 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_curcost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img,b.min_price mp from items i join items_bc b on b.item_no=i.item_no and i.item_no='" + textBox3.Text.Trim() + "' join taxs t on i.item_tax=t.tax_id", con2);
                SqlDataAdapter da23 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_curcost,i.item_price as item_price,i.item_barcode as  item_barcode,i.item_unit pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,1 pkqty,i.item_tax i_tax,i.item_image img,i.min_price mp from items i join taxs t on i.item_tax=t.tax_id  and i.item_no='" + textBox3.Text.Trim() + "' where i.inactive=0", con2);
                
                DataSet ds23 = new DataSet();
                da23.Fill(ds23, "0");

                if (ds23.Tables["0"].Rows.Count > 0)
                {

                    if (Convert.ToDouble(ds23.Tables["0"].Rows[0][3]) <= 0)
                    {
                        player.Play();
                        MessageBox.Show("الصنف ليس لديه سعر", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //  dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                        textBox3.Text = "";
                        dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                        return;
                    }

                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                    row.Cells[0].Value = ds23.Tables["0"].Rows[0][4];
                    row.Cells[1].Value = ds23.Tables["0"].Rows[0][0];
                    row.Cells[2].Value = ds23.Tables["0"].Rows[0][1];
                    row.Cells[3].Value = ds23.Tables["0"].Rows[0][5];
                    //  r.Cells[2] = dcombo;
                    //DataView dv1 = dtunits.DefaultView;
                    ////  dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                    //dv1.RowFilter = "unit_id in('" + ds23.Tables["0"].Rows[0][5] + "')";
                    //DataTable dtNew = dv1.ToTable();
                    //dcombo.DataSource = dtNew;
                    //dcombo.DisplayMember = "unit_name";
                    //dcombo.ValueMember = "unit_id";
                    //row.Cells[3] = dcombo;
                    //row.Cells[3].Value = ds23.Tables["0"].Rows[0][5];

                    ////  dataGridView1[2, r.Index] = dcombo;
                    //// dataGridView1[2, r.Index].Value = dtNew.Rows[0][1];
                    //// dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                    //dcombo.FlatStyle = FlatStyle.Flat;

                    row.Cells[9].Value = ds23.Tables["0"].Rows[0][11];
                    row.Cells[10].Value = ds23.Tables["0"].Rows[0]["pkqty"];
                    row.Cells[11].Value = ds23.Tables["0"].Rows[0]["i_tax"];
                    row.Cells[13].Value = ds23.Tables["0"].Rows[0]["img"];
                    row.Cells[14].Value = ds23.Tables["0"].Rows[0]["mp"];

                    if (row.Cells[4].Value == null)
                    {
                        row.Cells[4].Value = 1;
                    }
                    if (row.Cells[5].Value == null)
                    {
                        row.Cells[5].Value = ds23.Tables["0"].Rows[0][3];
                    }
                    if (row.Cells[6].Value == null)
                    {
                        row.Cells[6].Value = 0;
                    }
                    //  row.Cells[5].Value = ds23.Tables["0"].Rows[0][3];
                    row.Cells[7].Value = ds23.Tables["0"].Rows[0][2];
                    row.Cells[8].Value = Convert.ToDouble(row.Cells[4].Value) * Convert.ToDouble(row.Cells[5].Value);

                    if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + ds23.Tables[0].Rows[0][9]))
                    {
                        pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + ds23.Tables[0].Rows[0][9]);

                    }
                    else
                    {
                        pictureBox1.Image = Properties.Resources.background_button;

                    }

                    //  DataRow[] dtrvat = dttax.Select("tax_id =" + row.Cells[11].Value + "");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";

                    //row.Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : (Convert.ToDouble(row.Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(row.Cells[9].Value)) * ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) - ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) * Convert.ToDouble(row.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                    row.Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(row.Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) - ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) * Convert.ToDouble(row.Cells[6].Value)) / 100) : (Convert.ToDouble(row.Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(row.Cells[9].Value)) * ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) - ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) * Convert.ToDouble(row.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();


                    dataGridView1.Rows.Add(row);

                    //DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                    //    //  r.Cells[2] = dcombo;
                    //    DataView dv1 = dtunits.DefaultView;
                    //    //  dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                    //    dv1.RowFilter = "unit_id in('" + row.Cells[3].Value + "')";
                    //    DataTable dtNew = dv1.ToTable();
                    //    dcombo.DataSource = dtNew;
                    //    dcombo.DisplayMember = "unit_name";
                    //    dcombo.ValueMember = "unit_id";
                    //    row.Cells[3] = dcombo;
                    //    row.Cells[3].Value = dtNew.Rows[0][0];

                    //    //  dataGridView1[2, r.Index] = dcombo;
                    //    // dataGridView1[2, r.Index].Value = dtNew.Rows[0][1];
                    //    // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                    //    dcombo.FlatStyle = FlatStyle.Flat;

                    chk_qty_offer(row);

                    DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                    //  r.Cells[2] = dcombo;
                    DataView dv1 = dtunits.DefaultView;
                    //  dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                    dv1.RowFilter = "unit_id in('" + row.Cells[3].Value.ToString() + "')";
                    DataTable dtNew = dv1.ToTable();
                    dcombo.DataSource = dtNew;
                    dcombo.DisplayMember = "unit_name";
                    dcombo.ValueMember = "unit_id";

                    row.Cells[3] = dcombo;
                    row.Cells[3].Value = dtNew.Rows[0][0];

                    //  dataGridView1[2, r.Index] = dcombo;
                    // dataGridView1[2, r.Index].Value = dtNew.Rows[0][1];
                    // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                    dcombo.FlatStyle = FlatStyle.Flat;

                    total();
                    textBox3.Text = "";


                    //////dataGridView1.Rows[rowindex].Cells[0].Value = ds23.Tables["0"].Rows[0][0];
                    //////dataGridView1.Rows[rowindex].Cells[2].Value = ds23.Tables["0"].Rows[0][1];
                    //////dataGridView1.Rows[rowindex].Cells[3].Value = ds23.Tables["0"].Rows[0][5];
                    //////if (dataGridView1.Rows[rowindex].Cells[4].Value == null)
                    //////{
                    //////    dataGridView1.Rows[rowindex].Cells[4].Value = 1;
                    //////}
                    //////dataGridView1.Rows[rowindex].Cells[5].Value = ds23.Tables["0"].Rows[0][3];
                    //////dataGridView1.Rows[rowindex].Cells[6].Value = ds23.Tables["0"].Rows[0][2];

                    // label1.Text = rowindex.ToString();
                    // passes the value through the constructor to the 
                    //   second form.
                    //   MySecondForm f2 = new MySecondForm(firstColumnValue);
                    //  f2.Show();
                    //if (dataGridView1.Rows[rowindex].Cells[columnindex + 3].Value != null)
                    //{
                    //label3.Text = (dataGridView1.Rows.Count - 1).ToString();

                    ////////double sum1 = 0;
                    ////////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    ////////{
                    ////////    sum1 += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                    ////////}
                    ////////label3.Text = sum1.ToString();
                    //////////}
                    //////////else
                    //////////{

                    //////////}

                    //////////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    //////////{

                    ////////double sum = 0;
                    ////////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    ////////{
                    ////////    sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                    ////////}
                    ////////label5.Text = sum.ToString();


                    ////////double sumcost = 0;
                    ////////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    ////////{
                    ////////    sumcost += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                    ////////}
                    ////////textBox2.Text = sumcost.ToString();

                    total();
                    if (BL.CLS_Session.use_cd)
                        //  disply(Convert.ToDecimal(row.Cells[5].Value), total1);
                        disply(Convert.ToDecimal(ds23.Tables["0"].Rows[0][3]), total1);

                    cssc.txt_iname.Text = ds23.Tables["0"].Rows[0][1].ToString();
                    cssc.txt_iprice.Text = ds23.Tables["0"].Rows[0][3].ToString();
                    cssc.txt_total.Text = total1.ToString();
                    // sum += Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[columnindex + 3].Value);
                    // sum += Convert.ToDouble(ds2.Tables["0"].Rows[0][2]);
                    // }
                    // label5.Text = sum.ToString();



                    // dataGridView1.CurrentCell = dataGridView1.Rows[rowindex + 1].Cells[0];



                    // if ((string)dataGridView1.CurrentRow.Cells[0].Value=="")
                    // if (dataGridView1.SelectedCells==null)


                    //  }

                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                   // dataGridView1.Select();
                    dataGridView1.Select();

                }
                else
                {
                    if (dataGridView1.RowCount > 0 && txt_recv.Text == "")
                    {
                        txt_desc.Focus();
                    }
                    else
                    {
                       
                      //  MessageBox.Show("not found", "error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        textBox3.Text = "";
                        dataGridView1.Select();
                    }
                }



            }
            //else
            //    dataGridView1.Focus();

            if (e.KeyCode == Keys.F8)
            {
                //  dataGridView1.Rows.Add();
                // Item_Srch_G itm = new Item_Srch_G();
                // itm.ShowDialog();

                Search_All itm = new Search_All("2", "Pos");
               // itm.Size = new Size(626, 626);
                itm.ShowDialog();
                // this.dataGridView1.CurrentRow.Cells[0].Value = itm.dataGridView1.CurrentRow.Cells[0].Value;
                //   dataGridView1..Cells[0].KeyEntersEditMode();
                //DataGridViewCell cell = dataGridView1.CurrentRow.Cells[0];
                //dataGridView1.CurrentCell = cell;
                //dataGridView1.BeginEdit(true);
                try
                {

                    textBox3.Text = itm.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    //SendKeys.Send("Enter");
                    // goto rechk;
                }
                catch
                {
                }
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            datval.ValidateText_numric(txt_recv);
            if (string.IsNullOrEmpty(txt_recv.Text))
                txt_recv.Text = "0";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_recv.Text == "")
                {
                    int chk2 = dataGridView1.RowCount - 1;
                    dataGridView1.Focus();
                    // dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex]
                    dataGridView1.Rows[chk2].Cells[0].Selected = true;
                }
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //  dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[0];
            if (dataGridView1.Rows.Count > 1)
                btn_Exit.Enabled = false;
            else
                btn_Exit.Enabled = sl_alowexit ? true : false;

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ////var matchedRow = dataGridView1.Rows.OfType<DataGridViewRow>()
                ////            .FirstOrDefault(row => row.Cells[0].Value != null && row.Cells[15].Value == null &&
                ////                                 row.Cells[0].Value.Equals(dataGridView1.CurrentRow.Cells[0].Value));
                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4])
                {
                    dataGridView1.CurrentRow.Cells[4].Value = Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value);
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                }
                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[1])
                {
                    var matchedRow = dataGridView1.Rows.OfType<DataGridViewRow>()
                               .FirstOrDefault(row => row.Cells[dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim().StartsWith(BL.CLS_Session.mizansart) ? 16 : 0].Value != null && //row.Cells[15].Value == null &&
                                   //   row.Cells[0].Value.ToString().Trim().Equals(dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim()));
                                  row.Cells[dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim().StartsWith(BL.CLS_Session.mizansart) ? 16 : 0].Value.ToString().Trim().Equals(dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim()));
                                  
                    //var matchedRow = dataGridView1.Rows.OfType<DataGridViewRow>()
                    //           .FirstOrDefault(row => (row.Cells[0].Value != null &&
                    //                                row.Cells[0].Value.Equals(dataGridView1.CurrentRow.Cells[0].Value)));
                    //// if (matchedRow != null && matchedRow.Cells[15].Value == null)
                    if (matchedRow != null && BL.CLS_Session.imp_itm)
                    {
                        double qty = (double)matchedRow.Cells[4].Value + 1;
                        //  double price = (double)matchedRow.Cells[3].Value;
                        matchedRow.Cells[4].Value = qty;
                        matchedRow.Cells[8].Value = (Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) - ((Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) * Convert.ToDouble(matchedRow.Cells[6].Value) / 100);
                        matchedRow.Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(matchedRow.Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) - ((Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) * Convert.ToDouble(matchedRow.Cells[6].Value)) / 100) : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(matchedRow.Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) - ((Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) * Convert.ToDouble(matchedRow.Cells[6].Value)) / 100) : (Convert.ToDouble(matchedRow.Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(matchedRow.Cells[9].Value)) * ((Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) - ((Convert.ToDouble(matchedRow.Cells[5].Value) * Convert.ToDouble(matchedRow.Cells[4].Value)) * Convert.ToDouble(matchedRow.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                        matchedRow.DefaultCellStyle.BackColor = Color.PaleGreen;
                        total();
                      //  dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                        btnDown(matchedRow);
                        chk_qty_offer(matchedRow);
                        total();
                      //  return;
                        //dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                        //dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                    }
                }

                // return;
                // dataGridView1.Rows[matchedRow.Index].Selected = true;
                // moveDown();
                //if (dataGridView1.RowCount > 1 && dataGridView1.CurrentRow.Cells[0].Value == dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[0].Value)
                //{
                //    MessageBox.Show("prp");
                //    if (dataGridView1.CurrentRow.Cells[0].Value == dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[0].Value)
                //    {
                //        MessageBox.Show("prp");
                //        //dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value = Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value) + 1;
                //        //dataGridView1.Rows[e.RowIndex - 1].Cells[8].Value = (Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value) / 100);// + Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value);
                //        //dataGridView1.Rows[e.RowIndex - 1].Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value)) / 100) : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                //        //total();
                //        //dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                //        //return;
                //    }
                //}


                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[6])
                {
                    if (dataGridView1.CurrentCell.Value == null || dataGridView1.CurrentCell.Value == DBNull.Value || string.IsNullOrEmpty(dataGridView1.CurrentCell.Value.ToString()))
                    {
                        dataGridView1.CurrentCell.Value = 0;
                    }
                    if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4] && dataGridView1.CurrentRow.Cells[4].Value.ToString().Length > 6)
                    {
                        MessageBox.Show("الكمية المدخلة كبيرة جدا يرجى التاكد من الكمية", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView1.CurrentRow.Cells[4].Value = 1;
                        SendKeys.Send("{Home}");
                        //return;
                    }
                    if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5] && dataGridView1.CurrentRow.Cells[5].Value.ToString().Length > 6)
                    {
                        MessageBox.Show("السعر المدخل كبير جدا يرجى التاكد من السعر", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView1.CurrentRow.Cells[5].Value = oval;
                        SendKeys.Send("{Home}");
                        //return;
                    }

                    if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5])
                    {// SendKeys.Send("{Home}");
                        dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                        dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                    }
                }

            }
            catch { }

            int rowindex, columnindex;
            try
            {
                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0] && dataGridView1.CurrentRow.Cells[0].Value.ToString().StartsWith("+") && dataGridView1.CurrentRow.Cells[0].Value.ToString().Length < 6 && Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_chgqty"]) && !dataGridView1.CurrentRow.Cells[0].Value.ToString().Equals("+") && dataGridView1.CurrentRow.Cells[0].Value.ToString().Length <= 4)
                {
                    dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value = Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[0].Value.ToString().Replace("+", ""));
                    dataGridView1.Rows[e.RowIndex - 1].Cells[8].Value = (Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value) / 100);// + Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value);
                    dataGridView1.Rows[e.RowIndex - 1].Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value)) / 100) : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                    chk_qty_offer(dataGridView1.Rows[e.RowIndex - 1]);
                    total();

                    cssc.txt_iname.Text = "";
                    cssc.txt_iprice.Text = "0.00";
                    cssc.txt_total.Text = total1.ToString();
                }
                else
                {
                    if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0] && (dataGridView1.CurrentRow.Cells[0].Value.ToString().Equals("+") || (dataGridView1.CurrentRow.Cells[0].Value.ToString().StartsWith("+") && dataGridView1.CurrentRow.Cells[0].Value.ToString().Length > 4)))
                    {
                        player.Play();
                        MessageBox.Show("الصنف غير موجود", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


                //  string firstCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                // nothing is selected
                //if (dataGridView1.SelectedRows.Count == 0)
                //    return;
                // int chk4 = dataGridView1.RowCount - 1;
                if (1 > 0)
                {
                    DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                    //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down || e.KeyCode == Keys.Right || e.KeyCode == Keys.Left || e.KeyCode == Keys.Tab)
                    // if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down || e.KeyCode == Keys.Tab)
                    if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[1])
                    {
                        // int rowindex = dataGridView1.CurrentCell.RowIndex -1;
                        //= 

                        // int chk1 = dataGridView1.Rows.Count - (dataGridView1.AllowUserToAddRows ? 1 : 0);
                        // int rowindex = (dataGridView1.CurrentCell.RowIndex - 1 < 0) ? dataGridView1.CurrentCell.RowIndex : dataGridView1.CurrentCell.RowIndex - 1;
                        rowindex = dataGridView1.CurrentCell.RowIndex;
                        columnindex = dataGridView1.CurrentCell.ColumnIndex;




                        //string firstColumnValue = dataGridView1.Rows[rowindex].Cells[columnindex].Value.ToString();
                        //if (dataGridView1.Rows[rowindex].Cells[columnindex].Value.ToString().Trim() == null)
                        //  if (dataGridView1.Rows[rowindex].Cells[columnindex].Value != null || dataGridView1.Rows[rowindex].Cells[columnindex+1].Value != null)
                        //  if (dataGridView1.Rows[rowindex].Cells[columnindex].Value != null)
                        if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0] && dataGridView1.Rows[rowindex].Cells[0].Value != null)
                        {

                            string firstColumnValue = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
                            //   string nextColumnValue = dataGridView1.Rows[rowindex].Cells[columnindex+1].Value.ToString();

                            //  string value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
                            //string currentcell = dataGridView1.CurrentCell.Value.ToString();
                            // string firstColumnValue = dataGridView1.Rows[0].Cells[0].Value.ToString();
                            //string firstColumnValue =Convert.ToString(dataGridView1.CurrentCell

                            //SqlDataAdapter da2 = new SqlDataAdapter("select * from items where item_no='" + firstColumnValue + "' or item_no='" + nextColumnValue + "'", con2);
                            //  SqlDataAdapter da2 = new SqlDataAdapter("select * from DB.dbo.items where item_no='" + firstColumnValue + "'", con2);



                            // SqlDataAdapter da2 = new SqlDataAdapter("select * from items where item_barcode='" + firstColumnValue + "'", con2);
                            SqlDataAdapter da2 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_curcost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img,b.min_price mp,u.unit_name grpn from items i join items_bc b on b.item_no=i.item_no and b.barcode='" + firstColumnValue + "' and b.sl_no='" + BL.CLS_Session.sl_prices + "' join taxs t on i.item_tax=t.tax_id join units u on u.unit_id=b.pack where i.inactive=0", con2);

                            DataSet ds2 = new DataSet();
                            da2.Fill(ds2, "0");

                            if (ds2.Tables["0"].Rows.Count == 1)
                            {
                                string uname = ds2.Tables["0"].Rows[0]["grpn"].ToString().StartsWith("عرض")?  ds2.Tables["0"].Rows[0]["grpn"].ToString()  : "";
                                if (Convert.ToDouble(ds2.Tables["0"].Rows[0][3]) <= 0)
                                {
                                    player.Play();
                                    MessageBox.Show("الصنف ليس لديه سعر", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                                    return;
                                }

                                dataGridView1.Rows[rowindex].Cells[1].Value = ds2.Tables["0"].Rows[0][0];
                                dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToDouble(ds2.Tables["0"].Rows[0]["pkqty"]) > 1 ? ds2.Tables["0"].Rows[0][1].ToString().Trim() + " * " + Math.Round(Convert.ToDecimal(ds2.Tables["0"].Rows[0]["pkqty"]), 0).ToString().Trim() + " " + uname : ds2.Tables["0"].Rows[0][1].ToString().Trim();
                                dataGridView1.Rows[rowindex].Cells[3].Value = ds2.Tables["0"].Rows[0][5];
                                DataView dv1 = dtunits.DefaultView;
                                // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                                dv1.RowFilter = "unit_id in('" + ds2.Tables["0"].Rows[0][5] + "')";
                                dv1.Sort = "unit_id";
                                DataTable dtNew = dv1.ToTable();
                                // MessageBox.Show(dtNew.Rows[0][1].ToString());
                                // dataGridView2.DataSource = dtNew;
                                /*
                                dcombo.DataSource = dal.SelectData_query("select * from orpacking where pack_id in('" + dt2.Rows[0][1] + "','" + dt2.Rows[0][2] + "','" + dt2.Rows[0][4] + "')", null);
                                dcombo.DisplayMember = "pkname";
                                dcombo.ValueMember = "pack_id";
                                */
                                //ممتاز

                                dcombo.DataSource = dtNew;
                                dcombo.DisplayMember = "unit_name";
                                dcombo.ValueMember = "unit_id";

                                //  dcombo.DisplayIndex = 0;

                                dataGridView1.Rows[rowindex].Cells[3] = dcombo;
                                dataGridView1.CurrentRow.Cells[3].Value = ds2.Tables["0"].Rows[0][5];

                                dcombo.FlatStyle = FlatStyle.Flat;

                                dataGridView1.Rows[rowindex].Cells[10].Value = ds2.Tables["0"].Rows[0]["pkqty"];
                                dataGridView1.Rows[rowindex].Cells[9].Value = ds2.Tables["0"].Rows[0][11];
                                //  row.Cells[10].Value = ds23.Tables["0"].Rows[0]["pkqty"];
                                dataGridView1.Rows[rowindex].Cells[11].Value = ds2.Tables["0"].Rows[0]["i_tax"];
                                dataGridView1.Rows[rowindex].Cells[13].Value = ds2.Tables["0"].Rows[0]["img"];
                                dataGridView1.Rows[rowindex].Cells[14].Value = ds2.Tables["0"].Rows[0]["mp"];
                                if (dataGridView1.Rows[rowindex].Cells[4].Value == null)
                                {
                                    dataGridView1.Rows[rowindex].Cells[4].Value = Convert.ToDouble("1");
                                }
                                if (dataGridView1.Rows[rowindex].Cells[5].Value == null)
                                {
                                    dataGridView1.Rows[rowindex].Cells[5].Value = ds2.Tables["0"].Rows[0][3];
                                }
                                if (dataGridView1.Rows[rowindex].Cells[6].Value == null)
                                {
                                    dataGridView1.Rows[rowindex].Cells[6].Value = 0;
                                }
                                //  dataGridView1.Rows[rowindex].Cells[5].Value = ds2.Tables["0"].Rows[0][3];
                                dataGridView1.Rows[rowindex].Cells[7].Value = ds2.Tables["0"].Rows[0][2];
                                //  dataGridView1.Rows[rowindex].Cells[9].Value = ds2.Tables["0"].Rows[0][11];

                                dataGridView1.Rows[rowindex].Cells[8].Value = Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value);
                                // rowindex = rowindex + 1;
                                if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + ds2.Tables[0].Rows[0][9]))
                                {
                                    pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + ds2.Tables[0].Rows[0][9]);

                                }
                                else
                                {
                                    pictureBox1.Image = Properties.Resources.background_button;

                                }
                                //  DataRow[] dtrvat = dttax.Select("tax_id =" + dataGridView1.CurrentRow.Cells[11].Value + "");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";

                                dataGridView1.Rows[rowindex].Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();


                                chk_qty_offer(dataGridView1.Rows[rowindex]);
                                // label1.Text = rowindex.ToString();
                                // passes the value through the constructor to the 
                                //   second form.
                                //   MySecondForm f2 = new MySecondForm(firstColumnValue);
                                //  f2.Show();
                                //if (dataGridView1.Rows[rowindex].Cells[columnindex + 3].Value != null)
                                //{
                                //label3.Text = (dataGridView1.Rows.Count - 1).ToString();


                                //-------------------------------
                                //double sum1 = 0;
                                //for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                                //{
                                //    sum1 += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                //}
                                //label3.Text = sum1.ToString();
                                ////}
                                ////else
                                ////{

                                ////}

                                ////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                                ////{

                                //double sum = 0;
                                //for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                                //{
                                //    sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                //}
                                //label5.Text = sum.ToString();


                                //double sumcost = 0;
                                //for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                                //{
                                //    sumcost += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                                //}
                                //textBox2.Text = sumcost.ToString();
                                //-------------------------------------------
                                total();
                                if (BL.CLS_Session.use_cd)
                                    disply(Convert.ToDecimal(dataGridView1.Rows[rowindex].Cells[5].Value), total1);

                                cssc.txt_iname.Text = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
                                cssc.txt_iprice.Text = dataGridView1.Rows[rowindex].Cells[5].Value.ToString();
                                cssc.txt_total.Text = total1.ToString();

                                // sum += Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[columnindex + 3].Value);
                                // sum += Convert.ToDouble(ds2.Tables["0"].Rows[0][2]);
                                // }
                                // label5.Text = sum.ToString();
                                //   rowindex = rowindex + 1;

                                this.dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.NewRowIndex];
                                // rowindex = rowindex + 1;

                                // this.dataGridView1.CurrentCell = this.dataGridView1[dataGridView1.CurrentRow.Index, 0];
                                //  dataGridView1.CurrentCell = dataGridView1.Rows[rowindex+1].Cells[0];
                            }






                            else
                            {
                                if (firstColumnValue.StartsWith(BL.CLS_Session.mizansart))
                                {

                                    //   SqlDataAdapter da3 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img from items i join items_bc b on b.item_no=i.item_no and i.item_no='" + "21-" + firstColumnValue.Substring(2, 4) + "' join taxs t on i.item_tax=t.tax_id", con2);
                                    SqlDataAdapter da3 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_curcost, i.item_price,item_barcode,1 as pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,1 pkqty,i.item_tax i_tax,i.item_image img,i.min_price mp from items i  join taxs t on i.item_tax=t.tax_id and i.item_no='" + BL.CLS_Session.mizansart + "-" + firstColumnValue.Substring(2, BL.CLS_Session.mizanitemlen) + "' where i.inactive=0", con2);
                                    DataSet ds3 = new DataSet();
                                    da3.Fill(ds3, "0");

                                    if (ds3.Tables["0"].Rows.Count == 1)
                                    {
                                        if (Convert.ToDouble(ds3.Tables["0"].Rows[0][3]) <= 0)
                                        {
                                            player.Play();
                                            MessageBox.Show("الصنف ليس لديه سعر", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                                            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                                            return;
                                        }
                                        dataGridView1.Rows[rowindex].Cells["mizanbc"].Value = firstColumnValue;
                                        dataGridView1.Rows[rowindex].Cells[0].Value = ds3.Tables["0"].Rows[0][0];
                                        dataGridView1.Rows[rowindex].Cells[1].Value = ds3.Tables["0"].Rows[0][0];
                                        dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToDouble(ds3.Tables["0"].Rows[0]["pkqty"]) > 1 ? ds3.Tables["0"].Rows[0][1].ToString().Trim() + " * " + Math.Round(Convert.ToDecimal(ds3.Tables["0"].Rows[0]["pkqty"]), 0).ToString().Trim() : ds3.Tables["0"].Rows[0][1].ToString().Trim();
                                        dataGridView1.Rows[rowindex].Cells[3].Value = ds3.Tables["0"].Rows[0][5];

                                        dataGridView1.Rows[rowindex].Cells[10].Value = ds3.Tables["0"].Rows[0]["pkqty"];
                                        dataGridView1.Rows[rowindex].Cells[9].Value = ds3.Tables["0"].Rows[0][11];
                                        //  row.Cells[10].Value = ds23.Tables["0"].Rows[0]["pkqty"];
                                        dataGridView1.Rows[rowindex].Cells[11].Value = ds3.Tables["0"].Rows[0]["i_tax"];
                                        dataGridView1.Rows[rowindex].Cells[13].Value = ds3.Tables["0"].Rows[0]["img"];
                                        dataGridView1.Rows[rowindex].Cells[14].Value = ds3.Tables["0"].Rows[0]["mp"];
                                        if (dataGridView1.Rows[rowindex].Cells[4].Value == null)
                                        {
                                            if (BL.CLS_Session.mizantype.ToString().Equals("0"))
                                                dataGridView1.Rows[rowindex].Cells[4].Value = Convert.ToDouble("1");// Convert.ToDecimal(firstColumnValue.Substring(6, 4) + "." + firstColumnValue.Substring(10, 2)) / Convert.ToDecimal(ds3.Tables["0"].Rows[0]["item_price"]);
                                            else
                                                dataGridView1.Rows[rowindex].Cells[4].Value = Convert.ToDecimal(firstColumnValue.Substring(BL.CLS_Session.mizansart.Length + BL.CLS_Session.mizanitemlen, BL.CLS_Session.mizanpricelen - 3) + "." + firstColumnValue.Substring(BL.CLS_Session.mizansart.Length + BL.CLS_Session.mizanitemlen + BL.CLS_Session.mizanpricelen - 3, 3));
                                        }
                                        if (dataGridView1.Rows[rowindex].Cells[5].Value == null)
                                        {
                                            if (BL.CLS_Session.mizantype.ToString().Equals("0"))
                                                dataGridView1.Rows[rowindex].Cells[5].Value = Convert.ToDecimal(firstColumnValue.Substring(BL.CLS_Session.mizansart.Length + BL.CLS_Session.mizanitemlen, BL.CLS_Session.mizanpricelen - 2) + "." + firstColumnValue.Substring(BL.CLS_Session.mizansart.Length + BL.CLS_Session.mizanitemlen + BL.CLS_Session.mizanpricelen - 2, 2));//ds2.Tables["0"].Rows[0][3];
                                            else
                                                dataGridView1.Rows[rowindex].Cells[5].Value = ds3.Tables["0"].Rows[0][3];
                                        }
                                        if (dataGridView1.Rows[rowindex].Cells[6].Value == null)
                                        {
                                            dataGridView1.Rows[rowindex].Cells[6].Value = 0;
                                        }
                                        //  dataGridView1.Rows[rowindex].Cells[5].Value = ds2.Tables["0"].Rows[0][3];
                                        dataGridView1.Rows[rowindex].Cells[7].Value = ds3.Tables["0"].Rows[0][2];
                                        //  dataGridView1.Rows[rowindex].Cells[9].Value = ds2.Tables["0"].Rows[0][11];

                                        dataGridView1.Rows[rowindex].Cells[8].Value = Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value);
                                        // rowindex = rowindex + 1;
                                        if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + ds3.Tables[0].Rows[0][9]))
                                        {
                                            pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + ds3.Tables[0].Rows[0][9]);

                                        }
                                        else
                                        {
                                            pictureBox1.Image = Properties.Resources.background_button;

                                        }
                                        //  DataRow[] dtrvat = dttax.Select("tax_id =" + dataGridView1.CurrentRow.Cells[11].Value + "");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";

                                        dataGridView1.Rows[rowindex].Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                                        chk_qty_offer(dataGridView1.Rows[rowindex]);
                                        // label1.Text = rowindex.ToString();
                                        // passes the value through the constructor to the 
                                        //   second form.
                                        //   MySecondForm f2 = new MySecondForm(firstColumnValue);
                                        //  f2.Show();
                                        //if (dataGridView1.Rows[rowindex].Cells[columnindex + 3].Value != null)
                                        //{
                                        //label3.Text = (dataGridView1.Rows.Count - 1).ToString();


                                        //-------------------------------
                                        //double sum1 = 0;
                                        //for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                                        //{
                                        //    sum1 += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                        //}
                                        //label3.Text = sum1.ToString();
                                        ////}
                                        ////else
                                        ////{

                                        ////}

                                        ////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                                        ////{

                                        //double sum = 0;
                                        //for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                                        //{
                                        //    sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                        //}
                                        //label5.Text = sum.ToString();


                                        //double sumcost = 0;
                                        //for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                                        //{
                                        //    sumcost += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                                        //}
                                        //textBox2.Text = sumcost.ToString();
                                        //-------------------------------------------
                                        total();
                                        if (BL.CLS_Session.use_cd)
                                            disply(Convert.ToDecimal(dataGridView1.Rows[rowindex].Cells[5].Value), total1);

                                        cssc.txt_iname.Text = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
                                        cssc.txt_iprice.Text = dataGridView1.Rows[rowindex].Cells[5].Value.ToString();
                                        cssc.txt_total.Text = total1.ToString();

                                        // sum += Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[columnindex + 3].Value);
                                        // sum += Convert.ToDouble(ds2.Tables["0"].Rows[0][2]);
                                        // }
                                        // label5.Text = sum.ToString();
                                        //   rowindex = rowindex + 1;

                                        this.dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.NewRowIndex];
                                        // rowindex = rowindex + 1;

                                        // this.dataGridView1.CurrentCell = this.dataGridView1[dataGridView1.CurrentRow.Index, 0];
                                        //  dataGridView1.CurrentCell = dataGridView1.Rows[rowindex+1].Cells[0];



                                    }
                                    else
                                    {
                                        player.Play();
                                        MessageBox.Show("الصنف غير موجود", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        // MessageBox.Show("الصنف غير موجود");


                                        //if (dataGridView1.CurrentCell.RowIndex == dataGridView1.NewRowIndex)
                                        //{
                                        //    // user is in the new row, disable companyols.
                                        ////}
                                        ////if (dataGridView1.CurrentCell.DefaultNewRowValue == true)
                                        ////{

                                        //}
                                        //else
                                        //{

                                        if (dataGridView1.Rows.Count >= 0)
                                        {
                                            dataGridView1.Rows.RemoveAt(rowindex);
                                            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                                        }
                                        //}
                                        // dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex];
                                        // dataGridView1.Rows[rowindex].Cells[columnindex].Value = null;
                                        // label3.Text = (dataGridView1.Rows.Count - 2).ToString();
                                    }



                                }
                                else
                                {
                                    if (!dataGridView1.CurrentRow.Cells[0].Value.ToString().StartsWith("+"))
                                    {
                                        player.Play();
                                        MessageBox.Show("الصنف غير موجود", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    // MessageBox.Show("الصنف غير موجود");


                                    //if (dataGridView1.CurrentCell.RowIndex == dataGridView1.NewRowIndex)
                                    //{
                                    //    // user is in the new row, disable companyols.
                                    ////}
                                    ////if (dataGridView1.CurrentCell.DefaultNewRowValue == true)
                                    ////{

                                    //}
                                    //else
                                    //{

                                    if (dataGridView1.Rows.Count >= 0)
                                    {
                                        dataGridView1.Rows.RemoveAt(rowindex);
                                        dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                                    }
                                    //}
                                    // dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex];
                                    // dataGridView1.Rows[rowindex].Cells[columnindex].Value = null;
                                    // label3.Text = (dataGridView1.Rows.Count - 2).ToString();
                                }

                            }


                        }
                        else
                        {


                            if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[1] && dataGridView1.Rows[rowindex].Cells[1].Value != null)
                            {
                                // columnindex = columnindex + 1;
                                SqlDataAdapter da23;
                                string firstColumnValue3 = dataGridView1.Rows[rowindex].Cells[1].Value.ToString();
                                string baknam = "";
                                //   string nextColumnValue = dataGridView1.Rows[rowindex].Cells[columnindex+1].Value.ToString();
                                DataTable dtchkbar = daml.SELECT_QUIRY_only_retrn_dt("select count(*) from items_bc join items on items.item_no=items_bc.item_no where  items.inactive=0 and items_bc.item_no='" + firstColumnValue3 + "'");
                                if (Convert.ToInt32(dtchkbar.Rows[0][0]) > 1)
                                {
                                    Search_All ns = new Search_All("chkb", firstColumnValue3);
                                    ns.ShowDialog();
                                    firstColumnValue3 = ns.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                                    baknam = ns.dataGridView1.CurrentRow.Cells[1].Value.ToString().StartsWith("عرض") ? ns.dataGridView1.CurrentRow.Cells[1].Value.ToString() : "";
                                    da23 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_curcost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img,b.min_price mp from items i join items_bc b on b.item_no=i.item_no and b.barcode='" + firstColumnValue3 + "' join taxs t on i.item_tax=t.tax_id where i.inactive=0", con2);

                                }
                                else
                                {
                                    //  string value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
                                    //string currentcell = dataGridView1.CurrentCell.Value.ToString();
                                    // string firstColumnValue = dataGridView1.Rows[0].Cells[0].Value.ToString();
                                    //string firstColumnValue =Convert.ToString(dataGridView1.CurrentCell

                                    //SqlDataAdapter da2 = new SqlDataAdapter("select * from items where item_no='" + firstColumnValue + "' or item_no='" + nextColumnValue + "'", con2);
                                    //  SqlDataAdapter da2 = new SqlDataAdapter("select * from DB.dbo.items where item_no='" + firstColumnValue + "'", con2);



                                    // SqlDataAdapter da23 = new SqlDataAdapter("select i.*,t.tax_percent from items i, taxs t where i.item_tax=t.tax_id and i.item_no='" + firstColumnValue3 + "'", con2);
                                    da23 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_curcost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img,b.min_price mp from items i join items_bc b on b.item_no=i.item_no and i.item_no='" + firstColumnValue3 + "' join taxs t on i.item_tax=t.tax_id where i.inactive=0", con2);
                                }
                                DataSet ds23 = new DataSet();
                                da23.Fill(ds23, "0");

                                if (ds23.Tables["0"].Rows.Count == 1)
                                {
                                    if (Convert.ToDouble(ds23.Tables["0"].Rows[0][3]) <= 0)
                                    {
                                        player.Play();
                                        MessageBox.Show("الصنف ليس لديه سعر", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                                        dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                                        return;
                                    }

                                    dataGridView1.Rows[rowindex].Cells[0].Value = ds23.Tables["0"].Rows[0]["item_barcode"];
                                    dataGridView1.Rows[rowindex].Cells[2].Value = Convert.ToDouble(ds23.Tables["0"].Rows[0]["pkqty"]) > 1 ? ds23.Tables["0"].Rows[0][1].ToString().Trim() + " * " + Math.Round(Convert.ToDecimal(ds23.Tables["0"].Rows[0]["pkqty"]), 0).ToString().Trim() + " " + baknam : ds23.Tables["0"].Rows[0][1].ToString().Trim();// ds23.Tables["0"].Rows[0][1];
                                    dataGridView1.Rows[rowindex].Cells[3].Value = ds23.Tables["0"].Rows[0][5];

                                    if (dataGridView1.Rows[rowindex].Cells[4].Value == null)
                                    {
                                        dataGridView1.Rows[rowindex].Cells[4].Value = Convert.ToDouble("1");
                                    }
                                    if (dataGridView1.Rows[rowindex].Cells[5].Value == null)
                                    {
                                        dataGridView1.Rows[rowindex].Cells[5].Value = ds23.Tables["0"].Rows[0][3];
                                    }
                                    if (dataGridView1.Rows[rowindex].Cells[6].Value == null)
                                    {
                                        dataGridView1.Rows[rowindex].Cells[6].Value = 0;
                                    }
                                    //   dataGridView1.Rows[rowindex].Cells[5].Value = ds23.Tables["0"].Rows[0][3];
                                    dataGridView1.Rows[rowindex].Cells[7].Value = ds23.Tables["0"].Rows[0][2];
                                    dataGridView1.Rows[rowindex].Cells[9].Value = ds23.Tables["0"].Rows[0][11];
                                    dataGridView1.Rows[rowindex].Cells[10].Value = ds23.Tables["0"].Rows[0]["pkqty"];
                                    dataGridView1.Rows[rowindex].Cells[11].Value = ds23.Tables["0"].Rows[0]["i_tax"];
                                    dataGridView1.Rows[rowindex].Cells[13].Value = ds23.Tables["0"].Rows[0]["img"];
                                    dataGridView1.Rows[rowindex].Cells[14].Value = ds23.Tables["0"].Rows[0]["mp"];
                                    dataGridView1.Rows[rowindex].Cells[8].Value = Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value);


                                    if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + ds23.Tables[0].Rows[0][9]))
                                    {
                                        pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + ds23.Tables[0].Rows[0][9]);

                                    }
                                    else
                                    {
                                        pictureBox1.Image = Properties.Resources.background_button;

                                    }
                                    //   DataRow[] dtrvat = dttax.Select("tax_id =" + dataGridView1.CurrentRow.Cells[11].Value + "");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";

                                    dataGridView1.Rows[rowindex].Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                                    chk_qty_offer(dataGridView1.Rows[rowindex]);
                                    // label1.Text = rowindex.ToString();
                                    // passes the value through the constructor to the 
                                    //   second form.
                                    //   MySecondForm f2 = new MySecondForm(firstColumnValue);
                                    //  f2.Show();
                                    //if (dataGridView1.Rows[rowindex].Cells[columnindex + 3].Value != null)
                                    //{
                                    //label3.Text = (dataGridView1.Rows.Count - 1).ToString();

                                    ////double sum1 = 0;
                                    ////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                                    ////{
                                    ////    sum1 += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                    ////}
                                    ////label3.Text = sum1.ToString();
                                    //////}
                                    //////else
                                    //////{

                                    //////}

                                    //////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                                    //////{

                                    ////double sum = 0;
                                    ////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                                    ////{
                                    ////    sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                    ////}
                                    ////label5.Text = sum.ToString();


                                    ////double sumcost = 0;
                                    ////for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                                    ////{
                                    ////    sumcost += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                                    ////}
                                    ////textBox2.Text = sumcost.ToString();



                                    total();
                                    if (BL.CLS_Session.use_cd)
                                        disply(Convert.ToDecimal(dataGridView1.Rows[rowindex].Cells[5].Value), total1);

                                    cssc.txt_iname.Text = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
                                    cssc.txt_iprice.Text = dataGridView1.Rows[rowindex].Cells[5].Value.ToString();
                                    cssc.txt_total.Text = total1.ToString();

                                    // sum += Convert.ToDouble(dataGridView1.Rows[rowindex].Cells[columnindex + 3].Value);
                                    // sum += Convert.ToDouble(ds2.Tables["0"].Rows[0][2]);
                                    // }
                                    // label5.Text = sum.ToString();
                                    //  rowindex = rowindex + 1;


                                    // dataGridView1.CurrentCell = dataGridView1.Rows[rowindex + 1].Cells[0];



                                    // if ((string)dataGridView1.CurrentRow.Cells[0].Value=="")
                                    // if (dataGridView1.SelectedCells==null)


                                    //  }
                                    this.dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.NewRowIndex];
                                }



                                else
                                {

                                    player.Play();
                                    MessageBox.Show("الصنف غير موجود", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    // dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[1];
                                    // dataGridView1.Rows[rowindex].Cells[1].Value = null;
                                    dataGridView1.Rows.RemoveAt(rowindex);
                                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                                    // label3.Text = (dataGridView1.Rows.Count - 2).ToString();

                                }


                            }

                            //else
                            //{
                            //    MessageBox.Show("الصنف غير موجود");
                            //    // dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[1];
                            //    // dataGridView1.Rows[rowindex].Cells[1].Value = null;
                            //    dataGridView1.Rows.RemoveAt(rowindex);

                            //}




                        }


                        //else
                        //{

                        //    //MessageBox.Show("الرجاء ادخال رقم صنف");
                        //    //dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex];
                        //    //dataGridView1.Rows[rowindex].Cells[columnindex].Value = null;
                        //    //label3.Text = (dataGridView1.Rows.Count - 2).ToString();

                        //    textBox1.Focus();
                        //    textBox1.BackColor = Color.Green;
                        //    //dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex];
                        //    //dataGridView1.Rows[rowindex].Cells[columnindex].Value = null;
                        //    //label3.Text = (dataGridView1.Rows.Count - 2).ToString();

                        //    // else { }
                        //}

                        //if (textBox1.Text == "")
                        //{
                        //    //int pay = 0;
                        //    int remain = 0;
                        //    label10.Text = remain.ToString();
                        //}
                        //else
                        //{
                        //    int pay = Convert.ToInt32(textBox1.Text);
                        //    int remain = Convert.ToInt32(sum) - pay;
                        //    label10.Text = remain.ToString();
                        //}



                    }

                }






            }

            catch (System.Exception ex)
            {
                //  MessageBox.Show(ex.ToString());
            }

            //}
            //else
            //{
            //   // MessageBox.Show("لا يوجد اصناف");

            //    textBox1.Focus();
            //}

          //  double oval = Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value);

            if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[6])
            {
                // string per = (100 * (Convert.ToDouble(textBox4.Text)) / Convert.ToDouble(textBox5.Text)).ToString();

                //  if ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) > Convert.ToDouble(BL.CLS_Session.item_dsc)))
                if ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) > Convert.ToDouble(maxitmdsc)))
                {
                    //MessageBox.Show("تجاوزت الخصم المسموح لك");
                    MessageBox.Show("تجاوزت الخصم المسموح لك", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView1.CurrentRow.Cells[6].Value = 0;
                    // txt_desper.Text = "0";
                }

                if (Convert.ToDouble(dataGridView1.CurrentRow.Cells[14].Value) !=0 && Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) < Convert.ToDouble(dataGridView1.CurrentRow.Cells[14].Value))
                {
                   
                    MessageBox.Show("تجاوزت السعر الادنى للصنف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView1.CurrentRow.Cells[5].Value = oval;
                    // txt_desper.Text = "0";
                }

                dataGridView1.CurrentRow.Cells[8].Value = (Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) / 100);// + Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value);
                // dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[0];
                dataGridView1.CurrentRow.Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.CurrentRow.Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.CurrentRow.Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();



                total();
                if (BL.CLS_Session.use_cd)
                    disply(Convert.ToDecimal(dataGridView1.CurrentRow.Cells[5].Value), total1);

                cssc.txt_iname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                cssc.txt_iprice.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                cssc.txt_total.Text = total1.ToString();

            }

            //  SendKeys.Send("{Left 5}");

            total();
        }

        private void total()
        {
            try
            {

               // txt_dscper_Leave(sender, e);

                string per = (100 * (Convert.ToDouble(txt_desc.Text)) / Convert.ToDouble(txt_total.Text)).ToString();

                //  if ((Convert.ToDouble(per) > Convert.ToDouble(BL.CLS_Session.inv_dsc)))
                if ((Convert.ToDouble(per) > Convert.ToDouble(slpmaxdisc)))
                {
                    // MessageBox.Show("تجاوزت الخصم المسموح لك");
                    MessageBox.Show("تجاوزت الخصم المسموح لك", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_desc.Text = "0";
                    txt_dscper.Text = "0";
                    txt_desc.SelectAll();
                    return;
                    // txt_desper.Text = "0";
                }

                if (txt_recv.Text == "")
                {
                    txt_recv.Text = "0";
                }
                if (txt_desc.Text == "")
                {
                    txt_desc.Text = "0";
                }

                double sum1 = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value != null && dataGridView1.Rows[i].Cells[1].Value != null && !dataGridView1.Rows[i].Cells[0].Value.ToString().Trim().Equals("") && !dataGridView1.Rows[i].Cells[1].Value.ToString().Trim().Equals(""))
                    {
                        sum1 += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                        if (dataGridView1.Rows[i].Cells[15].Value != null)
                        {
                            dataGridView1.Rows[i].Cells[8].Value = (Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value)) - Convert.ToDouble(dataGridView1.Rows[i].Cells[15].Value);
                            dataGridView1.Rows[i].Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value)) - ((Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value)) * Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value)) - ((Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value)) * Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                        }
                    }
                }
                label3.Text = sum1.ToString();


                //}
                //else
                //{

                //}

                //for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                //{

                double sum = 0, sumv = 0, sumfv = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value != null && dataGridView1.Rows[i].Cells[1].Value != null && !dataGridView1.Rows[i].Cells[0].Value.ToString().Trim().Equals("") && !dataGridView1.Rows[i].Cells[1].Value.ToString().Trim().Equals(""))
                    {
                        // sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                        sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value);
                        sumfv += (Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value) == 0 ? Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value) : 0);
                        sumv += BL.CLS_Session.isshamltax.Equals("2") ? (Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value)) / (100 + Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value)) : (Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value)) / 100;
                    }
                }
                txt_total.Text = sum.ToString();


                //if (BL.CLS_Session.use_cd)
                //    disply(0, total1);


                double sumcost = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value != null && dataGridView1.Rows[i].Cells[1].Value != null && !dataGridView1.Rows[i].Cells[0].Value.ToString().Trim().Equals("") && !dataGridView1.Rows[i].Cells[1].Value.ToString().Trim().Equals(""))
                    {
                        sumcost += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value);
                    }
                }
                textBox2.Text = sumcost.ToString();

                lbl_mustpay.Text = BL.CLS_Session.isshamltax.Equals("1") ? ((Convert.ToDouble(txt_total.Text) + Convert.ToDouble(txt_tax.Text)) - Convert.ToDouble(txt_desc.Text)).ToString() : (Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_desc.Text)).ToString();
                total1 = Convert.ToDecimal(lbl_mustpay.Text);

                double sum11 = 0,sum12=0;
                // for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                // {//Column2
                //sum11 += (Convert.ToDouble(dataGridView1.Rows[i].Cells["tax_percent"].Value)/100) * Convert.ToDouble(dataGridView1.Rows[i].Cells["Column2"].Value); ;
                // 
                sum11 = BL.CLS_Session.isshamltax.Equals("1") ? Math.Round((Convert.ToDouble(lbl_mustpay.Text) - Convert.ToDouble(txt_tax.Text)) / ((100) / BL.CLS_Session.tax_per), 2) : Math.Round(Convert.ToDouble(lbl_mustpay.Text) / ((100 + BL.CLS_Session.tax_per) / BL.CLS_Session.tax_per), 2);
                // }
                sum12=((Convert.ToDouble(sumv) - (Convert.ToDouble(sumv) * (Convert.ToDouble(txt_dscper.Text) / 100))));
               // txt_tax.Text = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? "0" : (Math.Round(sum11, 2)).ToString();
                txt_tax.Text = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? "0" : (Math.Round(sum12, 4)).ToString();
                txt_taxfree.Text = BL.CLS_Session.isshamltax.Equals("1") ? Math.Round(((Convert.ToDouble(sumfv) - (Convert.ToDouble(sumfv) * (Convert.ToDouble(txt_dscper.Text) / 100)))), 2).ToString() : Math.Round(((Convert.ToDouble(sumfv) - (Convert.ToDouble(sumfv) * (Convert.ToDouble(txt_dscper.Text) / 100)))), 2).ToString();

                //double rem = Convert.ToDouble(lbl_mustpay.Text) * -1;
                //if (rem > 0)
                //{
                if (((Convert.ToDouble(txt_cardpay.Text) + Convert.ToDouble(txt_recv.Text)) - Convert.ToDouble(lbl_mustpay.Text)) >= 0)
                {
                    txt_remain.Text = (((Convert.ToDouble(txt_cardpay.Text) + Convert.ToDouble(txt_recv.Text)) - Convert.ToDouble(lbl_mustpay.Text))).ToString();
                    txt_remain.BackColor = this.BackColor;// Color.LightGreen;
                    label12.Text = BL.CLS_Session.lang.Equals("E") ? "Remain For Customer" : "المتبقي للزبون";
                }
                else
                {
                    if (((Convert.ToDouble(txt_cardpay.Text) + Convert.ToDouble(txt_recv.Text)) - Convert.ToDouble(lbl_mustpay.Text)) < 0)
                    {
                        txt_remain.Text = (((Convert.ToDouble(txt_cardpay.Text) + Convert.ToDouble(txt_recv.Text)) - Convert.ToDouble(lbl_mustpay.Text))*-1).ToString();
                        txt_remain.BackColor = Color.LightCoral;
                        label12.Text = BL.CLS_Session.lang.Equals("E") ? "Remain On Customer" : "المتبقي على الزبون";
                    }
                    else
                    {
                        txt_remain.Text = (((Convert.ToDouble(txt_cardpay.Text) + Convert.ToDouble(txt_recv.Text)) - Convert.ToDouble(lbl_mustpay.Text))).ToString();
                        txt_remain.BackColor = Color.Gainsboro;
                    }
                }

                txt_recv.Text = lbl_mustpay.Text;
                //txt_remain.Text = (((Convert.ToDouble(txt_cardpay.Text) + Convert.ToDouble(txt_recv.Text)) - Convert.ToDouble(lbl_mustpay.Text))).ToString();
                // txt_remain.Text = ((Convert.ToDouble(txt_cardpay.Text) + Convert.ToDouble(txt_recv.Text)) - Convert.ToDouble(lbl_mustpay.Text)) > 0 ? (((Convert.ToDouble(txt_cardpay.Text) + Convert.ToDouble(txt_recv.Text)) - Convert.ToDouble(lbl_mustpay.Text))).ToString() : "0";
                // txt_remain.Text = ((Convert.ToDouble(txt_cardpay.Text) + Convert.ToDouble(txt_recv.Text)) - Convert.ToDouble(lbl_mustpay.Text)) < 0 ? (((Convert.ToDouble(txt_cardpay.Text) + Convert.ToDouble(txt_recv.Text)) - Convert.ToDouble(lbl_mustpay.Text))).ToString() : "0";

                //  txt_remain.BackColor = ((Convert.ToDouble(txt_cardpay.Text) + Convert.ToDouble(txt_recv.Text)) - Convert.ToDouble(lbl_mustpay.Text)) > 0 ? Color.LightGreen : Color.Gainsboro;
                //  txt_remain.BackColor = ((Convert.ToDouble(txt_cardpay.Text) + Convert.ToDouble(txt_recv.Text)) - Convert.ToDouble(lbl_mustpay.Text)) < 0 ? Color.LightCoral : Color.Gainsboro;

                //}
                txt_remain.Text = String.Format("{0:0.00}", Convert.ToDouble(txt_remain.Text));
                lbl_mustpay.Text = String.Format("{0:0.00}", Convert.ToDouble(lbl_mustpay.Text));
                //txt_total.Text = String.Format("{0:0.00}", Convert.ToDouble(txt_total.Text));
                //txt_tax.Text = String.Format("{0:0.00}", Convert.ToDouble(txt_tax.Text));
            }
            catch { }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_desc.Text == "")
                {
                    txt_desc.Text = "0";
                    // int chk2 = dataGridView1.RowCount - 1;
                    txt_recv.Focus();
                    // dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex]
                    // dataGridView1.Rows[chk2].Cells[0].Selected = true;
                }
                else
                {

                    txt_recv.Text = "0";
                    txt_recv.Focus();
                    total();
                }
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (textBox1.Text == "")
            //{
            //    textBox1.Text = "0";
            //}
            //try
            //{
            //    double remain = Convert.ToDouble(textBox5.Text) - Convert.ToDouble(textBox4.Text) - Convert.ToDouble(textBox1.Text);
            //    label10.Text = remain.ToString();
            //}
            // catch
            //{ }
        }

        private void print_ref()
        {
            if (string.IsNullOrEmpty(txt_ref.Text))
            {
                MessageBox.Show("لا يوجد فاتورة للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty] from pos_dtl where ref=(select max(ref) from pos_dtl)", con2);

            // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from pos_hdr where ref=(select max(ref) from pos_hdr)", con2);//" + lblref.Text + ""
            SqlDataAdapter dacr1 = new SqlDataAdapter("select a.[brno],a.[slno],a.[ref],a.[contr],a.[type],a.[date],a.[total],a.[count],a.[payed],a.[total_cost],b.sl_name [saleman],a.[req_no],a.[cust_no],a.[discount],a.[net_total],a.[sysdate],a.[gen_ref],a.[tax_amt],a.[card_amt],a.[note],a.[mobile] from pos_hdr a join pos_salmen b on a.saleman=b.sl_id where a.ref=" + txt_ref.Text + " and a.[contr]=" + BL.CLS_Session.ctrno + " and  a.[brno]= '" + BL.CLS_Session.brno + "' ", con2);
            SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty],[offr_amt] from pos_dtl where ref=" + txt_ref.Text + " and [contr]=" + BL.CLS_Session.ctrno + " and  [brno]= '" + BL.CLS_Session.brno + "' ", con2);

            DataSet1 ds = new DataSet1();

            dacr1.Fill(ds, "hdr");
            dacr.Fill(ds, "dtl");

            if (ds.Tables["hdr"].Rows.Count == 0)
            {
                MessageBox.Show("لا يوجد فاتورة للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CrystalDecisions.CrystalReports.Engine.ReportDocument report1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            if (File.Exists(Directory.GetCurrentDirectory() + (string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? @"\reports\SalesReport1nt.rpt" : BL.CLS_Session.isshamltax.Equals("1") ? @"\reports\SalesReport112.rpt" : @"\reports\SalesReport111.rpt")))
            {
                string filePath = Directory.GetCurrentDirectory() + (string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? @"\reports\SalesReport1nt.rpt" : BL.CLS_Session.isshamltax.Equals("1") ? @"\reports\SalesReport112.rpt" : @"\reports\SalesReport111.rpt");
                report1.Load(filePath);

                // rpt.SalesReport111 report = new rpt.SalesReport111();

                //  CrystalReport1 report = new CrystalReport1();
                report1.SetDataSource(ds);

                //    crystalReportViewer1.ReportSource = report;

                //  crystalReportViewer1.Refresh();
                report1.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                report1.SetParameterValue("br_name", BL.CLS_Session.brname);
                report1.SetParameterValue("inv_bar", "*" + ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString() + "*");
                report1.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                report1.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
                report1.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
                // report.SetParameterValue("inv_bar", );
                //  report.SetParameterValue("picpath", Directory.GetCurrentDirectory() + "\\" + BL.CLS_Session.comp_logo);


                foreach (string line in lines_prt)
                {
                    //report1.PrintOptions.PrinterName = line;

                    //report1.PrintToPrinter(1, false, 0, 0);
                    // report.PrintToPrinter(0,true, 1, 1);
                    report1.PrintOptions.PrinterName = printer_nam;// "pos";

                    report1.PrintToPrinter(1, false, 0, 0);

                }


                //report1.PrintOptions.PrinterName = printer_nam;// "pos";

                //report1.PrintToPrinter(1, false, 0, 0);
                report1.Close();
            }
            else
            {
                rpt.SalesReport111 report = new rpt.SalesReport111();

                //  CrystalReport1 report = new CrystalReport1();
                report.SetDataSource(ds);

                //    crystalReportViewer1.ReportSource = report;

                //  crystalReportViewer1.Refresh();
                report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                report.SetParameterValue("br_name", BL.CLS_Session.brname);
                report.SetParameterValue("inv_bar", "*" + ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString() + "*");
                report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
                report.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
                // report.SetParameterValue("inv_bar", );
                //  report.SetParameterValue("picpath", Directory.GetCurrentDirectory() + "\\" + BL.CLS_Session.comp_logo);

                foreach (string line in lines_prt)
                {
                    //report1.PrintOptions.PrinterName = line;

                    //report1.PrintToPrinter(1, false, 0, 0);
                    // report.PrintToPrinter(0,true, 1, 1);
                    report.PrintOptions.PrinterName = printer_nam;// "pos";

                    report.PrintToPrinter(1, false, 0, 0);

                }

                report.PrintOptions.PrinterName = printer_nam;// "pos";

                report.PrintToPrinter(1, false, 0, 0);
                report.Close();
            }

        }

        private void print_ref_fr()
        {
            if (string.IsNullOrEmpty(txt_ref.Text))
            {
                MessageBox.Show("لا يوجد فاتورة للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty] from pos_dtl where ref=(select max(ref) from pos_dtl)", con2);

            // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from pos_hdr where ref=(select max(ref) from pos_hdr)", con2);//" + lblref.Text + ""
            SqlDataAdapter dacr1 = new SqlDataAdapter("select a.[brno],a.[slno],a.[ref],a.[contr],a.[type],FORMAT (a.[date], 'dd-MM-yyyy hh:mm:ss tt', 'en-us') date,a.[total],a.[count],a.[payed],a.[total_cost],b.sl_name [saleman],a.[req_no],a.[cust_no],a.[discount],a.[net_total],a.[sysdate],a.[gen_ref],a.[tax_amt],a.[card_amt],a.[note],a.[mobile] from pos_hdr a join pos_salmen b on a.saleman=b.sl_id where a.ref=" + txt_ref.Text + " and a.[contr]=" + BL.CLS_Session.ctrno + " and  a.[brno]= '" + BL.CLS_Session.brno + "' ", con2);
            SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty],[offr_amt] from pos_dtl where ref=" + txt_ref.Text + " and [contr]=" + BL.CLS_Session.ctrno + " and  [brno]= '" + BL.CLS_Session.brno + "' order by srno", con2);

            DataSet1 ds = new DataSet1();

            dacr1.Fill(ds, "hdr");
            dacr.Fill(ds, "dtl");

            if (ds.Tables["hdr"].Rows.Count == 0)
            {
                MessageBox.Show("لا يوجد فاتورة للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FastReport.Report rpt = new FastReport.Report();

            rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_RSale_rpt.frx");
            //  rpt.SetParameterValue("br_name", BL.CLS_Session.brname);
            rpt.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
            rpt.SetParameterValue("Br_Name", BL.CLS_Session.brname);
            rpt.SetParameterValue("inv_bar",  ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString() );
            rpt.SetParameterValue("Msg1", BL.CLS_Session.msg1.ToString());
            rpt.SetParameterValue("Msg2", BL.CLS_Session.msg2.ToString());
            rpt.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
            rpt.SetParameterValue("tax_type", BL.CLS_Session.autotax ? "3" : BL.CLS_Session.isshamltax.Equals("2") ? "2" : "0");
            rpt.SetParameterValue("tax_id", BL.CLS_Session.tax_no);
            rpt.SetParameterValue("type", ds.Tables["hdr"].Rows[0][4].ToString());

            string dtt = Convert.ToDateTime(ds.Tables["hdr"].Rows[0]["date"].ToString()).ToString("yyyy-MM-ddTHH:mm:ssZ", new CultureInfo("en-US", false));
            var tlvs = new GenerateQrCode.GenerateQrCodeTLV(
                          BL.CLS_Session.cmp_ename,
                           BL.CLS_Session.tax_no,
                           dtt,
                           ds.Tables["hdr"].Rows[0]["net_total"].ToString(),
                          Math.Round(Convert.ToDouble(ds.Tables["hdr"].Rows[0]["tax_amt"].ToString()), 2).ToString());

            //rpt.SetParameterValue("qr", Convert.ToBase64String(tlvs.GetQRCode()));
            if (BL.CLS_Session.is_einv_p2)
            {
                DataTable dtqrc = daml.SELECT_QUIRY_only_retrn_dt("select qr_code from pos_esend where ref=" + txt_ref.Text + "");
                rpt.SetParameterValue("qr", dtqrc.Rows[0][0].ToString());
            }
            else
                rpt.SetParameterValue("qr", Convert.ToBase64String(tlvs.GetQRCode()));

            rpt.RegisterData(ds.Tables["hdr"], "pos_hdr");
            rpt.RegisterData(ds.Tables["dtl"], "pos_dtl");
            // rpt.PrintSettings.ShowDialog = false;
            // rpt.PrintSettings.Copies =Convert.ToInt32(txt_count.Text);

            // rpt.Print();


            foreach (string line in lines_prt)
            {

                rpt.PrintSettings.Printer = printer_nam;// "pos";
                rpt.PrintSettings.ShowDialog = false;
               // FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                // rpt.PrintSettings.Copies =Convert.ToInt32(txt_count.Text);
                rpt.Print();
            }

            

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (BL.CLS_Session.prnt_type.Equals("0"))
                print_ref();
            else
                print_ref_fr();
            dataGridView1.Focus();
            //DataTable dthdr = daml.SELECT_QUIRY_only_retrn_dt("select a.[brno],a.[slno],a.[ref],a.[contr],a.[type],a.[date],a.[total],a.[count],a.[payed],a.[total_cost],b.sl_name [saleman],a.[req_no],a.[cust_no],a.[discount],a.[net_total],a.[sysdate],a.[gen_ref],a.[tax_amt],a.[card_amt] from pos_hdr a join pos_salmen b on a.saleman=b.sl_id where a.ref=" + txt_ref.Text + " and a.[contr]=" + BL.CLS_Session.ctrno + " and  a.[brno]= '" + BL.CLS_Session.brno + "' ");

            //// SqlDataAdapter dacr1 = new SqlDataAdapter("select * from pos_hdr where ref=(select max(ref) from pos_hdr)", con2);//" + lblref.Text + ""
            //DataTable dtdtl = daml.SELECT_QUIRY_only_retrn_dt("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty] from pos_dtl where ref=" + txt_ref.Text + " and [contr]=" + BL.CLS_Session.ctrno + " and  [brno]= '" + BL.CLS_Session.brno + "' ");

            ////////SqlDataAdapter dthdr = new SqlDataAdapter("select a.[brno],a.[slno],a.[ref],a.[contr],a.[type],a.[date],a.[total],a.[count],a.[payed],a.[total_cost],b.sl_name [saleman],a.[req_no],a.[cust_no],a.[discount],a.[net_total],a.[sysdate],a.[gen_ref],a.[tax_amt],a.[card_amt] from pos_hdr a join pos_salmen b on a.saleman=b.sl_id where a.ref=" + txt_ref.Text + " and a.[contr]=" + BL.CLS_Session.ctrno + " and  a.[brno]= '" + BL.CLS_Session.brno + "' ",con2);

            ////////// SqlDataAdapter dacr1 = new SqlDataAdapter("select * from pos_hdr where ref=(select max(ref) from pos_hdr)", con2);//" + lblref.Text + ""
            ////////SqlDataAdapter dtdtl = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty] from pos_dtl where ref=" + txt_ref.Text + " and [contr]=" + BL.CLS_Session.ctrno + " and  [brno]= '" + BL.CLS_Session.brno + "' ",con2);

            ////////DataSet1 ds = new DataSet1();

            ////////dthdr.Fill(ds, "dtl");
            ////////dtdtl.Fill(ds, "hdr");


            //LocalReport report = new LocalReport();
            /////////////////////////////////////////////////   report.ReportEmbeddedResource = "POS.Sal.rpt.Sal_Tran_Tax_rpt.rdlc";
            //if (File.Exists(Directory.GetCurrentDirectory() + @"\reports\Pos_Sal_rpt.rdlc"))
            //    report.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Pos_Sal_rpt.rdlc";
            //else
            //    report.ReportEmbeddedResource = "POS.Pos.rpt.Pos_Sal_rpt.rdlc";

            ////////report.DataSources.Add(new ReportDataSource("hdr", dthdr));
            //// report.DataSources.Add(new ReportDataSource("DataSet1", getYourDatasource()));
            //report.DataSources.Add(new ReportDataSource("hdr", dthdr));
            //report.DataSources.Add(new ReportDataSource("dtl", dtdtl));
            //// rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("sdt", dt));
            ///*
            //ReportParameter[] parameters = new ReportParameter[16];
            //parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
            ////  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
            ////  parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());
            //parameters[1] = new ReportParameter("br_name", BL.CLS_Session.brname);
            //parameters[2] = new ReportParameter("type", cmb_type.Text);
            //parameters[3] = new ReportParameter("ref", txt_ref.Text);
            //parameters[4] = new ReportParameter("date", txt_mdate.Text);
            //parameters[5] = new ReportParameter("cust", txt_custnam.Text + "  " + txt_custno.Text);

            //parameters[6] = new ReportParameter("desc", txt_desc.Text);
            //parameters[7] = new ReportParameter("tax_id", BL.CLS_Session.tax_no);
            //parameters[8] = new ReportParameter("total", txt_total.Text);
            //parameters[9] = new ReportParameter("descount", txt_des.Text);
            //parameters[10] = new ReportParameter("tax", txt_tax.Text);
            //parameters[11] = new ReportParameter("nettotal", txt_net.Text);


            //BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(txt_net.Text), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);

            ////  MessageBox.Show(toWord.ConvertToArabic());
            //parameters[12] = new ReportParameter("a_toword", toWord.ConvertToArabic());
            //parameters[13] = new ReportParameter("custno", txt_custno.Text);
            //parameters[14] = new ReportParameter("note", txt_remark.Text);
            //parameters[15] = new ReportParameter("clnt_taxid", txt_taxid.Text);

            //report.SetParameters(parameters);
            //*/

            ////var pageSettings = new PageSettings();
            ////pageSettings.PaperSize = report.GetDefaultPageSettings().PaperSize;
            ////pageSettings.Landscape = report.GetDefaultPageSettings().IsLandscape;
            ////pageSettings.Margins = report.GetDefaultPageSettings().Margins;
            //foreach (string line in lines_prt)
            //{
            //    BL.Print_Rdlc_Direct.Print(report, 1, printer_nam);

            //}
            //  BL.Print_Rdlc_Direct.Print(report, 1, "");

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            datval.ValidateText_numric(txt_desc);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // LoadSalesData();   

            /*
              Run();
            */

            SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty] from pos_dtl where ref=" + txt_ref.Text + "", con2);

            // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from pos_hdr where ref=(select max(ref) from pos_hdr)", con2);//" + lblref.Text + ""
            SqlDataAdapter dacr1 = new SqlDataAdapter("select a.[brno],a.[slno],a.[ref],a.[contr],a.[type],a.[date],a.[total],a.[count],a.[payed],a.[total_cost],b.sl_name [saleman],a.[req_no],a.[cust_no],a.[discount],a.[net_total],a.[sysdate],a.[gen_ref],a.[tax_amt],a.[card_amt] from pos_hdr a join pos_salmen b on a.saleman=b.sl_id where ref=" + txt_ref.Text + "", con2);

            DataSet1 ds = new DataSet1();

            dacr.Fill(ds, "dtl");
            dacr1.Fill(ds, "hdr");

            CrystalDecisions.CrystalReports.Engine.ReportDocument report = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            string filePath = Directory.GetCurrentDirectory() + (string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? @"\reports\SalesReport1nt.rpt" : BL.CLS_Session.isshamltax.Equals("1") ? @"\reports\SalesReport112.rpt" : @"\reports\SalesReport111.rpt");
            report.Load(filePath);

            // rpt.SalesReport111 report = new rpt.SalesReport111();

            //  CrystalReport1 report = new CrystalReport1();
            report.SetDataSource(ds);

            //    crystalReportViewer1.ReportSource = report;

            //  crystalReportViewer1.Refresh();
            report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
            report.SetParameterValue("br_name", BL.CLS_Session.brname);
            report.SetParameterValue("inv_bar", "*" + ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString() + "*");
            report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
            report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
            // report.SetParameterValue("inv_bar", );
            //  report.SetParameterValue("picpath", Directory.GetCurrentDirectory() + "\\" + BL.CLS_Session.comp_logo);



            report.PrintOptions.PrinterName = printer_nam;// "pos";

            report.PrintToPrinter(1, false, 0, 0);
            report.Close();
        }
        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        private DataSet LoadSalesData()
        {
            /*
            // Create a new DataSet and read sales data file 
            //    data.xml into the first DataTable.
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(@"..\..\data.xml");
            return dataSet.Tables[0];
             */
            SqlDataAdapter dacr = new SqlDataAdapter("select * from pos_dtl where ref=(select max(ref) from pos_dtl)", con2);
            SqlDataAdapter dacr1 = new SqlDataAdapter("select * from pos_hdr where ref=(select max(ref) from pos_hdr)", con2);
            DataSet1 ds = new DataSet1();

            dacr.Fill(ds, "dtl");
            dacr1.Fill(ds, "hdr");

            return ds;

            //  rpt.SalesReport1 report = new rpt.SalesReport1();

            //  CrystalReport1 report = new CrystalReport1();
            //  report.SetDataSource(ds);

            //    crystalReportViewer1.ReportSource = report;

            //  crystalReportViewer1.Refresh();
            // report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
            // report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
            // report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());



            //  report.PrintOptions.PrinterName = printer_nam;// "pos";

            // report.PrintToPrinter(1, false, 0, 0);
            //  report.Close();
            /*
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
             */
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
                <PageWidth>8.5in</PageWidth>
                <PageHeight>11in</PageHeight>
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
            // report.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Sales_direct.rdlc";
            report.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Sal_Report_Pos.rdlc";
            report.DataSources.Add(
               new ReportDataSource("hdr", LoadSalesData().Tables["hdr"]));
            report.DataSources.Add(
               new ReportDataSource("dtl", LoadSalesData().Tables["dtl"]));
            Export(report);
            Print();
        }

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            if (sl_alowexit)
            {
                BL.CLS_Session.is_sal_login = false;
                //  BL.CLS_Session.dtsalman.Rows.Clear();
                // sp.Close();
                BL.CLS_Session.scount = BL.CLS_Session.scount - 1;
                this.Close();

            }
        }

        private void dataGridView1_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
        {
            // dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[0];
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txt_dscper_Leave(null, null);
                textBox4_Leave(null, null);
                total();

                // dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells[0];
                int rowNumber = 1;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    // dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                    if (r.IsNewRow) continue;
                    //r.HeaderCell.Value = (e.RowIndex + 1).ToString();
                    r.HeaderCell.Value = rowNumber.ToString();
                    rowNumber = rowNumber + 1;


                    //// dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                    //if (r.IsNewRow) continue;
                    ////r.HeaderCell.Value = (e.RowIndex + 1).ToString();
                    //r.HeaderCell.Value = rowNumber.ToString();
                    //rowNumber = rowNumber + 1;
                    /*
                    DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                    //  r.Cells[2] = dcombo;
                    DataView dv1 = dtunits.DefaultView;
                    //  dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                    dv1.RowFilter = "unit_id in('" + r.Cells[3].Value + "')";
                    DataTable dtNew = dv1.ToTable();
                    dcombo.DataSource = dtNew;
                    dcombo.DisplayMember = "unit_name";
                    dcombo.ValueMember = "unit_id";
                    r.Cells[3] = dcombo;
                    r.Cells[3].Value = dtNew.Rows[0][0];

                    //  dataGridView1[2, r.Index] = dcombo;
                    // dataGridView1[2, r.Index].Value = dtNew.Rows[0][1];
                    // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                    dcombo.FlatStyle = FlatStyle.Flat;
                    */

                    //try
                    //{

                    //    if (dataGridView1.RowCount > 1)
                    //    {
                    //        if (dataGridView1.Rows[e.RowIndex - 1].Cells[0].Value==dataGridView1.CurrentRow.Cells[0].Value)
                    //        {
                    //            dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value = Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value) + 1;
                    //            dataGridView1.Rows[e.RowIndex - 1].Cells[8].Value = (Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value) / 100);// + Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value);
                    //            dataGridView1.Rows[e.RowIndex - 1].Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value)) / 100) : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                    //            total();
                    //        }
                    //    }
                    //}
                    //catch { }
                }
            }
            catch { }
            
        }
        

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[6])
                {

                    dataGridView1.CurrentRow.Cells[8].Value = (Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) / 100);// + Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value);
                    dataGridView1.CurrentRow.Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.CurrentRow.Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)) / 100) : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.CurrentRow.Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.CurrentRow.Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                    if (Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value) > 1)
                        dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.PaleGreen;
                    else
                        dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.White;

                    chk_qty_offer(dataGridView1.CurrentRow);
                    total();
                }
            }
            catch { }

            // DataRow[] dtrvat = dttax.Select("tax_id =" + dataGridView1.CurrentRow.Cells[11].Value + "");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";

            //  dataGridView1.CurrentRow.Cells[12].Value = (Convert.ToDouble(dataGridView1.CurrentRow.Cells["tax_percent"].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.CurrentRow.Cells["tax_percent"].Value)) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
            //  dataGridView1.CurrentRow.Cells[12].Value = (Convert.ToDouble(dataGridView1.CurrentRow.Cells["tax_percent"].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.CurrentRow.Cells["tax_percent"].Value)) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
            if (!dataGridView1.CurrentRow.IsNewRow && (dataGridView1.CurrentRow.Cells[0].ReadOnly == false))
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US", false));
                DataTable dtchkoffer = daml.SELECT_QUIRY_only_retrn_dt("select * from items_offer where InActive=0 and br_no='" + BL.CLS_Session.brno + "' and sl_no in ('','" + BL.CLS_Session.slno + "') and barcode='" + dataGridView1.CurrentRow.Cells[0].Value + "' and '" + date + "' between StartDate and EndDate");
                if (dtchkoffer.Rows.Count > 0)
                {
                    if (dtchkoffer.Rows[0]["disctype"].ToString().Equals("0"))
                    {
                        dataGridView1.CurrentRow.Cells[5].Value = dtchkoffer.Rows[0]["offer_price"];
                        dataGridView1.CurrentRow.Cells[8].Value = Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value);
                        dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.Yellow;
                        total();
                    }
                    //if (dtchkoffer.Rows[0]["disctype"].ToString().Equals("1"))
                    //{


                    //    //if (dataGridView1.CurrentRow.Cells[0].Value == dtchkoffer.Rows[0]["barcode"])
                    //    //    {
                    //    //        double sum = Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value);
                    //    //        dataGridView1.CurrentRow.Cells[4].Value = Convert.ToDouble(sum) + 1;

                    //    //    }
                    //    double count = 0, sumc = 0;
                    //    foreach (DataGridViewRow dtgvr in dataGridView1.Rows)
                    //    {

                    //        if (!dtgvr.IsNewRow && dtgvr.Cells[0].Value.ToString() == dtchkoffer.Rows[0]["barcode"].ToString())
                    //        {
                    //            //  MessageBox.Show(dtgvr.Cells[0].Value.ToString());
                    //            sumc = sumc + Convert.ToDouble(dtgvr.Cells[4].Value);
                    //            // double sum= Convert.ToDouble(dtgvr.Cells[4].Value);
                    //            // dtgvr.Cells[4].Value = sum + 1;
                    //            // dataGridView1.Rows.Remove(dtgvr);
                    //            count++;
                    //        }

                    //        //count++;
                    //    }
                    //    // MessageBox.Show(sumc.ToString());
                    //    if (sumc > 0 && sumc % Convert.ToDouble(dtchkoffer.Rows[0]["minqty"]) == 0 && (Convert.ToDouble(dtchkoffer.Rows[0]["maxqty"]) > 0 ? sumc <= Convert.ToDouble(dtchkoffer.Rows[0]["maxqty"]) : sumc > 0))
                    //    {
                    //        dataGridView1.CurrentRow.Cells[5].Value = Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) - Convert.ToDouble(dtchkoffer.Rows[0]["DiscountP"]);
                    //        dataGridView1.CurrentRow.Cells[8].Value = Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value);
                    //        dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.Gold;
                    //    }
                    //    /*
                    //    dataGridView1.CurrentRow.Cells[5].Value = dtchkoffer.Rows[0]["offer_price"];
                    //    dataGridView1.CurrentRow.Cells[8].Value = Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value);
                    //    dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen;
                    //     * */
                    //    total();
                    //}

                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dataGridView1.CurrentRow.IsNewRow)
                {
                    //  dataGridView1.CurrentRow.Cells[0].ReadOnly = false;
                    dataGridView1.CurrentRow.Cells[0].ReadOnly = false;
                    dataGridView1.CurrentRow.Cells[1].ReadOnly = false;
                    //  dataGridView1.CurrentRow.Cells[4].ReadOnly = false;
                }
                else
                {
                    dataGridView1.CurrentRow.Cells[0].ReadOnly = true;
                    dataGridView1.CurrentRow.Cells[1].ReadOnly = true;
                }


                if (!dataGridView1.CurrentRow.IsNewRow)
                {
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + dataGridView1.CurrentRow.Cells[13].Value.ToString()))
                    {
                        pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + dataGridView1.CurrentRow.Cells[13].Value.ToString());

                    }
                    else
                    {
                        //   pictureBox1.Image = Properties.Resources.background_button;

                    }
                }
            }
            catch { }

        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
           
            if (string.IsNullOrEmpty(txt_desc.Text))
                txt_desc.Text = "0";

            double td =Math.Round(Convert.ToDouble(txt_desc.Text),2);
            txt_desc.Text = td.ToString();

            string per = (100 * (Convert.ToDouble(txt_desc.Text)) / Convert.ToDouble(txt_total.Text)).ToString();

            //  if ((Convert.ToDouble(per) > Convert.ToDouble(BL.CLS_Session.inv_dsc)))
            if ((Convert.ToDouble(per) > Convert.ToDouble(slpmaxdisc)))
            {
                // MessageBox.Show("تجاوزت الخصم المسموح لك");
                MessageBox.Show("تجاوزت الخصم المسموح لك", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_desc.Text = "0";
                txt_dscper.Text = "0";
                txt_desc.SelectAll();
                // txt_desper.Text = "0";
            }

            txt_dscper.Text = (Math.Round((100 * (Convert.ToDouble(txt_desc.Text)) / Convert.ToDouble(txt_total.Text)), 2)).ToString();

            // total();

            total();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label10_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!dataGridView1.CurrentRow.IsNewRow && dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5])
            {
                oval = Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value);
              //  MessageBox.Show(oval.ToString());
            }
            // txt_remain.Text = "0";
            // txt_remain.BackColor = Color.Gainsboro;
        }

        private void dataGridView1_Enter(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
        }

        private void SALES_D_Shown(object sender, EventArgs e)
        {
            if (Convert.ToInt32(datval.convert_to_yyyyDDdd(DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false)))) > Convert.ToInt32(datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt)) || Convert.ToInt32(datval.convert_to_yyyyDDdd(DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false)))) < Convert.ToInt32(datval.convert_to_yyyyDDdd(BL.CLS_Session.start_dt)))
            {
                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Date Out of Years" : "لا يمكن ادخال حركة خارج السنة المالية", "Error خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
            // try
            //   {
            //  MessageBox.Show(dts.Rows.Count.ToString());
            //  Pos.SALES_D smn = new SALES_D();
            int form2Count;
            if (BL.CLS_Session.istuch)
                form2Count = Application.OpenForms.OfType<RESALES_D_TCH>().Count() + Application.OpenForms.OfType<SALES_D_TCH>().Count();
            else
                form2Count = Application.OpenForms.OfType<RESALES_D_TCH>().Count() + Application.OpenForms.OfType<SALES_D_TCH>().Count();
            //  MessageBox.Show(form2Count.ToString());

            if (form2Count > Convert.ToInt32(BL.CLS_Session.dtsalman.Rows.Count == 0 ? 1 : BL.CLS_Session.dtsalman.Rows[0]["scr_open"]))
            {
                BL.CLS_Session.scount = BL.CLS_Session.scount - 1;
                this.Close();
            }
            else
            {

                if (BL.CLS_Session.is_sal_login == false || BL.CLS_Session.dtsalman.Rows.Count==0)
                {
                    Pos.Salmen_LogIn salm = new Salmen_LogIn();
                    //salm.MdiParent = MdiParent;
                    salm.ShowDialog();
                    set_permition(salm.txt_salman.Text);
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            Pos.Salmen_LogIn salm = new Salmen_LogIn();
            //salm.MdiParent = MdiParent;
            salm.ShowDialog();
            set_permition(salm.txt_salman.Text);
            dataGridView1.Select();
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            DialogResult result;
            if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_delinv"]) == true && dataGridView1.Rows.Count > 1)
            {
                if (confirmd == 1)
                    result = MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Do you want to cancel this invoice" : "هل تريد الغاء الفاتورة", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                else
                    result = DialogResult.Yes;

                if (result == DialogResult.Yes)
                {
                    DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref),0) as eee from pos_hdr");
                    txt_ref.Text = Convert.ToString(dt2.Rows[0][0]);
                    int invref = Convert.ToInt32(txt_ref.Text) + 1;

                    if (dataGridView1.Rows[0].Cells[3].Value != null)
                    {
                        DataTable dtmax = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref),0)+1 from d_pos_hdr where brno='" + BL.CLS_Session.brno + "'");
                        int oref = Convert.ToInt32(dtmax.Rows[0][0]);
                        // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.sales_hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
                        using (SqlCommand cmd1 = new SqlCommand("INSERT INTO d_pos_hdr (brno,slno,ref,contr,type,date,total,count,payed,total_cost,saleman,cust_no,discount,net_total,tax_amt,o_ref) VALUES(@br,@sl,@refhd,@ctr,@a0,@a1,@a2,@a3,@a4,@a5,@a8,@a9,@a10,@a11,@a12,@orf)", con2))
                        {
                            cmd1.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                            cmd1.Parameters.AddWithValue("@sl", BL.CLS_Session.slno);
                            cmd1.Parameters.AddWithValue("@refhd", oref);
                            cmd1.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);

                            cmd1.Parameters.AddWithValue("@a0", 0);
                            cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                            // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                            cmd1.Parameters.AddWithValue("@a2", Convert.ToDouble(txt_total.Text));
                            cmd1.Parameters.AddWithValue("@a3", Convert.ToDouble(label3.Text));
                            cmd1.Parameters.AddWithValue("@a4", Convert.ToDouble(txt_recv.Text));
                            cmd1.Parameters.AddWithValue("@a5", Convert.ToDouble(textBox2.Text));
                            //  cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                            //  cmd1.Parameters.AddWithValue("@a7", comp_id);
                            cmd1.Parameters.AddWithValue("@a8", txt_salid.Text);
                            cmd1.Parameters.AddWithValue("@a9", 0);
                            cmd1.Parameters.AddWithValue("@a10", Convert.ToDouble(txt_desc.Text));
                            cmd1.Parameters.AddWithValue("@a11", (Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_desc.Text)));
                            cmd1.Parameters.AddWithValue("@a12", Convert.ToDouble(txt_tax.Text));
                            cmd1.Parameters.AddWithValue("@orf", invref);

                            if (con2.State == ConnectionState.Closed)
                                con2.Open();
                            cmd1.ExecuteNonQuery();
                        }


                        int srno = 1, reff = Convert.ToInt32(txt_ref.Text) + 1;

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            // SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from DB.dbo.sales_hdr", con2);


                            if (!row.IsNewRow && row.Cells[0].Value != null)
                            {
                                // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                                using (SqlCommand cmd = new SqlCommand("INSERT INTO d_pos_dtl (brno,slno,contr,ref,type, barcode, name, unit, price, qty, cost,is_req,itemno,srno,pkqty,discpc,tax_id,tax_amt) VALUES(@br,@sl,@ctr,@r1,@r0,@c1,@c2,@c3,@c4,@c5,@c6,@c9,@c10,@sn,@pk,@c11,@c12,@c13)", con2))
                                {
                                    cmd.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                                    cmd.Parameters.AddWithValue("@sl", BL.CLS_Session.slno);
                                    //cmd.Parameters.AddWithValue("@refdt", invref);
                                    cmd.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);

                                    cmd.Parameters.AddWithValue("@r1", oref);
                                    cmd.Parameters.AddWithValue("@r0", 0);
                                    cmd.Parameters.AddWithValue("@c1", row.Cells[0].Value);
                                   // cmd.Parameters.AddWithValue("@c2", row.Cells[2].Value);
                                    cmd.Parameters.AddWithValue("@c2", row.Cells[2].Value.ToString().Length > 100 ? row.Cells[2].Value.ToString().Substring(0, 100) : row.Cells[2].Value);
                                    cmd.Parameters.AddWithValue("@c3", row.Cells[3].Value);
                                    cmd.Parameters.AddWithValue("@c4", row.Cells[5].Value);
                                    cmd.Parameters.AddWithValue("@c5", row.Cells[4].Value);
                                    cmd.Parameters.AddWithValue("@c6", row.Cells[7].Value);
                                    // cmd.Parameters.AddWithValue("@c7", lblcashir.Text);
                                    // cmd.Parameters.AddWithValue("@c8", comp_id);
                                    cmd.Parameters.AddWithValue("@c9", 0);
                                    cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                                    cmd.Parameters.AddWithValue("@sn", srno);
                                    cmd.Parameters.AddWithValue("@pk", row.Cells[10].Value);
                                    cmd.Parameters.AddWithValue("@c11", row.Cells[6].Value);
                                    cmd.Parameters.AddWithValue("@c12", row.Cells[11].Value);
                                    cmd.Parameters.AddWithValue("@c13", string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : row.Cells[12].Value);

                                    if (con2.State == ConnectionState.Closed)
                                        con2.Open();
                                    cmd.ExecuteNonQuery();



                                }
                                srno++;
                            }
                        }
                        //   dataGridView1.Rows.Clear();
                        dataGridView1.Rows.Clear();
                        txt_ref.Text = reff.ToString();
                        // dataGridView1.Rows.Add(100);
                        // dataGridView1.TabIndex = 0;
                        txt_recv.Text = "0";
                        txt_desc.Text = "0";
                        txt_total.Text = "0";
                        lbl_mustpay.Text = "0";
                        dataGridView1.Focus();

                    }
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
            else
            {
                MessageBox.Show("غير مسموح لك بالغاء الفاتورة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.Focus();
            }

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.prnt_type.Equals("0"))
            {
                //Pos.Items_Update iu = new Pos.Items_Update();
                //iu.MdiParent = MdiParent;
                //iu.Show();
                rpt.opendrower report = new rpt.opendrower();

                ////  CrystalReport1 report = new CrystalReport1();
                //report.SetDataSource(ds);

                ////    crystalReportViewer1.ReportSource = report;

                ////  crystalReportViewer1.Refresh();
                report.SetParameterValue("datetime", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt", new CultureInfo("en-US", false)));
                //report.SetParameterValue("br_name", BL.CLS_Session.brname);
                ////    report.SetParameterValue("inv_bar", "*" + lblcashir.Text + "-" + lblref.Text + "*");
                //report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                //report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
                //  report.SetParameterValue("picpath", Directory.GetCurrentDirectory() + "\\" + BL.CLS_Session.comp_logo);



                report.PrintOptions.PrinterName = printer_nam;// "pos";

                report.PrintToPrinter(1, false, 0, 0);
                report.Close();
            }
            else
            {
                FastReport.Report rpt = new FastReport.Report();

                rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_Cashopn_rpt.frx");
                rpt.SetParameterValue("Datetim", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt", new CultureInfo("en-US", false)));
                rpt.PrintSettings.Printer = printer_nam;// "pos";
                rpt.PrintSettings.ShowDialog = false;

                rpt.Print();



            }
            dataGridView1.Focus();
        }

        private void txt_custno_Leave(object sender, EventArgs e)
        {
            DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select cu_no ,cu_name ,cl_acc,cu_crlmt from customers a join cuclass b on a.cu_class=b.cl_no  where a.inactive=0 and a.cu_brno='" + BL.CLS_Session.brno + "' and cu_no='" + txt_custno.Text + "'");

            // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

            if (dt2.Rows.Count > 0)
            {
                txt_custnam.Text = dt2.Rows[0][1].ToString();
                // txt_temp.Text = dt2.Rows[0][2].ToString();
                // txt_crlmt.Text = dt2.Rows[0][3].ToString();
                txt_recv.Text = "0";
                txt_recv.Enabled = false;
                txt_dscper.Text = dt2.Rows[0]["cu_disc"].ToString();
                // txt_dscper.Text = f4.dataGridView1.CurrentRow.Cells[6].Value.ToString();
                txt_dscper_Leave(sender, e);
                txt_cardpay.Text = "0";
                txt_cardpay.Enabled = false;
            }
            else
            {
                txt_custnam.Text = "";
                // txt_temp.Text = "";
                txt_custno.Text = "";
                txt_recv.Enabled = true;
                txt_cardpay.Enabled = true;
            }
            chk_agel_CheckedChanged(sender, e);
        }

        private void txt_custno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                // txt_name.Text = "";
                try
                {
                    Search_All f4 = new Search_All("5", "Cus");
                    f4.ShowDialog();
                    txt_custno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_custnam.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txt_dscper.Text = f4.dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    txt_dscper_Leave(sender, e);
                    // txt_temp.Text = f4.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                }
                catch { }

            }

            if (e.KeyCode == Keys.Enter)
            {
                txt_desc.Focus();
            }
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            total();
        }

        private void button3_Click_3(object sender, EventArgs e)
        {
            Pos.Items_Update iu = new Pos.Items_Update();
            iu.MdiParent = MdiParent;
            iu.Show();
            dataGridView1.Focus();
        }

        private void txt_recv_Leave(object sender, EventArgs e)
        {
            double td = Math.Round(Convert.ToDouble(txt_recv.Text), 2);
            txt_recv.Text = td.ToString();

            total();
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

        private void txt_dscper_Move(object sender, EventArgs e)
        {

        }

        private void txt_dscper_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_dscper.Text))
                txt_dscper.Text = "0";

            txt_desc.Text = (Math.Round(((Convert.ToDouble(txt_dscper.Text) * Convert.ToDouble(txt_total.Text)) / 100), 2)).ToString();
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

        private void txt_dscper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_dscper.Text == "")
                {
                    txt_dscper.Text = "0";
                    // int chk2 = dataGridView1.RowCount - 1;
                    txt_desc.Focus();
                    // dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex]
                    // dataGridView1.Rows[chk2].Cells[0].Selected = true;
                }
                else
                {

                    // txt_recv.Text = "0";
                    txt_desc.Focus();
                    total();
                }
            }
        }

        private void txt_dscper_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txt_dscper_TextChanged(object sender, EventArgs e)
        {
            datval.ValidateText_numric(txt_dscper);
        }

        private void SALES_D_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
               // btn_Exit_Click(sender, e);
            }

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
            {
                Pos.RESALES_D_TCH sales = new Pos.RESALES_D_TCH();
                // SALES sales = new SALES();
                sales.MdiParent = MdiParent;

                //var min = new MAIN();
                //if (min.scount == 0)
                //    min.scount = 1;
                //else
                //    min.scount = min.scount + 1;

                //sales.Text = this.Text + "  " + min.scount.ToString();
                sales.Show();
            }
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.M)
            {
                if (txt_mobil.Visible == false || lbl_mobil.Visible == false)
                {
                    txt_mobil.Visible = true;
                    lbl_mobil.Visible = true;
                    txt_mobil.Focus();
                }
                else
                {
                    txt_mobil.Visible = false;
                    lbl_mobil.Visible = false;
                    txt_recv.Focus();
                }
            }

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S && dataGridView1.RowCount>1)
            {
                total();

                if (string.IsNullOrEmpty(txt_custno.Text) && !chk_agel.Checked)
                {
                    txt_recv.Text = BL.CLS_Session.isshamltax.Equals("2") ? (Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_desc.Text)).ToString() : (Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_desc.Text) + Convert.ToDouble(txt_tax.Text)).ToString();
                  //  MessageBox.Show(txt_recv.Text);
                    button1_Click(sender, e);
                }
            }

            if (e.KeyCode == Keys.F2)
            {
                cmb_cards.Select();
                SendKeys.Send("{F4}");
            }

            if (e.KeyCode == Keys.F1)
            {
                txt_recv.Focus();
            }

            if (e.KeyCode == Keys.F3 && btn_opndr.Enabled == true)
            {
                btn_update_Click(sender, e);
            }

            if (e.KeyCode == Keys.F5)
            {
                bool ism = this.WindowState == FormWindowState.Maximized ? true : false;
                Items_Update iu = new Items_Update();
                try
                {

                    iu.MdiParent = MdiParent;
                    iu.Show();
                    iu.chk_offers.Checked = true;
                    iu.button1_Click(sender, e);
                    iu.Close();
                    this.WindowState = ism ? FormWindowState.Maximized : FormWindowState.Normal;
                }
                catch
                {
                    iu.Close();
                    this.WindowState = ism ? FormWindowState.Maximized : FormWindowState.Normal;
                }
            }

            if (e.KeyCode == Keys.F6)
            {
                Items_Update iu = new Items_Update();
                try
                {

                    iu.MdiParent = MdiParent;
                    iu.Show();
                    iu.button2_Click(sender, e);
                    iu.Close();
                }
                catch
                {
                    iu.Close();
                }
            }

            if (e.KeyCode == Keys.F9)
            {
                BL.CLS_Session.is_sal_login = true;
                button2_Click_1(sender, e);
            }
        }

        private void cmb_cards_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_cardpay.Text = (Convert.ToDouble(lbl_mustpay.Text) - Convert.ToDouble(txt_recv.Text)) > 0 ? (Convert.ToDouble(lbl_mustpay.Text) - Convert.ToDouble(txt_recv.Text)).ToString() : "0";
                txt_cardpay.Focus();
            }
        }

        private void txt_cardpay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_recv.Focus();
            }
        }

        private void txt_cardpay_Leave(object sender, EventArgs e)
        {
            double td = Math.Round(Convert.ToDouble(txt_cardpay.Text), 2);
            txt_cardpay.Text = td.ToString();

            total();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 4 || dataGridView1.CurrentCell.ColumnIndex == 5 || dataGridView1.CurrentCell.ColumnIndex == 6) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }

            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl tb = e.Control as DataGridViewTextBoxEditingControl;
                tb.KeyDown -= dataGridView1_KeyUp;
                tb.KeyDown += dataGridView1_KeyUp;
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }


        private void txt_cardpay_TextChanged(object sender, EventArgs e)
        {
            datval.ValidateText_numric(txt_cardpay);
           // total();
        }

        private void txt_custno_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_custno.Text))
                txt_custnam.ReadOnly = false;
            else
                txt_custnam.ReadOnly = true;

        }

        private void textBox3_LocationChanged(object sender, EventArgs e)
        {
            //if (!dataGridView1.CurrentRow.IsNewRow && dataGridView1.CurrentRow.Cells[0].ReadOnly == false)
            //{


        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            try
            {
                //  MessageBox.Show(dataGridView1[0, dataGridView1.Rows.Count - 2].Value.ToString());
                string date = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US", false));
                DataTable dtchkoffer = daml.SELECT_QUIRY_only_retrn_dt("select * from items_offer where InActive=0 and br_no='" + BL.CLS_Session.brno + "' and sl_no in ('','" + BL.CLS_Session.slno + "') and barcode='" + dataGridView1[0, dataGridView1.Rows.Count - 2].Value + "' and '" + date + "' between StartDate and EndDate");
                if (dtchkoffer.Rows.Count > 0)
                {
                    if (dtchkoffer.Rows[0]["disctype"].ToString().Equals("0"))
                    {
                        dataGridView1[5, dataGridView1.Rows.Count - 2].Value = dtchkoffer.Rows[0]["offer_price"];
                        dataGridView1[8, dataGridView1.Rows.Count - 2].Value = Convert.ToDouble(dataGridView1[4, dataGridView1.Rows.Count - 2].Value) * Convert.ToDouble(dataGridView1[5, dataGridView1.Rows.Count - 2].Value);
                        dataGridView1.Rows[dataGridView1.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Gold;
                        total();
                    }
                    //if (dtchkoffer.Rows[0]["disctype"].ToString().Equals("1"))
                    //{


                    //    //if (dataGridView1.CurrentRow.Cells[0].Value == dtchkoffer.Rows[0]["barcode"])
                    //    //    {
                    //    //        double sum = Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value);
                    //    //        dataGridView1.CurrentRow.Cells[4].Value = Convert.ToDouble(sum) + 1;

                    //    //    }
                    //    double count = 0, sumc = 0;
                    //    foreach (DataGridViewRow dtgvr in dataGridView1.Rows)
                    //    {

                    //        if (!dtgvr.IsNewRow && dtgvr.Cells[0].Value.ToString() == dtchkoffer.Rows[0]["barcode"].ToString())
                    //        {
                    //            //  MessageBox.Show(dtgvr.Cells[0].Value.ToString());
                    //            sumc = sumc + Convert.ToDouble(dtgvr.Cells[4].Value);
                    //            // double sum= Convert.ToDouble(dtgvr.Cells[4].Value);
                    //            // dtgvr.Cells[4].Value = sum + 1;
                    //            // dataGridView1.Rows.Remove(dtgvr);
                    //            count++;
                    //        }

                    //        //count++;
                    //    }
                    //    // MessageBox.Show(sumc.ToString());
                    //    if (sumc > 0 && sumc % Convert.ToDouble(dtchkoffer.Rows[0]["minqty"]) == 0 && (Convert.ToDouble(dtchkoffer.Rows[0]["maxqty"]) > 0 ? sumc <= Convert.ToDouble(dtchkoffer.Rows[0]["maxqty"]) : sumc > 0))
                    //    {
                    //        dataGridView1[5, dataGridView1.Rows.Count - 2].Value = Convert.ToDouble(dataGridView1[5, dataGridView1.Rows.Count - 2].Value) - Convert.ToDouble(dtchkoffer.Rows[0]["DiscountP"]);
                    //        dataGridView1[8, dataGridView1.Rows.Count - 2].Value = Convert.ToDouble(dataGridView1[4, dataGridView1.Rows.Count - 2].Value) * Convert.ToDouble(dataGridView1[5, dataGridView1.Rows.Count - 2].Value);
                    //        dataGridView1.Rows[dataGridView1.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Gold;
                    //    }
                    //    /*
                    //    dataGridView1.CurrentRow.Cells[5].Value = dtchkoffer.Rows[0]["offer_price"];
                    //    dataGridView1.CurrentRow.Cells[8].Value = Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value);
                    //    dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen;
                    //     * */
                    //    total();

                    //}
                }
            }
            catch
            { }
        }

        private void txt_recv_Enter(object sender, EventArgs e)
        {
            total();
        }

        private void txt_cardpay_Enter(object sender, EventArgs e)
        {
            total();
        }

        private void txt_desc_Enter(object sender, EventArgs e)
        {
            total();
        }

        private void txt_dscper_Enter(object sender, EventArgs e)
        {
            total();
        }

        private void txt_total_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void txt_code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string strqadd = "";
                    if (dataGridView2.ColumnCount > 4)
                    {
                        for (int co = 0; co <= dataGridView2.ColumnCount - 4; co++)
                        {
                            dataGridView2.Columns.RemoveAt(4);
                        }
                    }

                    if (!string.IsNullOrEmpty(txt_code.Text))
                    {
                        SqlDataAdapter da = new SqlDataAdapter("select * from server", con2);
                        DataTable dt = new DataTable();
                        da.Fill(dt);



                        string condi = label16.Text.Equals("باركود") ? " and b.barcode='" + txt_code.Text + "'" : " and i.item_no='" + txt_code.Text + "'";
                        //  string condi = " and b.barcode='" + txt_code.Text + "' ";
                        // select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img from items i join items_bc b on b.item_no=i.item_no and i.item_no='" + textBox3.Text + "' join taxs t on i.item_tax=t.tax_id", con2);
                        SqlDataAdapter da23 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_curcost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img,b.min_price mp from items i join items_bc b on b.item_no=i.item_no " + condi + " join taxs t on i.item_tax=t.tax_id where i.inactive=0", con2);
                        DataTable dt2 = new DataTable();
                        da23.Fill(dt2);

                        if (dt2.Rows.Count == 1)
                        {
                            // txt_code.Text = dt.Rows[0][0].ToString();
                            txt_price.Text = dt2.Rows[0][3].ToString();
                            // txt_newp.Select();
                            //  dataGridView2.Visible = true;
                        }
                        else
                        {
                            txt_code.Text = "";
                            txt_price.Text = "";
                        }

                        if (checkBox1.Checked)
                        {
                            SqlConnection con_dest = new SqlConnection("Data Source=" + dt.Rows[0][0].ToString() + ";Initial Catalog=" + dt.Rows[0][1].ToString() + ";User id=" + dt.Rows[0][2].ToString() + ";Password=" + endc.Decrypt(dt.Rows[0][3].ToString(), true) + ";Connection Timeout=" + dt.Rows[0][4].ToString() + "");


                            // DataTable dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select wh_no,wh_name from whouses where wh_brno='" + BL.CLS_Session.brno + "'");
                            SqlDataAdapter da233 = new SqlDataAdapter("select wh_no,wh_name from whouses where wh_brno='" + BL.CLS_Session.brno + "'", con_dest);
                            DataTable dt23 = new DataTable();
                            da233.Fill(dt23);

                            foreach (DataRow dtrr in dt23.Rows)
                            {
                                var col = new DataGridViewTextBoxColumn();
                                //  var col4 = new DataGridViewCheckBoxColumn();

                                col.HeaderText = dtrr[1].ToString();// "Column3";
                                col.Name = dtrr[1].ToString(); //"Column3";
                                // col.FillWeight = 15;
                                ////  col.Width = 100;
                                col.DataPropertyName = "val" + dtrr[0].ToString();
                                strqadd = strqadd + " ,sum(case when b.wh_no='" + dtrr[0].ToString() + "' then b.openbal + b.qty else 0.00 end) as val" + dtrr[0].ToString();
                                //  col4.HeaderText = "Column4";
                                //  col4.Name = "Column4";

                                // dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col3, col4 });


                                dataGridView2.Columns.AddRange(new DataGridViewColumn[] { col });
                            }

                            var col2 = new DataGridViewTextBoxColumn();
                            //  var col4 = new DataGridViewCheckBoxColumn();

                            col2.HeaderText = "اجمالي الرصيد";// "Column3";
                            col2.Name = "اجمالي الرصيد"; //"Column3";
                            // col.FillWeight = 15;
                            ////   col2.Width = 100;
                            col2.DataPropertyName = "valtotal";
                            strqadd = strqadd + " ,sum(isnull((b.openbal + b.qty),0)) as valtotal ";
                            //  col4.HeaderText = "Column4";
                            //  col4.Name = "Column4";

                            // dataGridView1.Columns.AddRange(new DataGridViewColumn[] { col3, col4 });
                            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { col2 });

                            // string query = "select a.item_no a1, a.item_name a2,cast(a.item_unit as nvarchar) a3,round(a.item_curcost,2) a4,a.item_price a5 " + strqadd + " from items a WITH (NOLOCK) join whbins b WITH (NOLOCK) on a.item_no=b.item_no where a.item_no='" + txt_code.Text + "'";
                            string condi2 = label16.Text.Equals("باركود") ? " a.item_barcode " : " a.item_no ";
                            SqlDataAdapter da2334 = new SqlDataAdapter("select a.item_no a1, a.item_name a2,cast(a.item_unit as nvarchar) a3," + (BL.CLS_Session.up_sal_icost ? "round(a.item_curcost,2) " : " 0 ") + " a4 " + strqadd + " from items a WITH (NOLOCK) join whbins b WITH (NOLOCK) on a.item_no=b.item_no where " + condi2 + "='" + txt_code.Text + "' group by a.item_no,a.item_name,a.item_unit,a.item_curcost ", con_dest);
                            DataTable dt234 = new DataTable();
                            da2334.Fill(dt234);

                            dtunits = daml.SELECT_QUIRY_only_retrn_dt("select * from units");
                            //    double sumopnd = 0;// sumopnm = 0, sumtrd = 0;//, sumtrm = 0, sumttld = 0, sumttlm = 0;
                            foreach (DataRow dtr in dt234.Rows)
                            {
                                //  sumopnd = sumopnd + Convert.ToDouble(dtr[2]);
                                //sumopnm = sumopnm + Convert.ToDouble(dtr[6]);
                                // sumtrd = sumtrd + Convert.ToDouble(dtr[7]);

                                DataRow[] res = dtunits.Select("unit_id ='" + dtr[2] + "'");
                                dtr[2] = res[0][1];
                                // row[6] = "%" + row[6];

                                // sumtrm = sumtrm + Convert.ToDouble(dtr[9]);
                                // sumttld = sumttld + Convert.ToDouble(dtr[6]);
                                //  sumttlm = sumttlm + Convert.ToDouble(dtr[7]);
                            }

                            if (dt234.Rows.Count > 0)
                            {
                                dataGridView2.DataSource = dt234;
                                dataGridView2.Visible = true;
                                //  dataGridView2.Refresh();
                            }
                            else
                                dataGridView2.Visible = false;

                            dataGridView2.ClearSelection();
                        }
                    }
                    else
                    {
                        //  MessageBox.Show("ادخل رقم الباركود");
                        dataGridView2.Visible = false;
                    }
                }
                catch
                {
                    MessageBox.Show("قد يكون النت مفصول او الجهاز الرئيسي غير متصل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private void txt_code_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_code.Text))
                dataGridView2.Visible = false;
        }

        private void label16_Click(object sender, EventArgs e)
        {

            if (label16.Text.Equals("باركود"))
                label16.Text = "صنف";
            else
                label16.Text = "باركود";
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
        }

        private void SALES_D_FormClosing(object sender, FormClosingEventArgs e)
        {
            confirmd = 0;
            if (dataGridView1.Rows.Count > 1)
            {
                 button3_Click_2(sender, e);
            }
            
        }

        private void SALES_D_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*
            if (dataGridView1.Rows.Count > 1)
            {
               
                DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref),0) as eee from pos_hdr");
                txt_ref.Text = Convert.ToString(dt2.Rows[0][0]);
                int invref = Convert.ToInt32(txt_ref.Text) + 1;

                if (dataGridView1.Rows[0].Cells[3].Value != null)
                {
                    DataTable dtmax = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref),0)+1 from d_pos_hdr where brno='" + BL.CLS_Session.brno + "'");
                    int oref = Convert.ToInt32(dtmax.Rows[0][0]);
                    // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.sales_hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
                    using (SqlCommand cmd1 = new SqlCommand("INSERT INTO d_pos_hdr (brno,slno,ref,contr,type,date,total,count,payed,total_cost,saleman,cust_no,discount,net_total,tax_amt,o_ref) VALUES(@br,@sl,@refhd,@ctr,@a0,@a1,@a2,@a3,@a4,@a5,@a8,@a9,@a10,@a11,@a12,@orf)", con2))
                    {
                        cmd1.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                        cmd1.Parameters.AddWithValue("@sl", BL.CLS_Session.slno);
                        cmd1.Parameters.AddWithValue("@refhd", oref);
                        cmd1.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);

                        cmd1.Parameters.AddWithValue("@a0", 1);
                        cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                        // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                        cmd1.Parameters.AddWithValue("@a2", Convert.ToDouble(txt_total.Text));
                        cmd1.Parameters.AddWithValue("@a3", Convert.ToDouble(label3.Text));
                        cmd1.Parameters.AddWithValue("@a4", Convert.ToDouble(txt_recv.Text));
                        cmd1.Parameters.AddWithValue("@a5", Convert.ToDouble(textBox2.Text));
                        //  cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                        //  cmd1.Parameters.AddWithValue("@a7", comp_id);
                        cmd1.Parameters.AddWithValue("@a8", txt_salid.Text);
                        cmd1.Parameters.AddWithValue("@a9", 0);
                        cmd1.Parameters.AddWithValue("@a10", Convert.ToDouble(txt_desc.Text));
                        cmd1.Parameters.AddWithValue("@a11", (Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_desc.Text)));
                        cmd1.Parameters.AddWithValue("@a12", Convert.ToDouble(txt_tax.Text));
                        cmd1.Parameters.AddWithValue("@orf", invref);
                        cmd1.ExecuteNonQuery();
                    }


                    int srno = 1, reff = Convert.ToInt32(txt_ref.Text) + 1;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from DB.dbo.sales_hdr", con2);


                        if (!row.IsNewRow && row.Cells[0].Value != null)
                        {
                            // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO d_pos_dtl (brno,slno,contr,ref,type, barcode, name, unit, price, qty, cost,is_req,itemno,srno,pkqty,discpc,tax_id,tax_amt) VALUES(@br,@sl,@ctr,@r1,@r0,@c1,@c2,@c3,@c4,@c5,@c6,@c9,@c10,@sn,@pk,@c11,@c12,@c13)", con2))
                            {
                                cmd.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                                cmd.Parameters.AddWithValue("@sl", BL.CLS_Session.slno);
                                //cmd.Parameters.AddWithValue("@refdt", invref);
                                cmd.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);

                                cmd.Parameters.AddWithValue("@r1", oref);
                                cmd.Parameters.AddWithValue("@r0", 1);
                                cmd.Parameters.AddWithValue("@c1", row.Cells[0].Value);
                                cmd.Parameters.AddWithValue("@c2", row.Cells[2].Value);
                                cmd.Parameters.AddWithValue("@c3", row.Cells[3].Value);
                                cmd.Parameters.AddWithValue("@c4", row.Cells[5].Value);
                                cmd.Parameters.AddWithValue("@c5", row.Cells[4].Value);
                                cmd.Parameters.AddWithValue("@c6", row.Cells[7].Value);
                                // cmd.Parameters.AddWithValue("@c7", lblcashir.Text);
                                // cmd.Parameters.AddWithValue("@c8", comp_id);
                                cmd.Parameters.AddWithValue("@c9", 0);
                                cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                                cmd.Parameters.AddWithValue("@sn", srno);
                                cmd.Parameters.AddWithValue("@pk", row.Cells[10].Value);
                                cmd.Parameters.AddWithValue("@c11", row.Cells[6].Value);
                                cmd.Parameters.AddWithValue("@c12", row.Cells[11].Value);
                                cmd.Parameters.AddWithValue("@c13", string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : row.Cells[12].Value);
                                cmd.ExecuteNonQuery();



                            }
                            srno++;
                        }
                    }



                }
                
            }
        */
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
           
            //try
            //{
            //    if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
            //        dataGridView1[4, dataGridView1.CurrentRow.Index].Value = "0.00";

            //    if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[5].Value.ToString()))
            //        dataGridView1[5, dataGridView1.CurrentRow.Index].Value = "0.00";

            //    if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[2].Value.ToString()))
            //        dataGridView1[6, dataGridView1.CurrentRow.Index].Value = "0.00";
            //}
            //catch { }
        }

        private void txt_custnam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_recv.Focus();
            }
        }

        private void txt_recv_Validating(object sender, CancelEventArgs e)
        {
           // datval.ValidateText_numric(txt_recv);
        }

        private void txt_mobil_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_recv.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            button3_Click_1(sender, e);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            total();
            if (string.IsNullOrEmpty(txt_invbar.Text) && BL.CLS_Session.is_einv)
            {
                MessageBox.Show("يجب ادخال رقم فاتورة البيع في حال الارجاع", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_invbar.Focus();
                return;
            }

           

            btn_Save.Enabled = false;
            txt_recv.Text = lbl_mustpay.Text;
            total();
            prt = false;
            if (Convert.ToDouble(lbl_mustpay.Text) <= 0)
            {
                MessageBox.Show("مبلغ الفاتورة غير صحيح", "تحدذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btn_Save.Enabled = true;
                // btn_Save.Enabled = true;
                return;
            }
            button1_Click( sender,  e);
            prt = true;

            DialogResult result = MessageBox.Show("هل تريد طباعة الفاتورة", "تاكيد الطباعة", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                button3_Click_1(sender, e);
                btn_Save.Enabled = true;
            }
            else if (result == DialogResult.No)
            {
                //...
                if (!BL.CLS_Session.nocahopen)
                btn_update_Click(sender, e);
                btn_Save.Enabled = true;
               
            }
            else
            {
                //...
                btn_Save.Enabled = true;
               
            }
            txt_invbar.Text = "";
            dataGridView1.Focus();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            button3_Click_2(sender, e);
        }

        private void btn_prtdirct_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.istuch)
            {
                Pos.RESALES_D_TCH sales = new Pos.RESALES_D_TCH();
                // SALES sales = new SALES();
                sales.MdiParent = MdiParent;
                sales.Show();
            }
            else
            {
                Pos.RESALES_D_TCH sales = new Pos.RESALES_D_TCH();
                // SALES sales = new SALES();
                sales.MdiParent = MdiParent;
                sales.Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            btn_Exit_Click(sender, e);
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                if (selectedIndex > -1 && sl_delline == true)
                {
                    //dataGridView1.Rows.RemoveAt(selectedIndex);
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.IsNewRow ? selectedIndex - 1 : selectedIndex);
                    dataGridView1.Refresh();
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                    dataGridView1.Select();
                    total();
                    if (dataGridView1.Rows.Count <= 1)
                        btn_Exit.Enabled = sl_alowexit ? true : false;
                    // dataGridView1.CurrentCell = dataGridView1.Rows[selectedIndex+1].Cells[0];
                    // if needed

                    cssc.txt_iname.Text = "";
                    cssc.txt_iprice.Text = "0.00";
                    cssc.txt_total.Text = total1.ToString();
                }
                dataGridView1.Focus();
            }
            catch { }
        }

        private void chk_payed_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_payed.Checked)
            {
                txt_recv.Text = lbl_mustpay.Text;
                txt_recv.Focus();
            }
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            try
            {
               

                //if (!dataGridView1.CurrentRow.Cells[0].Value.ToString().StartsWith("+"))

                //if (dataGridView1.Rows.Count >= 0)
                //{
                //    dataGridView1.Rows.RemoveAt(rowindex);
                //    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                //}

                if ( Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_chgqty"]))
                {
                    if (dataGridView1.CurrentRow.IsNewRow)
                    {
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value = Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value) + 1;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[8].Value = (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[6].Value) / 100);// + Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value);
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[6].Value)) / 100) : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                        if (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value) > 1)
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].DefaultCellStyle.BackColor = Color.PaleGreen;
                        else
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].DefaultCellStyle.BackColor = Color.White;

                        chk_qty_offer(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1]);
                        total();
                    }
                    else
                    {
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value = Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value) + 1;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[8].Value = (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[6].Value) / 100);// + Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value);
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[6].Value)) / 100) : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                       
                        if (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value) > 1)
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].DefaultCellStyle.BackColor = Color.PaleGreen;
                        else
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].DefaultCellStyle.BackColor = Color.White;
                        
                        chk_qty_offer(dataGridView1.Rows[dataGridView1.CurrentRow.Index]);
                        total();
                    }
                    cssc.txt_iname.Text = "";
                    cssc.txt_iprice.Text = "0.00";
                    cssc.txt_total.Text = total1.ToString();
                }
                dataGridView1.Focus();
            }
            catch { }
        }

        private void btn_mins_Click(object sender, EventArgs e)
        {
            try
            {
               

                //if (!dataGridView1.CurrentRow.Cells[0].Value.ToString().StartsWith("+"))

                //if (dataGridView1.Rows.Count >= 0)
                //{
                //    dataGridView1.Rows.RemoveAt(rowindex);
                //    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                //}

                if (Convert.ToBoolean(BL.CLS_Session.dtsalman.Rows[0]["sl_chgqty"]))
                {
                    if (dataGridView1.CurrentRow.IsNewRow)
                    {
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value = Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value) - 1;
                        if (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value) == 0)
                        {
                            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index - 1);
                            dataGridView1.Refresh();
                            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                            dataGridView1.Select();
                            total();
                        }
                        else
                        {
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[8].Value = (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[6].Value) / 100);// + Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value);
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[6].Value)) / 100) : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                            if (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[4].Value) > 1)
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].DefaultCellStyle.BackColor = Color.PaleGreen;
                            else
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].DefaultCellStyle.BackColor = Color.White;
                            
                            chk_qty_offer(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1]);
                            total();
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value = Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value) - 1; 
                        if (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index ].Cells[4].Value) == 0)
                        {
                            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index );
                            dataGridView1.Refresh();
                            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[0];
                            dataGridView1.Select();
                            total();
                        }
                        else
                        {
                           // dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value = Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value) - 1;
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value = (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value) / 100);// + Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value);
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value)) / 100) : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                            if (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value) > 1)
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].DefaultCellStyle.BackColor = Color.PaleGreen;
                            else
                                dataGridView1.Rows[dataGridView1.CurrentRow.Index].DefaultCellStyle.BackColor = Color.White;
                            
                            chk_qty_offer(dataGridView1.Rows[dataGridView1.CurrentRow.Index]);
                            total();
                        }
                    }

                    cssc.txt_iname.Text = "";
                    cssc.txt_iprice.Text = "0.00";
                    cssc.txt_total.Text = total1.ToString();
                }
                dataGridView1.Focus();
            }
            catch { }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                //textBox3.Select();
                //Search_All itm = new Search_All("2", "Pos");
                //itm.ShowDialog();
                //textBox3.Text = itm.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                dataGridView1.Focus();
                dataGridView1.Focus();
                SendKeys.Send("{F8}");
            }
            catch { }
        }

        //public static void Main(string[] args)
        //{
        //    using (Demo demo = new Demo())
        //    {
        //        demo.Run();
        //    }
        //}

        private void moveUp()
        {
            if (dataGridView1.RowCount > 0)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowCount = dataGridView1.Rows.Count;
                    int index = dataGridView1.SelectedCells[0].OwningRow.Index;

                    if (index == 0)
                    {
                        return;
                    }
                    DataGridViewRowCollection rows = dataGridView1.Rows;

                    // remove the previous row and add it behind the selected row.
                    DataGridViewRow prevRow = rows[index - 1];
                    rows.Remove(prevRow);
                    prevRow.Frozen = false;
                    rows.Insert(index, prevRow);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[index - 1].Selected = true;
                }
            }
        }

        private void moveDown()
        {
            if (dataGridView1.RowCount > 0)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowCount = dataGridView1.Rows.Count;
                    int index = dataGridView1.SelectedCells[0].OwningRow.Index;

                    if (index == (rowCount - 2)) // include the header row
                    {
                        return;
                    }
                    DataGridViewRowCollection rows = dataGridView1.Rows;

                    // remove the next row and add it in front of the selected row.
                    DataGridViewRow nextRow = rows[index + 1];
                    rows.Remove(nextRow);
                    nextRow.Frozen = false;
                    rows.Insert(index, nextRow);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[index + 1].Selected = true;
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dataGridView1;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == totalRows - 1)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
               // dgv.Rows.Insert(rowIndex + 1, selectedRow);
                dgv.Rows.Insert(dgv.Rows.Count - 1 , selectedRow);
                dgv.ClearSelection();
               // dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;
                dgv.Rows[dataGridView1.NewRowIndex].Cells[colIndex].Selected = true;
            }
            catch { }
        }

        private void btnDown(DataGridViewRow dtgvr)
        {
            DataGridView dgv = dataGridView1;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                //int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                int rowIndex = dtgvr.Index;
                if (rowIndex == totalRows - 1)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(dgv.CurrentRow);
                dgv.Rows.Remove(selectedRow);
                // dgv.Rows.Insert(rowIndex + 1, selectedRow);
                dgv.Rows.Insert(dgv.Rows.Count - 1, selectedRow);
                dgv.ClearSelection();
                // dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;
                dgv.Rows[dataGridView1.NewRowIndex].Cells[colIndex].Selected = true;
            }
            catch { }
        }

        private void btn_Find_Click(object sender, EventArgs e)
        {
            InvPrview inv = new InvPrview();
            inv.ShowDialog();
            dataGridView1.Focus();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            btnDown_Click(sender, e);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[6])
            //    {

            //        dataGridView1.CurrentRow.Cells[8].Value = (Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) / 100);// + Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value);
            //        dataGridView1.CurrentRow.Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.CurrentRow.Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)) / 100) : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.CurrentRow.Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.CurrentRow.Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
            //        if (Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value) > 1)
            //            dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.PaleGreen;
            //        else
            //            dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.White;

            //        chk_qty_offer(dataGridView1.CurrentRow);
            //        total();
            //    }
            //}
            //catch { }
        }

        private void chk_qty_offer(DataGridViewRow dtgvr)
        {
           // DataGridView dgv = dataGridView1;
            try
            {
                double tqty =Convert.ToDouble(dtgvr.Cells[4].Value) ;
               // string itemno = dtgvr.Cells[1].Value.ToString();
                string barcode = dtgvr.Cells[0].Value.ToString();

                string date = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US", false));
               // DataTable dtchkoffer = daml.SELECT_QUIRY_only_retrn_dt("select * from items_offer where InActive=0 and br_no='" + BL.CLS_Session.brno + "' and sl_no in ('','" + BL.CLS_Session.slno + "') and MinQty=" + tqty + " and disctype='1' and barcode='" + barcode + "' and '" + date + "' between StartDate and EndDate");
                DataTable dtchkoffer = daml.SELECT_QUIRY_only_retrn_dt("select * from items_offer where InActive=0 and br_no='" + BL.CLS_Session.brno + "' and sl_no in ('" + BL.CLS_Session.slno + "') and disctype='1' and barcode='" + barcode + "' and '" + date + "' between StartDate and EndDate");
               
                if (dtchkoffer.Rows.Count > 0)
                {
                  //  dataGridView1.Rows[dtgvr.Index ].Cells[5].Value = dtchkoffer.Rows[0]["DiscountP"];
                  //  dataGridView1.Rows[dtgvr.Index].Cells[15].Value = (Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[5].Value) % Convert.ToDouble(dtchkoffer.Rows[0]["MinQty"])) == 0 ? ((Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[5].Value) / Convert.ToDouble(dtchkoffer.Rows[0]["MinQty"])) * Convert.ToDouble(dtchkoffer.Rows[0]["DiscountP"])) : null;
                    if ((tqty / Convert.ToDouble(dtchkoffer.Rows[0]["MinQty"])) >= 1)
                    // dataGridView1.Rows[dtgvr.Index].Cells[15].Value = dtchkoffer.Rows[0]["DiscountP"];
                    {
                        if(tqty % Convert.ToDouble(dtchkoffer.Rows[0]["MinQty"])==0)
                            dataGridView1.Rows[dtgvr.Index].Cells[15].Value = ((tqty / Convert.ToDouble(dtchkoffer.Rows[0]["MinQty"])) * Convert.ToDouble(dtchkoffer.Rows[0]["DiscountP"]));
                        else //if ((tqty / Convert.ToDouble(dtchkoffer.Rows[0]["MinQty"])) < 1 && tqty % Convert.ToDouble(dtchkoffer.Rows[0]["MinQty"]) != 0)
                            dataGridView1.Rows[dtgvr.Index].Cells[15].Value =Math.Floor(((tqty / Convert.ToDouble(dtchkoffer.Rows[0]["MinQty"])))) * Convert.ToDouble(dtchkoffer.Rows[0]["DiscountP"]);
                         //   else
                         //   dataGridView1.Rows[dtgvr.Index].Cells[15].Value = null;// ((tqty / Convert.ToDouble(dtchkoffer.Rows[0]["MinQty"])) * Convert.ToDouble(dtchkoffer.Rows[0]["DiscountP"]));
                        dataGridView1.Rows[dtgvr.Index].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else
                    {
                        dataGridView1.Rows[dtgvr.Index].Cells[15].Value = null;

                        if(Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[4].Value)>1)
                            dataGridView1.Rows[dtgvr.Index].DefaultCellStyle.BackColor = Color.PaleGreen;
                        else
                            dataGridView1.Rows[dtgvr.Index].DefaultCellStyle.BackColor = Color.White;
                    }
                    dataGridView1.Rows[dtgvr.Index].Cells[8].Value = (Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[5].Value)) - Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[15].Value);
                    dataGridView1.Rows[dtgvr.Index].Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[6].Value)) / 100) : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[6].Value)) / 100) : (Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[9].Value)) * ((Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[4].Value)) * Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                    total();
                   // dataGridView1.Rows[dtgvr.Index].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if(Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[4].Value)>1)
                            dataGridView1.Rows[dtgvr.Index].DefaultCellStyle.BackColor = Color.PaleGreen;
                        else
                            dataGridView1.Rows[dtgvr.Index].DefaultCellStyle.BackColor = Color.White;
                
                //int totalRows = dgv.Rows.Count;
                //// get index of the row for the selected cell
                ////int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                //int rowIndex = dtgvr.Index;
                //if (rowIndex == totalRows - 1)
                //    return;
                //// get index of the column for the selected cell
                //int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                //DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                //dgv.Rows.Remove(selectedRow);
                //// dgv.Rows.Insert(rowIndex + 1, selectedRow);
                //dgv.Rows.Insert(dgv.Rows.Count - 1, selectedRow);
                //dgv.ClearSelection();
                //// dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;
                //dgv.Rows[dataGridView1.NewRowIndex].Cells[colIndex].Selected = true;

                dataGridView1.Rows[dtgvr.Index].DefaultCellStyle.BackColor = Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[15].Value) > 0 ? Color.Yellow : Convert.ToDouble(dataGridView1.Rows[dtgvr.Index].Cells[4].Value) > 1 ? Color.PaleGreen : Color.White;
            }
            catch { }
        }

        private void btn_resrv_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void txt_invbar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txt_invbar.Text))
            {

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

                   
                    //SqlDataAdapter da23;

                    //if (ci.textBox1.Text.Contains("-"))
                    //{
                    //    SqlConnection con_dest = new SqlConnection("Data Source=" + serset.Rows[0][0].ToString() + ";Initial Catalog=" + serset.Rows[0][1].ToString() + ";User id=" + serset.Rows[0][2].ToString() + ";Password=" + endc.Decrypt(serset.Rows[0][3].ToString(), true) + ";Connection Timeout=" + serset.Rows[0][4].ToString() + "");
                    //    da23 = new SqlDataAdapter("select b.itemno , b.name,i.item_curcost,b.price as item_price,b.barcode as  item_barcode,b.unit pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,1 pkqty,i.item_tax i_tax,i.item_image img,b.min_price mp,b.qty,b.discpc from r_pos_dtl b join items i on b.itemno=i.item_no join taxs t on i.item_tax=t.tax_id  where b.contr=" + s1 + " and b.ref=" + s2 + "", con_dest);
                    //}
                    //else
                    //    da23 = new SqlDataAdapter("select b.itemno , b.name,i.item_curcost,b.price as item_price,b.barcode as  item_barcode,b.unit pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,1 pkqty,i.item_tax i_tax,i.item_image img,b.min_price mp,b.qty,b.discpc from r_pos_dtl b join items i on b.itemno=i.item_no join taxs t on i.item_tax=t.tax_id  where b.contr=" + s1 + " and b.ref=" + s2 + "", con2);
                   // SqlDataAdapter da22 = new SqlDataAdapter("select b.itemno , b.name,i.item_curcost,b.price as item_price,b.barcode as  item_barcode,b.unit pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,1 pkqty,i.item_tax i_tax,i.item_image img,b.min_price mp,b.qty,b.discpc from pos_dtl b join items i on b.itemno=i.item_no join taxs t on i.item_tax=t.tax_id  where b.contr=" + s1 + " and b.ref=" + s2 + "", con2);

                    SqlDataAdapter da23h = new SqlDataAdapter("select * from pos_hdr where contr=" + s1 + " and ref=" + s2 + "", con2);
                    
                    SqlDataAdapter da23 = new SqlDataAdapter("select b.itemno , b.name,i.item_curcost,b.price as item_price,b.barcode as  item_barcode,b.unit pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent, pkqty,i.item_tax i_tax,i.item_image img,i.min_price mp,b.qty,b.discpc from pos_dtl b join items i on b.itemno=i.item_no join taxs t on i.item_tax=t.tax_id  where b.contr=" + s1 + " and b.ref=" + s2 + "", con2);
                    
                    DataSet ds23 = new DataSet();
                    da23.Fill(ds23, "0");
                    da23h.Fill(ds23, "1");

                  //  MessageBox.Show(ds23.Tables["0"].Rows.Count.ToString());
                    if (ds23.Tables["0"].Rows.Count > 0)
                    {
                       

                        Select_Rtn rtn = new Select_Rtn(ds23.Tables["0"], ds23.Tables["1"].Rows[0]["discount"].ToString(), ds23.Tables["1"].Rows[0]["net_total"].ToString());
                       // rtn.dataGridView1.DataSource = ds23.Tables["0"];
                        rtn.ShowDialog();

                        for (int i = 0; i < rtn.dataGridView1.Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(rtn.dataGridView1.Rows[i].Cells[17].Value) && !rtn.dataGridView1.Rows[i].IsNewRow)
                            {
                                DataGridViewRow row = (DataGridViewRow)rtn.dataGridView1.Rows[i].Clone();
                                row.Cells[0].Value = rtn.dataGridView1.Rows[i].Cells[0].Value;
                                row.Cells[1].Value = rtn.dataGridView1.Rows[i].Cells[1].Value;
                                row.Cells[2].Value = rtn.dataGridView1.Rows[i].Cells[2].Value;
                                row.Cells[3].Value = rtn.dataGridView1.Rows[i].Cells[3].Value;
                                //  r.Cells[2] = dcombo;
                                //DataView dv1 = dtunits.DefaultView;
                                ////  dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                                //dv1.RowFilter = "unit_id in('" + ds23.Tables["0"].Rows[i][5] + "')";
                                //DataTable dtNew = dv1.ToTable();
                                //dcombo.DataSource = dtNew;
                                //dcombo.DisplayMember = "unit_name";
                                //dcombo.ValueMember = "unit_id";
                                //row.Cells[3] = dcombo;
                                //row.Cells[3].Value = ds23.Tables["0"].Rows[i][5];

                                ////  dataGridView1[2, r.Index] = dcombo;
                                //// dataGridView1[2, r.Index].Value = dtNew.Rows[0][1];
                                //// dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                                //dcombo.FlatStyle = FlatStyle.Flat;

                                row.Cells[9].Value = rtn.dataGridView1.Rows[i].Cells[9].Value;
                                row.Cells[10].Value = rtn.dataGridView1.Rows[i].Cells[10].Value;
                                row.Cells[11].Value = rtn.dataGridView1.Rows[i].Cells[11].Value;
                                row.Cells[13].Value = rtn.dataGridView1.Rows[i].Cells[13].Value;
                                row.Cells[14].Value = rtn.dataGridView1.Rows[i].Cells[14].Value;

                                //if (row.Cells[4].Value == null)
                                //{
                                row.Cells[4].Value = rtn.dataGridView1.Rows[i].Cells[4].Value;
                                //}
                                //if (row.Cells[5].Value == null)
                                //{
                                row.Cells[5].Value = rtn.dataGridView1.Rows[i].Cells[5].Value;
                                //}
                                //if (row.Cells[6].Value == null)
                                //{
                                row.Cells[6].Value = rtn.dataGridView1.Rows[i].Cells[6].Value;
                                //}
                                //  row.Cells[5].Value = ds23.Tables["0"].Rows[0][3];
                                row.Cells[7].Value = rtn.dataGridView1.Rows[i].Cells[7].Value;
                                row.Cells[8].Value = Convert.ToDouble(row.Cells[4].Value) * Convert.ToDouble(row.Cells[5].Value);

                                if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + rtn.dataGridView1.Rows[i].Cells[9].Value))
                                {
                                    pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + rtn.dataGridView1.Rows[i].Cells[9].Value);

                                }
                                else
                                {
                                    pictureBox1.Image = Properties.Resources.background_button;

                                }

                                //  DataRow[] dtrvat = dttax.Select("tax_id =" + row.Cells[11].Value + "");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";

                                //row.Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : (Convert.ToDouble(row.Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(row.Cells[9].Value)) * ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) - ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) * Convert.ToDouble(row.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                                row.Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(row.Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) - ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) * Convert.ToDouble(row.Cells[6].Value)) / 100) : (Convert.ToDouble(row.Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(row.Cells[9].Value)) * ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) - ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) * Convert.ToDouble(row.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();


                                dataGridView1.Rows.Add(row);

                        //DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                        ////  r.Cells[2] = dcombo;
                        //DataView dv1 = dtunits.DefaultView;
                        ////  dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                        //dv1.RowFilter = "unit_id in('" + row.Cells[3].Value + "')";
                        //DataTable dtNew = dv1.ToTable();
                        //dcombo.DataSource = dtNew;
                        //dcombo.DisplayMember = "unit_name";
                        //dcombo.ValueMember = "unit_id";
                        //row.Cells[3] = dcombo;
                        //row.Cells[3].Value = dtNew.Rows[0][0];

                        ////  dataGridView1[2, r.Index] = dcombo;
                        //// dataGridView1[2, r.Index].Value = dtNew.Rows[0][1];
                        //// dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                        //dcombo.FlatStyle = FlatStyle.Flat;

                                chk_qty_offer(row);

                                DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                                //  r.Cells[2] = dcombo;
                                DataView dv1 = dtunits.DefaultView;
                                //  dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                                dv1.RowFilter = "unit_id in('" + row.Cells[3].Value.ToString() + "')";
                                DataTable dtNew = dv1.ToTable();
                                dcombo.DataSource = dtNew;
                                dcombo.DisplayMember = "unit_name";
                                dcombo.ValueMember = "unit_id";

                                row.Cells[3] = dcombo;
                                row.Cells[3].Value = dtNew.Rows[0][0];

                                //  dataGridView1[2, r.Index] = dcombo;
                                // dataGridView1[2, r.Index].Value = dtNew.Rows[0][1];
                                // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                                dcombo.FlatStyle = FlatStyle.Flat;

                                
                                total();
                            }
                         
                            total();
                        }
                        txt_dscper.Text = ds23.Tables["1"].Rows[0]["dscper"].ToString();
                        // txt_desc.Text = ((Convert.ToDouble(ds23.Tables["1"].Rows[0]["dscper"]) * Convert.ToDouble(ds23.Tables["1"].Rows[0]["discount"])) / 100).ToString();
                        txt_desc.Text = (Math.Round(((Convert.ToDouble(txt_dscper.Text) * Convert.ToDouble(txt_total.Text)) / 100), 2)).ToString();

                        total();
                        // DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();

                        ////for (int i = 0; i < ds23.Tables["0"].Rows.Count; i++)
                        ////{
                        ////    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[i].Clone();
                        ////    row.Cells[0].Value = ds23.Tables["0"].Rows[i][4];
                        ////    row.Cells[1].Value = ds23.Tables["0"].Rows[i][0];
                        ////    row.Cells[2].Value = ds23.Tables["0"].Rows[i][1];
                        ////    row.Cells[3].Value = ds23.Tables["0"].Rows[i][5];

                        ////    row.Cells[9].Value = ds23.Tables["0"].Rows[i][11];
                        ////    row.Cells[10].Value = ds23.Tables["0"].Rows[i]["pkqty"];
                        ////    row.Cells[11].Value = ds23.Tables["0"].Rows[i]["i_tax"];
                        ////    row.Cells[13].Value = ds23.Tables["0"].Rows[i]["img"];
                        ////    row.Cells[14].Value = ds23.Tables["0"].Rows[i]["mp"];

                        ////    //if (row.Cells[4].Value == null)
                        ////    //{
                        ////    row.Cells[4].Value = ds23.Tables["0"].Rows[i]["qty"];
                        ////    //}
                        ////    //if (row.Cells[5].Value == null)
                        ////    //{
                        ////    row.Cells[5].Value = ds23.Tables["0"].Rows[i][3];
                        ////    //}
                        ////    //if (row.Cells[6].Value == null)
                        ////    //{
                        ////    row.Cells[6].Value = ds23.Tables["0"].Rows[i]["discpc"];
                        ////    //}
                        ////    //  row.Cells[5].Value = ds23.Tables["0"].Rows[0][3];
                        ////    row.Cells[7].Value = ds23.Tables["0"].Rows[i][2];
                        ////    row.Cells[8].Value = Convert.ToDouble(row.Cells[4].Value) * Convert.ToDouble(row.Cells[5].Value);

                        ////    if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + ds23.Tables[0].Rows[i][9]))
                        ////    {
                        ////        pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + ds23.Tables[0].Rows[i][9]);

                        ////    }
                        ////    else
                        ////    {
                        ////        pictureBox1.Image = Properties.Resources.background_button;

                        ////    }

                        ////    //  DataRow[] dtrvat = dttax.Select("tax_id =" + row.Cells[11].Value + "");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";

                        ////    //row.Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : (Convert.ToDouble(row.Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(row.Cells[9].Value)) * ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) - ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) * Convert.ToDouble(row.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                        ////    row.Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(row.Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) - ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) * Convert.ToDouble(row.Cells[6].Value)) / 100) : (Convert.ToDouble(row.Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(row.Cells[9].Value)) * ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) - ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) * Convert.ToDouble(row.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();


                        ////    dataGridView1.Rows.Add(row);

                        ////    chk_qty_offer(row);

                        ////    total();
                        ////}
                        txt_recv.Focus();
                    }
                    else
                    {
                        MessageBox.Show("لا يوجد فاتورة بهذا الرقم", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       // dataGridView1.Focus();
                    }

                    

                dataGridView1.Focus();
            }
        }
        DateTime lastTime=DateTime.Now;
        private void txt_invbar_KeyPress(object sender, KeyPressEventArgs e)
        {
            
           // string barcode = string.Empty;

            try
            {
               // barcode += e.KeyChar;

                if (lastTime > new DateTime())
                {
                    if (DateTime.Now.Subtract(lastTime).Milliseconds > 30)
                    {
                        //
                       // MessageBox.Show("false");
                       // txt_invbar.Text = "";
                        //f1 = false;
                    }
                    else
                    {
                        txt_invbar.Text = "";
                       // MessageBox.Show("ok");
                       // f1 = true;
                    }
                }

                lastTime = DateTime.Now;

                /*

                Test your Barcode, and if it matches your criteria then change your TextBox text

                TextBox1.Text = barcode;

                */
               // txt_invbar.Text = barcode;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (groupBox1.BackColor == Color.Orange)
                groupBox1.BackColor = ColorTranslator.FromHtml(BL.CLS_Session.cmp_color);
            else
                groupBox1.BackColor = Color.Orange;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void chk_agel_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_agel.Checked && !string.IsNullOrEmpty(txt_custno.Text))
            {
                txt_recv.Enabled = false;
                txt_cardpay.Enabled = false;
            }
            else
            {
                txt_recv.Enabled = true;
                txt_cardpay.Enabled = true;
            }
        }
        private void create_einv_p2(string nref)
        {
            DataTable dth = daml.SELECT_QUIRY_only_retrn_dt("select * from pos_hdr where ref=" + nref + "");
           // DataTable dthash = daml.SELECT_QUIRY_only_retrn_dt("select hash from pos_esend where ref=" + (Convert.ToInt32(nref) - 1) + "");
            DataTable dthash = daml.SELECT_QUIRY_only_retrn_dt("select [inv_hash],[zref]+1 from pos_hash");


            foreach (DataRow dtrh in dth.Rows)
            {
                UBLXML ubl = new UBLXML();
                Invoice inv = new Invoice();
                Result res = new Result();

                inv.ID = dtrh[2].ToString(); //"1230"; // مثال SME00010
                inv.UUID = Guid.NewGuid().ToString();
                inv.IssueDate = Convert.ToDateTime(dtrh[5]).ToString("yyyy-MM-dd", new CultureInfo("en-US", false));
                inv.IssueTime = Convert.ToDateTime(dtrh[5]).ToString("HH:mm:ss", new CultureInfo("en-US", false));
                //388 فاتورة  
                //383 اشعار مدين
                //381 اشعار دائن
                inv.invoiceTypeCode.id = 381;
                //inv.invoiceTypeCode.Name based on format NNPNESB
                //NN 01 فاتورة عادية
                //NN 02 فاتورة مبسطة
                //P فى حالة فاتورة لطرف ثالث نكتب 1 فى الحالة الاخرى نكتب 0
                //N فى حالة فاتورة اسمية نكتب 1 وفى الحالة الاخرى نكتب 0
                // E فى حالة فاتورة للصادرات نكتب 1 وفى الحالة الاخرى نكتب 0
                //S فى حالة فاتورة ملخصة نكتب 1 وفى الحالة الاخرى نكتب 0
                //B فى حالة فاتورة ذاتية نكتب 1
                //B فى حالة ان الفاتورة صادرات=1 لايمكن ان تكون الفاتورة ذاتية =1
                //
                inv.invoiceTypeCode.Name = "0200000";
                inv.DocumentCurrencyCode = "SAR";//العملة
                inv.TaxCurrencyCode = "SAR"; ////فى حالة الدولار لابد ان تكون عملة الضريبة بالريال السعودى
                // inv.CurrencyRate = decimal.Parse("3.75"); // قيمة الدولار مقابل الريال
                // فى حالة ان اشعار دائن او مدين فقط هانكتب رقم الفاتورة اللى اصدرنا الاشعار ليها
                ////inv.billingReference.invoiceDocumentReferences =dtrh[21].ToString();// "123654"; رقم فاتورة البيع في حال ارجاع الفاتورة 
                if (inv.invoiceTypeCode.id == 381)
                {
                    // فى حالة ان اشعار دائن او مدين فقط هانكتب رقم الفاتورة اللى اصدرنا الاشعار ليها
                    // in case of return sales invoice or debit notes we must mention the original sales invoice number
                    InvoiceDocumentReference invoiceDocumentReference = new InvoiceDocumentReference();
                    invoiceDocumentReference.ID = "Invoice Number: " + dtrh[21].ToString() + " "; // mandatory in case of return sales invoice or debit notes
                    inv.billingReference.invoiceDocumentReferences.Add(invoiceDocumentReference);
                }
                // هنا ممكن اضيف ال pih من قاعدة البيانات  
                if (dthash.Rows.Count > 0)
                    inv.AdditionalDocumentReferencePIH.EmbeddedDocumentBinaryObject = string.IsNullOrEmpty(dthash.Rows[0][0].ToString()) ? "NWZlY2ViNjZmZmM4NmYzOGQ5NTI3ODZjNmQ2OTZjNzljMmRiYzIzOWRkNGU5MWI0NjcyOWQ3M2EyN2ZiNTdlOQ==" : dthash.Rows[0][0].ToString();
                else
                {
                    inv.AdditionalDocumentReferencePIH.EmbeddedDocumentBinaryObject = "NWZlY2ViNjZmZmM4NmYzOGQ5NTI3ODZjNmQ2OTZjNzljMmRiYzIzOWRkNGU5MWI0NjcyOWQ3M2EyN2ZiNTdlOQ==";
                   // daml.Insert_Update_Delete_Only("INSERT [dbo].[pos_hash] ([brno], [slno], [ref], [contr], [type], [zref], [zuuid], [inv_hash]) VALUES (N'01', N'01', 0, 1, N'1', 1, NEWID(), N'NWZlY2ViNjZmZmM4NmYzOGQ5NTI3ODZjNmQ2OTZjNzljMmRiYzIzOWRkNGU5MWI0NjcyOWQ3M2EyN2ZiNTdlOQ==');", false);
                }

                    // قيمة عداد الفاتورة
                inv.AdditionalDocumentReferenceICV.UUID = string.IsNullOrEmpty(dthash.Rows[0][1].ToString()) ? 1 : Convert.ToInt32(dthash.Rows[0][1].ToString()); //Convert.ToInt32(dtrh[2].ToString()); // لابد ان يكون ارقام فقط Convert.ToInt32(dtrh[2].ToString()); // لابد ان يكون ارقام فقط
                //فى حالة فاتورة مبسطة وفاتورة ملخصة هانكتب تاريخ التسليم واخر تاريخ التسليم
                // inv.delivery.ActualDeliveryDate = "2022-10-22"; للضريبية فقط اجباري
                // inv.delivery.LatestDeliveryDate = "2022-10-23"; للضريبية فقط اختياري
                //
                //بيانات الدفع 
                string paymentcode = "10";
                if (!string.IsNullOrEmpty(paymentcode))
                {
                    PaymentMeans paymentMeans = new PaymentMeans();
                    paymentMeans.PaymentMeansCode = paymentcode; // optional for invoices - mandatory for return invoice - debit notes
                    if ( inv.invoiceTypeCode.id == 381)
                    {
                        paymentMeans.InstructionNote = "return items"; //the reason of return invoice - debit notes // manatory only for return invoice - debit notes 
                    }
                    inv.paymentmeans.Add(paymentMeans);
                }
                // اكواد معين
                // اختيارى كود الدفع
                //inv.paymentmeans.PaymentMeansCode = "42";//اختيارى
                //inv.paymentmeans.InstructionNote = "Payment Notes"; //اجبارى فى الاشعارات
                //inv.paymentmeans.payeefinancialaccount.ID = "";//اختيارى
                //inv.paymentmeans.payeefinancialaccount.paymentnote = "Payment by credit";//اختيارى

                /*
                //بيانات البائع 
                inv.SupplierParty.partyIdentification.ID = BL.CLS_Session.dtcomp.Rows[0]["ownr_mob"].ToString();// "123456"; // رقم السجل التجارى الخاض بالبائع
                inv.SupplierParty.partyIdentification.schemeID = "CRN"; //رقم السجل التجارى
                inv.SupplierParty.postalAddress.StreetName = BL.CLS_Session.dtcomp.Rows[0]["street"].ToString();// "streetnumber";// اجبارى
                inv.SupplierParty.postalAddress.AdditionalStreetName = "";// "ststtstst"; //اختيارى
                inv.SupplierParty.postalAddress.BuildingNumber = BL.CLS_Session.dtcomp.Rows[0]["bulding_no"].ToString();// "3724"; // اجبارى رقم المبنى
                //inv.SupplierParty.postalAddress.PlotIdentification = "9833";//اختيارى رقم القطعة
                inv.SupplierParty.postalAddress.CityName = BL.CLS_Session.dtcomp.Rows[0]["city"].ToString();// "gaddah"; //اسم المدينة
                inv.SupplierParty.postalAddress.PostalZone = BL.CLS_Session.dtcomp.Rows[0]["postal_code"].ToString();// "15385";//الرقم البريدي
                inv.SupplierParty.postalAddress.CountrySubentity = "";// BL.CLS_Session.dtcomp.Rows[0]["city"].ToString();// "makka";//اسم المحافظة او المدينة مثال (مكة) اختيارى
                inv.SupplierParty.postalAddress.CitySubdivisionName = BL.CLS_Session.dtcomp.Rows[0]["site_name"].ToString();// "flassk";// اسم المنطقة او الحى 
                inv.SupplierParty.postalAddress.country.IdentificationCode = "SA";
                inv.SupplierParty.partyLegalEntity.RegistrationName = BL.CLS_Session.cmp_name;// "على ابراهيم"; // اسم الشركة المسجل فى الهيئة
                inv.SupplierParty.partyTaxScheme.CompanyID = BL.CLS_Session.tax_no;// "300300868600003";// رقم التسجيل الضريبي
                */
                if (string.IsNullOrEmpty(BL.CLS_Session.dtcomp.Rows[0]["ownr_mob"].ToString()))
                { MessageBox.Show(" لا يوجد رقم التعريف الخاص بالبائع ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                else
                    inv.SupplierParty.partyIdentification.ID = BL.CLS_Session.dtcomp.Rows[0]["ownr_mob"].ToString();// "123456"; // رقم السجل التجارى الخاض بالبائع

                inv.SupplierParty.partyIdentification.schemeID = BL.CLS_Session.cmpschem;// "CRN"; //رقم السجل التجارى
                if (string.IsNullOrEmpty(BL.CLS_Session.dtcomp.Rows[0]["street"].ToString()))
                { MessageBox.Show(" لا يوجد اسم الشارع في العنوان الوطني الخاص بالبائع ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                else
                    inv.SupplierParty.postalAddress.StreetName = BL.CLS_Session.dtcomp.Rows[0]["street"].ToString();// "streetnumber";// اجبارى
                inv.SupplierParty.postalAddress.AdditionalStreetName = "";// "ststtstst"; //اختيارى

                if (string.IsNullOrEmpty(BL.CLS_Session.dtcomp.Rows[0]["bulding_no"].ToString()))
                { MessageBox.Show(" لا يوجد رقم المبنى في العنوان الوطني الخاص بالبائع ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                else
                    inv.SupplierParty.postalAddress.BuildingNumber = BL.CLS_Session.dtcomp.Rows[0]["bulding_no"].ToString();// "3724"; // اجبارى رقم المبنى
                //inv.SupplierParty.postalAddress.PlotIdentification = "9833";//اختيارى رقم القطعة
                if (string.IsNullOrEmpty(BL.CLS_Session.dtcomp.Rows[0]["city"].ToString()))
                { MessageBox.Show(" لا يوجد اسم المدينة في العنوان الوطني الخاص بالبائع ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                else
                    inv.SupplierParty.postalAddress.CityName = BL.CLS_Session.dtcomp.Rows[0]["city"].ToString();// "gaddah"; //اسم المدينة

                if (string.IsNullOrEmpty(BL.CLS_Session.dtcomp.Rows[0]["postal_code"].ToString()))
                { MessageBox.Show(" لا يوجد الرقم البريدي في العنوان الوطني الخاص بالبائع ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                else
                    inv.SupplierParty.postalAddress.PostalZone = BL.CLS_Session.dtcomp.Rows[0]["postal_code"].ToString();// "15385";//الرقم البريدي
                inv.SupplierParty.postalAddress.CountrySubentity = "";// BL.CLS_Session.dtcomp.Rows[0]["city"].ToString();// "makka";//اسم المحافظة او المدينة مثال (مكة) اختيارى

                if (string.IsNullOrEmpty(BL.CLS_Session.dtcomp.Rows[0]["site_name"].ToString()))
                { MessageBox.Show(" لا يوجد اسم المنطقة او الحى في العنوان الوطني الخاص بالبائع ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                else
                    inv.SupplierParty.postalAddress.CitySubdivisionName = BL.CLS_Session.dtcomp.Rows[0]["site_name"].ToString();// "flassk";// اسم المنطقة او الحى 
                inv.SupplierParty.postalAddress.country.IdentificationCode = "SA";
                inv.SupplierParty.partyLegalEntity.RegistrationName = BL.CLS_Session.cmp_name;// "على ابراهيم"; // اسم الشركة المسجل فى الهيئة

                if (string.IsNullOrEmpty(BL.CLS_Session.tax_no))
                { MessageBox.Show(" لا يوجد الرقم الضريبي الخاص بالبائع ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                else
                    inv.SupplierParty.partyTaxScheme.CompanyID = BL.CLS_Session.tax_no;// "300300868600003";// رقم التسجيل الضريبي

                ////inv.legalMonetaryTotal.PayableAmount = decimal.Parse(dtrh[14].ToString()) ;// اجمالي الفاتورة
                ////inv.legalMonetaryTotal.TaxInclusiveAmount = decimal.Parse(dtrh[14].ToString());
                ////inv.legalMonetaryTotal.TaxExclusiveAmount = decimal.Parse(dtrh[14].ToString()) - decimal.Parse(dtrh[17].ToString());
                ////inv.legalMonetaryTotal.TaxExclusiveAmount = decimal.Parse(dtrh[14].ToString()) - decimal.Parse(dtrh[17].ToString());

                // inv.legalMonetaryTotal.TaxInclusiveAmount = decimal.Parse(dtrh[14].ToString());
                ////if (decimal.Parse(dtrh[13].ToString()) > 0)
                ////{
                ////    //this code incase of there is a discount in invoice level 
                ////    AllowanceCharge allowance = new AllowanceCharge();
                ////    //ChargeIndicator = false means that this is discount
                ////    //ChargeIndicator = true means that this is charges(like cleaning service - transportation)
                ////    allowance.ChargeIndicator = false;// يعني خصم وليس رسوم
                ////    //write this lines in case you will make discount as percentage
                ////    allowance.MultiplierFactorNumeric = 0; //dscount percentage like 10
                ////    allowance.BaseAmount = 0; // the amount we will apply percentage on example (MultiplierFactorNumeric=10 ,BaseAmount=1000 then AllowanceAmount will be 100 SAR)

                ////    // in case we will make discount as Amount 
                ////    allowance.Amount = decimal.Parse(dtrh[13].ToString()); // 
                ////    allowance.AllowanceChargeReasonCode = ""; //discount or charge reason code
                ////    allowance.AllowanceChargeReason = "discount"; //discount or charge reson
                ////    allowance.taxCategory.ID = "S";// كود الضريبة tax code (S Z O E )
                ////    allowance.taxCategory.Percent = decimal.Parse(BL.CLS_Session.tax_per.ToString()); //;// نسبة الضريبة tax percentage (0 - 15 - 5 )
                ////    //فى حالة عندى اكثر من خصم بعمل loop على الاسطر السابقة
                ////    inv.allowanceCharges.Add(allowance);
                ////}
                ////if (decimal.Parse(dtrh[13].ToString()) > 0)
                ////{
                ////    //this code incase of there is a discount in invoice level 
                ////    AllowanceCharge allowance = new AllowanceCharge();
                ////    //ChargeIndicator = false means that this is discount
                ////    //ChargeIndicator = true means that this is charges(like cleaning service - transportation)
                ////    allowance.ChargeIndicator = false;// يعني خصم وليس رسوم
                ////    //write this lines in case you will make discount as percentage
                ////    allowance.MultiplierFactorNumeric = 0; //dscount percentage like 10
                ////    allowance.BaseAmount = 0; // the amount we will apply percentage on example (MultiplierFactorNumeric=10 ,BaseAmount=1000 then AllowanceAmount will be 100 SAR)

                ////    // in case we will make discount as Amount 
                ////    allowance.Amount = decimal.Parse(dtrh[13].ToString()); // 
                ////    allowance.AllowanceChargeReasonCode = ""; //discount or charge reason code
                ////    allowance.AllowanceChargeReason = "discount"; //discount or charge reson
                ////    allowance.taxCategory.ID = "Z";// كود الضريبة tax code (S Z O E )
                ////    allowance.taxCategory.Percent = 0; //;// نسبة الضريبة tax percentage (0 - 15 - 5 )
                ////    //فى حالة عندى اكثر من خصم بعمل loop على الاسطر السابقة
                ////    inv.allowanceCharges.Add(allowance);
                ////}

                DataTable dtdtl = daml.SELECT_QUIRY_only_retrn_dt("select a.*,b.tax_id,b.tax_percent,b.zatka_code,b.zatka_reason from pos_dtl a join taxs b on a.tax_id=b.tax_id where a.ref=" + dtrh[2] + "");
                // فى حالة فى اكتر من منتج فى الفاتورة هانعمل ليست من invoiceline مثال الكود التالى
                // for (int i = 1; i <= dtdtl.Rows.Count; i++)
                foreach (DataRow dtrd in dtdtl.Rows)
                {
                    InvoiceLine invline = new InvoiceLine();
                    invline.InvoiceQuantity = decimal.Parse(dtrd[9].ToString());
                    //invline.allowanceCharge.AllowanceChargeReason = "discount"; //سبب الخصم على مستوى المنتج
                    //invline.allowanceCharge.Amount = 10;//قيم الخصم

                    //if the price is including vat set EncludingVat=true;
                    //invline.price.EncludingVat = true;
                    invline.price.EncludingVat = false;

                    // invline.price.PriceAmount = decimal.Parse(dtrd[8].ToString());// سعر المنتج بعد الخصم 
                    invline.price.PriceAmount = decimal.Parse(dtrd[8].ToString()) - (decimal.Parse(dtrd[17].ToString()) / decimal.Parse(dtrd[9].ToString()));// سعر المنتج بعد الخصم 

                    invline.item.Name = dtrd[6].ToString();


                    //invline.price.allowanceCharge.AllowanceChargeReason = "discount"; //سبب الخصم على مستوى المنتج
                    //invline.price.allowanceCharge.Amount = 0;//قيم الخصم
                    if (decimal.Parse(dtrd[17].ToString()) == 0)
                    {
                        //item Tax code
                        invline.item.classifiedTaxCategory.ID = dtrd[22].ToString().Equals("VATEX-SA-35") ? "Z" : "O"; // كود الضريبة
                        //item Tax code
                        invline.taxTotal.TaxSubtotal.taxCategory.ID = dtrd[22].ToString().Equals("VATEX-SA-35") ? "Z" : "O"; // كود الضريبة
                        //item Tax Exemption Reason Code mentioned in zatca pdf page(32-33)
                        invline.taxTotal.TaxSubtotal.taxCategory.TaxExemptionReasonCode = dtrd[22].ToString();// "VATEX-SA-35"; // كود الضريبة
                        //item Tax Exemption Reason mentioned in zatca pdf page(32-33)
                        invline.taxTotal.TaxSubtotal.taxCategory.TaxExemptionReason = dtrd[23].ToString();// "Medicines and medical equipment"; // كود الضريبة
                        invline.item.classifiedTaxCategory.Percent = 0; // نسبة الضريبة
                        invline.taxTotal.TaxSubtotal.taxCategory.Percent = 0; // نسبة الضريبة

                    }
                    else
                    {
                        //item Tax code
                        invline.item.classifiedTaxCategory.ID = "S"; // كود الضريبة
                        //item Tax code
                        invline.taxTotal.TaxSubtotal.taxCategory.ID = "S"; // كود الضريبة
                        invline.item.classifiedTaxCategory.Percent = decimal.Parse(BL.CLS_Session.tax_per.ToString()); // نسبة الضريبة
                        invline.taxTotal.TaxSubtotal.taxCategory.Percent = decimal.Parse(BL.CLS_Session.tax_per.ToString()); // نسبة الضريبة
                    }
                    // invline.item.classifiedTaxCategory.ID = "S";// كود الضريبة
                    //// invline.item.classifiedTaxCategory.Percent = decimal.Parse(BL.CLS_Session.tax_per.ToString());// نسبة الضريبة

                    // invline.taxTotal.TaxSubtotal.taxCategory.ID = "S";//كود الضريبة
                    //// invline.taxTotal.TaxSubtotal.taxCategory.Percent = decimal.Parse(BL.CLS_Session.tax_per.ToString());//نسبة الضريبة
                    //invline.taxTotal.TaxSubtotal.taxCategory.TaxExemptionReason = "Private healthcare to citizen";
                    //invline.taxTotal.TaxSubtotal.taxCategory.TaxExemptionReasonCode = "VATEX-SA-HEA";
                    //invline.taxTotal.TaxSubtotal.taxCategory.TaxExemptionReason = "";
                    //invline.taxTotal.TaxSubtotal.taxCategory.TaxExemptionReasonCode = "";
                    if (decimal.Parse(dtrh[18].ToString()) > 0)
                    // if (0 > 1)
                    {
                        // incase there is discount in invoice line level
                        AllowanceCharge allowanceCharge = new AllowanceCharge();
                        // فى حالة الرسوم incase of charges
                        // allowanceCharge.ChargeIndicator = true;
                        // فى حالة الخصم incase of discount
                        allowanceCharge.ChargeIndicator = false;

                        allowanceCharge.AllowanceChargeReason = "discount"; // سبب الخصم على مستوى المنتج
                        // allowanceCharge.AllowanceChargeReasonCode = "90"; // سبب الخصم على مستوى المنتج
                        //allowanceCharge.Amount = decimal.Parse(dtrd[19].ToString()); // قيم الخصم discount amount or charge amount

                        // allowanceCharge.MultiplierFactorNumeric = decimal.Parse(dtrh[18].ToString()); //0;
                        allowanceCharge.MultiplierFactorNumeric = ((decimal.Parse(dtrd[19].ToString()) / ((decimal.Parse(dtrd[8].ToString()) * decimal.Parse(dtrd[9].ToString())) - decimal.Parse(dtrd[17].ToString()) - decimal.Parse(dtrd[19].ToString()))) * 100) + decimal.Parse(dtrh[18].ToString()); //0;
                        // allowanceCharge.BaseAmount = decimal.Parse(dtrd[8].ToString()) - (decimal.Parse(dtrd[17].ToString()) / decimal.Parse(dtrd[9].ToString())); //0;
                        // allowanceCharge.BaseAmount = (decimal.Parse(dtrd[8].ToString()) - (decimal.Parse(dtrd[17].ToString()) / decimal.Parse(dtrd[9].ToString()))) * decimal.Parse(dtrd[9].ToString()); //0;
                        allowanceCharge.BaseAmount = (decimal.Parse(dtrd[8].ToString()) * decimal.Parse(dtrd[9].ToString())) - decimal.Parse(dtrd[17].ToString()) - decimal.Parse(dtrd[19].ToString()); //0;
                        invline.allowanceCharges.Add(allowanceCharge);
                    }
                    else
                    {
                        // incase there is discount in invoice line level
                        AllowanceCharge allowanceCharge = new AllowanceCharge();
                        // فى حالة الرسوم incase of charges
                        // allowanceCharge.ChargeIndicator = true;
                        // فى حالة الخصم incase of discount
                        allowanceCharge.ChargeIndicator = false;

                        allowanceCharge.AllowanceChargeReason = "discount"; // سبب الخصم على مستوى المنتج
                        // allowanceCharge.AllowanceChargeReasonCode = "90"; // سبب الخصم على مستوى المنتج
                        allowanceCharge.Amount = decimal.Parse(dtrd[19].ToString()); // قيم الخصم discount amount or charge amount

                        allowanceCharge.MultiplierFactorNumeric = 0;
                        allowanceCharge.BaseAmount = 0;
                        invline.allowanceCharges.Add(allowanceCharge);
                    }
                    inv.InvoiceLines.Add(invline);
                }

                res = ubl.GenerateInvoiceXML(inv, Directory.GetCurrentDirectory());


                if (res.IsValid)
                {
                    daml.Insert_Update_Delete_Only(@"INSERT INTO [dbo].[pos_esend]([brno],[slno],[ref],[contr],[type],[uuid],[hash],[qr_code],[file_name],[encoded_xml],[z_ref]) VALUES ('" + dtrh[0] + "','" + dtrh[1] + "'," + dtrh[2] + "," + dtrh[3] + "," + dtrh[4] + ",'" + inv.UUID + "','" + res.InvoiceHash + "','" + res.QRCode + "','" + res.SingedXMLFileName + "','" + res.EncodedInvoice + "'," + dthash.Rows[0][1] + ") ", false);
                    daml.Insert_Update_Delete_Only(@"update pos_hash set [ref]=" + dtrh[2] + ",[zref]=" + dthash.Rows[0][1] + ",[inv_hash]='" + res.InvoiceHash + "' ", false);
                    //القيم التالية تحتاج ان تحفظها فى سطر الفاتورة فى قاعدة البيانات الخاصة بكم  كي تكون مرجع لكم لاحقاً
                    //MessageBox.Show(res.InvoiceHash);
                    //MessageBox.Show(res.SingedXML);
                    //MessageBox.Show(res.EncodedInvoice);
                    //MessageBox.Show(res.UUID);
                    //MessageBox.Show(res.QRCode);
                    //MessageBox.Show(res.PIH);
                    //MessageBox.Show(res.SingedXMLFileName);
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

    


