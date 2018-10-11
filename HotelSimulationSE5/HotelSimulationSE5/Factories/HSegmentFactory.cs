using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5.Factories

{

    class HSegmentFactory
    {
        public HotelSegments.IHSegment Create(string areatype, int segment_num, int seg_x = 1, int seg_y = 1,string classification = null)
        {
            switch (areatype)
            {
                case "Cinema":
                    return new HotelSegments.Cinema(segment_num,seg_x,seg_y);
                case "Restaurant":
                    return new HotelSegments.Restaurant(segment_num, seg_x, seg_y);
                case "Fitness":
                    return new HotelSegments.Fitness(segment_num, seg_x, seg_y);
                case "Elevator":
                    return new HotelSegments.Elevator(segment_num, seg_x, seg_y);
                case "Room":                   
                    return new HotelSegments.GuestRoom(segment_num, seg_x, seg_y, classification);
                default:
                    return null;
            }
        }
    }
}
