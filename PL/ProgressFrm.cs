﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS
{
    public partial class ProgressFrm : BaseForm
    {
        public ProgressFrm()
        {
            InitializeComponent();
        }

        private void ProgressFrm_Load(object sender, EventArgs e)
        {
            if(BL.CLS_Session.lang.Equals("E"))
                this.Text="Please Wait";
            else
                this.Text="الرجاء الانتظار";
        }
    }
}
