namespace GenerateQrCode
{
    using System;
    using System.Text;

    public class TagClass
    {
        protected int Tag { get; set; }

        protected string Value { get; set; }

        public TagClass(int tag, string value)
        {
            Tag = tag;
            Value = value;
        }
       
        /// <summary>
        /// @return string Returns a string representing the encoded TLV data structure.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ToHex(Tag) + ToHex(Value.Length) + ToHex(Value);
        }
       
        /// <summary>
        /// To convert the string value to hex.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>string</returns>
        protected string ToHex(string str)
        {
            
           // byte[] plain = Convert.FromBase64String(data);
           // Encoding iso = Encoding.GetEncoding("ISO-8859-6");
            Encoding iso = Encoding.GetEncoding(864);
           // newData = iso.GetString(plain);
           // return newData;
            
          //  return BitConverter.ToString(Encoding.UTF8.GetBytes(str)).Replace("-", "");
            return BitConverter.ToString(iso.GetBytes(str)).Replace("-", "");
        }

        /// <summary>
        /// Converts to hex.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns></returns>
        protected string ToHex(int n)
        {
            return n.ToString("X2");
        }
    }
}
