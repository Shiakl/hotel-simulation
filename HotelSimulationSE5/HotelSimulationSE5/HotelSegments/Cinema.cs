﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5.HotelSegments
{
    public class Cinema : IHSegment
    {
        public int Room_Number { get; set; }
        public int Capacity { get; set; }
        public int X_Dim { get; set; }
        public int Y_Dim { get; set; }

        public Cinema()
        {

        }
    }
}