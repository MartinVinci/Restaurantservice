using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantservice
{
    public class GroupOrders
    {
        public string GroupName { get; set; }
        public List<Order> Orders { get; set; }
    }
}
