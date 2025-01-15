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

namespace POS.Sto
{
    public partial class Taxs : BaseForm
    {
        SqlConnection con1 = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        public Taxs()
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

                SqlDataAdapter da = new SqlDataAdapter("select * from items where item_tax=" + todel, con1);
                DataTable dt=new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show(" لا يمكن حذف مجموعة مرتبطة باصناف ", "تنبيه",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 12);
            dataGridView1.Columns[0].ReadOnly = true;
           // this.dataGridView1.Columns[0].Frozen = true;
            // TODO: This line of code loads data into the 'dataSet1.items' table. You can move, or remove it, as needed.
            //this.itemsTableAdapter.Fill(this.dataSet1.items);
            sqlConnection = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            // sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter("select * from taxs order by tax_id", sqlConnection);
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
    }
}
