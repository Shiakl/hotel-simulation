using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5.Rooms

{
    public class GuestRoom : IRoom
    {
        public int Room_Number { get; set; }
        public int Capacity { get; set; }
        public int X_Dim { get; set; }
        public int Y_Dim { get; set; }
        public string Classification { get; set; }

        public GuestRoom(string classification)
        {
            Classification = classification;
        }

    }
}
