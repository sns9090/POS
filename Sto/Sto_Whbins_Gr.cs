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
    public partial class Sto_Whbins_Gr : BaseForm
    {
        SqlConnection con = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dtslctr, dt, dtgrp, dtunits;
        public Sto_Whbins_Gr()
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

            string sl = BL.CLS_Session.stkey.Replace(" ", "','").Remove(0, 2) + "'";
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
            try{
                string condwh = cmb_wh.SelectedIndex == -1 ? "" : " and c.wh_no='" + cmb_wh.SelectedValue + "' ";
            string condgrp = cmb_grp.SelectedIndex == -1 ? "" : " and i.item_group='" + cmb_grp.SelectedValue + "' ";
            string condsgrp = cmb_sgrp.SelectedIndex == -1 ? "" : " and i.sgroup='" + cmb_sgrp.SelectedValue + "' ";
            //  string condp = rad_posted.Checked ? " and posted=1 " : (rad_notpostd.Checked ? " and posted=0 " : " ");
            string condbr = chk_allbr.Checked ? "" : " and c.br_no='" + BL.CLS_Session.brno + "' ";
            string condopenbal = chk_opnbalonly.Checked ? "  c.openbal " : " c.qty + c.openbal ";
            //  string condf = txt_fno.Text != "" ? txt_fno.Text : "1";
            // string condt = txt_tno.Text != "" ? txt_tno.Text : "999999999";
            // string condft = " and ref between " + condf + " and " + condt + " ";

            // string condcn = !string.IsNullOrEmpty(txt_code.Text) ? " and custno='" + txt_code.Text + "'" : " ";
            // string conslctr = cmb_salctr.SelectedIndex != -1 ? " and slcenter = '" + cmb_salctr.SelectedValue.ToString() + "' " : " ";
            //  string condinvtype = cmb_type.SelectedIndex != -1 ? " and c.invtype = '" + cmb_type.SelectedValue.ToString() + "' " : " ";
            //  string qstr = "select slcenter sl_no,invtype a_type,ref a_ref,CONVERT(VARCHAR(10), CAST(invmdate as date), 105) a_mdate,CONVERT(VARCHAR(10), CAST(invhdate as date), 105) a_hdate,text a_text,invttl invttl,invdsvl invdsvl,tax_amt_rcvd tax,nettotal nettotal,posted a_p,invtype a_t,round(((invttl - invdsvl) - invcst),2) invwin,('%' + CAST(round((((invttl - invdsvl) - invcst)/(invttl - invdsvl))*100,2) as varchar)) invwinp  from sales_hdr where branch='" + BL.CLS_Session.brno + "' " + condp + usrname + condft + conslctr + " and " + cond + " between '" + txt_fdate.Text.Replace("-", "") + "' and '" + txt_tdate.Text.Replace("-", "") + "'";
            //
            //string qstr = "select slcenter sl_no,invtype a_type,ref a_ref,CONVERT(VARCHAR(10), CAST(invmdate as date), 105) a_mdate,CONVERT(VARCHAR(10), CAST(invhdate as date), 105) a_hdate,text a_text,invttl invttl,invdsvl invdsvl,tax_amt_rcvd tax,nettotal nettotal,posted a_p,invtype a_t,round(((invttl - invdsvl) - invcst),2) invwin,('%' + CAST(round((((invttl - invdsvl) - invcst)/(invttl - invdsvl))*100,2) as varchar)) invwinp  from sales_hdr where branch='" + BL.CLS_Session.brno + "' and custno='" + txt_code.Text + "' " + condp + usrname + condft + conslctr + " and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'";
            string qstr = "";

            //if(chk_allsalctr.Checked)
            //    qstr = "Select a.branch br_no,b.sl_name br_name, nettotal=round(sum(case when a.invtype in ('06','07') then a.nettotal-a.tax_amt_rcvd else -(a.nettotal-a.tax_amt_rcvd) end),2),tax=round(sum(case when a.invtype in ('06','07') then a.tax_amt_rcvd else -a.tax_amt_rcvd end),2),salcost = round(sum((case when invtype in ('06','07') then round(a.invcst,2) else - round(a.invcst,2) end)),2) ,win = round(sum((case when a.invtype in ('06','07') then round(((a.invttl - a.invdsvl) - a.invcst),2) else -round(((a.invttl - a.invdsvl) - a.invcst),2) end)),2),winp = ('%' + cast( round(sum(case when a.invtype in ('06','07') then ((a.invttl - a.invdsvl) - a.invcst)/(a.invttl - a.invdsvl)*100 else ((a.invttl - a.invdsvl) - a.invcst)/(a.invttl - a.invdsvl)*100 end),2)  as varchar))  from  sales_hdr a join slcenters b with (NOLOCK) on a.branch = b.sl_brno and a.slcenter=b.sl_no  where " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + condbr + condp + condinvtype + "  group By  a.branch,b.sl_name order By a.branch";

            //else
            //////if(cmb_grp.SelectedIndex == -1)
            //////    qstr = "Select a.item_group br_no,b.group_name br_name, nettotal=round(sum(case when c.invtype in ('06','07') then c.QTY*(c.price-(c.price*c.discpc/100)) else -(c.QTY*(c.price-(c.price*c.discpc/100))) end),2),tax=round(sum(case when c.invtype in ('06','07') then c.QTY else - c.QTY end),2),salcost = round(sum((case when c.invtype in ('06','07') then round(c.qty * c.pkqty * a.item_price,2) else - round(c.qty * c.pkqty * round(a.item_price,2),2) end)),2) ,win = 0.00 ,winp ='' from  items a join groups b on a.item_group = b.group_id join pu_dtl c on a.item_no=c.itemno  where " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + condbr + condinvtype + condgrp + condsgrp + "  group By  a.item_group,b.group_name order By a.item_group";
            //////else
            //////    qstr = "Select a.sgroup br_no,b.group_name br_name, nettotal=round(sum(case when c.invtype in ('06','07') then c.QTY*(c.price-(c.price*c.discpc/100)) else -(c.QTY*(c.price-(c.price*c.discpc/100))) end),2),tax=round(sum(case when c.invtype in ('06','07') then c.QTY else - c.QTY end),2),salcost = round(sum((case when c.invtype in ('06','07') then round(c.qty * c.pkqty * a.item_price,2) else - round(c.qty * c.pkqty * round(a.item_price,2),2) end)),2) ,win = 0.00 ,winp ='' from  items a join groups b on a.sgroup = b.group_id  join pu_dtl c on a.item_no=c.itemno  where " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + condbr + condinvtype + condgrp + condsgrp + "  group By a.sgroup,b.group_name order By a.sgroup";
            if (cmb_grp.SelectedIndex == -1)
                qstr = "select b.group_id br_no,b.group_name br_name, nettotal=round(sum((" + condopenbal + ") * i.item_curcost),2),tax=round(sum((" + condopenbal + ")),2),salcost =cast (round(sum((" + condopenbal + ") * i.item_price),2) as decimal(10,2) ),win = 0 ,winp ='' from  items i join groups b on i.item_group=b.group_id join whbins c on i.item_no=c.item_no where b.group_pid is null " + condbr + condgrp + condsgrp + condwh + " group By b.group_id,b.group_name order By b.group_id";
            else
                qstr = "Select i.sgroup br_no,b.group_name br_name, nettotal=round(sum((" + condopenbal + ") * i.item_curcost),2),tax=round(sum((" + condopenbal + ")),2),salcost =cast (round(sum((" + condopenbal + ") * i.item_price),2) as decimal(10,2)),win = 0 ,winp ='' from items i join groups b on i.sgroup = b.group_id  join whbins c on i.item_no=c.item_no  where b.group_pid is not null " + condbr + condgrp + condsgrp + condwh + "  group By i.sgroup,b.group_name order By i.sgroup";


            //////if (rd_day.Checked)
            //////{

            //////    qstr = "select REPLACE((SUBSTRING(CONVERT(varchar,(dateadd(DAY,0, datediff(day,0, invmdate))),111),1,10)),'/','-') as 'date',case (DATENAME ( weekday , [invmdate] )) when 'Monday' then  'الاثنين' when 'Tuesday' then 'الثلاثاء' when 'Wednesday' then 'الأربعاء' when 'Thursday' then 'الخميس' when 'Friday' then 'الجمعة' when 'Saturday' then 'السبت' when 'Sunday' then 'الأحد' end 'day',round(isnull((sum(invttl-invdsvl)),0),2) AS 'nettotal', round(sum(((invttl-invdsvl) - invcst)),2) as 'invwin' from sales_hdr WHERE invmdate between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' AND '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + conslctr + usrname + condp + condft + condinvtype + condcn + " group by dateadd(DAY,0, datediff(day,0, invmdate)),DATENAME ( weekday , [invmdate] ) ORDER BY dateadd(DAY,0, datediff(day,0, invmdate)),DATENAME ( weekday , [invmdate] ) ";
            //////}
            //////else
            //////{
            //////  //  qstr = "SELECT  cast(YEAR(invmdate) as varchar) +'-'+ cast(MONTH(invmdate) as varchar)  as  'date',case (MONTH(invmdate)) when '1' then  'يناير' when '2' then 'فبراير' when '3' then 'مارس' when '4' then 'ابريل' when '5' then 'مايو' when '6' then 'يونيو' when '7' then 'يوليو' when '8' then 'اغسطس' when '9' then 'سبتمبر' when '10' then 'اكتوبر' when '11' then 'نوفمبر' when '12' then 'ديسمبر' end 'day', round(isnull((sum(invttl-invdsvl)),0),2) AS 'nettotal',round(sum(((invttl-invdsvl) - invcst)),2) as 'invwin'  FROM sales_hdr WHERE invmdate between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' AND '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + conslctr + usrname + condp + condft + condinvtype + condcn + " GROUP BY YEAR(invmdate), MONTH(invmdate) ORDER BY YEAR(invmdate), MONTH(invmdate)";
            //////    qstr = "SELECT  cast(YEAR(invmdate) as varchar) +'-'+ cast(MONTH(invmdate) as varchar)  as  'date',case (MONTH(invmdate)) when '1' then  'يناير' when '2' then 'فبراير' when '3' then 'مارس' when '4' then 'ابريل' when '5' then 'مايو' when '6' then 'يونيو' when '7' then 'يوليو' when '8' then 'اغسطس' when '9' then 'سبتمبر' when '10' then 'اكتوبر' when '11' then 'نوفمبر' when '12' then 'ديسمبر' end 'day', round(isnull((sum(invttl-invdsvl)),0),2) AS 'nettotal',round(sum(((invttl-invdsvl) - invcst)),2) as 'invwin'  FROM sales_hdr WHERE invmdate between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' AND '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + conslctr + usrname + condp + condft + condinvtype + condcn + " GROUP BY YEAR(invmdate), MONTH(invmdate) ORDER BY YEAR(invmdate), MONTH(invmdate)";
            //////}
            /*
            if (!string.IsNullOrEmpty(txt_desc.Text))
            {
                qstr = qstr + " and text like '%" + txt_desc.Text + "%'";
            }

            if (!string.IsNullOrEmpty(txt_equl.Text))
            {
                //  MessageBox.Show(comboBox2.SelectedValue.ToString());
                qstr = qstr + " and invttl " + comboBox2.Text.Substring(0, 1) + txt_equl.Text;
            }
            */
            // dt = daml.SELECT_QUIRY_only_retrn_dt(qstr + " and invtype like '%" + cmb_type.SelectedValue + "%'");
            dt = daml.SELECT_QUIRY_only_retrn_dt(qstr);

            DataRow dr = dt.NewRow();
            double sumopnd = 0, sumopnm = 0, sumtrd = 0, sumtrm = 0, sumttld = 0, sumttlm = 0;
            foreach (DataRow dtr in dt.Rows)
            {
                sumopnd = sumopnd + Convert.ToDouble(dtr[2]);
                sumopnm = sumopnm + Convert.ToDouble(dtr[3]);
                sumtrd = sumtrd + Convert.ToDouble(dtr[4]);
                sumtrm = sumtrm + Convert.ToDouble(dtr[5]);
                // sumttld = ((Convert.ToDouble(dtr[2]) - Convert.ToDouble(dtr[4]) )/ (Convert.ToDouble(dtr[2]) *100)).ToString() ;
                //  sumttlm = sumttlm + Convert.ToDouble(dtr[13].ToString().Replace("%",""));
            }
            //  sumttlm = Math.Round((sumttld / (sumopnd - sumopnm)) * 100, 2);
            dr[0] = "  -  -    ";
            //  dr[1] = "";
            //  dr[2] = "";
            //  dr[3] = "";
            //  dr[4] = "";
            dr[1] = "الاجمالي";
            dr[2] = Math.Round(sumopnd, 2);
            dr[3] = Math.Round(sumopnm, 2);
            dr[4] = Math.Round(sumtrd, 2);
            dr[5] = Math.Round(sumtrm, 2);
            // dr[6] = "";
            // dr[6] = "%" + ((Math.Round(sumopnd, 2) - Math.Round(sumtrd, 2)) / (Math.Round(sumopnd, 2)) * 100 ).ToString(); //round(sum(case when a.invtype in ('06','07') then ((a.invttl - a.invdsvl) - a.invcst)/(a.invttl - a.invdsvl)*100 else ((a.invttl - a.invdsvl) - a.invcst)/(a.invttl - a.invdsvl)*100 end),2)
            // dr[11] = " ";

            // dr[12] = Math.Round(sumttld, 2);
            // dr[13] = "%" + sumttlm.ToString();

            //  dr[11] = "";

            dt.Rows.Add(dr);

            dataGridView1.DataSource = dt;

            foreach (DataGridViewRow dtrg in dataGridView1.Rows)
            {
                dtrg.Cells["Column6"].Value = Math.Round(((Convert.ToDouble(dtrg.Cells["Column5"].Value)) - (Convert.ToDouble(dtrg.Cells["Column3"].Value))), 2);
                dtrg.Cells["Column7"].Value = "%" + Math.Round(((Convert.ToDouble(dtrg.Cells["Column6"].Value)) / (Convert.ToDouble(dtrg.Cells["Column5"].Value)) * 100), 2).ToString();

                //  dtrg.Cells["Column7"].Value = "%" + Math.Round(((Convert.ToDouble(dtrg.Cells[2].Value) - Convert.ToDouble(dtrg.Cells[4].Value)) / (Convert.ToDouble(dtrg.Cells[2].Value)) * 100), 2).ToString();
                //  sumttlm = sumttlm + Convert.ToDouble(dtr[13].ToString().Replace("%",""));
            }

            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
            dataGridView1.ClearSelection();

            //   dataGridView1.ClearSelection();
            //  dataGridView1.ClearSelection();

            /*
            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(rw.Cells[10].Value) == false)
                    // rw.DefaultCellStyle.BackColor = Color.Thistle;
                    rw.DefaultCellStyle.ForeColor = Color.Red;
                // rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02")? "س قبض" : "س صرف";
                if (!string.IsNullOrEmpty(rw.Cells[1].Value.ToString()))
                    rw.Cells[1].Value = datval.validate_trtypes(rw.Cells[1].Value.ToString());
            }
            */
            string[] ary = { "Column6", "Column7" };
            datval.showwin_report(dataGridView1, ary);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sto.rpt.Sto_Report_Br_rpt.rdlc";



                // ReportParameter[] parameters = new ReportParameter[2];
                // parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                // parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));

                // rf.reportViewer1.LocalReport.SetParameters(parameters);
                ReportParameter[] parameters = new ReportParameter[8];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                parameters[1] = new ReportParameter("fmdate", "");
                parameters[2] = new ReportParameter("tmdate", "");
                parameters[3] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[4] = new ReportParameter("br_namet", dataGridView1.Columns[1].HeaderText);
                parameters[5] = new ReportParameter("br_no", dataGridView1.Columns[0].HeaderText);
                parameters[6] = new ReportParameter("var1", "كمية المخزون");
                parameters[7] = new ReportParameter("title", this.Text);

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
                            XcelApp.Cells[i + 6, j + 1] = "'" + dataGridView1.Rows[i].Cells[j].Value.ToString();
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

                workSheet.Range["A" + (dataGridView1.Rows.Count + 5).ToString(), "H" + (dataGridView1.Rows.Count + 5).ToString()].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                workSheet.Range["A5", "H5"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
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

    }
}
