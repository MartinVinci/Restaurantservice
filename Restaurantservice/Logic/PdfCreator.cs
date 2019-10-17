using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        private static bool showAppConfigErrorMessage = true;

        #region Tentative Orders

        private static int SpaceBeforeHeader()
        {
            return 15;
        }

        private static int SpaceAfterText()
        {
            return 20;
        }

        private static XGraphics AddPdfPage(PdfDocument pdf, XGraphics graph)
        {
            var newPage = pdf.AddPage();
            graph.Dispose();
            return XGraphics.FromPdfPage(newPage);
        }

        private static bool TimeToAddNewPdfPage(int yCoord)
        {
            return yCoord > 750 ? true : false;
        }

        public static void CreateTentativeOrders(List<TentativeOrderList> orderLists, DateTime deliveryDate, string pickupRestaurant)
        {
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "Preliminära beställningar " + deliveryDate.ToShortDateString();
            PdfPage pdfPage = pdf.AddPage();

            XFont fontBig = new XFont("Times New Roman", 20, XFontStyle.Regular);
            XFont fontMedium = new XFont("Times New Roman", 16, XFontStyle.Regular);
            XFont fontSmall = new XFont("Times New Roman", 12, XFontStyle.Regular);
            XFont fontSmallBold = new XFont("Times New Roman", 12, XFontStyle.BoldItalic);

            XGraphics graph = XGraphics.FromPdfPage(pdfPage);

            // Write initial information
            string prelText = string.Format("Preliminära beställningar för: {0}, {1}.", pickupRestaurant, deliveryDate.ToShortDateString());
            graph.DrawString(prelText, fontBig, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), 35, 30, XStringFormats.TopLeft);

            string lookupText = string.Format("Informationen hämtad från databas: {0}.", DateTime.Now);
            graph.DrawString(lookupText, fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), 35, 60, XStringFormats.TopLeft);

            // Set beginning coordinates
            int nameXCoord = 40;
            int nameYCoord = 90;
            int quantityXCoord = 300;
            int quantityYCoord = 90;

            int pageCounter = 1;

            foreach (var orderList in orderLists)
            {
                if (orderList.OrderList.Count() > 0)
                {
                    nameYCoord += SpaceBeforeHeader();
                    quantityYCoord += SpaceBeforeHeader();

                    if (TimeToAddNewPdfPage(nameYCoord))
                    {
                        graph = AddPdfPage(pdf, graph);

                        pageCounter++;
                        graph.DrawString(string.Format("Sida: {0}", pageCounter), fontBig, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), 35, 30, XStringFormats.TopLeft);

                        nameXCoord = 40;
                        nameYCoord = 90;
                        quantityXCoord = 300;
                        quantityYCoord = 90;
                    }

                    graph.DrawString(orderList.KgPortalUser, fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), nameXCoord, nameYCoord, XStringFormats.TopLeft);

                    nameYCoord += SpaceAfterText();
                    quantityYCoord += SpaceAfterText();

                    if (orderList.OrderList.Count() > 0)
                    {
                        foreach (var order in orderList.OrderList)
                        {
                            if (TimeToAddNewPdfPage(nameYCoord))
                            {
                                graph = AddPdfPage(pdf, graph);

                                pageCounter++;
                                graph.DrawString(string.Format("Sida: {0}", pageCounter), fontBig, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), 35, 30, XStringFormats.TopLeft);

                                nameXCoord = 40;
                                nameYCoord = 90;
                                quantityXCoord = 300;
                                quantityYCoord = 90;
                            }

                            graph.DrawString(order.DishName, fontSmall, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), nameXCoord, nameYCoord, XStringFormats.TopLeft);

                            string quantityPlusInfoText = order.Quantity.ToString() + order.InfoText;
                            graph.DrawString(quantityPlusInfoText, fontSmall, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), quantityXCoord, quantityYCoord, XStringFormats.TopLeft);

                            nameYCoord += SpaceAfterText();
                            quantityYCoord += SpaceAfterText();
                        }
                        graph.DrawString("Totalt antal luncher", fontSmallBold, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), nameXCoord, nameYCoord, XStringFormats.TopLeft);
                        graph.DrawString(orderList.TotalDishCountInfo.ToString(), fontSmallBold, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), quantityXCoord, quantityYCoord, XStringFormats.TopLeft);

                        nameYCoord += SpaceAfterText();
                        quantityYCoord += SpaceAfterText();
                    }
                    else if (orderList.OrderList.Count() == 0)
                    {
                        graph.DrawString("Det finns inga beställningar för denna dag.", fontSmall, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), nameXCoord, nameYCoord, XStringFormats.TopLeft);
                        nameYCoord += SpaceAfterText();
                        quantityYCoord += SpaceAfterText();
                    }
                }
            }

            string filePath = GetFileNameAndPath(TYPE_TENTATIVE, false);
            pdf.Save(filePath);
            Process.Start(filePath);
        }
        #endregion

        #region Labels
        public static void CreateLabels(List<Order> orders, string date, bool tomorrow)
        {
            #region Multiple orderlist when testdata only has a few rows.
            //var orders2 = DataAccess.GetTodaysOrders(date);

            //orders.AddRange(orders2);
            //orders.AddRange(orders2);
            //orders.AddRange(orders2);
            //orders.AddRange(orders2);

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

           

            // Initial set up for page
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "Beställningar " + date;
            PdfPage pdfPage = pdf.AddPage();

            XFont fontBig = new XFont("Times New Roman", 20, XFontStyle.Regular);
            XFont fontMedium = new XFont("Times New Roman", 12, XFontStyle.Regular);
            XFont fontSmall = new XFont("Times New Roman", 12, XFontStyle.Regular);
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);


            /* Will during runtime have values between 1-10, sets position on label paper.
             * 12
             * 34
             * 56
             * 78
             * 9 10   */
            int counterPlaceHolder = 0;

            showAppConfigErrorMessage = true;

            // Draw the text
            foreach (var order in orders)
            {
                counterPlaceHolder++;
                DrawLabelOnPaper(graph, order, fontBig, fontMedium, fontSmall, counterPlaceHolder);

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
            string filePath = GetFileNameAndPath(TYPE_LABEL, tomorrow);
            pdf.Save(filePath);

            if (tomorrow == false)
            {
                showAppConfigErrorMessage = true;
                Process.Start(filePath);
            }
        }

        

        //private static void DrawLabelOnPaperSaved(XGraphics graph, Order label, XFont fontName, XFont fontOther, int position)
        //{
        //    Coordinate coord;

        //    // Left and right columns will have same X coordinates
        //    int Xleft = 160;
        //    int Xright = 440;

        //    // Startposition
        //    int sp = 0;

        //    switch (position)
        //    {
        //        case 1:
        //            sp = 45;
        //            coord = new Coordinate(Xleft, sp, Xleft, sp + 18, Xleft, sp + 33, Xleft, sp + 48, Xleft, sp + 67, Xleft, sp + 120);
        //            break;
        //        case 2:
        //            sp = 45;
        //            coord = new Coordinate(Xright, sp, Xright, sp + 18, Xright, sp + 33, Xright, sp + 48, Xright, sp + 67, Xright, sp + 120);
        //            break;
        //        case 3:
        //            sp = 205;
        //            coord = new Coordinate(Xleft, sp, Xleft, sp + 18, Xleft, sp + 33, Xleft, sp + 48, Xleft, sp + 67, Xleft, sp + 120);
        //            break;
        //        case 4:
        //            sp = 205;
        //            coord = new Coordinate(Xright, sp, Xright, sp + 18, Xright, sp + 33, Xright, sp + 48, Xright, sp + 67, Xright, sp + 120);
        //            break;
        //        case 5:
        //            sp = 365;
        //            coord = new Coordinate(Xleft, sp, Xleft, sp + 18, Xleft, sp + 33, Xleft, sp + 48, Xleft, sp + 67, Xleft, sp + 120);
        //            break;
        //        case 6:
        //            sp = 365;
        //            coord = new Coordinate(Xright, sp, Xright, sp + 18, Xright, sp + 33, Xright, sp + 48, Xright, sp + 67, Xright, sp + 120);
        //            break;
        //        case 7:
        //            sp = 530;
        //            coord = new Coordinate(Xleft, sp, Xleft, sp + 18, Xleft, sp + 33, Xleft, sp + 48, Xleft, sp + 67, Xleft, sp + 120);
        //            break;
        //        case 8:
        //            sp = 530;
        //            coord = new Coordinate(Xright, sp, Xright, sp + 18, Xright, sp + 33, Xright, sp + 48, Xright, sp + 67, Xright, sp + 120);
        //            break;
        //        case 9:
        //            sp = 690;
        //            coord = new Coordinate(Xleft, sp, Xleft, sp + 18, Xleft, sp + 33, Xleft, sp + 48, Xleft, sp + 67, Xleft, sp + 120);
        //            break;
        //        case 10:
        //            sp = 690;
        //            coord = new Coordinate(Xright, sp, Xright, sp + 18, Xright, sp + 33, Xright, sp + 48, Xright, sp + 67, Xright, sp + 120);
        //            break;
        //        default:
        //            coord = new Coordinate(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        //            break;
        //    }

        //    DrawLabelOnPaperByPosition(graph, label, fontName, fontOther, coord);
        //}
        //private static void DrawLabelOnPaperByPositionSaved(XGraphics graph, Order label, XFont fontName, XFont fontOther, Coordinate c)
        //{
        //    graph.DrawString(label.Name, fontName, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.NameXcoord, c.NameYcoord, XStringFormats.Center);
        //    graph.DrawString(label.Dish, fontOther, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.DishXcoord, c.DishYcoord, XStringFormats.Center);
        //    graph.DrawString(label.Date, fontOther, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.DateXcoord, c.DateYcoord, XStringFormats.Center);
        //    graph.DrawString(label.Addr, fontOther, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.AddrXcoord, c.AddrYcoord, XStringFormats.Center);

        //    graph.DrawImage(XImage.FromFile(label.Logo), c.LogoXcoord - 60, c.LogoYcoord);

        //    if (label.DeliverCold)
        //    {
        //        graph.DrawString("Levereras kall", fontName, new XSolidBrush(XColor.FromCmyk(100, 33, 0, 0)), c.ColdXcoord, c.ColdYcoord, XStringFormats.Center);
        //    }
        //}

        private static void DrawLabelOnPaper(XGraphics graph, Order label, XFont fontBig, XFont fontMedium, XFont fontSmall, int position)
        {
            Coordinate coord;

            // Read X coordinate adjustments from user
            int manuallyAdjustLeftColumn = 0;
            int manuallyAdjustRigthColumn = 0;

            try
            {
                manuallyAdjustLeftColumn = Form1.LEFT_COLUMN_ADJUST;
                manuallyAdjustRigthColumn = Form1.RIGHT_COLUMN_ADJUST;
            }
            catch
            {
                if (showAppConfigErrorMessage != false)
                {
                    System.Windows.Forms.MessageBox.Show("Det kan hända att positioneringen av etiketterna inte är helt centrerad, kolla med Andreas eller Niklas om varför");
                }

                showAppConfigErrorMessage = false;
            }
            // Left and right columns will have same y coordinates

            int Xleft = 160 + manuallyAdjustLeftColumn;
            int Xright = 440 + manuallyAdjustRigthColumn;

            // Startposition
            int sp = 0;

            int dish = 18;
            int date = 33;
            int addr = 48;

            int cold = 63;
            int spec = 63;
            int timb = 63;
            int rice = 78;
            int glut = 78;
            int lact = 78;

            int logo = 93;

            int adjDevLeft = 60;
            int adjDevRight = 60;

            switch (position)
            {
                case 1:
                    sp = 45;
                    coord = new Coordinate(Xleft, sp, Xleft, sp + dish, Xleft, sp + date, Xleft, sp + addr, Xleft, sp + logo,
                        (Xleft - adjDevLeft), sp + cold, (Xleft + adjDevRight), sp + spec, (Xleft - adjDevLeft), sp + rice, (Xleft + adjDevRight), sp + glut, Xleft, sp + lact, Xleft, sp + timb);
                    break;
                case 2:
                    sp = 45;
                    coord = new Coordinate(Xright, sp, Xright, sp + dish, Xright, sp + date, Xright, sp + addr, Xright, sp + logo,
                        (Xright - adjDevLeft), sp + cold, (Xright + adjDevRight), sp + spec, (Xright - adjDevLeft), sp + rice, (Xright + adjDevRight), sp + glut, Xright, sp + lact, Xright, sp + timb);
                    break;
                case 3:
                    sp = 205;
                    coord = new Coordinate(Xleft, sp, Xleft, sp + dish, Xleft, sp + date, Xleft, sp + addr, Xleft, sp + logo,
                        (Xleft - adjDevLeft), sp + cold, (Xleft + adjDevRight), sp + spec, (Xleft - adjDevLeft), sp + rice, (Xleft + adjDevRight), sp + glut, Xleft, sp + lact, Xleft, sp + timb);
                    break;
                case 4:
                    sp = 205;
                    coord = new Coordinate(Xright, sp, Xright, sp + dish, Xright, sp + date, Xright, sp + addr, Xright, sp + logo,
                        (Xright - adjDevLeft), sp + cold, (Xright + adjDevRight), sp + spec, (Xright - adjDevLeft), sp + rice, (Xright + adjDevRight), sp + glut, Xright, sp + lact, Xright, sp + timb);
                    break;
                case 5:
                    sp = 365;
                    coord = new Coordinate(Xleft, sp, Xleft, sp + dish, Xleft, sp + date, Xleft, sp + addr, Xleft, sp + logo,
                        (Xleft - adjDevLeft), sp + cold, (Xleft + adjDevRight), sp + spec, (Xleft - adjDevLeft), sp + rice, (Xleft + adjDevRight), sp + glut, Xleft, sp + lact, Xleft, sp + timb);
                    break;
                case 6:
                    sp = 365;
                    coord = new Coordinate(Xright, sp, Xright, sp + dish, Xright, sp + date, Xright, sp + addr, Xright, sp + logo,
                        (Xright - adjDevLeft), sp + cold, (Xright + adjDevRight), sp + spec, (Xright - adjDevLeft), sp + rice, (Xright + adjDevRight), sp + glut, Xright, sp + lact, Xright, sp + timb);
                    break;
                case 7:
                    sp = 530;
                    coord = new Coordinate(Xleft, sp, Xleft, sp + dish, Xleft, sp + date, Xleft, sp + addr, Xleft, sp + logo,
                        (Xleft - adjDevLeft), sp + cold, (Xleft + adjDevRight), sp + spec, (Xleft - adjDevLeft), sp + rice, (Xleft + adjDevRight), sp + glut, Xleft, sp + lact, Xleft, sp + timb);
                    break;
                case 8:
                    sp = 530;
                    coord = new Coordinate(Xright, sp, Xright, sp + dish, Xright, sp + date, Xright, sp + addr, Xright, sp + logo,
                        (Xright - adjDevLeft), sp + cold, (Xright + adjDevRight), sp + spec, (Xright - adjDevLeft), sp + rice, (Xright + adjDevRight), sp + glut, Xright, sp + lact, Xright, sp + timb);
                    break;
                case 9:
                    sp = 690;
                    coord = new Coordinate(Xleft, sp, Xleft, sp + dish, Xleft, sp + date, Xleft, sp + addr, Xleft, sp + logo,
                        (Xleft - adjDevLeft), sp + cold, (Xleft + adjDevRight), sp + spec, (Xleft - adjDevLeft), sp + rice, (Xleft + adjDevRight), sp + glut, Xleft, sp + lact, Xleft, sp + timb);
                    break;
                case 10:
                    sp = 690;
                    coord = new Coordinate(Xright, sp, Xright, sp + dish, Xright, sp + date, Xright, sp + addr, Xright, sp + logo,
                        (Xright - adjDevLeft), sp + cold, (Xright + adjDevRight), sp + spec, (Xright - adjDevLeft), sp + rice, (Xright + adjDevRight), sp + glut, Xright, sp + lact, Xright, sp + timb);
                    break;
                default:
                    coord = new Coordinate(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    break;
            }

            DrawLabelOnPaperByPosition(graph, label, fontBig, fontMedium, fontSmall, coord);
        }

        private static void DrawLabelOnPaperByPosition(XGraphics graph, Order label, XFont fontBig, XFont fontMedium, XFont fontSmall, Coordinate c)
        {
            graph.DrawString(label.Name, fontBig, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.NameXcoord, c.NameYcoord, XStringFormats.Center);
            graph.DrawString(label.Dish, fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.DishXcoord, c.DishYcoord, XStringFormats.Center);
            graph.DrawString(label.Date, fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.DateXcoord, c.DateYcoord, XStringFormats.Center);
            graph.DrawString(label.Addr, fontMedium, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.AddrXcoord, c.AddrYcoord, XStringFormats.Center);

            if (label.DeliverCold)
            {
                graph.DrawString("Lev kall", fontSmall, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.ColdXcoord, c.ColdYcoord, XStringFormats.Center);
            }

            if (label.TypeGroup != string.Empty)
            {
                string typeGroupText = typeGroupText = ("Väska " + label.TypeGroup);
                if (label.CaseGroup != "")
                {
                    typeGroupText += " - " + label.CaseGroup;
                }
                
                graph.DrawString(typeGroupText, fontSmall, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.SpecXcoord, c.SpecYcoord, XStringFormats.Center);
            }

            if (label.NoRice)
            {
                graph.DrawString("Inget ris", fontSmall, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.RiceXcoord, c.RiceYcoord, XStringFormats.Center);
            }

            if (label.NoGluten)
            {
                graph.DrawString("Glutenfritt", fontSmall, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.GlutXcoord, c.GlutYcoord, XStringFormats.Center);
            }

            if (label.NoLactose)
            {
                graph.DrawString("Laktosfritt", fontSmall, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.LactXcoord, c.LactYcoord, XStringFormats.Center);
            }

            if (label.Timbal)
            {
                graph.DrawString("Timbal", fontSmall, new XSolidBrush(XColor.FromCmyk(0, 0, 0, 100)), c.TimbXcoord, c.TimbYcoord, XStringFormats.Center);
            }

            graph.DrawImage(XImage.FromFile(label.Logo), c.LogoXcoord - 60, c.LogoYcoord);
        }

        #endregion

        private static string GetFileNameAndPath(string type, bool tomorrow)
        {
            DateTime date = DateTime.Now;
            if (tomorrow)
            {
                date = date.AddDays(1);
            }
            string time = string.Format(date.ToString());
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
                if (tomorrow == false)
                {
                    filePath = @"C:\Bestallning\Etiketter\" + "Etiketter_" + pdfFilename;
                }
                else
                {
                    filePath = @"C:\Bestallning\Panikmapp\" + "Etiketter_" + pdfFilename;

                }
            }

            return filePath;
        }
    }
}
