namespace POS
{
    partial class LOGIN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LOGIN));
            this.chk_lang = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_rp = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_year = new System.Windows.Forms.ComboBox();
            this.cmb_company = new System.Windows.Forms.ComboBox();
            this.cmb_brno = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_user = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // chk_lang
            // 
            resources.ApplyResources(this.chk_lang, "chk_lang");
            this.chk_lang.Name = "chk_lang";
            this.chk_lang.UseVisualStyleBackColor = true;
            this.chk_lang.CheckedChanged += new System.EventHandler(this.chk_lang_CheckedChanged);
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_rp);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmb_year);
            this.groupBox1.Controls.Add(this.cmb_company);
            this.groupBox1.Controls.Add(this.cmb_brno);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmb_user);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.txt_pass);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // chk_rp
            // 
            resources.ApplyResources(this.chk_rp, "chk_rp");
            this.chk_rp.Name = "chk_rp";
            this.chk_rp.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Name = "label6";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::POS.Properties.Resources.TS_Logo_removebg1;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // cmb_year
            // 
            this.cmb_year.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmb_year, "cmb_year");
            this.cmb_year.FormattingEnabled = true;
            this.cmb_year.Name = "cmb_year";
            this.cmb_year.Enter += new System.EventHandler(this.cmb_year_Enter);
            this.cmb_year.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_year_KeyDown);
            this.cmb_year.Leave += new System.EventHandler(this.cmb_year_Leave);
            this.cmb_year.MouseLeave += new System.EventHandler(this.cmb_year_MouseLeave);
            // 
            // cmb_company
            // 
            this.cmb_company.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmb_company, "cmb_company");
            this.cmb_company.FormattingEnabled = true;
            this.cmb_company.Name = "cmb_company";
            this.cmb_company.Enter += new System.EventHandler(this.cmb_company_Enter);
            this.cmb_company.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_company_KeyDown);
            // 
            // cmb_brno
            // 
            this.cmb_brno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmb_brno, "cmb_brno");
            this.cmb_brno.FormattingEnabled = true;
            this.cmb_brno.Name = "cmb_brno";
            this.cmb_brno.SelectedIndexChanged += new System.EventHandler(this.cmb_brno_SelectedIndexChanged);
            this.cmb_brno.Enter += new System.EventHandler(this.cmb_brno_Enter);
            this.cmb_brno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_brno_KeyDown);
            this.cmb_brno.Leave += new System.EventHandler(this.cmb_brno_Leave);
            this.cmb_brno.MouseHover += new System.EventHandler(this.cmb_brno_MouseHover);
            this.cmb_brno.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmb_brno_MouseMove);
            this.cmb_brno.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cmb_brno_MouseUp);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cmb_user
            // 
            this.cmb_user.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmb_user, "cmb_user");
            this.cmb_user.FormattingEnabled = true;
            this.cmb_user.Name = "cmb_user";
            this.cmb_user.Enter += new System.EventHandler(this.cmb_user_Enter);
            this.cmb_user.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox1_KeyDown);
            this.cmb_user.Leave += new System.EventHandler(this.comboBox1_Leave);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Image = global::POS.Properties.Resources.Key_32x32;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Image = global::POS.Properties.Resources.Log_Out_32x32;
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_pass
            // 
            resources.ApplyResources(this.txt_pass, "txt_pass");
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox2_MouseClick);
            this.txt_pass.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.txt_pass.Enter += new System.EventHandler(this.txt_pass_Enter);
            this.txt_pass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
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
            // LOGIN
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ControlBox = false;
            this.Controls.Add(this.chk_lang);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MaximizeBox = false;
            this.Name = "LOGIN";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LOGIN_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LOGIN_FormClosed);
            this.Load += new System.EventHandler(this.LOGIN_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_pass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cmb_user;
        //private Eugenis.CustomComboBox cmb_user;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmb_brno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_year;
        private System.Windows.Forms.ComboBox cmb_company;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chk_rp;
        private System.Windows.Forms.CheckBox chk_lang;
    }
}