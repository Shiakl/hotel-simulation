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
        public int segment_num { get; set; }
        public int Capacity { get; set; }
        public int X_Dim { get; set; }
        public int Y_Dim { get; set; }
        public Color MyColor { get; set; }
        public Image MyImage { get; set; }

        public Restaurant(int number)
        {
            segment_num = number;
            MyColor = Color.Yellow;
            MyImage = Image.FromFile(@"..\..\Images\Rest.png");
        }
    }
}
