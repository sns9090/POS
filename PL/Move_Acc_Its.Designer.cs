namespace POS.PL
{
    partial class Move_Acc_Its
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Move_Acc_Its));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rd_dm = new System.Windows.Forms.RadioButton();
            this.rd_d = new System.Windows.Forms.RadioButton();
            this.rd_m = new System.Windows.Forms.RadioButton();
            this.txt_newname = new System.Windows.Forms.TextBox();
            this.txt_newcode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_oldname = new System.Windows.Forms.TextBox();
            this.txt_oldcode = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rad_its = new System.Windows.Forms.RadioButton();
            this.rad_sup = new System.Windows.Forms.RadioButton();
            this.rad_cus = new System.Windows.Forms.RadioButton();
            this.rad_acc = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cmb_puctr = new System.Windows.Forms.ComboBox();
            this.txt_newno = new System.Windows.Forms.TextBox();
            this.txt_oldno = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_salctr = new System.Windows.Forms.ComboBox();
            this.cmb_type = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.txt_newname);
            this.tabPage1.Controls.Add(this.txt_newcode);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.txt_oldname);
            this.tabPage1.Controls.Add(this.txt_oldcode);
            this.tabPage1.Controls.Add(this.panel1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rd_dm);
            this.panel2.Controls.Add(this.rd_d);
            this.panel2.Controls.Add(this.rd_m);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // rd_dm
            // 
            resources.ApplyResources(this.rd_dm, "rd_dm");
            this.rd_dm.Checked = true;
            this.rd_dm.Name = "rd_dm";
            this.rd_dm.TabStop = true;
            this.rd_dm.UseVisualStyleBackColor = true;
            // 
            // rd_d
            // 
            resources.ApplyResources(this.rd_d, "rd_d");
            this.rd_d.Name = "rd_d";
            this.rd_d.UseVisualStyleBackColor = true;
            // 
            // rd_m
            // 
            resources.ApplyResources(this.rd_m, "rd_m");
            this.rd_m.Name = "rd_m";
            this.rd_m.UseVisualStyleBackColor = true;
            // 
            // txt_newname
            // 
            this.txt_newname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_newname, "txt_newname");
            this.txt_newname.Name = "txt_newname";
            this.txt_newname.ReadOnly = true;
            // 
            // txt_newcode
            // 
            this.txt_newcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_newcode, "txt_newcode");
            this.txt_newcode.Name = "txt_newcode";
            this.txt_newcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_newcode_KeyDown);
            this.txt_newcode.Leave += new System.EventHandler(this.txt_newcode_Leave);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // button1
            // 
            this.button1.Image = global::POS.Properties.Resources.Refresh_32x32;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_oldname
            // 
            this.txt_oldname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_oldname, "txt_oldname");
            this.txt_oldname.Name = "txt_oldname";
            this.txt_oldname.ReadOnly = true;
            // 
            // txt_oldcode
            // 
            this.txt_oldcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_oldcode, "txt_oldcode");
            this.txt_oldcode.Name = "txt_oldcode";
            this.txt_oldcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_oldcode_KeyDown);
            this.txt_oldcode.Leave += new System.EventHandler(this.txt_oldcode_Leave);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rad_its);
            this.panel1.Controls.Add(this.rad_sup);
            this.panel1.Controls.Add(this.rad_cus);
            this.panel1.Controls.Add(this.rad_acc);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // rad_its
            // 
            resources.ApplyResources(this.rad_its, "rad_its");
            this.rad_its.Name = "rad_its";
            this.rad_its.UseVisualStyleBackColor = true;
            // 
            // rad_sup
            // 
            resources.ApplyResources(this.rad_sup, "rad_sup");
            this.rad_sup.Name = "rad_sup";
            this.rad_sup.UseVisualStyleBackColor = true;
            // 
            // rad_cus
            // 
            resources.ApplyResources(this.rad_cus, "rad_cus");
            this.rad_cus.Name = "rad_cus";
            this.rad_cus.UseVisualStyleBackColor = true;
            this.rad_cus.CheckedChanged += new System.EventHandler(this.rad_cus_CheckedChanged);
            // 
            // rad_acc
            // 
            resources.ApplyResources(this.rad_acc, "rad_acc");
            this.rad_acc.Checked = true;
            this.rad_acc.Name = "rad_acc";
            this.rad_acc.TabStop = true;
            this.rad_acc.UseVisualStyleBackColor = true;
            this.rad_acc.CheckedChanged += new System.EventHandler(this.rad_acc_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.cmb_puctr);
            this.tabPage2.Controls.Add(this.txt_newno);
            this.tabPage2.Controls.Add(this.txt_oldno);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.cmb_salctr);
            this.tabPage2.Controls.Add(this.cmb_type);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            // 
            // button2
            // 
            this.button2.Image = global::POS.Properties.Resources.Refresh_32x32;
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // cmb_puctr
            // 
            this.cmb_puctr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmb_puctr, "cmb_puctr");
            this.cmb_puctr.FormattingEnabled = true;
            this.cmb_puctr.Name = "cmb_puctr";
            // 
            // txt_newno
            // 
            resources.ApplyResources(this.txt_newno, "txt_newno");
            this.txt_newno.Name = "txt_newno";
            // 
            // txt_oldno
            // 
            resources.ApplyResources(this.txt_oldno, "txt_oldno");
            this.txt_oldno.Name = "txt_oldno";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
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
            // cmb_salctr
            // 
            this.cmb_salctr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmb_salctr, "cmb_salctr");
            this.cmb_salctr.FormattingEnabled = true;
            this.cmb_salctr.Name = "cmb_salctr";
            // 
            // cmb_type
            // 
            this.cmb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmb_type, "cmb_type");
            this.cmb_type.FormattingEnabled = true;
            this.cmb_type.Name = "cmb_type";
            this.cmb_type.SelectedIndexChanged += new System.EventHandler(this.cmb_type_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // btn_Exit
            // 
            resources.ApplyResources(this.btn_Exit, "btn_Exit");
            this.btn_Exit.Image = global::POS.Properties.Resources.Log_Out_32x32;
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // Move_Acc_Its
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_Exit);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MaximizeBox = false;
            this.Name = "Move_Acc_Its";
            this.Load += new System.EventHandler(this.Move_Acc_Its_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txt_newname;
        private System.Windows.Forms.TextBox txt_newcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_oldname;
        private System.Windows.Forms.TextBox txt_oldcode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rad_its;
        private System.Windows.Forms.RadioButton rad_sup;
        private System.Windows.Forms.RadioButton rad_cus;
        private System.Windows.Forms.RadioButton rad_acc;
        private System.Windows.Forms.TextBox txt_newno;
        private System.Windows.Forms.TextBox txt_oldno;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cmb_salctr;
        public System.Windows.Forms.ComboBox cmb_type;
        public System.Windows.Forms.ComboBox cmb_puctr;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rd_dm;
        private System.Windows.Forms.RadioButton rd_d;
        private System.Windows.Forms.RadioButton rd_m;
        private System.Windows.Forms.Button button2;
    }
}