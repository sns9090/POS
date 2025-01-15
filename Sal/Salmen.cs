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

namespace POS.Sal
{
    public partial class Salmen : BaseForm
    {
       // SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        SqlConnection con1 = BL.DAML.con;// new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + BL.CLS_Session.sqldb + ";User id=" + BL.CLS_Session.sqluser + ";Password=sa123456;Connection Timeout=60");

        public Salmen()
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

                        int todel = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value);

                        SqlDataAdapter da = new SqlDataAdapter("select * from sales_hdr where slcode='" + todel + "' and branch='" + BL.CLS_Session.brno + "'", con1);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count >= 1)
                        {
                            MessageBox.Show(" لا يمكن حذف بائع مرتبط بحركات ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dataGridView1.Columns[1].ReadOnly = true;

            // this.dataGridView1.Columns[0].Frozen = true;
            // TODO: This line of code loads data into the 'dataSet1.items' table. You can move, or remove it, as needed.
            //this.itemsTableAdapter.Fill(this.dataSet1.items);
          //  sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            sqlConnection = BL.DAML.con; // new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + BL.CLS_Session.sqldb + ";User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=60");
       
        
            // sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter("select sp_brno,sp_id,sp_name,sp_short,sp_percent,sp_target,cashcrdit from sal_men where sp_brno='" + BL.CLS_Session.brno + "' and sp_name like '%" + textBox1.Text + "%' order by sp_id", sqlConnection);
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
              //  dataGridView1.CurrentRow.Cells[0].ReadOnly = false;
                dataGridView1.CurrentRow.Cells[1].ReadOnly = false;
                dataGridView1.CurrentRow.Cells[2].ReadOnly = false;
              //  dataGridView1.CurrentRow.Cells[4].ReadOnly = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //DataRow[] result= dataTable.Select("br_name like '%"+textBox1.Text+"%'");
            // dataGridView1.DataSource = result;
            Groups_Load(null, null);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {/*
            try
            {
                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[3] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[6] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[7] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[8] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[9])
                {

                    Search_All f4 = new Search_All("1", "Cus");
                    f4.ShowDialog();



                 //   dataGridView1.CurrentRow.Cells[3].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                    dataGridView1.CurrentCell.Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                    //  dataGridView1.CurrentRow.Cells[1].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
                    //  if (dataGridView1.CurrentRow.Cells[2].Value == null)
                    //  {
                    //     dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
                    //   }

                    //  dataGridView1.CurrentCell = this.dataGridView1[2, dataGridView1.CurrentRow.Index];
                }
                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[10] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[11])
                {
                    Search_All f4 = new Search_All("4", "Acc");
                    f4.ShowDialog();
                   // dataGridView1.CurrentRow.Cells[10].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                    dataGridView1.CurrentCell.Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                }
            }
            catch { }
            */
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Cells[0].Value = BL.CLS_Session.brno;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[1].Value == null || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[1].Value.ToString()))
                dataGridView1.CurrentRow.Cells[1].Value = (Convert.ToInt32(dataGridView1[1, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString().Length == 1 ? "0" + (Convert.ToInt32(dataGridView1[1, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString() : (Convert.ToInt32(dataGridView1[1, dataGridView1.CurrentRow.Index - 1].Value) + 1).ToString();
       
        }
    }
}
    

