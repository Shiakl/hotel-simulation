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
        public PictureBox panelPb;


        public HotelSegments.IHSegment MySegment { get; set; }
        public List<Guest> MyUnits { get; set; }

        public Node(Panel box)
        {
            MyUnits = new List<Guest>();
            MyPanel = box;
            panelPb = new PictureBox();
            panelPb.Size = MyPanel.Size;
            MyPanel.Controls.Add(panelPb);
            panelPb.BackgroundImage = Image.FromFile(@"..\..\Images\empty.png");

        }

        public void ColorMe()
        {
            if (MySegment!=null)
            {            
                if (MySegment.X_Dim>1)
                {
                    panelPb.BackgroundImage = MySegment.MyImages[(int)SEGMENT_PART.Main];
                    RightNode.panelPb.BackgroundImage = MySegment.MyImages[(int)SEGMENT_PART.RightSide];
                }
                if (MySegment.X_Dim>1 && MySegment.Y_Dim>1)
                {
                    panelPb.BackgroundImage = MySegment.MyImages[(int)SEGMENT_PART.Main];
                    RightNode.panelPb.BackgroundImage = MySegment.MyImages[(int)SEGMENT_PART.RightSide];
                    TopNode.panelPb.BackgroundImage = MySegment.MyImages[(int)SEGMENT_PART.Top];
                    TopNode.RightNode.panelPb.BackgroundImage = MySegment.MyImages[(int)SEGMENT_PART.TopRight];
                }
                else
                {
                    panelPb.BackgroundImage = MySegment.MyImages[(int)SEGMENT_PART.Main];
                }               
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
