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
    public partial class SetMainPrt : BaseForm
    {
        string[] lines_prt = null;
        string d_printer;
        public SetMainPrt()
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
            lines_prt = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\printers.txt");
           // checkBox1.Checked = lines_prt.Skip(1).First().ToString().Equals("1") ? true : false;
           // textBox1.Text = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\printers.txt");
            textBox1.Enabled = false;

            string readText = File.ReadAllText(Directory.GetCurrentDirectory() + @"\printers.txt");

            textBox1.Text = readText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var path = Directory.GetCurrentDirectory() + @"\printers.txt";

            //string[] lines = { textBox1.Text, checkBox1.Checked? "1" : "0"};
            string[] lines = { textBox1.Text};
            File.WriteAllLines(path, lines);

            //string createText = textBox1.Text;
            //File.WriteAllText(Directory.GetCurrentDirectory() + @"\printers.txt", createText);
            textBox1.Enabled = false;
            button1.Enabled = false;
            checkBox1.Enabled = false;

           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button3.Enabled = true;
            textBox1.Enabled = true;
            checkBox1.Enabled = true;

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

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                textBox1.Text = comboBox1.SelectedItem.ToString();
            }
            else
            {
                textBox1.Text += "\r\n" + comboBox1.SelectedItem.ToString();
            }
        }
    }
}
