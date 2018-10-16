using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace HotelSimulationSE5
{
    class Guest
    {
        public HotelSegments.GuestRoom MyRoom { get; set; }        
        public Image MyImage { get; set; }
        public int speed = -2;
        public Panel MyPanel { get; set; }
        public List<Node.DIRECTIONS> Path { get; set; }
        public Node MyNode { get; set; }

        private PictureBox panelPb;

        public Guest(Panel mypanel)
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

        public void Move_to_Node(Node next,Node current)
        {
            next.panelPb.Controls.Add(panelPb);
            current.panelPb.Controls.Remove(panelPb);
            MyNode = next;
        }

    }
}
