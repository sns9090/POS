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

namespace POS.Sup
{
    public partial class Acc_Mizan : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dtm;
        public static int qq;
        string su_no ;
        SqlConnection con3 = BL.DAML.con;//new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public Acc_Mizan()
        {
            InitializeComponent();

           // su_no = akey;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (chk_alllvl.Checked)
            {
                Load_Statment2();
            }
            else
            {

                if (radioButton2.Checked)
                {

                    try
                    {


                        //string qstr = "select a.su_no a,a.su_name b,round((case when a.su_opnbal < 0  then a.su_opnbal else 0 end),2) as c,round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),2) as d,(select isnull(sum(b.a_damt-b.a_camt),0) from acc_dtl b where a.su_no=b.su_no) e"
                        //            + " from suppliers a where a.su_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.su_no=b.su_no) <>0 "
                        //            + " order by a.su_name;";
                        /*
                        string qstr = "select a.su_no d1,a.su_name d2,round((case when a.su_opnbal < 0  then -a.su_opnbal else 0 end),2) d3,round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),2) as d4"
                                   + " ,(select round(isnull(sum(b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) d5,(select round(isnull(sum(b.a_damt),0),2) from acc_dtl b where a.su_no=b.su_no) d6"
                                   + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) < 0 then -(select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) else 0 end d7"
                                   + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) > 0 then (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) else 0 end d8"
                                   + " from suppliers a where (select round(a.su_opnbal +isnull(sum(a_damt-a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) <>0 and a_brno='" + BL.CLS_Session.brno + "'"
                                   + " order by a.su_no;";

                        dtm=daml.SELECT_QUIRY_only_retrn_dt(qstr);

                        DataRow dr = dtm.NewRow();
                        double sumopnd = 0, sumopnm = 0, sumtrd = 0, sumtrm = 0, sumttld = 0, sumttlm = 0;
                        foreach (DataRow dtr in dtm.Rows)
                        {
                            sumopnd = sumopnd + Convert.ToDouble(dtr[2]);
                            sumopnm = sumopnm + Convert.ToDouble(dtr[3]);
                            sumtrd = sumtrd + Convert.ToDouble(dtr[4]);
                            sumtrm = sumtrm + Convert.ToDouble(dtr[5]);
                            sumttld = sumttld + Convert.ToDouble(dtr[6]);
                            sumttlm = sumttlm + Convert.ToDouble(dtr[7]);
                        }
                        dr[0] = "";
                        dr[1] = "الاجمالي";
                        dr[2] = sumopnd;
                        dr[3] = sumopnm;
                        dr[4] = sumtrd;
                        dr[5] = sumtrm;
                        dr[6] = sumttld;
                        dr[7] = sumttlm;

                        dtm.Rows.Add(dr);

                        dataGridView1.DataSource = dtm;
                      //  dataGridView1.Columns[1].Width= 350;

                        */
                        /*
                        string condp = rad_posted.Checked ? " and c.a_posted=1 " : (rad_notpostd.Checked ? " and c.a_posted=0 " : " ");
                        string qstr = "select a.su_no d1,a.su_name d2,round((case when a.su_opnbal < 0  then -a.su_opnbal else 0 end),2) d3,round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),2) as d4"
                                  + " ,(select round(isnull(sum(b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no ) d5,(select round(isnull(sum(b.a_damt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) d6"
                                  + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) < 0 then -(select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) else 0 end d7"
                                  + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) > 0 then (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) else 0 end d8"
                                  + " from suppliers a where (select round(a.su_opnbal +isnull(sum(a_damt-a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) <>0 and a_brno='" + BL.CLS_Session.brno + "'"
                                  + " order by a.su_no;";
                        */

                        //select a.su_no d1,a.su_name d2,round((case when a.su_opnbal < 0  then -a.su_opnbal else 0 end),2) + (select isnull(sum(-b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.a_mdate < '20180101') d3,
                        //   round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),2) + (select isnull(sum(b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.a_mdate < '20180101') d4
                        //           ,(select round(isnull(sum(b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.a_mdate between '20180101' and '20181231'  where a.su_no=b.su_no ) d5
                        //           ,(select round(isnull(sum(b.a_damt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.a_mdate between '20180101' and '20181231'  where a.su_no=b.su_no) d6
                        //           ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type  where a.su_no=b.su_no) < 0 then -(select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type  where a.su_no=b.su_no) else 0 end d7
                        //           ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type  where a.su_no=b.su_no) > 0 then (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type  where a.su_no=b.su_no) else 0 end d8
                        //           from suppliers a where (select round(a.su_opnbal +isnull(sum(a_damt-a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type  where a.su_no=b.su_no) <>0 and a_brno='01'
                        //           order by a.su_no;
                        string condallbr = chk_allbr.Checked ? " " : " and a.su_brno='" + BL.CLS_Session.brno + "' ";
                        string startdt = datval.convert_to_yyyyDDdd(txt_fmdate.Text);
                        string enddt = datval.convert_to_yyyyDDdd(txt_tmdate.Text);
                        string condp = rad_posted.Checked ? " and c.a_posted=1 and c.a_type not in ('06','16') " : (rad_notpostd.Checked ? " and c.a_posted=0 and c.a_type not in ('06','16') " : " and c.a_type not in ('06','16') ");
                        string qstr = "";

                        if (chk_openbalonly.Checked)
                        {
                            qstr = "select a.su_no d1,a.su_name d2,"
                                       + " round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),4)  d3"
                                            + " ,round((case when a.su_opnbal < 0  then -a.su_opnbal else 0 end),4) d4"
                                            + " ,0.00 d5"
                                            + " ,0.00 d6"
                                            + " ,0.00 d7"
                                            + " ,0.00 d8"
                                            + " from suppliers a where " + (chk_zerobl.Checked ? " 1=1 " : " a.su_opnbal <> 0 ") + " " + condallbr //+ " and len(a.su_no)=9"
                                            + " order by a.su_no;";
                        }
                        else
                        {
                            if (numericUpDown1.Value == 4)
                            {
                                qstr = "select a.su_no d1,a.su_name d2,"
                                    + " round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),4) + (select isnull(sum(b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where a.su_no=b.su_no) d3"
                                         + " ,round((case when a.su_opnbal < 0  then -a.su_opnbal else 0 end),4) + (select isnull(sum(b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where a.su_no=b.su_no) d4"
                                         + " ,(select round(isnull(sum(b.a_damt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) d5"
                                         + " ,(select round(isnull(sum(b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) d6"
                                       //  + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) > 0 then (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) else 0 end d7"
                                       //  + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) < 0 then -(select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) else 0 end d8"
                                         +" ,0.0000 d7"
                                         +" ,0.0000 d8"
                                         + " from suppliers a where " + (chk_zerobl.Checked ? " 1=1 " : " (select round((case when a.su_opnbal > 0  then a.su_opnbal else a.su_opnbal end) +isnull(count(*),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where a.su_no=b.su_no) <>0 ") + " " + condallbr //+ " and len(a.su_no)=9"
                                         + " order by a.su_no;";
                            }
                            else
                            {
                                string condallbr2 = chk_allbr.Checked ? " " : " and b.a_brno='" + BL.CLS_Session.brno + "' ";
                                int keylen = numericUpDown1.Value == 3 ? 6 : numericUpDown1.Value == 2 ? 4 : numericUpDown1.Value == 1 ? 2 : 4;
                                qstr = "select a.su_no d1,a.su_name d2,"
                                        + " round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") d3"
                                        + ", round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) < 0  then -(select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") d4"
                                        + " ,(select round(isnull(sum(b.a_damt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") d5"
                                        + " ,(select round(isnull(sum(b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") d6"
                                       // + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") > 0 then (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d7"
                                       // + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") < 0 then -(select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d8"
                                       + " ,0.0000 d7"
                                       + " ,0.0000 d8"
                                        + " from suppliers a where " + (chk_zerobl.Checked ? " 1=1 " : " (select round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) else (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) end) +isnull(count(*),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") <>0 ") + " and len(a.su_no)=" + keylen + ""
                                        + " order by a.su_no;";
                            }
                        }
                        dtm = daml.SELECT_QUIRY_only_retrn_dt(qstr);

