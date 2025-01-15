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
using MetroFramework;
using System.Globalization;
using System.Drawing.Printing;

namespace POS.Pos
{
    public partial class Resturant_ReSales : Form
    {
        Pos.Add_Tameen at=new Add_Tameen();
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();

        SqlConnection con2 = BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        string sl_brno, sl_id, sl_name, sl_password, slpmaxdisc, maxitmdsc, txt = "";
        bool sl_chgqty, sl_chgprice, sl_delline, sl_delinv, sl_return, sl_admin, sl_alowdisc, sl_alowexit, alwreprint, alwitmdsc, sl_inactive;
        int ccc, typeno = 1, maxref, cur_row;
        string cmp_name="",comp_id="",printer_nam,tamin,taminnot;
        string a_slctr, a_ref, a_type;
        DataTable dtt;
        // StringBuilder input = new StringBuilder();
        int flag = 1;
        string[] lines_prt = null;
        public Resturant_ReSales(string slctr, string atype, string aref)
        {
            InitializeComponent();
            a_ref = aref;
            a_type = atype;
            a_slctr = slctr;
        }

        private void creat_btns()
        {
            try
            {

                SqlDataAdapter da = new SqlDataAdapter("select " + (BL.CLS_Session.lang.Equals("E") ? " item_ename" : " item_name") + "  as item_name,item_no,item_image,item_price from items where item_group=" + ccc + " and inactive=0 order by cast(item_no as int)", con2);
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
                   // b.TextAlign = ContentAlignment.BottomCenter;
                  //  b.FlatStyle = FlatStyle.Standard;



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
                        ////////b.BackgroundImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + ds.Tables["0"].Rows[i][2]);
                           
                        ////////b.BackgroundImageLayout = ImageLayout.Stretch;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                   // b.Image = Properties.Resources.background_button;
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
                    // b.Image=
                    // b.AutoSize = true;
                    b.Click += new EventHandler(b2_click);
                    // listBox1.Controls.Add(b);
                    flowLayoutPanel1.Controls.Add(b);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                ////////SqlDataAdapter da23 = new SqlDataAdapter("select * from items where item_no='" + b.Name + "'", con2);

                ////////DataSet ds23 = new DataSet();
                ////////da23.Fill(ds23, "0");

                ////////if (ds23.Tables["0"].Rows.Count == 1)
                ////////{
                ////////    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                ////////    row.Cells[0].Value = ds23.Tables["0"].Rows[0][0];
                ////////    row.Cells[1].Value = ds23.Tables["0"].Rows[0][4];
                ////////    row.Cells[2].Value = ds23.Tables["0"].Rows[0][1];
                ////////    row.Cells[3].Value = ds23.Tables["0"].Rows[0][5];
                ////////    if (row.Cells[4].Value == null)
                ////////    {
                ////////        row.Cells[4].Value = 1;
                ////////    }
                ////////    row.Cells[5].Value = ds23.Tables["0"].Rows[0][3];
                ////////    row.Cells[6].Value = ds23.Tables["0"].Rows[0][2];


                ////////    dataGridView1.Rows.Add(row);
                ////////}
                SqlDataAdapter dachk = new SqlDataAdapter("select count(*) from  items_bc where item_no='" + b.Name + "'", con2);
                DataTable dtchk = new DataTable();
                dachk.Fill(dtchk);
                if (Convert.ToInt32(dtchk.Rows[0][0]) > 1)
                {
                    Pos.Size_Select fr = new Pos.Size_Select(b.Name.ToString());
                    // fr.MdiParent = this;
                    fr.ShowDialog();
                    // fr.val;
                    da23 = new SqlDataAdapter("select a.[item_no],([" + (BL.CLS_Session.lang.Equals("E") ? "item_ename" : "item_name") + "] +' - ' + '" + fr.nam + "')as item_name,[item_curcost],b.[price],[barcode],[item_unit],[item_req] from  items a join items_bc b on a.item_no=b.item_no where b.barcode='" + fr.bar + "'", con2);
                }
                else
                {
                    da23 = new SqlDataAdapter("select [item_no],[" + (BL.CLS_Session.lang.Equals("E") ? "item_ename" : "item_name") + "] as item_name,[item_curcost],[item_price],[item_barcode],[item_unit],[item_req] from  items where item_no='" + b.Name + "'", con2);
                }
                DataSet ds23 = new DataSet();
                da23.Fill(ds23, "0");

                if (ds23.Tables["0"].Rows.Count == 1)
                {
                    

                    DataRow NewRow = dtt.NewRow();

                    NewRow[1] = ds23.Tables["0"].Rows[0][0];
                    NewRow[0] = ds23.Tables["0"].Rows[0][4];
                    NewRow[2] = ds23.Tables["0"].Rows[0][1];
                    NewRow[3] = ds23.Tables["0"].Rows[0][5];
                    //if (row.Cells[4].Value == null)
                    //{
                    //    row.Cells[4].Value = 1;
                    //}
                    NewRow[4] = 1;
                    NewRow[5] = ds23.Tables["0"].Rows[0][3];
                    NewRow[6] = ds23.Tables["0"].Rows[0][2];
                    NewRow[7] = ds23.Tables["0"].Rows[0][6];


                    /*
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                    row.Cells[0].Value = ds23.Tables["0"].Rows[0][0];
                    row.Cells[1].Value = ds23.Tables["0"].Rows[0][4];
                    row.Cells[2].Value = ds23.Tables["0"].Rows[0][1];
                    row.Cells[3].Value = ds23.Tables["0"].Rows[0][5];
                    if (row.Cells[4].Value == null)
                    {
                        row.Cells[4].Value = 1;
                    }
                    row.Cells[5].Value = ds23.Tables["0"].Rows[0][3];
                    row.Cells[6].Value = ds23.Tables["0"].Rows[0][2];
                  //  row.Cells[7].Value = ds23.Tables["0"].Rows[0][6];

                    //dataGridView1.Rows.Add(row);
                     */
                    dtt.Rows.Add(NewRow);

                    dataGridView1.DataSource = dtt;
                   // dataGridView1.Rows.Clear();
                   // dataGridView1.DataSource = dtt;
                    
                }

                dataGridView1.ClearSelection();//If you want
                int nRowIndex = dataGridView1.Rows.Count - 2;
                // int nColumnIndex = 3;
                hight();
                // dataGridView1.Rows[nRowIndex].Selected = true;
                // dataGridView1.Rows[nRowIndex].Cells[nColumnIndex].Selected = true;

                //In case if you want to scroll down as well.
                dataGridView1.FirstDisplayedScrollingRowIndex = nRowIndex;
                // dataGridView1.Rows[nRowIndex].Selected = true;
                total();

                

                cur_row = dataGridView1.Rows.Count - 2;

                dataGridView1.Rows[cur_row].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[cur_row].Cells[0];
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.NewRowIndex - 1].Cells[4];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        void b2_click(object sender, EventArgs e)
        {
            Button c = sender as Button;
            ccc = Convert.ToInt32(c.Name);

            creat_btns();


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

        private void Resturant_sales_Load(object sender, EventArgs e)
        {
            
            this.BackColor = ColorTranslator.FromHtml(BL.CLS_Session.cmp_color);
            lines_prt = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\printers.txt");

            PrinterSettings settings = new PrinterSettings();
            printer_nam = settings.PrinterName;

            DataTable dt3 = daml.SELECT_QUIRY_only_retrn_dt("select c_slno,contr_id from contrs");
            BL.CLS_Session.slno = dt3.Rows[0][0].ToString();
           // BL.CLS_Session.ctrno = dt3.Rows[0][1].ToString();
            lblcashir.Text = BL.CLS_Session.contr_id;

            if (con2.State == ConnectionState.Closed)
            {
                con2.Open();
            }
            else
            {
                // con2.Open();
            }
            //  creat_btns();
          //  var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\temp.txt");

           // lbluser.Text = lines.First().ToString();
          //  pass.Text = lines.Skip(1).First().ToString();
          //  lblcashir.Text = lines.Skip(2).First().ToString();
          //  cmp_name = lines.Skip(4).First().ToString();
          //  comp_id = lines.Skip(8).First().ToString();
            cmp_name = BL.CLS_Session.cmp_name;
            lbluser.Text = BL.CLS_Session.UserName;

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
                MessageBox.Show(ex.Message);
            }

          //  DataTable dt2 = daml.SELECT_QUIRY("select isnull(max(ref),0) as eee from pos_hdr");
          //  lblref.Text = dt2.Rows[0][0].ToString();
            dataGridView1.Columns[0].ReadOnly = true; dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true; dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[6].ReadOnly = true; dataGridView1.Columns[7].ReadOnly = true; dataGridView1.Columns[8].ReadOnly = true;
            creat_groups();

            Fill_Data(a_slctr, a_type, a_ref);

            this.WindowState = FormWindowState.Maximized;
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                if (selectedIndex > -1)
                {
                    dataGridView1.Rows.RemoveAt(selectedIndex);
                    dataGridView1.Refresh();
                    // dataGridView1.CurrentCell = dataGridView1.Rows[selectedIndex+1].Cells[0];
                    // if needed
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            total();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                // int selectedIndex = dataGridView1.CurrentRow.Index;

                if (selectedIndex > -1 && dataGridView1.Rows[selectedIndex].Cells[0].Value != null)
                {
                    // dataGridView1.Rows.RemoveAt(selectedIndex);
                    // dataGridView1.Refresh();
                    int a = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells[4].Value);
                    a = a + 1;
                    dataGridView1.Rows[selectedIndex].Cells[4].Value = a.ToString();
                    dataGridView1.Rows[selectedIndex].Cells[8].Value = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[selectedIndex].Cells[5].Value);
                    // if needed
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            total();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                if (selectedIndex > -1 && dataGridView1.Rows[selectedIndex].Cells[0].Value != null)
                {
                    // dataGridView1.Rows.RemoveAt(selectedIndex);
                    // dataGridView1.Refresh();
                    int a = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells[4].Value);
                    if (a > 1)
                    {
                        a = a - 1;
                        dataGridView1.Rows[selectedIndex].Cells[4].Value = a.ToString();
                        dataGridView1.Rows[selectedIndex].Cells[8].Value = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[selectedIndex].Cells[5].Value);
                    }
                    else
                    {
                        button1_Click(sender, e);
                    }
                    // if needed
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            if (flag == 2)
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
                string per = (100 * (Convert.ToDouble(txt_desc.Text)) / (Convert.ToDouble(label1.Text) + Convert.ToDouble(txt_desc.Text))).ToString();

                //  if ((Convert.ToDouble(per) > Convert.ToDouble(BL.CLS_Session.inv_dsc)))
                if ((Convert.ToDouble(per) > Convert.ToDouble(slpmaxdisc)))
                {
                    // MessageBox.Show("تجاوزت الخصم المسموح لك");
                    MessageBox.Show("تجاوزت الخصم المسموح لك", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_desc.Text = "0";
                   // txt_dscper.Text = "0";
                    txt_desc.SelectAll();
                    return;
                    // txt_desper.Text = "0";
                }


                double sum = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                    dataGridView1.Rows[i].Cells[8].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value);
                }
               // label1.Text = sum.ToString();
                label1.Text = BL.CLS_Session.isshamltax.Equals("1") ? ((Convert.ToDouble(sum.ToString()) + Convert.ToDouble(0)) - Convert.ToDouble(txt_desc.Text)).ToString() : (Convert.ToDouble(sum.ToString()) - Convert.ToDouble(txt_desc.Text)).ToString();
                
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    textBox1.Text = "";
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
                double remain = Convert.ToDouble(label1.Text) - pay;

