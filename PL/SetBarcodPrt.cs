using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace POS
{
    public partial class SetBarcPrt : BaseForm
    {
        string d_printer;
        public SetBarcPrt()
        {
            InitializeComponent();
        }

        private void SetMainPrt_Load(object sender, EventArgs e)
        {
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                //  MessageBox.Show();

                comboBox1.Items.Add(printer);
            }
           // var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\temp.txt");

           // textBox1.Text = File.ReadAllText("yourfile.ext"); //reading
           // var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\printers.txt");
           // textBox1.Text = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\printers.txt");
            textBox1.Enabled = false;

            string readText = File.ReadAllText(Directory.GetCurrentDirectory() + @"\bprinter.txt");

            textBox1.Text = readText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string createText = textBox1.Text;
            File.WriteAllText(Directory.GetCurrentDirectory() + @"\bprinter.txt", createText);
            textBox1.Enabled = false;
            button1.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            textBox1.Enabled = true;

            PrinterSettings settings = new PrinterSettings();
            d_printer = settings.PrinterName;

            DialogResult result = MessageBox.Show(" الافتراضية؟  " + d_printer + " هل تريد اعتماد الطابعة " , "تاكيد", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
            {
                textBox1.Text = d_printer;
            }
            else if (result == DialogResult.No)
            {
                //...
            }
            else
            {
                //...
            } 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.SelectedItem.ToString();
        }
    }
}
