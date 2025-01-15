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
using System.Globalization;

namespace POS.Acc
{
    public partial class Acc_cashinout_report : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dtper, dtacbal;
        public static int qq;
        // string typeno = "";
        SqlConnection con = BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        // sqlConnection

        public Acc_cashinout_report()
        {
            InitializeComponent();
        }

        private void Acc_bdgt_report_Load(object sender, EventArgs e)
        {
            dtper = BL.CLS_Session.dtyrper;

            txt_fdate.Text = BL.CLS_Session.start_dt;
            txt_tdate.Text = BL.CLS_Session.end_dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
        //    if  (string.IsNullOrEmpty(txt_fno.Text.Trim()))
        //    {
        //        MessageBox.Show("حقل اجباري", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        txt_fno.Focus();
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(txt_tno.Text.Trim()))
        //    {
        //        MessageBox.Show("حقل اجباري", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        txt_tno.Focus();
        //        return;
        //    }

          //  MessageBox.Show(dtper.Rows[0]["per01"].ToString());
            try
            {
               string condallbr = chk_allbr.Checked ? " " : " and d.a_brno='" + BL.CLS_Session.brno + "'";
               // string condtax = chk_tax.Checked ? " and a.acckind in ('1','2') " : " ";
                string cond = checkBox1.Checked ? "d.a_hdate" : "d.a_mdate";
                string condp = rad_posted.Checked ? " and h.a_posted=1 " : (rad_notpostd.Checked ? " and a.a_posted=0 " : " ");
                string condio = rd_out.Checked ? " having sum(case when d.jddbcr='D' then d.a_damt else 0.000 end)>0 " : rd_in.Checked ? " having sum(case when d.jddbcr='C' then d.a_camt else 0.000 end)>0 " : "";
               // string condp = rad_posted.Checked ? " and c.a_posted=1 " : (rad_notpostd.Checked ? " and c.a_posted=0 " : " ");
               // string condtype = rd_mt .Checked ? " and a.a_type='2' " : (rd_tshqil.Checked ? " and a.a_type ='0' " : (chk_ak.Checked ? " and a.a_type='3' " : (chk_mm.Checked ? " and a.a_type='1' ": "" )));
                string qstr;
               
                
              
                //if (!chk_openbalonly.Checked)
                //{
                qstr = @"Select a.a_key+a.glcurrency d1,a.a_name d2, dout=sum(case when d.jddbcr='D' then d.a_damt else 0.000 end), din=sum(case when d.jddbcr='C' then d.a_camt else 0.000 end) from  accounts a  (NOLOCK)  Left Outer Join  acc_dtl d  (NOLOCK)  inner Join  acc_hdr h  (NOLOCK) on d.a_brno=h.a_brno and d.a_type=h.a_type and d.a_ref = h.a_ref and d.sl_no=h.sl_no and d.pu_no=h.pu_no on  a.a_key=d.a_key and a.glcurrency = d.jdcurr 
                         and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + condp + " where len(a.a_key)=9  and jdcurr='' "+condallbr+" and ((d.a_type In ('02','04') And d.jddbcr='C') Or (d.a_type In ('03','06') And d.jddbcr='D')) group By a.a_key+a.glcurrency,a.a_name,jddbcr "+ condio +" order By jddbcr,a.a_key+a.glcurrency";
                //}
                //else
                //{
                //    qstr = "select a.a_key d1,a.a_name d2,round(a.a_opnbal,2) d3"
                //               + " from accounts a where round(a.a_opnbal,2) <>0 "
                //               + " " + condallbr + " " + " and len(a.a_key)=9 and a.a_key like '" + textBox3.Text + "%' " + condtype + ""
                //               + " order by a.a_key;";
                //}
                dtacbal=daml.SELECT_QUIRY_only_retrn_dt(qstr);

                

                DataRow dr = dtacbal.NewRow();
                DataRow dr2 = dtacbal.NewRow();
                double sum = 0,sum2=0;
                foreach (DataRow dtr in dtacbal.Rows)
                {
                    sum = sum + Convert.ToDouble(dtr[2]);
                    sum2 = sum2 + Convert.ToDouble(dtr[3]);
                    //dtr[4] = Convert.ToDouble(dtr[3]) - Convert.ToDouble(dtr[2]);

                    //dtr[5] = Convert.ToDouble(dtr[4]) / Convert.ToDouble(dtr[3]) * 100;

                    //sum3 = sum3 + Convert.ToDouble(dtr[4]);
                    //sum4 = sum4 + Convert.ToDouble(dtr[5]);

                }
                dr[0] = "";
                dr[1] = "الاجمالي";
                dr[2] = sum;
                dr[3] = sum2;
                //dr[4] = sum3;
                //dr[5] = sum4;

                dr2[0] = "";
                dr2[1] = "صافي التدفقات النقدية";
                dr2[2] = sum > sum2 ? sum - sum2 : 0;
                dr2[3] = sum2 > sum ? sum2 - sum : 0;

                dtacbal.Rows.Add(dr);
                dtacbal.Rows.Add(dr2);

               // dataGridView1.DataSource = dtacbal;

                dataGridView1.DataSource = dtacbal;

              
                //  MessageBox.Show(txt);

                ////foreach (DataGridViewRow dtr0 in dataGridView1.Rows)
                ////{
                ////    if (!string.IsNullOrEmpty(dtr0.Cells["Column1"].Value.ToString()))
                ////    {
                ////      //  DataTable dttem=daml.SELECT_QUIRY_only_retrn_dt("select " + txt + " from accbdgt where brno='" + BL.CLS_Session.brno + "' and bacc='" + dtr0.Cells["d1"].Value + "'");
             
                ////       // dtr0.Cells["d4"].Value = dttem.Rows[0][0].ToString();

                ////        dtr0.Cells["Column5"].Value = Convert.ToDouble(dtr0.Cells["Column4"].Value) - Convert.ToDouble(dtr0.Cells["Column3"].Value);

                ////        dtr0.Cells["Column6"].Value = Convert.ToDouble(dtr0.Cells["Column5"].Value) / Convert.ToDouble(dtr0.Cells["Column4"].Value) * 100;

                ////    }
                ////}

                dataGridView1.Rows[dataGridView1.Rows.Count - 2].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = sum2 > sum ? Color.GreenYellow : Color.LightPink;
                dataGridView1.ClearSelection();
                //dataGridView1.Columns[1].Width = 500;
                //dataGridView1.Columns[2].Width = 200;
                //dataGridView1.Columns[0].Width = 200;

                ////for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                ////{
                ////    if (chk_m.Checked && Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) > 0 )
                ////    {
                ////        dataGridView1.Rows.RemoveAt(i);
                ////       // dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
                ////    }

                ////    if (chk_d.Checked && Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) < 0 )
                ////    {
                ////        dataGridView1.Rows.RemoveAt(i);
                ////      //  dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
                ////    }
                ////}

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txt_code_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F8)
            //{
            //    // var selectedCell = dataGridView1.SelectedCells[0];
            //    // do something with selectedCell...
            //    Search_All f4 = new Search_All("1", "Acc");
            //    f4.ShowDialog();

