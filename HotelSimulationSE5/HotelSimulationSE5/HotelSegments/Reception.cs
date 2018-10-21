using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5.HotelSegments
{
    class Reception:Elevator
    {
        public Queue<Entity> WaitList { get; set; }

        public Reception(int number, int xseg, int yseg) : base(number, xseg, yseg)
        {
            MyImages = new List<Image>();
            X_Dim = xseg;
            Y_Dim = yseg;
            ID = number;
            MyImages.Add(Image.FromFile(@"..\..\Images\reception.png"));
            Elevator_here = true;
        }

    }
}
