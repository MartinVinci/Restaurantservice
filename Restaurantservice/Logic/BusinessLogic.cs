using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Restaurantservice.Logic
{
    public class BusinessLogic
    {
        private static List<string> extraLabelList = new List<string>()
        {
            "7. Hemgravad norsk fjordlax",
            "Bakad potatis",
        };

        public static void CreateTentativeOrders(DateTime deliveryDate, string pickupRest)
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

                // Add total orders for deliver cold after that
                listOfTentativeOrderList.Add(GetTotalDeliverCold(allOrdersFromDB));

                PdfCreator.CreateTentativeOrders(listOfTentativeOrderList, deliveryDate, pickupRest);
                MessageBox.Show("Preliminära beställningar skapade!");
            }
        }

        public static void CreateLabels(string date, bool tomorrow, string pickupRest)
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

        private static TentativeOrderList GetTotalDeliverCold(List<Order> allOrdersFromDB)
        {
            // Delivery groups
            // 1 is daily specials
            // 2 is other hot dishes
            // 6 is timbal
            var coldCategories = new List<int>()
            {
                1, 2, 6
            };

            var delColdDishes = (from hits in allOrdersFromDB
                                 where hits.DeliverCold == true && coldCategories.Contains(hits.ProductGroup)
                                 select hits).ToList();

            return CreateTentativeOrderList("- - TOTALT LEV KALL - -", delColdDishes);
        }

        private static TentativeOrderList CreateTentativeOrderList(string address, List<Order> orderList)
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

        private static int GetTotalDishCount(List<TentativeOrder> tentativeOrders)
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
        private static List<TentativeOrder> GetTentativeOrders(List<Order> orders)
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
        private static int GetNoRice(List<Order> subOrders)
        {
            return (from hits in subOrders
                    where hits.NoRice == true && hits.NoGluten == false
                    select hits).Count();
        }
        private static int GetNoGluten(List<Order> subOrders)
        {
            return (from hits in subOrders
                    where hits.NoGluten == true && hits.NoRice == false
                    select hits).Count();
        }
        private static int GetNoRiceAndGluten(List<Order> subOrders)
        {
            return (from hits in subOrders
                    where hits.NoGluten == true && hits.NoRice == true
                    select hits).Count();
        }
        private static int GetNoLactose(List<Order> subOrders)
        {
            return (from hits in subOrders
                    where hits.NoLactose == true
                    select hits).Count();
        }
        private static int GetTimbal(List<Order> subOrders)
        {
            return (from hits in subOrders
                    where hits.Timbal == true
                    select hits).Count();
        }
        private static string GetInfoText(List<Order> subOrders)
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
        private static int GetDishCount(List<Order> subOrders)
        {
            return subOrders.Count();
        }
        private static bool GetIsCountableDish(List<Order> subOrders)
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
        private static List<Order> SortOrdersToMatchProductionNeeds(List<Order> orders)
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

            // Dishes with "lev kall" should be last of all, even after drinks and sallad
            var allDeliverCold = (from hits in orders
                                  where hits.DeliverCold
                                  select hits).ToList();
            orders = orders.Except(allDeliverCold).ToList();

            var groupOrdersListFood = SortLabelsByGroup(orders);
            var groupOrdersListDrink = SortLabelsByGroup(allDrinks);
            var groupOrdersListSallad = SortLabelsByGroup(allSallad);
            var groupOrdersListDelCold = SortLabelsByGroup(allDeliverCold);

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
            foreach (var group in groupOrdersListDelCold)
            {
                group.Orders = SortGroupOrdersDeliverColdIndividually(group.Orders);
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
            foreach (var group in groupOrdersListDelCold)
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
        private static List<Order> SortGroupOrdersDeliverColdIndividually(List<Order> orders)
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
        private void CreateInvoiceCsvFile()
        {
            List<InvoiceDataRow> invoiceDataRows = DataAccess.GetInvoiceData();

            bool success = TextFileCreator.CreateInvoices(invoiceDataRows);
        }
    }
}