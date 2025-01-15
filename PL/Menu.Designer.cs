namespace POS
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("الاصناف");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("الوحدات");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("المخازن");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("المستخدمين");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("الشركة والفترات");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("ملفات التعريف", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("المبيعات");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("المرتجعات");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("حركات", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("التقرير اليومي");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("التقرير الشهري");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("تقارير", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(9, 26);
            this.treeView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node01";
            treeNode1.Text = "الاصناف";
            treeNode2.Name = "Node04";
            treeNode2.Text = "الوحدات";
            treeNode3.Name = "Node02";
            treeNode3.Text = "المخازن";
            treeNode4.Name = "Node30";
            treeNode4.Text = "المستخدمين";
            treeNode5.Name = "Node31";
            treeNode5.Text = "الشركة والفترات";
            treeNode6.Name = "Node0";
            treeNode6.Text = "ملفات التعريف";
            treeNode7.Name = "Node10";
            treeNode7.Text = "المبيعات";
            treeNode8.Name = "Node11";
            treeNode8.Text = "المرتجعات";
            treeNode9.Name = "Node1";
            treeNode9.Text = "حركات";
            treeNode10.Name = "Node30";
            treeNode10.Text = "التقرير اليومي";
            treeNode11.Name = "Node31";
            treeNode11.Text = "التقرير الشهري";
            treeNode12.Name = "Node2";
            treeNode12.Text = "تقارير";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode9,
            treeNode12});
            this.treeView1.RightToLeftLayout = true;
            this.treeView1.Size = new System.Drawing.Size(310, 418);
            this.treeView1.TabIndex = 0;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            this.treeView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeView1_KeyPress);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(22, 2);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(16, 22);
            this.button1.TabIndex = 1;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(40, 2);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(16, 22);
            this.button2.TabIndex = 2;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 469);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView1);
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Menu";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "القائمة";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}