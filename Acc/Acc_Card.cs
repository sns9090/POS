using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace POS.Acc
{
    public partial class Acc_Card : BaseForm
    {
        BL.Date_Validate datval = new BL.Date_Validate();
        BL.DAML daml = new BL.DAML();
        TreeNode selectedNode = null;
        DataTable acountsTb = null;
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter;
        bool newNode, thisLevel, update;
        string parent = "-1",key_id;
        int lvl = 0;
        public Acc_Card( string keyno)
        {
            key_id = keyno;
            InitializeComponent();
            newNode = thisLevel = update = false;
            acountsTb = new DataTable();
           // connection = new SqlConnection(@"Data Source=(local)\infosoft12;Initial Catalog=dbase;Integrated Security=True");
          //  connection= new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            connection = BL.DAML.con;
            command = new SqlCommand();
            command.Connection = connection;


           
        }
      
        private void Form2_Load(object sender, EventArgs e)
        {
            if (!BL.CLS_Session.issections)
            {
                label9.Visible = false;
                cmb_sc.Visible = false;
            }
            else
            {
                DataTable dtsc = daml.SELECT_QUIRY_only_retrn_dt("select sc_code,sc_name from sctions where sc_brno='" + BL.CLS_Session.brno + "'");

                cmb_sc.DataSource = dtsc;
                cmb_sc.DisplayMember = "sc_name";
                cmb_sc.ValueMember = "sc_code";
                cmb_sc.SelectedIndex = -1;
            }

            if (BL.CLS_Session.islight)
            {
                button1.Visible = false;
            }
            if (BL.CLS_Session.up_editop == false)
            {
                txt_opnbal.ReadOnly = true;
            }
            //Dictionary comboSource = new Dictionary();
            DataTable dtbrs = daml.SELECT_QUIRY_only_retrn_dt("select * from branchs");

            cmb_brno.DataSource = dtbrs;
            cmb_brno.DisplayMember = "br_name";
            cmb_brno.ValueMember = "br_no";
            cmb_brno.SelectedIndex = -1;


            


            comboBox1.Items.Insert(0, "تشغيل");
            comboBox1.Items.Insert(1, "مركز مالي");
            comboBox1.Items.Insert(2, "متاجرة");
            comboBox1.Items.Insert(3, "ارباح وخسائر");

            cmb_txtype.Items.Insert(0, "حساب عادي");
            cmb_txtype.Items.Insert(1, "حساب ضريبة محصلة");
            cmb_txtype.Items.Insert(2, "حساب ضريبة مدفوعة");
           // comboBox1.Items.Insert(3, "ارباح وخسائر");

            //comboBox2.Items.Insert(0, "تشغيل");
            //comboBox2.Items.Insert(1, "مركز مالي");
            //comboBox2.Items.Insert(2, "متاجرة");
            //comboBox2.Items.Insert(3, "ارباح وخسائر");


            String sql = "SELECT a_key,a_parent,a_name,a_opnbal,a_type,a_ptype,a_active,accontrol,a_brno,acckind,section,a_ename,a_note,glpurge  FROM accounts ";//where a_brno='"+BL.CLS_Session.brno+"'";
            try
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                adapter.Fill(acountsTb);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }

            PopulateTreeView(0, null);

            if (!string.IsNullOrEmpty(key_id))
            {
                selectedNode = GetNode(key_id);
                ShowNodeData(selectedNode);
                btn_Edit.Enabled = true;
            }

            //foreach (TreeNode node in treeView1.Nodes)
            //{
            //    foreach (TreeNode node2 in node.Nodes)
            //    {
            //        node2.ImageIndex = imageList1.Images.IndexOfKey("Text Document_16x16.png");
            //        node2.SelectedImageIndex = imageList1.Images.IndexOfKey("Text Document_16x16.png");
            //        //node2.ImageIndex = 100;
            //        //node2.SelectedImageIndex = 100;
            //    }
            //}
        }

        public TreeNode GetNode(object tag, TreeNode rootNode)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                if (node.Name.Equals(tag)) return node;

                //recursion
                var next = GetNode(tag, node);
                if (next != null) return next;
            }
            return null;
        }
        public TreeNode GetNode(object tag)
        {
            TreeNode itemNode = null;
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Name.Equals(tag)) return node;

                itemNode = GetNode(tag, node);
                if (itemNode != null) break;
            }
            return itemNode;
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {

            TreeNode childNode;

           // foreach (DataRow dr in acountsTb.Select("[a_parent]=" + parentId + " and [glcurrency]=''"))
            foreach (DataRow dr in acountsTb.Select("[a_parent]=" + parentId))
            {
                TreeNode t = new TreeNode();
                t.Text = dr["a_key"].ToString() + "  -  " + (BL.CLS_Session.lang.Equals("E") ? dr["a_ename"].ToString() : dr["a_name"].ToString());
                t.Name = dr["a_key"].ToString();
                t.Tag = acountsTb.Rows.IndexOf(dr);
                //t.ImageIndex=
                if (parentNode == null)
                {
                    treeView1.Nodes.Add(t);
                    childNode = t;
                }
                else
                {
                    parentNode.Nodes.Add(t);
                    childNode = t;
                }
                PopulateTreeView(Convert.ToInt32(dr["a_key"].ToString()), childNode);
            }

        }
  
        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_Edit.Enabled = true;
            btn_Del.Enabled = true;
            /*
            string nodee = treeView1.SelectedNode.Name;
           // MessageBox.Show(nodee);

            label1.Text = treeView1.SelectedNode.Text;

            DataTable dt=new DataTable();
            dt = daml.SELECT_QUIRY("select * from account where a_key='" + nodee + "'");

            textBox1.Text=dt.Rows[0][2].ToString();
            textBox2.Text = dt.Rows[0][4].ToString();
            textBox3.Text = dt.Rows[0][5].ToString();
            textBox4.Text = dt.Rows[0][6].ToString();
          //  MessageBox.Show(dt.Rows[0][2].ToString());
             */

            selectedNode = treeView1.SelectedNode;
            ShowNodeData(selectedNode);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(comboBox1.SelectedIndex.ToString());
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            viewToolStripMenuItem_Click(sender, e);
        }

        private void atThisLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedNode = treeView1.SelectedNode;
            int max = 0;
            if (treeView1.Nodes.Count > 0)
            {
                parent = acountsTb.Rows[int.Parse(selectedNode.Tag.ToString())]["a_parent"].ToString();
                DataRow[] nodes = acountsTb.Select("[a_parent]=" + parent);

                foreach (DataRow r in nodes)
                {
                    int n = int.Parse(r["a_key"].ToString());
                    if (n > max)
                        max = n;

                }
            }
            max += 1;
            txtCode.Text = "0" + max.ToString();
            txtName.Text = ""; txt_note.Text = ""; txtEName.Text = "";
            update = false;
            newNode = true;
            thisLevel = true;
           // txtName.Focus();
            inActive.Checked = false;
            chk_freetax.Checked = false;
            chk_rqabi.Checked = false;
           // MessageBox.Show(parent);
            if (txtCode.Text.Length == 9)
            {
                txt_opnbal.Enabled = true;
                cmb_txtype.Enabled = true;
                chk_rqabi.Enabled = true;
            }
            else { txt_opnbal.Enabled = false; }

            btn_Edit.Enabled = false;
            btn_Statment.Enabled = false;
            btn_Save.Enabled = true;

            txtName.Enabled = true;
            txt_note.Enabled = true;
            txtEName.Enabled = true;
            comboBox1.Enabled = true;
            cmb_sc.Enabled = true;
          //  cmb_brno.Enabled = true;
            cmb_brno.SelectedValue = BL.CLS_Session.brno;
            btn_Save.Enabled = true;
            btn_Save.Text = "حفظ الحساب";
            txt_opnbal.Text="0";
            txtName.Focus();
        }

        private void underSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedNode = treeView1.SelectedNode;

            DataRow r = acountsTb.Rows[int.Parse(treeView1.SelectedNode.Tag.ToString())];
            if (r["a_ptype"].ToString().Equals("P"))
            {
                newNode = true;
                thisLevel = false;
                string code = string.Empty;
                parent = acountsTb.Rows[int.Parse(selectedNode.Tag.ToString())]["a_key"].ToString();

                if (selectedNode.Nodes.Count > 0)
                {

                    DataRow[] nodes = acountsTb.Select("[a_parent]=" + parent);
                    int max = 0;
                    foreach (DataRow ra in nodes)
                    {
                        int n = int.Parse(ra["a_key"].ToString());
                        if (n > max)
                            max = n;

                    }
                    max += 1;
                    txtCode.Text = "0" + max.ToString();
                    txtName.Text = ""; txt_note.Text = ""; txtEName.Text = "";
                    code = max.ToString();
                }
                else
                {
                    if (selectedNode.Level < 2)
                    {
                      //  MessageBox.Show(selectedNode.Level.ToString() + "   01");
                        code = "01";
                    }
                    else
                    {
                       // MessageBox.Show(selectedNode.Level.ToString() + "   001");
                        code = "001";
                    }
                    txtCode.Text = r["a_key"] + code;
                }
                txtName.Focus();
                update = false;
                //  newNode = true;

                txtName.Enabled = true;
                txt_note.Enabled = true;
                txtEName.Enabled = true;
                comboBox1.Enabled = true;
                cmb_sc.Enabled = true;
                cmb_brno.SelectedValue = BL.CLS_Session.brno;
                txt_opnbal.Enabled = true;
                if (txtCode.Text.Length == 9)
                {
                    txt_opnbal.Enabled = true;
                    cmb_txtype.Enabled = true;
                    chk_rqabi.Enabled = true;
                }
                else { txt_opnbal.Enabled = false; }
                btn_Save.Enabled = true;
                btn_Save.Text = "حفظ الحساب";
                txt_opnbal.Text = "0";
                inActive.Checked = false;
                chk_freetax.Checked = false;
                chk_rqabi.Checked = false;
                txtName.Focus();
               // update = false;
            }
            else
            {
                newNode = false;
                MessageBox.Show("لا يمكن فتح حساب جديد تحت حساب حركة", "Acount opening Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

           // MessageBox.Show(Convert.ToInt32(parent).ToString());
        }

        private void ShowNodeData(TreeNode nod)
        {
            DataRow r = acountsTb.Rows[int.Parse(nod.Tag.ToString())];
          //  DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select * from accounts where a_key ='" + nod.Tag.ToString() + "'");
            txtCode.Text = r["a_key"].ToString();
            txtName.Text = r["a_name"].ToString();
            txt_note.Text = r["a_note"].ToString();
            txtEName.Text = r["a_ename"].ToString();
            //dtpDate.Value = DateTime.Parse(r["a_crtdate"].ToString());
            txt_opnbal.Text = r["a_opnbal"].ToString();

            if (r["a_ptype"].ToString().Equals("P"))
            {
                radioParent.Checked = true;
                txt_opnbal.Enabled = false;
            }
            else
                radioTransaction.Checked = true;
            if (r["a_ptype"].ToString().Equals("T"))
                txt_opnbal.Enabled = true;


            //if (r["a_type"].ToString().Equals("1"))
            //    comboBox1.SelectedIndex = 1;
            //if (r["a_type"].ToString().Equals("2"))
            //    comboBox1.SelectedIndex = 2;
            //if (r["a_type"].ToString().Equals("3"))
            comboBox1.SelectedIndex = Convert.ToInt32(r["a_type"]);

            cmb_brno.SelectedValue = r["a_brno"];

            if (r["a_active"].ToString().Equals("1"))
                inActive.Checked = false;
            else
                inActive.Checked = true;

          //  if (r["acckind"].ToString().Equals("1"))
            cmb_txtype.SelectedIndex = Convert.ToInt32(r["acckind"]);
            cmb_sc.SelectedValue = r["section"].ToString();
         //   else
           //     chk_tax.Checked = false;

            if (r["accontrol"].ToString().Equals("0"))
                chk_rqabi.Checked = false;
            else
                chk_rqabi.Checked = true;

            if (r["glpurge"].ToString().Equals("1"))
                chk_freetax.Checked = true;
            else
                chk_freetax.Checked = false;


            //    radioNA1.Checked = true;
            //else if (r["fixed"].ToString().Equals("Fixed"))
            //    radioFixed.Checked = true;
            //else
            //    radioVariable.Checked = true;
            //if (r["direct"].ToString().Equals("NA"))
            //    radioNA2.Checked = true;
            //else if (r["direct"].ToString().Equals("Direct"))
            //    radioDirect.Checked = true;
            //else
            //    radioIndirect.Checked = true;
          //  txtName.Focus();
            inActive.Enabled = false;
            chk_freetax.Enabled = false;
            txtName.Enabled = false;
            txt_note.Enabled = false;
            txtEName.Enabled = false;
            comboBox1.Enabled = false;
            txt_opnbal.Enabled = false;
            cmb_sc.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (cmb_brno.SelectedValue != BL.CLS_Session.brno && cmb_brno.Text !="")
            if ( txtCode.Text.Length==9)
            {
                if (string.IsNullOrEmpty(cmb_brno.SelectedValue.ToString()) || !cmb_brno.SelectedValue.ToString().Equals(BL.CLS_Session.brno))
                {
                    MessageBox.Show("لا يمكن تعديل حساب يخص فرع اخر", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            update = true;
            newNode = false;

            inActive.Enabled = true;
            chk_freetax.Enabled = true;
            chk_rqabi.Enabled = true;
            cmb_txtype.Enabled = true;
            txtName.Enabled = true;
            txt_note.Enabled = true;
            txtEName.Enabled = true;
            comboBox1.Enabled = true;
            cmb_sc.Enabled = true;

            if (txtCode.Text.Length == 9)
            {
                txt_opnbal.Enabled = true;
            }
            else { txt_opnbal.Enabled = false; }

           // button2.Enabled = true;
            btn_Statment.Enabled = true;
            btn_Edit.Enabled = false;
            btn_Save.Enabled = true;
            btn_Save.Text = "حفظ التعديلات";
            txtName.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (BL.CLS_Session.formkey.Contains("A133"))
            {
                if (txtCode.Text != "" && txtCode.Text.Length > 6)
                {
                    Acc_Statment_Exp f4a = new Acc_Statment_Exp(txtCode.Text);
                    f4a.MdiParent = MdiParent;
                    f4a.Show();
                }
            }
            /*
            button1.Enabled = true;
            button2.Enabled = false;

            inActive.Enabled = false;
            txtName.Enabled = false;
            comboBox1.Enabled = false;
            textBox1.Enabled = false;
             */
            //inActive.Enabled = false;


           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql;
            if (BL.CLS_Session.issections && (comboBox1.SelectedIndex == 2 || comboBox1.SelectedIndex == 3) && cmb_sc.SelectedIndex==-1)
            {
                MessageBox.Show("يجب اختيار القسم او القطاع", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_sc.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtName.Text) || comboBox1.SelectedIndex<0 || string.IsNullOrEmpty(cmb_txtype.Text.ToString()))
            {
                MessageBox.Show("لا يمكن يكون الاسم او النوع فاضي", "Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtName.Focus();
                return;
            }
            if (update)
            {
                //sql = "UPDATE [accounts] SET [ac_name] = '" + txtName.Text + "',[type] = '" + (radioParent.Checked ? "Parent Account" : "Transaction Account") + "',[fixed] = '" + (radioFixed.Checked ? "Fixed" : (radioVariable.Checked ? "Variable" : "NA")) + "',[direct] = '" + (radioDirect.Checked ? "Direct" : (radioIndirect.Checked ? "Indirect" : "NA")) + "',[open_bal]=" + (radioParent.Checked ? "0" : textBox1.Text) + ",[dt]='" + dtpDate.Value.ToString("yyyy-MM-dd") + "',[active]=" + (isActive.Checked ? 1 : 0) + " WHERE [code] =" + txtCode.Text;

                sql = "UPDATE [accounts] SET [a_name] = '" + txtName.Text + "',[a_note] = '" + txt_note.Text + "',[a_ename] = '" + txtEName.Text + "',[a_type] = '" + comboBox1.SelectedIndex + "', [a_active]='" + (txtCode.Text.Length <= 6 ? 0 : (inActive.Checked ? 0 : 1)) + "',acckind='" + cmb_txtype.SelectedIndex + "',accontrol=" + (txtCode.Text.Length <= 6 ? 0 : (chk_rqabi.Checked ? 1 : 0)) + ",[a_opnbal] = " + (txtCode.Text.Length <= 6 ? 0 : (string.IsNullOrEmpty(txt_opnbal.Text) ? 0 : Convert.ToDouble(txt_opnbal.Text))) + ",section='" + (txtCode.Text.Length <= 6 ? "" : (cmb_sc.SelectedIndex == -1 ? "" : cmb_sc.SelectedValue.ToString())) + "',glpurge='"+(chk_freetax.Checked? "1" : "0")+"' WHERE [a_key] =" + txtCode.Text;
                try
                {
                    connection.Open();
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                    MessageBox.Show("تم حفظ التعديلات بنجاح", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    selectedNode.Text = txtCode.Text + "  -  " + txtName.Text;
                    DataRow r = acountsTb.Rows[int.Parse(selectedNode.Tag.ToString())];
                    r["a_name"] = txtName.Text;
                    r["a_note"] = txt_note.Text;
                    r["a_ename"] = txtEName.Text;
                    r["a_opnbal"] =string.IsNullOrEmpty(txt_opnbal.Text)? 0: Convert.ToDouble(txt_opnbal.Text);
                    r["a_type"] = comboBox1.SelectedIndex;
                    r["a_active"] = (inActive.Checked ? 0 : 1);
                    r["acckind"] = cmb_txtype.SelectedIndex;
                    r["accontrol"] = (chk_rqabi.Checked ? 1 : 0);
                    r["a_brno"] = cmb_brno.SelectedValue;
                    r["acckind"] = cmb_txtype.SelectedIndex;
                    r["section"] = cmb_sc.SelectedValue;
                    r["glpurge"] = (chk_freetax.Checked ? "1" : "0");

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
               

               // r["type"] = (radioParent.Checked ? "Parent Account" : "Transaction Account");
              //  r["fixed"] = (radioFixed.Checked ? "Fixed" : (radioVariable.Checked ? "Variable" : "NA"));
              //  r["direct"] = (radioDirect.Checked ? "Direct" : (radioIndirect.Checked ? "Indirect" : "NA"));

            }
            else if (newNode)
            {
                //sql = "INSERT INTO [accounts]  VALUES (" + txtCode.Text + " ,'" + txtName.Text + "' ," + _parent + ",'" + (radioParent.Checked ? "Parent Account" : "Transaction Account") + "'," + (_thisLevel ? _selectedNode.Level : _selectedNode.Level + 1) + ",'" + (radioFixed.Checked ? "Fixed" : (radioNA1.Checked ? "Variable" : "NA")) + "','" + (radioDirect.Checked ? "Direct" : (radioNA2.Checked ? "Indirect" : "NA")) + "'," + textBox1.Text + ",'" + dtpDate.Value.ToString("yyyy-MM-dd") + "','" + (isActive.Checked ? 1 : 0) + "')";

                sql = "INSERT INTO [accounts]([a_key],[a_parent],[a_name],[a_ename],[a_group],[a_type],[a_ptype],[a_active],[a_level],[a_opnbal],[accontrol],[a_brno],[acckind],[section],[a_note],glpurge)  VALUES ('" + txtCode.Text + "' ," + parent + ",'" + txtName.Text + "','" + txtEName.Text + "','" + txtCode.Text.Substring(1, 1) + "'," + comboBox1.SelectedIndex + ",'" + (txtCode.Text.Length > 6 ? "T" : "P") + "','" + (txtCode.Text.Length <= 6 ? 0 : (inActive.Checked ? 0 : 1)) + "'," + (thisLevel ? selectedNode.Level + 1 : selectedNode.Level + 2) + "," + (txtCode.Text.Length <= 6 ? 0 : (string.IsNullOrEmpty(txt_opnbal.Text) ? 0 : Convert.ToDouble(txt_opnbal.Text))) + "," + (txtCode.Text.Length <= 6 ? 0 : (chk_rqabi.Checked ? 1 : 0)) + ",'" + (txtCode.Text.Length <= 6 ? "" : cmb_brno.SelectedValue) + "','" + (cmb_txtype.SelectedIndex == -1 ? 0 : cmb_txtype.SelectedIndex) + "','" + (txtCode.Text.Length <= 6 ? "" : (cmb_sc.SelectedIndex == -1 ? "" : cmb_sc.SelectedValue.ToString())) + "','" + txt_note.Text + "','" + (chk_freetax.Checked ? "1" : "0") + "')";
                try
                {
                    connection.Open();
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                    MessageBox.Show("تم حفظ الحساب بنجاح", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                  //  treeView1.Nodes.Clear();
                    //  PopulateTreeView(0, null);                                       a_key,a_parent,a_name,a_opnbal,a_type,a_ptype,a_active,accontrol,a_brno,acckind
                    acountsTb.Rows.Add(txtCode.Text, parent, txtName.Text, string.IsNullOrEmpty(txt_opnbal.Text) ? 0 : Convert.ToDouble(txt_opnbal.Text), comboBox1.SelectedIndex, (txtCode.Text.Length > 6 ? "T" : "P"), (txtCode.Text.Length <= 6 ? 0 : (inActive.Checked ? 0 : 1)), (txtCode.Text.Length <= 6 ? 0 : (chk_rqabi.Checked ? 1 : 0)), cmb_brno.SelectedValue, cmb_txtype.SelectedIndex, txt_note.Text);
                    TreeNode tn = new TreeNode();
                    tn.Text = txtCode.Text + " - " + txtName.Text;
                    tn.Name = txtCode.Text;
                    tn.Tag = acountsTb.Rows.Count - 1;

                    if (thisLevel)
                    {
                        if (selectedNode.Parent != null)
                            selectedNode.Parent.Nodes.Add(tn);
                        else
                            treeView1.Nodes.Add(tn);
                    }
                    else
                        selectedNode.Nodes.Add(tn);


                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }

                //SELECT a_key,a_parent,a_name,a_opnbal,a_type,a_ptype  FROM account
              

            }
            else
            {
                MessageBox.Show("لا يوجد اختيار", "Nothing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            btn_Save.Enabled = false;
            inActive.Enabled = false;
            chk_freetax.Enabled = false;
            chk_rqabi.Enabled = false;
            cmb_txtype.Enabled = false;
            txtName.Enabled = false;
            txt_note.Enabled = false;
            txtEName.Enabled = false;
            comboBox1.Enabled = false;
           // cmb_brno.Enabled = false;
            btn_Statment.Enabled = true;
            txt_opnbal.Enabled = false;
            txtCode.Enabled = false;
            cmb_sc.Enabled = false;
           // dataGridView1.DataSource = acountsTb;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedNode = treeView1.SelectedNode;
            ShowNodeData(selectedNode);

            if (string.IsNullOrEmpty(cmb_brno.SelectedValue.ToString()) || !cmb_brno.SelectedValue.ToString().Equals(BL.CLS_Session.brno))
            {
                MessageBox.Show("لا يمكن تعديل حساب يخص فرع اخر", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                update = true;
                newNode = false;

                inActive.Enabled = true;
                chk_freetax.Enabled = true;
                txtName.Enabled = true;
                txt_note.Enabled = true;
                txtEName.Enabled = true;
                comboBox1.Enabled = true;

                if (txtCode.Text.Length == 9)
                {
                    txt_opnbal.Enabled = true;
                }
                else { txt_opnbal.Enabled = false; }

                // button2.Enabled = true;
                btn_Statment.Enabled = true;
                btn_Edit.Enabled = false;
                txtName.Focus();

                btn_Save.Enabled = true;
                btn_Save.Text = "حفظ التعديلات";

            }
           // button1_Click(sender, e);
        }

        private void deletAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!cmb_brno.SelectedValue.Equals(BL.CLS_Session.brno))
            if (cmb_brno.SelectedIndex!=-1 && !cmb_brno.SelectedValue.ToString().Equals(BL.CLS_Session.brno))
            //if (cmb_brno.SelectedValue != null || !cmb_brno.SelectedValue.ToString().Equals(""))
            {
                MessageBox.Show("لا يمكن حذف حساب يخص فرع اخر","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

             DialogResult result2 = MessageBox.Show("هل تريد الحذف", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
             if (result2 == DialogResult.Yes)
             {
                 string sql, node;
                 node = treeView1.SelectedNode.Name;
                 sql = "delete from accounts where a_key='" + node + "'";
                 if (node.Length == 9)
                 {
                     // MessageBox.Show(node);

                     try
                     {
                         DataTable dt1 = daml.SELECT_QUIRY_only_retrn_dt("select count(*) from acc_dtl where a_key='" + node + "'");
                         DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select count(*) from accounts where a_key='" + node + "' and a_opnbal<>0");

                         if (Convert.ToInt32(dt1.Rows[0][0]) > 0 || Convert.ToInt32(dt2.Rows[0][0]) > 0 || Convert.ToDouble(txt_opnbal.Text) !=0)
                         {
                             MessageBox.Show("لا يمكن حذف حساب له رصيد او حركة", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         }
                         else
                         {
                             connection.Open();
                             command.CommandText = sql;
                             command.ExecuteNonQuery();

                             MessageBox.Show("تم حذف الحساب بنجاح", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                             treeView1.Nodes.Remove(treeView1.SelectedNode);
                             connection.Close();

                             //  treeView1.Nodes.Clear();
                             //  PopulateTreeView(0, null);
                             DataRow[] result = acountsTb.Select("a_key = '" + node + "'");
                             foreach (DataRow row in result)
                             {
                                 if (row["a_key"].ToString().Trim() == node)
                                     acountsTb.Rows.Remove(row);
                             }
                         }
                     }
                     catch (SqlException ex)
                     {

                         MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                     finally
                     {
                         connection.Close();
                     }

                     //SELECT a_key,a_parent,a_name,a_opnbal,a_type,a_ptype  FROM account


                 }

                 else
                 {
                     DataRow[] result = acountsTb.Select("a_key like '" + node + "%' and a_ptype='T'");

                     if (result.Length > 0)
                     {
                         MessageBox.Show("لا يمكن حذف حساب اب وتحته ابناء", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                     else
                     {
                         connection.Open();
                         command.CommandText = sql;
                         command.ExecuteNonQuery();
                         MessageBox.Show("تم حذف الحساب بنجاح", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         treeView1.Nodes.Remove(treeView1.SelectedNode);
                         connection.Close();
                     }


                 }
                 txt_opnbal.Text = "";
                 txtCode.Text = "";
                 txtName.Text = "";
                 txt_note.Text = "";
                 comboBox1.SelectedIndex = -1;
                 inActive.Enabled = false;
                 chk_freetax.Enabled = false;
                 txtName.Enabled = false;
                 txt_note.Enabled = false;
                 txtEName.Enabled = false;
                 comboBox1.Enabled = false;
                 txt_opnbal.Enabled = false;
                 // TreeNode tn = new TreeNode();
                 // tn.Text = txtCode.Text + " - " + txtName.Text;
                 // tn.Name = node;
                 // tn.Tag = acountsTb.Rows.Count - 1;

                 //  MessageBox.Show(treeView1.SelectedNode.Index.ToString());
                 // selectedNode.Parent.Nodes.Remove(tn);



                 // acountsTb.Rows.Remove(txtCode.Text, parent, txtName.Text, textBox1.Text, comboBox1.SelectedIndex, (txtCode.Text.Length > 6 ? "T" : "P"));
                 //TreeNode tn = new TreeNode();
                 //tn.Text = txtCode.Text + " - " + txtName.Text;
                 //tn.Name = txtCode.Text;
                 //tn.Tag = acountsTb.Rows.Count - 1;

                 //if (thisLevel)
                 //{
                 //    if (selectedNode.Parent != null)
                 //        selectedNode.Parent.Nodes.Remove(tn);
                 //    else
                 //        treeView1.Nodes.Remove(tn);
                 //}
                 //else
                 //    selectedNode.Nodes.Remove(tn);

                 // treeView1.Nodes.Clear();
                 // PopulateTreeView(0, null);

             }
             else if (result2 == DialogResult.No)
             {
                 //...
             }
             else
             {
                 //...
             }
            
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            //treeView1.SelectedNode = e.Node;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView1.SelectedNode = e.Node;
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            deletAccountToolStripMenuItem_Click(sender, e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtCode.Enabled = true;
        }

        private void btn_Rfrsh_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            Form2_Load(null, null);
        }

        private void btn_add_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            treeView1.CollapseAll();
            //if (lvl >= 0)
            //{
            //    treeView1.CollapseAll(lvl);
            //}
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
            //if (lvl <= 4)
            //{
            //    treeView1.exp
            //}
            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                treeView1.Nodes[i].Expand();                 
            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (txtCode.Text.Length == 9 && !string.IsNullOrEmpty(txtName.Text))
            {
                Acc_bdgt ad = new Acc_bdgt(txtCode.Text, txtName.Text);
                ad.ShowDialog();
            }
        }

        private void txt_opnbal_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_opnbal.Text))
            //    txt_opnbal.Text = "0";

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void txt_opnbal_Validating(object sender, CancelEventArgs e)
        {
            datval.ValidateText_float(txt_opnbal);
        }

        private void cmb_sc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_sc.SelectedIndex = -1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
                Search_All f4 = new Search_All("Acc-1", "Usr");
                f4.button1.Visible = true;
                f4.textBox1.Visible = false;
                f4.ShowDialog();
               // f4.button1_Click_1(sender, e);
           
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Acc_Card_Shown(object sender, EventArgs e)
        {
            if (!BL.CLS_Session.formkey.Contains(this.Tag.ToString()))
            {
                this.Close();
            }
        }

        //private void treeView1_DoubleClick_1(object sender, EventArgs e)
        //{
        //    ShowNodeData(treeView1.SelectedNode);
        //}

    }
}
