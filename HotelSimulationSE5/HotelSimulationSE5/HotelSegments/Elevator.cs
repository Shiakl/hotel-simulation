using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments
{
    class Elevator : IHSegment
    {
        public int segment_num { get; set; }
        public int Capacity { get; set; }
        public int X_Dim { get; set; }
        public int Y_Dim { get; set; }
        public bool Reception { get; set; }
        public Color MyColor { get; set; }
        public List<Image> MyImages { get; set; }

        public Elevator(int number)
        {
            if(number == 1)
            {
                Reception = true;
            }
            else
            {
                Reception = false;
            }
            segment_num = number;
            MyColor = Color.Red;
            MyImages.Add(Image.FromFile(@"..\..\Images\empty.png"));
        }
    }
}
