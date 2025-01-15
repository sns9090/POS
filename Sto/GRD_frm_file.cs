using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace POS.Sto
{
    public partial class GRD_frm_file : BaseForm
    {
        BL.Date_Validate dtv = new BL.Date_Validate();
        BL.DAML daml = new BL.DAML();
        string fname, fulldirectory, fullpath;
        public GRD_frm_file()
        {
            InitializeComponent();
        }

        private void cmb_ftwh_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void GRD_frm_file_Load(object sender, EventArgs e)
        {
            string sl = BL.CLS_Session.stkey.Replace(" ", "','").Remove(0, 2) + "'";
            DataTable dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select wh_no,wh_name from whouses where wh_brno + wh_no in(" + sl + ") and wh_brno='" + BL.CLS_Session.brno + "'");



            cmb_ftwh.DataSource = dtslctr;
            cmb_ftwh.DisplayMember = "wh_name";
            cmb_ftwh.ValueMember = "wh_no";
            cmb_ftwh.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
           // ofd.InitialDirectory = Directory.GetCurrentDirectory() +"\\images"  ;
           // ofd.Filter = "ملفات الجرد |*.JPG; *.PNG; *.GIF; *.BMP";
            ofd.Filter = "ملفات الجرد |*.DBF";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //pictureBox1.Image = Image.FromFile(ofd.FileName);
               // pictureBox1.Image = Image.FromFile(ofd.FileName);

               // fname = Path.GetFileName(ofd.FileName);
                fname = Path.GetFileName(ofd.FileName.Replace(".DBF",""));

                fulldirectory = Path.GetDirectoryName(ofd.FileName);

                fullpath = Path.GetFullPath(ofd.FileName);

               // textBox1.Text = fullpath2;

                textBox1.Text = fulldirectory + @"\" + fname;

            }
        }

        private void cmb_ftwh_Enter(object sender, EventArgs e)
        {
           
        }

        private void cmb_ftwh_Leave(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select * from ttk_hdr where ttbranch='" + BL.CLS_Session.brno + "' and ttwhno='" + cmb_ftwh.SelectedValue + "'");

                if (dt.Rows.Count > 0 )
                {
                    //  daml.Exec_Query_only("delete from ttk_hdr where ttbranch='" + BL.CLS_Session.brno + "' and ttwhno='" + cmb_ftwh.SelectedValue + "'");
                    //  MessageBox.Show("تم حذف الجرد الموجود ");
                    txt_mdate.Text = dt.Rows[0][2].ToString().Substring(6, 2) + dt.Rows[0][2].ToString().Substring(4, 2) + dt.Rows[0][2].ToString().Substring(0, 4);//datval.convert_to_ddDDyyyy(dth.Rows[0][2].ToString());
                    txt_hdate.Text = dt.Rows[0][3].ToString().Substring(6, 2) + dt.Rows[0][3].ToString().Substring(4, 2) + dt.Rows[0][3].ToString().Substring(0, 4);//datval.convert_to_ddDDyyyy(dth.Rows[0][3].ToString());

                }
                else
                {
                    MessageBox.Show("يرجى اختيار مخزن الجرد","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                  //  cmb_ftwh.SelectedIndex = -1;
                  //  cmb_ftwh.Focus();
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("يرجى اختيار مخزن الجرد وملف الجرد", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            progressBar1.Visible = true;
           // string dbfDirectory = Environment.CurrentDirectory;
            string connectionString = "Provider=VFPOLEDB;Data Source=" + fulldirectory;

            OleDbConnection connection = new OleDbConnection(connectionString);

            OleDbDataAdapter da = new OleDbDataAdapter("select d,sum(b * iif(shdqty>0,shdqty,1)) from " + fname + " group by d", connection);

            DataTable dtwh = new DataTable();

            da.Fill(dtwh);

            progressBar1.Minimum = 0;
            progressBar1.Maximum = dtwh.Rows.Count;
            for (int i = 0; i < dtwh.Rows.Count; i++)
            {
               
                string StrQuery = " MERGE ttk_dtl as t"
                                + " USING (select '" + dtwh.Rows[i][0].ToString().Trim() + "' as itemno, " + dtwh.Rows[i][1].ToString() + " as ttqty) as s"
                                + " ON ltrim(rtrim(T.itemno))=ltrim(rtrim(S.itemno)) and T.ttbranch='" + BL.CLS_Session.brno + "' and T.whno='" + cmb_ftwh.SelectedValue + "'"
                                + " WHEN MATCHED THEN"
                              //  + " UPDATE SET T.ttqty = S.ttqty";
                               + " UPDATE SET T.ttqty = S.ttqty;";
                             //   + " WHEN NOT MATCHED THEN"
                             //   + " INSERT VALUES(s.ttbranch, s.whno, s.name,s.binno,s.ttqty,s.ttstatus,s.srl_no,s.mclass,s.itemno,s.mqty,s.expdate,s.lcost,s.fcost,s.barcode,s.pack0,s.pack1,s.pkqty1,s.nobin);";
                try
                {
                    //SqlConnection conn = new SqlConnection();

                    /*
                    using (SqlCommand comm = new SqlCommand(StrQuery, con))
                    {
                       // con.Open();
                        comm.ExecuteNonQuery();
                       // con.Close();
                       
                    }
                     */

                    daml.Insert_Update_Delete_retrn_int(StrQuery, false);

                    progressBar1.Increment(1);
                    //   progressBar1.Value = i;
                    //   pfr.progressBar1.Value = i;


                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.ToString());
                }
                finally
                {

                }
            }

            button2.Enabled = false;
            MessageBox.Show("تم ادخال ملف الجرد بنجاح " + dtwh.Rows.Count.ToString(),"",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            progressBar1.Visible = false;
           // dataGridView1.DataSource = dt;


        }
    }
}
