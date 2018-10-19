using System.Collections.Generic;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments
{
    public class IHSegment
    {
        public int Capacity { get; set; }
        public int XDim { get; set; }
        public int YDim { get; set; }
        public int ID { get; set; }
        public List<Image> MyImages{ get; set; }
    }
}
