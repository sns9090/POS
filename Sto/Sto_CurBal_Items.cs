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
    public partial class Sto_CurBal_Items : BaseForm
    {
        BL.DAML daml = new BL.DAML();
       // SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        SqlConnection con1 = BL.DAML.con;// new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + BL.CLS_Session.sqldb + ";User id=" + BL.CLS_Session.sqluser + ";Password=sa123456;Connection Timeout=60");
        string item_no, item_name;
        public Sto_CurBal_Items(string itemno, string itemname)
        {
            InitializeComponent();
            item_no=itemno;
            item_name=itemname;
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

                        //int todel = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value);

                        //SqlDataAdapter da = new SqlDataAdapter("select * from sales_hdr where slcenter='" + todel+"' and branch='"+BL.CLS_Session.brno+"'" , con1);
                        //DataTable dt = new DataTable();
                        //da.Fill(dt);

                        //if (dt.Rows.Count >= 1)
                        //{
                        //    MessageBox.Show(" لا يمكن حذف مركز بيع فيه فواتير بيع ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}
                        //else
                        //{
                            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                            int execute = daml.Insert_Update_Delete_retrn_int("delete from whbins where item_no='"+textBox2.Text+"' and br_no='"+BL.CLS_Session.brno+"' and wh_no='"+dataGridView1.CurrentRow.Cells[1].Value+"'",false);


                            //sqlDataAdapter.Update(dataTable);
                        if(execute>0)
                            MessageBox.Show("تم الحذف بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                        else
                            MessageBox.Show("فشل الحذف");
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
            //sqlDataAdapter = new SqlDataAdapter("select a.br_no,a.wh_no,b.wh_name,a.openbal,a.openlcost from whbins a, whouses b where a.wh_no=b.wh_no and a.br_no='" + BL.CLS_Session.brno + "' and a.item_no='"+item_no+"' and b.wh_name like '%" + textBox1.Text + "%' order by wh_no", sqlConnection);
            double sumcost = 0, sumbal = 0;
            sqlConnection.Open();
            try
            {
                int exec = daml.Insert_Update_Delete_retrn_int("delete from whbins where br_no='" + BL.CLS_Session.brno + "' and item_no='" + textBox2.Text + "'", false);
                // for (int i = 0; i < dataGridView1.Rows.Count; i++)
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                   

                    if (!row.IsNewRow && row.Cells[0].Value != null)
                    {
                        sumbal = sumbal + Convert.ToDouble(row.Cells[3].Value);
                        sumcost = (Convert.ToDouble(row.Cells[3].Value) * Convert.ToDouble(row.Cells[4].Value)) / sumbal;

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO whbins(br_no, item_no,wh_no,openbal,openlcost) VALUES(@brno, @itemno,@whno,@opnbal,@opnlcost)", sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@brno", BL.CLS_Session.brno);
                            cmd.Parameters.AddWithValue("@itemno", textBox2.Text);
                            cmd.Parameters.AddWithValue("@whno", row.Cells[1].Value.ToString());
                            cmd.Parameters.AddWithValue("@opnbal", row.Cells[3].Value);
                            // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                            cmd.Parameters.AddWithValue("@opnlcost", row.Cells[4].Value);
                            // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                            /*
                            cmd.Parameters.AddWithValue("@a6", hdate);
                            cmd.Parameters.AddWithValue("@a7", row.Cells[0].Value);
                            cmd.Parameters.AddWithValue("@a8", row.Cells[4].Value);
                            cmd.Parameters.AddWithValue("@a9", row.Cells[5].Value);
                            cmd.Parameters.AddWithValue("@a10", row.Cells[2].Value);
                  
                             * cmd.Parameters.AddWithValue("@a11", row.Cells[3].Value);
                            cmd.Parameters.AddWithValue("@a12", row.HeaderCell.Value);
                            cmd.Parameters.AddWithValue("@a13", row.Cells[7].Value);
                            cmd.Parameters.AddWithValue("@a14", row.Cells[9].Value);
                            cmd.Parameters.AddWithValue("@a15", Convert.ToDouble(row.Cells[6].Value));
                            cmd.Parameters.AddWithValue("@a16", stwhno);
                            cmd.Parameters.AddWithValue("@a17", row.Cells[10].Value);
                             */
                            //con.Open();
                            //if (sqlConnection.State != ConnectionState.Open)
                            //{
                            //    sqlConnection.Open();
                            //}
                            cmd.ExecuteNonQuery();
                            //con.Close();
                            //if (sqlConnection.State == ConnectionState.Open)
                            //{
                            //    sqlConnection.Close();
                            //}
                        }
                    }

                }
                int exec2 = daml.Insert_Update_Delete_retrn_int("update  items set item_opencost="+sumcost+" ,item_obalance="+sumbal+"  where item_no='" + textBox2.Text + "'", false);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("تم حفظ التعديلات","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            sqlConnection.Close();
            this.Close();
            /*
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string StrQuery = "merge whbins as T"
                                + " using (select '" + dataGridView1.Rows[i].Cells[0].Value + "' as br_no,"
                                 + " '" + textBox2.Text + "' as item_no,"
                                 + " ' ' as unicode,"
                                + " '" + dataGridView1.Rows[i].Cells[1].Value + "' as wh_no,"
                                + " ' ' as bin_no,0 as qty,0 as rsvqty,"
                    //  + " '1' as pack,"

                                + " " + dataGridView1.Rows[i].Cells[3].Value + " as openbal,0 as lcost,0 as fcost," + dataGridView1.Rows[i].Cells[4].Value + " as openlcost,0 as openfcost,' ' as expdate from whbins where br_no='" + BL.CLS_Session.brno + "' and wh_no='" + dataGridView1.Rows[i].Cells[1].Value + "' and  item_no='" + textBox2.Text + "') as S"
                    // + " on T.barcode = S.barcode and"
                                + " on T.item_no = S.item_no and T.br_no=S.br_no and T.wh_no=S.wh_no"
                    //   + " T.report_date = S.report_date"
                                + " when matched then"
                                + " update set T.openbal = S.openbal ,T.openlcost = S.openlcost"
                                + " when not matched then"
                                + " insert (br_no, item_no,unicode,wh_no,bin_no,qty,rsvqty,openbal,lcost,fcost,openlcost,openfcost,expdate)"
                                + " values(S.br_no, S.item_no,S.unicode,S.wh_no,S.bin_no,S.qty,S.rsvqty,S.openbal,S.lcost,S.fcost,S.openlcost,S.openfcost,S.expdate);";
                             //   + " WHEN NOT MATCHED BY SOURCE"
                              //  + " THEN DELETE;";
                try
                {
                    //SqlConnection conn = new SqlConnection();


                    using (SqlCommand comm = new SqlCommand(StrQuery, sqlConnection))
                    {

                        comm.ExecuteNonQuery();
                        MessageBox.Show("تم حفظ التعديلات","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                       // Groups_Load(sender, e);
                    }

                  //  panel1.Enabled = false;
                  //  button2.Enabled = false;
                    //  textBox5.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            sqlConnection.Close();
             */
            /*
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
             */
        }

        private void Groups_Load(object sender, EventArgs e)
        {
            textBox2.Text = item_no;
            textBox3.Text = item_name;

            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 12);
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            // this.dataGridView1.Columns[0].Frozen = true;
            // TODO: This line of code loads data into the 'dataSet1.items' table. You can move, or remove it, as needed.
            //this.itemsTableAdapter.Fill(this.dataSet1.items);
          //  sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            sqlConnection = BL.DAML.con; // new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + BL.CLS_Session.sqldb + ";User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=60");
       
        
            // sqlConnection.Open();
         //   sqlDataAdapter = new SqlDataAdapter("select a.br_no,a.wh_no,b.wh_name,a.openbal+a.qty qty,case when (a.openbal + a.qty)<>0 then  round(((a.openlcost * a.openbal) + (a.lcost * a.qty))/ (a.openbal + a.qty),2) else 0 end cost from whbins a, whouses b where a.wh_no=b.wh_no and a.br_no='" + BL.CLS_Session.brno + "' and a.item_no='" + item_no + "' and b.wh_name like '%" + textBox1.Text + "%' order by wh_no", sqlConnection);
            //if(BL.CLS_Session.multi_bc)
            //   sqlDataAdapter = new SqlDataAdapter("select a.br_no,a.wh_no,b.wh_name,a.openbal+a.qty qty,case when (a.openbal + a.qty)<>0 then  round(((a.openlcost * a.openbal) + (a.lcost * a.qty))/ (a.openbal + a.qty),2) else a.lcost end cost from  whouses b left join whbins a on a.wh_no=b.wh_no and a.br_no=b.wh_brno where a.item_no='" + item_no + "' and b.wh_name like '%" + textBox1.Text + "%' order by wh_no", sqlConnection);
            //else
            //    sqlDataAdapter = new SqlDataAdapter("select a.br_no,a.wh_no,b.wh_name," + (BL.CLS_Session.multi_bc ? "(a.openbal+a.qty)" : " [dbo].[get_balanace_for_item] (a.br_no,a.wh_no,a.item_no,0)") + " qty,case when (a.openbal + a.qty)<>0 then  round(((a.openlcost * a.openbal) + (a.lcost * a.qty))/ (a.openbal + a.qty),2) else a.lcost end cost from  whouses b left join whbins a on a.wh_no=b.wh_no and a.br_no=b.wh_brno where a.item_no='" + item_no + "' and b.wh_name like '%" + textBox1.Text + "%' order by wh_no", sqlConnection);
            sqlDataAdapter = new SqlDataAdapter("select a.br_no,a.wh_no,b.wh_name," + (0 == 1 ? "(a.openbal+a.qty)" : " [dbo].[get_balanace_for_item] (a.br_no,a.wh_no,a.item_no,0)") + " qty, " + (BL.CLS_Session.up_sal_icost ? "case when (a.openbal + a.qty)<>0 then round(((a.openlcost * a.openbal) + (a.lcost * a.qty))/ (a.openbal + a.qty),2) else a.lcost end " : "0.00") + " cost from  whouses b left join whbins a on a.wh_no=b.wh_no and a.br_no=b.wh_brno where a.item_no='" + item_no + "' and b.wh_name like '%" + textBox1.Text + "%' order by wh_no", sqlConnection);

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
                dataGridView1.CurrentRow.Cells[3].ReadOnly = false;
                dataGridView1.CurrentRow.Cells[4].ReadOnly = false;
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
            try
            {
                //////if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[1]|| dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[2])
                //////{
                //////    Search_All f4 = new Search_All("4", "Acc");
                //////    f4.ShowDialog();

                //////     dataGridView1.CurrentRow.Cells[1].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                //////     dataGridView1.CurrentRow.Cells[2].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
                //////   // dataGridView1.CurrentCell.Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                //////}

                //if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[10] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[11])
                //{
                //    Search_All f4 = new Search_All("4", "Acc");
                //    f4.ShowDialog();
                //   // dataGridView1.CurrentRow.Cells[10].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                //    dataGridView1.CurrentCell.Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                //}
            }
            catch { }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Cells[0].Value = BL.CLS_Session.brno;

            if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[1])
            {

                DataTable dts = daml.SELECT_QUIRY_only_retrn_dt("Select wh_name from whouses where wh_no='" + dataGridView1.CurrentRow.Cells[1].Value+ "'");
                if (dts.Rows.Count > 0)
                    dataGridView1.CurrentRow.Cells[2].Value = dts.Rows[0][0].ToString();
                else
                {
                    dataGridView1.CurrentRow.Cells[1].Value = "";
                    dataGridView1.CurrentRow.Cells[2].Value = "";
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    int iColumn = dataGridView1.CurrentCell.ColumnIndex;
                    int iRow = dataGridView1.CurrentCell.RowIndex;
                    if (iColumn == dataGridView1.Columns.Count - 1)
                        dataGridView1.CurrentCell = dataGridView1[0, iRow + 1];
                    else
                        dataGridView1.CurrentCell = dataGridView1[iColumn + 1, iRow];
                }
            }
            catch { }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
    

