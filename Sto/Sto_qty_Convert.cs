using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;
using System.Globalization;
using GridPrintPreviewLib;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace POS.Sto
{
    public partial class Sto_qty_Convert : BaseForm
    {
        List<BL.CurrencyInfo> currencies = new List<BL.CurrencyInfo>();
        string inv_t, inv_r;
        int ref_max;
        double seso = 0;
       // DataTable dtg;
       // DataTable dt2,dtunits;
        DataTable dthdr, dtdtl, dt222, dtunits, dtsal, dtvat, dttrtyps, dtslctr, dtslctr2;
        string a_slctr, a_ref, a_type,strcash, strrcash, strrcrdt, strcrdt, strdsc, strtax, strcashr,stwhno,sttowhno;
        bool isnew,isupdate,notposted;
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate dtv = new BL.Date_Validate();
        SqlConnection con = BL.DAML.con;//new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public Sto_qty_Convert(string slctr, string atype, string aref)
        {
            InitializeComponent();
          //  this.KeyPreview = true;
            a_ref = aref;
            a_type = atype;
            a_slctr = slctr;
            //this.KeyPreview = true;
            //this.KeyPress += new KeyPressEventHandler(Control_KeyPress);

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectNextControl(ActiveControl, true, true, true, true);
                e.Handled = true;
            }
        }

        private const int CP_DISABLE_CLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CP_DISABLE_CLOSE_BUTTON;
                return cp;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete && (btn_Add.Enabled != true && btn_Edit.Enabled != true) && e.Modifiers != Keys.Shift)
                {
                    int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                    if (selectedIndex > -1)
                    {
                        dataGridView1.Rows.RemoveAt(selectedIndex);
                        total();
                    }

                }

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

          
          
            // if (e.KeyChar == (char)13)
            try
            {
                if (e.KeyCode == Keys.F8 && dataGridView1.ReadOnly==false)
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
                       // SendKeys.Send("{.}");
                       // dataGridView1.Rows.Add(1);
                       

                        dataGridView1.CurrentRow.Cells[0].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                        dataGridView1.CurrentRow.Cells[1].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
                        if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[3].Value) == 0 || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[3].Value.ToString()))
                            dataGridView1.CurrentRow.Cells[3].Value = "1.00";
                        dataGridView1.CurrentRow.Cells[4].Value = 1.00;
                        //if (dataGridView1.CurrentRow.Cells[2].Value == null)
                        //{
                        //    dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
                        //}


                        string item_bar = chk_usebarcode.Checked==false ? "  rtrim(ltrim(item_no))=" : "  rtrim(ltrim(item_barcode))=";
                        dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where " + item_bar + "'" + f4.dataGridView1.CurrentRow.Cells[0].Value + "'");
                   
                        DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                        DataView dv1 = dtunits.DefaultView;
                        dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                        DataTable dtNew = dv1.ToTable();
                        dcombo.DataSource = dtNew;
                        dcombo.DisplayMember = "unit_name";
                        dcombo.ValueMember = "unit_id";
                        dcombo.Value = dt222.Rows[0][5];
                        dataGridView1.CurrentRow.Cells[2] = dcombo;
                      // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                        dcombo.FlatStyle = FlatStyle.Flat;

                        dataGridView1.CurrentRow.Cells[7].Value = dt222.Rows[0]["item_curcost"];
                  //      dataGridView1.CurrentRow.Cells[5].Value = dt222.Rows[0]["item_curcost"];
                        dataGridView1.CurrentRow.Cells[5].ReadOnly = true;
                        dataGridView1.CurrentRow.Cells[8].Value = dt222.Rows[0]["item_barcode"];
                       // dataGridView1.CurrentRow.Cells[8].Value = dt222.Rows[0][11];
                        //if (dataGridView1.CurrentCell.RowIndex == dataGridView1.NewRowIndex)
                        //{
                        //   // dataGridView1.Rows.Add(1);
                        //    SendKeys.Send("{Down}");
                        //}

                        dataGridView1.CurrentCell = this.dataGridView1[2, dataGridView1.CurrentRow.Index];



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
                    catch { }
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
                //}
                //catch { }

            }
            catch { }
            //if (e.KeyCode == Keys.Enter)
            //{
            //    SelectNextControl(ActiveControl, false, false, false, false);
            //    e.Handled = false;
            //}



            //if (e.KeyCode == Keys.Enter)
            //{
            //    // var selectedCell = dataGridView1.SelectedCells[0];

            //    dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];

            //}


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            // if (e.Key == Key.Enter)


            // var selectedCell = dataGridView1.SelectedCells[0];
            // do something with selectedCell...


        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4])
                {
                    DataTable dtwhb = daml.SELECT_QUIRY_only_retrn_dt("select [dbo].[get_item_bal_by_whno] ('" + dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() + "','" + stwhno + "','" + BL.CLS_Session.brno + "')");
                    txt_whbal.Text = dtwhb.Rows[0][0].ToString();

                }
                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5] && (isnew || isupdate) && Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) == 0)
                {
                    // DataTable dtlp = daml.SELECT_QUIRY_only_retrn_dt("select [dbo].[get_lastprice_for_item] ('" + BL.CLS_Session.brno + "','" + dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() + "',0) lastp");
                    DataTable dtlp = daml.SELECT_QUIRY_only_retrn_dt("select isnull(case when lcost=0 then openlcost else lcost end,0) from whbins where br_no='" + BL.CLS_Session.brno + "' and item_no='" + dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() + "' and wh_no='" + cmb_frwh.SelectedValue + "'");
                    if (dtlp.Rows.Count > 0 )//&& Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) == 0)
                        dataGridView1.CurrentRow.Cells[5].Value = Convert.ToDouble(dtlp.Rows[0][0]) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value);
                }
                //if (dataGridView1.CurrentCell==dataGridView1.CurrentRow.Cells[1] || dataGridView1.CurrentCell==dataGridView1.CurrentRow.Cells[3] ||dataGridView1.CurrentCell==dataGridView1.CurrentRow.Cells[6])
                //{

                DataGridView dg = (DataGridView)sender;

                if (dg.CurrentCell.EditType == typeof(DataGridViewComboBoxEditingControl) && isnew)// && dataGridView1.CurrentRow.Index == dataGridView1.RowCount - 2 && Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) == 0)
                {
                    //dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where rtrim(ltrim(item_no))='" + dataGridView1.CurrentRow.Cells[0].Value + "'");

                    //DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                    //DataView dv1 = dtunits.DefaultView;
                    //dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                    //DataTable dtNew = dv1.ToTable();
                    //dcombo.DataSource = dtNew;
                    //dcombo.DisplayMember = "unit_name";
                    //dcombo.ValueMember = "unit_id";
                    //dcombo.Value = dt222.Rows[0][5];
                    //dataGridView1.CurrentRow.Cells[2] = dcombo;
                    //// dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                    //dcombo.FlatStyle = FlatStyle.Flat;

                    SendKeys.Send("{F4}");
                    //  dataGridView1.BeginEdit(true);
                    //  ((ComboBox)dataGridView1.EditingControl).DroppedDown = true;
                    SendKeys.Send("{Down}");
                    SendKeys.Send("{UP}");
                    // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                }

                if (dg.CurrentCell.EditType == typeof(DataGridViewComboBoxEditingControl) && isupdate)// && dataGridView1.CurrentRow.Index == dataGridView1.RowCount - 2 && Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) == 0)
                {
                    /*
                    DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                    //  DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
                    // dt.Clear();
                    dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where rtrim(ltrim(item_no))='" + dataGridView1.CurrentRow.Cells[0].Value + "'");
                  //  dataGridView1.CurrentRow.Cells[1].Value = dt222.Rows[0][1];
                    //   dataGridView1.CurrentRow.Cells[0].Value = dt222.Rows[0][1];
                  //  dataGridView1.CurrentRow.Cells[4].Value = 1.00;
                    // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][0];
                    DataView dv1 = dtunits.DefaultView;
                    // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                    dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                    DataTable dtNew = dv1.ToTable();

                    dcombo.DataSource = dtNew;
                    dcombo.DisplayMember = "unit_name";
                    dcombo.ValueMember = "unit_id";

                    dataGridView1.CurrentRow.Cells[2] = dcombo;
                   // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                    dcombo.FlatStyle = FlatStyle.Flat;
                    */
                    //if (dataGridView1.CurrentCell == dataGridView1[2, dataGridView1.CurrentRow.Index] && isupdate)
                    //{
                    /*
                        DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                        //  DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
                        // dt.Clear();
                        dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where rtrim(ltrim(item_no))='" + dataGridView1.CurrentRow.Cells[0].Value + "'");
                        //  dataGridView1.CurrentRow.Cells[1].Value = dt222.Rows[0][1];
                        //   dataGridView1.CurrentRow.Cells[0].Value = dt222.Rows[0][1];
                        //  dataGridView1.CurrentRow.Cells[4].Value = 1.00;
                        // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][0];
                        DataView dv1 = dtunits.DefaultView;
                        // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                        dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                        DataTable dtNew = dv1.ToTable();

                        dcombo.DataSource = dtNew;
                        dcombo.DisplayMember = "unit_name";
                        dcombo.ValueMember = "unit_id";

                        dataGridView1.CurrentRow.Cells[2] = dcombo;
                        dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];// dt222.Rows[0][5];
                        dcombo.FlatStyle = FlatStyle.Flat
                     */
                    //  }
                    SendKeys.Send("{F4}");
                    //  dataGridView1.BeginEdit(true);
                    //  ((ComboBox)dataGridView1.EditingControl).DroppedDown = true;
                    SendKeys.Send("{Down}");
                    SendKeys.Send("{UP}");

                }
                if (dataGridView1.CurrentCell == dataGridView1[6, dataGridView1.CurrentRow.Index])//.ReadOnly == true || dataGridView1[3, dataGridView1.CurrentRow.Index].ReadOnly == true || dataGridView1[6, dataGridView1.CurrentRow.Index].ReadOnly == true)
                {
                    // dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex + 1, dataGridView1.CurrentRow.Index];
                    // SendKeys.Send("{F4}");
                    // SendKeys.Send("{Down}");
                    SendKeys.Send("{Tab}");
                }

                //if (dataGridView1[1, dataGridView1.CurrentRow.Index].ReadOnly == true || dataGridView1[3, dataGridView1.CurrentRow.Index].ReadOnly == true || dataGridView1[6, dataGridView1.CurrentRow.Index].ReadOnly == true)
                //{
                //    // dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex + 1, dataGridView1.CurrentRow.Index];
                //    // SendKeys.Send("{F4}");
                //    // SendKeys.Send("{Down}");
                //    SendKeys.Send("{Tab}");
                //}
                // if( dataGridView1.CurrentCell ==dataGridView1[1, dataGridView1.CurrentRow.Index] || dataGridView1.CurrentCell ==dataGridView1[3, dataGridView1.CurrentRow.Index] || dataGridView1.CurrentCell ==dataGridView1[6, dataGridView1.CurrentRow.Index]);
                // if (dataGridView1.CurrentRow.Cells[e.ColumnIndex].ReadOnly)
                //  {
                //   dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex + 1, dataGridView1.CurrentRow.Index];
                //  SendKeys.Send("{F4}");
                //   SendKeys.Send("{Down}");
                //  SendKeys.Send("{Tab}");
                //e.SuppressKeyPress = true;
                //int iColumn = dataGridView1.CurrentCell.ColumnIndex;
                //int iRow = dataGridView1.CurrentCell.RowIndex;
                //if (iColumn == dataGridView1.Columns.Count - 1)
                //    dataGridView1.CurrentCell = dataGridView1[0, iRow + 1];
                //else
                //    dataGridView1.CurrentCell = dataGridView1[iColumn + 1, iRow];
                // }
                // SendKeys.Send("{ENTER}");

            }
            catch { }
          
        //  bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1); //Make sure the clicked row/column is valid.
        //var datagridview = sender as DataGridView;

        //// Check to make sure the cell clicked is the cell containing the combobox 
        //if(datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick)
        //{
        //    datagridview.BeginEdit(true);
        //    ((ComboBox)datagridview.EditingControl).DroppedDown = true;
        //}
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            try
            {

                DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                //  DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
                // dt.Clear();
                string item_bar = chk_usebarcode.Checked==false ? "  rtrim(ltrim(item_no))=" : "  rtrim(ltrim(item_barcode))=";
                dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where " + item_bar + "'" + dataGridView1.CurrentRow.Cells[0].Value + "'");
                // DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
                if (dt222.Rows.Count > 0)
                {
                    //  if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0] && dataGridView1.CurrentRow.Cells[2].Value==null)
                    if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0])
                    {

                        // dcombo.Name = dataGridView1.Columns["Column3"];
                        //  dcombo.Name = "Column3";
                        dataGridView1.CurrentRow.Cells[1].Value = dt222.Rows[0][1];
                        if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[3].Value) == 0 || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[3].Value.ToString()))
                            dataGridView1.CurrentRow.Cells[3].Value = "1.00";
                        //   dataGridView1.CurrentRow.Cells[0].Value = dt222.Rows[0][1];
                        //  dataGridView1.CurrentRow.Cells[4].Value = 1.00;
                        // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][0];
                        DataView dv1 = dtunits.DefaultView;
                        // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                        dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                        DataTable dtNew = dv1.ToTable();
                        // MessageBox.Show(dtNew.Rows[0][1].ToString());
                        // dataGridView2.DataSource = dtNew;
                        /*
                        dcombo.DataSource = dal.SelectData_query("select * from orpacking where pack_id in('" + dt2.Rows[0][1] + "','" + dt2.Rows[0][2] + "','" + dt2.Rows[0][4] + "')", null);
                        dcombo.DisplayMember = "pkname";
                        dcombo.ValueMember = "pack_id";
                        */
                        //ممتاز

                        dcombo.DataSource = dtNew;
                        dcombo.DisplayMember = "unit_name";
                        dcombo.ValueMember = "unit_id";

                        //  dcombo.DisplayIndex = 0;

                        dataGridView1.CurrentRow.Cells[2] = dcombo;
                        // dcombo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                        //if (e.RowIndex == dataGridView1.NewRowIndex)
                        //{
                        //    dataGridView1.Rows.Add(1);
                        //}

                        //  dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                       // if (isnew)
                            dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];

                        dataGridView1.CurrentRow.Cells[7].Value = dt222.Rows[0]["item_curcost"];
                     //   dataGridView1.CurrentRow.Cells[5].Value = dt222.Rows[0]["item_curcost"];
                        dataGridView1.CurrentRow.Cells[5].ReadOnly = true;
                        dataGridView1.CurrentRow.Cells[8].Value = dt222.Rows[0]["item_barcode"];
                        dcombo.FlatStyle = FlatStyle.Flat;

                        //  dataGridView1.CurrentRow.Cells[8].Value = dt222.Rows[0][11];


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

                    }
                    /*
                    if ((dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[6] && isnew) || (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[6] && isupdate))
                    {
                        if (Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) > Convert.ToDouble(BL.CLS_Session.item_dsc))
                        {
                            MessageBox.Show("تجاوزت الخصم المسموح");
                            dataGridView1.CurrentRow.Cells[6].Value = 0;
                        }
                    }
                     * */
                    //if ((dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4] && isnew) || (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4] && isupdate))
                    //{
                    //    if (BL.CLS_Session.sal_nobal.Equals("0") && Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value) > Convert.ToDouble(dataGridView1.CurrentRow.Cells["cur_bal"].Value))
                    //    {
                    //        MessageBox.Show("غير مسموح بتجاوز الرصيد");
                    //        dataGridView1.CurrentRow.Cells[4].Value = 0;
                    //        dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[4];
                    //        SendKeys.Send("{F2}");
                    //    }
                    //}

                    if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5])
                    {
                        if (string.IsNullOrEmpty(dataGridView1.CurrentCell.Value.ToString()))
                            dataGridView1.CurrentCell.Value = 0.00;
                    }

                    if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[2])
                    {
                        //DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                        //// dcombo.Name = dataGridView1.Columns["Column3"];
                        //// dcombo.Name = "Column3";
                        //dataGridView1.CurrentRow.Cells[2] = dcombo;
                        //dcombo.DataSource = dal.SelectData_query("select * from orpacking where pack_id in('" + dt.Rows[0][1] + "','" + dt.Rows[0][2] + "')", null);
                        //dcombo.DisplayMember = "pkname";
                        //dcombo.ValueMember = "pack_id";
                        //Select("

                        dataGridView1.CurrentRow.Cells[3].Value = dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][18].ToString()) ? dt222.Rows[0][19].ToString() : dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][15].ToString()) ? dt222.Rows[0][16].ToString() : dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][12].ToString()) ? dt222.Rows[0][13].ToString() : "1.00";

                        /*
                        if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[4].Value) == 0)
                            dataGridView1.CurrentRow.Cells[4].Value = "1.00";

                        if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[5].Value) == 0)
                         //   dataGridView1.CurrentRow.Cells[5].Value = dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][18].ToString()) ? dt222.Rows[0][20].ToString() : dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][15].ToString()) ? dt222.Rows[0][17].ToString() : dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][12].ToString()) ? dt222.Rows[0][14].ToString() : dt222.Rows[0][3].ToString();
                            dataGridView1.CurrentRow.Cells[5].Value = dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][5].ToString()) ? Convert.ToDouble(dt222.Rows[0][23].ToString()) : (dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][12].ToString()) ? (Convert.ToDouble(dt222.Rows[0][23].ToString()) * Convert.ToDouble(dt222.Rows[0][13].ToString())) : (dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][15].ToString()) ? (Convert.ToDouble(dt222.Rows[0][23].ToString()) * Convert.ToDouble(dt222.Rows[0][16].ToString())) : (Convert.ToDouble(dt222.Rows[0][23].ToString()) * Convert.ToDouble(dt222.Rows[0][19].ToString()))));
                */
                        if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[4].Value) == 0)
                            dataGridView1.CurrentRow.Cells[4].Value = "1.00";

                        if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[5].Value) == 0)
                            // dataGridView1.CurrentRow.Cells[5].Value = dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][18].ToString()) ? dt222.Rows[0][20].ToString() : dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][15].ToString()) ? dt222.Rows[0][17].ToString() : dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][12].ToString()) ? dt222.Rows[0][14].ToString() : dt222.Rows[0][3].ToString();
                            dataGridView1.CurrentRow.Cells[5].Value = dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][5].ToString()) ? Convert.ToDouble(dt222.Rows[0][23].ToString()) : (dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][12].ToString()) ? (Convert.ToDouble(dt222.Rows[0][23].ToString()) * Convert.ToDouble(dt222.Rows[0][13].ToString())) : (dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][15].ToString()) ? (Convert.ToDouble(dt222.Rows[0][23].ToString()) * Convert.ToDouble(dt222.Rows[0][16].ToString())) : (Convert.ToDouble(dt222.Rows[0][23].ToString()) * Convert.ToDouble(dt222.Rows[0][19].ToString()))));
                


                        // dcombo.Tag = "pack_id";

                    }

                    //  DataRow[] dtrvat = dtvat.Select("tax_id ='" + dt222.Rows[0][11] + "'");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                    //  dataGridView1.CurrentRow.Cells[7].Value = ((Convert.ToDouble(dtrvat[0][2])/100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)).ToString();

                    // var filteredData = dtvat.Select("tax_id='" + dt222.Rows[0][11] + "'").CopyToDataTable();
                    // txt_tax.Text = filteredData.Rows[0][2].ToString();


                    /*
                      DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
                    // dt.Clear();
                    dt = dal.SelectData_query("select itemno,pack0,pack1,pkqty1,pack2,pkqty2 from stunits where rtrim(ltrim(itemno))='" + dataGridView1.CurrentRow.Cells[0].Value + "'", null);
                    // DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
                    //  if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0] && dataGridView1.CurrentRow.Cells[2].Value==null)
                    if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0])
                    {

                      //   dcombo.Name = dataGridView1.Columns["Column3"];


                        dataGridView1.Columns["Column3"] = dcombo;
                   
                       // dataGridView1.Columns.(dcombo);
                        dcombo.DataSource = dal.SelectData_query("select * from orpacking where pack_id in('" + dt.Rows[0][1] + "','" + dt.Rows[0][2] + "','" + dt.Rows[0][4] + "')", null);
                        dcombo.DisplayMember = "pkname";
                        dcombo.ValueMember = "pack_id";

                        // dcombo.Tag = "pack_id";
                    }

                   */


                    // SendKeys.Send("{Tab}");
                    //        // SendKeys.Send("{UP}");
                    //if (dataGridView1.ColumnCount - 1 == e.ColumnIndex)  //if last column
                    //{
                    //    KeyEventArgs forKeyDown = new KeyEventArgs(Keys.Enter);
                    //    notlastColumn = false;
                    //    dataGridView1_KeyDown(dataGridView1, forKeyDown);
                    //}
                    //else
                    //{
                    //    SendKeys.Send("{up}");
                    //    SendKeys.Send("{right}");
                    //}


                    //SendKeys.Send("{Tab}");
                    //  SendKeys.Send("{up}");


                    try
                    {
                        if (e.ColumnIndex == dataGridView1.Columns.Count - 1)
                        {
                            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells[0];
                        }
                        else
                        {
                            SendKeys.Send("{UP}");
                            if (BL.CLS_Session.lang.Equals("E"))
                            {
                                if (dataGridView1.CurrentCell != dataGridView1.CurrentRow.Cells[4] && dataGridView1.CurrentCell != dataGridView1.CurrentRow.Cells[5])
                                SendKeys.Send("{right 2}");
                                else
                                    SendKeys.Send("{right 1}");
                                // SendKeys.Send("{right}");
                            }
                            else
                            {
                                if (dataGridView1.CurrentCell != dataGridView1.CurrentRow.Cells[4] && dataGridView1.CurrentCell != dataGridView1.CurrentRow.Cells[5])
                                SendKeys.Send("{left 2}");
                                else
                                    SendKeys.Send("{left 1}");
                                // SendKeys.Send("{left}");
                            }
                        }

                    }

                    catch { }
                }

                else
                {
                    Search_All f4 = new Search_All("2-2", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    // 
                    dataGridView1.CurrentRow.Cells[0].Value = "";
                    f4.ShowDialog();
                    try
                    {
                        // SendKeys.Send("{.}");
                        // dataGridView1.Rows.Add(1);


                        dataGridView1.CurrentRow.Cells[0].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                        dataGridView1.CurrentRow.Cells[1].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
                        if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[3].Value) == 0 || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[3].Value.ToString()))
                            dataGridView1.CurrentRow.Cells[3].Value = "1.00";
                        dataGridView1.CurrentRow.Cells[4].Value = 1.00;
                        //if (dataGridView1.CurrentRow.Cells[2].Value == null)
                        //{
                        //    dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
                        //}



                        dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where " + item_bar + "'" + f4.dataGridView1.CurrentRow.Cells[0].Value + "'");

                      //  DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                        DataView dv1 = dtunits.DefaultView;
                        dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                        DataTable dtNew = dv1.ToTable();
                        dcombo.DataSource = dtNew;
                        dcombo.DisplayMember = "unit_name";
                        dcombo.ValueMember = "unit_id";
                        dcombo.Value = dt222.Rows[0][5];
                        dataGridView1.CurrentRow.Cells[2] = dcombo;
                        // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                        dcombo.FlatStyle = FlatStyle.Flat;

                        dataGridView1.CurrentRow.Cells[7].Value = dt222.Rows[0]["item_curcost"];
                       // dataGridView1.CurrentRow.Cells[5].Value = dt222.Rows[0]["item_curcost"];
                        dataGridView1.CurrentRow.Cells[5].ReadOnly = true;
                        dataGridView1.CurrentRow.Cells[8].Value = dt222.Rows[0]["item_barcode"];
                        // dataGridView1.CurrentRow.Cells[8].Value = dt222.Rows[0][11];
                        //if (dataGridView1.CurrentCell.RowIndex == dataGridView1.NewRowIndex)
                        //{
                        //   // dataGridView1.Rows.Add(1);
                        //    SendKeys.Send("{Down}");
                        //}

                        dataGridView1.CurrentCell = this.dataGridView1[2, dataGridView1.CurrentRow.Index];



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
                    catch { }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
              //  dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                //if (!dataGridView1.CurrentRow.IsNewRow && isnew)
                //    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
            }
            
           

            // dataGridView1.Columns.Add(dcombo);
            /*


            //SqlConnection con = new SqlConnection(@"Data Source=RYD1-PC\INFOSOFT12;" +
            //                                      "Initial Catalog=DBASE;" +
            //                                      "User id=sa;" +
            //                                      "Password=infocic;");
            //if (e.KeyCode == Keys.Enter)
            //{
            //    // var selectedCell = dataGridView1.SelectedCells[0];

            //    dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];

            //}
            if (dataGridView1.CurrentCell == dataGridView1[0, dataGridView1.CurrentRow.Index])
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("select top 1 a_key a ,a_name b from accounts where a_active=1 and a_key = '" + dataGridView1.CurrentRow.Cells[0].Value + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // dataGridView1.DataSource = dt;
                    if (dataGridView1.CurrentRow.Cells[0].Value != null && dt.Rows.Count > 0)
                    {
                        dataGridView1.CurrentRow.Cells[0].Value = dt.Rows[0][0].ToString();
                        dataGridView1.CurrentRow.Cells[1].Value = dt.Rows[0][1].ToString();

                        if (dataGridView1.CurrentRow.Cells[2].Value == null)
                        {
                            dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
                        }

                        dataGridView1.CurrentCell = this.dataGridView1[1, dataGridView1.CurrentRow.Index];
                       // SendKeys.Send("{right}");
                      //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index-1];
                  
                    }
                    else
                    {
                        //  MessageBox.Show("not  found");
                        dataGridView1.CurrentRow.Cells[0].Value = "";
                      //  dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.CurrentRow.Index - 1];
                      //  SendKeys.Send("{UP}");
                      //  SendKeys.Send("{right}");

                       
                    }


                    //if (e.ColumnIndex == dataGridView1.Columns.Count - 1)
                    //{
                    //    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells[0];
                    //}
                    //else
                    //{
                    //SendKeys.Send("{UP}");
                    //SendKeys.Send("{right}");
                    //}

                   


                    //  else {  }

                }
                catch { }
            }

            try
            {
                if (e.ColumnIndex == dataGridView1.Columns.Count - 1)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells[0];
                }
                else
                {
                    SendKeys.Send("{UP}");
                    SendKeys.Send("{left}");
                }
            }
            catch { }
            //if (e.ColumnIndex == dataGridView1.Columns.Count - 1)
            //{
            //    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells[0];
            //}
            //else
            //{
            //    SendKeys.Send("{UP}");
             //   SendKeys.Send("{right}");
            //}
           // dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.NewRowIndex];

            //if (e.ColumnIndex == dataGridView1.Columns.Count - 1)
            //{
            //    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells[0];
            //}
            //else
            //{
              //  SendKeys.Send("{UP}");
                //SendKeys.Send("{right}");
            //}

            //if (dataGridView1.CurrentRow.Cells[2].Value == null)
            //{
            //    dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
            //}

            //   Form7 f7 = new Form7();
            //    f7.ShowDialog();

            //    dataGridView1.CurrentRow.Cells[0].Value = f7.dataGridView1.CurrentRow.Cells[0].Value;
            //    dataGridView1.CurrentRow.Cells[1].Value = f7.dataGridView1.CurrentRow.Cells[1].Value;
            //    //if (e.KeyCode == Keys.Oemplus)
            //    //{
            //    //    MessageBox.Show("+ pressed");
            //    //}
            //}
            //else
            //{
            //    // Enter key pressed
            //    MessageBox.Show("Enter pressed");
            //}
             */
          //  dataGridView1.CurrentRow.Cells[6].Value = Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value);
            total();
        }

        private void Acc_Tran_Load(object sender, EventArgs e)
        {
            if (!BL.CLS_Session.formkey.Contains("E122"))
            {
               // this.Close();
            }
            else
            {

                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Syria));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.UAE));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.SaudiArabia));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Tunisia));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Gold));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Yemen));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.nul));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Kuwait));
                currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Qatar));

                this.dataGridView1.CellValidated += new DataGridViewCellEventHandler(dataGridView1_CellValidated);


                dtunits = daml.SELECT_QUIRY_only_retrn_dt("select * from units");
                dtvat = daml.SELECT_QUIRY_only_retrn_dt("select * from taxs");
                // MessageBox.Show(dtv.convert_to_ddDDyyyy("20180526"));

                // dtg = dataGridView1.DataSource;

                // dataGridView1.Columns["SN"].ReadOnly = true;
                try
                {


                    txt_mdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
                    txt_hdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));

                    dataGridView1.TopLeftHeaderCell.Value = "م";

                    // MessageBox.Show(dtv.convert_to_yyyyDDdd(txt_mdate.Text));
                    // MessageBox.Show(dtv.convert_to_yyyyDDdd(BL.CLS_Session.start_dt));
                    // MessageBox.Show(BL.CLS_Session.end_dt.Substring(6, 4) + BL.CLS_Session.end_dt.Substring(3, 2) + BL.CLS_Session.end_dt.Substring(0, 2));
                    /*
                    comboBox1.DisplayMember = "Text";
                    comboBox1.ValueMember = "Value";

                    comboBox1.Items.Add(new { Text = "قيد عام", Value = "01" });
                    comboBox1.Items.Add(new { Text = "سند قبض", Value = "02" });
                    comboBox1.Items.Add(new { Text = "سند صرف", Value = "03" });
                     */

                    //comboBox1.DisplayMember = "Text";
                    //comboBox1.ValueMember = "Value";

                    //var items = new[] { new { Text = BL.CLS_Session.lang.Equals("E") ? "Cash Sale" : "مبيعات نقدية", Value = "04" }, new { Text = BL.CLS_Session.lang.Equals("E") ? "Credit Sale" : "مبيعات اجله", Value = "05" }, new { Text = BL.CLS_Session.lang.Equals("E") ? "ReCash Sale" : "مرتجع نقدي", Value = "14" }, new { Text = BL.CLS_Session.lang.Equals("E") ? "ReCredit Sale" : "مرتجع اجل", Value = "15" } };
                    //comboBox1.DataSource = items;
                    //comboBox1.SelectedIndex = -1;

                    string tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
                    // MessageBox.Show(tr);
                    dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('33')");
                   // dttrtyps2 = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('33')");



                    cmb_type.DataSource = dttrtyps;
                    cmb_type.DisplayMember = "tr_name";
                    cmb_type.ValueMember = "tr_no";
                    cmb_type.SelectedIndex = -1;


                    /*
                    dtsal = daml.SELECT_QUIRY_only_retrn_dt("Select * from slcenters where sl_brno="+BL.CLS_Session.brno);
                    cmb_salctr.DataSource = dtsal;  
                    cmb_salctr.DisplayMember = "sl_name";
                    cmb_salctr.ValueMember = "sl_no";
                    cmb_salctr.SelectedIndex = -1;
                    */

                    string sl = BL.CLS_Session.stkey.Replace(" ", "','").Remove(0, 2) + "'";
                    // MessageBox.Show(sl);
                    dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select wh_no,wh_name from whouses where wh_brno + wh_no in(" + sl + ") and wh_brno='" + BL.CLS_Session.brno + "'");
                    dtslctr2 = daml.SELECT_QUIRY_only_retrn_dt("select wh_no,wh_name from whouses where wh_brno + wh_no in(" + sl + ") and wh_brno='" + BL.CLS_Session.brno + "'");



                    cmb_frwh.DataSource = dtslctr;
                    cmb_frwh.DisplayMember = "wh_name";
                    cmb_frwh.ValueMember = "wh_no";
                    cmb_frwh.SelectedIndex = -1;

                    // dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select wh_no,wh_name from whouses where wh_brno + wh_no in(" + sl + ") and wh_brno='" + BL.CLS_Session.brno + "'");

                    //try
                    //{
                    //    var dv1 = new DataView();
                    //    dv1 = dtslctr.DefaultView;
                    //    // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                    //    dv1.RowFilter = "wh_no not in(" + cmb_wh.SelectedValue + ")";
                    //    DataTable dtNew = dv1.ToTable();
                    //    // MessageBox.Show(dtNew.Rows[0][1].ToString());
                    //    cmb_towh.DataSource = dtNew;

                    //    cmb_towh.DisplayMember = "wh_name";
                    //    cmb_towh.ValueMember = "wh_no";
                    //    cmb_towh.SelectedIndex = -1;
                    //}
                    //catch { }

                    cmb_towh.DataSource = dtslctr2;
                    cmb_towh.DisplayMember = "wh_name";
                    cmb_towh.ValueMember = "wh_no";
                    cmb_towh.SelectedIndex = -1;

                    // cmb_salctr.SelectedIndex = -1;

                    cmb_type.Enabled = false;
                    cmb_frwh.Enabled = false;
                    cmb_towh.Enabled = false;
                    txt_ref.Enabled = false;
                    txt_hdate.Enabled = false;
                    txt_mdate.Enabled = false;
                    txt_desc.Enabled = false;
                    txt_remark.Enabled = false;
                    txt_key.Enabled = false;
                    txt_custno.Enabled = false;
                    txt_des.Enabled = false;
                    txt_desper.Enabled = false;
                    dataGridView1.ReadOnly = true;

                    Fill_Data("", a_type, a_ref);
                    //  dataGridView1.AllowUserToAddRows = false;
                    //dataGridView1.Select();
                    // dataGridView1.BeginEdit(true);
                    // dataGridView1.Rows.Add(10); 

                    // this.Owner.Enabled = false;

                    //  Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                    string str2 = BL.CLS_Session.formkey;
                    string whatever = str2.Substring(str2.IndexOf("E122") + 5, 3);
                    if (whatever.Substring(0, 1).Equals("0"))
                        btn_Add.Visible = false;
                    if (whatever.Substring(1, 1).Equals("0"))
                        btn_Edit.Visible = false;
                    if (whatever.Substring(2, 1).Equals("0"))
                        btn_Del.Visible = false;

                    if (BL.CLS_Session.up_us_post == false)
                        btn_Post.Visible = false;
                }
                catch { }
            }
        }

        void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (chk_nodup.Checked)
            {
                if (e.ColumnIndex == 0 && dataGridView1.CurrentCell.Value != null)
                {

                    foreach (DataGridViewRow row in this.dataGridView1.Rows)
                    {

                        if (row.Index == this.dataGridView1.CurrentCell.RowIndex)

                        { continue; }

                        if (this.dataGridView1.CurrentCell.Value == null)

                        { continue; }

                        if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == dataGridView1.CurrentCell.Value.ToString())
                        {
                            MessageBox.Show("غير مسموح تكرار الصنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                           // dataGridView1.CurrentCell.Value = null;

                        }

                    }

                }
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {/*
            if (dataGridView1.CurrentRow.Cells[2].Value == null)
            {
                dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
            }

         //   if (dataGridView1.CurrentCell == dataGridView1[4, dataGridView1.CurrentRow.Index] && Convert.ToInt32(dataGridView1.CurrentCell.Value) > 0)
            if (dataGridView1.CurrentCell == dataGridView1[4, dataGridView1.CurrentRow.Index])
            {
                dataGridView1.CurrentRow.Cells[3].Value = "0";
              //  dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];


            }

           // if (dataGridView1.CurrentCell == dataGridView1[3, dataGridView1.CurrentRow.Index] && Convert.ToInt32(dataGridView1.CurrentCell.Value) > 0)
            if (dataGridView1.CurrentCell == dataGridView1[3, dataGridView1.CurrentRow.Index])
            {
                dataGridView1.CurrentRow.Cells[4].Value = "0";
               // dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];

            }
            */

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show((dataGridView1.Rows.Count-1).ToString());

            // dataGridView1.CurrentRow.Cells[2].Value=ConvertNumeralsToArabic(dataGridView1.CurrentRow.Cells[2].Value.ToString());
        }

        //private void Control_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    //your code

        public static string ConvertNumeralsToArabic(string input)
        {

            return input = input.Replace('٠', '0')

                        .Replace('۱', '1')

                        .Replace('۲', '2')

                        .Replace('۳', '3')

                        .Replace('٤', '4')

                        .Replace('۵', '5')

                        .Replace('٦', '6')

                        .Replace('٧', '7')

                        .Replace('٨', '8')

                        .Replace('٩', '9');

        }

        private void dataGridView1_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            int rowNumber = 1;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                // dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                if (r.IsNewRow) continue;
                //r.HeaderCell.Value = (e.RowIndex + 1).ToString();
                r.HeaderCell.Value = rowNumber.ToString();
                rowNumber = rowNumber + 1;

                //if (r.Cells[2].Value == null)
                //{
                //    r.Cells[2].Value = txt_desc.Text;
                //}
                //if (dataGridView1.CurrentRow.Cells[2].Value == null)
                //{
                //    dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
                //}

            }
            total();

           
            //try
            //{
            //    if (dataGridView1.CurrentRow.Cells[2].Value==null)
            //    {
            //        dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
            //    }
            //}
            //catch { }

            // dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //dataGridView1.Rows[e.RowIndex].Cells["SN"].Value = (e.RowIndex + 1).ToString();
            //  dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
        }

        private void Add_btn_Click(object sender, EventArgs e)
        {

        }

        private void txt_amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }



            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            //{
            //    e.Handled = true;
            //}
        }

        private void txt_amt_TextChanged(object sender, EventArgs e)
        {
            //if (System.Text.RegularExpressions.Regex.IsMatch(txt_amt.Text, "  ^ [0-9]"))
            //{
            //    txt_amt.Text = "0.0";
            //}

            if (txt_temp.Text.Trim() == "")
            {
                txt_temp.Text = "0";
            }
        }


        private string convert_to_mdate(string input)
        {
            input = (txt_hdate.Text.Replace("-", "").Substring(4, 4)) + "-" + txt_hdate.Text.Replace("-", "").Substring(2, 2) + "-" + txt_hdate.Text.Replace("-", "").Substring(0, 2);

            DateTime dt = Convert.ToDateTime(input, new CultureInfo("ar-SA", false));
            //  DateTime dt = Convert.ToDateTime(txt_mdate.Text.ToString("yyyy-MM-dd"), new CultureInfo("en-US"));
            // txt_hdate.Text = dt.ToString("ddMMyyyy", new CultureInfo("ar-SA"));

            input = dt.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

            return input;
           
        }

        private string convert_to_hdate(string input)
        {
            input = (txt_mdate.Text.Replace("-", "").Trim().Substring(4, 4)) + "-" + txt_mdate.Text.Replace("-", "").Substring(2, 2) + "-" + txt_mdate.Text.Replace("-", "").Substring(0, 2);

            DateTime dt = Convert.ToDateTime(input, new CultureInfo("en-US", false));
            //  DateTime dt = Convert.ToDateTime(txt_mdate.Text.ToString("yyyy-MM-dd"), new CultureInfo("en-US"));
            // txt_hdate.Text = dt.ToString("ddMMyyyy", new CultureInfo("ar-SA"));

            input = dt.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));

            return input;

        }

        private void txt_mdate_Validating(object sender, CancelEventArgs e)
        {
            DateTime rs;

            CultureInfo ci = new CultureInfo("en-US", false);

            if (!DateTime.TryParseExact(this.txt_mdate.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            {

                MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;

            }
            if (Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) < Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.start_dt)) || Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) > Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.end_dt)))
            {

                MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;

            }

            //if (Convert.ToInt32(txt_mdate.Text.Substring(6, 4) + txt_mdate.Text.Substring(3, 2) + txt_mdate.Text.Substring(0, 2)) < Convert.ToInt32(BL.CLS_Session.start_dt.Substring(6, 4) + BL.CLS_Session.start_dt.Substring(3, 2) + BL.CLS_Session.start_dt.Substring(0, 2)) || Convert.ToInt32(txt_mdate.Text.Substring(6, 4) + txt_mdate.Text.Substring(3, 2) + txt_mdate.Text.Substring(0, 2)) > Convert.ToInt32(BL.CLS_Session.end_dt.Substring(6, 4) + BL.CLS_Session.end_dt.Substring(3, 2) + BL.CLS_Session.end_dt.Substring(0, 2)))
            //{

            //    MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    e.Cancel = true;

            //}
        }

        private void txt_hdate_Validating(object sender, CancelEventArgs e)
        {
            DateTime rs;

            CultureInfo ci = new CultureInfo("ar-SA", false);

            if (!DateTime.TryParseExact(this.txt_hdate.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            {

                MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;

            }
            if (Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_hdate.Text)) < Convert.ToInt32(dtv.convert_to_yyyyDDdd(dtv.convert_to_hdate(BL.CLS_Session.start_dt))) || Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_hdate.Text)) > Convert.ToInt32(dtv.convert_to_yyyyDDdd(dtv.convert_to_hdate(BL.CLS_Session.end_dt))))
            {

                MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;

            }
            //if (Convert.ToInt32(txt_hdate.Text.Substring(6, 4) + txt_hdate.Text.Substring(3, 2) + txt_hdate.Text.Substring(0, 2)) < Convert.ToInt32(convert_to_hdate(BL.CLS_Session.start_dt).Substring(6, 4) + convert_to_hdate(BL.CLS_Session.start_dt).Substring(3, 2) + convert_to_hdate(BL.CLS_Session.start_dt).Substring(0, 2)) || Convert.ToInt32(txt_hdate.Text.Substring(6, 4) + txt_hdate.Text.Substring(3, 2) + txt_hdate.Text.Substring(0, 2)) > Convert.ToInt32(convert_to_hdate(BL.CLS_Session.end_dt).Substring(6, 4) + convert_to_hdate(BL.CLS_Session.end_dt).Substring(3, 2) + convert_to_hdate(BL.CLS_Session.end_dt).Substring(0, 2)))
            //{

            //    MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    e.Cancel = true;

            //}

        }

        private void txt_mdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_mdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_mdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_mdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_mdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                            txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                        else
                        {
                            txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                    }
                }
            }
            catch { }
        }

        private void txt_hdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr2 = txt_hdate.Text.ToString().Replace("-", "").Trim();
                if (datestr2.Length == 0)
                {
                    txt_hdate.Text = DateTime.Now.ToString("dd", new CultureInfo("ar-SA", false)) + DateTime.Now.ToString("MM", new CultureInfo("ar-SA", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("ar-SA", false));
                    txt_mdate.Text = convert_to_mdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr2.Length == 2)
                    {
                        txt_hdate.Text = datestr2 + DateTime.Now.ToString("MM", new CultureInfo("ar-SA", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("ar-SA", false));
                        txt_mdate.Text = convert_to_mdate(txt_hdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr2.Length == 4)
                        {
                            txt_hdate.Text = datestr2 + DateTime.Now.ToString("yyyy", new CultureInfo("ar-SA", false));
                            txt_mdate.Text = convert_to_mdate(txt_hdate.Text.ToString());
                        }
                        else
                        {
                            txt_mdate.Text = convert_to_mdate(txt_hdate.Text.ToString());
                        }
                    }
                }
            }
            catch { }
        }

        private void txt_desc_Enter(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("ar-SA", false));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // MessageBox.Show(comboBox1.SelectedValue.ToString());
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.Owner.Enabled = true;
            this.Close();
        }

        private void txt_amt_KeyUp(object sender, KeyEventArgs e)
        {
            //if (!(e.KeyCode == Keys.Back))
            //{
            //    string text = txt_amt.Text.Replace(",", "");
            //    if (text.Length % 3 == 0)
            //    {
            //        txt_amt.Text += ",";
            //        txt_amt.SelectionStart = txt_amt.Text.Length;
            //    }
            //}
        }

        private void txt_mdate_Enter(object sender, EventArgs e)
        {
            // TextBox textBox = (TextBox)sender;
            //   txt_mdate.SelectAll();

            //  txt_mdate.SelectionStart = 0;
            // txt_mdate.SelectionLength = txt_mdate.Text.Length;
            txt_mdate.Focus();
            txt_mdate.SelectAll();
        }

        private void txt_hdate_Enter(object sender, EventArgs e)
        {
            // TextBox textBox = (TextBox)sender;
            // txt_hdate.SelectAll();
            //txt_hdate.SelectionStart = 0;
            //txt_hdate.SelectionLength = txt_hdate.Text.Length;

            txt_hdate.Focus();
            txt_hdate.SelectAll();
        }

        private void Add_btn_Click_1(object sender, EventArgs e)
        {
            txt_mdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            txt_hdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));
            
            txt_total.Text = "0";
            txt_des.Text = "0";
            txt_desper.Text = "0";
            txt_net.Text = "0";

            cmb_type.Enabled = true;
           // cmb_towh.Enabled = true;
            //string tr =BL.CLS_Session.trkey.Replace(" ", "','").Remove(0,2) + "'";
            //MessageBox.Show(tr);
            //DataTable dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('04','05','14','15')");

            //foreach (DataRow dtr in dttrtyps.Rows)
            //{
            //   // string str2 = dtr[0].ToString();
            //  //  string whatever = str2.Substring(str2.IndexOf("Value =") + 8, 2);

            //    // MessageBox.Show(dtr[0].ToString());
            //    // string value = comboBox1.GetItemText(comboBox1.Items[i]);
            //  //  if (!BL.CLS_Session.trkey.Contains(dtr[0].ToString()))
            //    //    dttrtyps.Rows.Remove(dtr);
            //}

            //comboBox1.DataSource = dttrtyps;
            //comboBox1.DisplayMember = "tr_name";
            //comboBox1.ValueMember = "tr_no";

            //for (int i = 0; i < comboBox1.Items.Count; i++)
            //{
            //    string str2 = comboBox1.Items[i].ToString();
            //    string whatever = str2.Substring(str2.IndexOf("Value =") + 8, 2);

            //    // MessageBox.Show(whatever);
            //    // string value = comboBox1.GetItemText(comboBox1.Items[i]);
            //    if (!BL.CLS_Session.trkey.Contains(whatever))
            //        comboBox1.Items.RemoveAt(i);
            //}

            txt_key.Text = BL.CLS_Session.brcash;
            load_key();
           // dthdr.Rows.Clear();
           // dtdtl.Rows.Clear();
            isnew = true;
            isupdate = false;
            btn_qbl.Enabled = false;
            btn_tali.Enabled = false;
            btn_Save.Enabled = true;
            btn_Add.Enabled = false;
            btn_Undo.Enabled = true;
            btn_Exit.Enabled = false;
            btn_Find.Enabled = false;
            btn_Del.Enabled = false;
            btn_Edit.Enabled = false;
            btn_Post.Enabled = false;
            chk_nodup.Enabled = true;
            btn_Rfrsh.Enabled = false;
            
            //foreach (var CmbItem in comboBox1.Items)
            //{
            //    // var tempMeasured = CmbItem;
            //    // MessageBox.Show(CmbItem.ToString());
            //    string str2 = CmbItem.ToString();
            //    string whatever = str2.Substring(str2.IndexOf("Value =") + 8, 2);

            //     MessageBox.Show(whatever);
            //    // string value = comboBox1.GetItemText(comboBox1.Items[i]);
            //    if (!BL.CLS_Session.trkey.Contains(whatever))
            //        comboBox1.Items.Remove(str2);
            //}

           

            cmb_type.SelectedIndex = 0;

            cmb_frwh.Enabled = true;
            cmb_frwh.SelectedIndex = 0;
            cmb_towh.Enabled = true;
            cmb_towh.SelectedIndex = 0;

            txt_ref.Enabled = false;
            
            if (BL.CLS_Session.up_chang_dt)
            {
                txt_hdate.Enabled = true;
                txt_mdate.Enabled = true;
            }
            else
            {
                txt_hdate.Enabled = false;
                txt_mdate.Enabled = false;
            }
             
            txt_desc.Enabled = true;
            txt_remark.Enabled = true;
            txt_key.Enabled = true;
            txt_custno.Enabled = true;
            txt_des.Enabled = true;
            txt_desper.Enabled = true;
            txt_user.Text = BL.CLS_Session.UserName;
           // dataGridView1.Enabled = true;
           // dataGridView1.Rows.Add(10);
            dataGridView1.ReadOnly = false;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[7].ReadOnly = true;
            dataGridView1.Columns[5].ReadOnly = true;
            /*
            if (BL.CLS_Session.up_changprice)
            {
                dataGridView1.Columns[5].ReadOnly = false;
            }
            else
            {
                dataGridView1.Columns[5].ReadOnly = true;
            }
             */
           // DataGridView dtg = new DataGridView();
          //  dataGridView1.Rows.Clear();
           // dataGridView1.DataSource = dtg;
           
          //  dataGridView1.AllowUserToAddRows = true;
            //if (dataGridView1.Rows.Count-1 > 0)
            //{
            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        if (!row.IsNewRow)
            //        {
            //            dataGridView1.Rows.Remove(row);
            //        }

            //    }
            //    //dataGridView1.CurrentRow.Cells[0].Value = "";
            //    //dataGridView1.CurrentRow.Cells[1].Value = "";
            //    //dataGridView1.CurrentRow.Cells[2].Value = "";
            //    //dataGridView1.CurrentRow.Cells[3].Value = "0";
            //    //dataGridView1.CurrentRow.Cells[4].Value = "0";
            //    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            //}
            /*
            if (dataGridView1.Rows.Count >= 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        dataGridView1.Rows.Remove(row);
                    }

                }
              //  dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
             */
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                   // ((DataTable)dataGridView1.DataSource).Rows.Clear();
                    dataGridView1.Rows.Clear();
                }
            }
            catch { }
          //  dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToAddRows = true;

            cmb_type.Focus();
            txt_ref.Text = "";
            txt_desc.Text="";
            //txt_total.Text = "0";
            //txt_des.Text = "0";
            //txt_net.Text = "0";
            cmb_salctr_Leave(null, null);
            //dataGridView1.Rows.Clear();
            //dataGridView1.Refresh();
           // dataGridView1.AllowUserToAddRows = true;
        }

        private void btn_Undo_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("هل تريد التراجع عن التغييرات", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                
              

                if (isupdate)
                {
                    isupdate = false;
                   // btn_Save_Click(sender, e);
                    Fill_Data(cmb_frwh.SelectedValue.ToString(), cmb_type.SelectedValue.ToString(), txt_ref.Text);
                    btn_Save.Enabled = false;
                    btn_Add.Enabled = true;
                    btn_Undo.Enabled = false;
                    btn_Exit.Enabled = true;
                    btn_Find.Enabled = true;
                    btn_Edit.Enabled = true;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;
                    cmb_type.Enabled = false;
                    txt_ref.Enabled = false;
                    txt_remark.Enabled = false;
                    txt_key.Enabled=false;
                    txt_custno.Enabled = false;
                    btn_qbl.Enabled = true;
                    btn_tali.Enabled = true;
                    
                }
                else
                {
                    isnew = false;
                    btn_Save.Enabled = false;
                    btn_Add.Enabled = true;
                    btn_Undo.Enabled = false;
                    btn_Exit.Enabled = true;
                    btn_Find.Enabled = true;
                    btn_Edit.Enabled = false;
                    btn_qbl.Enabled = true;
                    btn_tali.Enabled = true;

                    try
                    {
                        if (dataGridView1.Rows.Count > 0)
                        {
                          //  ((DataTable)dataGridView1.DataSource).Rows.Clear();
                           // dataGridView1.DataSource = null;
                            dataGridView1.Rows.Clear();
                            dataGridView1.Refresh();
                            //dataGridView1.DataSource = null;
                            //dataGridView1.Refresh();
                        }
                    }
                    catch { }

                    dataGridView1.ReadOnly = true;

                    cmb_type.Enabled = false;
                    txt_ref.Enabled = false;
                  //  dataGridView1.AllowUserToAddRows = false;

                    cmb_type.SelectedIndex = -1;
                    txt_temp.Text = "0";
                    txt_desc.Text = "";

                    /*
                    if (dataGridView1.Rows.Count-1 > 0)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                dataGridView1.Rows.Remove(row);
                            }

                        }
                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                    }
                     */
                    try
                    {
                        if (dataGridView1.Rows.Count > 0)
                        {
                            ((DataTable)dataGridView1.DataSource).Rows.Clear();
                        }
                    }
                    catch { }
                    dataGridView1.AllowUserToAddRows = false;

                }

                
               // Acc_Tran_Load(sender,e);

                txt_mdate.Enabled = false;
                txt_hdate.Enabled = false;
                txt_desc.Enabled = false;
                txt_des.Enabled = false;
                txt_desper.Enabled = false;
                txt_temp.Enabled = false;
                cmb_type.Enabled = false;
                cmb_frwh.Enabled = false;
                cmb_towh.Enabled = false;
                txt_ref.Enabled = false;

                //...
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

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) > Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.end_dt)) || Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) < Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.start_dt)))
            {
                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Date Out of Years" : "لا يمكن ادخال حركة خارج السنة المالية", "Error خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridView1.RowCount <= 1)
            {
                MessageBox.Show("لا يوجد اصناف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            total();
            total();
            foreach (DataGridViewRow dgvr in dataGridView1.Rows)
            {

                if (Convert.ToDouble(dgvr.Cells[4].Value) == 0 && !dgvr.IsNewRow)
                {
                    MessageBox.Show("لا يمكن ان تكون الكمية صفر", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView1.CurrentCell = dataGridView1[4, dgvr.Index];
                    dataGridView1.Select();
                    return;
                }
            }
            cmb_salctr_Leave(null, null);
            try
            {
                if (cmb_type.SelectedValue.ToString().Equals("05") && (chek_bal_after() + Convert.ToDouble(txt_net.Text)) > Convert.ToDouble(txt_crlmt.Text) && Convert.ToDouble(txt_crlmt.Text) > 0)
                {
                    MessageBox.Show("لا يسمح بتجاوز حد المديونية");
                    return;
                }

                //if (txt_key.Text == "" || txt_name.Text=="")
                //{
                //    MessageBox.Show("يرجى ادخال حساب");
                //    txt_key.Focus();
                //    return;
                //}
                if (cmb_type.SelectedValue == "05" || cmb_type.SelectedValue == "15")
                {
                    if (txt_key.Text == "" || txt_name.Text == "")
                    {
                        MessageBox.Show("يرجى ادخال حساب");
                        txt_key.Focus();
                        return;
                    }
                }
                else
                {/*
                    if (txt_custno.Text == "" && txt_key.Text == "")
                    {
                        MessageBox.Show("يرجى ادخال عميل او حساب");
                        txt_custno.Focus();
                        return;
                    }
                  */
                }

                string mdate, hdate;
                mdate = txt_mdate.Text.Replace("-", "").Substring(4, 4) + txt_mdate.Text.Replace("-", "").Substring(2, 2) + txt_mdate.Text.Replace("-", "").Substring(0, 2);
                hdate = txt_hdate.Text.Replace("-", "").Substring(4, 4) + txt_hdate.Text.Replace("-", "").Substring(2, 2) + txt_hdate.Text.Replace("-", "").Substring(0, 2);

                DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref)," + BL.CLS_Session.dtbranch.Rows[0]["jv_" + cmb_type.SelectedValue + ""] + ") from sto_hdr where trtype='" + cmb_type.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "'");

                ref_max = Convert.ToInt32(dt.Rows[0][0]) + 1;



                if (isnew && string.IsNullOrEmpty(txt_ref.Text))
                {
                    daml.SqlCon_Open();
                    int exexcuteds = daml.Insert_Update_Delete_retrn_int("INSERT INTO sto_hdr(branch,trtype,ref,trmdate, trhdate,text,remarks,entries,amnttl,costttl,usrid,supno,whno,towhno) "
                    +" VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + ref_max + ",'" + mdate + "','" + hdate + "','" + txt_desc.Text + "','" + txt_remark.Text + "'," + (dataGridView1.RowCount - 1) + "," + txt_total.Text + ","+txt_cost.Text+",'" + txt_user.Text + "','" + txt_custno.Text + "','"+cmb_frwh.SelectedValue+"','"+cmb_towh.SelectedValue+"')", false);
                    daml.SqlCon_Close();
                    //daml.SqlCon_Open();
                   
                  //  int exexcuted = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries) VALUES('" + comboBox1.SelectedValue + "'," + ref_max + ",'" + mdate + "','" + hdate + "','" + txt_desc.Text + "'," + txt_amt.Text + "," + (dataGridView1.RowCount - 1) + ")", false);
                   // daml.SqlCon_Close();
                    if (exexcuteds > 0)
                    {
                        txt_ref.Text = ref_max.ToString();
                        txt_ref.Enabled = false;
                        txt_hdate.Enabled = false;
                        txt_mdate.Enabled = false;
                        txt_desc.Enabled = false;
                        txt_remark.Enabled = false;
                        txt_key.Enabled = false;
                        txt_custno.Enabled = false;
                        txt_des.Enabled = false;
                        txt_desper.Enabled = false;
                      //  dataGridView1.ReadOnly = true;
                        cmb_type.Enabled = false;
                        cmb_frwh.Enabled = false;
                        cmb_towh.Enabled = false;
                        btn_Post.Enabled = true;
                    }
                    else
                    {
                        
                    }
                     
                }
                else
                {
                    int exexcuteds = 0;
                    DataTable dtifex = daml.SELECT_QUIRY_only_retrn_dt("select count(*) from sto_hdr where branch='" + BL.CLS_Session.brno + "' and trtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + "");
                     if (Convert.ToInt32(dtifex.Rows[0][0]) == 0)
                     {
                         daml.SqlCon_Open();
                         exexcuteds = daml.Insert_Update_Delete_retrn_int("INSERT INTO sto_hdr(branch,trtype,ref,trmdate, trhdate,text,remarks,entries,amnttl,costttl,usrid,supno,whno,towhno) "
                         + " VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + ref_max + ",'" + mdate + "','" + hdate + "','" + txt_desc.Text + "','" + txt_remark.Text + "'," + (dataGridView1.RowCount - 1) + "," + txt_total.Text + "," + txt_cost.Text + ",'" + txt_user.Text + "','" + txt_custno.Text + "','" + cmb_frwh.SelectedValue + "','" + cmb_towh.SelectedValue + "')", false);
                         daml.SqlCon_Close();
                     }
                     else
                     {
                         daml.SqlCon_Open();
                         exexcuteds = daml.Insert_Update_Delete_retrn_int("update sto_hdr set trmdate='" + mdate + "', trhdate='" + hdate + "',text='" + txt_desc.Text + "',remarks='" + txt_remark.Text + "',supno='" + txt_custno.Text + "',entries=" + (dataGridView1.RowCount - 1) + ",lastupdt=getdate(),amnttl=" + txt_total.Text + ",costttl=" + txt_cost.Text + ",usrid='" + txt_user.Text + "',whno='" + cmb_frwh.SelectedValue + "',towhno='" + cmb_towh.SelectedValue + "' where branch='" + BL.CLS_Session.brno + "' and trtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + "", false);
                         daml.SqlCon_Close();
                     }

                     daml.SqlCon_Open();
                     int exexcuteds2 = daml.Insert_Update_Delete_retrn_int("delete from  sto_dtl where branch='" + BL.CLS_Session.brno + "' and trtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + "", false);
                     daml.SqlCon_Close();
                    
                    
                  //  daml.SqlCon_Open();
                   // int exexcuted = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_mdate='" + mdate + "', a_hdate='" + hdate + "',a_text='" + txt_desc.Text + "',a_amt=" + txt_amt.Text + ",a_entries=" + (dataGridView1.RowCount - 1) + " where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "'", false);
                   // exexcuted = exexcuted + daml.Insert_Update_Delete_retrn_int("delete from acc_dtl where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "'", false);
                   // daml.SqlCon_Close();
                   // if (exexcuteds > 0)
                     if (exexcuteds > 0)
                    {
                      //  txt_ref.Text = ref_max.ToString();
                        txt_ref.Enabled = false;
                        txt_hdate.Enabled = false;
                        txt_mdate.Enabled = false;
                        txt_desc.Enabled = false;
                        txt_remark.Enabled = false;
                        txt_key.Enabled = false;
                        txt_custno.Enabled = false;
                        txt_des.Enabled = false;
                        txt_desper.Enabled = false;
                        btn_Post.Enabled = true;
                       // cmb_salctr.Enabled = false;

                      //  dataGridView1.ReadOnly = true;
                    }
                    else
                    {

                    }
                  
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from DB.dbo.sales_hdr", con2);
                    /*
                    SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from sales_hdr", con2);
                    DataSet dss = new DataSet();
                    daa.Fill(dss, "0");
    */
                  //  if (!row.IsNewRow && row.Cells[0].Value != null)
                    if ((!row.IsNewRow && row.Cells[0].Value != null) && (!row.IsNewRow && row.Cells[1].Value != null) && (!row.IsNewRow && row.Cells[3].Value != null))
                    {
                        // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO sto_dtl(branch,trtype,ref,trmdate, trhdate, itemno, qty, lprice, pack, pkqty,folio,whno,towhno,barcode,lcost) VALUES(@a1,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a14,@a15,@a16,@a17)", con))
                        {
                            cmd.Parameters.AddWithValue("@a1", BL.CLS_Session.brno);
                         //   cmd.Parameters.AddWithValue("@a2", cmb_salctr.SelectedValue);
                            cmd.Parameters.AddWithValue("@a3", cmb_type.SelectedValue);
                            cmd.Parameters.AddWithValue("@a4", isnew ? ref_max : Convert.ToInt32(txt_ref.Text));
                           // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                            cmd.Parameters.AddWithValue("@a5", mdate);
                           // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                            cmd.Parameters.AddWithValue("@a6", hdate);
                            cmd.Parameters.AddWithValue("@a7", row.Cells[0].Value.ToString().Trim());
                            cmd.Parameters.AddWithValue("@a8", row.Cells[4].Value);
                            cmd.Parameters.AddWithValue("@a9", row.Cells[5].Value);
                            cmd.Parameters.AddWithValue("@a10",row.Cells[2].Value);
                            cmd.Parameters.AddWithValue("@a11", row.Cells[3].Value);
                            cmd.Parameters.AddWithValue("@a12", row.HeaderCell.Value);
                          //  cmd.Parameters.AddWithValue("@a13", Convert.ToDouble(row.Cells[6].Value));
                            cmd.Parameters.AddWithValue("@a14", cmb_frwh.SelectedValue);
                            cmd.Parameters.AddWithValue("@a15", cmb_towh.SelectedValue);
                            cmd.Parameters.AddWithValue("@a16", row.Cells[8].Value);
                            cmd.Parameters.AddWithValue("@a17", Convert.ToDouble(row.Cells[5].Value) / Convert.ToDouble(row.Cells[3].Value));
                            //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                            //if (row.Cells[7].Value != null)
                            // { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                            // cmd.Parameters.AddWithValue("@c9", comp_id);
                            // cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                            //con.Open();
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();
                            //con.Close();
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }
                            // lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);

                        }
                    }
                }

                chk_nodup.Enabled =false;
                btn_Save.Enabled = false;
                btn_Add.Enabled = true;
                btn_Exit.Enabled = true;
                btn_Find.Enabled = true;
                btn_Undo.Enabled = false;
                btn_Del.Enabled = true;
                btn_Edit.Enabled = true;
                if (notposted)
                { btn_Post.Enabled = true; }
                isnew = false;
                isupdate = false;
                btn_qbl.Enabled = true;
                btn_tali.Enabled = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ReadOnly = true;
                btn_Rfrsh.Enabled = true;
              //  save_acc();
                btn_Rfrsh_Click(sender, e); //to print
                //////daml.SqlCon_Open();
                //////daml.Exec_Query_only("bld_whbins_cost_by_tran @cond='sto', @br_no ='" + BL.CLS_Session.brno + "', @inv_type='" + cmb_type.SelectedValue + "',@wh_no='" + stwhno + "', @ref=" + txt_ref.Text + ",@depart =' '");
                inv_t = cmb_type.SelectedValue.ToString();
                inv_r = txt_ref.Text;

                Thread t = new Thread(() => thread1(inv_t, inv_r));
                t.Start();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        public void thread1(string s1, string s2)
        {
            try
            {
                SqlConnection con2 = BL.DAML.con_asyn;
                //////daml.SqlCon_Close();
                DataTable dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sto_dtl where trtype='" + s1 + "' and ref=" + s2 + " and branch='" + BL.CLS_Session.brno + "'");
                // daml.Exec_Query_only("bld_whbins_cost_all_by_tran @items_tran_tb=" + dttbld + "");
                using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con2;
                    cmd.Parameters.AddWithValue("@items_tran_tb", dttbld);
                    if (con2.State == ConnectionState.Closed)
                        con2.Open();
                    cmd.ExecuteNonQuery();
                    con2.Close();

                    //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch { }
        }
        public void threadt(string s1, string s2, string s3)
        {

            try
            {
                daml.SqlCon_Open();
                int exexcuted = daml.Insert_Update_Delete_retrn_int("update sto_hdr set posted=1 where branch='" + BL.CLS_Session.brno + "' and trtype='" + s1 + "' and ref=" + s2 + "", false);
                // exexcuted = exexcuted + daml.Insert_Update_Delete("delete from acc_dtl where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "'", false);
                daml.SqlCon_Close();

                if (exexcuted > 0)
                {
                    DataTable dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sto_dtl where trtype='" + s1 + "' and ref=" + s2 + " and branch='" + BL.CLS_Session.brno + "'");
                    using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@items_tran_tb", dttbld);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    }
                    MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "successfuly posted" : "تم الترحيل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    btn_Post.Enabled = false;
                }
                else
                {
                    MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "fail during post" : "فشل في الترحيل", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { }
        } 
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            cmb_salctr_Leave(sender, e);
            try
            {
               // btn_Rfrsh_Click(sender, e);
                if (txt_user.Text != BL.CLS_Session.UserName && BL.CLS_Session.up_edit_othr == false)
                {
                    MessageBox.Show("لا تملك صلاحية تعديل حركة شخص اخر", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (dthdr.Rows[0][8].ToString().Equals("Pos"))
                    {
                        MessageBox.Show("لا يمكن تعديل قيد الي تم تكوينه من الكاشير", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }
                }
                //   btn_Rfrsh_Click(sender, e);
                if (btn_Post.Enabled == true)
                {
                    chk_nodup.Enabled = true;
                    btn_Rfrsh.Enabled = false;
                    notposted = true;
                    isnew = false;
                    isupdate = true;
                    btn_qbl.Enabled = false;
                    btn_tali.Enabled = false;
                    btn_Save.Enabled = true;
                    btn_Add.Enabled = false;
                    btn_Undo.Enabled = true;
                    btn_Exit.Enabled = false;
                    btn_Find.Enabled = false;
                    btn_Del.Enabled = false;
                    btn_Edit.Enabled = false;
                    btn_Post.Enabled = false;

                    chk_nodup.Enabled = true;
                    txt_ref.Enabled = false;
                    if (BL.CLS_Session.up_chang_dt)
                    {
                        txt_hdate.Enabled = true;
                        txt_mdate.Enabled = true;
                    }
                    else
                    {
                        txt_hdate.Enabled = false;
                        txt_mdate.Enabled = false;
                    }
                    txt_desc.Enabled = true;
                    txt_remark.Enabled = true;
                    txt_key.Enabled = true;
                    txt_custno.Enabled = true;
                    txt_des.Enabled = true;
                    txt_desper.Enabled = true;
                    txt_user.Text = BL.CLS_Session.UserName;
                    // dataGridView1.Enabled = true;
                    dataGridView1.ReadOnly = false;
                    dataGridView1.Columns[1].ReadOnly = true;
                    dataGridView1.Columns[3].ReadOnly = true;
                    dataGridView1.Columns[7].ReadOnly = true;
                    if (BL.CLS_Session.up_changprice)
                    {
                        dataGridView1.Columns[5].ReadOnly = false;
                    }
                    else
                    {
                        dataGridView1.Columns[5].ReadOnly = true;
                    }
                    dataGridView1.Select();
                    dataGridView1.AllowUserToAddRows = true;
                    dataGridView1.Columns[5].ReadOnly = true;
                    //  ref_max = Convert.ToInt32(txt_ref.Text);
                }
                else
                {
                    MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Can't edit posted trans. " : "لا يمكن تعديل حركة مرحلة", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
           // btn_Rfrsh_Click(sender, e);
            if (btn_Post.Enabled == true)
            {
                DialogResult result = MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Do you want to dalete" : "هل تريد المسح", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
               
                try
                {
                    DataTable dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sto_dtl where trtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + " and branch='" + BL.CLS_Session.brno + "'");
                   
                    daml.SqlCon_Open();
                    ////if (dthdr.Rows[0][8].ToString().Equals("SL"))
                    ////{
                    ////    daml.Insert_Update_Delete_retrn_int("update pos_hdr set gen_ref=0 where gen_ref=" + txt_ref.Text, false);

                    ////}
                    int exexcuted = daml.Insert_Update_Delete_retrn_int("delete from sto_hdr where posted=0 and branch='" + BL.CLS_Session.brno + "' and trtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + "", false);
                 //////////   daml.Insert_Update_Delete_Only("delete from acc_hdr where a_posted=0 and a_brno='" + BL.CLS_Session.brno + "' and sl_no='" + cmb_frwh.SelectedValue + "' and a_type='" + cmb_type.SelectedValue + "' and a_ref=" + txt_ref.Text + "", false);
                    // exexcuted = exexcuted + daml.Insert_Update_Delete("delete from acc_dtl where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "'", false);
                    daml.SqlCon_Close();

                    if (exexcuted > 0)
                    {
                        MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Successfuly deleted " : "تم الحذف بنجاح", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        btn_Del.Enabled = false;
                      
                        if (dataGridView1.Rows.Count > 0)
                          {
                            // ((DataTable)dataGridView1.DataSource).Rows.Clear();
                              dataGridView1.Rows.Clear();
                          }
                        txt_ref.Text = "";
                        txt_desc.Text = "";
                      //  txt_amt.Text = "";
                        txt_total.Text = "0";
                        txt_des.Text = "0";
                        txt_net.Text = "0";
                        cmb_type.SelectedIndex = -1;

                      
                    }
                    else
                    {
                        MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? " not deleted " : "لم يتم الحذف", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Thread t = new Thread(() =>
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@items_tran_tb", dttbld);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();

                                //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                            }
                        }
                        catch { }
                        //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    });
                    t.Start();
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //...
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
            else
            {
                MessageBox.Show("لا يمكن تعديل حركة مرحلة", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                var sr = new Search_All("33", "Sto");
                sr.ShowDialog();

                Fill_Data("", sr.dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), sr.dataGridView1.CurrentRow.Cells[2].Value.ToString());
            }
            catch { }
        }

        private void Fill_Data(string slctr,string atype, string aref)
        {
            try
            {
              //  string mdate, hdate;
               // mdate = txt_mdate.Text.Replace("-", "").Substring(4, 4) + txt_mdate.Text.Replace("-", "").Substring(2, 2) + txt_mdate.Text.Replace("-", "").Substring(0, 2);
              //  hdate = txt_hdate.Text.Replace("-", "").Substring(4, 4) + txt_hdate.Text.Replace("-", "").Substring(2, 2) + txt_hdate.Text.Replace("-", "").Substring(0, 2);


                 dthdr = daml.SELECT_QUIRY_only_retrn_dt("select * from sto_hdr where trtype='" + atype + "' and ref=" + aref + " and branch='"+BL.CLS_Session.brno+"'");
                 dtdtl = daml.SELECT_QUIRY_only_retrn_dt("select aa.itemno itemno,bb.item_name item_name,aa.pack pack,aa.pkqty pkqty,aa.qty qty,aa.lprice lcost,(aa.qty*aa.lprice) total,aa.lcost cost,aa.barcode barcode  from sto_dtl aa,items bb where aa.itemno=bb.item_no and aa.trtype='" + atype + "' and aa.ref=" + aref + " and aa.branch='" + BL.CLS_Session.brno + "' order by aa.folio");// and aa.a_type='" + atype + "' and aa.a_ref=" + aref + "");

                cmb_type.SelectedValue=dthdr.Rows[0]["trtype"];
                cmb_frwh.SelectedValue = dthdr.Rows[0]["whno"];
                cmb_towh.SelectedValue = dthdr.Rows[0]["towhno"];
                txt_ref.Text =isnew?"": dthdr.Rows[0]["ref"].ToString();
                txt_mdate.Text = dthdr.Rows[0][3].ToString().Substring(6, 2) + dthdr.Rows[0][3].ToString().Substring(4, 2) + dthdr.Rows[0][3].ToString().Substring(0, 4);
                txt_hdate.Text = dthdr.Rows[0][4].ToString().Substring(6, 2) + dthdr.Rows[0][4].ToString().Substring(4, 2) + dthdr.Rows[0][4].ToString().Substring(0, 4);
                txt_desc.Text = dthdr.Rows[0]["text"].ToString();
                txt_remark.Text = dthdr.Rows[0]["remarks"].ToString();
                txt_total.Text = dthdr.Rows[0]["amnttl"].ToString();
               // txt_des.Text = dthdr.Rows[0]["invdsvl"].ToString();
              //  txt_desper.Text = dthdr.Rows[0]["invdspc"].ToString();
              //  txt_net.Text = dthdr.Rows[0]["nettotal"].ToString();
                txt_user.Text = dthdr.Rows[0]["usrid"].ToString();
               // txt_cost.Text = dthdr.Rows[0]["invcst"].ToString();

               // txt_key.Text = dthdr.Rows[0][19].ToString();
                txt_custno.Text = dthdr.Rows[0]["supno"].ToString();
                load_key();

                if (Convert.ToBoolean(dthdr.Rows[0]["posted"]) == true)
                {
                    btn_Post.Enabled = false;

                }
                else { btn_Post.Enabled = true; }

                if (!isnew && !isupdate)
                {
                    btn_Del.Enabled = true;
                    btn_Edit.Enabled = true;
                }
                else
                {
                    btn_Del.Enabled = false;
                    btn_Edit.Enabled = false;
                }
               // txt_ref.Text = dthdr.Rows[0][2].ToString();

              //  dataGridView1.DataSource = dtdtl;
                //dataGridView1.DataSource = dataGridView1.DataSource;
                //dthdr.Rows.Clear();
                //dtdtl.Rows.Clear();
                /*
                foreach (DataColumn col in dtdtl.Columns)
                {
                    var c = new DataGridViewTextBoxColumn() { HeaderText = col.ColumnName }; //Let say that the default column template of DataGridView is DataGridViewTextBoxColumn
                    dataGridView1.Columns.Add(c);
                   // dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());

                }
                 */
                dataGridView1.Rows.Clear();

               

                foreach (DataRow dr in dtdtl.Rows)
                {

                    dataGridView1.Rows.Add(dr.ItemArray);

                }
                

                int rowNumber = 1;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    // dataGridView1.Rows.Add(r);
                    // dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                    if (r.IsNewRow) continue;
                    //r.HeaderCell.Value = (e.RowIndex + 1).ToString();
                    r.HeaderCell.Value = rowNumber.ToString();
                    rowNumber = rowNumber + 1;
                    // r.Cells[6].Value = "7";
                    //  dtdtl.Rows[r.Index][6] =Convert.ToDecimal(r.Cells[5].Value) * Convert.ToDecimal(r.Cells[4].Value);
                    // r.Cells[2] = null;
                    // r.Cells[1].Value = "1111111111111111";
                    /*
                    DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                    DataView dv1 = dtunits.DefaultView;
                    dv1.RowFilter = "unit_id ='" + r.Cells[2].Value + "'";
                    DataTable dtNew = dv1.ToTable();
                    dcombo.DataSource = dtNew;
                    dcombo.DisplayMember = "unit_name";
                    dcombo.ValueMember = "unit_id";
                    dcombo.Value = r.Cells[2].Value;
                    dataGridView1.CurrentRow.Cells[2] = dcombo;
                  //  dcombo.data = "account_id";
                  //  dcombo.Value = r.Cells[2].Value;
                   // r.Cells[2] = dcombo;
                    // dataGridView1.CurrentRow.Cells[2] = dcombo;
                  //  DataGridViewTextBoxCell textBoxcell = (DataGridViewTextBoxCell)(r.Cells[2]);
                  //  textBoxcell.Value = "1";
                   
                    // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                    dcombo.FlatStyle = FlatStyle.Flat;
                    */
                 //   DataGridViewComboBoxCell dcombo = (DataGridViewComboBoxCell)(r.Cells[2]);
                   // cell.DataSource = new string[] { "10", "30" };
                    string dvc = r.Cells[2].Value.ToString();
                  //  dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where rtrim(ltrim(item_no))='" + r.Cells[0].Value + "'");
                    
                    DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                  //  r.Cells[2] = dcombo;
                    DataView dv1 = dtunits.DefaultView;
                  //  dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                    dv1.RowFilter = "unit_id in('" + r.Cells[2].Value + "')";
                    DataTable dtNew = dv1.ToTable();
                    dcombo.DataSource = dtNew;
                    dcombo.DisplayMember = "unit_name";
                    dcombo.ValueMember = "unit_id";
                  //  MessageBox.Show(r.Cells[2].Value.ToString());
                  //  dcombo.Value = r.Cells[2].Value.ToString();// dt222.Rows[0][5];
                  //  dcombo.Value = dtunits.Rows[0][1];

                    //for (int i = 0; i < dtunits.Rows.Count; i++)
                    //{
                    //    if (dtunits.Rows[i]["unit_id"].ToString() == r.Cells[2].Value.ToString()) // 1 for MALE & 2 for FEMALE
                    //    {
                    //        dcombo.Value = dtunits.Rows[i]["unit_name"];
                    //    }
                    //}
                   // r.Cells[2] = dcombo;
                  //  dataGridView1.CurrentRow.Cells[2] = dcombo;
                    // dcombo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                    //if (e.RowIndex == dataGridView1.NewRowIndex)
                    //{
                    //    dataGridView1.Rows.Add(1);
                    //}

                    //  dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                    r.Cells[2] = dcombo;
                    r.Cells[2].Value = dtNew.Rows[0][0];

                  //  dataGridView1[2, r.Index] = dcombo;
                   // dataGridView1[2, r.Index].Value = dtNew.Rows[0][1];
                    // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                    dcombo.FlatStyle = FlatStyle.Flat;
                     
                       // var dgc = new DataGridViewComboBoxColumn();
                        // dgc.Items.AddRange("Male", "Female");

                    /*
                        DataTable dt = new DataTable();
                        DataColumn dc1 = new DataColumn("ID");
                        DataColumn dc2 = new DataColumn("Name");
                        dt.Columns.Add(dc1);
                        dt.Columns.Add(dc2);
                        dt.Rows.Add(1, "Male");
                        dt.Rows.Add(2, "Female");
                    */

                        //DataGridViewComboBoxColumn c1 = new DataGridViewComboBoxColumn() { HeaderText = "الوحدة" };
                        //c1.DataSource = dtNew;
                        //c1.DisplayMember = "unit_name";
                        //c1.ValueMember = "unit_id";
                    

                        //for (int i = 0; i < dtNew.Rows.Count; i++)
                        //{
                        //    if (dtNew.Rows[i]["unit_id"].ToString() == "1") // 1 for MALE & 2 for FEMALE
                        //    {
                        //        c1.DefaultCellStyle.NullValue = dtNew.Rows[i]["unit_name"];
                        //    }

                        //}
                        //dataGridView1.Columns.Add(c1);
                        //continue;
                    
                    //DataRow sel = dtunits.Select("unit_id = '" + r.Cells[2].Value.ToString() + "'").FirstOrDefault();
                    //if (sel != null)
                    //{
                    //  //  dcombo.Value = sel["unit_name"].ToString();
                    //    r.Cells[2].Value = sel["unit_name"].ToString();
                    //}
                  //  r.Cells[2] = dcombo;
                   /*

                  //  string dv = r.Cells[2].Value.ToString();
                    DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                    //  DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
                    // dt.Clear();
                    
                    dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where rtrim(ltrim(item_no))='" + r.Cells[0].Value + "'");
                    
                   // MessageBox.Show(r.Cells[0].Value.ToString());
                   // MessageBox.Show(dt222.Rows[0][0].ToString());

                    DataView dv1 = dtunits.DefaultView;
                    /*
                    dv1.RowFilter = "unit_id in('" + r.Cells[2].Value.ToString() + "')";
                    DataTable dtNew = dv1.ToTable();
                    dcombo.DataSource = dtunits;
                    dcombo.DisplayMember = "unit_name";
                    dcombo.ValueMember = "unit_id";
                    //  dataGridView1.Columns[2] = dcombo;
                    dcombo.Value = r.Cells[2];

                    
                    r.Cells[2] = dcombo;
                    dcombo.FlatStyle = FlatStyle.Flat;
                     * */
                    /*
                    dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                    DataTable dtNew = dv1.ToTable();
                    dcombo.DataSource = dtNew;
                    dcombo.DisplayMember = "unit_name";
                    dcombo.ValueMember = "unit_id";
                   // MessageBox.Show(dt222.Rows[0][5].ToString());
                   // dcombo.Selected = r.Cells[2].Value;
                    r.Cells[2] = dcombo;
                    // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                    dcombo.FlatStyle = FlatStyle.Flat;
                    */
                }

               // dcombo.Value = dt222.Rows[0][5];
               // dataGridView1.CurrentRow.Cells[2] = dcombo;
                /*
                int rowNumber = 1;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                   // dataGridView1.Rows.Add(r);
                    // dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                    if (r.IsNewRow) continue;
                    //r.HeaderCell.Value = (e.RowIndex + 1).ToString();
                    r.HeaderCell.Value = rowNumber.ToString();
                    rowNumber = rowNumber + 1;
                   // r.Cells[6].Value = "7";
                  //  dtdtl.Rows[r.Index][6] =Convert.ToDecimal(r.Cells[5].Value) * Convert.ToDecimal(r.Cells[4].Value);
                   // r.Cells[2] = null;

                    DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();

                    DataView dv1 = dtunits.DefaultView;
                    dv1.RowFilter = "unit_id in('" + r.Cells[2] + "')";
                    DataTable dtNew = dv1.ToTable();
                    dcombo.DataSource = dtNew;
                    dcombo.DisplayMember = "unit_name";
                    dcombo.ValueMember = "unit_id";

                    r.Cells[2] = dcombo;
                    // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                    dcombo.FlatStyle = FlatStyle.Flat;
                  //  r.Cells[2].Value = dcombo;
                }
                 */
               // dtdtl.Rows.Clear();

               
               // dataGridView1.Refresh();
                total();  
            }
            catch { }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
               // dataGridView1.ReadOnly = false;
                var fsn = new Search_by_No("30");

                fsn.comboBox1.DataSource = dttrtyps;
                fsn.comboBox1.DisplayMember = "tr_name";
                fsn.comboBox1.ValueMember = "tr_no";
                /*
                fsn.cmb_salctr.DataSource = dtslctr;
                fsn.cmb_salctr.DisplayMember = "sl_name";
                fsn.cmb_salctr.ValueMember = "sl_no";
                */
                fsn.ShowDialog();

                Fill_Data("", fsn.comboBox1.SelectedValue.ToString(), fsn.textBox1.Text);
              //  total();
            }
            catch { }
          //  dataGridView1.Enabled = true;
        }

        private void btn_Post_Click(object sender, EventArgs e)
        {/*
            if (Convert.ToDouble(txt_tatal.Text) != Convert.ToDouble(txt_amt.Text) || Convert.ToDouble(txt_des.Text) != Convert.ToDouble(txt_amt.Text))
            {
                MessageBox.Show("لا يمكن ترحيل حركة غير متزنة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
          */
            
            btn_Post.Enabled = false;
            inv_t = cmb_type.SelectedValue.ToString();
            inv_r = txt_ref.Text;
           // inv_s = "";

            Thread t = new Thread(() => threadt(inv_t, inv_r, ""));
            t.Start();
            //try
            //    {
            //        daml.SqlCon_Open();
            //        int exexcuted = daml.Insert_Update_Delete_retrn_int("update sto_hdr set posted=1 where branch='" + BL.CLS_Session.brno + "' and trtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + "", false);
            //        // exexcuted = exexcuted + daml.Insert_Update_Delete("delete from acc_dtl where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "'", false);
            //        daml.SqlCon_Close();

            //        if (exexcuted > 0)
            //        {
            //            DataTable dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sto_dtl where trtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + " and branch='" + BL.CLS_Session.brno + "'");
            //            using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
            //            {

            //                cmd.CommandType = CommandType.StoredProcedure;
            //                cmd.Connection = con;
            //                cmd.Parameters.AddWithValue("@items_tran_tb", dttbld);
            //                con.Open();
            //                cmd.ExecuteNonQuery();
            //                con.Close();

            //                //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            //            }
            //            MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "successfuly posted" : "تم الترحيل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //            btn_Post.Enabled = false;
            //        }
            //        else
            //        {
            //            MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "fail during post" : "فشل في الترحيل", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //catch { }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {/*
                // dataGridView1_CellDoubleClick( null,  null);
                Acc.Acc_Statment_Exp f4a = new Acc.Acc_Statment_Exp(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                f4a.MdiParent = ParentForm;
                f4a.Show();
              */
            }
            catch { }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //double su
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
           // total();
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(string.IsNullOrEmpty(txt_desc.Text))
                    txt_desc.Text= cmb_type.Text + " - " +cmb_frwh.Text;
                cmb_frwh.Focus();
               
            }
        }

        private void txt_mdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_hdate.Focus();
               // txt_hdate.SelectionStart = 0;
                txt_hdate.SelectionLength = 0;
            }
        }

        private void txt_hdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_towh.Focus();
            }
        }

        private void txt_desc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_remark.Focus();
            }
        }

        private void txt_amt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridView1.Focus();
            }
        }

        private void btn_Rfrsh_Click(object sender, EventArgs e)
        {
            try
            {
                Fill_Data("", cmb_type.SelectedValue.ToString(), txt_ref.Text);
            }
            catch { }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

           // seso = Convert.ToDouble(txt_net.Text);
          //  BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal( seso < 0 ? seso * -1 : seso), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);
           // MessageBox.Show(toWord.ConvertToArabic());
        //    GridPrintDocument doc = new GridPrintDocument(this.dataGridView1, this.dataGridView1.Font, true);
        //    doc.DocumentName = "Preview Test";
        //    doc.DrawCellBox = true;
        //    PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
        //    printPreviewDialog.ClientSize = new Size(1000, 800);
        //    printPreviewDialog.Location = new System.Drawing.Point(29, 29);
        //    printPreviewDialog.Name = "Print Preview Dialog";
        //    printPreviewDialog.UseAntiAlias = true;
        //    printPreviewDialog.Document = doc;
        //    printPreviewDialog.ShowDialog();
        //    doc.Dispose();
        //    doc = null;
            try
            {
                // Fill_Data(cmb_salctr.SelectedValue.ToString(), cmb_type.SelectedValue.ToString(), txt_ref.Text);

                Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();
                //rf.reportViewer1.RefreshReport();
                //SqlConnection con = new SqlConnection(@"Data Source=RYD1-PC\INFOSOFT12;" +
                //                                    "Initial Catalog=DBASE;" +
                //                                    "User id=sa;" +
                //                                    "Password=infocic;");

                //SqlDataAdapter da1 = new SqlDataAdapter("select * from accounts where a_key='010201001'", con);
                //SqlDataAdapter da2 = new SqlDataAdapter("select * from acc_hdr where a_ref=7", con);
                //SqlDataAdapter da3 = new SqlDataAdapter("SELECT acc_dtl.a_key, accounts.a_name, acc_dtl.a_text, acc_dtl.a_camt, acc_dtl.a_damt"
                //                                       + " FROM acc_dtl INNER JOIN"
                //                                       + " accounts ON acc_dtl.a_key = accounts.a_key"
                //                                       + " where acc_dtl.a_key='010201001'", con);

                //  DataSet ds1 = new DataSet();

                // ds1.Tables["accounts"]=dthdr;
                // ds1.Tables["accounts"] = dthdr;
                //  da3.Fill(ds1, "details");

                // this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds1));

                //ReportParameter[] parameters = new ReportParameter[1];
                //parameters[0] = new ReportParameter("comp_name", "hi");

                //rf.reportViewer1.LocalReport.SetParameters(parameters);

                // rf.reportViewer1.LocalReport.SetParameters("comp_name", BL.CLS_Session.cmp_name);

                // rf.reportViewer1.LocalReport.Refresh();
                /*
                 DataTable dttt = new DataTable();
                 foreach (DataGridViewColumn col in dataGridView1.Columns)
                 {
                     dttt.Columns.Add(col.Name);
                     // dt.Columns.Add(col.HeaderText);
                 }

                 foreach (DataGridViewRow row in dataGridView1.Rows)
                 {
                     DataRow dRow = dttt.NewRow();
                     foreach (DataGridViewCell cell in row.Cells)
                     {
                         dRow[cell.ColumnIndex] = cell.Value;
                     }
                     dttt.Rows.Add(dRow);
                 }
                */

              

                DataTable dt2 = new DataTable();
                int cn = 1;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    /*
                    dt.Columns.Add(col.Name);
                    // dt.Columns.Add(col.HeaderText);
                     * */
                    dt2.Columns.Add("c" + cn.ToString());
                    cn++;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataRow dRow = dt2.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    dt2.Rows.Add(dRow);
                }

                DataTable dt = new DataTable();
                dt = dt2.Copy();
                foreach (DataRow row in dt.Rows)
                {/*
                    DataView dv1 = dtunits.DefaultView;
                    dv1.RowFilter = "unit_id ='" + row[2] +"'";
                    DataTable dtNew = dv1.ToTable();
                    //dcombo.DataSource = dtNew;
                    row[2] = dtNew.Rows[0][1];
                  * */
                    DataRow[] res = dtunits.Select("unit_id ='" + row[2] + "'");
                    row[2] = res[0][1];
                    // row[6] = "%" + row[6];
                }

                rf.reportViewer1.LocalReport.DataSources.Clear();
                // rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("acc_hdr", dthdr));
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("sdt", dt));

                if (File.Exists(Directory.GetCurrentDirectory() + @"\reports\Sto_Convert_rpt.rdlc"))
                    rf.reportViewer1.LocalReport.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Sto_Convert_rpt.rdlc";
                else
                    rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sto.rpt.Sto_Convert_rpt.rdlc";
                

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[13];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                //  parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());
                parameters[1] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[2] = new ReportParameter("type", cmb_type.Text);
                parameters[3] = new ReportParameter("ref", txt_ref.Text);
                parameters[4] = new ReportParameter("date", txt_mdate.Text);
                parameters[5] = new ReportParameter("fwh_name", cmb_frwh.Text);

                parameters[6] = new ReportParameter("desc", txt_desc.Text);
                parameters[7] = new ReportParameter("twh_name", cmb_towh.Text);
                parameters[8] = new ReportParameter("total", txt_total.Text);
                parameters[9] = new ReportParameter("descount", txt_des.Text);
                parameters[10] = new ReportParameter("tax", "0");
                parameters[11] = new ReportParameter("nettotal", txt_total.Text);

                BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(txt_total.Text), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);

                //  MessageBox.Show(toWord.ConvertToArabic());
                parameters[12] = new ReportParameter("a_toword", toWord.ConvertToArabic());

                rf.reportViewer1.LocalReport.SetParameters(parameters);

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();


                /*
                Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();

                //LocalReport report = new LocalReport();
                //report.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Sales_direct.rdlc";
                //report.DataSources.Add(new ReportDataSource("acc_hdr", dthdr));
                //report.DataSources.Add(new ReportDataSource("details", dtdtl));

                //ReportParameter[] parameters = new ReportParameter[3];
                //parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                //parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());

                //rf.reportViewer1.LocalReport.SetParameters(parameters);
                //rf.reportViewer1.RefreshReport();
                //rf.MdiParent = ParentForm;
                //rf.Show();

                rf.reportViewer1.LocalReport.DataSources.Clear();
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("acc_hdr", dthdr));
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("details", dtdtl));

                rf.reportViewer1.LocalReport.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Acc_Tran_rpt.rdlc";

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());

                rf.reportViewer1.LocalReport.SetParameters(parameters);

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();
                * */
            }
            catch { }

            //catch (Exception ex)
            //{ 
            //    MessageBox.Show(ex.Message); 
            //}
                 
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {/*
            try
            {
                double sumc = 0, sumd = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    sumc = sumc + Convert.ToDouble(row.Cells[3].Value);
                    sumd = sumd + Convert.ToDouble(row.Cells[4].Value);
                }

                txt_des.Text = sumc.ToString();
                txt_tatal.Text = sumd.ToString();
            }
            catch{}
          */
        }

        private void txt_camt_TextChanged(object sender, EventArgs e)
        {/*
            if (Convert.ToDouble(txt_camt.Text) > Convert.ToDouble(txt_amt.Text))
            {
                MessageBox.Show("تجاوز مبلغ القيد","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
          */
            if (string.IsNullOrEmpty(txt_des.Text))
                txt_des.Text = "0";
            if ((Convert.ToDouble(txt_desper.Text) > Convert.ToDouble(BL.CLS_Session.inv_dsc) && isnew) || (Convert.ToDouble(txt_desper.Text) > Convert.ToDouble(BL.CLS_Session.inv_dsc) && isupdate))
            {
                MessageBox.Show("تجاوزت الخصم المسموح لك");
                txt_des.Text = "0";
                txt_desper.Text = "0";
            }
          //  if (string.IsNullOrEmpty(txt_des.Text))
            //    txt_des.Text = "0";
          //  total();
        }

        private void txt_damt_TextChanged(object sender, EventArgs e)
        {/*
            if (Convert.ToDouble(txt_damt.Text) > Convert.ToDouble(txt_amt.Text))
            {
                MessageBox.Show("تجاوز مبلغ القيد","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
          */
        }

        private void نسخToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(dataGridView1.CurrentCell.Value.ToString());
            }
            catch { }
        }

        private void حذفالصفToolStripMenuItem_Click(object sender, EventArgs e)
        {
             try
            {
            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
            catch { }
        }

        private void كشفحسابToolStripMenuItem_Click(object sender, EventArgs e)
        {
             try
            {
            // dataGridView1_CellDoubleClick( null,  null);
                Acc.Acc_Statment_Exp f4a = new Acc.Acc_Statment_Exp(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                f4a.MdiParent = ParentForm;
                f4a.Show();
            }
            catch { }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    // Add this
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    // Can leave these here - doesn't hurt
                   // dataGridView1.Rows[e.RowIndex].Selected = true;
                    dataGridView1.Focus();
                }
            }
            catch { }

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 4 || dataGridView1.CurrentCell.ColumnIndex == 5 || dataGridView1.CurrentCell.ColumnIndex == 6) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl tb = e.Control as DataGridViewTextBoxEditingControl;
                tb.KeyDown -= dataGridView1_KeyDown;
                tb.KeyDown += dataGridView1_KeyDown;
            }
            //if (dataGridView1.CurrentCell.ColumnIndex == 0 )//|| dataGridView1.CurrentCell.ColumnIndex == 5 || dataGridView1.CurrentCell.ColumnIndex == 6) //Desired Column
            //{
            //    TextBox tb = e.Control as TextBox;
            //    if (tb != null)
            //    {
            //        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress2);
            //    }
            //}
          
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        //private void Column1_KeyPress2(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //}

        private void dataGridView1_Enter(object sender, EventArgs e)
        {
           // dataGridView1.CellEnter += new DataGridViewCellEventHandler(dataGridView1_CellEnter);
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
           // dvc = dataGridView1.CurrentRow.Cells[2].Value.ToString();
           
            
            try
            {
                if (dataGridView1.CurrentCell == dataGridView1[4, dataGridView1.CurrentRow.Index] || dataGridView1.CurrentCell == dataGridView1[5, dataGridView1.CurrentRow.Index] || dataGridView1.CurrentCell == dataGridView1[6, dataGridView1.CurrentRow.Index])
            {
                //   total();

              //  DataRow[] dtrvat = dtvat.Select("tax_id ='" + dataGridView1.CurrentRow.Cells[8].Value + "'");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
              //  dataGridView1.CurrentRow.Cells[7].Value = ((Convert.ToDouble(dtrvat[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)).ToString();
              //  dataGridView1.CurrentRow.Cells[6].Value = (Convert.ToDouble(dtrvat[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
               
              //  var filteredData = dtvat.Select("tax_id='" + dt222.Rows[0][11] + "'").CopyToDataTable();
              //  dataGridView1.CurrentRow.Cells[7].Value = ((Convert.ToDouble(filteredData.Rows[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)).ToString();
              //  dataGridView1.CurrentRow.Cells[6].Value = (Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value) * Convert.ToDouble(0))/100);            
             //   dataGridView1.CurrentRow.Cells[6].Value = Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value);
               
             // dataGridView1.CurrentRow.Cells[5].Value = dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][5].ToString()) ? Convert.ToDouble(dt222.Rows[0][23].ToString()) : (dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][12].ToString()) ? (Convert.ToDouble(dt222.Rows[0][23].ToString()) * Convert.ToDouble(dt222.Rows[0][13].ToString())) : (dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][15].ToString()) ? (Convert.ToDouble(dt222.Rows[0][23].ToString()) * Convert.ToDouble(dt222.Rows[0][16].ToString())) : (Convert.ToDouble(dt222.Rows[0][23].ToString()) * Convert.ToDouble(dt222.Rows[0][19].ToString()))));
              //  dataGridView1.CurrentRow.Cells[5].Value = Convert.ToDouble(dt222.Rows[0][23].ToString());
                dataGridView1.CurrentRow.Cells[6].Value = Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value);
               
             }
            if (dataGridView1.CurrentCell == dataGridView1[2, dataGridView1.CurrentRow.Index] && isupdate)
            {
                /*
                DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                //  DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
                // dt.Clear();
                dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where rtrim(ltrim(item_no))='" + dataGridView1.CurrentRow.Cells[0].Value + "'");
                //  dataGridView1.CurrentRow.Cells[1].Value = dt222.Rows[0][1];
                //   dataGridView1.CurrentRow.Cells[0].Value = dt222.Rows[0][1];
                //  dataGridView1.CurrentRow.Cells[4].Value = 1.00;
                // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][0];
                DataView dv1 = dtunits.DefaultView;
                // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                DataTable dtNew = dv1.ToTable();

                dcombo.DataSource = dtNew;
                dcombo.DisplayMember = "unit_name";
                dcombo.ValueMember = "unit_id";

                dataGridView1.CurrentRow.Cells[2] = dcombo;
               // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];// dt222.Rows[0][5];
                dcombo.FlatStyle = FlatStyle.Flat;
                 */
            }
           
            //if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[3])
            //{
            //    // dataGridView1.Rows.Add(1);
            //   // SendKeys.Send("{left 1}");
            //    dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[4];
            //}

            //try
            //{
            //if (dataGridView1.CurrentCell.RowIndex == dataGridView1.NewRowIndex)
            //{
            //   // dataGridView1.Rows.Add(1);
            //    SendKeys.Send("{Down}");
            //}
            //if (dataGridView1.CurrentCell == dataGridView1[2, dataGridView1.CurrentRow.Index])//&& dataGridView1.CurrentCell.RowIndex == dataGridView1.NewRowIndex)
            //{
            //   // SendKeys.Send("{Down}");
            //    dataGridView1_KeyDown(null, null);
            //}
            total();
            }
            catch { }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            //{
            //    MessageBox.Show("Pressed " + Keys.Shift);
            //}
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
        }

        private void load_key()
        {
            DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select a_name from accounts where a_key='" + txt_key.Text + "' and a_brno='" + BL.CLS_Session.brno + "'");
            // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

            if (dt.Rows.Count > 0)
                txt_name.Text = dt.Rows[0][0].ToString();
            else
                txt_name.Text = "";
            /*
            DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select cu_name from customers where cu_no='" + txt_custno.Text + "' and cu_brno='" + BL.CLS_Session.brno + "'");
            // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

            if (dt2.Rows.Count > 0)
                txt_custnam.Text = dt2.Rows[0][0].ToString();
            else
                txt_custnam.Text = "";
            */

            DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select su_no ,su_name ,cl_acc,su_crlmt from suppliers a join suclass b on a.su_class=b.cl_no  where a.inactive=0 and a.su_brno='" + BL.CLS_Session.brno + "' and su_no='" + txt_custno.Text + "'");

            // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

            if (dt2.Rows.Count > 0)
            {
                txt_custnam.Text = dt2.Rows[0][1].ToString();
                txt_temp.Text = dt2.Rows[0][2].ToString();
                txt_crlmt.Text = dt2.Rows[0][3].ToString();
            }
            else
            {
                txt_custnam.Text = "";
                txt_temp.Text = "";
                txt_crlmt.Text = "";
            }

            //    txt_code.Text = dth.Rows[0][2].ToString();
            //   txt_opnbal.Text = dth.Rows[0][1].ToString();

        }

        private void txt_key_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8 && dataGridView1.ReadOnly==false)
            {
                txt_name.Text = "";
                try
                {
                    Search_All f4 = new Search_All("1", "Acc");
                    f4.ShowDialog();
                    txt_key.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                }
                catch { }

            }
            if (e.KeyCode == Keys.Enter)
            {
               // txt_name.Text = "";
                try
                {
                   // load_key();
                    dataGridView1.Focus();
                }
                catch { }

            }
        }

        private void cmb_salctr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_mdate.Enabled == true)
                {
                    txt_mdate.Focus();
                    // txt_mdate.Select();
                    txt_mdate.SelectionStart = 0;
                    //   txt_mdate.SelectionLength = 0;// txt_mdate.Text.Length;  
                }
                else
                {
                    cmb_towh.Focus();
                }
                //cmb_type.Focus();
                // txt_mdate.Select();
                //  txt_mdate.SelectionStart = 0;
              //  txt_mdate.SelectionLength = 0;// txt_mdate.Text.Length;  
            }
        }

        private void txt_remark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridView1.Focus();
            }
        }

        private void txt_key_Layout(object sender, LayoutEventArgs e)
        {
           // load_key();
        }

        private void txt_key_Leave(object sender, EventArgs e)
        {
            load_key();
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
          // dvc = dataGridView1.CurrentRow.Cells[2].ToString();
          // MessageBox.Show(dvc);
            try {
                if (BL.CLS_Session.up_sal_icost)
                    txt_icost.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                else
                    txt_icost.Text = "-";
            }
            catch { }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void txt_camt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void total()
        {
            double sum = 0, sumv = 0, sumc = 0;
            foreach (DataGridViewRow ro in dataGridView1.Rows)
            {
                sum = sum + Convert.ToDouble(ro.Cells[6].Value);
                // sumv = sumv + Convert.ToDouble(ro.Cells[6].Value);
                sumc = sumc + (Convert.ToDouble(ro.Cells[7].Value) * Convert.ToDouble(ro.Cells[4].Value));
            }
            //  textBox1.Text = sum.ToString();
            txt_total.Text = Math.Round(sum, 4).ToString();
            txt_cost.Text = Math.Round(sumc, 4).ToString();
            // txt_tax.Text = sumv.ToString();
            txt_net.Text = (Math.Round((Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_des.Text)), 4)).ToString();
        }

        //private void total()
        //{
        //    double sum = 0,sumv=0,sumc=0;
        //    foreach (DataGridViewRow ro in dataGridView1.Rows)
        //    {
        //        sum = sum + Convert.ToDouble(ro.Cells[3].Value) * Convert.ToDouble(ro.Cells[4].Value) * Convert.ToDouble(ro.Cells[5].Value) ;
        //       // sumv = sumv + Convert.ToDouble(ro.Cells[6].Value);
        //        sumc = sumc + (Convert.ToDouble(ro.Cells[7].Value) * Convert.ToDouble(ro.Cells[4].Value));
        //    }
        //  //  textBox1.Text = sum.ToString();
        //    txt_total.Text = Math.Round(sum,2).ToString();
        //    txt_cost.Text = Math.Round(sumc,2).ToString();
        //   // txt_tax.Text = sumv.ToString();
        //    txt_net.Text = (Math.Round((Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_des.Text)) ,2)).ToString();
        //}

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {/*
            if (dataGridView1.CurrentRow.Cells[dataGridView1.CurrentCell.ColumnIndex].ReadOnly)
            {
                //   dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex + 1, dataGridView1.CurrentRow.Index];
                //  SendKeys.Send("{F4}");
                //   SendKeys.Send("{Down}");
                SendKeys.Send("{Tab}");
                //e.SuppressKeyPress = true;
                //int iColumn = dataGridView1.CurrentCell.ColumnIndex;
                //int iRow = dataGridView1.CurrentCell.RowIndex;
                //if (iColumn == dataGridView1.Columns.Count - 1)
                //    dataGridView1.CurrentCell = dataGridView1[0, iRow + 1];
                //else
                //    dataGridView1.CurrentCell = dataGridView1[iColumn + 1, iRow];
            }
          * */
        }

        private void txt_desper_TextChanged(object sender, EventArgs e)
        {
            /*
            if (string.IsNullOrEmpty(txt_desper.Text))
                txt_desper.Text = "0";

            if ((Convert.ToDouble(txt_desper.Text) > Convert.ToDouble(BL.CLS_Session.inv_dsc) && isnew) || (Convert.ToDouble(txt_desper.Text) > Convert.ToDouble(BL.CLS_Session.inv_dsc) && isupdate))
            {
                MessageBox.Show("تجاوزت الخصم المسموح لك");
                txt_des.Text = "0";
                txt_desper.Text = "0";
            }
             * */
        }

        private void txt_des_Leave(object sender, EventArgs e)
        {
            txt_desper.Text = (Math.Round((100 * (Convert.ToDouble(txt_des.Text)) / Convert.ToDouble(txt_total.Text)),2)).ToString();

            total();
        }

        private void txt_desper_Leave(object sender, EventArgs e)
        {
            txt_des.Text = (Math.Round(((Convert.ToDouble(txt_desper.Text) * Convert.ToDouble(txt_total.Text)) / 100),2)).ToString();
            total();
        }

        private void txt_desper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_desper_Leave(null, null);
                txt_des.Focus();
            }
        }

        private void txt_des_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_des_Leave(null, null);
                txt_net.Focus();
            }
        }

        private void txt_desper_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch { }
        }

        private void cmb_salctr_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_desc.Text))
                    txt_desc.Text = cmb_type.Text + " - " + cmb_frwh.Text;

                if (cmb_type.SelectedValue.Equals("05") || cmb_type.SelectedValue.Equals("15"))
                {
                    txt_key.Text = "";
                    txt_name.Text = "";
                }
                cmb_salctr_Leave(null, null);
            }
            catch { }
        }

        private void txt_custno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8 && dataGridView1.ReadOnly==false)
            {
                txt_name.Text = "";
                try
                {
                    Search_All f4 = new Search_All("7", "Sup");
                    f4.ShowDialog();
                    txt_custno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_custnam.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                }
                catch { }

            }
            if (e.KeyCode == Keys.Enter)
            {
                txt_remark.Focus();
            }
        }

        private void txt_custno_Leave(object sender, EventArgs e)
        {
            load_key();
        }

         private void save_acc()
        {
          //  cmb_salctr_Leave(null, null);

            string strkey,strcustno;
            try
            {
                if (cmb_type.SelectedValue.ToString().Equals("05") || cmb_type.SelectedValue.ToString().Equals("15"))
                {
             
                    strkey = txt_temp.Text;
                    strcustno = txt_custno.Text;
                }
                else { 
                    strkey = txt_key.Text;
                    strcustno = "";
                }

                string mdate, hdate;
                mdate = txt_mdate.Text.Replace("-", "").Substring(4, 4) + txt_mdate.Text.Replace("-", "").Substring(2, 2) + txt_mdate.Text.Replace("-", "").Substring(0, 2);
                hdate = txt_hdate.Text.Replace("-", "").Substring(4, 4) + txt_hdate.Text.Replace("-", "").Substring(2, 2) + txt_hdate.Text.Replace("-", "").Substring(0, 2);

                DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(a_ref)," + BL.CLS_Session.dtbranch.Rows[0]["jv_" + cmb_type.SelectedValue + ""] + ") from acc_hdr where a_type='" + cmb_type.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "'");

                //int ref_max = Convert.ToInt32(dt.Rows[0][0]) + 1;

                if (isnew)
                {
                    daml.SqlCon_Open();
                    int exexcuted = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_brno,a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,usrid,jhsrc,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + ref_max + ",'" + mdate + "','" + hdate + "','" + txt_desc.Text + "'," + (Convert.ToDouble(txt_total.Text) + Convert.ToDouble(0)) + "," + (dataGridView1.RowCount - 1) + ",'" + txt_user.Text + "','Sal','"+ cmb_frwh.SelectedValue +"')", false);
                    daml.SqlCon_Close();                                          
                }
                else
                {
                    daml.SqlCon_Open();
                    int exexcuted = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_mdate='" + mdate + "', a_hdate='" + hdate + "',a_text='" + txt_desc.Text + "',a_amt=" + txt_total.Text + ",a_entries=" + (dataGridView1.RowCount - 1) + ",usrid='" + txt_user.Text + "' where a_ref=" + txt_ref.Text + " and a_type='" + cmb_type.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "'", false);
                    exexcuted = exexcuted + daml.Insert_Update_Delete_retrn_int("delete from acc_dtl where a_ref=" + txt_ref.Text + " and a_type='" + cmb_type.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "' and sl_no='" + cmb_frwh.SelectedValue + "'", false);
                    daml.SqlCon_Close();
                }

               
                    // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                switch (cmb_type.SelectedValue.ToString())
                {
                    case "05":
                       // strkey = txt_temp.Text;
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strcrdt + "','" + txt_desc.Text + "'," + txt_total.Text + ",0,1,'" + mdate + "','" + hdate + "','C','" + cmb_frwh.SelectedValue + "')", false);
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,cu_no,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strkey + "','" + txt_desc.Text + "',0," + txt_net.Text + ",3,'" + mdate + "','" + hdate + "','D','" + txt_custno.Text + "','" + cmb_frwh.SelectedValue + "')", false);
                       
                        if (Convert.ToDouble(txt_des.Text) > 0)
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strdsc + "','خصومات مبيعات'," + 0 + "," + txt_des.Text + ",3,'" + mdate + "','" + hdate + "','D','" + cmb_frwh.SelectedValue + "')", false);
                        }
                        daml.SqlCon_Close();
                        break;

                    case "15":
                       // strkey = txt_temp.Text;
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strrcrdt + "','" + txt_desc.Text + "',0," + (Convert.ToDouble(txt_net.Text) + Convert.ToDouble(txt_des.Text) - Convert.ToDouble(0)) + ",1,'" + mdate + "','" + hdate + "','D','" + cmb_frwh.SelectedValue + "')", false);
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,cu_no,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strkey + "','" + txt_desc.Text + "'," + txt_net.Text + "," + 0 + ",3,'" + mdate + "','" + hdate + "','C','" + txt_custno.Text + "','" + cmb_frwh.SelectedValue + "')", false);
                        
                        if (Convert.ToDouble(txt_des.Text) > 0)
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strdsc + "','خصومات مبيعات'," + txt_des.Text + "," + 0 + ",3,'" + mdate + "','" + hdate + "','C','" + cmb_frwh.SelectedValue + "')", false);
                        }
                        daml.SqlCon_Close();
                        break;

                    case "04":
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strcash + "','" + txt_desc.Text + "'," + txt_total.Text + ",0,1,'" + mdate + "','" + hdate + "','C','" + cmb_frwh.SelectedValue + "')", false);
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,cu_no,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strkey + "','" + txt_desc.Text + "',0," + txt_net.Text + ",3,'" + mdate + "','" + hdate + "','D','" + txt_custno.Text + "','" + cmb_frwh.SelectedValue + "')", false);
                        
                        if (Convert.ToDouble(txt_des.Text) > 0)
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strdsc + "','خصومات مبيعات'," + 0 + "," + txt_des.Text + ",3,'" + mdate + "','" + hdate + "','D','" + cmb_frwh.SelectedValue + "')", false);
                        }
                        daml.SqlCon_Close();
                        break;

                    case "14":
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strrcash + "','" + txt_desc.Text + "',0," + (Convert.ToDouble(txt_net.Text) + Convert.ToDouble(txt_des.Text) - Convert.ToDouble(0)) + ",1,'" + mdate + "','" + hdate + "','D','" + cmb_frwh.SelectedValue + "')", false);
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,cu_no,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strkey + "','" + txt_desc.Text + "'," + txt_net.Text + "," + 0 + ",3,'" + mdate + "','" + hdate + "','C','" + txt_custno.Text + "','" + cmb_frwh.SelectedValue + "')", false);
                       
                        if (Convert.ToDouble(txt_des.Text) > 0)
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strdsc + "','خصومات مبيعات'," + txt_des.Text + "," + 0 + ",3,'" + mdate + "','" + hdate + "','C','" + cmb_frwh.SelectedValue + "')", false);
                        }
                        daml.SqlCon_Close();
                        break;
                }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
           
        }

        private void cmb_salctr_Leave(object sender, EventArgs e)
        {
           //  DataRow[] dr = dtslctr.Select("wh_no = '" + cmb_wh.SelectedValue + "'");
           //// strcashr = dr[0][3].ToString();
           // strcash = dr[0][4].ToString();
           // strcrdt = dr[0][5].ToString();
           // strrcash = dr[0][6].ToString();
           // strrcrdt = dr[0][7].ToString();
           // strdsc = dr[0][8].ToString();
           // strtax = dr[0][9].ToString();
            stwhno = cmb_frwh.SelectedValue.ToString(); // dr[0][14].ToString();
        }

        private double chek_bal_after()
        {
            
            double bal = 0;
            /*
            if (txt_custno.Text != "")
            {
                // double bal;
                DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("get_cust_bal @cust_no='" + txt_custno.Text + "', @branch_no='" + BL.CLS_Session.brno + "'");
                bal = Convert.ToDouble(dt.Rows[0][0]);
            }
            //txt_crlmt.Text=dt.Rows[0][0].ToString();
             */
            return bal;

             
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
           // txt_icost.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }

        private void cmb_towh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_desc.Focus();
            }
        }

        private void cmb_towh_Enter(object sender, EventArgs e)
        {
            try
            {
                var dv1 = new DataView();
                dv1 = dtslctr.DefaultView;
                // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                dv1.RowFilter = "wh_no not in(" + cmb_frwh.SelectedValue + ")";
                DataTable dtNew = dv1.ToTable();
                // MessageBox.Show(dtNew.Rows[0][1].ToString());
                cmb_towh.DataSource = dtNew;

                cmb_towh.DisplayMember = "wh_name";
                cmb_towh.ValueMember = "wh_no";
            }
            catch { }
        }

        private void cmb_towh_Leave(object sender, EventArgs e)
        {
            sttowhno = cmb_towh.SelectedValue.ToString();
        }

        private void chk_usebarcode_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_usebarcode.Checked==true)
            {
                dataGridView1.Columns[0].HeaderText = "الباركود";
                // dataGridView1.Columns[0].FillWeight = 20;
                // dataGridView1.Columns[1].Visible = false;
            }
            else
            {
                dataGridView1.Columns[0].HeaderText = "رقم الصنف";
            }
        }

        private void btn_Save_Enter(object sender, EventArgs e)
        {
            total();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!dataGridView1.CurrentRow.IsNewRow && dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0] && e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{Tab}");
                }
            }
            catch { }
        }

        private void txt_ref_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_qbl_Click(object sender, EventArgs e)
        {
            try
            {
                Fill_Data("", cmb_type.SelectedValue.ToString(), (Convert.ToInt32(txt_ref.Text) - 1).ToString());
                total();
            }
            catch { }
        }

        private void btn_tali_Click(object sender, EventArgs e)
        {
            try
            {
                Fill_Data("", cmb_type.SelectedValue.ToString(), (Convert.ToInt32(txt_ref.Text) + 1).ToString());
                total();
            }
            catch { }
        }
    }
}



