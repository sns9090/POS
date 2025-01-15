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
    public partial class Add_Select : BaseForm
    {
        SqlConnection con2 = BL.DAML.con;
        public string val = "";
        public Add_Select()
        {
            InitializeComponent();
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

                SqlDataAdapter da = new SqlDataAdapter("select " + (BL.CLS_Session.lang.Equals("E") ? " add_edesc" : " add_desc") + "  as name from pos_adds where inactive=0", con2);
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
                        b.Text = ds.Tables["0"].Rows[i][0].ToString();
                        //*****  b.TextAlign = ContentAlignment.BottomCenter;
                        b.FlatStyle = FlatStyle.Standard;

                        b.BackgroundImage = Properties.Resources.background_button;
                        b.BackgroundImageLayout = ImageLayout.Stretch;

                        //  b.Margin = new Padding(100, 100, 100, 0);

                       
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

                val = val + " - " + b.Text;
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
