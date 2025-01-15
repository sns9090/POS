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
using System.Globalization;

namespace POS.Acc
{
    public partial class Periods : BaseForm
    {
        SqlConnection con1 = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        BL.Date_Validate datval = new BL.Date_Validate();
        public Periods()
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

                 
                        dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);


                        sqlDataAdapter.Update(dataTable);

                        MessageBox.Show("تم الحذف بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                   
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
               // sqlDataAdapter.Update(dataTable);
               // MessageBox.Show("تم حفظ التعديلات","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //  MessageBox.Show(datval.convert_to_yyyyDDdd(row.Cells["expdate"].Value.ToString()));

                    if ( row.Cells[0].Value != null)
                    {

                        string StrQuery = "merge taxperiods as T"
                                        + " using (select '' as prd_brno," + row.Cells[1].Value + " as prd_no,"
                            // + " '" + item_no + "' as item_no,"
                                        + " '" + row.Cells[2].Value + "' as prd_name,"
                                        + " '" + row.Cells[3].Value + "' as prd_lname,'"
                            //  + " " + row.Cells[4].Value + " as prd_start," + " " + row.Cells[5].Value + " as prd_end,"+ " " + row.Cells[6].Value + " as tax_percent,'" 
                                        + datval.convert_to_yyyyDDdd(row.Cells[4].Value.ToString()) + "' as prd_start,'"
                                        + datval.convert_to_yyyyDDdd(row.Cells[5].Value.ToString()) + "' as prd_end,"
                                        + row.Cells[6].Value + " as tax_percent) as S"
                            // + " on T.barcode = S.barcode and"
                                        + " on T.prd_brno = S.prd_brno and T.prd_no = S.prd_no"
                            //   + " T.report_date = S.report_date"
                                        + " when matched then"
                                        + " update set T.prd_name = S.prd_name,T.prd_lname = S.prd_lname,T.prd_start=S.prd_start,T.prd_end=S.prd_end,T.tax_percent=S.tax_percent"
                                        + " when not matched then"
                                        + " insert (prd_brno, prd_no,prd_name,prd_lname,prd_start,prd_end,tax_percent)"
                                        + " values(S.prd_brno, S.prd_no,S.prd_name,S.prd_lname,S.prd_start,S.prd_end,S.tax_percent);";

                        //SqlConnection conn = new SqlConnection();


                        using (SqlCommand comm = new SqlCommand(StrQuery, sqlConnection))
                        {
                            if(sqlConnection.State==ConnectionState.Closed)
                                sqlConnection.Open();

                            comm.ExecuteNonQuery();

                        }
                    }
                }
                MessageBox.Show("تم حفظ التعديلات", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
            dataGridView1.Columns[1].ReadOnly = true;
           // this.dataGridView1.Columns[0].Frozen = true;
            // TODO: This line of code loads data into the 'dataSet1.items' table. You can move, or remove it, as needed.
            //this.itemsTableAdapter.Fill(this.dataSet1.items);
            sqlConnection = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            // sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter("select prd_brno,prd_no,prd_name,prd_lname,CONVERT(VARCHAR(10), CAST(prd_start as date), 105) prd_start,CONVERT(VARCHAR(10), CAST(prd_end as date), 105) prd_end,tax_percent from taxperiods order by prd_no", sqlConnection);
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
                dataGridView1.CurrentRow.Cells[1].ReadOnly = false;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if ((e.ColumnIndex == 3 && e.RowIndex > -1 && e.RowIndex != dataGridView1.NewRowIndex) || (e.ColumnIndex == 4 && e.RowIndex > -1 && e.RowIndex != dataGridView1.NewRowIndex))
            //{
            //    //check whether this can be parsed as a date.
            //    string enteredVal = dataGridView1.CurrentCell.Value.ToString();
            //    DateTime dt;
            //    if (DateTime.TryParse(enteredVal, out dt))
            //    {
            //        dataGridView1.CurrentCell.Value = dt.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            //    }
            //    else
            //    {
            //        MessageBox.Show("Doh, that's not a date: " + enteredVal);
            //    }
            //}
        }
    }
}
