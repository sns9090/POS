using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using MetroFramework.Forms;
//using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using System.Diagnostics;

namespace POS.Sto
{
    public partial class Import_From_Xls : BaseForm
    {
        private string Excel03ConString = "Provider=Microsoft.Jet.Sql.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.Sql.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";

      //  BL.DAML daml =new BL.DAML();
        public Import_From_Xls()
        {
            InitializeComponent();
        }

       // SqlConnection con = new SqlConnection(@"Provider=Microsoft.ACE.Sql.12.0;Data Source=" + Directory.GetCurrentDirectory() + @"\db\db.accdb; Jet Sql:Database Password=123456;");
        SqlConnection con2 = BL.DAML.con;
        OleDbConnection con = new OleDbConnection(Properties.Settings.Default.dbConnectionString);
        //new SqlConnection(Properties.Settings.Default.dbConnectionString);

        private void btnSelect_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string filePath = openFileDialog1.FileName;
            string extension = Path.GetExtension(filePath);
            string header = rbHeaderYes.Checked ? "YES" : "NO";
            string conStr, sheetName;

            conStr = string.Empty;
            switch (extension)
            {

                case ".xls": //Excel 97-03
                    conStr = string.Format(Excel03ConString, filePath, header);
                    break;

                case ".xlsx": //Excel 07
                    conStr = string.Format(Excel07ConString, filePath, header);
                    break;
            }

