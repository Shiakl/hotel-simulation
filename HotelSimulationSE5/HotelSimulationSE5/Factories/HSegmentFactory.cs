using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5.Factories

{

    class HSegmentFactory :IFactory
    {
        public IBuildingBlock Create(string areatype, string classification = null)
        {
            switch (areatype)
            {
                case "Cinema":
                    return new HotelSegments.Cinema();
                case "Restaurant":
                    return new HotelSegments.Restaurant();
                case "Fitness":
                    return new HotelSegments.Fitness();
                case "Room":                   
                    return new HotelSegments.GuestRoom(classification);
                default:
                    return null;
            }
        }
    }
}
