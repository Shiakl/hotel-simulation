using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5.Factories

{

    class HSegmentFactory :IFactory
    {
        public HotelSegments.IHSegment Create(string areatype, int segment_num,string classification = null)
        {
            switch (areatype)
            {
                case "Cinema":
                    return new HotelSegments.Cinema(segment_num);
                case "Restaurant":
                    return new HotelSegments.Restaurant(segment_num);
                case "Fitness":
                    return new HotelSegments.Fitness(segment_num);
                case "Elevator":
                    return new HotelSegments.Elevator(segment_num);
                case "Room":                   
                    return new HotelSegments.GuestRoom(segment_num,classification);
                default:
                    return null;
            }
        }
    }
}
