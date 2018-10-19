using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments
{
    public class Cinema : IHSegment
    {
        /// <summary>
        /// Sets the properties of each cinema that is created
        /// </summary>
        /// <param name="number">The ID of the cinema</param>
        /// <param name="xseg">The dimension of the X axes</param>
        /// <param name="yseg">The dimension of the Y axes</param>
        public Cinema(int number, int xseg, int yseg)
        {
            MyImages = new List<Image>(); //Cinemas have the size of 4 nodes. This List will contain 4 images that will be used on the 4 different nodes
            XDim = xseg;
            YDim = yseg;
            ID = number;
            MyImages.Add(Image.FromFile(@"..\..\Images\Cinema1.png"));
            MyImages.Add(Image.FromFile(@"..\..\Images\Cinema2.png"));
            MyImages.Add(Image.FromFile(@"..\..\Images\Cinema3.png"));
            MyImages.Add(Image.FromFile(@"..\..\Images\Cinema4.png"));
        }
    }
}
