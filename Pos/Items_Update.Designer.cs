namespace POS.Pos
{
    partial class Items_Update
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Items_Update));
            this.chk_offers = new System.Windows.Forms.CheckBox();
            this.chk_all_itms = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_tmdate = new System.Windows.Forms.MaskedTextBox();
            this.txt_fmdate = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chk_salman = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_newp = new System.Windows.Forms.TextBox();
            this.txt_oldp = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.txt_code = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chk_fast = new System.Windows.Forms.CheckBox();
            this.chk_cust = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chk_offers
            // 
            resources.ApplyResources(this.chk_offers, "chk_offers");
            this.chk_offers.Name = "chk_offers";
            this.chk_offers.UseVisualStyleBackColor = true;
            // 
            // chk_all_itms
            // 
            resources.ApplyResources(this.chk_all_itms, "chk_all_itms");
            this.chk_all_itms.Name = "chk_all_itms";
            this.chk_all_itms.UseVisualStyleBackColor = true;
            this.chk_all_itms.CheckedChanged += new System.EventHandler(this.chk_all_itms_CheckedChanged);
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
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txt_tmdate
            // 
            resources.ApplyResources(this.txt_tmdate, "txt_tmdate");
            this.txt_tmdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tmdate.Name = "txt_tmdate";
            this.txt_tmdate.Enter += new System.EventHandler(this.txt_tmdate_Enter);
            this.txt_tmdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_tmdate_KeyDown);
            this.txt_tmdate.Leave += new System.EventHandler(this.txt_tmdate_Leave);
            this.txt_tmdate.Validating += new System.ComponentModel.CancelEventHandler(this.txt_tmdate_Validating);
            // 
            // txt_fmdate
            // 
            resources.ApplyResources(this.txt_fmdate, "txt_fmdate");
            this.txt_fmdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_fmdate.Name = "txt_fmdate";
            this.txt_fmdate.Enter += new System.EventHandler(this.txt_fmdate_Enter);
            this.txt_fmdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_fmdate_KeyDown);
            this.txt_fmdate.Leave += new System.EventHandler(this.txt_fmdate_Leave);
            this.txt_fmdate.Validating += new System.ComponentModel.CancelEventHandler(this.txt_fmdate_Validating);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Name = "label3";
            // 
            // chk_salman
            // 
            resources.ApplyResources(this.chk_salman, "chk_salman");
            this.chk_salman.Name = "chk_salman";
            this.chk_salman.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // button2
            // 
            this.button2.Image = global::POS.Properties.Resources.Refresh_32x32;
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_Exit
            // 
            resources.ApplyResources(this.btn_Exit, "btn_Exit");
            this.btn_Exit.Image = global::POS.Properties.Resources.Log_Out_32x32;
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
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
            // txt_newp
            // 
            this.txt_newp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_newp, "txt_newp");
            this.txt_newp.Name = "txt_newp";
            this.txt_newp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_newp_KeyDown);
            // 
            // txt_oldp
            // 
            this.txt_oldp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_oldp, "txt_oldp");
            this.txt_oldp.Name = "txt_oldp";
            this.txt_oldp.ReadOnly = true;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // txt_name
            // 
            this.txt_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_name, "txt_name");
            this.txt_name.Name = "txt_name";
            this.txt_name.ReadOnly = true;
            // 
            // txt_code
            // 
            this.txt_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_code, "txt_code");
            this.txt_code.Name = "txt_code";
            this.txt_code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_code_KeyDown);
            // 
            // button1
            // 
            this.button1.Image = global::POS.Properties.Resources.Refresh_32x32;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chk_fast
            // 
            resources.ApplyResources(this.chk_fast, "chk_fast");
            this.chk_fast.Checked = true;
            this.chk_fast.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_fast.Name = "chk_fast";
            this.chk_fast.UseVisualStyleBackColor = true;
            // 
            // chk_cust
            // 
            resources.ApplyResources(this.chk_cust, "chk_cust");
            this.chk_cust.Name = "chk_cust";
            this.chk_cust.UseVisualStyleBackColor = true;
            // 
            // Items_Update
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.chk_cust);
            this.Controls.Add(this.chk_fast);
            this.Controls.Add(this.chk_offers);
            this.Controls.Add(this.chk_all_itms);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_tmdate);
            this.Controls.Add(this.txt_fmdate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chk_salman);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_newp);
            this.Controls.Add(this.txt_oldp);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.txt_code);
            this.Controls.Add(this.button1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MaximizeBox = false;
            this.Name = "Items_Update";
            this.Load += new System.EventHandler(this.Price_Chang_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.TextBox txt_code;
        private System.Windows.Forms.TextBox txt_oldp;
        private System.Windows.Forms.TextBox txt_newp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chk_all_itms;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.CheckBox chk_offers;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.CheckBox chk_salman;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.MaskedTextBox txt_tmdate;
        public System.Windows.Forms.MaskedTextBox txt_fmdate;
        public System.Windows.Forms.CheckBox chk_fast;
        public System.Windows.Forms.CheckBox chk_cust;
    }
}