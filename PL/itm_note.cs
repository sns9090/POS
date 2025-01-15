using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class itm_note : Form
    {
        public itm_note()
        {
            InitializeComponent();
        }

        private void Sal_itm_not_Load(object sender, EventArgs e)
        {
            txt_ino.Select();
        }
    }
}
