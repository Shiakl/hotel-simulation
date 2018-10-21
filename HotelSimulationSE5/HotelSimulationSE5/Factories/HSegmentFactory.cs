﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5.Factories

{
    class HSegmentFactory
    {
        public HotelSegments.HSegment Create(string areatype, int segment_ID, int seg_x = 1, int seg_y = 1,int classification = 0, bool firstfloor = false)
        {
            switch (areatype)
            {
                case "Cinema":
                    return new HotelSegments.Cinema(segment_ID,seg_x,seg_y);
                case "Restaurant":
                    return new HotelSegments.Restaurant(segment_ID, seg_x, seg_y);
                case "Fitness":
                    return new HotelSegments.Fitness(segment_ID, seg_x, seg_y);
                case "Elevator":
                    return new HotelSegments.Elevator(segment_ID, seg_x, seg_y);
                case "Reception":
                    return new HotelSegments.Reception(segment_ID, seg_x, seg_y);
                case "Staircase":
                    return new HotelSegments.Staircase(segment_ID, seg_x, seg_y, firstfloor);
                case "Room":                   
                    return new HotelSegments.GuestRoom(segment_ID, seg_x, seg_y, classification);
                default:
                    return null;
            }
        }
    }
}
