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
    public partial class GRD_Enter : BaseForm
    {
        SqlConnection con1 = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        public GRD_Enter()
        {
            InitializeComponent();
        }

        private SqlConnection sqlConnection = null;

        private SqlDataAdapter sqlDataAdapter = null;

        private SqlCommandBuilder sqlCommandBuilder = null;

        private DataTable dataTable = null, dttrtyps, dtslctr;

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

                    int todel = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                    SqlDataAdapter da = new SqlDataAdapter("select * from items where item_unit=" + todel, con1);
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
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("لا يوجد اصناف", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   // cmb_ftwh.Focus();
                    return;

                }
                //  sqlDataAdapter.Update(dataTable);
                // MessageBox.Show("تم حفظ التعديلات","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                // Groups_Load(sender, e);
                //  foreach(DataRow drg in dataGridView1.Rows)
                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = dataGridView1.Rows.Count;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                   
                    string StrQuery = " MERGE ttk_dtl as t"
                                    + " USING (select '" + dataGridView1.Rows[i].Cells[1].Value.ToString().Trim() + "' as itemno, " + dataGridView1.Rows[i].Cells[5].Value.ToString().Trim() + " as ttqty,'" + dataGridView1.Rows[i].Cells[6].Value.ToString().Trim() + "' as note) as s"
                                    + " ON T.itemno=S.itemno and T.ttbranch='"+BL.CLS_Session.brno+"' and T.whno='"+cmb_ftwh.SelectedValue+"'"
                                    + " WHEN MATCHED THEN"
                        //  + " UPDATE SET T.ttqty = S.ttqty";
                                    + " UPDATE SET T.ttqty = S.ttqty,T.note = S.note;";
                    //   + " WHEN NOT MATCHED THEN"

                    daml.Insert_Update_Delete_retrn_int(StrQuery, false);
                    progressBar1.Increment(1);
                }
                MessageBox.Show("تم حفظ التعديلات", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                progressBar1.Visible = false;
            }

            catch (Exception exceptionObj)
            {

                MessageBox.Show(exceptionObj.Message.ToString());
                progressBar1.Visible = false;

            }
        }

        private void Groups_Load(object sender, EventArgs e)
        {
           // this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 12);
            // dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            // this.dataGridView1.Columns[0].Frozen = true;
            // TODO: This line of code loads data into the 'dataSet1.items' table. You can move, or remove it, as needed.
            //this.itemsTableAdapter.Fill(this.dataSet1.items);
          


            string sl = BL.CLS_Session.stkey.Replace(" ", "','").Remove(0, 2) + "'";
            DataTable dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select wh_no,wh_name from whouses where wh_brno + wh_no in(" + sl + ") and wh_brno='" + BL.CLS_Session.brno + "'");



            cmb_ftwh.DataSource = dtslctr;
            cmb_ftwh.DisplayMember = "wh_name";
            cmb_ftwh.ValueMember = "wh_no";
            cmb_ftwh.SelectedIndex = -1;


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
            //if (dataGridView1.CurrentRow.IsNewRow)
            //{
            //    dataGridView1.CurrentRow.Cells[0].ReadOnly = false;
            //}
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {

                Sto.Sto_ItemStmt_Exp f4a = new Sto.Sto_ItemStmt_Exp(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                f4a.MdiParent = ParentForm;
                f4a.Show();
            }
            if (e.KeyCode == Keys.F8)
            {
                //  SendKeys.Send("{.}");
                // SendKeys.Send("{.}");
                // var selectedCell = dataGridView1.SelectedCells[0];
                // do something with selectedCell...
                Search_All f4 = new Search_All("2", "Acc");
                f4.ShowDialog();
                //    dataGridView1.CurrentRow
                try
                {
                    string searchValue = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();

                   //// dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    //try
                    //{
                    dataGridView1.ClearSelection();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Equals(searchValue))
                        {
                          ////  row.Selected = true;
                          //   row.Cells[1].Selected = true;
                            dataGridView1.CurrentCell = row.Cells[1];
                            break;
                        }
                    }
                   


                    //dataGridView1.CurrentRow.Cells[0].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                    //dataGridView1.CurrentRow.Cells[1].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
                    //dataGridView1.CurrentRow.Cells[4].Value = 1.00;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }


            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmb_ftwh.SelectedIndex == -1)
                {
                    MessageBox.Show("يجب اختيار المخزن المجرود", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmb_ftwh.Focus();
                    return;

                }

                sqlConnection = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
                // sqlConnection.Open();
                DataTable dth = daml.SELECT_QUIRY_only_retrn_dt("select * from ttk_hdr where ttbranch='" + BL.CLS_Session.brno + "' and ttwhno='" + cmb_ftwh.SelectedValue + "'");
                if (dth.Rows.Count==0)
                {
                    MessageBox.Show("لا يوجد اصناف للجرد", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmb_ftwh.Focus();
                    return;

                }
                
                
                txt_mdate.Text = dth.Rows[0][2].ToString().Substring(6, 2) + dth.Rows[0][2].ToString().Substring(4, 2) + dth.Rows[0][2].ToString().Substring(0, 4);//datval.convert_to_ddDDyyyy(dth.Rows[0][2].ToString());
                txt_hdate.Text = dth.Rows[0][3].ToString().Substring(6, 2) + dth.Rows[0][3].ToString().Substring(4, 2) + dth.Rows[0][3].ToString().Substring(0, 4);//datval.convert_to_ddDDyyyy(dth.Rows[0][3].ToString());


                sqlDataAdapter = new SqlDataAdapter("select srl_no,itemno,name,mqty,unit_name unit,ttqty,note from ttk_dtl,units where ttk_dtl.pack0=units.unit_id and ttbranch='" + BL.CLS_Session.brno + "' and whno='" + cmb_ftwh.SelectedValue + "' order by srl_no", sqlConnection);
                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                dataTable = new DataTable();



                sqlDataAdapter.Fill(dataTable);


                bindingSource = new BindingSource();

                bindingSource.DataSource = dataTable;


                dataGridView1.DataSource = bindingSource;
                dataGridView1.Focus();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        //search datagrid
        private void button22_Click(object sender, EventArgs e)
        {
            string searchValue = "";// textBox2.Text;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                dataGridView1.ClearSelection();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        row.Selected = true;
                        //   row.Cells[0].Selected = true;
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int rowNumber = 1;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                // dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                if (r.IsNewRow) continue;
                //r.HeaderCell.Value = (e.RowIndex + 1).ToString();
                r.HeaderCell.Value = rowNumber.ToString();
                rowNumber = rowNumber + 1;
                // su=su+r.Cells[6].Value;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                XcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    if (dataGridView1.Columns[i - 1].Visible == true)
                    {
                        XcelApp.Cells[1, i] = "'" + dataGridView1.Columns[i - 1].HeaderText;
                    }
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Visible == true && !dataGridView1.Rows[i].IsNewRow)
                            // XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString().StartsWith("0") && dataGridView1.Rows[i].Cells[j].Value.ToString().Length > 1 ? "'" + dataGridView1.Rows[i].Cells[j].Value.ToString() : dataGridView1.Rows[i].Cells[j].Value.ToString() ;
                            XcelApp.Cells[i + 2, j + 1] = "'" + dataGridView1.Rows[i].Cells[j].Value.ToString().Trim();
                    }

                }
                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var fsn = new Search_by_No("04");

            string tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
            // MessageBox.Show(tr);
            dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('24')");
            fsn.comboBox1.DataSource = dttrtyps;
            fsn.comboBox1.DisplayMember = "tr_name";
            fsn.comboBox1.ValueMember = "tr_no";

            string sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";
            // MessageBox.Show(sl);
            dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select * from slcenters where sl_brno + sl_no in(" + sl + ") and sl_brno='" + BL.CLS_Session.brno + "'");
            fsn.cmb_salctr.DataSource = dtslctr;
            fsn.cmb_salctr.DisplayMember = "sl_name";
            fsn.cmb_salctr.ValueMember = "sl_no";
            //cmb_salctr.DataSource = dtslctr;
            //cmb_salctr.DisplayMember = "sl_name";
            //cmb_salctr.ValueMember = "sl_no"; 

           

           //// DataTable dttp = BL.CLS_Session.dttrtype.Copy();

           //// DataRow dtt = dttp.NewRow();
           //// dtt[0] = "24"; dtt[1] = "نسخ من عرض سعر"; dtt[2] = ""; dtt[3] = "";
            //////  dttp.Rows.Add(new { Text = "نسخ من عرض سعر", Value = "24" });
           //// dttp.Rows.Add(dtt);
            



            ////  fsn.cmb_salctr.DataSource = dtslctr;
            ////  fsn.cmb_salctr.DisplayMember = "sl_name";
            ////  fsn.cmb_salctr.ValueMember = "sl_no";
            ////  fsn.checkBox1.Visible = true;
            ////  fsn.checkBox1.Text = "برقم العقد";
            fsn.ShowDialog();

            DialogResult result = MessageBox.Show(BL.CLS_Session.lang.Equals("E")? "":"سيتم نسخ البيانات من عرض السعر وتجاهل الكميات السابقة بالجرد ولن يتم اعتماد الكميات المنسوخة الا بعد عملية الحفظ", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Fill_Data(fsn.cmb_salctr.SelectedValue.ToString(), fsn.comboBox1.SelectedValue.ToString(), fsn.textBox1.Text);
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

        private void Fill_Data(string slctr, string atype, string aref)
        {
            WaitForm wf = new WaitForm("جاري نسخ البيانات من عرض السعر .. يرجى الانتظار ...");
            wf.MdiParent = MdiParent;
            wf.Show();
            Application.DoEvents();
            try
            {
                DataTable dth = daml.SELECT_QUIRY_only_retrn_dt("select itemno,sum(qty*pkqty) sm from salofr_dtl where branch='" + BL.CLS_Session.brno + "' and slcenter='" + slctr + "' and invtype='" + atype + "' and ref in (" + aref + ") group by itemno");
                
                if (dth.Rows.Count == 0)
                {
                    MessageBox.Show("لا يوجد اصناف للجرد", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmb_ftwh.Focus();
                    return;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[5].Value = "0.0000";
                    foreach (DataRow dtrow in dth.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Trim().Equals(dtrow[0].ToString().Trim()))
                        {
                            // row.Selected = true;
                            row.Cells[5].Value = dtrow[1].ToString().Trim();
                            break;
                        }
                    }
                }

                wf.Close();
                MessageBox.Show("تم الاستيراد بنجاح", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch(Exception ex) 
            {
                wf.Close();
                MessageBox.Show(ex.Message , "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}

