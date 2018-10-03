using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments

{
    public class GuestRoom : IHSegment
    {
        public int segment_num { get; set; }
        public int Capacity { get; set; }
        public int X_Dim { get; set; }
        public int Y_Dim { get; set; }
        public string Classification { get; set; }
        public Color MyColor { get; set; }

        public GuestRoom(int number, string classification)
        {
            Classification = classification;
            segment_num = number;
            MyColor = Color.Green;
        }

    }
}
