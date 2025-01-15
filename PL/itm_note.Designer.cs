namespace POS
{
    partial class itm_note
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(itm_note));
            this.txt_ino = new System.Windows.Forms.TextBox();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.txt_note = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_ino
            // 
            resources.ApplyResources(this.txt_ino, "txt_ino");
            this.txt_ino.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ino.Name = "txt_ino";
            this.txt_ino.ReadOnly = true;
            // 
            // txt_name
            // 
            resources.ApplyResources(this.txt_name, "txt_name");
            this.txt_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_name.Name = "txt_name";
            this.txt_name.ReadOnly = true;
            // 
            // txt_note
            // 
            resources.ApplyResources(this.txt_note, "txt_note");
            this.txt_note.Name = "txt_note";
            this.txt_note.ReadOnly = true;
            // 
            // itm_note
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Controls.Add(this.txt_ino);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.txt_note);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Name = "itm_note";
            this.Load += new System.EventHandler(this.Sal_itm_not_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txt_note;
        public System.Windows.Forms.TextBox txt_name;
        public System.Windows.Forms.TextBox txt_ino;

    }
}