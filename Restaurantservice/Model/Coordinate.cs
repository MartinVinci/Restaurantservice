﻿using System;
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
        public int SpecXcoord { get; set; }
        public int SpecYcoord { get; set; }
        public int RiceXcoord { get; set; }
        public int RiceYcoord { get; set; }
        public int GlutXcoord { get; set; }
        public int GlutYcoord { get; set; }
        public int LactXcoord { get; set; }
        public int LactYcoord { get; set; }
        public int TimbXcoord { get; set; }
        public int TimbYcoord { get; set; }

        public Coordinate(int nameX, int nameY, int dishX, int dishY, int dateX, int dateY, int addrX, int addrY, int logoX, int logoY, 
            int coldX, int coldY, int specX, int specY, int riceX, int riceY, int glutX, int glutY, int lactX, int lactY, int timbX, int timbY)
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
            SpecXcoord = specX;
            SpecYcoord = specY;
            RiceXcoord = riceX;
            RiceYcoord = riceY;
            GlutXcoord = glutX;
            GlutYcoord = glutY;
            LactXcoord = lactX;
            LactYcoord = lactY;
            TimbXcoord = timbX;
            TimbYcoord = timbY;
        }
    }
}
