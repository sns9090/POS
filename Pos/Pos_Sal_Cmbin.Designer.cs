namespace POS.Pos
{
    partial class Pos_Sal_Cmbin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pos_Sal_Cmbin));
            this.label12 = new System.Windows.Forms.Label();
            this.cmb_salctr = new System.Windows.Forms.ComboBox();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chk_all = new System.Windows.Forms.RadioButton();
            this.chk_rsal = new System.Windows.Forms.RadioButton();
            this.chk_sal = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_tmdate = new System.Windows.Forms.MaskedTextBox();
            this.txt_fmdate = new System.Windows.Forms.MaskedTextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button2 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // cmb_salctr
            // 
            resources.ApplyResources(this.cmb_salctr, "cmb_salctr");
            this.cmb_salctr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_salctr.FormattingEnabled = true;
            this.cmb_salctr.Name = "cmb_salctr";
            this.cmb_salctr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_salctr_KeyDown);
            this.cmb_salctr.Leave += new System.EventHandler(this.cmb_salctr_Leave);
            // 
            // btn_Exit
            // 
            resources.ApplyResources(this.btn_Exit, "btn_Exit");
            this.btn_Exit.Image = global::POS.Properties.Resources.Log_Out_32x32;
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chk_all);
            this.panel2.Controls.Add(this.chk_rsal);
            this.panel2.Controls.Add(this.chk_sal);
            this.panel2.Name = "panel2";
            // 
            // chk_all
            // 
            resources.ApplyResources(this.chk_all, "chk_all");
            this.chk_all.Checked = true;
            this.chk_all.Name = "chk_all";
            this.chk_all.TabStop = true;
            this.chk_all.UseVisualStyleBackColor = true;
            // 
            // chk_rsal
            // 
            resources.ApplyResources(this.chk_rsal, "chk_rsal");
            this.chk_rsal.Name = "chk_rsal";
            this.chk_rsal.UseVisualStyleBackColor = true;
            // 
            // chk_sal
            // 
            resources.ApplyResources(this.chk_sal, "chk_sal");
            this.chk_sal.Name = "chk_sal";
            this.chk_sal.UseVisualStyleBackColor = true;
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
            // 
            // txt_fmdate
            // 
            resources.ApplyResources(this.txt_fmdate, "txt_fmdate");
            this.txt_fmdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_fmdate.Name = "txt_fmdate";
            this.txt_fmdate.Enter += new System.EventHandler(this.txt_fmdate_Enter);
            this.txt_fmdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_fmdate_KeyDown);
            this.txt_fmdate.Leave += new System.EventHandler(this.txt_fmdate_Leave);
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Image = global::POS.Properties.Resources.Play_32x32;
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Pos_Sal_Cmbin
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmb_salctr);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_tmdate);
            this.Controls.Add(this.txt_fmdate);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button2);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MaximizeBox = false;
            this.Name = "Pos_Sal_Cmbin";
            this.Load += new System.EventHandler(this.Pos_Sal_Cmbin_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox txt_tmdate;
        private System.Windows.Forms.MaskedTextBox txt_fmdate;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton chk_all;
        private System.Windows.Forms.RadioButton chk_rsal;
        private System.Windows.Forms.RadioButton chk_sal;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmb_salctr;
    }
}