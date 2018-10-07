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
        public int segmentSize_X = 80;
        public int segmentSize_Y = 50;
        public int max_x;
        public int max_y;
        private Node[] nodes;
        private Node[] elevatorNodes;
        private List<TempRoom> temp;
        public bool elevatorLeft;



        public Building()
        {
            temp = new List<TempRoom>();
            layoutstring = System.IO.File.ReadAllText(@"..\..\External\Hotel2_reparatieVanVersie1.layout");
            Read_Layout();
            elevatorLeft = true;
            max_x = Find_Max_X(temp);
            max_y = Find_Max_Y(temp);

            nodes = new Node[Find_Max_X(temp) * Find_Max_Y(temp)];
            elevatorNodes = new Node[Find_Max_Y(temp)];

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
            Factories.HSegmentFactory sFac = new Factories.HSegmentFactory();
            int segmentcount = 1;
            int nodecounter = 0;

            //Create elevator nodes
            for(int y = max_y-1; y >= 0; y--)
            {

                Panel tempPanel = new Panel
                {
                    Size = new Size(segmentSize_X, segmentSize_Y),
                    Location = new Point(0, y * segmentSize_Y),
                };

                elevatorNodes[y] = new Node(tempPanel);
                elevatorNodes[y].MySegment = sFac.Create("Elevator", segmentcount) as HotelSegments.IHSegment;
                mainform.Controls.Add(elevatorNodes[y].MyPanel);
                elevatorNodes[y].ColorMe();
                segmentcount++;
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

            //Move the button



            int elevatorLevel = 0;
            //connect nodes
            for (int tc = 0; tc < (max_x*max_y); tc++)
            {
                //Except for the first row(>max_x) all nodes have a top connection.
                if (tc > max_x - 1)
                {
                    nodes[tc].TopNode = nodes[tc - max_x];
                }
                //Except for the last row all nodes have a bottom connection.
                if (tc < (max_x * max_y) - max_x)
                {
                    nodes[tc].BottomNode = nodes[tc + max_x];
                }
                //Except for the last column all nodes have a right connection.
                if (tc % max_x < max_x - 1)
                {
                    nodes[tc].RightNode = nodes[tc + 1];
                }
                //Except for the first column all nodes have a left connection.
                if (tc % max_x > 0)
                {
                    nodes[tc].LeftNode = nodes[tc - 1];
                }
                //Add elevator nodes to connections on all the nodes in the first column.
                if (nodes[tc].LeftNode is null)
                {
                    nodes[tc].LeftNode = elevatorNodes[elevatorLevel];
                    elevatorNodes[elevatorLevel].RightNode = nodes[tc];

                    if (elevatorLevel<max_y-1)
                    {
                        elevatorNodes[elevatorLevel].BottomNode = elevatorNodes[elevatorLevel+1];
                    }
                    if (elevatorLevel!=0)
                    {
                    elevatorNodes[elevatorLevel].TopNode = elevatorNodes[elevatorLevel-1];
                    }

                    elevatorNodes[elevatorLevel].Add_myConnections();

                    elevatorLevel++;
                }

                nodes[tc].Add_myConnections();

            }
            

            foreach (TempRoom blankRoom in temp)
            {
                HotelSegments.IHSegment tempSeg;

                if (blankRoom.AreaType.Equals("Room"))
                {
                   tempSeg = sFac.Create(blankRoom.AreaType, segmentcount, blankRoom.Classification) as HotelSegments.IHSegment;
                    segmentcount++;
                }
                else
                {
                    tempSeg = sFac.Create(blankRoom.AreaType, segmentcount) as HotelSegments.IHSegment;
                    segmentcount++;
                }

                x_track = blankRoom.Position_X;
                y_track = blankRoom.Position_Y-1;
                Go_Right(elevatorNodes[max_y - 1]).MySegment = tempSeg;
            }

            foreach (Node reload in nodes)
            {
                reload.ColorMe();
            }

            Console.WriteLine("Checkpoint: 2");

        }//Create Hotel

        private int x_track;
        private int y_track;
        public Node Go_Right(Node Nav)
        {
            if(Nav.RightNode != null)
            {
                if (x_track == 0)
                {
                    return Go_Up(Nav);
                }
                else
                {
                    x_track--;
                    return Go_Right(Nav.RightNode);
                }
            }
            else
            {
                return Nav;
            }
        }

        public Node Go_Up(Node Nav)
        {
            if (Nav.TopNode != null)
            {
                if (y_track==0)
                {
                    return Nav;
                }
                else
                {
                    y_track--;
                return Go_Up(Nav.TopNode);
                }
            }
            else
            {
                return Nav;
            }
        }

        public void Create_Guest(Form mainform)
        {
            if (elevatorNodes[max_y-1]!=null)
            {
                Panel tempPanel = new Panel
                {
                    Size = new Size(15, 25),
                    Location = new Point(elevatorNodes[max_y-1].MyPanel.Location.X, elevatorNodes[max_y - 1].MyPanel.Location.Y+max_y)
                };
                Guest arrival = new Guest(tempPanel);
                //arrival.Add_panel(tempPanel);
                mainform.Controls.Add(arrival.MyPanel);
                arrival.Move();
                elevatorNodes[max_y - 1].MyUnits.Add(arrival);
            }
            //panelPb.BackColor = Color.Transparent;

            foreach (Guest visitor in elevatorNodes[max_y-1].MyUnits)
            {
                visitor.Move();
            }


            Console.WriteLine("Checkpoint: 3");
        }

        public void BreakPoint()
        {
            Console.WriteLine("Checkpoint: 4");
        }





    }//Building
}
