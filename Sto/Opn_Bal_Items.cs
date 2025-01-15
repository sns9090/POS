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
using System.Threading;

namespace POS.Sto
{
    public partial class Opn_Bal_Items : BaseForm
    {
        //MaskedTextBox masktxtbox;
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
       // SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        SqlConnection con1 = BL.DAML.con;// new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + BL.CLS_Session.sqldb + ";User id=" + BL.CLS_Session.sqluser + ";Password=sa123456;Connection Timeout=60");
        string item_no, item_name;
        public Opn_Bal_Items(string itemno,string itemname)
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

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (msg.WParam.ToInt32() == (int)Keys.Enter)
            {
                SendKeys.Send("{Tab}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

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
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                  //  MessageBox.Show(datval.convert_to_yyyyDDdd(row.Cells["expdate"].Value.ToString()));

                    if ( row.Cells[0].Value != null || !row.IsNewRow)
                    {
                        sumbal = sumbal + Convert.ToDouble(row.Cells[3].Value);
                        sumcost = (Convert.ToDouble(row.Cells[3].Value) * Convert.ToDouble(row.Cells[4].Value)) / sumbal;

                        string StrQuery = "merge whbins as T"
                                        + " using (select '" + row.Cells[0].Value + "' as br_no,"
                                        + " '" + item_no + "' as item_no,"
                                        + " '" + row.Cells[1].Value + "' as wh_no,"
                                        + " " + row.Cells[3].Value + " as openbal,"
                                      //  + " " + row.Cells[4].Value + " as openlcost,'" + (string.IsNullOrEmpty( row.Cells["expdate"].Value.ToString().Replace("-",""))? "" : datval.convert_to_yyyyDDdd(row.Cells["expdate"].Value.ToString()) ) + "' as expdate where " + row.Cells[3].Value + " <> 0 or " + row.Cells[4].Value + " <> 0  ) as S"
                           + " " + row.Cells[4].Value + " as openlcost,'" + (string.IsNullOrEmpty(row.Cells["expdate"].Value.ToString().Replace("-", "")) ? "" : datval.convert_to_yyyyDDdd(row.Cells["expdate"].Value.ToString())) + "' as expdate ) as S"
                                      // + " on T.barcode = S.barcode and"
                                        + " on T.item_no = S.item_no and T.br_no = S.br_no and T.wh_no=S.wh_no"
                            //   + " T.report_date = S.report_date"
                                        + " when matched then"
                                        + " update set T.openbal = S.openbal,T.openlcost = S.openlcost,T.expdate=S.expdate"
                                        + " when not matched then"
                                        + " insert (br_no, item_no,wh_no,openbal,openlcost,expdate)"
                                        + " values(S.br_no, S.item_no,S.wh_no,S.openbal,S.openlcost,S.expdate);";

                        //SqlConnection conn = new SqlConnection();


                        using (SqlCommand comm = new SqlCommand(StrQuery, sqlConnection))
                        {

                            comm.ExecuteNonQuery();

                        }
                    }
                }

               // int exec2 = daml.Insert_Update_Delete_retrn_int("update items set item_opencost=" + sumcost + " ,item_curcost=" + sumcost + ", item_cbalance=" + sumbal + ",item_obalance=" + sumbal + "  where item_no='" + item_no + "'", false);

               

                //new Thread(() =>
                //{
                //    using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
                //    {

                //        cmd.CommandType = CommandType.StoredProcedure;
                //        cmd.Connection = sqlConnection;
                //        cmd.Parameters.AddWithValue("@items_tran_tb", dti);
                //        // con.Open();
                //        cmd.ExecuteNonQuery();
                //        sqlConnection.Close();

                //        //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                //    }
                //}).Start();
               

                /*
                int exec = daml.Insert_Update_Delete_retrn_int("delete from whbins where br_no='" + BL.CLS_Session.brno + "' and item_no='" + textBox2.Text + "'", false);
                daml.Insert_Update_Delete_retrn_int("delete from whbins where br_no='" + BL.CLS_Session.brno + "' and item_no='" + textBox2.Text + "'", false);
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
                //      cmd.ExecuteNonQuery();
                //con.Close();
                //if (sqlConnection.State == ConnectionState.Open)
                //{
                //    sqlConnection.Close();
                //}
                //    }
                //  }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show("تم حفظ التعديلات","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            sqlConnection.Close();
            this.Close();

            DataTable dti = new DataTable();
            dti.Clear();
            dti.Columns.Add("item_no");
            dti.Columns.Add("item_qty");
            object[] o = { item_no, sumbal };
            dti.Rows.Add(o);
            Thread t = new Thread(() => thread1(dti));
            t.Start();

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

        public void thread1(DataTable dtb)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sqlConnection;
                    cmd.Parameters.AddWithValue("@items_tran_tb", dtb);
                    // con.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();

                    //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
            }
            catch { }
        }
        private void Groups_Load(object sender, EventArgs e)
        {
           //// this.maskedTextBox = new MaskedTextBox();

           //// this.maskedTextBox.Visible = false;


          ////  this.dataGridView1.Controls.Add(this.maskedTextBox);
            try
            {

                if (string.IsNullOrEmpty(item_no))
                    dataGridView1.ReadOnly = true;

                textBox2.Text = item_no;
                textBox3.Text = item_name;

                // this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 12);
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                // this.dataGridView1.Columns[0].Frozen = true;
                // TODO: This line of code loads data into the 'dataSet1.items' table. You can move, or remove it, as needed.
                //this.itemsTableAdapter.Fill(this.dataSet1.items);
                //  sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
                sqlConnection = BL.DAML.con; // new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + BL.CLS_Session.sqldb + ";User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=60");


                // sqlConnection.Open();
                // sqlDataAdapter = new SqlDataAdapter("select b.wh_brno,b.wh_no,b.wh_name,a.openbal,a.openlcost from  whbins a join whouses b on a.wh_no=b.wh_no and a.br_no=b.wh_brno where b.wh_brno='" + BL.CLS_Session.brno + "' and a.item_no='"+item_no+"' and b.wh_name like '%" + textBox1.Text + "%' order by b.wh_brno", sqlConnection);
                // sqlDataAdapter = new SqlDataAdapter("select a.br_no,a.br_name,isnull(b.lprice1,0) br_price from branchs a left join brprices b on a.br_no=b.branch and b.itemno='" + item_no + "' where a.br_name like '%" + textBox1.Text + "%' order by a.br_no", sqlConnection);
                sqlDataAdapter = new SqlDataAdapter("select b.wh_brno,b.wh_no,b.wh_name,isnull(a.openbal,0) openbal,isnull(a.openlcost,0) openlcost,(case when ltrim(rtrim(a.expdate))='' then '  -  -    ' else convert(varchar,cast(a.expdate as date),105) end )expdate from whouses b left join whbins a on a.wh_no=b.wh_no and a.br_no=b.wh_brno where b.wh_brno='" + BL.CLS_Session.brno + "' and a.item_no='" + item_no + "' and b.wh_name like '%" + textBox1.Text + "%' order by b.wh_brno", sqlConnection);

                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                dataTable = new DataTable();



                sqlDataAdapter.Fill(dataTable);


                bindingSource = new BindingSource();

                bindingSource.DataSource = dataTable;


                dataGridView1.DataSource = bindingSource;


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataTable dtn = daml.SELECT_QUIRY_only_retrn_dt("select wh_name from  whouses where wh_brno='" + BL.CLS_Session.brno + "' and wh_no='" + row.Cells[1].Value + "'");

                    row.Cells[2].Value = dtn.Rows[0][0].ToString();

                }

                //  dataGridView1.Select();
                //masktxtbox = new MaskedTextBox();

                //masktxtbox.Visible = false;

                //dataGridView1.Controls.Add(masktxtbox);
                //dataGridView1.CellBeginEdit += new DataGridViewCellCancelEventHandler(dataGridView1_CellBeginEdit);
                //dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
                //dataGridView1.Scroll += new ScrollEventHandler(dataGridView1_Scroll); 
                //masktxtbox.Mask = "##-##-####";
                //dataGridView1.CurrentRow.Cells["expdate"] = masktxtbox;
            }
            catch { }

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
                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[1]|| dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[2])
                {
                    Search_All f4 = new Search_All("4", "Acc");
                    f4.ShowDialog();

                     dataGridView1.CurrentRow.Cells[1].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                     dataGridView1.CurrentRow.Cells[2].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
                   // dataGridView1.CurrentCell.Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                }
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
                {
                    dataGridView1.CurrentRow.Cells[2].Value = dts.Rows[0][0].ToString();
                    dataGridView1.CurrentRow.Cells[3].Value = 0;
                    dataGridView1.CurrentRow.Cells[4].Value = 0;
                }
                else
                {
                    dataGridView1.CurrentRow.Cells[1].Value = "";
                    dataGridView1.CurrentRow.Cells[2].Value = "";
                }
            }

            ////////if (this.maskedTextBox.Visible && e.ColumnIndex == 5)
            ////////{
               


            ////////    this.dataGridView1.CurrentCell.Value = this.maskedTextBox.Text;

            ////////    this.maskedTextBox.Visible = false;

               

            ////////}

            //////if (dataGridView1.CurrentCell.ColumnIndex == 5)
            //////{
            //////    dataGridView1.CurrentCell.Value = editMBox.Text;
            //////    //editMBox.Text = "";
            //////}

            //if (masktxtbox.Visible)
            //{
            //    dataGridView1.CurrentCell.Value = masktxtbox.Text;
            //    masktxtbox.Visible = false;
            //}
        }

       
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        e.SuppressKeyPress = true;
            //        int iColumn = dataGridView1.CurrentCell.ColumnIndex;
            //        int iRow = dataGridView1.CurrentCell.RowIndex;
            //        if (iColumn == dataGridView1.Columns.Count - 1)
            //            dataGridView1.CurrentCell = dataGridView1[0, iRow + 1];
            //        else
            //            dataGridView1.CurrentCell = dataGridView1[iColumn + 1, iRow];
            //    }

            //    //if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells["expdate"])
            //    //{
            //    //    if (dataGridView1.CurrentCell.Value.ToString().Length == 2)
            //    //        dataGridView1.CurrentCell.Value = dataGridView1.CurrentCell.Value + "-";

            //    //    if (dataGridView1.CurrentCell.Value.ToString().Length == 5)
            //    //        dataGridView1.CurrentCell.Value = dataGridView1.CurrentCell.Value + "-";
            //    //}
            //}
            //catch { }
        }

        private void dataGridView1_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells["expdate"])
            //{
            //    DateTime rs;

            //    CultureInfo ci = new CultureInfo("en-US", false);

            //    if (!DateTime.TryParseExact(e.FormattedValue.ToString(), "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            //    {

            //        MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        // e.Cancel = true;
            //        this.dataGridView1.CurrentCell.Value = "  -  -    ";
            //        //  txt_mdate.Focus();

            //    }
            //}
           
            
            ////////if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells["expdate"])
            ////////{
            ////////  //  MessageBox.Show(Convert.ToDateTime(dataGridView1.CurrentRow.Cells["expdate"].Value).ToString() + "\r\n" + DateTime.Now.ToString());
            ////////    DateTime parsed;
            ////////    if (!DateTime.TryParse(e.FormattedValue.ToString(), out parsed))
            ////////    {
            ////////        this.dataGridView1.CurrentCell.Value = "  -  -    ";
            ////////       // this.dataGridView1.CancelEdit();
            ////////    }
            ////////    else
            ////////    {
            ////////        //if (Convert.ToDateTime(dataGridView1.CurrentCell.Value) < DateTime.Now)
            ////////        //{
            ////////        //    MessageBox.Show("يجب ان يكون تاريخ الانتهاء اكبر");
            ////////        //    this.dataGridView1.CurrentCell.Value = "  -  -    ";
            ////////        //    this.dataGridView1.CancelEdit();
            ////////        //}
            ////////    }
            ////////}

            if (dataGridView1.CurrentCell.ColumnIndex == 5)
            {
                DateTime rs;

                CultureInfo ci = new CultureInfo("en-US", false);

                if (!DateTime.TryParseExact(e.FormattedValue.ToString(), "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
                {

                    MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // e.Cancel = true;
                    this.dataGridView1.CurrentCell.Value = "  -  -    ";
                    //  txt_mdate.Focus();

                }
            }
        }

        private void dataGridView1_CellErrorTextChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells["expdate"])
            //{
            //    if (dataGridView1.CurrentCell.Value.ToString().Length == 2)
            //        dataGridView1.CurrentCell.Value = dataGridView1.CurrentCell.Value + "-";

            //    if (dataGridView1.CurrentCell.Value.ToString().Length == 5)
            //        dataGridView1.CurrentCell.Value = dataGridView1.CurrentCell.Value + "-";
            //}
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //masktxtbox.Mask = "##-##-####";
            //Rectangle rec = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            //masktxtbox.Location = rec.Location;
            //masktxtbox.Size = rec.Size;
            //masktxtbox.Text = "";

            //if (dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)
            //    masktxtbox.Text = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();

            //masktxtbox.Visible = true;

            ////////if (e.ColumnIndex == 5)
            ////////{
            ////////    this.maskedTextBox.Mask = "##-##-####";

            ////////    Rectangle rect =
            ////////       this.dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

            ////////    this.maskedTextBox.Location = rect.Location;

            ////////    this.maskedTextBox.Size = rect.Size;

            ////////    this.maskedTextBox.Text = "";

            ////////    if (this.dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)
            ////////    {

            ////////        this.maskedTextBox.Text = this.dataGridView1[e.ColumnIndex,
            ////////            e.RowIndex].Value.ToString();

            ////////    }

            ////////    this.maskedTextBox.Visible = true;
            ////////}

        }

       //// MaskedTextBox editMBox;

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            //if (masktxtbox.Visible)
            //{
            //    Rectangle rec = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, true);
            //    masktxtbox.Location = rec.Location;
            //}
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ////////if (dataGridView1.CurrentCell.ColumnIndex == 5)
            ////////{
            ////////    editMBox = new MaskedTextBox();
            ////////    // TextBox editBox = new TextBox();
            ////////    DataGridViewCell cell = dataGridView1.CurrentCell;
            ////////    editMBox.Parent = dataGridView1;
            ////////    editMBox.Location = dataGridView1.GetCellDisplayRectangle(cell.ColumnIndex,
            ////////                                  cell.RowIndex, false).Location;
            ////////    // editMBox.Size = editBox.Size;
            ////////    editMBox.Show();
            ////////    editMBox.Mask = "##-##-####";
            ////////    editMBox.BringToFront();
            ////////    // dataGridView1.CurrentCell.Value = editMBox.Text;
            ////////}
        }
    }
}
    

