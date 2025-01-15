namespace POS.Sto
{
    partial class Sgroup
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sgroup));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.underSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.تعديلالاسمToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.حذفToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_dsc = new System.Windows.Forms.TextBox();
            this.txt_nm = new System.Windows.Forms.TextBox();
            this.txt_sg = new System.Windows.Forms.TextBox();
            this.txt_mg = new System.Windows.Forms.TextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.underSelectedToolStripMenuItem,
            this.تعديلالاسمToolStripMenuItem,
            this.حذفToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // underSelectedToolStripMenuItem
            // 
            resources.ApplyResources(this.underSelectedToolStripMenuItem, "underSelectedToolStripMenuItem");
            this.underSelectedToolStripMenuItem.Image = global::POS.Properties.Resources.Add_32x32;
            this.underSelectedToolStripMenuItem.Name = "underSelectedToolStripMenuItem";
            this.underSelectedToolStripMenuItem.Click += new System.EventHandler(this.underSelectedToolStripMenuItem_Click);
            // 
            // تعديلالاسمToolStripMenuItem
            // 
            resources.ApplyResources(this.تعديلالاسمToolStripMenuItem, "تعديلالاسمToolStripMenuItem");
            this.تعديلالاسمToolStripMenuItem.Image = global::POS.Properties.Resources.Edit_32x32;
            this.تعديلالاسمToolStripMenuItem.Name = "تعديلالاسمToolStripMenuItem";
            this.تعديلالاسمToolStripMenuItem.Click += new System.EventHandler(this.تعديلالاسمToolStripMenuItem_Click);
            // 
            // حذفToolStripMenuItem
            // 
            resources.ApplyResources(this.حذفToolStripMenuItem, "حذفToolStripMenuItem");
            this.حذفToolStripMenuItem.Image = global::POS.Properties.Resources.Delete_32x32;
            this.حذفToolStripMenuItem.Name = "حذفToolStripMenuItem";
            this.حذفToolStripMenuItem.Click += new System.EventHandler(this.حذفToolStripMenuItem_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txt_dsc
            // 
            resources.ApplyResources(this.txt_dsc, "txt_dsc");
            this.txt_dsc.Name = "txt_dsc";
            // 
            // txt_nm
            // 
            resources.ApplyResources(this.txt_nm, "txt_nm");
            this.txt_nm.Name = "txt_nm";
            // 
            // txt_sg
            // 
            resources.ApplyResources(this.txt_sg, "txt_sg");
            this.txt_sg.Name = "txt_sg";
            // 
            // txt_mg
            // 
            resources.ApplyResources(this.txt_mg, "txt_mg");
            this.txt_mg.Name = "txt_mg";
            // 
            // treeView1
            // 
            resources.ApplyResources(this.treeView1, "treeView1");
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.ItemHeight = 23;
            this.treeView1.Name = "treeView1";
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // btn_Exit
            // 
            resources.ApplyResources(this.btn_Exit, "btn_Exit");
            this.btn_Exit.Image = global::POS.Properties.Resources.Log_Out_32x32;
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Image = global::POS.Properties.Resources.Save_32x32;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Sgroup
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_dsc);
            this.Controls.Add(this.txt_nm);
            this.Controls.Add(this.txt_sg);
            this.Controls.Add(this.txt_mg);
            this.Controls.Add(this.treeView1);
            this.Name = "Sgroup";
            this.Load += new System.EventHandler(this.Sgroup_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem underSelectedToolStripMenuItem;
        private System.Windows.Forms.TextBox txt_mg;
        private System.Windows.Forms.TextBox txt_sg;
        private System.Windows.Forms.TextBox txt_nm;
        private System.Windows.Forms.TextBox txt_dsc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.ToolStripMenuItem تعديلالاسمToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem حذفToolStripMenuItem;
    }
}