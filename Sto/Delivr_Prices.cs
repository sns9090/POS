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

namespace POS.Sto
{
    public partial class Delivr_Prices : BaseForm
    {
        SqlConnection con1 = BL.DAML.con;//
        string item_no, item_name,d_price,brc;
        public Delivr_Prices(string itemno, string itemname, string dprice, string dbrc)
        {
            InitializeComponent();
            item_no = itemno;
            item_name = itemname;
            d_price = dprice;
            brc = dbrc;
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

                SqlDataAdapter da = new SqlDataAdapter("select * from accounts where a_brno='" + todel + "'", con1);
                DataTable dt=new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
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
            string StrQuery;
          
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (BL.CLS_Session.price_type.Equals("1"))
                {
                    StrQuery = "merge brprices as t"
                                   + " using (select '" + dataGridView1.Rows[i].Cells[0].Value + "' as branch,"
                                   + " '' as slcenter,"
                                   + " '" + item_no + "' as itemno,"
                                 //  + " '" + (Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) == 0 ? d_price : dataGridView1.Rows[i].Cells[2].Value) + "' as lprice1, '" + brc + "' as barcode  where " + dataGridView1.Rows[i].Cells[2].Value + " <> " + d_price + " ) as s"
                                   + " '" + (Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) == 0 ? d_price : dataGridView1.Rows[i].Cells[2].Value) + "' as lprice1, '" + brc + "' as barcode ) as s"
                        // + " on T.barcode = S.barcode and"
                                   + " on t.branch = s.branch and t.slcenter=s.slcenter and t.itemno=s.itemno"
                        //   + " T.report_date = S.report_date"
                                   + " when matched then"
                                   + " update set t.lprice1 = s.lprice1"
                                   + " when not matched then"
                                   + " insert (branch, slcenter, itemno, lprice1,barcode)"
                                   + " values(s.branch, s.slcenter, s.itemno, s.lprice1,s.barcode);";
                }
                else
                {
                    StrQuery = "merge brprices as t"
                                       + " using (select '' as branch,"
                                       + " '" + dataGridView1.Rows[i].Cells[0].Value + "' as slcenter,"
                                       + " '" + item_no + "' as itemno,"
                                       + " '" + (Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) == 0 ? d_price : dataGridView1.Rows[i].Cells[2].Value) + "' as lprice1, '" + brc + "' as barcode ) as s"
                        // + " on T.barcode = S.barcode and"
                                       + " on t.branch = s.branch and t.slcenter=s.slcenter and t.itemno=s.itemno"
                        //   + " T.report_date = S.report_date"
                                       + " when matched then"
                                       + " update set t.lprice1 = s.lprice1"
                                       + " when not matched then"
                                       + " insert (branch, slcenter, itemno, lprice1,barcode)"
                                       + " values(s.branch, s.slcenter, s.itemno, s.lprice1,s.barcode);";
                }
                try
                {
                    //SqlConnection conn = new SqlConnection();


                    using (SqlCommand comm = new SqlCommand(StrQuery, con1))
                    {
                        if(con1.State==ConnectionState.Closed)
                            con1.Open();
                        comm.ExecuteNonQuery();

                    }

                    using (SqlCommand comm2 = new SqlCommand("update items set last_updt='" + DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false)) + "' where item_no='" + item_no + "'", con1))
                    {
                        if (con1.State == ConnectionState.Closed)
                            con1.Open();
                        comm2.ExecuteNonQuery();

                    }

                    ////panel1.Enabled = false;
                    ////button2.Enabled = false;
                    //  textBox5.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            con1.Close();

            //try
            //{
            //    sqlDataAdapter.Update(dataTable);
            MessageBox.Show("تم حفظ التعديلات","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            Groups_Load(sender, e);

            //}

            //catch (Exception exceptionObj)
            //{

            //    MessageBox.Show(exceptionObj.Message.ToString());

            //}
        }

        private void Groups_Load(object sender, EventArgs e)
        {
            textBox2.Text = item_no;
            textBox3.Text = item_name;
            
           //// this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 12);
           //// dataGridView1.Columns[0].ReadOnly = true;
           // this.dataGridView1.Columns[0].Frozen = true;
            // TODO: This line of code loads data into the 'dataSet1.items' table. You can move, or remove it, as needed.
            //this.itemsTableAdapter.Fill(this.dataSet1.items);
            sqlConnection = BL.DAML.con;//
            // sqlConnection.Open();
            if (BL.CLS_Session.price_type.Equals("1"))
                sqlDataAdapter = new SqlDataAdapter("select a.br_no,a.br_name,isnull(b.lprice1," + d_price + ") br_price from branchs a left join brprices b on a.br_no=b.branch and b.itemno='" + item_no + "' where a.br_name like '%" + textBox1.Text + "%' order by a.br_no", sqlConnection);
            else
            {
                sqlDataAdapter = new SqlDataAdapter("select a.sl_no br_no,a.sl_name br_name,isnull(b.lprice1," + d_price + ") br_price from slcenters a left join brprices b on a.sl_no=b.slcenter and b.itemno='" + item_no + "' where a.sl_no<>'' and a.sl_name like '%" + textBox1.Text + "%' order by a.sl_no", sqlConnection);
                dataGridView1.Columns[0].HeaderText = "رقم المركز";
                dataGridView1.Columns[1].HeaderText = "اسم المركز";
                dataGridView1.Columns[2].HeaderText = "سعر المركز";
            }
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
            //////if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[3])
            //////{

            //////    Search_All f4 = new Search_All("1", "Cus");
            //////    f4.ShowDialog();

            //////    try
            //////    {

            //////        dataGridView1.CurrentRow.Cells[3].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
            //////        //  dataGridView1.CurrentRow.Cells[1].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
            //////        //  if (dataGridView1.CurrentRow.Cells[2].Value == null)
            //////        //  {
            //////        //     dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
            //////        //   }

            //////        //  dataGridView1.CurrentCell = this.dataGridView1[2, dataGridView1.CurrentRow.Index];
            //////    }
            //////    catch { }
            //////}
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
