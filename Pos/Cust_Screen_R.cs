using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS.Pos
{
    public partial class Cust_Screen_R : BaseForm
    {
        public Cust_Screen_R()
        {
            InitializeComponent();
        }

        private void Cust_Screen_R_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if(dataGridView1.RowCount>1)
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 2;
        }
    }
}
