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
        private const string TYPE_TENTATIVE = "Tentative";
        private const string TYPE_LABEL = "Label";

        #region Tentative Orders

        public static void CreateTentativeOrders(List<TentativeOrder> orders, DateTime deliveryDate)
        {
            // Initial set up for page
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "Preliminära beställningar " + deliveryDate.ToShortDateString();
            PdfPage pdfPage = pdf.AddPage();

            XFont fontName = new XFont("Times New Roman", 20, XFontStyle.Regular);
            XFont fontOther = new XFont("Times New Roman", 10, XFontStyle.Regular);

            XGraphics graph = XGraphics.FromPdfPage(pdfPage);

            // Write initial information
            string prelText = string.Format("Preliminära beställningar för: {0}.", deliveryDate.ToShortDateString());
            graph.DrawString(prelText, fontName, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), 35, 30, XStringFormats.TopLeft);
            
            string lookupText = string.Format("Informationen hämtad från databas: {0}.", DateTime.Now);
            graph.DrawString(lookupText, fontOther, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), 35, 60, XStringFormats.TopLeft);

            // Set beginning coordinates
            int nameXCoord = 40;
            int nameYCoord = 90;
            int quantityXCoord = 500;
            int quantityYCoord = 90;

            if (orders.Count > 0)
            {
                foreach (var item in orders)
                {
                    graph.DrawString(item.DishName, fontName, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), nameXCoord, nameYCoord, XStringFormats.TopLeft);
                    graph.DrawString(item.Quantity.ToString(), fontName, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), quantityXCoord, quantityYCoord, XStringFormats.TopLeft);

                    nameYCoord += 35;
                    quantityYCoord += 35;
                }
            }
            else if (orders.Count == 0)
            {
                graph.DrawString("Det finns inga beställningar för denna dag.", fontName, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), nameXCoord, nameYCoord, XStringFormats.TopLeft);
            }

            string filePath = GetFileNameAndPath(TYPE_TENTATIVE);
            pdf.Save(filePath);
            Process.Start(filePath);
        }
        #endregion

        #region Labels
        public static void CreateLabels(List<Order> orders, string date)
        {
            #region Multiple orderlist when testdata only has a few rows.
            var orders2 = DataAccess.GetTodaysOrders(date);

            orders.AddRange(orders2);
            orders.AddRange(orders2);
            orders.AddRange(orders2);
            orders.AddRange(orders2);

            #endregion  
            #region TestOrders without using database
            //Order testLabel1 = new Order("Gunnel Nilsson", "Boef Bourguignon", "fredag 7/4", "Kyrkbyn", false);
            //Order testLabel2 = new Order("Gunnel Nilsson", "Lasagne", "fredag 7/4", "En lång adress någonstans", true);
            //Order testLabel3 = new Order("Gunnel Nilsson", "Boef Bourguignon", "fredag 7/4", "Kyrkbyn", true);
            //Order testLabel4 = new Order("Gunnel Nilsson", "Spätta", "fredag 7/4", "Kyrkbyn", false);
            //Order testLabel5 = new Order("Gunnel Nilsson", "Gravad lax", "fredag 7/4", "Kyrkbyn", false);
            //Order testLabel6 = new Order("Gunnel Nilsson", "Boef Bourguignon", "fredag 7/4", "Kyrkbyn", true);
            //Order testLabel7 = new Order("Gunnel Nilsson", "Boef Bourguignon", "fredag 7/4", "Kyrkbyn", false);
            //Order testLabel8 = new Order("Gunnel Nilsson", "Boef Bourguignon", "fredag 7/4", "Kyrkbyn", true);
            //Order testLabel9 = new Order("Gunnel Nilsson", "Boef Bourguignon", "fredag 7/4", "Kyrkbyn", false);
            //Order testLabel10 = new Order("Gunnel Nilsson", "Boef Bourguignon", "fredag 7/4", "Kyrkbyn", true);


            //DrawLabelOnPaper(graph, testLabel1, fontName, fontOther, 1);
            //DrawLabelOnPaper(graph, testLabel2, fontName, fontOther, 2);
            //DrawLabelOnPaper(graph, testLabel3, fontName, fontOther, 3);
            //DrawLabelOnPaper(graph, testLabel4, fontName, fontOther, 4);
            //DrawLabelOnPaper(graph, testLabel5, fontName, fontOther, 5);
            //DrawLabelOnPaper(graph, testLabel6, fontName, fontOther, 6);
            //DrawLabelOnPaper(graph, testLabel7, fontName, fontOther, 7);
            //DrawLabelOnPaper(graph, testLabel8, fontName, fontOther, 8);
            //DrawLabelOnPaper(graph, testLabel9, fontName, fontOther, 9);
            //DrawLabelOnPaper(graph, testLabel10, fontName, fontOther, 10);

            #endregion

            // Sort by delivery address. 
            orders = orders.OrderBy(o => o.Addr).ToList();

            // Initial set up for page
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "Beställningar " + date;
            PdfPage pdfPage = pdf.AddPage();

            XFont fontName = new XFont("Times New Roman", 20, XFontStyle.Regular);
            XFont fontOther = new XFont("Times New Roman", 12, XFontStyle.Regular);
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);


            /* Will during runtime have values between 1-10, sets position on label paper.
             * 12
             * 34
             * 56
             * 78
             * 9 10   */
            int counterPlaceHolder = 0;
            
            // Draw the text
            foreach (var order in orders)
            {
                counterPlaceHolder++;
                DrawLabelOnPaper(graph, order, fontName, fontOther, counterPlaceHolder);

                // Used when one paper is full
                if (counterPlaceHolder == 10)
                {
                    counterPlaceHolder = 0;
                    var newPage = pdf.AddPage();
                    graph.Dispose();
                    graph = XGraphics.FromPdfPage(newPage);
                }
            }

            // Save Pdf on disk
            string filePath = GetFileNameAndPath(TYPE_LABEL);
            pdf.Save(filePath);
            Process.Start(filePath);
        }
        
        private static void DrawLabelOnPaper(XGraphics graph, Order label, XFont fontName, XFont fontOther, int position)
        {
            Coordinate coord;

            // Left and right columns will have same X coordinates
            int Xleft = 160;
            int Xright = 440;

            // Startposition
            int sp = 0; 

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

            DrawLabelOnPaperByPosition(graph, label, fontName, fontOther, coord);
        }
        private static void DrawLabelOnPaperByPosition(XGraphics graph, Order label, XFont fontName, XFont fontOther, Coordinate c)
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

        #endregion

        private static string GetFileNameAndPath(string type)
        {
            string time = string.Format(DateTime.Now.ToString());
            time = time.Replace(':', '-');
            time = time.Replace(' ', '_');
            string pdfFilename = string.Format("{0}.pdf", time);

            string filePath = "";
            if (type == TYPE_TENTATIVE)
            {
                filePath = @"C:\Bestallning\PreliminaraBestallningar\" + "Prel_" + pdfFilename;
            }
            else if (type == TYPE_LABEL)
            {
                filePath = @"C:\Bestallning\Etiketter\" + "Etiketter_" + pdfFilename;
            }

            return filePath;
        }
    }
}
