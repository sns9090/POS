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
    public partial class TreeTest : BaseForm
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        DataTable acountsTb = null;
        public TreeTest()
        {
            InitializeComponent();
            acountsTb = new DataTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //////DataTable dt = new DataTable();
            //////SqlDataAdapter da = new SqlDataAdapter("select * from users", con);
            //////da.Fill(dt);

            //////// treeView1.Nodes.Add("users");

            //////foreach (DataRow dr in dt.Rows)
            //////{
            //////    TreeNode nod = new TreeNode(dr["user_id"].ToString());
            //////    nod.Nodes.Add(dr["username"].ToString());
            //////    int sss = Convert.ToInt32(dr["user_id"]);
            //////    // treeView1.Nodes.Add(nod);

            //////   // PopulateTreeView(treeView1.Nodes, sss, dt);
            //////}
            //DataTable acountsTb = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from users", con);
            da.Fill(acountsTb);

            PopulateTreeView(1, null);


        }
        //protected void PopulateTreeView(TreeNodeCollection parentNode, int parentID, DataTable folders)
        //{
        //    foreach (DataRow folder in folders.Rows)
        //    {
        //        if (Convert.ToInt32(folder["user_id"]) == parentID)
        //        {
        //            String key = folder["username"].ToString();
        //            String text = folder["password"].ToString();
        //            TreeNodeCollection newParentNode = parentNode.Add(key, text).Nodes;
        //            PopulateTreeView(newParentNode, Convert.ToInt32(folder["username"]), folders);
        //        }
        //    }
        //}

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            

            TreeNode childNode;

            foreach (DataRow dr in acountsTb.Select("[password]=" + parentId))
            {
                TreeNode t = new TreeNode();
                t.Text = dr["user_id"].ToString() + " - " + dr["username"].ToString();
                t.Name = dr["user_id"].ToString();
                t.Tag = acountsTb.Rows.IndexOf(dr);
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
                PopulateTreeView(Convert.ToInt32(dr["user_id"].ToString()), childNode);
            }
        }

        private void TreeTest_Load(object sender, EventArgs e)
        {
            
           

            
        }
    }
}
