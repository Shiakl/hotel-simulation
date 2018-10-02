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
                case "Horizontal":
                    return new Rooms.Cinema();
                case "Vertical":
                    return new Rooms.Restaurant();
                default:
                    return null;
            }
        }
    }
}