            //Get the name of the First Sheet.
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    con.Close();
                }
            }

            //Read Data from the First Sheet.
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    using (OleDbDataAdapter oda = new OleDbDataAdapter())
                    {
                        DataTable dt = new DataTable();
                        cmd.CommandText = "SELECT * From [" + sheetName + "]";
                        cmd.Connection = con;
                        con.Open();
                        oda.SelectCommand = cmd;
                        oda.Fill(dt);
                        con.Close();

                        //Populate DataGridView.
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string saveStaff, savebc, savebr,savewb;

            progressBar1.Visible = true;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = dataGridView1.Rows.Count;

            if (dataGridView1.Rows.Count <= 0)
            {
                MessageBox.Show("لا يوجد بيانات", "alter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if(con2.State==ConnectionState.Closed)
                  con2.Open();
                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //{
                //    //StrQuery = @"INSERT INTO contacts VALUES ("
                //    //    + dataGridView1.Rows[i].Cells["ColumnName"].Value + ", "
                //    //    + dataGridView1.Rows[i].Cells["ColumnName"].Value + ");";
                //    //cmd.CommandText = StrQuery;
                //    //cmd.ExecuteNonQuery();

                //    saveStaff = "INSERT into items (item_no,item_name,item_price,item_barcode,item_unit,item_group,unit2,uq2,unit2p,supno) " + " VALUES ('" + dataGridView1.Rows[i].Cells[0].Value + "', '" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "');";

                //    SqlCommand cmd = new SqlCommand(saveStaff, con);
                 
                //    cmd.ExecuteNonQuery();

                //}
                //////foreach (DataRow row in dataGridView1.Rows)
                //////{

                //////    saveStaff = "INSERT into items (item_no,item_name,item_price,item_barcode,item_unit,item_group,unit2,uq2,unit2p,supno) "
                //////              + " VALUES ('" + row[0] + "', '" + row[1] + "'," + row[2] + ",'" + row[3] + "'," + row[4] + "," + row[5] + "," + row[6] + "," + row[7] + "," + row[8] + ",'" + row[9] + "');";

                //////    SqlCommand cmd = new SqlCommand(saveStaff, con2);

                //////    cmd.ExecuteNonQuery();

                //////}

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    saveStaff = "INSERT into items (item_no,item_name,item_price,item_barcode,item_unit,item_group,unit2,uq2,unit2p,supno,item_ename) "
                      //  + " VALUES ('" +(chk_autoitemno.Checked?dataGridView1.Rows[i].HeaderCell.Value.ToString() :  dataGridView1.Rows[i].Cells[0].Value.ToString().Trim().Substring(0, 16) )+ "', '" + dataGridView1.Rows[i].Cells[1].Value.ToString().Trim().Substring(0, 100) + "'," + dataGridView1.Rows[i].Cells[2].Value + ",'" +(chk_autobarcode.Checked?dataGridView1.Rows[i].HeaderCell.Value.ToString() : dataGridView1.Rows[i].Cells[3].Value.ToString().Trim().Substring(0, 20) )+ "'," + dataGridView1.Rows[i].Cells[4].Value + ",'" + dataGridView1.Rows[i].Cells[5].Value + "'," + dataGridView1.Rows[i].Cells[6].Value + "," + dataGridView1.Rows[i].Cells[7].Value + "," + dataGridView1.Rows[i].Cells[8].Value + ",'" + dataGridView1.Rows[i].Cells[9].Value.ToString().Trim() + "');";
                     //  + " VALUES ('" + dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(0, 16) + "', '" + dataGridView1.Rows[i].Cells[1].Value.ToString().Substring(0, 100) + "'," + dataGridView1.Rows[i].Cells[2].Value + ",'" +  dataGridView1.Rows[i].Cells[3].Value.ToString().Substring(0, 20) + "'," + dataGridView1.Rows[i].Cells[4].Value + ",'" + dataGridView1.Rows[i].Cells[5].Value + "'," + dataGridView1.Rows[i].Cells[6].Value + "," + dataGridView1.Rows[i].Cells[7].Value + "," + dataGridView1.Rows[i].Cells[8].Value + ",'" + dataGridView1.Rows[i].Cells[9].Value.ToString() + "');";
                       + " VALUES ('" + dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() + "', '" + dataGridView1.Rows[i].Cells[1].Value.ToString().Trim() + "'," + dataGridView1.Rows[i].Cells[2].Value + ",'" + dataGridView1.Rows[i].Cells[3].Value.ToString().Trim() + "'," + dataGridView1.Rows[i].Cells[4].Value + ",'" + dataGridView1.Rows[i].Cells[5].Value + "'," + dataGridView1.Rows[i].Cells[6].Value + "," + dataGridView1.Rows[i].Cells[7].Value + "," + dataGridView1.Rows[i].Cells[8].Value + ",'" + dataGridView1.Rows[i].Cells[9].Value.ToString().Trim() + "','"+dataGridView1.Rows[i].Cells[13].Value.ToString().Trim()+"');";

                    SqlCommand cmd = new SqlCommand(saveStaff, con2);

                    savebc = "INSERT into items_bc(item_no,barcode,pack,pk_qty,price,pkorder,br_no,sl_no)"
                      //  + " VALUES('" +(chk_autoitemno.Checked?dataGridView1.Rows[i].HeaderCell.Value.ToString() : dataGridView1.Rows[i].Cells[0].Value.ToString().Trim().Substring(0, 16) )+ "','" +(chk_autobarcode.Checked?dataGridView1.Rows[i].HeaderCell.Value.ToString() : dataGridView1.Rows[i].Cells[3].Value.ToString().Trim().Substring(0, 20) )+ "'," + dataGridView1.Rows[i].Cells[4].Value + ",1," + dataGridView1.Rows[i].Cells[2].Value + ",1);";
                      //  +" VALUES('" +  dataGridView1.Rows[i].Cells[0].Value.ToString().Trim().Substring(0, 16) + "','" +   dataGridView1.Rows[i].Cells[3].Value.ToString().Trim().Substring(0, 20) + "'," + dataGridView1.Rows[i].Cells[4].Value + ",1," + dataGridView1.Rows[i].Cells[2].Value + ",1);";
                          + " VALUES('" + dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() + "','" + dataGridView1.Rows[i].Cells[3].Value.ToString().Trim() + "'," + dataGridView1.Rows[i].Cells[4].Value + ",1," + dataGridView1.Rows[i].Cells[2].Value + ",1,'01','01');";
                   
                    SqlCommand cmd2 = new SqlCommand(savebc, con2);

                    savebr = "INSERT into brprices(branch,slcenter,itemno,lprice1,barcode)"
                     //   + " VALUES('" + BL.CLS_Session.brno + "','','" +(chk_autoitemno.Checked?dataGridView1.Rows[i].HeaderCell.Value.ToString() : dataGridView1.Rows[i].Cells[0].Value.ToString().Trim().Substring(0, 16) )+ "'," + dataGridView1.Rows[i].Cells[2].Value + ");";
                          + " VALUES('" + BL.CLS_Session.brno + "','','" + dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() + "'," + dataGridView1.Rows[i].Cells[2].Value + ",'" + dataGridView1.Rows[i].Cells[3].Value.ToString().Trim() + "');";
                   
                    SqlCommand cmd3 = new SqlCommand(savebr, con2);

                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();

                    progressBar1.Value = i;
                }

                if (checkBox1.Checked)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        savewb = "INSERT into whbins (br_no, item_no, unicode, wh_no, bin_no, qty, rsvqty, openbal, lcost, fcost, openlcost, openfcost, expdate) "
                             + " VALUES ('" + BL.CLS_Session.brno + "','" + (chk_autoitemno.Checked ? dataGridView1.Rows[i].HeaderCell.Value.ToString() : dataGridView1.Rows[i].Cells[0].Value.ToString().Trim().Substring(0, 16)) + "','', '" + dataGridView1.Rows[i].Cells[10].Value.ToString().Trim() + "',''," + dataGridView1.Rows[i].Cells[11].Value + ",0," + dataGridView1.Rows[i].Cells[11].Value + "," + dataGridView1.Rows[i].Cells[12].Value + ",0," + dataGridView1.Rows[i].Cells[12].Value + ",0,'');";

                        SqlCommand cmd4 = new SqlCommand(savewb, con2);

                       
                        cmd4.ExecuteNonQuery();
                       
                    }
                }

                con2.Close();
               // MetroFramework.MetroMessageBox.Show(this, "تم الحفظ بنجاح", "alter", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                MessageBox.Show("تم الحفظ بنجاح", "alter", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                dataGridView1.DataSource = null;
                progressBar1.Value = 0;
            }

            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.ToString());
            }



        }

        private void Import_From_Xls_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            btnSelect_Click(sender, e);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
           // button1_Click((object) sender, (EventArgs) e);
            button1_Click(sender, e);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog2.ShowDialog();
                string filePath = openFileDialog2.FileName;
                MessageBox.Show(filePath);

                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                // Microsoft.Office.Interop.Excel.Workbook excelBook = xlApp.Workbooks.Open("D:\\xls.xlsx");
                Microsoft.Office.Interop.Excel.Workbook excelBook = xlApp.Workbooks.Open(filePath);
                Microsoft.Office.Interop.Excel.Worksheet worksheet1 = excelBook.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;
                MessageBox.Show("The name of the active sheet is: " +
                    worksheet1.Name);
                //String name = "Items";
                //String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                //                "C:\\Sample.xlsx" +
                //                ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
                // String name = "Items";
                String name = worksheet1.Name;
                String constr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                filePath +
                                ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                OleDbConnection con = new OleDbConnection(constr);
                OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable data = new DataTable();
                sda.Fill(data);
                con.Close();

                dataGridView1.DataSource = data;
                // excelBook.Close(0);
                //// excelBook.Close(false);
                ////  xlApp.Quit();
                // excelBook.Close(null, null, null);
                // kill_exl();
                excelBook.Close(); // close your workbook
                xlApp.Quit();
                kill_exl();
                /*
                object fileName =filePath;
                Microsoft.Office.Interop.Excel.Workbook workbook = xlApp.Workbooks.get_Item(fileName);
                workbook.Close(false);
                 * */
                // System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                int rowNumber = 1;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    // dataGridView1.Rows.Add(r);
                    // dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                    if (r.IsNewRow) continue;
                    //r.HeaderCell.Value = (e.RowIndex + 1).ToString();
                    r.HeaderCell.Value = rowNumber.ToString();
                    rowNumber = rowNumber + 1;
                }
            }
            catch { }
        }
        private void kill_exl()
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
            foreach (System.Diagnostics.Process p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }

        }
        //private void KillExcel()
        //{
        //    Process[] AllProcesses = Process.GetProcessesByName("excel");

        //    // check to kill the right process
        //    foreach (Process ExcelProcess in AllProcesses)
        //    {
        //        if (myHashtable.ContainsKey(ExcelProcess.Id) == false)
        //            ExcelProcess.Kill();
        //    }

        //    AllProcesses = null;
       //}
        private void button3_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo(Directory.GetCurrentDirectory() + @"\ImportItems.xlsx");
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + @"\ImportItems.xlsx");
            }
            else
            {
                //file doesn't exist
            }
        }
    }
}

