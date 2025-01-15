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

namespace POS.Sal
{
    public partial class Trans : BaseForm
    {
        SqlConnection con1 = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        public Trans()
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
            DialogResult result = MessageBox.Show("هل تريد المسح", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {

                try
                {

                    int todel = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                    SqlDataAdapter da = new SqlDataAdapter("select * from sales_hdr where carrier=" + todel, con1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        MessageBox.Show(" لا يمكن حذف مخرج مرتبط بفواتير ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                sqlDataAdapter.Update(dataTable);
                MessageBox.Show("تم حفظ التعديلات","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                Groups_Load(sender, e);


            }

            catch (Exception exceptionObj)
            {

                MessageBox.Show(exceptionObj.Message.ToString());

            }
        }

        private void Groups_Load(object sender, EventArgs e)
        {
            //dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 12);
            dataGridView1.Columns[0].ReadOnly = true;
           // this.dataGridView1.Columns[0].Frozen = true;
            // TODO: This line of code loads data into the 'dataSet1.items' table. You can move, or remove it, as needed.
            //this.itemsTableAdapter.Fill(this.dataSet1.items);
            sqlConnection = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            // sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter("select code, cname, address, tel from sltrans order by code", sqlConnection);
            sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

            dataTable = new DataTable();

          // BL.CLS_Session.dtunits = daml.SELECT_QUIRY_only_retrn_dt("select * from units");
            BL.CLS_Session.dtunits.Rows.Clear();
            sqlDataAdapter.Fill(dataTable);

            BL.CLS_Session.dtunits = dataTable;
            bindingSource = new BindingSource();

            bindingSource.DataSource = dataTable;


            dataGridView1.DataSource = bindingSource;

            


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
            if (dataGridView1.CurrentRow.IsNewRow)
            {
                dataGridView1.CurrentRow.Cells[0].ReadOnly = false;
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //try
            //{
                //if (e.Value == null)
                //    return;
                //var s = e.Graphics.MeasureString(e.Value.ToString(), dataGridView1.Font);
                //if (s.Width > dataGridView1.Columns[e.ColumnIndex].Width)
                //{
                //    using (Brush gridBrush = new SolidBrush(this.dataGridView1.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                //    {
                //        using (Pen gridLinePen = new Pen(gridBrush))
                //        {
                //            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                //            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
                //            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                //            e.Graphics.DrawString(e.Value.ToString(), dataGridView1.Font, Brushes.Black, e.CellBounds, StringFormat.GenericDefault);
                //            dataGridView1.Rows[e.RowIndex].Height = (int)(s.Height * Math.Ceiling(s.Width / dataGridView1.Columns[e.ColumnIndex].Width));
                //            e.Handled = true;
                //        }
                //    }
                //}
            //}
            //catch { }
        }
    }
}
