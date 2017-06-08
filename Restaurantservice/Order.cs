using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantservice
{
    public class Order
    {
        public string Name { get; set; }
        public string Dish { get; set; }
        public string Date { get; set; }
        public string Addr { get; set; }
        public string Logo { get; set; }
        public bool DeliverCold { get; set; }
        public string PickupRestaurant { get; set; }
        public bool SpecialPackaging { get; set; }

        public Order(string name, string dish, string date, string addr, bool deliverCold, string pickupRest, bool specialPackaging)
        {
            Name = name;
            Dish = dish;
            Date = date;
            Addr = addr;
            Logo = @"C:\Bestallning\Logga\kniv-och-gaffel-logotyp-1.png";
            DeliverCold = deliverCold;
            PickupRestaurant = pickupRest;
            SpecialPackaging = specialPackaging;
        }
    }
}
