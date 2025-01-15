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

namespace POS.Sto
{
    public partial class Sto_Whbins_Org : BaseForm
    {
        SqlConnection con = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        BL.DAML daml = new BL.DAML();
        DataTable dtslctr, dt, dtgrp, dtunits;
        string sl;
        public Sto_Whbins_Org()
        {
            InitializeComponent();
        }

        private void whamount_Load(object sender, EventArgs e)
        {
            

           //////// daml.SqlCon_Open();
           //////// daml.Exec_Query("bld_whbins");
           //////// daml.Exec_Query_only("bld_whbins_sl_pu");
           ////// DialogResult result = MessageBox.Show("Do you want to bild items now هل تريد بناء ارصدة الاصناف الان", "تنبيه", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
           ////// if (result == DialogResult.Yes)
           ////// {
           //////     daml.Exec_Query_only("bld_whbins_cost_all");

           //////     MessageBox.Show("تم بناء الارصدة بنجاح");
           //////     // bk.MdiParent = this;
           //////     // bk.Show();
           //////     // Application.Exit();

           ////// }
           ////// else if (result == DialogResult.No)
           ////// {
           //////     // if (this.ActiveMdiChild != null)
           //////     // int fcont = Application.OpenForms.Count;


           //////     //  this.ActiveMdiChild.Close();
           //////    // daml.Exec_Query_only("update tobld set id=1");
           //////    // Application.Exit();

           //////     //...
           ////// }
           ////// else
           ////// {
           //////     //...
           ////// }

            sreach("");
           // daml.SqlCon_Close();
            total();

            dtunits = daml.SELECT_QUIRY_only_retrn_dt("select * from units");

             sl = BL.CLS_Session.stkey.Replace(" ", "','").Remove(0, 2) + "'";
            // MessageBox.Show(sl);
            dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select wh_no,wh_name from whouses where wh_brno + wh_no in(" + sl + ") and wh_brno='" + BL.CLS_Session.brno + "'");



            cmb_wh.DataSource = dtslctr;
            cmb_wh.DisplayMember = "wh_name";
            cmb_wh.ValueMember = "wh_no";
            cmb_wh.SelectedIndex = -1;

            dtgrp = daml.SELECT_QUIRY_only_retrn_dt("select group_id,group_name from groups where group_pid is null");
           // DataTable dt = new DataTable();
           // da.Fill(dt);


            cmb_grp.DataSource = dtgrp;
            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
            cmb_grp.DisplayMember = "group_name";
            cmb_grp.ValueMember = "group_id";
            cmb_grp.SelectedIndex = -1;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void total()
        {
          //  MessageBox.Show(dataGridView1.RowCount.ToString());
            double sum = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                sum = sum + Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
            }

            textBox1.Text = sum.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           // sreach(textBox2.Text);
            //daml.SqlCon_Close();
           
        }

