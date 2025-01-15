using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Net.Mail;
using System.IO;
using System.Configuration;
using System.Net;
using System.Globalization;
using System.Runtime.Serialization.Json;
using System.Threading;
//using System.Windows.Input;

namespace POS.Acc.rpt
{
    public partial class Acc_frmPrint : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.EncDec enco = new BL.EncDec();
        string exttype, extn;
        public Acc_frmPrint()
        {
            InitializeComponent();
        }

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    base.OnKeyDown(e);
        //    if ((e.Key == Key.P) && (Keyboard.IsKeyDown(Key.LeftCtrl) ||
        //          Keyboard.IsKeyDown(Key.RightCtrl)))
        //        reportViewer1.PrintDialog();
        //}

        private void Acc_frmPrint_Load(object sender, EventArgs e)
        {
            textBox3.Text = string.IsNullOrEmpty(BL.CLS_Session.whatsappmsg) ? textBox3.Text : BL.CLS_Session.whatsappmsg;

            if (BL.CLS_Session.whatsappactv)
                textBox3.Enabled = true;
            else
                textBox3.Enabled = false;

            //if (BL.CLS_Session.sysno.Equals("s") || BL.CLS_Session.sysno.Equals("S") || BL.CLS_Session.sysno.Equals("e") || BL.CLS_Session.sysno.Equals("E") || BL.CLS_Session.sysno.Equals("l") || BL.CLS_Session.sysno.Equals("L") )
            //{
            //    button2.Visible = false;
            //    textBox1.Visible = false;
            //}

            if (BL.CLS_Session.islight)
            {
                btn_mail.Visible = false;
               // button2.Visible = false;
              //  textBox1.Visible = false;
            }
            //if(!BL.CLS_Session.whatsappactv)
            //    button2.Visible = false;
            try
            {
                if (BL.CLS_Session.dtemail == null)
                    BL.CLS_Session.dtemail = daml.SELECT_QUIRY_only_retrn_dt("select * from email");

                //if (BL.CLS_Session.sysno.Equals("1"))
                //    btn_mail.Visible = false;

                comboBox1.SelectedIndex = 0;
                //  this.reportViewer1.RefreshReport();
                //  reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                //reportViewer1.ZoomPercent = ;
                txt_body.AppendText(BL.CLS_Session.UserName);
                txt_body.AppendText(Environment.NewLine);
                txt_body.AppendText(BL.CLS_Session.cmp_name + " - " + BL.CLS_Session.brname);

                txt_toemail.Text = BL.CLS_Session.lang.Equals("E") ? "Enter Email To Send" : "ادخل الايميل";

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsValidEmail(txt_toemail.Text))
            {
                MessageBox.Show("ايميل غير صالح", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_toemail.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txt_toemail.Text) || comboBox1.SelectedIndex == -1 || string.IsNullOrEmpty(txt_body.Text))
            {
                MessageBox.Show("يرجى تعبئة كل البيانات", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBox1.SelectedIndex == 0)
            {
                exttype = "PDF";
                extn = "pdf";
            }
            if (comboBox1.SelectedIndex == 1)
            {
                exttype = "Excel";
                extn = "xlsx";
            }
            if (comboBox1.SelectedIndex == 2)
            {
                exttype = "Word";
                extn = "docx";
            }


            SendMail(reportViewer1);
            MessageBox.Show("Sent Success", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            panel1.Visible = false;
        }

        private void SendMail(ReportViewer reportViewer)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = reportViewer.LocalReport.Render
                          (exttype, null, out mimeType, out encoding, out extension, out 
		                   streamids, out warnings);

            MemoryStream memoryStream = new MemoryStream(bytes);
            memoryStream.Seek(0, SeekOrigin.Begin);

            MailMessage message = new MailMessage();
            Attachment attachment = new Attachment(memoryStream, "PosReport" + DateTime.Now.ToString("yyyyMMdd_HHmmss", new CultureInfo("en-US", false)) + "." + extn);
            message.Attachments.Add(attachment);

            // message.From = new MailAddress("info2pos@gmail.com");
            message.From = new MailAddress(BL.CLS_Session.dtemail.Rows[0][1].ToString());
            //  message.To.Add("sns9091@gmail.com");
            message.To.Add(txt_toemail.Text);

            //  message.CC.Add("info2pos@gmail.com");

            // message.Subject = "Pos Report";
            message.Subject = txt_supject.Text;
            message.IsBodyHtml = true;

            //  message.Body = BL.CLS_Session.lang.Equals("E") ? "Please find Attached Report herewith." : "يرجى التحميل من مرفقات الرسالة";
            // message.Body = "<p dir='rtl'><font size='6'>" + txt_body.Text.Replace(Environment.NewLine, "<br>") + "</font></p>";
            // message.Body = "<table width='100%' style='font-family:Arial; font-size:200%'><tr><td align='right'>" + txt_body.Text.Replace(Environment.NewLine, "<br>") + "</td></tr></table></font>";
            string rtl = BL.CLS_Session.lang.Equals("E") ? "align='left'" : "align='right'";
            message.Body = "<table width='100%' style='font-family:Arial; font-size:16pt;'><tr><td " + rtl + ">" + txt_body.Text.Replace(Environment.NewLine, "<br>") + "</td></tr></table></font>";

            //  if (ConfigurationManager.AppSettings["SendMail"].ToString() == "Y")
            string sss = "Y";
            if (sss == "Y")
            {
                // SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                SmtpClient smtp = new SmtpClient(BL.CLS_Session.dtemail.Rows[0][5].ToString());
                // smtp.Port = 25;
                smtp.Port = Convert.ToInt32(BL.CLS_Session.dtemail.Rows[0][3]);

                //smtp.EnableSsl = true;
                smtp.EnableSsl = Convert.ToBoolean(BL.CLS_Session.dtemail.Rows[0][4]);

                //smtp.Credentials = new NetworkCredential("info2pos@gmail.com", "Ss12345678");
                //  MessageBox.Show(enco.Decrypt(BL.CLS_Session.dtemail.Rows[0][2].ToString(), true));
                smtp.Credentials = new NetworkCredential(BL.CLS_Session.dtemail.Rows[0][1].ToString(), enco.Decrypt(BL.CLS_Session.dtemail.Rows[0][2].ToString(), true));

                smtp.Send(message);
            }
            else
            {
                //This is for testing.
                SmtpClient smtp = new SmtpClient();
                smtp.Send(message);
            }

            memoryStream.Close();
            memoryStream.Dispose();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (txt_toemail.Text.Equals("Enter Email To Send") || txt_toemail.Text.Equals("ادخل الايميل"))
                txt_toemail.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (txt_toemail.Text.Equals(""))
                txt_toemail.Text = BL.CLS_Session.lang.Equals("E") ? "Enter Email To Send" : "ادخل الايميل";
        }

        private void btn_mail_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            if (panel1.Visible == true)
                panel1.Visible = false;
            else
                panel1.Visible = true;
        }

