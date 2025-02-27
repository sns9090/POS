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
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;

namespace POS.Sal
{
    public partial class Sal_Items_Offer : BaseForm
    {
        DataTable dt222, dtslctr, dtgrp;
        BL.DAML daml = new BL.DAML();
        SqlConnection con1 = BL.DAML.con;//new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        public Sal_Items_Offer()
        {
            InitializeComponent();
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
            try
            {

                int todel = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                // SqlDataAdapter da = new SqlDataAdapter("select * from accounts where a_brno=" + todel, con1);
                DataTable dt1 = daml.SELECT_QUIRY_only_retrn_dt("select count(*) from acc_dtl where a_key='" + todel + "'");
                DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select count(*) from accounts where a_key='" + todel + "' and a_opnbal<>0");
                DataTable dt = new DataTable();
                //  da.Fill(dt);

                // if (dt.Rows.Count >= 1)
                if (Convert.ToInt32(dt1.Rows[0][0]) == 0 && Convert.ToInt32(dt1.Rows[0][0]) == 0)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);


                    sqlDataAdapter.Update(dataTable);

                    MessageBox.Show("تم حذف الحساب بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                else
                {
                    MessageBox.Show(" لا يمكن حذف حساب لديه رصيد او حركة ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (cmb_salctr.SelectedIndex == -1)
                {
                    MessageBox.Show("يرجى اختيار مركز البيع", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmb_salctr.Focus();
                    return;
                }

                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("يرجى اختيار نوع العرض", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox1.Focus();
                    return;
                }

                double sumcost = 0, sumbal = 0;
                sqlConnection = BL.DAML.con;
                sqlConnection.Open();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow && row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        // sumbal = sumbal + Convert.ToDouble(row.Cells[3].Value);
                        // sumcost = (Convert.ToDouble(row.Cells[3].Value) * Convert.ToDouble(row.Cells[4].Value)) / sumbal;

                        string StrQuery = "merge items_offer as T"
                                        + " using (select '" + BL.CLS_Session.brno + "' as br_no,"
                                        + " '" + (cmb_salctr.SelectedIndex != -1 ? cmb_salctr.SelectedValue : "") + "' as sl_no,"
                                        + " '" + row.Cells[0].Value + "' as itemno,"
                                        + " '" + row.Cells[1].Value + "' as barcode,"
                                        + " " + row.Cells[4].Value + " as " + (comboBox1.SelectedIndex.ToString().Equals("1") ? " DiscountP," : " offer_price,") + ""
                                        + " " + row.Cells[5].Value + " as MinQty,"
                                        + " " + row.Cells[6].Value + " as MaxQty,"
                                        + " '" + row.Cells[7].Value.ToString().Replace("-", "").Substring(4, 4) + row.Cells[7].Value.ToString().Replace("-", "").Substring(2, 2) + row.Cells[7].Value.ToString().Replace("-", "").Substring(0, 2) + "' as StartDate,"
                                        + " '" + row.Cells[8].Value.ToString().Replace("-", "").Substring(4, 4) + row.Cells[8].Value.ToString().Replace("-", "").Substring(2, 2) + row.Cells[8].Value.ToString().Replace("-", "").Substring(0, 2) + "' as EndDate,"
                                        + " '" + (comboBox1.SelectedIndex != -1 ? comboBox1.SelectedIndex.ToString() : "0") + "' as disctype,"
                                        + " " + (Convert.ToBoolean(row.Cells[9].Value) == true ? "1" : "0") + " as InActive," + row.Cells[3].Value + " lprice1) as S"
                            // + " on T.barcode = S.barcode and"
                                        + " on T.br_no = S.br_no and T.sl_no = S.sl_no and T.barcode = S.barcode"
                            //   + " T.report_date = S.report_date"
                                        + " when matched then"
                                        + " update set " + (comboBox1.SelectedIndex.ToString().Equals("1") ? " T.DiscountP = S.DiscountP" : " T.offer_price = S.offer_price") + ",T.disctype = S.disctype,T.MinQty = S.MinQty, T.MaxQty = S.MaxQty,T.StartDate = S.StartDate,T.EndDate = S.EndDate,T.InActive = S.InActive,T.lastupdt=getdate()"
                                        + " when not matched then"
                                        + " insert (br_no,sl_no, itemno,barcode," + (comboBox1.SelectedIndex.ToString().Equals("1") ? " DiscountP" : " offer_price") + ",disctype,MinQty,MaxQty,StartDate,EndDate,InActive,lprice1)"
                                        + " values(S.br_no,S.sl_no, S.itemno,S.barcode," + (comboBox1.SelectedIndex.ToString().Equals("1") ? " S.DiscountP" : " S.offer_price") + ",S.disctype,S.MinQty,S.MaxQty,S.StartDate,S.EndDate,S.InActive,S.lprice1);";

                        //SqlConnection conn = new SqlConnection();


                        using (SqlCommand comm = new SqlCommand(StrQuery, sqlConnection))
                        {

                            comm.ExecuteNonQuery();

                        }
                    }
                }
                sqlConnection.Close();
                MessageBox.Show(" تم حفظ العروض بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                button1.Enabled = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                sqlConnection.Close();
            }
            //////        int exexcuted=0;
            //////        double val1 = 0,val2=0,res=0;
            //////        try
            //////        {
            //////           // sqlDataAdapter.Update(dataTable);
            //////            foreach (DataGridViewRow row in dataGridView1.Rows)
            //////            {
            //////                // SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from DB.dbo.sales_hdr", con2);
            //////                /*
            //////                SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from sales_hdr", con2);
            //////                DataSet dss = new DataSet();
            //////                daa.Fill(dss, "0");
            //////*/
            //////              //  val1 = Convert.ToDouble(row.Cells[2].Value);
            //////              //  val2 = Convert.ToDouble(row.Cells[3].Value);
            //////                res = Convert.ToDouble(row.Cells[2].Value) > 0 ? Convert.ToDouble(row.Cells[2].Value) * -1 : Convert.ToDouble(row.Cells[3].Value);

            //////                exexcuted = daml.Insert_Update_Delete_retrn_int("update accounts set a_opnbal=" + res + " where a_key='" + row.Cells[0].Value + "' and a_brno='" + BL.CLS_Session.brno + "'", false);


            //////            }
            //////            if (exexcuted > 0)
            //////                MessageBox.Show("تم التعديل بنجاح","", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);


            //////            Groups_Load(sender, e);

            //////}

            //////catch (Exception exceptionObj)
            //////{

            //////    MessageBox.Show(exceptionObj.Message.ToString());

            //////}
        }

        private void Groups_Load(object sender, EventArgs e)
        {
            dtgrp = daml.SELECT_QUIRY_only_retrn_dt("select group_id,group_name from groups where group_pid is null");
            // DataTable dt = new DataTable();
            // da.Fill(dt);


            cmb_grp.DataSource = dtgrp;
            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
            cmb_grp.DisplayMember = "group_name";
            cmb_grp.ValueMember = "group_id";
            cmb_grp.SelectedIndex = -1;

            dtpk.CustomFormat = "dd-MM-yyyy";
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            // dataGridView1.Columns[7].ReadOnly = true;
            // dataGridView1.Columns[8].ReadOnly = true;

            txt_start.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

            txt_end.Text = DateTime.Now.AddDays(365).ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

            string sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";

            dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select * from slcenters where sl_brno + sl_no in(" + sl + ") and sl_brno='" + BL.CLS_Session.brno + "'");
            cmb_salctr.DataSource = dtslctr;
            cmb_salctr.DisplayMember = "sl_name";
            cmb_salctr.ValueMember = "sl_no";
            cmb_salctr.SelectedIndex = -1;
            /*
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 12);
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            // this.dataGridView1.Columns[0].Frozen = true;
            // TODO: This line of code loads data into the 'dataSet1.items' table. You can move, or remove it, as needed.
            //this.itemsTableAdapter.Fill(this.dataSet1.items);
            sqlConnection = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            // sqlConnection.Open();
           // sqlDataAdapter = new SqlDataAdapter("select a_key,a_name,case  when a_opnbal >0 then a_opnbal when a_opnbal<=0 then 0 end a_d,case  when a_opnbal<0 then a_opnbal when a_opnbal >=0 then 0 end a_c from accounts where len(a_key)=9 and a_name like '%" + textBox1.Text + "%' or a_key like '" + textBox1.Text + "%' and len(a_key)=9 order by a_key", sqlConnection);
            sqlDataAdapter = new SqlDataAdapter("select a_key,a_name,case  when a_opnbal <0 then -a_opnbal when a_opnbal>=0 then 0 end a_c,case  when a_opnbal>0 then a_opnbal when a_opnbal <=0 then 0 end a_d from accounts where len(a_key)=9 and a_brno='" + BL.CLS_Session.brno + "' and a_name like '%" + textBox1.Text + "%' order by a_key", sqlConnection);
           
            sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

            dataTable = new DataTable();



            sqlDataAdapter.Fill(dataTable);


            bindingSource = new BindingSource();

            bindingSource.DataSource = dataTable;


            dataGridView1.DataSource = bindingSource;


            total();
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Groups_Load(sender, e);
            this.Close();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.IsNewRow)
            {
                dataGridView1.CurrentRow.Cells[0].ReadOnly = false;
                dataGridView1.CurrentRow.Cells[1].ReadOnly = false;
                //dataGridView1.CurrentRow.Cells[3].ReadOnly = false;
                // dataGridView1.CurrentRow.Cells[4].ReadOnly = false;
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
                //if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[3])
                //{

                //    Search_All f4 = new Search_All("1", "Acc");
                //    f4.ShowDialog();



                //    dataGridView1.CurrentRow.Cells[3].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                //    //  dataGridView1.CurrentRow.Cells[1].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
                //    //  if (dataGridView1.CurrentRow.Cells[2].Value == null)
                //    //  {
                //    //     dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
                //    //   }

                //    //  dataGridView1.CurrentCell = this.dataGridView1[2, dataGridView1.CurrentRow.Index];
                //}
                //if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4])
                //{
                //    Search_All f4 = new Search_All("4", "Acc");
                //    f4.ShowDialog();
                //    dataGridView1.CurrentRow.Cells[4].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                //}
            }
            catch { }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //if (dataGridView1.CurrentCell == dataGridView1[0, dataGridView1.CurrentRow.Index] || dataGridView1.CurrentCell == dataGridView1[1, dataGridView1.CurrentRow.Index])
            //  {
            // if(dataGridView1.CurrentRow.Cells[3].Value==null)
            //     dataGridView1.CurrentRow.Cells[3].Value=0;

            // if (dataGridView1.CurrentRow.Cells[4].Value == null)
            // //if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
            //     dataGridView1.CurrentRow.Cells[4].Value = 0;

            //// if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[5].Value.ToString()))
            // if (dataGridView1.CurrentRow.Cells[5].Value == null)
            //     dataGridView1.CurrentRow.Cells[5].Value = 0;

            //// if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[6].Value.ToString()))
            // if (dataGridView1.CurrentRow.Cells[6].Value == null)
            //     dataGridView1.CurrentRow.Cells[6].Value = 0;
            //}
            //if (dataGridView1.CurrentCell == dataGridView1[3, dataGridView1.CurrentRow.Index])
            //{
            //    dataGridView1.CurrentRow.Cells[2].Value = "0.00";
            //    //  dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];


            //}

            //// if (dataGridView1.CurrentCell == dataGridView1[3, dataGridView1.CurrentRow.Index] && Convert.ToInt32(dataGridView1.CurrentCell.Value) > 0)
            //if (dataGridView1.CurrentCell == dataGridView1[2, dataGridView1.CurrentRow.Index])
            //{
            //    dataGridView1.CurrentRow.Cells[3].Value = "0.00";
            //    // dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];

            //}

        }

        private void total()
        {
            try
            {
                //double totalc = 0, totald = 0;
                //foreach (DataGridViewRow row in dataGridView1.Rows)
                //{
                //    totalc = totalc + Convert.ToDouble(row.Cells[2].Value);
                //    totald = totald + Convert.ToDouble(row.Cells[3].Value);
                //}
                //txt_discount.Text = totalc.ToString();
                //txt_maxq.Text = totald.ToString();
            }
            catch { }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataTable dtchkbar = new DataTable();

                // DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                //  DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
                // dt.Clear();
                string quiryt;
                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[1])
                {
                    if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[1])
                    {
                        quiryt = "SELECT items.item_no, items.item_name, items.item_cost, b.price AS item_price, b.barcode AS item_barcode, b.pack AS item_unit, items.item_obalance,"
                                + "items.item_cbalance, items.item_group, items.item_image, items.item_req, items.item_tax, items.unit2, items.uq2, items.unit2p, items.unit3, items.uq3, items.unit3p,"
                                + "items.unit4, items.uq4, items.unit4p, items.item_ename, items.item_opencost, items.item_curcost, items.supno, items.note, items.last_updt, items.sgroup,"
                                + "items.price2, b.pk_qty FROM items INNER JOIN items_bc AS b ON b.item_no = items.item_no AND b.barcode = '" + dataGridView1.CurrentRow.Cells[1].Value + "' and b.sl_no='"+BL.CLS_Session.sl_prices+"'";
                    }
                    else //if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0])
                    {
                        // if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0])
                        // {
                        string firstColumnValue3 = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        //   string nextColumnValue = dataGridView1.Rows[rowindex].Cells[columnindex+1].Value.ToString();
                        dtchkbar = daml.SELECT_QUIRY_only_retrn_dt("select count(*) from items_bc where item_no='" + firstColumnValue3 + "'");
                        if (Convert.ToInt32(dtchkbar.Rows[0][0]) > 1)
                        {
                            Search_All ns = new Search_All("chkb", firstColumnValue3);
                            ns.ShowDialog();
                            // firstColumnValue3 = ns.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                            quiryt = "SELECT items.item_no, items.item_name, items.item_cost, b.price AS item_price, b.barcode AS item_barcode, b.pack AS item_unit, items.item_obalance,"
                               + "items.item_cbalance, items.item_group, items.item_image, items.item_req, items.item_tax, items.unit2, items.uq2, items.unit2p, items.unit3, items.uq3, items.unit3p,"
                               + "items.unit4, items.uq4, items.unit4p, items.item_ename, items.item_opencost, items.item_curcost, items.supno, items.note, items.last_updt, items.sgroup,"
                               + "items.price2, b.pk_qty FROM items INNER JOIN items_bc AS b ON b.item_no = items.item_no AND b.barcode = '" + ns.dataGridView1.CurrentRow.Cells[0].Value.ToString() + "' and b.sl_no='" + BL.CLS_Session.sl_prices + "'";

                            // quiryt = "select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img from items i join items_bc b on b.item_no=i.item_no and b.barcode='" + firstColumnValue3 + "' join taxs t on i.item_tax=t.tax_id";

                        }
                        else
                        {
                            //  string value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
                            //string currentcell = dataGridView1.CurrentCell.Value.ToString();
                            // string firstColumnValue = dataGridView1.Rows[0].Cells[0].Value.ToString();
                            //string firstColumnValue =Convert.ToString(dataGridView1.CurrentCell

                            //SqlDataAdapter da2 = new SqlDataAdapter("select * from items where item_no='" + firstColumnValue + "' or item_no='" + nextColumnValue + "'", con2);
                            //  SqlDataAdapter da2 = new SqlDataAdapter("select * from DB.dbo.items where item_no='" + firstColumnValue + "'", con2);



                            // SqlDataAdapter da23 = new SqlDataAdapter("select i.*,t.tax_percent from items i, taxs t where i.item_tax=t.tax_id and i.item_no='" + firstColumnValue3 + "'", con2);
                            // quiryt = "select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img from items i join items_bc b on b.item_no=i.item_no and i.item_no='" + firstColumnValue3 + "' join taxs t on i.item_tax=t.tax_id";
                            quiryt = "select *,1 pk_qty from items where rtrim(ltrim(item_no))='" + dataGridView1.CurrentRow.Cells[0].Value + "'";
                        }
                        //  }
                        //  quiryt = "select * from items where rtrim(ltrim(item_no))='" + dataGridView1.CurrentRow.Cells[0].Value + "'";
                    }
                    //  string item_bar = chk_usebarcode.Checked==false ? "  rtrim(ltrim(item_no))=" : "  rtrim(ltrim(item_barcode))=";
                    //  dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where " + item_bar + "'" + dataGridView1.CurrentRow.Cells[0].Value + "'");
                    dt222 = daml.SELECT_QUIRY_only_retrn_dt(quiryt);
                    if (dt222.Rows.Count > 0)
                    {
                        // DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
                        //  if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0] && dataGridView1.CurrentRow.Cells[2].Value==null)

                        //////if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0])
                        //////{

                        // dcombo.Name = dataGridView1.Columns["Column3"];
                        //  dcombo.Name = "Column3";
                        // dataGridView1.CurrentRow.Cells[1].Value = dt222.Rows[0][1];
                        dataGridView1.CurrentRow.Cells[0].Value = dt222.Rows[0][0];
                        dataGridView1.CurrentRow.Cells[1].Value = dt222.Rows[0][4];
                        dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][1];
                        dataGridView1.CurrentRow.Cells[3].Value = dt222.Rows[0][3];

                        // dataGridView1.CurrentCell = this.dataGridView1[4, dataGridView1.CurrentRow.Index];
                        //  SendKeys.Send("{UP}");
                        // if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[4].Value) == 0 || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
                        //     dataGridView1.CurrentRow.Cells[4].Value = "1.00";
                        //   dataGridView1.CurrentRow.Cells[0].Value = dt222.Rows[0][1];
                        //  dataGridView1.CurrentRow.Cells[4].Value = 1.00;
                        // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][0];
                        // DataView dv1 = dtunits.DefaultView;
                        // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                        // dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                        // DataTable dtNew = dv1.ToTable();
                        // MessageBox.Show(dtNew.Rows[0][1].ToString());
                        // dataGridView2.DataSource = dtNew;
                        /*
                        dcombo.DataSource = dal.SelectData_query("select * from orpacking where pack_id in('" + dt2.Rows[0][1] + "','" + dt2.Rows[0][2] + "','" + dt2.Rows[0][4] + "')", null);
                        dcombo.DisplayMember = "pkname";
                        dcombo.ValueMember = "pack_id";
                        */
                        //ممتاز

                        // dcombo.DataSource = dtNew;
                        // dcombo.DisplayMember = "unit_name";
                        // dcombo.ValueMember = "unit_id";

                        //  dcombo.DisplayIndex = 0;

                        //  dataGridView1.CurrentRow.Cells[3] = dcombo;
                        // dcombo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                        //if (e.RowIndex == dataGridView1.NewRowIndex)
                        //{
                        //    dataGridView1.Rows.Add(1);
                        //}

                        //  dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                        //if (isnew)
                        //    dataGridView1.CurrentRow.Cells[3].Value = dt222.Rows[0][5];

                        //dcombo.FlatStyle = FlatStyle.Flat;


                        //dataGridView1.CurrentRow.Cells[10].Value = dt222.Rows[0][11];
                        //dataGridView1.CurrentRow.Cells[11].Value = dt222.Rows[0]["item_curcost"];
                        //dataGridView1.CurrentRow.Cells["cur_bal"].Value = dt222.Rows[0]["item_cbalance"];
                        //dataGridView1.CurrentRow.Cells[1].Value = dt222.Rows[0]["item_barcode"];
                        //dataGridView1.CurrentRow.Cells[13].Value = dt222.Rows[0]["item_cost"];

                        // chk_shaml_tax.Enabled = false;

                        // dataGridView1.Rows.Add(1);

                        //row.HeaderCell.Value = "Row " + rowNumber;
                        //rowNumber = rowNumber + 1;

                        //   dcombo.dr

                        //   dataGridView1.CurrentRow.Cells[2].sele


                        //if (e.ColumnIndex == 2) // your combo column index
                        //{

                        //    dataGridView1.CurrentRow.Cells[2].Value = dt2.Rows[0][0];

                        //}

                        // dcombo.Items.Contains(dt.Rows[0][0]);

                        // dcombo.Tag = "pack_id";
                        //dataGridView1.AutoGenerateColumns = false;

                        //DataTable dt = new DataTable();
                        //dt.Columns.Add("Name", typeof(String));
                        //dt.Columns.Add("Money", typeof(String));
                        //dt.Rows.Add(new object[] { "Hi", 100 });
                        //dt.Rows.Add(new object[] { "Ki", 30 });

                        //DataGridViewComboBoxColumn money = new DataGridViewComboBoxColumn();
                        //var list11 = new List<string>() { "10", "30", "80", "100" };
                        //money.DataSource = list11;
                        //money.HeaderText = "Money";
                        //money.DataPropertyName = "Money";

                        //DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
                        //name.HeaderText = "Name";
                        //name.DataPropertyName = "Name";

                        //dataGridView1.DataSource = dt;
                        //dataGridView1.Columns.AddRange(name, money);

                        ////// }

                    }
                    else
                    {
                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                    }

                }


                //total();
                if (dataGridView1.CurrentRow.Cells[3].Value == null)
                    dataGridView1.CurrentRow.Cells[3].Value = 0;

                if (dataGridView1.CurrentRow.Cells[4].Value == null)
                    //if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
                    dataGridView1.CurrentRow.Cells[4].Value = 0;

                // if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[5].Value.ToString()))
                if (dataGridView1.CurrentRow.Cells[5].Value == null)
                    dataGridView1.CurrentRow.Cells[5].Value = 0;

                // if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[6].Value.ToString()))
                if (dataGridView1.CurrentRow.Cells[6].Value == null)
                    dataGridView1.CurrentRow.Cells[6].Value = 0;

                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4] && comboBox1.SelectedIndex==0)
                {
                    if (Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value) > Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value))
                    {
                        MessageBox.Show("غير مسموح بالخصم باكثر من سعر الصنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView1.CurrentRow.Cells[4].Value = 0;
                        dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[4];
                    }


                }

                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4] && comboBox1.SelectedIndex == 1)
                {
                    if (Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value) > (Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value)))
                    {
                        MessageBox.Show("غير مسموح بالخصم باكثر من سعر الصنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView1.CurrentRow.Cells[4].Value = 0;
                        dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[4];
                    }


                }

                if (dataGridView1.CurrentRow.Cells[7].Value==null)
                {
                    dataGridView1.CurrentRow.Cells[7].Value = txt_start.Text;
                }
                if (dataGridView1.CurrentRow.Cells[8].Value==null)
                {
                    dataGridView1.CurrentRow.Cells[8].Value = txt_end.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 3 || dataGridView1.CurrentCell.ColumnIndex == 4 || dataGridView1.CurrentCell.ColumnIndex == 5 || dataGridView1.CurrentCell.ColumnIndex == 6) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Load_date(string ser)
        {
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 12);
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            // this.dataGridView1.Columns[0].Frozen = true;
            // TODO: This line of code loads data into the 'dataSet1.items' table. You can move, or remove it, as needed.
            //this.itemsTableAdapter.Fill(this.dataSet1.items);
            sqlConnection = BL.DAML.con;//
            // sqlConnection.Open();
            // sqlDataAdapter = new SqlDataAdapter("select a_key,a_name,case  when a_opnbal >0 then a_opnbal when a_opnbal<=0 then 0 end a_d,case  when a_opnbal<0 then a_opnbal when a_opnbal >=0 then 0 end a_c from accounts where len(a_key)=9 and a_name like '%" + textBox1.Text + "%' or a_key like '" + textBox1.Text + "%' and len(a_key)=9 order by a_key", sqlConnection);
            sqlDataAdapter = new SqlDataAdapter("select a_key,a_name,case  when a_opnbal <0 then -a_opnbal when a_opnbal>=0 then 0 end a_c,case  when a_opnbal>0 then a_opnbal when a_opnbal <=0 then 0 end a_d from accounts where len(a_key)=9 and a_key like '" + ser + "%' order by a_key", sqlConnection);

            sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

            dataTable = new DataTable();



            sqlDataAdapter.Fill(dataTable);


            bindingSource = new BindingSource();

            bindingSource.DataSource = dataTable;


            dataGridView1.DataSource = bindingSource;


            total();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Load_date(txt_minq.Text);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8 && dataGridView1.ReadOnly == false)
                {
                    //  SendKeys.Send("{.}");
                    // SendKeys.Send("{.}");
                    // var selectedCell = dataGridView1.SelectedCells[0];
                    // do something with selectedCell...
                    Search_All f4 = new Search_All("2", "Acc");
                    f4.ShowDialog();
                    //    dataGridView1.CurrentRow

                    // SendKeys.Send("{.}");
                    // dataGridView1.Rows.Add(1);


                    dataGridView1.CurrentRow.Cells[0].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                    dataGridView1.CurrentRow.Cells[1].Value = f4.dataGridView1.CurrentRow.Cells["i_b"].Value;
                    dataGridView1.CurrentRow.Cells[2].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
                    dataGridView1.CurrentRow.Cells[3].Value = f4.dataGridView1.CurrentRow.Cells[2].Value;
                    //if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[4].Value) == 0 || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
                    //    dataGridView1.CurrentRow.Cells[4].Value = "1.00";
                    //dataGridView1.CurrentRow.Cells[5].Value = 1.00;
                    //if (dataGridView1.CurrentRow.Cells[2].Value == null)
                    //{
                    //    dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
                    //}


                    //////string item_bar = chk_usebarcode.Checked==false ? "  rtrim(ltrim(item_no))=" : "  rtrim(ltrim(item_barcode))=";
                    //////dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where " + item_bar + "'" + f4.dataGridView1.CurrentRow.Cells[0].Value + "'");
                    //////string item_bar = chk_usebarcode.Checked == false ? "  rtrim(ltrim(item_no))='" + f4.dataGridView1.CurrentRow.Cells[0].Value + "'" : "  rtrim(ltrim(item_barcode))='" + f4.dataGridView1.CurrentRow.Cells[1].Value + "'";
                    //////// dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where " + item_bar + "'" + f4.dataGridView1.CurrentRow.Cells[0].Value + "'");
                    ////////  dt222 = daml.SELECT_QUIRY_only_retrn_dt("select *,0 pk_qty from items where " + item_bar + "");
                    //////dt222 = daml.SELECT_QUIRY_only_retrn_dt("select *,0 pk_qty from items where rtrim(ltrim(item_no))='" + f4.dataGridView1.CurrentRow.Cells[0].Value + "'");

                    //////DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                    //////DataView dv1 = dtunits.DefaultView;
                    //////dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                    //////DataTable dtNew = dv1.ToTable();
                    //////dcombo.DataSource = dtNew;
                    //////dcombo.DisplayMember = "unit_name";
                    //////dcombo.ValueMember = "unit_id";
                    //////dcombo.Value = dt222.Rows[0][5];
                    //////dataGridView1.CurrentRow.Cells[3] = dcombo;
                    //////// dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                    //////dcombo.FlatStyle = FlatStyle.Flat;

                    //////dataGridView1.CurrentRow.Cells[10].Value = dt222.Rows[0][11];
                    //////dataGridView1.CurrentRow.Cells[11].Value = dt222.Rows[0]["item_curcost"];
                    //////dataGridView1.CurrentRow.Cells[1].Value = dt222.Rows[0]["item_barcode"];
                    //////dataGridView1.CurrentRow.Cells[13].Value = dt222.Rows[0]["item_cost"];
                    //////dataGridView1.CurrentRow.Cells["cur_bal"].Value = dt222.Rows[0]["item_cbalance"];
                    //if (dataGridView1.CurrentCell.RowIndex == dataGridView1.NewRowIndex)
                    //{
                    //   // dataGridView1.Rows.Add(1);
                    //    SendKeys.Send("{Down}");
                    //}

                    //chk_shaml_tax.Enabled = false;
                    //  dataGridView1.CurrentCell = this.dataGridView1[4, dataGridView1.CurrentRow.Index];



                    //  DataRow[] dtrvat= dtvat.Select("tax_id ='" + dt222.Rows[0][11] + "'");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                    //  txt_tax.Text = dtrvat[0][2].ToString();


                    //  DataRow[] dtrvat = dtvat.Select("tax_id ='" + dt222.Rows[0][11] + "'");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                    // dataGridView1.CurrentRow.Cells[7].Value = (Convert.ToDouble(dtrvat[0][2]) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)).ToString();

                    // DataTable dtNew = dv1.ToTable();
                    // dcombo.DataSource = dtNew;
                    //        dataGridView1.Rows.Add(1);
                    //row.HeaderCell.Value = "Row " + rowNumber;
                    //rowNumber = rowNumber + 1;

                    /*
                   dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
                   dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
                   dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                    */

                    //if (dataGridView1.CurrentRow.Cells[2].Value == null)
                    //{
                    //    dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
                    //}
                    //if (dataGridView1.CurrentRow.Cells[2].Value == null)
                    //{
                    //    dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
                    //}

                }

                if (e.KeyCode == Keys.F7)
                {

                    Sto.Sto_ItemStmt_Exp f4a = new Sto.Sto_ItemStmt_Exp(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    f4a.MdiParent = ParentForm;
                    f4a.Show();
                }

                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
                {
                    Sto.Item_Card ac = new Sto.Item_Card(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    ac.MdiParent = ParentForm;
                    ac.Show();
                }

                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F)
                {
                    Find_By_No fi = new Find_By_No();
                    // fi.MdiParent = ParentForm;
                    fi.ShowDialog();

                    string searchValue = fi.txt_itemno.Text;
                    // MessageBox.Show(searchValue);
                    // dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    try
                    {
                        dataGridView1.ClearSelection();

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[0].Value.ToString().Trim().Equals(searchValue))
                            {
                                ////  row.Selected = true;
                                //   row.Cells[1].Selected = true;
                                dataGridView1.CurrentCell = row.Cells[0];
                                dataGridView1.Select();
                                break;
                            }
                        }

                    }
                    catch (Exception exc)
                    {
                        //  MessageBox.Show(exc.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dataGridView1.Rows)
            {
                if (!dgvr.IsNewRow)
                {
                    if (Convert.ToDouble(txt_prcent.Text)!=0)
                    {
                        if (label2.Text.Equals("نسبة الخصم"))
                            dgvr.Cells[4].Value = Convert.ToDouble(txt_prcent.Text) > 0 ? Math.Round(Convert.ToDouble(dgvr.Cells[3].Value) - (Convert.ToDouble(dgvr.Cells[3].Value) * Convert.ToDouble(txt_prcent.Text) / 100), 2) : Math.Round(Convert.ToDouble(dgvr.Cells[3].Value) - Convert.ToDouble(txt_discount.Text), 2);
                        else
                            dgvr.Cells[4].Value = Convert.ToDouble(txt_prcent.Text);
                    }

                    if(Convert.ToDouble(txt_minq.Text)!=0)
                    dgvr.Cells[5].Value = txt_minq.Text;

                    if (Convert.ToDouble(txt_maxq.Text) != 0)
                    dgvr.Cells[6].Value = txt_maxq.Text;

                   // if (!txt_minq.Text.Equals("0"))
                    dgvr.Cells[7].Value = txt_start.Text;

                  //  if (!txt_minq.Text.Equals("0"))
                    dgvr.Cells[8].Value = txt_end.Text;

                    if (checkBox1.Checked)
                        dgvr.Cells[9].Value = true;
                }
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txt_prcent.Text) <= 100)
            {
                if (string.IsNullOrEmpty(txt_prcent.Text))
                    txt_prcent.Text = "0";

                if (Convert.ToDouble(txt_prcent.Text) > 0)
                {
                    txt_discount.Text = "0";
                    txt_discount.Enabled = false;
                }
                else
                {
                    txt_discount.Enabled = true;
                }
            }
            else
            {
                txt_prcent.Text = "0";
            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                txt_minq.Text = "1";
                txt_minq.Enabled = false;

                txt_maxq.Text = "0";
                txt_maxq.Enabled = false;

                dataGridView1.Columns[5].ReadOnly = true;
                dataGridView1.Columns[6].ReadOnly = true;
                dataGridView1.Columns[4].HeaderText = "سعر الخصم";
            }
            else
            {
                txt_minq.Text = "0";
                txt_maxq.Text = "0";
                txt_minq.Enabled = true;
                txt_maxq.Enabled = true;
                dataGridView1.Columns[5].ReadOnly = false;
                dataGridView1.Columns[6].ReadOnly = false;
                dataGridView1.Columns[4].HeaderText = "سعر الخصم";
                label2.Text = "نسبة الخصم";
            }

        }

        private void txt_start_Validating(object sender, CancelEventArgs e)
        {
            DateTime rs;

            CultureInfo ci = new CultureInfo("en-US", false);

            if (!DateTime.TryParseExact(this.txt_start.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            {

                MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                txt_start.Focus();

            }
            //if (Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) < Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.start_dt)) || Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) > Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.end_dt)))
            //{

            //    MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    e.Cancel = true;

            //}

        }

        private void txt_end_Validating(object sender, CancelEventArgs e)
        {
            DateTime rs;

            CultureInfo ci = new CultureInfo("en-US", false);

            if (!DateTime.TryParseExact(this.txt_end.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            {

                MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                txt_end.Focus();

            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[7] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[8])
            {
                //string DateFormat;
                //// DateFormat = dataGridView1.CurrentRow.Cells["DGV_PatientSessions_Date"].Value.ToString();
                //DateFormat = dataGridView1.CurrentCell.Value.ToString();
                //if (Regex.IsMatch(DateFormat, @"(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"))
                //{
                //    // MessageBox.Show("done");
                //}
                //else
                //{
                //    MessageBox.Show("value should match dd/MM/yyyy format");
                //}
            }
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 1 && dataGridView1.CurrentCell.Value != null))
            {

                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {

                    if (row.Index == this.dataGridView1.CurrentCell.RowIndex)

                    { continue; }

                    if (this.dataGridView1.CurrentCell.Value == null)

                    { continue; }

                    if (row.Cells[1].Value != null && row.Cells[1].Value.ToString() == dataGridView1.CurrentCell.Value.ToString())
                    {
                        // MessageBox.Show("غير مسموح تكرار الصنف","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                        MessageBox.Show("غير مسموح تكرار الصنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                       // dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);

                    }

                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (cmb_salctr.SelectedIndex == -1)
            {
                MessageBox.Show("يرجى اختيار مركز البيع", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_salctr.Focus();
                return;
            }

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("يرجى اختيار نوع العرض", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Focus();
                return;
            }

            if (comboBox1.SelectedIndex != -1)
            {
                string stitem = !string.IsNullOrEmpty(txt_srch.Text.Trim()) && label10.Text.Equals("باركود") ? " and a.barcode='" + txt_srch.Text + "' " : !string.IsNullOrEmpty(txt_srch.Text.Trim()) && label10.Text.Equals("رقم صنف") ? " and a.itemno='" + txt_srch.Text + "' " : "";

                // sqlConnection = BL.DAML.con; // new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + BL.CLS_Session.sqldb + ";User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=60");


                // sqlConnection.Open();
                // sqlDataAdapter = new SqlDataAdapter("select a.br_no,a.wh_no,b.wh_name,a.openbal,a.openlcost from whbins a, whouses b where a.wh_no=b.wh_no and a.br_no='" + BL.CLS_Session.brno + "' and a.item_no='" + item_no + "' and b.wh_name like '%" + textBox1.Text + "%' order by wh_no", sqlConnection);
                // sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                //  dataTable = new DataTable();



                //  sqlDataAdapter.Fill(dataTable);


                // bindingSource = new BindingSource();

                // bindingSource.DataSource = dataTable;


                //  dataGridView1.DataSource = bindingSource;
              //  dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select a.itemno,a.barcode,b.item_name,c.price," + (comboBox1.SelectedIndex.ToString().Equals("1") ? " a.DiscountP" : " a.offer_price") + " as offer_price,a.MinQty, a.MaxQty, CONVERT(VARCHAR(10), CAST(a.StartDate as date), 105) StartDate,CONVERT(VARCHAR(10), CAST(a.EndDate as date), 105) EndDate, a.InActive from items_offer a join items b on a.itemno=b.item_no join items_bc c on a.barcode=c.barcode where a.disctype='" + comboBox1.SelectedIndex + "' and a.sl_no='" + (cmb_salctr.SelectedIndex != -1 ? cmb_salctr.SelectedValue : "") + "' and a.br_no='" + BL.CLS_Session.brno + "' "+stitem+"");

                DataTable dtbl = daml.SELECT_QUIRY_only_retrn_dt("select a.itemno,a.barcode,b.item_name,c.price," + (comboBox1.SelectedIndex.ToString().Equals("1") ? " a.DiscountP" : " a.offer_price") + " as offer_price,a.MinQty, a.MaxQty, CONVERT(VARCHAR(10), CAST(a.StartDate as date), 105) StartDate,CONVERT(VARCHAR(10), CAST(a.EndDate as date), 105) EndDate, a.InActive from items_offer a join items b on a.itemno=b.item_no join items_bc c on a.barcode=c.barcode where a.disctype='" + comboBox1.SelectedIndex + "' and a.sl_no='" + (cmb_salctr.SelectedIndex != -1 ? cmb_salctr.SelectedValue : "") + "' and c.sl_no='" + (cmb_salctr.SelectedIndex != -1 ? cmb_salctr.SelectedValue : "") + "' and a.br_no='" + BL.CLS_Session.brno + "' " + stitem + " order by a.lastupdt");
                foreach (DataRow dtr in dtbl.Rows)
                {
                    dataGridView1.Rows.Add(dtr.ItemArray);
                }
                button5.Enabled = false;

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
        }

        private void cmb_salctr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_salctr.SelectedIndex = -1;
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // If any cell is clicked on the Second column which is our date Column  
            ////if (e.ColumnIndex == 7 || e.ColumnIndex == 8)
            ////{
            ////    //Initialized a new DateTimePicker Control  
            ////    dtpk = new DateTimePicker();

            ////    //Adding DateTimePicker control into DataGridView   
            ////    dataGridView1.Controls.Add(dtpk);

            ////    // Setting the format (i.e. 2014-10-10)  
            ////    dtpk.Format = DateTimePickerFormat.Short;

            ////    // It returns the retangular area that represents the Display area for a cell  
            ////    Rectangle oRectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

            ////    //Setting area for DateTimePicker Control  
            ////    dtpk.Size = new Size(oRectangle.Width, oRectangle.Height);

            ////    // Setting Location  
            ////    dtpk.Location = new Point(oRectangle.X, oRectangle.Y);

            ////    // An event attached to dateTimePicker Control which is fired when DateTimeControl is closed  
            ////    dtpk.CloseUp += new EventHandler(dtpk_CloseUp);

            ////    // An event attached to dateTimePicker Control which is fired when any date is selected  
            ////    dtpk.TextChanged += new EventHandler(dtpk_OnTextChange);

            ////    // Now make it visible  
            ////    dtpk.Visible = true;
            ////}


        }
        void dtpk_CloseUp(object sender, EventArgs e)
        {
            // Hiding the control after use   
           //// dtpk.Visible = false;
        }
        private void dtpk_OnTextChange(object sender, EventArgs e)
        {
            // Saving the 'Selected Date on Calendar' into DataGridView current cell  
          ////  dataGridView1.CurrentCell.Value = dtpk.Text.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                if (label2.Text.Equals("نسبة الخصم"))
                    label2.Text = "سعر ثابت";
                else
                    label2.Text = "نسبة الخصم";
            }
        }

        private void cmb_grp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_grp.SelectedIndex = -1;
            }
        }

        private void cmb_sgrp_Enter(object sender, EventArgs e)
        {
            try
            {
                cmb_sgrp.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select group_id,group_name from groups where group_pid='" + cmb_grp.SelectedValue + "' and group_pid is not null");
                // metroComboBox3.DataSource = dt2;
                //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
                // metroComboBox3.DisplayMember = "a";
                //  comboBox1.ValueMember = comboBox1.Text;
                cmb_sgrp.DisplayMember = "group_name";
                cmb_sgrp.ValueMember = "group_id";

            }
            catch { }
        }

        private void cmb_grp_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_sgrp_Enter(sender, e);
            cmb_sgrp.SelectedIndex = -1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select b.item_no itemno,c.barcode,b.item_name,c.price,0 as offer_price,1 MinQty, 0 MaxQty, '' StartDate,'' EndDate, 0 InActive from  items b join items_bc c on b.item_no=c.item_no where 0<>1 and c.sl_no='"+BL.CLS_Session.sl_prices+"'");
            panel2.Enabled = false;   
        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (label10.Text.Equals("باركود"))
                label10.Text = "رقم صنف";
            else
                label10.Text = "باركود";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string searchValue = txt_srch.Text;

                try
                {
                    if (label10.Text.Equals("باركود"))
                    {
                        dataGridView1.ClearSelection();

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value.ToString().Equals(searchValue))
                            {
                                ////  row.Selected = true;
                                //   row.Cells[1].Selected = true;
                                dataGridView1.CurrentCell = row.Cells[1];
                                dataGridView1.Select();
                                break;
                            }
                        }
                    }
                    else
                    {
                        dataGridView1.ClearSelection();

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[0].Value.ToString().Equals(searchValue))
                            {
                                ////  row.Selected = true;
                                //   row.Cells[1].Selected = true;
                                dataGridView1.CurrentCell = row.Cells[0];
                                dataGridView1.Select();
                                break;
                            }
                        }
                    }

                }
                catch //(Exception exc)
                {
                  //  MessageBox.Show(exc.Message);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string searchValue = txt_srch.Text;

            try
            {
                if (label10.Text.Equals("باركود"))
                {
                    dataGridView1.ClearSelection();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Equals(searchValue))
                        {
                            ////  row.Selected = true;
                            //   row.Cells[1].Selected = true;
                            dataGridView1.CurrentCell = row.Cells[1];
                            dataGridView1.Select();
                            break;
                        }
                    }
                }
                else
                {
                    dataGridView1.ClearSelection();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value.ToString().Equals(searchValue))
                        {
                            ////  row.Selected = true;
                            //   row.Cells[1].Selected = true;
                            dataGridView1.CurrentCell = row.Cells[0];
                            dataGridView1.Select();
                            break;
                        }
                    }
                }

            }
            catch //(Exception exc)
            {
               // MessageBox.Show(exc.Message);
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //int rowNumber = 1;
            //foreach (DataGridViewRow r in dataGridView1.Rows)
            //{
            //    // dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
            //    if (r.IsNewRow) continue;
            //    //r.HeaderCell.Value = (e.RowIndex + 1).ToString();
            //    r.HeaderCell.Value = rowNumber.ToString();
            //    rowNumber = rowNumber + 1;
            //    // su=su+r.Cells[6].Value;

            //}
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 1)
                return;
            /*
            Reports.Report_Form rrf = new Reports.Report_Form("DR", dtt);
            rrf.MdiParent = ParentForm;
            rrf.Show();
             */
            try
            {
                Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();

                /*
                DataSet ds1 = new DataSet();

                */
                DataTable dt = new DataTable();
                int cn = 1;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    /*
                    dt.Columns.Add(col.Name);
                    // dt.Columns.Add(col.HeaderText);
                     * */
                    if (col.Index <= 6)
                    {
                        dt.Columns.Add("c" + cn.ToString());
                        //MessageBox.Show("c" + cn.ToString());
                        cn++;
                    }
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        DataRow dRow = dt.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.ColumnIndex <= 6)
                            {
                                dRow[cell.ColumnIndex] = cell.Value;
                              //  MessageBox.Show(cell.Value.ToString()); 
                            }
                        }
                        dt.Rows.Add(dRow);
                    }
                }


                rf.reportViewer1.LocalReport.DataSources.Clear();
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sal.rpt.Sal_Items_Offer_rpt.rdlc";



                // ReportParameter[] parameters = new ReportParameter[2];
                // parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                // parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));

                // rf.reportViewer1.LocalReport.SetParameters(parameters);
                ReportParameter[] parameters = new ReportParameter[6];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                parameters[1] = new ReportParameter("fmdate", txt_start.Text);
                parameters[2] = new ReportParameter("tmdate", txt_end.Text);
                parameters[3] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[4] = new ReportParameter("txt", this.Text);
                parameters[5] = new ReportParameter("ofertype", dataGridView1.Columns[4].HeaderText);

                rf.reportViewer1.LocalReport.SetParameters(parameters);

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();

                //rf.reportViewer1.RefreshReport();
                //rf.MdiParent = ParentForm;
                //rf.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void cmb_salctr_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

    

