using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5.HotelSegments
{
    public class Reception:Elevator
    {
        public Queue<Entity> WaitList { get; set; }
        private int Waittime { get; set; }
        private const int waitduration = 2;

        public Reception(int number, int xseg, int yseg) : base(number, xseg, yseg)
        {
            MyImages = new List<Image>();
            X_Dim = xseg;
            Y_Dim = yseg;
            ID = number;
            WaitList = new Queue<Entity>();
            MyImages.Add(Image.FromFile(@"..\..\Images\reception.png"));
            Elevator_here = true;
        }

        public void Assigned_Room(List<Entity> guestList)
        {
            if (WaitList.Any())
            {
                if (Waittime == waitduration)
                {
                    Waittime = 0;
                    guestList.Add(WaitList.First());
                    WaitList.Dequeue();
                }
                else
                {
                    Waittime++;
                }
            }


        }

    }
}
