using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastReport;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Globalization;

namespace POS
{
    public partial class FR_Viewer : BaseForm
    {
        BL.Date_Validate datval = new BL.Date_Validate();
        Report rpt1;
        public FR_Viewer(Report rpt)
        {
            InitializeComponent();
            rpt.Preview = previewControl1;
            rpt1 = rpt;
           // rpt.Prepare();
           // FastReport.Utils.Config.PreviewSettings.Buttons = PreviewButtons.Print | PreviewButtons.Edit;
           // rpt.ShowPrepared();
            rpt.Show();
        }

        private void FR_Viewer_Load(object sender, EventArgs e)
        {
           

            textBox3.Text = string.IsNullOrEmpty(BL.CLS_Session.whatsappmsg) ? textBox3.Text : BL.CLS_Session.whatsappmsg;
           // previewControl1.TopLevelControl. = false;
            ////if (BL.CLS_Session.sysno.Equals("s") || BL.CLS_Session.sysno.Equals("S") || BL.CLS_Session.sysno.Equals("e") || BL.CLS_Session.sysno.Equals("E") || BL.CLS_Session.sysno.Equals("l") || BL.CLS_Session.sysno.Equals("L"))
            ////{
            ////    button1.Visible = false;
            ////    textBox1.Visible = false;
            ////}
            //if (!BL.CLS_Session.whatsappactv)
            //    button1.Visible = false;
            if(BL.CLS_Session.whatsappactv)
                textBox3.Enabled = true;
            else
                textBox3.Enabled = false;
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
            PictureBox1.Image = im;
            //INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            WhatsAppFrm sd = new WhatsAppFrm();
            string mob = textBox1.Text.StartsWith("05") ? "966" + textBox1.Text.Substring(1, 9) : textBox1.Text;
            // MessageBox.Show(rpt1.FileName);
            if (!Directory.Exists(Environment.CurrentDirectory + "\\temp"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\temp");
            string fnam = string.Format("{0:yyyyMMdd_HHmmssfff}", DateTime.Now);
            // sd.MdiParent = MdiParent;
            // sd.SendMediaFromDisk(txtmobile_no.Text, txtMessage.Text, txtfilepath.Text, txttocken.Text, 0);
            FastReport.Export.Pdf.PDFExport pdf = new FastReport.Export.Pdf.PDFExport();
            rpt1.Export(pdf, Environment.CurrentDirectory + "\\temp\\inv_" + fnam + ".pdf");
            
           // sd.SendMediaFromDisk(mob, sd.txtMessage.Text, Environment.CurrentDirectory + "\\Reportwf.pdf", sd.txttocken.Text, 0);

            sd.SendMediaFromDisk(mob, BL.CLS_Session.cmp_name + "\n\r" + Environment.NewLine + textBox3.Text, Environment.CurrentDirectory + "\\temp\\inv_" + fnam + ".pdf", BL.CLS_Session.whatsapptokn, 0);
           // MessageBox.Show(" تم ارسال الرسالة بنجاح ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\r" +  " صادف خطا خلال الارسال ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WhatsAppFrm sd = new WhatsAppFrm();
            //string st = sd.ScanQR(sd.txttocken.Text);
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
                    PictureBox1.Image = null;
                    // lblstatus.Text = "Device Connected";
                    // MessageBox.Show("الجهاز متصل مسبقا ");

                    MessageBox.Show(" سيتم ارسال الرسالة خلال لحضات ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Thread ab2 = new Thread(() => thread_send_wa_msg());// new Thread(new ThreadStart(tc.thread1(cmb_type.SelectedValue.ToString(), txt_ref.Text.ToString(), cmb_salctr.SelectedValue.ToString())));
                    ab2.Start();

                    return;

                }
                panel3.Visible = false;
                panel1.Visible = true;
                ViewImage(cls.src);
            }
           
        }

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3.Focus();
                button3_Click(sender, e);
            }
        }

        private void FR_Viewer_Shown(object sender, EventArgs e)
        {
            textBox1.Text = !string.IsNullOrEmpty(BL.CLS_Session.cust_mob) ? BL.CLS_Session.cust_mob : "";

            if (Convert.ToDouble(datval.convert_to_yyyyDDdd(Convert.ToDateTime((BL.CLS_Session.minstart.Substring(4, 2) + "/" + BL.CLS_Session.minstart.Substring(6, 2) + "/" + BL.CLS_Session.minstart.Substring(0, 4)), new CultureInfo("en-US", false)).AddDays(365).ToString())) < Convert.ToDouble(DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false))))
            {
                MessageBox.Show(" بيانات الصيانه مفقودة يرجى التواصل مع الدعم الفني لتحديثها ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }
    }
}
