using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.Entity;
using MySql.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;


namespace Restaurantservice
{
    public partial class Form1 : Form
    {
        public static string DATABASETEST = "BRADGARD_NU";
        public static string DATABASELIVE = "KNIVOCHGAFFEL";

        public static string PICKUP_MOBILIA = "mobilia";
        public static string PICKUP_JAGERSRO = "jägersro";

        public static int LEFT_COLUMN_ADJUST = Int32.Parse(ConfigurationManager.AppSettings["LeftColumnPosition"]);
        public static int RIGHT_COLUMN_ADJUST = Int32.Parse(ConfigurationManager.AppSettings["RigthColumnPosition"]);

        public static List<string> extraLabelList = new List<string>()
        {
            //TODO: Fixa listan när Niklas återkommit
            "7. Hemgravad norsk fjordlax",
            "Bakad potatis",
        };

        public static string DataBaseVersion = "";

        public Form1()
        {
            InitializeComponent();
            CustomInitialize();
            //DevelopmentInitialize(); // TODO remove at release
        }
        private void DevelopmentInitialize()
        {

            //CreateLabels(DateTime.Now.ToShortDateString(), false, PICKUP_JAGERSRO);
            //CreateLabels("2017-12-13", false, PICKUP_JAGERSRO);

            //DataBaseVersion = DATABASETEST;
            //CreateTentativeOrders(DateTime.Now, PICKUP_JAGERSRO);

        }
        private void CustomInitialize()
        {
            rbnTodaysDate.Checked = true;
            dtpDateTimePicker.Enabled = false;
            rbnRealDatabase.Checked = true;
            rbnRealDatabase.Enabled = false;
            rbnTestDataBase.Enabled = false;
            lblVersion.Text = "Version: 2.1 - 18-06-26";

            DataBaseVersion = DATABASELIVE;

            nudLeftColumn.Enabled = false;
            nudLeftColumn.Maximum = 1000;
            nudLeftColumn.Minimum = -1000;
            nudLeftColumn.Value = LEFT_COLUMN_ADJUST;

            nudRightColumn.Enabled = false;
            nudRightColumn.Maximum = 1000;
            nudRightColumn.Minimum = -1000;
            nudRightColumn.Value = RIGHT_COLUMN_ADJUST;
            btnColumnXPosition.Enabled = false;

        }

