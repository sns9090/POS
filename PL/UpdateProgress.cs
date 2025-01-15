using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace POS
{
    public partial class UpdateProgress : BaseForm
    {
        WebClient webClient;               // Our WebClient that will be doing the downloading for us
        Stopwatch sw = new Stopwatch();    // The stopwatch which we will be using to calculate the download speed

        public UpdateProgress()
        {
            InitializeComponent();
        }

        private void UpdateProgress_Load(object sender, EventArgs e)
        {
           // DownloadFile("https://www.dropbox.com/s/h98qc645q3xb268/update.zip?dl=1", @".\update.zip");
           // this.Close();
            
        }

        public void DownloadFile(string urlAddress, string location)
        {
            using (webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                // The variable that will be holding the url address (making sure it starts with http://)
               //// Uri URL = urlAddress.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ? new Uri(urlAddress) : new Uri("http://" + urlAddress);
                Uri URL = new Uri(urlAddress);

                // Start the stopwatch which we will be using to calculate the download speed
                sw.Start();

                try
                {
                    // Start downloading the file
                    webClient.DownloadFileAsync(URL, location);
                   //// webClient.DownloadFile(urlAddress, location);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   // this.Close();
                }
            }
        }

        // The event that will fire whenever the progress of the WebClient is changed
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Calculate download speed and output it to labelSpeed.
            labelSpeed.Text = string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"));

            // Update the progressbar percentage only when the value is not the same.
            progressBar1.Value = e.ProgressPercentage;

            // Show the percentage on our label.
            labelPerc.Text = e.ProgressPercentage.ToString() + "%";

            // Update the label with how much data have been downloaded so far and the total size of the file we are currently downloading
            labelDownloaded.Text = string.Format("{0} MB's / {1} MB's",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
        }

        // The event that will trigger when the WebClient is completed
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            // Reset the stopwatch.
            sw.Reset();

            if (e.Cancelled == true || string.IsNullOrEmpty(labelPerc.Text) || labelSpeed.Text.Equals("") || string.IsNullOrEmpty(labelDownloaded.Text))
            {
                MessageBox.Show("فشل التحميل .. يرجى المحاولة لاحقا", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
               // MessageBox.Show(labelPerc.Text);
               // MessageBox.Show(labelSpeed.Text);
               // MessageBox.Show(labelDownloaded.Text);
                MessageBox.Show("تم التحميل بنجاح","OK",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                Process.Start(@".\App_Updater.exe");
                this.Close();
            }
        }

        private void UpdateProgress_Shown(object sender, EventArgs e)
        {
            timer1.Start();
           // DownloadFile("https://www.dropbox.com/s/h98qc645q3xb268/update.zip?dl=1", @".\update.zip");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DownloadFile("https://www.dropbox.com/s/h98qc645q3xb268/update.zip?dl=1", @".\update.zip");
            timer1.Stop();
        }
    }
}
