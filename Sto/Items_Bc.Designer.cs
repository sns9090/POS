namespace POS.Sto
{
    partial class Items_Bc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Items_Bc));
            this.btn_Exit = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_unit = new System.Windows.Forms.ComboBox();
            this.txt_price = new System.Windows.Forms.TextBox();
            this.txt_itemname = new System.Windows.Forms.TextBox();
            this.txt_itemno = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.txt_shdqty = new System.Windows.Forms.TextBox();
            this.txt_barcode = new System.Windows.Forms.TextBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.item_bc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pk_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pkorder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bc_cst = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.min_p = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_mprice = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Exit
            // 
            resources.ApplyResources(this.btn_Exit, "btn_Exit");
            this.btn_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Exit.Image = global::POS.Properties.Resources.Log_Out_32x32;
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // button4
            // 
            this.button4.Image = global::POS.Properties.Resources.barcode;
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txt_mprice);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmb_unit);
            this.panel1.Controls.Add(this.txt_price);
            this.panel1.Controls.Add(this.txt_itemname);
            this.panel1.Controls.Add(this.txt_itemno);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.txt_shdqty);
            this.panel1.Controls.Add(this.txt_barcode);
            this.panel1.Controls.Add(this.btn_add);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
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
            // cmb_unit
            // 
            this.cmb_unit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmb_unit, "cmb_unit");
            this.cmb_unit.FormattingEnabled = true;
            this.cmb_unit.Name = "cmb_unit";
            this.cmb_unit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_unit_KeyDown);
            // 
            // txt_price
            // 
            this.txt_price.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_price, "txt_price");
            this.txt_price.Name = "txt_price";
            this.txt_price.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_price_KeyDown);
            // 
            // txt_itemname
            // 
            this.txt_itemname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_itemname, "txt_itemname");
            this.txt_itemname.Name = "txt_itemname";
            this.txt_itemname.ReadOnly = true;
            // 
            // txt_itemno
            // 
            this.txt_itemno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_itemno, "txt_itemno");
            this.txt_itemno.Name = "txt_itemno";
            this.txt_itemno.ReadOnly = true;
            // 
            // button3
            // 
            this.button3.Image = global::POS.Properties.Resources.Delete_32x32;
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txt_shdqty
            // 
            this.txt_shdqty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_shdqty, "txt_shdqty");
            this.txt_shdqty.Name = "txt_shdqty";
            this.txt_shdqty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // txt_barcode
            // 
            this.txt_barcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_barcode, "txt_barcode");
            this.txt_barcode.Name = "txt_barcode";
            this.txt_barcode.Enter += new System.EventHandler(this.txt_barcode_Enter);
            this.txt_barcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.txt_barcode.Leave += new System.EventHandler(this.txt_barcode_Leave);
            // 
            // btn_add
            // 
            this.btn_add.Image = global::POS.Properties.Resources.Add_32x32;
            resources.ApplyResources(this.btn_add, "btn_add");
            this.btn_add.Name = "btn_add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox5
            // 
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBox5, "textBox5");
            this.textBox5.Name = "textBox5";
            // 
            // button2
            // 
            this.button2.Image = global::POS.Properties.Resources.Save_32x32;
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.item_bc,
            this.pack,
            this.pk_qty,
            this.price,
            this.pkorder,
            this.unit_id,
            this.bc_cst,
            this.min_p});
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // item_bc
            // 
            this.item_bc.DataPropertyName = "barcode";
            this.item_bc.FillWeight = 30F;
            resources.ApplyResources(this.item_bc, "item_bc");
            this.item_bc.Name = "item_bc";
            this.item_bc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pack
            // 
            this.pack.DataPropertyName = "unit";
            this.pack.FillWeight = 15F;
            resources.ApplyResources(this.pack, "pack");
            this.pack.Name = "pack";
            this.pack.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.pack.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pk_qty
            // 
            this.pk_qty.DataPropertyName = "shdqty";
            this.pk_qty.FillWeight = 15F;
            resources.ApplyResources(this.pk_qty, "pk_qty");
            this.pk_qty.Name = "pk_qty";
            this.pk_qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // price
            // 
            this.price.DataPropertyName = "price";
            this.price.FillWeight = 15F;
            resources.ApplyResources(this.price, "price");
            this.price.Name = "price";
            this.price.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pkorder
            // 
            this.pkorder.DataPropertyName = "serial";
            this.pkorder.FillWeight = 10F;
            resources.ApplyResources(this.pkorder, "pkorder");
            this.pkorder.Name = "pkorder";
            this.pkorder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // unit_id
            // 
            this.unit_id.DataPropertyName = "u_i";
            resources.ApplyResources(this.unit_id, "unit_id");
            this.unit_id.Name = "unit_id";
            this.unit_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // bc_cst
            // 
            this.bc_cst.DataPropertyName = "bccst";
            this.bc_cst.FillWeight = 15F;
            resources.ApplyResources(this.bc_cst, "bc_cst");
            this.bc_cst.Name = "bc_cst";
            this.bc_cst.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // min_p
            // 
            this.min_p.DataPropertyName = "min_price";
            this.min_p.FillWeight = 15F;
            resources.ApplyResources(this.min_p, "min_p");
            this.min_p.Name = "min_p";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txt_mprice
            // 
            this.txt_mprice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_mprice, "txt_mprice");
            this.txt_mprice.Name = "txt_mprice";
            this.txt_mprice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_mprice_KeyDown);
            // 
            // Items_Bc
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Name = "Items_Bc";
            this.Load += new System.EventHandler(this.Items_Bc_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_barcode;
        private System.Windows.Forms.TextBox txt_shdqty;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txt_itemname;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.ComboBox cmb_unit;
        private System.Windows.Forms.TextBox txt_price;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Exit;
        public System.Windows.Forms.TextBox txt_itemno;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_bc;
        private System.Windows.Forms.DataGridViewTextBoxColumn pack;
        private System.Windows.Forms.DataGridViewTextBoxColumn pk_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn pkorder;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn bc_cst;
        private System.Windows.Forms.DataGridViewTextBoxColumn min_p;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_mprice;
    }
}