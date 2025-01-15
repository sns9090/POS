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
using Microsoft.Reporting.WinForms;

namespace POS.Acc
{
    public partial class Acc_opnbal : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        SqlConnection con1 = BL.DAML.con;//new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        string allacc="";
        public Acc_opnbal()
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

                    MessageBox.Show("تم حذف الحساب بنجاح","", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    
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
            int exexcuted=0;
            double val1 = 0,val2=0,res=0;
            try
            {
               // sqlDataAdapter.Update(dataTable);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from DB.dbo.sales_hdr", con2);
                    /*
                    SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from sales_hdr", con2);
                    DataSet dss = new DataSet();
                    daa.Fill(dss, "0");
    */
                  //  val1 = Convert.ToDouble(row.Cells[2].Value);
                  //  val2 = Convert.ToDouble(row.Cells[3].Value);
                    res = Convert.ToDouble(row.Cells[3].Value) > 0 ? Convert.ToDouble(row.Cells[3].Value) * -1 : Convert.ToDouble(row.Cells[2].Value);

                    exexcuted = daml.Insert_Update_Delete_retrn_int("update accounts set a_opnbal=" + res + " where a_key='" + row.Cells[0].Value + "' and a_brno='" + BL.CLS_Session.brno + "'", false);
                 
                   
                }
                if (exexcuted > 0)
                    MessageBox.Show("تم التعديل بنجاح","", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

               
                Groups_Load(sender, e);

            }

            catch (Exception exceptionObj)
            {

                MessageBox.Show(exceptionObj.Message.ToString());

            }
        }

        private void Groups_Load(object sender, EventArgs e)
        {
            //allacc = "";
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 12);
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            if (BL.CLS_Session.up_editop == false)
            {
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
            }
            // this.dataGridView1.Columns[0].Frozen = true;
            // TODO: This line of code loads data into the 'dataSet1.items' table. You can move, or remove it, as needed.
            //this.itemsTableAdapter.Fill(this.dataSet1.items);
            sqlConnection = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            // sqlConnection.Open();
           // sqlDataAdapter = new SqlDataAdapter("select a_key,a_name,case  when a_opnbal >0 then a_opnbal when a_opnbal<=0 then 0 end a_d,case  when a_opnbal<0 then a_opnbal when a_opnbal >=0 then 0 end a_c from accounts where len(a_key)=9 and a_name like '%" + textBox1.Text + "%' or a_key like '" + textBox1.Text + "%' and len(a_key)=9 order by a_key", sqlConnection);
            sqlDataAdapter = new SqlDataAdapter("select a_key,a_name,case  when a_opnbal>0 then a_opnbal when a_opnbal <=0 then 0 end a_d,case  when a_opnbal <0 then -a_opnbal when a_opnbal>=0 then 0 end a_c from accounts where len(a_key)=9 and a_brno='" + BL.CLS_Session.brno + "' and a_name like '%" + textBox1.Text + "%' " + allacc + " order by a_key", sqlConnection);
           
            sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

            dataTable = new DataTable();



            sqlDataAdapter.Fill(dataTable);


            bindingSource = new BindingSource();

            bindingSource.DataSource = dataTable;


            dataGridView1.DataSource = bindingSource;


            total();

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
                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[3])
                {

                    Search_All f4 = new Search_All("1", "Acc");
                    f4.ShowDialog();



                    dataGridView1.CurrentRow.Cells[3].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                    //  dataGridView1.CurrentRow.Cells[1].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
                    //  if (dataGridView1.CurrentRow.Cells[2].Value == null)
                    //  {
                    //     dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
                    //   }

                    //  dataGridView1.CurrentCell = this.dataGridView1[2, dataGridView1.CurrentRow.Index];
                }
                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[4])
                {
                    Search_All f4 = new Search_All("4", "Acc");
                    f4.ShowDialog();
                    dataGridView1.CurrentRow.Cells[4].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                }
            }
            catch { }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
           
            if (dataGridView1.CurrentCell == dataGridView1[3, dataGridView1.CurrentRow.Index])
            {
                dataGridView1.CurrentRow.Cells[2].Value = "0.00";
                //  dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];


            }

            // if (dataGridView1.CurrentCell == dataGridView1[3, dataGridView1.CurrentRow.Index] && Convert.ToInt32(dataGridView1.CurrentCell.Value) > 0)
            if (dataGridView1.CurrentCell == dataGridView1[2, dataGridView1.CurrentRow.Index])
            {
                dataGridView1.CurrentRow.Cells[3].Value = "0.00";
                // dataGridView1.CurrentCell = this.dataGridView1[0, dataGridView1.CurrentRow.Index + 1];

            }

        }

        private void total()
        {
            try
            {
                double totalc = 0, totald = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    totalc = totalc + Convert.ToDouble(row.Cells[2].Value);
                    totald = totald + Convert.ToDouble(row.Cells[3].Value);
                }
                textBox2.Text = totalc.ToString();
                textBox3.Text = totald.ToString();
            }
            catch { }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            total();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 2 || dataGridView1.CurrentCell.ColumnIndex == 3) //Desired Column
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
            Load_date(textBox4.Text);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F7)
                {

                    Acc.Acc_Statment_Exp f4a = new Acc.Acc_Statment_Exp(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    f4a.MdiParent = ParentForm;
                    f4a.Show();
                }

                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
                {
                    Acc.Acc_Card ac = new Acc.Acc_Card(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    ac.MdiParent = ParentForm;
                    ac.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rd_all_CheckedChanged(object sender, EventArgs e)
        {
            allacc="";
            Groups_Load(null, null);
        }

        private void chk_mm_CheckedChanged(object sender, EventArgs e)
        {
             allacc=" and a_type='1' ";
            Groups_Load(null, null);
        }

        private void chk_ak_CheckedChanged(object sender, EventArgs e)
        {
              allacc=" and a_type='3' ";
            Groups_Load(null, null);
        }

        private void chk_m_CheckedChanged(object sender, EventArgs e)
        {
              allacc=" and a_type='2' ";
            Groups_Load(null, null);
        }

        private void rd_tshqil_CheckedChanged(object sender, EventArgs e)
        {
              allacc=" and a_type='0' ";
            Groups_Load(null, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;
            /*
            Reports.Report_Form rrf = new Reports.Report_Form("DR", dtt);
            rrf.MdiParent = ParentForm;
            rrf.Show();
             */
            try
            {
                Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();


               // DataSet ds1 = new DataSet();

                DataTable dt = new DataTable();
                int cn = 1;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    /*
                    dt.Columns.Add(col.Name);
                    // dt.Columns.Add(col.HeaderText);
                     * */
                    dt.Columns.Add("c" + cn.ToString());
                    cn++;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataRow dRow = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    dt.Rows.Add(dRow);
                }


                rf.reportViewer1.LocalReport.DataSources.Clear();
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                //  rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("details", dtdtl));

                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Acc.rpt.Acc_OpnBal_rpt.rdlc";

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[4];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                //parameters[1] = new ReportParameter("mdate", txt_tdate.Text);
                parameters[1] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[2] = new ReportParameter("dd", textBox2.Text);
                parameters[3] = new ReportParameter("cc", textBox3.Text);

                rf.reportViewer1.LocalReport.SetParameters(parameters);

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
              //  int testInt = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["a_p"].Value) ? 1 : 0;
                datval.formate_dgv(this.dataGridView1, 1);
            }
            catch { }
        }
    }
}
    

