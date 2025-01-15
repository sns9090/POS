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

namespace POS.Acc
{
    public partial class Acc_bdgt_report : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dtper, dtacbal;
        public static int qq;
        // string typeno = "";
        SqlConnection con = BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        // sqlConnection

        public Acc_bdgt_report()
        {
            InitializeComponent();
        }

        private void Acc_bdgt_report_Load(object sender, EventArgs e)
        {
            dtper = BL.CLS_Session.dtyrper;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if  (string.IsNullOrEmpty(txt_fno.Text.Trim()))
            {
                MessageBox.Show("حقل اجباري", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_fno.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txt_tno.Text.Trim()))
            {
                MessageBox.Show("حقل اجباري", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_tno.Focus();
                return;
            }

          //  MessageBox.Show(dtper.Rows[0]["per01"].ToString());
            try
            {
               //string condallbr = chk_allbr.Checked ? " " : " and a.a_brno='" + BL.CLS_Session.brno + "' ";
               // string condtax = chk_tax.Checked ? " and a.acckind in ('1','2') " : " ";
               // string cond = checkBox1.Checked ? "b.a_hdate" : "b.a_mdate";
               // string condp = rad_posted.Checked ? " and c.a_posted=1 " : (rad_notpostd.Checked ? " and c.a_posted=0 " : " ");
               // string condtype = rd_mt .Checked ? " and a.a_type='2' " : (rd_tshqil.Checked ? " and a.a_type ='0' " : (chk_ak.Checked ? " and a.a_type='3' " : (chk_mm.Checked ? " and a.a_type='1' ": "" )));
                string qstr;
                string txt = "";
                
                for (int i = Convert.ToInt32(txt_fno.Text); i <= Convert.ToInt32(txt_tno.Text); i++)
                {
                    txt = txt + "+g.bubal" + (i.ToString().Length == 1 ? "0" + i.ToString() : i.ToString());
                }

                txt = txt.Remove(0, 1);

                string condt ;
                    if(rd_pass.Checked)
                        condt = " where " + txt + " < round((select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no  where a.a_key=b.a_key  and b.a_mdate >='" + dtper.Rows[0]["per" + (txt_fno.Text.Length == 1 ? "0" + txt_fno.Text : txt_fno.Text)].ToString() + "' and b.a_mdate < '" + dtper.Rows[0]["per" + (txt_tno.Text.Length == 1 ? "0" + txt_tno.Text : txt_tno.Text)].ToString() + "'),2)";
                else   
                    {
                        if (rd_notpass.Checked)
                            condt = " where " + txt + " > round((select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no  where a.a_key=b.a_key  and b.a_mdate >='" + dtper.Rows[0]["per" + (txt_fno.Text.Length == 1 ? "0" + txt_fno.Text : txt_fno.Text)].ToString() + "' and b.a_mdate < '" + dtper.Rows[0]["per" + (txt_tno.Text.Length == 1 ? "0" + txt_tno.Text : txt_tno.Text)].ToString() + "'),2)";
                        else
                        {
                            if (rd_equal.Checked)
                                condt = " where " + txt + " = round((select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no  where a.a_key=b.a_key  and b.a_mdate >='" + dtper.Rows[0]["per" + (txt_fno.Text.Length == 1 ? "0" + txt_fno.Text : txt_fno.Text)].ToString() + "' and b.a_mdate < '" + dtper.Rows[0]["per" + (txt_tno.Text.Length == 1 ? "0" + txt_tno.Text : txt_tno.Text)].ToString() + "'),2)";
                            else
                                condt = " where 0<>1 ";
                        }
                    }
                //if (!chk_openbalonly.Checked)
                //{
                    qstr = "select a.a_key d1,a.a_name d2,round((select isnull(sum(a_damt-a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no  where a.a_key=b.a_key  and b.a_mdate >='" + dtper.Rows[0]["per" + (txt_fno.Text.Length == 1 ? "0" + txt_fno.Text : txt_fno.Text)].ToString() + "' and b.a_mdate < '" + dtper.Rows[0]["per" + (txt_tno.Text.Length == 1 ? "0" + txt_tno.Text : txt_tno.Text)].ToString() + "'),2) d3"
                            + " , d4=" + txt + ",d5=0,d6=0  from accounts a join accbdgt g on a.a_key=g.bacc and a.a_brno=g.brno " + condt + " "
                            + " and len(a.a_key)=9 and a.a_key like '" + txt_code.Text + "%' and a_brno='" + BL.CLS_Session.brno + "' and exists(select * from accbdgt g where g.bacc=a.a_key and g.brno=a.a_brno)"
                            + " order by a.a_key;";
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
                double sum = 0,sum2=0,sum3=0,sum4=0;
                foreach (DataRow dtr in dtacbal.Rows)
                {
                    sum = sum + Convert.ToDouble(dtr[2]);
                    sum2 = sum2 + Convert.ToDouble(dtr[3]);
                    dtr[4] = Convert.ToDouble(dtr[3]) - Convert.ToDouble(dtr[2]);

                    dtr[5] = Convert.ToDouble(dtr[4]) / Convert.ToDouble(dtr[3]) * 100;

                    sum3 = sum3 + Convert.ToDouble(dtr[4]);
                    sum4 = sum4 + Convert.ToDouble(dtr[5]);

                }
                dr[0] = "";
                dr[1] = "الاجمالي";
                dr[2] = sum;
                dr[3] = sum2;
                dr[4] = sum3;
                dr[5] = sum4;

                dtacbal.Rows.Add(dr);

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

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
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
            if (e.KeyCode == Keys.F8)
            {
                // var selectedCell = dataGridView1.SelectedCells[0];
                // do something with selectedCell...
                Search_All f4 = new Search_All("1", "Acc");
                f4.ShowDialog();

                try
                {

                    txt_code.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                    /*
                   dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
                   dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
                   dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                    */


                }
                catch { }
            }

            if (e.KeyCode == Keys.Enter)
            {

               // button1_Click(sender, e);
                DataTable dth = daml.SELECT_QUIRY_only_retrn_dt("select a_name,round(a_opnbal,2) a_opnbal,a_key from accounts where a_key='" + txt_code.Text + "' and a_brno='" + BL.CLS_Session.brno + "'");
                if (dth.Rows.Count > 0)
                {
                    txt_name.Text = dth.Rows[0][0].ToString();
                    txt_code.Text = dth.Rows[0][2].ToString();
                }


            }

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

                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Acc.rpt.Acc_bdgt_rpt.rdlc";

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
