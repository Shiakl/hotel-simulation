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
        public int segment_num { get; set; }
        public int Capacity { get; set; }
        public int X_Dim { get; set; }
        public int Y_Dim { get; set; }
        public Color MyColor { get; set; }
        public List<Image> MyImages { get; set; }
        public bool first_floor { get; set; }

        public Staircase(int number, int xseg, int yseg, bool firstfloor)
        {
            first_floor = first_floor;
            MyImages = new List<Image>();
            X_Dim = xseg;
            Y_Dim = yseg;
            segment_num = number;
            MyColor = Color.Gray;

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
