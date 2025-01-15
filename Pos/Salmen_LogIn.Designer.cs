namespace POS.Pos
{
    partial class Salmen_LogIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Salmen_LogIn));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_salnam = new System.Windows.Forms.TextBox();
            this.txt_salman = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txt_salnam);
            this.groupBox1.Controls.Add(this.txt_salman);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.txt_password);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txt_salnam
            // 
            resources.ApplyResources(this.txt_salnam, "txt_salnam");
            this.txt_salnam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_salnam.Name = "txt_salnam";
            this.txt_salnam.ReadOnly = true;
            // 
            // txt_salman
            // 
            resources.ApplyResources(this.txt_salman, "txt_salman");
            this.txt_salman.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_salman.Name = "txt_salman";
            this.txt_salman.TextChanged += new System.EventHandler(this.txt_salman_TextChanged);
            this.txt_salman.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_salman_KeyDown);
            this.txt_salman.Leave += new System.EventHandler(this.txt_salman_Leave);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Image = global::POS.Properties.Resources.Key_32x32;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Image = global::POS.Properties.Resources.Log_Out_32x32;
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_password
            // 
            resources.ApplyResources(this.txt_password, "txt_password");
            this.txt_password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_password.Name = "txt_password";
            this.txt_password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_password_KeyDown);
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
            // Salmen_LogIn
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.KeyPreview = true;
            this.Name = "Salmen_LogIn";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Salmen_LogIn_FormClosing);
            this.Load += new System.EventHandler(this.Salmen_LogIn_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Salmen_LogIn_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txt_salman;
        public System.Windows.Forms.TextBox txt_password;
        public System.Windows.Forms.TextBox txt_salnam;
    }
}