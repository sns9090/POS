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

namespace POS
{
    public partial class Server_Links : BaseForm
    {
        SqlConnection con1 = BL.DAML.con;//

        public Server_Links()
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
            try
            {

                int todel =Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                //SqlDataAdapter da = new SqlDataAdapter("select * from accounts where a_brno='" + todel + "'", con1);
                //DataTable dt=new DataTable();
                //da.Fill(dt);

               // if (dt.Rows.Count >= 1)
                if (0== 1)
                {
                    MessageBox.Show(" لا يمكن حذف فرع لدية حسابات مرتبطة ", "تنبيه",MessageBoxButtons.OK,MessageBoxIcon.Error);
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
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
         
            if (BL.CLS_Session.islight)
                dataGridView1.AllowUserToAddRows = false;

            if (BL.CLS_Session.sysno == "1")
            {
                dataGridView1.Columns[3].Visible = false; dataGridView1.Columns[4].Visible = false; dataGridView1.Columns[5].Visible = false; textBox1.Visible = false;
            }
            //this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 12);
            dataGridView1.Columns[0].ReadOnly = true;
           // this.dataGridView1.Columns[0].Frozen = true;
            // TODO: This line of code loads data into the 'dataSet1.items' table. You can move, or remove it, as needed.
            //this.itemsTableAdapter.Fill(this.dataSet1.items);
            sqlConnection = BL.DAML.con;//
            // sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter("select  server_id, server_name, server_ename, db, acc, cus, sup, its, ac, sl, pu, st,posted,dtl FROM  server_links where server_name  like '%" + textBox1.Text + "%' order by server_id", sqlConnection);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //DataRow[] result= dataTable.Select("br_name like '%"+textBox1.Text+"%'");
           // dataGridView1.DataSource = result;
            Groups_Load(null, null);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[3] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[6] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[7])
            //{

            //    Search_All f4 = new Search_All("1", "Cus");
            //    f4.ShowDialog();

            //    try
            //    {
            //        dataGridView1.CurrentCell.Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
            //       // dataGridView1.CurrentRow.Cells[3].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
            //        //  dataGridView1.CurrentRow.Cells[1].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
            //        //  if (dataGridView1.CurrentRow.Cells[2].Value == null)
            //        //  {
            //        //     dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
            //        //   }

            //        //  dataGridView1.CurrentCell = this.dataGridView1[2, dataGridView1.CurrentRow.Index];
            //    }
            //    catch { }
            //}
        }
    }
}
