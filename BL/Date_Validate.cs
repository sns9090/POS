using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.ComponentModel;

namespace POS.BL
{
    class Date_Validate
    {
        BL.DAML daml = new DAML();

        public string convert_to_mdate(string input)
        {
            string outbut;
            outbut = (input.Replace("-", "").Substring(4, 4)) + "-" + input.Replace("-", "").Substring(2, 2) + "-" + input.Replace("-", "").Substring(0, 2);

            DateTime dt = Convert.ToDateTime(outbut, new CultureInfo("ar-SA", false));
            //  DateTime dt = Convert.ToDateTime(txt_mdate.Text.ToString("yyyy-MM-dd"), new CultureInfo("en-US"));
            // txt_hdate.Text = dt.ToString("ddMMyyyy", new CultureInfo("ar-SA"));

            outbut = dt.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

            return outbut;
        }

        public string convert_to_hdate(string input)
        {
            string outbut;
            outbut = (input.Replace("-", "").Trim().Substring(4, 4)) + "-" + input.Replace("-", "").Substring(2, 2) + "-" + input.Replace("-", "").Substring(0, 2);

            DateTime dt = Convert.ToDateTime(outbut, new CultureInfo("en-US", false));
            //  DateTime dt = Convert.ToDateTime(txt_mdate.Text.ToString("yyyy-MM-dd"), new CultureInfo("en-US"));
            // txt_hdate.Text = dt.ToString("ddMMyyyy", new CultureInfo("ar-SA"));

            outbut = dt.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));