                if (remain > 0)
                {


                    label5.Text = remain.ToString() + "   " + "المتبقي على الزبون";
                    // label4.BackColor = Color.Pink;
                    label5.BackColor = Color.Pink;
                }
                else
                {
                    // label4.Text = "";
                    label5.Text = remain.ToString() + "   " + "المتبقي للزبون";
                    // label4.BackColor = Color.GreenYellow;
                    label5.BackColor = Color.GreenYellow;
                }
            }
            else
            {
                //  label4.Text = "المتبقي على الزبون";
                label5.Text = label1.Text + "   " + "المتبقي على الزبون";
                // label4.BackColor = Color.Pink;
                label5.BackColor = Color.Pink;
            }







        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {

        }

        private void SavePrint_Click(object sender, EventArgs e)
        {
            total();
            total();
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
                if (textBox1.Text != "")
                {
                    if (Convert.ToDouble(textBox1.Text) >= Convert.ToDouble(label1.Text))
                    {
                        int toup = Convert.ToInt32(tet_ref.Text);
                        // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.pos_hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
                        using (SqlCommand cmd1 = new SqlCommand("delete from pos_dtl where ref=" + toup + " and contr=" + (txt_contr.Text.Equals("") ? BL.CLS_Session.ctrno : txt_contr.Text) + "", con2))
                        {

                            //    cmd1.Parameters.AddWithValue("@a0", 0);
                            //    cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                            //    // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                            //    cmd1.Parameters.AddWithValue("@a2", Convert.ToInt32(label1.Text));
                            //    cmd1.Parameters.AddWithValue("@a3", Convert.ToInt32(label9.Text));
                            //    cmd1.Parameters.AddWithValue("@a4", Convert.ToInt32(textBox1.Text));
                            //    cmd1.Parameters.AddWithValue("@a5", Convert.ToInt32(textBox2.Text));
                            //    cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                            //    cmd1.Parameters.AddWithValue("@a7", lbluser.Text);
                            cmd1.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd2 = new SqlCommand("update pos_hdr set type=@type,total=@total,count=@count,total_cost=@cust,payed=@payed,cust_no=@cust_no,discount=@dis,net_total=@net,tax_amt=@ta,card_type=@ct,card_amt=@cm,note=@not,mobile=@mob where ref=" + toup + " and contr=" + (txt_contr.Text.Equals("") ? BL.CLS_Session.ctrno : txt_contr.Text) + "", con2))
                        {

                            //    cmd1.Parameters.AddWithValue("@a0", 0);
                            //    cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                            //    // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                            cmd2.Parameters.AddWithValue("@type", (chk_agel.Checked && !string.IsNullOrEmpty(txt_custno.Text)) ? 3 : typeno);
                            cmd2.Parameters.AddWithValue("@total", Convert.ToDouble(label1.Text) + Convert.ToDouble(txt_desc.Text));
                            cmd2.Parameters.AddWithValue("@count", Convert.ToDouble(label9.Text));
                            cmd2.Parameters.AddWithValue("@cust", Convert.ToDouble(textBox2.Text));
                            cmd2.Parameters.AddWithValue("@payed", (chk_iscard.Checked ? 0 :Convert.ToDouble(textBox1.Text)));
                            cmd2.Parameters.AddWithValue("@cust_no", string.IsNullOrEmpty(txt_custno.Text) ? 0 : Convert.ToInt32(txt_custno.Text));//Convert.ToInt32(lbl_cust_no.Text));
                            cmd2.Parameters.AddWithValue("@dis", txt_desc.Text);
                            cmd2.Parameters.AddWithValue("@net", Convert.ToDouble(label1.Text));
                          //  cmd2.Parameters.AddWithValue("@ta", string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : Math.Round(((Convert.ToDouble(label1.Text) - Convert.ToDouble(string.IsNullOrEmpty(txt_desc.Text) ? "0" : txt_desc.Text)) * Convert.ToDouble(BL.CLS_Session.tax_per) / (100 + Convert.ToDouble(BL.CLS_Session.tax_per))),4));
                            cmd2.Parameters.AddWithValue("@ta", string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : Math.Round(((Convert.ToDouble(label1.Text) - Convert.ToDouble(string.IsNullOrEmpty("0") ? "0" : "0")) * Convert.ToDouble(BL.CLS_Session.tax_per) / (100 + Convert.ToDouble(BL.CLS_Session.tax_per))), 4));
                            //    cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                            //    cmd1.Parameters.AddWithValue("@a7", lbluser.Text);
                            cmd2.Parameters.AddWithValue("@ct", (chk_iscard.Checked ? "1" : "0"));
                            cmd2.Parameters.AddWithValue("@cm", (chk_iscard.Checked ? textBox1.Text : "0"));
                            cmd2.Parameters.AddWithValue("@not", txt_custnam.Text);
                            cmd2.Parameters.AddWithValue("@mob", txt_temp.Text);
                            cmd2.ExecuteNonQuery();
                        }

                        if (1==1)
                        {
                            DataTable dtb = daml.SELECT_QUIRY_only_retrn_dt("SELECT * FROM pos_hdr_r where dref=" + toup + " and dcontr=" + (txt_contr.Text.Equals("") ? BL.CLS_Session.ctrno : txt_contr.Text) + "");
                            if(dtb.Rows.Count>0)
                            {
                                using (SqlCommand cmd1 = new SqlCommand("update pos_hdr_r set dtype=@tb, driver_id=@a1,driver_nam=@a2,dpayed=@a3,dnote=@a4,dtameen=@a5,tameen_note=@a6 where dref=" + toup + " and dcontr=" + (txt_contr.Text.Equals("") ? BL.CLS_Session.ctrno : txt_contr.Text) + "", con2))
                                {
                                    cmd1.Parameters.AddWithValue("@tb", (chk_agel.Checked && !string.IsNullOrEmpty(txt_custno.Text)) ? 3 : typeno);
                                    cmd1.Parameters.AddWithValue("@a1", txt_driver.Text); //ToString("yyyy-MM-dd");
                                    // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                                    cmd1.Parameters.AddWithValue("@a2", txt_drivernam.Text);
                                    cmd1.Parameters.AddWithValue("@a3", chk_payed.Checked? "1" : "0");
                                    cmd1.Parameters.AddWithValue("@a4", txt);
                                    cmd1.Parameters.AddWithValue("@a5", string.IsNullOrEmpty(at.txt_tameemamt.Text) ? "0" : at.txt_tameemamt.Text);
                                    cmd1.Parameters.AddWithValue("@a6", at.txt_tameennot.Text);

                                    cmd1.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                using (SqlCommand cmd1 = new SqlCommand("INSERT INTO pos_hdr_r(dbrno,dslno,dref,dcontr,dtype,driver_id,driver_nam,dpayed,dnote,dtameen,tameen_note) VALUES(@br,@sl,@rf,@ctr,@a0,@a1,@a2,@a3,@a4,@a5,@a6)", con2))
                                {
                                    cmd1.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                                    cmd1.Parameters.AddWithValue("@sl", BL.CLS_Session.slno);
                                    cmd1.Parameters.AddWithValue("@rf", toup);
                                    cmd1.Parameters.AddWithValue("@ctr", (txt_contr.Text.Equals("") ? BL.CLS_Session.ctrno : txt_contr.Text));
                                    cmd1.Parameters.AddWithValue("@a0", (chk_agel.Checked && !string.IsNullOrEmpty(txt_custno.Text)) ? 3 : typeno);

                                    cmd1.Parameters.AddWithValue("@a1", txt_driver.Text); //ToString("yyyy-MM-dd");
                                    // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                                    cmd1.Parameters.AddWithValue("@a2", txt_drivernam.Text);
                                    cmd1.Parameters.AddWithValue("@a3", chk_payed.Checked ? "1" : "0");
                                    cmd1.Parameters.AddWithValue("@a4", txt);
                                    cmd1.Parameters.AddWithValue("@a5", string.IsNullOrEmpty(at.txt_tameemamt.Text) ? "0" : at.txt_tameemamt.Text);
                                    cmd1.Parameters.AddWithValue("@a6", at.txt_tameennot.Text);
                                    cmd1.ExecuteNonQuery();
                                }
                            }
                        }

                        int srno = 1;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            // SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from DB.dbo.pos_hdr", con2);
                            //SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from pos_hdr", con2);
                            //DataSet dss = new DataSet();
                            //daa.Fill(dss, "0");
                           
                            if (!row.IsNewRow && row.Cells[0].Value != null || !row.IsNewRow && row.Cells[4].Value != null || !row.IsNewRow && row.Cells[5].Value != null)
                            {
                                // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.pos_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                                using (SqlCommand cmd = new SqlCommand("insert into pos_dtl(brno,slno,ref,contr,type, barcode, name, unit, price, qty, cost,is_req,itemno,srno,tax_amt) values(@br,@sl,@r1,@ctr,@r0,@c1,@c2,@c3,@c4,@c5,@c6,@c8,@c9,@sn,@ta) ", con2))
                                {
                                    cmd.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                                    cmd.Parameters.AddWithValue("@sl", BL.CLS_Session.slno);
                                    //cmd1.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);
                                    cmd.Parameters.AddWithValue("@r1", toup);
                                    cmd.Parameters.AddWithValue("@ctr", (txt_contr.Text.Equals("") ? BL.CLS_Session.ctrno : txt_contr.Text));
                                    cmd.Parameters.AddWithValue("@r0", (chk_agel.Checked && !string.IsNullOrEmpty(txt_custno.Text)) ? 3 : typeno);
                                    cmd.Parameters.AddWithValue("@c1", row.Cells[0].Value);
                                    cmd.Parameters.AddWithValue("@c2", row.Cells[2].Value.ToString().Length > 100 ? row.Cells[2].Value.ToString().Substring(0, 100) : row.Cells[2].Value);
                                    cmd.Parameters.AddWithValue("@c3", row.Cells[3].Value);
                                    cmd.Parameters.AddWithValue("@c4", row.Cells[5].Value);
                                    cmd.Parameters.AddWithValue("@c5", row.Cells[4].Value);
                                    cmd.Parameters.AddWithValue("@c6", row.Cells[6].Value);
                                  //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                                  //  cmd.Parameters.AddWithValue("@c00", comp_id);
                                    cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value);
                                    cmd.Parameters.AddWithValue("@c9", row.Cells[1].Value);
                                    cmd.Parameters.AddWithValue("@sn", srno);
                                    cmd.Parameters.AddWithValue("@ta", string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 :Math.Round((Convert.ToDouble(row.Cells[8].Value) * Convert.ToDouble(BL.CLS_Session.tax_per) / (100 + Convert.ToDouble(BL.CLS_Session.tax_per))),4));
                                    cmd.ExecuteNonQuery();

                                    //  lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);

                                    // dataGridView1.Rows.Clear();
                                }
                                srno++;
                            }
                           
                        }

                        // MessageBox.Show(label5.Text);
                        total();
                        textBox1.Text = "";
                        // dataGridView1.Rows.Add(100);
                        // dataGridView1.TabIndex = 0;
                        // dataGridView1.Focus();
                        // textBox1.Text = "";
                        // textBox1.BackColor = Color.White;

                        //  Report1 report1 = new Report1();
                        //    report1.Show();

                        DialogResult result = MessageBox.Show("تم حفظ الفاتورة ....... هل تريد طباعة الفاتورة؟", "تاكيد", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                        if (result == DialogResult.Yes)
                        {
                            Print_btn_Click(sender, e);
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
                        if (lbl_cust_name.Text != "." && Convert.ToDouble(textBox1.Text) >= 0)
                        {
                            int toup = Convert.ToInt32(tet_ref.Text);
                            // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.pos_hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
                            using (SqlCommand cmd1 = new SqlCommand("delete from pos_dtl where ref=" + toup+ " and contr=" + (txt_contr.Text.Equals("") ? BL.CLS_Session.ctrno : txt_contr.Text) + "", con2))
                            {

                                //    cmd1.Parameters.AddWithValue("@a0", 0);
                                //    cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                                //    // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                                //    cmd1.Parameters.AddWithValue("@a2", Convert.ToInt32(label1.Text));
                                //    cmd1.Parameters.AddWithValue("@a3", Convert.ToInt32(label9.Text));
                                //    cmd1.Parameters.AddWithValue("@a4", Convert.ToInt32(textBox1.Text));
                                //    cmd1.Parameters.AddWithValue("@a5", Convert.ToInt32(textBox2.Text));
                                //    cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                                //    cmd1.Parameters.AddWithValue("@a7", lbluser.Text);
                                cmd1.ExecuteNonQuery();
                            }
                            using (SqlCommand cmd2 = new SqlCommand("update pos_hdr set type=@type,total=@total,count=@count,total_cost=@cust,payed=@payed,cust_no=@cust_no where ref=" + toup+ " and contr=" + (txt_contr.Text.Equals("") ? BL.CLS_Session.ctrno : txt_contr.Text) + "", con2))
                            {

                                //    cmd1.Parameters.AddWithValue("@a0", 0);
                                //    cmd1.Parameters.AddWithValue("@a1", DateTime.Now); //ToString("yyyy-MM-dd");
                                //    // cmd1.Parameters.AddWithValue("@a1", DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss")); //ToString("yyyy-MM-dd");
                                cmd2.Parameters.AddWithValue("@type", (chk_agel.Checked && !string.IsNullOrEmpty(txt_custno.Text)) ? 3 : typeno);
                                cmd2.Parameters.AddWithValue("@total", Convert.ToDouble(label1.Text));
                                cmd2.Parameters.AddWithValue("@count", Convert.ToDouble(label9.Text));
                                cmd2.Parameters.AddWithValue("@cust", Convert.ToDouble(textBox2.Text));
                                cmd2.Parameters.AddWithValue("@payed", Convert.ToDouble(textBox1.Text));
                                cmd2.Parameters.AddWithValue("@cust_no", Convert.ToInt32(lbl_cust_no.Text));

                                //    cmd1.Parameters.AddWithValue("@a6", Convert.ToInt32(lblcashir.Text));
                                //    cmd1.Parameters.AddWithValue("@a7", lbluser.Text);
                                cmd2.ExecuteNonQuery();
                            }
                            int srno = 1;
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                // SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from DB.dbo.pos_hdr", con2);
                                //SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from pos_hdr", con2);
                                //DataSet dss = new DataSet();
                                //daa.Fill(dss, "0");

                              
                               if (!row.IsNewRow && row.Cells[0].Value != null)
                               {
                                   // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.pos_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                                   using (SqlCommand cmd = new SqlCommand("insert into pos_dtl(brno,slno,ref,contr,type, barcode, name, unit, price, qty, cost,is_req,itemno,srno) values(@br,@sl,@r1,@ctr,@r0,@c1,@c2,@c3,@c4,@c5,@c6,@c8,@c9,@sn) ", con2))
                                   {
                                       cmd.Parameters.AddWithValue("@br", BL.CLS_Session.brno);
                                       cmd.Parameters.AddWithValue("@sl", BL.CLS_Session.slno);
                                       //cmd1.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);
                                       cmd.Parameters.AddWithValue("@r1", toup);
                                       cmd.Parameters.AddWithValue("@ctr", BL.CLS_Session.ctrno);
                                       cmd.Parameters.AddWithValue("@r0", (chk_agel.Checked && !string.IsNullOrEmpty(txt_custno.Text)) ? 3 : typeno);
                                       cmd.Parameters.AddWithValue("@c1", row.Cells[0].Value);
                                       cmd.Parameters.AddWithValue("@c2", row.Cells[2].Value);
                                       cmd.Parameters.AddWithValue("@c3", row.Cells[3].Value);
                                       cmd.Parameters.AddWithValue("@c4", row.Cells[5].Value);
                                       cmd.Parameters.AddWithValue("@c5", row.Cells[4].Value);
                                       cmd.Parameters.AddWithValue("@c6", row.Cells[6].Value);
                                       //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                                       //  cmd.Parameters.AddWithValue("@c00", comp_id);
                                       cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value);
                                       cmd.Parameters.AddWithValue("@c9", row.Cells[1].Value);
                                       cmd.Parameters.AddWithValue("@sn", srno);
                                       cmd.ExecuteNonQuery();

                                       //  lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);

                                       // dataGridView1.Rows.Clear();
                                   }
                                   srno++;
                               }
                                
                            }

                            // MessageBox.Show(label5.Text);
                            total();
                            textBox1.Text = "";
                            // dataGridView1.Rows.Add(100);
                            // dataGridView1.TabIndex = 0;
                            // dataGridView1.Focus();
                            // textBox1.Text = "";
                            // textBox1.BackColor = Color.White;

                            //  Report1 report1 = new Report1();
                            //    report1.Show();

                            DialogResult result = MessageBox.Show("تم حفظ الفاتورة ....... هل تريد طباعة الفاتورة؟", "تاكيد", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                            if (result == DialogResult.Yes)
                            {
                                Print_btn_Click(sender, e);
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
                            MessageBox.Show("يجب دفع المبلغ كاملا", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // textBox1.Text = "";
                            //textBox1.Focus();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("يرجى ادخال مبلغ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //textBox1.Text = "";
                    //textBox1.Focus();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void lbl_getref_Click(object sender, EventArgs e)
        {
            if (tet_ref.Text != "")
            {
                maxref = Convert.ToInt32(tet_ref.Text);
                SqlDataAdapter order = new SqlDataAdapter("select a.*,b.* from pos_hdr a left join pos_hdr_r b on  a.[brno]=b.[dbrno] and a.[slno]=b.[dslno] and a.[ref]=b.[dref] and a.[contr] = b.[dcontr] where a.ref=" + maxref + " and a.contr=" + (txt_contr.Text.Equals("")? BL.CLS_Session.ctrno : txt_contr.Text) + "", con2);
                // SqlDataAdapter pos_dtl = new SqlDataAdapter("SELECT pos_dtl.barcode as a,items.item_no as b,pos_dtl.name as c,pos_dtl.unit as d,pos_dtl.qty as e,pos_dtl.price as f,pos_dtl.cost from pos_dtl,items where item_barcode=pos_dtl.barcode and pos_dtl.ref=" + Convert.ToInt32(tet_ref.Text), con2);
                SqlDataAdapter pos_dtl = new SqlDataAdapter("SELECT a.barcode as a,b.item_no as b,a.name as c,a.unit as d,a.qty as e,a.price as f,a.cost as g,a.is_req as h,(a.qty * a.price) as i from pos_dtl a,items_bc b where ltrim(rtrim(a.barcode))=ltrim(rtrim(b.barcode)) and a.ref=" + Convert.ToInt32(tet_ref.Text) + " and a.contr=" + (txt_contr.Text.Equals("") ? BL.CLS_Session.ctrno : txt_contr.Text) + " and b.sl_no=a.slno order by a.srno", con2);

                DataSet1 ds = new DataSet1();

                order.Fill(ds, "0");
                pos_dtl.Fill(ds, "1");

                if (ds.Tables["0"].Rows.Count != 0)
                {
                    // string xxx = dt.ToString("yyyy-MM-dd", new CultureInfo("en-US",false));
                    lbldate.Text = (Convert.ToDateTime(ds.Tables["0"].Rows[0][5])).ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US", false));
                    lblref.Text = ds.Tables["0"].Rows[0][2].ToString();
                    lbluser.Text = ds.Tables["0"].Rows[0][10].ToString();
                    lblcashir.Text = ds.Tables["0"].Rows[0][3].ToString();

                    txt_custno.Text = ds.Tables["0"].Rows[0]["cust_no"].ToString().Equals("0") ? "" : ds.Tables["0"].Rows[0]["cust_no"].ToString();
                    txt_custnam.Text = ds.Tables["0"].Rows[0]["note"].ToString();
                    txt_temp.Text = ds.Tables["0"].Rows[0]["mobile"].ToString();
                    // txt_custnam.Text = ds.Tables["0"].Rows[0][""].ToString();
                    txt_contr.Text = ds.Tables["0"].Rows[0]["contr"].ToString();
                    txt_driver.Text = ds.Tables["0"].Rows[0]["driver_id"].ToString();
                    txt_drivernam.Text = ds.Tables["0"].Rows[0]["driver_nam"].ToString();
                    txt = ds.Tables["0"].Rows[0]["dnote"].ToString();
                    chk_payed.Checked = string.IsNullOrEmpty(ds.Tables["0"].Rows[0]["dpayed"].ToString()) ? false : Convert.ToBoolean(ds.Tables["0"].Rows[0]["dpayed"]);
                    chk_agel.Checked = Convert.ToInt32(ds.Tables["0"].Rows[0][4].ToString()) == 3 ? true : false;
                   // chk_agel.Enabled = Convert.ToInt32(ds.Tables["0"].Rows[0][4].ToString()) == 0 ? true : false;
                    at.txt_tameemamt.Text = ds.Tables["0"].Rows[0]["dtameen"].ToString();
                    at.txt_tameennot.Text = ds.Tables["0"].Rows[0]["tameen_note"].ToString();

                    tamin = ds.Tables["0"].Rows[0]["dtameen"].ToString();
                    taminnot = ds.Tables["0"].Rows[0]["tameen_note"].ToString();
                    txt_desc.Text= ds.Tables["0"].Rows[0]["discount"].ToString(); 

                    if (ds.Tables["0"].Rows[0][4].ToString() == "1")
                    {
                        button5_Click(sender, e);
                    }
                    else
                    {
                        if (ds.Tables["0"].Rows[0][4].ToString() == "4")
                        {
                            button6_Click(sender, e);
                        }
                        else
                        {
                            if (ds.Tables["0"].Rows[0][4].ToString() == "5")
                            {
                                //// button4_Click(sender, e);
                                //SqlDataAdapter cust = new SqlDataAdapter("select first_name,ID_CUSTOMER from customers where ID_CUSTOMER=" + ds.Tables["0"].Rows[0][11], con2);
                                //DataTable dt = new DataTable();
                                //cust.Fill(dt);
                                //lbl_cust_name.Text = dt.Rows[0][1].ToString();
                                //lbl_cust_no.Text = dt.Rows[0][2].ToString();

                                //typeno = 4;

                                //button4.BackColor = Color.GreenYellow;
                                ////button5.BackColor = default(Color);
                                //// button6.BackColor = default(Color);
                                ////  button5.BackColor = SystemColors.Control;
                                //// button6.BackColor = SystemColors.Control;
                                //button5.UseVisualStyleBackColor = true;
                                //button6.UseVisualStyleBackColor = true;
                                button22_Click(sender, e);
                            }
                        }
                    }

                    dataGridView1.DataSource = ds.Tables["1"];
                    dtt = ds.Tables["1"];
                    total();
                    textBox1.Text = ds.Tables["0"].Rows[0][8].ToString();
                    if (Convert.ToDouble(ds.Tables["0"].Rows[0]["card_amt"]) > 0)
                        chk_iscard.Checked = true;
                    else
                        chk_iscard.Checked = false;

                    hight();
                    txt_contr.Enabled = false;
                }
                else
                {
                    MessageBox.Show("لا توجد فاتورة بهذ الرقم","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                SqlDataAdapter max = new SqlDataAdapter("select max(ref) from pos_hdr where contr=" + (txt_contr.Text.Equals("") ? BL.CLS_Session.ctrno : txt_contr.Text) + "", con2);
                DataTable dtm = new DataTable();
                max.Fill(dtm);
                maxref = Convert.ToInt32(dtm.Rows[0][0]);
                tet_ref.Text = maxref.ToString();
                Fill_Data(BL.CLS_Session.slno, "5", maxref.ToString());
                
            }
            hight();
        }

        private void Fill_Data(string slctr, string atype, string aref)
        {
            try
            {
                if (!string.IsNullOrEmpty(slctr) && !string.IsNullOrEmpty(atype) && !string.IsNullOrEmpty(aref))
                {
                    tet_ref.Text = aref;
                    // MessageBox.Show("ادخل رقم الفاتورة");
                    ////SqlDataAdapter max = new SqlDataAdapter("select max(ref) from pos_hdr where contr=" + BL.CLS_Session.ctrno + "", con2);
                    ////DataTable dtm = new DataTable();
                    ////max.Fill(dtm);
                    ////maxref = Convert.ToInt32(dtm.Rows[0][0]);
                    ////tet_ref.Text = maxref.ToString();
                    SqlDataAdapter order = new SqlDataAdapter("select a.*,b.* from pos_hdr a left join pos_hdr_r b on  a.[brno]=b.[dbrno] and a.[slno]=b.[dslno] and a.[ref]=b.[dref] and a.[contr] = b.[dcontr] where a.ref=" + aref + " and a.contr=" + (slctr.Equals("") ? BL.CLS_Session.ctrno : slctr) + " and a.[slno]='" + BL.CLS_Session.slno + "'", con2);
                    SqlDataAdapter pos_dtl = new SqlDataAdapter("SELECT a.barcode as a,b.item_no as b,a.name as c,a.unit as d,a.qty as e,a.price as f,a.cost as g,a.is_req as h,(a.qty * a.price) as i from pos_dtl a,items_bc b where ltrim(rtrim(a.barcode))=ltrim(rtrim(b.barcode)) and a.ref=" + aref + " and a.contr=" + (slctr.Equals("") ? BL.CLS_Session.ctrno : slctr) + " and a.slno='" + BL.CLS_Session.slno + "' order by a.srno", con2);

                    DataSet1 ds = new DataSet1();

                    order.Fill(ds, "0");
                    pos_dtl.Fill(ds, "1");

                    if (ds.Tables["0"].Rows.Count != 0)
                    {

                        lbldate.Text = (Convert.ToDateTime(ds.Tables["0"].Rows[0][5])).ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US", false));
                        lblref.Text = ds.Tables["0"].Rows[0][2].ToString();
                        lbluser.Text = ds.Tables["0"].Rows[0][10].ToString();
                        lblcashir.Text = ds.Tables["0"].Rows[0][3].ToString();

                        txt_custno.Text = ds.Tables["0"].Rows[0]["cust_no"].ToString().Equals("0") ? "" : ds.Tables["0"].Rows[0]["cust_no"].ToString();
                        txt_custnam.Text = ds.Tables["0"].Rows[0]["note"].ToString();
                        txt_temp.Text = ds.Tables["0"].Rows[0]["mobile"].ToString();
                        txt_contr.Text = ds.Tables["0"].Rows[0]["contr"].ToString();
                        // txt_custnam.Text = ds.Tables["0"].Rows[0][""].ToString();

                        txt_driver.Text = ds.Tables["0"].Rows[0]["driver_id"].ToString();
                        txt_drivernam.Text = ds.Tables["0"].Rows[0]["driver_nam"].ToString();
                        txt = ds.Tables["0"].Rows[0]["dnote"].ToString();
                        ds.Tables["0"].Rows[0][3].ToString();
                        ds.Tables["0"].Rows[0][3].ToString();
                        chk_payed.Checked = string.IsNullOrEmpty(ds.Tables["0"].Rows[0]["dpayed"].ToString()) ? false : Convert.ToBoolean(ds.Tables["0"].Rows[0]["dpayed"]);
                        chk_agel.Checked = Convert.ToInt32(ds.Tables["0"].Rows[0][4].ToString()) == 3 ? true : false;
                       // chk_agel.Enabled = Convert.ToInt32(ds.Tables["0"].Rows[0][4].ToString()) == 0 ? true : false;
                        at.txt_tameemamt.Text = ds.Tables["0"].Rows[0]["dtameen"].ToString();
                        at.txt_tameennot.Text = ds.Tables["0"].Rows[0]["tameen_note"].ToString();

                        tamin = ds.Tables["0"].Rows[0]["dtameen"].ToString();
                        taminnot = ds.Tables["0"].Rows[0]["tameen_note"].ToString();
                        txt_desc.Text = ds.Tables["0"].Rows[0]["discount"].ToString(); 

                        if (ds.Tables["0"].Rows[0][4].ToString() == "1")
                        {
                            button5_Click(null, null);
                        }
                        else
                        {
                            if (ds.Tables["0"].Rows[0][4].ToString() == "4")
                            {
                                button6_Click(null, null);
                            }
                            else
                            {
                                if (ds.Tables["0"].Rows[0][4].ToString() == "5")
                                {
                                    button22_Click(null, null);
                                    // button4_Click(sender, e);
                                    //SqlDataAdapter cust = new SqlDataAdapter("select first_name,ID_CUSTOMER from customers where ID_CUSTOMER=" + ds.Tables["0"].Rows[0][11], con2);
                                    //DataTable dt = new DataTable();
                                    //cust.Fill(dt);
                                    //lbl_cust_name.Text = dt.Rows[0][0].ToString();
                                    //lbl_cust_no.Text = dt.Rows[0][1].ToString();

                                    //typeno = 4;

                                    //button4.BackColor = Color.GreenYellow;
                                    ////button5.BackColor = default(Color);
                                    //// button6.BackColor = default(Color);
                                    ////  button5.BackColor = SystemColors.Control;
                                    //// button6.BackColor = SystemColors.Control;
                                    //button5.UseVisualStyleBackColor = true;
                                    //button6.UseVisualStyleBackColor = true;

                                }
                            }
                        }

                        dataGridView1.DataSource = ds.Tables["1"];
                        dtt = ds.Tables["1"];

                        total();
                        textBox1.Text = ds.Tables["0"].Rows[0][8].ToString();

                        txt_contr.Enabled = false;
                        hight();
                    }
                    else
                    {
                        MessageBox.Show("لا توجد فاتورة بهذ الرقم", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void print_ref()
        {
            int toprnt = Convert.ToInt32(tet_ref.Text);
            SqlDataAdapter dacr = new SqlDataAdapter("select * from pos_dtl where ref=" + toprnt + " and contr=" + (txt_contr.Text.Equals("") ? BL.CLS_Session.ctrno : txt_contr.Text) + "", con2);
            SqlDataAdapter dacr1 = new SqlDataAdapter("select * from pos_hdr where ref=" + toprnt + " and contr=" + (txt_contr.Text.Equals("") ? BL.CLS_Session.ctrno : txt_contr.Text) + "", con2);
            DataSet1 ds = new DataSet1();

            dacr.Fill(ds, "dtl");
            dacr1.Fill(ds, "hdr");


            rpt.SalesReport report = new rpt.SalesReport();

            //  CrystalReport1 report = new CrystalReport1();
            report.SetDataSource(ds);
            report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
            report.SetParameterValue("br_name", BL.CLS_Session.brname);
            report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
            report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
            report.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
            //    crystalReportViewer1.ReportSource = report;

            //  crystalReportViewer1.Refresh();

            //// report.PrintOptions.PrinterName = "pos";
            foreach (string line in lines_prt)
            {
                report.PrintOptions.PrinterName = line;

                report.PrintToPrinter(1, false, 0, 0);
                // report.PrintToPrinter(0,true, 1, 1);

            }
            report.Close();
            //// report.PrintToPrinter(1, false, 0, 0);
            //// report.Dispose();
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
            SqlDataAdapter dacr1 = new SqlDataAdapter("select a.[brno],a.[slno],a.[ref],a.[contr],a.[type],FORMAT (a.[date], 'dd-MM-yyyy hh:mm:ss tt', 'en-us') date,a.[total],a.[count],a.[payed],a.[total_cost],b.sl_name [saleman],a.[req_no],a.[cust_no],a.[discount],a.[net_total],a.[sysdate],a.[gen_ref],a.[tax_amt],a.[card_amt],a.[note],a.[mobile] from pos_hdr a join pos_salmen b on a.saleman=b.sl_id where a.ref=" + lblref.Text + " and a.[contr]="  + (txt_contr.Text.Equals("") ? BL.CLS_Session.ctrno : txt_contr.Text) + "" + " and  a.[brno]= '" + BL.CLS_Session.brno + "' ", con2);
            SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty],[offr_amt] from pos_dtl where ref=" + lblref.Text + " and [contr]=" + (txt_contr.Text.Equals("") ? BL.CLS_Session.ctrno : txt_contr.Text) + " and  [brno]= '" + BL.CLS_Session.brno + "' ", con2);
            SqlDataAdapter dacr2 = new SqlDataAdapter("select dnote,dtameen,tameen_note from pos_hdr_r where dref=" + lblref.Text + " and dcontr=" + BL.CLS_Session.ctrno + " and  dbrno= '" + BL.CLS_Session.brno + "' ", con2);

          
            DataTable dtn = new DataTable();
            
            
            dacr2.Fill(dtn);
            DataSet1 ds = new DataSet1();

            dacr1.Fill(ds, "hdr");
            dacr.Fill(ds, "dtl");

            if (ds.Tables["hdr"].Rows.Count == 0)
            {
                MessageBox.Show("لا يوجد فاتورة للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FastReport.Report rpt = new FastReport.Report();

            // if (isocu == false)
            rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt.frx");
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
            rpt.SetParameterValue("dnote",dtn.Rows.Count>0? dtn.Rows[0][0].ToString() : "");
            rpt.SetParameterValue("dtameen", dtn.Rows.Count > 0 ? dtn.Rows[0][1].ToString() : "");
            rpt.SetParameterValue("tameen_note", dtn.Rows.Count > 0 ? dtn.Rows[0][2].ToString() : "");
            //rpt.SetParameterValue("type", ds.Tables["hdr"].Rows[0][4].ToString());
            rpt.SetParameterValue("type", ds.Tables["hdr"].Rows[0][4].ToString());
            //string dtt = Convert.ToDateTime(ds.Tables["hdr"].Rows[0]["date"].ToString()).ToString("yyyy-MM-ddTHH:mm:ssZ", new CultureInfo("en-US", false));
            //var tlvs = new GenerateQrCode.GenerateQrCodeTLV(
            //              BL.CLS_Session.cmp_ename,
            //               BL.CLS_Session.tax_no,
            //               dtt,
            //               ds.Tables["hdr"].Rows[0]["net_total"].ToString(),
            //              Math.Round(Convert.ToDouble(ds.Tables["hdr"].Rows[0]["tax_amt"].ToString()), 2).ToString());

            //rpt.SetParameterValue("qr", Convert.ToBase64String(tlvs.GetQRCode()));
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

              //  rpt.PrintSettings.Printer = line;// "pos";
                rpt.PrintSettings.Printer = iii == 0 ? printer_nam : line;// "pos";
                rpt.PrintSettings.ShowDialog = false;
                // FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                // rpt.PrintSettings.Copies =Convert.ToInt32(txt_count.Text);
                rpt.Print();
                iii++;
            }

           
        }

        private void Print_btn_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.prnt_type.Equals("0"))
                print_ref();
            else
                print_ref_fr();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //typeno = 4;
            //CUST_SEARCH cs = new CUST_SEARCH();
            //cs.StartPosition = FormStartPosition.CenterScreen;
            //cs.ShowDialog();

            //if (cs.dataGridView1.Rows.Count>0)
            //{
            //lbl_cust_no.Text = cs.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //lbl_cust_name.Text = cs.dataGridView1.CurrentRow.Cells[1].Value.ToString();

            //button4.BackColor = Color.GreenYellow;
            ////button5.BackColor = default(Color);
            //// button6.BackColor = default(Color);
            ////  button5.BackColor = SystemColors.Control;
            //// button6.BackColor = SystemColors.Control;
            //button5.UseVisualStyleBackColor = true;
            //button6.UseVisualStyleBackColor = true;
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
            lbl_cust_name.Text = ".";
            lbl_cust_no.Text = "0";
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
            lbl_cust_name.Text = ".";
            lbl_cust_no.Text = "0";
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                maxref = Convert.ToInt32(tet_ref.Text) - 1;
                tet_ref.Text = maxref.ToString();
                lbl_getref_Click(sender, e);
            }
            catch { }
            /*
            SqlDataAdapter order = new SqlDataAdapter("select * from pos_hdr where ref=" + maxref, con2);
            SqlDataAdapter pos_dtl = new SqlDataAdapter("SELECT pos_dtl.barcode as a,items.item_no as b,pos_dtl.name as c,pos_dtl.unit as d,pos_dtl.qty as e,pos_dtl.price as f,pos_dtl.cost,pos_dtl.is_req as h from pos_dtl,items where ltrim(rtrim(pos_dtl.barcode))=ltrim(rtrim(items.item_barcode)) and pos_dtl.ref=" + maxref, con2);

            DataSet1 ds = new DataSet1();

            order.Fill(ds, "0");
            pos_dtl.Fill(ds, "1");
            tet_ref.Text = maxref.ToString();

            if (ds.Tables["0"].Rows.Count != 0)
            {

                lbldate.Text = (Convert.ToDateTime(ds.Tables["0"].Rows[0][4])).ToString();
                lblref.Text = ds.Tables["0"].Rows[0][1].ToString();
                lbluser.Text = ds.Tables["0"].Rows[0][9].ToString();
                lblcashir.Text = ds.Tables["0"].Rows[0][2].ToString();

                if (ds.Tables["0"].Rows[0][3].ToString() == "1")
                {
                    button5_Click(sender, e);
                }
                else
                {
                    if (ds.Tables["0"].Rows[0][3].ToString() == "3")
                    {
                        button6_Click(sender, e);
                    }
                    else
                    {
                        if (ds.Tables["0"].Rows[0][3].ToString() == "4")
                        {
                            // button4_Click(sender, e);
                            SqlDataAdapter cust = new SqlDataAdapter("select first_name,ID_CUSTOMER from customers where ID_CUSTOMER=" + ds.Tables["0"].Rows[0][11], con2);
                            DataTable dt = new DataTable();
                            cust.Fill(dt);
                            lbl_cust_name.Text = dt.Rows[0][0].ToString();
                            lbl_cust_no.Text = dt.Rows[0][1].ToString();

                            typeno = 4;

                            button4.BackColor = Color.GreenYellow;
                            //button5.BackColor = default(Color);
                            // button6.BackColor = default(Color);
                            //  button5.BackColor = SystemColors.Control;
                            // button6.BackColor = SystemColors.Control;
                            button5.UseVisualStyleBackColor = true;
                            button6.UseVisualStyleBackColor = true;

                        }
                    }
                }

                dataGridView1.DataSource = ds.Tables["1"];
                dtt = ds.Tables["1"];

                total();
                textBox1.Text = ds.Tables["0"].Rows[0][7].ToString();
             
            }
            else
            {
                MetroMessageBox.Show(this, "No Found Invoice لا يوجد فاتورة ", "Alert تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
             */
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                maxref = Convert.ToInt32(tet_ref.Text) + 1;
                tet_ref.Text = maxref.ToString();
                lbl_getref_Click(sender, e);
            }
            catch { }
            /*
            SqlDataAdapter order = new SqlDataAdapter("select * from pos_hdr where ref=" + maxref, con2);
            SqlDataAdapter pos_dtl = new SqlDataAdapter("SELECT pos_dtl.barcode as a,items.item_no as b,pos_dtl.name as c,pos_dtl.unit as d,pos_dtl.qty as e,pos_dtl.price as f,pos_dtl.cost,pos_dtl.is_req as h from pos_dtl,items where ltrim(rtrim(pos_dtl.barcode))=ltrim(rtrim(items.item_barcode)) and pos_dtl.ref=" + maxref, con2);

            DataSet1 ds = new DataSet1();
            tet_ref.Text = maxref.ToString();
            order.Fill(ds, "0");
            pos_dtl.Fill(ds, "1");

            if (ds.Tables["0"].Rows.Count != 0)
            {

                lbldate.Text = (Convert.ToDateTime(ds.Tables["0"].Rows[0][4])).ToString();
                lblref.Text = ds.Tables["0"].Rows[0][1].ToString();
                lbluser.Text = ds.Tables["0"].Rows[0][9].ToString();
                lblcashir.Text = ds.Tables["0"].Rows[0][2].ToString();

                if (ds.Tables["0"].Rows[0][3].ToString() == "1")
                {
                    button5_Click(sender, e);
                }
                else
                {
                    if (ds.Tables["0"].Rows[0][3].ToString() == "3")
                    {
                        button6_Click(sender, e);
                    }
                    else
                    {
                        if (ds.Tables["0"].Rows[0][3].ToString() == "4")
                        {
                            // button4_Click(sender, e);
                            SqlDataAdapter cust = new SqlDataAdapter("select first_name,ID_CUSTOMER from customers where ID_CUSTOMER=" + ds.Tables["0"].Rows[0][11], con2);
                            DataTable dt = new DataTable();
                            cust.Fill(dt);
                            lbl_cust_name.Text = dt.Rows[0][0].ToString();
                            lbl_cust_no.Text = dt.Rows[0][1].ToString();

                            typeno = 4;

                            button4.BackColor = Color.GreenYellow;
                            //button5.BackColor = default(Color);
                            // button6.BackColor = default(Color);
                            //  button5.BackColor = SystemColors.Control;
                            // button6.BackColor = SystemColors.Control;
                            button5.UseVisualStyleBackColor = true;
                            button6.UseVisualStyleBackColor = true;

                        }
                    }
                }

                dataGridView1.DataSource = ds.Tables["1"];
                dtt = ds.Tables["1"];

                total();
                textBox1.Text = ds.Tables["0"].Rows[0][7].ToString();
            }

            else
            //{ MessageBox.Show("لا يوجد فاتورة بهذا الرقم No Found Invoice "); }
            {
                MetroMessageBox.Show(this, "No Found Invoice لا يوجد فاتورة ", "Alert تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
             */
        }

        private void Resturant_ReSales_Shown(object sender, EventArgs e)
        {
            if (Convert.ToInt32(datval.convert_to_yyyyDDdd(DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false)))) > Convert.ToInt32(datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt)) || Convert.ToInt32(datval.convert_to_yyyyDDdd(DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false)))) < Convert.ToInt32(datval.convert_to_yyyyDDdd(BL.CLS_Session.start_dt)))
            {
                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Date Out of Years" : "لا يمكن ادخال حركة خارج السنة المالية", "Error خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            hight();
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

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.Value == null && (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[8]))
                dataGridView1.CurrentCell.Value = 0;
            total();
        }

        private void button17_Click(object sender, EventArgs e)
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

        private void button16_Click(object sender, EventArgs e)
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

        private void button19_Click(object sender, EventArgs e)
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

        private void button20_Click(object sender, EventArgs e)
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

        private void button9_Click(object sender, EventArgs e)
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
            lbl_cust_name.Text = ".";
            lbl_cust_no.Text = "0";
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
                txt_custnam.ReadOnly = false;
            else
                txt_custnam.ReadOnly = true;
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

        private void tet_ref_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tet_ref.Text))
                txt_contr.Enabled = true;
            else
                txt_contr.Enabled = false;

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
            if (dataGridView1.RowCount > 1)
            {
                Pos.Pos_note pn = new Pos_note(txt);
                pn.ShowDialog();
                txt = pn.textBox1.Text;
            }
        }

        private void chk_agel_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_agel.Checked)
                typeno = 3;
            else
                typeno = 5; 
        }

        private void Resturant_ReSales_FormClosing(object sender, FormClosingEventArgs e)
        {
            BL.CLS_Session.is_sal_login = false;
           // cssc.Close();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            at = new Pos.Add_Tameen();
            at.txt_tameemamt.Text = tamin;
            at.txt_tameennot.Text = taminnot;
            at.ShowDialog();

        }

        private void txt_desc_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_desc.Text))
                txt_desc.Text = "0";
            else
            {
                double sum = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                    dataGridView1.Rows[i].Cells[8].Value = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value);
                }
                label1.Text = BL.CLS_Session.isshamltax.Equals("1") ? ((Convert.ToDouble(sum.ToString()) + Convert.ToDouble(0)) - Convert.ToDouble(txt_desc.Text)).ToString() : (Convert.ToDouble(sum.ToString()) - Convert.ToDouble(txt_desc.Text)).ToString();
            }
        }
       
    }
}

