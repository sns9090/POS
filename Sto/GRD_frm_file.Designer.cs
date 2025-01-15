namespace POS.Sto
{
    partial class GRD_frm_file
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GRD_frm_file));
            this.label9 = new System.Windows.Forms.Label();
            this.cmb_ftwh = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_hdate = new System.Windows.Forms.MaskedTextBox();
            this.txt_mdate = new System.Windows.Forms.MaskedTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // cmb_ftwh
            // 
            resources.ApplyResources(this.cmb_ftwh, "cmb_ftwh");
            this.cmb_ftwh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ftwh.FormattingEnabled = true;
            this.cmb_ftwh.Name = "cmb_ftwh";
            this.cmb_ftwh.SelectedIndexChanged += new System.EventHandler(this.cmb_ftwh_SelectedIndexChanged);
            this.cmb_ftwh.Enter += new System.EventHandler(this.cmb_ftwh_Enter);
            this.cmb_ftwh.Leave += new System.EventHandler(this.cmb_ftwh_Leave);
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
            // txt_hdate
            // 
            resources.ApplyResources(this.txt_hdate, "txt_hdate");
            this.txt_hdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_hdate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txt_hdate.Name = "txt_hdate";
            this.txt_hdate.ReadOnly = true;
            // 
            // txt_mdate
            // 
            resources.ApplyResources(this.txt_mdate, "txt_mdate");
            this.txt_mdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_mdate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txt_mdate.Name = "txt_mdate";
            this.txt_mdate.ReadOnly = true;
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // button2
            // 
            this.button2.Image = global::POS.Properties.Resources.Download_32x32;
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // GRD_frm_file
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_hdate);
            this.Controls.Add(this.txt_mdate);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmb_ftwh);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Name = "GRD_frm_file";
            this.Load += new System.EventHandler(this.GRD_frm_file_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmb_ftwh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox txt_hdate;
        private System.Windows.Forms.MaskedTextBox txt_mdate;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}