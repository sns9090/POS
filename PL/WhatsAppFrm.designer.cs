using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Net;

using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
//using Newtonsoft.Json;
//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace POS
{
	[Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
	public partial class WhatsAppFrm : Form
	{
		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WhatsAppFrm));
            this.Button2 = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.txtlog = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblspeed = new System.Windows.Forms.Label();
            this.txttocken = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtmobile_no = new System.Windows.Forms.TextBox();
            this.Button5 = new System.Windows.Forms.Button();
            this.txtfileURl = new System.Windows.Forms.TextBox();
            this.Button6 = new System.Windows.Forms.Button();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Button7 = new System.Windows.Forms.Button();
            this.lblstatus = new System.Windows.Forms.Label();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.Button4 = new System.Windows.Forms.Button();
            this.txtfilepath = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Button8 = new System.Windows.Forms.Button();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.txtmessageId = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.txtmobile_list = new System.Windows.Forms.TextBox();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(37, 345);
            this.Button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(91, 29);
            this.Button2.TabIndex = 14;
            this.Button2.Text = "Clear";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button3
            // 
            this.Button3.Location = new System.Drawing.Point(542, 60);
            this.Button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(202, 32);
            this.Button3.TabIndex = 15;
            this.Button3.Text = "Send Text Message";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // txtlog
            // 
            this.txtlog.Location = new System.Drawing.Point(37, 378);
            this.txtlog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtlog.Multiline = true;
            this.txtlog.Name = "txtlog";
            this.txtlog.Size = new System.Drawing.Size(658, 178);
            this.txtlog.TabIndex = 17;
            this.txtlog.TextChanged += new System.EventHandler(this.txtlog_TextChanged);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(27, 6);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(51, 17);
            this.Label1.TabIndex = 52;
            this.Label1.Text = "Token";
            // 
            // lblspeed
            // 
            this.lblspeed.AutoSize = true;
            this.lblspeed.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblspeed.ForeColor = System.Drawing.Color.Red;
            this.lblspeed.Location = new System.Drawing.Point(459, 4);
            this.lblspeed.Name = "lblspeed";
            this.lblspeed.Size = new System.Drawing.Size(17, 17);
            this.lblspeed.TabIndex = 53;
            this.lblspeed.Text = "0";
            // 
            // txttocken
            // 
            this.txttocken.Location = new System.Drawing.Point(31, 34);
            this.txttocken.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txttocken.Name = "txttocken";
            this.txttocken.Size = new System.Drawing.Size(654, 20);
            this.txttocken.TabIndex = 56;
            this.txttocken.Text = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJudW1iZXIiOiJzbnM5MDkwIiwic2VyaWFsIjoiMzBh" +
    "MGQ4NTA3MjMwYTcyIiwiaWF0IjoxNzIzMzA0MjY5LCJleHAiOjE4MDk3MDQyNjl9.f4oMQMpJYZ__d9T" +
    "ZwTjCz2khGAxc5_v3pslSmW25SHw";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(42, 104);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(446, 47);
            this.txtMessage.TabIndex = 57;
            this.txtMessage.Text = resources.GetString("txtMessage.Text");
            this.txtMessage.TextChanged += new System.EventHandler(this.txtMessage_TextChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(46, 81);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(67, 17);
            this.Label2.TabIndex = 58;
            this.Label2.Text = "Message";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(45, 63);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(76, 17);
            this.Label3.TabIndex = 59;
            this.Label3.Text = "Mobile No";
            // 
            // txtmobile_no
            // 
            this.txtmobile_no.Location = new System.Drawing.Point(137, 63);
            this.txtmobile_no.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtmobile_no.Name = "txtmobile_no";
            this.txtmobile_no.Size = new System.Drawing.Size(120, 20);
            this.txtmobile_no.TabIndex = 60;
            this.txtmobile_no.Text = "966537422638";
            // 
            // Button5
            // 
            this.Button5.Location = new System.Drawing.Point(542, 99);
            this.Button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(202, 32);
            this.Button5.TabIndex = 61;
            this.Button5.Text = "Send Image From URL";
            this.Button5.UseVisualStyleBackColor = true;
            this.Button5.Click += new System.EventHandler(this.Button5_Click_1);
            // 
            // txtfileURl
            // 
            this.txtfileURl.Location = new System.Drawing.Point(44, 206);
            this.txtfileURl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtfileURl.Name = "txtfileURl";
            this.txtfileURl.Size = new System.Drawing.Size(463, 20);
            this.txtfileURl.TabIndex = 62;
            this.txtfileURl.Text = "https://i0.wp.com/islamphotos.net/wp-content/uploads/2018/05/d0e8cfde95cd80dd46cd" +
    "332257ace4d0.png";
            // 
            // Button6
            // 
            this.Button6.Location = new System.Drawing.Point(746, 448);
            this.Button6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(152, 32);
            this.Button6.TabIndex = 182;
            this.Button6.Text = "Scan QR";
            this.Button6.UseVisualStyleBackColor = true;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // PictureBox1
            // 
            this.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox1.Location = new System.Drawing.Point(811, 10);
            this.PictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(372, 391);
            this.PictureBox1.TabIndex = 183;
            this.PictureBox1.TabStop = false;
            // 
            // Button7
            // 
            this.Button7.Location = new System.Drawing.Point(542, 174);
            this.Button7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(202, 32);
            this.Button7.TabIndex = 184;
            this.Button7.Text = "Query Device status";
            this.Button7.UseVisualStyleBackColor = true;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // lblstatus
            // 
            this.lblstatus.AutoSize = true;
            this.lblstatus.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblstatus.Location = new System.Drawing.Point(939, 448);
            this.lblstatus.Name = "lblstatus";
            this.lblstatus.Size = new System.Drawing.Size(179, 17);
            this.lblstatus.TabIndex = 185;
            this.lblstatus.Text = "Click Scan QR to connect";
            // 
            // Timer1
            // 
            this.Timer1.Interval = 1000;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Button4
            // 
            this.Button4.Location = new System.Drawing.Point(542, 136);
            this.Button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(202, 32);
            this.Button4.TabIndex = 186;
            this.Button4.Text = "Send Image From disk";
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click_2);
            // 
            // txtfilepath
            // 
            this.txtfilepath.Location = new System.Drawing.Point(42, 254);
            this.txtfilepath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtfilepath.Name = "txtfilepath";
            this.txtfilepath.Size = new System.Drawing.Size(464, 20);
            this.txtfilepath.TabIndex = 187;
            this.txtfilepath.Text = "D:\\TreeSoft\\background.png";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(50, 182);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(143, 17);
            this.Label4.TabIndex = 188;
            this.Label4.Text = "Image URL address";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(57, 230);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(89, 17);
            this.Label5.TabIndex = 189;
            this.Label5.Text = "Image Path";
            // 
            // Button8
            // 
            this.Button8.Image = global::POS.Properties.Resources.open1;
            this.Button8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Button8.Location = new System.Drawing.Point(405, 229);
            this.Button8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(110, 21);
            this.Button8.TabIndex = 190;
            this.Button8.Text = "Select File";
            this.Button8.UseVisualStyleBackColor = true;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "OpenFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(657, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 21);
            this.button1.TabIndex = 191;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // txtmessageId
            // 
            this.txtmessageId.Location = new System.Drawing.Point(111, 155);
            this.txtmessageId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtmessageId.Name = "txtmessageId";
            this.txtmessageId.Size = new System.Drawing.Size(184, 20);
            this.txtmessageId.TabIndex = 198;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(21, 158);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(84, 17);
            this.Label7.TabIndex = 197;
            this.Label7.Text = "MessageID";
            // 
            // Button9
            // 
            this.Button9.Location = new System.Drawing.Point(542, 210);
            this.Button9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(202, 32);
            this.Button9.TabIndex = 200;
            this.Button9.Text = "Is Number Register in whatsapp";
            this.Button9.UseVisualStyleBackColor = true;
            this.Button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(542, 246);
            this.button10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(202, 32);
            this.button10.TabIndex = 199;
            this.button10.Text = "Delete message for me";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(692, 24);
            this.button11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(114, 30);
            this.button11.TabIndex = 201;
            this.button11.Text = "Save Tocken";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(522, 298);
            this.button12.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(241, 32);
            this.button12.TabIndex = 202;
            this.button12.Text = "Send Messge to multi phone numbers";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // txtmobile_list
            // 
            this.txtmobile_list.Location = new System.Drawing.Point(42, 277);
            this.txtmobile_list.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtmobile_list.Multiline = true;
            this.txtmobile_list.Name = "txtmobile_list";
            this.txtmobile_list.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtmobile_list.Size = new System.Drawing.Size(464, 66);
            this.txtmobile_list.TabIndex = 203;
            this.txtmobile_list.Text = "967777331777,967777615211,966537422638";
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(415, 345);
            this.button13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(91, 29);
            this.button13.TabIndex = 204;
            this.button13.Text = "...";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(522, 333);
            this.button14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(241, 32);
            this.button14.TabIndex = 205;
            this.button14.Text = "Send Image to multi phone numbers";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // WhatsAppFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 629);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.txtmobile_list);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.Button9);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.txtmessageId);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Button8);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.txtfilepath);
            this.Controls.Add(this.Button4);
            this.Controls.Add(this.lblstatus);
            this.Controls.Add(this.Button7);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.Button6);
            this.Controls.Add(this.txtfileURl);
            this.Controls.Add(this.Button5);
            this.Controls.Add(this.txtmobile_no);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txttocken);
            this.Controls.Add(this.lblspeed);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtlog);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Button2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "WhatsAppFrm";
            this.Text = "Whatsapp API Test";
            this.Closed += new System.EventHandler(this.Form1_Closed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		internal System.Windows.Forms.Button Button2;
		internal System.Windows.Forms.Button Button3;
		internal System.Windows.Forms.TextBox txtlog;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label lblspeed;
		internal System.Windows.Forms.TextBox txttocken;
		internal System.Windows.Forms.TextBox txtMessage;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.TextBox txtmobile_no;
		internal System.Windows.Forms.Button Button5;
		internal System.Windows.Forms.TextBox txtfileURl;
		internal System.Windows.Forms.Button Button6;
		internal System.Windows.Forms.PictureBox PictureBox1;
		internal System.Windows.Forms.Button Button7;
		internal System.Windows.Forms.Label lblstatus;
		internal System.Windows.Forms.Timer Timer1;
		internal System.Windows.Forms.Button Button4;
		internal System.Windows.Forms.TextBox txtfilepath;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Button Button8;
		internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;

		//[STAThread]
        //private static void Main()
        //{
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new Form1());
        //}

        internal Button button1;
        internal TextBox txtmessageId;
        internal Label Label7;
        internal Button Button9;
        internal Button button10;
        internal Button button11;
        internal Button button12;
        internal TextBox txtmobile_list;
        internal Button button13;
        internal Button button14;
    }

}