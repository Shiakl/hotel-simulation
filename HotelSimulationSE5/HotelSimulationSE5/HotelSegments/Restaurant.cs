using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments
{
    class Restaurant : IHSegment
    {
        /// <summary>
        /// Sets the properties of each Restaurant
        /// </summary>
        /// <param name="number">The ID of this fitness</param>
        /// <param name="xseg">The dimension of the X axes</param>
        /// <param name="yseg">The dimension of the Y axes</param>
        public Restaurant(int number, int xseg, int yseg)
        {
            MyImages = new List<Image>(); //Every restuarant is atleast 2 nodes wide. This List will save 2 different pictures, 1 for each node
            XDim = xseg;
            YDim = yseg;
            ID = number;
            MyImages.Add(Image.FromFile(@"..\..\Images\Restaurant1.png"));
            MyImages.Add(Image.FromFile(@"..\..\Images\Restaurant2.png"));
        }
    }
}
