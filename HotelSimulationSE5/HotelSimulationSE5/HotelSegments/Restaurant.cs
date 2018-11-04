using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments
{
    class Restaurant : HSegment
    {
        public Restaurant(int number, int xseg, int yseg)
        {
            MyImages = new List<Image>();
            X_Dim = xseg;
            Y_Dim = yseg;
            ID = number;
            MyImages.Add(Image.FromFile(@"..\..\Images\Restaurant1.png"));
            MyImages.Add(Image.FromFile(@"..\..\Images\Restaurant2.png"));
        }
    }
}
