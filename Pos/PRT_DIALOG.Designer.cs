namespace POS.Pos
{
    partial class PRT_DIALOG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRT_DIALOG));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.POS5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.POS4 = new System.Windows.Forms.Button();
            this.POS3 = new System.Windows.Forms.Button();
            this.POS2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.POS5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.POS4);
            this.groupBox1.Controls.Add(this.POS3);
            this.groupBox1.Controls.Add(this.POS2);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // POS5
            // 
            resources.ApplyResources(this.POS5, "POS5");
            this.POS5.Name = "POS5";
            this.POS5.UseVisualStyleBackColor = true;
            this.POS5.Click += new System.EventHandler(this.POS5_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // POS4
            // 
            resources.ApplyResources(this.POS4, "POS4");
            this.POS4.Name = "POS4";
            this.POS4.UseVisualStyleBackColor = true;
            this.POS4.Click += new System.EventHandler(this.POS4_Click);
            // 
            // POS3
            // 
            resources.ApplyResources(this.POS3, "POS3");
            this.POS3.Name = "POS3";
            this.POS3.UseVisualStyleBackColor = true;
            this.POS3.Click += new System.EventHandler(this.POS3_Click);
            // 
            // POS2
            // 
            resources.ApplyResources(this.POS2, "POS2");
            this.POS2.Name = "POS2";
            this.POS2.UseVisualStyleBackColor = true;
            this.POS2.Click += new System.EventHandler(this.POS2_Click);
            // 
            // PRT_DIALOG
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.groupBox1);
            this.Name = "PRT_DIALOG";
            this.Load += new System.EventHandler(this.PRT_DIALOG_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button POS4;
        private System.Windows.Forms.Button POS3;
        private System.Windows.Forms.Button POS2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button POS5;
        private System.Windows.Forms.Button button1;

    }
}