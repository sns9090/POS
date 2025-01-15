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
    public partial class Menu : BaseForm
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            treeView1.CollapseAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == treeView1.Nodes[0].Nodes[0])
            {
                var node00 = new Sto.Item_Card("");
                node00.MdiParent = Application.OpenForms["MAIN"];

                node00.Show();
            }
            if (treeView1.SelectedNode == treeView1.Nodes[0].Nodes[1])
            {
                var node01 = new Sto.Item_Card("");
                node01.MdiParent = Application.OpenForms["MAIN"];

                node01.Show();
            }

            if (treeView1.SelectedNode == treeView1.Nodes[1].Nodes[0])
            {
                //var node10 = new Pos.SALES();
                //node10.MdiParent = Application.OpenForms["MAIN"];

                //node10.Show();
            }
            if (treeView1.SelectedNode == treeView1.Nodes[1].Nodes[1])
            {
                //var node11 = new Pos.RE_SALES();
                //node11.MdiParent = Application.OpenForms["MAIN"];

                //node11.Show();
            }
            if (treeView1.SelectedNode == treeView1.Nodes[2].Nodes[0])
            {
                var node20 = new Pos.DilySalesReport();
                node20.MdiParent = Application.OpenForms["MAIN"];

                node20.Show();
            }
            if (treeView1.SelectedNode == treeView1.Nodes[2].Nodes[1])
            {
                var node21 = new Acc.Acc_Report();
                node21.MdiParent = Application.OpenForms["MAIN"];

                node21.Show();
            }
            if (treeView1.SelectedNode == treeView1.Nodes[0].Nodes[3])
            {
                //var node20 = new USERS();
                //node20.MdiParent = Application.OpenForms["MAIN"];

                //node20.Show();
            }

            
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void treeView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            treeView1_DoubleClick(sender, e);
        }
    }
}
