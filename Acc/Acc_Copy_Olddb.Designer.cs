namespace POS.Acc
{
    partial class Acc_Copy_Olddb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Acc_Copy_Olddb));
            this.panel2 = new System.Windows.Forms.Panel();
            this.chk_copy_files = new System.Windows.Forms.CheckBox();
            this.txt_oyr = new System.Windows.Forms.TextBox();
            this.txt_alrt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.rd_sto_copy = new System.Windows.Forms.RadioButton();
            this.cmb_wh = new System.Windows.Forms.ComboBox();
            this.rd_sup_copy = new System.Windows.Forms.RadioButton();
            this.rd_cus_copy = new System.Windows.Forms.RadioButton();
            this.rd_acc_copy = new System.Windows.Forms.RadioButton();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chk_copy_files);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // chk_copy_files
            // 
            resources.ApplyResources(this.chk_copy_files, "chk_copy_files");
            this.chk_copy_files.Name = "chk_copy_files";
            this.chk_copy_files.UseVisualStyleBackColor = true;
            this.chk_copy_files.CheckedChanged += new System.EventHandler(this.chk_copy_files_CheckedChanged);
            // 
            // txt_oyr
            // 
            resources.ApplyResources(this.txt_oyr, "txt_oyr");
            this.txt_oyr.Name = "txt_oyr";
            this.txt_oyr.Tag = "مثال تكتب db01y2020";
            // 
            // txt_alrt
            // 
            this.txt_alrt.BackColor = System.Drawing.Color.Gainsboro;
            this.txt_alrt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_alrt.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.txt_alrt, "txt_alrt");
            this.txt_alrt.Name = "txt_alrt";
            this.txt_alrt.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.rd_sto_copy);
            this.panel1.Controls.Add(this.cmb_wh);
            this.panel1.Controls.Add(this.rd_sup_copy);
            this.panel1.Controls.Add(this.rd_cus_copy);
            this.panel1.Controls.Add(this.rd_acc_copy);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // rd_sto_copy
            // 
            resources.ApplyResources(this.rd_sto_copy, "rd_sto_copy");
            this.rd_sto_copy.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rd_sto_copy.Name = "rd_sto_copy";
            this.rd_sto_copy.UseVisualStyleBackColor = false;
            // 
            // cmb_wh
            // 
            resources.ApplyResources(this.cmb_wh, "cmb_wh");
            this.cmb_wh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_wh.FormattingEnabled = true;
            this.cmb_wh.Name = "cmb_wh";
            this.cmb_wh.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_wh_KeyDown);
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
            this.rd_acc_copy.Checked = true;
            this.rd_acc_copy.Name = "rd_acc_copy";
            this.rd_acc_copy.TabStop = true;
            this.rd_acc_copy.UseVisualStyleBackColor = false;
            // 
            // btn_Exit
            // 
            resources.ApplyResources(this.btn_Exit, "btn_Exit");
            this.btn_Exit.Image = global::POS.Properties.Resources.Log_Out_32x32;
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // button1
            // 
            this.button1.Image = global::POS.Properties.Resources.Play_32x32;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Acc_Copy_Olddb
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txt_oyr);
            this.Controls.Add(this.txt_alrt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.button1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Name = "Acc_Copy_Olddb";
            this.Load += new System.EventHandler(this.Acc_Copy_Olddb_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rd_cus_copy;
        private System.Windows.Forms.RadioButton rd_acc_copy;
        private System.Windows.Forms.RadioButton rd_sto_copy;
        private System.Windows.Forms.RadioButton rd_sup_copy;
        private System.Windows.Forms.TextBox txt_oyr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_alrt;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chk_copy_files;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmb_wh;
    }
}