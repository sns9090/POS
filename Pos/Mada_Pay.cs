using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
//using InterSoftModel;
//using SpanapiWrapper;
using System.Xml.Serialization;
using System.IO;
//using integrPOS;

namespace POS.Pos
{
    public partial class Mada_Pay : BaseForm
    {
        public string PaymentSuccessCode = "000";
        public string PaymentDeclinedCode = "116";
       // public string PortName = "COM7";

        private string getAmountHax(double amount)
        {
            string value = (System.Convert.ToDecimal((amount * 100))).ToString().PadLeft(12, '0');
            byte[] ba = Encoding.Default.GetBytes(value);
            var hexString = BitConverter.ToString(ba);
            hexString = hexString.Replace("-", "");
            return hexString;
        }
        public static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2 - 1 + 1];

            for (int i = 0; i <= NumberChars - 1; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

            return bytes;
        }

       // public int i;
        public bool its_ok=false;
        [DllImport(@"mapiv18.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr api_RequestCOMTrxn(int port, int Rate, int x, int y, int z, byte[] inOutBuff, byte[] intval, int trnxType, byte[] panNo, byte[] purAmount, byte[] stanNo, byte[] dataTime, byte[] expDate, byte[] trxRrn, byte[] authCode, byte[] rspCode, byte[] terminalId, byte[] schemeId, byte[] merchantId, byte[] addtlAmount, byte[] ecrrefno, byte[] version, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder outResp, byte[] outRespLen);

        public Mada_Pay()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.Text = BL.CLS_Session.dtyrper.Rows[0]["mada_portno"].ToString();
          //  MessageBox.Show(BL.CLS_Session.dtyrper.Rows[0]["mada_portno"].ToString());
            //webBrowser1.DocumentText =
            //    "<html><body>Please enter your name:<br/>" +
            //    "<input type='text' name='userName'/><br/>" +
            //    "<a href='http://www.microsoft.com'>continue</a>" +
            //    "</body></html>";
           // button1_Click(sender, e);
            textBox1.Text = String.Format("{0:0.00}", Convert.ToDouble(textBox1.Text));
            timer1.Start();

            if (!BL.CLS_Session.dtyrper.Rows[0]["mada_type"].ToString().Equals("0"))
            timer2.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_responce.Text = "";

            if (BL.CLS_Session.dtyrper.Rows[0]["mada_type"].ToString().Equals("0"))
            {
                

                string TextString;
                byte[] intval = new byte[1];
                byte[] panNo = new byte[23];
                byte[] purAmount = new byte[13];
                byte[] stanNo = new byte[7];
                byte[] dataTime = new byte[13];
                byte[] expDate = new byte[5];
                byte[] trxRrn = new byte[13];
                byte[] authCode = new byte[7];
                byte[] rspCode = new byte[4];
                byte[] terminalId = new byte[17];
                byte[] schemeId = new byte[3];
                byte[] merchantId = new byte[16];
                byte[] addtlAmount = new byte[13];
                byte[] ecrrefno = new byte[17];
                byte[] version = new byte[10];
                byte[] outRespLen = new byte[1];
                StringBuilder outResp = new StringBuilder(15000);


                int trnxType = 0;
                //if(BL.CLS_Session.dtyrper.Rows[0]["mada_type"].ToString().Equals("0"))
                //    TextString = (this.textBox1.Text.Trim().Equals("9.77") || this.textBox1.Text.Trim().Equals("8.77") ? float.Parse(this.textBox1.Text) / 10000 : float.Parse(this.textBox1.Text) * 100).ToString() + ";1;1!";
                //else
                //    TextString = (float.Parse(this.textBox1.Text) * 100).ToString() + ";1;1!";

                //byte[] inOutBuff = System.Text.Encoding.ASCII.GetBytes(TextString);
                //intval[0] = (byte)inOutBuff.Length;
                // TextString = (float.Parse(this.textBox1.Text) * 100).ToString() + ";1;1!";

                TextString = "" + (Convert.ToDouble(this.textBox1.Text) * 100).ToString() + ";1;1!";
                byte[] inOutBuff = System.Text.Encoding.ASCII.GetBytes(TextString);
                // intval[0] = (byte)inOutBuff.Length;
                intval[0] = Convert.ToByte(inOutBuff.Length);

                // var Result = api_RequestCOMTrxn(Int32.Parse(this.textBox2.Text.Trim()), 38400, 0, 8, 0, inOutBuff, intval, trnxType, panNo, purAmount, stanNo, dataTime, expDate, trxRrn, authCode, rspCode, terminalId, schemeId, merchantId, addtlAmount, ecrrefno, version, outResp, outRespLen);
                var Result = api_RequestCOMTrxn(Convert.ToInt32(this.textBox2.Text.Trim()), 38400, 0, 8, 0, inOutBuff, intval, trnxType, panNo, purAmount, stanNo, dataTime, expDate, trxRrn, authCode, rspCode, terminalId, schemeId, merchantId, addtlAmount, ecrrefno, version, outResp, outRespLen);

                //textBox3.Text=Result.ToString());
                // int i = Int32.Parse(Result.ToString());
                int i = Convert.ToInt32(Result.ToString());
                label1.Text = Convert.ToInt32(Result.ToString()).ToString();
                switch (i)
                {
                    case 0:
                        //////this.richTextBox1.AppendText("\n inOutBuff: " + System.Text.Encoding.UTF8.GetString(inOutBuff));
                        //////this.richTextBox1.AppendText("\n intval: " + System.Text.Encoding.UTF8.GetString(intval));
                        //////this.richTextBox1.AppendText("\n panNo: " + System.Text.Encoding.UTF8.GetString(panNo));
                        //////this.richTextBox1.AppendText("\n purAmount: " + System.Text.Encoding.UTF8.GetString(purAmount));
                        //////this.richTextBox1.AppendText("\n stanNo: " + System.Text.Encoding.UTF8.GetString(stanNo));
                        //////this.richTextBox1.AppendText("\n dataTime: " + System.Text.Encoding.UTF8.GetString(dataTime));
                        //////this.richTextBox1.AppendText("\n expDate: " + System.Text.Encoding.UTF8.GetString(expDate));
                        //////this.richTextBox1.AppendText("\n trxRrn: " + System.Text.Encoding.UTF8.GetString(trxRrn));
                        //////this.richTextBox1.AppendText("\n authCode: " + System.Text.Encoding.UTF8.GetString(authCode));
                        //////this.richTextBox1.AppendText("\n rspCode: " + System.Text.Encoding.UTF8.GetString(rspCode));
                        //////this.richTextBox1.AppendText("\n terminalId: " + System.Text.Encoding.UTF8.GetString(terminalId));
                        //////this.richTextBox1.AppendText("\n schemeId: " + System.Text.Encoding.UTF8.GetString(schemeId));
                        //////this.richTextBox1.AppendText("\n merchantId: " + System.Text.Encoding.UTF8.GetString(merchantId));
                        //////this.richTextBox1.AppendText("\n addtlAmount: " + System.Text.Encoding.UTF8.GetString(addtlAmount));
                        //////this.richTextBox1.AppendText("\n ecrrefno: " + System.Text.Encoding.UTF8.GetString(ecrrefno));
                        //////this.richTextBox1.AppendText("\n version: " + System.Text.Encoding.UTF8.GetString(version));
                        //////webBrowser1.DocumentText = outResp.ToString();
                        //////this.richTextBox1.AppendText("\n outRespLen: " + System.Text.Encoding.UTF8.GetString(outRespLen));
                        // label2.Text = int.Parse(System.Text.Encoding.UTF8.GetString(rspCode)).ToString();
                        label2.Text = Convert.ToInt32(System.Text.Encoding.UTF8.GetString(rspCode)).ToString();
                        // switch (int.Parse(System.Text.Encoding.UTF8.GetString(rspCode)))
                        switch (Convert.ToInt32(System.Text.Encoding.UTF8.GetString(rspCode)))
                        {
                            case 116:
                                txt_responce.Text = "الرصيد لا يسمح";
                                break;
                            case 0:
                                txt_responce.Text = "العملية مقبولة-مستلمة";
                                break;
                            case 1:
                                txt_responce.Text = "العملية مقبولة";
                                break;
                            case 3:
                                txt_responce.Text = "العملية مقبولة";
                                break;
                            case 7:
                                txt_responce.Text = "العملية مقبولة";
                                break;
                            case 87:
                                txt_responce.Text = "العملية مقبولة";
                                break;
                            case 89:
                                txt_responce.Text = "العملية مقبولة";
                                break;
                            case 100:
                                txt_responce.Text = "العملية مقبولة";
                                break;
                            case 127:
                                txt_responce.Text = "كلمة السر غير صحيحة";
                                break;
                            default:
                                break;
                        }
                        break;
                    case -1:
                        txt_responce.Text = "خطاء في المكتبة";
                        break;
                    case -2:
                        txt_responce.Text = "لا يوجد هناك استجابة";
                        break;
                    case -3:
                        txt_responce.Text = "عدم القدرة على فتح البورت";
                        break;
                    case -4:
                        txt_responce.Text = "خطاء بالاتصال عن طريق البورت";
                        break;
                    case 1:
                        txt_responce.Text = "تاخر في تحميل البيانات";
                        break;
                    case 2:
                        txt_responce.Text = "الكرت موقف";
                        break;
                    case 3:
                        txt_responce.Text = "لايوجد تطبيق نشط";
                        break;
                    case 4:
                        txt_responce.Text = "خطاء في قراءة الكرت";
                        break;
                    case 5:
                        txt_responce.Text = "ادخل الكرت فقط";
                        break;
                    case 6:
                        txt_responce.Text = "تجاوز الحد المسموح";
                        break;
                    case 7:
                        txt_responce.Text = "PIN Quit";
                        break;
                    case 8:
                        txt_responce.Text = "المستخدم الغى العملية او تاخر وقت المعالجة";
                        break;
                    case 9:
                        txt_responce.Text = "الكرت عطلان او غير مدعوم";
                        break;
                    case 10:
                        txt_responce.Text = "الجهاز مشغول";
                        break;
                    case 11:
                        txt_responce.Text = "الورق خارج";
                        break;
                    case 12:
                        txt_responce.Text = "لا يوجد ورق عشان الطباعة";
                        break;
                    case 13:
                        txt_responce.Text = "الغيت العملية";
                        break;
                    case 14:
                        txt_responce.Text = "De-SAF معالجة";
                        break;
                    default:
                        txt_responce.Text = "هناك خطاء ما";
                        break;

                }

               // if ((label2.Text.Equals("0")) && (label1.Text.Equals("0") || label1.Text.Equals("1") || label1.Text.Equals("3") || label1.Text.Equals("7") || label1.Text.Equals("87") || label1.Text.Equals("89") || label1.Text.Equals("100")))
                 DialogResult result = MessageBox.Show("هل تم قبول عملية الدفع بنجاح؟", "تاكيد", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2);
                 if (result == DialogResult.Yes)
                 {

                     // timer2.Stop();
                     its_ok = true;
                     this.Close();
                 }

                 else if (result == DialogResult.No)
                 {
                     //...
                 }
                 else
                 {
                     //...
                 }
                //  textBox3.Text=Result.ToString());
                //rspCode -> Response code refer if the trasncation approved or declined
                //trxRrn -> RRN for bank reference number
                // string result = System.Text.Encoding.UTF8.GetString(panNo);
                //   this.richTextBox1.AppendText("PAN NO: " + result);
            }
            else if (BL.CLS_Session.dtyrper.Rows[0]["mada_type"].ToString().Equals("1"))
            {
                try
                {
                    serialPort1.PortName ="COM" + textBox2.Text.Trim();
                    serialPort1.BaudRate = 115200;
                    serialPort1.DataBits = 8;
                    serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One");
                    serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), "None");

                    serialPort1.Close();
                    serialPort1.Open();

                    if (serialPort1.IsOpen)
                    {
                        var hexString = getAmountHax(Convert.ToDouble(textBox1.Text));
                        byte[] data = StringToByteArray("0230303035" + hexString + "03");
                        serialPort1.Write(data, 0, 18);
                    }
                    //if (its_ok)
                    //    this.Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    serialPort1.Close();
                }


            }

