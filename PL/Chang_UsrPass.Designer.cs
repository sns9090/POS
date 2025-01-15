namespace POS
{
    partial class Chang_UsrPass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chang_UsrPass));
            this.label15 = new System.Windows.Forms.Label();
            this.txt_cnewp = new System.Windows.Forms.TextBox();
            this.txt_oldp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_newp = new System.Windows.Forms.TextBox();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_uid = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // txt_cnewp
            // 
            this.txt_cnewp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_cnewp, "txt_cnewp");
            this.txt_cnewp.Name = "txt_cnewp";
            // 
            // txt_oldp
            // 
            this.txt_oldp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_oldp, "txt_oldp");
            this.txt_oldp.Name = "txt_oldp";
            this.txt_oldp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_oldp_KeyDown);
            this.txt_oldp.Leave += new System.EventHandler(this.txt_oldp_Leave);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Name = "label2";
            // 
            // txt_newp
            // 
            this.txt_newp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_newp, "txt_newp");
            this.txt_newp.Name = "txt_newp";
            // 
            // btn_Exit
            // 
            this.btn_Exit.Image = global::POS.Properties.Resources.Log_Out_32x32;
            resources.ApplyResources(this.btn_Exit, "btn_Exit");
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btn_save
            // 
            this.btn_save.Image = global::POS.Properties.Resources.Save_32x32;
            resources.ApplyResources(this.btn_save, "btn_save");
            this.btn_save.Name = "btn_save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Name = "label3";
            // 
            // txt_uid
            // 
            this.txt_uid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_uid, "txt_uid");
            this.txt_uid.Name = "txt_uid";
            this.txt_uid.ReadOnly = true;
            // 
            // Chang_UsrPass
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txt_uid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txt_cnewp);
            this.Controls.Add(this.txt_oldp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_newp);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.btn_save);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Chang_UsrPass";
            this.Load += new System.EventHandler(this.Chang_UsrPass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_cnewp;
        private System.Windows.Forms.TextBox txt_oldp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_newp;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txt_uid;
    }
}