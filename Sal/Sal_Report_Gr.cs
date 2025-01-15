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
    public partial class Sal_Report_Gr : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dt, dtusers;
        //  DataTable dtt;
        public static int qq;
        string sl;
        // string typeno = "";
        SqlConnection con3 = BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public Sal_Report_Gr()
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
            //if (string.IsNullOrEmpty(txt_code.Text) || string.IsNullOrEmpty(txt_name.Text))
            //{
            //    MessageBox.Show("يجب ادخال رقم العميل", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txt_code.Focus();
            //    return;
            //}
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
                string condst = cmb_salctr.SelectedIndex != -1 ? " and c.slcenter = '" + cmb_salctr.SelectedValue.ToString() + "' " : " and c.branch + c.slcenter in(" + sl + ") ";
                string cond = checkBox1.Checked ? "c.invhdate" : "c.invmdate";
                string condgrp = cmb_grp.SelectedIndex == -1 ? "" : " and a.item_group='" + cmb_grp.SelectedValue + "' ";
                string condsgrp = cmb_sgrp.SelectedIndex == -1 ? "" : " and a.sgroup='" + cmb_sgrp.SelectedValue + "' ";
              //  string condp = rad_posted.Checked ? " and posted=1 " : (rad_notpostd.Checked ? " and posted=0 " : " ");
                string condbr = chk_allbr.Checked ? "" : " and c.branch='" + BL.CLS_Session.brno + "' ";
              //  string condf = txt_fno.Text != "" ? txt_fno.Text : "1";
               // string condt = txt_tno.Text != "" ? txt_tno.Text : "999999999";
               // string condft = " and ref between " + condf + " and " + condt + " ";

               // string condcn = !string.IsNullOrEmpty(txt_code.Text) ? " and custno='" + txt_code.Text + "'" : " ";
               // string conslctr = cmb_salctr.SelectedIndex != -1 ? " and slcenter = '" + cmb_salctr.SelectedValue.ToString() + "' " : " ";
                string condinvtype = cmb_type.SelectedIndex != -1 ? " and c.invtype = '" + cmb_type.SelectedValue.ToString() + "' " : " ";
                //  string qstr = "select slcenter sl_no,invtype a_type,ref a_ref,CONVERT(VARCHAR(10), CAST(invmdate as date), 105) a_mdate,CONVERT(VARCHAR(10), CAST(invhdate as date), 105) a_hdate,text a_text,invttl invttl,invdsvl invdsvl,tax_amt_rcvd tax,nettotal nettotal,posted a_p,invtype a_t,round(((invttl - invdsvl) - invcst),2) invwin,('%' + CAST(round((((invttl - invdsvl) - invcst)/(invttl - invdsvl))*100,2) as varchar)) invwinp  from sales_hdr where branch='" + BL.CLS_Session.brno + "' " + condp + usrname + condft + conslctr + " and " + cond + " between '" + txt_fdate.Text.Replace("-", "") + "' and '" + txt_tdate.Text.Replace("-", "") + "'";
              //
                //string qstr = "select slcenter sl_no,invtype a_type,ref a_ref,CONVERT(VARCHAR(10), CAST(invmdate as date), 105) a_mdate,CONVERT(VARCHAR(10), CAST(invhdate as date), 105) a_hdate,text a_text,invttl invttl,invdsvl invdsvl,tax_amt_rcvd tax,nettotal nettotal,posted a_p,invtype a_t,round(((invttl - invdsvl) - invcst),2) invwin,('%' + CAST(round((((invttl - invdsvl) - invcst)/(invttl - invdsvl))*100,2) as varchar)) invwinp  from sales_hdr where branch='" + BL.CLS_Session.brno + "' and custno='" + txt_code.Text + "' " + condp + usrname + condft + conslctr + " and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'";
                string qstr = "";

                //if(chk_allsalctr.Checked)
                //    qstr = "Select a.branch br_no,b.sl_name br_name, nettotal=round(sum(case when a.invtype in ('04','05') then a.nettotal-a.tax_amt_rcvd else -(a.nettotal-a.tax_amt_rcvd) end),2),tax=round(sum(case when a.invtype in ('04','05') then a.tax_amt_rcvd else -a.tax_amt_rcvd end),2),salcost = round(sum((case when invtype in ('04','05') then round(a.invcst,2) else - round(a.invcst,2) end)),2) ,win = round(sum((case when a.invtype in ('04','05') then round(((a.invttl - a.invdsvl) - a.invcst),2) else -round(((a.invttl - a.invdsvl) - a.invcst),2) end)),2),winp = ('%' + cast( round(sum(case when a.invtype in ('04','05') then ((a.invttl - a.invdsvl) - a.invcst)/(a.invttl - a.invdsvl)*100 else ((a.invttl - a.invdsvl) - a.invcst)/(a.invttl - a.invdsvl)*100 end),2)  as varchar))  from  sales_hdr a join slcenters b with (NOLOCK) on a.branch = b.sl_brno and a.slcenter=b.sl_no  where " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + condbr + condp + condinvtype + "  group By  a.branch,b.sl_name order By a.branch";
               
                //else
                //////if(cmb_grp.SelectedIndex == -1)
                //////    qstr = "Select a.item_group br_no,b.group_name br_name, nettotal=round(sum(case when c.invtype in ('04','05') then c.QTY * c.pkqty *(c.price) else -(c.QTY * c.pkqty *(c.price)) end),2),tax=round(sum(case when c.invtype in ('04','05') then c.QTY else - c.QTY end),2),salcost = round(sum(case when c.invtype in ('04','05') then c.QTY * c.pkqty *(c.cost) else -(c.QTY * c.pkqty *(c.cost)) end),2) ,win=round(sum( case when c.invtype in ('04','05') then c.QTY * c.pkqty *((c.price - c.cost)) else -(c.QTY * c.pkqty *((c.price - c.cost))) end),2),winp='' from  items a join groups b on a.item_group = b.group_id join sales_dtl c on a.item_no=c.itemno  where " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + condbr + condinvtype + condgrp + condsgrp + "  group By  a.item_group,b.group_name order By a.item_group";
                //////else
                //////  //  qstr = "Select a.sgroup br_no,b.group_name br_name, nettotal=round(sum(case when c.invtype in ('04','05') then c.QTY * c.pkqty *(c.price-(c.price)) else -(c.QTY * c.pkqty *(c.price-(c.price))) end),2),tax=round(sum(case when c.invtype in ('04','05') then c.QTY else - c.QTY end),2),salcost = round(sum(case when c.invtype in ('04','05') then c.QTY * c.pkqty *(c.cost-(c.cost)) else -(c.QTY * c.pkqty *(c.cost-(c.cost))) end),2) ,win=round(sum( case when c.invtype in ('04','05') then c.QTY * c.pkqty *((c.price - c.cost)-((c.price - c.cost))) else -(c.QTY * c.pkqty *((c.price - c.cost)-((c.price - c.cost)))) end),2),winp='' from  items a join groups b on a.sgroup = b.group_id  join sales_dtl c on a.item_no=c.itemno  where " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + condbr + condinvtype + condgrp + condsgrp + "  group By a.sgroup,b.group_name order By a.sgroup";
                //////    qstr = "Select a.sgroup br_no,b.group_name br_name, nettotal=round(sum(case when c.invtype in ('04','05') then c.QTY * c.pkqty *(c.price) else -(c.QTY * c.pkqty *(c.price)) end),2),tax=round(sum(case when c.invtype in ('04','05') then c.QTY else - c.QTY end),2),salcost = round(sum(case when c.invtype in ('04','05') then c.QTY * c.pkqty *(c.cost) else -(c.QTY * c.pkqty *(c.cost)) end),2) ,win=round(sum( case when c.invtype in ('04','05') then c.QTY * c.pkqty *((c.price - c.cost)) else -(c.QTY * c.pkqty *((c.price - c.cost))) end),2),winp='' from  items a join groups b on a.sgroup = b.group_id join sales_dtl c on a.item_no=c.itemno  where " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + condbr + condinvtype + condgrp + condsgrp + "  group By a.sgroup,b.group_name order By a.sgroup";
                //////if (cmb_grp.SelectedIndex == -1)
                //////    qstr = "Select a.item_group br_no,b.group_name br_name, nettotal=round(sum(case when c.invtype in ('04','05') then c.QTY * c.pkqty *(case when h.with_tax=1 then (((c.price-(c.tax_amt/(c.qty * c.pkqty))-(c.offer_amt/(c.qty * c.pkqty)))) - (((c.price-(c.tax_amt/(c.qty * c.pkqty))-(c.offer_amt/(c.qty * c.pkqty)))) * h.invdspc/100)) else ((c.price) - ((c.price) * h.invdspc/100)) end ) else -(c.QTY * c.pkqty *(case when h.with_tax=1 then (((c.price-(c.tax_amt/(c.qty * c.pkqty))-(c.offer_amt/(c.qty * c.pkqty)))) - (((c.price-(c.tax_amt/(c.qty * c.pkqty))-(c.offer_amt/(c.qty * c.pkqty)))) * h.invdspc/100)) else ((c.price) - ((c.price) * h.invdspc/100)) end )) end),2),tax=round(sum(case when c.invtype in ('04','05') then c.QTY else - c.QTY end),2),salcost = round(sum(case when c.invtype in ('04','05') then c.QTY * c.pkqty *(c.cost) else -(c.QTY * c.pkqty *(c.cost)) end),2) ,win=round(sum( case when c.invtype in ('04','05') then c.QTY * c.pkqty *(((case when h.with_tax=1 then (((c.price-(c.tax_amt/(c.qty * c.pkqty))-(c.offer_amt/(c.qty * c.pkqty)))) - (((c.price-(c.tax_amt/(c.qty * c.pkqty))-(c.offer_amt/(c.qty * c.pkqty)))) * h.invdspc/100)) else ((c.price) - ((c.price) * h.invdspc/100)) end ) - c.cost)) else -(c.QTY * c.pkqty *(((case when h.with_tax=1 then (((c.price-(c.tax_amt/(c.qty * c.pkqty))-(c.offer_amt/(c.qty * c.pkqty)))) - (((c.price-(c.tax_amt/(c.qty * c.pkqty))-(c.offer_amt/(c.qty * c.pkqty)))) * h.invdspc/100)) else ((c.price) - ((c.price) * h.invdspc/100)) end ) - c.cost))) end),2),winp='' from  items a join groups b on a.item_group = b.group_id join sales_dtl c on a.item_no=c.itemno join sales_hdr h on c.[branch]=h.[branch] and c.[slcenter]=h.[slcenter] and c.[invtype]=h.[invtype] and  c.[ref]=h.[ref]  where " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + condbr + condinvtype + condgrp + condsgrp + "  group By  a.item_group,b.group_name order By a.item_group";
                //////else
                //////    //  qstr = "Select a.sgroup br_no,b.group_name br_name, nettotal=round(sum(case when c.invtype in ('04','05') then c.QTY * c.pkqty *(c.price-(c.price)) else -(c.QTY * c.pkqty *(c.price-(c.price))) end),2),tax=round(sum(case when c.invtype in ('04','05') then c.QTY else - c.QTY end),2),salcost = round(sum(case when c.invtype in ('04','05') then c.QTY * c.pkqty *(c.cost-(c.cost)) else -(c.QTY * c.pkqty *(c.cost-(c.cost))) end),2) ,win=round(sum( case when c.invtype in ('04','05') then c.QTY * c.pkqty *((c.price - c.cost)-((c.price - c.cost))) else -(c.QTY * c.pkqty *((c.price - c.cost)-((c.price - c.cost)))) end),2),winp='' from  items a join groups b on a.sgroup = b.group_id  join sales_dtl c on a.item_no=c.itemno  where " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + condbr + condinvtype + condgrp + condsgrp + "  group By a.sgroup,b.group_name order By a.sgroup";
                //////    qstr = "Select a.sgroup br_no,b.group_name br_name, nettotal=round(sum(case when c.invtype in ('04','05') then c.QTY * c.pkqty *(case when h.with_tax=1 then (((c.price-(c.tax_amt/(c.qty * c.pkqty))-(c.offer_amt/(c.qty * c.pkqty)))) - (((c.price-(c.tax_amt/(c.qty * c.pkqty))-(c.offer_amt/(c.qty * c.pkqty)))) * h.invdspc/100)) else ((c.price) - ((c.price) * h.invdspc/100)) end ) else -(c.QTY * c.pkqty *(case when h.with_tax=1 then (((c.price-(c.tax_amt/(c.qty * c.pkqty))-(c.offer_amt/(c.qty * c.pkqty)))) - (((c.price-(c.tax_amt/(c.qty * c.pkqty))-(c.offer_amt/(c.qty * c.pkqty)))) * h.invdspc/100)) else ((c.price) - ((c.price) * h.invdspc/100)) end )) end),2),tax=round(sum(case when c.invtype in ('04','05') then c.QTY else - c.QTY end),2),salcost = round(sum(case when c.invtype in ('04','05') then c.QTY * c.pkqty *(c.cost) else -(c.QTY * c.pkqty *(c.cost)) end),2) ,win=round(sum( case when c.invtype in ('04','05') then c.QTY * c.pkqty *(((case when h.with_tax=1 then (((c.price-(c.tax_amt/(c.qty * c.pkqty))-(c.offer_amt/(c.qty * c.pkqty)))) - (((c.price-(c.tax_amt/(c.qty * c.pkqty))-(c.offer_amt/(c.qty * c.pkqty)))) * h.invdspc/100)) else ((c.price) - ((c.price) * h.invdspc/100)) end ) - c.cost)) else -(c.QTY * c.pkqty *(((case when h.with_tax=1 then (((c.price-(c.tax_amt/(c.qty * c.pkqty))-(c.offer_amt/(c.qty * c.pkqty)))) - (((c.price-(c.tax_amt/(c.qty * c.pkqty))-(c.offer_amt/(c.qty * c.pkqty)))) * h.invdspc/100)) else ((c.price) - ((c.price) * h.invdspc/100)) end ) - c.cost))) end),2),winp='' from  items a join groups b on a.sgroup = b.group_id join sales_dtl c on a.item_no=c.itemno join sales_hdr h on c.[branch]=h.[branch] and c.[slcenter]=h.[slcenter] and c.[invtype]=h.[invtype] and  c.[ref]=h.[ref] where " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + condbr + condinvtype + condgrp + condsgrp + "  group By a.sgroup,b.group_name order By a.sgroup";

                if (cmb_grp.SelectedIndex == -1)
                    qstr = "Select a.item_group br_no,b.group_name br_name, nettotal=round(sum(((qty*(price-iif(1=1,(price*discpc/100),floor(price*discpc/100))))-offer_amt-(case when h.with_tax=0 then 0 else tax_amt end))*IIF(c.invtype in ('04','05'),1,-1)),2),tax=round(sum(case when c.invtype in ('04','05') then c.QTY else - c.QTY end),2),salcost = round(sum(case when c.invtype in ('04','05') then c.QTY * c.pkqty *(c.cost) else -(c.QTY * c.pkqty *(c.cost)) end),2) ,win=round(sum(((qty*(price-iif(1=1,(price*discpc/100),floor(price*discpc/100))))-offer_amt)*IIF(c.invtype in ('04','05'),1,-1))-sum((invdsvl/(nettotal+invdsvl))*iif(1=1,((c.QTY*(c.price-(c.price*c.discpc/100)))-c.offer_amt),((c.QTY*(c.price-Floor(c.price*c.discpc/100)))-c.offer_amt))*(case when h.invtype in ('04','05') then 1 else -1 end))-sum(case when c.invtype in ('04','05') then c.QTY * c.pkqty *(c.cost) else -(c.QTY * c.pkqty *(c.cost)) end)  ,2),winp='',vtax=round(sum((tax_amt)*IIF(c.invtype in ('04','05'),1,-1)),2),twtax=round(sum(((qty*(price-iif(1=1,(price*discpc/100),floor(price*discpc/100))))-offer_amt-(case when h.with_tax=0 then 0 else tax_amt end))*IIF(c.invtype in ('04','05'),1,-1)),2)+round(sum((tax_amt)*IIF(c.invtype in ('04','05'),1,-1)),2),per='' from  items a join groups b on a.item_group = b.group_id join sales_dtl c on a.item_no=c.itemno join sales_hdr h on c.[branch]=h.[branch] and c.[slcenter]=h.[slcenter] and c.[invtype]=h.[invtype] and  c.[ref]=h.[ref]  where " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + condbr + condinvtype + condgrp + condsgrp + condst + "  group By  a.item_group,b.group_name order By a.item_group";
                else
                    qstr = "Select a.sgroup br_no,b.group_name br_name, nettotal=round(sum(((qty*(price-iif(1=1,(price*discpc/100),floor(price*discpc/100))))-offer_amt" + (chk_isshameltax.Checked ? " -(case when h.with_tax=0 then 0 else tax_amt end) " : " +(case when h.with_tax=1 then tax_amt else 0 end) ") + ")*IIF(c.invtype in ('04','05'),1,-1)),2),tax=round(sum(case when c.invtype in ('04','05') then c.QTY else - c.QTY end),2),salcost = round(sum(case when c.invtype in ('04','05') then c.QTY * c.pkqty *(c.cost) else -(c.QTY * c.pkqty *(c.cost)) end),2) ,win=round(sum(((qty*(price-iif(1=1,(price*discpc/100),floor(price*discpc/100))))-offer_amt)*IIF(c.invtype in ('04','05'),1,-1))-sum((invdsvl/(nettotal+invdsvl))*iif(1=1,((c.QTY*(c.price-(c.price*c.discpc/100)))-c.offer_amt),((c.QTY*(c.price-Floor(c.price*c.discpc/100)))-c.offer_amt))*(case when h.invtype in ('04','05') then 1 else -1 end))-sum(case when c.invtype in ('04','05') then c.QTY * c.pkqty *(c.cost) else -(c.QTY * c.pkqty *(c.cost)) end)  ,2),winp='',per='' from  items a join groups b on a.sgroup = b.group_id and b.group_pid is not null join sales_dtl c on a.item_no=c.itemno join sales_hdr h on c.[branch]=h.[branch] and c.[slcenter]=h.[slcenter] and c.[invtype]=h.[invtype] and  c.[ref]=h.[ref]  where " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' " + condbr + condinvtype + condgrp + condsgrp + condst + "  group By  a.sgroup,b.group_name order By a.sgroup";
                
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
                dt = daml.CMD_SELECT_QUIRY_only_retrn_dt(qstr);
                
                DataRow dr = dt.NewRow();
                double sumopnd = 0, sumopnm = 0, sumtrd = 0, sumtrm = 0, sumttld = 0, sumttlm = 0;
                foreach (DataRow dtr in dt.Rows)
                {
                    sumopnd = sumopnd + Convert.ToDouble(dtr[2]);
                    sumopnm = sumopnm + Convert.ToDouble(dtr[3]);
                    sumtrd = sumtrd + Convert.ToDouble(dtr[4]);
                    sumtrm = sumtrm + Convert.ToDouble(dtr[5]);
                    sumttld = sumttld + Convert.ToDouble(dtr[7]);
                    sumttlm = sumttlm + Convert.ToDouble(dtr[8]);
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
                dr[7] = Math.Round(sumttld, 2);
                dr[8] = Math.Round(sumttlm, 2);
               // dr[6] = "";
                 dr[9] = "";
               // dr[6] = "%" + ((Math.Round(sumopnd, 2) - Math.Round(sumtrd, 2)) / (Math.Round(sumopnd, 2)) * 100 ).ToString(); //round(sum(case when a.invtype in ('04','05') then ((a.invttl - a.invdsvl) - a.invcst)/(a.invttl - a.invdsvl)*100 else ((a.invttl - a.invdsvl) - a.invcst)/(a.invttl - a.invdsvl)*100 end),2)
                // dr[11] = " ";

               // dr[12] = Math.Round(sumttld, 2);
               // dr[13] = "%" + sumttlm.ToString();

                //  dr[11] = "";

                dt.Rows.Add(dr);
                
                dataGridView1.DataSource = dt;

                foreach (DataGridViewRow dtrg in dataGridView1.Rows)
                {

                    dtrg.Cells["Column7"].Value = "%" + Math.Round(((Convert.ToDouble(dtrg.Cells[2].Value) - Convert.ToDouble(dtrg.Cells[4].Value)) / (Convert.ToDouble(dtrg.Cells[2].Value)) * 100), 2).ToString();
                    //  sumttlm = sumttlm + Convert.ToDouble(dtr[13].ToString().Replace("%",""));
                    dtrg.Cells["Column8"].Value = Math.Round(((Convert.ToDouble(dtrg.Cells[2].Value) / sumopnd) * 100), 2);

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
                string[] ary = { "Column7", "Column6" };
                datval.showwin_report(dataGridView1, ary);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            chk_isshameltax.Checked = BL.CLS_Session.isshamltax.Equals("2") ? true : false;
            string tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
            //   dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('04','05','14','15')");
            cmb_type.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('04','05','14','15')");
            cmb_type.DisplayMember = "tr_name";
            cmb_type.ValueMember = "tr_no";
            cmb_type.SelectedIndex = -1;

             sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";
            // MessageBox.Show(sl);

            cmb_salctr.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select * from slcenters where sl_brno + sl_no in(" + sl + ") and sl_brno='" + BL.CLS_Session.brno + "'");
            cmb_salctr.DisplayMember = "sl_name";
            cmb_salctr.ValueMember = "sl_no";
            cmb_salctr.SelectedIndex = -1;

            ////dtusers = daml.SELECT_QUIRY_only_retrn_dt("select user_name,full_name from users");

            ////cmb_user.DataSource = dtusers;
            ////cmb_user.DisplayMember = "full_name";
            ////cmb_user.ValueMember = "user_name";
            ////cmb_user.SelectedIndex = -1;

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

                workSheet.Range["E" + (dataGridView1.Rows.Count + 6).ToString(), "N" + (dataGridView1.Rows.Count + 6).ToString()].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                workSheet.Range["A6", "N6"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                // workSheet.Cells[2, "D"]

                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
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
                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sal.rpt.Sal_Report_Gr_rpt.rdlc";



                // ReportParameter[] parameters = new ReportParameter[2];
                // parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                // parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));

                // rf.reportViewer1.LocalReport.SetParameters(parameters);
                ReportParameter[] parameters = new ReportParameter[8];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                parameters[1] = new ReportParameter("fmdate", txt_fdate.Text);
                parameters[2] = new ReportParameter("tmdate", txt_tdate.Text);
                parameters[3] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[4] = new ReportParameter("br_namet", dataGridView1.Columns[1].HeaderText);
                parameters[5] = new ReportParameter("br_no", dataGridView1.Columns[0].HeaderText);
                parameters[6] = new ReportParameter("var1", "كمية البيع");
                parameters[7] = new ReportParameter("title", this.Text);

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
            if (e.KeyCode == Keys.F9)
            {
                //Sal.Sal_Tran_D f4 = new Sal.Sal_Tran_D(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString());
                //f4.MdiParent = ParentForm;
                //f4.Show();
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

        private void cmb_user_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete)
            //{
            //    cmb_user.SelectedIndex = -1;
            //}
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
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_allsalctr.Checked)
            {
                chk_allbr.Checked = false;
                dataGridView1.Columns[0].HeaderText = "مركز البيع";
                dataGridView1.Columns[1].HeaderText = "اسم مركز البيع";
            }
            else
            {
                dataGridView1.Columns[0].HeaderText = "رقم الفرع";
                dataGridView1.Columns[1].HeaderText = "اسم الفرع";
            }
        }

        private void chk_allbr_CheckedChanged(object sender, EventArgs e)
        {
            //if (chk_allbr.Checked)
            //    chk_allsalctr.Checked = false;


           
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

        private void cmb_grp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_grp.SelectedIndex = -1;
            }
        }

        private void cmb_sgrp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_sgrp.SelectedIndex = -1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Sal.Sal_Report_Gr_Daily rs = new Sal.Sal_Report_Gr_Daily();
           // rs.MdiParent = this.MdiParent;
            rs.ShowDialog();
        }
    }
}
