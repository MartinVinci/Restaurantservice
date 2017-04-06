using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantservice
{
    public class Coordinate
    {
        public int NameXcoord { get; set; }
        public int NameYcoord { get; set; }
        public int DishXcoord { get; set; }
        public int DishYcoord { get; set; }
        public int DateXcoord { get; set; }
        public int DateYcoord { get; set; }
        public int AddrXcoord { get; set; }
        public int AddrYcoord { get; set; }
        public int LogoXcoord { get; set; }
        public int LogoYcoord { get; set; }
        public int ColdXcoord { get; set; }
        public int ColdYcoord { get; set; }

        public Coordinate(int nameX, int nameY, int dishX, int dishY, int dateX, int dateY, int addrX, int addrY, int logoX, int logoY, int coldX, int coldY)
        {
            NameXcoord = nameX;
            NameYcoord = nameY; 
            DishXcoord = dishX;
            DishYcoord = dishY;
            DateXcoord = dateX;
            DateYcoord = dateY;
            AddrXcoord = addrX;
            AddrYcoord = addrY;
            LogoXcoord = logoX;
            LogoYcoord = logoY;
            ColdXcoord = coldX;
            ColdYcoord = coldY;
        }
    }
}
