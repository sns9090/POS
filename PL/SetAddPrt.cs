using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace POS
{
    public partial class SetAddPrt : BaseForm
    {
        public SetAddPrt()
        {
            InitializeComponent();
        }

        private void SetMainPrt_Load(object sender, EventArgs e)
        {
           // var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\temp.txt");

           // textBox1.Text = File.ReadAllText("yourfile.ext"); //reading
           // var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\printers.txt");
           // textBox1.Text = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\printers.txt");
            textBox1.Enabled = false;

            string readText = File.ReadAllText(Directory.GetCurrentDirectory() + @"\prttype.txt");

            textBox1.Text = readText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string createText = textBox1.Text;
            File.WriteAllText(Directory.GetCurrentDirectory() + @"\prttype.txt", createText);
            textBox1.Enabled = false;
            button1.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            textBox1.Enabled = true;
        }
    }
}
