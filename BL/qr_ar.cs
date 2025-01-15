using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS.BL
{
    class qr_ar
    {
        //هنا داله التشفير
        #region Code Modified / Added By Kashif

        byte[] Seller;
        byte[] VatNo;
        byte[] dateTime;
        byte[] Total;
        byte[] Tax;

        //private void btnEncoded_Click(object sender, EventArgs e)
        //{
        //    GenerateQrCodeTLV(textSellerName.Text, textVATRegistrationNumber.Text, textTime.Text, Convert.ToDouble(textTotal.Text), Convert.ToDouble(textVATTotal.Text));
        //}


        public string GenerateQrCodeTLV(string sellerName, string vATNumber, string invoiceDateTime, double invoiceTotal, double vATTotal)
        {
            Savestrings(sellerName, vATNumber, Convert.ToDateTime(invoiceDateTime), invoiceTotal, vATTotal);
            List<byte> cryptoBytes = new List<byte>();
            cryptoBytes.AddRange(getBytes(1, this.Seller));
            cryptoBytes.AddRange(getBytes(2, this.VatNo));
            cryptoBytes.AddRange(getBytes(3, this.dateTime));
            cryptoBytes.AddRange(getBytes(4, this.Total));
            cryptoBytes.AddRange(getBytes(5, this.Tax));

            //Convert HexByteArray to Base64String
            string strBase64 = Convert.ToBase64String(cryptoBytes.ToArray());

            return strBase64;
            //richTextBox1.Text = strBase64;
        }

        public void Savestrings(string Seller, string TaxNo, DateTime dateTime, double Total, double Tax)
        {
            this.Seller = Encoding.UTF8.GetBytes(Seller);
            this.VatNo = Encoding.UTF8.GetBytes(TaxNo);

            this.dateTime = Encoding.UTF8.GetBytes(dateTime.ToString());
            this.Total = Encoding.UTF8.GetBytes(Total.ToString());
            this.Tax = Encoding.UTF8.GetBytes(Tax.ToString());
        }
        private string getString()
        {
            string result = "";
            result += this.funGetTVL(1, this.Seller);
            result += this.funGetTVL(2, this.VatNo);
            result += this.funGetTVL(3, this.dateTime);
            result += this.funGetTVL(4, this.Total);
            result += this.funGetTVL(5, this.Tax);
            return result;
        }

        public override string ToString()
        {
            return this.getString();
        }

        private byte[] getBytes(int id, byte[] Value)
        {
            byte[] val = new byte[2 + Value.Length];
            val[0] = (byte)id;
            val[1] = (byte)Value.Length;
            Value.CopyTo(val, 2);
            return val;
        }
        //Get TLV in Hex Reperntaion

        private string funGetTVL(int intTag, byte[] strValeu)
        {
            return ToHex(intTag) + ToHex(strValeu.Length) + ToHex(strValeu);
        }

        private string ToHex(byte[] str)
        {
            return BitConverter.ToString(str).Replace("-", "");
        }

        private string ToHex(int n)
        {
            return n.ToString("X2");
        }
        #endregion


    }
}
