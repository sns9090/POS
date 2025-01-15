using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS.PL
{
    public partial class Ts_Msg : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        public Ts_Msg()
        {
            InitializeComponent();
        }

        private void Ts_Msg_Load(object sender, EventArgs e)
        {

        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            SqlCommand myCommand = new SqlCommand();
            //1 SqlDataReader myReader;

           // myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Connection = BL.DAML.con;
            myCommand.CommandText = "update ts_msg set inactive = 1 where id="+label3.Text+"";

            if (BL.DAML.con.State == ConnectionState.Closed) BL.DAML.con.Open();
            //con.Open();

            //3  myReader = myCommand.ExecuteReader();
            myCommand.ExecuteNonQuery();
            BL.DAML.con.Close();

            MessageBox.Show("تم ايقاف الرسالة","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            btn_Del.Enabled = false;
        }
    }
}
