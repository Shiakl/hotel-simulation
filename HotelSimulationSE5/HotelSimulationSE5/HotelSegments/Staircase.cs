using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments
{
    class Staircase : HSegment
    {
        public bool First_floor { get; set; }

        public Staircase(int number, int xseg, int yseg, bool firstfloor)
        {
            First_floor = First_floor;
            MyImages = new List<Image>();
            X_Dim = xseg;
            Y_Dim = yseg;
            ID = number;

            if (firstfloor == true)
            {
                MyImages.Add(Image.FromFile(@"..\..\Images\stairwell2.png"));
            }
            else
            {
                MyImages.Add(Image.FromFile(@"..\..\Images\stairwell1.png"));
            }

        }
    }
}