            return outbut;

        }

        public string convert_to_yyyyDDdd(string input)
        {
            string result, temp;
            temp = input.Replace("-", "");
            result = (temp.Substring(4, 4)) + temp.Substring(2, 2) + temp.Substring(0, 2);

            // DateTime dt = Convert.ToDateTime(outbut, new CultureInfo("ar-SA", false));
            //  DateTime dt = Convert.ToDateTime(txt_mdate.Text.ToString("yyyy-MM-dd"), new CultureInfo("en-US"));
            // txt_hdate.Text = dt.ToString("ddMMyyyy", new CultureInfo("ar-SA"));

            //outbut = dt.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

            return result;
        }

        public string convert_to_ddDDyyyy(string input)
        {
            string result, temp;
            temp = input;
            //  temp = input.Replace("-", "");
            result = (temp.Substring(0, 4)) + "-" + temp.Substring(4, 2) + "-" + temp.Substring(6, 2);

            // DateTime dt = Convert.ToDateTime(outbut, new CultureInfo("ar-SA", false));
            //  DateTime dt = Convert.ToDateTime(txt_mdate.Text.ToString("yyyy-MM-dd"), new CultureInfo("en-US"));
            // txt_hdate.Text = dt.ToString("ddMMyyyy", new CultureInfo("ar-SA"));

            //outbut = dt.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

            return result;
        }
        public string convert_to_yyyy_MMddwith_dash(string input)
        {
            string result, temp;
            //temp = input;
            temp = input.Replace("-", "");
            result = (temp.Substring(4, 4)) + "-" + temp.Substring(2, 2) + "-" + temp.Substring(0, 2);

            // DateTime dt = Convert.ToDateTime(outbut, new CultureInfo("ar-SA", false));
            //  DateTime dt = Convert.ToDateTime(txt_mdate.Text.ToString("yyyy-MM-dd"), new CultureInfo("en-US"));
            // txt_hdate.Text = dt.ToString("ddMMyyyy", new CultureInfo("ar-SA"));

            //outbut = dt.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

            return result;
        }
        public string convert_to_ddMMyyyy_dash(string input)
        {
            string result, temp;
            //temp = input;
            temp = input.Replace("-", "");
            result = (temp.Substring(6, 2)) + "-" + temp.Substring(4, 2) + "-" + temp.Substring(0, 4);

            // DateTime dt = Convert.ToDateTime(outbut, new CultureInfo("ar-SA", false));
            //  DateTime dt = Convert.ToDateTime(txt_mdate.Text.ToString("yyyy-MM-dd"), new CultureInfo("en-US"));
            // txt_hdate.Text = dt.ToString("ddMMyyyy", new CultureInfo("ar-SA"));

            //outbut = dt.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

            return result;
        }

        public void txtdate_Validating(MaskedTextBox txtbx, CancelEventArgs e)
        {
            DateTime rs;

            CultureInfo ci = new CultureInfo("en-US", false);

            if (!DateTime.TryParseExact(txtbx.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            {

                MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                txtbx.Focus();

            }
            //if (Convert.ToInt32(convert_to_yyyyDDdd(txtbx.Text)) < Convert.ToInt32(convert_to_yyyyDDdd(BL.CLS_Session.start_dt)) || Convert.ToInt32(convert_to_yyyyDDdd(txtbx.Text)) > Convert.ToInt32(convert_to_yyyyDDdd(BL.CLS_Session.end_dt)))
            //{

            //    MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    e.Cancel = true;

            //}
        }

        public void ValidateText(TextBox textbox)
        {
            Decimal value;

            bool isConverted = Decimal.TryParse(textbox.Text.Trim(), out value);
            if (!isConverted)
            {
                // MessageBox.Show("Only numbers allowed");
                textbox.Text = "0.00";
                return;
            }

            if (value < 0 || value > 999999999)
            {
                // MessageBox.Show("Please enter a value between 1-24");
                textbox.Text = "0.00";
            }
        }

        public void ValidateText_float(TextBox textbox)
        {
            float value;

            bool isConverted = float.TryParse(textbox.Text.Trim(), out value);
            if (!isConverted)
            {
                // MessageBox.Show("Only numbers allowed");
                textbox.Text = "0.00";
                return;
            }

            //if (value < 0 || value > 999999999)
            //{
            //    // MessageBox.Show("Please enter a value between 1-24");
            //    textbox.Text = "0.00";
            //}
        }

        public void ValidateText_numric(TextBox textbox)
        {
            decimal value;

            bool isConverted = decimal.TryParse(textbox.Text.Trim(), out value);
            if (!isConverted)
            {
                // MessageBox.Show("Only numbers allowed");
                textbox.Text = "0.00";
                return;
            }

            //if (value < 0 || value > 999999999)
            //{
            //    // MessageBox.Show("Please enter a value between 1-24");
            //    textbox.Text = "0.00";
            //}
        }

        public void textBox_Validating(TextBox textbox)
        {
            // textBox1.Text = string.Format("{0:n0}", decimal.Parse(textBox1.Text));
            textbox.Text = string.Format("{0:#,##0.00}", decimal.Parse(textbox.Text));
        }

        public string validate_trtypes(string input)
        {
            /**** ok****
            string rettype;
            DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select tr_no,tr_abriv from [trtypes]");

            DataRow[] dtabr= dt.Select("tr_no ='" + input + "'");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
            rettype = dtabr[0][1].ToString();
            return rettype;
             */
            string rettype;
          //  DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select tr_no,tr_abriv from [trtypes]");

            DataRow[] dtabr = BL.CLS_Session.dttrtype.Select("tr_no ='" + input + "'");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
            rettype = BL.CLS_Session.lang.Equals("ع") ? dtabr[0][1].ToString() : dtabr[0][2].ToString();
            return rettype;

        }

        public string validate_fieldnm(string inpt)
        {

            string fieldnm;

            DataRow[] dtabr = BL.CLS_Session.dtfieldnm.Select("fld_tag ='" + inpt + "'");//,'" + dt222.Rows[0][12] + "','" + dt222.Rows[0][15] + "','" + dt222.Rows[0][18] + "')";
            fieldnm = BL.CLS_Session.lang.Equals("ع") ? dtabr[0][3].ToString() : dtabr[0][4].ToString();
            return fieldnm;

        }

        public void Vildate_Form(Form frm,string tag)
        {
           //frm=new 
            if (BL.CLS_Session.formkey.Contains(tag))
            {
                frm = new Form();
              //  frm.MdiParent = ParentForm;
                frm.Show();
            }
            
        }
        
        //formate datagrid
        public void formate_dgv(DataGridView dgv, int toclr)
        {
            // designing code=:
            
           // dgv.BorderStyle = BorderStyle.None;
           // dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
           // dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
           //// dgv.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
          ////  dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 224, 224);
           // dgv.DefaultCellStyle.SelectionBackColor = Color.Lavender;
            dgv.DefaultCellStyle.SelectionBackColor = Color.Thistle;
          //  dgv.BackgroundColor = Color.White;

            ////if (Convert.ToBoolean(dgv.CurrentRow.Cells[toclr].Value) == false && toclr!=0)
            ////dgv.CurrentRow.DefaultCellStyle.ForeColor = Color.Red;
            if (toclr == 0)
               // dgv.DefaultCellStyle.SelectionBackColor = Color.White;
                dgv.DefaultCellStyle.SelectionForeColor = Color.Red;
               // dgv.DefaultCellStyle.ForeColor = Color.Red;
            else
               // dgv.DefaultCellStyle.ForeColor = Color.Black;
                dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
         
             // dgv.CurrentRow.DefaultCellStyle.ForeColor = Color.Red;
            //dgv.EnableHeadersVisualStyles = false;
            //dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            //dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            //dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            
        }

        public DataTable convert_dtg_to_dt(DataGridView dtg)
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn col in dtg.Columns)
            {
                dt.Columns.Add(col.Name);
                // dt.Columns.Add(col.HeaderText);
            }

            foreach (DataGridViewRow row in dtg.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }
            //////int cn = 1;
            //////foreach (DataGridViewColumn col in dtg.Columns)
            //////{
            //////    /*
            //////    dt.Columns.Add(col.Name);
            //////    // dt.Columns.Add(col.HeaderText);
            //////     */
            //////    dt.Columns.Add("c" + cn.ToString());
            //////    cn++;
            //////}

            //////foreach (DataGridViewRow row in dtg.Rows)
            //////{
            //////    DataRow dRow = dt.NewRow();
            //////    foreach (DataGridViewCell cell in row.Cells)
            //////    {
            //////        dRow[cell.ColumnIndex] = cell.Value;

            //////        //if (cell.ColumnIndex == 3)
            //////        //{
            //////        //    DataRow[] res = dtunits.Select("unit_id ='" + dRow[3] + "'");
            //////        //    //   row[2] = res[0][1];
            //////        //    dRow[cell.ColumnIndex] = res[0][1];
            //////        //}
            //////    }
            //////    dt.Rows.Add(dRow);
            //////}
            return dt;
        }

        //enum NumberType
        //{
        //    ByteNumber,
        //    IntegerNumber,
        //    LongNumber,
        //    DoubleNumber,
        //    DecimalNumber
        //}

       // public bool IsValidGridViewCellNumber(object value, string type, bool allowNegativeValues = false, bool allowZero = false, bool showMessage = false)
        public void showwin_report(DataGridView dgv, string[] cols)
        {
          //  for (int i = 0; i < cols.Length; i++)
            if (BL.CLS_Session.showwin == false)
            {
                foreach (string ch in cols)
                {
                    //  Console.Write(arr[i]);
                    // } 
                    // var col = dgv.Columns[ch];
                    //  dgv.Columns[ch].Expression = "'a'";

                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {
                        dgvr.Cells[ch].Value = 0;
                    }

                }
            }
        }
        public bool IsValidNumber(object value, string type, bool allowNegativeValues = false, bool allowZero = false, bool showMessage = false)
        {
            bool temp = false;
            switch (type)
            {
                case "ByteNumber":
                    {
                        byte result = 0;
                        if (value != null)
                        {
                            if (byte.TryParse(value.ToString(), out result) == false)
                            {
                                if (showMessage == true)
                                    MessageBox.Show("");
                                // MsgBox(Messages.MsgDoubleValuesOnlyAllowed, MsgBoxStyle + MsgBoxStyle.Information, MsgBoxTitle);
                            }
                            else if (allowNegativeValues == true)
                            {
                                if (showMessage == true)
                                    MessageBox.Show("");
                                //MsgBox(Messages.MsgNegativeValuesNotAllowed, MsgBoxStyle + MsgBoxStyle.Information, MsgBoxTitle);
                            }
                            else if (allowZero == false & System.Convert.ToInt32(value) == 0)
                            {
                                if (showMessage == true)
                                    MessageBox.Show("");
                                //MsgBox(Messages.MsgZeroNotAllowed, MsgBoxStyle + MsgBoxStyle.Information, MsgBoxTitle);
                            }
                            else
                                temp = true;
                        }
                        else
                            temp = true;
                        break;
                    }

                case "IntegerNumber":
                    {
                        int result = 0;
                        if (value != null)
                        {
                            if (int.TryParse(value.ToString(), out result) == false)
                            {
                                if (showMessage == true)
                                    MessageBox.Show("");
                                // MsgBox(Messages.MsgDoubleValuesOnlyAllowed, MsgBoxStyle + MsgBoxStyle.Information, MsgBoxTitle);
                            }
                            else if (allowNegativeValues == false & System.Convert.ToInt32(value) < 0)
                            {
                                if (showMessage == true)
                                    MessageBox.Show("");
                                // MsgBox(Messages.MsgNegativeValuesNotAllowed, MsgBoxStyle + MsgBoxStyle.Information, MsgBoxTitle);
                            }
                            else if (allowZero == false & System.Convert.ToInt32(value) == 0)
                            {
                                if (showMessage == true)
                                    MessageBox.Show("");
                                // MsgBox(Messages.MsgZeroNotAllowed, MsgBoxStyle + MsgBoxStyle.Information, MsgBoxTitle);
                            }
                            else
                                temp = true;
                        }
                        else
                            temp = true;
                        break;
                    }

                case "LongNumber":
                    {
                        long result = 0;
                        if (value != null)
                        {
                            if (long.TryParse(value.ToString(), out result) == false)
                            {
                                if (showMessage == true)
                                    MessageBox.Show("");
                                // MsgBox(Messages.MsgLongValuesOnlyAllowed, MsgBoxStyle + MsgBoxStyle.Information, MsgBoxTitle);
                            }
                            else if (allowNegativeValues == false & System.Convert.ToInt64(value) < 0)
                            {
                                if (showMessage == true)
                                    MessageBox.Show("");
                                //MsgBox(Messages.MsgNegativeValuesNotAllowed, MsgBoxStyle + MsgBoxStyle.Information, MsgBoxTitle);
                            }
                            else if (allowZero == false & System.Convert.ToInt64(value) == 0)
                            {
                                if (showMessage == true)
                                    MessageBox.Show("");
                                //MsgBox(Messages.MsgZeroNotAllowed, MsgBoxStyle + MsgBoxStyle.Information, MsgBoxTitle);
                            }
                            else
                                temp = true;
                        }
                        else
                            temp = true;
                        break;
                    }

                case "DoubleNumber":
                    {
                        double result = 0;
                        if (value != null)
                        {
                            if (double.TryParse(value.ToString(), out result) == false)
                            {
                                if (showMessage == true)
                                    MessageBox.Show("");
                                // MsgBox(Messages.MsgDoubleValuesOnlyAllowed, MsgBoxStyle + MsgBoxStyle.Information, MsgBoxTitle);
                            }
                            else if (allowNegativeValues == false & System.Convert.ToDouble(value) < 0)
                            {
                                if (showMessage == true)
                                    MessageBox.Show("");
                                // MsgBox(Messages.MsgNegativeValuesNotAllowed, MsgBoxStyle + MsgBoxStyle.Information, MsgBoxTitle);
                            }
                            else if (allowZero == false & System.Convert.ToDouble(value) == 0)
                            {
                                if (showMessage == true)
                                    MessageBox.Show("");
                                // MsgBox(Messages.MsgZeroNotAllowed, MsgBoxStyle + MsgBoxStyle.Information, MsgBoxTitle);
                            }
                            else
                                temp = true;
                        }
                        else
                            temp = true;
                        break;
                    }

                case "DecimalNumber":
                    {
                        decimal result = 0;
                        if (value != null)
                        {
                            if (decimal.TryParse(value.ToString(), out result) == false)
                            {
                                if (showMessage == true)
                                    MessageBox.Show("");
                                // MsgBox(Messages.MsgDecimalValuesOnlyAllowed, MsgBoxStyle + MsgBoxStyle.Information, MsgBoxTitle);
                            }
                            else if (allowNegativeValues == false & System.Convert.ToDecimal(value) < 0)
                            {
                                if (showMessage == true)
                                    MessageBox.Show("");
                                // MsgBox(Messages.MsgNegativeValuesNotAllowed, MsgBoxStyle + MsgBoxStyle.Information, MsgBoxTitle);
                            }
                            else if (allowZero == false & System.Convert.ToDecimal(value) == 0)
                            {
                                if (showMessage == true)
                                    MessageBox.Show("");
                                // MsgBox(Messages.MsgZeroNotAllowed, MsgBoxStyle + MsgBoxStyle.Information, MsgBoxTitle);
                            }
                            else
                                temp = true;
                        }
                        else
                            temp = true;
                        break;
                    }
            }
            return temp;
        }

      //  public string validat_is_date(string input)
       // {
            //DateTime rs;

            //CultureInfo ci = new CultureInfo("en-US", false);

            //if (!DateTime.TryParseExact(this.txt_mdate.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            //{

            //    MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    e.Cancel = true;

            //}
            //if (Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) < Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.start_dt)) || Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) > Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.end_dt)))
            //{

            //    MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    e.Cancel = true;

            //}
       // }
    }
}
