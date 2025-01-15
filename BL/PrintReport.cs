//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace POS.BL
//{
//    class PrintReport
//    {
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Reporting.WinForms;
using System.Reflection;
using System.Drawing.Printing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace POS.BL
{
     public static class PrintReport{

         private static int m_currentPageIndex;
         /// <summary>
         public static string pagetype,pagewidth,pagehight;
         /// </summary>
         private static IList<Stream> m_streams;

         public static Stream CreateStream(string name,
           string fileNameExtension, Encoding encoding,
           string mimeType, bool willSeek)
         {
             Stream stream = new MemoryStream();
             m_streams.Add(stream);
             return stream;
         }

         public static void Export(LocalReport report, bool print = true)
         {
             string deviceInfo;
//             string deviceInfo =pagetype.Equals("Sal")?
////               @"<DeviceInfo>
////                <OutputFormat>EMF</OutputFormat>
////                <PageWidth>6.2in</PageWidth>
////                <PageHeight>8.3in</PageHeight>
////                <MarginTop>0.1in</MarginTop>
////                <MarginLeft>0.1in</MarginLeft>
////                <MarginRight>0.1in</MarginRight>
////                <MarginBottom>0.1in</MarginBottom>
////            </DeviceInfo>";
// @"<DeviceInfo>
//                <OutputFormat>EMF</OutputFormat>
//                <PageWidth>8.50in</PageWidth>
//                <PageHeight>11in</PageHeight>
//                <MarginTop>0.1in</MarginTop>
//                <MarginLeft>0.1in</MarginLeft>
//                <MarginRight>0.1in</MarginRight>
//                <MarginBottom>0.1in</MarginBottom>
//            </DeviceInfo>" :
//   @"<DeviceInfo>
//                <OutputFormat>EMF</OutputFormat>
//                <PageWidth>8.27in</PageWidth>
//                <PageHeight>11.69in</PageHeight>
//                <MarginTop>0.1in</MarginTop>
//                <MarginLeft>0.9in</MarginLeft>
//                <MarginRight>0.1in</MarginRight>
//                <MarginBottom>0.1in</MarginBottom>
//            </DeviceInfo>";

             if (!string.IsNullOrEmpty(pagetype) && string.IsNullOrEmpty(pagewidth) && string.IsNullOrEmpty(pagehight))
             {
                 deviceInfo = pagetype.Equals("Sal") ?
                     //               @"<DeviceInfo>
                     //                <OutputFormat>EMF</OutputFormat>
                     //                <PageWidth>6.2in</PageWidth>
                     //                <PageHeight>8.3in</PageHeight>
                     //                <MarginTop>0.1in</MarginTop>
                     //                <MarginLeft>0.1in</MarginLeft>
                     //                <MarginRight>0.1in</MarginRight>
                     //                <MarginBottom>0.1in</MarginBottom>
                     //            </DeviceInfo>";
 @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.50in</PageWidth>
                <PageHeight>11in</PageHeight>
                <MarginTop>0.1in</MarginTop>
                <MarginLeft>0.1in</MarginLeft>
                <MarginRight>0.1in</MarginRight>
                <MarginBottom>0.1in</MarginBottom>
            </DeviceInfo>" :
   @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.27in</PageWidth>
                <PageHeight>11.69in</PageHeight>
                <MarginTop>0.1in</MarginTop>
                <MarginLeft>0.9in</MarginLeft>
                <MarginRight>0.1in</MarginRight>
                <MarginBottom>0.1in</MarginBottom>
            </DeviceInfo>";

             }
             else
             {
                 if (string.IsNullOrEmpty(pagetype) && !string.IsNullOrEmpty(pagewidth) && !string.IsNullOrEmpty(pagehight))
                 {
                     deviceInfo = @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>"+pagewidth+@"</PageWidth>
                <PageHeight>"+pagehight+@"</PageHeight>
                <MarginTop>0.1in</MarginTop>
                <MarginLeft>0.1in</MarginLeft>
                <MarginRight>0.1in</MarginRight>
                <MarginBottom>0.1in</MarginBottom>
                </DeviceInfo>";
                 }
                 else
                 {
                     deviceInfo = @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>" + pagewidth + @"</PageWidth>
                <PageHeight>" + pagehight + @"</PageHeight>
                <MarginTop>0.1in</MarginTop>
                <MarginLeft>0.1in</MarginLeft>
                <MarginRight>0.1in</MarginRight>
                <MarginBottom>0.1in</MarginBottom>
             </DeviceInfo>";
                 }
             
             }
             
             Warning[] warnings;
             m_streams = new List<Stream>();
             report.Render("Image", deviceInfo, CreateStream,
                out warnings);
             foreach (Stream stream in m_streams)
                 stream.Position = 0;

             if (print)
             {
                 Print();
             }
         }


         // Handler for PrintPageEvents
         public static void PrintPage(object sender, PrintPageEventArgs ev)
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

         public static void Print()
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

         public static void PrintToPrinter(this LocalReport report)
         {
             Export(report);
         }

         public static void DisposePrint()
         {
             if (m_streams != null)
             {
                 foreach (Stream stream in m_streams)
                     stream.Close();
                 m_streams = null;
             }
         }
     }
}
