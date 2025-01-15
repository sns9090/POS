using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using System.Data.SqlClient;

namespace POS.Pos
{
    public partial class Cust_Screen : BaseForm
    {
        DataTable dt;
        BL.DAML daml = new BL.DAML();
        string[] filePaths;
        int cnt,img=0;
        public Cust_Screen()
        {
            InitializeComponent();
        }

        private void Cust_Screen_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;

            filePaths = Directory.GetFiles(Environment.CurrentDirectory + @"\img_show\", "*.*");
           // cnt = filePaths.Length;
           // timer1.Start();
            timer2.Start();

            dt = daml.SELECT_QUIRY_only_retrn_dt("select a.item_name,b.minqty,b.minqty * b.lprice1 lprice1,b.DiscountP,b.disctype from items_offer b join items a on a.item_no=b.itemno where b.InActive=0 and EndDate>getdate()");
            cnt = dt.Rows.Count;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            showimg(img);
        }
        private void showimg(int img2)
        {
            pictureBox1.Image = Image.FromFile(filePaths[img2]);

            img=img2+1;
            if (img2 + 1 == cnt)
                img = 0;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            showimgtb(img);
        }

        private void showimgtb(int img2)
        {
            //pictureBox1.Image = Image.FromFile(filePaths[img2]);

            //img = img2 + 1;
            //if (img2 + 1 == cnt)
            //    img = 0;
            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dt.Rows[img2][0].ToString();
                // textBox2.Text = "العدد : " + dt.Rows[img2][1].ToString();
                textBox8.Text = dt.Rows[img2][1].ToString();
                textBox6.Text = dt.Rows[img2][2].ToString();

                if (dt.Rows[img2][4].ToString().Equals("1"))
                    textBox7.Text = (Convert.ToDecimal(dt.Rows[img2][2]) - Convert.ToDecimal(dt.Rows[img2][3])).ToString();
                else
                    textBox7.Text = (Convert.ToDecimal(dt.Rows[img2][3])).ToString();

                img = img2 + 1;
                if (img2 + 1 == cnt)
                    img = 0;
            }
        }
    }
}
