namespace POS.Sto
{
    partial class Sto_Whbins_Br
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sto_Whbins_Br));
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_sgrp = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chk_opnbalonly = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_grp = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.txt_code = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_supname = new System.Windows.Forms.TextBox();
            this.txt_supno = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmb_wh = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chk_allbr = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chk_allsalctr = new System.Windows.Forms.CheckBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(173, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 14);
            this.label3.TabIndex = 223;
            this.label3.Text = "م فرعية";
            // 
            // cmb_sgrp
            // 
            this.cmb_sgrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_sgrp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_sgrp.FormattingEnabled = true;
            this.cmb_sgrp.Location = new System.Drawing.Point(10, 64);
            this.cmb_sgrp.Name = "cmb_sgrp";
            this.cmb_sgrp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmb_sgrp.Size = new System.Drawing.Size(157, 22);
            this.cmb_sgrp.TabIndex = 222;
            this.cmb_sgrp.Enter += new System.EventHandler(this.cmb_sgrp_Enter);
            this.cmb_sgrp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_sgrp_KeyDown);
            // 
            // button3
            // 
            this.button3.Image = global::POS.Properties.Resources.Print_32x32;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button3.Location = new System.Drawing.Point(357, 13);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 40);
            this.button3.TabIndex = 221;
            this.button3.Text = "طباعة";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Image = global::POS.Properties.Resources.Excel_32_32;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button2.Location = new System.Drawing.Point(255, 13);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 40);
            this.button2.TabIndex = 220;
            this.button2.Text = "EXCEL";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Image = global::POS.Properties.Resources.Play_32x32;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(459, 13);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 40);
            this.button1.TabIndex = 219;
            this.button1.Text = "تنفيذ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.EnabledChanged += new System.EventHandler(this.button1_EnabledChanged);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chk_opnbalonly
            // 
            this.chk_opnbalonly.AutoSize = true;
            this.chk_opnbalonly.Location = new System.Drawing.Point(597, 6);
            this.chk_opnbalonly.Name = "chk_opnbalonly";
            this.chk_opnbalonly.Size = new System.Drawing.Size(161, 18);
            this.chk_opnbalonly.TabIndex = 218;
            this.chk_opnbalonly.Text = "المخزون الافتتاحي فقط";
            this.chk_opnbalonly.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(173, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 217;
            this.label2.Text = "المجموعة";
            // 
            // cmb_grp
            // 
            this.cmb_grp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_grp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_grp.FormattingEnabled = true;
            this.cmb_grp.Location = new System.Drawing.Point(10, 21);
            this.cmb_grp.Name = "cmb_grp";
            this.cmb_grp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmb_grp.Size = new System.Drawing.Size(157, 22);
            this.cmb_grp.TabIndex = 216;
            this.cmb_grp.SelectedIndexChanged += new System.EventHandler(this.cmb_grp_SelectedIndexChanged);
            this.cmb_grp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_grp_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(984, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 14);
            this.label8.TabIndex = 214;
            this.label8.Text = "الصنف";
            // 
            // txt_name
            // 
            this.txt_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_name.Location = new System.Drawing.Point(640, 67);
            this.txt_name.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_name.Name = "txt_name";
            this.txt_name.ReadOnly = true;
            this.txt_name.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_name.Size = new System.Drawing.Size(222, 22);
            this.txt_name.TabIndex = 213;
            // 
            // txt_code
            // 
            this.txt_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_code.Location = new System.Drawing.Point(862, 67);
            this.txt_code.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_code.Name = "txt_code";
            this.txt_code.Size = new System.Drawing.Size(116, 22);
            this.txt_code.TabIndex = 212;
            this.txt_code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_code_KeyDown);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(576, 72);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(43, 14);
            this.label17.TabIndex = 211;
            this.label17.Text = "المورد";
            // 
            // txt_supname
            // 
            this.txt_supname.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_supname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_supname.Enabled = false;
            this.txt_supname.Location = new System.Drawing.Point(303, 67);
            this.txt_supname.MaxLength = 100;
            this.txt_supname.Name = "txt_supname";
            this.txt_supname.ReadOnly = true;
            this.txt_supname.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_supname.Size = new System.Drawing.Size(190, 22);
            this.txt_supname.TabIndex = 210;
            // 
            // txt_supno
            // 
            this.txt_supno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_supno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_supno.Location = new System.Drawing.Point(493, 67);
            this.txt_supno.Name = "txt_supno";
            this.txt_supno.Size = new System.Drawing.Size(73, 22);
            this.txt_supno.TabIndex = 209;
            this.txt_supno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_supno_KeyDown);
            this.txt_supno.Leave += new System.EventHandler(this.txt_supno_Leave);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(980, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 14);
            this.label9.TabIndex = 117;
            this.label9.Text = "المخزن";
            // 
            // cmb_wh
            // 
            this.cmb_wh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_wh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_wh.Enabled = false;
            this.cmb_wh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_wh.FormattingEnabled = true;
            this.cmb_wh.Location = new System.Drawing.Point(784, 26);
            this.cmb_wh.Name = "cmb_wh";
            this.cmb_wh.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmb_wh.Size = new System.Drawing.Size(182, 22);
            this.cmb_wh.TabIndex = 116;
            this.cmb_wh.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_wh_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(192, 569);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "قيمة المخزون";
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.textBox1.Location = new System.Drawing.Point(10, 571);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(157, 27);
            this.textBox1.TabIndex = 1;
            this.textBox1.Visible = false;
            // 
            // chk_allbr
            // 
            this.chk_allbr.AutoSize = true;
            this.chk_allbr.Checked = true;
            this.chk_allbr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_allbr.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chk_allbr.Location = new System.Drawing.Point(597, 45);
            this.chk_allbr.Name = "chk_allbr";
            this.chk_allbr.Size = new System.Drawing.Size(96, 18);
            this.chk_allbr.TabIndex = 224;
            this.chk_allbr.Text = "جميع الفروع";
            this.chk_allbr.UseVisualStyleBackColor = true;
            this.chk_allbr.CheckedChanged += new System.EventHandler(this.chk_allbr_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Menu;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dataGridView1.Location = new System.Drawing.Point(10, 97);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1023, 471);
            this.dataGridView1.TabIndex = 225;
            // 
            // chk_allsalctr
            // 
            this.chk_allsalctr.AutoSize = true;
            this.chk_allsalctr.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chk_allsalctr.Location = new System.Drawing.Point(597, 25);
            this.chk_allsalctr.Name = "chk_allsalctr";
            this.chk_allsalctr.Size = new System.Drawing.Size(133, 18);
            this.chk_allsalctr.TabIndex = 226;
            this.chk_allsalctr.Text = "حسب مخازن الفرع";
            this.chk_allsalctr.UseVisualStyleBackColor = true;
            this.chk_allsalctr.CheckedChanged += new System.EventHandler(this.chk_allsalctr_CheckedChanged);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "br_no";
            this.Column1.FillWeight = 15F;
            this.Column1.HeaderText = "رقم الفرع";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "br_name";
            this.Column2.FillWeight = 35F;
            this.Column2.HeaderText = "اسم الفرع";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "nettotal";
            this.Column3.FillWeight = 15F;
            this.Column3.HeaderText = "قيمة المخزون";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "tax";
            this.Column4.FillWeight = 15F;
            this.Column4.HeaderText = "كمية المخزون";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "salcost";
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = "0.00";
            this.Column5.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column5.FillWeight = 15F;
            this.Column5.HeaderText = "سعر البيع";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "win";
            this.Column6.FillWeight = 15F;
            this.Column6.HeaderText = "هامش الربح";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Visible = false;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "winp";
            this.Column7.FillWeight = 15F;
            this.Column7.HeaderText = "نسبة الربح";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Visible = false;
            // 
            // Sto_Whbins_Br
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1042, 579);
            this.Controls.Add(this.chk_allsalctr);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.chk_allbr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmb_sgrp);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chk_opnbalonly);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb_grp);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.txt_code);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txt_supname);
            this.Controls.Add(this.txt_supno);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmb_wh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Sto_Whbins_Br";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ارصدة وقمية مخزون الفروع";
            this.Load += new System.EventHandler(this.whamount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmb_wh;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_supname;
        private System.Windows.Forms.TextBox txt_supno;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.TextBox txt_code;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_grp;
        private System.Windows.Forms.CheckBox chk_opnbalonly;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_sgrp;
        private System.Windows.Forms.CheckBox chk_allbr;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox chk_allsalctr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}