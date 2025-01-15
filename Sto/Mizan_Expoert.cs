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

namespace POS.Sto
{
    public partial class Mizan_Expoert : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dtitems, dtgrp;
        public static int qq;
      //  string a_key;
        SqlConnection con = BL.DAML.con;

        public Mizan_Expoert()
        {
            InitializeComponent();
        }

        private void Mizan_Expoert_Load(object sender, EventArgs e)
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            string congrp = cmb_grp.SelectedIndex != -1 ? " and item_group='" + cmb_grp.SelectedValue + "'" : " ";
            string congrsp = cmb_sgrp.SelectedIndex != -1 ? " and sgroup='" + cmb_sgrp.SelectedValue + "'" : " ";
            dtitems = daml.SELECT_QUIRY_only_retrn_dt("select '1' as col1, SUBSTRING(item_no,4,4) as col2,'2' as col3,SUBSTRING(item_no,4,4) as col4"
                + ",item_ename as col5,'' as col6,'21' as col7,'0' as col8,replace(round(item_price,2),'.','') as col9,'' as col10,'' as col11 from items where SUBSTRING(item_no,1,3)='21-' " + congrp + congrsp + "");

            dataGridView1.DataSource = dtitems;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                XcelApp.Application.Workbooks.Add(Type.Missing);



                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    // XcelApp.Cells[5, i] = dataGridView1.Columns[i - 1].Visible == true ? dataGridView1.Columns[i - 1].HeaderText : "";
                    //if (dataGridView1.Columns[i - 1].Visible == true)
                    //    XcelApp.Cells[5, i] = "'" + dataGridView1.Columns[i - 1].HeaderText;
                    //else
                    //    XcelApp.Cells[5, i] = null;
                    XcelApp.Cells[1, i] = "'" + dataGridView1.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        //  XcelApp.Cells[i + 6, j + 1] = dataGridView1.Columns[j].Visible == true ? dataGridView1.Rows[i].Cells[j].Value.ToString() : "";
                        //if (dataGridView1.Columns[j].Visible == true)
                        //    XcelApp.Cells[i + 6, j + 1] = "'" + dataGridView1.Rows[i].Cells[j].Value.ToString();
                        //else
                        //    XcelApp.Cells[i + 6, j + 1] = null;

                        XcelApp.Cells[i + 2, j + 1] = "'" + dataGridView1.Rows[i].Cells[j].Value.ToString();
                    
                    }
                }
                // XcelApp.Cells[0, 0] = "123";

              //  Microsoft.Office.Interop.Excel._Worksheet workSheet = XcelApp.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;

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
              //  workSheet.Cells[2, "D"] = BL.CLS_Session.cmp_name;
               // workSheet.Cells[3, "D"] = BL.CLS_Session.brname;

               // workSheet.Range["C2", "E2"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
               // workSheet.Range["C3", "E3"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

              //  workSheet.Range["A5", "H5"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                // workSheet.Cells[2, "D"]


                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "Excel Documents (*.xls)|*.xls";
            //sfd.FileName = "export.xls";
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    //ToCsV(dataGridView1, @"c:\export.xls");
            //    ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name 
            //} 


            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "export.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //ToCsV(dataGridView1, @"c:\export.xls");
                ToCsV2(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name
            }
        }

        private void ToCsV(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf8 = Encoding.GetEncoding("windows-1256");
            byte[] output = utf8.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }

        private void ToCsV2(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
         //  for (int i = 0; i < dGV.RowCount - 1; i++)
            for (int i = 0; i < dGV.RowCount ; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
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
    }
}
