using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments
{
    public class Cinema : HSegment
    {
        public Cinema(int number, int xseg, int yseg)
        {
            MyImages = new List<Image>();
            X_Dim = xseg;
            Y_Dim = yseg;
            ID = number;
            MyImages.Add(Image.FromFile(@"..\..\Images\Cinema1.png"));
            MyImages.Add(Image.FromFile(@"..\..\Images\Cinema2.png"));
            MyImages.Add(Image.FromFile(@"..\..\Images\Cinema3.png"));
            MyImages.Add(Image.FromFile(@"..\..\Images\Cinema4.png"));
        }
    }
}
