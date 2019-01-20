using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantservice
{
    public class TextFileCreator
    {
        private static string GetTextFileName()
        {
            string date = string.Format(DateTime.Now.ToString());
            date = date.Replace(':', '-');
            date = date.Replace(' ', '_');

            return date;
        }
        public static bool CreateInvoices(List<InvoiceDataRow> invoiceDataRows)
        {
            // TODO ändra till csv
            string textFilename = string.Format("{0}.txt", GetTextFileName());

            // Create new text file in folder
            string path = @"C:\Bestallning\Fakturaunderlag\Fakturaunderlag_" + textFilename;

            if (!File.Exists(path))
            { using (StreamWriter sw = File.CreateText(path)) { } }

            // Get unique Ids 
            List<string> brukareIds = (from hits in invoiceDataRows
                                       select hits.Id).Distinct().ToList();


            foreach (var brukareId in brukareIds)
            {
                List<InvoiceDataRow> brukareRows = (from hits in invoiceDataRows
                                                    where hits.Id == brukareId
                                                    select hits).ToList();

                AppentLinesToTextFile(brukareRows, path);
            }




            return true;
        }

        private static void AppentLinesToTextFile(List<InvoiceDataRow> dataRows, string path)
        {
            // 0 = Id 
            string brukareId = dataRows[0].Id;

            // 1 = Namn
            string brukareName = dataRows[0].Name;

            // 2 = Dagens
            string amountDaily = GetAmountDailySpecial(dataRows);

            // 3 = Spätta
            string amountSpatta = GetAmountRodSpatta(dataRows);

            // 4 = Totalt pris
            string totalPrice = GetTotalPrice(dataRows);


            using (StreamWriter sw = File.AppendText(path))
            {
                string insertString = string.Format("{0};{1};{2};{3};{4}",
                    brukareId,
                    brukareName,
                    amountDaily,
                    amountSpatta,
                    totalPrice
                    );


                sw.WriteLine(insertString);
            }
        }

        private static string GetAmountDailySpecial(List<InvoiceDataRow> dataRows)
        {
            // TODO gå på id istället för rättens namn

            InvoiceDataRow row = (from hits in dataRows
                                  where hits.Dish == "Dagens lunch"
                                  select hits).FirstOrDefault();

            return row != null ? row.Amount.ToString() : "";
        }

        private static string GetAmountRodSpatta(List<InvoiceDataRow> dataRows)
        {
            InvoiceDataRow row = (from hits in dataRows
                                  where hits.Dish == "Rödspätta med remouladsås"
                                  select hits).FirstOrDefault();

            return row != null ? row.Amount.ToString() : "";
        }
        private static string GetTotalPrice(List<InvoiceDataRow> dataRows)
        {
            int sum = 0;

            foreach (var row in dataRows)
            {
                int s = row.Amount * row.Price;
                sum += s;
            }

            return sum.ToString();
        }

        private static string MockMethod(List<InvoiceDataRow> dataRows)
        {


            return "";
        }
    }
}
