using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
//using System.Data.OleDb;
using System.IO;
using System.Globalization;

namespace POS.Sto
{
    public partial class Grd_Entry : Form
    {
        string filno;
        bool savedok = false;
        BL.EncDec ende = new BL.EncDec();
       // OleDbConnection convfb = new OleDbConnection("Provider=VFPOLEDB;Data Source=" + Environment.CurrentDirectory);
        SqlConnection convfb = BL.DAML.con;
        SqlConnection consql;
        
       // OleDbDataAdapter da222;
        SqlDataAdapter da222;

        public Grd_Entry()
        {
            InitializeComponent();
        }

        private const int CP_DISABLE_CLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CP_DISABLE_CLOSE_BUTTON;
                return cp;
            }
        }

        private void Grd_Entry_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = "TreeSoft    ::    " + this.Text;
                var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\ServerSetting.txt");

                //BL.CLS_Session.sqlserver = lines.First().ToString();
                //// BL.CLS_Session.sqldb = ConfigurationManager.AppSettings["logfilelocation"];
                //BL.CLS_Session.sqluser = lines.Skip(2).First().ToString();
                //BL.CLS_Session.sqluserpass = lines.Skip(3).First().ToString();
                //BL.CLS_Session.sqlcontimout = lines.Skip(4).First().ToString();

                ////////filno =string.IsNullOrEmpty(lines.Skip(6).First().ToString().Trim())? "1" : lines.Skip(6).First().ToString().Trim();
                
                // MessageBox.Show(filno);

                ////////if (!File.Exists(Directory.GetCurrentDirectory() + "\\Zttk_" + filno + ".dbf"))
                ////////{
                ////////    // File.Copy(Directory.GetCurrentDirectory() + "\\Zttk_empty.dbf", Directory.GetCurrentDirectory() + "\\Zttk_" + filno + ".dbf", false);
                ////////   // OleDbCommand ocmd = new OleDbCommand(@"SET DEFAULT TO "+Directory.GetCurrentDirectory()+" CREATE TABLE newtable free;(newid INT AUTOINC,; newname CHAR(20));", convfb);
                ////////    OleDbCommand ocmd = new OleDbCommand(@"CREATE TABLE Zttk_" + filno + " (a C(20) NOT NULL, b B(3) NOT NULL, c N(3,0) NOT NULL,d C(24) NOT NULL,e B(2) NOT NULL,t T NOT NULL,s I NOT NULL,l C(6) NOT NULL,shdqty N(5,2) NOT NULL, shdpk N(5,2) NOT NULL, frtqty N(6,2) NOT NULL, mqty B(3) NOT NULL, whno C(2) NOT NULL,qtyfound L NOT NULL,colorit L NOT NULL, eraseit L NOT NULL,unit C(20) NOT NULL)", convfb);
                   
                ////////    convfb.Open();
                ////////    ocmd.ExecuteNonQuery();
                ////////    convfb.Close();

                ////////}

                ////////string w_file = "Zttk_" + filno + ".dbf";
                ////////string w_directory = Directory.GetCurrentDirectory();

                ////////DateTime c3 = File.GetLastWriteTime(System.IO.Path.Combine(w_directory, w_file));
                ////////// RTB_info.AppendText("Program created at: " + c3.ToString());
                ////////// MessageBox.Show("Program created at: " + c3.ToString("dd-MM-yyyy", new CultureInfo("en-US", false)));
                ////////textBox1.Text = "" + c3.ToString("dd-MM-yyyy   hh:mm:ss tt", new CultureInfo("en-US", false));
                ////////textBox2.Text = filno;

                //dataGridView1.Rows[-1].Cells[-1].Value = "تسلسل";
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[4].ReadOnly = true;
                dataGridView1.Columns[5].ReadOnly = true;

                ////OleDbDataAdapter da2 = new OleDbDataAdapter("select a.a,a.d,b.name,a.e,a.unit,a.shdqty,a.b from Zttk_" + filno + " a ,ttkitems b where ltrim(a.d)=ltrim(b.barcode) order by a.s", convfb);
                SqlDataAdapter da2 = new SqlDataAdapter("select a.a,a.d,b.item_name,a.e,b.item_unit,a.shdqty,a.b from Zttk a ,items b where ltrim(a.d)=ltrim(b.item_no) order by a.s", convfb);

                DataTable dt2 = new DataTable();

                da2.Fill(dt2);

                foreach (DataRow dtr in dt2.Rows)
                {
                    dataGridView1.Rows.Add(dtr.ItemArray);
                }

                dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.NewRowIndex];
                /*
                OleDbDataAdapter da = new OleDbDataAdapter("select * from ttkitems", convfb);

                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
                 * */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\ServerSetting.txt");

                //BL.CLS_Session.sqlserver = lines.First().ToString();
                //// BL.CLS_Session.sqldb = ConfigurationManager.AppSettings["logfilelocation"];
                //BL.CLS_Session.sqluser = lines.Skip(2).First().ToString();
                //BL.CLS_Session.sqluserpass = lines.Skip(3).First().ToString();
                //BL.CLS_Session.sqlcontimout = lines.Skip(4).First().ToString();

                consql = new SqlConnection("Data Source=" + lines.First().ToString() + ";Initial Catalog=" + lines.Skip(1).First().ToString() + ";User id=" + lines.Skip(2).First().ToString() + ";Password=" + ende.Decrypt(lines.Skip(3).First().ToString(), true) + ";Connection Timeout=" + lines.Skip(4).First().ToString() + "");
                button44_Click(sender, e);
                //  this.Close();
                button1.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               // this.Close();
                button1.Enabled = true;
            }
        }

        private void button44_Click(object sender, EventArgs e)
        {
            //try
            //{
            progressBar1.Visible = true;

            if (checkBox1.Checked == false)
            {
                //SqlDataAdapter da = new SqlDataAdapter("select ltrim(rtrim(item_no)) itemno,substring(ltrim(rtrim(replace(REPLACE(replace(item_name,char(9),' '),char(10),' '),char(13),' '))),1,50) name,ltrim(rtrim(item_barcode)) barcode,round(item_price,2) lprice1,round(item_curcost,5) lcost, 0 lprice2,0 lprice3 , uq2 pkqty1,unit_name  from items ,units where items.item_unit=units.unit_id", consql);
                SqlDataAdapter da = new SqlDataAdapter("select ltrim(rtrim(a.item_no)) itemno,substring(ltrim(rtrim(replace(REPLACE(replace(a.item_name,char(9),' '),char(10),' '),char(13),' '))),1,50) name,ltrim(rtrim(b.barcode)) barcode,round(b.price,2) lprice1,round((case when a.item_curcost>0 then a.item_curcost when a.item_curcost=0 then a.static_cost else a.item_curcost end),5) lcost, 0 lprice2,0 lprice3 , b.pk_qty pkqty1,c.unit_name  from items a join items_bc b  on a.item_no=b.item_no  join units c on b.pack=c.unit_id", consql);

                DataTable dt = new DataTable();

                da.Fill(dt);
                progressBar1.Minimum = 0;
                progressBar1.Maximum = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    button33_Click(sender, e);
                   
                    foreach (DataRow row in dt.Rows)
                    {

                       // OleDbCommand command = convfb.CreateCommand();
                        SqlCommand command = convfb.CreateCommand();
                        command.CommandText = "insert into ttkitems( itemno, name, barcode, lprice1, lcost,  lprice2, lprice3 , pkqty1,unit) values('" + row[0] + "','" + row[1] + "','" + row[2] + "'," + row[3] + "," + row[4] + ",0,0," + row[7] + ",'" + row[8] + "')";

                        if(convfb.State==ConnectionState.Closed)
                            convfb.Open();
                        command.ExecuteNonQuery();
                        progressBar1.Increment(1);
                    }
                    convfb.Close();

                    MessageBox.Show("Items Updated " + dt.Rows.Count.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    progressBar1.Visible = false;
                }
            }
            else
            {
               // SqlDataAdapter da = new SqlDataAdapter("select ltrim(rtrim(a.item_no)) itemno,substring(ltrim(rtrim(replace(REPLACE(replace(b.item_name,char(9),' '),char(10),' '),char(13),' '))),1,50) name,ltrim(rtrim(a.barcode)) barcode,round(a.price,2) lprice1,round(b.item_curcost,5) lcost, 0 lprice2,0 lprice3 , b.uq2 pkqty1,u.unit_name  from items_bc a, items b,units u where a.item_no=b.item_no and b.item_unit=u.unit_id order by a.item_no", consql);
                SqlDataAdapter da = new SqlDataAdapter("select ltrim(rtrim(a.item_no)) itemno,substring(ltrim(rtrim(replace(REPLACE(replace(a.item_name,char(9),' '),char(10),' '),char(13),' '))),1,50) name,ltrim(rtrim(b.barcode)) barcode,round(b.price,2) lprice1,round(a.item_curcost,5) lcost, 0 lprice2,0 lprice3 , b.pk_qty pkqty1,c.unit_name  from items a join items_bc b  on a.item_no=b.item_no  join units c on b.pack=c.unit_id", consql);


                DataTable dt = new DataTable();

                da.Fill(dt);
                progressBar1.Minimum = 0;
                progressBar1.Maximum = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    button33_Click(sender, e);
                    convfb.Open();
                    foreach (DataRow row in dt.Rows)
                    {

                       // OleDbCommand command = convfb.CreateCommand();
                        SqlCommand command = convfb.CreateCommand();

                        command.CommandText = "insert into ttkitems( itemno, name, barcode, lprice1, lcost,  lprice2, lprice3 , pkqty1,unit) values('" + row[0] + "','" + row[1] + "','" + row[2] + "'," + row[3] + "," + row[4] + "," + row[5] + "," + row[6] + "," + row[7] + ",'" + row[8] + "')";

                        command.ExecuteNonQuery();
                        progressBar1.Increment(1);
                    }
                    convfb.Close();

                    MessageBox.Show("Items Updated " + dt.Rows.Count.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    progressBar1.Visible = false;
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void button33_Click(object sender, EventArgs e)
        {
            ////////string path = Environment.CurrentDirectory;
            ////////string connectionString = "Provider=VFPOLEDB;Data Source=" + path;
            ////////using (OleDbConnection connection = new OleDbConnection(connectionString))
            ////////{
            ////////    connection.Open();
            ////////    OleDbCommand command = connection.CreateCommand();
            ////////    //  string queryString1 = " SET EXCLUSIVE ON" + " DELETE FROM ttkitems.dbf " + " PACK ";
            ////////    // command.CommandText = "CREATE TABLE Test (Id I, Changed D, Name C(100))";
            ////////    command.CommandText = "delete from ttkitems";
            ////////    //   command.CommandText = " SET EXCLUSIVE ON" + " DELETE FROM ttkitems.dbf " + " ZAP ";

            ////////    if (connection.State == ConnectionState.Closed)
            ////////        connection.Open();
            ////////    command.ExecuteNonQuery();

            ////////    connection.Close();


            ////////    if (connection.State == ConnectionState.Closed)
            ////////        connection.Open();
            ////////    OleDbCommand command2 = connection.CreateCommand();
            ////////    command2.CommandText = "pack ttkitems";
            ////////    command2.ExecuteNonQuery();
            ////////    connection.Close();
            ////////}

            //using (OleDbConnection connection2 = new OleDbConnection(connectionString))
            //{
            //    connection2.Open();
            //    OleDbCommand command2 = connection2.CreateCommand();

            //    // command.CommandText = "CREATE TABLE Test (Id I, Changed D, Name C(100))";
            //    //  command.CommandText = "delete from ttkitems";
            //    command2.CommandText = "pack";
            //    command2.ExecuteNonQuery();
            //    connection2.Close();
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ////////try
            ////////{
            button2.Enabled = false;
            button2.Text = "يرجى الانتظار";
            ////////    // OleDbCommand ocmd = new OleDbCommand("select * from Zttk_"+filno+" where .F. into table ZZZ", convfb);
            ////////    OleDbCommand ocmd = new OleDbCommand("select * from Zttk_" + filno + " into table Zttk_" + filno + "_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss", new CultureInfo("en-US", false)) + "", convfb);
            ////////    if (convfb.State == ConnectionState.Closed)
            ////////        convfb.Open();
            ////////    ocmd.ExecuteNonQuery();
            ////////    convfb.Close();

            ////////    save_data();
            save_data();
            savedok = true;
            ////////    savedok = true;

            ////////    this.Close();
            this.Close();
            ////////}
            ////////catch { }

            // DataTable dt = new DataTable();
        }

        private void save_data()
        {
            //try
            //{
                ////////OleDbCommand command2 = convfb.CreateCommand();

                ////////command2.CommandText = "delete from Zttk_"+filno+"";

                ////////if (convfb.State == ConnectionState.Closed) 
                ////////    convfb.Open();
                ////////command2.ExecuteNonQuery();
                ////////convfb.Close();

                ////////if (convfb.State == ConnectionState.Closed) 
                ////////    convfb.Open();
                ////////OleDbCommand command22 = convfb.CreateCommand();
                ////////command22.CommandText = "pack Zttk_" + filno + "";
                ////////command22.ExecuteNonQuery();
                ////////convfb.Close();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow && row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[6].Value != null && Convert.ToDouble(row.Cells[6].Value) != 0 && !row.Cells[0].Value.ToString().Trim().Equals("") && !row.Cells[1].Value.ToString().Trim().Equals("")) 
                    {
                        progressBar1.Visible = true;
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = dataGridView1.Rows.Count-1;

                        ////////OleDbCommand command = convfb.CreateCommand();
                        SqlCommand command = convfb.CreateCommand();

                        ////////command.CommandText = "insert into Zttk_" + filno + "(A,B,C,D,E,T,S,L,SHDQTY,SHDPK,FRTQTY,MQTY,WHNO,QTYFOUND,COLORIT,ERASEIT,UNIT) values('" + row.Cells[0].Value.ToString().Trim() + "'," + row.Cells[6].Value.ToString().Trim() + ",0,'" + row.Cells[1].Value.ToString().Trim() + "'," + row.Cells[3].Value.ToString().Trim() + ",{ts'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'}," + row.HeaderCell.Value + ",'0'," + row.Cells[5].Value.ToString().Trim() + ",0,0,0,'0',.F.,.F.,.F.,'" + row.Cells[4].Value.ToString().Trim() + "')";
//                        command.CommandText = @"MERGE Zttk as t 
//                                                using (select '" + row.Cells[0].Value.ToString().Trim() + "' A," + row.Cells[6].Value.ToString().Trim() + " B,0 C,'" + row.Cells[1].Value.ToString().Trim() + "' D," + row.Cells[3].Value.ToString().Trim() + " E,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' T," + row.HeaderCell.Value + " S,'0' L," + row.Cells[5].Value.ToString().Trim() + " SHDQTY,0 SHDPK,0 FRTQTY,0 MQTY,'0' WHNO,0 QTYFOUND,0 COLORIT,0 ERASEIT,'" + row.Cells[4].Value.ToString().Trim() + "' UNIT) as s "
//                                                                 + @" ON t.d = s.a and t.s=s.s
//                                                                      WHEN MATCHED THEN
//                                                                      UPDATE SET t.d=s.d , t.b=s.b
//                                                                      WHEN NOT MATCHED THEN                                      
//                                                INSERT VALUES(s.A,s.B,s.C,s.D,s.E,s.T,s.S,s.L,s.SHDQTY,s.SHDPK,s.FRTQTY,s.MQTY,s.WHNO,s.QTYFOUND,s.COLORIT,s.ERASEIT,s.UNIT);";


                        if (convfb.State == ConnectionState.Closed)
                            convfb.Open();
                        command.ExecuteNonQuery();

                       

                        progressBar1.Increment(1);
                    }
                }

                MessageBox.Show("تم حفظ البيانات", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                convfb.Close();

              //  MessageBox.Show("Items Saved " + dataGridView1.Rows.Count.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                progressBar1.Visible = false;
            //   // MessageBox.Show("Items Updated " + dt.Rows.Count.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
               
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    convfb.Close();
            //}
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete && !dataGridView1.CurrentRow.IsNewRow)
                {
                    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                }
                ////////if (e.KeyCode == Keys.Space)
                ////////{
                ////////    ////if (dataGridView1.CurrentRow.Cells[0].Value == null || dataGridView1.CurrentRow.Cells[0].Value == null)
                ////////    ////{
                ////////    ////    MessageBox.Show("لا يوجد صنف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ////////    ////    return;
                ////////    ////}

                ////////    // columnindex = columnindex + 1;
                  
                  
                ////////    //   string nextColumnValue = dataGridView1.Rows[rowindex].Cells[columnindex+1].Value.ToString();
                ////////    OleDbConnection con = new OleDbConnection("Provider=VFPOLEDB;Data Source=" + Environment.CurrentDirectory);
                ////////    OleDbDataAdapter da2 = new OleDbDataAdapter("select count(*) from ttkitems where ltrim(rtrim(itemno))='" + dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim() + "'", con);
                ////////    DataTable dtchkbar = new DataTable();
                ////////    da2.Fill(dtchkbar);
                ////////    if (Convert.ToInt32(dtchkbar.Rows[0][0]) > 1)
                ////////    {
                ////////       // MessageBox.Show(dtchkbar.Rows[0][0].ToString(), "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ////////        Search_All ns = new Search_All("chkb", dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim());
                ////////        ns.ShowDialog();
                ////////        dataGridView1.CurrentRow.Cells[0].Value = ns.dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim();
                ////////        dataGridView1.CurrentRow.Cells[4].Value = ns.dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
                ////////        dataGridView1.CurrentRow.Cells[5].Value = ns.dataGridView1.CurrentRow.Cells[2].Value.ToString().Trim();
                ////////        dataGridView1.CurrentRow.Cells[3].Value = ns.dataGridView1.CurrentRow.Cells[3].Value.ToString().Trim();
                ////////        dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[6];
                ////////       // dataGridView1.CurrentRow.Cells[5].Value = dt222.Rows[0][7];
                ////////       // firstColumnValue3 = ns.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                ////////       // baknam = ns.dataGridView1.CurrentRow.Cells[1].Value.ToString().StartsWith("عرض") ? ns.dataGridView1.CurrentRow.Cells[1].Value.ToString() : "";
                ////////       // da23 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_curcost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img,i.price2 mp from items i join items_bc b on b.item_no=i.item_no and b.barcode='" + firstColumnValue3 + "' join taxs t on i.item_tax=t.tax_id", con2);

                ////////    }
                ////////}

                if (e.KeyCode == Keys.F8)
                {
                    //  SendKeys.Send("{.}");
                    // SendKeys.Send("{.}");
                    // var selectedCell = dataGridView1.SelectedCells[0];
                    // do something with selectedCell...
                    Search_All f4 = new Search_All("Grd","");
                    f4.ShowDialog();
                    //    dataGridView1.CurrentRow

                    // SendKeys.Send("{.}");
                    // dataGridView1.Rows.Add(1);


                    dataGridView1.CurrentRow.Cells[1].Value = f4.dataGridView1.CurrentRow.Cells[0].Value;
                    // SendKeys.Send("F2");
                    dataGridView1.CurrentRow.Cells[0].Value = f4.dataGridView1.CurrentRow.Cells["i_b"].Value;
                    dataGridView1.CurrentRow.Cells[2].Value = f4.dataGridView1.CurrentRow.Cells[1].Value;
                    dataGridView1.CurrentRow.Cells[3].Value = f4.dataGridView1.CurrentRow.Cells[2].Value;
                    dataGridView1.CurrentRow.Cells[4].Value = f4.dataGridView1.CurrentRow.Cells[4].Value;
                    dataGridView1.CurrentRow.Cells[5].Value = 1;
                    //  if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[4].Value) == 0 || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
                    // dataGridView1.CurrentRow.Cells[4].Value = "1.00";
                    if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[6].Value) == 0)
                    dataGridView1.CurrentRow.Cells[6].Value = 0;
                    dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[6];
                }

                if (e.KeyCode == Keys.Enter  && dataGridView1.CurrentCell == dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6])
                {
                    if (string.IsNullOrEmpty(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value.ToString()) || Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value) == 0)
                    {
                        SendKeys.Send("{UP}");
                        SendKeys.Send("{F2}");
                    }
                    else
                        dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells[0];
                }

                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F)
                {
                    Find_By_No fi = new Find_By_No();
                    // fi.MdiParent = ParentForm;
                    fi.ShowDialog();

                    string searchValue = fi.txt_itemno.Text;
                    // MessageBox.Show(searchValue);
                    // dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    try
                    {
                        dataGridView1.ClearSelection();

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (fi.checkBox1.Checked)
                            {
                                if (row.Cells[0].Value.ToString().Trim().Equals(searchValue))
                                {
                                    ////  row.Selected = true;
                                    //   row.Cells[1].Selected = true;
                                    dataGridView1.CurrentCell = row.Cells[0];
                                    dataGridView1.Select();
                                    break;
                                }
                            }
                            else
                            {
                                if (row.Cells[1].Value.ToString().Trim().Equals(searchValue))
                                {
                                    ////  row.Selected = true;
                                    //   row.Cells[1].Selected = true;
                                    dataGridView1.CurrentCell = row.Cells[1];
                                    dataGridView1.Select();
                                    break;
                                }
                            }
                        }

                    }
                    catch (Exception exc)
                    {
                        //  MessageBox.Show(exc.Message);
                    }
                }
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool notfound = false;
                string quiryt;
                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0] || dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[1])
                {
                    if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[1])
                    {
                        quiryt = "select * from ttkitems where ltrim(rtrim(itemno)) = '" + dataGridView1.CurrentRow.Cells[1].Value + "'";
                    }
                    else
                    {
                        quiryt = "select * from ttkitems where ltrim(rtrim(barcode)) = '" + dataGridView1.CurrentRow.Cells[0].Value + "'";
                    }
                    //////else //if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0])
                    //////{
                    //////    // if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0])
                    //////    // {
                    //////    string firstColumnValue3 = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    //////    //   string nextColumnValue = dataGridView1.Rows[rowindex].Cells[columnindex+1].Value.ToString();
                    //////    dtchkbar = daml.SELECT_QUIRY_only_retrn_dt("select count(*) from items_bc where item_no='" + firstColumnValue3 + "'");
                    //////    if (Convert.ToInt32(dtchkbar.Rows[0][0]) > 1)
                    //////    {
                    //////        Search_All ns = new Search_All("chkb", firstColumnValue3);
                    //////        ns.ShowDialog();
                    //////        // firstColumnValue3 = ns.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    //////        quiryt = "SELECT items.item_no, items.item_name, items.item_cost, b.price AS item_price, b.barcode AS item_barcode, b.pack AS item_unit, items.item_obalance,"
                    //////           + "items.item_cbalance, items.item_group, items.item_image, items.item_req, items.item_tax, items.unit2, items.uq2, items.unit2p, items.unit3, items.uq3, items.unit3p,"
                    //////           + "items.unit4, items.uq4, items.unit4p, items.item_ename, items.item_opencost, items.item_curcost, items.supno, items.note, items.last_updt, items.sgroup,"
                    //////           + "items.price2, b.pk_qty FROM items INNER JOIN items_bc AS b ON b.item_no = items.item_no AND b.barcode = '" + ns.dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";

                    //////        // quiryt = "select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img from items i join items_bc b on b.item_no=i.item_no and b.barcode='" + firstColumnValue3 + "' join taxs t on i.item_tax=t.tax_id";

                    //////    }
                    //////    else
                    //////    {
                    //////        //  string value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
                    //////        //string currentcell = dataGridView1.CurrentCell.Value.ToString();
                    //////        // string firstColumnValue = dataGridView1.Rows[0].Cells[0].Value.ToString();
                    //////        //string firstColumnValue =Convert.ToString(dataGridView1.CurrentCell

                    //////        //SqlDataAdapter da2 = new SqlDataAdapter("select * from items where item_no='" + firstColumnValue + "' or item_no='" + nextColumnValue + "'", con2);
                    //////        //  SqlDataAdapter da2 = new SqlDataAdapter("select * from DB.dbo.items where item_no='" + firstColumnValue + "'", con2);



                    //////        // SqlDataAdapter da23 = new SqlDataAdapter("select i.*,t.tax_percent from items i, taxs t where i.item_tax=t.tax_id and i.item_no='" + firstColumnValue3 + "'", con2);
                    //////        // quiryt = "select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img from items i join items_bc b on b.item_no=i.item_no and i.item_no='" + firstColumnValue3 + "' join taxs t on i.item_tax=t.tax_id";
                    //////        quiryt = "select *,0 pk_qty from items where rtrim(ltrim(item_no))='" + dataGridView1.CurrentRow.Cells[0].Value + "'";
                    //////    }
                    //////    //  }
                    //////    //  quiryt = "select * from items where rtrim(ltrim(item_no))='" + dataGridView1.CurrentRow.Cells[0].Value + "'";
                    //////}
                    //  string item_bar = chk_usebarcode.Checked==false ? "  rtrim(ltrim(item_no))=" : "  rtrim(ltrim(item_barcode))=";
                    //  dt222 = daml.SELECT_QUIRY_only_retrn_dt("select * from items where " + item_bar + "'" + dataGridView1.CurrentRow.Cells[0].Value + "'");
                    // dt222 = daml.SELECT_QUIRY_only_retrn_dt(quiryt);

                   //// da222 = new OleDbDataAdapter(quiryt, convfb);
                    da222 = new SqlDataAdapter(quiryt, convfb);
                    DataTable dt222 = new DataTable();
                    da222.Fill(dt222);

                    if (dt222.Rows.Count > 0)
                    {
                        // DataGridViewComboBoxColumn dcombo = new DataGridViewComboBoxColumn();
                        //  if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0] && dataGridView1.CurrentRow.Cells[2].Value==null)

                        //////if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[0])
                        //////{

                        // dcombo.Name = dataGridView1.Columns["Column3"];
                        //  dcombo.Name = "Column3";
                        // dataGridView1.CurrentRow.Cells[1].Value = dt222.Rows[0][1];
                        dataGridView1.CurrentRow.Cells[0].Value = dt222.Rows[0][2];
                        // SendKeys.Send("F2");
                        dataGridView1.CurrentRow.Cells[1].Value = dt222.Rows[0][0];
                        dataGridView1.CurrentRow.Cells[2].Value = dt222.Rows[0][1];
                        dataGridView1.CurrentRow.Cells[3].Value = dt222.Rows[0][3];
                        dataGridView1.CurrentRow.Cells[4].Value = dt222.Rows[0][8];
                        dataGridView1.CurrentRow.Cells[5].Value = dt222.Rows[0][7];
                        //  if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[4].Value) == 0 || string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[4].Value.ToString()))
                        // dataGridView1.CurrentRow.Cells[4].Value = "1.00";
                        if (Convert.ToDecimal(dataGridView1.CurrentRow.Cells[6].Value) == 0)
                            dataGridView1.CurrentRow.Cells[6].Value = 0;

                        if (checkBox2.Checked)
                            dataGridView1.CurrentRow.Cells[6].Value = 1;
                        else
                            dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[6];

                    }
                    else
                    {
                        if (!dataGridView1.CurrentRow.IsNewRow)
                        {
                            MessageBox.Show(dataGridView1.CurrentCell.Value + " الصنف غير موجود", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                         //   dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.NewRowIndex];
                        }
                       /// dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.NewRowIndex];
                        dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells[0];
                        notfound = true;
                    }
                }


                if (e.ColumnIndex == dataGridView1.Columns.Count - 1)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells[0];
                }
                else
                {
                    if (!checkBox2.Checked && !notfound)
                        SendKeys.Send("{UP}");

                }

            }
            catch { }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 6) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (dataGridView1.CurrentRow.IsNewRow)
            //    {
            //        dataGridView1.CurrentRow.Cells[0].ReadOnly = false;
            //        dataGridView1.CurrentRow.Cells[1].ReadOnly = false;
            //    }
            //    else
            //    {
            //        dataGridView1.CurrentRow.Cells[0].ReadOnly = true;
            //        dataGridView1.CurrentRow.Cells[1].ReadOnly = true;
            //    }
            //}
            //catch { }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.IsNewRow)
                {
                    dataGridView1.CurrentRow.Cells[0].ReadOnly = false;
                    dataGridView1.CurrentRow.Cells[1].ReadOnly = false;
                }
                else
                {
                    dataGridView1.CurrentRow.Cells[0].ReadOnly = true;
                    dataGridView1.CurrentRow.Cells[1].ReadOnly = true;
                }
            }
            catch { }
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            int rowNumber = 1;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                // dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                if (r.IsNewRow) continue;
                //r.HeaderCell.Value = (e.RowIndex + 1).ToString();
                r.HeaderCell.Value = rowNumber.ToString();
                rowNumber = rowNumber + 1;

                //if (r.Cells[2].Value == null)
                //{
                //    r.Cells[2].Value = txt_desc.Text;
                //}
                //if (dataGridView1.CurrentRow.Cells[2].Value == null)
                //{
                //    dataGridView1.CurrentRow.Cells[2].Value = txt_desc.Text;
                //}
                //if (Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value) == 0)
                //{
                //    MessageBox.Show("لا يسمح بكمية صفر");
                //    dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[5];
                //}
            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[6] && string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[6].Value.ToString()))// && (string.IsNullOrEmpty(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value.ToString()) || Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value) == 0))
                {
                  //  MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView1.CurrentRow.Cells[6].Value = 0;
                 //   SendKeys.Send("{UP}");
                 //   SendKeys.Send("{F2}");

                }
            }
            catch { }
        }

        private void Grd_Entry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(savedok==false)
               button2_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button3.Text = "انتظر";
            ////////OleDbCommand ocmd = new OleDbCommand("select * from Zttk_" + filno + " into table Zttk_" + filno + "_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss", new CultureInfo("en-US", false)) + "", convfb);
            ////////if (convfb.State == ConnectionState.Closed)
            ////////    convfb.Open();
            ////////ocmd.ExecuteNonQuery();
            ////////convfb.Close();

            ////////save_data();
            save_data();
            ////////textBox1.Text = "" + DateTime.Now.ToString("dd-MM-yyyy   hh:mm:ss tt", new CultureInfo("en-US", false));
            ////////MessageBox.Show("تم حفظ البيانات", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            button3.Enabled = true;
            button3.Text = "حفظ";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[1])
            {
                string curcell = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                double sumitem = 0;
                //foreach(DataGridViewRow dtr in dataGridView1.Rows)
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow && row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[6].Value != null && row.Cells[1].Value.ToString().Equals(curcell))
                    {
                        sumitem = sumitem + (Convert.ToDouble(row.Cells[6].Value) * Convert.ToDouble(row.Cells[5].Value));
                    }
                }

                MessageBox.Show("اجمالي  ماتم جردة من الصنف" + " --> " + dataGridView1.CurrentRow.Cells[2].Value.ToString().Trim() + " == " + sumitem.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {


            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                XcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    if (dataGridView1.Columns[i - 1].Visible == true)
                    {
                        XcelApp.Cells[1, i] = "'" + dataGridView1.Columns[i - 1].HeaderText;
                    }
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Visible == true && !dataGridView1.Rows[i].IsNewRow)
                            // XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString().StartsWith("0") && dataGridView1.Rows[i].Cells[j].Value.ToString().Length > 1 ? "'" + dataGridView1.Rows[i].Cells[j].Value.ToString() : dataGridView1.Rows[i].Cells[j].Value.ToString() ;
                            XcelApp.Cells[i + 2, j + 1] = "'" + dataGridView1.Rows[i].Cells[j].Value.ToString().Trim();
                    }

                }
                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
        }


    }
}