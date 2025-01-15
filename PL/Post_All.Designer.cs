namespace POS
{
    partial class Post_All
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Post_All));
            this.txt_tno = new System.Windows.Forms.TextBox();
            this.txt_fno = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txt_tdate = new System.Windows.Forms.MaskedTextBox();
            this.txt_fdate = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cmb_salctr = new System.Windows.Forms.ComboBox();
            this.cmb_type = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txt_tno
            // 
            resources.ApplyResources(this.txt_tno, "txt_tno");
            this.txt_tno.Name = "txt_tno";
            this.txt_tno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_tno_KeyDown);
            // 
            // txt_fno
            // 
            resources.ApplyResources(this.txt_fno, "txt_fno");
            this.txt_fno.Name = "txt_fno";
            this.txt_fno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_fno_KeyDown);
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txt_tdate
            // 
            this.txt_tdate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            resources.ApplyResources(this.txt_tdate, "txt_tdate");
            this.txt_tdate.Name = "txt_tdate";
            this.txt_tdate.Enter += new System.EventHandler(this.txt_tdate_Enter);
            this.txt_tdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_tdate_KeyDown);
            this.txt_tdate.Leave += new System.EventHandler(this.txt_tdate_Leave);
            // 
            // txt_fdate
            // 
            this.txt_fdate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            resources.ApplyResources(this.txt_fdate, "txt_fdate");
            this.txt_fdate.Name = "txt_fdate";
            this.txt_fdate.Enter += new System.EventHandler(this.txt_fdate_Enter);
            this.txt_fdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_fdate_KeyDown);
            this.txt_fdate.Leave += new System.EventHandler(this.txt_fdate_Leave);
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
            // button2
            // 
            this.button2.Image = global::POS.Properties.Resources.Log_Out_32x32;
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Image = global::POS.Properties.Resources.Play_32x32;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmb_salctr
            // 
            this.cmb_salctr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmb_salctr, "cmb_salctr");
            this.cmb_salctr.FormattingEnabled = true;
            this.cmb_salctr.Name = "cmb_salctr";
            this.cmb_salctr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_salctr_KeyDown);
            // 
            // cmb_type
            // 
            this.cmb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmb_type, "cmb_type");
            this.cmb_type.FormattingEnabled = true;
            this.cmb_type.Name = "cmb_type";
            this.cmb_type.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox1_KeyDown);
            // 
            // Post_All
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Controls.Add(this.txt_tno);
            this.Controls.Add(this.txt_fno);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txt_tdate);
            this.Controls.Add(this.txt_fdate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmb_salctr);
            this.Controls.Add(this.cmb_type);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MaximizeBox = false;
            this.Name = "Post_All";
            this.Load += new System.EventHandler(this.Search_by_No_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cmb_type;
        public System.Windows.Forms.ComboBox cmb_salctr;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MaskedTextBox txt_tdate;
        private System.Windows.Forms.MaskedTextBox txt_fdate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_tno;
        private System.Windows.Forms.TextBox txt_fno;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;

    }
}