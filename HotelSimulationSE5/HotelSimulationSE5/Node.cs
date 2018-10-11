using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace HotelSimulationSE5
{
    class Node
    {
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
        public Node TopNode { get; set; }
        public Node BottomNode { get; set; }
        public Node[] MyConnections { get; set; }

        public Panel MyPanel { get; set; }
        private PictureBox panelPb;


        public HotelSegments.IHSegment MySegment { get; set; }
        public List<Guest> MyUnits { get; set; }

        public Node(Panel box)
        {
            MyUnits = new List<Guest>();
            MyPanel = box;
            panelPb = new PictureBox();
            panelPb.Size = MyPanel.Size;
            panelPb.BackgroundImageLayout = ImageLayout.Tile;
            MyPanel.Controls.Add(panelPb);
        }

        public void ColorMe()
        {
            if (MySegment!=null)
            {

                panelPb.BackgroundImage = MySegment.MyImages[0];
                
            }
            else
            {

                panelPb.BackgroundImage = Image.FromFile(@"..\..\Images\empty.png");

            }

        }

        public enum SEGMENT_PART
        {
            Main,
            RightSide,
            Top,
            TopRight
        }

        public enum DIRECTIONS
        {
            LEFT,
            RIGHT,
            TOP,
            BOTTOM
        }


        public void Add_myConnections()
        {
            MyConnections = new Node[4];
            MyConnections[(int)DIRECTIONS.LEFT] = LeftNode;
            MyConnections[(int)DIRECTIONS.RIGHT] = RightNode;
            MyConnections[(int)DIRECTIONS.TOP] = TopNode;
            MyConnections[(int)DIRECTIONS.BOTTOM] = BottomNode;
        }
    }
}
