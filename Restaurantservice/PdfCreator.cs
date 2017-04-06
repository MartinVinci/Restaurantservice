using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
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

            XFont fontName = new XFont("Times New Roman", 20, XFontStyle.Regular);
            XFont fontOther = new XFont("Times New Roman", 12, XFontStyle.Regular);
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);

            Label testLabel1 = new Label("Gunnel Nilsson", "Boef Bourguignon", "fredag 7/4", "Kyrkbyn", false);
            Label testLabel2 = new Label("Gunnel Nilsson", "Lasagne", "fredag 7/4", "En lång adress någonstans", true);
            Label testLabel3 = new Label("Gunnel Nilsson", "Boef Bourguignon", "fredag 7/4", "Kyrkbyn", true);
            Label testLabel4 = new Label("Gunnel Nilsson", "Spätta", "fredag 7/4", "Kyrkbyn", false);
            Label testLabel5 = new Label("Gunnel Nilsson", "Gravad lax", "fredag 7/4", "Kyrkbyn", false);
            Label testLabel6 = new Label("Gunnel Nilsson", "Boef Bourguignon", "fredag 7/4", "Kyrkbyn", true);
            Label testLabel7 = new Label("Gunnel Nilsson", "Boef Bourguignon", "fredag 7/4", "Kyrkbyn", false);
            Label testLabel8 = new Label("Gunnel Nilsson", "Boef Bourguignon", "fredag 7/4", "Kyrkbyn", true);
            Label testLabel9 = new Label("Gunnel Nilsson", "Boef Bourguignon", "fredag 7/4", "Kyrkbyn", false);
            Label testLabel10 = new Label("Gunnel Nilsson", "Boef Bourguignon", "fredag 7/4", "Kyrkbyn", true);


            //XTextFormatter tf = new XTextFormatter(graph);

            //tf.Alignment = XParagraphAlignment.Center;
            //tf.DrawString("hej", fontName, XBrushes.Black,  XStringFormats.TopLeft);

            DrawOnPaper(graph, testLabel1, fontName, fontOther, 1);
            DrawOnPaper(graph, testLabel2, fontName, fontOther, 2);
            DrawOnPaper(graph, testLabel3, fontName, fontOther, 3);
            DrawOnPaper(graph, testLabel4, fontName, fontOther, 4);
            DrawOnPaper(graph, testLabel5, fontName, fontOther, 5);
            DrawOnPaper(graph, testLabel6, fontName, fontOther, 6);
            DrawOnPaper(graph, testLabel7, fontName, fontOther, 7);
            DrawOnPaper(graph, testLabel8, fontName, fontOther, 8);
            DrawOnPaper(graph, testLabel9, fontName, fontOther, 9);
            DrawOnPaper(graph, testLabel10, fontName, fontOther, 10);

            //graph.DrawString("1", fontName, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Millimeter, pdfPage.Height.Millimeter), XStringFormats.TopLeft);

            // Draw the text


            string filePath = GetFileNameAndPath();
            pdf.Save(filePath);
            Process.Start(filePath);
        }
        private static void DrawOnPaper(XGraphics graph, Label label, XFont fontName, XFont fontOther, int position)
        {
            Coordinate coord;
            int Xleft = 160;
            int Xright = 440;

            int sp = 0; // startposition

            switch (position)
            {
                case 1:
                    sp = 45;
                    coord = new Coordinate(Xleft, sp, Xleft, sp + 18, Xleft, sp + 33, Xleft, sp + 48, Xleft, sp + 67, Xleft, sp + 120);
                    break;
                case 2:
                    sp = 45;
                    coord = new Coordinate(Xright, sp, Xright, sp + 18, Xright, sp + 33, Xright, sp + 48, Xright, sp + 67, Xright, sp + 120);
                    break;
                case 3:
                    sp = 205;
                    coord = new Coordinate(Xleft, sp, Xleft, sp + 18, Xleft, sp + 33, Xleft, sp + 48, Xleft, sp + 67, Xleft, sp + 120);
                    break;
                case 4:
                    sp = 205;
                    coord = new Coordinate(Xright, sp, Xright, sp + 18, Xright, sp + 33, Xright, sp + 48, Xright, sp + 67, Xright, sp + 120);
                    break;
                case 5:
                    sp = 365;
                    coord = new Coordinate(Xleft, sp, Xleft, sp + 18, Xleft, sp + 33, Xleft, sp + 48, Xleft, sp + 67, Xleft, sp + 120);
                    break;
                case 6:
                    sp = 365;
                    coord = new Coordinate(Xright, sp, Xright, sp + 18, Xright, sp + 33, Xright, sp + 48, Xright, sp + 67, Xright, sp + 120);
                    break;
                case 7:
                    sp = 530;
                    coord = new Coordinate(Xleft, sp, Xleft, sp + 18, Xleft, sp + 33, Xleft, sp + 48, Xleft, sp + 67, Xleft, sp + 120);
                    break;
                case 8:
                    sp = 530;
                    coord = new Coordinate(Xright, sp, Xright, sp + 18, Xright, sp + 33, Xright, sp + 48, Xright, sp + 67, Xright, sp + 120);
                    break;
                case 9:
                    sp = 690;
                    coord = new Coordinate(Xleft, sp, Xleft, sp + 18, Xleft, sp + 33, Xleft, sp + 48, Xleft, sp + 67, Xleft, sp + 120);
                    break;
                case 10:
                    sp = 690;
                    coord = new Coordinate(Xright, sp, Xright, sp + 18, Xright, sp + 33, Xright, sp + 48, Xright, sp + 67, Xright, sp + 120);
                    break;
                default:
                    coord = new Coordinate(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
            }


            DrawOnPaperByPosition(graph, label, fontName, fontOther, coord);
        }
        private static void DrawOnPaperByPosition(XGraphics graph, Label label, XFont fontName, XFont fontOther, Coordinate c)
        {
            graph.DrawString(label.Name, fontName, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.NameXcoord, c.NameYcoord, XStringFormats.Center);
            graph.DrawString(label.Dish, fontOther, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.DishXcoord, c.DishYcoord, XStringFormats.Center);
            graph.DrawString(label.Date, fontOther, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.DateXcoord, c.DateYcoord, XStringFormats.Center);
            graph.DrawString(label.Addr, fontOther, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.AddrXcoord, c.AddrYcoord, XStringFormats.Center);

            graph.DrawImage(XImage.FromFile(label.Logo), c.LogoXcoord - 60, c.LogoYcoord);

            if (label.DeliverCold)
            {
                graph.DrawString("Levereras kall", fontName, new XSolidBrush(XColor.FromCmyk(100, 33, 0, 0)), c.ColdXcoord, c.ColdYcoord, XStringFormats.Center);
            }

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