            else if (BL.CLS_Session.dtyrper.Rows[0]["mada_type"].ToString().Equals("2"))
            {
               // IntgrForm frm = new IntgrForm(Convert.ToDouble(textBox1.Text), Convert.ToInt32(textBox2.Text));
                ////try
                ////{
                ////    SpanInteg spanInteg = new SpanInteg();
                ////    string ComPort = this.textBox2.Text.Trim();
                ////    int iRet = spanInteg.CheckExistance(ComPort.Trim());
                ////    bool flag = iRet == 0;
                ////    if (flag)
                ////    {
                ////        string amount = this.textBox1.Text.Trim();
                ////        StringBuilder StrXmlResult = new StringBuilder(10240);
                ////        int nXmlResultLen = 0;
                ////        iRet = spanInteg.PerformMadaEcrTrx(ComPort, "SALE", "  ", amount, "", ref StrXmlResult, ref nXmlResultLen);
                ////        this.richTextBox1.Text = StrXmlResult.ToString();
                ////        XmlSerializer serializer = new XmlSerializer(typeof(Inter.MadaTransactionResult));
                ////        using (TextReader reader = new StringReader(StrXmlResult.ToString()))
                ////        {
                ////            Inter.MadaTransactionResult result = (Inter.MadaTransactionResult)serializer.Deserialize(reader);
                ////            int terminalStatusCode = result.EMVTags.TerminalStatusCode;
                ////            int num = terminalStatusCode;
                ////            if (num != 11)
                ////            {
                ////                txt_responce.Text= "OK";
                ////            }
                ////            txt_responce.Text= "Customer Has been Cancel";
                ////        }
                ////    }
                ////   // txt_responce.Text= "OK";
                ////}
                ////catch (Exception ex)
                ////{
                ////   // MessageBox.Show(ex.Message);
                ////    txt_responce.Text= "يوجد هناك خطا";
                ////}
            }
        }
        private void DoUpDate(object s, EventArgs e)
        {
            MessageBox.Show(serialPort1.ReadLine());
        }
        private Queue<byte> recievedData = new Queue<byte>();
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //int intBuffer;
            //intBuffer = serialPort1.BytesToRead;
            //byte[] byteBuffer = new byte[intBuffer];
            //serialPort1.Read(byteBuffer, 0, intBuffer);
            //this.Invoke(new EventHandler(DoUpDate));

            //byte[] data = new byte[serialPort1.BytesToRead];
            //serialPort1.Read(data, 0, data.Length);      
            //data.ToList().ForEach(b => recievedData.Enqueue(b));

            ////foreach (var money in data)
            ////{
            ////    MessageBox.Show(money.ToString());
            ////}
            ////foreach (var word in data)
            ////{
            ////    MessageBox.Show(word.ToString());
            ////}
            //for (var i = 0; i < data.Count; i++)
            //{
            //    Console.WriteLine("Amount is {0} and type is {1}", data[i].amount, data[i].);
            //}

            bool itsok=false;
            try
            {
                var sp = serialPort1;

                string availableData = sp.ReadExisting();

                if (availableData == "-2")
                {
                    itsok = false;
                    txt_responce.Text = "Error Cancel Key Pressed";
                }
                else if (availableData == "-46")
                {
                    itsok = false;
                    txt_responce.Text = "Error PIN CANCELED";
                }
                else if (availableData == "-26")
                {
                    itsok = false;
                    txt_responce.Text = "Error TIMEOUT SWIPE or INSER";
                }
                else if (availableData == "-25")
                {
                    itsok = false;
                    txt_responce.Text = "Error TIMEOUT SWIPE or INSER";
                }
                else if (availableData == "-45")
                {
                    itsok = false;
                    txt_responce.Text = "Error CONFIRM_AMOUNT_CANCELE";
                }
                else if (availableData == "-48")
                {
                    itsok = false;
                    txt_responce.Text = "Error ICC_TRX_FAILED";
                }
                else if (availableData == "-49")
                {
                    itsok = false;
                    txt_responce.Text = "Error TRX_NOT_ALLOWED";
                }
                else if (availableData == "-50")
                {
                    itsok = false;
                    txt_responce.Text = "ERR_MANUAL_ENTR_NOT_ALLOWED";
                }
                else if (availableData == "-42")
                {
                    itsok = false;
                    txt_responce.Text = "ERR_FUN_NOT_SUPPORTED";
                }
                else if (availableData == "-44")
                {
                    txt_responce.Text = "ERR_NO_INCOMING_REQ";
                    itsok = false;
                }
                else
                {
                    try
                    {
                        var paymentCode = availableData.Split('|')[5];

                        if (paymentCode == PaymentSuccessCode)
                        {
                            itsok = true;
                            its_ok = true;
                            
                        }
                        else if (paymentCode == PaymentDeclinedCode)
                        {
                            itsok = false;
                        }
                        else
                        {
                            itsok = false;
                        }
                    }
                    catch (Exception ex)
                    {
                       // textBox3.Text = ex.Message; // "حدث خطاء غير متوقع";
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        itsok = false;
                    }

                }

                this.serialPort1.DiscardInBuffer();
                this.serialPort1.DiscardOutBuffer();
                this.serialPort1.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error); // "حدث خطاء غير متوقع";
                itsok = false;
                serialPort1.Close();
            }

            //if (itsok)
            //    this.Close();
           
        }

        private void Mada_Pay_Shown(object sender, EventArgs e)
        {
            ///button1_Click(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            timer1.Stop();
            button1_Click(sender, e);
            
        }

        private void Mada_Pay_FormClosing(object sender, FormClosingEventArgs e)
        {
            //var hexString = getAmountHax(Convert.ToDouble(0));
            //byte[] data = StringToByteArray("0230303035" + hexString + "03");
            //serialPort1.Write(data, 0, 0);
          //  serialPort1.Dispose();
            serialPort1.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (its_ok)
            {
                timer2.Stop();
                this.Close();
            }
        }

        private void txt_responce_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
