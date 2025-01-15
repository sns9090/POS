using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace POS.Sto
{
    public partial class GRD_Result_Print : BaseForm
    {
        string typeg;
        BL.DAML daml = new BL.DAML();
        public GRD_Result_Print()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (msg.WParam.ToInt32() == (int)Keys.Enter)
            {
                SendKeys.Send("{Tab}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sami = chk_same.Checked ? " and ttqty<>mqty " : "";
            if (cmb_ftwh.SelectedIndex == -1)
            {
                MessageBox.Show("يجب اختيار المخزن", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_ftwh.Focus();
                return;

            }
             if (cmb_ftwh.SelectedIndex == -1)
            {
                MessageBox.Show("يجب اختيار المخزن المجرود", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_ftwh.Focus();
                return;

            }

          //  sqlConnection = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            // sqlConnection.Open();
            DataTable dth = daml.SELECT_QUIRY_only_retrn_dt("select * from ttk_hdr where ttbranch='" + BL.CLS_Session.brno + "' and ttwhno='" + cmb_ftwh.SelectedValue + "'");
            if (dth.Rows.Count == 0)
            {
                MessageBox.Show("لا يوجد جرد على المخزن المختار", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_ftwh.Focus();
                return;

            }
            txt_mdate.Text = dth.Rows[0][2].ToString().Substring(6, 2) + dth.Rows[0][2].ToString().Substring(4, 2) + dth.Rows[0][2].ToString().Substring(0, 4);//datval.convert_to_ddDDyyyy(dth.Rows[0][2].ToString());
            txt_hdate.Text = dth.Rows[0][3].ToString().Substring(6, 2) + dth.Rows[0][3].ToString().Substring(4, 2) + dth.Rows[0][3].ToString().Substring(0, 4);//datval.convert_to_ddDDyyyy(dth.Rows[0][3].ToString());
            typeg = rad_plus.Checked ? " الزيادة " : (rad_mins.Checked ? " العجز " : " الجميع ");


            string condp = rad_plus.Checked ? " and round((ttqty-mqty),2)>0 " : (rad_mins.Checked ? " and round((ttqty-mqty),2)<0 " : " ");
            DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select srl_no,itemno,name,round(mqty,2) mqty,round(ttqty,2) ttqty,round((ttqty-mqty),2) frq,round(lcost,4) lcost,round(((ttqty-mqty)*lcost),4) totalcost,price,round(((ttqty-mqty)*price),2) totalprice from ttk_dtl where ttbranch='" + BL.CLS_Session.brno + "' and whno='" + cmb_ftwh.SelectedValue + "' " + condp + sami + " order by srl_no");

            DataRow dr = dt.NewRow();
            double sumopnd = 0, sumopnm = 0, sumtrd = 0, sumtrm = 0, sumttld = 0, sumttlm = 0;
            foreach (DataRow dtr in dt.Rows)
            {
               // sumopnd = sumopnd + Convert.ToDouble(dtr[2]);
                sumopnm = sumopnm + Convert.ToDouble(dtr[3]);
                sumtrd = sumtrd + Convert.ToDouble(dtr[4]);
                sumtrm = sumtrm + Convert.ToDouble(dtr[5]);
                sumttlm = sumttlm + Convert.ToDouble(dtr[7]);
                sumttld = sumttld + Convert.ToDouble(dtr[9]);
            }
           // dr[0] = 0;
           // dr[1] = "";
            dr[2] = "الاجمالي";
            dr[3] = Math.Round(sumopnm,2);
            dr[4] = Math.Round(sumtrd,2);
            dr[5] = Math.Round(sumtrm,2);
           // dr[6] = sumttld;
            dr[7] = Math.Round(sumttlm,4);
            dr[9] = Math.Round(sumttld, 2);
            dt.Rows.Add(dr);

           // dataGridView1.DataSource = dtm;
            dataGridView1.DataSource = dt;

            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
            dataGridView1.ClearSelection();

            chk_lastp.Enabled = true;
        }

        private void GRD_Result_Print_Load(object sender, EventArgs e)
        {
            string sl = BL.CLS_Session.stkey.Replace(" ", "','").Remove(0, 2) + "'";
            DataTable dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select wh_no,wh_name from whouses where wh_brno + wh_no in(" + sl + ") and wh_brno='" + BL.CLS_Session.brno + "'");



            cmb_ftwh.DataSource = dtslctr;
            cmb_ftwh.DisplayMember = "wh_name";
            cmb_ftwh.ValueMember = "wh_no";
            cmb_ftwh.SelectedIndex = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string searchValue =  textBox1.Text;

           // dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
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
                //foreach (DataGridViewRow row in dataGridView1.Rows)
                //{
                //    if (row.Cells[0].Value.ToString().Equals(searchValue))
                //    {
                //        row.Selected = true;
                //        //   row.Cells[0].Selected = true;
                //        break;
                //    }
                //}
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                // Fill_Data(cmb_salctr.SelectedValue.ToString(), cmb_type.SelectedValue.ToString(), txt_ref.Text);

                Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();


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



                rf.reportViewer1.LocalReport.DataSources.Clear();
                // rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("acc_hdr", dthdr));
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("sdt", dt2));

                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sto.rpt.Sto_Grd_rpt.rdlc";

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[5];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                //  parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());
                parameters[1] = new ReportParameter("br_name", BL.CLS_Session.brname);
                // parameters[2] = new ReportParameter("type", cmb_type.Text);
                //  parameters[3] = new ReportParameter("ref", txt_ref.Text);
                parameters[2] = new ReportParameter("date", txt_mdate.Text);
                parameters[3] = new ReportParameter("fwh_name", cmb_ftwh.Text);
              //  parameters[4] = new ReportParameter("twh_name", rad_plus.Checked ? " الزيادة " : (rad_mins.Checked ? " العجز " : " الجميع "));
                parameters[4] = new ReportParameter("twh_name", typeg);
                // parameters[6] = new ReportParameter("desc", txt_desc.Text);
                //  parameters[7] = new ReportParameter("twh_name", cmb_towh.Text);
                //  parameters[8] = new ReportParameter("total", txt_total.Text);
                //   parameters[9] = new ReportParameter("descount", txt_des.Text);
                //  parameters[4] = new ReportParameter("tax", "0");
                // parameters[11] = new ReportParameter("nettotal", txt_total.Text);

                // BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(txt_total.Text), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);

                //  MessageBox.Show(toWord.ConvertToArabic());
                //  parameters[12] = new ReportParameter("a_toword", toWord.ConvertToArabic());

                rf.reportViewer1.LocalReport.SetParameters(parameters);

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();
            }
            catch { }


        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {

                Sto.Sto_ItemStmt_Exp f4a = new Sto.Sto_ItemStmt_Exp(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                f4a.MdiParent = ParentForm;
                f4a.Show();
            }

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
            {
                Sto.Item_Card ac = new Sto.Item_Card(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                ac.MdiParent = ParentForm;
                ac.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void chk_lastp_CheckedChanged(object sender, EventArgs e)
        {
            WaitForm wf = new WaitForm("جاري احتساب اخر سعر شراء .. يرجى الانتظار ...");
            wf.MdiParent = MdiParent;
            wf.Show();
            Application.DoEvents();
            try
            {
                DataTable dth = daml.SELECT_QUIRY_only_retrn_dt(@"SELECT Itemno,(price/pkqty) price,invmdate FROM (SELECT  *, ROW_NUMBER() OVER (PARTITION BY Itemno ORDER By invmdate DESC) AS rn FROM pu_dtl) x WHERE rn = 1 and branch='" + BL.CLS_Session.brno + "' and whno='" + cmb_ftwh.SelectedValue + "'");

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //row.Cells[5].Value = "0.0000";
                    foreach (DataRow dtrow in dth.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Trim().Equals(dtrow[0].ToString().Trim()))
                        {
                            // row.Selected = true;
                            row.Cells[6].Value = dtrow[1].ToString().Trim();
                            break;
                        }
                    }
                }
                wf.Close();
                MessageBox.Show("تم الاحتساب بنجاح", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                wf.Close();
                MessageBox.Show(ex.Message, "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
