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
        public int segment_num { get; set; }
        public int Capacity { get; set; }
        public int X_Dim { get; set; }
        public int Y_Dim { get; set; }
        public Color MyColor { get; set; }
        public List<Image> MyImages { get; set; }

        public Cinema(int number)
        {
            segment_num = number;
            MyColor = Color.Black;
            MyImages.Add(Image.FromFile(@"..\..\Images\Cinema1.png"));
            MyImages.Add(Image.FromFile(@"..\..\Images\Cinema2.png"));
            MyImages.Add(Image.FromFile(@"..\..\Images\Cinema3.png"));
            MyImages.Add(Image.FromFile(@"..\..\Images\Cinema4.png"));
        }
    }
}
