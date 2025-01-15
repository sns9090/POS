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
    public partial class Adds : BaseForm
    {
        SqlConnection con1 = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        public Adds()
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

                    //int todel = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                    //SqlDataAdapter da = new SqlDataAdapter("select * from items where item_unit=" + todel, con1);
                    //DataTable dt = new DataTable();
                    //da.Fill(dt);

                    //if (dt.Rows.Count >= 1)
                    //{
                    //    MessageBox.Show(" لا يمكن حذف مجموعة مرتبطة باصناف ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //else
                    //{
                        dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);


                        sqlDataAdapter.Update(dataTable);

                        MessageBox.Show("تم الحذف بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    //}
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
                    ch1 = (DataGridViewCheckBoxCell)dataGridView1.Rows[dgr.Index].Cells[3];

                    if (ch1.Value == null)
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
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 12);
            dataGridView1.Columns[0].ReadOnly = true;
           // this.dataGridView1.Columns[0].Frozen = true;
            // TODO: This line of code loads data into the 'dataSet1.items' table. You can move, or remove it, as needed.
            //this.itemsTableAdapter.Fill(this.dataSet1.items);
            sqlConnection = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            // sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter("select * from Pos_Adds", sqlConnection);
            sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

            dataTable = new DataTable();

          // BL.CLS_Session.dtunits = daml.SELECT_QUIRY_only_retrn_dt("select * from units");
           // BL.CLS_Session.dtunits.Rows.Clear();
            sqlDataAdapter.Fill(dataTable);

          //  BL.CLS_Session.dtunits = dataTable;
            bindingSource = new BindingSource();

            bindingSource.DataSource = dataTable;


            dataGridView1.DataSource = bindingSource;

            


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Groups_Load(sender, e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.CurrentRow.IsNewRow)
            //{
            //    dataGridView1.CurrentRow.Cells[0].ReadOnly = false;
            //}
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.IsNewRow)
            {
                dataGridView1.CurrentRow.Cells[0].ReadOnly = false;
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value == null || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[0].Value.ToString()))
                dataGridView1.CurrentRow.Cells[0].Value = (Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString().Length == 1 ?  (Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString() : (Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString();

            //if (dataGridView1.CurrentRow.Cells[3].Value == null || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[3].Value.ToString()))
            //    dataGridView1.CurrentRow.Cells[3].Value = (Convert.ToInt32(dataGridView1[3, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString().Length == 1 ? (Convert.ToInt32(dataGridView1[3, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString() : (Convert.ToInt32(dataGridView1[3, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString();

            //if (dataGridView1.CurrentRow.Cells[4].Value == null || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
            //    dataGridView1.CurrentRow.Cells[4].Value = 1;
        }
    }
}
