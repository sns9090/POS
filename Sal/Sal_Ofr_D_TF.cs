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
using System.Text.RegularExpressions;
using System.Drawing.Printing;
//using System.Drawing.Printing;
//using System.Threading.task

namespace POS.Sal
{
    public partial class Sal_Ofr_D_TF : BaseForm
    {
        double tax_per = BL.CLS_Session.tax_per;
        List<BL.CurrencyInfo> currencies = new List<BL.CurrencyInfo>();
       // public Sal_Tran_D frm;
       // DataTable dtg;
       // DataTable dt2,dtunits;
        DataTable dthdr, dtdtl, dt222, dtunits, dtsal, dtvat, dttrtyps, dtslctr, dtcolman, dtexits;//,dtbind;
       // int count = 0;
        int ref_max,cnt=0,cntto=0,temp=0,jrdacc;
        string a_slctr, a_ref, a_type, strcash, strrcash, strrcrdt, strcrdt, strdsc, strtax, strcashr, stwhno, strsndoq, stcstcode, stslcust, stmkhzon, inv_t, inv_r, inv_s;
        bool isnew,isupdate,notposted,shameltax,isfeched, is_printd = false,is_cash = false;
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate dtv = new BL.Date_Validate();
        SqlConnection con = BL.DAML.con;//new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        BindingSource bindsrc = new BindingSource();
        public Sal_Ofr_D_TF(string slctr, string atype, string aref)
        {
            InitializeComponent();
          //  this.KeyPreview = true;
            a_ref = aref;
            a_type = atype;
            a_slctr = slctr;
            //this.KeyPreview = true;
            //this.KeyPress += new KeyPressEventHandler(Control_KeyPress);

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;

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
                if (e.KeyCode == Keys.Delete && (btn_Add.Enabled != true && btn_Edit.Enabled != true))
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
                if (e.KeyCode == Keys.F5 && dataGridView1.ReadOnly == false && e.Modifiers != Keys.Control && !dataGridView1.CurrentRow.IsNewRow && !string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[0].Value.ToString()))
                {
                    Sto.Sto_CurBal_Items f4a = new Sto.Sto_CurBal_Items(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    f4a.ShowDialog();
                }

                if (e.KeyCode == Keys.F8 && dataGridView1.ReadOnly==false && e.Modifiers != Keys.Control)
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
                       // SendKeys.Send("F2");
                       // SendKeys.Send("F2");
                        dataGridView1.CurrentRow.Cells[1].Value = f4.dataGridView1.CurrentRow.Cells["i_b"].Value;
                        dataGridView1.CurrentRow.Cells[2].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
                      //  if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[4].Value) == 0 || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
                       // dataGridView1.CurrentRow.Cells[4].Value = "1.00";
                        if (dataGridView1.CurrentRow.Cells[5].Value == null)
                        dataGridView1.CurrentRow.Cells[5].Value = 0;
                        //if (dataGridView1.CurrentRow.Cells[2].Value == null)
                        //{
                        //    dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
                        //}


                        //////string item_bar = chk_usebarcode.Checked==false ? "  rtrim(ltrim(item_no))=" : "  rtrim(ltrim(item_barcode))=";
                        //////dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where " + item_bar + "'" + f4.dataGridView1.CurrentRow.Cells[0].Value + "'");
                        string item_bar = chk_usebarcode.Checked == false ? "  rtrim(ltrim(item_no))='" + f4.dataGridView1.CurrentRow.Cells[0].Value + "'" : "  rtrim(ltrim(item_barcode))='" + f4.dataGridView1.CurrentRow.Cells[1].Value + "'";
                        // dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where " + item_bar + "'" + f4.dataGridView1.CurrentRow.Cells[0].Value + "'");
                        //  dt222 = daml.SELECT_QUIRY_only_retrn_dt("select *,0 pk_qty from items where " + item_bar + "");
                        dt222 = daml.SELECT_QUIRY_only_retrn_dt("select *,1.00 pk_qty from items where rtrim(ltrim(item_no))='" + f4.dataGridView1.CurrentRow.Cells[0].Value + "'");
                        /*
                        DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                        DataView dv1 = dtunits.DefaultView;
                        dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                        DataTable dtNew = dv1.ToTable();
                        dcombo.DataSource = dtNew;
                        dcombo.DisplayMember = "unit_name";
                        dcombo.ValueMember = "unit_id";
                        dcombo.Value = dt222.Rows[0][5];
                        dataGridView1.CurrentRow.Cells[3] = dcombo;
                      // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                        dcombo.FlatStyle = FlatStyle.Flat;
                        */
                        dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0]["item_name"];
                        if (dataGridView1.CurrentRow.Cells[6].Value == null)
                            dataGridView1.CurrentRow.Cells[6].Value = dt222.Rows[0]["item_price"];
                        dataGridView1.CurrentRow.Cells[4].Value = dt222.Rows[0]["uq2"];
                        if(dataGridView1.CurrentRow.Cells[3].Value==null)
                             dataGridView1.CurrentRow.Cells[3].Value = 0;
                        dataGridView1.CurrentRow.Cells[10].Value = dt222.Rows[0][11];
                        dataGridView1.CurrentRow.Cells[11].Value = dt222.Rows[0]["item_curcost"];
                        dataGridView1.CurrentRow.Cells[1].Value = dt222.Rows[0]["item_barcode"];
                        dataGridView1.CurrentRow.Cells[13].Value = dt222.Rows[0]["item_cost"];
                        dataGridView1.CurrentRow.Cells[14].Value = dt222.Rows[0]["item_unit"];
                        dataGridView1.CurrentRow.Cells["cur_bal"].Value = dt222.Rows[0]["item_cbalance"];
                        //if (dataGridView1.CurrentCell.RowIndex == dataGridView1.NewRowIndex)
                        //{
                        //   // dataGridView1.Rows.Add(1);
                        //    SendKeys.Send("{Down}");
                        //}

