namespace POS
{
    partial class Date_Enter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Date_Enter));
            this.txt_date = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_date
            // 
            this.txt_date.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_date.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_date.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txt_date.Location = new System.Drawing.Point(58, 21);
            this.txt_date.Mask = "00-00-0000";
            this.txt_date.Name = "txt_date";
            this.txt_date.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_date.Size = new System.Drawing.Size(82, 22);
            this.txt_date.TabIndex = 107;
            this.txt_date.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txt_date_MaskInputRejected);
            this.txt_date.TextChanged += new System.EventHandler(this.txt_date_TextChanged);
            this.txt_date.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_per01_KeyDown);
            this.txt_date.Validating += new System.ComponentModel.CancelEventHandler(this.txt_date_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(165, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 14);
            this.label1.TabIndex = 108;
            this.label1.Text = "تاريخ الانتهاء";
            // 
            // Date_Enter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(283, 66);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_date);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Date_Enter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ادخال تاريخ الانتهاء";
            this.Load += new System.EventHandler(this.Date_Enter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.MaskedTextBox txt_date;
    }
}