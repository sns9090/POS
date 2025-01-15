namespace POS.Sto
{
    partial class Items_Bld
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Items_Bld));
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rd_convrt_qtcost = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.rd_sup_copy = new System.Windows.Forms.RadioButton();
            this.rd_cus_copy = new System.Windows.Forms.RadioButton();
            this.rd_acc_copy = new System.Windows.Forms.RadioButton();
            this.rd_sal_cost = new System.Windows.Forms.RadioButton();
            this.rd_qty_cost = new System.Windows.Forms.RadioButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Image = global::POS.Properties.Resources.Play_32x32;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Exit
            // 
            resources.ApplyResources(this.btn_Exit, "btn_Exit");
            this.btn_Exit.Image = global::POS.Properties.Resources.Log_Out_32x32;
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.rd_convrt_qtcost);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.rd_sup_copy);
            this.panel1.Controls.Add(this.rd_cus_copy);
            this.panel1.Controls.Add(this.rd_acc_copy);
            this.panel1.Controls.Add(this.rd_sal_cost);
            this.panel1.Controls.Add(this.rd_qty_cost);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
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
            // rd_convrt_qtcost
            // 
            this.rd_convrt_qtcost.FlatAppearance.BorderColor = System.Drawing.Color.White;
            resources.ApplyResources(this.rd_convrt_qtcost, "rd_convrt_qtcost");
            this.rd_convrt_qtcost.Name = "rd_convrt_qtcost";
            this.rd_convrt_qtcost.UseVisualStyleBackColor = false;
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // rd_sup_copy
            // 
            resources.ApplyResources(this.rd_sup_copy, "rd_sup_copy");
            this.rd_sup_copy.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rd_sup_copy.Name = "rd_sup_copy";
            this.rd_sup_copy.UseVisualStyleBackColor = false;
            // 
            // rd_cus_copy
            // 
            resources.ApplyResources(this.rd_cus_copy, "rd_cus_copy");
            this.rd_cus_copy.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rd_cus_copy.Name = "rd_cus_copy";
            this.rd_cus_copy.UseVisualStyleBackColor = false;
            // 
            // rd_acc_copy
            // 
            resources.ApplyResources(this.rd_acc_copy, "rd_acc_copy");
            this.rd_acc_copy.Name = "rd_acc_copy";
            this.rd_acc_copy.UseVisualStyleBackColor = false;
            // 
            // rd_sal_cost
            // 
            resources.ApplyResources(this.rd_sal_cost, "rd_sal_cost");
            this.rd_sal_cost.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rd_sal_cost.Name = "rd_sal_cost";
            this.rd_sal_cost.UseVisualStyleBackColor = false;
            // 
            // rd_qty_cost
            // 
            resources.ApplyResources(this.rd_qty_cost, "rd_qty_cost");
            this.rd_qty_cost.Checked = true;
            this.rd_qty_cost.Name = "rd_qty_cost";
            this.rd_qty_cost.TabStop = true;
            this.rd_qty_cost.UseVisualStyleBackColor = false;
            this.rd_qty_cost.CheckedChanged += new System.EventHandler(this.rd_qty_cost_CheckedChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // Items_Bld
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.button1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Name = "Items_Bld";
            this.Load += new System.EventHandler(this.Items_Bld_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rd_sal_cost;
        private System.Windows.Forms.RadioButton rd_qty_cost;
        private System.Windows.Forms.RadioButton rd_sup_copy;
        private System.Windows.Forms.RadioButton rd_cus_copy;
        private System.Windows.Forms.RadioButton rd_acc_copy;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.RadioButton rd_convrt_qtcost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
    }
}