            //    try
            //    {

            //        txt_code.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //        txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //        //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

            //        /*
            //       dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
            //       dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
            //       dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
            //        */


            //    }
            //    catch { }
            //}

            //if (e.KeyCode == Keys.Enter)
            //{

            //   // button1_Click(sender, e);
            //    DataTable dth = daml.SELECT_QUIRY_only_retrn_dt("select a_name,round(a_opnbal,2) a_opnbal,a_key from accounts where a_key='" + txt_code.Text + "' and a_brno='" + BL.CLS_Session.brno + "'");
            //    if (dth.Rows.Count > 0)
            //    {
            //        txt_name.Text = dth.Rows[0][0].ToString();
            //        txt_code.Text = dth.Rows[0][2].ToString();
            //    }


            //}

        }

        private void button3_Click(object sender, EventArgs e)
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

                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Acc.rpt.Acc_cashinout_rpt.rdlc";

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[4];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                //parameters[1] = new ReportParameter("mdate", txt_tdate.Text);
                parameters[1] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[2] = new ReportParameter("dd", "0");
                parameters[3] = new ReportParameter("cc", "0");

                rf.reportViewer1.LocalReport.SetParameters(parameters);

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        
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
                    Acc.Acc_Card ac = new Acc.Acc_Card(dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim());
                    ac.MdiParent = ParentForm;
                    ac.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txt_fdate_Validating(object sender, CancelEventArgs e)
        {
            txtdate_Validating(txt_fdate, e);
        }

        private void txt_tdate_Validating(object sender, CancelEventArgs e)
        {
            txtdate_Validating(txt_tdate, e);
        }

        public void txtdate_Validating(MaskedTextBox txtbx, CancelEventArgs e)
        {
            DateTime rs;

            CultureInfo ci = new CultureInfo("en-US", false);

            if (!DateTime.TryParseExact(txtbx.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            {

                MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                txtbx.Focus();

            }
            if (Convert.ToInt32(datval.convert_to_yyyyDDdd(txtbx.Text)) < Convert.ToInt32(datval.convert_to_yyyyDDdd(BL.CLS_Session.start_dt)) || Convert.ToInt32(datval.convert_to_yyyyDDdd(txtbx.Text)) > Convert.ToInt32(datval.convert_to_yyyyDDdd(BL.CLS_Session.end_dt)))
            {

                MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;

            }
        }

        private void txt_fdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_fdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_fdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    // txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_fdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_fdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                        else
                        {
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                    }
                }
            }
            catch { }
        }

        private void txt_tdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_tdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_tdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    // txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_tdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_tdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                        else
                        {
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                    }
                }
            }
            catch { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // string temy1 =
            if (checkBox1.Checked)
            {
                txt_fdate.Text = DateTime.Now.ToString("01-01-yyyy", new CultureInfo("ar-SA", false));
                //  string temy2 =
                txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));
            }
            else
            {
                txt_fdate.Text = DateTime.Now.ToString("01-01-yyyy", new CultureInfo("en-US", false));
                //  string temy2 =
                txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               // int testInt = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["a_p"].Value) ? 1 : 0;
                datval.formate_dgv(this.dataGridView1, 1);
            }
            catch { }
        }
    }
}