        #region Forms actions
        private void rbnPickDate_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    dtpDateTimePicker.Enabled = true;
                }
            }
        }
        private void rbnTodaysDate_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    dtpDateTimePicker.Enabled = false;
                }
            }
        }
        private void rbnTestDataBase_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    DataBaseVersion = DATABASETEST;
                }
            }
        }

        private void rbnRealDatabase_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    DataBaseVersion = DATABASELIVE;
                }
            }
        }
        private void btnTentativeOrders_Click(object sender, EventArgs e)
        {
            string pickupRest = "";
            if (rbnPrelPickupMobilia.Checked == true)
            {
                pickupRest = PICKUP_MOBILIA;
            }
            else if (rbnPrelPickupJägersro.Checked == true)
            {
                pickupRest = PICKUP_JAGERSRO;
            }

            if (pickupRest == "")
            {
                MessageBox.Show("Du har inte valt någon restaurang.");
            }
            else
            {
                if (rbnTodaysDate.Checked == true)
                {
                    CreateTentativeOrders(DateTime.Now, pickupRest);
                }
                else if (rbnPickDate.Checked == true)
                {
                    CreateTentativeOrders(dtpDateTimePicker.Value, pickupRest);
                }
            }
        }

        private void btnCreateLabels_Click(object sender, EventArgs e)
        {
            string pickupRest = "";
            if (rbnPickupMobilia.Checked == true)
            {
                pickupRest = PICKUP_MOBILIA;
            }
            else if (rbnPickupJagersro.Checked == true)
            {
                pickupRest = PICKUP_JAGERSRO;
            }

            if (pickupRest == "")
            {
                MessageBox.Show("Du har inte valt någon restaurang.");
            }
            else
            {
                // Today
                CreateLabels(DateTime.Now.ToShortDateString(), false, pickupRest);

                // Tomorrow
                DateTime nextDeliveryDate = DateTime.Now;
                nextDeliveryDate = nextDeliveryDate.AddDays(1);
                CreateLabels(nextDeliveryDate.ToShortDateString(), true, pickupRest);
            }

            #region Three days forward
            //int dayOfWeek = (int)nextDeliveryDate.DayOfWeek;

            //int tomorrow = dayOfWeek++;

            //if (dayOfWeek == 5)
            //{
            //    nextDeliveryDate = nextDeliveryDate.AddDays(3);
            //}
            //else if (tomorrow == 6)
            //{
            //    nextDeliveryDate = nextDeliveryDate.AddDays(2);
            //}
            //else 
            //{
            //    nextDeliveryDate = nextDeliveryDate.AddDays(1);
            //}

            //CreateLabels(nextDeliveryDate.ToShortDateString(), true);
            #endregion
        }
        private void btnCreateInvoices_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ingen funktionalitet utvecklad för denna knapp än.");
            //CreateInvoiceCsvFile();
        }

        private void btnAdminPassWord_Click(object sender, EventArgs e)
        {
            if (tbxAdminPassWord.Text == "niklas")
            {
                rbnRealDatabase.Enabled = true;
                rbnTestDataBase.Enabled = true;

                nudLeftColumn.Enabled = true;
                nudRightColumn.Enabled = true;
                btnColumnXPosition.Enabled = true;
            }
            else
            {
                MessageBox.Show("Fel lösenord");
            }
        }

        private void btnColumnXPosition_Click(object sender, EventArgs e)
        {
            ChangeColumnsAdjustments();
        }

        private void btnColumnAdjustmentInformation_Click(object sender, EventArgs e)
        {
            string text = "Här kan du ändra positionen i sidled för etiketterna. \n";
            text += "Du flyttar de fem etiketterna som finns på vänstra ";
            text += "sidan av pappret genom att ändra siffran uppåt eller nedåt. ";
            text += "Tänk på att det går att ha negativa tal också, tex -5.\n\n";
            text += "Ökar du siffran kommer etiketterna hamna längre till höger ";
            text += "på pappret och minskar du siffran flyttas etiketterna till vänster. \n\n";
            text += "Du får prova dig fram till vad som blir bra, men höjs talet med 10 ";
            text += "innebär det ca 4 mm.";

            MessageBox.Show(text);
        }
        #endregion

        #region Methods
        private void ChangeColumnsAdjustments()
        {
            try
            {
                int leftAdjust = (int)nudLeftColumn.Value;
                int rightAdjust = (int)nudRightColumn.Value;

                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string configFile = System.IO.Path.Combine(appPath, "Restaurantservice.exe.config");

                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                configFileMap.ExeConfigFilename = configFile;
                System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                config.AppSettings.Settings["LeftColumnPosition"].Value = leftAdjust.ToString();
                config.AppSettings.Settings["RigthColumnPosition"].Value = rightAdjust.ToString();

                LEFT_COLUMN_ADJUST = leftAdjust;
                RIGHT_COLUMN_ADJUST = rightAdjust;

                config.Save();

                MessageBox.Show("Ändringarna sparade.");
            }
            catch (Exception e)
            {
                MessageBox.Show("Något gick dessvärre fel" + e.ToString());
            }
        }

        private void CreateInvoiceCsvFile()
        {
            List<InvoiceDataRow> invoiceDataRows = DataAccess.GetInvoiceData();

            bool success = TextFileCreator.CreateInvoices(invoiceDataRows);


        }

        private void CreateTentativeOrders(DateTime deliveryDate, string pickupRest)
        {
            string dateAsString = deliveryDate.ToShortDateString();
            List<Order> allOrdersFromDB = DataAccess.GetTodaysOrders(dateAsString, pickupRest);
            if (allOrdersFromDB.Count == 0)
            {
                MessageBox.Show("Inga beställningar hittade");
            }
            else
            {
                List<string> addresses = (from o in allOrdersFromDB
                                          orderby o.Addr
                                          select o.Addr).Distinct().ToList();

                List<TentativeOrderList> listOfTentativeOrderList = new List<TentativeOrderList>();
                foreach (var addr in addresses)
                {
                    List<Order> ordersForAddr = (from o in allOrdersFromDB
                                                 where o.Addr == addr
                                                 select o).ToList();

                    List<string> typeGroups = (from hits in ordersForAddr
                                               select hits.TypeGroup).Distinct().ToList();

                    // Order by alphabetic (numeric) order
                    typeGroups = (from hits in typeGroups
                                  orderby hits ascending
                                  select hits).ToList();

                    // If the address has not been activated for väskor, we dont need to loop the typeGroups
                    if (typeGroups.Count() == 1 && typeGroups[0] == "")
                    {
                        var tentativeOrderList = CreateTentativeOrderList(addr, ordersForAddr);
                        listOfTentativeOrderList.Add(tentativeOrderList);
                    }
                    else
                    {
                        foreach (var typeGroup in typeGroups)
                        {
                            // Get the orders that matches the group
                            var typeGroupOrders = (from hits in ordersForAddr
                                                   where hits.TypeGroup == typeGroup
                                                   select hits).ToList();

                            string addrNameAndType = string.Format("{0} - Väska {1}", addr, typeGroup);

                            var tentativeOrderList = CreateTentativeOrderList(addrNameAndType, typeGroupOrders);
                            listOfTentativeOrderList.Add(tentativeOrderList);
                        }

                    }
                }

                // Add total orders to print after everything else
                var totalOrdersTentList = CreateTentativeOrderList("- - TOTALT - -", allOrdersFromDB);
                listOfTentativeOrderList.Add(totalOrdersTentList);

                PdfCreator.CreateTentativeOrders(listOfTentativeOrderList, deliveryDate, pickupRest);
                MessageBox.Show("Preliminära beställningar skapade!");
            }
        }
        private TentativeOrderList CreateTentativeOrderList(string address, List<Order> orderList)
        {
            List<TentativeOrder> tentativeOrders = GetTentativeOrders(orderList);

            int totalDishCount = GetTotalDishCount(tentativeOrders);
            TentativeOrderList tentList = new TentativeOrderList(address, tentativeOrders, totalDishCount);

            return tentList;
        }
        private List<Order> GetSpecialPackOrders(List<Order> orderList)
        {
            return (from hits in orderList
                    where hits.SpecialPackaging == true
                    select hits).ToList();
        }

        private int GetTotalDishCount(List<TentativeOrder> tentativeOrders)
        {
            int returnInt = 0;

            foreach (var tent in tentativeOrders)
            {
                if (tent.IsCountableDish)
                {
                    returnInt += tent.Quantity;
                }
            }

            return returnInt;
        }
        private List<TentativeOrder> GetTentativeOrders(List<Order> orders)
        {
            List<string> uniqueDishes = (from o in orders
                                         orderby o.Dish
                                         select o.Dish).Distinct().ToList();

            List<TentativeOrder> tentativeOrders = new List<TentativeOrder>();

            foreach (var dish in uniqueDishes)
            {
                var subOrders = (from o in orders
                                 where o.Dish == dish
                                 select o).ToList();

                int dishCount = GetDishCount(subOrders);
                string infoText = GetInfoText(subOrders);
                bool isCountableDish = GetIsCountableDish(subOrders);

                tentativeOrders.Add(new TentativeOrder(dish, dishCount, infoText, isCountableDish));
            }

            return tentativeOrders;
        }
        private int GetNoRice(List<Order> subOrders)
        {
            return (from hits in subOrders
                    where hits.NoRice == true && hits.NoGluten == false
                    select hits).Count();
        }

        private int GetNoGluten(List<Order> subOrders)
        {
            return (from hits in subOrders
                    where hits.NoGluten == true && hits.NoRice == false
                    select hits).Count();
        }
        private int GetNoRiceAndGluten(List<Order> subOrders)
        {
            return (from hits in subOrders
                    where hits.NoGluten == true && hits.NoRice == true
                    select hits).Count();
        }
        private int GetNoLactose(List<Order> subOrders)
        {
            return (from hits in subOrders
                    where hits.NoLactose == true
                    select hits).Count();
        }
        private int GetTimbal(List<Order> subOrders)
        {
            return (from hits in subOrders
                    where hits.Timbal == true
                    select hits).Count();
        }

        private string GetInfoText(List<Order> subOrders)
        {
            string returnText = "";

            int noRice = GetNoRice(subOrders);
            int noGluten = GetNoGluten(subOrders);
            int noLactose = GetNoLactose(subOrders);
            int noRiceOrGluten = GetNoRiceAndGluten(subOrders);
            int timbal = GetTimbal(subOrders);

            if (noRice > 0 || noGluten > 0 || noLactose > 0 || timbal > 0)
            {
                returnText += ", varav ";
            }

            if (noRice > 0)
            {
                returnText += string.Format("{0} ej ris. ", noRice);
            }
            if (noGluten > 0)
            {
                returnText += string.Format("{0} ej gluten. ", noGluten);
            }
            if (noLactose > 0)
            {
                returnText += string.Format("{0} ej laktos. ", noLactose);
            }
            if (noRiceOrGluten > 0)
            {
                returnText += string.Format("{0} ej ris ELLER gluten.", noRiceOrGluten);
            }
            if (timbal > 0)
            {
                returnText += string.Format("{0} timbal. ", timbal);
            }

            return returnText;
        }

        private int GetDishCount(List<Order> subOrders)
        {
            return subOrders.Count();
        }

        private bool GetIsCountableDish(List<Order> subOrders)
        {
            if (subOrders[0].ProductGroup == 1 || subOrders[0].ProductGroup == 2 || subOrders[0].ProductGroup == 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CreateLabels(string date, bool tomorrow, string pickupRest)
        {
            List<Order> orders = DataAccess.GetTodaysOrders(date, pickupRest);

            if (orders.Count == 0)
            {
                if (tomorrow == false)
                {
                    MessageBox.Show("Inga beställningar hittade");
                }
            }
            else
            {
                // Sort orders
                // Sort by delivery address, then by dish
                //orders = orders.OrderBy(o => o.Addr).ThenBy(o => o.Dish).ToList();
                orders = SortOrdersToMatchProductionNeeds(orders);

                // Add a second label to Norsk Fjordlax
                orders = AddExtraLabelWhereWanted(orders);

                PdfCreator.CreateLabels(orders, date, tomorrow);

                if (tomorrow == false)
                {
                    MessageBox.Show("Etiketter skapade!");
                }
            }
        }
        private List<Order> SortOrdersToMatchProductionNeeds(List<Order> orders)
        {
            #region Niklas wishes
            /*
             * Kolumnens värden är siffror och så här betyder de idag:
             1 = Dagens lunch
             2 = Al a Carte
             3 = Dessert
             4 = Dryck
             5 = Övrigt

            Först kommer alla "Varm dagens" Det innebär att först kommer alla varma dagens för Grupp A 
              sedan kommer alla avvikande till Grupp A såom kallt, gluten ris etc
            
            Därefter kommer alla drycker indelade gruppvis Grupp A mjölk, lättöl, måltidsdryck och 
              mineralvatten.Därefter kommer nästa Bellis osv.
            
            Specialpack hanteras som reglerna ovan men som en egen grupp. Så sorteringen blir "Grupp A", 
              "Grupp A specialpack", "Bellis", "Bellis specialpack" osv.
                        
            Alla specialare sorteras in sist inom varje grupp (det blir nog lättare vid sorteringen)
            
            Ovan innebär alltså också att "Sallad" kommer blandat, dvs uppdelat per grupp för 
              specialare, alternativt hade jag kunnat lägga ALLA sallad efter specialpack men innan specialarna. 
              I så fall kommer alla sallad för Grupp A, sedan alla sallad för Bellis och sedan alla sallad för Oxievång.Det innebär också att ALLA sallad kommer tillsammans, oavsett om de tillhör en dagens, en fläskstek, en specialpack eller någon som inte ska ha ris.
            Lägg alla sallader sist efter drickan.
            */

            var ordersInCount = orders.Count();
            #endregion

            // Drinks and sallad is sorted after all meals so lets put them aside in seperate lists.
            var allDrinks = (from hits in orders
                             where hits.ProductGroup == 4
                             select hits).ToList();
            orders = orders.Except(allDrinks).ToList();

            var allSallad = (from hits in orders
                             where hits.ProductGroup == 5
                             select hits).ToList();
            orders = orders.Except(allSallad).ToList();

            var groupOrdersListFood = SortLabelsByGroup(orders);
            var groupOrdersListDrink = SortLabelsByGroup(allDrinks);
            var groupOrdersListSallad = SortLabelsByGroup(allSallad);

            // Sort each individually
            foreach (var group in groupOrdersListFood)
            {
                group.Orders = SortGroupOrdersFoodIndividually(group.Orders);
            }
            foreach (var group in groupOrdersListDrink)
            {
                group.Orders = SortGroupOrdersDrinksIndividually(group.Orders);
            }
            foreach (var group in groupOrdersListSallad)
            {
                group.Orders = SortGroupOrdersSalladIndividually(group.Orders);
            }


            var returnList = new List<Order>();
            foreach (var group in groupOrdersListFood)
            {
                foreach (var order in group.Orders)
                {
                    returnList.Add(order);
                }
            }
            foreach (var group in groupOrdersListDrink)
            {
                foreach (var order in group.Orders)
                {
                    returnList.Add(order);
                }
            }
            foreach (var group in groupOrdersListSallad)
            {
                foreach (var order in group.Orders)
                {
                    returnList.Add(order);
                }
            }

            return returnList;
        }

        private static List<GroupOrders> SortLabelsByGroup(List<Order> orders)
        {
            var groupOrdersList = new List<GroupOrders>();

            foreach (var order in orders)
            {
                string typeName = " - Väska " + order.TypeGroup + " - " + order.CaseGroup;

                string groupName = order.Addr + typeName;

                // Does the groupName already exists?
                var group = (from hits in groupOrdersList
                             where hits.GroupName == groupName
                             select hits).FirstOrDefault();

                // Add group if not exists
                if (group == null)
                {
                    group = (new GroupOrders()
                    {
                        GroupName = groupName,
                        Orders = new List<Order>()
                    });

                    groupOrdersList.Add(group);
                }

                // Add order to group
                group.Orders.Add(order);
            }

            groupOrdersList = groupOrdersList.OrderBy(o => o.GroupName).ToList();

            return groupOrdersList;
        }
        private static List<Order> SortGroupOrdersFoodIndividually(List<Order> orders)
        {
            var returnList = new List<Order>();

            // Get all normal daily specials
            var normalDailySpecials = (from hits in orders
                                       where hits.ProductGroup == 1 && hits.NoGluten == false && hits.NoLactose == false && hits.NoRice == false && hits.DeliverCold == false && hits.Timbal == false
                                       select hits).ToList();

            orders = orders.Except(normalDailySpecials).ToList();

            // Get all with no rice
            var noRiceDailySpecials = (from hits in orders
                                       where hits.NoRice == true
                                       select hits).ToList();

            orders = orders.Except(noRiceDailySpecials).ToList();

            // Get (sort) the rest
            orders = (from hits in orders
                      orderby hits.Dish
                      select hits).ToList();

            returnList.AddRange(normalDailySpecials);
            returnList.AddRange(noRiceDailySpecials);
            returnList.AddRange(orders);

            return returnList;
        }

        private static List<Order> SortGroupOrdersSalladIndividually(List<Order> orders)
        {
            orders = (from hits in orders
                      orderby hits.Addr, hits.Dish
                      select hits).ToList();

            return orders;
        }
        private static List<Order> SortGroupOrdersDrinksIndividually(List<Order> orders)
        {
            orders = (from hits in orders
                      orderby hits.Addr, hits.Dish
                      select hits).ToList();

            return orders;
        }
        private static List<Order> AddExtraLabelWhereWanted(List<Order> orders)
        {
            for (int i = orders.Count() - 1; i >= 0; i--)
            {
                if (extraLabelList.Any(s => orders[i].Dish.Contains(s)))
                {
                    var orderCopy = new Order(orders[i]);

                    orders.Insert(i + 1, orderCopy);
                    orders[i].Name += " 1/2";
                    orders[i + 1].Name += " 2/2";
                }
            }

            return orders;

        }
        #endregion
        #region Easter egg
        string _eastereggstring = "";
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckEasterEggString(tabControl1.SelectedIndex.ToString());
        }
        private void CheckEasterEggString(string number)
        {
            _eastereggstring += number;

            // Only 8 characters
            if (_eastereggstring.Length > 4)
            {
                _eastereggstring = _eastereggstring.Substring(1, 4);
            }

            if (_eastereggstring == "1302")
            {
                System.Diagnostics.Process.Start("http://www.hamsterdance.org/hamsterdance/");
            }
        }

        #endregion
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        #region Garbage
        private void DoStuff()
        {
            var conn = new MySqlConnection();
            string conString = "server=mysql507.loopia.se; userid=wp_admin@b113010; password=mediakonsten99; database=bradgard_nu;";

            conn.ConnectionString = "server=mysql507.loopia.se; userid=wp_admin@b113010; password=mediakonstren99; database=bradgard_nu;";
            //bilarDset.Clear();

            //DataSet dset = new DataSet("Hej");


            conn.Open();
            //SqlDataAdapter myAdapter = new SqlDataAdapter("SELECT * FROM testtabell", conn);
            //myAdapter.Fill(dset, "Bilar");
            conn.Close();

            //dataGridView1.DataSource = null;
            //this.dataGridView1.DataSource = bilarDset.Tables["Bilar"];



            //cn.ConnectionString = "server=mysql507.loopia.se; userid=wp_admin@b113010; password=mediakonstren99; database=bradgard_nu;";
            //cn.Open();


            // using (var db = new MyContext)


            MessageBox.Show("Yes!!!");

        }







        #endregion

    }
}

