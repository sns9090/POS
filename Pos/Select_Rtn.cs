using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS.Pos
{
    public partial class Select_Rtn : BaseForm
    {
        DataTable ds23;
        string strd,srdt;
        public Select_Rtn(DataTable dtl,string dis,string ttl)
        {
            InitializeComponent();
            ds23 = dtl;
            strd = dis;
            srdt = ttl;
        }

        private void Select_Rtn_Load(object sender, EventArgs e)
        {
           // DataGridViewRow row = new DataGridViewRow();
          //  MessageBox.Show(ds23.Rows.Count.ToString());
            textBox1.Text = strd;
            textBox2.Text = srdt;
            for (int i = 0; i < ds23.Rows.Count; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[i].Clone();
                row.Cells[0].Value = ds23.Rows[i][4];
                row.Cells[1].Value = ds23.Rows[i][0];
                row.Cells[2].Value = ds23.Rows[i][1];
                row.Cells[3].Value = ds23.Rows[i][5];

                row.Cells[9].Value = ds23.Rows[i][11];
                row.Cells[10].Value = ds23.Rows[i]["pkqty"];
                row.Cells[11].Value = ds23.Rows[i]["i_tax"];
                row.Cells[13].Value = ds23.Rows[i]["img"];
                row.Cells[14].Value = ds23.Rows[i]["mp"];

                //if (row.Cells[4].Value == null)
                //{
                row.Cells[4].Value = ds23.Rows[i]["qty"];
                //}
                //if (row.Cells[5].Value == null)
                //{
                row.Cells[5].Value = ds23.Rows[i][3];
                //}
                //if (row.Cells[6].Value == null)
                //{
                row.Cells[6].Value = ds23.Rows[i]["discpc"];
                //}
                //  row.Cells[5].Value = ds23.Rows[0][3];
                row.Cells[7].Value = ds23.Rows[i][2];
                row.Cells[8].Value = Convert.ToDouble(row.Cells[4].Value) * Convert.ToDouble(row.Cells[5].Value);

                //if (File.Exists(Directory.GetCurrentDirectory() + "\\images\\" + ds23.Tables[0].Rows[i][9]))
                //{
                //    pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\" + ds23.Tables[0].Rows[i][9]);

                //}
                //else
                //{
                //    pictureBox1.Image = Properties.Resources.background_button;

                //}

                //  DataRow[] dtrvat = dttax.Select("tax_id =" + row.Cells[11].Value + "");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";

                //row.Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : (Convert.ToDouble(row.Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(row.Cells[9].Value)) * ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) - ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) * Convert.ToDouble(row.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                row.Cells[12].Value = string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? 0 : BL.CLS_Session.isshamltax.Equals("1") ? (Convert.ToDouble(row.Cells[9].Value)) / (Convert.ToDouble(100)) * ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) - ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) * Convert.ToDouble(row.Cells[6].Value)) / 100) : (Convert.ToDouble(row.Cells[9].Value)) / (Convert.ToDouble(100) + Convert.ToDouble(row.Cells[9].Value)) * ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) - ((Convert.ToDouble(row.Cells[5].Value) * Convert.ToDouble(row.Cells[4].Value)) * Convert.ToDouble(row.Cells[6].Value)) / 100);// Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value)).ToString();
                row.Cells[17].Value = false;

                dataGridView1.Rows.Add(row);
            }
        }

        private void Select_Rtn_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DataTable dt = dataGridView1.DataSource as DataTable;
            //grdSecondGridView.DataSource = dt;
            //grdSecondGridView.DataBind();
        }

        public bool ClosedByXButtonOrAltF4 { get; private set; }
        private const int SC_CLOSE = 0xF060;
        private const int WM_SYSCOMMAND = 0x0112;
        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == WM_SYSCOMMAND && msg.WParam.ToInt32() == SC_CLOSE)
                ClosedByXButtonOrAltF4 = true;
            base.WndProc(ref msg);
        }
        protected override void OnShown(EventArgs e)
        {
            ClosedByXButtonOrAltF4 = false;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (ClosedByXButtonOrAltF4)
                ////// MessageBox.Show("Closed by X or Alt+F4");
                {
                    //dataGridView1.ClearSelection();
                    dataGridView1.DataSource = null;
                }
            }
            catch { }
            // else
            //    MessageBox.Show("Closed by calling Close()");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                foreach (DataGridViewRow dtg in dataGridView1.Rows)
                {
                    if(!dtg.IsNewRow)
                    dtg.Cells[17].Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow dtg in dataGridView1.Rows)
                {
                    if (!dtg.IsNewRow)
                    dtg.Cells[17].Value = false;
                }
            }
        }
    }
}