                      //  DataRow dr0 = dtm.NewRow();
                        foreach (DataRow dtr0 in dtm.Rows)
                        {

                            dtr0["d7"] = (Convert.ToDouble(dtr0["d3"]) + Convert.ToDouble(dtr0["d5"]) - Convert.ToDouble(dtr0["d4"]) - Convert.ToDouble(dtr0["d6"])) > 0 ? Convert.ToDouble(dtr0["d3"]) + Convert.ToDouble(dtr0["d5"]) - Convert.ToDouble(dtr0["d4"]) - Convert.ToDouble(dtr0["d6"]) : 0.0000;
                            dtr0["d8"] = (Convert.ToDouble(dtr0["d4"]) + Convert.ToDouble(dtr0["d6"]) - Convert.ToDouble(dtr0["d3"]) - Convert.ToDouble(dtr0["d5"])) > 0 ? Convert.ToDouble(dtr0["d4"]) + Convert.ToDouble(dtr0["d6"]) - Convert.ToDouble(dtr0["d3"]) - Convert.ToDouble(dtr0["d5"]) : 0.0000;
                        }

                        DataRow dr = dtm.NewRow();
                        double sumopnd = 0, sumopnm = 0, sumtrd = 0, sumtrm = 0, sumttld = 0, sumttlm = 0;
                        foreach (DataRow dtr in dtm.Rows)
                        {
                            sumopnd = sumopnd + Convert.ToDouble(dtr[3]);
                            sumopnm = sumopnm + Convert.ToDouble(dtr[2]);
                            sumtrd = sumtrd + Convert.ToDouble(dtr[5]);
                            sumtrm = sumtrm + Convert.ToDouble(dtr[4]);
                            sumttld = sumttld + Convert.ToDouble(dtr[7]);
                            sumttlm = sumttlm + Convert.ToDouble(dtr[6]);
                        }
                        dr[0] = "";
                        dr[1] = "الاجمالي";
                        dr[2] = sumopnm;
                        dr[3] = sumopnd;
                        dr[4] = sumtrm;
                        dr[5] = sumtrd;
                        dr[6] = sumttlm;
                        dr[7] = sumttld;

                        dtm.Rows.Add(dr);

