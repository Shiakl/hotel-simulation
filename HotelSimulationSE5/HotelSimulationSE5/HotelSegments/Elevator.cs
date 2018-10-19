using System.Collections.Generic;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments
{
    class Elevator : IHSegment
    {
        public bool Reception { get; set; }
        public bool Elevator_here { get; set; }

        /// <summary>
        /// Sets the properties of each elevator
        /// </summary>
        /// <param name="number">The ID of the elevator</param>
        /// <param name="xseg">The dimension of the X axes</param>
        /// <param name="yseg">The dimension of the Y axes</param>
        /// <param name="reception">True if this elevator is also a reception, false if this elevator is not a reception</param>
        public Elevator(int number, int xseg, int yseg, bool reception)
        {
            MyImages = new List<Image>();
            XDim = xseg;
            YDim = yseg;
            Reception = reception;
            ID = number;

            if (Reception == true) //If this elevator is indeed a reception then the picture must be different
            {
                MyImages.Add(Image.FromFile(@"..\..\Images\reception.png"));
                Elevator_here = true;
            }
            else //If this elevator is not a reception the picture will be a normal elevator shaft
            {
            MyImages.Add(Image.FromFile(@"..\..\Images\elevator.png"));
                Elevator_here = false;
            }
        }    
    }
}