        private void Acc_frmPrint_Shown(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void txt_body_Enter(object sender, EventArgs e)
        {
            if (!BL.CLS_Session.lang.Equals("E"))
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
        }

        private void txt_supject_Enter(object sender, EventArgs e)
        {
            if (!BL.CLS_Session.lang.Equals("E"))
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("AR"));
        }

        private void Acc_frmPrint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.P)
            //if ((e.KeyCode == Keys.P) && (Keyboard.IsKeyDown(Key.LeftCtrl)
            // if (e.KeyCode == Keys.P)
            {
                //cmb_cards.Select();
                //SendKeys.Send("{F4}");
                reportViewer1.PrintDialog();
            }
        }

        public static Rootobject JsonDeserializeQr(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Rootobject));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            Rootobject obj = (Rootobject)ser.ReadObject(ms);
            return obj;
        }

        public object ViewImage(string st)
        {
            string base64Data = st.Substring(st.IndexOf(",") + 1);
            byte[] imageBytes = Convert.FromBase64String(base64Data);
            MemoryStream ms = new MemoryStream(imageBytes);
            Image im = Image.FromStream(ms);
            pictureBox1.Image = im;
            //INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
            return null;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            if (panel3.Visible == true)
                panel3.Visible = false;
            else
            {
                panel3.Visible = true;
                textBox1.Focus();
                textBox1.SelectAll();
            }

           
        }
        public void thread_send_wa_msg()
        {
            try
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;

                byte[] bytes = reportViewer1.LocalReport.Render(
                    "PDF", null, out mimeType, out encoding, out filenameExtension,
                    out streamids, out warnings);

                if (!Directory.Exists(Environment.CurrentDirectory + "\\temp"))
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\temp");

                string fnam = string.Format("{0:yyyyMMdd_HHmmssfff}", DateTime.Now);

                using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\temp\\rpt_" + fnam + ".pdf", FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }
                WhatsAppFrm sd = new WhatsAppFrm();
                string mob = textBox1.Text.StartsWith("05") ? "966" + textBox1.Text.Substring(1, 9) : textBox1.Text;
                // MessageBox.Show(rpt1.FileName);

                // sd.MdiParent = MdiParent;
                // sd.SendMediaFromDisk(txtmobile_no.Text, txtMessage.Text, txtfilepath.Text, txttocken.Text, 0);
                FastReport.Export.Pdf.PDFExport pdf = new FastReport.Export.Pdf.PDFExport();
                // rpt1.Export( .Export(pdf, Environment.CurrentDirectory + "\\Reportwa.pdf");
               // sd.SendMediaFromDisk(mob, textBox3.Text, Environment.CurrentDirectory + "\\Reportv.pdf", sd.txttocken.Text, 0);

                sd.SendMediaFromDisk(mob, BL.CLS_Session.cmp_name + "\n\r" +  Environment.NewLine + textBox3.Text, Environment.CurrentDirectory + "\\temp\\rpt_" + fnam + ".pdf", BL.CLS_Session.whatsapptokn, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\r" + " صادف خطا خلال الارسال ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3.Focus();
                button3_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WhatsAppFrm sd = new WhatsAppFrm();
           // string st = sd.ScanQR(sd.txttocken.Text);
            string st = sd.ScanQR(BL.CLS_Session.whatsapptokn);

            if ((st + "").Length > 40)
            {
                if (st.Contains(""))
                {

                }
                Rootobject cls = JsonDeserializeQr(st);
                if (cls.message == "ready")
                {
                    sd.Timer1.Enabled = false;
                    pictureBox1.Image = null;
                    // lblstatus.Text = "Device Connected";
                    // MessageBox.Show("الجهاز متصل مسبقا ");

                    MessageBox.Show(" سيتم ارسال الرسالة خلال لحضات ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Thread ab2 = new Thread(() => thread_send_wa_msg());// new Thread(new ThreadStart(tc.thread1(cmb_type.SelectedValue.ToString(), txt_ref.Text.ToString(), cmb_salctr.SelectedValue.ToString())));
                    ab2.Start();

                    return;

                }
                panel3.Visible = false;
                panel2.Visible = true;
                ViewImage(cls.src);
            }

        }

        private void txt_body_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}
