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
        public string InfoText { get; set; }
        public bool IsCountableDish { get; set; }

        public TentativeOrder(string dishName, int quantity, string infoText, bool isCountableDish)
        {
            DishName = dishName;
            Quantity = quantity;
            InfoText = infoText;
            IsCountableDish = isCountableDish;
        }
    }
}
