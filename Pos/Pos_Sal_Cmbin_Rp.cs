using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;
using System.Threading;

namespace POS.Pos
{
    public partial class Pos_Sal_Cmbin_Rp : BaseForm
    {
        DataTable dtt, dthdr, dtdtl, dt222, dtunits, dtsal, dtvat, dttrtyps, dtslctr, dttax, dttbld;
        string a_slctr, a_ref, a_type, strcash, strrcash, strrcrdt, strcrdt, strdsc, strtax, strcashr, stwhno, stbrno, stsndoq, stccno;
        string typeno = "", xxx, zzz, temy, msg;
        // SqlConnection con2 = BL.DAML.con;
        SqlConnection con3 = BL.DAML.con;
        //  BL.EncDec endc = new BL.EncDec();
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
       // string zzz;
        //  int count_hdr, count_dtl;

        public Pos_Sal_Cmbin_Rp()
        {
            InitializeComponent();
        }

        private void Pos_Sal_Cmbin_Load(object sender, EventArgs e)
        {
           // txt_fmdate.Text = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            txt_fmdate.Text = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
          //  txt_tmdate.Text = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

            string sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";

            dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select * from slcenters where sl_brno + sl_no in(" + sl + ") and sl_brno='" + BL.CLS_Session.brno + "'");
            cmb_salctr.DataSource = dtslctr;
            cmb_salctr.DisplayMember = "sl_name";
            cmb_salctr.ValueMember = "sl_no";
          //  cmb_salctr.SelectedIndex = -1;

            cmb_salctr.Select();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
            int qidcount = 0;
            if (cmb_salctr.SelectedIndex == -1)
            {
                MessageBox.Show("يرجى اختيار مركز البيع قبل التكوين");
                cmb_salctr.Focus();
                return;
            }
            if (txt_ref.Text.Equals(""))
            {
                MessageBox.Show("يجب ادخال رقم فاتورة لاصلاحها", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            button2.Text = "يرجى الانتظار";
            button2.Enabled = false;

            progressBar1.Minimum = 0;
           // MessageBox.Show(Convert.ToDateTime(txt_tmdate.Text + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00").AddDays(5).ToString());
            TimeSpan duration = Convert.ToDateTime(txt_fmdate.Text + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00").AddDays(1) - Convert.ToDateTime(txt_fmdate.Text + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00");
           
            int daycnt = Convert.ToInt32(duration.TotalDays);

           //// MessageBox.Show(daycnt.ToString());

            progressBar1.Visible = true;
            progressBar1.Maximum = daycnt ;
           
            for (int i = 0; i < daycnt; i++)
            {
               // MessageBox.Show(i.ToString());
                CultureInfo culture = new CultureInfo("en-US");
                DateTime tempDate = Convert.ToDateTime(datval.convert_to_yyyy_MMddwith_dash(txt_fmdate.Text) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000", culture).AddDays(i+1);
                // string.
                //  DateTime yyy = DateTime.TryParse(xxx).AddHours(24);
                //    MessageBox.Show(tempDate.ToString("dd-MM-yyyy", new CultureInfo("en-US")));
                //   MessageBox.Show(datval.convert_to_yyyy_MMddwith_dash(txt_mdate.Text) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000");

                zzz = tempDate.ToString("dd-MM-yyyy", new CultureInfo("en-US"));

                DateTime tempDate2 = Convert.ToDateTime(datval.convert_to_yyyy_MMddwith_dash(txt_fmdate.Text) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000", culture).AddDays(i);
              //  xxx = txt_fmdate.Text;
                xxx = tempDate2.ToString("dd-MM-yyyy", new CultureInfo("en-US"));

                

              ////  MessageBox.Show(xxx + "\n\r" +zzz);

                try
                {
                    DataTable chktq = daml.SELECT_QUIRY_only_retrn_dt("exec chk_for_taqween @from_date='" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " " + (BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) + ":00:00.000',@to_date='" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " " + (BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) + ":00:00.000',@br_no='" + BL.CLS_Session.brno + "',@sl_no='" + cmb_salctr.SelectedValue + "',@shmel_tax=" + BL.CLS_Session.isshamltax + "");
                  if (chktq.Rows.Count > 0)
                  {
                      MessageBox.Show(" الاجمالي لا يتطابق مع تفاصيل الفواتير يرجى اعادة ارسال الفواتير من الكاشيرات يوم " + xxx, "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      //cmb_salctr.Focus();
                      // return;
                  }
                  else
                  {
                      // int exexcuteds = daml.Insert_Update_Delete_retrn_int("INSERT INTO sales_hdr(branch,slcenter,invtype,ref,invmdate, invhdate,text,remarks,casher,entries,invttl,invdsvl,nettotal,invdspc,tax_amt_rcvd,with_tax,usrid,custno,invcst,suspend) VALUES('" + BL.CLS_Session.brno + "','" + cmb_salctr.SelectedValue + "','" + cmb_type.SelectedValue + "'," + txt_ref.Text  + ",'" + mdate + "','" + hdate + "','" + txt_desc.Text + "','" + txt_remark.Text + "','" + txt_key.Text + "'," + (dataGridView1.RowCount - 1) + "," + txt_total.Text + "," + txt_des.Text + "," + txt_net.Text + "," + txt_desper.Text + "," + txt_tax.Text + "," + (chk_shaml_tax.Checked ? 1 : 0) + ",'" + txt_user.Text + "','" + txt_custno.Text + "'," + txt_cost.Text + "," + (chk_suspend.Checked ? 1 : 0) + ")", false);
                      cmb_salctr_Leave(sender, e);

                      //  System.Data.DataTable dtsalchk = daml.SELECT_QUIRY_only_retrn_dt("select count(*) from pos_hdr where gen_ref=0 and type=1 and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000'");
                      // System.Data.DataTable dtrsalchk = daml.SELECT_QUIRY_only_retrn_dt("select count(*) from pos_hdr where gen_ref=0 and type=0 and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000'");

                      //System.Data.DataTable dtsalhdr = daml.SELECT_QUIRY_only_retrn_dt("select isnull(sum(count),0) count,isnull(sum(total),0) total,isnull(sum(net_total),0) net_total,isnull(sum(discount),0) discount,isnull(sum(tax_amt),0) tax_amt,isnull(sum(gen_ref),0) gen_ref from pos_hdr where gen_ref=0 and type=1 and date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000'");
                      //System.Data.DataTable dtsaldtl = daml.SELECT_QUIRY_only_retrn_dt("select * from pos_dtl a where exists(select * from pos_hdr b where a.brno=b.brno and a.slno=b.slno and a.ref=b.ref and a.contr=b.contr and a.type=b.type and b.type=1 and b.gen_ref=0 and b.date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' )");
                      System.Data.DataTable dtsalhdr = daml.SELECT_QUIRY_only_retrn_dt("select isnull(sum(count),0) count,isnull(sum(total - tax_amt),0) total,isnull(sum(net_total),0) net_total,isnull(sum(discount),0) discount,isnull(sum(tax_amt),0) tax_amt,isnull(sum(gen_ref),0) gen_ref,isnull(sum(taxfree_amt),0) taxfree,isnull(sum(total_cost),0) cost,isnull(round((sum(discount) / sum(total-tax_amt)) * 100,2),0) dscpr from pos_hdr where gen_ref in (0,"+txt_ref.Text+") and type in (1,4,5)  and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "' and date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000'");
                      System.Data.DataTable dtsaldtl = daml.SELECT_QUIRY_only_retrn_dt("select a.[brno],a.[slno],a.[contr],a.[type],a.[barcode],a.[unit],a.[price],sum(a.[qty]) qty,a.[cost],a.[itemno],a.[pkqty],a.[discpc],a.[tax_id],sum(a.[tax_amt]) tax_amt,sum(a.[offr_amt])  offr_amt from pos_dtl a where a.qty<>0 and exists(select * from pos_hdr b where a.brno=b.brno and a.slno=b.slno and a.ref=b.ref and a.contr=b.contr and a.type=b.type and b.type in (1,4,5)  and b.gen_ref in (0," + txt_ref.Text + ") and  b.brno='" + BL.CLS_Session.brno + "' and b.slno='" + cmb_salctr.SelectedValue + "' and b.date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' ) group by a.[brno],a.[slno],a.[contr],a.[type],a.[barcode],a.[unit],a.[price],[qty],a.[cost],a.[itemno],a.[pkqty],a.[discpc],a.[tax_id],a.[tax_amt],a.[offr_amt]");


                      System.Data.DataTable dtrsalhdr = daml.SELECT_QUIRY_only_retrn_dt("select isnull(sum(count),0) count,isnull(sum(total  - tax_amt),0) total,isnull(sum(net_total),0) net_total,isnull(sum(discount),0) discount,isnull(sum(tax_amt),0) tax_amt,isnull(sum(gen_ref),0) gen_ref,isnull(sum(taxfree_amt),0) taxfree,isnull(sum(total_cost),0) cost,isnull(round((sum(discount) / sum(total-tax_amt)) * 100,2),0) dscpr from pos_hdr where gen_ref in (0," + txt_ref.Text + ") and type=0 and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "' and date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000'");
                      System.Data.DataTable dtrsaldtl = daml.SELECT_QUIRY_only_retrn_dt("select a.[brno],a.[slno],a.[contr],a.[type],a.[barcode],a.[unit],a.[price],sum(a.[qty]) qty,a.[cost],a.[itemno],a.[pkqty],a.[discpc],a.[tax_id],sum(a.[tax_amt]) tax_amt,sum(a.[offr_amt]) offr_amt from pos_dtl a where a.qty<>0 and exists(select * from pos_hdr b where a.brno=b.brno and a.slno=b.slno and a.ref=b.ref and a.contr=b.contr and a.type=b.type and gen_ref in (0," + txt_ref.Text + ") and b.type=0 and b.brno='" + BL.CLS_Session.brno + "' and b.slno='" + cmb_salctr.SelectedValue + "' and b.date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' ) group by a.[brno],a.[slno],a.[contr],a.[type],a.[barcode],a.[unit],a.[price],[qty],a.[cost],a.[itemno],a.[pkqty],a.[discpc],a.[tax_id],a.[tax_amt],a.[offr_amt]");

                  //    System.Data.DataTable dtsref = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref)," + BL.CLS_Session.dtbranch.Rows[0]["jv_04"] + ") from sales_hdr where invtype='04' and slcenter='" + cmb_salctr.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "'");
                  //    System.Data.DataTable dtrref = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref)," + BL.CLS_Session.dtbranch.Rows[0]["jv_14"] + ") from sales_hdr where invtype='14' and slcenter='" + cmb_salctr.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "'");
                      ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



                   //   int txt_ref.Text  = Convert.ToInt32(dtsref.Rows[0][0]) + 1;
                   //   int txt_ref.Text  = Convert.ToInt32(dtrref.Rows[0][0]) + 1;


                      //  MessageBox.Show(Convert.ToDouble(dtsalhdr.Rows[0]["count"]).ToString());
                      //  MessageBox.Show(Convert.ToDouble(dtrsalhdr.Rows[0]["count"]).ToString());
                      daml.SqlCon_Open();

                      if (Convert.ToDouble(dtsalhdr.Rows[0]["count"]) <= 0)
                      {
                          //  MessageBox.Show("لا يوجد فواتير بيع للتكوين");
                          // return;
                      }
                      else if (chk_sal.Checked)
                      {

                          int exexcuteds = daml.Insert_Update_Delete_retrn_int("update sales_hdr set invttl=" + (BL.CLS_Session.isshamltax.Equals("1") ? Convert.ToDouble(dtsalhdr.Rows[0]["total"]) : Convert.ToDouble(dtsalhdr.Rows[0]["total"]) + Convert.ToDouble(dtsalhdr.Rows[0]["tax_amt"])) + ",invdsvl=" + dtsalhdr.Rows[0]["discount"] + ",nettotal=" + dtsalhdr.Rows[0]["net_total"] + ",invdspc=" + dtsalhdr.Rows[0]["dscpr"] + ",tax_amt_rcvd=" + dtsalhdr.Rows[0]["tax_amt"] + ",with_tax=" + (BL.CLS_Session.isshamltax.Equals("1") ? 0 : 1) + ",invcst=" + Convert.ToDouble(dtsalhdr.Rows[0]["cost"]) + ",taxfree_amt=" + Convert.ToDouble(dtsalhdr.Rows[0]["taxfree"]) + ",tax_percent=" + BL.CLS_Session.tax_per + " where branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "' and invtype='" + (chk_sal.Checked ? "04" : "14") + "' and ref=" + txt_ref.Text + "", false);
                          int exexcuteds2 = daml.Insert_Update_Delete_retrn_int("update pos_hdr set gen_ref= " + txt_ref.Text + " where type in (1,4,5) and gen_ref=0 and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "'", false);
                          int exexcuteds3 = daml.Insert_Update_Delete_retrn_int("delete from sales_dtl where branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "' and invtype='" + (chk_sal.Checked ? "04" : "14") + "' and ref=" + txt_ref.Text + "", false);
                          
                          int folio = 0;
                          foreach (DataRow row in dtsaldtl.Rows)
                          { // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                              using (SqlCommand cmd = new SqlCommand("INSERT INTO sales_dtl(branch,slcenter,invtype,ref,invmdate, invhdate, itemno, qty, price, pack, pkqty,folio,tax_amt,tax_id,discpc,whno,cost,barcode,offer_amt) VALUES(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13,@a14,@a15,@a16,@a17,@a18,@a19)", con3))
                              {
                                  cmd.Parameters.AddWithValue("@a1", stbrno);
                                  cmd.Parameters.AddWithValue("@a2", cmb_salctr.SelectedValue);
                                  cmd.Parameters.AddWithValue("@a3", "04");
                                  cmd.Parameters.AddWithValue("@a4", txt_ref.Text);
                                  // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                                  cmd.Parameters.AddWithValue("@a5", datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", ""));
                                  // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                                  cmd.Parameters.AddWithValue("@a6", datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")));
                                  cmd.Parameters.AddWithValue("@a7", row["itemno"]);
                                  cmd.Parameters.AddWithValue("@a8", row["qty"]);
                                  cmd.Parameters.AddWithValue("@a9", Math.Round(Convert.ToDouble(row["price"]) - (Convert.ToDouble(row["offr_amt"]) / (Convert.ToDouble(row["qty"]))), 2));
                                  cmd.Parameters.AddWithValue("@a10", row["unit"]);
                                  cmd.Parameters.AddWithValue("@a11", row["pkqty"]);
                                  cmd.Parameters.AddWithValue("@a12", folio + 1);
                                  // DataRow[] dtr = dttax.Select("tex_id=" + row["tax_id"]);
                                  cmd.Parameters.AddWithValue("@a13", row["tax_amt"]);
                                  cmd.Parameters.AddWithValue("@a14", row["tax_id"]);
                                  cmd.Parameters.AddWithValue("@a15", row["discpc"]);
                                  cmd.Parameters.AddWithValue("@a16", stwhno);
                                  cmd.Parameters.AddWithValue("@a17", row["cost"]);
                                  cmd.Parameters.AddWithValue("@a18", row["barcode"]);
                                  // cmd.Parameters.AddWithValue("@a19", row["offr_amt"]);
                                  cmd.Parameters.AddWithValue("@a19", 0);

                                  //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                                  //if (row.Cells[7].Value != null)
                                  // { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                                  // cmd.Parameters.AddWithValue("@c9", comp_id);
                                  // cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                                  //con.Open();
                                  if (con3.State != ConnectionState.Open)
                                  {
                                      con3.Open();
                                  }
                                  cmd.ExecuteNonQuery();
                                  //con.Close();
                                  if (con3.State == ConnectionState.Open)
                                  {
                                      con3.Close();
                                  }
                                  // lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);
                                  folio = folio + 1;
                              }
                          }

                          daml.SqlCon_Open();
                          int exexcutedacd = daml.Insert_Update_Delete_retrn_int("delete from acc_hdr where a_brno='" + BL.CLS_Session.brno + "' and sl_no='" + cmb_salctr.SelectedValue + "' and a_type='" + (chk_sal.Checked ? "04" : "14") + "' and a_ref=" + txt_ref.Text + "", false);


                          int exexcutedac = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_brno,a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,usrid,jhsrc,sl_no)"
                                         + " VALUES('" + stbrno + "','04'," + txt_ref.Text + ",'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "',' مبيعات نقدية من الكاشير '," + (Convert.ToDouble(dtsalhdr.Rows[0]["net_total"]) + Convert.ToDouble(dtsalhdr.Rows[0]["discount"])) + "," + 0 + ",'" + BL.CLS_Session.UserName + "','Pos','" + cmb_salctr.SelectedValue + "')", false);

                          daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + stbrno + "','04'," + txt_ref.Text  + ",'" + strcash + "',' مبيعات نقدية من الكاشير '," + (Convert.ToDouble(dtsalhdr.Rows[0]["net_total"]) - Convert.ToDouble(dtsalhdr.Rows[0]["tax_amt"]) + Convert.ToDouble(dtsalhdr.Rows[0]["discount"])) + ",0,1,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','C','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);
                          if (Convert.ToDouble(dtsalhdr.Rows[0]["tax_amt"]) > 0)
                          {
                              daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,taxcatId,cc_no) VALUES('" + stbrno + "','04'," + txt_ref.Text  + ",'" + strtax + "','ضريبة مبيعات من الكاشير'," + Convert.ToDouble(dtsalhdr.Rows[0]["tax_amt"]) + ", 0,2,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','C','" + cmb_salctr.SelectedValue + "',1,'" + stccno + "')", false);
                          }
                          daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,cu_no,sl_no,cc_no) VALUES('" + stbrno + "','04'," + txt_ref.Text  + ",'" + stsndoq + "',' مبيعات نقدية من الكاشير ',0," + Convert.ToDouble(dtsalhdr.Rows[0]["net_total"]) + ",3,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','D','','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);

                          if (Convert.ToDouble(dtsalhdr.Rows[0]["discount"]) > 0)
                          {
                              daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + stbrno + "','04'," + txt_ref.Text  + ",'" + strdsc + "','خصومات مبيعات من الكاشير'," + 0 + "," + Convert.ToDouble(dtsalhdr.Rows[0]["discount"]) + ",4,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','D','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);
                          }
                          daml.SqlCon_Close();
                          //  daml.SqlCon_Close(); 
                          dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sales_dtl where invtype='04' and ref=" + txt_ref.Text  + " and branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "'");


                          Thread t = new Thread(() =>
                          {
                              try
                              {
                                  using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
                                  {

                                      cmd.CommandType = CommandType.StoredProcedure;
                                      cmd.Connection = con3;
                                      cmd.Parameters.AddWithValue("@items_tran_tb", dttbld);
                                      con3.Open();
                                      cmd.ExecuteNonQuery();
                                      con3.Close();

                                      //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                                  }
                              }
                              catch { }
                          });

                          //   MessageBox.Show("تم تكوين المبيعات");
                          qidcount = qidcount + 1;
                      }

                      if (Convert.ToDouble(dtrsalhdr.Rows[0]["count"]) <= 0)
                      {
                          //   MessageBox.Show("لا يوجد فواتير مرتجع للتكوين");
                          //  return;
                      }
                      else if (chk_rsal.Checked)
                      {

                          int exexcuteds = daml.Insert_Update_Delete_retrn_int("update sales_hdr set invttl=" + (BL.CLS_Session.isshamltax.Equals("1") ? Convert.ToDouble(dtrsalhdr.Rows[0]["total"]) : Convert.ToDouble(dtrsalhdr.Rows[0]["total"]) + Convert.ToDouble(dtrsalhdr.Rows[0]["tax_amt"])) + ",invdsvl=" + dtrsalhdr.Rows[0]["discount"] + ",nettotal=" + dtrsalhdr.Rows[0]["net_total"] + ",invdspc=" + dtrsalhdr.Rows[0]["dscpr"] + ",tax_amt_rcvd=" + dtrsalhdr.Rows[0]["tax_amt"] + ",with_tax=" + (BL.CLS_Session.isshamltax.Equals("1") ? 0 : 1) + ",invcst=" + Convert.ToDouble(dtrsalhdr.Rows[0]["cost"]) + ",taxfree_amt=" + Convert.ToDouble(dtrsalhdr.Rows[0]["taxfree"]) + ",tax_percent=" + BL.CLS_Session.tax_per + " where branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "' and invtype='" + (chk_sal.Checked ? "04" : "14") + "' and ref=" + txt_ref.Text + "", false);
                          int exexcutedr2 = daml.Insert_Update_Delete_retrn_int("update pos_hdr set gen_ref= " + txt_ref.Text + " where type=0 and gen_ref=0 and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "'", false);
                          int exexcuteds3 = daml.Insert_Update_Delete_retrn_int("delete from sales_dtl where branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "' and invtype='" + (chk_sal.Checked ? "04" : "14") + "' and ref=" + txt_ref.Text + "", false);
                          
                          int folio = 0;
                          foreach (DataRow row in dtrsaldtl.Rows)
                          { // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                              using (SqlCommand cmd = new SqlCommand("INSERT INTO sales_dtl(branch,slcenter,invtype,ref,invmdate, invhdate, itemno, qty, price, pack, pkqty,folio,tax_amt,tax_id,discpc,whno,cost,barcode,offer_amt) VALUES(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13,@a14,@a15,@a16,@a17,@a18,@a19)", con3))
                              {
                                  cmd.Parameters.AddWithValue("@a1", stbrno);
                                  cmd.Parameters.AddWithValue("@a2", cmb_salctr.SelectedValue);
                                  cmd.Parameters.AddWithValue("@a3", "14");
                                  cmd.Parameters.AddWithValue("@a4", txt_ref.Text );
                                  // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                                  cmd.Parameters.AddWithValue("@a5", datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", ""));
                                  // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                                  cmd.Parameters.AddWithValue("@a6", datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")));
                                  cmd.Parameters.AddWithValue("@a7", row["itemno"]);
                                  cmd.Parameters.AddWithValue("@a8", row["qty"]);
                                  cmd.Parameters.AddWithValue("@a9", Math.Round(Convert.ToDouble(row["price"]) - (Convert.ToDouble(row["offr_amt"]) / (Convert.ToDouble(row["qty"]))), 2));
                                  cmd.Parameters.AddWithValue("@a10", row["unit"]);
                                  cmd.Parameters.AddWithValue("@a11", row["pkqty"]);
                                  cmd.Parameters.AddWithValue("@a12", folio + 1);
                                  // DataRow[] dtr = dttax.Select("tex_id=" + row["tax_id"]);
                                  cmd.Parameters.AddWithValue("@a13", row["tax_amt"]);
                                  cmd.Parameters.AddWithValue("@a14", row["tax_id"]);
                                  cmd.Parameters.AddWithValue("@a15", row["discpc"]);
                                  cmd.Parameters.AddWithValue("@a16", stwhno);
                                  cmd.Parameters.AddWithValue("@a17", row["cost"]);
                                  cmd.Parameters.AddWithValue("@a18", row["barcode"]);
                                  // cmd.Parameters.AddWithValue("@a19", row["offr_amt"]);
                                  cmd.Parameters.AddWithValue("@a19", 0);
                                  //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                                  //if (row.Cells[7].Value != null)
                                  // { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                                  // cmd.Parameters.AddWithValue("@c9", comp_id);
                                  // cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                                  //con.Open();
                                  if (con3.State != ConnectionState.Open)
                                  {
                                      con3.Open();
                                  }
                                  cmd.ExecuteNonQuery();
                                  //con.Close();
                                  if (con3.State == ConnectionState.Open)
                                  {
                                      con3.Close();
                                  }
                                  // lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);
                                  folio = folio + 1;
                              }
                          }

                          daml.SqlCon_Open();

                          int exexcutedacd = daml.Insert_Update_Delete_retrn_int("delete from acc_hdr where a_brno='" + BL.CLS_Session.brno + "' and sl_no='" + cmb_salctr.SelectedValue + "' and a_type='" + (chk_sal.Checked ? "04" : "14") + "' and a_ref=" + txt_ref.Text + "", false);

                          int exexcutedac = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_brno,a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,usrid,jhsrc,sl_no)"
                                         + " VALUES('" + stbrno + "','14'," + txt_ref.Text  + ",'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "',' مرتجعات نقدية من الكاشير '," + (Convert.ToDouble(dtrsalhdr.Rows[0]["net_total"]) + Convert.ToDouble(dtrsalhdr.Rows[0]["discount"])) + "," + 0 + ",'" + BL.CLS_Session.UserName + "','Pos','" + cmb_salctr.SelectedValue + "')", false);


                          daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + stbrno + "','14'," + txt_ref.Text  + ",'" + strrcash + "',' مرتجعات نقدية من الكاشير '," + (Convert.ToDouble(dtrsalhdr.Rows[0]["net_total"]) - Convert.ToDouble(dtrsalhdr.Rows[0]["tax_amt"]) + Convert.ToDouble(dtrsalhdr.Rows[0]["discount"])) + ",0,1,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','D','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);
                          if (Convert.ToDouble(dtrsalhdr.Rows[0]["tax_amt"]) > 0)
                          {
                              daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,taxcatId,cc_no) VALUES('" + stbrno + "','14'," + txt_ref.Text  + ",'" + strtax + "','ضريبة مرتجعات من الكاشير'," + Convert.ToDouble(dtrsalhdr.Rows[0]["tax_amt"]) + ", 0,2,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','D','" + cmb_salctr.SelectedValue + "',1,'" + stccno + "')", false);
                          }
                          daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,cu_no,sl_no,cc_no) VALUES('" + stbrno + "','14'," + txt_ref.Text  + ",'" + stsndoq + "',' مرتجعات نقدية من الكاشير ',0," + Convert.ToDouble(dtrsalhdr.Rows[0]["net_total"]) + ",3,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','C','','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);

                          if (Convert.ToDouble(dtrsalhdr.Rows[0]["discount"]) > 0)
                          {
                              daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + stbrno + "','14'," + txt_ref.Text  + ",'" + strdsc + "','خصومات مرتجعات من الكاشير'," + 0 + "," + Convert.ToDouble(dtrsalhdr.Rows[0]["discount"]) + ",4,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','C','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);
                          }
                          // daml.SqlCon_Close();
                          daml.SqlCon_Close();

                          dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sales_dtl where invtype='14' and ref=" + txt_ref.Text  + " and branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "'");


                          Thread t = new Thread(() =>
                          {
                              try
                              {
                                  using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
                                  {

                                      cmd.CommandType = CommandType.StoredProcedure;
                                      cmd.Connection = con3;
                                      cmd.Parameters.AddWithValue("@items_tran_tb", dttbld);
                                      con3.Open();
                                      cmd.ExecuteNonQuery();
                                      con3.Close();

                                      //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                                  }
                              }
                              catch { }
                          });
                          //   MessageBox.Show("تم تكوين المرتجعات");
                          qidcount = qidcount + 1;
                      }

                      ////////////////////////////////////////////////////////////////////////////////////AGL/////////////////////////////////////////////////////////////////////////////////////////
                      System.Data.DataTable dt_agl = daml.SELECT_QUIRY_only_retrn_dt("SELECT [cust_no] FROM [pos_hdr] where [cust_no]<>0 and date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' group by [cust_no]");

                      // MessageBox.Show(dt_agl.Rows.Count.ToString());
                      foreach (DataRow dtar in dt_agl.Rows)
                      {
                          System.Data.DataTable dt_cuclass = daml.SELECT_QUIRY_only_retrn_dt("select a.cu_no,b.cl_acc from customers a join cuclass b on a.cu_class=b.cl_no  where a.inactive=0 and a.cu_brno='" + BL.CLS_Session.brno + "' and cu_no='" + dtar[0].ToString() + "'");

                          System.Data.DataTable dtsalhdr_agl = daml.SELECT_QUIRY_only_retrn_dt("select isnull(sum(count),0) count,isnull(sum(total - tax_amt),0) total,isnull(sum(net_total),0) net_total,isnull(sum(discount),0) discount,isnull(sum(tax_amt),0) tax_amt,isnull(sum(gen_ref),0) gen_ref,isnull(sum(taxfree_amt),0) taxfree,isnull(sum(total_cost),0) cost,isnull(round((sum(discount) / sum(total-tax_amt)) * 100,2),0) dscpr from pos_hdr where gen_ref=0 and cust_no=" + dtar[0].ToString() + " and type=3 and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "' and date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000'");
                          System.Data.DataTable dtsaldtl_agl = daml.SELECT_QUIRY_only_retrn_dt("select a.[brno],a.[slno],a.[contr],a.[type],a.[barcode],a.[unit],a.[price],sum(a.[qty]) qty,a.[cost],a.[itemno],a.[pkqty],a.[discpc],a.[tax_id],sum(a.[tax_amt]) tax_amt,sum(a.[offr_amt])  offr_amt  from pos_dtl a where a.qty<>0 and exists(select * from pos_hdr b where a.brno=b.brno and a.slno=b.slno and a.ref=b.ref and a.contr=b.contr and a.type=b.type and b.type=3 and b.cust_no=" + dtar[0].ToString() + "  and b.gen_ref=0 and b.brno='" + BL.CLS_Session.brno + "' and b.slno='" + cmb_salctr.SelectedValue + "' and b.date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' ) group by a.[brno],a.[slno],a.[contr],a.[type],a.[barcode],a.[unit],a.[price],[qty],a.[cost],a.[itemno],a.[pkqty],a.[discpc],a.[tax_id],a.[tax_amt],a.[offr_amt]");


                          System.Data.DataTable dtrsalhdr_agl = daml.SELECT_QUIRY_only_retrn_dt("select isnull(sum(count),0) count,isnull(sum(total - tax_amt),0) total,isnull(sum(net_total),0) net_total,isnull(sum(discount),0) discount,isnull(sum(tax_amt),0) tax_amt,isnull(sum(gen_ref),0) gen_ref,isnull(sum(taxfree_amt),0) taxfree,isnull(sum(total_cost),0) cost,isnull(round((sum(discount) / sum(total-tax_amt)) * 100,2),0) dscpr from pos_hdr where gen_ref=0 and type=2 and cust_no=" + dtar[0].ToString() + " and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "' and date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000'");
                          System.Data.DataTable dtrsaldtl_agl = daml.SELECT_QUIRY_only_retrn_dt("select a.[brno],a.[slno],a.[contr],a.[type],a.[barcode],a.[unit],a.[price],sum(a.[qty]) qty,a.[cost],a.[itemno],a.[pkqty],a.[discpc],a.[tax_id],sum(a.[tax_amt]) tax_amt,sum(a.[offr_amt]) offr_amt  from pos_dtl a where a.qty<>0 and exists(select * from pos_hdr b where a.brno=b.brno and a.slno=b.slno and a.ref=b.ref and a.contr=b.contr and a.type=b.type and b.gen_ref=0 and b.type=2 and b.cust_no=" + dtar[0].ToString() + " and b.brno='" + BL.CLS_Session.brno + "' and b.slno='" + cmb_salctr.SelectedValue + "' and b.date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' ) group by a.[brno],a.[slno],a.[contr],a.[type],a.[barcode],a.[unit],a.[price],[qty],a.[cost],a.[itemno],a.[pkqty],a.[discpc],a.[tax_id],a.[tax_amt],a.[offr_amt]");

                          System.Data.DataTable dtsref_agl = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref)," + BL.CLS_Session.dtbranch.Rows[0]["jv_05"] + ") from sales_hdr where invtype='05' and slcenter='" + cmb_salctr.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "'");
                          System.Data.DataTable dtrref_agl = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref)," + BL.CLS_Session.dtbranch.Rows[0]["jv_15"] + ") from sales_hdr where invtype='15' and slcenter='" + cmb_salctr.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "'");

                          int ref_max_agl = Convert.ToInt32(dtsref_agl.Rows[0][0]) + 1;
                          int ref_rmax_agl = Convert.ToInt32(dtrref_agl.Rows[0][0]) + 1;

                          if (Convert.ToDouble(dtsalhdr_agl.Rows[0]["count"]) <= 0)
                          {
                              //  MessageBox.Show("لا يوجد فواتير بيع اجل للتكوين");
                              // return;
                          }
                          else
                          {

                              int exexcuteds = daml.Insert_Update_Delete_retrn_int("INSERT INTO sales_hdr(branch,slcenter,invtype,ref,invmdate, invhdate,text,remarks,casher,entries,invttl,invdsvl,nettotal,invdspc,tax_amt_rcvd,with_tax,usrid,custno,invcst,suspend,glser,taxfree_amt,tax_percent)"
                                             + " VALUES('" + stbrno + "','" + cmb_salctr.SelectedValue + "','05'," + ref_max_agl + ",'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','مبيعات اجله من الكاشير','" + dt_cuclass.Rows[0][1].ToString() + "','" + stsndoq + "'," + dtsalhdr_agl.Rows[0]["count"] + "," + (BL.CLS_Session.isshamltax.Equals("1") ? Convert.ToDouble(dtsalhdr_agl.Rows[0]["total"]) : Convert.ToDouble(dtsalhdr_agl.Rows[0]["total"]) + Convert.ToDouble(dtsalhdr_agl.Rows[0]["tax_amt"])) + "," + dtsalhdr_agl.Rows[0]["discount"] + "," + dtsalhdr_agl.Rows[0]["net_total"] + "," + dtsalhdr_agl.Rows[0]["dscpr"] + "," + dtsalhdr_agl.Rows[0]["tax_amt"] + "," + (BL.CLS_Session.isshamltax.Equals("1") ? 0 : 1) + ",'" + BL.CLS_Session.UserName + "','" + dtar[0].ToString() + "'," + Convert.ToDouble(dtsalhdr_agl.Rows[0]["cost"]) + "," + 0 + ",'Pos'," + Convert.ToDouble(dtsalhdr_agl.Rows[0]["taxfree"]) + "," + BL.CLS_Session.tax_per + ")", false);
                              int exexcuteds2 = daml.Insert_Update_Delete_retrn_int("update pos_hdr set gen_ref= " + ref_max_agl + " where type=3 and gen_ref=0 and cust_no=" + dtar[0].ToString() + " and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "'", false);

                              int folio = 0;
                              foreach (DataRow row in dtsaldtl_agl.Rows)
                              { // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                                  using (SqlCommand cmd = new SqlCommand("INSERT INTO sales_dtl(branch,slcenter,invtype,ref,invmdate, invhdate, itemno, qty, price, pack, pkqty,folio,tax_amt,tax_id,discpc,whno,cost,barcode,offer_amt) VALUES(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13,@a14,@a15,@a16,@a17,@a18,@a19)", con3))
                                  {
                                      cmd.Parameters.AddWithValue("@a1", stbrno);
                                      cmd.Parameters.AddWithValue("@a2", cmb_salctr.SelectedValue);
                                      cmd.Parameters.AddWithValue("@a3", "05");
                                      cmd.Parameters.AddWithValue("@a4", ref_max_agl);
                                      // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                                      cmd.Parameters.AddWithValue("@a5", datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", ""));
                                      // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                                      cmd.Parameters.AddWithValue("@a6", datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")));
                                      cmd.Parameters.AddWithValue("@a7", row["itemno"]);
                                      cmd.Parameters.AddWithValue("@a8", row["qty"]);
                                      cmd.Parameters.AddWithValue("@a9", Math.Round(Convert.ToDouble(row["price"]) - (Convert.ToDouble(row["offr_amt"]) / (Convert.ToDouble(row["qty"]))), 2));
                                      cmd.Parameters.AddWithValue("@a10", row["unit"]);
                                      cmd.Parameters.AddWithValue("@a11", row["pkqty"]);
                                      cmd.Parameters.AddWithValue("@a12", folio + 1);
                                      // DataRow[] dtr = dttax.Select("tex_id=" + row["tax_id"]);
                                      cmd.Parameters.AddWithValue("@a13", row["tax_amt"]);
                                      cmd.Parameters.AddWithValue("@a14", row["tax_id"]);
                                      cmd.Parameters.AddWithValue("@a15", row["discpc"]);
                                      cmd.Parameters.AddWithValue("@a16", stwhno);
                                      cmd.Parameters.AddWithValue("@a17", row["cost"]);
                                      cmd.Parameters.AddWithValue("@a18", row["barcode"]);
                                      // cmd.Parameters.AddWithValue("@a19", row["offr_amt"]);
                                      cmd.Parameters.AddWithValue("@a19", 0);
                                      //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                                      //if (row.Cells[7].Value != null)
                                      // { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                                      // cmd.Parameters.AddWithValue("@c9", comp_id);
                                      // cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                                      //con.Open();
                                      if (con3.State != ConnectionState.Open)
                                      {
                                          con3.Open();
                                      }
                                      cmd.ExecuteNonQuery();
                                      //con.Close();
                                      if (con3.State == ConnectionState.Open)
                                      {
                                          con3.Close();
                                      }
                                      // lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);
                                      folio = folio + 1;
                                  }
                              }

                              daml.SqlCon_Open();
                              int exexcutedac = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_brno,a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,usrid,jhsrc,sl_no)"
                                             + " VALUES('" + stbrno + "','05'," + ref_max_agl + ",'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "',' مبيعات اجلة من الكاشير '," + (Convert.ToDouble(dtsalhdr.Rows[0]["net_total"]) + Convert.ToDouble(dtsalhdr.Rows[0]["discount"])) + "," + dtsalhdr_agl.Rows[0]["count"] + ",'" + BL.CLS_Session.UserName + "','Pos','" + cmb_salctr.SelectedValue + "')", false);

                              daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + stbrno + "','05'," + ref_max_agl + ",'" + strcrdt + "',' مبيعات اجلة من الكاشير '," + (Convert.ToDouble(dtsalhdr_agl.Rows[0]["net_total"]) - Convert.ToDouble(dtsalhdr_agl.Rows[0]["tax_amt"]) + Convert.ToDouble(dtsalhdr_agl.Rows[0]["discount"])) + ",0,1,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','C','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);
                              if (Convert.ToDouble(dtsalhdr_agl.Rows[0]["tax_amt"]) > 0)
                              {
                                  daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,taxcatId,cc_no) VALUES('" + stbrno + "','05'," + ref_max_agl + ",'" + strtax + "','ضريبة مبيعات من الكاشير'," + Convert.ToDouble(dtsalhdr_agl.Rows[0]["tax_amt"]) + ", 0,2,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','C','" + cmb_salctr.SelectedValue + "',1,'" + stccno + "')", false);
                              }
                              daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,cu_no,sl_no,cc_no) VALUES('" + stbrno + "','05'," + ref_max_agl + ",'" + dt_cuclass.Rows[0][1].ToString() + "',' مبيعات اجلة من الكاشير ',0," + Convert.ToDouble(dtsalhdr_agl.Rows[0]["net_total"]) + ",3,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','D','" + dtar[0].ToString() + "','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);

                              if (Convert.ToDouble(dtsalhdr_agl.Rows[0]["discount"]) > 0)
                              {
                                  daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + stbrno + "','05'," + ref_max_agl + ",'" + strdsc + "','خصومات مبيعات من الكاشير'," + 0 + "," + Convert.ToDouble(dtsalhdr_agl.Rows[0]["discount"]) + ",4,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','D','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);
                              }
                              daml.SqlCon_Close();
                              //  daml.SqlCon_Close(); 
                              dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sales_dtl where invtype='05' and ref=" + ref_max_agl + " and branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "'");


                              Thread t = new Thread(() =>
                              {
                                  try
                                  {
                                      using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
                                      {

                                          cmd.CommandType = CommandType.StoredProcedure;
                                          cmd.Connection = con3;
                                          cmd.Parameters.AddWithValue("@items_tran_tb", dttbld);
                                          con3.Open();
                                          cmd.ExecuteNonQuery();
                                          con3.Close();

                                          //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                                      }
                                  }
                                  catch { }
                              });

                              //   MessageBox.Show("تم تكوين المبيعات الاجل");
                              qidcount = qidcount + 1;
                          }

                          if (Convert.ToDouble(dtrsalhdr_agl.Rows[0]["count"]) <= 0)
                          {
                              //   MessageBox.Show("لا يوجد فواتير مرتجع اجل للتكوين");
                              //  return;
                          }
                          else
                          {

                              int exexcutedr = daml.Insert_Update_Delete_retrn_int("INSERT INTO sales_hdr(branch,slcenter,invtype,ref,invmdate, invhdate,text,remarks,casher,entries,invttl,invdsvl,nettotal,invdspc,tax_amt_rcvd,with_tax,usrid,custno,invcst,suspend,glser,taxfree_amt,tax_percent)"
                                             + " VALUES('" + stbrno + "','" + cmb_salctr.SelectedValue + "','15'," + ref_rmax_agl + ",'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','مرتجعات اجلة من الكاشير','" + dt_cuclass.Rows[0][1].ToString() + "','" + stsndoq + "'," + dtrsalhdr_agl.Rows[0]["count"] + "," + (BL.CLS_Session.isshamltax.Equals("1") ? Convert.ToDouble(dtrsalhdr_agl.Rows[0]["total"]) : Convert.ToDouble(dtrsalhdr_agl.Rows[0]["total"]) + Convert.ToDouble(dtrsalhdr_agl.Rows[0]["tax_amt"])) + "," + dtrsalhdr_agl.Rows[0]["discount"] + "," + dtrsalhdr_agl.Rows[0]["net_total"] + "," + dtrsalhdr_agl.Rows[0]["dscpr"] + "," + dtrsalhdr_agl.Rows[0]["tax_amt"] + "," + (BL.CLS_Session.isshamltax.Equals("1") ? 0 : 1) + ",'" + BL.CLS_Session.UserName + "','" + dtar[0].ToString() + "'," + Convert.ToDouble(dtrsalhdr_agl.Rows[0]["cost"]) + "," + 0 + ",'Pos'," + Convert.ToDouble(dtrsalhdr_agl.Rows[0]["taxfree"]) + "," + BL.CLS_Session.tax_per + ")", false);
                              int exexcutedr2 = daml.Insert_Update_Delete_retrn_int("update pos_hdr set gen_ref= " + ref_rmax_agl + " where type=2 and gen_ref=0 and cust_no=" + dtar[0].ToString() + " and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000' and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "'", false);

                              int folio = 0;
                              foreach (DataRow row in dtrsaldtl_agl.Rows)
                              { // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                                  using (SqlCommand cmd = new SqlCommand("INSERT INTO sales_dtl(branch,slcenter,invtype,ref,invmdate, invhdate, itemno, qty, price, pack, pkqty,folio,tax_amt,tax_id,discpc,whno,cost,barcode,offer_amt) VALUES(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13,@a14,@a15,@a16,@a17,@a18,@a19)", con3))
                                  {
                                      cmd.Parameters.AddWithValue("@a1", stbrno);
                                      cmd.Parameters.AddWithValue("@a2", cmb_salctr.SelectedValue);
                                      cmd.Parameters.AddWithValue("@a3", "15");
                                      cmd.Parameters.AddWithValue("@a4", ref_rmax_agl);
                                      // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                                      //cmd.Parameters.AddWithValue("@a5", xxx.Replace("-", ""));
                                      //// cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                                      //cmd.Parameters.AddWithValue("@a6", datval.convert_to_hdate(xxx).Replace("-", ""));
                                      cmd.Parameters.AddWithValue("@a5", datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", ""));
                                      // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                                      cmd.Parameters.AddWithValue("@a6", datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")));
                                      cmd.Parameters.AddWithValue("@a7", row["itemno"]);
                                      cmd.Parameters.AddWithValue("@a8", row["qty"]);
                                      cmd.Parameters.AddWithValue("@a9", Math.Round(Convert.ToDouble(row["price"]) - (Convert.ToDouble(row["offr_amt"]) / (Convert.ToDouble(row["qty"]))), 2));
                                      cmd.Parameters.AddWithValue("@a10", row["unit"]);
                                      cmd.Parameters.AddWithValue("@a11", row["pkqty"]);
                                      cmd.Parameters.AddWithValue("@a12", folio + 1);
                                      // DataRow[] dtr = dttax.Select("tex_id=" + row["tax_id"]);
                                      cmd.Parameters.AddWithValue("@a13", row["tax_amt"]);
                                      cmd.Parameters.AddWithValue("@a14", row["tax_id"]);
                                      cmd.Parameters.AddWithValue("@a15", row["discpc"]);
                                      cmd.Parameters.AddWithValue("@a16", stwhno);
                                      cmd.Parameters.AddWithValue("@a17", row["cost"]);
                                      cmd.Parameters.AddWithValue("@a18", row["barcode"]);
                                      // cmd.Parameters.AddWithValue("@a19", row["offr_amt"]);
                                      cmd.Parameters.AddWithValue("@a19", 0);
                                      //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                                      //if (row.Cells[7].Value != null)
                                      // { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                                      // cmd.Parameters.AddWithValue("@c9", comp_id);
                                      // cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                                      //con.Open();
                                      if (con3.State != ConnectionState.Open)
                                      {
                                          con3.Open();
                                      }
                                      cmd.ExecuteNonQuery();
                                      //con.Close();
                                      if (con3.State == ConnectionState.Open)
                                      {
                                          con3.Close();
                                      }
                                      // lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);
                                      folio = folio + 1;
                                  }
                              }

                              daml.SqlCon_Open();
                              int exexcutedac = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_brno,a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,usrid,jhsrc,sl_no)"
                                             + " VALUES('" + stbrno + "','15'," + ref_rmax_agl + ",'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "',' مرتجع مبيعات اجلة من الكاشير '," + (Convert.ToDouble(dtsalhdr.Rows[0]["net_total"]) + Convert.ToDouble(dtsalhdr.Rows[0]["discount"])) + "," + dtrsalhdr_agl.Rows[0]["count"] + ",'" + BL.CLS_Session.UserName + "','Pos','" + cmb_salctr.SelectedValue + "')", false);


                              daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + stbrno + "','15'," + ref_rmax_agl + ",'" + strrcrdt + "',' مرتجع مبيعات اجلة من الكاشير '," + (Convert.ToDouble(dtrsalhdr_agl.Rows[0]["net_total"]) - Convert.ToDouble(dtrsalhdr_agl.Rows[0]["tax_amt"]) + Convert.ToDouble(dtrsalhdr_agl.Rows[0]["discount"])) + ",0,1,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','D','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);
                              if (Convert.ToDouble(dtrsalhdr_agl.Rows[0]["tax_amt"]) > 0)
                              {
                                  daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,taxcatId,cc_no) VALUES('" + stbrno + "','15'," + ref_rmax_agl + ",'" + strtax + "','ضريبة مبيعات من الكاشير'," + Convert.ToDouble(dtrsalhdr_agl.Rows[0]["tax_amt"]) + ", 0,2,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','D','" + cmb_salctr.SelectedValue + "',1,'" + stccno + "')", false);
                              }
                              daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,cu_no,sl_no,cc_no) VALUES('" + stbrno + "','15'," + ref_rmax_agl + ",'" + dt_cuclass.Rows[0][1].ToString() + "',' مرتجع مبيعات اجلة من الكاشير ',0," + Convert.ToDouble(dtrsalhdr_agl.Rows[0]["net_total"]) + ",3,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','C','" + dtar[0].ToString() + "','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);

                              if (Convert.ToDouble(dtrsalhdr_agl.Rows[0]["discount"]) > 0)
                              {
                                  daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + stbrno + "','15," + ref_rmax_agl + ",'" + strdsc + "','خصومات مبيعات من الكاشير'," + 0 + "," + Convert.ToDouble(dtrsalhdr_agl.Rows[0]["discount"]) + ",4,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','C','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);
                              }
                              // daml.SqlCon_Close();
                              daml.SqlCon_Close();
                              dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sales_dtl where invtype='15' and ref=" + ref_rmax_agl + " and branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "'");


                              Thread t = new Thread(() =>
                              {
                                  try
                                  {
                                      using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
                                      {

                                          cmd.CommandType = CommandType.StoredProcedure;
                                          cmd.Connection = con3;
                                          cmd.Parameters.AddWithValue("@items_tran_tb", dttbld);
                                          con3.Open();
                                          cmd.ExecuteNonQuery();
                                          con3.Close();

                                          //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                                      }
                                  }
                                  catch { }
                              });
                              //  MessageBox.Show("تم تكوين المرتجعات الاجل");
                              qidcount = qidcount + 1;
                          }
                      }



                      /*
                      System.Data.DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(a_ref),0) from acc_hdr where a_type='01'");
                      int mref = Convert.ToInt32(dt.Rows[0][0]) + 1;
                      daml.SqlCon_Open();
                      // exexcuted = exexcuted + daml.Insert_Update_Delete("delete from acc_dtl where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "'", false);
                    //  daml.SqlCon_Close();

                      System.Data.DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select sum(total),sum(discount),sum(net_total) from pos_hdr where gen_ref=0 and date between'" + xxx + "' and '" + zzz + "'");
                      string aamt = dt2.Rows[0][2].ToString();

                      int exexcuted = daml.Insert_Update_Delete_retrn_int("update pos_hdr set gen_ref= " + mref + " where gen_ref=0 and date between'" + xxx + "' and '" + zzz + "'", false);
            
                     // daml.SqlCon_Open();
                      int exexcuted2 = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,jhsrc) VALUES('01'," + mref + ",(CONVERT([varchar],getdate(),(112))),(CONVERT([varchar],CONVERT([date],CONVERT([varchar],getdate(),(131)),(103)),(112))),'مبيعات نقدية من الكاشير'," + aamt + ",2,'SL')", false);
                      int exexcuted3 = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_dtl(a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr) VALUES('01'," + mref + ",'010201001','مبيعات نقدية من الكاشير','" + aamt + "',0,1,(CONVERT([varchar],getdate(),(112))),(CONVERT([varchar],CONVERT([date],CONVERT([varchar],getdate(),(131)),(103)),(112))),'D')", false);
                      int exexcuted4 = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_dtl(a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr) VALUES('01'," + mref + ",'040101001','مبيعات نقدية من الكاشير','" + aamt + "',0,2,(CONVERT([varchar],getdate(),(112))),(CONVERT([varchar],CONVERT([date],CONVERT([varchar],getdate(),(131)),(103)),(112))),'C')", false);
                      */


                      daml.SqlCon_Close();

                      progressBar1.Value = i + 1;



                  }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                   
                }


            }

            

            
            button2.Text = "تكوين الفواتير والقيود";

            MessageBox.Show("       تمت المهمة بنجاح       " + "\n\r\n\r عدد القيود التي تم اصلاح تكوينها " + qidcount.ToString(), "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            progressBar1.Visible = false;
            button2.Enabled = true;
            /*
            button2.Text = "يرجى الانتظار";
            button2.Enabled = false;
            try
            {
                //WaitForm wf = new WaitForm("");
                //wf.MdiParent = MdiParent;
                //wf.Show();
                //Application.DoEvents();

                CultureInfo culture = new CultureInfo("en-US", false);
                DateTime tempDate = Convert.ToDateTime(datval.convert_to_yyyy_MMddwith_dash(txt_tmdate.Text) + " "+(BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) +":00:00.000", culture).AddDays(1);
                zzz = tempDate.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

                // MessageBox.Show(zzz);
                progressBar1.Minimum = 0;
            }
            catch { }
             * */
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txt_fmdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_fmdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_fmdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    //  txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
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

               // txt_tmdate.Text = txt_fmdate.Text;
            }
            catch { }
        }

        private void txt_tmdate_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    string datestr = txt_tmdate.Text.ToString().Replace("-", "").Trim();
            //    if (datestr.Length == 0)
            //    {
            //        txt_tmdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
            //        // txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
            //    }
            //    else
            //    {
            //        if (datestr.Length == 2)
            //        {
            //            txt_tmdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
            //            //  txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
            //        }
            //        else
            //        {
            //            if (datestr.Length == 4)
            //            {
            //                txt_tmdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
            //                //  txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
            //            }
            //            else
            //            {
            //                // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
            //            }
            //        }
            //    }
            //}
            //catch { }
        }

        private void txt_fmdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
              //  txt_tmdate.Focus();
             //   txt_tmdate.SelectionStart = 0;
               // txt_tmdate.SelectAll();
                // txt_hdate.SelectionStart = 0;
              //  txt_tmdate.SelectionLength = 0;
                 
               // txt_tmdate.SelectAll();
            }
        }

        private void txt_tmdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                button2.Focus();
                // txt_hdate.SelectionStart = 0;
                //  txt_tmdate.SelectionLength = 0;

                // txt_tmdate.SelectAll();
            }
        }

        private void cmb_salctr_Leave(object sender, EventArgs e)
        {
            DataRow[] dr = dtslctr.Select("sl_no = '" + cmb_salctr.SelectedValue + "'");
            stbrno = dr[0][0].ToString();
            stsndoq = dr[0][3].ToString();
            strcash = dr[0][4].ToString();
            strcrdt = dr[0][5].ToString();
            strrcash = dr[0][6].ToString();
            strrcrdt = dr[0][7].ToString();
            strdsc = dr[0][8].ToString();
            strtax = dr[0][9].ToString();
            stwhno = dr[0][14].ToString();
            stccno = dr[0][22].ToString();
        }

        private void cmb_salctr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_fmdate.Focus();
                // txt_mdate.Select();
                txt_fmdate.SelectionStart = 0;
               
            }
        }

        private void txt_fmdate_Enter(object sender, EventArgs e)
        {
            txt_fmdate.SelectAll();
        }

        private void txt_tmdate_Enter(object sender, EventArgs e)
        {
           // txt_tmdate.SelectAll();
        }

        private void txt_ref_Leave(object sender, EventArgs e)
        {
            DataTable dtl = daml.SELECT_QUIRY_only_retrn_dt("select invmdate from sales_hdr where branch='"+BL.CLS_Session.brno+"' and slcenter='"+cmb_salctr.SelectedValue+"' and invtype='"+(chk_sal.Checked? "04" : "14")+"' and ref="+txt_ref.Text+"");
            if (dtl.Rows.Count == 1)
                txt_fmdate.Text = datval.convert_to_ddMMyyyy_dash(dtl.Rows[0][0].ToString());
            else
            {
                MessageBox.Show("يجب ادخال رقم فاتورة لاصلاحها", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_ref.Text = "";
                return;
            }
        }
    }
}
