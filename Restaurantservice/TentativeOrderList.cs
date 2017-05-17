using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantservice
{
    public class TentativeOrderList
    {
        public string KgPortalUser { get; set; }
        public List<TentativeOrder> OrderList { get; set; }

        public TentativeOrderList(string kgPortalUser, List<TentativeOrder> orderList)
        {
            KgPortalUser = kgPortalUser;
            OrderList = orderList;
        }
    }
}
