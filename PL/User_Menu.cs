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
using System.Diagnostics;
using System.IO;

namespace POS
{
    public partial class User_Menu : BaseForm
    {
        public TreeNode previousSelectedNode = null;
        public TreeNode m_previousSelectedNode = null;
        BL.DAML daml = new BL.DAML();
        SqlConnection con2 = BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        string usid;
        public User_Menu(string id)
        {
            InitializeComponent();
            usid = id;
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

           // CallRecursive(treeView1);
            var nodes = new Stack<TreeNode>(treeView1.Nodes.Cast<TreeNode>());
            while (nodes.Count > 0)
            {
                var n = nodes.Pop();
                if (!n.Checked)
                {
                    if (n.Parent != null)
                    {
                        n.Parent.Nodes.Remove(n);
                    }
                    else
                    {
                        treeView1.Nodes.Remove(n);
                    }
                }
                else
                {
                    foreach (TreeNode tn in n.Nodes)
                    {
                        nodes.Push(tn);
                    }
                }
            }

            ////foreach (TreeNode t in treeView1.Nodes)
            ////{
            ////    if (t.Checked == false)
            ////    {
            ////        treeView1.Nodes.Remove(t);
            ////    }
            ////    else
            ////    {
            ////        for (int iParent = 0; iParent < t.Nodes.Count; iParent++)
            ////        {
            ////            if (t.Nodes[iParent].Checked == false)
            ////            {
            ////                treeView1.Nodes.Remove(t.Nodes[iParent]);
            ////            }
            ////            else
            ////            {
            ////                for (int iChild = 0; iChild < t.Nodes[iParent].Nodes.Count; iChild++)
            ////                {
            ////                    if (t.Nodes[iParent].Nodes[iChild].Checked == false)
            ////                    {
            ////                        treeView1.Nodes.Remove(t.Nodes[iParent].Nodes[iChild]);
            ////                    }
            ////                }
            ////            }
            ////        }
            ////    }
            ////}

            ////foreach (TreeNode t in treeView1.Nodes)
            ////{
            ////    if (dt.Rows[0][0].ToString().Contains(t.Name.ToString()))
            ////    {
            ////        t.Checked = true;
            ////        for (int iParent = 0; iParent < t.Nodes.Count; iParent++)
            ////        {
            ////            if (dt.Rows[0][0].ToString().Contains(t.Nodes[iParent].Name.ToString()))
            ////            {
            ////                t.Nodes[iParent].Checked = true;
            ////                for (int iChild = 0; iChild < t.Nodes[iParent].Nodes.Count; iChild++)
            ////                {
            ////                    if (dt.Rows[0][0].ToString().Contains(t.Nodes[iParent].Nodes[iChild].Name.ToString()))

            ////                        t.Nodes[iParent].Nodes[iChild].Checked = true;
            ////                  //  else
            ////                    //    treeView1.Nodes.Remove(t.Nodes[iParent].Nodes[iChild]);
            ////                    //string str2 = dt.Rows[0][0].ToString();
            ////                    ////t.Nodes[iParent].Nodes[iChild].Name.ToString();
            ////                    //string whatever = str2.Substring(str2.IndexOf(t.Nodes[iParent].Nodes[iChild].Name.ToString()) + 5, 3);
            ////                    ////string str2 = t.Nodes[iParent].Nodes[iChild].Name.ToString();
            ////                    ////var result = str2.Substring(str2.LastIndexOf() + 1);                         
            ////                    ////  int index = str2.IndexOf('.');
            ////                    //t.Nodes[iParent].Nodes[iChild].Tag = whatever;

            ////                }
            ////            }
            ////           // else
            ////            //    treeView1.Nodes.Remove(t.Nodes[iParent]);

                       
            ////        }
            ////    }
            ////    else
            ////        treeView1.Nodes.Remove(t);

               
            ////}

          //  TrimTree();

          //  var visibleNodes = GetVisibleNodes(treeView1).ToList();
            //////foreach (TreeNode t in treeView1.Nodes)
            //////{
            //////    if (!dt.Rows[0][0].ToString().Contains(t.Name.ToString()))
            //////        //t.Checked = true;

            //////        treeView1.Nodes.RemoveAt(t.Index);
            //////    else
            //////    {

            //////        for (int iParent = 0; iParent < t.Nodes.Count; iParent++)
            //////        {
            //////            if (!dt.Rows[0][0].ToString().Contains(t.Nodes[iParent].Name.ToString()))
            //////                // t.Nodes[iParent].Checked = true;
            //////                treeView1.Nodes.RemoveAt(t.Nodes[iParent].Index);
            //////            else
            //////            {
            //////                for (int iChild = 0; iChild < t.Nodes[iParent].Nodes.Count; iChild++)
            //////                {
            //////                    if (!dt.Rows[0][0].ToString().Contains(t.Nodes[iParent].Nodes[iChild].Name.ToString()))

            //////                        //t.Nodes[iParent].Nodes[iChild].Checked = true;

            //////                        treeView1.Nodes.RemoveAt(t.Nodes[iParent].Nodes[iChild].Index);


            //////                    else
            //////                    {
            //////                        /*
            //////                        string str2 = dt.Rows[0][0].ToString();
            //////                        //t.Nodes[iParent].Nodes[iChild].Name.ToString();
            //////                        string whatever = str2.Substring(str2.IndexOf(t.Nodes[iParent].Nodes[iChild].Name.ToString()) + 5, 3);
            //////                        //string str2 = t.Nodes[iParent].Nodes[iChild].Name.ToString();
            //////                        //var result = str2.Substring(str2.LastIndexOf() + 1);                         
            //////                        //  int index = str2.IndexOf('.');
            //////                        t.Nodes[iParent].Nodes[iChild].Tag = whatever;
            //////                         */
            //////                    }
            //////                }
            //////            }
            //////        }
            //////    }
            //////}
           // treeView1.Refresh();
            
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
            
            ////if (usid == "admin")
            ////{
            ////    button5_Click(sender, e);
            ////    treeView1.Enabled = false;
            ////    button4.Enabled = false;
            ////}

        }
        private void PrintRecursive(TreeNode treeNode)
        {
            //do something with each node
            //System.Diagnostics.Debug.WriteLine(treeNode.Text);
            foreach (TreeNode tn in treeNode.Nodes)
            {
                if (tn.Checked == false)
                    treeView1.Nodes.Remove(tn);
                else
                  PrintRecursive(tn);
            }
        }

