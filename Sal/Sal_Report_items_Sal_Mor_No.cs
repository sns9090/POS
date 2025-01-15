using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using Microsoft.Reporting.WinForms;
using System.Globalization;

namespace POS.Sal
{
    public partial class Sal_Report_items_Sal_Mor_No : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dt, dtusers;
        //  DataTable dtt;
        public static int qq;
        // string typeno = "";
        SqlConnection con3 = BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public Sal_Report_items_Sal_Mor_No()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_count.Text))
            {
                MessageBox.Show("يجب ادخال عدد الاصناف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_count.Focus();
                return;
            }
            WaitForm wf = new WaitForm("جاري احتساب مبيعات الاصناف  .. يرجى الانتظار ...");
            wf.MdiParent = MdiParent;
            wf.Show();
            Application.DoEvents();
            try
            {
                /*
                if (typeno == "")
                {
                    string xxx;
                    xxx = maskedTextBox1.Text.ToString();

                    string zzz;
                    zzz = maskedTextBox2.Text.ToString();
                    SqlDataAdapter da3 = new SqlDataAdapter("select [ref] ,[contr] ,[type],[date] ,[total] ,[count] ,[payed],[total_cost],[saleman] ,[req_no] ,[cust_no],[discount],[net_total] from sales_hdr where date between'" + xxx + "' and '" + zzz + "'", con3);
                    SqlDataAdapter da4 = new SqlDataAdapter("select sum(total),sum(total_cost),sum(discount),sum(net_total) from sales_hdr where date between'" + xxx + "' and '" + zzz + "'", con3);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3, "0");
                    da4.Fill(ds3, "1");
                    dataGridView1.DataSource = ds3.Tables["0"];
                    txt_total.Text = ds3.Tables[1].Rows[0][0].ToString();
                    txt_cost.Text = ds3.Tables[1].Rows[0][1].ToString();


                    textBox1.Text = ds3.Tables[1].Rows[0][2].ToString();
                    textBox2.Text = ds3.Tables[1].Rows[0][3].ToString();

                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    dtt = ds3.Tables["0"];
                }
                else
                {// and type = " + typeno + "
                    string xxx;
                    xxx = maskedTextBox1.Text.ToString();

                    string zzz;
                    zzz = maskedTextBox2.Text.ToString();
                    SqlDataAdapter da3 = new SqlDataAdapter("select [ref] ,[contr] ,[type],[date] ,[total] ,[count] ,[payed],[total_cost],[saleman] ,[req_no] ,[cust_no],[discount],[net_total] from sales_hdr where date between'" + xxx + "' and '" + zzz + "' and type = " + typeno + "", con3);
                    SqlDataAdapter da4 = new SqlDataAdapter("select sum(total),sum(total_cost),sum(discount),sum(net_total) from sales_hdr where date between'" + xxx + "' and '" + zzz + "' and type = " + typeno + "", con3);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3, "0");
                    da4.Fill(ds3, "1");
                    dataGridView1.DataSource = ds3.Tables["0"];
                    txt_total.Text = ds3.Tables[1].Rows[0][0].ToString();
                    txt_cost.Text = ds3.Tables[1].Rows[0][1].ToString();

                    textBox1.Text = ds3.Tables[1].Rows[0][2].ToString();
                    textBox2.Text = ds3.Tables[1].Rows[0][3].ToString();

                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    dtt = ds3.Tables["0"];

                }
                */
               //// string usrname = cmb_user.SelectedIndex != -1 ? " and sales_hdr.usrid = '" + cmb_user.SelectedValue.ToString() + "' " : " ";
              ////////  string cond = checkBox1.Checked ? "c.invhdate" : "c.invmdate";
              ////////  string condp = rad_posted.Checked ? " and c.posted=1 " : (rad_notpostd.Checked ? " and c.posted=0 " : " ");

              ////////  string congrp = cmb_grp.SelectedIndex != -1 ? " and items.item_group='" + cmb_grp.SelectedValue + "' " : " ";
              ////////  string congrsp = cmb_sgrp.SelectedIndex != -1 ? " and items.sgroup='" + cmb_sgrp.SelectedValue + "' " : " ";

              ////////  string condf = txt_fno.Text != "" ? txt_fno.Text : "1";
              ////////  string condt = txt_tno.Text != "" ? txt_tno.Text : "999999999";
              ////////  string condft = " and c.ref between " + condf + " and " + condt + " ";
              ////////  string condfcst = txt_code.Text != "" ? " and c.custno='" + txt_code.Text + "' " : " ";
              ////////////  string conditem = txt_itemno.Text != "" ? " where items.item_no='" + txt_itemno.Text + "' " : " where items.item_no like '%" + txt_itemno.Text + "%' ";
              //////// // string conddate = " and c.invmdate '" + txt_code.Text + "' " : " ";

              ////////  string conslctr = cmb_salctr.SelectedIndex != -1 ? " and c.slcenter = '" + cmb_salctr.SelectedValue.ToString() + "' " : " ";
              ////////  string consltyp = cmb_type.SelectedIndex != -1 ? " and c.invtype = '" + cmb_type.SelectedValue.ToString() + "' " : " ";
              ////////  string conslord = rd_more.Checked ? " Desc " : " ASC ";
            string cond = checkBox1.Checked ? "sales_dtl.invhdate" : "sales_dtl.invmdate";
            string condp = rad_posted.Checked ? " and sales_dtl.posted=1 " : (rad_notpostd.Checked ? " and sales_dtl.posted=0 " : " ");

            string congrp = cmb_grp.SelectedIndex != -1 ? " and items.item_group='" + cmb_grp.SelectedValue + "' " : " ";
            string congrsp = cmb_sgrp.SelectedIndex != -1 ? " and items.sgroup='" + cmb_sgrp.SelectedValue + "' " : " ";

            string condf = txt_fno.Text != "" ? txt_fno.Text : "1";
            string condt = txt_tno.Text != "" ? txt_tno.Text : "999999999";
            string condft = " and sales_dtl.ref between " + condf + " and " + condt + " ";
            string condfcst = txt_code.Text != "" ? " and sales_dtl.custno='" + txt_code.Text + "' " : " ";
            ////  string conditem = txt_itemno.Text != "" ? " where items.item_no='" + txt_itemno.Text + "' " : " where items.item_no like '%" + txt_itemno.Text + "%' ";
            // string conddate = " and c.invmdate '" + txt_code.Text + "' " : " ";

            string conslctr = cmb_salctr.SelectedIndex != -1 ? " and sales_dtl.slcenter = '" + cmb_salctr.SelectedValue.ToString() + "' " : " ";
            string consltyp = cmb_type.SelectedIndex != -1 ? " and sales_dtl.invtype = '" + cmb_type.SelectedValue.ToString() + "' " : " ";
            string conslord = rd_more.Checked ? " Desc " : " ASC ";
                //  string qstr = "select slcenter sl_no,invtype a_type,ref a_ref,CONVERT(VARCHAR(10), CAST(invmdate as date), 105) a_mdate,CONVERT(VARCHAR(10), CAST(invhdate as date), 105) a_hdate,text a_text,invttl invttl,invdsvl invdsvl,tax_amt_rcvd tax,nettotal nettotal,posted a_p,invtype a_t,round(((invttl - invdsvl) - invcst),2) invwin,('%' + CAST(round((((invttl - invdsvl) - invcst)/(invttl - invdsvl))*100,2) as varchar)) invwinp  from sales_hdr where branch='" + BL.CLS_Session.brno + "' " + condp + usrname + condft + conslctr + " and " + cond + " between '" + txt_fdate.Text.Replace("-", "") + "' and '" + txt_tdate.Text.Replace("-", "") + "'";
              /*  string qstr = "select a.item_no item_no,a.item_name item_name,( select sum(b.qty) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and b.type in ('04','05') where a.item_no=b.itemno) sal_qty"
                            +" ,( select sum(b.qty) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and b.type in ('14','15') where a.item_no=b.itemno) rsal_qty"
                            +" 0 net_sal_qty"
                            +" 0 net_qty_price"
                            +" 0 item_win"
                            +" 0 a_p"
                            +" 0 a_t"
                            +" 0 item_winp"
                            +"  from items a ";
                */
////////                string qstr = "Select Top " + txt_count.Text + @" items.item_no,items.item_name ,slqty=sum(case when sales_dtl.invtype in ('04','05') then qty * pkqty  else -( qty * pkqty ) end),
////////                              slprice=sum(case when sales_dtl.invtype in ('04','05') then sales_dtl.qty * pkqty * (sales_dtl.price-(sales_dtl.price*sales_dtl.discpc/100)) else -(sales_dtl.qty* pkqty * (sales_dtl.price-(sales_dtl.price*sales_dtl.discpc/100))) end) ,
////////                              slcost=sum(case when sales_dtl.invtype in ('04','05') then sales_dtl.qty * pkqty * (sales_dtl.cost-(sales_dtl.cost*sales_dtl.discpc/100)) else -(sales_dtl.qty* pkqty * (sales_dtl.cost-(sales_dtl.cost*sales_dtl.discpc/100))) end),0 win  ,
////////                              supno,supno supno2 FROM  items  (NOLOCK)  inner Join  sales_dtl  (NOLOCK) inner join sales_hdr (NOLOCK)  on sales_hdr.branch=sales_dtl.branch and sales_hdr.ref=sales_dtl.ref and sales_hdr.invtype=sales_dtl.invtype and sales_hdr.slcenter=sales_dtl.slcenter
////////                              on items.item_no=sales_dtl.itemno where  1<>0 and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and sales_dtl.branch='" + BL.CLS_Session.brno + "' " + condft + congrp + congrsp + condp + condfcst + conslctr + consltyp + " group by items.item_no,items.item_name,item_cost,supno ORDER BY 1 "+conslord+"";

////////                string qstr = "Select Top " + txt_count.Text + @" items.item_no,items.item_name ,slqty=sum(case when b.invtype in ('04','05') then qty * pkqty  else -( qty * pkqty ) end),
////////                              slprice=sum(case when b.invtype in ('04','05') then b.qty * pkqty * (case when c.with_tax=1 then (((b.price-(b.tax_amt/(b.qty * b.pkqty))-(b.offer_amt/(b.qty * b.pkqty)))) - (((b.price-(b.tax_amt/(b.qty * b.pkqty))-(b.offer_amt/(b.qty * b.pkqty)))) * c.invdspc/100)) else ((b.price) - ((b.price) * c.invdspc/100)) end ) else -( b.qty * pkqty * (case when c.with_tax=1 then (((b.price-(b.tax_amt/(b.qty * b.pkqty))-(b.offer_amt/(b.qty * b.pkqty)))) - (((b.price-(b.tax_amt/(b.qty * b.pkqty))-(b.offer_amt/(b.qty * b.pkqty)))) * c.invdspc/100)) else ((b.price) - ((b.price) * c.invdspc/100)) end) ) end) ,
////////                              slcost=sum(case when b.invtype in ('04','05') then b.qty * pkqty * (b.cost) else -(b.qty * b.pkqty * b.cost) end),0 win  ,
////////                              supno,supno supno2 FROM  items  (NOLOCK)  inner Join  sales_dtl b (NOLOCK) inner join sales_hdr c (NOLOCK)  on c.branch=b.branch and c.ref=b.ref and c.invtype=b.invtype and c.slcenter=b.slcenter
////////                              on items.item_no=b.itemno where  1<>0 and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and b.branch='" + BL.CLS_Session.brno + "' " + condft + congrp + congrsp + condp + condfcst + conslctr + consltyp + " group by items.item_no,items.item_name,item_cost,supno ORDER BY 1 " + conslord + "";
            string qstr = @"Select Top " + txt_count.Text + @" isnull(sales_dtl.itemno,' ') as item_no,rtrim(rTrim(items.item_Name)) as item_name,sum(sales_dtl.qty*sales_dtl.pkqty *(case when sales_hdr.invtype in('04','05') then 1 else 0 end)) 
                             - sum(sales_dtl.qty*sales_dtl.pkqty *(case when sales_hdr.invtype in('14','15') then 1 else 0 end)) slqty
                             , (sum(((qty*(price-iif(1=1,(price*discpc/100),floor(price*discpc/100))))-offer_amt-(case when sales_hdr.with_tax=0 then 0 else tax_amt end))*IIF(sales_hdr.invtype in('04','05'),1,0))-sum(((qty*(price-iif(1=1,(price*discpc/100),floor(price*discpc/100))))-offer_amt-(case when sales_hdr.with_tax=0 then 0 else tax_amt end))*IIF(sales_hdr.invtype in('14','15'),1,0)))  slprice
                             , isnull(sum(sales_dtl.qty*sales_dtl.pkqty *(case when sales_hdr.invtype in('04','05') then 1 else 0 end)*item_curcost) 
                             - sum(sales_dtl.qty*sales_dtl.pkqty *(case when sales_hdr.invtype in('14','15') then 1 else 0 end)*item_curcost),0.00) slcost ,isnull(sum(((qty*pkqty*(price-iif(1=1,(price*discpc/100),floor(price*discpc/100)))-item_curcost)-offer_amt-(case when sales_hdr.with_tax=0 then 0 else tax_amt end))*IIF(sales_hdr.invtype in('04','05'),1,0)),0) as win,isnull(supno,'') supno,isnull(supno,'') supno2
                             from sales_hdr inner Join sales_dtl LEFT OUTER JOIN items  ON sales_dtl.itemno=items.item_no on sales_hdr.branch=sales_dtl.branch and sales_hdr.invtype=sales_dtl.invtype and sales_hdr.ref=sales_dtl.ref and sales_hdr.slcenter=sales_dtl.slcenter
                             where  1<>0 and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and sales_dtl.branch='" + BL.CLS_Session.brno + "' " + condft + congrp + congrsp + condp + condfcst + conslctr + consltyp +
                             @" GROUP BY sales_dtl.itemno,rtrim(rTrim(items.item_Name)),supno order By (sum(sales_dtl.qty*sales_dtl.pkqty *(case when sales_hdr.invtype in('04','05') then 1 else 0 end)) 
                             - sum(sales_dtl.qty*sales_dtl.pkqty *(case when sales_hdr.invtype in('14','15') then 1 else 0 end))) " + conslord + "";
                    //////string qstr = "select a.item_no item_no,a.item_name item_name,(select isnull(sum(b.qty * b.pkqty),0) from sales_dtl b join sales_hdr c on c.branch=b.branch and c.ref=b.ref and c.invtype=b.invtype and b.invtype in ('04','05') " + condft + condp + condfcst + " and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' where a.item_no=b.itemno) sal_qty"
                //////           + " ,(select isnull(sum(b.qty * b.pkqty),0) from sales_dtl b join sales_hdr c on c.branch=b.branch and c.ref=b.ref and c.invtype=b.invtype and b.invtype in ('14','15') " + condft + condp + condfcst + " and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' where a.item_no=b.itemno) rsal_qty"
                //////           + " ,0 net_sal_qty"
                //////           + " ,round(((select isnull(sum(b.qty * b.pkqty * b.price),0) from sales_dtl b join sales_hdr c on c.branch=b.branch and c.ref=b.ref and c.invtype=b.invtype and b.invtype in ('04','05') " + condft + condp + condfcst + " and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' where a.item_no=b.itemno) - (select isnull(sum(b.qty * b.pkqty * b.price),0) from sales_dtl b join sales_hdr c on c.branch=b.branch and c.ref=b.ref and c.invtype=b.invtype and b.invtype in ('14','15') " + condft + condp + condfcst + " and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' where a.item_no=b.itemno)),2) net_qty_price"
                //////           + " , 0 item_win,'0' item_winp, a.item_price price, a.item_curcost cost from items a  " + conditem + congrp + congrsp + "";
                         //  +" where cast((select isnull(sum(b.qty),0) from sales_dtl b join sales_hdr c on c.branch=b.branch and c.ref=b.ref and c.invtype=b.invtype and b.invtype in ('04','05') where a.item_no=b.itemno) as float) <> 0"
                        //   + " and cast((select isnull(sum(b.qty),0) from sales_dtl b join sales_hdr c on c.branch=b.branch and c.ref=b.ref and c.invtype=b.invtype and b.invtype in ('14','15') where a.item_no=b.itemno) as float) <> 0";
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
              //  dt = daml.SELECT_QUIRY_only_retrn_dt(qstr + " and invtype like '%" + cmb_type.SelectedValue + "%'");
                dt = daml.SELECT_QUIRY_only_retrn_dt(qstr);

                DataRow dr = dt.NewRow();
                CultureInfo culture = new CultureInfo("en-US");
                decimal sumtrd = 0;
                double sumopnd = 0, sumopnm = 0, sumtrm = 0, sumttld = 0, sumttlm = 0, sumttlm2 = 0, sumttlm3 = 0;
                foreach (DataRow dtr in dt.Rows)
                {
                    sumopnd = sumopnd + Convert.ToDouble(dtr[2]);
                    sumopnm = sumopnm + Convert.ToDouble(dtr[3]);
                    sumtrd = sumtrd + Convert.ToDecimal(dtr[4],culture);
                    sumtrm = sumtrm + Convert.ToDouble(dtr[5]);
                   // sumttld = sumttld + Convert.ToDouble(dtr[6]);
                  //  sumttlm2 = sumttlm2 + Convert.ToDouble(dtr[8]);
                   // sumttlm3 = sumttlm3 + Convert.ToDouble(dtr[9]);
                }
               // sumttlm = (sumttld / (sumtrm)) * 100;
                dr[0] = "";
                //  dr[1] = "";
                //  dr[2] = "";
                //  dr[3] = "";
                //  dr[4] = "";
                //dr[1] = "الاجمالي";
                //dr[2] = Math.Round(sumopnd, 2);
                //dr[3] = Math.Round(sumopnm, 2);
                //dr[4] = Math.Round(sumtrd, 2);
                //dr[5] = Math.Round(sumtrm, 2);
                //dr[6] = Math.Round(sumttld, 2);
                //dr[7] = Math.Round(sumttlm, 2);
                dr[1] = "الاجمالي";
                dr[2] = Math.Round(sumopnd,2);
                dr[3] = Math.Round(sumopnm,2);
                dr[4] = Math.Round(sumtrd,2);
                dr[5] = Math.Round(sumtrm,2);
                dr[6] = "";
                dr[7] = "";
                // dr[11] = " ";

                //  dr[12] = Math.Round(sumttld, 2);
              //  dr[7] = sumttlm.ToString();

                //  dr[11] = "";
               // dr[8] = sumttlm2;
               // dr[9] = sumttlm3;

                dt.Rows.Add(dr);
                
                dataGridView1.DataSource = dt;

              

                //   dataGridView1.ClearSelection();
                //  dataGridView1.ClearSelection();
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {

                    dataGridView1.Rows[i].Cells[5].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value) - Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                  //  dataGridView1.Rows[i].Cells[5].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString().Trim()) * Convert.ToDouble(dataGridView1.Rows[i].Cells[10].Value.ToString().Trim());
                  //  dataGridView1.Rows[i].Cells[6].Value = (Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString().Trim()) * Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value.ToString().Trim())) - (Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString().Trim()) * Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value.ToString().Trim()));
                  //////dataGridView1.Rows[i].Cells[6].Value = (Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString().Trim()))  - ((Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString().Trim()) * Convert.ToDouble(dataGridView1.Rows[i].Cells[9].Value.ToString().Trim())));// / (Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString().Trim()) * Convert.ToDouble(dataGridView1.Rows[i].Cells[10].Value.ToString().Trim())) * 100;
                  ////// if (Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value) != 0 )
                  //////  //   if (Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value) != 0 && !dataGridView1.Rows[i].Cells[7].Value.ToString().Contains("%"))
                  //////// if (Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) != 0 && Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value) != 0)
                  ////// {
                  //////     double sumpz=Math.Round((Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value) / Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value)) * 100,2);
                  //////     dataGridView1.Rows[i].Cells[7].Value = "%" + sumpz.ToString();
                  ////// }
                    DataTable dtsn = daml.SELECT_QUIRY_only_retrn_dt("select su_name from suppliers where su_brno='" + BL.CLS_Session.brno + "' and  su_no='" + dataGridView1.Rows[i].Cells[7].Value.ToString().Trim() + "'");
                    
                    dataGridView1.Rows[i].Cells[6].Value =dtsn.Rows.Count>0? dtsn.Rows[0][0] : "" ;
                 
                   /*  
                  //  DataGridViewRow dr;// = dt.NewRow();
                    double sumopnd = 0, sumopnm = 0, sumtrd = 0, sumtrm = 0, sumttld = 0, sumttlm = 0;
                    foreach (DataRow dtr in dataGridView1.Rows)
                    {
                        sumopnd = sumopnd + Convert.ToDouble(dtr[2]);
                        sumopnm = sumopnm + Convert.ToDouble(dtr[3]);
                        sumtrd = sumtrd + Convert.ToDouble(dtr[4]);
                        sumtrm = sumtrm + Convert.ToDouble(dtr[5]);
                        sumttld = sumttld + Convert.ToDouble(dtr[6]);
                        // sumttlm = sumttlm + Convert.ToDouble(dtr[7]);
                    }
                    sumttlm = (sumttld / (sumtrm)) * 100;
                    //  dr[0] = "";
                    //  dr[1] = "";
                    //  dr[2] = "";
                    //  dr[3] = "";
                    //  dr[4] = "";
                    //dr[1] = "الاجمالي";
                    //dr[2] = Math.Round(sumopnd, 2);
                    //dr[3] = Math.Round(sumopnm, 2);
                    //dr[4] = Math.Round(sumtrd, 2);
                    //dr[5] = Math.Round(sumtrm, 2);
                    //dr[6] = Math.Round(sumttld, 2);
                    //dr[7] = Math.Round(sumttlm, 2);
                     /*
                    dr[1] = "الاجمالي";
                    dr[2] = sumopnd;
                    dr[3] = sumopnm;
                    dr[4] = sumtrd;
                    dr[5] = sumtrm;
                    dr[6] = sumttld.ToString();
                    //  dr[7] = sumttlm;
                    // dr[11] = " ";

                    //  dr[12] = Math.Round(sumttld, 2);
                    dr[7] = "%" + sumttlm.ToString();
                     */
                   //  dr[11] = "";

                  // dataGridView1.Rows.Add("", "الاجمالي", sumopnd.ToString(), sumopnm.ToString(), sumtrd.ToString(), sumtrm.ToString(), "%" + sumttlm.ToString());
                  // dataGridView1.Rows.Add(dr);

                   dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                   dataGridView1.ClearSelection();
                  //  else
                    //    dataGridView1.Rows.Remove(dataGridView1.Rows[i]);  
                    //  dataGridView1.Rows[i].Cells[0].Value = seso > 0 ? seso.ToString() : 0.ToString();
                    //   dataGridView1.Rows[i].Cells[1].Value = seso < 0 ? seso.ToString() : 0.ToString();

                    //  seso = seso + Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString().Trim()) - Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value.ToString().Trim());
                    //dataGridView1.Rows[i].Cells[0].Value = seso.ToString();
                    // dataGridView1.Rows[i].Cells[0].Value = seso > 0 ? seso.ToString() : 0.ToString();
                    //   dataGridView1.Rows[i].Cells[1].Value = seso < 0 ? seso.ToString() : 0.ToString();

                }

                /*
                for (int ii = 0; ii < dataGridView1.Rows.Count; ++ii)
                {

                    if (Convert.ToDouble(dataGridView1.Rows[ii].Cells[5].Value) != 0)
                      //  dataGridView1.Rows[ii].Cells[7].Value = (Convert.ToDouble(dataGridView1.Rows[ii].Cells[6].Value) / Convert.ToDouble(dataGridView1.Rows[ii].Cells[5].Value)) * 100;
                        dataGridView1.Rows[ii].Cells[7].Value = (((Convert.ToDouble(dataGridView1.Rows[ii].Cells[4].Value.ToString().Trim()) * Convert.ToDouble(dataGridView1.Rows[ii].Cells[10].Value.ToString().Trim())) - (Convert.ToDouble(dataGridView1.Rows[ii].Cells[4].Value.ToString().Trim()) * Convert.ToDouble(dataGridView1.Rows[ii].Cells[11].Value.ToString().Trim()))) / (Convert.ToDouble(dataGridView1.Rows[ii].Cells[4].Value.ToString().Trim()) * Convert.ToDouble(dataGridView1.Rows[ii].Cells[10].Value.ToString().Trim()))) * 100;
                   
                    else
                        dataGridView1.Rows.Remove(dataGridView1.Rows[ii]);

                }
                */
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
                string[] ary = { "Column7", "Column4" };
                datval.showwin_report(dataGridView1, ary);
                wf.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                wf.Close();
            }
        }

        private void SalesReport_Load(object sender, EventArgs e)
        {
            txt_fdate.Text = BL.CLS_Session.start_dt;
            txt_tdate.Text = BL.CLS_Session.end_dt;
            /*
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";

            var items = new[] { new { Text = "قيد عام", Value = "01" }, new { Text = "سند قبض", Value = "02" }, new { Text = "سند صرف", Value = "03" } };
            comboBox1.DataSource = items;
            comboBox1.SelectedIndex = -1;
            */
            string tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
            //   dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('04','05','14','15')");
            cmb_type.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('04','05','14','15')");
            cmb_type.DisplayMember = "tr_name";
            cmb_type.ValueMember = "tr_no";
            cmb_type.SelectedIndex = -1;

            string sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";
            // MessageBox.Show(sl);

            cmb_salctr.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select * from slcenters where sl_brno + sl_no in(" + sl + ") and sl_brno='" + BL.CLS_Session.brno + "'");
            cmb_salctr.DisplayMember = "sl_name";
            cmb_salctr.ValueMember = "sl_no";
            cmb_salctr.SelectedIndex = -1;

            dtusers = daml.SELECT_QUIRY_only_retrn_dt("select user_name,full_name from users");

            cmb_user.DataSource = dtusers;
            cmb_user.DisplayMember = "full_name";
            cmb_user.ValueMember = "user_name";
            cmb_user.SelectedIndex = -1;


          
            // DataTable dt = new DataTable();
            // da.Fill(dt);


            cmb_grp.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select group_id,group_name from groups where group_pid is null");
            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
            cmb_grp.DisplayMember = "group_name";
            cmb_grp.ValueMember = "group_id";
            cmb_grp.SelectedIndex = -1;
            /*
            comboBox2.DisplayMember = "Text";
            comboBox2.ValueMember = "Value";
            var items2 = new[] { new { Text = ">", Value = ">" }, new { Text = "=", Value = "=" }, new { Text = "<", Value = "<" } };
            comboBox2.DataSource = items2;
            comboBox2.SelectedIndex = -1;
            */
            /*
            maskedTextBox1.Text = DateTime.Now.ToString("yyyy-MM-01") + "05:00:00";

            DateTime dt = new DateTime();

            dt=DateTime.Now.AddDays(1);

           // maskedTextBox2.Text = DateTime.Now.ToString("yyyy-MM-dd") + "05:00:00";
            maskedTextBox2.Text = dt.ToString("yyyy-MM-dd") + "05:00:00";
             * */
            //  string temy1 =
            //  txt_fdate.Text = DateTime.Now.ToString("yyyy-01-01", new CultureInfo("en-US"));

            // string temy2=
            //  txt_tdate.Text = DateTime.Now.ToString("yyyy-12-31", new CultureInfo("en-US"));
            txt_code.Select();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            /*
            if (e.KeyCode == Keys.F5)
            {
                // int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                DilySalesReport.cc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                // if (selectedIndex > -1)
                // {
                //dataGridView1.Rows.RemoveAt(selectedIndex);
                //dataGridView1.Refresh(); // if needed


                // }
                SalesDtlReport sdtr = new SalesDtlReport();
                //MAIN mn = new MAIN();
                //sdtr.MdiParent = mn;

                sdtr.ShowDialog();


            }
             * */
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            
                // int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                DilySalesReport.cc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                // if (selectedIndex > -1)
                // {
                //dataGridView1.Rows.RemoveAt(selectedIndex);
                //dataGridView1.Refresh(); // if needed


                // }
                SalesDtlReport sdtr = new SalesDtlReport();
                //MAIN mn = new MAIN();
                //sdtr.MdiParent = mn;

                sdtr.ShowDialog();
            */


        }

        private void label3_Click(object sender, EventArgs e)
        {

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
                        XcelApp.Cells[6, i] = "'" + dataGridView1.Columns[i - 1].HeaderText;
                    else
                        XcelApp.Cells[6, i] = null;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        // XcelApp.Cells[i + 6, j + 1] = dataGridView1.Rows[i].Cells[j].Visible == true ? dataGridView1.Rows[i].Cells[j].Value.ToString() : "";
                        if (dataGridView1.Columns[j].Visible == true)
                            //XcelApp.Cells[i + 7, j + 1] = "'" + dataGridView1.Rows[i].Cells[j].Value.ToString();
                            XcelApp.Cells[i + 7, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString().StartsWith("0") && dataGridView1.Rows[i].Cells[j].Value.ToString().Length > 1 ? "'" + dataGridView1.Rows[i].Cells[j].Value.ToString() : dataGridView1.Rows[i].Cells[j].Value.ToString();
                        else
                            XcelApp.Cells[i + 7, j + 1] = null;
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
                workSheet.Cells[2, "F"] = BL.CLS_Session.cmp_name;
                workSheet.Cells[3, "F"] = BL.CLS_Session.brname;
                workSheet.Cells[4, "F"] = this.Text + "  " + "من" + "  " + txt_fdate.Text + "  " + "الى" + "  " + txt_tdate.Text;

                workSheet.Range["E2", "G2"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                workSheet.Range["E3", "G3"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                workSheet.Range["E4", "G4"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                workSheet.Range["E" + (dataGridView1.Rows.Count + 6).ToString(), "H" + (dataGridView1.Rows.Count + 6).ToString()].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                workSheet.Range["A6", "H6"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                // workSheet.Cells[2, "D"]

                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
                /*
                if (dataGridView1.Rows.Count > 0)
                {
                    Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                    XcelApp.Application.Workbooks.Add(Type.Missing);

                    for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                    {
                        XcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                    }

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    XcelApp.Columns.AutoFit();
                    XcelApp.Visible = true;
                }
                 * */
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            // dataGridView1_CellContentDoubleClick((object)sender, (DataGridViewCellEventArgs)e);
            /*
            DilySalesReport.cc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            // if (selectedIndex > -1)
            // {
            //dataGridView1.Rows.RemoveAt(selectedIndex);
            //dataGridView1.Refresh(); // if needed


            // }
            SalesDtlReport sdtr = new SalesDtlReport();
            //MAIN mn = new MAIN();
            //sdtr.MdiParent = mn;

            sdtr.ShowDialog();
             */
            //  dataGridView1_KeyDown((object)sender,(EventArgs) e);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (comboBox1.SelectedItem.ToString() == "محلي" || comboBox1.SelectedItem.ToString() == "Local")
            {
                typeno = "1";
            }
            else
            {
                if (comboBox1.SelectedItem.ToString() == "سفري" || comboBox1.SelectedItem.ToString() == "Out")
                {
                    typeno = "3";
                }
                else
                {
                    if (comboBox1.SelectedItem.ToString() == "على الحساب" || comboBox1.SelectedItem.ToString() == "On Acc")
                    {
                        typeno = "4";
                    }
                    else
                    {
                        typeno = "";
                    }
                }

            }
             */
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
                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sal.rpt.Sal_Report_itm_ML_rpt.rdlc";



                // ReportParameter[] parameters = new ReportParameter[2];
                // parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                // parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));

                // rf.reportViewer1.LocalReport.SetParameters(parameters);
                ReportParameter[] parameters = new ReportParameter[4];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                parameters[1] = new ReportParameter("fmdate", txt_fdate.Text);
                parameters[2] = new ReportParameter("tmdate", txt_tdate.Text);
                parameters[3] = new ReportParameter("br_name", BL.CLS_Session.brname);

                rf.reportViewer1.LocalReport.SetParameters(parameters);

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();

                //rf.reportViewer1.RefreshReport();
                //rf.MdiParent = ParentForm;
                //rf.Show();
            }
            catch { }
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

        private void dataGridView1_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            Acc_Tran f4 = new Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
            f4.MdiParent = ParentForm;
            // ActiveMdiChild.WindowState = FormWindowState.Normal;
            f4.Show();
             */
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_type.SelectedIndex = -1;
            }


        }

        private void كشفحسابToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            Acc_Tran f4 = new Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
            f4.MdiParent = ParentForm;
            // ActiveMdiChild.WindowState = FormWindowState.Normal;
            f4.Show();
             */
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // string temy1 =
            if (checkBox1.Checked)
            {
                txt_fdate.Text = DateTime.Now.ToString("yyyy-01-01", new CultureInfo("ar-SA", false));
                //  string temy2 =
                txt_tdate.Text = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("ar-SA", false));
            }
            else
            {
                txt_fdate.Text = DateTime.Now.ToString("yyyy-01-01", new CultureInfo("en-US", false));
                //  string temy2 =
                txt_tdate.Text = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US", false));
            }
        }

        private void cmb_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_user.SelectedIndex = -1;
            }
        }

        private void cmb_salctr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_salctr.SelectedIndex = -1;
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

        private void txt_fdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_tdate.Focus();
                // SendKeys.Send("{Home}");
                txt_tdate.SelectAll();
            }
        }

        private void txt_tdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
        }

        private void txt_code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                // var selectedCell = dataGridView1.SelectedCells[0];
                // do something with selectedCell...
                Search_All f4 = new Search_All("5", "Cus");
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

              //  button1_Click(sender, e);
                txt_code_Leave(null, null);

            }
        }

        private void txt_code_DoubleClick(object sender, EventArgs e)
        {
            Search_All f4 = new Search_All("5", "Cus");
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

        private void txt_code_Leave(object sender, EventArgs e)
        {
            DataTable dt1 = daml.SELECT_QUIRY_only_retrn_dt("select cu_name a_name,round(cu_opnbal,2) a_opnbal,cu_no a_key from customers where cu_no='" + txt_code.Text + "' and cu_brno='" + BL.CLS_Session.brno + "'");
            if (dt1.Rows.Count > 0)
            {
                txt_name.Text = dt1.Rows[0][0].ToString();
                txt_code.Text = dt1.Rows[0][2].ToString();
                //txt_opnbal.Text = dth.Rows[0][1].ToString();
            }
            else
            {
                txt_name.Text = "";
                txt_code.Text = "";
            }
        }

        private void txt_itemno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                // var selectedCell = dataGridView1.SelectedCells[0];
                // do something with selectedCell...
                Search_All f4 = new Search_All("2", "Itm");
                f4.ShowDialog();

                try
                {

                    txt_itemno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_itmname.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
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

                //  button1_Click(sender, e);
                txt_itemno_Leave(null, null);

            }
        }

        private void txt_itemno_DoubleClick(object sender, EventArgs e)
        {
            Search_All f4 = new Search_All("2", "Itm");
            f4.ShowDialog();

            try
            {

                txt_itemno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txt_itmname.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                /*
               dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
               dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
               dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                */


            }
            catch { }
        }

        private void txt_itemno_Leave(object sender, EventArgs e)
        {
            DataTable dt1 = daml.SELECT_QUIRY_only_retrn_dt("select item_no ,item_name  from items where item_no='" + txt_itemno.Text + "'");
            if (dt1.Rows.Count > 0)
            {
                txt_itmname.Text = dt1.Rows[0][1].ToString();
                txt_itemno.Text = dt1.Rows[0][0].ToString();
                //txt_opnbal.Text = dth.Rows[0][1].ToString();
            }
            else
            {
                txt_itmname.Text = "";
                txt_itemno.Text = "";
            }
        }

        private void cmb_grp_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_sgrp_Enter(sender, e);
            cmb_sgrp.SelectedIndex = -1;
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

        private void cmb_sgrp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_sgrp.SelectedIndex = -1;
            }
        }

        private void cmb_grp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_grp.SelectedIndex = -1;
            }
        }
    }
}
