﻿using System;
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

namespace POS.Acc
{
    public partial class Acc_Tax_Tran2 : BaseForm
    {
       // DataTable dtg;
       // Timer timer;
        DataTable dthdr, dtdtl, dttrtyps,dttaxcats;
        string a_ref, a_type,acc,conkey, a_condi;
        bool isnew,isupdate,notposted;
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate dtv = new BL.Date_Validate();
        SqlConnection con = BL.DAML.con;//new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public Acc_Tax_Tran2(string atype, string aref,string condi)
        {
            InitializeComponent();
          //  this.KeyPreview = true;
            a_ref = aref;
            a_type = atype;
             a_condi = condi;
            //this.KeyPreview = true;
            //this.KeyPress += new KeyPressEventHandler(Control_KeyPress);

        }

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        SelectNextControl(ActiveControl, true, true, true, true);
        //        e.Handled = true;
        //    }
        //}
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (msg.WParam.ToInt32() == (int)Keys.Enter)
            {
                SendKeys.Send("{Tab}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
            // if (e.KeyChar == (char)13)
            try
            {
                if (e.KeyCode == Keys.Delete && (btn_Add.Enabled != true && btn_Edit.Enabled != true) && e.Modifiers != Keys.Shift)
                {
                    int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                    if (selectedIndex > -1)
                    {
                        dataGridView1.Rows.RemoveAt(selectedIndex);
                    }

                }

                if (e.KeyCode == Keys.F8 && dataGridView1.ReadOnly==false)
                {

                    // var selectedCell = dataGridView1.SelectedCells[0];
                    // do something with selectedCell...
                    if (dataGridView1.CurrentCell == dataGridView1[2, dataGridView1.CurrentRow.Index])
                    {
                       // dataGridView1.CurrentRow.Cells[7].Value = 0;

                        Search_All f4 = new Search_All("1", "Acc1");
                        f4.ShowDialog();
                        dataGridView1.CurrentRow.Cells[2].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                        SendKeys.Send("{F2}");
                        SendKeys.Send("{ENTER}");
                       // dataGridView1.CurrentCell = this.dataGridView1[4, dataGridView1.CurrentRow.Index];
                       //////// try
                       //////// {
                       ////////     DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                       ////////     dataGridView1.CurrentRow.Cells[0].Value = "";
                       ////////     dataGridView1.CurrentRow.Cells[1].Value = "";
                       ////////     dataGridView1.CurrentRow.Cells[2].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                       ////////     dataGridView1.CurrentRow.Cells[3].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
                       ////////     dataGridView1.CurrentRow.Cells[8].Value = f4.dataGridView1.CurrentRow.Cells[2].Value;
                       ////////    // dataGridView1.CurrentRow.Cells[7].Value = f4.dataGridView1.CurrentRow.Cells[3].Value;
                       ////////     if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
                       ////////     {
                       ////////         dataGridView1.CurrentRow.Cells[4].Value = txt_desc.Text;
                       ////////     }

                       ////////     dataGridView1.CurrentCell = this.dataGridView1[4, dataGridView1.CurrentRow.Index];

                       ////////     if (dataGridView1.CurrentRow.Cells[8].Value.ToString().Equals("2"))
                       ////////     {

                       ////////         DataView dv1 = dttaxcats.DefaultView;
                       ////////         dv1.RowFilter = "In_out_tax in('" + dataGridView1.CurrentRow.Cells[8].Value + "')";
                       ////////         DataTable dtNew = dv1.ToTable();
                       ////////         dcombo.DataSource = dtNew;
                       ////////         dcombo.DisplayMember = "taxcat_name";
                       ////////         dcombo.ValueMember = "taxcatID";

                       ////////         dataGridView1.CurrentRow.Cells[7] = dcombo;
                       ////////       //  dataGridView1.CurrentRow.Cells[7].Value = "5";
                       ////////         dcombo.FlatStyle = FlatStyle.Flat;
                       ////////     }
                       ////////     else
                       ////////     {
                       ////////         if (dataGridView1.CurrentRow.Cells[8].Value.ToString().Equals("1"))
                       ////////         {

                       ////////             DataView dv1 = dttaxcats.DefaultView;
                       ////////             dv1.RowFilter = "In_out_tax in('" + dataGridView1.CurrentRow.Cells[8].Value + "')";
                       ////////             DataTable dtNew = dv1.ToTable();
                       ////////             dcombo.DataSource = dtNew;
                       ////////             dcombo.DisplayMember = "taxcat_name";
                       ////////             dcombo.ValueMember = "taxcatID";

                       ////////             dataGridView1.CurrentRow.Cells[7] = dcombo;
                       ////////           //  dataGridView1.CurrentRow.Cells[7].Value = "1";
                       ////////             dcombo.FlatStyle = FlatStyle.Flat;
                       ////////         }

                       ////////         else
                       ////////         {
                       ////////             // dataGridView1.CurrentRow.Cells[5] = dcombo;
                       ////////             dataGridView1.CurrentRow.Cells[7] = null;
                       ////////             // dcombo.FlatStyle = FlatStyle.Flat;
                       ////////         }
                       ////////     }
                       //////// }
                       //////// /*
                       ////////dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
                       ////////dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
                       ////////dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                       //////// */

                       ////////     //if (dataGridView1.CurrentRow.Cells[2].Value == null)
                       //////// //{
                       //////// //    dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
                       //////// //}
                       //////// //if (dataGridView1.CurrentRow.Cells[2].Value == null)
                       //////// //{
                       //////// //    dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
                       //////// //}


                       //////// catch { }
                       
                    }

                    if (dataGridView1.CurrentCell == dataGridView1[12, dataGridView1.CurrentRow.Index])
                    {
                        // var selectedCell = dataGridView1.SelectedCells[0];
                        // do something with selectedCell...
                        Search_All f4 = new Search_All("M", "");
                        f4.ShowDialog();



                        dataGridView1.CurrentRow.Cells[12].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                        //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                        /*
                       dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
                       dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
                       dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                        */



                    }

                    if (dataGridView1.CurrentCell == dataGridView1[1, dataGridView1.CurrentRow.Index])
                    {
                        Search_All f4 = new Search_All("7", "Sup2");
                        f4.ShowDialog();

                        try
                        {

                            dataGridView1.CurrentRow.Cells[1].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                            dataGridView1.CurrentRow.Cells[0].Value = "";
                            dataGridView1.CurrentRow.Cells[2].Value = "";
                            dataGridView1.CurrentRow.Cells[3].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
                            dataGridView1.CurrentRow.Cells[9].Value = f4.dataGridView1.CurrentRow.Cells[3].Value;
                            dataGridView1.CurrentRow.Cells[8].Value = "0";
                            if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
                            {
                                dataGridView1.CurrentRow.Cells[4].Value = txt_desc.Text;
                            }

                            dataGridView1.CurrentCell = this.dataGridView1[4, dataGridView1.CurrentRow.Index];

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

                    if (dataGridView1.CurrentCell == dataGridView1[0, dataGridView1.CurrentRow.Index])
                    {
                        Search_All f4 = new Search_All("5", "Cus2");
                        f4.ShowDialog();

                        try
                        {

                            dataGridView1.CurrentRow.Cells[0].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                            dataGridView1.CurrentRow.Cells[1].Value = "";
                            dataGridView1.CurrentRow.Cells[2].Value = "";
                            dataGridView1.CurrentRow.Cells[3].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
                            dataGridView1.CurrentRow.Cells[9].Value = f4.dataGridView1.CurrentRow.Cells[3].Value;
                            dataGridView1.CurrentRow.Cells[8].Value = "0";
                            if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
                            {
                                dataGridView1.CurrentRow.Cells[4].Value = txt_desc.Text;
                            }

                            dataGridView1.CurrentCell = this.dataGridView1[4, dataGridView1.CurrentRow.Index];

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

                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[1])
                {
                    if (e.KeyCode == Keys.F7)
                    {
                        Sup.Acc_Statment_Exp f4a = new Sup.Acc_Statment_Exp(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        f4a.MdiParent = ParentForm;
                        f4a.Show();
                    }

                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
                    {
                        Sup.Suppliers ac = new Sup.Suppliers(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        ac.MdiParent = ParentForm;
                        ac.Show();
                    }
                }

                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0])
                {
                    if (e.KeyCode == Keys.F7)
                    {
                    Cus.Acc_Statment_Exp f4a = new Cus.Acc_Statment_Exp(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        f4a.MdiParent = ParentForm;
                        f4a.Show();
                    }

                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
                    {
                        Cus.Customers ac = new Cus.Customers(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        ac.MdiParent = ParentForm;
                        ac.Show();
                    }
                }

                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[2])
                {
                    if (e.KeyCode == Keys.F7)
                    {
                        Acc_Statment_Exp f4a = new Acc_Statment_Exp(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                        f4a.MdiParent = ParentForm;
                        f4a.Show();
                    }

                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
                    {
                        Acc_Card ac = new Acc_Card(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                        ac.MdiParent = ParentForm;
                        ac.Show();
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

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                {
                    if ((dgvr.Cells[5].Value == null || dgvr.Cells[5] == null) && !dgvr.IsNewRow)
                        dgvr.Cells[5].Value = "0.0000";

                    if ((dgvr.Cells[6].Value == null || dgvr.Cells[6] == null) && !dgvr.IsNewRow)
                        dgvr.Cells[6].Value = "0.0000";

                    if ((dgvr.Cells[7].Value == null || dgvr.Cells[7] == null) && !dgvr.IsNewRow)
                        dgvr.Cells[7].Value = "0.0000";

                    if ((dgvr.Cells[10].Value == null || dgvr.Cells[10] == null) && !dgvr.IsNewRow)
                        dgvr.Cells[10].Value = "0.0000";

                    if ((dgvr.Cells[11].Value == null || dgvr.Cells[11] == null) && !dgvr.IsNewRow)
                        dgvr.Cells[11].Value = "";

                    if ((dgvr.Cells[12].Value == null || dgvr.Cells[12] == null) && !dgvr.IsNewRow)
                        dgvr.Cells[12].Value = "";
                }

                if (dataGridView1.CurrentCell == dataGridView1[5, dataGridView1.CurrentRow.Index])
                {
                    if (string.IsNullOrEmpty(dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString()))
                        dataGridView1[5, dataGridView1.CurrentRow.Index].Value = 0;
                }
                if (dataGridView1.CurrentCell == dataGridView1[6, dataGridView1.CurrentRow.Index])
                {
                    if (string.IsNullOrEmpty(dataGridView1[6, dataGridView1.CurrentRow.Index].Value.ToString()))
                        dataGridView1[6, dataGridView1.CurrentRow.Index].Value = 0;
                }
                if (dataGridView1.CurrentCell == dataGridView1[7, dataGridView1.CurrentRow.Index])
                {
                    if (string.IsNullOrEmpty(dataGridView1[7, dataGridView1.CurrentRow.Index].Value.ToString()))
                        dataGridView1[7, dataGridView1.CurrentRow.Index].Value = 0;
                }
                if (dataGridView1.CurrentCell == dataGridView1[10, dataGridView1.CurrentRow.Index])
                {
                    if (string.IsNullOrEmpty(dataGridView1[10, dataGridView1.CurrentRow.Index].Value.ToString()))
                        dataGridView1[10, dataGridView1.CurrentRow.Index].Value = 0;
                }
                if (dataGridView1.CurrentCell == dataGridView1[11, dataGridView1.CurrentRow.Index])
                {
                    if (string.IsNullOrEmpty(dataGridView1[11, dataGridView1.CurrentRow.Index].Value.ToString()))
                        dataGridView1[11, dataGridView1.CurrentRow.Index].Value = "";
                }
                if (dataGridView1.CurrentCell == dataGridView1[12, dataGridView1.CurrentRow.Index])
                {
                    if (string.IsNullOrEmpty(dataGridView1[12, dataGridView1.CurrentRow.Index].Value.ToString()))
                        dataGridView1[12, dataGridView1.CurrentRow.Index].Value = "";
                }
                //SqlConnection con = new SqlConnection(@"Data Source=RYD1-PC\INFOSOFT12;" +
                //                                      "Initial Catalog=DBASE;" +
                //                                      "User id=sa;" +
                //                                      "Password=infocic;");
                //if (e.KeyCode == Keys.Enter)
                //{
                //    // var selectedCell = dataGridView1.SelectedCells[0];

                //    dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];

                //}

                if (dataGridView1.CurrentCell == dataGridView1[7, dataGridView1.CurrentRow.Index])
                {
                    if (Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value) > 0)
                    {
                        dataGridView1.CurrentRow.Cells[6].Value = 0;
                        dataGridView1.CurrentRow.Cells[10].Value = (Convert.ToDouble(BL.CLS_Session.tax_per) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)) / (100 + Convert.ToDouble(BL.CLS_Session.tax_per));
                        dataGridView1.CurrentRow.Cells[5].Value = (100 * Convert.ToDouble(dataGridView1.CurrentRow.Cells[10].Value)) / Convert.ToDouble(BL.CLS_Session.tax_per);
                        // dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];
                    }
                    else
                    {
                        dataGridView1.CurrentRow.Cells[5].Value = 0;
                        dataGridView1.CurrentRow.Cells[10].Value = (Convert.ToDouble(BL.CLS_Session.tax_per) * (Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value) * -1)) / (100 + Convert.ToDouble(BL.CLS_Session.tax_per));
                        dataGridView1.CurrentRow.Cells[6].Value = (100 * Convert.ToDouble(dataGridView1.CurrentRow.Cells[10].Value)) / Convert.ToDouble(BL.CLS_Session.tax_per);
                        dataGridView1.CurrentRow.Cells[7].Value = Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value) * -1;
                    }
                }


                if (dataGridView1.CurrentCell == dataGridView1[10, dataGridView1.CurrentRow.Index])
                {
                    if (Convert.ToDouble(dataGridView1.CurrentRow.Cells[10].Value) > 0)
                    {
                        dataGridView1.CurrentRow.Cells[6].Value = 0;
                        dataGridView1.CurrentRow.Cells[7].Value = ((Convert.ToDouble(BL.CLS_Session.tax_per) + 100) * Convert.ToDouble(dataGridView1.CurrentRow.Cells[10].Value)) / (Convert.ToDouble(BL.CLS_Session.tax_per));
                        dataGridView1.CurrentRow.Cells[5].Value = (100 * Convert.ToDouble(dataGridView1.CurrentRow.Cells[10].Value)) / Convert.ToDouble(BL.CLS_Session.tax_per);
                        // dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];
                    }
                    else
                    {
                        dataGridView1.CurrentRow.Cells[5].Value = 0;
                        dataGridView1.CurrentRow.Cells[7].Value = ((Convert.ToDouble(BL.CLS_Session.tax_per) + 100) * (Convert.ToDouble(dataGridView1.CurrentRow.Cells[10].Value) * -1)) / (Convert.ToDouble(BL.CLS_Session.tax_per));
                        dataGridView1.CurrentRow.Cells[6].Value = (100 * -1 * Convert.ToDouble(dataGridView1.CurrentRow.Cells[10].Value)) / Convert.ToDouble(BL.CLS_Session.tax_per);
                        dataGridView1.CurrentRow.Cells[10].Value = Convert.ToDouble(dataGridView1.CurrentRow.Cells[10].Value) * -1;
                    }
                }
                if (dataGridView1.CurrentCell == dataGridView1[2, dataGridView1.CurrentRow.Index])
                {


                   // DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                   // dcombo.FlatStyle = FlatStyle.Flat;
                    SqlDataAdapter da = new SqlDataAdapter("select top 1 a_key a ,a_name b,acckind acckind from accounts where a_active=1 and accontrol=0 and a_key = '" + dataGridView1.CurrentRow.Cells[2].Value + "' and a_brno='" + BL.CLS_Session.brno + "'" + conkey + "", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // dataGridView1.DataSource = dt;

                    if (dataGridView1.CurrentRow.Cells[2].Value != null && dt.Rows.Count > 0)
                    {
                        dataGridView1.CurrentRow.Cells[0].Value = "";
                        dataGridView1.CurrentRow.Cells[1].Value = "";
                        dataGridView1.CurrentRow.Cells[2].Value = dt.Rows[0][0].ToString();
                        dataGridView1.CurrentRow.Cells[3].Value = dt.Rows[0][1].ToString();
                        // dataGridView1.CurrentRow.Cells[7] = dcombo;
                        dataGridView1.CurrentRow.Cells[8].Value = dt.Rows[0]["acckind"].ToString();

                        if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
                        {
                            dataGridView1.CurrentRow.Cells[4].Value = txt_desc.Text;
                        }

                        dataGridView1.CurrentCell = this.dataGridView1[4, dataGridView1.CurrentRow.Index];
                        // SendKeys.Send("{right}");
                        //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index-1];
                        /*
                        if (dt.Rows[0]["acckind"].ToString().Equals("2"))
                        {

                            DataView dv1 = dttaxcats.DefaultView;
                            dv1.RowFilter = "In_out_tax in('" + dataGridView1.CurrentRow.Cells[8].Value + "')";
                            DataTable dtNew = dv1.ToTable();
                            dcombo.DataSource = dtNew;
                            dcombo.DisplayMember = "taxcat_name";
                            dcombo.ValueMember = "taxcatID";

                            dataGridView1.CurrentRow.Cells[7] = dcombo;
                            // dataGridView1.CurrentRow.Cells[7].Value = "5";
                            // dcombo.FlatStyle = FlatStyle.Flat;
                            //  dataGridView1.CurrentRow.Cells[7].Value = "5";

                        }
                        else
                        {
                            if (dt.Rows[0]["acckind"].ToString().Equals("1"))
                            {

                                DataView dv1 = dttaxcats.DefaultView;
                                dv1.RowFilter = "In_out_tax in('" + dataGridView1.CurrentRow.Cells[8].Value + "')";
                                DataTable dtNew = dv1.ToTable();
                                dcombo.DataSource = dtNew;
                                dcombo.DisplayMember = "taxcat_name";
                                dcombo.ValueMember = "taxcatID";


                                dataGridView1.CurrentRow.Cells[7] = dcombo;
                                // dataGridView1.CurrentRow.Cells[7].Value = "1";
                                // dcombo.FlatStyle = FlatStyle.Flat;
                                //  dataGridView1.CurrentRow.Cells[7].Value = "1";


                            }

                            else
                            {
                                //  dataGridView1.CurrentRow.Cells[5] = dcombo;
                                //  dataGridView1.CurrentRow.Cells[5].Value = "";
                                // dataGridView1.CurrentRow.Cells[7].Value = "";
                                //  dataGridView1.CurrentRow.Cells[8].Value = ""; 

                                //  dcombo.DataSource = null;
                                // dataGridView1.CurrentRow.Cells[7] = null;
                                dataGridView1.CurrentRow.Cells[7] = dcombo;
                                //  dataGridView1.CurrentRow.Cells[7].Value = "";
                                //  dataGridView1.CurrentRow.Cells[7].Value="";
                                //  dcombo.FlatStyle = FlatStyle.Flat;


                                // dataGridView1.CurrentRow.Cells[9].Value = "";
                                // null;
                                //  dcombo.FlatStyle = FlatStyle.Flat;
                            }
                        }*/

                    }
                    else
                    {
                        //  MessageBox.Show("not  found");
                        dataGridView1.CurrentRow.Cells[1].Value = "";
                        dataGridView1.CurrentRow.Cells[2].Value = "";
                        dataGridView1.CurrentRow.Cells[3].Value = "";
                        dataGridView1.CurrentRow.Cells[0].Value = "";

                       // dataGridView1.CurrentRow.Cells[7] = dcombo;
                        // dataGridView1.CurrentRow.Cells[7].Value = "";
                        // dataGridView1.CurrentRow.Cells[7].Value = "";


                        //  dataGridView1.CurrentRow.Cells[8].Value = "";
                        //dataGridView1.CurrentRow.Cells[7].Value = null;
                        //dataGridView1.CurrentRow.Cells[9].Value = "";
                        //dataGridView1.CurrentRow.Cells[7].Value = null;
                        //dataGridView1.CurrentRow.Cells[8].Value = 0;
                        //dataGridView1.CurrentRow.Cells[9].Value = "";
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

                if (dataGridView1.CurrentCell == dataGridView1[0, dataGridView1.CurrentRow.Index])
                {

                    SqlDataAdapter da2 = new SqlDataAdapter("select top 1 cast(cu_no as varchar) cu_no ,cu_name ,cl_acc from customers a join cuclass b on a.cu_class=b.cl_no where a.inactive=0 and a.cu_no = '" + dataGridView1.CurrentRow.Cells[0].Value + "' and a.cu_brno='" + BL.CLS_Session.brno + "'", con);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);

                    // dataGridView1.DataSource = dt;
                    if (dataGridView1.CurrentRow.Cells[0].Value != null && dt2.Rows.Count > 0)
                    {
                        dataGridView1.CurrentRow.Cells[1].Value = "";
                        dataGridView1.CurrentRow.Cells[2].Value = "";

                        dataGridView1.CurrentRow.Cells[0].Value = dt2.Rows[0][0].ToString();
                        dataGridView1.CurrentRow.Cells[3].Value = dt2.Rows[0][1].ToString();
                        dataGridView1.CurrentRow.Cells[8].Value = "0";
                        dataGridView1.CurrentRow.Cells[9].Value = dt2.Rows[0][2].ToString();

                        if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
                        {
                            dataGridView1.CurrentRow.Cells[4].Value = txt_desc.Text;
                        }

                        dataGridView1.CurrentCell = this.dataGridView1[4, dataGridView1.CurrentRow.Index];
                        // SendKeys.Send("{right}");
                        //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index-1];

                    }
                    else
                    {
                        //  MessageBox.Show("not  found");
                        dataGridView1.CurrentRow.Cells[1].Value = "";
                        dataGridView1.CurrentRow.Cells[2].Value = "";
                        dataGridView1.CurrentRow.Cells[3].Value = "";
                        dataGridView1.CurrentRow.Cells[0].Value = "";
                       // dataGridView1.CurrentRow.Cells[7].Value = "";
                        //  dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.CurrentRow.Index - 1];
                        //  SendKeys.Send("{UP}");
                        //  SendKeys.Send("{right}");


                    }
                }

                if (dataGridView1.CurrentCell == dataGridView1[12, dataGridView1.CurrentRow.Index])
                {

                    SqlDataAdapter da2 = new SqlDataAdapter("select top 1 cc_id from costcenters where cc_id='" + dataGridView1.CurrentRow.Cells[12].Value + "' and cc_brno='" + BL.CLS_Session.brno + "'", con);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);

                    // dataGridView1.DataSource = dt;
                    if (dataGridView1.CurrentRow.Cells[12].Value != null && dt2.Rows.Count > 0)
                    {
                        dataGridView1.CurrentRow.Cells[12].Value = dt2.Rows[0][0].ToString();

                       // dataGridView1.CurrentCell = this.dataGridView1[4, dataGridView1.CurrentRow.Index];
                        // SendKeys.Send("{right}");
                        //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index-1];

                    }
                    else
                    {
                        dataGridView1.CurrentRow.Cells[12].Value = "";
                    }
                }

                if (dataGridView1.CurrentCell == dataGridView1[1, dataGridView1.CurrentRow.Index])
                {

                    SqlDataAdapter da2 = new SqlDataAdapter("select top 1 cast(su_no as varchar) su_no ,su_name ,cl_acc from suppliers a join suclass b on a.su_class=b.cl_no where a.inactive=0 and a.su_no = '" + dataGridView1.CurrentRow.Cells[1].Value + "' and a.su_brno='" + BL.CLS_Session.brno + "'", con);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);

                    // dataGridView1.DataSource = dt;
                    if (dataGridView1.CurrentRow.Cells[1].Value != null && dt2.Rows.Count > 0)
                    {
                        dataGridView1.CurrentRow.Cells[0].Value = "";
                        dataGridView1.CurrentRow.Cells[2].Value = "";

                        dataGridView1.CurrentRow.Cells[1].Value = dt2.Rows[0][0].ToString();
                        dataGridView1.CurrentRow.Cells[3].Value = dt2.Rows[0][1].ToString();
                        dataGridView1.CurrentRow.Cells[8].Value = "0";
                        dataGridView1.CurrentRow.Cells[9].Value = dt2.Rows[0][2].ToString();

                        if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
                        {
                            dataGridView1.CurrentRow.Cells[4].Value = txt_desc.Text;
                        }

                        dataGridView1.CurrentCell = this.dataGridView1[4, dataGridView1.CurrentRow.Index];
                        // SendKeys.Send("{right}");
                        //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index-1];

                    }
                    else
                    {
                        //  MessageBox.Show("not  found");
                        dataGridView1.CurrentRow.Cells[0].Value = "";
                        dataGridView1.CurrentRow.Cells[2].Value = "";
                        dataGridView1.CurrentRow.Cells[1].Value = "";
                        dataGridView1.CurrentRow.Cells[3].Value = "";
                       // dataGridView1.CurrentRow.Cells[7].Value = "";
                        //  dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.CurrentRow.Index - 1];
                        //  SendKeys.Send("{UP}");
                        //  SendKeys.Send("{right}");


                    }
                }
            }
            catch { }
            //try
            //{
            //    if (e.ColumnIndex == dataGridView1.Columns.Count - 1)
            //    {
            //        dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells[0];
            //    }
            //    else
            //    {
            //        SendKeys.Send("{UP}");
            //        SendKeys.Send("{left}");
            //    }
            //}
            //catch { }
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
        }

        private void Acc_Tran_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.TsIcon;
             acc =!string.IsNullOrEmpty(BL.CLS_Session.acckey) ? BL.CLS_Session.acckey.Replace(" ", "','").Remove(0, 2) + "'" : " ";
             conkey = !string.IsNullOrEmpty(BL.CLS_Session.acckey) ? " and a_key in(" + acc + ")" : " ";

             dttaxcats = daml.SELECT_QUIRY_only_retrn_dt("select  cast([taxcatID] as varchar) taxcatID,[taxcat_name],[taxcat_lname],[cat_protected],[lastupdt],[sysname],[In_out_tax],[txdsc_allowed] from taxcats");
           // MessageBox.Show(dtv.convert_to_ddDDyyyy("20180526"));
           // dataGridView1.Columns["Column2"].ReadOnly = true;
           // dtg = dataGridView1.DataSource;
            //dataGridView1.Columns[5].ReadOnly = true;
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

                var items = new[] { new { Text = "قيد عام", Value = "01" }, new { Text = "سند قبض", Value = "02" }, new { Text = "سند صرف", Value = "03" } };
                comboBox1.DataSource = items;
                comboBox1.SelectedIndex = -1;
                */
                string tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
                // MessageBox.Show(tr);
               // dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('01','02','03')");
                dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no <= 21 ");

                comboBox1.DataSource = dttrtyps;
                comboBox1.DisplayMember = BL.CLS_Session.lang.Equals("E") ? "tr_ename" : "tr_name";
                comboBox1.ValueMember = "tr_no";
                comboBox1.SelectedIndex = -1;


                comboBox1.Enabled = false;
                txt_ref.Enabled = false;
                txt_hdate.Enabled = false;
                txt_mdate.Enabled = false;
                txt_desc.Enabled = false;
                txt_ccno.Enabled = false;
                txt_taxid.Enabled = false;
                txt_invsupno.Enabled = false;
                txt_bookno.Enabled = false;
                txt_amt.Enabled = false;
                dataGridView1.ReadOnly = true;

                Fill_Data(a_type, a_ref, a_condi);
                //  dataGridView1.AllowUserToAddRows = false;
                //dataGridView1.Select();
                // dataGridView1.BeginEdit(true);
                // dataGridView1.Rows.Add(10);
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();

                foreach (DataRow dtr in BL.CLS_Session.dtdescs.Rows)
                {
                    MyCollection.Add(BL.CLS_Session.lang.Equals("E") ? dtr[2].ToString() : dtr[1].ToString());
                }
                txt_desc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txt_desc.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txt_desc.AutoCompleteCustomSource = MyCollection;
                // this.Owner.Enabled = false;

                //  Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                string str2 = BL.CLS_Session.formkey;
                string whatever = str2.Substring(str2.IndexOf("A125") + 5, 3);
                if (whatever.Substring(0, 1).Equals("0"))
                    btn_Add.Visible = false;
                if (whatever.Substring(1, 1).Equals("0"))
                    btn_Edit.Visible = false;
                if (whatever.Substring(2, 1).Equals("0"))
                    btn_Del.Visible = false;

                if (BL.CLS_Session.up_us_post==false)
                    btn_Post.Visible = false;
               // timer1.Start();

               // btn_Rfrsh_Click(sender, e);
            }
            catch (Exception ex) {// MessageBox.Show(ex.Message); 
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Cells[4].Value == null)
                {
                    dataGridView1.CurrentRow.Cells[4].Value = txt_desc.Text;
                }

                //   if (dataGridView1.CurrentCell == dataGridView1[4, dataGridView1.CurrentRow.Index] && Convert.ToInt32(dataGridView1.CurrentCell.Value) > 0)
                if (dataGridView1.CurrentCell == dataGridView1[6, dataGridView1.CurrentRow.Index])
                {
                    dataGridView1.CurrentRow.Cells[5].Value = 0;
                    dataGridView1.CurrentRow.Cells[7].Value = 0;
                    dataGridView1.CurrentRow.Cells[10].Value = 0;
                    //  dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];


                }

                // if (dataGridView1.CurrentCell == dataGridView1[3, dataGridView1.CurrentRow.Index] && Convert.ToInt32(dataGridView1.CurrentCell.Value) > 0)
                if (dataGridView1.CurrentCell == dataGridView1[5, dataGridView1.CurrentRow.Index])
                {
                    dataGridView1.CurrentRow.Cells[6].Value = 0;
                    dataGridView1.CurrentRow.Cells[7].Value = 0;
                    dataGridView1.CurrentRow.Cells[10].Value = 0;
                    // dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];

                }

                

                ////if (dataGridView1.CurrentCell == dataGridView1[0, dataGridView1.CurrentRow.Index])
                ////{
                ////    dataGridView1.CurrentRow.Cells[1].Value = "";
                ////    dataGridView1.CurrentRow.Cells[2].Value = "";
                ////    // dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];

                ////}

                ////if (dataGridView1.CurrentCell == dataGridView1[1, dataGridView1.CurrentRow.Index])
                ////{
                ////    dataGridView1.CurrentRow.Cells[0].Value = "";
                ////    dataGridView1.CurrentRow.Cells[2].Value = "";
                ////    // dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];

                ////}
                ////if (dataGridView1.CurrentCell == dataGridView1[2, dataGridView1.CurrentRow.Index])
                ////{
                ////    dataGridView1.CurrentRow.Cells[0].Value = "";
                ////    dataGridView1.CurrentRow.Cells[1].Value = "";
                ////    // dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];

                ////}
            }
            catch { }
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
            comboBox1.SelectedIndex = 0;
            // dthdr.Rows.Clear();
           // dtdtl.Rows.Clear();
            btn_Rfrsh.Enabled = false;
            isnew = true;
            isupdate = false;
            btn_Save.Enabled = true;
            btn_Add.Enabled = false;
            btn_Undo.Enabled = true;
            btn_Exit.Enabled = false;
            btn_Find.Enabled = false;
            btn_Del.Enabled = false;
            btn_Edit.Enabled = false;
            btn_Post.Enabled = false;
            txt_user.Text = BL.CLS_Session.UserName;
            comboBox1.Enabled = true;
            btn_Print.Enabled = false;
           // comboBox1.SelectedIndex = 0;
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
            txt_ccno.Enabled = true;
            txt_taxid.Enabled = true;
            txt_invsupno.Enabled = true;
            txt_bookno.Enabled = true;
            txt_amt.Enabled = true;
           // dataGridView1.Enabled = true;
           // dataGridView1.Rows.Add(10);
            dataGridView1.ReadOnly = false;
            dataGridView1.Columns[3].ReadOnly = true;
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
                    ((DataTable)dataGridView1.DataSource).Rows.Clear();
                }
            }
            catch { }
            //try
            //{
            //    if (dataGridView1.Rows.Count > 0)
            //    {
            //        // ((DataTable)dataGridView1.DataSource).Rows.Clear();
            //        dataGridView1.Rows.Clear();
            //    }
            //}
            //catch { }
           
          //  dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToAddRows = true;

            comboBox1.Focus();
            txt_ref.Text = "";
            txt_desc.Text="";
            txt_bookno.Text = "";
            txt_amt.Text = "0";
             
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
                    Fill_Data(comboBox1.SelectedValue.ToString(), txt_ref.Text,"");
                    btn_Save.Enabled = false;
                    btn_Add.Enabled = true;
                    btn_Undo.Enabled = false;
                    btn_Exit.Enabled = true;
                    btn_Find.Enabled = true;
                    btn_Edit.Enabled = true;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;
                    comboBox1.Enabled = false;
                    txt_ref.Enabled = false;

                    
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

                    try
                    {
                        if (dataGridView1.Rows.Count > 0)
                        {
                           // ((DataTable)dataGridView1.DataSource).Rows.Clear();
                            dataGridView1.Rows.Clear();
                            dataGridView1.Refresh();
                        }
                    }
                    catch { }

                    dataGridView1.ReadOnly = true;

                    comboBox1.Enabled = false;
                    txt_ref.Enabled = false;
                  //  dataGridView1.AllowUserToAddRows = false;

                    comboBox1.SelectedIndex = -1;
                    txt_amt.Text = "0";
                    txt_desc.Text = "";
                    txt_bookno.Text = "";

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
                txt_ccno.Enabled = false;
                txt_taxid.Enabled = false;
                txt_invsupno.Enabled = false;
                txt_bookno.Enabled = false;
                txt_amt.Enabled = false;
                comboBox1.Enabled = false;
                txt_ref.Enabled = false;
                btn_Print.Enabled = true;
                btn_Rfrsh.Enabled = true;

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
            total();
            ////foreach (DataGridViewRow row2 in dataGridView1.Rows)
            ////{
            ////    if (!row2.Cells[8].Value.Equals("0") && (row2.Cells[7].Value.ToString().Equals("") ||  row2.Cells[7].Value==null))
            ////    {
            ////        MessageBox.Show("يرجى اختيار نوع الضريبة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////        dataGridView1.CurrentCell = dataGridView1[7, row2.Index];
            ////        return;
            ////    }
            ////}

            //try
            //{
            if (Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) > Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.end_dt)) || Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) < Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.start_dt)))
            {
                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Date Out of Years" : "لا يمكن ادخال حركة خارج السنة المالية", "Error خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridView1.RowCount <= 1)
            {
                MessageBox.Show("لا يوجد مدخلات", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (DataGridViewRow dgvr in dataGridView1.Rows)
            {
                if (dgvr.Cells[5].Value == null && !dgvr.IsNewRow)
                    dgvr.Cells[5].Value = "0.00";

                if (dgvr.Cells[6].Value == null && !dgvr.IsNewRow)
                    dgvr.Cells[6].Value = "0.00";

                if (dgvr.Cells[7].Value == null && !dgvr.IsNewRow)
                    dgvr.Cells[7].Value = "0.00";

                if (dgvr.Cells[10].Value == null && !dgvr.IsNewRow)
                    dgvr.Cells[10].Value = "0.00";


            }
            if (BL.CLS_Session.mustequal && (Convert.ToDouble(txt_amt.Text) != Convert.ToDouble(txt_damt.Text) || Convert.ToDouble(txt_amt.Text) != Convert.ToDouble(txt_camt.Text) || Convert.ToDouble(txt_damt.Text) != Convert.ToDouble(txt_damt.Text)))
            {
                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Must be equal when save" : "يجب ان يكون القيد متزن عند الحفظ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (DataGridViewRow dgvr in dataGridView1.Rows)
            {

                if (Convert.ToDouble(dgvr.Cells[5].Value) == 0 && Convert.ToDouble(dgvr.Cells[6].Value) == 0 && !dgvr.IsNewRow)
                {
                    MessageBox.Show("لا يمكن ان يكون الدائن والمدين صفر", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView1.CurrentCell = dataGridView1[5, dgvr.Index];
                    dataGridView1.Select();
                    return;
                }
            }

            if (BL.CLS_Session.mustcost && string.IsNullOrEmpty(txt_ccno.Text))
            {
                MessageBox.Show("يرجى ادخال مركز التكلفة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_ccno.Focus();
                return;
            }

                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("يرجى اختيار نوع القيد","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }

                string mdate, hdate;
                mdate = txt_mdate.Text.Replace("-", "").Substring(4, 4) + txt_mdate.Text.Replace("-", "").Substring(2, 2) + txt_mdate.Text.Replace("-", "").Substring(0, 2);
                hdate = txt_hdate.Text.Replace("-", "").Substring(4, 4) + txt_hdate.Text.Replace("-", "").Substring(2, 2) + txt_hdate.Text.Replace("-", "").Substring(0, 2);

                DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(a_ref)," + BL.CLS_Session.dtbranch.Rows[0]["jv_" + comboBox1.SelectedValue + ""] + ") from acc_hdr where a_type='" + comboBox1.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "'");

                int ref_max = Convert.ToInt32(dt.Rows[0][0]) + 1;



                if (isnew && string.IsNullOrEmpty(txt_ref.Text))
                {
                    daml.SqlCon_Open();
                    int exexcuted = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_brno,a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,usrid,jhsrc,serial_no,cc_no,taxid,invsupno) VALUES('" + BL.CLS_Session.brno + "','" + comboBox1.SelectedValue + "'," + ref_max + ",'" + mdate + "','" + hdate + "','" + txt_desc.Text + "'," + txt_amt.Text + "," + (dataGridView1.RowCount - 1) + ",'" + txt_user.Text + "','Acct2','" + txt_bookno.Text + "','" + txt_ccno.Text + "','" + txt_taxid.Text + "','" + txt_invsupno.Text + "')", false);
                    daml.SqlCon_Close();
                    if (exexcuted > 0)
                    {
                        txt_ref.Text = ref_max.ToString();
                        txt_ref.Enabled = false;
                        txt_hdate.Enabled = false;
                        txt_mdate.Enabled = false;
                        txt_desc.Enabled = false;
                        txt_ccno.Enabled = false;
                        txt_taxid.Enabled = false;
                        txt_invsupno.Enabled = false;
                        txt_bookno.Enabled = false;
                        txt_amt.Enabled = false;
                      //  dataGridView1.ReadOnly = true;
                        comboBox1.Enabled = false;
                        isnew = false;
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    daml.SqlCon_Open();
                    int exexcuted = daml.Insert_Update_Delete_retrn_int("update acc_hdr set lastupdt=getdate(),a_mdate='" + mdate + "', a_hdate='" + hdate + "',a_text='" + txt_desc.Text + "',a_amt=" + txt_amt.Text + ",a_entries=" + (dataGridView1.RowCount - 1) + ",usrid='" + txt_user.Text + "',serial_no='" + txt_bookno.Text + "',cc_no='" + txt_ccno.Text + "',taxid='" + txt_taxid.Text + "',invsupno='" + txt_invsupno.Text + "' where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "'", false);
                    exexcuted = exexcuted + daml.Insert_Update_Delete_retrn_int("delete from acc_dtl where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "' and sl_no='' and pu_no=''", false);
                    daml.SqlCon_Close();
                    if (exexcuted > 0)
                    {
                      //  txt_ref.Text = ref_max.ToString();
                        txt_ref.Enabled = false;
                        txt_hdate.Enabled = false;
                        txt_mdate.Enabled = false;
                        txt_desc.Enabled = false;
                        txt_ccno.Enabled = false;
                        txt_taxid.Enabled = false;
                        txt_invsupno.Enabled = false;
                        txt_bookno.Enabled = false;
                        txt_amt.Enabled = false;
                      //  dataGridView1.ReadOnly = true;
                    }
                    else
                    {

                    }

                }
                double sumtxd = 0, sumtx = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from DB.dbo.sales_hdr", con2);
                    /*
                    SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from sales_hdr", con2);
                    DataSet dss = new DataSet();
                    daa.Fill(dss, "0");
    */
                  //  if (!row.IsNewRow && row.Cells[0].Value != null)
                    if (!row.IsNewRow && !string.IsNullOrEmpty(row.Cells[0].Value.ToString() + row.Cells[1].Value.ToString() + row.Cells[2].Value.ToString()))
                    {
                        // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text,  a_damt, a_camt,a_folio,a_mdate,a_hdate,jddbcr,taxcatId,su_no,cu_no,cc_no,jdfc_imgval,jdcstval,cstkey) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@su,@cu,@cc,@val,@tax,@taxno)", con))
                        {
                            cmd.Parameters.AddWithValue("@a0", BL.CLS_Session.brno);
                            cmd.Parameters.AddWithValue("@a1", comboBox1.SelectedValue);
                            cmd.Parameters.AddWithValue("@a2", (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)));
                            cmd.Parameters.AddWithValue("@a3", (string.IsNullOrEmpty(row.Cells[0].Value.ToString()) ? (string.IsNullOrEmpty(row.Cells[1].Value.ToString()) ? row.Cells[2].Value : row.Cells[9].Value) : row.Cells[9].Value));
                            cmd.Parameters.AddWithValue("@a4", (string.IsNullOrEmpty(row.Cells[4].Value.ToString())? "" : row.Cells[4].Value.ToString()));
                           // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                            cmd.Parameters.AddWithValue("@a5", row.Cells[5].Value);
                           // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                            cmd.Parameters.AddWithValue("@a6", row.Cells[6].Value);
                            cmd.Parameters.AddWithValue("@a7", row.HeaderCell.Value);
                            cmd.Parameters.AddWithValue("@a8", mdate);
                            cmd.Parameters.AddWithValue("@a9", hdate);
                            cmd.Parameters.AddWithValue("@a10", (Convert.ToDouble(row.Cells[6].Value)>0 ? "C" : "D"));
                           // cmd.Parameters.AddWithValue("@a11", (row.Cells[8].Value.ToString().Equals("0")? 0 : row.Cells[7].Value));
                          //  cmd.Parameters.AddWithValue("@a11", (row.Cells[8].Value.ToString().Equals("0")? 0 : string.IsNullOrEmpty(row.Cells[7].Value.ToString()) ? 0 : row.Cells[7].Value));
                            cmd.Parameters.AddWithValue("@a11",  0);
                            cmd.Parameters.AddWithValue("@su", (string.IsNullOrEmpty(row.Cells[1].Value.ToString()) ? "" : row.Cells[1].Value));
                            cmd.Parameters.AddWithValue("@cu", (string.IsNullOrEmpty(row.Cells[0].Value.ToString()) ? "" : row.Cells[0].Value));
                            cmd.Parameters.AddWithValue("@cc", (string.IsNullOrEmpty(row.Cells[12].ToString()) || string.IsNullOrEmpty(row.Cells[12].Value.ToString())) ? txt_ccno.Text : row.Cells[12].Value);
                            cmd.Parameters.AddWithValue("@val", row.Cells[7].Value);
                            cmd.Parameters.AddWithValue("@tax", row.Cells[10].Value);
                            cmd.Parameters.AddWithValue("@taxno", (string.IsNullOrEmpty(row.Cells[11].ToString()) || string.IsNullOrEmpty(row.Cells[11].Value.ToString())) ? "" : row.Cells[11].Value);
                            //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                            //if (row.Cells[7].Value != null)
                            // { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                            // cmd.Parameters.AddWithValue("@c9", comp_id);
                            // cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                            //con.Open();
                            sumtxd = sumtxd + Convert.ToDouble(row.Cells[7].Value);
                            sumtx = sumtx + Convert.ToDouble(row.Cells[10].Value);
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();
                            //con.Close();
                            //if (con.State == ConnectionState.Open)
                            //{
                            //    con.Close();
                            //}
                            // lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);

                        }
                    }
                }
                if (Convert.ToDouble(txt_ttltxout.Text) > 0)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text,  a_damt, a_camt,a_folio,a_mdate,a_hdate,jddbcr,taxcatId,su_no,cu_no,cc_no,jdfc_imgval,jdcstval,cstkey) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@su,@cu,@cc,@val,@tax,@taxno)", con))
                    {
                        cmd.Parameters.AddWithValue("@a0", BL.CLS_Session.brno);
                        cmd.Parameters.AddWithValue("@a1", comboBox1.SelectedValue);
                        cmd.Parameters.AddWithValue("@a2", (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)));
                        cmd.Parameters.AddWithValue("@a3", BL.CLS_Session.dtbranch.Rows[0]["taxout_acc"].ToString());
                        cmd.Parameters.AddWithValue("@a4", txt_desc.Text);
                        // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                        cmd.Parameters.AddWithValue("@a5", txt_ttltxout.Text);
                        // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                        cmd.Parameters.AddWithValue("@a6", 0);
                        cmd.Parameters.AddWithValue("@a7", dataGridView1.RowCount + 1 );
                        cmd.Parameters.AddWithValue("@a8", mdate);
                        cmd.Parameters.AddWithValue("@a9", hdate);
                        cmd.Parameters.AddWithValue("@a10", "D");
                        // cmd.Parameters.AddWithValue("@a11", (row.Cells[8].Value.ToString().Equals("0")? 0 : row.Cells[7].Value));
                        cmd.Parameters.AddWithValue("@a11", 15);
                        cmd.Parameters.AddWithValue("@su", "");
                        cmd.Parameters.AddWithValue("@cu", "");
                        cmd.Parameters.AddWithValue("@cc", txt_ccno.Text);
                        cmd.Parameters.AddWithValue("@val", sumtxd);
                        cmd.Parameters.AddWithValue("@tax", sumtx);
                        cmd.Parameters.AddWithValue("@taxno", "");
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
                if (Convert.ToDouble(txt_ttltxin.Text) > 0)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text,  a_damt, a_camt,a_folio,a_mdate,a_hdate,jddbcr,taxcatId,su_no,cu_no,cc_no,jdfc_imgval,jdcstval,cstkey) VALUES(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@su,@cu,@cc,@val,@tax,@taxno)", con))
                    {
                        cmd.Parameters.AddWithValue("@a0", BL.CLS_Session.brno);
                        cmd.Parameters.AddWithValue("@a1", comboBox1.SelectedValue);
                        cmd.Parameters.AddWithValue("@a2", (isnew ? ref_max : Convert.ToInt32(txt_ref.Text)));
                        cmd.Parameters.AddWithValue("@a3", BL.CLS_Session.dtbranch.Rows[0]["taxin_acc"].ToString());
                        cmd.Parameters.AddWithValue("@a4", txt_desc.Text);
                        // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                        cmd.Parameters.AddWithValue("@a5", 0);
                        // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                        cmd.Parameters.AddWithValue("@a6", txt_ttltxin.Text);
                        cmd.Parameters.AddWithValue("@a7", dataGridView1.RowCount + 2);
                        cmd.Parameters.AddWithValue("@a8", mdate);
                        cmd.Parameters.AddWithValue("@a9", hdate);
                        cmd.Parameters.AddWithValue("@a10", "C");
                        // cmd.Parameters.AddWithValue("@a11", (row.Cells[8].Value.ToString().Equals("0")? 0 : row.Cells[7].Value));
                        cmd.Parameters.AddWithValue("@a11", 15);
                        cmd.Parameters.AddWithValue("@su", "");
                        cmd.Parameters.AddWithValue("@cu", "");
                        cmd.Parameters.AddWithValue("@cc", txt_ccno.Text);
                        cmd.Parameters.AddWithValue("@val", sumtxd);
                        cmd.Parameters.AddWithValue("@tax", sumtx);
                        cmd.Parameters.AddWithValue("@taxno", "");
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


                btn_Save.Enabled = false;
                btn_Add.Enabled = true;
                btn_Exit.Enabled = true;
                btn_Find.Enabled = true;
                btn_Undo.Enabled = false;
                btn_Del.Enabled = true;
                btn_Edit.Enabled = true;
                btn_Print.Enabled = true;
                if (notposted)
                { btn_Post.Enabled = true; }
                isnew = false;
                isupdate = false;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ReadOnly = true;
                btn_Rfrsh.Enabled = true;
                btn_Rfrsh_Click(sender, e); //to print

            //}
            //catch (Exception ex)
            //{ MessageBox.Show(ex.Message); }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (txt_user.Text != BL.CLS_Session.UserName && BL.CLS_Session.up_edit_othr == false)
            {
                MessageBox.Show("لا تملك صلاحية تعديل حركة شخص اخر", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (!dthdr.Rows[0][8].ToString().Equals("Acct2") && !dthdr.Rows[0][8].ToString().Equals("Acc1"))
                {
                    MessageBox.Show("لا يمكن تعديل القيد من هذه الشاشة","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;

                }

                btn_Rfrsh_Click(sender, e);
                if (btn_Post.Enabled == true)
                {
                    btn_Rfrsh.Enabled = false;
                    notposted = true;
                    isnew = false;
                    isupdate = true;
                    btn_Save.Enabled = true;
                    btn_Add.Enabled = false;
                    btn_Undo.Enabled = true;
                    btn_Exit.Enabled = false;
                    btn_Find.Enabled = false;
                    btn_Del.Enabled = false;
                    btn_Edit.Enabled = false;
                    btn_Post.Enabled = false;
                    btn_Print.Enabled = false;
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
                    txt_ccno.Enabled = true;
                    txt_taxid.Enabled = true;
                    txt_invsupno.Enabled = true;
                    txt_bookno.Enabled = true;
                    txt_amt.Enabled = true;
                    txt_user.Text = BL.CLS_Session.UserName;
                    // dataGridView1.Enabled = true;
                    // dataGridView1.Rows.Add(10);
                    dataGridView1.ReadOnly = false;
                    dataGridView1.Columns[3].ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = true;
                }
                else
                {
                    MessageBox.Show("لا يمكن تعديل حركة مرحلة", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            btn_Rfrsh_Click(sender, e);
            if (!dthdr.Rows[0][8].ToString().Equals("Acct2"))
            {
                MessageBox.Show("لا يمكن حذف القيد من هذه الشاشة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (btn_Post.Enabled == true)
            {
            DialogResult result = MessageBox.Show("هل تريد المسح", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
               
                try
                {
                    daml.SqlCon_Open();
                    ////if (dthdr.Rows[0][8].ToString().Equals("SL"))
                    ////{
                    ////    daml.Insert_Update_Delete_retrn_int("update sales_hdr set gen_ref=0 where gen_ref=" + txt_ref.Text, false);

                    ////}
                    int exexcuted = daml.Insert_Update_Delete_retrn_int("delete from acc_hdr where a_posted=0 and a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "' and sl_no='' and pu_no=''", false);
                    // exexcuted = exexcuted + daml.Insert_Update_Delete("delete from acc_dtl where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "'", false);
                    daml.SqlCon_Close();

                    if (exexcuted > 0)
                    {
                        MessageBox.Show("تم الحذف بنجاح", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        btn_Del.Enabled = false;
                      
                        if (dataGridView1.Rows.Count > 0)
                          {
                             ((DataTable)dataGridView1.DataSource).Rows.Clear();
                          }
                        txt_ref.Text = "";
                        txt_desc.Text = "";
                        txt_bookno.Text = "";
                        txt_amt.Text = "";
                        comboBox1.SelectedIndex = -1;

                      
                    }
                    else
                    {
                        MessageBox.Show("لم يتم الحذف", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch //(Exception ex)
                {
                   // MessageBox.Show(ex.Message);
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
                var sr = new Search_All("3", "Acct2");
                sr.ShowDialog();

                Fill_Data(sr.dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), sr.dataGridView1.CurrentRow.Cells[1].Value.ToString(),"");
            }
            catch { }
        }

        private void Fill_Data(string atype, string aref, string acondi)
        {
            string condit = "";
            try
            {
                if (atype.Equals("04") || atype.Equals("05") || atype.Equals("14") || atype.Equals("15"))
                {
                    condit = "sl_no ='" + acondi + "'";
                }
                else if (atype.Equals("06") || atype.Equals("07") || atype.Equals("16") || atype.Equals("17"))
                {
                    condit = "pu_no ='" + acondi + "'";
                }
                else
                {
                    condit = "sl_no='' and pu_no=''";
                }
              //  string mdate, hdate;
               // mdate = txt_mdate.Text.Replace("-", "").Substring(4, 4) + txt_mdate.Text.Replace("-", "").Substring(2, 2) + txt_mdate.Text.Replace("-", "").Substring(0, 2);
              //  hdate = txt_hdate.Text.Replace("-", "").Substring(4, 4) + txt_hdate.Text.Replace("-", "").Substring(2, 2) + txt_hdate.Text.Replace("-", "").Substring(0, 2);


                dthdr = daml.SELECT_QUIRY_only_retrn_dt("select * from acc_hdr where a_type='" + atype + "' and a_ref=" + aref + " and a_brno='" + BL.CLS_Session.brno + "' and " + condit + " ");
                dtdtl = daml.SELECT_QUIRY_only_retrn_dt("select cast(aa.cu_no as varchar) cust,cast(aa.su_no as varchar) sup,cast(aa.a_key as varchar) a_key,bb.a_name a_name,aa.a_text a_text,aa.a_damt a_damt,aa.a_camt a_camt,aa.[jdfc_imgval] taxcatId,bb.acckind acckind,(case when aa.cu_no <> '' then aa.a_key when aa.su_no <> '' then aa.a_key else '' end) acckey,aa.[jdcstval] tax,aa.[cstkey] taxno,aa.cc_no from acc_dtl aa,accounts bb where aa.a_key=bb.a_key and aa.a_type='" + atype + "' and aa.a_ref=" + aref + " and aa.a_key not in ('" + BL.CLS_Session.dtbranch.Rows[0]["taxout_acc"].ToString() + "','" + BL.CLS_Session.dtbranch.Rows[0]["taxin_acc"].ToString() + "') and aa.a_brno='" + BL.CLS_Session.brno + "'and aa." + condit + " order by aa.a_folio");

                comboBox1.SelectedValue=dthdr.Rows[0][1];
                txt_ref.Text =isnew?"": dthdr.Rows[0][2].ToString();
                txt_mdate.Text = dthdr.Rows[0][3].ToString().Substring(6, 2) + dthdr.Rows[0][3].ToString().Substring(4, 2) + dthdr.Rows[0][3].ToString().Substring(0, 4);
                txt_hdate.Text = dthdr.Rows[0][4].ToString().Substring(6, 2) + dthdr.Rows[0][4].ToString().Substring(4, 2) + dthdr.Rows[0][4].ToString().Substring(0, 4);
                txt_desc.Text = dthdr.Rows[0][5].ToString();
                txt_amt.Text = dthdr.Rows[0][6].ToString();
                txt_user.Text = dthdr.Rows[0]["usrid"].ToString();
                txt_bookno.Text = dthdr.Rows[0]["serial_no"].ToString();
                txt_ccno.Text = dthdr.Rows[0]["cc_no"].ToString();
                txt_ccno_Leave(null, null);
                txt_taxid.Text = dthdr.Rows[0]["taxid"].ToString();
                txt_invsupno.Text = dthdr.Rows[0]["invsupno"].ToString();

                if (Convert.ToBoolean(dthdr.Rows[0][14]) == true)
                    btn_Post.Enabled = false;
                else 
                    btn_Post.Enabled = true;

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

                //dataGridView1.Rows.Clear();
                //foreach (DataRow dr in dtdtl.Rows)
                //{

                //    dataGridView1.Rows.Add(dr.ItemArray);

                //}
                dataGridView1.DataSource = dtdtl;
                //dataGridView1.DataSource = dataGridView1.DataSource;
                //dthdr.Rows.Clear();
                //dtdtl.Rows.Clear();

                int rowNumber = 1;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    // dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                    if (r.IsNewRow) continue;
                    //r.HeaderCell.Value = (e.RowIndex + 1).ToString();
                    r.HeaderCell.Value = rowNumber.ToString();
                    rowNumber = rowNumber + 1;

                    DataTable dtc = daml.SELECT_QUIRY_only_retrn_dt("select cu_name  from customers where cu_no='" + r.Cells[0].Value + "' and cu_brno='" + BL.CLS_Session.brno + "'");

                    if (dtc.Rows.Count > 0)
                    {
                        r.Cells[2].Value = "";
                        r.Cells[3].Value = dtc.Rows[0][0];
                    }
                    DataTable dts = daml.SELECT_QUIRY_only_retrn_dt("select su_name  from suppliers where su_no='" + r.Cells[1].Value + "' and su_brno='" + BL.CLS_Session.brno + "'");

                    if (dts.Rows.Count > 0)
                    {
                        r.Cells[2].Value = "";
                        r.Cells[3].Value = dts.Rows[0][0];
                    }
                    //string dvc = r.Cells[5].Value.ToString();
                    //  dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where rtrim(ltrim(item_no))='" + r.Cells[0].Value + "'");
                    /*
                    DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                    //  r.Cells[2] = dcombo;
                    DataView dv1 = dttaxcats.DefaultView;
                    //  dv1.RowFilter = "unit_id in('" + dt222.Rows[0][5] + "','" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
                    dv1.RowFilter = "taxcatID in('" + r.Cells[5].Value + "')";
                    DataTable dtNew = dv1.ToTable();
                    dcombo.DataSource = dtNew;
                    dcombo.DisplayMember = "taxcat_name";
                    dcombo.ValueMember = "taxcatID";

                    r.Cells[5] = dcombo;
                    r.Cells[5].Value = dtNew.Rows[0][0];

                    //  dataGridView1[2, r.Index] = dcombo;
                    // dataGridView1[2, r.Index].Value = dtNew.Rows[0][1];
                    // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                    dcombo.FlatStyle = FlatStyle.Flat;
                    */
                    /*
                    DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                    dcombo.FlatStyle = FlatStyle.Flat;
                    if (r.Cells[8].Value.ToString().Equals("2"))
                    {

                        DataView dv1 = dttaxcats.DefaultView;
                        dv1.RowFilter = "In_out_tax in('" + r.Cells[8].Value + "')";
                        DataTable dtNew = dv1.ToTable();
                        dcombo.DataSource = dtNew;
                        dcombo.DisplayMember = "taxcat_name";
                        dcombo.ValueMember = "taxcatID";

                        r.Cells[7] = dcombo;
                       // r.Cells[5].Value = r.Cells[5].Value;
                       // dcombo.FlatStyle = FlatStyle.Flat;
                    }
                    else
                    {
                        if (r.Cells[8].Value.ToString().Equals("1"))
                        {

                            DataView dv1 = dttaxcats.DefaultView;
                            dv1.RowFilter = "In_out_tax in('" + r.Cells[8].Value + "')";
                            DataTable dtNew = dv1.ToTable();
                            dcombo.DataSource = dtNew;
                            dcombo.DisplayMember = "taxcat_name";
                            dcombo.ValueMember = "taxcatID";

                            r.Cells[7] = dcombo;
                           // r.Cells[5].Value = r.Cells[5].Value;
                           // dcombo.FlatStyle = FlatStyle.Flat;
                        }

                        else
                        {
                           // if (r.Cells[6].Value.ToString().Equals("0"))
                            r.Cells[7] = dcombo;
                            // dataGridView1.CurrentRow.Cells[5] = dcombo;
                           ////////////////////////////////////////////////// r.Cells[5].Value = 0;

                            // dcombo.FlatStyle = FlatStyle.Flat;
                        }
                    }*/
                }
                /*
                  DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                  if (dataGridView1.CurrentRow.Cells[6].Value.ToString().Equals("2"))
                  {

                      DataView dv1 = dttaxcats.DefaultView;
                      dv1.RowFilter = "In_out_tax in('" + dataGridView1.CurrentRow.Cells[6].Value + "')";
                      DataTable dtNew = dv1.ToTable();
                      dcombo.DataSource = dtNew;
                      dcombo.DisplayMember = "taxcat_name";
                      dcombo.ValueMember = "taxcatID";

                      dataGridView1.CurrentRow.Cells[5] = dcombo;
                      dcombo.FlatStyle = FlatStyle.Flat;
                  }
                  else
                  {
                      if (dataGridView1.CurrentRow.Cells[6].Value.ToString().Equals("1"))
                      {

                          DataView dv1 = dttaxcats.DefaultView;
                          dv1.RowFilter = "In_out_tax in('" + dataGridView1.CurrentRow.Cells[6].Value + "')";
                          DataTable dtNew = dv1.ToTable();
                          dcombo.DataSource = dtNew;
                          dcombo.DisplayMember = "taxcat_name";
                          dcombo.ValueMember = "taxcatID";

                          dataGridView1.CurrentRow.Cells[5] = dcombo;
                          dcombo.FlatStyle = FlatStyle.Flat;
                      }

                      else
                      {
                          // dataGridView1.CurrentRow.Cells[5] = dcombo;
                          dataGridView1.CurrentRow.Cells[5].Value = "";
                          // dcombo.FlatStyle = FlatStyle.Flat;
                      }
                  }
                 */
                check_system(dthdr.Rows[0][8].ToString()); 
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                var fsn = new Search_by_No("01");

                fsn.comboBox1.DataSource = dttrtyps;
                fsn.comboBox1.DisplayMember = "tr_name";
                fsn.comboBox1.ValueMember = "tr_no";

                fsn.ShowDialog();

                Fill_Data(fsn.comboBox1.SelectedValue.ToString(), fsn.textBox1.Text,"");
              //  check_system(dthdr.Rows[0][8].ToString());
            }
            catch { }
          //  dataGridView1.Enabled = true;
        }

        private void check_system(string tochek)
        {
            if (tochek.StartsWith("Sal"))
                txt_system.Text = "المبيعات";

            if (tochek.StartsWith("Acc"))
                txt_system.Text = "الحسابات";

            if (tochek.StartsWith("Cus"))
                txt_system.Text = "العملاء";

            if (tochek.StartsWith("Sup"))
                txt_system.Text = "الموردين";

            if (tochek.StartsWith("Pos"))
                txt_system.Text = "نقاط البيع";

            if (tochek.StartsWith("Pur"))
                txt_system.Text = "المشتريات";
        }
        private void btn_Post_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txt_damt.Text) != Convert.ToDouble(txt_amt.Text) || Convert.ToDouble(txt_camt.Text) != Convert.ToDouble(txt_amt.Text))
            {
                MessageBox.Show("لا يمكن ترحيل حركة غير متزنة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
                {
                    daml.SqlCon_Open();
                    int exexcuted = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_posted=1 where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "' and a_brno='" + BL.CLS_Session.brno + "'", false);
                    // exexcuted = exexcuted + daml.Insert_Update_Delete("delete from acc_dtl where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "'", false);
                    daml.SqlCon_Close();

                    if (exexcuted > 0)
                    {
                        MessageBox.Show("تم الترحيل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        btn_Post.Enabled = false;
                    }
                    else
                    {
                         MessageBox.Show("فشل في الترحيل", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            catch { }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {/*
            try
            {
                // dataGridView1_CellDoubleClick( null,  null);
                Acc_Statment_Exp f4a = new Acc_Statment_Exp(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                f4a.MdiParent = ParentForm;
                f4a.Show();
            }
            catch { }
          * */
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


            }

          //  btn_Rfrsh_Click( sender,  e);
      
           //     Fill_Data(comboBox1.SelectedValue.ToString(), txt_ref.Text);
          
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                //System.Windows.Forms.Control.SelectNextControl();
                //   SendKeys.Send("{TAB}");
                if (txt_mdate.Enabled == true)
                {
                    txt_mdate.Focus();
                    // txt_mdate.Select();
                    //  txt_mdate.SelectionStart = 0;
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
            }
        }

        private void txt_hdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_bookno.Focus();
            }
        }

        private void txt_desc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_amt.Focus();
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
                Fill_Data(comboBox1.SelectedValue.ToString(), txt_ref.Text,"");
            }
            catch { }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
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
            {/*
                Acc1.rpt.Acc_frmPrint rf = new Acc1.rpt.Acc_frmPrint();

                //SqlConnection con = new SqlConnection(@"Data Source=RYD1-PC\INFOSOFT12;" +
                //                                    "Initial Catalog=DBASE;" +
                //                                    "User id=sa;" +
                //                                    "Password=infocic;");

                //SqlDataAdapter da1 = new SqlDataAdapter("select * from account where a_key='010201001'", con);
                //SqlDataAdapter da2 = new SqlDataAdapter("select * from acc_hdr where a_ref=7", con);
                //SqlDataAdapter da3 = new SqlDataAdapter("SELECT acc_dtl.a_key, account.a_name, acc_dtl.a_text, acc_dtl.a_camt, acc_dtl.a_damt"
                //                                       + " FROM acc_dtl INNER JOIN"
                //                                       + " account ON acc_dtl.a_key = account.a_key"
                //                                       + " where acc_dtl.a_key='010201001'", con);

                DataSet ds1 = new DataSet();

                // ds1.Tables["account"]=dthdr;
                // ds1.Tables["account"] = dthdr;
                //  da3.Fill(ds1, "details");

                // this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds1));

                //ReportParameter[] parameters = new ReportParameter[1];
                //parameters[0] = new ReportParameter("comp_name", "hi");

                //rf.reportViewer1.LocalReport.SetParameters(parameters);

                // rf.reportViewer1.LocalReport.SetParameters("comp_name", BL.CLS_Session.cmp_name);

                // rf.reportViewer1.LocalReport.Refresh();


                rf.reportViewer1.LocalReport.DataSources.Clear();
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("acc_hdr", dthdr));
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("details", dtdtl));

                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Acc1.rpt.Acc_Tran_rpt.rdlc";

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
              
                */
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
                DataTable dtl = new DataTable();
                int cn = 1;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    /*
                    dt.Columns.Add(col.Name);
                    // dt.Columns.Add(col.HeaderText);
                     */
                    dtl.Columns.Add("c" + cn.ToString());
                    cn++;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataRow dRow = dtl.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    dtl.Rows.Add(dRow);
                }

                rf.reportViewer1.LocalReport.DataSources.Clear();
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("acc_hdr", dthdr));
              //  rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("details", dtdtl));
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("details", dtl));

              //  rf.reportViewer1.LocalReport.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Acc_Tran_rpt.rdlc";
              //rf.reportViewer1.LocalReport.ReportEmbeddedResource
                if (File.Exists(Directory.GetCurrentDirectory() + @"\reports\Acc_Tran_All_rpt2.rdlc"))
                    rf.reportViewer1.LocalReport.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Acc_Tran_All_rpt2.rdlc";
                else
                  rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Acc.rpt.Acc_Tran_All_rpt2.rdlc";

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[10];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
               // parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());
                parameters[2] = new ReportParameter("aType", comboBox1.Text);
                parameters[3] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[4] = new ReportParameter("userin", txt_user.Text);
                parameters[5] = new ReportParameter("userprint", BL.CLS_Session.UserName);
                parameters[6] = new ReportParameter("comp_ename", BL.CLS_Session.cmp_ename);
                parameters[7] = new ReportParameter("comp_taxid", BL.CLS_Session.tax_no);
                parameters[8] = new ReportParameter("comp_adress", BL.CLS_Session.dtcomp.Rows[0]["city"].ToString() + " - " + BL.CLS_Session.dtcomp.Rows[0]["site_name"].ToString() + " - " + BL.CLS_Session.dtcomp.Rows[0]["street"].ToString());
                parameters[9] = new ReportParameter("comp_rg", BL.CLS_Session.dtcomp.Rows[0]["ownr_mob"].ToString());
                rf.reportViewer1.LocalReport.SetParameters(parameters);

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();
                 
            }
            catch { }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            total();
        }
        private void total()
        {
            try
            {
                foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                {
                    if ((dgvr.Cells[5].Value == null || dgvr.Cells[5] == null) && !dgvr.IsNewRow)
                        dgvr.Cells[5].Value = "0.0000";

                    if ((dgvr.Cells[6].Value == null || dgvr.Cells[6] == null) && !dgvr.IsNewRow)
                        dgvr.Cells[6].Value = "0.0000";

                    if ((dgvr.Cells[7].Value == null || dgvr.Cells[7] == null) && !dgvr.IsNewRow)
                        dgvr.Cells[7].Value = "0.0000";

                    if ((dgvr.Cells[10].Value == null || dgvr.Cells[10] == null) && !dgvr.IsNewRow)
                        dgvr.Cells[10].Value = "0.0000";

                    if ((dgvr.Cells[11].Value == null || dgvr.Cells[11] == null) && !dgvr.IsNewRow)
                        dgvr.Cells[11].Value = "";

                    if ((dgvr.Cells[12].Value == null || dgvr.Cells[12] == null) && !dgvr.IsNewRow)
                        dgvr.Cells[12].Value = "";
                }

                double sumc = 0, sumd = 0, sumto = 0, sumtin = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        sumc = sumc + Convert.ToDouble(row.Cells[6].Value);
                        sumd = sumd + Convert.ToDouble(row.Cells[5].Value);
                        sumto =  Convert.ToDouble(row.Cells[7].Value) > 0 && Convert.ToDouble(row.Cells[5].Value) > 0 && Convert.ToDouble(row.Cells[6].Value) == 0 ? sumto + Convert.ToDouble(row.Cells["tax"].Value) :sumto + 0;
                        sumtin = Convert.ToDouble(row.Cells[7].Value) > 0 && Convert.ToDouble(row.Cells[6].Value) > 0 && Convert.ToDouble(row.Cells[5].Value) == 0 ? sumtin + Convert.ToDouble(row.Cells["tax"].Value) :sumtin + 0;
                    }
                }

              //  MessageBox.Show(sumto.ToString()); MessageBox.Show(sumtin.ToString());

                txt_ttltxout.Text =Math.Round(sumto,4).ToString();
                txt_ttltxin.Text = Math.Round(sumtin,4).ToString();

                txt_camt.Text = Math.Round((sumc + sumtin),4).ToString();
                txt_damt.Text = Math.Round((sumd + sumto),4).ToString();
               

            }
            catch (Exception ex) { 
               // MessageBox.Show(ex.Message);
            }
        }


        private void txt_camt_TextChanged(object sender, EventArgs e)
        {
            total();
            if (Convert.ToDouble(txt_camt.Text) > Convert.ToDouble(txt_amt.Text))
            {
                MessageBox.Show("تجاوز مبلغ القيد", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               // return;
            }
            txt_fdamt.Text = (Convert.ToDouble(txt_camt.Text) > Convert.ToDouble(txt_damt.Text) ? Convert.ToDouble(txt_camt.Text) - Convert.ToDouble(txt_damt.Text) : 0).ToString();
            txt_fcamt.Text = (Convert.ToDouble(txt_damt.Text) > Convert.ToDouble(txt_camt.Text) ? Convert.ToDouble(txt_damt.Text) - Convert.ToDouble(txt_camt.Text) : 0).ToString();
            total();
        }

        private void txt_damt_TextChanged(object sender, EventArgs e)
        {
            total();
            if (Convert.ToDouble(txt_damt.Text) > Convert.ToDouble(txt_amt.Text))
            {
                MessageBox.Show("تجاوز مبلغ القيد", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               // return;
            }
            txt_fdamt.Text = (Convert.ToDouble(txt_camt.Text) > Convert.ToDouble(txt_damt.Text) ? Convert.ToDouble(txt_camt.Text) - Convert.ToDouble(txt_damt.Text) : 0).ToString();
            txt_fcamt.Text = (Convert.ToDouble(txt_damt.Text) > Convert.ToDouble(txt_camt.Text) ? Convert.ToDouble(txt_damt.Text) - Convert.ToDouble(txt_camt.Text) : 0).ToString();
            total();
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
                Acc_Statment_Exp f4a = new Acc_Statment_Exp(dataGridView1.CurrentRow.Cells[0].Value.ToString());
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
            try
            {
                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                // if (dataGridView1.CurrentCell.ColumnIndex == 5 || dataGridView1.CurrentCell.ColumnIndex == 6 || dataGridView1.CurrentCell.ColumnIndex == 0 || dataGridView1.CurrentCell.ColumnIndex == 1 || dataGridView1.CurrentCell.ColumnIndex == 2) //Desired Column
                if (dataGridView1.CurrentCell.ColumnIndex == 5 || dataGridView1.CurrentCell.ColumnIndex == 6)
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

                /*
                if (dataGridView1.CurrentCell.ColumnIndex == 0)//|| dataGridView1.CurrentCell.ColumnIndex == 5 || dataGridView1.CurrentCell.ColumnIndex == 6) //Desired Column
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress2);
                    }
                }
              */
            }
            catch { }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Column1_KeyPress2(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Fill_Data(comboBox1.SelectedValue.ToString(), txt_ref.Text,"");
                timer1.Stop();
            }
            catch { }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dg = (DataGridView)sender;

                if (dg.CurrentCell.EditType == typeof(DataGridViewComboBoxEditingControl))
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
                    //////DataGridViewComboBoxCell dcombo = new DataGridViewComboBoxCell();
                    //////if (dataGridView1.CurrentRow.Cells[8].Value.ToString().Equals("2"))
                    //////{

                    //////    DataView dv1 = dttaxcats.DefaultView;
                    //////    dv1.RowFilter = "In_out_tax in('" + dataGridView1.CurrentRow.Cells[8].Value + "')";
                    //////    DataTable dtNew = dv1.ToTable();
                    //////    dcombo.DataSource = dtNew;
                    //////    dcombo.DisplayMember = "taxcat_name";
                    //////    dcombo.ValueMember = "taxcatID";

                    //////    dataGridView1.CurrentRow.Cells[7] = dcombo;
                    //////    dataGridView1.CurrentRow.Cells[7].Value = "5";
                    //////    dcombo.FlatStyle = FlatStyle.Flat;
                    //////}
                    //////else
                    //////{
                    //////    if (dataGridView1.CurrentRow.Cells[8].Value.ToString().Equals("1"))
                    //////    {

                    //////        DataView dv1 = dttaxcats.DefaultView;
                    //////        dv1.RowFilter = "In_out_tax in('" + dataGridView1.CurrentRow.Cells[8].Value + "')";
                    //////        DataTable dtNew = dv1.ToTable();
                    //////        dcombo.DataSource = dtNew;
                    //////        dcombo.DisplayMember = "taxcat_name";
                    //////        dcombo.ValueMember = "taxcatID";


                    //////        dataGridView1.CurrentRow.Cells[7] = dcombo;
                    //////        dataGridView1.CurrentRow.Cells[7].Value = "1";
                    //////        dcombo.FlatStyle = FlatStyle.Flat;
                    //////    }
                    //////}
                    SendKeys.Send("{F4}");
                    //  dataGridView1.BeginEdit(true);
                    //  ((ComboBox)dataGridView1.EditingControl).DroppedDown = true;
                    SendKeys.Send("{Down}");
                    SendKeys.Send("{UP}");
                    // dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][5];
                }
            }
            catch { }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[5].Value.ToString()))
                    dataGridView1[5, dataGridView1.CurrentRow.Index].Value = 0;

                if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[6].Value.ToString()))
                    dataGridView1[6, dataGridView1.CurrentRow.Index].Value = 0;

                if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[7].Value.ToString()))
                    dataGridView1[7, dataGridView1.CurrentRow.Index].Value = 0;

                if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[10].Value.ToString()))
                    dataGridView1[10, dataGridView1.CurrentRow.Index].Value = 0;

                if (string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
                    dataGridView1.CurrentRow.Cells[4].Value = txt_desc.Text;

                total();
            }
            catch { }

        }

        private void Acc_Tax_Tran_Shown(object sender, EventArgs e)
        {
            btn_Rfrsh_Click( sender,  e);
            if (!BL.CLS_Session.formkey.Contains(this.Tag.ToString()))
            {
                this.Close();
            }
        }

        private void txt_bookno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_desc.Focus();
            }
        }

        private void txt_bookno_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.Handled = !(char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar));
            if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                if (e.KeyChar >= '0' && e.KeyChar <= '9')
                    e.Handled = false;
                else
                    e.Handled = true;
                //  e.Handled = true;
            }
        }

        private void txt_ccno_Leave(object sender, EventArgs e)
        {
            DataTable dta = daml.SELECT_QUIRY_only_retrn_dt("select cc_id,cc_name from costcenters where cc_id='" + txt_ccno.Text + "' and cc_brno='" + BL.CLS_Session.brno + "'");
            if (dta.Rows.Count > 0)
            {
                txt_ccname.Text = dta.Rows[0][1].ToString();
                txt_ccno.Text = dta.Rows[0][0].ToString();
                // txt_opnbal.Text = dta.Rows[0][1].ToString();
            }
            else
            {
                //    MessageBox.Show("الحساب غير موجود");
                txt_ccname.Text = "";
                txt_ccno.Text = "";
                //  txt_code.Text = dt.Rows[0][2].ToString();
                // txt_opnbal.Text = "0";
            }
        }

        private void txt_ccno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    // var selectedCell = dataGridView1.SelectedCells[0];
                    // do something with selectedCell...
                    Search_All f4 = new Search_All("M", "");
                    f4.ShowDialog();



                    txt_ccno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_ccname.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                    /*
                   dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
                   dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
                   dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                    */



                }

                if (e.KeyCode == Keys.Enter)
                {

                    // button1_Click( sender,  e);
                    DataTable dta = daml.SELECT_QUIRY_only_retrn_dt("select cc_id,cc_name from costcenters where cc_id='" + txt_ccno.Text + "' and cc_brno='" + BL.CLS_Session.brno + "'");
                    if (dta.Rows.Count > 0)
                    {
                        txt_ccname.Text = dta.Rows[0][1].ToString();
                        txt_ccno.Text = dta.Rows[0][0].ToString();
                        // txt_opnbal.Text = dta.Rows[0][1].ToString();
                    }
                    else
                    {
                        //    MessageBox.Show("الحساب غير موجود");
                        txt_ccname.Text = "";
                        txt_ccno.Text = "";
                        //  txt_code.Text = dt.Rows[0][2].ToString();
                        // txt_opnbal.Text = "0";
                    }

                }
            }
            catch { }
        }

        private void btn_qbl_Click(object sender, EventArgs e)
        {
            try
            {
                Fill_Data(comboBox1.SelectedValue.ToString(), (Convert.ToInt32(txt_ref.Text) - 1).ToString(), "");

            }
            catch { }
        }

        private void btn_tali_Click(object sender, EventArgs e)
        {
            try
            {
                Fill_Data(comboBox1.SelectedValue.ToString(), (Convert.ToInt32(txt_ref.Text) + 1).ToString(), "");

            }
            catch { }
        }

        private void ادراجصفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dataGridView1.ReadOnly==false)
            this.dataGridView1.Rows.Insert(dataGridView1.CurrentRow.Index);
        }

        private void MoveRow(int i)
        {
            try
            {
                if ((this.dataGridView1.SelectedCells.Count > 0))
                {
                    int curr_index = this.dataGridView1.CurrentCell.RowIndex;
                    int curr_col_index = this.dataGridView1.CurrentCell.ColumnIndex;
                    DataGridViewRow curr_row = this.dataGridView1.CurrentRow;
                    this.dataGridView1.Rows.Remove(curr_row);
                    this.dataGridView1.Rows.Insert(curr_index + i, curr_row);
                    this.dataGridView1.CurrentCell = this.dataGridView1[curr_col_index, curr_index + i];
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btn_down_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentCell.RowIndex < dataGridView1.Rows.Count - 1)
                MoveRow(+1);// move down in the datagridview (row index is 1 more)
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentCell.RowIndex > 0)
                MoveRow(-1);// move up in the datagridview (row index is 1 less
        }
    }
}



