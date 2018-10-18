using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using HotelEvents;

namespace HotelSimulationSE5
{
    class Building
    {
        private string layoutstring; //Hotel layout(blueprint)
        public int segmentSize_X = 104; //X size of each hotel segment
        public int segmentSize_Y = 60; //Y size of each hotel segment
        public int max_x;
        public int max_y;
        private Node[] nodes; //Saves a array of all rooms(GuestRoom, Cinema, Fitness, Restaurant) with its properties
        public Node[] elevatorNodes; //Saves a array of all elevators with its properties
        private Node[] staircaseNodes; //Saves a array of staircases with its properties
        private List<TempRoom> temp; //Saves a list of every room in the hotel
        private List<Guest> _guestList = new List<Guest>();//List of every guest currently in the hotel
        public bool elevatorLeft;
       

        public Building()
        {
            temp = new List<TempRoom>();
            layoutstring = System.IO.File.ReadAllText(@"..\..\External\Hotel3.layout"); //Reads the layout file and pushes it into a string
            Read_Layout();
            elevatorLeft = true;
            max_x = Find_Max_X(temp);
            max_y = Find_Max_Y(temp);

            nodes = new Node[max_x * max_y];
            elevatorNodes = new Node[max_y];
            staircaseNodes = new Node[max_y];

            Console.WriteLine("Checkpoint: 1");
        }
        /// <summary>
        /// Deserializes the string.layoutstring to the temp list. Each item in the list will now be given its properties
        /// </summary>
        public void Read_Layout()
        {
            temp = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TempRoom>>(layoutstring);
        }

        /// <summary>
        /// Checks each item in the list.temp for the X coordinate and saves it to int.hotel_X_Size if it's bigger than the last greatest found X coordinate
        /// </summary>
        /// <param name="roomList">List of all rooms in the hotel with its properties</param>
        /// <returns>The biggest found X coordinate</returns>
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

        /// <summary>
        /// Checks each item in the list.temp for the Y coordinate and saves it to int.hotel_Y_Size if it's bigger then the last greatest found Y coordinate
        /// </summary>
        /// <param name="roomList">List of all rooms in the hotel with its properties</param>
        /// <returns>The biggest found Y coordinate</returns>
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

        /// <summary>
        /// Sets IDs for staircases, elevators and the reception
        /// </summary>
        private enum ID_List
        {
            Staircase = 0,
            Elevator = 1,
            Reception = 2
        }

        /// <summary>
        /// Creates panels for all the rooms. After which it will generates the segments in nodes for each room using the segment factory. It will then adds itself to the form with its corresponding picture
        /// </summary>
        /// <param name="mainform">The display window</param>
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
                //Aside from the first row(>max_x) all nodes have a top connection.
                if (tc > max_x - 1)
                {
                    nodes[tc].TopNode = nodes[tc - max_x];
                }
                // Aside from the last row all nodes have a bottom connection.
                if (tc < (max_x * max_y) - max_x)
                {
                    nodes[tc].BottomNode = nodes[tc + max_x];
                }
                //Aside from the last column all nodes have a right connection.
                if (tc % max_x < max_x - 1)
                {
                    nodes[tc].RightNode = nodes[tc + 1];
                }
                //Aside from the first column all nodes have a left connection.
                if (tc % max_x > 0)
                {
                    nodes[tc].LeftNode = nodes[tc - 1];
                }
                //Add staircase nodes to connections on all the nodes in the first column.
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
            
            //Connect elevator nodes to the graph
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

            //Add a segment to each node according to their position in the layout file from temp
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

            //Redraw all the nodes
            foreach (Node reload in nodes)
            {
                if (reload != null)
                {
                    reload.ColorMe();
                }
            }

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
        }

        public void Create_Guest(Node currentNode)
        {
            Reload_Available_Rooms();
            Guest arrival = new Guest(currentNode);
            if (AvailableRooms.Count() >  0)
            {
                arrival.MyRoom = AssignRoom(AvailableRooms[0].ID);
                arrival.MyRoom.Reserved = true;
                arrival.Path = arrival.MyNode.Pathfinding(arrival.MyNode, arrival.MyRoom);
            }
            _guestList.Add(arrival);
            arrival.Redraw();
        }

        public void Move_Guest(Form mainform)
        {               
            foreach(Guest currentG in _guestList)
            {
                if (currentG.Moving == true)
                {
                    currentG.Destination_reached();
                    if (currentG.Moving == true && currentG.Path.Count()>0)
                    {
                        currentG.Move_to_Node(currentG.MyNode.MyConnections[(int)currentG.Path[0]], currentG.MyNode);
                        currentG.Redraw();                   
                    }

                }
                else
                {
                    currentG.Moving = true;
                }
            }

        }

    }//Building
}
