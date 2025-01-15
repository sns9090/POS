namespace GenerateQrCode
{
    //using QRCoder;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class GenerateQrCodeTLV
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateQrCode"/> class.
        /// </summary>
        /// <param name="tags">The tags.</param>
        public GenerateQrCodeTLV(string sellerName,string vATNumber,string invoiceDateTime,string invoiceTotal,string vATTotal)
        {
            Tags = new List<TagClass> {
                   new TagClass(1, sellerName) ,
                                new TagClass(2, vATNumber),
                                new TagClass(3, invoiceDateTime),
                                new TagClass(4, invoiceTotal),
                                new TagClass(5, vATTotal)

                };
        }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<TagClass> Tags { get; set; }


        /// <summary>
        /// Encodes an TLV data structure.
        /// </summary>
        /// <returns>string Returns a string representing the encoded TLV data structure.</returns>
        private string ToTLV()
        {
            return ToSingleStringWithEmptySeparator(Tags.Select(x => x.ToString()));
        }

        /**
         * Encodes an TLV as base64
         *
         * @return string Returns the TLV as base64 encode.
         */
        private string GetImageBase64()
        {
            return Convert.ToBase64String(HexStringToByteArray(ToTLV()));
        }
        
        public string QRCodeBase64()
        {
            return GetImageBase64();
        }

        /// /**
        /// * Render the QR code as base64 data image.
        /// *
        /// * @return string
        /// */        
        public byte[] GetQRCode()
        {
            return Convert.FromBase64String(GetImageBase64());
        }

        /// <summary>
        /// Converts to qrcode.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        //private string ToQRCode(string value, int pixelsPerModule = 20)
        //{
        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeData qrCodeData = qrGenerator.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
        //    QRCode qrCode = new QRCode(qrCodeData);
        //    Bitmap qrCodeImage = qrCode.GetGraphic(pixelsPerModule);
        //    return Convert.ToBase64String(ToArray(qrCodeImage));
        //}

        //private byte[] ToArray(Bitmap image)
        //{
        //    using (var memStream = new MemoryStream())
        //    {
        //        image.Save(memStream, ImageFormat.Png);
        //        return memStream.ToArray();
        //    }
        //}

        private string ToSingleStringWithEmptySeparator(IEnumerable<string> items, string separator = "")
        {
           // return string.Join(separator, items);
            StringBuilder builder = new StringBuilder();
            foreach (string s in items)
            {
                builder.Append(s).Append(separator);
            }
           // return builder.ToString().TrimEnd(new char[] { ',' });
            return builder.ToString();
        }

        private byte[] HexStringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
