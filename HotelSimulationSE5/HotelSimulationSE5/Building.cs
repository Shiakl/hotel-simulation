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
        public int segmentSize_X = 104;
        public int segmentSize_Y = 60;
        public int max_x;
        public int max_y;
        private Node[] nodes;
        public Node[] elevatorNodes;
        private Node[] staircaseNodes;
        private List<TempRoom> temp;
        public bool elevatorLeft;



        public Building()
        {
            temp = new List<TempRoom>();
            layoutstring = System.IO.File.ReadAllText(@"..\..\External\Hotel.layout");
            Read_Layout();
            elevatorLeft = true;
            max_x = Find_Max_X(temp);
            max_y = Find_Max_Y(temp);

            nodes = new Node[max_x * max_y];
            elevatorNodes = new Node[max_y];
            staircaseNodes = new Node[max_y];

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

        private enum ID_List
        {
            Staircase = 0,
            Elevator = 1,
            Reception = 2
        }


        public void CreateHotel(Form mainform)
        {
            Factories.HSegmentFactory sFac = new Factories.HSegmentFactory();
            int nodecounter = 0;

            //Create elevator nodes
            for(int y = 0; y < max_y; y++)
            {

                Panel tempPanel = new Panel
                {
                    Size = new Size(segmentSize_X, segmentSize_Y),
                    Location = new Point(0, y * segmentSize_Y),
                };

                elevatorNodes[y] = new Node(tempPanel);

                if (y == max_y-1)
                {
                    elevatorNodes[y].MySegment = sFac.Create("Elevator" , (int)ID_List.Reception, firstfloor: true) as HotelSegments.IHSegment;
                }
                else
                {

                elevatorNodes[y].MySegment = sFac.Create("Elevator", (int)ID_List.Elevator) as HotelSegments.IHSegment;
                }
                mainform.Controls.Add(elevatorNodes[y].MyPanel);
                elevatorNodes[y].ColorMe();
            }

            //Create staircase nodes
            for (int y = 0; y < max_y; y++)
            {

                Panel tempPanel = new Panel
                {
                    Size = new Size(segmentSize_X, segmentSize_Y),
                    Location = new Point(segmentSize_X, y * segmentSize_Y),
                };

                staircaseNodes[y] = new Node(tempPanel);

                if (y == max_y - 1)
                {
                    staircaseNodes[y].MySegment = sFac.Create("Staircase", (int)ID_List.Staircase, firstfloor: true) as HotelSegments.IHSegment;
                }
                else
                {
                    staircaseNodes[y].MySegment = sFac.Create("Staircase", (int)ID_List.Staircase) as HotelSegments.IHSegment;
                }
                mainform.Controls.Add(staircaseNodes[y].MyPanel);
                staircaseNodes[y].ColorMe();
            }

            //Create all the Nodes
            for (int y = 0; y < max_y; y++)
            {
                for (int x = 0; x < max_x; x++)
                {
                    Panel tempPanel = new Panel
                    {
                        Size = new Size(segmentSize_X, segmentSize_Y),
                        Location = new Point(x * segmentSize_X + segmentSize_X + segmentSize_X, y * segmentSize_Y)
                    };

                    nodes[nodecounter] = new Node(tempPanel);
                    mainform.Controls.Add(nodes[nodecounter].MyPanel);
                    nodecounter++;
                }
            }


            int elevatorLevel = 0;
            int staircaselevel = 0;
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
                    nodes[tc].LeftNode = staircaseNodes[staircaselevel];
                    staircaseNodes[staircaselevel].RightNode = nodes[tc];

                    if (staircaselevel < max_y-1)
                    {
                        staircaseNodes[staircaselevel].BottomNode = staircaseNodes[staircaselevel + 1];
                    }
                    if (elevatorLevel != 0)
                    {
                        staircaseNodes[staircaselevel].TopNode = staircaseNodes[staircaselevel - 1];
                    }

                    staircaseNodes[staircaselevel].Add_myConnections();

                    staircaselevel++;
                }

                nodes[tc].Add_myConnections();
            }
            
            foreach (var Elevator in elevatorNodes)
            {
                elevatorNodes[elevatorLevel].RightNode = staircaseNodes[elevatorLevel];
                staircaseNodes[elevatorLevel].LeftNode = elevatorNodes[elevatorLevel];

                if (elevatorLevel < max_y - 1)
                {
                    elevatorNodes[elevatorLevel].BottomNode = elevatorNodes[elevatorLevel + 1];
                }

                if (elevatorLevel != 0)
                {
                    elevatorNodes[elevatorLevel].TopNode = elevatorNodes[elevatorLevel - 1];
                }

                elevatorNodes[elevatorLevel].Add_myConnections();

                elevatorLevel++;
            }

            foreach (TempRoom blankRoom in temp)
            {
                HotelSegments.IHSegment tempSeg;

                if (blankRoom.AreaType.Equals("Room"))
                {
                   tempSeg = sFac.Create(blankRoom.AreaType, blankRoom.Seg_ID,blankRoom.Dimension_X,blankRoom.Dimension_Y, blankRoom.Classification) as HotelSegments.IHSegment;
                }
                else
                {
                    tempSeg = sFac.Create(blankRoom.AreaType, blankRoom.Seg_ID, blankRoom.Dimension_X, blankRoom.Dimension_Y) as HotelSegments.IHSegment;
                }

                x_track = blankRoom.Position_X + 1;
                y_track = blankRoom.Position_Y-1;
                Go_Right(elevatorNodes[max_y - 1]).MySegment = tempSeg;
            }

            foreach (Node reload in nodes)
            {
                if (reload != null)
                {
                    reload.ColorMe();
                }
            }

            Reload_Available_Rooms();

            Console.WriteLine("Checkpoint: 2");

        }//Create Hotel

        private int x_track;
        private int y_track;
        public Node Go_Right(Node nav)
        {
            if (x_track != 0)
            {
                if (nav.RightNode != null)
                {
                    x_track -= 1;
                    return Go_Right(nav.RightNode);
                }
                else
                {
                    return nav;
                }
            }
            else
            {
                return Go_Up(nav);
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

        List<HotelSegments.GuestRoom> AvailableRooms = new List<HotelSegments.GuestRoom>();
        public HotelSegments.GuestRoom AssignRoom(int value)
        {
            var tempSeg =
                from w in nodes
                where (w.MySegment is HotelSegments.GuestRoom)
                select w;

            List<Node> tempNode = (
                from w in tempSeg
                where (w.MySegment.ID==value)
                select w).ToList();

            Console.WriteLine("Checkpoint: 5");

            if (tempNode[0] != null && tempNode[0].MySegment is HotelSegments.GuestRoom)
            {               
                return tempNode[0].MySegment as HotelSegments.GuestRoom;
            }
            else
            {
                return null;
            }

        }

        public void Reload_Available_Rooms()
        {
            List<HotelSegments.GuestRoom> GuestRooms = (
                from w in nodes
                where (w.MySegment is HotelSegments.GuestRoom)
                select w.MySegment as HotelSegments.GuestRoom
                ).ToList();

            AvailableRooms = (
                from w in GuestRooms
                where (w.Reserved == false)
                select w
                ).ToList();
                    
            BreakPoint();
        }

        private List<Guest> _guestList = new List<Guest>();
        public void Create_Guest(Node currentNode)
        {
            //Test Create guest
            Reload_Available_Rooms();
            Guest arrival = new Guest(currentNode.panelPb);
            arrival.MyNode = currentNode;
            if (AvailableRooms[0] != null)
            {
            arrival.MyRoom = AssignRoom(AvailableRooms[0].ID);
            arrival.MyRoom.Reserved = true;
            }
            //elevatorNodes[max_y - 1].MyUnits.Add(arrival);
            _guestList.Add(arrival);
            arrival.Move();
            Console.WriteLine("Checkpoint: 3");
        }


        public void Move_Guest(Form mainform)
        {               
            foreach(Guest currentG in _guestList)
            {
                if (currentG.MyNode.RightNode != null)
                {
                //currentG.MyNode.RightNode.MyUnits.Add(currentG);
                currentG.Move_to_Node(currentG.MyNode.RightNode, currentG.MyNode);
                //currentG.MyNode.MyUnits.Remove(currentG);
                currentG.Move();
                }
            }

        }

            public void BreakPoint()
        {
            Console.WriteLine("Checkpoint: 4");
        }


    }//Building
}
