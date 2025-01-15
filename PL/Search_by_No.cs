using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS
{
    public partial class Search_by_No : Form
    {
        BL.DAML daml = new BL.DAML();   
        string skey;
        public Search_by_No(string key )
        {
            InitializeComponent();
            skey = key;
        }

        private void Search_by_No_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.TsIcon;
            if (skey == "01")
            {/*
                comboBox1.DisplayMember = "Text";
                comboBox1.ValueMember = "Value";

                var items = new[] { new { Text = "قيد عام", Value = "01" }, new { Text = "سند قبض", Value = "02" }, new { Text = "سند صرف", Value = "03" } };
                comboBox1.DataSource = items;
                comboBox1.SelectedIndex = -1;
              */
            }
            if (skey == "02")
            {/*
                comboBox1.DisplayMember = "Text";
                comboBox1.ValueMember = "Value";

                var items = new[] { new { Text = "سند قبض", Value = "02" }};
                comboBox1.DataSource = items;
                comboBox1.SelectedIndex = 0;
              */
                textBox1.Select();
            }
            if (skey == "03")
            {/*
                comboBox1.DisplayMember = "Text";
                comboBox1.ValueMember = "Value";

                var items = new[] { new { Text = "سند صرف", Value = "03" } };
                comboBox1.DataSource = items;
                comboBox1.SelectedIndex = 0;
              * */
                textBox1.Select();
            }

            if (skey == "04")
            {
                var stn = new Sal.Sal_Tran_notax("","","");
                cmb_salctr.Visible = true;

              //  comboBox1.Items.Add(new { Text = "نسخ من عرض سعر", Value = "24" });
                /*
                DataTable dtsal = daml.SELECT_QUIRY_only_retrn_dt("Select * from slcenters where sl_brno=" + BL.CLS_Session.brno);
                cmb_salctr.DataSource = dtsal;
                cmb_salctr.DisplayMember = "sl_name";
                cmb_salctr.ValueMember = "sl_no";
                cmb_salctr.SelectedIndex = 0;
                */
                //comboBox1.DisplayMember = "Text";
                //comboBox1.ValueMember = "Value";

                //var items = new[] { new { Text = "مبيعات نقدية", Value = "04" }, new { Text = "مبيعات اجله", Value = "05" }, new { Text = "مرتجع نقدي", Value = "14" }, new { Text = "مرتجع اجل", Value = "15" } };
                //comboBox1.DataSource = items;
                //comboBox1.SelectedIndex = -1;
              //  comboBox1.DataSource = stn.comboBox1.DataSource;
            }
            if (skey == "07")
            {
                var stn = new Pur.Pur_Tran_D("", "", "");
                cmb_salctr.Visible = true;
            }

            if (skey == "30")
            {
                var stn = new Sto.Sto_ImpExp("", "", "");
                cmb_salctr.Visible = false;
            }

           // comboBox1.FlatStyle = FlatStyle.Flat;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                this.Close();

            }


        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox1.Focus();
            }
        }

        private void cmb_salctr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox1.Focus();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked && checkBox1.Text.Equals("برقم فاتورة المورد"))
                txt_sno.Visible = true;
            else
                txt_sno.Visible = false;

            textBox1.Select();
        }

        private void txt_sno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                this.Close();


            }
        }
    }
}