                        chk_shaml_tax.Enabled = false;
                        dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];



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

                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F8 && dataGridView1.ReadOnly == false)
                {
                    Search_All f4 = new Search_All("chkalter", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    f4.ShowDialog();
                    try
                    {
                        dataGridView1.CurrentRow.Cells[0].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                        SendKeys.Send("{F2}");
                        SendKeys.Send("{ENTER}");

                    }
                    catch { }
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

                //if (dataGridView1.CurrentCell==dataGridView1.CurrentRow.Cells[1] || dataGridView1.CurrentCell==dataGridView1.CurrentRow.Cells[3] ||dataGridView1.CurrentCell==dataGridView1.CurrentRow.Cells[6])
                //{

                DataGridView dg = (DataGridView)sender;

                //if (dg.CurrentCell.EditType == typeof(DataGridViewComboBoxEditingControl) && isnew)
                //{
                //    //dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where rtrim(ltrim(item_no))='" + dataGridView1.CurrentRow.Cells[0].Value + "'");

                //    //DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                //    //DataView dv1 = dtunits.DefaultView;
                //    //dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                //    //DataTable dtNew = dv1.ToTable();
                //    //dcombo.DataSource = dtNew;
                //    //dcombo.DisplayMember = "unit_name";
                //    //dcombo.ValueMember = "unit_id";
                //    //dcombo.Value = dt222.Rows[0][5];
                //    //dataGridView1.CurrentRow.Cells[2] = dcombo;
                //    //// dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                //    //dcombo.FlatStyle = FlatStyle.Flat;
                //    //   dataGridView1.BeginEdit(true);
                //    SendKeys.Send("{F4}");
                //    //  dataGridView1.BeginEdit(true);
                //    //  ((ComboBox)dataGridView1.EditingControl).DroppedDown = true;
                //    SendKeys.Send("{Down}");
                //    SendKeys.Send("{UP}");
                //    //// dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                //    //  SendKeys.Send("{F2}");
                //    //  dataGridView1.BeginEdit(true);
                //}

                //if (dg.CurrentCell.EditType == typeof(DataGridViewComboBoxEditingControl) && isupdate)
                //{
                //    /*
                //    DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                //    //  DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
                //    // dt.Clear();
                //    dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where rtrim(ltrim(item_no))='" + dataGridView1.CurrentRow.Cells[0].Value + "'");
                //  //  dataGridView1.CurrentRow.Cells[1].Value = dt222.Rows[0][1];
                //    //   dataGridView1.CurrentRow.Cells[0].Value = dt222.Rows[0][1];
                //  //  dataGridView1.CurrentRow.Cells[4].Value = 1.00;
                //    // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][0];
                //    DataView dv1 = dtunits.DefaultView;
                //    // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                //    dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                //    DataTable dtNew = dv1.ToTable();

                //    dcombo.DataSource = dtNew;
                //    dcombo.DisplayMember = "unit_name";
                //    dcombo.ValueMember = "unit_id";

                //    dataGridView1.CurrentRow.Cells[2] = dcombo;
                //   // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                //    dcombo.FlatStyle = FlatStyle.Flat;
                //    */
                //    //if (dataGridView1.CurrentCell == dataGridView1[2, dataGridView1.CurrentRow.Index] && isupdate)
                //    //{
                //    /*
                //        DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                //        //  DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
                //        // dt.Clear();
                //        dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where rtrim(ltrim(item_no))='" + dataGridView1.CurrentRow.Cells[0].Value + "'");
                //        //  dataGridView1.CurrentRow.Cells[1].Value = dt222.Rows[0][1];
                //        //   dataGridView1.CurrentRow.Cells[0].Value = dt222.Rows[0][1];
                //        //  dataGridView1.CurrentRow.Cells[4].Value = 1.00;
                //        // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][0];
                //        DataView dv1 = dtunits.DefaultView;
                //        // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                //        dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                //        DataTable dtNew = dv1.ToTable();

                //        dcombo.DataSource = dtNew;
                //        dcombo.DisplayMember = "unit_name";
                //        dcombo.ValueMember = "unit_id";

                //        dataGridView1.CurrentRow.Cells[2] = dcombo;
                //        dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];// dt222.Rows[0][5];
                //        dcombo.FlatStyle = FlatStyle.Flat
                //     */
                //    //  }
                //    // dataGridView1.BeginEdit(true);
                //    SendKeys.Send("{F4}");
                //    //  dataGridView1.BeginEdit(true);
                //    //  ((ComboBox)dataGridView1.EditingControl).DroppedDown = true;
                //    SendKeys.Send("{Down}");
                //    SendKeys.Send("{UP}");
                //    // SendKeys.Send("{F2}");
                //    // dataGridView1.BeginEdit(true);

                //}
                if (dataGridView1.CurrentCell == dataGridView1[9, dataGridView1.CurrentRow.Index])//.ReadOnly == true || dataGridView1[3, dataGridView1.CurrentRow.Index].ReadOnly == true || dataGridView1[6, dataGridView1.CurrentRow.Index].ReadOnly == true)
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
            if (dataGridView1.CurrentRow.Cells[3].Value==null)
                dataGridView1.CurrentRow.Cells[3].Value = 0;
               
                    //////DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                    ////////  DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
                    //////// dt.Clear();
                    //////string item_bar = chk_usebarcode.Checked==false ? "  rtrim(ltrim(item_no))=" : "  rtrim(ltrim(item_barcode))=";
                    //////dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where " + item_bar + "'" + dataGridView1.CurrentRow.Cells[0].Value + "'");
                 DataTable dtchkbar = new DataTable();

             //   DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                    //  DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
                    // dt.Clear();
                    string quiryt;
                    if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[1])
                    {
                        if (chk_usebarcode.Checked)
                        {
                            quiryt = "SELECT items.item_no, items.item_name, items.item_cost, b.price AS item_price, b.barcode AS item_barcode, b.pack AS item_unit, items.item_obalance,"
                                    + "items.item_cbalance, items.item_group, items.item_image, items.item_req, items.item_tax, items.unit2, items.uq2, items.unit2p, items.unit3, items.uq3, items.unit3p,"
                                    + "items.unit4, items.uq4, items.unit4p, items.item_ename, items.item_opencost, items.item_curcost, items.supno, items.note, items.last_updt, items.sgroup,"
                                    + "items.price2, b.pk_qty FROM items INNER JOIN items_bc AS b ON b.item_no = items.item_no AND b.barcode = '" + dataGridView1.CurrentRow.Cells[1].Value + "'";
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
                                   + "items.price2, b.pk_qty FROM items INNER JOIN items_bc AS b ON b.item_no = items.item_no AND b.barcode = '" + ns.dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";

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
                                quiryt = "select *,1.00 pk_qty from items where rtrim(ltrim(item_no))='" + dataGridView1.CurrentRow.Cells[0].Value + "'";
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
                            if (dataGridView1.CurrentRow.Cells[5].Value == null)
                                dataGridView1.CurrentRow.Cells[5].Value = 0;

                            dataGridView1.CurrentRow.Cells[0].Value = dt222.Rows[0][0];
                            dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0]["item_name"];
                            if (dataGridView1.CurrentRow.Cells[6].Value==null)
                            dataGridView1.CurrentRow.Cells[6].Value = dt222.Rows[0]["item_price"];
                          //  dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][1];
                           // if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[4].Value) == 0 || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
                            //    dataGridView1.CurrentRow.Cells[4].Value = "1.00";
                            //   dataGridView1.CurrentRow.Cells[0].Value = dt222.Rows[0][1];
                            //  dataGridView1.CurrentRow.Cells[4].Value = 1.00;
                            // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][0];
                            /*
                            DataView dv1 = dtunits.DefaultView;
                            // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
                            dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                            DataTable dtNew = dv1.ToTable();
                            */
                            // MessageBox.Show(dtNew.Rows[0][1].ToString());
                            // dataGridView2.DataSource = dtNew;
                            /*
                            dcombo.DataSource = dal.SelectData_query("select * from orpacking where pack_id in('" + dt2.Rows[0][1] + "','" + dt2.Rows[0][2] + "','" + dt2.Rows[0][4] + "')", null);
                            dcombo.DisplayMember = "pkname";
                            dcombo.ValueMember = "pack_id";
                            */
                            //ممتاز
                            /*
                            dcombo.DataSource = dtNew;
                            dcombo.DisplayMember = "unit_name";
                            dcombo.ValueMember = "unit_id";

                            //  dcombo.DisplayIndex = 0;

                            dataGridView1.CurrentRow.Cells[3] = dcombo;
                            // dcombo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                            //if (e.RowIndex == dataGridView1.NewRowIndex)
                            //{
                            //    dataGridView1.Rows.Add(1);
                            //}

                            //  dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                            if (isnew)
                                dataGridView1.CurrentRow.Cells[3].Value = dt222.Rows[0][5];

                            dcombo.FlatStyle = FlatStyle.Flat;
                            */
                            if (dataGridView1.CurrentRow.Cells[3].Value==null)
                                dataGridView1.CurrentRow.Cells[3].Value = 0;
                            dataGridView1.CurrentRow.Cells[4].Value = dt222.Rows[0]["uq2"];
                            dataGridView1.CurrentRow.Cells[10].Value = dt222.Rows[0][11];
                            dataGridView1.CurrentRow.Cells[11].Value = dt222.Rows[0]["item_curcost"];
                            dataGridView1.CurrentRow.Cells["cur_bal"].Value = dt222.Rows[0]["item_cbalance"];
                            dataGridView1.CurrentRow.Cells[1].Value = dt222.Rows[0]["item_barcode"];
                            dataGridView1.CurrentRow.Cells[13].Value = dt222.Rows[0]["item_cost"];
                            dataGridView1.CurrentRow.Cells[14].Value = dt222.Rows[0]["item_unit"];

                            chk_shaml_tax.Enabled = false;

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
                            Search_All f4 = new Search_All("2-2", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                            // 
                            dataGridView1.CurrentRow.Cells[0].Value = "";
                            f4.ShowDialog();
                            // f4.textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                            //    dataGridView1.CurrentRow
                            try
                            {
                                // SendKeys.Send("{.}");
                                // dataGridView1.Rows.Add(1);


                                dataGridView1.CurrentRow.Cells[0].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                                dataGridView1.CurrentRow.Cells[2].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
                              //  if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[4].Value) == 0 || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
                              //      dataGridView1.CurrentRow.Cells[4].Value = "1.00";
                                if (dataGridView1.CurrentRow.Cells[5].Value == null)
                                dataGridView1.CurrentRow.Cells[5].Value = 0;
                                //if (dataGridView1.CurrentRow.Cells[2].Value == null)
                                //{
                                //    dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
                                //}


                                // string  string item_bar = chk_usebarcode.Checked==false ? "  rtrim(ltrim(item_no))=" : "  rtrim(ltrim(item_barcode))="; = chk_usebarcode.Checked==false ? "  rtrim(ltrim(item_no))=" : "  rtrim(ltrim(item_barcode))=";
                                // dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items " + item_bar + "'" + f4.dataGridView1.CurrentRow.Cells[0].Value + "'");
                                //// dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where " + item_bar + "'" + f4.dataGridView1.CurrentRow.Cells[0].Value + "'");
                                string item_bar = chk_usebarcode.Checked == false ? "  rtrim(ltrim(item_no))='" + f4.dataGridView1.CurrentRow.Cells[0].Value + "'" : "  rtrim(ltrim(item_barcode))='" + f4.dataGridView1.CurrentRow.Cells[1].Value + "'";
                                dt222 = daml.SELECT_QUIRY_only_retrn_dt("select *,1.00 pk_qty from items where " + item_bar + "");


                                // DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                                /*
                                DataView dv1 = dtunits.DefaultView;
                                dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                                DataTable dtNew = dv1.ToTable();
                                dcombo.DataSource = dtNew;
                                dcombo.DisplayMember = "unit_name";
                                dcombo.ValueMember = "unit_id";
                                dcombo.Value = dt222.Rows[0][5];
                                dataGridView1.CurrentRow.Cells[3] = dcombo;
                                // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                                dcombo.FlatStyle = FlatStyle.Flat;
                                */
                                if (dataGridView1.CurrentRow.Cells[3].Value==null)
                                    dataGridView1.CurrentRow.Cells[3].Value = 0;
                                dataGridView1.CurrentRow.Cells[4].Value = dt222.Rows[0]["uq2"];
                                if (dataGridView1.CurrentRow.Cells[6].Value==null)
                                dataGridView1.CurrentRow.Cells[6].Value = dt222.Rows[0]["item_price"];
                                dataGridView1.CurrentRow.Cells[10].Value = dt222.Rows[0][11];
                                dataGridView1.CurrentRow.Cells[11].Value = dt222.Rows[0]["item_curcost"];
                                dataGridView1.CurrentRow.Cells["cur_bal"].Value = dt222.Rows[0]["item_cbalance"];
                                dataGridView1.CurrentRow.Cells[1].Value = dt222.Rows[0]["item_barcode"];
                                dataGridView1.CurrentRow.Cells[13].Value = dt222.Rows[0]["item_cost"];
                                dataGridView1.CurrentRow.Cells[14].Value = dt222.Rows[0]["item_unit"];
                                //if (dataGridView1.CurrentCell.RowIndex == dataGridView1.NewRowIndex)
                                //{
                                //   // dataGridView1.Rows.Add(1);
                                //    SendKeys.Send("{Down}");
                                //}


                               // dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];



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
                      if ((dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[7] && isnew) || (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[7] && isupdate))
                        {
                            if (Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value) > Convert.ToDouble(BL.CLS_Session.item_dsc))
                            {
                                MessageBox.Show("تجاوزت الخصم المسموح", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dataGridView1.CurrentRow.Cells[7].Value = 0;
                            }
                        }

                        if ((dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5] && isnew) || (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5] && isupdate))
                        {
                            if (BL.CLS_Session.sal_nobal.Equals("0") && Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) > Convert.ToDouble(txt_curbal.Text))
                            {
                                MessageBox.Show("غير مسموح بتجاوز الرصيد","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                dataGridView1.CurrentRow.Cells[5].Value = 0;
                                dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[5];
                              //  SendKeys.Send("{F2}");
                            }
                        }
                        /*
                            if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0])
                            {
                                if (chk_nodup.Checked)
                                {
                                    // string strdgvc=dataGridView1.CurrentRow.Cells[0].Value.ToString();
                                    // dataGridView1.CurrentRow.Cells[0].Value="";

                                    int count = 0;
                                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                                    //  for (int i = 0; i <= dataGridView1.Rows.Count-1 ; i++)
                                    {
                               
                                        // if (!dgvr.IsNewRow) continue;
                                        // if (dataGridView1.Rows[i].Cells[0].Value == dataGridView1.CurrentRow.Cells[0].Value)
                                        if (dgvr.Cells[0].Value == dataGridView1.CurrentRow.Cells[0].Value)
                                        {
                                            count = count + 1;
                                           // MessageBox.Show(count.ToString());
                                            if (count > 1)
                                            {
                                                MessageBox.Show("عدم تكرار الصنف");
                                                dataGridView1.CurrentRow.Cells[0].Value = "";
                                               // return;
                                            }
                                        }
                                        //  else
                                        //     dataGridView1.CurrentRow.Cells[0].Value = strdgvc;
                                    }
                                }
                            }
                        */
                        if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[3] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[6] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[7])
                        {
                            if (string.IsNullOrEmpty(dataGridView1.CurrentCell.Value.ToString()))
                                dataGridView1.CurrentCell.Value = 0.00;
                        }
                        //if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[3] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[5])
                        //{
                        //    if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[6].Value.ToString()))
                        //        dataGridView1.CurrentRow.Cells[6].Value = dt222.Rows[0][3].ToString();
                        //}
                /*
                        if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[3])
                        {
                            //DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                            //// dcombo.Name = dataGridView1.Columns["Column3"];
                            //// dcombo.Name = "Column3";
                            //dataGridView1.CurrentRow.Cells[2] = dcombo;
                            //dcombo.DataSource = dal.SelectData_query("select * from orpacking where pack_id in('" + dt.Rows[0][1] + "','" + dt.Rows[0][2] + "')", null);
                            //dcombo.DisplayMember = "pkname";
                            //dcombo.ValueMember = "pack_id";
                            //Select("
                            //if (chk_usebarcode.Checked || Convert.ToDouble(dt222.Rows[0]["pk_qty"]) != 0)
                            //    dataGridView1.CurrentRow.Cells[4].Value = dt222.Rows[0]["pk_qty"].ToString();
                                
                            //else
                            //    dataGridView1.CurrentRow.Cells[4].Value = dataGridView1.CurrentRow.Cells[3].Value.ToString().Equals(dt222.Rows[0][18].ToString()) ? dt222.Rows[0][19].ToString() : dataGridView1.CurrentRow.Cells[3].Value.ToString().Equals(dt222.Rows[0][15].ToString()) ? dt222.Rows[0][16].ToString() : dataGridView1.CurrentRow.Cells[3].Value.ToString().Equals(dt222.Rows[0][12].ToString()) ? dt222.Rows[0][13].ToString() : "1.00";
                            dataGridView1.CurrentRow.Cells[4].Value = dt222.Rows[0]["uq2"];
                            dataGridView1.CurrentRow.Cells[6].Value = dt222.Rows[0]["item_price"];  //dataGridView1.CurrentRow.Cells[3].Value.ToString().Equals(dt222.Rows[0][5].ToString()) ? dt222.Rows[0][3].ToString() : (dataGridView1.CurrentRow.Cells[3].Value.ToString().Equals(dt222.Rows[0][12].ToString()) ? dt222.Rows[0][14].ToString() : (dataGridView1.CurrentRow.Cells[3].Value.ToString().Equals(dt222.Rows[0][15].ToString()) ? dt222.Rows[0][17].ToString() : dt222.Rows[0][20].ToString()));
                           // dataGridView1.CurrentRow.Cells[7].Value = string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[7].Value.ToString()) ? 0 : dataGridView1.CurrentRow.Cells[7].Value;
                        if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[5].Value) == 0)
                            dataGridView1.CurrentRow.Cells[5].Value = "1.00";
                            //////dataGridView1.CurrentRow.Cells[3].Value = dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][18].ToString()) ? dt222.Rows[0][19].ToString() : dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][15].ToString()) ? dt222.Rows[0][16].ToString() : dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][12].ToString()) ? dt222.Rows[0][13].ToString() : "1.00";

                            //////if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[4].Value) == 0)
                            //////    dataGridView1.CurrentRow.Cells[4].Value = "0.00";

                            //////if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[5].Value) == 0)
                            //////    dataGridView1.CurrentRow.Cells[5].Value = dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][18].ToString()) ? dt222.Rows[0][20].ToString() : dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][15].ToString()) ? dt222.Rows[0][17].ToString() : dataGridView1.CurrentRow.Cells[2].Value.ToString().Equals(dt222.Rows[0][12].ToString()) ? dt222.Rows[0][14].ToString() : dt222.Rows[0][3].ToString();

                            // dcombo.Tag = "pack_id";

                        }
                */
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
                            if (e.ColumnIndex == dataGridView1.Columns.Count - 1 )
                            {
                                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells[0];
                            }
                            else
                            {
                                SendKeys.Send("{UP}");
                                if (BL.CLS_Session.lang.Equals("E"))
                                {
                                    if( dataGridView1.CurrentCell != dataGridView1.CurrentRow.Cells[5] && dataGridView1.CurrentCell != dataGridView1.CurrentRow.Cells[6] && dataGridView1.CurrentCell != dataGridView1.CurrentRow.Cells[7])
                                    SendKeys.Send("{right 2}");
                                    else
                                        SendKeys.Send("{right 1}");
                                    // SendKeys.Send("{right}");
                                }
                                else
                                {
                                    if (dataGridView1.CurrentCell != dataGridView1.CurrentRow.Cells[5] && dataGridView1.CurrentCell != dataGridView1.CurrentRow.Cells[6] && dataGridView1.CurrentCell != dataGridView1.CurrentRow.Cells[7])
                                    SendKeys.Send("{left 2}");
                                    else
                                        SendKeys.Send("{left 1}");
                                    // SendKeys.Send("{left}");
                                }
                            }
                       
                        }
                        catch { }
                    
                //    count = 0;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                if (!dataGridView1.CurrentRow.IsNewRow && isnew)
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
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
            total();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
               // case (Keys.Alt | Keys.Left):
                case (Keys.PageUp):
                    bindingNavigator1.Items[1].PerformClick();
                    break;
               // case (Keys.Alt | Keys.Right):
                case (Keys.PageDown):
                    bindingNavigator1.Items[6].PerformClick();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void Acc_Tran_Load(object sender, EventArgs e)
        {
            if (BL.CLS_Session.up_suspend == false)
            {
               chk_suspend.Enabled = false;
            }

            //this.KeyPreview = true;
            //this.KeyDown += new KeyEventHandler(Sal_Tran_D_KeyDown);

            isnew = false;
            isupdate = false;
            DataTable dtcnt = daml.SELECT_QUIRY_only_retrn_dt("select count(*) from salofr_hdr where branch='" + BL.CLS_Session.brno + "'");

            /* binding navigator
            if(BL.CLS_Session.dtsalbind==null)
                BL.CLS_Session.dtsalbind = daml.SELECT_QUIRY_only_retrn_dt("select top 100 [slcenter],[invtype],[ref] from sales_hdr where branch='" + BL.CLS_Session.brno + "' order by released desc ");

            bindsrc.DataSource = BL.CLS_Session.dtsalbind;
            bindingNavigator1.BindingSource = bindsrc;
           

            txt_s.DataBindings.Add(new Binding("Text",bindsrc,"slcenter"));
            txt_t.DataBindings.Add(new Binding("Text", bindsrc, "invtype"));
            txt_r.DataBindings.Add(new Binding("Text", bindsrc, "ref"));
            */
            
            cnt = Convert.ToInt32(dtcnt.Rows[0][0]);
            cntto = cnt;
            if (BL.CLS_Session.isshamltax.Equals("2"))
            {
                chk_shaml_tax.Checked = true;
                chk_shaml_tax.Enabled = false;
            }
            else
            {
                chk_shaml_tax.Checked = false;
                chk_shaml_tax.Enabled = false;
            }

            if (!BL.CLS_Session.formkey.Contains("B121"))
            {
              //  this.Close();
            }
            else
            {
                // this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
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
                    /*
                    comboBox1.DisplayMember = "Text";
                    comboBox1.ValueMember = "Value";

                    var items = new[] { new { Text = BL.CLS_Session.lang.Equals("E") ? "Cash Sale" : "مبيعات نقدية", Value = "04" }, new { Text = BL.CLS_Session.lang.Equals("E") ? "Credit Sale" : "مبيعات اجله", Value = "05" }, new { Text = BL.CLS_Session.lang.Equals("E") ? "ReCash Sale" : "مرتجع نقدي", Value = "14" }, new { Text = BL.CLS_Session.lang.Equals("E") ? "ReCredit Sale" : "مرتجع اجل", Value = "15" } };
                    comboBox1.DataSource = items;
                    comboBox1.SelectedIndex = -1;


                    dtsal = daml.SELECT_QUIRY_only_retrn_dt("Select * from slcenters where sl_brno="+BL.CLS_Session.brno);
                    cmb_salctr.DataSource = dtsal;  
                    cmb_salctr.DisplayMember = "sl_name";
                    cmb_salctr.ValueMember = "sl_no";
                    cmb_salctr.SelectedIndex = -1;
                    */
                    string tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
                    // MessageBox.Show(tr);
                    dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('24')");
                    cmb_type.DataSource = dttrtyps;
                    cmb_type.DisplayMember = "tr_name";
                    cmb_type.ValueMember = "tr_no";
                    cmb_type.SelectedIndex = -1;

                    string sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";
                    // MessageBox.Show(sl);
                    dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select * from slcenters where sl_brno + sl_no in(" + sl + ") and sl_brno='" + BL.CLS_Session.brno + "'");
                    cmb_salctr.DataSource = dtslctr;
                    cmb_salctr.DisplayMember = "sl_name";
                    cmb_salctr.ValueMember = "sl_no";


                    dtcolman = daml.SELECT_QUIRY_only_retrn_dt("select * from sal_men where sp_brno='" + BL.CLS_Session.brno + "'");

                    cmb_salman.DataSource = dtcolman;
                    cmb_salman.DisplayMember = "sp_name";
                    cmb_salman.ValueMember = "sp_id";
                    cmb_salman.SelectedIndex = -1;

                    dtexits = daml.SELECT_QUIRY_only_retrn_dt("select code, cname, address, tel FROM sltrans");

                    cmb_exits.DataSource = dtexits;
                    cmb_exits.DisplayMember = "cname";
                    cmb_exits.ValueMember = "code";
                    cmb_exits.SelectedIndex = -1;

                    cmb_salman.Enabled = false;
                   // cmb_salman.SelectedIndex = -1;
                    cmb_type.Enabled = false;
                    cmb_salctr.Enabled = false;
                    txt_ref.Enabled = false;
                    txt_hdate.Enabled = false;
                    txt_mdate.Enabled = false;
                    txt_desc.Enabled = false;
                    txt_validdate.Enabled = false;
                    cmb_exits.Enabled = false;
                    txt_remark.Enabled = false;
                    txt_key.Enabled = false;
                    txt_custno.Enabled = false;
                    txt_des.Enabled = false;
                    txt_desper.Enabled = false;
                    dataGridView1.ReadOnly = true;

                   // MessageBox.Show("ok1");
                    if(string.IsNullOrEmpty(a_ref))
                  //  Fill_Data(a_slctr, a_type, a_ref);
                       Fill_Data(txt_s.Text, txt_t.Text, txt_r.Text);
                    else
                        Fill_Data(a_slctr, a_type, a_ref);
                   // MessageBox.Show("ok3");
                    //  dataGridView1.AllowUserToAddRows = false;
                    //dataGridView1.Select();
                    // dataGridView1.BeginEdit(true);
                    // dataGridView1.Rows.Add(10);

                    // this.Owner.Enabled = false;

                    //  Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                    string str2 = BL.CLS_Session.formkey;
                    string whatever = str2.Substring(str2.IndexOf("B121") + 5, 3);
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
                if ((e.ColumnIndex == 0 && dataGridView1.CurrentCell.Value != null) || (e.ColumnIndex == 1 && dataGridView1.CurrentCell.Value != null))
                {

                    foreach (DataGridViewRow row in this.dataGridView1.Rows)
                    {

                        if (row.Index == this.dataGridView1.CurrentCell.RowIndex)

                        { continue; }

                        if (this.dataGridView1.CurrentCell.Value == null)

                        { continue; }

                        if ((row.Cells[0].Value != null && row.Cells[0].Value.ToString() == dataGridView1.CurrentCell.Value.ToString()) || (row.Cells[1].Value != null && row.Cells[1].Value.ToString() == dataGridView1.CurrentCell.Value.ToString()))
                        {
                           // MessageBox.Show("غير مسموح تكرار الصنف","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                            MessageBox.Show("غير مسموح تكرار الصنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                           // dataGridView1.CurrentCell.Value = null;

                        }

                    }

                }
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            /*
            if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[3].Value.ToString()))
            {
                dataGridView1.CurrentRow.Cells[3].Value = "0";
            }

            if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[5].Value.ToString()))
            {
                dataGridView1.CurrentRow.Cells[5].Value = "0";
            }
            */
            /*
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
            //txt_desper_Leave(null, null);
            //txt_des_Leave(null, null);
            //total();

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

            if (txt_amt.Text.Trim() == "")
            {
                txt_amt.Text = "0";
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
                txt_mdate.Focus();

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
                txt_hdate.Focus();

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
            try
            {
                if (cmb_salctr.SelectedValue.ToString().Equals("05") || cmb_salctr.SelectedValue.ToString().Equals("15"))
                {
                    txt_key.Text = "";
                    txt_name.Text = "";
                   // txt_key.Enabled = false;
                  //  txt_name.Enabled = false;
                }
            }
            catch { }
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
            try
            {
                txt_validdate.Text = BL.CLS_Session.end_dt;// DateTime.Now.AddDays(7).ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

                tax_per = BL.CLS_Session.tax_per;
                txt_mdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
                txt_hdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));

                btn_Rfrsh.Enabled = false;
                chk_usebarcode.Enabled = true;

                txt_taxid.Enabled = true;
                txt_taxid.Text = "";
                txt_custno.Text = "";
                txt_total.Text = "0";
                txt_des.Text = "0";
                txt_desper.Text = "0";
                txt_net.Text = "0";

              //  txt_key.Text = BL.CLS_Session.brcash;
              //  load_key();
                // dthdr.Rows.Clear();
                // dtdtl.Rows.Clear();
                isnew = true;
                isupdate = false;
                btn_Save.Enabled = true;
                btn_Add.Enabled = false;
                btn_Undo.Enabled = true;
                btn_Exit.Enabled = false;
                btn_Find.Enabled = false;
                btn_Del.Enabled = false;
                btn_prtdirct.Enabled = false;
                btn_Print.Enabled = false;
                btn_Edit.Enabled = false;
                btn_Post.Enabled = false;
                chk_nodup.Enabled = true;
                cmb_type.Enabled = true;

                if (BL.CLS_Session.up_suspend == false)
                    chk_suspend.Enabled = false;
                else
                    chk_suspend.Enabled = true;

                cmb_type.SelectedIndex = 0;

                cmb_salctr.Enabled = true;
                cmb_salctr.SelectedIndex = 0;

                cmb_salman.Enabled = true;
                cmb_salman.SelectedIndex = -1;


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
                txt_validdate.Enabled = true;
                cmb_exits.Enabled = true;
                txt_remark.Enabled = true;
                txt_key.Enabled = true;
                txt_custno.Enabled = true;
                txt_des.Enabled = true;
                txt_desper.Enabled = true;
                txt_user.Text = BL.CLS_Session.UserName;
                // dataGridView1.Enabled = true;
                // dataGridView1.Rows.Add(10);
                dataGridView1.ReadOnly = false;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[4].ReadOnly = true;
                dataGridView1.Columns[8].ReadOnly = true;
                if (BL.CLS_Session.up_changprice)
                {
                    dataGridView1.Columns[6].ReadOnly = false;
                }
                else
                {
                    dataGridView1.Columns[6].ReadOnly = true;
                }
               // chk_shaml_tax.Enabled = true;
               // chk_shaml_tax.Checked = false;
                shameltax =chk_shaml_tax.Checked?true: false;


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
                txt_desc.Text = "";
                txt_key.Text = "";
                
                //txt_total.Text = "0";
                //txt_des.Text = "0";
                //txt_net.Text = "0";
                cmb_salctr_Leave(null, null);
                load_key();
                //dataGridView1.Rows.Clear();
                //dataGridView1.Refresh();
                // dataGridView1.AllowUserToAddRows = true;
            }

            catch { }
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
                    Fill_Data(cmb_salctr.SelectedValue.ToString(), cmb_type.SelectedValue.ToString(), txt_ref.Text);
                    btn_Save.Enabled = false;
                    btn_Add.Enabled = true;
                    btn_Undo.Enabled = false;
                    btn_Exit.Enabled = true;
                    btn_Find.Enabled = true;
                    btn_Edit.Enabled = true;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;
                    cmb_type.Enabled = false;
                    cmb_salman.Enabled = false;
                    txt_ref.Enabled = false;
                    txt_remark.Enabled = false;
                    txt_key.Enabled=false;
                    txt_custno.Enabled = false;
                    chk_suspend.Enabled = false;

                    
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
                    chk_suspend.Enabled = false;

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
                    chk_suspend.Enabled = false;
                    txt_ref.Enabled = false;
                  //  dataGridView1.AllowUserToAddRows = false;

                    cmb_type.SelectedIndex = -1;
                    txt_amt.Text = "0";
                    txt_desc.Text = "";

                    cmb_salman.Enabled = false;
                    cmb_salman.SelectedIndex = -1;
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
                txt_validdate.Enabled = false;
                cmb_exits.Enabled = false;
                txt_des.Enabled = false;
                txt_desper.Enabled = false;
                txt_amt.Enabled = false;
                cmb_type.Enabled = false;
                cmb_salctr.Enabled = false;
                txt_ref.Enabled = false;
                btn_Rfrsh.Enabled = true;
                btn_Print.Enabled = true;

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
            foreach (DataGridViewRow ro in dataGridView1.Rows)
            {
                if ((!ro.IsNewRow && ro.Cells[0].Value != null) && (!ro.IsNewRow && ro.Cells[1].Value != null) && (!ro.IsNewRow && ro.Cells[3].Value != null))
                {
                    if (shameltax)
                    {
                        DataRow[] dtrvat = dtvat.Select("tax_id ='" + ro.Cells[10].Value + "'");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";

                        // dataGridView1.CurrentRow.Cells[8].Value = Math.Round((Convert.ToDouble(dtrvat[0][2])) / (Convert.ToDouble(100) + Convert.ToDouble(dtrvat[0][2])) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value))) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value))) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)) / 100), 2);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                        // dataGridView1.CurrentRow.Cells[8].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : Math.Round((Convert.ToDouble(dtrvat[0][2])) / (Convert.ToDouble(100) + Convert.ToDouble(dtrvat[0][2])) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * 1) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * 1) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)) / 100), 2);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                        ro.Cells[8].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : Math.Round((Convert.ToDouble(dtrvat[0][2])) / (Convert.ToDouble(100) + Convert.ToDouble(dtrvat[0][2])) * ((Convert.ToDouble(ro.Cells[6].Value) * ((Convert.ToDouble(ro.Cells[3].Value) * Convert.ToDouble(ro.Cells[4].Value)) + Convert.ToDouble(ro.Cells[5].Value))) - ((Convert.ToDouble(ro.Cells[6].Value) * ((Convert.ToDouble(ro.Cells[3].Value) * Convert.ToDouble(ro.Cells[4].Value)) + Convert.ToDouble(ro.Cells[5].Value))) * Convert.ToDouble(ro.Cells[7].Value)) / 100), 4);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                        //  var filteredData = dtvat.Select("tax_id='" + dt222.Rows[0][11] + "'").CopyToDataTable();
                        //  dataGridView1.CurrentRow.Cells[7].Value = ((Convert.ToDouble(filteredData.Rows[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)).ToString();
                        ro.Cells[9].Value = (Convert.ToDouble(ro.Cells[6].Value) * ((Convert.ToDouble(ro.Cells[3].Value) * Convert.ToDouble(ro.Cells[4].Value)) + Convert.ToDouble(ro.Cells[5].Value)) - (Convert.ToDouble(ro.Cells[6].Value) * ((Convert.ToDouble(ro.Cells[3].Value) * Convert.ToDouble(ro.Cells[4].Value))) + Convert.ToDouble(ro.Cells[5].Value)) * Convert.ToDouble(ro.Cells[7].Value) / 100);// + Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value);

                        // dataGridView1.CurrentRow.Cells[7].Value = (Convert.ToDouble(dtrvat[0][2]) / (Convert.ToDouble(100) + Convert.ToDouble(dtrvat[0][2]))) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[8].Value);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                    }
                    else
                    {
                        DataRow[] dtrvat = dtvat.Select("tax_id ='" + ro.Cells[10].Value + "'");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";

                        //  dataGridView1.CurrentRow.Cells[7].Value = ((Convert.ToDouble(dtrvat[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)).ToString();
                        //   dataGridView1.CurrentRow.Cells[8].Value = Math.Round(((Convert.ToDouble(dtrvat[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)  / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * (Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)) / 100), 2);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                        //  dataGridView1.CurrentRow.Cells[8].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : Math.Round(((Convert.ToDouble(dtrvat[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * 1) - (((Convert.ToDouble(dtrvat[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * 1 * Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)) / 100), 2);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                        ro.Cells[8].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : Math.Round(((Convert.ToDouble(dtrvat[0][2]) / 100) * Convert.ToDouble(ro.Cells[6].Value) * ((Convert.ToDouble(ro.Cells[3].Value) * Convert.ToDouble(ro.Cells[4].Value)) + Convert.ToDouble(ro.Cells[5].Value))) - (((Convert.ToDouble(dtrvat[0][2]) / 100) * Convert.ToDouble(ro.Cells[6].Value) * ((Convert.ToDouble(ro.Cells[3].Value) * Convert.ToDouble(ro.Cells[4].Value)) + Convert.ToDouble(ro.Cells[5].Value)) * Convert.ToDouble(ro.Cells[7].Value)) / 100), 4);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                        //  dataGridView1.CurrentRow.Cells[7].Value = 0.00 ;// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                        //  var filteredData = dtvat.Select("tax_id='" + dt222.Rows[0][11] + "'").CopyToDataTable();
                        //  dataGridView1.CurrentRow.Cells[7].Value = ((Convert.ToDouble(filteredData.Rows[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)).ToString();
                        //  dataGridView1.CurrentRow.Cells[8].Value = (Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value) - (((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)/100);
                        ro.Cells[9].Value = (Convert.ToDouble(ro.Cells[6].Value) * ((Convert.ToDouble(ro.Cells[3].Value) * Convert.ToDouble(ro.Cells[4].Value)) + Convert.ToDouble(ro.Cells[5].Value))) - ((Convert.ToDouble(ro.Cells[6].Value) * (Convert.ToDouble(ro.Cells[3].Value) * Convert.ToDouble(ro.Cells[4].Value)) + Convert.ToDouble(ro.Cells[5].Value)) * Convert.ToDouble(ro.Cells[7].Value) / 100);// + Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value);

                        //this ok  dataGridView1.CurrentRow.Cells[8].Value = (Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - (((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value))) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) / 100);

                        // dataGridView1.CurrentRow.Cells[7].Value = (Convert.ToDouble(dtrvat[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[8].Value);// * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                    }
                }
                else if (!ro.IsNewRow)
                    dataGridView1.Rows.Remove(ro);
            }

            total();
            total();

            if (cmb_type.SelectedIndex == -1 || string.IsNullOrEmpty(cmb_type.Text.Trim()))
            {
                MessageBox.Show("يرجى اختيار نوع الفاتورة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_type.Focus();
                return;
            }

            cmb_salctr_Leave(null, null);
           // MessageBox.Show(chek_bal_after().ToString());
          //  MessageBox.Show(txt_crlmt.Text.ToString());

            try
            {
                if (cmb_type.SelectedValue.ToString().Equals("05") || cmb_type.SelectedValue.ToString().Equals("15"))
                {
                    //if (txt_key.Text == "" || txt_name.Text == "")
                    //{
                    //    MessageBox.Show("يرجى ادخال حساب");
                    //    txt_key.Focus();
                    //    return;
                    //}
                    //else
                    //{
                    if (txt_custno.Text == "" && txt_key.Text == "")
                    {
                        MessageBox.Show("يرجى ادخال عميل او حساب","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        txt_custno.Focus();
                        return;
                    }
                    //}
                }
                //else
                //{

                //}
                if (cmb_type.SelectedValue.ToString().Equals("05") && (chek_bal_after() + Convert.ToDouble(txt_net.Text)) > Convert.ToDouble(txt_crlmt.Text) && Convert.ToDouble(txt_crlmt.Text) > 0)
                {
                    MessageBox.Show("لا يسمح بتجاوز حد المديونية");
                    return;
                }

               

                string mdate, hdate,vmdate;
                mdate = txt_mdate.Text.Replace("-", "").Substring(4, 4) + txt_mdate.Text.Replace("-", "").Substring(2, 2) + txt_mdate.Text.Replace("-", "").Substring(0, 2);
                hdate = txt_hdate.Text.Replace("-", "").Substring(4, 4) + txt_hdate.Text.Replace("-", "").Substring(2, 2) + txt_hdate.Text.Replace("-", "").Substring(0, 2);
                vmdate = txt_validdate.Text.Replace("-", "").Substring(4, 4) + txt_validdate.Text.Replace("-", "").Substring(2, 2) + txt_validdate.Text.Replace("-", "").Substring(0, 2);

                DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref)," + BL.CLS_Session.dtbranch.Rows[0]["jv_" + cmb_type.SelectedValue + ""] + ") from salofr_hdr where invtype='" + cmb_type.SelectedValue + "' and slcenter='" + cmb_salctr.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "'");
               // DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(a_ref)," + BL.CLS_Session.dtbranch.Rows[0]["jv_21"] + ") from acc_hdr where a_type='21' and a_brno='" + BL.CLS_Session.brno + "'");
                

                ref_max = Convert.ToInt32(dt.Rows[0][0]) + 1;
               // jrdacc = Convert.ToInt32(dt2.Rows[0][0]) + 1;

                if (isnew && string.IsNullOrEmpty(txt_ref.Text))
                {
                   
                    daml.SqlCon_Open();
                    int exexcuteds = daml.Insert_Update_Delete_retrn_int("INSERT INTO salofr_hdr(branch,slcenter,invtype,ref,invmdate, invhdate,text,remarks,casher,entries,invttl,invdsvl,nettotal,invdspc,tax_amt_rcvd,with_tax,usrid,custno,invcst,suspend,glser,slcode,stkjvno,taxid,tax_percent,taxfree_amt,carrier,chkdate) VALUES('" + BL.CLS_Session.brno + "','" + cmb_salctr.SelectedValue + "','" + cmb_type.SelectedValue + "'," + ref_max + ",'" + mdate + "','" + hdate + "','" + txt_desc.Text + "','" + txt_remark.Text + "','" + txt_key.Text + "'," + (dataGridView1.RowCount - 1) + "," + txt_total.Text + "," + txt_des.Text + "," + txt_net.Text + "," + txt_desper.Text + "," + txt_tax.Text + "," + (chk_shaml_tax.Checked ? 1 : 0) + ",'" + txt_user.Text + "','" + txt_custno.Text + "'," + txt_cost.Text + "," + (chk_suspend.Checked ? 1 : 0) + ",'Sal1','" + (cmb_salman.SelectedIndex != -1 ? cmb_salman.SelectedValue.ToString() : "") + "'," + (BL.CLS_Session.is_dorymost.Equals("2") ? jrdacc : 0) + ",'" + txt_taxid.Text + "'," + tax_per + "," + txt_taxfree.Text + ",'" + cmb_exits.SelectedValue + "','"+vmdate+"')", false);
                    daml.SqlCon_Close();

                   ////// DataRow dr1 = BL.CLS_Session.dtsalbind.NewRow();

                   ////// dr1[0] = cmb_salctr.SelectedValue;
                   ////// dr1[1] = cmb_type.SelectedValue;
                   ////// dr1[2] = ref_max;
                   //////// dtbind.Rows.Add(dr1);
                   //////// dtbind.Rows.InsertAt(dr1, 0);
                   ////// BL.CLS_Session.dtsalbind.Rows.InsertAt(dr1, 0);
                   

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
                        txt_validdate.Enabled = false;
                        cmb_exits.Enabled = false;
                        txt_remark.Enabled = false;
                        txt_key.Enabled = false;
                        txt_custno.Enabled = false;
                        txt_des.Enabled = false;
                        txt_desper.Enabled = false;
                        txt_taxid.Enabled = false;
                      //  dataGridView1.ReadOnly = true;
                        cmb_type.Enabled = false;
                        cmb_salman.Enabled = false;
                        cmb_salctr.Enabled = false;
                        btn_Post.Enabled = true;
                    }
                    else
                    {
                        
                    }
                     
                }
                else
                {
                    daml.SqlCon_Open();
                    //int exexcuteds = daml.Insert_Update_Delete_retrn_int("update sales_hdr set invmdate='" + mdate + "', invhdate='" + hdate + "',text='" + txt_desc.Text + "',remarks='" + txt_remark.Text + "',casher='" + txt_key.Text + "',custno='" + txt_custno.Text + "',entries=" + (dataGridView1.RowCount - 1) + ",lastupdt='" + DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false)) + "',invttl=" + txt_total.Text + ",invdsvl=" + txt_des.Text + ",nettotal=" + txt_net.Text + ",invdspc=" + txt_desper.Text + ",tax_amt_rcvd=" + txt_tax.Text + ",invcst=" + txt_cost.Text + ",usrid='" + txt_user.Text + "',suspend=" + (chk_suspend.Checked ? 1 : 0) + ",slcode='" + (cmb_salman.SelectedIndex != -1 ? cmb_salman.SelectedValue.ToString() : "") + "',lastupdt=getdate() where branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "' and invtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + "", false);
                    int exexcuteds = daml.Insert_Update_Delete_retrn_int("update salofr_hdr set invmdate='" + mdate + "', invhdate='" + hdate + "',text='" + txt_desc.Text + "',remarks='" + txt_remark.Text + "',casher='" + txt_key.Text + "',custno='" + txt_custno.Text + "',entries=" + (dataGridView1.RowCount - 1) + ",invttl=" + txt_total.Text + ",invdsvl=" + txt_des.Text + ",nettotal=" + txt_net.Text + ",invdspc=" + txt_desper.Text + ",tax_amt_rcvd=" + txt_tax.Text + ",invcst=" + txt_cost.Text + ",usrid='" + txt_user.Text + "',suspend=" + (chk_suspend.Checked ? 1 : 0) + ",slcode='" + (cmb_salman.SelectedIndex != -1 ? cmb_salman.SelectedValue.ToString() : "") + "',lastupdt=getdate(),taxid='" + txt_taxid.Text + "',taxfree_amt=" + txt_taxfree.Text + ",carrier='" + cmb_exits.SelectedValue + "',chkdate='"+vmdate+"' where branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "' and invtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + "", false);
                   
                    int exexcuteds2 = daml.Insert_Update_Delete_retrn_int("delete from  salofr_dtl where branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "' and invtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + "", false);
                    
                    daml.SqlCon_Close();
                    
                    
                  //  daml.SqlCon_Open();
                   // int exexcuted = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_mdate='" + mdate + "', a_hdate='" + hdate + "',a_text='" + txt_desc.Text + "',a_amt=" + txt_amt.Text + ",a_entries=" + (dataGridView1.RowCount - 1) + " where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "'", false);
                   // exexcuted = exexcuted + daml.Insert_Update_Delete_retrn_int("delete from acc_dtl where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "'", false);
                   // daml.SqlCon_Close();
                    if (exexcuteds > 0)
                    {
                      //  txt_ref.Text = ref_max.ToString();
                        txt_ref.Enabled = false;
                        txt_hdate.Enabled = false;
                        txt_mdate.Enabled = false;
                        txt_desc.Enabled = false;
                        txt_validdate.Enabled = false;
                        cmb_exits.Enabled = false;
                        txt_remark.Enabled = false;
                        txt_key.Enabled = false;
                        txt_custno.Enabled = false;
                        txt_des.Enabled = false;
                        txt_desper.Enabled = false;
                        btn_Post.Enabled = true;
                        cmb_salman.Enabled = false;
                        txt_taxid.Enabled = false;
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
                   // if (!row.IsNewRow && row.Cells[0].Value != null)
                    if ((!row.IsNewRow && row.Cells[0].Value != null) && (!row.IsNewRow && row.Cells[1].Value != null) && (!row.IsNewRow && row.Cells[3].Value != null))
                    {
                        // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO salofr_dtl(branch,slcenter,invtype,ref,invmdate, invhdate, itemno, qty, price, pack, pkqty,folio,tax_amt,tax_id,discpc,whno,cost,barcode,stk_or_ser,shdqty,frtqty,item_img,shdpk) VALUES(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13,@a14,@a15,@a16,@a17,@a18,@a19,@a20,@a21,@a22,@a23)", con))
                        {
                            cmd.Parameters.AddWithValue("@a1", BL.CLS_Session.brno);
                            cmd.Parameters.AddWithValue("@a2", cmb_salctr.SelectedValue);
                            cmd.Parameters.AddWithValue("@a3", cmb_type.SelectedValue);
                            cmd.Parameters.AddWithValue("@a4", isnew ? ref_max : Convert.ToInt32(txt_ref.Text));
                           // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                            cmd.Parameters.AddWithValue("@a5", mdate);
                           // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                            cmd.Parameters.AddWithValue("@a6", hdate);
                            cmd.Parameters.AddWithValue("@a7", row.Cells[0].Value);
                            cmd.Parameters.AddWithValue("@a8", (Convert.ToDouble(row.Cells[3].Value) * Convert.ToDouble(row.Cells[4].Value)) + Convert.ToDouble(row.Cells[5].Value));
                            cmd.Parameters.AddWithValue("@a9", string.IsNullOrEmpty(row.Cells[6].ToString())? 0 : row.Cells[6].Value);
                            cmd.Parameters.AddWithValue("@a10", 1);
                            cmd.Parameters.AddWithValue("@a11", 1);// row.Cells[4].Value);
                            cmd.Parameters.AddWithValue("@a12", row.HeaderCell.Value);
                            cmd.Parameters.AddWithValue("@a13", row.Cells[8].Value);
                            cmd.Parameters.AddWithValue("@a14", row.Cells[10].Value);
                            cmd.Parameters.AddWithValue("@a15", Convert.ToDouble(row.Cells[7].Value));
                            cmd.Parameters.AddWithValue("@a16", stwhno);
                            cmd.Parameters.AddWithValue("@a17", row.Cells[11].Value);
                            cmd.Parameters.AddWithValue("@a18", row.Cells[1].Value);
                            cmd.Parameters.AddWithValue("@a19", row.Cells[13].Value);
                            cmd.Parameters.AddWithValue("@a20", row.Cells[3].Value);
                            cmd.Parameters.AddWithValue("@a21", row.Cells[5].Value);
                            cmd.Parameters.AddWithValue("@a22", row.Cells[1].Value);
                            cmd.Parameters.AddWithValue("@a23", row.Cells[4].Value);
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
                chk_usebarcode.Enabled = false;
                btn_Save.Enabled = false;
                btn_Add.Enabled = true;
                btn_Exit.Enabled = true;
                btn_Find.Enabled = true;
                btn_Undo.Enabled = false;
                btn_Del.Enabled = true;
                btn_Edit.Enabled = true;
                btn_prtdirct.Enabled = true;
                btn_Print.Enabled = true;
                chk_suspend.Enabled = false;
                btn_Rfrsh.Enabled = true;
                if (notposted)
                { btn_Post.Enabled = true; }
               
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ReadOnly = true;

              
               //to print
                /*********************************************************
                ThreadStart th = NewThread;
                Thread th1 = new Thread(th);
                th1.Start();
                **********************************************************/
               // save_acc();

                isnew = false;
                isupdate = false;
               // btn_Rfrsh_Click(sender, e);
              //  bindingNavigator1.Items[0].PerformClick();
                //////daml.SqlCon_Open();
                //////daml.Exec_Query_only("bld_whbins_cost_by_tran @cond='sal', @br_no ='" + BL.CLS_Session.brno + "', @inv_type='" + cmb_type.SelectedValue + "',@wh_no='" + stwhno + "', @ref=" + txt_ref.Text + ",@depart ='" + cmb_salctr.SelectedValue + "'");
                //////daml.SqlCon_Close();

                //new Thread(() =>
                //{
                //    Thread.CurrentThread.IsBackground = true;

               /*
                    daml.SqlCon_Open();
                    daml.Exec_Query_only("bld_whbins_cost_by_tran @cond='sal', @br_no ='" + BL.CLS_Session.brno + "', @inv_type='" + cmb_type.SelectedValue + "',@wh_no='" + stwhno + "', @ref=" + txt_ref.Text + ",@depart ='" + cmb_salctr.SelectedValue + "'");
                    daml.SqlCon_Close();
                * */
            //    backgroundWorker1.RunWorkerAsync();
                                //}).Start();
               //Task  task = Task.Run((Action)MyFunction);
                /*
                Thread t = new Thread(NewThread);
                t.Start();
                 */
                //////Thread t1 = new Thread(NewThread);
                //////t1.Start();
               //////////ok daml.Exec_Query_only("bld_whbins_cost_by_tran @cond='sal', @br_no ='" + BL.CLS_Session.brno + "', @inv_type='" + cmb_type.SelectedValue + "',@wh_no='" + stwhno + "', @ref=" + txt_ref.Text + ",@depart ='" + cmb_salctr.SelectedValue + "'");
               //////// DataTable dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sales_dtl where invtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + " and branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "'");
               ////////// daml.Exec_Query_only("bld_whbins_cost_all_by_tran @items_tran_tb=" + dttbld + "");
               //////// using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
               //////// {

               ////////     cmd.CommandType = CommandType.StoredProcedure;
               ////////     cmd.Connection = con;
               ////////     cmd.Parameters.AddWithValue("@items_tran_tb", dttbld);
               ////////     con.Open();
               ////////     cmd.ExecuteNonQuery();
               ////////     con.Close();

               ////////  //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

               //////// }
               //////// //isnew = false;
               //////// //isupdate = false;

                //////inv_t = cmb_type.SelectedValue.ToString();
                //////inv_r = txt_ref.Text;
                //////inv_s = cmb_salctr.SelectedValue.ToString();
                //////////// }
                //////////// BL.Thrad_Class tc = new BL.Thrad_Class();
                //////////  Thread a = new Thread(() => thread1(inv_t, inv_r, inv_s));// new Thread(new ThreadStart(tc.thread1(cmb_type.SelectedValue.ToString(), txt_ref.Text.ToString(), cmb_salctr.SelectedValue.ToString())));
                //////Thread a = new Thread(() => thread1(inv_t, inv_r, inv_s));
                ////////////// Thread b = new Thread(ExThread.thread2);
                //////////// a.Start(); 
                //////// Thread a = new Thread(() => thread1("1", "2" ,"3"));
                //////a.Start();
                btn_Rfrsh_Click(sender, e);//to print
                //isnew = false;
                //isupdate = false;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        public void thread1(string s1, string s2, string s3)
        {
            //double s = 0;
            //for (double z = 0; z < 500000000; z++)
            //{
            //    s = z + 1;
            //}
            //MessageBox.Show(s.ToString());
            DataTable dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sales_dtl where invtype='" + s1 + "' and ref=" + s2 + " and branch='" + BL.CLS_Session.brno + "' and slcenter='" + s3 + "'");
            // daml.Exec_Query_only("bld_whbins_cost_all_by_tran @items_tran_tb=" + dttbld + "");
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
      
        //public  async Task Method1()  
        //{  
        //    await Task.Run(() =>  
        //    {  
        //        for (int i = 0; i < 100; i++)  
        //        {  
        //            Console.WriteLine(" Method 1");  
        //        }  
        //    });             
        //}  
        private static void MyFunction()
        {
            // Loop in here

        }

        private void NewThread()
        {
           
              //  Thread.CurrentThread.IsBackground = true;

             //   daml.SqlCon_Open();
                daml.Exec_Query_only("bld_whbins_cost_by_tran @cond='sal', @br_no ='" + BL.CLS_Session.brno + "', @inv_type='" + cmb_type.SelectedValue + "',@wh_no='" + stwhno + "', @ref=" + txt_ref.Text + ",@depart ='" + cmb_salctr.SelectedValue + "'");
              //  daml.SqlCon_Close();
            
        }
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            //try
            //{
              
               // btn_Rfrsh_Click(sender, e);
              //  if (dthdr.Rows[0]["usrid"].ToString() != BL.CLS_Session.UserName && dthdr.Rows.Count > 0)
                if (txt_user.Text != BL.CLS_Session.UserName && BL.CLS_Session.up_edit_othr==false)
                {
                    MessageBox.Show("لا تملك صلاحية تعديل حركة شخص اخر", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (dthdr !=null && dthdr.Rows.Count > 0)
                    {
                        if (dthdr.Rows[0][8].ToString().Equals("Pos"))
                        {
                            MessageBox.Show("لا يمكن تعديل قيد الي تم تكوينه من الكاشير", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                
                //  btn_Rfrsh_Click(sender, e);
                if (btn_Post.Enabled == true)
                {
                    chk_nodup.Enabled = true;
                    btn_Rfrsh.Enabled = false;
                    chk_usebarcode.Enabled = true;
                    notposted = true;
                    isnew = false;
                    isupdate = true;
                    btn_Save.Enabled = true;
                    btn_Add.Enabled = false;
                    btn_Undo.Enabled = true;
                    btn_Exit.Enabled = false;
                    btn_Find.Enabled = false;
                    btn_Del.Enabled = false;
                    btn_prtdirct.Enabled = false;
                    btn_Print.Enabled = false;
                    btn_Edit.Enabled = false;
                    btn_Post.Enabled = false;
                    cmb_salman.Enabled = true;
                    txt_taxid.Enabled = true;
                   // cmb_salman.SelectedIndex = -1;
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
                    txt_validdate.Enabled = true;
                    cmb_exits.Enabled = true;
                    txt_remark.Enabled = true;
                    txt_key.Enabled = true;
                    txt_custno.Enabled = true;
                    txt_des.Enabled = true;
                    txt_desper.Enabled = true;
                    txt_user.Text = BL.CLS_Session.UserName;
                    // dataGridView1.Enabled = true;
                    dataGridView1.ReadOnly = false;
                    dataGridView1.Columns[2].ReadOnly = true;
                    dataGridView1.Columns[4].ReadOnly = true;
                    dataGridView1.Columns[8].ReadOnly = true;
                    if (BL.CLS_Session.up_changprice)
                    {
                        dataGridView1.Columns[6].ReadOnly = false;
                    }
                    else
                    {
                        dataGridView1.Columns[6].ReadOnly = true;
                    }
                    dataGridView1.Select();
                    dataGridView1.AllowUserToAddRows = true;
                    if (BL.CLS_Session.up_suspend == false)
                        chk_suspend.Enabled = false;
                    else
                        chk_suspend.Enabled = true;
                    // ref_max = Convert.ToInt32(txt_ref.Text);
                }
                else
                {
                    MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Can't edit posted trans. " : "لا يمكن تعديل حركة مرحلة", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
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
                    DataTable dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sales_dtl where invtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + " and branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "'");
                    // daml.Exec_Query_only("bld_whbins_cost_all_by_tran @items_tran_tb=" + dttbld + "");
                   
                    daml.SqlCon_Open();
                    dthdr = daml.SELECT_QUIRY_only_retrn_dt("select * from sales_hdr where invtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + " and branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "'");
                   
                    if (dthdr.Rows[0][8].ToString().Equals("Pos"))
                    {
                        daml.Insert_Update_Delete_retrn_int("update pos_hdr set gen_ref=0 where gen_ref=" + txt_ref.Text + " and type='" + (cmb_type.SelectedValue.Equals("04")? 1: cmb_type.SelectedValue.Equals("05")? 3 :cmb_type.SelectedValue.Equals("14")?0: 2) + "' and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "'", false);

                    }
                    int exexcuted = daml.Insert_Update_Delete_retrn_int("delete from sales_hdr where posted=0 and branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "' and invtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + "", false);
                    daml.Insert_Update_Delete_Only("delete from acc_hdr where a_posted=0 and a_brno='" + BL.CLS_Session.brno + "' and sl_no='" + cmb_salctr.SelectedValue + "' and a_type='" + cmb_type.SelectedValue + "' and a_ref=" + txt_ref.Text + "", false);
                   if(BL.CLS_Session.is_dorymost.Equals("2"))
                       daml.Insert_Update_Delete_Only("delete from acc_hdr where a_brno='" + BL.CLS_Session.brno + "' and sl_no='" + cmb_salctr.SelectedValue + "' and a_type='21' and a_ref=" + txt_jrdacc.Text + "", false);
                  
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
                isfeched = false;
                //var sr = new Search_All("6", "Sal");
                var sr = new Search_All("16", "Ofr");
                sr.checkBox1.Visible = true;
                sr.ShowDialog();

                Fill_Data(sr.dataGridView1.CurrentRow.Cells[0].Value.ToString(), sr.dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), sr.dataGridView1.CurrentRow.Cells[2].Value.ToString());
                total();
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
                string strtblh = atype.Equals("24") ? "salofr_hdr" : "sales_hdr";
                string strtbld = atype.Equals("24") ? "salofr_dtl" : "sales_dtl";
                if (isfeched)
                {

                }
                else
                {
                    dthdr = daml.SELECT_QUIRY_only_retrn_dt("select * from " + strtblh + " where invtype='" + atype + "' and ref=" + aref + " and branch='" + BL.CLS_Session.brno + "' and slcenter='" + slctr + "'");
                    // string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price)+aa.tax_amt)-(((aa.qty*aa.price)+aa.tax_amt)*aa.discpc/100)";
                    // string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price)+aa.tax_amt)-(((aa.qty*aa.price)+aa.tax_amt)*aa.discpc/100)";
                    string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price))-(((aa.qty*aa.price))*aa.discpc/100)";

                    if(dthdr.Rows[0][2].ToString().Equals("24"))
                        dtdtl = daml.SELECT_QUIRY_only_retrn_dt("select aa.itemno itemno,aa.barcode barcode,bb.item_name item_name,aa.shdqty pack,aa.shdpk pkqty,aa.frtqty qty," + (Convert.ToDouble(dthdr.Rows[0]["chkdate"]) > Convert.ToDouble(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) ? "aa.price" : "bb.item_price") + " price,aa.discpc discpc,aa.tax_amt tax_amt," + strvt + " total,aa.tax_id tax_id, aa.cost cost,bb.item_cbalance cur_bal,aa.stk_or_ser stk_or_ser,cast(bb.item_unit as varchar) unm from " + strtbld + " aa,items bb where aa.itemno=bb.item_no and aa.invtype='" + atype + "' and aa.ref=" + aref + " and aa.branch='" + BL.CLS_Session.brno + "' and aa.slcenter='" + slctr + "' order by aa.folio");// and aa.a_type='" + atype + "' and aa.a_ref=" + aref + "");
                    else
                        dtdtl = daml.SELECT_QUIRY_only_retrn_dt("select aa.itemno itemno,aa.barcode barcode,bb.item_name item_name,aa.shdqty pack,aa.shdpk pkqty,aa.frtqty qty,aa.price price,aa.discpc discpc,aa.tax_amt tax_amt," + strvt + " total,aa.tax_id tax_id, aa.cost cost,bb.item_cbalance cur_bal,aa.stk_or_ser stk_or_ser,cast(bb.item_unit as varchar) unm from " + strtbld + " aa,items bb where aa.itemno=bb.item_no and aa.invtype='" + atype + "' and aa.ref=" + aref + " and aa.branch='" + BL.CLS_Session.brno + "' and aa.slcenter='" + slctr + "' order by aa.folio");// and aa.a_type='" + atype + "' and aa.a_ref=" + aref + "");
                }
                cmb_type.SelectedValue=dthdr.Rows[0][2];
                cmb_salman.SelectedValue = dthdr.Rows[0]["slcode"];
                cmb_salctr.SelectedValue = dthdr.Rows[0][1];
                txt_ref.Text =isnew?"": dthdr.Rows[0][3].ToString();
                txt_mdate.Text = dthdr.Rows[0][4].ToString().Substring(6, 2) + dthdr.Rows[0][4].ToString().Substring(4, 2) + dthdr.Rows[0][4].ToString().Substring(0, 4);
                txt_hdate.Text = dthdr.Rows[0][5].ToString().Substring(6, 2) + dthdr.Rows[0][5].ToString().Substring(4, 2) + dthdr.Rows[0][5].ToString().Substring(0, 4);
                txt_desc.Text = dthdr.Rows[0][7].ToString();
                txt_remark.Text = dthdr.Rows[0][60].ToString();

                txt_total.Text = dthdr.Rows[0]["invttl"].ToString();
                txt_desper.Text = dthdr.Rows[0]["invdspc"].ToString();
                txt_des.Text = dthdr.Rows[0]["invdsvl"].ToString();

                tax_per = string.IsNullOrEmpty(dthdr.Rows[0]["tax_percent"].ToString()) ? 5 : Convert.ToDouble(dthdr.Rows[0]["tax_percent"]);

                txt_tax.Text = dthdr.Rows[0]["tax_amt_rcvd"].ToString();
                txt_net.Text = dthdr.Rows[0]["nettotal"].ToString();
                txt_user.Text = dthdr.Rows[0]["usrid"].ToString();
                txt_cost.Text = dthdr.Rows[0]["invcst"].ToString();
               // txt_net.Text = dthdr.Rows[0]["nettotal"].ToString();
                txt_jrdacc.Text = dthdr.Rows[0]["stkjvno"].ToString();
                txt_key.Text = dthdr.Rows[0][19].ToString();
                txt_custno.Text = dthdr.Rows[0]["custno"].ToString();
                txt_taxid.Text = dthdr.Rows[0]["taxid"].ToString();
                txt_taxfree.Text = dthdr.Rows[0]["taxfree_amt"].ToString();
                cmb_exits.SelectedValue = dthdr.Rows[0]["carrier"];
                txt_validdate.Text = dthdr.Rows[0]["chkdate"].ToString().Substring(6, 2) + dthdr.Rows[0]["chkdate"].ToString().Substring(4, 2) + dthdr.Rows[0]["chkdate"].ToString().Substring(0, 4);// dthdr.Rows[0]["chkdate"].ToString();

                load_key();

                if (Convert.ToBoolean(dthdr.Rows[0][22]) == true)
                {
                    btn_Post.Enabled = false;

                }
                else { btn_Post.Enabled = true; }

                if (Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) == true)
                    chk_shaml_tax.Checked = true;

                if (Convert.ToBoolean(dthdr.Rows[0]["suspend"]) == true)
                    chk_suspend.Checked = true;
                else
                    chk_suspend.Checked = false;

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

                //if (Convert.ToBoolean(dthdr.Rows[0]["suspend"]) == true)
                //    chk_suspend.Checked = true;
                //else
                //    chk_suspend.Checked = false;
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
              //  dataGridView1.ReadOnly = false;
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

                   // string dvc = r.Cells[3].Value.ToString();
                    //  dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where rtrim(ltrim(item_no))='" + r.Cells[0].Value + "'");

                  //  DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                    //  r.Cells[2] = dcombo;
                    /*
                    DataView dv1 = dtunits.DefaultView;
                    //  dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                    dv1.RowFilter = "unit_id in('" + r.Cells[14].Value + "')";
                    DataTable dtNew = dv1.ToTable();
                    r.Cells[14].Value = dtNew.Rows[0][1].ToString();
                     */
                   // dcombo.DisplayMember = "unit_name";
                   // dcombo.ValueMember = "unit_id";
                 //   // r.Cells[6].Value = "7";
                 //   //  dtdtl.Rows[r.Index][6] =Convert.ToDecimal(r.Cells[5].Value) * Convert.ToDecimal(r.Cells[4].Value);
                 //   // r.Cells[2] = null;
                 //   // r.Cells[1].Value = "1111111111111111";
                 //   /*
                 //   DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                 //   DataView dv1 = dtunits.DefaultView;
                 //   dv1.RowFilter = "unit_id ='" + r.Cells[2].Value + "'";
                 //   DataTable dtNew = dv1.ToTable();
                 //   dcombo.DataSource = dtNew;
                 //   dcombo.DisplayMember = "unit_name";
                 //   dcombo.ValueMember = "unit_id";
                 //   dcombo.Value = r.Cells[2].Value;
                 //   dataGridView1.CurrentRow.Cells[2] = dcombo;
                 // //  dcombo.data = "account_id";
                 // //  dcombo.Value = r.Cells[2].Value;
                 //  // r.Cells[2] = dcombo;
                 //   // dataGridView1.CurrentRow.Cells[2] = dcombo;
                 // //  DataGridViewTextBoxCell textBoxcell = (DataGridViewTextBoxCell)(r.Cells[2]);
                 // //  textBoxcell.Value = "1";
                   
                 //   // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                 //   dcombo.FlatStyle = FlatStyle.Flat;
                 //   */
                 ////   DataGridViewComboBoxCell dcombo = (DataGridViewComboBoxCell)(r.Cells[2]);
                 //  // cell.DataSource = new string[] { "10", "30" };
                 //   string dvc = r.Cells[3].Value.ToString();
                 // //  dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where rtrim(ltrim(item_no))='" + r.Cells[0].Value + "'");
                    
                 //   DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                 // //  r.Cells[2] = dcombo;
                 //   DataView dv1 = dtunits.DefaultView;
                 // //  dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                 //   dv1.RowFilter = "unit_id in('" + r.Cells[3].Value + "')";
                 //   DataTable dtNew = dv1.ToTable();
                 //   dcombo.DataSource = dtNew;
                 //   dcombo.DisplayMember = "unit_name";
                 //   dcombo.ValueMember = "unit_id";
                 // //  MessageBox.Show(r.Cells[2].Value.ToString());
                 // //  dcombo.Value = r.Cells[2].Value.ToString();// dt222.Rows[0][5];
                 // //  dcombo.Value = dtunits.Rows[0][1];

                 //   //for (int i = 0; i < dtunits.Rows.Count; i++)
                 //   //{
                 //   //    if (dtunits.Rows[i]["unit_id"].ToString() == r.Cells[2].Value.ToString()) // 1 for MALE & 2 for FEMALE
                 //   //    {
                 //   //        dcombo.Value = dtunits.Rows[i]["unit_name"];
                 //   //    }
                 //   //}
                 //  // r.Cells[2] = dcombo;
                 // //  dataGridView1.CurrentRow.Cells[2] = dcombo;
                 //   // dcombo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                 //   //if (e.RowIndex == dataGridView1.NewRowIndex)
                 //   //{
                 //   //    dataGridView1.Rows.Add(1);
                 //   //}

                 //   //  dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                 //   r.Cells[3] = dcombo;
                 //   r.Cells[3].Value = dtNew.Rows[0][0];

                 // //  dataGridView1[2, r.Index] = dcombo;
                 //  // dataGridView1[2, r.Index].Value = dtNew.Rows[0][1];
                 //   // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                 //   dcombo.FlatStyle = FlatStyle.Flat;
                    
                   
                 //      // var dgc = new DataGridViewComboBoxColumn();
                 //       // dgc.Items.AddRange("Male", "Female");

                 //   /*
                 //       DataTable dt = new DataTable();
                 //       DataColumn dc1 = new DataColumn("ID");
                 //       DataColumn dc2 = new DataColumn("Name");
                 //       dt.Columns.Add(dc1);
                 //       dt.Columns.Add(dc2);
                 //       dt.Rows.Add(1, "Male");
                 //       dt.Rows.Add(2, "Female");
                 //   */

                 //       //DataGridViewComboBoxColumn c1 = new DataGridViewComboBoxColumn() { HeaderText = "الوحدة" };
                 //       //c1.DataSource = dtNew;
                 //       //c1.DisplayMember = "unit_name";
                 //       //c1.ValueMember = "unit_id";
                    

                 //       //for (int i = 0; i < dtNew.Rows.Count; i++)
                 //       //{
                 //       //    if (dtNew.Rows[i]["unit_id"].ToString() == "1") // 1 for MALE & 2 for FEMALE
                 //       //    {
                 //       //        c1.DefaultCellStyle.NullValue = dtNew.Rows[i]["unit_name"];
                 //       //    }

                 //       //}
                 //       //dataGridView1.Columns.Add(c1);
                 //       //continue;
                    
                 //   //DataRow sel = dtunits.Select("unit_id = '" + r.Cells[2].Value.ToString() + "'").FirstOrDefault();
                 //   //if (sel != null)
                 //   //{
                 //   //  //  dcombo.Value = sel["unit_name"].ToString();
                 //   //    r.Cells[2].Value = sel["unit_name"].ToString();
                 //   //}
                 // //  r.Cells[2] = dcombo;
                 //  /*

                 // //  string dv = r.Cells[2].Value.ToString();
                 //   DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                 //   //  DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
                 //   // dt.Clear();
                    
                 //   dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where rtrim(ltrim(item_no))='" + r.Cells[0].Value + "'");
                    
                 //  // MessageBox.Show(r.Cells[0].Value.ToString());
                 //  // MessageBox.Show(dt222.Rows[0][0].ToString());

                 //   DataView dv1 = dtunits.DefaultView;
                 //   /*
                 //   dv1.RowFilter = "unit_id in('" + r.Cells[2].Value.ToString() + "')";
                 //   DataTable dtNew = dv1.ToTable();
                 //   dcombo.DataSource = dtunits;
                 //   dcombo.DisplayMember = "unit_name";
                 //   dcombo.ValueMember = "unit_id";
                 //   //  dataGridView1.Columns[2] = dcombo;
                 //   dcombo.Value = r.Cells[2];

                    
                 //   r.Cells[2] = dcombo;
                 //   dcombo.FlatStyle = FlatStyle.Flat;
                 //    * */
                 //   /*
                 //   dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                 //   DataTable dtNew = dv1.ToTable();
                 //   dcombo.DataSource = dtNew;
                 //   dcombo.DisplayMember = "unit_name";
                 //   dcombo.ValueMember = "unit_id";
                 //  // MessageBox.Show(dt222.Rows[0][5].ToString());
                 //  // dcombo.Selected = r.Cells[2].Value;
                 //   r.Cells[2] = dcombo;
                 //   // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                 //   dcombo.FlatStyle = FlatStyle.Flat;
                 //   */
                }

               // dataGridView1.ReadOnly = true;
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
                if (!dthdr.Rows[0]["glser"].ToString().Equals("Pos"))
                total();
            }
            catch {  }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                isfeched = false;
               // dataGridView1.ReadOnly = false;
              ////////  var fsn = new Search_by_No("04");

              ////////  DataTable dttp = dttrtyps.Copy();

              ////////  DataRow dtt = dttp.NewRow();
              ////////  dtt[0] = "24"; dtt[1] = "نسخ من عرض سعر"; dtt[2] = ""; dtt[3] = "";
              //////////  dttp.Rows.Add(new { Text = "نسخ من عرض سعر", Value = "24" });
              ////////  dttp.Rows.Add(dtt);
              ////////  fsn.comboBox1.DataSource = dttp;
              ////////  fsn.comboBox1.DisplayMember = "tr_name";
              ////////  fsn.comboBox1.ValueMember = "tr_no";

               

              ////////  fsn.cmb_salctr.DataSource = dtslctr;
              ////////  fsn.cmb_salctr.DisplayMember = "sl_name";
              ////////  fsn.cmb_salctr.ValueMember = "sl_no";

              ////////  fsn.ShowDialog();
                var fsn = new Search_by_No("04");

                fsn.comboBox1.DataSource = dttrtyps;
                fsn.comboBox1.DisplayMember = "tr_name";
                fsn.comboBox1.ValueMember = "tr_no";

                fsn.cmb_salctr.DataSource = dtslctr;
                fsn.cmb_salctr.DisplayMember = "sl_name";
                fsn.cmb_salctr.ValueMember = "sl_no";

                fsn.ShowDialog();

                Fill_Data(fsn.cmb_salctr.SelectedValue.ToString(), fsn.comboBox1.SelectedValue.ToString(), fsn.textBox1.Text);
              //  dataGridView1_CellLeave(null, null);
                if(!dthdr.Rows[0]["glser"].ToString().Equals("Pos"))
                total();

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
            try
                {
                    daml.SqlCon_Open();
                    int exexcuted = daml.Insert_Update_Delete_retrn_int("update sales_hdr set posted=1 where branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "' and invtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + "", false);
                    int exexcuted0 = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_posted=1 where a_brno='" + BL.CLS_Session.brno + "' and sl_no='" + cmb_salctr.SelectedValue + "' and a_type='" + cmb_type.SelectedValue + "' and a_ref=" + txt_ref.Text + "", false);
                    // exexcuted = exexcuted + daml.Insert_Update_Delete("delete from acc_dtl where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "'", false);
                    daml.SqlCon_Close();

                    if (exexcuted > 0)
                    {
                        DataTable dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sales_dtl where invtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + " and branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "'");
                   
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

                        MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "successfuly posted" : "تم الترحيل بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                        btn_Post.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "fail during post" : "فشل في الترحيل", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            catch { }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

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

                if (txt_mdate.Enabled == true)
                {
                    txt_mdate.Focus();
                    // txt_mdate.Select();
                    txt_mdate.SelectionStart = 0;
                    //   txt_mdate.SelectionLength = 0;// txt_mdate.Text.Length;  
                }
                else
                {
                    txt_desc.Focus();
                }
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
                txt_desc.Focus();
            }
        }

        private void txt_desc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_custno.Focus();
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
                Fill_Data(cmb_salctr.SelectedValue.ToString(), cmb_type.SelectedValue.ToString(), txt_ref.Text);
                total();
            }
            catch { }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

            try
            {

                //  DataSet ds1 = new DataSet();

                DataTable dt = new DataTable();
                int cn = 1;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    /*
                    dt.Columns.Add(col.Name);
                    // dt.Columns.Add(col.HeaderText);
                     */
                    dt.Columns.Add("c" + cn.ToString());
                    cn++;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataRow dRow = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;

                        if (cell.ColumnIndex == 3)
                        {
                            DataRow[] res = dtunits.Select("unit_id ='" + dRow[3] + "'");
                            //   row[2] = res[0][1];
                            dRow[cell.ColumnIndex] = res[0][1];
                        }
                    }
                    dt.Rows.Add(dRow);
                }


                //////////foreach (DataRow row in dt.Rows)
                //////////{/*
                //////////    DataView dv1 = dtunits.DefaultView;
                //////////    dv1.RowFilter = "unit_id ='" + row[2] +"'";
                //////////    DataTable dtNew = dv1.ToTable();
                //////////    //dcombo.DataSource = dtNew;
                //////////    row[2] = dtNew.Rows[0][1];
                //////////  * */
                //////////    DataRow[] res = dtunits.Select("unit_id ='" + row[2] + "'");
                //////////    row[2] = res[0][1];
                //////////    // row[6] = "%" + row[6];
                //////////}

                // dataGridView2.DataSource = dt;
                if (BL.CLS_Session.prnt_type.Equals("0"))
                {
                    Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();
                    rf.reportViewer1.LocalReport.DataSources.Clear();
                    rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("sdt", dt));
                    // rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("account", dth));

                    //***************ok  rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sal.rpt.Sal_Tran_Tax_rpt.rdlc";
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\reports\Sal_Ofr_Tax_rpt" + numericUpDown1.Value + ".rdlc"))
                        rf.reportViewer1.LocalReport.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Sal_Ofr_Tax_rpt" + numericUpDown1.Value + ".rdlc";
                    else
                        rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sal.rpt.Sal_Tran_Tax_rpt.rdlc";

                    //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                    //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                    ReportParameter[] parameters = new ReportParameter[19];
                    parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                    //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                    //  parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());
                    parameters[1] = new ReportParameter("br_name", BL.CLS_Session.brname);
                    parameters[2] = new ReportParameter("type", cmb_type.Text);
                    parameters[3] = new ReportParameter("ref", txt_ref.Text);
                    parameters[4] = new ReportParameter("date", txt_mdate.Text);
                    parameters[5] = new ReportParameter("cust", txt_custnam.Text);

                    parameters[6] = new ReportParameter("desc", txt_desc.Text);
                    parameters[7] = new ReportParameter("tax_id", BL.CLS_Session.tax_no);
                    parameters[8] = new ReportParameter("total", txt_total.Text);
                    parameters[9] = new ReportParameter("descount", txt_des.Text);
                    parameters[10] = new ReportParameter("tax", txt_tax.Text);
                    parameters[11] = new ReportParameter("nettotal", txt_net.Text);

                    BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(txt_net.Text), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);

                    //  MessageBox.Show(toWord.ConvertToArabic());
                    parameters[12] = new ReportParameter("a_toword", toWord.ConvertToArabic());
                    parameters[13] = new ReportParameter("custno", txt_custno.Text);
                    parameters[14] = new ReportParameter("note", txt_remark.Text);
                    parameters[15] = new ReportParameter("clnt_taxid", txt_taxid.Text);
                    parameters[16] = new ReportParameter("trans", cmb_exits.Text);
                    parameters[17] = new ReportParameter("count", dataGridView1.RowCount.ToString());
                    parameters[18] = new ReportParameter("salman", cmb_salman.Text);

                    rf.reportViewer1.LocalReport.SetParameters(parameters);
                    // */
                    rf.reportViewer1.RefreshReport();
                    rf.MdiParent = ParentForm;
                    rf.Show();
                }
                else if (BL.CLS_Session.prnt_type.Equals("1"))
                {
                    FastReport.Report rpt = new FastReport.Report();

                    rpt.Load(Environment.CurrentDirectory + @"\reports\FR_Sal_Offr_Tax_rpt" + numericUpDown1.Value + ".frx");
                    //  rpt.SetParameterValue("br_name", BL.CLS_Session.brname);
                    rpt.SetParameterValue("comp_name", BL.CLS_Session.cmp_name);
                    rpt.SetParameterValue("br_name", BL.CLS_Session.brname);
                    rpt.SetParameterValue("type", cmb_type.Text);
                    rpt.SetParameterValue("ref", txt_ref.Text);
                    rpt.SetParameterValue("date", txt_mdate.Text);
                    rpt.SetParameterValue("cust", txt_custnam.Text);
                    rpt.SetParameterValue("desc", txt_desc.Text);

                    rpt.SetParameterValue("tax_id", BL.CLS_Session.tax_no);
                    rpt.SetParameterValue("total", txt_total.Text);
                    rpt.SetParameterValue("descount", txt_des.Text);
                    rpt.SetParameterValue("tax", txt_tax.Text);
                    rpt.SetParameterValue("nettotal", txt_net.Text);


                    BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(txt_net.Text), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);

                    //  MessageBox.Show(toWord.ConvertToArabic());
                    rpt.SetParameterValue("a_toword", toWord.ConvertToArabic());
                    rpt.SetParameterValue("custno", txt_custno.Text);
                    rpt.SetParameterValue("note", txt_remark.Text);
                    rpt.SetParameterValue("clnt_taxid", txt_taxid.Text);
                    rpt.SetParameterValue("trans", cmb_exits.Text);
                    rpt.SetParameterValue("count", dataGridView1.RowCount.ToString());
                    rpt.SetParameterValue("salman", cmb_salman.Text);
                    rpt.SetParameterValue("taxper", BL.CLS_Session.tax_per);


                    // string dtt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ", new CultureInfo("en-US", false));
                    string dtt = Convert.ToDateTime(txt_mdate.Text).ToString("yyyy-MM-ddTHH:mm:ssZ", new CultureInfo("en-US", false));

                    var tlvs = new GenerateQrCode.GenerateQrCodeTLV(
                           BL.CLS_Session.cmp_ename,
                            BL.CLS_Session.tax_no,
                            dtt,
                            txt_net.Text,
                           Math.Round(Convert.ToDouble(txt_tax.Text), 2).ToString());
                    //////////////////////// OKKKKKK ////////////////////////////////////////////////////////////////////////////////////////////////////
                    ////////////QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    ////////////QRCodeData qrCodeData = qrGenerator.CreateQrCode(Convert.ToBase64String(tlvs.GetQRCode()), QRCodeGenerator.ECCLevel.Q);
                    ////////////QRCode qrCode = new QRCode(qrCodeData);
                    //////////////Bitmap qrCodeImage = qrCode.GetGraphic(pixelsPerModule);
                    ////////////// return Convert.ToBase64String(ToArray(qrCodeImage));
                    //////////////  pictureBoxQRCode.BackgroundImage = qrCode.GetGraphic(20);
                    ////////////qr_img qri = new qr_img();
                    //////////////string qrstr = "saiffd";
                    //////////////var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(qrstr);
                    //////////////rpt.SetParameterValue("Qr_Code",System.Convert.ToBase64String(plainTextBytes));
                    ////////////// MessageBox.Show(tlvs.QRCodeBase64());
                    ////////////qri.pictureBox1.BackgroundImage = qrCode.GetGraphic(20);// ByteToImage(tlvs.GetQRCode());
                    ////////////qri.ShowDialog();

                    ////////////var graph = rpt.FindObject("Picture1") as FastReport.PictureObject;
                    ////////////graph.Image = qrCode.GetGraphic(20);
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    // MessageBox.Show();
                    // rpt.SetParameterValue("qr", Convert.ToBase64String(tlvs.GetQRCode()));
                    rpt.SetParameterValue("qr", Convert.ToBase64String(tlvs.GetQRCode()));

                    // rpt.Prepare();

                    // rpt.SetParameterValue("qr", graph);

                    ///////////////////////////////////////////////////////////////////////ok  MessageBox.Show(Convert.ToBase64String(tlvs.GetQRCode()));

                    rpt.RegisterData(dt, "tbl_rpt");
                    //// rpt.RegisterData(ds.Tables["dtl"], "pos_dtl");

                    // rpt.PrintSettings.ShowDialog = false;
                    // rpt.PrintSettings.Copies =Convert.ToInt32(txt_count.Text);

                    // rpt.Print();

                    FR_Viewer rptd = new FR_Viewer(rpt);

                    rptd.MdiParent = MdiParent;
                    rptd.Show();
                    //////PrinterSettings settings = new PrinterSettings();
                    ////////  printer_nam = settings.PrinterName;

                    //////rpt.PrintSettings.Printer = settings.PrinterName; ;// "pos";
                    //////rpt.PrintSettings.ShowDialog = false;
                    //////// FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                    //////// rpt.PrintSettings.Copies =Convert.ToInt32(txt_count.Text);
                    //////rpt.Print();
                }

            }
           // catch (Exception ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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
            if ( dataGridView1.CurrentCell.ColumnIndex == 5 || dataGridView1.CurrentCell.ColumnIndex == 6 || dataGridView1.CurrentCell.ColumnIndex == 7 || dataGridView1.CurrentCell.ColumnIndex == 8) //Desired Column
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
                if (dataGridView1.CurrentCell == dataGridView1[3, dataGridView1.CurrentRow.Index] || dataGridView1.CurrentCell == dataGridView1[5, dataGridView1.CurrentRow.Index] || dataGridView1.CurrentCell == dataGridView1[6, dataGridView1.CurrentRow.Index] || dataGridView1.CurrentCell == dataGridView1[7, dataGridView1.CurrentRow.Index])
            {
                //   total();
                if (shameltax)
                {
                    DataRow[] dtrvat = dtvat.Select("tax_id ='" + dataGridView1.CurrentRow.Cells[10].Value + "'");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";

                   // dataGridView1.CurrentRow.Cells[8].Value = Math.Round((Convert.ToDouble(dtrvat[0][2])) / (Convert.ToDouble(100) + Convert.ToDouble(dtrvat[0][2])) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value))) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value))) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)) / 100), 2);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                   //// dataGridView1.CurrentRow.Cells[8].Value =Math.Round( (Convert.ToDouble(dtrvat[0][2])) / (Convert.ToDouble(100) + Convert.ToDouble(dtrvat[0][2])) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * 1)-((Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * 1) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)) / 100),2);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                    dataGridView1.CurrentRow.Cells[8].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : Math.Round((Convert.ToDouble(dtrvat[0][2])) / (Convert.ToDouble(100) + Convert.ToDouble(dtrvat[0][2])) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value))) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value))) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)) / 100), 4);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                    //  var filteredData = dtvat.Select("tax_id='" + dt222.Rows[0][11] + "'").CopyToDataTable();
                    //  dataGridView1.CurrentRow.Cells[7].Value = ((Convert.ToDouble(filteredData.Rows[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)).ToString();
                    dataGridView1.CurrentRow.Cells[9].Value = (Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value))+ Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value)) - ( Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)))+ Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value) / 100);// + Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value);
                  
                    // dataGridView1.CurrentRow.Cells[7].Value = (Convert.ToDouble(dtrvat[0][2]) / (Convert.ToDouble(100) + Convert.ToDouble(dtrvat[0][2]))) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[8].Value);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                }
                else
                {
                    DataRow[] dtrvat = dtvat.Select("tax_id ='" + dataGridView1.CurrentRow.Cells[10].Value + "'");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
               
                      //  dataGridView1.CurrentRow.Cells[7].Value = ((Convert.ToDouble(dtrvat[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)).ToString();
                 //   dataGridView1.CurrentRow.Cells[8].Value = Math.Round(((Convert.ToDouble(dtrvat[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)  / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * (Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)) / 100), 2);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                  ////  dataGridView1.CurrentRow.Cells[8].Value = Math.Round(((Convert.ToDouble(dtrvat[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * 1) - (((Convert.ToDouble(dtrvat[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * 1 * Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)) / 100), 2);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                    dataGridView1.CurrentRow.Cells[8].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : Math.Round(((Convert.ToDouble(dtrvat[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value))) - (((Convert.ToDouble(dtrvat[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)) / 100), 4);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                      
                  //  dataGridView1.CurrentRow.Cells[7].Value = 0.00 ;// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                    //  var filteredData = dtvat.Select("tax_id='" + dt222.Rows[0][11] + "'").CopyToDataTable();
                    //  dataGridView1.CurrentRow.Cells[7].Value = ((Convert.ToDouble(filteredData.Rows[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)).ToString();
                  //  dataGridView1.CurrentRow.Cells[8].Value = (Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value) - (((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value)/100);
                    dataGridView1.CurrentRow.Cells[9].Value = (Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value))) - ((Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) * (Convert.ToDouble(dataGridView1.CurrentRow.Cells[3].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) + Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value)) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value) / 100);// + Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value);
                  
                    //this ok  dataGridView1.CurrentRow.Cells[8].Value = (Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value)) - (((Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value))) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[6].Value) / 100);
                  
                    // dataGridView1.CurrentRow.Cells[7].Value = (Convert.ToDouble(dtrvat[0][2]) / 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[8].Value);// * Convert.ToDouble(dataGridView1.CurrentRow.Cells[4].Value);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();

                }
                if (dataGridView1.CurrentRow.Cells[7].Value==null || dataGridView1.CurrentRow.Cells[7].Value=="")
                    dataGridView1.CurrentRow.Cells[7].Value = 0;
            }
            //if (dataGridView1.CurrentCell == dataGridView1[3, dataGridView1.CurrentRow.Index] && isupdate)
            //{
            //    /*
            //    DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
            //    //  DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
            //    // dt.Clear();
            //    dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where rtrim(ltrim(item_no))='" + dataGridView1.CurrentRow.Cells[0].Value + "'");
            //    //  dataGridView1.CurrentRow.Cells[1].Value = dt222.Rows[0][1];
            //    //   dataGridView1.CurrentRow.Cells[0].Value = dt222.Rows[0][1];
            //    //  dataGridView1.CurrentRow.Cells[4].Value = 1.00;
            //    // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][0];
            //    DataView dv1 = dtunits.DefaultView;
            //    // dv1.RowFilter = " pack_id = 1";//or ProductId = 2 or ProductID = 3";
            //    dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
            //    DataTable dtNew = dv1.ToTable();

            //    dcombo.DataSource = dtNew;
            //    dcombo.DisplayMember = "unit_name";
            //    dcombo.ValueMember = "unit_id";

            //    dataGridView1.CurrentRow.Cells[2] = dcombo;
            //   // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];// dt222.Rows[0][5];
            //    dcombo.FlatStyle = FlatStyle.Flat;
            //     */
            //}
           
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
            {
                txt_name.Text = "";
                txt_key.Text = "";
            }
         //   DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select cu_name from customers where cu_no='" + txt_custno.Text + "' and cu_brno='" + BL.CLS_Session.brno + "'");

            DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select cu_no ,cu_name ,cl_acc,cu_crlmt,vndr_taxcode from customers a join cuclass b on a.cu_class=b.cl_no  where a.inactive=0 and a.cu_brno='" + BL.CLS_Session.brno + "' and cu_no='" + txt_custno.Text + "'");

            // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

            if (dt2.Rows.Count > 0)
            {
                txt_custnam.Text = dt2.Rows[0][1].ToString();
                txt_temp.Text = dt2.Rows[0][2].ToString();
                txt_crlmt.Text = dt2.Rows[0][3].ToString();
                txt_taxid.Text = dt2.Rows[0]["vndr_taxcode"].ToString();
            }
            else
            {
                txt_custnam.Text = "";
                txt_custno.Text = "";
                txt_temp.Text = "";
                txt_crlmt.Text = "";
            }
            //    txt_code.Text = dth.Rows[0][2].ToString();
            //   txt_opnbal.Text = dth.Rows[0][1].ToString();

        }

        private void txt_key_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                // txt_name.Text = "";
                try
                {
                    Acc.Acc_Statment_Exp f6 = new Acc.Acc_Statment_Exp(txt_key.Text);
                    f6.MdiParent = ParentForm;
                    f6.Show();
                }
                catch { }

            }

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


            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
            {
                Acc.Acc_Card ac = new Acc.Acc_Card(txt_key.Text);
                ac.MdiParent = ParentForm;
                ac.Show();
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
                cmb_type.Focus();
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
            try
            {
                if (BL.CLS_Session.up_sal_icost)
                    txt_icost.Text =Math.Round(Convert.ToDouble(dataGridView1.CurrentRow.Cells[11].Value.ToString()),2).ToString();
               
                else
                    txt_icost.Text = "-";

                txt_curbal.Text = dataGridView1.CurrentRow.Cells["cur_bal"].Value.ToString();
            }
            catch { }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

       // private static readonly Regex IsDouble = new Regex(@"^(0|(-?(((0|[1-9]\d*)\.\d+)|([1-9]\d*))))$");

        private void txt_camt_KeyPress(object sender, KeyPressEventArgs e)
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
          
            // allows 0-9, backspace, and decimal
            /*
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
             * */
            //var text = txt_des.Text.Trim();
            //if (Regex.IsMatch(text, @"^[0-9]([.,][0-9]{1,3})?$"))
            //{
            //    // Do something here
            //}
            //else
            //{
            //    txt_des.Text = "0";
            //}
        }

        private void total()
        {
            //if ((Convert.ToDouble(txt_desper.Text) > Convert.ToDouble(BL.CLS_Session.inv_dsc) && isnew) || (Convert.ToDouble(txt_desper.Text) > Convert.ToDouble(BL.CLS_Session.inv_dsc) && isupdate))
            //{
            //    MessageBox.Show("تجاوزت الخصم المسموح لك");
            //    txt_des.Text = "0";
            //    txt_desper.Text = "0";
            //    txt_des.Focus();
            //    return;
            //}

             string per = (100 * (Convert.ToDouble(txt_des.Text)) / Convert.ToDouble(txt_total.Text)).ToString();

                //  if ((Convert.ToDouble(per) > Convert.ToDouble(BL.CLS_Session.inv_dsc)))
             if ((Convert.ToDouble(per) > Convert.ToDouble(BL.CLS_Session.inv_dsc)))
                {
                    MessageBox.Show("تجاوزت الخصم المسموح لك","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txt_des.Text = "0";
                    txt_desper.Text = "0";
                    txt_des.Focus();
                    return;
                }

             double sum = 0, sumv = 0, sumc = 0, sumfv = 0;
            foreach (DataGridViewRow ro in dataGridView1.Rows)
            {
                if (shameltax)
                {
                    sum = sum + Convert.ToDouble(ro.Cells[9].Value);
                    sumv = sumv + Convert.ToDouble(ro.Cells[8].Value);
                    sumfv = sumfv + (Convert.ToDouble(ro.Cells[8].Value) == 0 ? Convert.ToDouble(ro.Cells[9].Value) : 0);
                    sumc = sumc + (Convert.ToDouble(ro.Cells[11].Value) * Convert.ToDouble(ro.Cells[5].Value));
                }
                else
                {
                  //  sum = sum + Convert.ToDouble(ro.Cells[8].Value) - Convert.ToDouble(ro.Cells[7].Value);
                    sum = sum + Convert.ToDouble(ro.Cells[9].Value);
                    sumv = sumv + Convert.ToDouble(ro.Cells[8].Value);
                    sumfv = sumfv + (Convert.ToDouble(ro.Cells[8].Value) == 0 ? Convert.ToDouble(ro.Cells[9].Value) : 0);
                    sumc = sumc + (Convert.ToDouble(ro.Cells[11].Value) * Convert.ToDouble(ro.Cells[5].Value));
                }
            }
          //  textBox1.Text = sum.ToString();
           

           
           // txt_tax.Text = sumv.ToString();
         
           // if(isnew)
           //  txt_tax.Text = (Math.Round((sumv - (sumv * Convert.ToDouble(txt_desper.Text) / 100)), 1)).ToString();

            if (shameltax)
                txt_total.Text = Math.Round(sum , 2).ToString();
            else
                txt_total.Text = Math.Round(sum ,2).ToString();

            txt_cost.Text = Math.Round(sumc,2).ToString();
           // txt_des.Text = (Math.Round(((Convert.ToDouble(txt_desper.Text) * Convert.ToDouble(txt_total.Text)) / 100),2)).ToString();

            if (shameltax)
                txt_net.Text = (Math.Round((Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_des.Text)) ,2)).ToString();
            else
                txt_net.Text = (Math.Round((Convert.ToDouble(txt_total.Text) + Convert.ToDouble(txt_tax.Text) - Convert.ToDouble(txt_des.Text)) ,2)).ToString();
            //  txt_tax.Text = (Math.Round( (sumv - (sumv * Convert.ToDouble(txt_desper.Text)/100)),2)).ToString();

           // txt_tax.Text = BL.CLS_Session.isshamltax.Equals("1") ? Math.Round(((Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_des.Text)) / ((100) / BL.CLS_Session.tax_per)), 2).ToString() : Math.Round(((Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_des.Text)) / ((100 + BL.CLS_Session.tax_per) / BL.CLS_Session.tax_per)), 2).ToString();
           //// txt_tax.Text = BL.CLS_Session.isshamltax.Equals("1") ? Math.Round(((Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_des.Text)) / ((100) / tax_per)), 2).ToString() : Math.Round(((Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_des.Text)) / ((100 + tax_per) / tax_per)), 2).ToString();
          ////  txt_tax.Text = BL.CLS_Session.isshamltax.Equals("1") ? Math.Round(((Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_des.Text)) / ((100) / tax_per)), 2).ToString() : Math.Round(((Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_des.Text)) / ((100 + tax_per) / tax_per)), 2).ToString();
            txt_desper.Text = (Math.Round((100 * (Convert.ToDouble(txt_des.Text)) / Convert.ToDouble(txt_total.Text)), 2)).ToString();
            txt_tax.Text = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? "0" : BL.CLS_Session.isshamltax.Equals("1") ? Math.Round(((Convert.ToDouble(sumv) - (Convert.ToDouble(sumv) * (Convert.ToDouble(txt_desper.Text) / 100)))), 4).ToString() : Math.Round(((Convert.ToDouble(sumv) - (Convert.ToDouble(sumv) * (Convert.ToDouble(txt_desper.Text) / 100)))), 4).ToString();
            txt_taxfree.Text = BL.CLS_Session.isshamltax.Equals("1") ? Math.Round(((Convert.ToDouble(sumfv) - (Convert.ToDouble(sumfv) * (Convert.ToDouble(txt_desper.Text) / 100)))), 4).ToString() : Math.Round(((Convert.ToDouble(sumfv) - (Convert.ToDouble(sumfv) * (Convert.ToDouble(txt_desper.Text) / 100)))), 4).ToString();

            //if (shameltax)
            //    txt_total.Text = Math.Round((Convert.ToDouble(txt_net.Text) - Convert.ToDouble(txt_tax.Text) ), 2).ToString();
            //else
            //    txt_total.Text = Math.Round((Convert.ToDouble(txt_net.Text) - Convert.ToDouble(txt_tax.Text) ), 2).ToString();
        }

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_shaml_tax.Checked == true)
            {
                chk_shaml_tax.Enabled = false;
                shameltax = true;
            }
            else
            {
                chk_shaml_tax.Enabled = false;
                shameltax = false ;
            }

        }

        private void txt_desper_TextChanged(object sender, EventArgs e)
        {
           // if (string.IsNullOrEmpty(txt_des.Text))
            //    txt_des.Text = "0";
            if (string.IsNullOrEmpty(txt_desper.Text))
                txt_desper.Text = "0";
            if ((Convert.ToDouble(txt_desper.Text) > Convert.ToDouble(BL.CLS_Session.inv_dsc) && isnew) || (Convert.ToDouble(txt_desper.Text) > Convert.ToDouble(BL.CLS_Session.inv_dsc) && isupdate))
            {
                MessageBox.Show("تجاوزت الخصم المسموح لك", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_des.Text = "0";
                txt_desper.Text = "0";
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

        private void txt_desper_Leave(object sender, EventArgs e)
        {
           

            txt_des.Text = (Math.Round(((Convert.ToDouble(txt_desper.Text) * Convert.ToDouble(txt_total.Text)) / 100),2)).ToString();
            total();
        }

        private void txt_des_Leave(object sender, EventArgs e)
        {
            
            txt_desper.Text = (Math.Round((100 * (Convert.ToDouble(txt_des.Text)) / Convert.ToDouble(txt_total.Text)),2)).ToString();

            total();
        }

        private void txt_des_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_des_Leave(null, null);
                txt_net.Focus();
            }
        }

        private void txt_desper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_desper_Leave(null, null);
                txt_des.Focus();
            }
        }

        private void txt_tax_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_tax.Text))
                txt_tax.Text = "0";
        }

        private void cmb_salctr_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
             try
            {

                if (string.IsNullOrEmpty(txt_desc.Text))
                    txt_desc.Text = cmb_type.Text + " - " + cmb_salctr.Text;
                cmb_salctr_Leave(null, null);

                if (cmb_type.SelectedValue.Equals("05") || cmb_type.SelectedValue.Equals("15"))
                {
                    txt_key.Text = "";
                    txt_name.Text = "";
                }
                else
                {
                    if (string.IsNullOrEmpty(txt_key.Text))
                    {
                        txt_key.Text = strsndoq;
                        load_key();
                    }
                }
               
            }
            catch { }
        }

        private void txt_custno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                // txt_name.Text = "";
                try
                {
                    Cus.Acc_Statment_Exp f6 = new Cus.Acc_Statment_Exp(txt_custno.Text);
                    f6.MdiParent = ParentForm;
                    f6.Show();
                }
                catch { }

            }

            if (e.KeyCode == Keys.F8 && dataGridView1.ReadOnly==false)
            {
                txt_name.Text = "";
                try
                {
                    Search_All f4 = new Search_All("5", "Cus");
                    f4.ShowDialog();
                    txt_custno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_custnam.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txt_temp.Text = f4.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    txt_taxid.Text = f4.dataGridView1.CurrentRow.Cells[5].Value.ToString();
                }
                catch { }

            }
           

             if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
             {
                 Cus.Customers ac = new Cus.Customers(txt_custno.Text);
                 ac.MdiParent = ParentForm;
                 ac.Show();
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
           // cmb_salctr_Leave(null, null);

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

              ////  DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(a_ref)," + BL.CLS_Session.dtbranch.Rows[0]["jv_" + cmb_type.SelectedValue + ""] + ") from acc_hdr where a_type='" + cmb_type.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "' and sl_no='"+cmb_salctr.SelectedValue+"'");
               // DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(a_ref)," + BL.CLS_Session.dtbranch.Rows[0]["jv_21"]+") from acc_hdr where a_type='21' and a_brno='" + BL.CLS_Session.brno + "'");

              ////  int ref_max = Convert.ToInt32(dt.Rows[0][0]) + 1;
               // int ref_max2 = Convert.ToInt32(dt2.Rows[0][0]) + 1;
                //int ref_max = Convert.ToInt32(dt.Rows[0][0]) + 1;
                DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(a_ref),0) from acc_hdr where a_type='" + cmb_type.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "' and sl_no='" + cmb_salctr.SelectedValue + "' and a_ref="+ref_max+"");


                if (isnew)
                {
                    daml.SqlCon_Open();
                    if(dt.Rows[0][0].ToString().Equals("0"))
                        daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_brno,a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,usrid,jhsrc,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + ref_max + ",'" + mdate + "','" + hdate + "','" + txt_desc.Text + "'," + (shameltax ? (Convert.ToDouble(txt_total.Text)) : Convert.ToDouble(txt_total.Text) + Convert.ToDouble(txt_tax.Text) ) + "," + (dataGridView1.RowCount - 1) + ",'" + txt_user.Text + "','Sal1','" + cmb_salctr.SelectedValue + "')", false);
                    if(BL.CLS_Session.is_dorymost.Equals("2"))
                      daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_brno,a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,usrid,jhsrc,sl_no) VALUES('" + BL.CLS_Session.brno + "','21'," + jrdacc + ",'" + mdate + "','" + hdate + "','قيمة بضاعة جرد مستمر'," + (Convert.ToDouble(txt_cost.Text)) + "," + 2 + ",'" + txt_user.Text + "','Grd1','" + cmb_salctr.SelectedValue + "')", false);                
                    daml.SqlCon_Close();                                          
                }
                else
                {
                    daml.SqlCon_Open();
                    int exexcuted = daml.Insert_Update_Delete_retrn_int("update acc_hdr set lastupdt=getdate(),a_mdate='" + mdate + "', a_hdate='" + hdate + "',a_text='" + txt_desc.Text + "',a_amt=" + (shameltax ? (Convert.ToDouble(txt_total.Text)) : Convert.ToDouble(txt_total.Text) + Convert.ToDouble(txt_tax.Text) ) + ",a_entries=" + (dataGridView1.RowCount - 1) + ",usrid='" + txt_user.Text + "' where a_ref=" + txt_ref.Text + " and a_type='" + cmb_type.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "' and sl_no='" + cmb_salctr.SelectedValue + "'", false);
                    if (BL.CLS_Session.is_dorymost.Equals("2"))
                        daml.Insert_Update_Delete_retrn_int("update acc_hdr set lastupdt=getdate(),a_mdate='" + mdate + "', a_hdate='" + hdate + "',a_amt=" + txt_cost.Text + ",usrid='" + txt_user.Text + "' where a_ref=" + txt_jrdacc.Text + " and a_type='21' and a_brno='" + BL.CLS_Session.brno + "' and sl_no='"+ cmb_salctr.SelectedValue +"'", false);
                    exexcuted = exexcuted + daml.Insert_Update_Delete_retrn_int("delete from acc_dtl where a_ref=" + txt_ref.Text + " and a_type='" + cmb_type.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "' and sl_no='" + cmb_salctr.SelectedValue + "'", false);
                    if (BL.CLS_Session.is_dorymost.Equals("2"))
                        daml.Insert_Update_Delete_retrn_int("delete from acc_dtl where a_ref=" + txt_jrdacc.Text + " and a_type='21' and a_brno='" + BL.CLS_Session.brno + "' and sl_no='" + cmb_salctr.SelectedValue + "'", false);
                    daml.SqlCon_Close();
                }

               
                    // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                switch (cmb_type.SelectedValue.ToString())
                {
                    case "05":
                       // strkey = txt_temp.Text;
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strcrdt + "','" + txt_desc.Text + "'," + (shameltax ? (Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_tax.Text)) : Convert.ToDouble(txt_total.Text)) + ",0,1,'" + mdate + "','" + hdate + "','C','" + cmb_salctr.SelectedValue + "','" + stcstcode + "')", false);
                       
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,cu_no,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strkey + "','" + txt_desc.Text + "',0," + txt_net.Text + ",2,'" + mdate + "','" + hdate + "','D','" + txt_custno.Text + "','" + cmb_salctr.SelectedValue + "')", false);
                       
                        if (Convert.ToDouble(txt_des.Text) > 0)
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strdsc + "','خصومات مبيعات'," + 0 + "," + txt_des.Text + ",3,'" + mdate + "','" + hdate + "','D','" + cmb_salctr.SelectedValue + "','"+stcstcode+"')", false);
                        }
                        if (Convert.ToDouble(txt_tax.Text) > 0)
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,taxcatId) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strtax + "','ضريبة مبيعات'," + txt_tax.Text + ", 0," + (Convert.ToDouble(txt_des.Text) > 0? "4" : "3") + ",'" + mdate + "','" + hdate + "','C','" + cmb_salctr.SelectedValue + "',1)", false);
                        }

                        if (BL.CLS_Session.is_dorymost.Equals("2"))
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + BL.CLS_Session.brno + "','21'," + (isnew ? jrdacc : Convert.ToInt32(txt_jrdacc.Text)) + ",'" + stslcust + "','" + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)).ToString() + " قيمة البضاعة مبيعات اجلة '," + 0 + "," + (shameltax ? (Convert.ToDouble(txt_cost.Text)) : Convert.ToDouble(txt_cost.Text)) + ",1,'" + mdate + "','" + hdate + "','D','" + cmb_salctr.SelectedValue + "','" + stcstcode + "')", false);
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + BL.CLS_Session.brno + "','21'," + (isnew ? jrdacc : Convert.ToInt32(txt_jrdacc.Text)) + ",'" + stmkhzon + "','" + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)).ToString() + " قيمة البضاعة مبيعات اجلة '," + (shameltax ? (Convert.ToDouble(txt_cost.Text)) : Convert.ToDouble(txt_cost.Text)) + ",0,2,'" + mdate + "','" + hdate + "','C','" + cmb_salctr.SelectedValue + "','" + stcstcode + "')", false);
                        }

                        daml.SqlCon_Close();
                        break;

                    case "15":
                       // strkey = txt_temp.Text;
                      //  daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strrcrdt + "','" + txt_desc.Text + "',0," + (Convert.ToDouble(txt_net.Text) + Convert.ToDouble(txt_des.Text) - Convert.ToDouble(txt_tax.Text)) + ",1,'" + mdate + "','" + hdate + "','D')", false);
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strrcrdt + "','" + txt_desc.Text + "',0," + (shameltax ? (Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_tax.Text)) : Convert.ToDouble(txt_total.Text)) + ",1,'" + mdate + "','" + hdate + "','D','" + cmb_salctr.SelectedValue + "','" + stcstcode + "')", false);
                        
                    
                      daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,cu_no,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strkey + "','" + txt_desc.Text + "'," + txt_net.Text + "," + 0 + ",2,'" + mdate + "','" + hdate + "','C','" + txt_custno.Text + "','" + cmb_salctr.SelectedValue + "')", false);
                        
                        if (Convert.ToDouble(txt_des.Text) > 0)
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strdsc + "','خصومات مبيعات'," + txt_des.Text + "," + 0 + ",3,'" + mdate + "','" + hdate + "','C','" + cmb_salctr.SelectedValue + "','" + stcstcode + "')", false);
                        }

                        if (Convert.ToDouble(txt_tax.Text) > 0)
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,taxcatId) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strtax + "','ضريبة مبيعات'," + 0 + "," + txt_tax.Text + "," + (Convert.ToDouble(txt_des.Text) > 0 ? "4" : "3") + ",'" + mdate + "','" + hdate + "','D','" + cmb_salctr.SelectedValue + "',1)", false);
                        }

                        if (BL.CLS_Session.is_dorymost.Equals("2"))
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + BL.CLS_Session.brno + "','21'," + (isnew ? jrdacc : Convert.ToInt32(txt_jrdacc.Text)) + ",'" + stslcust + "','" + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)).ToString() + " قيمة البضاعة مرتجع اجل '," + 0 + "," + (shameltax ? (Convert.ToDouble(txt_cost.Text)) : Convert.ToDouble(txt_cost.Text)) + ",1,'" + mdate + "','" + hdate + "','C','" + cmb_salctr.SelectedValue + "','" + stcstcode + "')", false);
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + BL.CLS_Session.brno + "','21'," + (isnew ? jrdacc : Convert.ToInt32(txt_jrdacc.Text)) + ",'" + stmkhzon + "','" + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)).ToString() + " قيمة البضاعة مرتجع اجل '," + (shameltax ? (Convert.ToDouble(txt_cost.Text)) : Convert.ToDouble(txt_cost.Text)) + ",0,2,'" + mdate + "','" + hdate + "','D','" + cmb_salctr.SelectedValue + "','" + stcstcode + "')", false);
                        }

                        daml.SqlCon_Close();
                        break;

                    case "04":
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strcash + "','" + txt_desc.Text + "'," + (shameltax ? (Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_tax.Text)) : Convert.ToDouble(txt_total.Text)) + ",0,1,'" + mdate + "','" + hdate + "','C','" + cmb_salctr.SelectedValue + "','" + stcstcode + "')", false);
                       
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,cu_no,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + txt_key.Text + "','" + txt_desc.Text + "',0," + txt_net.Text + ",2,'" + mdate + "','" + hdate + "','D','" + txt_custno.Text + "','" + cmb_salctr.SelectedValue + "')", false);
                        
                        if (Convert.ToDouble(txt_des.Text) > 0)
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strdsc + "','خصومات مبيعات'," + 0 + "," + txt_des.Text + ",3,'" + mdate + "','" + hdate + "','D','" + cmb_salctr.SelectedValue + "','" + stcstcode + "')", false);
                        }
                        if (Convert.ToDouble(txt_tax.Text) > 0)
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,taxcatId) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strtax + "','ضريبة مبيعات'," + txt_tax.Text + ", 0," + (Convert.ToDouble(txt_des.Text) > 0 ? "4" : "3") + ",'" + mdate + "','" + hdate + "','C','" + cmb_salctr.SelectedValue + "',1)", false);
                        }

                        if (BL.CLS_Session.is_dorymost.Equals("2"))
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + BL.CLS_Session.brno + "','21'," + (isnew ? jrdacc : Convert.ToInt32(txt_jrdacc.Text)) + ",'" + stslcust + "','" + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)).ToString() + " قيمة البضاعة مبيعات نقدي '," + 0 + "," + (shameltax ? (Convert.ToDouble(txt_cost.Text)) : Convert.ToDouble(txt_cost.Text)) + ",1,'" + mdate + "','" + hdate + "','D','" + cmb_salctr.SelectedValue + "','" + stcstcode + "')", false);
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + BL.CLS_Session.brno + "','21'," + (isnew ? jrdacc : Convert.ToInt32(txt_jrdacc.Text)) + ",'" + stmkhzon + "','" + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)).ToString() + " قيمة البضاعة مبيعات نقدي '," + (shameltax ? (Convert.ToDouble(txt_cost.Text)) : Convert.ToDouble(txt_cost.Text)) + ",0,2,'" + mdate + "','" + hdate + "','C','" + cmb_salctr.SelectedValue + "','" + stcstcode + "')", false);
                        }

                        daml.SqlCon_Close();
                        break;

                    case "14":
                      //  daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strrcash + "','" + txt_desc.Text + "',0," + (Convert.ToDouble(txt_net.Text) + Convert.ToDouble(txt_des.Text) - Convert.ToDouble(txt_tax.Text)) + ",1,'" + mdate + "','" + hdate + "','D')", false);
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strrcash + "','" + txt_desc.Text + "',0," + (shameltax ? (Convert.ToDouble(txt_total.Text) - Convert.ToDouble(txt_tax.Text)) : Convert.ToDouble(txt_total.Text)) + ",1,'" + mdate + "','" + hdate + "','D','" + cmb_salctr.SelectedValue + "','" + stcstcode + "')", false);
                      
                     
                      daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,cu_no,sl_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + txt_key.Text + "','" + txt_desc.Text + "'," + txt_net.Text + "," + 0 + ",2,'" + mdate + "','" + hdate + "','C','" + txt_custno.Text + "','" + cmb_salctr.SelectedValue + "')", false);
                       
                        if (Convert.ToDouble(txt_des.Text) > 0)
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strdsc + "','خصومات مبيعات'," + txt_des.Text + "," + 0 + ",3,'" + mdate + "','" + hdate + "','C','" + cmb_salctr.SelectedValue + "','" + stcstcode + "')", false);
                        }
                        if (Convert.ToDouble(txt_tax.Text) > 0)
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,taxcatId) VALUES('" + BL.CLS_Session.brno + "','" + cmb_type.SelectedValue + "'," + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)) + ",'" + strtax + "','ضريبة مبيعات'," + 0 + "," + txt_tax.Text + "," + (Convert.ToDouble(txt_des.Text) > 0 ? "4" : "3") + ",'" + mdate + "','" + hdate + "','D','" + cmb_salctr.SelectedValue + "',1)", false);
                        }

                        if (BL.CLS_Session.is_dorymost.Equals("2"))
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + BL.CLS_Session.brno + "','21'," + (isnew ? jrdacc : Convert.ToInt32(txt_jrdacc.Text)) + ",'" + stslcust + "','" + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)).ToString() + " قيمة البضاعة مرتجع نقدي '," + 0 + "," + (shameltax ? (Convert.ToDouble(txt_cost.Text)) : Convert.ToDouble(txt_cost.Text)) + ",1,'" + mdate + "','" + hdate + "','C','" + cmb_salctr.SelectedValue + "','" + stcstcode + "')", false);
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + BL.CLS_Session.brno + "','21'," + (isnew ? jrdacc : Convert.ToInt32(txt_jrdacc.Text)) + ",'" + stmkhzon + "','" + (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)).ToString() + " قيمة البضاعة مرتجع نقدي '," + (shameltax ? (Convert.ToDouble(txt_cost.Text)) : Convert.ToDouble(txt_cost.Text)) + ",0,2,'" + mdate + "','" + hdate + "','D','" + cmb_salctr.SelectedValue + "','" + stcstcode + "')", false);
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
            DataRow[] dr = dtslctr.Select("sl_no = '" + cmb_salctr.SelectedValue + "'");
            strsndoq = dr[0][3].ToString();
            strcash = dr[0][4].ToString();
            strcrdt = dr[0][5].ToString();
            strrcash = dr[0][6].ToString();
            strrcrdt = dr[0][7].ToString();
            strdsc = dr[0][8].ToString();
            strtax = dr[0][9].ToString();
            stwhno = dr[0][14].ToString();
            stcstcode = dr[0]["cstcode"].ToString();
            stslcust = dr[0]["dp_salcust_acc"].ToString();
            stmkhzon = dr[0]["dp_mkhzon_acc"].ToString();

            //if (string.IsNullOrEmpty(txt_key.Text))
            //txt_key.Text = dr[0][3].ToString();
           // MessageBox.Show(strcash);
        }

        private double chek_bal_after()
        {
            double bal=0;
            if (txt_custno.Text != "")
            {
               // double bal;
                DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("get_cust_bal @cust_no='" + txt_custno.Text + "', @branch_no='" + BL.CLS_Session.brno + "'");
                bal = Convert.ToDouble(dt.Rows[0][0]);
            }
                //txt_crlmt.Text=dt.Rows[0][0].ToString();
            return bal;
            
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
           // txt_icost.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(new Uri(Application.StartupPath + @"\Logo.jpg").AbsoluteUri);
            MessageBox.Show(("file:///" + Application.StartupPath + @"\Logo.jpg").Replace(@"\", "/"));
            try
            {
                Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();
                //  DataSet ds1 = new DataSet();

                DataTable dt = new DataTable();
                int cn = 1;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    /*
                    dt.Columns.Add(col.Name);
                    // dt.Columns.Add(col.HeaderText);
                     */
                    dt.Columns.Add("c" + cn.ToString());
                    cn++;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataRow dRow = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;

                        if (cell.ColumnIndex == 2)
                        {
                            DataRow[] res = dtunits.Select("unit_id ='" + dRow[2] + "'");
                            //   row[2] = res[0][1];
                            dRow[cell.ColumnIndex] = res[0][1];
                        }
                    }
                    dt.Rows.Add(dRow);
                }


             

                rf.reportViewer1.LocalReport.DataSources.Clear();
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("sdt", dt));
                // rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("account", dth));

                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sal.rpt.Sal_Tran_Tax_i_rpt.rdlc";

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[14];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                //  parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());
                parameters[1] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[2] = new ReportParameter("type", cmb_type.Text);
                parameters[3] = new ReportParameter("ref", txt_ref.Text);
                parameters[4] = new ReportParameter("date", txt_mdate.Text);
                parameters[5] = new ReportParameter("cust", txt_custnam.Text + "  " + txt_custno.Text);

                parameters[6] = new ReportParameter("desc", txt_desc.Text);
                parameters[7] = new ReportParameter("tax_id", BL.CLS_Session.tax_no);
                parameters[8] = new ReportParameter("total", txt_total.Text);
                parameters[9] = new ReportParameter("descount", txt_des.Text);
                parameters[10] = new ReportParameter("tax", txt_tax.Text);
                parameters[11] = new ReportParameter("nettotal", txt_net.Text);
                

                BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(txt_net.Text), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);

                //  MessageBox.Show(toWord.ConvertToArabic());
                parameters[12] = new ReportParameter("a_toword", toWord.ConvertToArabic());

               // parameters[13] = new ReportParameter("img",new Uri(Application.StartupPath + @"\Logo.jpg").AbsoluteUri);
                parameters[13] = new ReportParameter("img", ("file:///" + Application.StartupPath + @"\Logo.jpg").Replace(@"\","/"));
                rf.reportViewer1.LocalReport.EnableExternalImages = true;
                rf.reportViewer1.LocalReport.SetParameters(parameters);
                // */
                
                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void chk_usebarcode_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_usebarcode.Checked==true)
            {
              ////  dataGridView1.Columns[0].HeaderText = "الباركود";
                // dataGridView1.Columns[0].FillWeight = 20;
                // dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = true;
            }
            else
            {
               //// dataGridView1.Columns[0].HeaderText = "رقم الصنف";
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[0].Visible = true;
            }
        }

        private void txt_custno_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txt_custno_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           // Thread.Sleep(1000); // One second.
            try
            {
                daml.SqlCon_Open();
                daml.Exec_Query_only("bld_whbins_cost_by_tran @cond='sal', @br_no ='" + BL.CLS_Session.brno + "', @inv_type='" + cmb_type.SelectedValue + "',@wh_no='" + stwhno + "', @ref=" + txt_ref.Text + ",@depart ='" + cmb_salctr.SelectedValue + "'");
                daml.SqlCon_Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmb_salman_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_salman.SelectedIndex = -1;
            }
        }

        private void btn_prvs_Click(object sender, EventArgs e)
        {
            

            try{
                if (isnew == false && isupdate == false)
                {
                    isfeched = true;
                    dthdr = daml.SELECT_QUIRY_only_retrn_dt("select * from sales_hdr where branch='" + BL.CLS_Session.brno + "' order by released OFFSET " + ((temp)) + " ROWS FETCH NEXT 1 ROWS ONLY ");
                    // string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price)+aa.tax_amt)-(((aa.qty*aa.price)+aa.tax_amt)*aa.discpc/100)";
                    // string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price)+aa.tax_amt)-(((aa.qty*aa.price)+aa.tax_amt)*aa.discpc/100)";
                    string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price))-(((aa.qty*aa.price))*aa.discpc/100)";

                    dtdtl = daml.SELECT_QUIRY_only_retrn_dt("select aa.itemno itemno,bb.item_name item_name,aa.pack pack,aa.pkqty pkqty,aa.qty qty,aa.price price,aa.discpc discpc,aa.tax_amt tax_amt," + strvt + " total,aa.tax_id tax_id, aa.cost cost,aa.barcode barcode,bb.item_cbalance cur_bal from sales_dtl aa,items bb where aa.itemno=bb.item_no and aa.invtype='" + dthdr.Rows[0]["invtype"] + "' and aa.ref=" + dthdr.Rows[0]["ref"] + " and aa.branch='" + BL.CLS_Session.brno + "' and aa.slcenter='" + dthdr.Rows[0]["slcenter"] + "' order by aa.folio");// and aa.a_type='" + atype + "' and aa.a_ref=" + aref + "");
                    Fill_Data("", "", "");
                    if ((temp + 1) < cnt)
                        temp++;
                }
             }
            catch { }
            //if(cnt<=cntto)
            //cnt--;
           // Select 

        //    dthdr = daml.SELECT_QUIRY_only_retrn_dt("select * from acc_hdr order by a_sysdate OFFSET "+ cnt +" ROWS FETCH NEXT 1 ROWS ONLY invtype='" + atype + "' and ref=" + aref + " and branch='" + BL.CLS_Session.brno + "' and slcenter='" + slctr + "'");
            // string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price)+aa.tax_amt)-(((aa.qty*aa.price)+aa.tax_amt)*aa.discpc/100)";
            // string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price)+aa.tax_amt)-(((aa.qty*aa.price)+aa.tax_amt)*aa.discpc/100)";
         //   string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price))-(((aa.qty*aa.price))*aa.discpc/100)";

         //   dtdtl = daml.SELECT_QUIRY_only_retrn_dt("select aa.itemno itemno,bb.item_name item_name,aa.pack pack,aa.pkqty pkqty,aa.qty qty,aa.price price,aa.discpc discpc,aa.tax_amt tax_amt," + strvt + " total,aa.tax_id tax_id, aa.cost cost,aa.barcode barcode,bb.item_cbalance cur_bal from sales_dtl aa,items bb where aa.itemno=bb.item_no and aa.invtype='" + atype + "' and aa.ref=" + aref + " and aa.branch='" + BL.CLS_Session.brno + "' and aa.slcenter='" + slctr + "' order by aa.folio");// and aa.a_type='" + atype + "' and aa.a_ref=" + aref + "");
               
        }

        private void btn_last_Click(object sender, EventArgs e)
        {
            try
            {
                if (isnew == false && isupdate == false)
                {
                    temp = cnt - 2;
                    cnt = cntto;
                    isfeched = true;
                    dthdr = daml.SELECT_QUIRY_only_retrn_dt("select * from sales_hdr where branch='" + BL.CLS_Session.brno + "' order by released OFFSET " + ((cnt - 1)) + " ROWS FETCH NEXT 1 ROWS ONLY ");
                    // string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price)+aa.tax_amt)-(((aa.qty*aa.price)+aa.tax_amt)*aa.discpc/100)";
                    // string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price)+aa.tax_amt)-(((aa.qty*aa.price)+aa.tax_amt)*aa.discpc/100)";
                    string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price))-(((aa.qty*aa.price))*aa.discpc/100)";

                    dtdtl = daml.SELECT_QUIRY_only_retrn_dt("select aa.itemno itemno,bb.item_name item_name,aa.pack pack,aa.pkqty pkqty,aa.qty qty,aa.price price,aa.discpc discpc,aa.tax_amt tax_amt," + strvt + " total,aa.tax_id tax_id, aa.cost cost,aa.barcode barcode,bb.item_cbalance cur_bal from sales_dtl aa,items bb where aa.itemno=bb.item_no and aa.invtype='" + dthdr.Rows[0]["invtype"] + "' and aa.ref=" + dthdr.Rows[0]["ref"] + " and aa.branch='" + BL.CLS_Session.brno + "' and aa.slcenter='" + dthdr.Rows[0]["slcenter"] + "' order by aa.folio");// and aa.a_type='" + atype + "' and aa.a_ref=" + aref + "");
                    Fill_Data("", "", "");
                }
            }
            catch { }
        }

        private void btn_first_Click(object sender, EventArgs e)
        {
            try
            {
                if (isnew == false && isupdate == false)
                {
                    temp = 1;
                    isfeched = true;
                    dthdr = daml.SELECT_QUIRY_only_retrn_dt("select * from sales_hdr where branch='" + BL.CLS_Session.brno + "' order by released OFFSET " + 0 + " ROWS FETCH NEXT 1 ROWS ONLY ");
                    // string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price)+aa.tax_amt)-(((aa.qty*aa.price)+aa.tax_amt)*aa.discpc/100)";
                    // string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price)+aa.tax_amt)-(((aa.qty*aa.price)+aa.tax_amt)*aa.discpc/100)";
                    string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price))-(((aa.qty*aa.price))*aa.discpc/100)";

                    dtdtl = daml.SELECT_QUIRY_only_retrn_dt("select aa.itemno itemno,bb.item_name item_name,aa.pack pack,aa.pkqty pkqty,aa.qty qty,aa.price price,aa.discpc discpc,aa.tax_amt tax_amt," + strvt + " total,aa.tax_id tax_id, aa.cost cost,aa.barcode barcode,bb.item_cbalance cur_bal from sales_dtl aa,items bb where aa.itemno=bb.item_no and aa.invtype='" + dthdr.Rows[0]["invtype"] + "' and aa.ref=" + dthdr.Rows[0]["ref"] + " and aa.branch='" + BL.CLS_Session.brno + "' and aa.slcenter='" + dthdr.Rows[0]["slcenter"] + "' order by aa.folio");// and aa.a_type='" + atype + "' and aa.a_ref=" + aref + "");
                    Fill_Data("", "", "");
                }
            }
            catch { }


        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            try
            {
                if (isnew == false && isupdate == false)
                {
                    isfeched = true;
                    dthdr = daml.SELECT_QUIRY_only_retrn_dt("select * from sales_hdr where branch='" + BL.CLS_Session.brno + "' order by released OFFSET " + ((temp)) + " ROWS FETCH NEXT 1 ROWS ONLY ");
                    // string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price)+aa.tax_amt)-(((aa.qty*aa.price)+aa.tax_amt)*aa.discpc/100)";
                    // string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price)+aa.tax_amt)-(((aa.qty*aa.price)+aa.tax_amt)*aa.discpc/100)";
                    string strvt = Convert.ToBoolean(dthdr.Rows[0]["with_tax"]) ? "((aa.qty*aa.price)-((aa.qty*aa.price)*aa.discpc/100))" : "((aa.qty*aa.price))-(((aa.qty*aa.price))*aa.discpc/100)";

                    dtdtl = daml.SELECT_QUIRY_only_retrn_dt("select aa.itemno itemno,bb.item_name item_name,aa.pack pack,aa.pkqty pkqty,aa.qty qty,aa.price price,aa.discpc discpc,aa.tax_amt tax_amt," + strvt + " total,aa.tax_id tax_id, aa.cost cost,aa.barcode barcode,bb.item_cbalance cur_bal from sales_dtl aa,items bb where aa.itemno=bb.item_no and aa.invtype='" + dthdr.Rows[0]["invtype"] + "' and aa.ref=" + dthdr.Rows[0]["ref"] + " and aa.branch='" + BL.CLS_Session.brno + "' and aa.slcenter='" + dthdr.Rows[0]["slcenter"] + "' order by aa.folio");// and aa.a_type='" + atype + "' and aa.a_ref=" + aref + "");
                    Fill_Data("", "", "");

                    if ((temp) > 0)
                        temp--;
                }
            }
            catch { }
            //if(cnt<=cntto)
            //cnt++;
        }

        private void btn_Save_Enter(object sender, EventArgs e)
        {
            //total();
        }

        private void btn_prtdirct_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;
            DataTable dtdate = daml.SELECT_QUIRY_only_retrn_dt("select released from sales_hdr where branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "' and invtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + "");
            daml.Exec_Query_only("update sales_hdr set inv_printed=isnull(inv_printed,0)+1 where branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "' and invtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + "");
            is_printd = true;

            Fill_Data(cmb_salctr.SelectedValue.ToString(), cmb_type.SelectedValue.ToString(), txt_ref.Text);
            total();

            Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();
            //  DataSet ds1 = new DataSet();

            DataTable dt = new DataTable();
            int cn = 1;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                /*
                dt.Columns.Add(col.Name);
                // dt.Columns.Add(col.HeaderText);
                 */
                dt.Columns.Add("c" + cn.ToString());
                cn++;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;

                    if (cell.ColumnIndex == 14)
                    {
                        DataRow[] res = dtunits.Select("unit_id ='" + dRow[14] + "'");
                        //   row[2] = res[0][1];
                        dRow[cell.ColumnIndex] = res[0][1];
                    }
                }
                dt.Rows.Add(dRow);
            }

            if (BL.CLS_Session.prnt_type.Equals("0"))
            {
            LocalReport report = new LocalReport();
         ///////////////////////////////////////////////   report.ReportEmbeddedResource = "POS.Sal.rpt.Sal_Tran_Tax_rpt.rdlc";
            if (File.Exists(Directory.GetCurrentDirectory() + @"\reports\Sal_Ofr_Tax_TF_rpt" + numericUpDown1.Value + ".rdlc"))
                report.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Sal_Ofr_Tax_TF_rpt" + numericUpDown1.Value + ".rdlc";
            else
                report.ReportEmbeddedResource = "POS.Sal.rpt.Sal_Ofr_Tax_TF_rpt.rdlc";
           // report.DataSources.Add(new ReportDataSource("DataSet1", getYourDatasource()));
            report.DataSources.Add(new ReportDataSource("sdt", dt));
            // rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("sdt", dt));

            ReportParameter[] parameters = new ReportParameter[19];
            parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
            //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
            //  parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());
            parameters[1] = new ReportParameter("br_name", BL.CLS_Session.brname);
            parameters[2] = new ReportParameter("type", cmb_type.Text);
            parameters[3] = new ReportParameter("ref", txt_ref.Text);
            parameters[4] = new ReportParameter("date", txt_mdate.Text);
            parameters[5] = new ReportParameter("cust", txt_custnam.Text + "  " + txt_custno.Text);

            parameters[6] = new ReportParameter("desc", txt_desc.Text);
            parameters[7] = new ReportParameter("tax_id", BL.CLS_Session.tax_no);
            parameters[8] = new ReportParameter("total", txt_total.Text);
            parameters[9] = new ReportParameter("descount", txt_des.Text);
            parameters[10] = new ReportParameter("tax", txt_tax.Text);
            parameters[11] = new ReportParameter("nettotal", txt_net.Text);


            BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(txt_net.Text), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);

            //  MessageBox.Show(toWord.ConvertToArabic());
            parameters[12] = new ReportParameter("a_toword", toWord.ConvertToArabic());
            parameters[13] = new ReportParameter("custno", txt_custno.Text);
            parameters[14] = new ReportParameter("note", txt_remark.Text);
            parameters[15] = new ReportParameter("clnt_taxid", txt_taxid.Text);
            parameters[16] = new ReportParameter("trans", cmb_exits.Text);
            parameters[17] = new ReportParameter("count", dataGridView1.RowCount.ToString());
            parameters[18] = new ReportParameter("salman", cmb_salman.Text);

            report.SetParameters(parameters);


            //var pageSettings = new PageSettings();
            //pageSettings.PaperSize = report.GetDefaultPageSettings().PaperSize;
            //pageSettings.Landscape = report.GetDefaultPageSettings().IsLandscape;
            //pageSettings.Margins = report.GetDefaultPageSettings().Margins;
            BL.Print_Rdlc_Direct.Print(report,1,"");

            ////////BL.PrintReport.pagetype = "";
            ////////BL.PrintReport.pagewidth = "8.5in";
            ////////BL.PrintReport.pagehight = "11in";
            ////////BL.PrintReport.PrintToPrinter(report); // prtr = new BL.PrintReport();
           // BL.PrintRdlcDirect.PrintToPrinter(report);
         //   report.PrintToPrinter();
            }
            else if (BL.CLS_Session.prnt_type.Equals("1"))
            {
                //SqlDataAdapter dacr1 = new SqlDataAdapter("select a.[brno],a.[slno],a.[ref],a.[contr],a.[type],FORMAT (a.[date], 'dd-MM-yyyy hh:mm:ss tt', 'en-us') date,a.[total],a.[count],a.[payed],a.[total_cost],b.sl_name [saleman],a.[req_no],a.[cust_no],a.[discount],a.[net_total],a.[sysdate],a.[gen_ref],a.[tax_amt],a.[card_amt],a.[note],a.[mobile] from pos_hdr a join pos_salmen b on a.saleman=b.sl_id where a.ref=" + txt_ref.Text + " and a.[contr]=" + BL.CLS_Session.ctrno + " and  a.[brno]= '" + BL.CLS_Session.brno + "' ", con2);
                //SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty] from pos_dtl where ref=" + txt_ref.Text + " and [contr]=" + BL.CLS_Session.ctrno + " and  [brno]= '" + BL.CLS_Session.brno + "' ", con2);

                //DataSet1 ds = new DataSet1();

                //dacr1.Fill(ds, "hdr");
                //dacr.Fill(ds, "dtl");

                FastReport.Report rpt = new FastReport.Report();

                rpt.Load(Environment.CurrentDirectory + @"\reports\FR_Sal_Tran_Tax_rpt" + numericUpDown1.Value + ".frx");
                //  rpt.SetParameterValue("br_name", BL.CLS_Session.brname);
                rpt.SetParameterValue("comp_name", BL.CLS_Session.cmp_name);
                rpt.SetParameterValue("br_name", BL.CLS_Session.brname);
                rpt.SetParameterValue("type", cmb_type.Text);
                rpt.SetParameterValue("ref", txt_ref.Text);
                rpt.SetParameterValue("date", txt_mdate.Text);
                rpt.SetParameterValue("cust", txt_custnam.Text);
                rpt.SetParameterValue("desc", txt_desc.Text);

                rpt.SetParameterValue("tax_id", BL.CLS_Session.tax_no);
                rpt.SetParameterValue("total", txt_total.Text);
                rpt.SetParameterValue("descount", txt_des.Text);
                rpt.SetParameterValue("tax", txt_tax.Text);
                rpt.SetParameterValue("nettotal", txt_net.Text);


                BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(txt_net.Text), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);

                //  MessageBox.Show(toWord.ConvertToArabic());
                rpt.SetParameterValue("a_toword", toWord.ConvertToArabic());
                rpt.SetParameterValue("custno", txt_custno.Text);
                rpt.SetParameterValue("note", txt_remark.Text);
                rpt.SetParameterValue("clnt_taxid", txt_taxid.Text);
                rpt.SetParameterValue("trans", cmb_exits.Text);
                rpt.SetParameterValue("count", dataGridView1.RowCount.ToString());
                rpt.SetParameterValue("salman", cmb_salman.Text);
                rpt.SetParameterValue("taxper", BL.CLS_Session.tax_per);
                //string qrstr = BL.CLS_Session.cmp_name + "\n" + BL.CLS_Session.brname + "\n" + BL.CLS_Session.msg1 + "\n" + "رقم الفاتورة : " + txt_ref.Text + "\n" + "تاريخ الفاتورة : " + txt_mdate.Text + "\n" + "ضريبة القيمة المظافة " + BL.CLS_Session.tax_per + "% : " + txt_tax.Text + "\n" + "صافي الفاتورة شامل الضريبة : " + txt_net.Text;
                //string qrstr = "saiffd";
                //var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(qrstr);
                //rpt.SetParameterValue("Qr_Code",System.Convert.ToBase64String(plainTextBytes));
                //var tlvs = new GenerateQrCode.GenerateQrCodeTLV(
                //    "sss",
                //    "310122393500003",
                //    "2022-04-25T15:30:09Z",
                //    "1000.00",
                //    "150.00");


                // string dtt = Convert.ToDateTime(txt_mdate.Text).ToString("yyyy-MM-ddTHH:mm:ssZ", new CultureInfo("en-US", false));
                string dtt = Convert.ToDateTime(dtdate.Rows[0][0].ToString()).ToString("yyyy-MM-ddTHH:mm:ssZ", new CultureInfo("en-US", false));
                var tlvs = new GenerateQrCode.GenerateQrCodeTLV(
                           BL.CLS_Session.cmp_ename,
                            BL.CLS_Session.tax_no,
                            dtt,
                            txt_net.Text,
                           Math.Round(Convert.ToDouble(txt_tax.Text), 2).ToString());
                rpt.SetParameterValue("qr", Convert.ToBase64String(tlvs.GetQRCode()));
                rpt.RegisterData(dt, "tbl_rpt");
                //// rpt.RegisterData(ds.Tables["dtl"], "pos_dtl");

                // rpt.PrintSettings.ShowDialog = false;
                // rpt.PrintSettings.Copies =Convert.ToInt32(txt_count.Text);

                // rpt.Print();


                PrinterSettings settings = new PrinterSettings();
                //  printer_nam = settings.PrinterName;

                rpt.PrintSettings.Printer = settings.PrinterName; ;// "pos";
                //  rpt.PrintSettings.ShowDialog = false;
                // FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                // rpt.PrintSettings.Copies =Convert.ToInt32(txt_count.Text);
                rpt.Print();
            }
        }

        private void txt_r_TextChanged(object sender, EventArgs e)
        {
            Fill_Data(txt_s.Text, txt_t.Text, txt_r.Text);
        }

        private void txt_custno_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_custno.Text) && txt_custno.Enabled==true)
                txt_taxid.Enabled = true;
            else
                txt_taxid.Enabled = false;
        }

        private void txt_taxid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridView1.Focus();
            }
        }

        private void Sal_Tran_D_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F11)
            //{
            //    this.Close();
            //  //  button3_Click(sender, e);
            //}
            //if (e.KeyCode.ToString() == "F1")
            //{
            //    // the user pressed the F1 key
            //    this.Close();
            //    //button3_Click(sender, e);
            //}

            //if (e.KeyCode == Keys.F11)
            //{
            //    this.Close();
            //}
        }

        private void cmb_exits_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_exits.SelectedIndex = -1;
            }
        }

        private void txt_validdate_Enter(object sender, EventArgs e)
        {
            txt_validdate.Focus();
            txt_validdate.SelectAll();
        }

        private void txt_validdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_desc.Focus();
            }
        }

        private void txt_validdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_validdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_validdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    // txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_validdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        //  txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_validdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                        else
                        {
                            //  txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                    }
                }
            }
            catch { }
        }

        private void txt_validdate_Validating(object sender, CancelEventArgs e)
        {
            DateTime rs;

            CultureInfo ci = new CultureInfo("en-US", false);

            if (!DateTime.TryParseExact(this.txt_validdate.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            {

                MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                txt_validdate.Focus();

            }
            if (Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_validdate.Text)) < Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.start_dt)) || Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_validdate.Text)) > Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.end_dt)))
            {

                MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;

            }
        }

        private void designToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.prnt_type.Equals("0"))
            {
                System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + @"\reports\Sal_Ofr_Tax_rpt" + numericUpDown1.Value + ".rdlc");
            }
            else
            {
                FastReport.Report rpt2 = new FastReport.Report();
                rpt2.Load(Environment.CurrentDirectory + @"\reports\FR_Sal_Offr_Tax_rpt" + numericUpDown1.Value + ".frx");
                FR_Designer rptd = new FR_Designer(rpt2);
                rptd.MdiParent = MdiParent;
                rptd.Show();

            }
        }
    }
}



