namespace POS.Pos
{
    partial class SalesReport_tm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesReport_tm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.طباعةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_custnam = new System.Windows.Forms.TextBox();
            this.txt_custno = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.chk_allp = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_drivernam = new System.Windows.Forms.TextBox();
            this.txt_driver = new System.Windows.Forms.TextBox();
            this.maskedTextBox4 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox3 = new System.Windows.Forms.MaskedTextBox();
            this.txt_other = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.txt_card = new System.Windows.Forms.TextBox();
            this.txt_cash = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_net1 = new System.Windows.Forms.TextBox();
            this.txt_tax = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmb_salctr = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column66 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column99 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.txt_cost = new System.Windows.Forms.TextBox();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.chk_notpayd = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.طباعةToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // طباعةToolStripMenuItem
            // 
            this.طباعةToolStripMenuItem.Name = "طباعةToolStripMenuItem";
            resources.ApplyResources(this.طباعةToolStripMenuItem, "طباعةToolStripMenuItem");
            this.طباعةToolStripMenuItem.Click += new System.EventHandler(this.طباعةToolStripMenuItem_Click);
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // txt_custnam
            // 
            resources.ApplyResources(this.txt_custnam, "txt_custnam");
            this.txt_custnam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_custnam.Name = "txt_custnam";
            this.txt_custnam.ReadOnly = true;
            this.txt_custnam.Click += new System.EventHandler(this.txt_custnam_Click);
            // 
            // txt_custno
            // 
            resources.ApplyResources(this.txt_custno, "txt_custno");
            this.txt_custno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_custno.Name = "txt_custno";
            this.txt_custno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_custno_KeyDown);
            this.txt_custno.Leave += new System.EventHandler(this.txt_custno_Leave);
            // 
            // btn_save
            // 
            this.btn_save.Image = global::POS.Properties.Resources.Save_32x32;
            resources.ApplyResources(this.btn_save, "btn_save");
            this.btn_save.Name = "btn_save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // dateTimePicker2
            // 
            resources.ApplyResources(this.dateTimePicker2, "dateTimePicker2");
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Name = "dateTimePicker2";
            // 
            // dateTimePicker1
            // 
            resources.ApplyResources(this.dateTimePicker1, "dateTimePicker1");
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Name = "dateTimePicker1";
            // 
            // chk_allp
            // 
            resources.ApplyResources(this.chk_allp, "chk_allp");
            this.chk_allp.Name = "chk_allp";
            this.chk_allp.UseVisualStyleBackColor = true;
            this.chk_allp.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // txt_drivernam
            // 
            resources.ApplyResources(this.txt_drivernam, "txt_drivernam");
            this.txt_drivernam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_drivernam.Name = "txt_drivernam";
            this.txt_drivernam.ReadOnly = true;
            this.txt_drivernam.Click += new System.EventHandler(this.txt_drivernam_Click);
            // 
            // txt_driver
            // 
            resources.ApplyResources(this.txt_driver, "txt_driver");
            this.txt_driver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_driver.Name = "txt_driver";
            this.txt_driver.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_driver_KeyDown);
            this.txt_driver.Leave += new System.EventHandler(this.txt_driver_Leave);
            // 
            // maskedTextBox4
            // 
            this.maskedTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBox4.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            resources.ApplyResources(this.maskedTextBox4, "maskedTextBox4");
            this.maskedTextBox4.Name = "maskedTextBox4";
            // 
            // maskedTextBox3
            // 
            this.maskedTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBox3.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            resources.ApplyResources(this.maskedTextBox3, "maskedTextBox3");
            this.maskedTextBox3.Name = "maskedTextBox3";
            // 
            // txt_other
            // 
            resources.ApplyResources(this.txt_other, "txt_other");
            this.txt_other.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_other.Name = "txt_other";
            this.txt_other.ReadOnly = true;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // button5
            // 
            this.button5.Image = global::POS.Properties.Resources.Print_32x32;
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // txt_card
            // 
            resources.ApplyResources(this.txt_card, "txt_card");
            this.txt_card.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_card.Name = "txt_card";
            this.txt_card.ReadOnly = true;
            // 
            // txt_cash
            // 
            resources.ApplyResources(this.txt_cash, "txt_cash");
            this.txt_cash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cash.Name = "txt_cash";
            this.txt_cash.ReadOnly = true;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // txt_net1
            // 
            resources.ApplyResources(this.txt_net1, "txt_net1");
            this.txt_net1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_net1.Name = "txt_net1";
            this.txt_net1.ReadOnly = true;
            // 
            // txt_tax
            // 
            resources.ApplyResources(this.txt_tax, "txt_tax");
            this.txt_tax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tax.Name = "txt_tax";
            this.txt_tax.ReadOnly = true;
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // cmb_salctr
            // 
            this.cmb_salctr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmb_salctr, "cmb_salctr");
            this.cmb_salctr.FormattingEnabled = true;
            this.cmb_salctr.Name = "cmb_salctr";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Menu;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column11,
            this.Column3,
            this.Column66,
            this.Column99,
            this.Column5,
            this.Column6});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick_1);
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "contr";
            this.Column8.FillWeight = 15F;
            resources.ApplyResources(this.Column8, "Column8");
            this.Column8.Name = "Column8";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ref";
            this.Column1.FillWeight = 15F;
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "date";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column2.FillWeight = 25F;
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "count";
            this.Column4.FillWeight = 10F;
            resources.ApplyResources(this.Column4, "Column4");
            this.Column4.Name = "Column4";
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "net_total";
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0.00";
            this.Column11.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column11.FillWeight = 15F;
            resources.ApplyResources(this.Column11, "Column11");
            this.Column11.Name = "Column11";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "cu_name";
            this.Column3.FillWeight = 20F;
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            // 
            // Column66
            // 
            this.Column66.DataPropertyName = "tamin";
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0.00";
            this.Column66.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column66.FillWeight = 20F;
            resources.ApplyResources(this.Column66, "Column66");
            this.Column66.Name = "Column66";
            // 
            // Column99
            // 
            this.Column99.DataPropertyName = "saleman";
            this.Column99.FillWeight = 15F;
            resources.ApplyResources(this.Column99, "Column99");
            this.Column99.Name = "Column99";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "dpayed";
            this.Column5.FillWeight = 10F;
            resources.ApplyResources(this.Column5, "Column5");
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "at";
            resources.ApplyResources(this.Column6, "Column6");
            this.Column6.Name = "Column6";
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            // 
            // button3
            // 
            this.button3.Image = global::POS.Properties.Resources.Print_32x32;
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txt_cost
            // 
            resources.ApplyResources(this.txt_cost, "txt_cost");
            this.txt_cost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cost.Name = "txt_cost";
            this.txt_cost.ReadOnly = true;
            // 
            // txt_total
            // 
            resources.ApplyResources(this.txt_total, "txt_total");
            this.txt_total.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_total.Name = "txt_total";
            this.txt_total.ReadOnly = true;
            // 
            // button2
            // 
            this.button2.Image = global::POS.Properties.Resources.Excel_32_32;
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Name = "label5";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBox2.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            resources.ApplyResources(this.maskedTextBox2, "maskedTextBox2");
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Enter += new System.EventHandler(this.maskedTextBox1_Enter);
            this.maskedTextBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.maskedTextBox2_KeyDown);
            this.maskedTextBox2.Leave += new System.EventHandler(this.maskedTextBox2_Leave);
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBox1.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            resources.ApplyResources(this.maskedTextBox1, "maskedTextBox1");
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Enter += new System.EventHandler(this.maskedTextBox1_Enter);
            this.maskedTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.maskedTextBox1_KeyDown);
            this.maskedTextBox1.Leave += new System.EventHandler(this.maskedTextBox1_Leave);
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
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Name = "label1";
            // 
            // button1
            // 
            this.button1.Image = global::POS.Properties.Resources.Play_32x32;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chk_notpayd
            // 
            resources.ApplyResources(this.chk_notpayd, "chk_notpayd");
            this.chk_notpayd.Name = "chk_notpayd";
            this.chk_notpayd.UseVisualStyleBackColor = true;
            // 
            // SalesReport_tm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.chk_notpayd);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txt_custnam);
            this.Controls.Add(this.txt_custno);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.chk_allp);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txt_drivernam);
            this.Controls.Add(this.txt_driver);
            this.Controls.Add(this.maskedTextBox4);
            this.Controls.Add(this.maskedTextBox3);
            this.Controls.Add(this.txt_other);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.txt_card);
            this.Controls.Add(this.txt_cash);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txt_net1);
            this.Controls.Add(this.txt_tax);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmb_salctr);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txt_cost);
            this.Controls.Add(this.txt_total);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.maskedTextBox2);
            this.Controls.Add(this.maskedTextBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Name = "SalesReport_tm";
            this.Load += new System.EventHandler(this.SalesReport_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txt_total;
        private System.Windows.Forms.TextBox txt_cost;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmb_salctr;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txt_tax;
        private System.Windows.Forms.TextBox txt_net1;
        private System.Windows.Forms.TextBox txt_card;
        private System.Windows.Forms.TextBox txt_cash;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem طباعةToolStripMenuItem;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txt_other;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.MaskedTextBox maskedTextBox3;
        private System.Windows.Forms.MaskedTextBox maskedTextBox4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_drivernam;
        public System.Windows.Forms.TextBox txt_driver;
        private System.Windows.Forms.CheckBox chk_allp;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_custnam;
        public System.Windows.Forms.TextBox txt_custno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column66;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column99;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.CheckBox chk_notpayd;
    }
}