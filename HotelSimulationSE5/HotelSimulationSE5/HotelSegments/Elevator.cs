using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments
{
    class Elevator : HSegment
    {
        public bool Reception { get; set; }
        public bool Elevator_here { get; set; }

        public Elevator(int number, int xseg, int yseg, bool reception)
        {
            MyImages = new List<Image>();
            X_Dim = xseg;
            Y_Dim = yseg;
            Reception = reception;
            ID = number;

            
            if (Reception == true)
            {
                MyImages.Add(Image.FromFile(@"..\..\Images\reception.png"));
                Elevator_here = true;
            }
            else
            {
            MyImages.Add(Image.FromFile(@"..\..\Images\elevator.png"));
                Elevator_here = false;
            }
        }

        public bool CheckElevatorFull(Entity[] guests)
        {
            int ammountinElevator = 0;
            foreach (Entity guest in guests)
            {
                if (guest.MyNode.MySegment is Elevator)
                {
                    ammountinElevator++;
                }
            }

            if (ammountinElevator >= Capacity)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
