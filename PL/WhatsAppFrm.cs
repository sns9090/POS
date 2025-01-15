//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Data.SqlClient;

using System.IO;
using System.Net;
//using RestSharp;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
//using Newtonsoft.Json;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace POS
{
	public partial class WhatsAppFrm : Form
	{
		private string whatsurl = "https://business.enjazatik.com/";
		private int timer_counter = 0;
		private int modvalue = 17;


		private int k;
		private string imagepath = "C:\\inetpub\\wwwroot\\storage\\";
		private string logppath = "C:\\work\\whatsapp\\custlogo\\";

		private string enjaztoken = "enj";


		private int mywhatstype;
		private DataTable tb;
		private Queue qt = new Queue();
		internal delegate void SetTextCallback(string text);
		private int batchSize = 4; // The number of rows to process in each batch



		private void SetText(string text)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (this.txtlog.InvokeRequired)
			{

				SetTextCallback d = new SetTextCallback(SetText);
				this.Invoke(d, new object[] {text});
			}
			else
			{
				///text = text.Trim(); 
				txtlog.Text += text;
			}
		}



		public void writelog(string source, string st)
		{
			SetText(Environment.NewLine + source + "- " + st);
		}


		private void TextBox1_TextChanged(System.Object sender, System.EventArgs e)
		{

		}


		private static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}



		public string SendWhatssms(string mobiel_no, string msg, string tkn, int whatstype)
		{
			string st1 = "whats";
			ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
			ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(48 | 192 | 768 | 3072);


			string url = "https://business.enjazatik.com/api/v1/send-message";


			try
			{
				msg = msg.Replace(Environment.NewLine, "\\n");
				msg = msg.Replace("\n", "\\n");
				msg = msg.Replace("\r", "\\n");
			

				string RequestBody = "";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
               
				req.ContentType = "application/json";
				RequestBody = "{\"number\":\"" + mobiel_no + "\",\"message\":\"" + msg + "\"}";
				//   RequestBody = "{""data"":[{""phone"":""" & mobiel_no & """,""message"":""" & msg & """,""isGroup"":false}]}"
				req.ContentType = "application/json";
				byte[] ar = Encoding.UTF8.GetBytes(RequestBody);
				req.Headers["Authorization"] = "Bearer " + tkn;
				req.Method = "POST";
				req.ContentLength = ar.Length;
				req.Timeout = 220000;
				Stream mstream = req.GetRequestStream();
				mstream.Write(ar, 0, ar.Length);
				mstream.Close();
				System.Net.HttpWebResponse webResponse = null;
				try
				{
                    webResponse = (HttpWebResponse)req.GetResponse();
				}
				catch (System.Net.WebException ex)
				{
					if (ex.Response != null)
					{
                        webResponse = (HttpWebResponse)ex.Response;
					}
				}
				if (webResponse != null)
				{
					System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream());
					string s = reader.ReadToEnd();
					req = null;
					webResponse = null;
					reader.Dispose();

					writelog("sendwhatssms", " " + s);
					return s.Trim();
				}
				else
				{
					return "7878|unkown error webResponse is nothing";
				}
			}
			catch (Exception e1)
			{
				writelog("sendwhatssms", "error:" + e1.Message);
			}
//INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
			return null;
		}

		public string sendMedia(string mobiel_no, string msg, string filename, string tkn, int whatstype)
		{
			ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
			ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(48 | 192 | 768 | 3072);
			try
			{
				string whatsurl = "https://enjazwhatsapp.com/api/v1/send-media";
				ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
				ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(48 | 192 | 768 | 3072);

					whatsurl = "https://business.enjazatik.com/api/v1/send-media";


				msg = msg.Replace(Environment.NewLine, "\\n");
				msg = msg.Replace("\n", "\\n");
				msg = msg.Replace("\r", "\\n");

				string RequestBody = "";
				string fileurl = filename;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(whatsurl);
				req.ContentType = "application/json";
				RequestBody = "{";
				RequestBody += "\"urlfile\": \"" + fileurl + "\",";
				RequestBody += "\"message\":\"" + msg + "\",";
				RequestBody += "\"number\": \"" + mobiel_no + "\"}";
				req.ContentType = "application/json";
				byte[] ar = Encoding.UTF8.GetBytes(RequestBody);
				req.Headers["Authorization"] = "Bearer " + tkn;
				req.Method = "POST";
				req.ContentLength = ar.Length;
				req.Timeout = 220000;
				Stream mstream = req.GetRequestStream();
				mstream.Write(ar, 0, ar.Length);
				mstream.Close();
				System.Net.HttpWebResponse webResponse = null;
				try
				{
                    webResponse = (HttpWebResponse)req.GetResponse();
				}
				catch (System.Net.WebException ex)
				{
					if (ex.Response != null)
					{
                        webResponse = (HttpWebResponse)ex.Response;
					}
				}
				if (webResponse != null)
				{
					System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream());
					string s = reader.ReadToEnd();
					req = null;
					webResponse = null;
					reader.Dispose();
					return s.Trim();
				}
				else
				{
					return "7878|unkown error webResponse is nothing";
				}
			}
			catch (Exception ex2)
			{
				return "9090|" + ex2.Message;
			}
		}













		private void Button1_Click(System.Object sender, System.EventArgs e)
		{


		}











		//Public Function getKey(ByVal key As String) As String
		//    Dim p As String = Application.StartupPath & "\config.ini"
		//    Dim f As New IniFile(p)
		//    Dim c As String = f.ReadValue("Setting", key)
		//    If c = "" Then
		//        c = "0"
		//    End If
		//    Return c
		//End Function

		private void Form1_Load(System.Object sender, System.EventArgs e)
		{
            GetSetting();




		

		}



		public WhatsAppFrm()
		{

			// This call is required by the Windows Form Designer.
			InitializeComponent();



			// Add any initialization after the InitializeComponent() call.

		}

		private void Button2_Click(System.Object sender, System.EventArgs e)
		{
			txtlog.Text = "";
		}

        public static messageResult DeserializeMessageResult(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(messageResult));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            messageResult obj = (messageResult)ser.ReadObject(ms);
            return obj;
        }

		//private async void Button3_Click(System.Object sender, System.EventArgs e)
       private  void Button3_Click(System.Object sender, System.EventArgs e)
		{
            try
            {
                string msg = txtMessage.Text;
                msg = SendWhatssms(txtmobile_no.Text, msg, txttocken.Text, 0);
                try
                {
                    messageResult obj = DeserializeMessageResult(msg);
                    txtmessageId.Text = obj.messageId;
                }
                catch (Exception ex)
                {

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

		}

		private void Button4_Click(System.Object sender, System.EventArgs e)
		{
		}

		private void ListBox1_SelectedIndexChanged(System.Object sender, System.EventArgs e)
		{

		}

		private void Button5_Click(System.Object sender, System.EventArgs e)
		{


		}

		private void txtlog_TextChanged(System.Object sender, System.EventArgs e)
		{
			if (txtlog.Lines.Count() > 500)
			{
				txtlog.Text = "";
			}
		}























		private void Form1_Closed(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void Button1_Click_1(object sender, EventArgs e)
		{

		}

		private void Button4_Click_1(object sender, EventArgs e)
		{

		}

		public string ScanQR(string tkn)
		{
			string st1 = "whats";
			ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
			ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(48 | 192 | 768 | 3072);


			string url = whatsurl + "/api/device/qr-code";


			try
			{

				object watch = System.Diagnostics.Stopwatch.StartNew();

				string RequestBody = "";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);



				req.Headers["Authorization"] = "Bearer " + tkn;
				req.Method = "GET";
				req.ContentLength = 0;
				req.Timeout = 220000;

				System.Net.HttpWebResponse webResponse = null;
				try
				{
                    webResponse = (HttpWebResponse)req.GetResponse();
				}
				catch (System.Net.WebException ex)
				{
					if (ex.Response != null)
					{
                        webResponse = (HttpWebResponse)ex.Response;
					}
				}
				if (webResponse != null)
				{
					System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream());
					string s = reader.ReadToEnd();
					req = null;
					webResponse = null;
					reader.Dispose();

					writelog("sendwhatssms",  " " + s);
					return s.Trim();
				}
				else
				{
					return "7878|unkown error webResponse is nothing";
				}
			}
			catch (Exception e1)
			{
				writelog("sendwhatssms", "error:" + e1.Message);
			}
//INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
			return null;
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


		private void Button6_Click(object sender, EventArgs e)
		{

			modvalue = 17;
			Timer1.Enabled = true;
			timer_counter = 1;
			string st = ScanQR(txttocken.Text);
			if ((st + "").Length > 40)
			{
				if (st.Contains(""))
				{

				}
				Rootobject cls = JsonDeserializeQr(st);
				if (cls.message == "ready")
				{
					Timer1.Enabled = false;
					PictureBox1.Image = null;
					lblstatus.Text = "Device Connected";
					MessageBox.Show("الجهاز متصل مسبقا ");
					return;

				}
				ViewImage(cls.src);
			}
			txtlog.Text += st;
		}


		public string QrInfo(string tkn)
		{
			string st1 = "whats";
			ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
			ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(48 | 192 | 768 | 3072);


			string url = whatsurl + "/api/device/info";


			try
			{


				string RequestBody = "";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);



				req.Headers["Authorization"] = "Bearer " + tkn;
				req.Method = "GET";
				req.ContentLength = 0;
				req.Timeout = 220000;

				System.Net.HttpWebResponse webResponse = null;
				try
				{
                    webResponse = (HttpWebResponse)req.GetResponse();
				}
				catch (System.Net.WebException ex)
				{
					if (ex.Response != null)
					{
                        webResponse = (HttpWebResponse)ex.Response;
					}
				}
				if (webResponse != null)
				{
					System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream());
					string s = reader.ReadToEnd();
					req = null;
					webResponse = null;
					reader.Dispose();

					writelog("sendwhatssms",  " " + s);
					return s.Trim();
				}
				else
				{
					return "7878|unkown error webResponse is nothing";
				}
			}
			catch (Exception e1)
			{
				writelog("sendwhatssms", "error:" + e1.Message);
			}
//INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
			return null;
		}

		public static devicestatus JsonDeserialize(string jsonString)
		{
			DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(devicestatus));
			MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
			devicestatus obj = (devicestatus)ser.ReadObject(ms);
			return obj;
		}
		private void Button7_Click(object sender, EventArgs e)
		{
			string st = QrInfo(txttocken.Text);
			devicestatus cls = JsonDeserialize(st);

			txtlog.Text = st;
		}

		private void Timer1_Tick(object sender, EventArgs e)
		{
			timer_counter = (timer_counter + 1) % modvalue;
			lblstatus.Text = "Waiting for scaning QR ..." + timer_counter;
			if (timer_counter == 0)
			{
				string st = QrInfo(txttocken.Text);
				modvalue = 11;
				devicestatus cls = JsonDeserialize(st);
				if (cls.data.status == "connected")
				{
					Timer1.Enabled = false;
					PictureBox1.Image = null;
					lblstatus.Text = "Device Connected";
				}
			}
		}



		private void Button5_Click_1(object sender, EventArgs e)
		{
			string st = sendMedia("967" + txtmobile_no.Text, txtMessage.Text, txtfileURl.Text, txttocken.Text, 0);
			txtlog.Text = st;
		}

		private void Button8_Click(object sender, EventArgs e)
		{
			try
			{

				OpenFileDialog1.Filter = "All Files|*.*|Bitmaps|*.bmp|GIFs|*.gif|JPEGs|*.jpg";
				if (OpenFileDialog1.ShowDialog() != 0)
				{

					// ResizeImage(Image.FromFile(OpenFileDialog1.FileName), 355, 250)
					txtfilepath.Text = OpenFileDialog1.FileName;

				}

			}
			catch (Exception ex)
			{

			}
		}

		public string SendMediaFromDisk(string mobiel_no, string msg, string filepath, string tkn, int whatstype)
		{
            string whatsurl = null;
            ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(48 | 192 | 768 | 3072);
            whatsurl = "https://business.enjazatik.com/api/v1/send-media";

            try
            {

                dynamic client = new RestSharp.RestClient(whatsurl);
                client.Timeout = -1;
                dynamic request = new RestSharp.RestRequest(RestSharp.Method.POST);
                request.AddHeader("Authorization", "Bearer " + tkn);
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Content-Type", "multipart/form-data");
                request.AddParameter("message", msg);
                request.AddParameter("type", "media");
                request.AddParameter("number", mobiel_no);
                request.AddFile("file", filepath);
                RestSharp.IRestResponse response = client.Execute(request);
                //   Console.WriteLine(response.Content)
                //     writelog("sendwhatssms", response.Content)
                return response.Content;
            }
            catch (Exception e1)
            {
                writelog("sendwhatimage", "error:" + e1.Message);
            }
            //INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
			return null;
		}

		private void Button4_Click_2(object sender, EventArgs e)
		{
            //966537422638,966594647848
           // string st = SendMediaFromDisk("967" + txtmobile_no.Text, txtMessage.Text, txtfilepath.Text, txttocken.Text, 0);
            string st = SendMediaFromDisk( txtmobile_no.Text, txtMessage.Text, txtfilepath.Text, txttocken.Text, 0);

            txtlog.Text = st;
		}

        private void button1_Click_2(object sender, EventArgs e)
        {
            txttocken.Text = "";
        }
        public string DeleteMessage(string mobiel_no, string tkn, string messageId)
        {
            string st1 = "whats";
            ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(48 | 192 | 768 | 3072);
            string url = "https://business.enjazatik.com/api/v1/delete-message";
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                string RequestBody = "";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.ContentType = "application/json";
                RequestBody = "{\"messageId\":\"" + messageId + "\",\"number\":\"" + mobiel_no + "\", \"deleteFromMe\":true}";
                //   RequestBody = "{""data"":[{""phone"":""" & mobiel_no & """,""message"":""" & msg & """,""isGroup"":false}]}"
                req.ContentType = "application/json";
                byte[] ar = Encoding.UTF8.GetBytes(RequestBody);
                req.Headers["Authorization"] = "Bearer " + tkn;
                req.Method = "POST";
                req.ContentLength = ar.Length;
                req.Timeout = 220000;
                Stream mstream = req.GetRequestStream();
                mstream.Write(ar, 0, ar.Length);
                mstream.Close();
                System.Net.HttpWebResponse webResponse = null;
                try
                {
                    webResponse = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException ex)
                {
                    if (ex.Response != null)
                    {
                        webResponse = (HttpWebResponse)ex.Response;
                    }
                }
                if (webResponse != null)
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream());
                    string s = reader.ReadToEnd();
                    req = null;
                    webResponse = null;
                    reader.Dispose();

                    writelog("isNumberRegister", watch.ElapsedMilliseconds + " " + s);
                    return s.Trim();
                }
                else
                {
                    return "7878|unkown error webResponse is nothing";
                }
            }
            catch (Exception e1)
            {

            }
            //INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
            return null;
        }

        public string IsNumberRegister(string mobiel_no, string tkn)
        {
            string st1 = "whats";
            ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(48 | 192 | 768 | 3072);
            string url = "https://business.enjazatik.com/api/v1/check-number";
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                string RequestBody = "";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.ContentType = "application/json";
                RequestBody = "{\"number\":\"" + mobiel_no + "\"}";
                //   RequestBody = "{""data"":[{""phone"":""" & mobiel_no & """,""message"":""" & msg & """,""isGroup"":false}]}"
                req.ContentType = "application/json";
                byte[] ar = Encoding.UTF8.GetBytes(RequestBody);
                req.Headers["Authorization"] = "Bearer " + tkn;
                req.Method = "POST";
                req.ContentLength = ar.Length;
                req.Timeout = 220000;
                Stream mstream = req.GetRequestStream();
                mstream.Write(ar, 0, ar.Length);
                mstream.Close();
                System.Net.HttpWebResponse webResponse = null;
                try
                {
                    webResponse = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException ex)
                {
                    if (ex.Response != null)
                    {
                        webResponse = (HttpWebResponse)ex.Response;
                    }
                }
                if (webResponse != null)
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream());
                    string s = reader.ReadToEnd();
                    req = null;
                    webResponse = null;
                    reader.Dispose();

                    writelog("isNumberRegister", watch.ElapsedMilliseconds + " " + s);
                    return s.Trim();
                }
                else
                {
                    return "7878|unkown error webResponse is nothing";
                }
            }
            catch (Exception e1)
            {

            }
            //INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
            return null;
        }
        private void Button9_Click(object sender, EventArgs e)
        {
            string st = IsNumberRegister(txtmobile_no.Text, txttocken.Text);
            txtlog.Text += Environment.NewLine + st;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            string st = DeleteMessage(txtmobile_no.Text, txttocken.Text, txtmessageId.Text);
            txtlog.Text += Environment.NewLine + st;

        }
        void SaveSetting(string tocken)
        {
            try
            {

                string subKey = "Software\\mywhatapp";
                string keyName = "tocket";


                RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(subKey);
                registryKey.SetValue(keyName, tocken);
                registryKey.Close();
            }
            catch { }
        }

        void GetSetting()
        {
            try
            {
                string subKey = "Software\\mywhatapp";
                string keyName = "tocket";


                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(subKey);
                if (registryKey != null)
                {
                    string value = registryKey.GetValue(keyName).ToString();
                    txttocken.Text = value;
                    registryKey.Close();
                }
            }
            catch { }
        }

        private void button11_Click(object sender, EventArgs e)
        {
           SaveSetting(txttocken.Text);
        }

        public string SendWhatssmsMulti(string mobiel_list, string msg, string tkn, int whatstype)
        {
           
            string st1 = "whats";
            ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(48 | 192 | 768 | 3072);

            //string url = "https://business.enjazatik.com/api/v1/send-message";
            string url = "https://business.enjazatik.com/api/v1/scheduler/send-message?token=" + tkn;


            try
            {
                string destinations = "[";
                msg = msg.Replace(Environment.NewLine, "\\n");
                msg = msg.Replace("\n", "\\n");
                msg = msg.Replace("\r", "\\n");
                string [] phones=mobiel_list.Split(',') ;
                if (phones.Length >= 1)
                { 
                 for (int i=0;i<phones.Length ;i++)
                 {  if (phones[0].Length >5)
                 { 
                     if (i > 0) destinations += ",";
                     destinations += "\"" + phones[i] + "\"";
                 }
                 }
                }
                destinations += "]";
                string RequestBody = "";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.ContentType = "application/json";
                RequestBody = "{\"destinations\":" + destinations + ",\"message\":\"" + msg + "\",  \"delay\":5";
                RequestBody += ",\"contactGroups\":[] ,\"repeat\":1,\"repeatPeriod\":1,\"auto_time_enabled\":false,\"skip_reg_error\":true}";

   

             
                req.ContentType = "application/json";
                byte[] ar = Encoding.UTF8.GetBytes(RequestBody);
             //   req.Headers["Authorization"] = "Bearer " + tkn;
                req.Method = "POST";
                req.ContentLength = ar.Length;
                req.Timeout = 220000;
                Stream mstream = req.GetRequestStream();
                mstream.Write(ar, 0, ar.Length);
                mstream.Close();
                System.Net.HttpWebResponse webResponse = null;
                try
                {
                    webResponse = (HttpWebResponse)req.GetResponse();
                }
                catch (System.Net.WebException ex)
                {
                    if (ex.Response != null)
                    {
                        webResponse = (HttpWebResponse)ex.Response;
                    }
                }
                if (webResponse != null)
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream());
                    string s = reader.ReadToEnd();
                    req = null;
                    webResponse = null;
                    reader.Dispose();

                    writelog("sendwhatssms", " " + s);
                    return s.Trim();
                }
                else
                {
                    return "7878|unkown error webResponse is nothing";
                }
            }
            catch (Exception e1)
            {
                writelog("sendwhatssms", "error:" + e1.Message);
            }
            //INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
            return null;
        }
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = txtMessage.Text;
                msg = SendWhatssmsMulti(txtmobile_list.Text, msg, txttocken.Text, 0);
                try
                {
                    messageResult obj = DeserializeMessageResult(msg);
                    txtmessageId.Text = obj.messageId;
                }
                catch (Exception ex)
                {

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog of = new System.Windows.Forms.OpenFileDialog();
            of.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            DialogResult dr = of.ShowDialog();
            string std = "";
            if (dr == DialogResult.OK)
            {
                //     if (!string.IsNullOrEmpty(of.FileName))
                // {
                // MessageBox.Show(of.FileName);

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                Excel.Range range;

                string str = "";
                int rCnt;
                int cCnt;
                int rw = 0;
                int cl = 0;

                xlApp = new Excel.Application();
                // xlWorkBook = xlApp.Workbooks.Open(@"d:\csharp-Excel.xls", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkBook = xlApp.Workbooks.Open(of.FileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)(xlWorkBook.Worksheets.get_Item(1)); //(Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;
                rw = range.Rows.Count;
                cl = range.Columns.Count;


                // for (rCnt = 1; rCnt <= rw; rCnt++)
                // for (int i =  2; i <= rw; i++)
                for (int i = rw; i >= 2; i--)
                {
                    //for (cCnt = 1; cCnt <= cl; cCnt++)
                    //{
                    //    str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                    //    MessageBox.Show(str);
                    //}



                    // str = (string)(xlWorkSheet.Cells[i, 1] as Excel.Range).Value2; // xlApp.Cells[i, 1]; // (string)(range.Cells[i, 1] as Excel.Range).Value2;
                    str = (xlWorkSheet.Cells[i, 1]).Value.ToString() + "," + str;
                    // MessageBox.Show(str);
                    //  std = (string)(range.Cells[rw, 0] as Excel.Range).Value2 + "," + std;

                }
                // MessageBox.Show(str.Substring(0, str.Length - 1));
                txtmobile_list.Text = str.Substring(0, str.Length - 1);

                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
            }
        }

        public string SendMediaFromDiskMultiPhone(string mobiel_list, string msg, string filepath, string tkn, int whatstype)
        {
            string whatsurl = null;
            ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(48 | 192 | 768 | 3072);
            whatsurl = "https://business.enjazatik.com/api/v1/send-media";
            whatsurl = "https://business.enjazatik.com/api/v1/scheduler/send-media?token=" + tkn;
            try
            {
                string destinations = "[";
                string[] phones = mobiel_list.Split(',');
                if (phones.Length >= 1)
                {
                    for (int i = 0; i < phones.Length; i++)
                    {
                        if (phones[0].Length > 5)
                        {
                            if (i > 0) destinations += ",";
                            destinations += "\"" + phones[i] + "\"";
                        }
                    }
                }
                destinations += "]";

                dynamic client = new RestSharp.RestClient(whatsurl);
                client.Timeout = -1;
                dynamic request = new RestSharp.RestRequest(RestSharp.Method.POST);
                request.AddHeader("Authorization", "Bearer " + tkn);
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Content-Type", "multipart/form-data");
                request.AddParameter("message", msg);
                request.AddParameter("type", "media");
                request.AddParameter("destinations", destinations);
                request.AddParameter("delay", "10");
                request.AddParameter("repeat", "1");
                request.AddParameter("contactGroups", "[]");
                request.AddParameter("repeatPeriod", 1);
                request.AddParameter("skip_reg_error", 1);
                request.AddParameter("auto_time_enabled", 0);
                request.AddFile("file", filepath);
                RestSharp.IRestResponse response = client.Execute(request);
                //   Console.WriteLine(response.Content)
                //     writelog("sendwhatssms", response.Content)
                return response.Content;
            }
            catch (Exception e1)
            {
                writelog("sendwhatimage", "error:" + e1.Message);
            }
            //INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
            return null;
        }
        private void button14_Click(object sender, EventArgs e)
        {
            string st = SendMediaFromDiskMultiPhone(txtmobile_list.Text, txtMessage.Text, txtfilepath.Text, txttocken.Text, 0);
            txtlog.Text += st + Environment.NewLine;
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {

        }

    }


}