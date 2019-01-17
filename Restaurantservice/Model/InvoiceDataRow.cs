using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantservice
{
    public class InvoiceDataRow
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Dish { get; set; }
        public int Price { get; set; }

        public InvoiceDataRow(string id, string name, int amount, string dish, int price)
        {
            Id = id;
            Name = name;
            Amount = amount;
            Dish = dish;
            Price = price;
        }
    }
}
