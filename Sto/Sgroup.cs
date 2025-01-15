using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace POS.Sto
{
    public partial class Sgroup : BaseForm
    {
        BL.DAML daml=new BL.DAML();
        SqlConnection objsqlconnection = BL.DAML.con;
        SqlCommand objsqlcommand = null;
      //  SqlCommand objsqlcommand2 = null;
        SqlDataAdapter objsqladapter = null;
       // SqlDataAdapter objsqladapter2 = null;
        DataSet objdataset = null;
        bool isnew, isupdate;
        TreeNode selectedNode = null;

        public Sgroup()
        {
            InitializeComponent();
            objdataset = new DataSet();
        }

        private void Sgroup_Load(object sender, EventArgs e)
        {
            //selectedNode = GetNode(treeView1.SelectedNode.Tag);
            /*
            objsqlcommand = new SqlCommand("SELECT group_id, group_name, ISNULL(group_pid, 0) AS group_pid FROM groups", objsqlconnection);
            objsqladapter = new SqlDataAdapter(objsqlcommand);
            objsqladapter.Fill(objdataset);
          //  MessageBox.Show(objdataset.Tables[0].Rows[0][1].ToString());
            CreateTreeView(treeView1.Nodes, Convert.ToString("0"), objdataset.Tables[0]);
          //  treeView1.BeginUpdate();
            */
            DataTable dtsg = new DataTable();
            dtsg=daml.SELECT_QUIRY_only_retrn_dt("SELECT group_id, group_name, ISNULL(group_pid, 0) AS group_pid FROM groups");
           // objsqladapter = new SqlDataAdapter(objsqlcommand);
           // objsqladapter.Fill(objdataset);
            //  MessageBox.Show(objdataset.Tables[0].Rows[0][1].ToString());
            CreateTreeView(treeView1.Nodes, Convert.ToString("0"), dtsg);
            //  treeView1.BeginUpdate();
        }

        protected void CreateTreeView(TreeNodeCollection parentNode, string parentID, DataTable mytab)
        {
            foreach (DataRow dta in mytab.Rows)
            {
                if (Convert.ToString(dta["group_pid"]) == parentID)
                {
                    String key = dta["group_id"].ToString();
                    String text = dta["group_id"].ToString() +" - " + dta["group_name"].ToString();
                    TreeNodeCollection newParentNode = parentNode.Add(key, text).Nodes;
                    CreateTreeView(newParentNode, Convert.ToString(dta["group_id"]), mytab);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            if (isnew)
            {
                if (string.IsNullOrEmpty(txt_nm.Text.Trim()) || string.IsNullOrEmpty(txt_sg.Text.Trim()))
                {
                    MessageBox.Show("يرجى اكمال المعلومات", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_nm.Focus();
                    return;
                }
                objsqlconnection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO groups (group_id, group_name, group_pid,group_desc) VALUES(@a1,@a2,@a3,@a4)", objsqlconnection))
                {


                    cmd.Parameters.AddWithValue("@a1", txt_mg.Text + txt_sg.Text);
                    cmd.Parameters.AddWithValue("@a2", txt_nm.Text);
                    cmd.Parameters.AddWithValue("@a3", txt_mg.Text);
                    cmd.Parameters.AddWithValue("@a4", txt_dsc.Text);

                    cmd.ExecuteNonQuery();
                    objsqlconnection.Close();
                }

                txt_sg.Enabled = false;
                txt_nm.Enabled = false;
                txt_dsc.Enabled = false;
                button1.Enabled = false;

                TreeNode tn = new TreeNode();
                tn.Text = txt_mg.Text + txt_sg.Text;
                tn.Name = txt_mg.Text + txt_sg.Text;
                tn.Tag = txt_mg.Text + txt_sg.Text;

                /*
                TreeNode tn = new TreeNode();
                tn.Text = txt_mg.Text + txt_sg.Text + " - " + txt_nm.Text;
                tn.Name = txt_mg.Text + txt_sg.Text + " - " + txt_nm.Text;
                tn.Tag  = txt_mg.Text + txt_sg.Text;

               // if (thisLevel)
               // {
                //    if (selectedNode.Parent != null)
                //        selectedNode.Parent.Nodes.Add(tn);
                //    else
                    //    treeView1.Nodes.Add(tn);
               // }
               // else
                  //  selectedNode.Nodes.Add(tn);
                   //     treeView1.SelectedNode = tn;
                if (selectedNode.Parent != null)
                    selectedNode.Parent.Nodes.Add(tn);
                else
                    treeView1.Nodes.Add(tn);
                */
                // treeView1.Nodes.Clear();

                treeView1.Nodes.Clear();
                Sgroup_Load(sender, e);
                treeView1.SelectedNode = tn;

                // treeView1.Refresh();
            }
            else
            {
                if (string.IsNullOrEmpty(txt_nm.Text.Trim()))
                {
                    MessageBox.Show("يرجى اكمال المعلومات", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_nm.Focus();
                    return;
                }
                objsqlconnection.Open();
                using (SqlCommand cmd = new SqlCommand("update groups set group_name=@a1,group_desc=@a2 where group_id=@a3", objsqlconnection))
                {


                   // cmd.Parameters.AddWithValue("@a1", txt_mg.Text + txt_sg.Text);
                    cmd.Parameters.AddWithValue("@a1", txt_nm.Text);
                    cmd.Parameters.AddWithValue("@a2", txt_dsc.Text);
                    cmd.Parameters.AddWithValue("@a3", txt_mg.Text);

                    cmd.ExecuteNonQuery();
                    objsqlconnection.Close();
                }

                txt_sg.Enabled = false;
                txt_nm.Enabled = false;
                txt_dsc.Enabled = false;
                button1.Enabled = false;

                TreeNode tn = new TreeNode();
                tn.Text = txt_mg.Text + txt_sg.Text;
                tn.Name = txt_mg.Text + txt_sg.Text;
                tn.Tag = txt_mg.Text + txt_sg.Text;

                /*
                TreeNode tn = new TreeNode();
                tn.Text = txt_mg.Text + txt_sg.Text + " - " + txt_nm.Text;
                tn.Name = txt_mg.Text + txt_sg.Text + " - " + txt_nm.Text;
                tn.Tag  = txt_mg.Text + txt_sg.Text;

               // if (thisLevel)
               // {
                //    if (selectedNode.Parent != null)
                //        selectedNode.Parent.Nodes.Add(tn);
                //    else
                    //    treeView1.Nodes.Add(tn);
               // }
               // else
                  //  selectedNode.Nodes.Add(tn);
                   //     treeView1.SelectedNode = tn;
                if (selectedNode.Parent != null)
                    selectedNode.Parent.Nodes.Add(tn);
                else
                    treeView1.Nodes.Add(tn);
                */
                // treeView1.Nodes.Clear();

                treeView1.Nodes.Clear();
                Sgroup_Load(sender, e);
                treeView1.SelectedNode = tn;
            
            }
        }

        private void underSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select  isnull(max(cast(substring(group_id,3,2) as int))+1,1) from groups where group_pid='" + treeView1.SelectedNode.Text.Substring(0, treeView1.SelectedNode.Text.IndexOf("-") - 1) + "'");
             isnew=true;
             isupdate=false;
           // selectedNode = treeView1.SelectedNode;//.Substring(0, treeView1.SelectedNode.Text.IndexOf("-") - 1);
          //  selectedNode = GetNode(treeView1.SelectedNode.Tag);
            selectedNode = treeView1.SelectedNode;
            if (treeView1.SelectedNode.Level == 0)
            {
                txt_mg.Text = treeView1.SelectedNode.Text.Substring(0, treeView1.SelectedNode.Text.IndexOf("-")-1);
                txt_sg.Text = dt.Rows[0][0].ToString().Length < 2 ? "0" + dt.Rows[0][0].ToString() : dt.Rows[0][0].ToString();
                txt_nm.Text = "";
                txt_dsc.Text = "";
                txt_sg.Enabled = true;
                txt_nm.Enabled = true;
                txt_dsc.Enabled = true;
                button1.Enabled = true;
                txt_nm.Focus();

            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void تعديلالاسمToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isnew = false;
            isupdate = true;
            if (treeView1.SelectedNode.Level == 0)
            {
                MessageBox.Show("لا تستطيع تعديل مجموعة رئيسية من هذه الشاشة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                txt_mg.Text = treeView1.SelectedNode.Text.Substring(0, treeView1.SelectedNode.Text.IndexOf("-") - 1);
                txt_nm.Text = treeView1.SelectedNode.Text.Substring(treeView1.SelectedNode.Text.IndexOf("-") + 2, (treeView1.SelectedNode.Text.Length - (treeView1.SelectedNode.Text.IndexOf("-") + 2)));
                txt_nm.Enabled = true;
                txt_dsc.Enabled = true;
                button1.Enabled = true;

                //DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select top 1 * from items where sgroup='" + treeView1.SelectedNode.Text.Substring(0, treeView1.SelectedNode.Text.IndexOf("-") - 1) + "'");
                //if (dt.Rows.Count > 0)
                //{
                //    MessageBox.Show("لا تستطيع حذف مجموعة فرعية لديها اصناف مرتبطة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //else
                //{
                  //  daml.Exec_Query_only("delete from groups where group_id='" + treeView1.SelectedNode.Text.Substring(0, treeView1.SelectedNode.Text.IndexOf("-") - 1) + "'");

                  //  MessageBox.Show("تم التعديل", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
               // }  
            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Level == 0)
            {
                MessageBox.Show("لا تستطيع حذف مجموعة رئيسية من هذه الشاشة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select top 1 * from items where sgroup='" + treeView1.SelectedNode.Text.Substring(0, treeView1.SelectedNode.Text.IndexOf("-") - 1) + "'");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("لا تستطيع حذف مجموعة فرعية لديها اصناف مرتبطة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    daml.Exec_Query_only("delete from groups where group_id='" + treeView1.SelectedNode.Text.Substring(0, treeView1.SelectedNode.Text.IndexOf("-") - 1) + "'");

                    MessageBox.Show("تم الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    treeView1.Nodes.Clear();
                    Sgroup_Load(sender, e);
                }
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView1.SelectedNode = e.Node;
        }
    }
}
