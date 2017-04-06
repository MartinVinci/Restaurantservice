using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantservice
{
    public class PdfCreator
    {

        public static void CreateTentativeOrder()
        {
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "Preliminär beställning " + DateTime.Now.ToString();
            PdfPage pdfPage = pdf.AddPage();

            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

            graph.DrawString("This is my first PDF document", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.Center);
















            string filePath = GetFileNameAndPath();
            pdf.Save(filePath);
            Process.Start(filePath);
        }
        private static string GetFileNameAndPath()
        {
            string time = string.Format(DateTime.Now.ToString());
            time = time.Replace(':', '-');
            time = time.Replace(' ', '_');
            string pdfFilename = string.Format("{0}.pdf", time);
            string filePath = @"C:\PreliminaraBestallningar\" + pdfFilename;

            return filePath;
        }
    }
}
