using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments
{
    public abstract class HSegment
    {
        public int Capacity { get; set; }   //Amount of guests allowed in the segment
        public int X_Dim { get; set; }  //Width of the segment in nodes
        public int Y_Dim { get; set; }  //Heigth of the segment in nodes
        public int ID { get; set; }     //Unique segment ID
        public List<Image> MyImages{ get; set; }    //Image of the segment
    }
}
