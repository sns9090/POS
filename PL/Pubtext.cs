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
    public partial class Pubtext : BaseForm
    {
        SqlConnection con1 = BL.DAML.con;//new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        public Pubtext()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (msg.WParam.ToInt32() == (int)Keys.Enter)
            {
                SendKeys.Send("{Tab}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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

              //  string todel = dataGridView1.CurrentRow.Cells[1].Value.ToString();

                //SqlDataAdapter da = new SqlDataAdapter("select * from suppliers where cu_class='" + todel + "'", con1);
                //DataTable dt = new DataTable();
                //da.Fill(dt);

                //if (dt.Rows.Count >= 1)
                //{
                //    MessageBox.Show(" لا يمكن حذف تصنيف لدية موردين مرتبطين ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sqlDataAdapter.Update(dataTable);
                MessageBox.Show("تم حفظ التعديلات","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                Groups_Load(sender, e);

                SqlDataAdapter dads = new SqlDataAdapter("select * from descs", BL.DAML.con);
                DataTable dtds = new DataTable();
                dads.Fill(dtds);
                BL.CLS_Session.dtdescs = dtds;

            }

            catch (Exception exceptionObj)
            {

                MessageBox.Show(exceptionObj.Message.ToString());

            }
        }

        private void Groups_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 12);
            // dataGridView1.Columns[0].ReadOnly = true;
           // dataGridView1.Columns[1].ReadOnly = true;
            // this.dataGridView1.Columns[0].Frozen = true;
            // TODO: This line of code loads data into the 'dataSet1.items' table. You can move, or remove it, as needed.
            //this.itemsTableAdapter.Fill(this.dataSet1.items);
            sqlConnection = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            // sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter("select desc_id,desc_text,desc_etext from descs where desc_text like '%" + textBox1.Text + "%' or desc_etext like '%" + textBox1.Text + "%' order by desc_id", sqlConnection);
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
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //DataRow[] result= dataTable.Select("br_name like '%"+textBox1.Text+"%'");
            // dataGridView1.DataSource = result;
            Groups_Load(null, null);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //////if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[3] )
                //////{

                //////    Search_All f4 = new Search_All("1-1", "Acc");
                //////    f4.ShowDialog();



                ////// //   dataGridView1.CurrentRow.Cells[3].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                //////    dataGridView1.CurrentCell.Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                //////    //  dataGridView1.CurrentRow.Cells[1].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
                //////    //  if (dataGridView1.CurrentRow.Cells[2].Value == null)
                //////    //  {
                //////    //     dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
                //////    //   }

                //////    //  dataGridView1.CurrentCell = this.dataGridView1[2, dataGridView1.CurrentRow.Index];
                //////}
                /*
                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[10] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[11])
                {
                    Search_All f4 = new Search_All("4");
                    f4.ShowDialog();
                   // dataGridView1.CurrentRow.Cells[10].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                    dataGridView1.CurrentCell.Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                }
                 */
            }
            catch { }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
          //  dataGridView1.CurrentRow.Cells[0].Value = BL.CLS_Session.brno;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.IsNewRow)
            {
                dataGridView1.CurrentRow.Cells[0].ReadOnly = false;
                dataGridView1.CurrentRow.Cells[1].ReadOnly = false;
                dataGridView1.CurrentRow.Cells[2].ReadOnly = false;
                // dataGridView1.CurrentRow.Cells[3].ReadOnly = false;
            }
            else
            {
                dataGridView1.CurrentRow.Cells[0].ReadOnly = true;
                //  dataGridView1.CurrentRow.Cells[1].ReadOnly = false;
                //   dataGridView1.CurrentRow.Cells[2].ReadOnly = false;
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value == null || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[0].Value.ToString()))
                dataGridView1.CurrentRow.Cells[0].Value = (Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString().Length == 1 ? "0" + (Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString() : (Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString();
        }
    }
}
    

