namespace POS.Acc
{
    partial class Accounts
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.atThisLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.underSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.isActive = new System.Windows.Forms.CheckBox();
            this.lblDebitCredit = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioNA2 = new System.Windows.Forms.RadioButton();
            this.radioIndirect = new System.Windows.Forms.RadioButton();
            this.radioDirect = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioNA1 = new System.Windows.Forms.RadioButton();
            this.radioVariable = new System.Windows.Forms.RadioButton();
            this.radioFixed = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioTransaction = new System.Windows.Forms.RadioButton();
            this.radioParent = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Location = new System.Drawing.Point(14, 15);
            this.treeView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(264, 361);
            this.treeView1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deletAccountToolStripMenuItem,
            this.toolStripSeparator1,
            this.openNewToolStripMenuItem,
            this.toolStripSeparator2,
            this.atThisLevelToolStripMenuItem,
            this.underSelectedToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 148);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.viewToolStripMenuItem.Text = "View Account";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.editToolStripMenuItem.Text = "Edit Account";
            // 
            // deletAccountToolStripMenuItem
            // 
            this.deletAccountToolStripMenuItem.Name = "deletAccountToolStripMenuItem";
            this.deletAccountToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.deletAccountToolStripMenuItem.Text = "Delet Account";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // openNewToolStripMenuItem
            // 
            this.openNewToolStripMenuItem.Enabled = false;
            this.openNewToolStripMenuItem.Name = "openNewToolStripMenuItem";
            this.openNewToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.openNewToolStripMenuItem.Text = "New Account";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(150, 6);
            // 
            // atThisLevelToolStripMenuItem
            // 
            this.atThisLevelToolStripMenuItem.Name = "atThisLevelToolStripMenuItem";
            this.atThisLevelToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.atThisLevelToolStripMenuItem.Text = "At this level";
            this.atThisLevelToolStripMenuItem.Click += new System.EventHandler(this.atThisLevelToolStripMenuItem_Click);
            // 
            // underSelectedToolStripMenuItem
            // 
            this.underSelectedToolStripMenuItem.Name = "underSelectedToolStripMenuItem";
            this.underSelectedToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.underSelectedToolStripMenuItem.Text = "Under Selected";
            this.underSelectedToolStripMenuItem.Click += new System.EventHandler(this.underSelectedToolStripMenuItem_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd-MM-yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(450, 249);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(116, 24);
            this.dtpDate.TabIndex = 185;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(377, 256);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 17);
            this.label12.TabIndex = 191;
            this.label12.Text = "Date:";
            // 
            // isActive
            // 
            this.isActive.AutoSize = true;
            this.isActive.Location = new System.Drawing.Point(640, 283);
            this.isActive.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.isActive.Name = "isActive";
            this.isActive.Size = new System.Drawing.Size(64, 21);
            this.isActive.TabIndex = 186;
            this.isActive.Text = "Active";
            this.isActive.UseVisualStyleBackColor = true;
            // 
            // lblDebitCredit
            // 
            this.lblDebitCredit.AutoSize = true;
            this.lblDebitCredit.Location = new System.Drawing.Point(586, 286);
            this.lblDebitCredit.Name = "lblDebitCredit";
            this.lblDebitCredit.Size = new System.Drawing.Size(0, 17);
            this.lblDebitCredit.TabIndex = 189;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(308, 284);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 17);
            this.label4.TabIndex = 187;
            this.label4.Text = "Opening Balance:";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(586, 351);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(87, 28);
            this.cmdCancel.TabIndex = 190;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(450, 351);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(87, 28);
            this.cmdSave.TabIndex = 188;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(450, 53);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(291, 24);
            this.txtName.TabIndex = 178;
            // 
            // txtCode
            // 
            this.txtCode.Enabled = false;
            this.txtCode.Location = new System.Drawing.Point(450, 18);
            this.txtCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(164, 24);
            this.txtCode.TabIndex = 177;
            this.txtCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radioNA2);
            this.panel3.Controls.Add(this.radioIndirect);
            this.panel3.Controls.Add(this.radioDirect);
            this.panel3.Location = new System.Drawing.Point(450, 190);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(292, 43);
            this.panel3.TabIndex = 181;
            // 
            // radioNA2
            // 
            this.radioNA2.AutoSize = true;
            this.radioNA2.Location = new System.Drawing.Point(192, 10);
            this.radioNA2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioNA2.Name = "radioNA2";
            this.radioNA2.Size = new System.Drawing.Size(48, 21);
            this.radioNA2.TabIndex = 4;
            this.radioNA2.TabStop = true;
            this.radioNA2.Text = "N/A";
            this.radioNA2.UseVisualStyleBackColor = true;
            // 
            // radioIndirect
            // 
            this.radioIndirect.AutoSize = true;
            this.radioIndirect.Location = new System.Drawing.Point(104, 10);
            this.radioIndirect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioIndirect.Name = "radioIndirect";
            this.radioIndirect.Size = new System.Drawing.Size(72, 21);
            this.radioIndirect.TabIndex = 1;
            this.radioIndirect.Text = "Indirect";
            this.radioIndirect.UseVisualStyleBackColor = true;
            // 
            // radioDirect
            // 
            this.radioDirect.AutoSize = true;
            this.radioDirect.Checked = true;
            this.radioDirect.Location = new System.Drawing.Point(3, 10);
            this.radioDirect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioDirect.Name = "radioDirect";
            this.radioDirect.Size = new System.Drawing.Size(62, 21);
            this.radioDirect.TabIndex = 0;
            this.radioDirect.TabStop = true;
            this.radioDirect.Text = "Direct";
            this.radioDirect.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioNA1);
            this.panel2.Controls.Add(this.radioVariable);
            this.panel2.Controls.Add(this.radioFixed);
            this.panel2.Location = new System.Drawing.Point(450, 139);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(292, 43);
            this.panel2.TabIndex = 180;
            // 
            // radioNA1
            // 
            this.radioNA1.AutoSize = true;
            this.radioNA1.Location = new System.Drawing.Point(192, 10);
            this.radioNA1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioNA1.Name = "radioNA1";
            this.radioNA1.Size = new System.Drawing.Size(48, 21);
            this.radioNA1.TabIndex = 2;
            this.radioNA1.TabStop = true;
            this.radioNA1.Text = "N/A";
            this.radioNA1.UseVisualStyleBackColor = true;
            // 
            // radioVariable
            // 
            this.radioVariable.AutoSize = true;
            this.radioVariable.Location = new System.Drawing.Point(104, 10);
            this.radioVariable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioVariable.Name = "radioVariable";
            this.radioVariable.Size = new System.Drawing.Size(72, 21);
            this.radioVariable.TabIndex = 1;
            this.radioVariable.Text = "Variable";
            this.radioVariable.UseVisualStyleBackColor = true;
            // 
            // radioFixed
            // 
            this.radioFixed.AutoSize = true;
            this.radioFixed.Checked = true;
            this.radioFixed.Location = new System.Drawing.Point(3, 10);
            this.radioFixed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioFixed.Name = "radioFixed";
            this.radioFixed.Size = new System.Drawing.Size(58, 21);
            this.radioFixed.TabIndex = 0;
            this.radioFixed.TabStop = true;
            this.radioFixed.Text = "Fixed";
            this.radioFixed.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioTransaction);
            this.panel1.Controls.Add(this.radioParent);
            this.panel1.Location = new System.Drawing.Point(450, 89);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 43);
            this.panel1.TabIndex = 179;
            // 
            // radioTransaction
            // 
            this.radioTransaction.AutoSize = true;
            this.radioTransaction.Location = new System.Drawing.Point(139, 10);
            this.radioTransaction.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioTransaction.Name = "radioTransaction";
            this.radioTransaction.Size = new System.Drawing.Size(152, 21);
            this.radioTransaction.TabIndex = 1;
            this.radioTransaction.Text = "Transaction Account";
            this.radioTransaction.UseVisualStyleBackColor = true;
            // 
            // radioParent
            // 
            this.radioParent.AutoSize = true;
            this.radioParent.Checked = true;
            this.radioParent.Location = new System.Drawing.Point(3, 10);
            this.radioParent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioParent.Name = "radioParent";
            this.radioParent.Size = new System.Drawing.Size(121, 21);
            this.radioParent.TabIndex = 0;
            this.radioParent.TabStop = true;
            this.radioParent.Text = "Parent Account";
            this.radioParent.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(376, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 182;
            this.label3.Text = "Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(371, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 183;
            this.label2.Text = "Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(371, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 184;
            this.label1.Text = "Code: ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(450, 281);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(116, 24);
            this.textBox1.TabIndex = 192;
            this.textBox1.Text = "0.00";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Accounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 444);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.isActive);
            this.Controls.Add(this.lblDebitCredit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView1);
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Accounts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Populating TreeView from Database";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem openNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem atThisLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem underSelectedToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox isActive;
        private System.Windows.Forms.Label lblDebitCredit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioNA2;
        private System.Windows.Forms.RadioButton radioIndirect;
        private System.Windows.Forms.RadioButton radioDirect;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioNA1;
        private System.Windows.Forms.RadioButton radioVariable;
        private System.Windows.Forms.RadioButton radioFixed;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioTransaction;
        private System.Windows.Forms.RadioButton radioParent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

