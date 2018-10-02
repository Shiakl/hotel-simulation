using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5.Factories
{
    public interface IFactory
    {
        IBuildingBlock Create(string type, string classification = null);
    }
}
