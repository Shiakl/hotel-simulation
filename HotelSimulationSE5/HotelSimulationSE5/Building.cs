using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace HotelSimulationSE5
{
    class Building
    {
        private string layoutstring;
        public const int segmentSize_X = 80;
        public const int segmentSize_Y = 50;
        private List<Node> nodes;
        private List<Node> elevatorNodes;
        private List<TempRoom> temp;



        public Building()
        {
            temp = new List<TempRoom>();
            layoutstring = System.IO.File.ReadAllText(@"..\..\External\Hotel2_reparatieVanVersie1.layout");
            Read_Layout();

            Console.WriteLine("Checkpoint: 1");
        }

        public void Read_Layout()
        {
            temp = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TempRoom>>(layoutstring);
        }

        private int Find_Max_X(List<TempRoom> roomList)
        {
            int hotel_X_Size= 0 ;

            foreach(TempRoom segment in roomList)
            {
                if (segment.Position_X > hotel_X_Size)
                {
                    if (segment.Dimension_X>1)
                    {
                    hotel_X_Size = segment.Position_X + (segment.Dimension_X-1);
                    }
                    else
                    {
                        hotel_X_Size = segment.Position_X;
                    }
                }
            }

            return hotel_X_Size; 
        }

        private int Find_Max_Y(List<TempRoom> roomList)
        {
            int hotel_Y_Size = 0;

            foreach (TempRoom segment in roomList)
            {
                if (segment.Position_Y > hotel_Y_Size)
                {
                    if (segment.Dimension_X > 1)
                    {
                        hotel_Y_Size = segment.Position_Y + (segment.Dimension_Y - 1);
                    }
                    else
                    {
                        hotel_Y_Size = segment.Position_Y;
                    }
                }
            }

            return hotel_Y_Size;
        }

        public void CreateHotel(Form mainform)
        {
            int max_x = Find_Max_X(temp);
            int max_y = Find_Max_Y(temp);
            int nodecounter = 0;

            //Create elevator nodes
            for(int y = 0; y < max_y; y++)
            {

                Panel tempPanel = new Panel
                {
                    Size = new Size(segmentSize_X, segmentSize_Y),
                    Location = new Point(0, y * segmentSize_Y)
                };

                elevatorNodes[y] = new Node(tempPanel);
            }

            //Create all the Nodes
            for (int y = 0; y < max_y; y++)
            {
                for (int x = 0; x <max_x; x++)
                {
                    Panel tempPanel = new Panel
                    {
                        Size = new Size(segmentSize_X, segmentSize_Y),
                        Location = new Point(x * segmentSize_X+segmentSize_X, y * segmentSize_Y)
                    };

                    nodes[nodecounter] = new Node(tempPanel);
                    mainform.Controls.Add(nodes[nodecounter].MyPanel);
                    nodecounter++;
                }
            }


            //connect nodes
            for (int tc = 0; tc < (max_x*max_y); tc++)
            {
                //Add neighbours to Array in Tile Class
                if (tc > max_x - 1)
                {
                    nodes[tc].topNode = nodes[tc - max_x];
                }
                if (tc < (max_x * max_y) - max_x)
                {
                    nodes[tc].bottomNode = nodes[tc + max_x];
                }
                if (tc % max_x < max_x - 1)
                {
                    nodes[tc].rightNode = nodes[tc + 1];
                }
                if (tc % max_x > 0)
                {
                    nodes[tc].leftNode = nodes[tc - 1];
                }
                nodes[tc].Add_myConnections();
            }

            //Test Factory
            Factories.HSegmentFactory sFac = new Factories.HSegmentFactory();

            foreach (TempRoom blankRoom in temp)
            {
                if ()
                {
                    HotelSegments.IHSegment myRoom = sFac.Create("Room") as HotelSegments.IHSegment;

                }
                else
                {
                    HotelSegments.IHSegment myRoom = sFac.Create("Room") as HotelSegments.IHSegment;
                }


            }



            Console.WriteLine("Checkpoint: 2");

        }


    }
}
