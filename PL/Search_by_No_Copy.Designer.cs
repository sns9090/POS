namespace POS
{
    partial class Search_by_No_Copy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Search_by_No_Copy));
            this.cmb_type = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cmb_salctr = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_year = new System.Windows.Forms.ComboBox();
            this.cmb_company = new System.Windows.Forms.ComboBox();
            this.cmb_brno = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmb_type
            // 
            this.cmb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_type.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_type.FormattingEnabled = true;
            this.cmb_type.Location = new System.Drawing.Point(143, 191);
            this.cmb_type.Name = "cmb_type";
            this.cmb_type.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmb_type.Size = new System.Drawing.Size(249, 22);
            this.cmb_type.TabIndex = 4;
            this.cmb_type.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox1_KeyDown);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(199, 231);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(193, 22);
            this.textBox1.TabIndex = 5;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // cmb_salctr
            // 
            this.cmb_salctr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_salctr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_salctr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_salctr.FormattingEnabled = true;
            this.cmb_salctr.Location = new System.Drawing.Point(143, 150);
            this.cmb_salctr.Name = "cmb_salctr";
            this.cmb_salctr.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmb_salctr.Size = new System.Drawing.Size(249, 22);
            this.cmb_salctr.TabIndex = 3;
            this.cmb_salctr.Visible = false;
            this.cmb_salctr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_salctr_KeyDown);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(233, 273);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox1.Size = new System.Drawing.Size(159, 18);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "نسخ السعر بدلا التكلفة";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(431, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 14);
            this.label7.TabIndex = 118;
            this.label7.Text = "النــــوع";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(421, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 14);
            this.label9.TabIndex = 119;
            this.label9.Text = "المركـــــز";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(405, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 14);
            this.label5.TabIndex = 125;
            this.label5.Text = "السنة المالية";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(408, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 14);
            this.label4.TabIndex = 124;
            this.label4.Text = "اسم الشركة";
            // 
            // cmb_year
            // 
            this.cmb_year.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_year.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_year.FormattingEnabled = true;
            this.cmb_year.Location = new System.Drawing.Point(143, 66);
            this.cmb_year.Name = "cmb_year";
            this.cmb_year.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmb_year.Size = new System.Drawing.Size(249, 22);
            this.cmb_year.TabIndex = 1;
            this.cmb_year.Enter += new System.EventHandler(this.cmb_year_Enter);
            this.cmb_year.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_year_KeyDown);
            // 
            // cmb_company
            // 
            this.cmb_company.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_company.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_company.FormattingEnabled = true;
            this.cmb_company.Location = new System.Drawing.Point(12, 24);
            this.cmb_company.Name = "cmb_company";
            this.cmb_company.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmb_company.Size = new System.Drawing.Size(380, 22);
            this.cmb_company.TabIndex = 0;
            this.cmb_company.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_company_KeyDown);
            // 
            // cmb_brno
            // 
            this.cmb_brno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_brno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_brno.FormattingEnabled = true;
            this.cmb_brno.Location = new System.Drawing.Point(12, 108);
            this.cmb_brno.Name = "cmb_brno";
            this.cmb_brno.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmb_brno.Size = new System.Drawing.Size(380, 22);
            this.cmb_brno.TabIndex = 2;
            this.cmb_brno.Enter += new System.EventHandler(this.cmb_brno_Enter);
            this.cmb_brno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_brno_KeyDown);
            this.cmb_brno.Leave += new System.EventHandler(this.cmb_brno_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(411, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 14);
            this.label3.TabIndex = 123;
            this.label3.Text = "الــفــــــــرع";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(407, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 14);
            this.label1.TabIndex = 126;
            this.label1.Text = "رقم الحركة";
            // 
            // Search_by_No_Copy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(502, 298);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmb_year);
            this.Controls.Add(this.cmb_company);
            this.Controls.Add(this.cmb_brno);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cmb_salctr);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cmb_type);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Search_by_No_Copy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "نسخ من فرع او شركة اخرى";
            this.Load += new System.EventHandler(this.Search_by_No_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cmb_type;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.ComboBox cmb_salctr;
        public System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_year;
        private System.Windows.Forms.ComboBox cmb_company;
        private System.Windows.Forms.ComboBox cmb_brno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;

    }
}