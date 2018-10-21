using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments
{
    public class Elevator : HSegment
    {
        public bool Elevator_here { get; set; }  //Property to check if the elevator is on this Elevator Segment.
        public Elevator(int number, int xseg, int yseg)
        {
            MyImages = new List<Image>();
            X_Dim = xseg;
            Y_Dim = yseg;
            ID = number;          
            MyImages.Add(Image.FromFile(@"..\..\Images\elevator.png"));
            Elevator_here = false;
            
        }
    }
}
