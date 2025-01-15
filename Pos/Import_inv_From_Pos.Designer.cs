namespace POS.Pos
{
    partial class Import_inv_From_Pos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Import_inv_From_Pos));
            this.label8 = new System.Windows.Forms.Label();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_tmdate = new System.Windows.Forms.MaskedTextBox();
            this.txt_fmdate = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_ctrno = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(445, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 14);
            this.label8.TabIndex = 44;
            this.label8.Text = "الباركود";
            this.label8.Visible = false;
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Exit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btn_Exit.Image = global::POS.Properties.Resources.Log_Out_32x32;
            this.btn_Exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Exit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_Exit.Location = new System.Drawing.Point(212, 11);
            this.btn_Exit.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(77, 39);
            this.btn_Exit.TabIndex = 102;
            this.btn_Exit.Text = "خروج";
            this.btn_Exit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // button2
            // 
            this.button2.Image = global::POS.Properties.Resources.Refresh_32x32;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.Location = new System.Drawing.Point(12, 174);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 47);
            this.button2.TabIndex = 103;
            this.button2.Text = "جلب الفواتير";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 243);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(476, 25);
            this.progressBar1.TabIndex = 104;
            this.progressBar1.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(266, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 14);
            this.label4.TabIndex = 124;
            this.label4.Text = "الى";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(425, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 14);
            this.label6.TabIndex = 123;
            this.label6.Text = "من";
            // 
            // txt_tmdate
            // 
            this.txt_tmdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_tmdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tmdate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txt_tmdate.Location = new System.Drawing.Point(167, 188);
            this.txt_tmdate.Mask = "00-00-0000";
            this.txt_tmdate.Name = "txt_tmdate";
            this.txt_tmdate.Size = new System.Drawing.Size(95, 22);
            this.txt_tmdate.TabIndex = 122;
            this.txt_tmdate.Enter += new System.EventHandler(this.txt_tmdate_Enter);
            this.txt_tmdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_tmdate_KeyDown);
            this.txt_tmdate.Leave += new System.EventHandler(this.txt_tmdate_Leave);
            this.txt_tmdate.Validating += new System.ComponentModel.CancelEventHandler(this.txt_tmdate_Validating);
            // 
            // txt_fmdate
            // 
            this.txt_fmdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_fmdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_fmdate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txt_fmdate.Location = new System.Drawing.Point(324, 188);
            this.txt_fmdate.Mask = "00-00-0000";
            this.txt_fmdate.Name = "txt_fmdate";
            this.txt_fmdate.Size = new System.Drawing.Size(95, 22);
            this.txt_fmdate.TabIndex = 121;
            this.txt_fmdate.Enter += new System.EventHandler(this.txt_fmdate_Enter);
            this.txt_fmdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_fmdate_KeyDown);
            this.txt_fmdate.Leave += new System.EventHandler(this.txt_fmdate_Leave);
            this.txt_fmdate.Validating += new System.ComponentModel.CancelEventHandler(this.txt_fmdate_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(328, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 14);
            this.label5.TabIndex = 125;
            this.label5.Text = "رقم الكاشير";
            // 
            // cmb_ctrno
            // 
            this.cmb_ctrno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ctrno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_ctrno.FormattingEnabled = true;
            this.cmb_ctrno.Location = new System.Drawing.Point(167, 107);
            this.cmb_ctrno.Name = "cmb_ctrno";
            this.cmb_ctrno.Size = new System.Drawing.Size(155, 22);
            this.cmb_ctrno.TabIndex = 126;
            this.cmb_ctrno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_ctrno_KeyDown);
            // 
            // Import_inv_From_Pos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(516, 335);
            this.Controls.Add(this.cmb_ctrno);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_tmdate);
            this.Controls.Add(this.txt_fmdate);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.label8);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Import_inv_From_Pos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "جلب الفواتير";
            this.Load += new System.EventHandler(this.Price_Chang_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox txt_tmdate;
        private System.Windows.Forms.MaskedTextBox txt_fmdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_ctrno;
    }
}