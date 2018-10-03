using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5.Factories
{
    public interface IFactory
    {
        HotelSegments.IHSegment Create(string type, int segment_num , string classification = null);
    }
}
