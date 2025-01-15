using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace POS
{
    public partial class Notify : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dtin, dtout, dtusers;
        bool isin = true, isout = false;
        public Notify()
        {
            InitializeComponent();
        }

        private void Notify_Load(object sender, EventArgs e)
        {
            dtusers = daml.SELECT_QUIRY_only_retrn_dt("select user_name,full_name from users");

            cmb_user.DataSource = dtusers;
            cmb_user.DisplayMember = "full_name";
            cmb_user.ValueMember = "user_name";
            cmb_user.SelectedIndex = -1;

            btn_in_Click(sender, e);
           
        }

        private void btn_out_Click(object sender, EventArgs e)
        {
            isin = false;
            isout = true;
            dtout = daml.SELECT_QUIRY_only_retrn_dt("select usrnotify sender,n_subject subject,n_no ref,notes text,datenotify date,1 opn,datenotify date2 from notify where usrid='" + BL.CLS_Session.UserID + "' and del_by_s=0 order by n_no DESC");
            //foreach (DataRow rw1 in dtout.Rows)
            //{
            //    // if (Convert.ToBoolean(rw.Cells[5].Value) == false)
            //    // rw.DefaultCellStyle.BackColor = Color.Thistle;
            //    //     rw.DefaultCellStyle.BackColor = Color.PaleGreen;
            //    DateTime dtd = Convert.ToDateTime(rw1[6]);
            //    // txt_dattime.Text = dtd.ToString("dd-MM-yyyy HH:mm:ss", new CultureInfo("en-US", false));
            //    string tod2 = dtd.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            //    rw1[6] = tod2;
            //}
            dataGridView1.DataSource = dtout;

            dataGridView1.Columns[0].HeaderText = "المستلم";
            label1.Text = "المستلم :";
            btn_del.Enabled = true;
            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(rw.Cells[5].Value) == false)
                    // rw.DefaultCellStyle.BackColor = Color.Thistle;
                    rw.DefaultCellStyle.BackColor = Color.PaleGreen;
             //   DateTime dtd = Convert.ToDateTime(rw.Cells[6].Value);
                // txt_dattime.Text = dtd.ToString("dd-MM-yyyy HH:mm:ss", new CultureInfo("en-US", false));
               // string tod2 = dtd.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
              //  dataGridView1.Rows[rw.Index].Cells[6].Value = tod2;
            }
            dataGridView1.ClearSelection();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            btn_add.Visible = false;
            btn_Undo.Visible = true;
            btn_send.Enabled = true;
            btn_del.Enabled = false;
            //txt_dattime.Enabled = true;
            txt_note.ReadOnly = false;
            cmb_user.Enabled = true;
            txt_subject.ReadOnly = false;

            cmb_user.SelectedIndex = -1;
            txt_subject.Text = "";
            txt_note.Text = "";
            txt_dattime.Text = "";
            cmb_user.Focus();
            label1.Text = "المستلم :";
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (cmb_user.SelectedIndex==-1  || string.IsNullOrEmpty(txt_note.Text) || string.IsNullOrEmpty(txt_subject.Text))
            {
                MessageBox.Show("يرجى تعبئة كل البيانات","",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            btn_add.Enabled = true;
            btn_del.Enabled = true;
            btn_send.Enabled = false;

            int exexcuteds = daml.Insert_Update_Delete_retrn_int("INSERT INTO notify(branch,n_subject,usrnotify,usrid,notes) VALUES('" + BL.CLS_Session.brno + "','" + txt_subject.Text + "','" + cmb_user.SelectedValue + "','" + BL.CLS_Session.UserID + "','" + txt_note.Text + "')", false);

            MessageBox.Show("تم الارسال", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            // txt_dattime.Enabled = false;
            txt_note.ReadOnly = true;
            cmb_user.Enabled = false;
            txt_subject.ReadOnly = true;
            btn_Undo.Visible = false;
            btn_add.Visible = true;
        }

        private void btn_in_Click(object sender, EventArgs e)
        {
            isin = true;
            isout = false;    
            dtin = daml.SELECT_QUIRY_only_retrn_dt("select usrid sender,n_subject subject,n_no ref,notes text,datenotify date,isopen opn,datenotify date2 from notify where usrnotify='" + BL.CLS_Session.UserID + "' and del_by_r=0 order by isopen, n_no DESC");
           // dtout = daml.SELECT_QUIRY_only_retrn_dt("select usrnotify sender,n_subject subject,n_no ref,notes text,datenotify date,1 opn,datenotify date2 from notify where usrid='" + BL.CLS_Session.UserID + "' and del_by_s=0 order by n_no DESC");
            //foreach (DataRow rw1 in dtin.Rows)
            //{
            //    // if (Convert.ToBoolean(rw.Cells[5].Value) == false)
            //    // rw.DefaultCellStyle.BackColor = Color.Thistle;
            //    //     rw.DefaultCellStyle.BackColor = Color.PaleGreen;
            //    DateTime dtd = Convert.ToDateTime(rw1[6]);
            //    // txt_dattime.Text = dtd.ToString("dd-MM-yyyy HH:mm:ss", new CultureInfo("en-US", false));
            //    string tod2 = dtd.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            //    rw1[6] = tod2;
            //}
            
            dataGridView1.DataSource = dtin;

            dataGridView1.Columns[0].HeaderText = "المرسل";
            label1.Text = "المرسل :";

            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(rw.Cells[5].Value) == false)
                    // rw.DefaultCellStyle.BackColor = Color.Thistle;
                    rw.DefaultCellStyle.BackColor = Color.PaleGreen;
                //DateTime dtd = Convert.ToDateTime(rw.Cells[6].Value);
                //// txt_dattime.Text = dtd.ToString("dd-MM-yyyy HH:mm:ss", new CultureInfo("en-US", false));
                //string tod2 = dtd.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
                //dataGridView1.Rows[rw.Index].Cells[6].Value = tod2;
            }
            dataGridView1.ClearSelection();
            btn_del.Enabled = true;
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                cmb_user.SelectedValue = row.Cells[0].Value.ToString();
                txt_note.Text = row.Cells[3].Value.ToString();
              //  txt_dattime.Text = row.Cells[4].Value.ToString();
                DateTime dtd = Convert.ToDateTime(row.Cells[4].Value);
               // txt_dattime.Text = dtd.ToString("dd-MM-yyyy HH:mm:ss", new CultureInfo("en-US", false));
                txt_dattime.Text = dtd.ToString("dd-MM-yyyy hh:mm:ss tt", new CultureInfo("en-US", false));
                txt_subject.Text = row.Cells[1].Value.ToString();

                //DialogResult result = MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Do you want to dalete" : "هل تريد المسح", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                //if (result == DialogResult.Yes)
                //{
               // string coninout = isin ? "del_by_r=1" : "del_by_s=1";
                if (isin)
                {
                    daml.Insert_Update_Delete_retrn_int("update notify set isopen=1 where n_no=" + dataGridView1.CurrentRow.Cells[2].Value + "", false);
                    dataGridView1.CurrentRow.Cells[5].Value = true;

                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        if (Convert.ToBoolean(rw.Cells[5].Value) == false)
                            // rw.DefaultCellStyle.BackColor = Color.Thistle;
                            rw.DefaultCellStyle.BackColor = Color.PaleGreen;
                        else
                            rw.DefaultCellStyle.BackColor = Color.White;
                    }
                }
                 //   MessageBox.Show("تم المسح", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //}
                //else if (result == DialogResult.No)
                //{
                //    //...
                //}
                //else
                //{
                //    //...
                //}
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (cmb_user.SelectedIndex == -1 || string.IsNullOrEmpty(txt_note.Text) || string.IsNullOrEmpty(txt_subject.Text))
            {
                MessageBox.Show("لا يوجد بيانات للمسح", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Do you want to dalete" : "هل تريد المسح", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string coninout = isin ? "del_by_r=1" : "del_by_s=1";
                daml.Insert_Update_Delete_retrn_int("update notify set " + coninout + " where n_no=" + dataGridView1.CurrentRow.Cells[2].Value + "", false);
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                MessageBox.Show("تم المسح", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
            if (isin)
                btn_in_Click(sender, e);
            else
                btn_out_Click(sender, e);
        }

        private void Notify_Shown(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();

            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(rw.Cells[5].Value) == false)
                    // rw.DefaultCellStyle.BackColor = Color.Thistle;
                    rw.DefaultCellStyle.BackColor = Color.PaleGreen;
                else
                    rw.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void btn_Undo_Click(object sender, EventArgs e)
        {
            btn_add.Visible = true;
            btn_Undo.Visible = false;
            btn_send.Enabled = false;
            btn_del.Enabled = true;
            //txt_dattime.Enabled = true;
            txt_note.ReadOnly = true;
            cmb_user.Enabled = true;
            txt_subject.ReadOnly = true;

            cmb_user.SelectedIndex = -1;
            txt_subject.Text = "";
            txt_note.Text = "";
            txt_dattime.Text = "";
            cmb_user.Enabled = false;
        }
    }
}
