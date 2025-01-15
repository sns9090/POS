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
using System.IO;
using System.Globalization;

namespace POS.Sto
{
    public partial class Groups : BaseForm
    {
        SqlConnection con1 = BL.DAML.con;//new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        string path = "0.png", strimgname = "";
        public Groups()
        {
            InitializeComponent();
        }

        private SqlConnection sqlConnection = null;

        private SqlDataAdapter sqlDataAdapter = null;

        private SqlCommandBuilder sqlCommandBuilder = null;

        private DataTable dataTable = null;

        private BindingSource bindingSource = null;


        private void button2_Click(object sender, EventArgs e)
        {
             DialogResult result = MessageBox.Show("هل تريد المسح", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

             if (result == DialogResult.Yes)
             {
                 try
                 {

                     int todel = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                     SqlDataAdapter da = new SqlDataAdapter("select * from items where item_group=" + todel, con1);
                     DataTable dt = new DataTable();
                     da.Fill(dt);

                     if (dt.Rows.Count >= 1)
                     {
                         MessageBox.Show(" لا يمكن حذف مجموعة مرتبطة باصناف ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                     else
                     {
                         dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);


                         sqlDataAdapter.Update(dataTable);

                         MessageBox.Show("تم الحذف بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                     }
                 }

                 // try
                 // {


                     //if (dataGridView1.CurrentRow.Cells[0].Value.ToString() == "1")
                 //{
                 //    MessageBox.Show("لا يمكن حذف الاغتراضي ");
                 //}
                 //else
                 //{
                 //    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);


                     //    sqlDataAdapter.Update(dataTable);

                     //    MessageBox.Show("تم الحذف بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                 //}

                // }

                 catch (Exception exceptionObj)
                 {

                     MessageBox.Show(exceptionObj.Message.ToString());

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow dgr in dataGridView1.Rows)
                {
                    DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                    ch1 = (DataGridViewCheckBoxCell)dataGridView1.Rows[dgr.Index].Cells[5];

                    if (ch1 == null)
                        ch1.Value = false;
                }
               
                sqlDataAdapter.Update(dataTable);
                MessageBox.Show("تم حفظ التعديلات","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                Groups_Load(sender, e);

            }

            catch (Exception exceptionObj)
            {

                MessageBox.Show(exceptionObj.Message.ToString());

            }
        }

        private void Groups_Load(object sender, EventArgs e)
        {
            //if (BL.CLS_Session.islight)
            //{
            //    button4.Visible = false;          
            //}
            if (BL.CLS_Session.iscofi)
                dataGridView1.Columns["Column7"].Visible = true;
            else
                dataGridView1.Columns["Column7"].Visible = false;


            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 12);
            dataGridView1.Columns[0].ReadOnly = true;
           // this.dataGridView1.Columns[0].Frozen = true;
            // TODO: This line of code loads data into the 'dataSet1.items' table. You can move, or remove it, as needed.
            //this.itemsTableAdapter.Fill(this.dataSet1.items);
            sqlConnection = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            // sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter("select [group_id],[group_name],[group_desc],[group_order],[group_img],[inactive],[printer] from groups where group_pid is null order by group_order", sqlConnection);
            sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

            dataTable = new DataTable();



            sqlDataAdapter.Fill(dataTable);


            bindingSource = new BindingSource();

            bindingSource.DataSource = dataTable;


            dataGridView1.DataSource = bindingSource;




        }

        private void button3_Click(object sender, EventArgs e)
        {
            Groups_Load(sender, e);
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.IsNewRow)
            {
                dataGridView1.CurrentRow.Cells[0].ReadOnly = false;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells["Column5"])
            {
                //  MessageBox.Show(path +"\n"+Directory.GetCurrentDirectory() + "\\images");
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = Directory.GetCurrentDirectory() + "\\images";
                ofd.Filter = "ملفات الصور |*.JPG; *.PNG; *.GIF; *.BMP";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    //pictureBox1.Image = Image.FromFile(ofd.FileName);
                 //   pictureBox1.Image = Image.FromFile(ofd.FileName);
                    path = Path.GetFileName(ofd.FileName);

                    string fullpath = Path.GetDirectoryName(ofd.FileName);

                   // strimgname = DateTime.Now.ToString("yyyyMMdd_HHmmss_", new CultureInfo("en-US", false)) + path;

                    // MessageBox.Show(fullpath + "\\" + path + "\n" + "\n" + Directory.GetCurrentDirectory() + "\\images\\" + path);
                    if (!File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + path))
                    {
                        File.Copy(fullpath + "\\" + path, Directory.GetCurrentDirectory() + "\\images\\" + path, true);
                        //    //  Console.WriteLine("The file exists.");
                    }

                  //  Bitmap img = new Bitmap(Directory.GetCurrentDirectory() + "\\images\\" + strimgname);
                    var img = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + path);
                  //  MessageBox.Show(Directory.GetCurrentDirectory() + "\\images\\" + path);
                    dataGridView1.CurrentRow.Cells[4].Value = img;
                    //else
                    //{ 

                    //}
                }
            }
            if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells["Column7"])
            {
                var sr = new Search_All("prntr", "");
                sr.checkBox1.Visible = false;
                sr.dataGridView1.AllowUserToAddRows = true;
                sr.dataGridView1.ColumnCount = 1;
                sr.dataGridView1.Columns[0].Name = "الطابعة";
                sr.ShowDialog();
                dataGridView1.CurrentCell.Value = sr.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
        }

        private void حذفالصورةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentRow.Cells[4].Value = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Sgroup sg = new Sgroup();
            sg.ShowDialog();
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value == null || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[0].Value.ToString()))
                dataGridView1.CurrentRow.Cells[0].Value = (Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString().Length == 1 ? "0" + (Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString() : (Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString();

            if (dataGridView1.CurrentRow.Cells[3].Value == null || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[3].Value.ToString()))
                dataGridView1.CurrentRow.Cells[3].Value = (Convert.ToInt32(dataGridView1[3, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[""] && e.KeyCode == Keys.F8)
            //{
            //    var sr = new Search_All("prntr", "");
            //    sr.checkBox1.Visible = false;
            //    sr.ShowDialog();
            //    dataGridView1.CurrentCell.Value = sr.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //}
        }
    }
}
