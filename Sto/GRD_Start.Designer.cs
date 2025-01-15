namespace POS.Sto
{
    partial class GRD_Start
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GRD_Start));
            this.label9 = new System.Windows.Forms.Label();
            this.cmb_ftwh = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_hdate = new System.Windows.Forms.MaskedTextBox();
            this.txt_mdate = new System.Windows.Forms.MaskedTextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_grp = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_sgrp = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // cmb_ftwh
            // 
            resources.ApplyResources(this.cmb_ftwh, "cmb_ftwh");
            this.cmb_ftwh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ftwh.FormattingEnabled = true;
            this.cmb_ftwh.Name = "cmb_ftwh";
            this.cmb_ftwh.SelectedIndexChanged += new System.EventHandler(this.cmb_ftwh_SelectedIndexChanged);
            this.cmb_ftwh.Enter += new System.EventHandler(this.cmb_ftwh_Enter);
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
            this.txt_hdate.Leave += new System.EventHandler(this.txt_hdate_Leave);
            this.txt_hdate.Validating += new System.ComponentModel.CancelEventHandler(this.txt_hdate_Validating);
            // 
            // txt_mdate
            // 
            resources.ApplyResources(this.txt_mdate, "txt_mdate");
            this.txt_mdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_mdate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txt_mdate.Name = "txt_mdate";
            this.txt_mdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_mdate_KeyDown);
            this.txt_mdate.Leave += new System.EventHandler(this.txt_mdate_Leave);
            this.txt_mdate.Validating += new System.ComponentModel.CancelEventHandler(this.txt_mdate_Validating);
            // 
            // btn_start
            // 
            this.btn_start.Image = global::POS.Properties.Resources.Play_32x32;
            resources.ApplyResources(this.btn_start, "btn_start");
            this.btn_start.Name = "btn_start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_update
            // 
            this.btn_update.Image = global::POS.Properties.Resources.Refresh_32x32;
            resources.ApplyResources(this.btn_update, "btn_update");
            this.btn_update.Name = "btn_update";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // button1
            // 
            this.button1.Image = global::POS.Properties.Resources.Delete_32x32;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmb_grp
            // 
            resources.ApplyResources(this.cmb_grp, "cmb_grp");
            this.cmb_grp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_grp.FormattingEnabled = true;
            this.cmb_grp.Name = "cmb_grp";
            this.cmb_grp.SelectedIndexChanged += new System.EventHandler(this.cmb_grp_SelectedIndexChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // cmb_sgrp
            // 
            resources.ApplyResources(this.cmb_sgrp, "cmb_sgrp");
            this.cmb_sgrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_sgrp.FormattingEnabled = true;
            this.cmb_sgrp.Name = "cmb_sgrp";
            this.cmb_sgrp.Enter += new System.EventHandler(this.cmb_sgrp_Enter);
            this.cmb_sgrp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_sgrp_KeyDown);
            // 
            // GRD_Start
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmb_sgrp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_grp);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmb_ftwh);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_hdate);
            this.Controls.Add(this.txt_mdate);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Name = "GRD_Start";
            this.Load += new System.EventHandler(this.GRD_Start_Load);
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
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_grp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_sgrp;
    }
}