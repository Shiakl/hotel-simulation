using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5.HotelSegments
{
    class Elevator : IHSegment
    {
        public int segment_num { get; set; }
        public int Capacity { get; set; }
        public int X_Dim { get; set; }
        public int Y_Dim { get; set; }
        public bool Reception { get; set; }

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
        }
    }
}
