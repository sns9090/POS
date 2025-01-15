using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace POS.BL
{
    class CLS_PRINT
    {
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        SqlConnection sqlconnection;

        //This Constructor Inisialize the connection object
        public CLS_PRINT()
        {
            sqlconnection = BL.DAML.con;//

           // sqlconnection = new SqlConnection(@"server=SUPER-PC\INFOSOFT12;database=Product_DB;Integrated Security=True");
            //Data Source=TARIQ-PC;Initial Catalog=a;Integrated Security=True;Pooling=False
            ////////////////string mode = Properties.Settings.Default.Mode;
            ////////////////if (mode == "SQL")
            ////////////////{
            ////////////////    sqlconnection = new SqlConnection(@"Server=" + Properties.Settings.Default.Server + "; Database=" +
            ////////////////                                        Properties.Settings.Default.Database + "; Integrated Security=false; User ID=" +
            ////////////////                                        Properties.Settings.Default.ID + "; Password=" + Properties.Settings.Default.Password + "");
            ////////////////}
            ////////////////else
            ////////////////{
            ////////////////    sqlconnection = new SqlConnection(@"Server=" + Properties.Settings.Default.Server + "; Database=" + Properties.Settings.Default.Database + "; Integrated Security=true");
            ////////////////}
            
        }

        private DataTable LoadSalesData()
        {
            // Create a new DataSet and read sales data file 
            ////////    data.xml into the first DataTable.
            //////DataSet dataSet = new DataSet();
            //////dataSet.ReadXml(@"..\..\data.xml");
            //////return dataSet.Tables[0];


            //DataTable dtb = rs.select_report(textBox1.Text);

            //return dtb;
            SqlDataAdapter da = new SqlDataAdapter("select max(ref) from hdr", sqlconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int toprt = Convert.ToInt32(dt.Rows[0][0]);
            SqlDataAdapter dacr = new SqlDataAdapter("SELECT hdr.ref, hdr.contr, hdr.date, hdr.total, hdr.count, hdr.payed, hdr.saleman, hdr.req_no, dtl.barcode, dtl.name, dtl.price, dtl.qty FROM hdr CROSS JOIN dtl where hdr.ref=" + toprt + " and dtl.ref=" + toprt, sqlconnection);
            // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from hdr where ref=" + toprt, con2);
            // SqlDataAdapter chk = new SqlDataAdapter("select sum(isnull(is_req,0)) from dtl where ref=" + toprt, con2);

            DataSet1 ds = new DataSet1();

            dacr.Fill(ds, "DataTable1");
            // dacr1.Fill(ds, "hdr");
            // DataSet ds = new DataSet();
            // da.Fill(ds, "0");
            return ds.Tables["DataTable1"];
        }
        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.27in</PageWidth>
                <PageHeight>11.69in</PageHeight>
                <MarginTop>0.25in</MarginTop>
                <MarginLeft>0.25in</MarginLeft>
                <MarginRight>0.25in</MarginRight>
                <MarginBottom>0.25in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }
        // Create a local report for Report.rdlc, load the data,
        //    export the report to an .emf file, and print it.
        public void Run()
        {
            LocalReport report = new LocalReport();
            report.ReportPath = @"reports\Report2.rdlc";
            report.DataSources.Add(
            new ReportDataSource("DataSet1", LoadSalesData()));
            Export(report);
            Print();
        }

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

        //public static void Main1(string[] args)
        //{
        //    using (Form1 demo = new Form1())
        //    {
        //        demo.Run();
        //    }
        //}

        /*
        private void button6_Click(object sender, EventArgs e)
        {
            Run();
            //using (Form1 demo = new Form1())
            //{
            //    demo.Run();
            //}
        }
         * */



    }
}
