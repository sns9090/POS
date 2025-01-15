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
    public partial class Accounts : BaseForm
    {
        TreeNode _selectedNode = null;
        DataTable _acountsTb = null;
        SqlConnection _connection;
        SqlCommand _command;
        SqlDataAdapter _adapter;
        bool _newNode, _thisLevel, _update;
        int _parent = -1;
        public Accounts()
        {
            InitializeComponent();
            _newNode = _thisLevel = _update = false;
            _acountsTb = new DataTable();
           // _connection = new SqlConnection(@"Data Source=S-PC\SQLEXPRESS;Initial Catalog=dbase;Integrated Security=True");
            _connection = BL.DAML.con;//
            _command = new SqlCommand();
            _command.Connection = _connection;
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedNode = treeView1.SelectedNode;
            ShowNodeData(_selectedNode);
        }
        private void ShowNodeData(TreeNode nod)
        {
            DataRow r = _acountsTb.Rows[int.Parse(nod.Tag.ToString())];
            txtCode.Text = r["code"].ToString();
            txtName.Text = r["ac_name"].ToString();
            dtpDate.Value = DateTime.Parse(r["dt"].ToString());
            textBox1.Text = r["open_bal"].ToString();

            if (r["type"].ToString().Equals("Parent Account"))
            {
                radioParent.Checked = true;
                textBox1.Enabled = false;
            }
            else
                radioTransaction.Checked = true;
            if (r["fixed"].ToString().Equals("NA"))
                radioNA1.Checked = true;
            else if (r["fixed"].ToString().Equals("Fixed"))
                radioFixed.Checked = true;
            else
                radioVariable.Checked = true;
            if (r["direct"].ToString().Equals("NA"))
                radioNA2.Checked = true;
            else if (r["direct"].ToString().Equals("Direct"))
                radioDirect.Checked = true;
            else
                radioIndirect.Checked = true;
            txtName.Focus();
        }

        private void atThisLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedNode = treeView1.SelectedNode;
            int max = 0;
            if (treeView1.Nodes.Count > 0)
            {
                _parent = int.Parse(_acountsTb.Rows[int.Parse(_selectedNode.Tag.ToString())]["parent"].ToString());
                DataRow[] nodes = _acountsTb.Select("[parent]=" + _parent);

                foreach (DataRow r in nodes)
                {
                    int n = int.Parse(r["code"].ToString());
                    if (n > max)
                        max = n;

                }
            }
            max += 1;
            txtCode.Text = max.ToString();

            _newNode = true;
            _thisLevel = true;
            txtName.Focus();

        }

        private void underSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedNode = treeView1.SelectedNode;
            
            DataRow r = _acountsTb.Rows[int.Parse(treeView1.SelectedNode.Tag.ToString())];
            if (r["type"].ToString().Equals("Parent Account"))
            {
                _newNode = true;
                _thisLevel = false;
                string code = string.Empty;
                _parent = int.Parse(_acountsTb.Rows[int.Parse(_selectedNode.Tag.ToString())]["code"].ToString());
                
                if (_selectedNode.Nodes.Count > 0)
                {

                    DataRow[] nodes = _acountsTb.Select("[parent]=" + _parent);
                    int max = 0;
                    foreach (DataRow ra in nodes)
                    {
                        int n = int.Parse(ra["code"].ToString());
                        if (n > max)
                            max = n;

                    }
                    max += 1;
                    txtCode.Text = max.ToString();
                    code = max.ToString();
                }
                else
                {
                    if (_selectedNode.Level < 3)
                        code = "01";
                    else
                        code = "001";

                    txtCode.Text = r["code"] + code;
                }
                txtName.Focus();

            }
            else
            {
                _newNode = false;
                MessageBox.Show("New Account can't be opened under a Transaction Account", "Acount opening Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            string sql;
          //  if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Name Can not be empty", "Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtName.Focus();
                return;
            }
            if (_update)
            {
                 sql= "UPDATE [accounts] SET [ac_name] = '" + txtName.Text + "',[type] = '" + (radioParent.Checked ? "Parent Account" : "Transaction Account") + "',[fixed] = '" + (radioFixed.Checked ? "Fixed" : (radioVariable.Checked ? "Variable" : "NA")) + "',[direct] = '" + (radioDirect.Checked ? "Direct" : (radioIndirect.Checked ? "Indirect" : "NA")) + "',[open_bal]=" + (radioParent.Checked ? "0" : textBox1.Text) + ",[dt]='" + dtpDate.Value.ToString("yyyy-MM-dd") + "',[active]=" + (isActive.Checked ? 1 : 0) + " WHERE [code] =" + txtCode.Text;
                try
                {
                    _connection.Open();
                    _command.CommandText = sql;
                    _command.ExecuteNonQuery();
                    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (SqlException ex)
                {
                    
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                _selectedNode.Text = txtCode.Text + " - " + txtName.Text;
                DataRow r = _acountsTb.Rows[int.Parse(_selectedNode.Tag.ToString())];
                r["ac_name"] = txtName.Text;
                r["type"] = (radioParent.Checked ? "Parent Account" : "Transaction Account");
                r["fixed"] = (radioFixed.Checked ? "Fixed" : (radioVariable.Checked ? "Variable" : "NA"));
                r["direct"] = (radioDirect.Checked ? "Direct" : (radioIndirect.Checked ? "Indirect" : "NA"));
                
            }
            else if (_newNode)
            {
                sql = "INSERT INTO [accounts]  VALUES (" + txtCode.Text + " ,'" + txtName.Text + "' ," + _parent + ",'" + (radioParent.Checked ? "Parent Account" : "Transaction Account") + "'," + (_thisLevel ? _selectedNode.Level : _selectedNode.Level + 1) + ",'" + (radioFixed.Checked ? "Fixed" : (radioNA1.Checked ? "Variable" : "NA")) + "','" + (radioDirect.Checked ? "Direct" : (radioNA2.Checked ? "Indirect" : "NA")) + "'," + textBox1.Text + ",'" + dtpDate.Value.ToString("yyyy-MM-dd") + "','" + (isActive.Checked ? 1 : 0) + "')";
                try
                {
                    _connection.Open();
                    _command.CommandText = sql;
                    _command.ExecuteNonQuery();
                    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                _acountsTb.Rows.Add(txtCode.Text, txtName.Text, _parent, (radioParent.Checked ? "Parent Account" : "Transaction Account"), _selectedNode.Level, (radioFixed.Checked ? "Fixed" : "Variable"), (radioDirect.Checked ? "Direct" : "Indirect"), textBox1.Text, dtpDate.Value.ToString("yyyy-MM-dd"), (isActive.Checked ? 1 : 0));
                    TreeNode tn = new TreeNode();
                    tn.Text = txtCode.Text + " - " + txtName.Text;
                    tn.Name = txtCode.Text;
                    tn.Tag = _acountsTb.Rows.Count - 1;
                    
                    if (_thisLevel)
                    {
                        if (_selectedNode.Parent != null)
                            _selectedNode.Parent.Nodes.Add(tn);
                        else
                            treeView1.Nodes.Add(tn);
                    }
                    else
                        _selectedNode.Nodes.Add(tn);
                    
                
            }
            else {
                MessageBox.Show("Nothing is selected", "Nothing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String sql = "SELECT *  FROM [accounts]";
            try
            {
                _connection.Open();
                _adapter = new SqlDataAdapter(sql, _connection);

                _adapter.Fill(_acountsTb);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _connection.Close();
            }

            PopulateTreeView(0, null);

        }
        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {

            TreeNode childNode;

            foreach (DataRow dr in _acountsTb.Select("[parent]=" + parentId))
            {
                TreeNode t = new TreeNode();
                t.Text = dr["code"].ToString() + " - " + dr["ac_name"].ToString();
                t.Name = dr["code"].ToString();
                t.Tag = _acountsTb.Rows.IndexOf(dr);
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
                PopulateTreeView(Convert.ToInt32(dr["code"].ToString()), childNode);
            }
        }
    }
}
