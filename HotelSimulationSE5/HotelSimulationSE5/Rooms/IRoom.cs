using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5.Rooms
{
    public interface  IRoom: IBuildingBlock
    {
        int Room_Number { get; set; }
        int Capacity{get; set;}
        int X_Dim { get; set; }
        int Y_Dim { get; set; }
    }
}