        private void sreach(string par)
        {
           

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WaitForm wf = new WaitForm("جاري قراءة المخزون الحالي .. يرجى الانتظار ...");
            wf.MdiParent = MdiParent;
            wf.Show();
            Application.DoEvents();
            try{
                string constaticcst = chk_staticost.Checked ? " round((case when a.static_cost>0 then a.static_cost when a.static_cost=0 then " + (cmb_wh.SelectedIndex == -1 ? "a.item_curcost" : "b.lcost") + " else a.static_cost end),2) " : " round((case when " + (cmb_wh.SelectedIndex == -1 ? "a.item_curcost" : "b.lcost") + ">0 then " + (cmb_wh.SelectedIndex == -1 ? "a.item_curcost" : "b.lcost") + " when " + (cmb_wh.SelectedIndex == -1 ? "a.item_curcost" : "b.lcost") + "=0 then a.static_cost else " + (cmb_wh.SelectedIndex == -1 ? "a.item_curcost" : "b.lcost") + " end),2) ";
            string conwh = cmb_wh.SelectedIndex != -1 ? " and b.wh_no='" + cmb_wh.SelectedValue + "'" : " and b.br_no + b.wh_no in(" + sl + ") ";
            string congrp = cmb_grp.SelectedIndex != -1 ? " and a.item_group='" + cmb_grp.SelectedValue + "'" : " ";
            string congplc = chk_plc.Checked ? " and a.item_price < " + (cmb_wh.SelectedIndex == -1 ? "a.item_curcost" : "b.lcost") + " " : " ";
            string congrsp = cmb_sgrp.SelectedIndex != -1 ? " and a.sgroup='" + cmb_sgrp.SelectedValue + "'" : " ";
            string conall = checkBox1.Checked ? " " : " and (b.qty + b.openbal) <> 0 ";
            string conaball = chk_allbr.Checked ? " " : " where b.br_no='" + BL.CLS_Session.brno + "' ";
            string condopnbalonly = chk_zerobal.Checked ? " and (b.qty + b.openbal) = 0 " : " ";
            string condminslonly = chk_mins.Checked ? " and (b.qty + b.openbal) < 0 " : " ";
            string conit = string.IsNullOrEmpty(txt_code.Text) ? " " : " and a.item_no='" + txt_code.Text + "' ";
            string consup = string.IsNullOrEmpty(txt_supno.Text) ? " " : " and a.supno='" + txt_supno.Text + "' ";
            string consupc = " and a.item_cost<>0";
            string conaballwh = cmb_wh.SelectedIndex != -1 ?" and b.wh_no='" + cmb_wh.SelectedValue + "' " : " ";
          //  string query = "select a.item_no a1, a.item_name b1, b.qty + b.openbal c1,b.lcost d1, (b.qty + b.openbal) * b.lcost e1 from items a join whbins b on a.item_no=b.item_no and a.item_no + a.item_name like '%" + textBox2.Text + "%' " + conwh + conall +" ";
           // string query = "select a.item_no a1, a.item_name b1, b.qty + b.openbal c1,cast(a.item_unit as nvarchar) unit,round(a.item_curcost,2) d1,a.item_price price,round(((b.qty + b.openbal) * a.item_curcost),2) e1, round(((b.qty + b.openbal) * a.item_price),2) totalp from items a WITH (NOLOCK) join whbins b WITH (NOLOCK) on a.item_no=b.item_no " + conit + consup + conwh + conall + congrp + congrsp + condopnbalonly + conaball +"";
           // string query = "select a.item_no a1, a.item_name b1, b.qty + b.openbal c1,cast(a.item_unit as nvarchar) unit,round(a.item_curcost,2) d1,a.item_price price,round(((b.qty + b.openbal) * a.item_curcost),2) e1, round(((b.qty + b.openbal) * a.item_price),2) totalp,b.wh_no whno from items a WITH (NOLOCK) join whbins b WITH (NOLOCK) on a.item_no=b.item_no " + conit + consup + conwh + conall + congrp + congrsp + condopnbalonly + conaball + "";
           //// string query = "select a.item_no a1, a.item_name b1,case when a.item_cbalance > 0 then FLOOR(a.item_cbalance/a.uq2) else CEILING(a.item_cbalance/a.uq2) end trd,(c.unit_name + ' ' + cast(cast(a.uq2 as decimal(10,0)) as varchar)) shd,(a.item_cbalance%a.uq2) frt,a.item_cbalance totalq,round(a.item_curcost,2) d1,a.item_price price,round(((b.qty + b.openbal) * a.item_curcost),2) e1, round(((b.qty + b.openbal) * a.item_price),2) totalp,b.wh_no whno from items a WITH (NOLOCK) join units c WITH (NOLOCK) on a.item_unit=c.unit_id join whbins b WITH (NOLOCK) on a.item_no=b.item_no " + conit + consup + conwh + conall + congrp + congrsp + condopnbalonly + conaball + "";

           // string query = "select a.item_no a1, a.item_name b1,case when a.item_cbalance > 0 then FLOOR((b.qty + b.openbal)/a.uq2) else CEILING((b.qty + b.openbal)/a.uq2) end trd,(c.unit_name + ' ' + cast(cast(a.uq2 as decimal(10,0)) as varchar)) shd,((b.qty + b.openbal)%a.uq2) frt,(b.qty + b.openbal) totalq,round(a.item_curcost,2) d1,a.item_price price,round(((b.qty + b.openbal) * a.item_curcost),2) e1, round(((b.qty + b.openbal) * a.item_price),2) totalp,b.wh_no whno from items a WITH (NOLOCK) join units c WITH (NOLOCK) on a.item_unit=c.unit_id join whbins b WITH (NOLOCK) on a.item_no=b.item_no " + conit + consup + conwh + conall + congrp + congrsp + condopnbalonly + condminslonly + conaball + congplc + "";
            string query;
            //if (!BL.CLS_Session.multi_bc)
            //    query = "select a.item_no a1, a.item_name b1,0.00 trd,(c.unit_name + ' ' + cast(cast(a.uq2 as decimal(10,0)) as varchar)) shd,0.00 frt,[dbo].[get_balanace_for_item] (b.br_no,b.wh_no,a.item_no,0) totalq,round(a.item_curcost,2) d1,a.item_price price,0.00 e1,0.00 totalp,b.wh_no whno,a.uq2 from items a WITH (NOLOCK) join units c WITH (NOLOCK) on a.item_unit=c.unit_id join whbins b WITH (NOLOCK) on a.item_no=b.item_no " + conit + consup + conwh + conall + congrp + congrsp + condopnbalonly + condminslonly + conaball + congplc + consupc + "";
            //   // query = "select a.item_no a1, a.item_name b1,case when [dbo].[get_balanace_for_item] (b.br_no,b.wh_no,a.item_no,0) > 0 then FLOOR(([dbo].[get_balanace_for_item] (b.br_no,b.wh_no,a.item_no,0))/a.uq2) else CEILING(([dbo].[get_balanace_for_item] (b.br_no,b.wh_no,a.item_no,0))/a.uq2) end trd,(c.unit_name + ' ' + cast(cast(a.uq2 as decimal(10,0)) as varchar)) shd,(([dbo].[get_balanace_for_item] (b.br_no,b.wh_no,a.item_no,0))%a.uq2) frt,[dbo].[get_balanace_for_item] (b.br_no,b.wh_no,a.item_no,0) totalq,round(a.item_curcost,2) d1,a.item_price price,round((([dbo].[get_balanace_for_item] (b.br_no,b.wh_no,a.item_no,0)) * a.item_curcost),2) e1, round((([dbo].[get_balanace_for_item] (b.br_no,b.wh_no,a.item_no,0)) * a.item_price),2) totalp,b.wh_no whno from items a WITH (NOLOCK) join units c WITH (NOLOCK) on a.item_unit=c.unit_id join whbins b WITH (NOLOCK) on a.item_no=b.item_no " + conit + consup + conwh + conall + congrp + congrsp + condopnbalonly + condminslonly + conaball + congplc + consupc + "";
            //else
            //   // query = "select a.item_no a1, a.item_name b1,0 trd,(c.unit_name + ' ' + cast(cast(a.uq2 as decimal(10,0)) as varchar)) shd,0 frt,[dbo].[get_balanace_for_item] (b.br_no,b.wh_no,a.item_no,0) totalq,round(a.item_curcost,2) d1,a.item_price price,0.00 e1,0.00 totalp,b.wh_no whno from items a WITH (NOLOCK) join units c WITH (NOLOCK) on a.item_unit=c.unit_id join whbins b WITH (NOLOCK) on a.item_no=b.item_no " + conit + consup + conwh + conall + congrp + congrsp + condopnbalonly + condminslonly + conaball + congplc + consupc + "";
            //  // query = "select a.item_no a1, a.item_name b1,0.00 trd,(c.unit_name + ' ' + cast(cast(a.uq2 as decimal(10,0)) as varchar)) shd,0.00 frt,[dbo].[get_balanace_for_item] (b.br_no,b.wh_no,a.item_no,0) totalq,round(a.item_curcost,2) d1,a.item_price price,0.00 e1,0.00 totalp,b.wh_no whno,a.uq2 from items a WITH (NOLOCK) join units c WITH (NOLOCK) on a.item_unit=c.unit_id join whbins b WITH (NOLOCK) on a.item_no=b.item_no " + conit + consup + conwh + conall + congrp + congrsp + condopnbalonly + condminslonly + conaball + congplc + consupc + "";
            //query = "select a.item_no a1, a.item_name b1,case when a.item_cbalance > 0 then FLOOR((b.qty + b.openbal)/a.uq2) else CEILING((b.qty + b.openbal)/a.uq2) end trd,(c.unit_name + ' ' + cast(cast(a.uq2 as decimal(10,0)) as varchar)) shd,((b.qty + b.openbal)%a.uq2) frt,(b.qty + b.openbal) totalq,round(a.item_curcost,2) d1,a.item_price price,round(((b.qty + b.openbal) * a.item_curcost),2) e1, round(((b.qty + b.openbal) * a.item_price),2) totalp,b.wh_no whno,a.uq2 from items a WITH (NOLOCK) join units c WITH (NOLOCK) on a.item_unit=c.unit_id join whbins b WITH (NOLOCK) on a.item_no=b.item_no " + conit + consup + conwh + conall + congrp + congrsp + condopnbalonly + condminslonly + conaball + congplc + consupc + "";
           // query = "select a.item_no a1, a.item_name b1," + (BL.CLS_Session.multi_bc ? "0.00" : "case when a.item_cbalance > 0 then FLOOR((b.qty + b.openbal)/a.uq2) else CEILING((b.qty + b.openbal)/a.uq2) end") + " trd,(c.unit_name + ' ' + cast(cast(a.uq2 as decimal(10,0)) as varchar)) shd," + (BL.CLS_Session.multi_bc ? "0.00" : "((b.qty + b.openbal)%a.uq2)") + " frt," + (BL.CLS_Session.multi_bc ? "(b.qty + b.openbal)" : " [dbo].[get_balanace_for_item] (b.br_no,b.wh_no,a.item_no,0)") + " totalq,round((case when a.item_curcost>0 then a.item_curcost when a.item_curcost=0 then a.static_cost else a.item_curcost end),2) d1,a.item_price price,round(((b.qty + b.openbal) * a.item_curcost),2) e1, round(((b.qty + b.openbal) * a.item_price),2) totalp,b.wh_no whno,a.uq2 from items a WITH (NOLOCK) join units c WITH (NOLOCK) on a.item_unit=c.unit_id join whbins b WITH (NOLOCK) on a.item_no=b.item_no " + conit + consup + conwh + conall + congrp + congrsp + condopnbalonly + condminslonly + conaball + congplc + consupc + conaballwh + "";

            query = "select a.item_no a1, a.item_name b1," + (BL.CLS_Session.multi_bc ? "0.00" : "case when a.item_cbalance > 0 then FLOOR((b.qty + b.openbal)/a.uq2) else CEILING((b.qty + b.openbal)/a.uq2) end") + " trd,(c.unit_name + ' ' + cast(cast(a.uq2 as decimal(10,0)) as varchar)) shd," + (BL.CLS_Session.multi_bc ? "0.00" : "((b.qty + b.openbal)%a.uq2)") + " frt," + (BL.CLS_Session.multi_bc ? "(b.qty + b.openbal)" : " (b.qty + b.openbal) ") + " totalq," + constaticcst + " d1,a.item_price price,round(((b.qty + b.openbal) * " + (cmb_wh.SelectedIndex == -1 ? "a.item_curcost" : "b.lcost") + "),2) e1, round(((b.qty + b.openbal) * a.item_price),2) totalp,b.wh_no whno,a.uq2 from items a WITH (NOLOCK) join units c WITH (NOLOCK) on a.item_unit=c.unit_id join whbins b WITH (NOLOCK) on a.item_no=b.item_no " + conit + consup + conwh + conall + congrp + congrsp + condopnbalonly + condminslonly + conaball + congplc + consupc + conaballwh + "";
            dt = daml.SELECT_QUIRY_only_retrn_dt(query);
           // ok
           //// query = "select a.item_no a1, a.item_name b1,0.00 trd,(c.unit_name + ' ' + cast(cast(a.uq2 as decimal(10,0)) as varchar)) shd,0.00 frt,[dbo].[get_balanace_for_item] (b.br_no,b.wh_no,a.item_no,0) totalq,round(a.item_curcost,2) d1,a.item_price price,0.00 e1,0.00 totalp,b.wh_no whno,a.uq2 from items a WITH (NOLOCK) join units c WITH (NOLOCK) on a.item_unit=c.unit_id join whbins b WITH (NOLOCK) on a.item_no=b.item_no " + conit + consup + conwh + conall + congrp + congrsp + condopnbalonly + condminslonly + conaball + congplc + consupc + "";
           //// dt = daml.SELECT_QUIRY_only_retrn_dt(query);
           //////// DataTable dtc = daml.SELECT_QUIRY_only_retrn_dt("select count(*) from items a WITH (NOLOCK) join units c WITH (NOLOCK) on a.item_unit=c.unit_id join whbins b WITH (NOLOCK) on a.item_no=b.item_no " + conit + consup + conwh + conall + congrp + congrsp + condopnbalonly + condminslonly + conaball + congplc + consupc + "");
           //////// SqlCommand cmd = new SqlCommand("select a.item_no a1, a.item_name b1,0.00 trd,(c.unit_name + ' ' + cast(cast(a.uq2 as decimal(10,0)) as varchar)) shd,0.00 frt,[dbo].[get_balanace_for_item] (b.br_no,b.wh_no,a.item_no,0) totalq,round(a.item_curcost,2) d1,a.item_price price,0.00 e1,0.00 totalp,b.wh_no whno,a.uq2 from items a WITH (NOLOCK) join units c WITH (NOLOCK) on a.item_unit=c.unit_id join whbins b WITH (NOLOCK) on a.item_no=b.item_no " + conit + consup + conwh + conall + congrp + congrsp + condopnbalonly + condminslonly + conaball + congplc + consupc + "", BL.DAML.con);
           //////// BL.DAML.con.Open();
           //////// SqlDataReader reader = cmd.ExecuteReader();
           //////// int i = 1;
           //////// dt = new DataTable();
           ////////// dt.Columns.Add("a1"); dt.Columns.Add("b1"); dt.Columns.Add("trd"); dt.Columns.Add("shd"); dt.Columns.Add("frt"); dt.Columns.Add("totalq"); dt.Columns.Add("d1"); dt.Columns.Add("price"); dt.Columns.Add("e1"); dt.Columns.Add("totalp"); dt.Columns.Add("whno"); dt.Columns.Add("uq2");
           //////// progressBar1.Maximum = Convert.ToInt32(dtc.Rows[0][0]);

           ////////// SqlDataReader sdr = cmd.ExecuteReader();
           ////////// dataGridView1.DataSource = reader;

           //////// while (reader.Read())
           //////// {
           ////////   //  DataRow dataRow = dt.NewRow();
           ////////     progressBar1.Value = i;
           ////////     i++;
           ////////    //  dt.Rows.Add(dataRow);
           ////////      dt.Load(reader);
           //////// }
           //////// // var dataTable = new DataTable();
           ////////// dt.Load(reader);
           //////// BL.DAML.con.Close();
            DataRow dr = dt.NewRow();
            double sumopnd = 0, sumopnm = 0, sumtrd = 0, sumtrm = 0, sumttld = 0;//, sumttlm = 0;
            foreach (DataRow dtr in dt.Rows)
            {
                //if (!BL.CLS_Session.multi_bc)
                //{
                    dtr[8] = Convert.ToDouble(dtr[5]) * Convert.ToDouble(dtr[6]);
                    dtr[9] = Convert.ToDouble(dtr[5]) * Convert.ToDouble(dtr[7]);
                //}
                //else
                //{
                //    dtr[2] = Convert.ToDouble(dtr[5]) > 0 ? Math.Floor(Convert.ToDouble(dtr[5]) / Convert.ToDouble(dtr[11])) : Math.Ceiling(Convert.ToDouble(dtr[5]) / Convert.ToDouble(dtr[11]));
                //    dtr[4] = Convert.ToDouble(dtr[5]) % Convert.ToDouble(dtr[11]);
                //}
                sumopnd = sumopnd + Convert.ToDouble(dtr[5]);
                sumopnm = sumopnm + Convert.ToDouble(dtr[8]);
                sumtrd = sumtrd + Convert.ToDouble(dtr[9]);
                sumtrm = sumtrm + Convert.ToDouble(dtr[2]);
                sumttld = sumttld + Convert.ToDouble(dtr[4]);
                ////////DataRow[] res = dtunits.Select("unit_id ='" + dtr[3] + "'");
                ////////dtr[3] = res[0][1];

                    // row[6] = "%" + row[6];
              
               // sumtrm = sumtrm + Convert.ToDouble(dtr[9]);
                // sumttld = sumttld + Convert.ToDouble(dtr[6]);
                //  sumttlm = sumttlm + Convert.ToDouble(dtr[7]);
            }
            //  dr[0] = "";
            //  dr[1] = "";
            // dr[2] = "";
            // dr[3] = "";
            //  dr[4] = "";
            dr[1] = "الاجمالي";
            dr[2] = Math.Round(sumtrm, 2);
            dr[4] = Math.Round(sumttld, 2);
            dr[5] = Math.Round(sumopnd, 2);
            dr[8] = Math.Round(sumopnm, 2);
            dr[9] = Math.Round(sumtrd, 2);
           // dr[9] = Math.Round(sumtrm, 2);
           // dr[10] = true;
            // dr[11] = "";

            dt.Rows.Add(dr);

            dataGridView1.DataSource = dt;

            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
            //   dataGridView1.ClearSelection();
            dataGridView1.ClearSelection();

            wf.Close();
        }
        catch{wf.Close();}
           // total();
        }

