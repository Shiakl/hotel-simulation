using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace HotelSimulationSE5
{
    public class Guest
    {
        public HotelSegments.GuestRoom MyRoom { get; set; }
        
        public Image MyImage { get; set; }
        private const int speed = -2;

        public Guest()
        {
            MyImage = Image.FromFile(@"..\..\Images\TempGuest.png");
        }

        public void Move()
        {

        }
    }
}
