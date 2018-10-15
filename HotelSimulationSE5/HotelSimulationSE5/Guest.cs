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
        public int guest_x_size = 15;
        public int guest_y_size = 15;
        public HotelSegments.GuestRoom MyRoom { get; set; }
        
        public Image MyImage { get; set; }
        public int speed = -2;

        public PictureBox panelPb;

        public Guest(PictureBox mypanel)
        {
            MyImage = Image.FromFile(@"..\..\Images\TempGuest2.png");
            panelPb = new PictureBox();
            panelPb.Size = MyImage.Size;
            panelPb.BackgroundImageLayout = ImageLayout.None;
            panelPb.Parent = mypanel;
            panelPb.BackColor = Color.Transparent;
            mypanel.Controls.Add(panelPb);
        }

        public void Add_panel(Panel mypanel)
        {
        }

        public void Move()
        {
            panelPb.BackgroundImage = MyImage;
            panelPb.BringToFront();
        }

        public void Move_to_Node(PictureBox next,PictureBox current)
        {
            next.Controls.Add(panelPb);
            current.Controls.Remove(panelPb);
            panelPb.BackgroundImage = MyImage;
            panelPb.BringToFront();
        }

    }
}
