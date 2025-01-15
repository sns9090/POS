using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace POS
{
    public partial class SendMail : BaseForm
    {
        MailMessage message;
        SmtpClient smtp;
        public SendMail()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSubject.Text) || string.IsNullOrEmpty(txtBody.Text) || string.IsNullOrEmpty(txt_sendernam.Text) || string.IsNullOrEmpty(txt_senderemail.Text) || string.IsNullOrEmpty(txt_sendermob.Text))
            {
                MessageBox.Show("اكمل الحقول المطلوبة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {

                btnSend.Enabled = false;

                btnCancel.Visible = true;

                message = new MailMessage();



                if (IsValidEmail(txtTo.Text))
                {

                    message.To.Add(txtTo.Text);

                }



                if (IsValidEmail(txtCC.Text))
                {

                    message.CC.Add(txtCC.Text);

                }

                message.Subject = txtSubject.Text;

                message.From = new MailAddress("treesoft@yahoo.com");

                message.IsBodyHtml = true;
              //  message.Body = txtBody.Text.Replace(Environment.NewLine, "<br>");
                string rtl = BL.CLS_Session.lang.Equals("E") ? "align='left'" : "align='right'";
                message.Body = "<table width='100%' style='font-family:Arial; font-size:16pt;'><tr><td " + rtl + ">" + txtBody.Text.Replace(Environment.NewLine, "<br>") + "<br> <br> <br>" + txt_sendernam.Text + "<br>" + txt_senderemail.Text + "<br>" +txt_sendermob.Text + "</td></tr></table></font>";
            

                if (lblAttachment.Text.Length > 0)
                {

                    if (System.IO.File.Exists(lblAttachment.Text))
                    {

                        message.Attachments.Add(new Attachment(lblAttachment.Text));

                    }

                }



                // set smtp details

                smtp = new SmtpClient("smtp.mail.yahoo.com");

                smtp.Port = 587;// 25;

                smtp.EnableSsl = true;

                smtp.Credentials = new NetworkCredential("treesoft@yahoo.com", "xeibxoswdsublxcr");

                smtp.SendAsync(message, message.Subject);

                smtp.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                btnCancel.Visible = false;

                btnSend.Enabled = true;

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            smtp.SendAsyncCancel();

            MessageBox.Show("Email sending cancelled!","",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        void smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {

            if (e.Cancelled == true)
            {

                MessageBox.Show("Email sending cancelled!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            else if (e.Error != null)
            {

                MessageBox.Show(e.Error.Message);

            }

            else
            {

                MessageBox.Show("Email sent sucessfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                txtBody.Enabled = false;
                txtTo.Enabled = false;
                txtCC.Enabled = false;
                txtSubject.Enabled = false;
                btnSend.Enabled = false;

            }



            btnCancel.Visible = false;

           // btnSend.Enabled = true;

        }

        private void lnkAttachFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {

                lblAttachment.Text = openFileDialog1.FileName;

                lblAttachment.Visible = true;

                lnkAttachFile.Visible = false;
                label5.Visible = false;

            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        //////private void Form1_KeyDown(object sender, KeyEventArgs e)
        //////{
        //////    if (e.KeyCode == Keys.Enter)
        //////    {

        //////        if (this.GetNextControl(ActiveControl, true) != null)
        //////        {
        //////            e.Handled = true;
        //////            this.GetNextControl(ActiveControl, true).Focus();
        //////        }
        //////    }
        //////}

        ////protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        ////{
        ////    if (keyData == (Keys.Enter))
        ////    {
        ////        SendKeys.Send("{TAB}");
        ////    }

        ////    return base.ProcessCmdKey(ref msg, keyData);
        ////}

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //switch (e.KeyChar)
            //{
            //    case (char)Keys.Enter:
            //        SendKeys.Send("{Tab}");
            //        break;
            //    default:
            //        break;
            //}
        }

        private void txtSubject_Enter(object sender, EventArgs e)
        {
            if(!BL.CLS_Session.lang.Equals("E"))
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
        }

        private void txtBody_Enter(object sender, EventArgs e)
        {
            if (!BL.CLS_Session.lang.Equals("E"))
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
        }

        private void SendMail_Load(object sender, EventArgs e)
        {
            txt_sendernam.Select();
        }


    }
}
