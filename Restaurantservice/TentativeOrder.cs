using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantservice
{
    public class TentativeOrder
    {
        public string DishName { get; set; }
        public int Quantity { get; set; }

        public TentativeOrder(string dishName, int quantity)
        {
            DishName = dishName;
            Quantity = quantity;
        }
    }
}
