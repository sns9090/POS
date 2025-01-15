using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
//using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Configuration;
namespace POS.Sto
{
    public partial class FRM_PRODUCTS : BaseForm
    {

        private static FRM_PRODUCTS frm;
        BL.DAML daml = new BL.DAML();
        SqlConnection con2 = BL.DAML.con; // new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        static void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm = null;
        }

        public static FRM_PRODUCTS getMainForm
        {
            get
            {
                if (frm == null)
                {
                    frm = new FRM_PRODUCTS();
                    frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                }
                return frm;
            }
        }



        BL.CLS_PRODUCTS prd = new BL.CLS_PRODUCTS();
        public FRM_PRODUCTS()
        {
            InitializeComponent();
            if (frm == null)
                frm = this;
          //  this.dataGridView1.DataSource = prd.GET_ALL_PRODUCTS();
          //  dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("SELECT [item_no] ,[item_name],[item_cost] ,[item_price],[item_barcode],[item_group],[item_image] FROM [dbo].[items] WITH (NOLOCK)");
            //add
          //  dataGridView1.Columns[6].Visible = false;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           // DataTable Dt = new DataTable();
           // Dt = prd.SearchProduct(txtSearch.Text);
           // this.dataGridView1.DataSource = Dt;
            dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("SELECT a.[item_no],a.[item_barcode] ,a.[item_name],a.[item_curcost] ,a.[item_price],b.[group_name] [item_group],a.[item_image] FROM [dbo].[items] a join [dbo].[groups] b on a.[item_group]=b.[group_id] where item_name + item_no like '%" + txtSearch.Text.Replace(" ", "%") + "%'" + (cmb_group.SelectedIndex != -1 ? " and item_group ='" + cmb_group.SelectedValue + "'" : "") + " " + (cmb_sgrp.SelectedIndex != -1 ? " and sgroup ='" + cmb_sgrp.SelectedValue + "'" : "") + "");
           
           // hight();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FRM_ADD_PRODUCT frm = new FRM_ADD_PRODUCT();
           // frm.ShowDialog();

            Sto.Item_Card itc = new Sto.Item_Card("");
            itc.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Sto.Item_Card ac = new Sto.Item_Card(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                ac.MdiParent = ParentForm;
                ac.Show();
            }
            catch { }
            /*
            ////if (MessageBox.Show("هل تريد فعلا حذف المنتوج المحدد؟", "عملية الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            ////{
            ////    prd.DeleteProduct(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
            ////    MessageBox.Show("تمت عملية الحذف بنجاح", "عملية الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////    this.dataGridView1.DataSource = prd.GET_ALL_PRODUCTS();
            ////}
            ////else {
            ////    MessageBox.Show("تم إلغاء عملية الحذف", "عملية الحذف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            ////}

            string to_del = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();

            if (to_del=="")
            {
                MessageBox.Show("لا يوجد صنف لحذفة يرجى اختار صنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlDataAdapter da10 = new SqlDataAdapter("select * from dtl where barcode ='" + to_del + "'", con2);

                DataTable dt10 = new DataTable();
                da10.Fill(dt10);
                if (dt10.Rows.Count != 0)
                {
                    MessageBox.Show("لا يمكن حذف صنف تمت عليه حركة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("هل تريد فعلا حذف المنتوج المحدد؟", "عملية الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        //////prd.DeleteProduct(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        //////MessageBox.Show("تمت عملية الحذف بنجاح", "عملية الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //////this.dataGridView1.DataSource = prd.GET_ALL_PRODUCTS();
                        con2.Open();
                        using (SqlCommand cmd10 = new SqlCommand("delete from items where item_no='" + to_del + "'", con2))
                        {



                            cmd10.ExecuteNonQuery();
                            con2.Close();
                            MessageBox.Show("تم مسح الصنف " + to_del);
                            //textBox1.Text = "";
                            //textBox2.Text = "";
                            //textBox3.Text = "";
                            //textBox4.Text = "";
                            //textBox5.Text = "";
                            //textBox6.Text = "";
                            //comboBox1.Items.Clear();
                        }

                        this.dataGridView1.DataSource = prd.GET_ALL_PRODUCTS();
                    }
                    else
                    {
                        MessageBox.Show("تم إلغاء عملية الحذف", "عملية الحذف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    //string xx = textBox1.Text.ToString();
                    
                    // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))
                   
                }
            }
             */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //////FRM_ADD_PRODUCT frm = new FRM_ADD_PRODUCT();
            //////frm.txtRef.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //////frm.txtDes.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //////frm.txtQte.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            //////frm.txtPrice.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            //////frm.cmbCategories.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
            //////frm.Text = "تحديث المنتوج: " + this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //////frm.btnOk.Text = "تحديث";
            //////frm.state = "update";
            //////frm.txtRef.ReadOnly = true;
            //////byte[] image = (byte[])prd.GET_IMAGE_PRODUCT(this.dataGridView1.CurrentRow.Cells[0].Value.ToString()).Rows[0][0];
            //////MemoryStream ms = new MemoryStream(image);
            //////frm.pbox.Image = Image.FromStream(ms);
            //////frm.ShowDialog();

           // MAIN mn = new MAIN();

           ////// Sto.Item_Card itca = new Sto.Item_Card("");
           //////// itca.MdiParent = mn;

           ////// itca.textBox7.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
           ////// itca.button4_Click(sender, e);
           ////// itca.button5_Click(sender, e);
           ////// itca.ShowDialog();
            Sto.Item_Card itca = new Sto.Item_Card(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
            itca.MdiParent = MdiParent;
            //itca.button5_Click(sender, e);
            //itca.textBox7.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //itca.button4_Click(sender, e);
            //itca.button5_Click(sender, e);
            itca.Show();
            itca.button5_Click(sender, e);


        }

        private void button4_Click(object sender, EventArgs e)
        {
            //////FRM_PREVIEW frm = new FRM_PREVIEW();
            //////byte[] image = (byte[])prd.GET_IMAGE_PRODUCT(this.dataGridView1.CurrentRow.Cells[0].Value.ToString()).Rows[0][0];
            //////MemoryStream ms = new MemoryStream(image);
            //////frm.pictureBox1.Image = Image.FromStream(ms);
            //////frm.ShowDialog();

            PL.FRM_PREVIEW frm = new PL.FRM_PREVIEW();

            if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + prd.GET_IMAGE_PRODUCT(this.dataGridView1.CurrentRow.Cells[0].Value.ToString()).Rows[0][0]))
            {
                frm.pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + prd.GET_IMAGE_PRODUCT(this.dataGridView1.CurrentRow.Cells[0].Value.ToString()).Rows[0][0]);
                frm.ShowDialog();

            }
            else
            {
                frm.pictureBox1.Image = Properties.Resources.background_button;
                frm.ShowDialog();
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //RPT.rpt_prd_single myReport = new RPT.rpt_prd_single();
            //myReport.SetParameterValue("@ID", this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
            //RPT.FRM_RPT_PRODUCT myForm = new RPT.FRM_RPT_PRODUCT();
            //myForm.crystalReportViewer1.ReportSource = myReport;
            //myForm.ShowDialog();

            RPT.rpt_prd_single myReport = new RPT.rpt_prd_single();
           
            myReport.SetParameterValue("@ID", this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
            RPT.FRM_RPT_PRODUCT myForm = new RPT.FRM_RPT_PRODUCT();
            myForm.crystalReportViewer1.ReportSource = myReport;
            myForm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RPT.rpt_all_products myReport = new RPT.rpt_all_products();
            RPT.FRM_RPT_PRODUCT myForm = new RPT.FRM_RPT_PRODUCT();
            myForm.crystalReportViewer1.ReportSource = myReport;
            myForm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            RPT.rpt_all_products myReport = new RPT.rpt_all_products();

            //Create Export Options
            ExportOptions export = new ExportOptions();

            //Create Object For destination
            DiskFileDestinationOptions dfoptions = new DiskFileDestinationOptions();

            //Create Object For Excel Format
            ExcelFormatOptions excelformat = new ExcelFormatOptions();

            //Set the path of destination
            dfoptions.DiskFileName = @"E:\ProductsList.xls";

            //Set Report Options to crystal export options
            export = myReport.ExportOptions;

            //Set Destination type
            export.ExportDestinationType = ExportDestinationType.DiskFile;

            //Set the type of document
            export.ExportFormatType = ExportFormatType.Excel;

            //format the excel document
            export.ExportFormatOptions = excelformat;

            //Set Destination option
            export.ExportDestinationOptions = dfoptions;

            //Export the report
            myReport.Export();

            MessageBox.Show("List Exported Successfully !", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FRM_PRODUCTS_Load(object sender, EventArgs e)
        {
            hight();
            txtSearch.Select();
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));

            cmb_group.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select group_id,group_name from groups where group_pid is null");
            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
            cmb_group.DisplayMember = "group_name";
            cmb_group.ValueMember = "group_id";
            cmb_group.SelectedIndex = -1;

        }

        private void hight()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 30;
            }

            //int x = dataGridView1.RowCount;
            //dataGridView1.Rows[x - 2].Height = 30;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                XcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                   // XcelApp.Cells[1, i] ="'" + dataGridView1.Columns[i - 1].HeaderText;
                    XcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                       // XcelApp.Cells[i + 2, j + 1] = "'" + dataGridView1.Rows[i].Cells[j].Value.ToString();
                        XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
          //  InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
        }

        private void txtSearch_MouseHover(object sender, EventArgs e)
        {
          //  InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Sto.Item_Card itc = new Sto.Item_Card("");
          //  itc.button3_Click(sender, e);
            itc.MdiParent = MdiParent;
           // itc.button3_Click(sender, e);
            itc.Show();
            itc.button3_Click(sender, e);
        }

        private void cmb_group_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_group.SelectedIndex = -1;
            }
        }

        private void cmb_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmb_sgrp.SelectedIndex = -1;
                dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("SELECT a.[item_no],a.[item_barcode] ,a.[item_name],a.[item_curcost] ,a.[item_price],b.[group_name] [item_group],a.[item_image] FROM [dbo].[items] a join [dbo].[groups] b on a.[item_group]=b.[group_id] where item_name + item_no like '%" + txtSearch.Text.Replace(" ", "%") + "%'" + (cmb_group.SelectedIndex != -1 ? " and item_group ='" + cmb_group.SelectedValue + "'" : "") + " " + (cmb_sgrp.SelectedIndex != -1 ? " and sgroup ='" + cmb_sgrp.SelectedValue + "'" : "") + "");
                cmb_sgrp_Enter(sender, e);
               
            }
            catch { }
        }

        private void cmb_sgrp_Enter(object sender, EventArgs e)
        {
            try
            {
                cmb_sgrp.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select group_id,group_name from groups where group_pid='" + cmb_group.SelectedValue + "' and group_pid is not null");
                // metroComboBox3.DataSource = dt2;
                //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
                // metroComboBox3.DisplayMember = "a";
                //  comboBox1.ValueMember = comboBox1.Text;
                cmb_sgrp.DisplayMember = "group_name";
                cmb_sgrp.ValueMember = "group_id";
                cmb_sgrp.SelectedIndex = -1;
            }
            catch { }
        }

        private void cmb_sgrp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_sgrp.SelectedIndex = -1;
            }
        }

        private void cmb_sgrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
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

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
            {
                Sto.Item_Card ac = new Sto.Item_Card(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                ac.MdiParent = ParentForm;
                ac.Show();
            }

            if (e.KeyCode == Keys.F7)
            {
                Sto.Sto_ItemStmt_Exp f4a = new Sto.Sto_ItemStmt_Exp(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                f4a.MdiParent = ParentForm;
                f4a.Show();
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //   // button2_Click(sender, e);
            //    Sto.Item_Card ac = new Sto.Item_Card(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            //    ac.MdiParent = ParentForm;
            //    ac.Show();
            //}
            //catch { }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int rwin=dataGridView1.CurrentRow.Index;
            if (dataGridView1.CurrentRow.Cells[0].Value.ToString() == "")
            {
                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "No Item selected" : "لا يوجد صنف لحذفة يرجى اختار صنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlDataAdapter da10 = new SqlDataAdapter("select count(*) from sales_dtl where barcode ='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", con2);
                SqlDataAdapter da11 = new SqlDataAdapter("select count(*) from pu_dtl where barcode ='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", con2);
                SqlDataAdapter da12 = new SqlDataAdapter("select count(*) from sto_dtl where barcode ='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", con2);
                SqlDataAdapter da13 = new SqlDataAdapter("select count(*) from salofr_dtl where barcode ='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", con2);
                DataTable dt10 = new DataTable();
                DataTable dt11 = new DataTable();
                DataTable dt12 = new DataTable();
                DataTable dt13 = new DataTable();
                da10.Fill(dt10);
                da11.Fill(dt11);
                da12.Fill(dt12);
                da13.Fill(dt13);
                // if (dt10.Rows.Count != 0)
                if (Convert.ToInt32(dt10.Rows[0][0]) != 0 || Convert.ToInt32(dt11.Rows[0][0]) != 0 || Convert.ToInt32(dt12.Rows[0][0]) != 0 || Convert.ToInt32(dt13.Rows[0][0]) != 0)
                {
                    MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Can't delete moved" : "لا يمكن حذف صنف تمت عليه حركة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "do you want to delete?" : "هل تريد حذف " + dataGridView1.CurrentRow.Cells[0].Value.ToString() + " فعلا", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        string xx = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                        //...
                        using (SqlCommand cmd10 = new SqlCommand("delete from items where item_no='" + xx + "'", con2))
                        {


                            con2.Open();
                            cmd10.ExecuteNonQuery();
                            con2.Close();
                            MessageBox.Show("تم مسح الصنف " + xx, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            //textBox1.Text = "";
                            //textBox2.Text = "";
                            ////textBox3.Text = "0";
                            //txt_price.Text = "";
                            //textBox5.Text = "";
                            //cmb_unit.Text = "";
                            dataGridView1.Rows.RemoveAt(rwin);
                            //comboBox1.Items.Clear();
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

                    // using (SqlCommand cmd1 = new SqlCommand("INSERT INTO DB.dbo.hdr (date, total, count) VALUES(@a1,@a2,@a3)", con2))

                }
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // button2_Click(sender, e);
                Sto.Item_Card ac = new Sto.Item_Card(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                ac.MdiParent = ParentForm;
                ac.Show();
            }
            catch { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
               if(checkBox1.Checked)
                   dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("SELECT a.[item_no],a.[item_barcode] ,a.[item_name],a.[item_curcost] ,a.[item_price],b.[group_name] [item_group],a.[item_image] FROM [dbo].[items] a join [dbo].[groups] b on a.[item_group]=b.[group_id] where a.item_tax=2 and item_name + item_no like '%" + txtSearch.Text.Replace(" ", "%") + "%'" + (cmb_group.SelectedIndex != -1 ? " and item_group ='" + cmb_group.SelectedValue + "'" : "") + " " + (cmb_sgrp.SelectedIndex != -1 ? " and sgroup ='" + cmb_sgrp.SelectedValue + "'" : "") + "");
                else
                   dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("SELECT a.[item_no],a.[item_barcode] ,a.[item_name],a.[item_curcost] ,a.[item_price],b.[group_name] [item_group],a.[item_image] FROM [dbo].[items] a join [dbo].[groups] b on a.[item_group]=b.[group_id] where item_name + item_no like '%" + txtSearch.Text.Replace(" ", "%") + "%'" + (cmb_group.SelectedIndex != -1 ? " and item_group ='" + cmb_group.SelectedValue + "'" : "") + " " + (cmb_sgrp.SelectedIndex != -1 ? " and sgroup ='" + cmb_sgrp.SelectedValue + "'" : "") + "");
                
            }
            catch { }
        }

        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmb_type.SelectedIndex!=-1)
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("SELECT a.[item_no],a.[item_barcode] ,a.[item_name],a.[item_curcost] ,a.[item_price],b.[group_name] [item_group],a.[item_image] FROM [dbo].[items] a join [dbo].[groups] b on a.[item_group]=b.[group_id] where a.item_cost=" + (cmb_type.SelectedIndex == 1 ? 0 : cmb_type.SelectedIndex == 2 ? 2 : 1) + " and item_name + item_no like '%" + txtSearch.Text.Replace(" ", "%") + "%'" + (cmb_group.SelectedIndex != -1 ? " and item_group ='" + cmb_group.SelectedValue + "'" : "") + " " + (cmb_sgrp.SelectedIndex != -1 ? " and sgroup ='" + cmb_sgrp.SelectedValue + "'" : "") + "");
                else
                    dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt("SELECT a.[item_no],a.[item_barcode] ,a.[item_name],a.[item_curcost] ,a.[item_price],b.[group_name] [item_group],a.[item_image] FROM [dbo].[items] a join [dbo].[groups] b on a.[item_group]=b.[group_id] where item_name + item_no like '%" + txtSearch.Text.Replace(" ", "%") + "%'" + (cmb_group.SelectedIndex != -1 ? " and item_group ='" + cmb_group.SelectedValue + "'" : "") + " " + (cmb_sgrp.SelectedIndex != -1 ? " and sgroup ='" + cmb_sgrp.SelectedValue + "'" : "") + "");

            }
            catch { }
        }

        private void cmb_type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_type.SelectedIndex = -1;
            }
        }


    }
}
