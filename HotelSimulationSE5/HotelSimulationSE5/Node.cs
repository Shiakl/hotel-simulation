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
        public Node leftNode { get; set; }
        public Node rightNode { get; set; }
        public Node topNode { get; set; }
        public Node bottomNode { get; set; }
        public Node[] myConnections { get; set; }

        public Panel MyPanel { get; set; }


        public HotelSegments.IHSegment mySegment { get; set; }
        public List<Guest> MyUnits { get; set; }

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

        public Node(Panel box)
        {
            MyPanel = box;
        }

        public void Add_myConnections()
        {
            myConnections[(int)DIRECTIONS.LEFT] = leftNode;
            myConnections[(int)DIRECTIONS.RIGHT] = rightNode;
            myConnections[(int)DIRECTIONS.TOP] = topNode;
            myConnections[(int)DIRECTIONS.BOTTOM] = bottomNode;
        }
    }
}