        private void cmb_wh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_wh.SelectedIndex = -1;
            }
        }

        private void txt_supno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                // txt_name.Text = "";
                try
                {
                    Search_All f4 = new Search_All("7", "Sup");
                    f4.ShowDialog();
                    txt_supno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_supname.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                }
                catch { }

            }
        }

        private void txt_supno_Leave(object sender, EventArgs e)
        {
            DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select su_no ,su_name  from suppliers where su_brno='" + BL.CLS_Session.brno + "' and su_no='" + txt_supno.Text + "'");


            // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

            if (dt2.Rows.Count > 0)
            {
                txt_supname.Text = dt2.Rows[0][1].ToString();
                // txt_temp.Text = dt2.Rows[0][2].ToString();
                // txt_crlmt.Text = dt2.Rows[0][3].ToString();
            }
            else
            {
                txt_supname.Text = "";
                txt_supno.Text = "";
                // txt_crlmt.Text = "";
            }
        }

        private void txt_code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    // var selectedCell = dataGridView1.SelectedCells[0];
                    // do something with selectedCell...
                    Search_All f4 = new Search_All("2", "Sto");
                    f4.ShowDialog();



                    txt_code.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
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
                    DataTable dta = daml.SELECT_QUIRY_only_retrn_dt("select item_name,round(item_obalance,2) a_opnbal,item_no  from items where item_no='" + txt_code.Text + "'");
                    if (dta.Rows.Count > 0)
                    {
                        txt_name.Text = dta.Rows[0][0].ToString();
                        txt_code.Text = dta.Rows[0][2].ToString();
                        //  txt_opnbal.Text = dta.Rows[0][1].ToString();
                    }
                    else
                    {
                        //    MessageBox.Show("الحساب غير موجود");
                        txt_name.Text = "";
                        txt_code.Text = "";
                        //  txt_code.Text = dt.Rows[0][2].ToString();
                        //  txt_opnbal.Text = "0";
                    }

                }
            }
            catch { }
            
        }

        private void cmb_grp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_grp.SelectedIndex = -1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;
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

                //foreach (DataRow row in dt.Rows)
                //{/*
                //    DataView dv1 = dtunits.DefaultView;
                //    dv1.RowFilter = "unit_id ='" + row[2] +"'";
                //    DataTable dtNew = dv1.ToTable();
                //    //dcombo.DataSource = dtNew;
                //    row[2] = dtNew.Rows[0][1];
                //  * */
                //    DataRow[] res = dtunits.Select("unit_id ='" + row[2] + "'");
                //    row[2] = res[0][1];
                //    // row[6] = "%" + row[6];
                //}
            
                rf.reportViewer1.LocalReport.DataSources.Clear();
                // rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("acc_hdr", dthdr));
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("sdt", dt));

               // rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sto.rpt.Sto_balcost_rpt.rdlc";
                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sto.rpt.Sto_balcost_org_rpt.rdlc";

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                //  parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());
                parameters[1] = new ReportParameter("br_name", BL.CLS_Session.brname);
                //parameters[2] = new ReportParameter("type", cmb_type.Text);
                //parameters[3] = new ReportParameter("ref", txt_ref.Text);
                //parameters[4] = new ReportParameter("date", txt_mdate.Text);
                //parameters[5] = new ReportParameter("fwh_name", cmb_frwh.Text);

                //parameters[6] = new ReportParameter("desc", txt_desc.Text);
                //parameters[7] = new ReportParameter("twh_name", cmb_towh.Text);
                //parameters[8] = new ReportParameter("total", txt_total.Text);
                //parameters[9] = new ReportParameter("descount", txt_des.Text);
                //parameters[10] = new ReportParameter("tax", "0");
                //parameters[11] = new ReportParameter("nettotal", txt_total.Text);

                //BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(txt_total.Text), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);

                ////  MessageBox.Show(toWord.ConvertToArabic());
                //parameters[12] = new ReportParameter("a_toword", toWord.ConvertToArabic());

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

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                XcelApp.Application.Workbooks.Add(Type.Missing);



                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    // XcelApp.Cells[5, i] = dataGridView1.Columns[i - 1].Visible == true ? dataGridView1.Columns[i - 1].HeaderText : "";
                    if (dataGridView1.Columns[i - 1].Visible == true)
                        XcelApp.Cells[5, i] = "'" + dataGridView1.Columns[i - 1].HeaderText;
                    else
                        XcelApp.Cells[5, i] = null;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        //  XcelApp.Cells[i + 6, j + 1] = dataGridView1.Columns[j].Visible == true ? dataGridView1.Rows[i].Cells[j].Value.ToString() : "";
                        if (dataGridView1.Columns[j].Visible == true)
                            XcelApp.Cells[i + 6, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString().StartsWith("0") && dataGridView1.Rows[i].Cells[j].Value.ToString().Length>1 ? "'" + dataGridView1.Rows[i].Cells[j].Value.ToString() : dataGridView1.Rows[i].Cells[j].Value.ToString();
                        else
                            XcelApp.Cells[i + 6, j + 1] = null;
                    }
                }
                // XcelApp.Cells[0, 0] = "123";

                Microsoft.Office.Interop.Excel._Worksheet workSheet = XcelApp.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;

                // workSheet.Rows.Insert();
                /*
                workSheet.Cells[1, "H"] = "ID Number";
                workSheet.Cells[1, "J"] = "Current Balance";

                var row = 1;
                foreach (var acct in bankAccounts)
                {
                    row++;
                    workSheet.Cells[row, "H"] = acct.ID;
                    workSheet.Cells[row, "J"] = acct.Balance;
                }
                */
                workSheet.Cells[1, "D"] = BL.CLS_Session.cmp_name;
                workSheet.Cells[2, "D"] = BL.CLS_Session.brname;
                workSheet.Cells[3, "D"] = this.Text;

                workSheet.Range["C1", "F1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                workSheet.Range["C2", "F2"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                workSheet.Range["C3", "F3"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                workSheet.Range["A" + (dataGridView1.Rows.Count + 5).ToString(), "K" + (dataGridView1.Rows.Count + 5).ToString()].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                workSheet.Range["A5", "K5"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                // workSheet.Cells[2, "D"]

                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
        }

        private void cmb_sgrp_Enter(object sender, EventArgs e)
        {
            try
            {
                cmb_sgrp.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select group_id,group_name from groups where group_pid='" + cmb_grp.SelectedValue + "' and group_pid is not null");
                // metroComboBox3.DataSource = dt2;
                //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
                // metroComboBox3.DisplayMember = "a";
                //  comboBox1.ValueMember = comboBox1.Text;
                cmb_sgrp.DisplayMember = "group_name";
                cmb_sgrp.ValueMember = "group_id";
                
            }
            catch { }
        }

        private void cmb_grp_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_sgrp_Enter(sender, e);
            cmb_sgrp.SelectedIndex = -1;
        }

        private void cmb_sgrp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_sgrp.SelectedIndex = -1;
            }
        }

        private void chk_allbr_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_allbr.Checked)
            {
                cmb_wh.Enabled = false;
                cmb_wh.SelectedIndex = -1;

            }
            else
            {
                cmb_wh.Enabled = true;
            }
        }

        private void cmb_sgrp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

    }
}