        // Call the procedure using the TreeView.
        private void CallRecursive(TreeView treeView)
        {
            // Print each node recursively.
            TreeNodeCollection nodes = treeView.Nodes;
            foreach (TreeNode n in nodes)
            {
                if (n.Checked == false)
                    treeView1.Nodes.Remove(n);
                else
                    PrintRecursive(n);
            }
        }
        void TrimTree()
        {
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Checked == false)
                    treeView1.Nodes.Remove(node);
                else
                {
                    if (node.Nodes.Count > 0)
                    {
                        if (node.Checked == true)
                        {
                            foreach (TreeNode childNode in node.Nodes)
                            {
                                if (childNode.Checked == false)
                                    treeView1.Nodes.Remove(childNode);
                            }
                        }
                        else { treeView1.Nodes.Remove(node); }
                    }
                }
            }
            /*
            TreeNode node = null;
            for (int ndx = treeView1.Nodes.Count; ndx > 0; ndx--)
            {
                node =treeView1.Nodes[ndx - 1];
              //  TrimTree(treeView1.Nodes.ChildNodes, l);
                if (node.Checked==false)
                    treeView1.Nodes.Remove(node);
            }*/
        }
        public IEnumerable<TreeNode> GetVisibleNodes(TreeView t)
        {
            var node = t.Nodes.Cast<TreeNode>().Where(x => x.IsVisible).FirstOrDefault();
            while (node != null)
            {
                var temp = node;

                node = node.NextVisibleNode;
                yield return temp;
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

                if (treeView1.SelectedNode.Level.ToString().Equals("2"))
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
           // treeView1.SelectedNode.Expand();
            treeView1.ExpandAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //treeView1.SelectedNode.Collapse();
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
                            t.Nodes[iParent].Nodes[iChild].Tag = "111";
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

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode.Name == "B113")
                {
                    if (BL.CLS_Session.formkey.Contains("B113"))
                    {
                        Sal.Trans rs = new Sal.Trans();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }

                if (treeView1.SelectedNode.Name == "F110")
                {
                    if (BL.CLS_Session.formkey.Contains("F110"))
                    {
                        Company rs = new Company();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "F120")
                {
                    if (BL.CLS_Session.formkey.Contains("F120"))
                    {
                        Branchs f6 = new Branchs();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "F130")
                {
                    if (BL.CLS_Session.UserID.Equals("admin"))
                    {
                        USER_PRO us = new USER_PRO();
                        us.MdiParent = MdiParent;
                        us.Show();
                    }
                    else
                    {
                        Chang_UsrPass us = new Chang_UsrPass(BL.CLS_Session.UserID);
                        us.MdiParent = MdiParent;
                        us.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "F140")
                {
                    if (BL.CLS_Session.formkey.Contains("F140"))
                    {
                        Acc.Crncy rs = new Acc.Crncy();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "F150")
                {
                    if (BL.CLS_Session.formkey.Contains("F150"))
                    {
                        Pubtext rs = new Pubtext();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "F160")
                {
                    if (BL.CLS_Session.formkey.Contains("F160"))
                    {
                        SetBarcPrt br = new SetBarcPrt();
                        br.MdiParent = MdiParent;
                        br.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A111")
                {
                    if (BL.CLS_Session.formkey.Contains("A111"))
                    {
                        Acc.Acc_Card ac = new Acc.Acc_Card(null);
                        ac.MdiParent = MdiParent;
                        ac.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A112")
                {
                    if (BL.CLS_Session.formkey.Contains("A112"))
                    {
                        Acc.Acc_opnbal rs = new Acc.Acc_opnbal();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A114")
                {
                    if (BL.CLS_Session.formkey.Contains("A114"))
                    {
                        Acc.CostCnters acc = new Acc.CostCnters();
                        acc.MdiParent = MdiParent;
                        acc.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A113")
                {
                    if (BL.CLS_Session.formkey.Contains("A113"))
                    {
                        Acc.Periods rs = new Acc.Periods();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A121")
                {
                    if (BL.CLS_Session.formkey.Contains("A121"))
                    {
                        Acc.Acc_Tran f6 = new Acc.Acc_Tran("", "", "");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A122")
                {
                    if (BL.CLS_Session.formkey.Contains("A122"))
                    {
                        Acc.Acc_Tran_Q rs = new Acc.Acc_Tran_Q("", "");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A123")
                {
                    if (BL.CLS_Session.formkey.Contains("A123"))
                    {
                        Acc.Acc_Tran_S rs = new Acc.Acc_Tran_S("", "");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A125")
                {
                    if (BL.CLS_Session.formkey.Contains("A125"))
                    {
                        Acc.Acc_Close rs = new Acc.Acc_Close();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }

                if (treeView1.SelectedNode.Name == "A126")
                {
                    if (BL.CLS_Session.formkey.Contains("A126"))
                    {
                        Acc.Acc_Update rs = new Acc.Acc_Update();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }

                if (treeView1.SelectedNode.Name == "A127")
                {
                    if (BL.CLS_Session.formkey.Contains("A127"))
                    {
                        Acc.Copy_Data rs = new Acc.Copy_Data();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }

                if (treeView1.SelectedNode.Name == "A124")
                {
                    if (BL.CLS_Session.formkey.Contains("A124"))
                    {
                        Acc.Acc_Tax_Tran act = new Acc.Acc_Tax_Tran("", "", "");
                        // Acc.Acc_Tax_Tran_All act = new Acc.Acc_Tax_Tran_All("", "");
                        act.MdiParent = MdiParent;
                        act.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A131")
                {
                    if (BL.CLS_Session.formkey.Contains("A131"))
                    {
                        Acc.Acc_Report f6 = new Acc.Acc_Report();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A13E")
                {
                    if (BL.CLS_Session.formkey.Contains("A13E"))
                    {
                        Acc.Acc_Report_Dtl f6 = new Acc.Acc_Report_Dtl();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A132")
                {
                    if (BL.CLS_Session.formkey.Contains("A132"))
                    {
                        Acc.Acc_Statment_org f6 = new Acc.Acc_Statment_org("");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A133")
                {
                    if (BL.CLS_Session.formkey.Contains("A133"))
                    {
                        Acc.Acc_Statment_Exp f6 = new Acc.Acc_Statment_Exp("");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A139")
                {
                    if (BL.CLS_Session.formkey.Contains("A139"))
                    {
                        Acc.Acc_Statment_All f6 = new Acc.Acc_Statment_All("");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A134")
                {
                    if (BL.CLS_Session.formkey.Contains("A134"))
                    {
                        Acc.Acc_Balance f6 = new Acc.Acc_Balance();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A135")
                {
                    if (BL.CLS_Session.formkey.Contains("A135"))
                    {
                        Acc.Acc_Mizan f6 = new Acc.Acc_Mizan();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A13H")
                {
                    if (BL.CLS_Session.formkey.Contains("A13H"))
                    {
                        Acc.Acc_Mizan_Tran f6 = new Acc.Acc_Mizan_Tran();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A136")
                {
                    if (BL.CLS_Session.formkey.Contains("A136"))
                    {
                        Acc.Acc_Final_Report f6 = new Acc.Acc_Final_Report();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A13A")
                {
                    if (BL.CLS_Session.formkey.Contains("A13A"))
                    {
                        Acc.Acc_QD_Report f6 = new Acc.Acc_QD_Report();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A13B")
                {
                    if (BL.CLS_Session.formkey.Contains("A13B"))
                    {
                        Acc.CC_Statment_Exp f6 = new Acc.CC_Statment_Exp("");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A13C")
                {
                    if (BL.CLS_Session.formkey.Contains("A13C"))
                    {
                        Acc.CC_Balance f6 = new Acc.CC_Balance();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A13D")
                {
                    if (BL.CLS_Session.formkey.Contains("A13D"))
                    {
                        Acc.CC_Final_Report f6 = new Acc.CC_Final_Report();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A13F")
                {
                    if (BL.CLS_Session.formkey.Contains("A13F"))
                    {
                        Acc.Acc_bdgt_report f6 = new Acc.Acc_bdgt_report();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A13G")
                {
                    if (BL.CLS_Session.formkey.Contains("A13G"))
                    {
                        Acc.Acc_cashinout_report f6 = new Acc.Acc_cashinout_report();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A13I")
                {
                    if (BL.CLS_Session.formkey.Contains("A13I"))
                    {
                        Acc.Acc_Final_SC_Report f6 = new Acc.Acc_Final_SC_Report();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A13J")
                {
                    if (BL.CLS_Session.formkey.Contains("A13J"))
                    {
                        Acc.Acc_Final_Report_mm f6 = new Acc.Acc_Final_Report_mm();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A138")
                {
                    if (BL.CLS_Session.formkey.Contains("A138"))
                    {
                        Acc.Acc_Tax_Report f6 = new Acc.Acc_Tax_Report();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "A137")
                {
                    if (BL.CLS_Session.formkey.Contains("A137"))
                    {
                        Acc.VAT rs = new Acc.VAT();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B111")
                {
                    if (BL.CLS_Session.formkey.Contains("B111"))
                    {
                        Sal.SLcenters rs = new Sal.SLcenters();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B112")
                {
                    if (BL.CLS_Session.formkey.Contains("B112"))
                    {
                        Sal.Salmen rs = new Sal.Salmen();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B121")
                {
                    if (BL.CLS_Session.formkey.Contains("B121"))
                    {
                        if (BL.CLS_Session.cmp_type.StartsWith("a"))
                        {
                            Sal.Sal_Tran_D_TF rs = new Sal.Sal_Tran_D_TF("", "", "");
                            rs.MdiParent = MdiParent;
                            rs.Show();
                        }
                        else
                        {
                            Sal.Sal_Tran_D rs = new Sal.Sal_Tran_D("", "", "");
                            rs.MdiParent = MdiParent;
                            rs.Show();
                        }
                    }
                }
                if (treeView1.SelectedNode.Name == "B122")
                {
                    if (BL.CLS_Session.formkey.Contains("B122"))
                    {
                        Sal.Sal_Tran_notax rs = new Sal.Sal_Tran_notax("", "", "");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B123")
                {
                    if (BL.CLS_Session.formkey.Contains("B123"))
                    {
                        Sal.Sal_Ofr_Tran_D rs = new Sal.Sal_Ofr_Tran_D("", "", "");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B124")
                {
                    if (BL.CLS_Session.formkey.Contains("B124"))
                    {
                        Sal.Sal_Ord_Tran_D rs = new Sal.Sal_Ord_Tran_D("", "", "");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B125")
                {
                    if (BL.CLS_Session.formkey.Contains("B125"))
                    {
                        Sal.Sal_Items_Offer rs = new Sal.Sal_Items_Offer();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }

                if (treeView1.SelectedNode.Name == "B126")
                {
                    if (BL.CLS_Session.formkey.Contains("B126"))
                    {
                        Sal.Sal_Chang_Type rs = new Sal.Sal_Chang_Type();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }

                if (treeView1.SelectedNode.Name == "B131")
                {
                    if (BL.CLS_Session.formkey.Contains("B131"))
                    {
                        Sal.Sal_Report rs = new Sal.Sal_Report();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B13B")
                {
                    if (BL.CLS_Session.formkey.Contains("B13B"))
                    {
                        Sal.Sal_Report_Dtl f6 = new Sal.Sal_Report_Dtl();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }

                if (treeView1.SelectedNode.Name == "B13F")
                {
                    if (BL.CLS_Session.formkey.Contains("B13F"))
                    {
                        Sal.Sal_Report_Sal_WC rs = new Sal.Sal_Report_Sal_WC();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }

                if (treeView1.SelectedNode.Name == "B13Q")
                {
                    if (BL.CLS_Session.formkey.Contains("B13Q"))
                    {
                        Sal.Sal_Qst_Balance rs = new Sal.Sal_Qst_Balance();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }

                if (treeView1.SelectedNode.Name == "B13S")
                {
                    if (BL.CLS_Session.formkey.Contains("B13S"))
                    {
                        Sal.Sal_Qst_Statment_Exp f4a = new Sal.Sal_Qst_Statment_Exp("", "");
                        f4a.MdiParent = MdiParent;
                        f4a.Show();
                    }
                }

                if (treeView1.SelectedNode.Name == "B132")
                {
                    if (BL.CLS_Session.formkey.Contains("B132"))
                    {
                        Sal.Sal_Report_Sal_WR rs = new Sal.Sal_Report_Sal_WR();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B133")
                {
                    if (BL.CLS_Session.formkey.Contains("B133"))
                    {
                        Sal.Sal_Report_Cus_WR rs = new Sal.Sal_Report_Cus_WR();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B134")
                {
                    if (BL.CLS_Session.formkey.Contains("B134"))
                    {
                        Sal.Sal_Report_items_WR rs = new Sal.Sal_Report_items_WR();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B135")
                {
                    if (BL.CLS_Session.formkey.Contains("B135"))
                    {
                        Sal.Sal_Ofr_Report rs = new Sal.Sal_Ofr_Report();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B136")
                {
                    if (BL.CLS_Session.formkey.Contains("B136"))
                    {
                        Sal.Sal_Report_sp rs = new Sal.Sal_Report_sp();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B137")
                {
                    if (BL.CLS_Session.formkey.Contains("B137"))
                    {
                        Sal.Sal_Report_DM rs = new Sal.Sal_Report_DM();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B138")
                {
                    if (BL.CLS_Session.formkey.Contains("B138"))
                    {
                        Sal.Sal_Report_Br rs = new Sal.Sal_Report_Br();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B139")
                {
                    if (BL.CLS_Session.formkey.Contains("B139"))
                    {
                        Sal.Sal_Report_Gr rs = new Sal.Sal_Report_Gr();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B13A")
                {
                    if (BL.CLS_Session.formkey.Contains("B13A"))
                    {
                        Sal.Sal_Report_Cs rs = new Sal.Sal_Report_Cs();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B13E")
                {
                    if (BL.CLS_Session.formkey.Contains("B13E"))
                    {
                        Sal.Sal_Report_SupItems_WR rs = new Sal.Sal_Report_SupItems_WR();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B13C")
                {
                    if (BL.CLS_Session.formkey.Contains("B13C"))
                    {
                        Sal.Sal_Report_items_Sal_Mor_No fr = new Sal.Sal_Report_items_Sal_Mor_No();
                        fr.MdiParent = MdiParent;
                        fr.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "B13D")
                {
                    if (BL.CLS_Session.formkey.Contains("B13D"))
                    {
                        Sal.Sal_Tax_Report rs = new Sal.Sal_Tax_Report();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S111")
                {
                    if (BL.CLS_Session.formkey.Contains("S111"))
                    {
                        if (BL.CLS_Session.cmp_type.StartsWith("m"))
                        {
                            Pos.Cashirs rs = new Pos.Cashirs();
                            rs.MdiParent = MdiParent;
                            rs.Show();
                        }
                        else
                        {
                            Pos.CTR_FRM rs = new Pos.CTR_FRM();
                            rs.MdiParent = MdiParent;
                            rs.Show();
                        }
                    }
                }
                if (treeView1.SelectedNode.Name == "S112")
                {
                    if (BL.CLS_Session.formkey.Contains("S112"))
                    {
                        Pos.Set_Form stf = new Pos.Set_Form();
                        stf.MdiParent = MdiParent;
                        stf.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S113")
                {
                    if (BL.CLS_Session.formkey.Contains("S113"))
                    {
                        SetMainPrt rs = new SetMainPrt();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S114")
                {
                    if (BL.CLS_Session.formkey.Contains("S114"))
                    {
                        SetAddPrt rs = new SetAddPrt();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S115")
                {
                    if (BL.CLS_Session.formkey.Contains("S115"))
                    {
                        Pos.Pos_Sal_Men f6 = new Pos.Pos_Sal_Men();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }

                if (treeView1.SelectedNode.Name == "S116")
                {
                    if (BL.CLS_Session.formkey.Contains("S116"))
                    {
                        Pos.Adds f6 = new Pos.Adds();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S117")
                {
                    if (BL.CLS_Session.formkey.Contains("S117"))
                    {
                        Pos.Pos_Drivers f6 = new Pos.Pos_Drivers();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S118")
                {
                    if (BL.CLS_Session.formkey.Contains("S118"))
                    {
                        Pos.Apps f6 = new Pos.Apps();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }

                if (treeView1.SelectedNode.Name == "S121")
                {
                    if (BL.CLS_Session.formkey.Contains("S121"))
                    {
                        if (BL.CLS_Session.istuch)
                        {
                            Pos.SALES_D_TCH sales = new Pos.SALES_D_TCH();
                            // SALES sales = new SALES();
                            sales.MdiParent = MdiParent;
                            sales.Show();
                        }
                        else
                        {
                            Pos.SALES_D_TCH sales = new Pos.SALES_D_TCH();
                            // SALES sales = new SALES();
                            sales.MdiParent = MdiParent;
                            sales.Show();
                        }
                    }
                }
                if (treeView1.SelectedNode.Name == "S122")
                {
                    if (BL.CLS_Session.formkey.Contains("S122"))
                    {
                        if (BL.CLS_Session.istuch)
                        {
                            Pos.RESALES_D_TCH sales = new Pos.RESALES_D_TCH();
                            // SALES sales = new SALES();
                            sales.MdiParent = MdiParent;
                            sales.Show();
                        }
                        else
                        {
                            Pos.RESALES_D_TCH sales = new Pos.RESALES_D_TCH();
                            // SALES sales = new SALES();
                            sales.MdiParent = MdiParent;
                            sales.Show();
                        }
                    }
                }
                if (treeView1.SelectedNode.Name == "S123")
                {
                    if (BL.CLS_Session.formkey.Contains("S123"))
                    {
                        Pos.Resturant_sales dp = new Pos.Resturant_sales();
                        dp.MdiParent = MdiParent;
                        dp.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S12D")
                {
                    if (BL.CLS_Session.formkey.Contains("S12D"))
                    {
                        Pos.Pos_Tafseel dp = new Pos.Pos_Tafseel("","","");
                        dp.MdiParent = MdiParent;
                        dp.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S100")
                {
                    if (BL.CLS_Session.formkey.Contains("S100"))
                    {
                        Pos.SalesReport_ToSend dp = new Pos.SalesReport_ToSend("");
                        dp.MdiParent = MdiParent;
                        dp.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S124")
                {
                    if (BL.CLS_Session.formkey.Contains("S124"))
                    {
                        if (BL.CLS_Session.is_einv)
                        {
                            Pos.Resturant_ReSales2 dp = new Pos.Resturant_ReSales2();
                            dp.MdiParent = MdiParent;
                            dp.Show();
                        }
                        else
                        {
                            Pos.Resturant_ReSales dp = new Pos.Resturant_ReSales("", "", "");
                            dp.MdiParent = MdiParent;
                            dp.Show();
                        }
                        //Pos.Resturant_ReSales dp = new Pos.Resturant_ReSales("","","");
                        //dp.MdiParent = MdiParent;
                        //dp.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S125")
                {
                    if (BL.CLS_Session.formkey.Contains("S125"))
                    {
                        //Pos.Resturant_sales_800_600 rs = new Pos.Resturant_sales_800_600();
                        //rs.MdiParent = MdiParent;
                        //rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S126")
                {
                    if (BL.CLS_Session.formkey.Contains("S126"))
                    {
                        //Pos.Resturant_ReSales_800_600 frf = new Pos.Resturant_ReSales_800_600();
                        //frf.MdiParent = MdiParent;
                        //frf.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S129")
                {
                    if (BL.CLS_Session.formkey.Contains("S129"))
                    {
                        Pos.Import_inv_From_Pos rs = new Pos.Import_inv_From_Pos();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S12A")
                {
                    if (BL.CLS_Session.formkey.Contains("S12A"))
                    {
                        Pos.Pos_Sal_Cmbin f6 = new Pos.Pos_Sal_Cmbin();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }

                if (treeView1.SelectedNode.Name == "S12B")
                {
                    if (BL.CLS_Session.formkey.Contains("S12B"))
                    {
                        Pos.Pos_Sal_Cmbin_Rp f6 = new Pos.Pos_Sal_Cmbin_Rp();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }

                if (treeView1.SelectedNode.Name == "S12C")
                {
                    if (BL.CLS_Session.formkey.Contains("S12C"))
                    {
                        Pos.SalesReport_ToSend ts = new Pos.SalesReport_ToSend("");
                        ts.MdiParent = MdiParent;
                        ts.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S131")
                {
                    if (BL.CLS_Session.formkey.Contains("S131"))
                    {
                        Pos.DilySalesReport sr = new Pos.DilySalesReport();
                        sr.MdiParent = MdiParent;
                        sr.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S132")
                {
                    if (BL.CLS_Session.formkey.Contains("S132"))
                    {
                        Pos.Sales_Report sr = new Pos.Sales_Report();
                        sr.MdiParent = MdiParent;
                        sr.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S133")
                {
                    if (BL.CLS_Session.formkey.Contains("S133"))
                    {
                        Pos.ItemsDilySalesReport idr = new Pos.ItemsDilySalesReport();
                        idr.MdiParent = MdiParent;
                        idr.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S134")
                {
                    if (BL.CLS_Session.formkey.Contains("S134"))
                    {
                        Pos.RangItemsDilySalesReport rd = new Pos.RangItemsDilySalesReport();
                        rd.MdiParent = MdiParent;
                        rd.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S137")
                {
                    if (BL.CLS_Session.formkey.Contains("S137"))
                    {
                        Pos.RE_PRT_FRM prt = new Pos.RE_PRT_FRM();
                        prt.MdiParent = MdiParent;
                        prt.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S138")
                {
                    if (BL.CLS_Session.formkey.Contains("S138"))
                    {
                        Pos.MenuSalesItemReport rs = new Pos.MenuSalesItemReport();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S13D")
                {
                    if (BL.CLS_Session.formkey.Contains("S13D"))
                    {
                        Pos.MenuSalesCustReport rs = new Pos.MenuSalesCustReport();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S139")
                {
                    if (BL.CLS_Session.formkey.Contains("S139"))
                    {
                        Pos.PosSal_Report_DM rs = new Pos.PosSal_Report_DM();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "S13A")
                {
                    if (BL.CLS_Session.formkey.Contains("S13A"))
                    {
                        Pos.SumSalesItemReport rs = new Pos.SumSalesItemReport();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C111")
                {
                    if (BL.CLS_Session.formkey.Contains("C111"))
                    {
                        Cus.Customers cust = new Cus.Customers("");
                        cust.MdiParent = MdiParent;
                        cust.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C112")
                {
                    if (BL.CLS_Session.formkey.Contains("C112"))
                    {
                        Cus.Cclass rs = new Cus.Cclass();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C113")
                {
                    if (BL.CLS_Session.formkey.Contains("C113"))
                    {
                        Cus.Cites rs = new Cus.Cites();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C114")
                {
                    if (BL.CLS_Session.formkey.Contains("C114"))
                    {
                        Cus.Colmen rs = new Cus.Colmen();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C121")
                {
                    if (BL.CLS_Session.formkey.Contains("C121"))
                    {
                        Cus.Acc_Tran_Q rs = new Cus.Acc_Tran_Q("", "");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C122")
                {
                    if (BL.CLS_Session.formkey.Contains("C122"))
                    {
                        Cus.Acc_Tran rs = new Cus.Acc_Tran("", "");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C124")
                {
                    if (BL.CLS_Session.formkey.Contains("C124"))
                    {
                        Cus.Acc_Tran_QAK rs = new Cus.Acc_Tran_QAK("", "");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C131")
                {
                    if (BL.CLS_Session.formkey.Contains("C131"))
                    {
                        Cus.Acc_Report f6 = new Cus.Acc_Report();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C13D")
                {
                    if (BL.CLS_Session.formkey.Contains("C13D"))
                    {
                        Cus.Acc_Report_Dtl f6 = new Cus.Acc_Report_Dtl();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C132")
                {
                    if (BL.CLS_Session.formkey.Contains("C132"))
                    {
                        Cus.Acc_Statment_org f6 = new Cus.Acc_Statment_org("");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C133")
                {
                    if (BL.CLS_Session.formkey.Contains("C133"))
                    {
                        Cus.Acc_Statment_Exp f6 = new Cus.Acc_Statment_Exp("");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C135")
                {
                    if (BL.CLS_Session.formkey.Contains("C135"))
                    {
                        Cus.Acc_Statment_All f6 = new Cus.Acc_Statment_All("");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C138")
                {
                    if (BL.CLS_Session.formkey.Contains("C138"))
                    {
                        Cus.Acc_Statment_Exp_Life f6 = new Cus.Acc_Statment_Exp_Life("");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C13C")
                {
                    if (BL.CLS_Session.formkey.Contains("C13C"))
                    {
                        Cus.Acc_Statment_org_U f6 = new Cus.Acc_Statment_org_U("");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C134")
                {
                    if (BL.CLS_Session.formkey.Contains("C134"))
                    {
                        Cus.Acc_Balance rs = new Cus.Acc_Balance();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C13A")
                {
                    if (BL.CLS_Session.formkey.Contains("C13A"))
                    {
                        Cus.Acc_Balance_WLF rs = new Cus.Acc_Balance_WLF();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C13B")
                {
                    if (BL.CLS_Session.formkey.Contains("C13B"))
                    {
                        Cus.Acc_Balance_Sumry rs = new Cus.Acc_Balance_Sumry();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C13E")
                {
                    if (BL.CLS_Session.formkey.Contains("C13E"))
                    {
                        // Cus.Acc_Balances_Life f6 = new Cus.Acc_Balances_Life("");
                        Cus.Acc_Statment_Exp_Aqd f6 = new Cus.Acc_Statment_Exp_Aqd("");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C13G")
                {
                    if (BL.CLS_Session.formkey.Contains("C13G"))
                    {
                        Cus.Acc_Balance_SM_Sumry rs = new Cus.Acc_Balance_SM_Sumry();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C136")
                {
                    if (BL.CLS_Session.formkey.Contains("C136"))
                    {
                        Cus.Acc_Balance_WLP rs = new Cus.Acc_Balance_WLP();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C137")
                {
                    if (BL.CLS_Session.formkey.Contains("C137"))
                    {
                        Cus.Acc_Report_col rs = new Cus.Acc_Report_col();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C139")
                {
                    if (BL.CLS_Session.formkey.Contains("C139"))
                    {
                        Cus.Acc_Balances_Life f6 = new Cus.Acc_Balances_Life("");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "P111")
                {
                    if (BL.CLS_Session.formkey.Contains("P111"))
                    {
                        Pur.PUcenters rs = new Pur.PUcenters();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "P121")
                {
                    if (BL.CLS_Session.formkey.Contains("P121"))
                    {
                        Pur.Pur_Tran_D rs = new Pur.Pur_Tran_D("", "", "");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "P124")
                {
                    if (BL.CLS_Session.formkey.Contains("P124"))
                    {
                        Pur.Pur_Tran_D_WI rs = new Pur.Pur_Tran_D_WI("", "", "");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "P122")
                {
                    if (BL.CLS_Session.formkey.Contains("P122"))
                    {
                        Pur.Pur_Tran_notax rs = new Pur.Pur_Tran_notax("", "", "");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "P123")
                {
                    if (BL.CLS_Session.formkey.Contains("P123"))
                    {
                        Pur.Pur_Ord_Tran_D rs = new Pur.Pur_Ord_Tran_D("", "", "");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "P131")
                {
                    if (BL.CLS_Session.formkey.Contains("P131"))
                    {
                        Pur.Pur_Report rs = new Pur.Pur_Report();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "P139")
                {
                    if (BL.CLS_Session.formkey.Contains("P139"))
                    {
                        Pur.Pur_Report_Dtl f6 = new Pur.Pur_Report_Dtl();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "P132")
                {
                    if (BL.CLS_Session.formkey.Contains("P132"))
                    {
                        Pur.Pur_Report_items_WR rs = new Pur.Pur_Report_items_WR();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "P138")
                {
                    if (BL.CLS_Session.formkey.Contains("P138"))
                    {
                        Pur.Pur_Report_Sal_WR rs = new Pur.Pur_Report_Sal_WR();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "P133")
                {
                    if (BL.CLS_Session.formkey.Contains("P133"))
                    {
                        Pur.Pur_Report_Sup_WR rs = new Pur.Pur_Report_Sup_WR();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "P134")
                {
                    if (BL.CLS_Session.formkey.Contains("P134"))
                    {
                        Pur.Pur_Ofr_Report rs = new Pur.Pur_Ofr_Report();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "P135")
                {
                    if (BL.CLS_Session.formkey.Contains("P135"))
                    {
                        Pur.Pur_Report_Br rs = new Pur.Pur_Report_Br();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "P136")
                {
                    if (BL.CLS_Session.formkey.Contains("P136"))
                    {
                        Pur.Pur_Report_Gr rs = new Pur.Pur_Report_Gr();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "P137")
                {
                    if (BL.CLS_Session.formkey.Contains("P137"))
                    {
                        Pur.Pur_Report_Su rs = new Pur.Pur_Report_Su();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "P13A")
                {
                    if (BL.CLS_Session.formkey.Contains("P13A"))
                    {
                        Pur.Pur_Tax_Report rs = new Pur.Pur_Tax_Report();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "D111")
                {
                    if (BL.CLS_Session.formkey.Contains("D111"))
                    {
                        Sup.Suppliers cust = new Sup.Suppliers("");
                        cust.MdiParent = MdiParent;
                        cust.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "D112")
                {
                    if (BL.CLS_Session.formkey.Contains("D112"))
                    {
                        Sup.Sclass rs = new Sup.Sclass();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "D113")
                {
                    if (BL.CLS_Session.formkey.Contains("D113"))
                    {
                        Cus.Import_SC_From_Xls rs = new Cus.Import_SC_From_Xls("Sup");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "D121")
                {
                    if (BL.CLS_Session.formkey.Contains("D121"))
                    {
                        Sup.Acc_Tran_S rs = new Sup.Acc_Tran_S("", "");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "D122")
                {
                    if (BL.CLS_Session.formkey.Contains("D122"))
                    {
                        Sup.Acc_Tran rs = new Sup.Acc_Tran("", "");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "D123")
                {
                    if (BL.CLS_Session.formkey.Contains("D123"))
                    {
                        Sup.Acc_Tran_Cur rs = new Sup.Acc_Tran_Cur("", "");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "D131")
                {
                    if (BL.CLS_Session.formkey.Contains("D131"))
                    {
                        Sup.Acc_Report f6 = new Sup.Acc_Report();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "D13A")
                {
                    if (BL.CLS_Session.formkey.Contains("D13A"))
                    {
                        Sup.Acc_Report_Dtl f6 = new Sup.Acc_Report_Dtl();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }

                if (treeView1.SelectedNode.Name == "D13G")
                {
                    if (BL.CLS_Session.formkey.Contains("D13G"))
                    {
                        Sup.Acc_Balances_Life f6 = new Sup.Acc_Balances_Life("");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }

                if (treeView1.SelectedNode.Name == "D132")
                {
                    if (BL.CLS_Session.formkey.Contains("D132"))
                    {
                        Sup.Acc_Statment_org f6 = new Sup.Acc_Statment_org("");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "D133")
                {
                    if (BL.CLS_Session.formkey.Contains("D133"))
                    {
                        Sup.Acc_Statment_Exp f6 = new Sup.Acc_Statment_Exp("");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "D135")
                {
                    if (BL.CLS_Session.formkey.Contains("D135"))
                    {
                        Sup.Acc_Statment_All f6 = new Sup.Acc_Statment_All("");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "D139")
                {
                    if (BL.CLS_Session.formkey.Contains("D139"))
                    {
                        Sup.Acc_Statment_org_SU f6 = new Sup.Acc_Statment_org_SU("");
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "D134")
                {
                    if (BL.CLS_Session.formkey.Contains("D134"))
                    {
                        Sup.Acc_Balance rs = new Sup.Acc_Balance();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "D137")
                {
                    if (BL.CLS_Session.formkey.Contains("D137"))
                    {
                        Sup.Acc_Balance_WLF rs = new Sup.Acc_Balance_WLF();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "D136")
                {
                    if (BL.CLS_Session.formkey.Contains("D136"))
                    {
                        Sup.Acc_Balance_WLP rs = new Sup.Acc_Balance_WLP();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "D138")
                {
                    if (BL.CLS_Session.formkey.Contains("D138"))
                    {
                        Sup.Acc_Balance_Sumry rs = new Sup.Acc_Balance_Sumry();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E111")
                {
                    if (BL.CLS_Session.formkey.Contains("E111"))
                    {
                        Sto.Item_Card it = new Sto.Item_Card("");
                        it.MdiParent = MdiParent;
                        it.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E112")
                {
                    if (BL.CLS_Session.formkey.Contains("E112"))
                    {
                        Sto.Units crd = new Sto.Units();
                        crd.MdiParent = MdiParent;
                        crd.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E113")
                {
                    if (BL.CLS_Session.formkey.Contains("E113"))
                    {
                        Sto.Groups gr = new Sto.Groups();
                        gr.MdiParent = MdiParent;
                        gr.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E115")
                {
                    if (BL.CLS_Session.formkey.Contains("E115"))
                    {
                        Sto.Whouses dp = new Sto.Whouses();
                        dp.MdiParent = MdiParent;
                        dp.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E116")
                {
                    if (BL.CLS_Session.formkey.Contains("E116"))
                    {
                        Sto.Taxs f6 = new Sto.Taxs();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E117")
                {
                    if (BL.CLS_Session.formkey.Contains("E117"))
                    {
                        Sto.Import_From_Xls st = new Sto.Import_From_Xls();
                        st.MdiParent = MdiParent;
                        st.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E11A")
                {
                    if (BL.CLS_Session.formkey.Contains("E11A"))
                    {
                        Sto.Price_Chang rs = new Sto.Price_Chang();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E121")
                {
                    if (BL.CLS_Session.formkey.Contains("E121"))
                    {
                        Sto.Sto_ImpExp rr = new Sto.Sto_ImpExp("", "", "");
                        rr.MdiParent = MdiParent;
                        rr.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E122")
                {
                    if (BL.CLS_Session.formkey.Contains("E122"))
                    {
                        Sto.Sto_qty_Convert dp = new Sto.Sto_qty_Convert("", "", "");
                        dp.MdiParent = MdiParent;
                        dp.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E125")
                {
                    if (BL.CLS_Session.formkey.Contains("E125"))
                    {
                        Sto.Sto_Cost_Tran rr = new Sto.Sto_Cost_Tran("", "", "");
                        rr.MdiParent = MdiParent;
                        rr.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E123")
                {
                    if (BL.CLS_Session.formkey.Contains("E123"))
                    {
                        Sto.Sto_item_Combine sto = new Sto.Sto_item_Combine("", "", "");
                        sto.MdiParent = MdiParent;
                        sto.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E124")
                {
                    if (BL.CLS_Session.formkey.Contains("E124"))
                    {
                        Sto.Sto_ImpExp_Br rr = new Sto.Sto_ImpExp_Br("", "", "");
                        rr.MdiParent = MdiParent;
                        rr.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E126")
                {
                    if (BL.CLS_Session.formkey.Contains("E126"))
                    {
                        Sto.Sto_InOut rr = new Sto.Sto_InOut("", "", "");
                        rr.MdiParent = MdiParent;
                        rr.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E131")
                {
                    if (BL.CLS_Session.formkey.Contains("E131"))
                    {
                        Sto.Sto_Report rs = new Sto.Sto_Report();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E13G")
                {
                    if (BL.CLS_Session.formkey.Contains("E13G"))
                    {
                        Sto.Sto_Report_Dtl f6 = new Sto.Sto_Report_Dtl();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E13A")
                {
                    if (BL.CLS_Session.formkey.Contains("E13A"))
                    {
                        Sto.Sto_Report_Br rs = new Sto.Sto_Report_Br();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E13H")
                {
                    if (BL.CLS_Session.formkey.Contains("E13H"))
                    {
                        Sto.Sto_Report_Br_Dtl f6 = new Sto.Sto_Report_Br_Dtl();
                        f6.MdiParent = MdiParent;
                        f6.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E133")
                {
                    if (BL.CLS_Session.formkey.Contains("E133"))
                    {
                        Sto.Sto_ItemStmt_Exp rs = new Sto.Sto_ItemStmt_Exp("");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E135")
                {
                    if (BL.CLS_Session.formkey.Contains("E135"))
                    {
                        Sto.Sto_ItemStmt_All rs = new Sto.Sto_ItemStmt_All("");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E132")
                {
                    if (BL.CLS_Session.formkey.Contains("E132"))
                    {
                        //Sto.Sto_Whbins_New fr = new Sto.Sto_Whbins_New();
                        Sto.Sto_Whbins_Org fr = new Sto.Sto_Whbins_Org();
                        fr.MdiParent = MdiParent;
                        fr.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E13D")
                {
                    if (BL.CLS_Session.formkey.Contains("E13D"))
                    {
                        Sto.Sto_Whbins_Wh rs = new Sto.Sto_Whbins_Wh();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E13E")
                {
                    if (BL.CLS_Session.formkey.Contains("E13E"))
                    {
                        Sto.Sto_Whbins_Brb rs = new Sto.Sto_Whbins_Brb();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E13J")
                {
                    if (BL.CLS_Session.formkey.Contains("E13J"))
                    {
                        Sto.Sto_Report_Convrt rs = new Sto.Sto_Report_Convrt();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E13M")
                {
                    if (BL.CLS_Session.formkey.Contains("E13M"))
                    {
                        Sto.Items_Rivew fr = new Sto.Items_Rivew();
                        fr.MdiParent = MdiParent;
                        fr.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E13I")
                {
                    if (BL.CLS_Session.formkey.Contains("E13I"))
                    {
                        Sto.FRM_PRODUCTS fr = new Sto.FRM_PRODUCTS();
                        fr.MdiParent = MdiParent;
                        fr.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E134")
                {
                    if (BL.CLS_Session.formkey.Contains("E134"))
                    {
                        Sto.Sto_Whbins_o fr = new Sto.Sto_Whbins_o();
                        fr.MdiParent = MdiParent;
                        fr.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E13B")
                {
                    if (BL.CLS_Session.formkey.Contains("E13B"))
                    {
                        Sto.Sto_Whbins_Gr rs = new Sto.Sto_Whbins_Gr();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E13C")
                {
                    if (BL.CLS_Session.formkey.Contains("E13C"))
                    {
                        Sto.Sto_Whbins_Br rs = new Sto.Sto_Whbins_Br();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E13F")
                {
                    if (BL.CLS_Session.formkey.Contains("E13F"))
                    {
                        Sto.Sto_Whbins_RM fr = new Sto.Sto_Whbins_RM();
                        fr.MdiParent = MdiParent;
                        fr.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E141")
                {
                    if (BL.CLS_Session.formkey.Contains("E141"))
                    {
                        Sto.GRD_Start rs = new Sto.GRD_Start();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E142")
                {
                    if (BL.CLS_Session.formkey.Contains("E142"))
                    {
                        Sto.GRD_Enter rs = new Sto.GRD_Enter();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E143")
                {
                    if (BL.CLS_Session.formkey.Contains("E143"))
                    {
                        Sto.GRD_frm_file rs = new Sto.GRD_frm_file();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E144")
                {
                    if (BL.CLS_Session.formkey.Contains("E144"))
                    {
                        Sto.GRD_Result_Print rs = new Sto.GRD_Result_Print();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "E145")
                {
                    if (BL.CLS_Session.formkey.Contains("E145"))
                    {
                        Sto.GRD_End rs = new Sto.GRD_End();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "M181")
                {
                    if (BL.CLS_Session.formkey.Contains("M181"))
                    {
                        Post_All rs = new Post_All("acc");
                        rs.MdiParent = MdiParent;
                        rs.Text = rs.Text + " " + "الحسابات";
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "M182")
                {
                    if (BL.CLS_Session.formkey.Contains("M182"))
                    {
                        Post_All rs = new Post_All("sal");
                        rs.MdiParent = MdiParent;
                        rs.Text = rs.Text + " " + "المبيعات";
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "M183")
                {
                    if (BL.CLS_Session.formkey.Contains("M183"))
                    {
                        Post_All rs = new Post_All("pu");
                        rs.MdiParent = MdiParent;
                        rs.Text = rs.Text + " " + "المشتروات";
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "M184")
                {
                    if (BL.CLS_Session.formkey.Contains("M184"))
                    {
                        Post_All rs = new Post_All("sto");
                        rs.MdiParent = MdiParent;
                        rs.Text = rs.Text + " " + "المخازن";
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "M111")
                {
                    if (BL.CLS_Session.formkey.Contains("M111"))
                    {
                        UnPost_All rs = new UnPost_All("acc");
                        rs.MdiParent = MdiParent;
                        rs.Text = rs.Text + " " + "حسابات";
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "M112")
                {
                    if (BL.CLS_Session.formkey.Contains("M112"))
                    {
                        UnPost_All rs = new UnPost_All("sal");
                        rs.MdiParent = MdiParent;
                        rs.Text = rs.Text + " " + "مبيعات";
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "M113")
                {
                    if (BL.CLS_Session.formkey.Contains("M113"))
                    {
                        UnPost_All rs = new UnPost_All("pu");
                        rs.MdiParent = MdiParent;
                        rs.Text = rs.Text + " " + "مشتروات";
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "M114")
                {
                    if (BL.CLS_Session.formkey.Contains("M114"))
                    {
                        UnPost_All rs = new UnPost_All("sto");
                        rs.MdiParent = MdiParent;
                        rs.Text = rs.Text + " " + "مخازن";
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "M121")
                {
                    if (BL.CLS_Session.formkey.Contains("M121"))
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(@"TV.exe");
                        }
                        catch { }
                    }
                }
                if (treeView1.SelectedNode.Name == "M122")
                {
                    if (BL.CLS_Session.formkey.Contains("M122"))
                    {
                        try
                        {
                            //System.Diagnostics.Process.Start(@"AD.exe");
                            if (File.Exists(@"C:\Program Files (x86)\AnyDesk\AnyDesk.exe"))
                                System.Diagnostics.Process.Start(@"C:\Program Files (x86)\AnyDesk\AnyDesk.exe");
                            else
                                System.Diagnostics.Process.Start(@"AD.exe");
                        }
                        catch { }
                    }
                }
                if (treeView1.SelectedNode.Name == "M123")
                {
                    if (BL.CLS_Session.formkey.Contains("M123"))
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(@"AA.exe");
                        }
                        catch { }
                    }
                }
                if (treeView1.SelectedNode.Name == "M130")
                {
                    if (BL.CLS_Session.formkey.Contains("M130"))
                    {
                        Backup bk = new Backup();
                        bk.MdiParent = MdiParent;
                        bk.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "M140")
                {
                    if (BL.CLS_Session.formkey.Contains("M140"))
                    {
                        ManagmentQuery mgq = new ManagmentQuery();
                        mgq.MdiParent = MdiParent;
                        mgq.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "M150")
                {
                    if (BL.CLS_Session.formkey.Contains("M150"))
                    {
                        Server_Setting mgq = new Server_Setting();
                        mgq.MdiParent = MdiParent;
                        mgq.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "M160")
                {
                    if (BL.CLS_Session.formkey.Contains("M160"))
                    {
                        Sto.Items_Bld iu = new Sto.Items_Bld();
                        iu.MdiParent = MdiParent;
                        iu.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "M170")
                {
                    if (BL.CLS_Session.formkey.Contains("M170"))
                    {
                        Acc.Acc_Copy_Olddb rs = new Acc.Acc_Copy_Olddb();
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "M191")
                {
                    if (BL.CLS_Session.formkey.Contains("M191"))
                    {
                        Open_New_Yr bk = new Open_New_Yr();
                        bk.MdiParent = MdiParent;
                        bk.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "C115")
                {
                    if (BL.CLS_Session.formkey.Contains("C115"))
                    {
                        Cus.Import_SC_From_Xls rs = new Cus.Import_SC_From_Xls("Cust");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "D113")
                {
                    if (BL.CLS_Session.formkey.Contains("D113"))
                    {
                        Cus.Import_SC_From_Xls rs = new Cus.Import_SC_From_Xls("Sup");
                        rs.MdiParent = MdiParent;
                        rs.Show();
                    }
                }
                if (treeView1.SelectedNode.Name == "M100")
                {
                    if (BL.CLS_Session.formkey.Contains("M100"))
                    {
                        Process.Start(@".\App_Updater.exe");
                    }
                }
                if (treeView1.SelectedNode.Name == "M102")
                {
                    if (BL.CLS_Session.formkey.Contains("M102"))
                    {
                        About ab = new About();
                        ab.MdiParent = MdiParent;
                        ab.Show();
                    }
                }

            }
            catch { }

        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
                treeView1_DoubleClick(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {   
        }
    }
}
