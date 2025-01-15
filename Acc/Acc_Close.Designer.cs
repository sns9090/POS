namespace POS.Acc
{
    partial class Acc_Close
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Acc_Close));
            this.label8 = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.txt_code = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rad_tashqil = new System.Windows.Forms.RadioButton();
            this.chk_mtar = new System.Windows.Forms.RadioButton();
            this.chk_arbah = new System.Windows.Forms.RadioButton();
            this.chk_mtagra = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txt_name
            // 
            this.txt_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_name, "txt_name");
            this.txt_name.Name = "txt_name";
            this.txt_name.ReadOnly = true;
            // 
            // txt_code
            // 
            this.txt_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txt_code, "txt_code");
            this.txt_code.Name = "txt_code";
            this.txt_code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_code_KeyDown);
            this.txt_code.Leave += new System.EventHandler(this.txt_code_Leave);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rad_tashqil);
            this.panel2.Controls.Add(this.chk_mtar);
            this.panel2.Controls.Add(this.chk_arbah);
            this.panel2.Controls.Add(this.chk_mtagra);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // rad_tashqil
            // 
            resources.ApplyResources(this.rad_tashqil, "rad_tashqil");
            this.rad_tashqil.Name = "rad_tashqil";
            this.rad_tashqil.UseVisualStyleBackColor = true;
            // 
            // chk_mtar
            // 
            resources.ApplyResources(this.chk_mtar, "chk_mtar");
            this.chk_mtar.Checked = true;
            this.chk_mtar.Name = "chk_mtar";
            this.chk_mtar.TabStop = true;
            this.chk_mtar.UseVisualStyleBackColor = true;
            // 
            // chk_arbah
            // 
            resources.ApplyResources(this.chk_arbah, "chk_arbah");
            this.chk_arbah.Name = "chk_arbah";
            this.chk_arbah.UseVisualStyleBackColor = true;
            // 
            // chk_mtagra
            // 
            resources.ApplyResources(this.chk_mtagra, "chk_mtagra");
            this.chk_mtagra.Name = "chk_mtagra";
            this.chk_mtagra.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Image = global::POS.Properties.Resources.Play_32x32;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Acc_Close
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.txt_code);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Name = "Acc_Close";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.TextBox txt_code;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton chk_mtar;
        private System.Windows.Forms.RadioButton chk_arbah;
        private System.Windows.Forms.RadioButton chk_mtagra;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rad_tashqil;
    }
}