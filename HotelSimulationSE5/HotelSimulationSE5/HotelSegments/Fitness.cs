using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulationSE5.HotelSegments
{
    class Fitness : IHSegment
    {
        public int ID { get; set; }
        public int Capacity { get; set; }
        public int X_Dim { get; set; }
        public int Y_Dim { get; set; }
        public List<Image> MyImages { get; set; }

        public Fitness(int number, int xseg, int yseg)
        {
            MyImages = new List<Image>();
            X_Dim = xseg;
            Y_Dim = yseg;
            ID = number;

            MyImages.Add(Image.FromFile(@"..\..\Images\Fitness1.png"));
            MyImages.Add(Image.FromFile(@"..\..\Images\Fitness2.png"));
        }
    }
}
