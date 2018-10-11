﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments
{
    public interface IHSegment
    {
        int segment_num { get; set; }
        int Capacity { get; set; }
        int X_Dim { get; set; }
        int Y_Dim { get; set; }
        Color MyColor { get; set; }
        List<Image> MyImages{ get; set; }

    }
}
