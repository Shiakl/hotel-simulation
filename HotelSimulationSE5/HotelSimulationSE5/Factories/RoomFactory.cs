using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5.Factories

{

    class RoomFactory :IFactory
    {
        public IBuildingBlock Create(string areatype)
        {
            switch (areatype)
            {
                case "Cinema":
                    return new Rooms.Cinema();
                case "Restaurant":
                    return new Rooms.Restaurant();
                case "Fitness":
                    return new Rooms.Fitness();
                case "Room":
                    return new Rooms.GuestRoom();
                default:
                    return null;
            }
        }
    }
}
