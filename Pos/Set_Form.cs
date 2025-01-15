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

namespace POS.Pos
{
    public partial class Set_Form : BaseForm
    {
        SqlConnection con2 = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public Set_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("هل تريد تصفير التسلسل بالفعل ؟", "تاكيد", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
            {
                SqlCommand sqlcmd = new SqlCommand("update pos_hdr set req_no=0", con2);
                con2.Open();
                sqlcmd.ExecuteNonQuery();
                con2.Close();
                MessageBox.Show("تم تصفير التسلسل بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                this.Close();
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
    }
}
