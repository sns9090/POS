using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace POS
{
    public partial class User_advance : BaseForm
    {
        public TreeNode previousSelectedNode = null;
        public TreeNode m_previousSelectedNode = null;

        SqlConnection con2 = BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        string usid;
        public User_advance(string id)
        {
            InitializeComponent();
            usid = id;
        }

        private void User_advance_Load(object sender, EventArgs e)
        {
            textBox1.Text = usid;

            
           // SqlConnection con = new SqlConnection(@"Data Source=RYD1-PC\INFOSOFT12;Initial Catalog=DBASE;User id=sa;Password=infocic;");
            SqlDataAdapter da = new SqlDataAdapter("select formkey from users where user_name='" + textBox1.Text + "'", con2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            
            foreach (TreeNode t in treeView1.Nodes)
            {
                if (dt.Rows[0][0].ToString().Contains(t.Name.ToString()))
                    t.Checked = true;

                for (int iParent = 0; iParent < t.Nodes.Count; iParent++)
                {
                    if (dt.Rows[0][0].ToString().Contains(t.Nodes[iParent].Name.ToString()))
                        t.Nodes[iParent].Checked = true;

                    for (int iChild = 0; iChild < t.Nodes[iParent].Nodes.Count; iChild++)
                    {
                        if (dt.Rows[0][0].ToString().Contains(t.Nodes[iParent].Nodes[iChild].Name.ToString()))
                        {
                            t.Nodes[iParent].Nodes[iChild].Checked = true;

                            string str2 = dt.Rows[0][0].ToString();
                            //t.Nodes[iParent].Nodes[iChild].Name.ToString();
                            string whatever = str2.Substring(str2.IndexOf(t.Nodes[iParent].Nodes[iChild].Name.ToString()) + 5, 3);
                            //string str2 = t.Nodes[iParent].Nodes[iChild].Name.ToString();
                            //var result = str2.Substring(str2.LastIndexOf() + 1);                         
                            //  int index = str2.IndexOf('.');
                            t.Nodes[iParent].Nodes[iChild].Tag = whatever;
                        }
                    }
                }
            }

            //foreach (TreeNode t in treeView1.Nodes)
            //{
            //    if (dt.Rows[0][0].ToString().Contains(t.Tag.ToString()))
            //        t.Checked = true;

            //    for (int iParent = 0; iParent < t.Nodes.Count; iParent++)
            //    {
            //        if (dt.Rows[0][0].ToString().Contains(t.Nodes[iParent].Tag.ToString()))
            //            t.Nodes[iParent].Checked = true;

            //        for (int iChild = 0; iChild < t.Nodes[iParent].Nodes.Count; iChild++)
            //        {
            //            if (dt.Rows[0][0].ToString().Contains(t.Nodes[iParent].Nodes[iChild].Tag.ToString()))
            //                t.Nodes[iParent].Nodes[iChild].Checked = true;
            //        }
            //    }
            //}
            if (usid == "admin")
            {
                button5_Click(sender, e);
                treeView1.Enabled = false;
                button4.Enabled = false;
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                treeView1.SelectedNode.Tag = "1" + treeView1.SelectedNode.Tag.ToString().Substring(1, 2);
            }
            else
            {
                treeView1.SelectedNode.Tag = "0" + treeView1.SelectedNode.Tag.ToString().Substring(1, 2);
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // if (treeView1.SelectedNode.Tag.ToString().Substring(1, 1).Equals("0"))
            if (checkBox2.Checked)
            {
                treeView1.SelectedNode.Tag = treeView1.SelectedNode.Tag.ToString().Substring(0, 1) + "1" + treeView1.SelectedNode.Tag.ToString().Substring(2, 1);
            }
            else
            {
                treeView1.SelectedNode.Tag = treeView1.SelectedNode.Tag.ToString().Substring(0, 1) + "0" + treeView1.SelectedNode.Tag.ToString().Substring(2, 1);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                treeView1.SelectedNode.Tag = treeView1.SelectedNode.Tag.ToString().Substring(0, 2) + "1";
            }
            else
            {
                treeView1.SelectedNode.Tag = treeView1.SelectedNode.Tag.ToString().Substring(0, 2) + "0";
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            // MessageBox.Show(treeView1.SelectedNode.Level.ToString());
            //  label1.Text = treeView1.SelectedNode.Tag.ToString();
            try
            {
                //if (treeView1.SelectedNode.Level.ToString().Equals("2"))
                //{

                if (treeView1.SelectedNode.Tag.ToString().Substring(0, 1).Equals("1"))
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }

                if (treeView1.SelectedNode.Tag.ToString().Substring(1, 1).Equals("1"))
                {
                    checkBox2.Checked = true;
                }
                else
                {
                    checkBox2.Checked = false;
                }

                if (treeView1.SelectedNode.Tag.ToString().Substring(2, 1).Equals("1"))
                {
                    checkBox3.Checked = true;
                }
                else
                {
                    checkBox3.Checked = false;
                }

                if (treeView1.SelectedNode.Level.ToString().Equals("2") && (treeView1.SelectedNode.Name.StartsWith("A12") || treeView1.SelectedNode.Name.StartsWith("B12") || treeView1.SelectedNode.Name.StartsWith("C12") || treeView1.SelectedNode.Name.StartsWith("P12") || treeView1.SelectedNode.Name.StartsWith("D12") || treeView1.SelectedNode.Name.StartsWith("E12") || treeView1.SelectedNode.Name.StartsWith("C111") || treeView1.SelectedNode.Name.StartsWith("D111") || treeView1.SelectedNode.Name.StartsWith("E111")))
                {
                    checkBox1.Enabled = true;
                    checkBox2.Enabled = true;
                    checkBox3.Enabled = true;

                }
                //}
                else
                {
                    checkBox1.Enabled = false;
                    checkBox2.Enabled = false;
                    checkBox3.Enabled = false;
                }
                //  treeView1.SelectedNode.BackColor = SystemColors.Highlight;
                /*
                if (previousSelectedNode != null)
                {
                    previousSelectedNode.BackColor = treeView1.BackColor;
                    previousSelectedNode.ForeColor = treeView1.ForeColor;
                }
                 */
                if (m_previousSelectedNode != null)
                {
                    m_previousSelectedNode.BackColor = treeView1.BackColor;
                    m_previousSelectedNode.ForeColor = treeView1.ForeColor;
                }

                e.Node.BackColor = SystemColors.Highlight;
                e.Node.ForeColor = Color.White;

                m_previousSelectedNode = treeView1.SelectedNode;
            }
            catch { }
        }

        private void treeView1_Validating(object sender, CancelEventArgs e)
        {/*
            try
            {
                treeView1.SelectedNode.BackColor = SystemColors.Highlight;
                treeView1.SelectedNode.ForeColor = Color.White;
                previousSelectedNode = treeView1.SelectedNode;
            }
            catch { }
          */
        }

        
        

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "";

            //foreach (TreeNode node in treeView1.Nodes)
            //{
            //    if (node.Checked)
            //        str = str + " " + node.Tag;

            //    //foreach (TreeNode child in node.Nodes)
            //    //    if (child.Checked)
            //    //        str = str + " " + child.Tag;
            //    foreach (TreeNode child in node.Nodes)
            //        if (child.Checked)
            //            str = str + " " + child.Tag;
            //    /*
            //    foreach (TreeNode bnchild in child.Nodes)
            //        if (bnchild.Checked)
            //            str = str + " " + bnchild.Tag;
            //    */
            //}

            foreach (TreeNode t in treeView1.Nodes)
            {
                if (t.Checked)
                    //str = str + " " + t.Name+"_"+t.Tag;
                    str = str + " " + t.Name;

                for (int iParent = 0; iParent < t.Nodes.Count; iParent++)
                {
                    if (t.Nodes[iParent].Checked)
                        // str = str + " " + t.Nodes[iParent].Name + "_" + t.Nodes[iParent].Tag;
                        str = str + " " + t.Nodes[iParent].Name;
                    ////////for (int iChild = 0; iChild < t.Nodes[iParent].Nodes.Count; iChild++)
                    ////////{
                    ////////    if (t.Nodes[iParent].Nodes[iChild].Checked && t.Nodes[iParent].Nodes[iChild].Level.ToString().Equals("2") && (t.Nodes[iParent].Nodes[iChild].Name.StartsWith("A12") || t.Nodes[iParent].Nodes[iChild].Name.StartsWith("B12") || t.Nodes[iParent].Nodes[iChild].Name.StartsWith("C12") || t.Nodes[iParent].Nodes[iChild].Name.StartsWith("P12") || t.Nodes[iParent].Nodes[iChild].Name.StartsWith("D12") || t.Nodes[iParent].Nodes[iChild].Name.StartsWith("E12") || t.Nodes[iParent].Nodes[iChild].Name.StartsWith("C111") || t.Nodes[iParent].Nodes[iChild].Name.StartsWith("D111") || t.Nodes[iParent].Nodes[iChild].Name.StartsWith("E111")))
                    ////////        str = str + " " + t.Nodes[iParent].Nodes[iChild].Name + "_" + t.Nodes[iParent].Nodes[iChild].Tag;
                    ////////    else
                    ////////        str = str + " " + t.Nodes[iParent].Nodes[iChild].Name + "_" + "000";
                    ////////}
                    for (int iChild = 0; iChild < t.Nodes[iParent].Nodes.Count; iChild++)
                    {
                        if (t.Nodes[iParent].Nodes[iChild].Checked)
                            str = str + " " + t.Nodes[iParent].Nodes[iChild].Name + "_" + t.Nodes[iParent].Nodes[iChild].Tag;
                    }
                }
            }

            str = "T100" + " " +str;

            //  TreeNode node1 = e.Node;
            //  bool is_checked = node1.Checked;
            // foreach (TreeNode child in node1.Nodes)
            //      child.Checked = is_checked;
            // trvMeals.SelectedNode = node1;

          //  SqlConnection con = new SqlConnection(@"Data Source=RYD1-PC\INFOSOFT12;Initial Catalog=DBASE;User id=sa;Password=infocic;");
            SqlCommand com = new SqlCommand();
            // SqlCommand sqlcmd = new SqlCommand();
            // sqlcmd.CommandType = CommandType.StoredProcedure;
            com.CommandText = "update users set formkey='" + str + "' where user_name='"+textBox1.Text+"'";
            com.Connection = con2;
            if(con2.State == ConnectionState.Closed) con2.Open();
            com.ExecuteNonQuery();
            con2.Close();

            MessageBox.Show("تم الحفظ بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            treeView1.CollapseAll();
        }

        private void CheckAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = true; 
                CheckChildren(node, true);
            }
        }

        private void UnCheckAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = false; 
                CheckChildren(node, false);
            }
        }
        private void CheckChildren(TreeNode rootNode, bool isChecked)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                CheckChildren(node, isChecked);
                node.Checked = isChecked; 
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
          //  CheckAllNodes(treeView1.Nodes);
            foreach (TreeNode t in treeView1.Nodes)
            {
                //  if (dt.Rows[0][0].ToString().Contains(t.Name.ToString()))
                t.Checked = true;

                for (int iParent = 0; iParent < t.Nodes.Count; iParent++)
                {
                    //  if (dt.Rows[0][0].ToString().Contains(t.Nodes[iParent].Name.ToString()))
                    t.Nodes[iParent].Checked = true;

                    for (int iChild = 0; iChild < t.Nodes[iParent].Nodes.Count; iChild++)
                    {
                        //    if (dt.Rows[0][0].ToString().Contains(t.Nodes[iParent].Nodes[iChild].Name.ToString()))
                        {
                            t.Nodes[iParent].Nodes[iChild].Checked = true;
                            if (t.Nodes[iParent].Nodes[iChild].Level.ToString().Equals("2") && (t.Nodes[iParent].Nodes[iChild].Name.StartsWith("A12") || t.Nodes[iParent].Nodes[iChild].Name.StartsWith("B12") || t.Nodes[iParent].Nodes[iChild].Name.StartsWith("C12") || t.Nodes[iParent].Nodes[iChild].Name.StartsWith("P12") || t.Nodes[iParent].Nodes[iChild].Name.StartsWith("D12") || t.Nodes[iParent].Nodes[iChild].Name.StartsWith("E12") || t.Nodes[iParent].Nodes[iChild].Name.StartsWith("C111") || t.Nodes[iParent].Nodes[iChild].Name.StartsWith("D111") || t.Nodes[iParent].Nodes[iChild].Name.StartsWith("E111")))
                                t.Nodes[iParent].Nodes[iChild].Tag = "111";
                            else
                                t.Nodes[iParent].Nodes[iChild].Tag = "000";
                            //  string str2 = dt.Rows[0][0].ToString();
                            //t.Nodes[iParent].Nodes[iChild].Name.ToString();
                            //   string whatever = str2.Substring(str2.IndexOf(t.Nodes[iParent].Nodes[iChild].Name.ToString()) + 5, 3);
                            //string str2 = t.Nodes[iParent].Nodes[iChild].Name.ToString();
                            //var result = str2.Substring(str2.LastIndexOf() + 1);                         
                            //  int index = str2.IndexOf('.');
                            //  t.Nodes[iParent].Nodes[iChild].Tag = whatever;
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
          //  UnCheckAllNodes(treeView1.Nodes);
            foreach (TreeNode t in treeView1.Nodes)
            {
                //  if (dt.Rows[0][0].ToString().Contains(t.Name.ToString()))
                t.Checked = false;

                for (int iParent = 0; iParent < t.Nodes.Count; iParent++)
                {
                    //  if (dt.Rows[0][0].ToString().Contains(t.Nodes[iParent].Name.ToString()))
                    t.Nodes[iParent].Checked = false;

                    for (int iChild = 0; iChild < t.Nodes[iParent].Nodes.Count; iChild++)
                    {
                        //    if (dt.Rows[0][0].ToString().Contains(t.Nodes[iParent].Nodes[iChild].Name.ToString()))
                        {
                            t.Nodes[iParent].Nodes[iChild].Checked = false;
                            t.Nodes[iParent].Nodes[iChild].Tag = "000";
                            //  string str2 = dt.Rows[0][0].ToString();
                            //t.Nodes[iParent].Nodes[iChild].Name.ToString();
                            //   string whatever = str2.Substring(str2.IndexOf(t.Nodes[iParent].Nodes[iChild].Name.ToString()) + 5, 3);
                            //string str2 = t.Nodes[iParent].Nodes[iChild].Name.ToString();
                            //var result = str2.Substring(str2.LastIndexOf() + 1);                         
                            //  int index = str2.IndexOf('.');
                            //  t.Nodes[iParent].Nodes[iChild].Tag = whatever;
                        }
                    }
                }
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
           // this.CheckAllChildren(treeView1.SelectedNode);
            /*
            treeView1.BeginUpdate();
            foreach (TreeNode tn in e.Node.Nodes)
            {
                tn.Checked = e.Node.Checked;
                tn.Tag = "111";
            }
            treeView1.EndUpdate();
             */
        }

        private void CheckAllChildren(TreeNode node)
        {
            //foreach (TreeNode child in node.Nodes)
            //{
            //    child.Checked = true;
            //    CheckAllChildren(child);
            //}
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            if (usid == "admin")
            {
                button5_Click(sender, e);
                treeView1.Enabled = true;
                button4.Enabled = true;
            }
        }
    }
}
