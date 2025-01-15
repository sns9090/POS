using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS.Pos
{
    public partial class Size_Select : BaseForm
    {
      //  DataSet ds;
        SqlConnection con2 = BL.DAML.con;
        public string nam = "",item,bar="";
        public double pric;
        public Size_Select(string itmno)
        {
            InitializeComponent();
            item = itmno;
        }

        private void Add_Select_Load(object sender, EventArgs e)
        {
            creat_btns();
            flowLayoutPanel1.RightToLeft = BL.CLS_Session.lang.Equals("E") ? RightToLeft.No : RightToLeft.Yes;
        }


        private void creat_btns()
        {
            try
            {

                SqlDataAdapter da = new SqlDataAdapter("select b.unit_name,a.pk_qty, a.price,a.barcode  from items_bc a,units b where a.pack=b.unit_id and a.item_no ='" + item + "' order by a.pkorder", con2);
                DataSet ds = new DataSet();
                da.Fill(ds, "0");

                int cunt = ds.Tables["0"].Rows.Count;


                    //   pictureBox1.Image = Image.FromStream(ms);


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
                      //  b.Name = ds.Tables["0"].Rows[i][1].ToString();
                        b.Text = ds.Tables["0"].Rows[i][0].ToString() + " : "  + ds.Tables["0"].Rows[i][2].ToString();
                        //*****  b.TextAlign = ContentAlignment.BottomCenter;
                        b.FlatStyle = FlatStyle.Standard;

                        b.BackgroundImage = Properties.Resources.background_button;
                        b.BackgroundImageLayout = ImageLayout.Stretch;

                        //  b.Margin = new Padding(100, 100, 100, 0);
                        b.Tag = ds.Tables["0"].Rows[i][3].ToString();
                      //  pric =Convert.ToDouble(ds.Tables["0"].Rows[i][2]);
                       // bar = ds.Tables["0"].Rows[i][3].ToString();
                        /*
                        byte[] data = (byte[])ds.Tables["0"].Rows[i][2];
                        using (MemoryStream ms = new MemoryStream(data))
                        {
                            //here you get the image and assign it to the button.
                            Image image = new Bitmap(ms);
                        }
                        */



                        // b.Image = new Bitmap(ms);
                        b.Width = 150;
                        b.Height = 50;
                        // b.Top = 10;
                        b.Font = new Font("Arial", 11, FontStyle.Bold);
                        b.Click += new EventHandler(b_click);
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


            Button b = sender as Button;
            // label1.Text = b.Text;

            // label1.Text = b.Name;

            try
            {

                nam = b.Text.Substring(0, b.Text.IndexOf(":"));
                bar = b.Tag.ToString();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