                        dataGridView1.DataSource = dtm;
                        //  dataGridView1.Columns[1].Width= 350;

                       
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    Load_Statment();
                }
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                dataGridView1.ClearSelection();
            }
        }
        private void Load_Statment()
        {
            // double sum = 0;
          //  double seso = 0;
            try
            {

                // txt_code.Text = su_no;
                //string qstr = "select a.su_no 'رقم الحساب',a.su_name 'اسم الحساب',round((case when a.su_opnbal < 0  then a.su_opnbal else 0 end),2) as 'رصيد سابق دائن',round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),2) as 'رصيد سابق مدين'"
                //               + " ,(select round(isnull(sum(b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) 'الحركات الدائنة',(select round(isnull(sum(b.a_damt),0),2) from acc_dtl b where a.su_no=b.su_no) 'الحركات المدينة'"
                //              + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) < 0 then (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) else 0 end 'رصيد دائن'"
                //              + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) > 0 then (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) else 0 end 'رصيد مدين'"
                //               + " from suppliers a where (select round(a.su_opnbal +isnull(sum(a_damt-a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) <>0"
                //               + " order by a.su_name;";
                //join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + "
                /*
                string qstr = "select a.su_no d1,a.su_name d2,round((case when a.su_opnbal < 0  then -a.su_opnbal else 0 end),2) as d3,round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),2) as d4"
                              + " ,(select round(isnull(sum(b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) d5,(select round(isnull(sum(b.a_damt),0),2) from acc_dtl b where a.su_no=b.su_no) d6"
                              + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) < 0 then (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) else 0 end d7"
                              + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) > 0 then (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) else 0 end d8"
                              + " from suppliers a where (select round(a.su_opnbal +isnull(sum(a_damt-a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) <>0"
                              + " order by a.su_no;";

                dtm = daml.SELECT_QUIRY_only_retrn_dt(qstr);

                DataRow dr = dtm.NewRow();
                double sumopnd = 0, sumopnm = 0, sumtrd = 0, sumtrm = 0, sumttld = 0, sumttlm = 0;
                foreach (DataRow dtr in dtm.Rows)
                {
                    sumopnd = sumopnd + Convert.ToDouble(dtr[2]);
                    sumopnm = sumopnm + Convert.ToDouble(dtr[3]);
                    sumtrd = sumtrd + Convert.ToDouble(dtr[4]);
                    sumtrm = sumtrm + Convert.ToDouble(dtr[5]);
                    sumttld = sumttld + Convert.ToDouble(dtr[6]);
                    sumttlm = sumttlm + Convert.ToDouble(dtr[7]);
                }
                dr[0] = "";
                dr[1] = "الاجمالي";
                dr[2] = sumopnd;
                dr[3] = sumopnm;
                dr[4] = sumtrd;
                dr[5] = sumtrm;
                dr[6] = sumttld;
                dr[7] = sumttlm;

                dtm.Rows.Add(dr);

                dataGridView1.DataSource = dtm;
               // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr);

               // dataGridView1.Columns[1].Width = 350;
                */

                /*
                string condp = rad_posted.Checked ? " and c.a_posted=1 " : (rad_notpostd.Checked ? " and c.a_posted=0 " : " ");
                string qstr = "select a.su_no d1,a.su_name d2,round((case when a.su_opnbal < 0  then -a.su_opnbal else 0 end),2) as d3,round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),2) as d4"
                            + " ,(select round(isnull(sum(b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) d5,(select round(isnull(sum(b.a_damt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) d6"
                            + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) < 0 then (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) else 0 end d7"
                            + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) > 0 then (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) else 0 end d8"
                            + " from suppliers a where (select round(a.su_opnbal +isnull(sum(a_damt-a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) <>0"
                            + " order by a.su_no;";
                */
                string condallbr = chk_allbr.Checked ? " " : " and a.cu_brno='" + BL.CLS_Session.brno + "' ";
                string startdt = datval.convert_to_yyyyDDdd(txt_fmdate.Text);
                string enddt = datval.convert_to_yyyyDDdd(txt_tmdate.Text);
                string condp = rad_posted.Checked ? " and c.a_posted=1 and c.a_type not in ('06','16') " : (rad_notpostd.Checked ? " and c.a_posted=0 and c.a_type not in ('06','16') " : " and c.a_type not in ('06','16') ");
                string qstr="";

                if (chk_openbalonly.Checked)
                {
                    qstr = "select a.su_no d1,a.su_name d2,"
                               + " round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),4)  d3"
                                    + " ,round((case when a.su_opnbal < 0  then -a.su_opnbal else 0 end),4) d4"
                                    + " ,0.00 d5"
                                    + " ,0.00 d6"
                                    + " ,0.00 d7"
                                    + " ,0.00 d8"
                                    + " from suppliers a where a.su_opnbal <> 0 " + condallbr //+ " and len(a.su_no)=9"
                                    + " order by a.su_no;";
                }
                else
                {
                    if (numericUpDown1.Value == 4)
                    {
                        qstr = "select a.su_no d1,a.su_name d2,"
                                 + " round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),4) + (select isnull(sum(b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where a.su_no=b.su_no) d3"
                                 + " , round((case when a.su_opnbal < 0  then -a.su_opnbal else 0 end),4) + (select isnull(sum(b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where a.su_no=b.su_no) d4"
                                 + " ,(select round(isnull(sum(b.a_damt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) d5"
                                 + " ,(select round(isnull(sum(b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) d6"
                                // + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) > 0 then (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) else 0 end d7"
                                // + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) < 0 then -(select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) else 0 end d8"
                                + " ,0.0000 d7"
                                + " ,0.0000 d8"
                                 + " from suppliers a where (select round((case when a.su_opnbal > 0  then a.su_opnbal else a.su_opnbal end) +isnull(count(*),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where a.su_no=b.su_no) <>0 " + condallbr //+ " and len(a.su_no)=9"
                                 + " order by a.su_no;";

                    }
                    else
                    {
                        string condallbr2 = chk_allbr.Checked ? " " : " and b.a_brno='" + BL.CLS_Session.brno + "' ";
                        int keylen = numericUpDown1.Value == 3 ? 6 : numericUpDown1.Value == 2 ? 4 : numericUpDown1.Value == 1 ? 2 : 4;
                        qstr = "select a.su_no d1,a.su_name d2,"
                                + " round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") d3"
                                + " ,round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) < 0  then -(select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") d4"
                                + " ,(select round(isnull(sum(b.a_damt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") d5"
                                + " ,(select round(isnull(sum(b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") d6"
                               // + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") > 0 then (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d7"
                              //  + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") < 0 then -(select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d8"
                              + " ,0.0000 d7"
                              + " ,0.0000 d8"
                              //  + " from suppliers a where (select round(a.su_opnbal +isnull(sum(a_damt-a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where b.su_no like CONCAT(a.su_no, '%') and b.a_brno='" + BL.CLS_Session.brno + "') <>0 and len(a.su_no)=" + keylen + ""
                                + " from suppliers a where (select round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) else (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) end) +isnull(count(*),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") <>0 and len(a.su_no)=" + keylen + ""

                                + " order by a.su_no;";
                    }
                }
                dtm = daml.SELECT_QUIRY_only_retrn_dt(qstr);

                foreach (DataRow dtr0 in dtm.Rows)
                {

                    dtr0["d7"] = Convert.ToDouble(dtr0["d3"]) + Convert.ToDouble(dtr0["d5"]);
                    dtr0["d8"] = Convert.ToDouble(dtr0["d4"]) + Convert.ToDouble(dtr0["d6"]);
                }

                DataRow dr = dtm.NewRow();
                double sumopnd = 0, sumopnm = 0, sumtrd = 0, sumtrm = 0, sumttld = 0, sumttlm = 0;
                foreach (DataRow dtr in dtm.Rows)
                {
                    sumopnd = sumopnd + Convert.ToDouble(dtr[3]);
                    sumopnm = sumopnm + Convert.ToDouble(dtr[2]);
                    sumtrd = sumtrd + Convert.ToDouble(dtr[5]);
                    sumtrm = sumtrm + Convert.ToDouble(dtr[4]);
                    sumttld = sumttld + Convert.ToDouble(dtr[7]);
                    sumttlm = sumttlm + Convert.ToDouble(dtr[6]);
                }
                dr[0] = "";
                dr[1] = "الاجمالي";
                dr[2] = sumopnm;
                dr[3] = sumopnd;
                dr[4] = sumtrm;
                dr[5] = sumtrd;
                dr[6] = sumttlm;
                dr[7] = sumttld;

                dtm.Rows.Add(dr);

                dataGridView1.DataSource = dtm;

                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {

                        dataGridView1.Rows[i].Cells[7].Value =  Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value.ToString().Trim()) + Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString().Trim());
                        dataGridView1.Rows[i].Cells[6].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString().Trim()) + Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString().Trim());

                      //  dataGridView1.Rows[i].Cells[0].Value = seso > 0 ? seso.ToString() : 0.ToString();
                     //   dataGridView1.Rows[i].Cells[1].Value = seso < 0 ? seso.ToString() : 0.ToString();
                
                      //  seso = seso + Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString().Trim()) - Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value.ToString().Trim());
                        //dataGridView1.Rows[i].Cells[0].Value = seso.ToString();
                       // dataGridView1.Rows[i].Cells[0].Value = seso > 0 ? seso.ToString() : 0.ToString();
                     //   dataGridView1.Rows[i].Cells[1].Value = seso < 0 ? seso.ToString() : 0.ToString();
                 
                }


                 
            }

            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
            }
        }

        private void Load_Statment2()
        {
            if (radioButton2.Checked)
            {

                try
                {


                    //string qstr = "select a.su_no a,a.su_name b,round((case when a.su_opnbal < 0  then a.su_opnbal else 0 end),2) as c,round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),2) as d,(select isnull(sum(b.a_damt-b.a_camt),0) from acc_dtl b where a.su_no=b.su_no) e"
                    //            + " from suppliers a where a.su_opnbal +(select isnull(sum(a_damt-a_camt),0) from acc_dtl b where a.su_no=b.su_no) <>0 "
                    //            + " order by a.su_name;";
                    /*
                    string qstr = "select a.su_no d1,a.su_name d2,round((case when a.su_opnbal < 0  then -a.su_opnbal else 0 end),2) d3,round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),2) as d4"
                               + " ,(select round(isnull(sum(b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) d5,(select round(isnull(sum(b.a_damt),0),2) from acc_dtl b where a.su_no=b.su_no) d6"
                               + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) < 0 then -(select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) else 0 end d7"
                               + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) > 0 then (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) else 0 end d8"
                               + " from suppliers a where (select round(a.su_opnbal +isnull(sum(a_damt-a_camt),0),2) from acc_dtl b where a.su_no=b.su_no) <>0 and a_brno='" + BL.CLS_Session.brno + "'"
                               + " order by a.su_no;";

                    dtm=daml.SELECT_QUIRY_only_retrn_dt(qstr);

                    DataRow dr = dtm.NewRow();
                    double sumopnd = 0, sumopnm = 0, sumtrd = 0, sumtrm = 0, sumttld = 0, sumttlm = 0;
                    foreach (DataRow dtr in dtm.Rows)
                    {
                        sumopnd = sumopnd + Convert.ToDouble(dtr[2]);
                        sumopnm = sumopnm + Convert.ToDouble(dtr[3]);
                        sumtrd = sumtrd + Convert.ToDouble(dtr[4]);
                        sumtrm = sumtrm + Convert.ToDouble(dtr[5]);
                        sumttld = sumttld + Convert.ToDouble(dtr[6]);
                        sumttlm = sumttlm + Convert.ToDouble(dtr[7]);
                    }
                    dr[0] = "";
                    dr[1] = "الاجمالي";
                    dr[2] = sumopnd;
                    dr[3] = sumopnm;
                    dr[4] = sumtrd;
                    dr[5] = sumtrm;
                    dr[6] = sumttld;
                    dr[7] = sumttlm;

                    dtm.Rows.Add(dr);

                    dataGridView1.DataSource = dtm;
                  //  dataGridView1.Columns[1].Width= 350;

                    */
                    /*
                    string condp = rad_posted.Checked ? " and c.a_posted=1 " : (rad_notpostd.Checked ? " and c.a_posted=0 " : " ");
                    string qstr = "select a.su_no d1,a.su_name d2,round((case when a.su_opnbal < 0  then -a.su_opnbal else 0 end),2) d3,round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),2) as d4"
                              + " ,(select round(isnull(sum(b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no ) d5,(select round(isnull(sum(b.a_damt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) d6"
                              + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) < 0 then -(select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) else 0 end d7"
                              + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) > 0 then (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) else 0 end d8"
                              + " from suppliers a where (select round(a.su_opnbal +isnull(sum(a_damt-a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where a.su_no=b.su_no) <>0 and a_brno='" + BL.CLS_Session.brno + "'"
                              + " order by a.su_no;";
                    */

                    //select a.su_no d1,a.su_name d2,round((case when a.su_opnbal < 0  then -a.su_opnbal else 0 end),2) + (select isnull(sum(-b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.a_mdate < '20180101') d3,
                    //   round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),2) + (select isnull(sum(b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.a_mdate < '20180101') d4
                    //           ,(select round(isnull(sum(b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.a_mdate between '20180101' and '20181231'  where a.su_no=b.su_no ) d5
                    //           ,(select round(isnull(sum(b.a_damt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.a_mdate between '20180101' and '20181231'  where a.su_no=b.su_no) d6
                    //           ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type  where a.su_no=b.su_no) < 0 then -(select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type  where a.su_no=b.su_no) else 0 end d7
                    //           ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type  where a.su_no=b.su_no) > 0 then (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type  where a.su_no=b.su_no) else 0 end d8
                    //           from suppliers a where (select round(a.su_opnbal +isnull(sum(a_damt-a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type  where a.su_no=b.su_no) <>0 and a_brno='01'
                    //           order by a.su_no;
                    string condallbr = chk_allbr.Checked ? " " : " and a.a_brno='" + BL.CLS_Session.brno + "' ";
                    string startdt = datval.convert_to_yyyyDDdd(txt_fmdate.Text);
                    string enddt = datval.convert_to_yyyyDDdd(txt_tmdate.Text);
                    string condp = rad_posted.Checked ? " and c.a_posted=1 " : (rad_notpostd.Checked ? " and c.a_posted=0 " : " ");
                    string qstr = "";

                    if (chk_openbalonly.Checked)
                    {
                        qstr = "select a.su_no d1,a.su_name d2,"
                                   + " round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),4)  d3"
                                        + " ,round((case when a.su_opnbal < 0  then -a.su_opnbal else 0 end),4) d4"
                                        + " ,0.00 d5"
                                        + " ,0.00 d6"
                                        + " ,0.00 d7"
                                        + " ,0.00 d8"
                                        + " from suppliers a where  " + (chk_zerobl.Checked ? " 1=1 " : " a.su_opnbal <> 0 ") + " " + condallbr + " and len(a.su_no)=9"
                                        + " order by a.su_no;";
                    }
                    else
                    {
                      
                            ////string condallbr2 = chk_allbr.Checked ? " " : " and b.a_brno='" + BL.CLS_Session.brno + "' ";
                            ////int keylen = numericUpDown1.Value == 3 ? 6 : numericUpDown1.Value == 2 ? 4 : numericUpDown1.Value == 1 ? 2 : 4;
                            ////qstr = "select a.su_no d1,a.su_name d2,"
                            ////        + " round((case when (select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") d3"
                            ////        + ", round((case when (select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) < 0  then -(select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(-b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") d4"
                            ////        + " ,(select round(isnull(sum(b.a_damt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") d5"
                            ////        + " ,(select round(isnull(sum(b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") d6"
                            ////        + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") > 0 then (select round((select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d7"
                            ////        + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") < 0 then -(select round((select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d8"

                            ////        + " from suppliers a where (select round((case when (select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) else -(select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) end) +isnull(sum(a_damt-a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") <>0 "
                            ////        + " order by a.su_no;";

                        qstr = "select a.su_no d1,a.su_name d2,"
                            + " round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),4) + (select isnull(sum(b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where a.su_no=b.su_no) d3"
                                 + " ,round((case when a.su_opnbal < 0  then -a.su_opnbal else 0 end),4) + (select isnull(sum(b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where a.su_no=b.su_no) d4"
                                 + " ,(select round(isnull(sum(b.a_damt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) d5"
                                 + " ,(select round(isnull(sum(b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) d6"
                            //  + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) > 0 then (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) else 0 end d7"
                            //  + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) < 0 then -(select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) else 0 end d8"
                                 + " ,0.0000 d7"
                                 + " ,0.0000 d8"
                                 + " from suppliers a where " + (chk_zerobl.Checked ? " 1=1 " :" (select round((case when a.su_opnbal > 0  then a.su_opnbal else a.su_opnbal end) +isnull(count(*),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where a.su_no=b.su_no) <>0 ") + condallbr + " and len(a.su_no)=9"
                            //  + " order by a.su_no";
                   ;
                            string condallbr2 = chk_allbr.Checked ? " " : " and b.a_brno='" + BL.CLS_Session.brno + "' ";
                          //  int keylen = numericUpDown1.Value == 3 ? 6 : numericUpDown1.Value == 2 ? 4 : numericUpDown1.Value == 1 ? 2 : 4;
                            qstr =qstr  + " union all " + "select a.su_no d1,a.su_name d2,"
                                    + " round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 6 + ") + '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 6 + ") + '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + 6 + ") = substring(a.su_no,1," + 6 + ") " + condallbr2 + ") d3"
                                    + ", round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 6 + ") + '%' and len(b.su_no)=9) < 0  then -(select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 6 + ") + '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + 6 + ") = substring(a.su_no,1," + 6 + ") " + condallbr2 + ") d4"
                                    + " ,(select round(isnull(sum(b.a_damt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + 6 + ") = substring(a.su_no,1," + 6 + ") " + condallbr2 + ") d5"
                                    + " ,(select round(isnull(sum(b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + 6 + ") = substring(a.su_no,1," + 6 + ") " + condallbr2 + ") d6"
                                // + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") > 0 then (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d7"
                                // + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") < 0 then -(select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d8"
                                   + " ,0.0000 d7"
                                   + " ,0.0000 d8"
                                    + " from suppliers a where " + (chk_zerobl.Checked ? " 1=1 " :" (select round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 6 + ") + '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 6 + ") + '%' and len(b.su_no)=9) else (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 6 + ") + '%' and len(b.su_no)=9) end) +isnull(count(*),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + 6 + ") = substring(a.su_no,1," + 6 + ") " + condallbr2 + ") <>0 ") + " and len(a.su_no)=" + 6 + ""
                                  //  + " order by a.su_no";
                                  ;
                            qstr = qstr + " union all " + "select a.su_no d1,a.su_name d2,"
                                       + " round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 4 + ") + '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 4 + ") + '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + 4 + ") = substring(a.su_no,1," + 4 + ") " + condallbr2 + ") d3"
                                       + ", round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 4 + ") + '%' and len(b.su_no)=9) < 0  then -(select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 4 + ") + '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + 4 + ") = substring(a.su_no,1," + 4 + ") " + condallbr2 + ") d4"
                                       + " ,(select round(isnull(sum(b.a_damt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + 4 + ") = substring(a.su_no,1," + 4 + ") " + condallbr2 + ") d5"
                                       + " ,(select round(isnull(sum(b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + 4 + ") = substring(a.su_no,1," + 4 + ") " + condallbr2 + ") d6"
                                // + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") > 0 then (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d7"
                                // + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") < 0 then -(select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d8"
                                      + " ,0.0000 d7"
                                      + " ,0.0000 d8"
                                       + " from suppliers a where " + (chk_zerobl.Checked ? " 1=1 " :" (select round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 4 + ") + '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 4 + ") + '%' and len(b.su_no)=9) else -(select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 4 + ") + '%' and len(b.su_no)=9) end) +isnull(count(*),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + 4 + ") = substring(a.su_no,1," + 4 + ") " + condallbr2 + ") <>0 ") + " and len(a.su_no)=" + 4 + ""
                                     //  + " order by a.su_no";
                                     ;
                            qstr = qstr + " union all " + "select a.su_no d1,a.su_name d2,"
                                       + " round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 2 + ") + '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 2 + ") + '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + 2 + ") = substring(a.su_no,1," + 2 + ") " + condallbr2 + ") d3"
                                       + ", round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 2 + ") + '%' and len(b.su_no)=9) < 0  then -(select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 2 + ") + '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + 2 + ") = substring(a.su_no,1," + 2 + ") " + condallbr2 + ") d4"
                                       + " ,(select round(isnull(sum(b.a_damt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + 2 + ") = substring(a.su_no,1," + 2 + ") " + condallbr2 + ") d5"
                                       + " ,(select round(isnull(sum(b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + 2 + ") = substring(a.su_no,1," + 2 + ") " + condallbr2 + ") d6"
                                // + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") > 0 then (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d7"
                                // + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") < 0 then -(select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d8"
                                      + " ,0.0000 d7"
                                      + " ,0.0000 d8"
                                       + " from suppliers a where " + (chk_zerobl.Checked ? " 1=1 " : " (select round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 2 + ") + '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 2 + ") + '%' and len(b.su_no)=9) else (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 2 + ") + '%' and len(b.su_no)=9) end) +isnull(count(*),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + 2 + ") = substring(a.su_no,1," + 2 + ") " + condallbr2 + ") <>0 ") + " and len(a.su_no)=" + 2 + ""
                                       + " order by a.su_no;";
                        
                        
                    }

                   // MessageBox.Show(qstr);
                    dtm = daml.SELECT_QUIRY_only_retrn_dt(qstr);

                    foreach (DataRow dtr0 in dtm.Rows)
                    {

                        dtr0["d7"] = (Convert.ToDouble(dtr0["d3"]) + Convert.ToDouble(dtr0["d5"]) - Convert.ToDouble(dtr0["d4"]) - Convert.ToDouble(dtr0["d6"])) > 0 ? Convert.ToDouble(dtr0["d3"]) + Convert.ToDouble(dtr0["d5"]) - Convert.ToDouble(dtr0["d4"]) - Convert.ToDouble(dtr0["d6"]) : 0.0000;
                        dtr0["d8"] = (Convert.ToDouble(dtr0["d4"]) + Convert.ToDouble(dtr0["d6"]) - Convert.ToDouble(dtr0["d3"]) - Convert.ToDouble(dtr0["d5"])) > 0 ? Convert.ToDouble(dtr0["d4"]) + Convert.ToDouble(dtr0["d6"]) - Convert.ToDouble(dtr0["d3"]) - Convert.ToDouble(dtr0["d5"]) : 0.0000;
                    }

                    DataRow dr = dtm.NewRow();
                    double sumopnd = 0, sumopnm = 0, sumtrd = 0, sumtrm = 0, sumttld = 0, sumttlm = 0;
                    foreach (DataRow dtr in dtm.Rows)
                    {
                        if (dtr[0].ToString().Trim().Length == 9)
                        {
                            sumopnd = sumopnd + Convert.ToDouble(dtr[3]);
                            sumopnm = sumopnm + Convert.ToDouble(dtr[2]);
                            sumtrd = sumtrd + Convert.ToDouble(dtr[5]);
                            sumtrm = sumtrm + Convert.ToDouble(dtr[4]);
                            sumttld = sumttld + Convert.ToDouble(dtr[7]);
                            sumttlm = sumttlm + Convert.ToDouble(dtr[6]);
                        }
                    }
                    dr[0] = "";
                    dr[1] = "الاجمالي";
                    dr[2] = sumopnm;
                    dr[3] = sumopnd;
                    dr[4] = sumtrm;
                    dr[5] = sumtrd;
                    dr[6] = sumttlm;
                    dr[7] = sumttld;

                    dtm.Rows.Add(dr);

                    dataGridView1.DataSource = dtm;
                    //  dataGridView1.Columns[1].Width= 350;

                    foreach (DataGridViewRow dtr in dataGridView1.Rows)
                    {
                        if (dtr.Cells[0].Value.ToString().Trim().Length == 6)
                            dataGridView1.Rows[dtr.Index].DefaultCellStyle.BackColor = Color.Tan;
                       
                        if (dtr.Cells[0].Value.ToString().Trim().Length == 4)
                            dataGridView1.Rows[dtr.Index].DefaultCellStyle.BackColor = Color.Khaki;

                        if (dtr.Cells[0].Value.ToString().Trim().Length == 2)
                            dataGridView1.Rows[dtr.Index].DefaultCellStyle.BackColor = Color.LightCoral;
                    }

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                Load_Statment3();
            }
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
            dataGridView1.ClearSelection();
        }

        private void Load_Statment3()
        {
            try
            {

                string condallbr = chk_allbr.Checked ? " " : " and a.a_brno='" + BL.CLS_Session.brno + "' ";
                string startdt = datval.convert_to_yyyyDDdd(txt_fmdate.Text);
                string enddt = datval.convert_to_yyyyDDdd(txt_tmdate.Text);
                string condp = rad_posted.Checked ? " and c.a_posted=1 " : (rad_notpostd.Checked ? " and c.a_posted=0 " : " ");
                string qstr = "";

                if (chk_openbalonly.Checked)
                {
                    qstr = "select a.su_no d1,a.su_name d2,"
                               + " round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),4)  d3"
                                    + " ,round((case when a.su_opnbal < 0  then -a.su_opnbal else 0 end),4) d4"
                                    + " ,0.00 d5"
                                    + " ,0.00 d6"
                                    + " ,0.00 d7"
                                    + " ,0.00 d8"
                                    + " from suppliers a where " + (chk_zerobl.Checked ? " 1=1 " :" a.su_opnbal <> 0 ") +" " + condallbr + " and len(a.su_no)=9"
                                    + " order by a.su_no;";
                }
                else
                {
                   
                        ////string condallbr2 = chk_allbr.Checked ? " " : " and b.a_brno='" + BL.CLS_Session.brno + "' ";
                        ////int keylen = numericUpDown1.Value == 3 ? 6 : numericUpDown1.Value == 2 ? 4 : numericUpDown1.Value == 1 ? 2 : 4;
                        ////qstr = "select a.su_no d1,a.su_name d2,"
                        ////        + " round((case when (select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") d3"
                        ////        + " ,round((case when (select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) < 0  then -(select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(-b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") d4"
                        ////        + " ,(select round(isnull(sum(b.a_damt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") d5"
                        ////        + " ,(select round(isnull(sum(b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") d6"
                        ////        + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") > 0 then (select round((select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d7"
                        ////        + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") < 0 then -(select round((select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d8"

                        ////      //  + " from suppliers a where (select round(a.su_opnbal +isnull(sum(a_damt-a_camt),0),2) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type " + condp + " where b.su_no like CONCAT(a.su_no, '%') and b.a_brno='" + BL.CLS_Session.brno + "') <>0 and len(a.su_no)=" + keylen + ""
                        ////        + " from suppliers a where (select round((case when (select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) else -(select sum(su_opnbal) from suppliers b where b.su_no like '%' and len(b.su_no)=9) end) +isnull(sum(a_damt-a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") <>0 "

                        ////        + " order by a.su_no;";

                    qstr = "select a.su_no d1,a.su_name d2,"
                        + " round((case when a.su_opnbal > 0  then a.su_opnbal else 0 end),4) + (select isnull(sum(b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where a.su_no=b.su_no) d3"
                             + " ,round((case when a.su_opnbal < 0  then -a.su_opnbal else 0 end),4) + (select isnull(sum(b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where a.su_no=b.su_no) d4"
                             + " ,(select round(isnull(sum(b.a_damt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) d5"
                             + " ,(select round(isnull(sum(b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) d6"
                        //  + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) > 0 then (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) else 0 end d7"
                        //  + " ,case when (select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) < 0 then -(select round(a.su_opnbal + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where a.su_no=b.su_no) else 0 end d8"
                             + " ,0.0000 d7"
                             + " ,0.0000 d8"
                             + " from suppliers a where " + (chk_zerobl.Checked ? " 1=1 " :" (select round((case when a.su_opnbal > 0  then a.su_opnbal else a.su_opnbal end) +isnull(count(*),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where a.su_no=b.su_no) <>0 ") + condallbr + " and len(a.su_no)=9"
                        //  + " order by a.su_no";
               ;
                    string condallbr2 = chk_allbr.Checked ? " " : " and b.a_brno='" + BL.CLS_Session.brno + "' ";
                    //  int keylen = numericUpDown1.Value == 3 ? 6 : numericUpDown1.Value == 2 ? 4 : numericUpDown1.Value == 1 ? 2 : 4;
                    qstr = qstr + " union all " + "select a.su_no d1,a.su_name d2,"
                            + " round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 6 + ") + '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 6 + ") + '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + 6 + ") = substring(a.su_no,1," + 6 + ") " + condallbr2 + ") d3"
                            + ", round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 6 + ") + '%' and len(b.su_no)=9) < 0  then -(select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 6 + ") + '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + 6 + ") = substring(a.su_no,1," + 6 + ") " + condallbr2 + ") d4"
                            + " ,(select round(isnull(sum(b.a_damt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + 6 + ") = substring(a.su_no,1," + 6 + ") " + condallbr2 + ") d5"
                            + " ,(select round(isnull(sum(b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + 6 + ") = substring(a.su_no,1," + 6 + ") " + condallbr2 + ") d6"
                        // + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") > 0 then (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d7"
                        // + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") < 0 then -(select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d8"
                           + " ,0.0000 d7"
                           + " ,0.0000 d8"
                            + " from suppliers a where " + (chk_zerobl.Checked ? " 1=1 " :" (select round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 6 + ") + '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 6 + ") + '%' and len(b.su_no)=9) else -(select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 6 + ") + '%' and len(b.su_no)=9) end) +isnull(count(*),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + 6 + ") = substring(a.su_no,1," + 6 + ") " + condallbr2 + ") <>0") + " and len(a.su_no)=" + 6 + ""
                        //  + " order by a.su_no";
                          ;
                    qstr = qstr + " union all " + "select a.su_no d1,a.su_name d2,"
                               + " round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 4 + ") + '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 4 + ") + '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + 4 + ") = substring(a.su_no,1," + 4 + ") " + condallbr2 + ") d3"
                               + ", round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 4 + ") + '%' and len(b.su_no)=9) < 0  then -(select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 4 + ") + '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + 4 + ") = substring(a.su_no,1," + 4 + ") " + condallbr2 + ") d4"
                               + " ,(select round(isnull(sum(b.a_damt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + 4 + ") = substring(a.su_no,1," + 4 + ") " + condallbr2 + ") d5"
                               + " ,(select round(isnull(sum(b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + 4 + ") = substring(a.su_no,1," + 4 + ") " + condallbr2 + ") d6"
                        // + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") > 0 then (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d7"
                        // + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") < 0 then -(select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d8"
                              + " ,0.0000 d7"
                              + " ,0.0000 d8"
                               + " from suppliers a where " + (chk_zerobl.Checked ? " 1=1 " :" (select round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 4 + ") + '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 4 + ") + '%' and len(b.su_no)=9) else (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 4 + ") + '%' and len(b.su_no)=9) end) +isnull(count(*),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + 4 + ") = substring(a.su_no,1," + 4 + ") " + condallbr2 + ") <>0") + " and len(a.su_no)=" + 4 + ""
                        //  + " order by a.su_no";
                             ;
                    qstr = qstr + " union all " + "select a.su_no d1,a.su_name d2,"
                               + " round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 2 + ") + '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 2 + ") + '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_damt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + 2 + ") = substring(a.su_no,1," + 2 + ") " + condallbr2 + ") d3"
                               + ", round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 2 + ") + '%' and len(b.su_no)=9) < 0  then -(select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 2 + ") + '%' and len(b.su_no)=9) else 0 end),4) + (select isnull(sum(b.a_camt),0) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " and c.a_mdate < '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' where substring(b.su_no,1," + 2 + ") = substring(a.su_no,1," + 2 + ") " + condallbr2 + ") d4"
                               + " ,(select round(isnull(sum(b.a_damt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + 2 + ") = substring(a.su_no,1," + 2 + ") " + condallbr2 + ") d5"
                               + " ,(select round(isnull(sum(b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + 2 + ") = substring(a.su_no,1," + 2 + ") " + condallbr2 + ") d6"
                        // + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") > 0 then (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d7"
                        // + " ,case when (select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") < 0 then -(select round((select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + keylen + ") + '%' and len(b.su_no)=9) + isnull(sum(b.a_damt-b.a_camt),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no and c.a_mdate between '" + datval.convert_to_yyyyDDdd(txt_fmdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tmdate.Text) + "' " + condp + " where substring(b.su_no,1," + keylen + ") = substring(a.su_no,1," + keylen + ") " + condallbr2 + ") else 0 end d8"
                              + " ,0.0000 d7"
                              + " ,0.0000 d8"
                               + " from suppliers a where " + (chk_zerobl.Checked ? " 1=1 " :" (select round((case when (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 2 + ") + '%' and len(b.su_no)=9) > 0  then (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 2 + ") + '%' and len(b.su_no)=9) else (select sum(su_opnbal) from suppliers b where b.su_no like ''+ substring(a.su_no,1," + 2 + ") + '%' and len(b.su_no)=9) end) +isnull(count(*),0),4) from acc_dtl b join acc_hdr c on c.a_brno=b.a_brno and c.a_ref=b.a_ref and c.a_type=b.a_type and c.sl_no=b.sl_no and c.pu_no=b.pu_no " + condp + " where substring(b.su_no,1," + 2 + ") = substring(a.su_no,1," + 2 + ") " + condallbr2 + ") <>0 ") + " and len(a.su_no)=" + 2 + ""
                               + " order by a.su_no;";
                    
                }
                dtm = daml.SELECT_QUIRY_only_retrn_dt(qstr);

                foreach (DataRow dtr0 in dtm.Rows)
                {

                    dtr0["d7"] = Convert.ToDouble(dtr0["d3"]) + Convert.ToDouble(dtr0["d5"]);
                    dtr0["d8"] = Convert.ToDouble(dtr0["d4"]) + Convert.ToDouble(dtr0["d6"]);
                }

                DataRow dr = dtm.NewRow();
                double sumopnd = 0, sumopnm = 0, sumtrd = 0, sumtrm = 0, sumttld = 0, sumttlm = 0;
                foreach (DataRow dtr in dtm.Rows)
                {
                    if (dtr[0].ToString().Trim().Length == 9)
                    {
                        sumopnd = sumopnd + Convert.ToDouble(dtr[3]);
                        sumopnm = sumopnm + Convert.ToDouble(dtr[2]);
                        sumtrd = sumtrd + Convert.ToDouble(dtr[5]);
                        sumtrm = sumtrm + Convert.ToDouble(dtr[4]);
                        sumttld = sumttld + Convert.ToDouble(dtr[7]);
                        sumttlm = sumttlm + Convert.ToDouble(dtr[6]);
                    }
                }
                dr[0] = "";
                dr[1] = "الاجمالي";
                dr[2] = sumopnm;
                dr[3] = sumopnd;
                dr[4] = sumtrm;
                dr[5] = sumtrd;
                dr[6] = sumttlm;
                dr[7] = sumttld;

                dtm.Rows.Add(dr);

                dataGridView1.DataSource = dtm;

                foreach (DataGridViewRow dtr in dataGridView1.Rows)
                {
                    if (dtr.Cells[0].Value.ToString().Trim().Length == 6)
                        dataGridView1.Rows[dtr.Index].DefaultCellStyle.BackColor = Color.Tan;

                    if (dtr.Cells[0].Value.ToString().Trim().Length == 4)
                        dataGridView1.Rows[dtr.Index].DefaultCellStyle.BackColor = Color.Khaki;

                    if (dtr.Cells[0].Value.ToString().Trim().Length == 2)
                        dataGridView1.Rows[dtr.Index].DefaultCellStyle.BackColor = Color.LightCoral;
                }

                //for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                //{

                //    dataGridView1.Rows[i].Cells[7].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value.ToString().Trim()) + Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString().Trim());
                //    dataGridView1.Rows[i].Cells[6].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString().Trim()) + Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString().Trim());

                //    //  dataGridView1.Rows[i].Cells[0].Value = seso > 0 ? seso.ToString() : 0.ToString();
                //    //   dataGridView1.Rows[i].Cells[1].Value = seso < 0 ? seso.ToString() : 0.ToString();

                //    //  seso = seso + Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString().Trim()) - Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value.ToString().Trim());
                //    //dataGridView1.Rows[i].Cells[0].Value = seso.ToString();
                //    // dataGridView1.Rows[i].Cells[0].Value = seso > 0 ? seso.ToString() : 0.ToString();
                //    //   dataGridView1.Rows[i].Cells[1].Value = seso < 0 ? seso.ToString() : 0.ToString();

                //}



            }

            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
            }
        }

        private void SalesReport_Load(object sender, EventArgs e)
        {
            txt_fmdate.Text = BL.CLS_Session.start_dt;
            txt_tmdate.Text = BL.CLS_Session.end_dt;
              //  Load_Statment(su_no);
           
                txt_fmdate.Focus();
                // txt_hdate.SelectionStart = 0;
                txt_fmdate.SelectionLength = 0;
           
           
           // double sum = 0;
           // double seso = 0;
           // try
           // {
               
           //         textBox3.Text=su_no;
              
           // //string qstr = "select a.su_no a,a.su_name b,round(a.su_opnbal+isnull(sum(case b.jddbcr when 'D' then b.a_camt else -b.a_damt end),0),2) c"
           // //               + " from suppliers a left outer join acc_dtl b on a.a_comp=b.a_comp and a.su_no=b.su_no and a.glcurrency=b.jdcurr"
           // //               + " and b.su_no<>''"
           // //        // +" where a.glcomp='01' --and a.glopnbal<>0 --and b.jdcurr='' "
           // //               + " where a.a_comp='01' and a.su_no like '"+ textBox3.Text + "%'"
           // //               + " group by a.a_comp,a.su_no,a.su_name,a.su_opnbal"
           // //               + " having a.su_opnbal+isnull(sum(case b.jddbcr when 'D' then b.a_camt else -b.a_damt end),0)<>0"
           // //               + " order by a.su_name";
           //     string qstr2 = "select round((case jddbcr when 'D' then a_damt else - a_camt end),2) as a,a_text as b,a_mdate as c from acc_dtl where su_no='" + su_no + "' and a_comp='01' order by a_mdate";


           //     DataTable dt = daml.SELECT_QUIRY("select su_name,round(su_opnbal,2),su_no from suppliers where su_no='" + su_no + "' and a_comp='01'");
           //     dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

           //     txt_name.Text=dt.Rows[0][0].ToString();
           //     for (int i = 0; i < dataGridView1.Rows.Count; ++i)
           //     {
           //         if (i == 0)
           //         {
           //             seso = Convert.ToDouble(dt.Rows[0][1]) + Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString().Trim());
           //             dataGridView1.Rows[i].Cells[0].Value = seso.ToString();
           //         }
           //         else
           //         {
           //             seso = seso + Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString().Trim());
           //             dataGridView1.Rows[i].Cells[0].Value = seso.ToString();
           //         }
           //         //GridView2.Rows[i].Cells[0].Text = Convert.ToString(seso);
           //       //  dataGridView1.Rows[i].Cells[3].Style[HtmlTextWriterStyle.Direction] = "rtl";
           //       //  dataGridView1.Rows[i].Cells[2].Style[HtmlTextWriterStyle.Direction] = "rtl";
           //     }
               

           // }

           // catch (Exception ex)
           // {
           //     MessageBox.Show(ex.Message);
           // }
           // /*
           // maskedTextBox1.Text = DateTime.Now.ToString("yyyy-MM-01") + "05:00:00";

           // DateTime dt = new DateTime();

           // dt=DateTime.Now.AddDays(1);

           //// maskedTextBox2.Text = DateTime.Now.ToString("yyyy-MM-dd") + "05:00:00";
           // maskedTextBox2.Text = dt.ToString("yyyy-MM-dd") + "05:00:00";
           //  * */
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
                           // XcelApp.Cells[i + 6, j + 1] = "'" + dataGridView1.Rows[i].Cells[j].Value.ToString();
                            XcelApp.Cells[i + 6, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString().StartsWith("0") && dataGridView1.Rows[i].Cells[j].Value.ToString().Length > 1 ? "'" + dataGridView1.Rows[i].Cells[j].Value.ToString() : dataGridView1.Rows[i].Cells[j].Value.ToString();
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
                foreach (var acct in banksuppliers)
                {
                    row++;
                    workSheet.Cells[row, "H"] = acct.ID;
                    workSheet.Cells[row, "J"] = acct.Balance;
                }
                */
                workSheet.Cells[1, "D"] = BL.CLS_Session.cmp_name;
                workSheet.Cells[2, "D"] = BL.CLS_Session.brname;
                workSheet.Cells[3, "D"] = this.Text;

                workSheet.Range["C1", "E1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                workSheet.Range["C2", "E2"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                workSheet.Range["C3", "E3"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                workSheet.Range["A" + (dataGridView1.Rows.Count + 5).ToString(), "H" + (dataGridView1.Rows.Count + 5).ToString()].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                workSheet.Range["A5", "H5"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                // workSheet.Cells[2, "D"]

                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
            //if (dataGridView1.Rows.Count > 0)
            //{
            //    Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
            //    XcelApp.Application.Workbooks.Add(Type.Missing);

            //    for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            //    {
            //        XcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            //    }

            //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //    {
            //        for (int j = 0; j < dataGridView1.Columns.Count; j++)
            //        {
            //            XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j] == dataGridView1.Rows[i].Cells[0] ? "'" + dataGridView1.Rows[i].Cells[j].Value.ToString() : dataGridView1.Rows[i].Cells[j].Value.ToString();
            //        }
            //    }
            //    XcelApp.Columns.AutoFit();
            //    XcelApp.Visible = true;
            //}
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


                DataSet ds1 = new DataSet();




                rf.reportViewer1.LocalReport.DataSources.Clear();
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("details", dtm));
                //  rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("details", dtdtl));

                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Acc.rpt.Acc_Mizan_rpt.rdlc";

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[6];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                parameters[1] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[2] = new ReportParameter("desc", radioButton1.Checked? "ميزان مراجعة مجاميع" : "ميزان مراجعة ارصدة");
                parameters[3] = new ReportParameter("fmdate", txt_fmdate.Text);
                parameters[4] = new ReportParameter("tmdate", txt_tmdate.Text);
                parameters[5] = new ReportParameter("rpt_name", "ميزان مراجعة موردين");
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
             //   parameters[1] = new ReportParameter("mdate", maskedTextBox2.Text);

                rf.reportViewer1.LocalReport.SetParameters(parameters);

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();
              //  rf.reportViewer1.PrintDialog();
            }
            catch { }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F9)
            //{
            //    Acc_Tran f4 = new Acc_Tran(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(),"");
            //    f4.MdiParent = MdiParent;
            //    f4.Show();

             
            //}
        }

        private void dataGridView1_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            Acc_Tran f4 = new Acc_Tran(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
            f4.Show();
             * */
        }

        private void Load_Statment(string aakey)
        {
           // double sum = 0;
            double seso = 0;
            try
            {

               // txt_code.Text = su_no;
                string qstr2 = "select round((case jddbcr when 'D' then a_damt else 0 end),2) as f,round((case jddbcr when 'C' then a_camt else 0 end),2) as e,a_text as d,CONVERT(VARCHAR(10), CAST(a_mdate as date), 105) as c,a_type a,a_ref b from acc_dtl where su_no='" + aakey + "' and a_comp='01' order by a_mdate";
                DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select su_name,round(su_opnbal,2),su_no from suppliers where su_no='" + aakey + "' and a_comp='01'");
                dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt(qstr2);
                /*
                if (dt.Rows.Count > 0)
                {
                    txt_name.Text = dt.Rows[0][0].ToString();
                    txt_code.Text = dt.Rows[0][2].ToString();
                    txt_opnbal.Text = dt.Rows[0][1].ToString();
                }
                else { 
                txt_name.Text = "";
               // txt_code.Text = dt.Rows[0][2].ToString();
                txt_opnbal.Text = "";
                }
                */
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    if (i == 0)
                    {
                        seso = Convert.ToDouble(dt.Rows[0][1]) + Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString().Trim()) - Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value.ToString().Trim());
                        dataGridView1.Rows[i].Cells[0].Value = seso > 0 ? seso.ToString() : 0.ToString();
                        dataGridView1.Rows[i].Cells[1].Value = seso < 0 ? seso.ToString() : 0.ToString();
                    }
                    else
                    {
                        seso = seso + Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString().Trim()) - Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value.ToString().Trim());
                        //dataGridView1.Rows[i].Cells[0].Value = seso.ToString();
                        dataGridView1.Rows[i].Cells[0].Value = seso > 0 ? seso.ToString() : 0.ToString();
                        dataGridView1.Rows[i].Cells[1].Value = seso < 0 ? seso.ToString() : 0.ToString();
                    }
                }


            }

            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
            }

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                // var selectedCell = dataGridView1.SelectedCells[0];
                // do something with selectedCell...
                Search_All f4 = new Search_All("1", "Acc");
                f4.ShowDialog();

                try
                {

               //  txt_code.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
               //  txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
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

                button1_Click(sender, e);


            }

         
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            /*
            Acc_Statment_Exp f4a = new Acc_Statment_Exp(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            f4a.MdiParent = MdiParent;
            f4a.Show();
*/

            
        }

        private void dataGridView1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                Acc_Statment_Exp f4a = new Acc_Statment_Exp(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                f4a.MdiParent = MdiParent;
                f4a.Show();


            }
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
            {
                Sup.Suppliers ac = new Sup.Suppliers(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                ac.MdiParent = ParentForm;
                ac.Show();
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_fmdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_fmdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                   // txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_fmdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                       // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_fmdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
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

        private void txt_fmdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_tmdate.Focus();
                // txt_hdate.SelectionStart = 0;
                txt_tmdate.SelectionLength = 0;
            }
        }

        private void txt_tmdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_tmdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_tmdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    // txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_tmdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_tmdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
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

        private void txt_tmdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
                // txt_hdate.SelectionStart = 0;
              //  txt_tmdate.SelectionLength = 0;
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

        private void chk_openbalonly_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_openbalonly.Checked)
            {
                dataGridView1.Columns[4].Visible = false; dataGridView1.Columns[5].Visible = false; dataGridView1.Columns[6].Visible = false; dataGridView1.Columns[7].Visible = false;
            }
            else
            {
                dataGridView1.Columns[4].Visible = true; dataGridView1.Columns[5].Visible = true; dataGridView1.Columns[6].Visible = true; dataGridView1.Columns[7].Visible = true;
            }

            button1_Click(sender, e);
        }

        private void chk_alllvl_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_alllvl.Checked)
                numericUpDown1.Enabled = false;
            else
                numericUpDown1.Enabled = true;
        }
    }
}
