using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments
{
    class Staircase : IHSegment
    { 
        public bool First_floor { get; set; }

        /// <summary>
        /// Sets the properties of each fitness
        /// </summary>
        /// <param name="number">The ID of this fitness</param>
        /// <param name="xseg">The dimension of the X axes</param>
        /// <param name="yseg">The dimension of the Y axes</param>
        /// <param name="firstfloor">Is this the first floor or not</param>
        public Staircase(int number, int xseg, int yseg, bool firstfloor)
        {
            First_floor = firstfloor;
            MyImages = new List<Image>(); 
            XDim = xseg;
            YDim = yseg;
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
