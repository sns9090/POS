namespace POS
{
    partial class UnPost_All
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnPost_All));
            this.cmb_type = new System.Windows.Forms.ComboBox();
            this.txt_ref = new System.Windows.Forms.TextBox();
            this.cmb_salctr = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmb_type
            // 
            resources.ApplyResources(this.cmb_type, "cmb_type");
            this.cmb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_type.FormattingEnabled = true;
            this.cmb_type.Name = "cmb_type";
            this.cmb_type.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox1_KeyDown);
            // 
            // txt_ref
            // 
            resources.ApplyResources(this.txt_ref, "txt_ref");
            this.txt_ref.Name = "txt_ref";
            this.txt_ref.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // cmb_salctr
            // 
            resources.ApplyResources(this.cmb_salctr, "cmb_salctr");
            this.cmb_salctr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_salctr.FormattingEnabled = true;
            this.cmb_salctr.Name = "cmb_salctr";
            this.cmb_salctr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_salctr_KeyDown);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Image = global::POS.Properties.Resources.Play_32x32;
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
            // UnPost_All
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmb_salctr);
            this.Controls.Add(this.txt_ref);
            this.Controls.Add(this.cmb_type);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MaximizeBox = false;
            this.Name = "UnPost_All";
            this.Load += new System.EventHandler(this.Search_by_No_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cmb_type;
        public System.Windows.Forms.TextBox txt_ref;
        public System.Windows.Forms.ComboBox cmb_salctr;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;

    }
}