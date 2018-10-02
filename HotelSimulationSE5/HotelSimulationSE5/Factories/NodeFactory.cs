using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5.Factories
{
    class NodeFactory : IFactory
    {
        public IBuildingBlock Create(string areatype, string classification = null)
        {
            switch (areatype)
            {
                case "Hallway":
                    return new Rooms.Cinema();
                case "Elevator":
                    return new Rooms.Restaurant();
                default:
                    return null;
            }
        }
    }
}
