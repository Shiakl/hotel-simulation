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

        public int segmentSizeX = 104; //X size of each hotel segment
        public int segmentSizeY = 60; //Y size of each hotel segment
        public int maxXcoordinate;
        public int maxYcoordinate;
        public List<Guest> _guestList = new List<Guest>();//List of every guest currently in the hotel
        public bool elevatorLeft;
        private Node[] _nodes; //Saves a array of all rooms(GuestRoom, Cinema, Fitness, Restaurant) with its properties
        public Node[] elevatorNodes; //Saves a array of all elevators with its properties
        private Node[] _staircaseNodes; //Saves a array of staircases with its properties
        private List<TempRoom> _temp; //Saves a list of every room in the hotel
        private string _layoutstring; //Hotel layout(blueprint)
        private const int _startwaarde = 0;

        public Building()
        {
            _temp = new List<TempRoom>();
            _layoutstring = System.IO.File.ReadAllText(@"..\..\External\Hotel3.layout"); //Reads the layout file and pushes it into a string
            Read_Layout();
            elevatorLeft = true;
            maxXcoordinate = FindMaxX(_temp);
            maxYcoordinate = FindMaxY(_temp);

            _nodes = new Node[maxXcoordinate * maxYcoordinate];
            elevatorNodes = new Node[maxYcoordinate];
            _staircaseNodes = new Node[maxYcoordinate];

            Console.WriteLine("Checkpoint: 1");
        }

        /// <summary>
        /// Deserializes the string.layout file to the temp list. Each item in the list will now be given its properties
        /// </summary>
        public void Read_Layout()
        {
            _temp = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TempRoom>>(_layoutstring);
        }

        /// <summary>
        /// Checks each item in the list.temp for the X coordinate and saves it to int.hotel_X_Size if it's bigger than the last greatest found X coordinate
        /// </summary>
        /// <param name="roomList">List of all rooms in the hotel with its properties</param>
        /// <returns>The biggest found X coordinate</returns>
        private int FindMaxX(List<TempRoom> roomList)
        {
            int hotelXSize = 0;

            foreach(TempRoom segment in roomList)
            {
                if (segment.PositionX > hotelXSize)
                {
                    if (segment.DimensionX>1)
                    {
                    hotelXSize = segment.PositionX + (segment.DimensionX-1);
                    }
                    else
                    {
                        hotelXSize = segment.PositionX;
                    }
                }
            }
            return hotelXSize; 
        }

        /// <summary>
        /// Checks each item in the list.temp for the Y coordinate and saves it to int.hotel_Y_Size if it's bigger then the last greatest found Y coordinate
        /// </summary>
        /// <param name="roomList">List of all rooms in the hotel with its properties</param>
        /// <returns>The biggest found Y coordinate</returns>
        private int FindMaxY(List<TempRoom> roomList)
        {
            int hotelYsize = 0;

            foreach (TempRoom segment in roomList)
            {
                if (segment.PositionY > hotelYsize)
                {
                    if (segment.DimensionX > 1)
                    {
                        hotelYsize = segment.PositionY + (segment.DimensionY - 1);
                    }
                    else
                    {
                        hotelYsize = segment.PositionY;
                    }
                }
            }

            return hotelYsize;
        }

        /// <summary>
        /// IDs for staircases, elevators and the reception
        /// </summary>
        private enum ID_List
        {
            Staircase = 0,
            Elevator = 1,
            Reception = 2
        }

        /// <summary>
        /// Create and link all the nodes on which the rooms are drawn and assigns each node a segment.
        /// </summary>
        /// <param name="mainform">The display window</param>
        public void CreateHotel(Form mainform)
        {
            //Factory for room segments
            Factories.HSegmentFactory sFac = new Factories.HSegmentFactory();

            int nodecounter = 0;

            //Create elevator nodes
            for(int currentYcoordinate = _startwaarde; currentYcoordinate < maxYcoordinate; currentYcoordinate++)
            {
                //A temporary panel is created for every node with x,y coordiniates according to their position in the hotel.
                Panel tempPanel = new Panel
                {
                    Size = new Size(segmentSizeX, segmentSizeY),
                    Location = new Point(_startwaarde, currentYcoordinate * segmentSizeY),
                };

                elevatorNodes[currentYcoordinate] = new Node(tempPanel);

                if (currentYcoordinate == maxYcoordinate-1)
                {
                    elevatorNodes[currentYcoordinate].MySegment = sFac.Create("Elevator" , (int)ID_List.Reception, firstfloor: true) as HotelSegments.IHSegment;
                }
                else
                {
                    elevatorNodes[currentYcoordinate].MySegment = sFac.Create("Elevator", (int)ID_List.Elevator) as HotelSegments.IHSegment;
                }
                mainform.Controls.Add(elevatorNodes[currentYcoordinate].MyPanel);
                elevatorNodes[currentYcoordinate].ColorMe();
            }

            //Create staircase nodes
            for (int currentYcoordinate = _startwaarde; currentYcoordinate < maxYcoordinate; currentYcoordinate++)
            {
                //A temporary panel is created for every node with x,y coordiniates according to their position in the hotel.
                Panel tempPanel = new Panel
                {
                    Size = new Size(segmentSizeX, segmentSizeY),
                    Location = new Point(segmentSizeX, currentYcoordinate * segmentSizeY),
                };

                _staircaseNodes[currentYcoordinate] = new Node(tempPanel);

                if (currentYcoordinate == maxYcoordinate - 1)
                {
                    _staircaseNodes[currentYcoordinate].MySegment = sFac.Create("Staircase", (int)ID_List.Staircase, firstfloor: true) as HotelSegments.IHSegment;
                }
                else
                {
                    _staircaseNodes[currentYcoordinate].MySegment = sFac.Create("Staircase", (int)ID_List.Staircase) as HotelSegments.IHSegment;
                }
                mainform.Controls.Add(_staircaseNodes[currentYcoordinate].MyPanel);
                _staircaseNodes[currentYcoordinate].ColorMe();
            }

            //Create all the Nodes
            for (int currentycoordinate = _startwaarde; currentycoordinate < maxYcoordinate; currentycoordinate++)
            {
                for (int currentXcoordinate = _startwaarde; currentXcoordinate < maxXcoordinate; currentXcoordinate++)
                {
                    Panel tempPanel = new Panel
                    {
                        Size = new Size(segmentSizeX, segmentSizeY),
                        Location = new Point(currentXcoordinate * segmentSizeX + segmentSizeX + segmentSizeX, currentycoordinate * segmentSizeY)
                    };

                    _nodes[nodecounter] = new Node(tempPanel);
                    mainform.Controls.Add(_nodes[nodecounter].MyPanel);
                    nodecounter++;
                }
            }


            int elevatorLevel = _startwaarde;
            int staircaselevel = _startwaarde;
            //connect nodes

            for (int currentnode = 0; currentnode < (maxXcoordinate*maxYcoordinate); currentnode++)
            {
                //Except for the first row(>max_x) all nodes have a top connection.
                if (currentnode > maxXcoordinate - 1)
                {
                    _nodes[currentnode].TopNode = _nodes[currentnode - maxXcoordinate];
                }
                //Except for the last row all nodes have a bottom connection.
                if (currentnode < (maxXcoordinate * maxYcoordinate) - maxXcoordinate)
                {
                    _nodes[currentnode].BottomNode = _nodes[currentnode + maxXcoordinate];
                }
                //Except for the last column all nodes have a right connection.
                if (currentnode % maxXcoordinate < maxXcoordinate - 1)
                {
                    _nodes[currentnode].RightNode = _nodes[currentnode + 1];
                }
                //Except for the first column all nodes have a left connection.
                if (currentnode % maxXcoordinate > 0)
                {
                    _nodes[currentnode].LeftNode = _nodes[currentnode - 1];
                }
                //Add elevator nodes to connections on all the nodes in the first column.
                if (_nodes[currentnode].LeftNode is null)
                {
                    _nodes[currentnode].LeftNode = _staircaseNodes[staircaselevel];
                    _staircaseNodes[staircaselevel].RightNode = _nodes[currentnode];

                    if (staircaselevel < maxYcoordinate - 1)
                    {
                        _staircaseNodes[staircaselevel].BottomNode = _staircaseNodes[staircaselevel + 1];
                    }

                    if (staircaselevel != _startwaarde)
                    {
                        _staircaseNodes[staircaselevel].TopNode = _staircaseNodes[staircaselevel - 1];
                    }

                    _staircaseNodes[staircaselevel].Add_myConnections();

                    staircaselevel++;
                }

                _nodes[currentnode].Add_myConnections();
            }
            
            //Connect elevator nodes to the graph
            foreach (var Elevator in elevatorNodes)
            {
                elevatorNodes[elevatorLevel].RightNode = _staircaseNodes[elevatorLevel];
                _staircaseNodes[elevatorLevel].LeftNode = elevatorNodes[elevatorLevel];

                if (elevatorLevel < maxYcoordinate - 1)
                {
                    elevatorNodes[elevatorLevel].BottomNode = elevatorNodes[elevatorLevel + 1];
                }

                if (elevatorLevel != _startwaarde)
                {
                    elevatorNodes[elevatorLevel].TopNode = elevatorNodes[elevatorLevel - 1];
                }

                elevatorNodes[elevatorLevel].Add_myConnections();

                elevatorLevel++;
            }

            //Add a segment to each node according to their position in the layout file from temp
            foreach (TempRoom blankRoom in _temp)
            {
                HotelSegments.IHSegment tempSeg;

                if (blankRoom.AreaType.Equals("Room"))
                {
                   tempSeg = sFac.Create(blankRoom.AreaType, blankRoom.SegID,blankRoom.DimensionX,blankRoom.DimensionY, blankRoom.Classification) as HotelSegments.IHSegment;
                }
                else
                {
                    tempSeg = sFac.Create(blankRoom.AreaType, blankRoom.SegID, blankRoom.DimensionX, blankRoom.DimensionY) as HotelSegments.IHSegment;
                }

                x_track = blankRoom.PositionX + 1;
                y_track = blankRoom.PositionY-1;
                Go_Right(elevatorNodes[maxYcoordinate - 1]).MySegment = tempSeg;
            }

            //Redraw all the nodes
            foreach (Node reload in _nodes)
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
            if (x_track != _startwaarde)
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
                if (y_track == _startwaarde)
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

        public List<HotelSegments.GuestRoom> AvailableRooms = new List<HotelSegments.GuestRoom>();
        public HotelSegments.GuestRoom AssignRoom(int value)
        {
            var tempSeg =
                from w in _nodes
                where (w.MySegment is HotelSegments.GuestRoom)
                select w;

            List<Node> tempNode = (
                from w in tempSeg
                where (w.MySegment.ID==value)
                select w).ToList();

            if (tempNode.FirstOrDefault() != null && tempNode.FirstOrDefault().MySegment is HotelSegments.GuestRoom)
            {               
                return tempNode.FirstOrDefault().MySegment as HotelSegments.GuestRoom;
            }
            else
            {
                return null;
            }

        }

        public void ReloadAvailableRooms()
        {
            List<HotelSegments.GuestRoom> GuestRooms = (
                from w in _nodes
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
            ReloadAvailableRooms();
            Guest arrival = new Guest(currentNode);
            if (AvailableRooms.Any())
            {
                arrival.MyRoom = AssignRoom(AvailableRooms.First().ID);
                arrival.MyRoom.Reserved = true;
                arrival.Path = arrival.MyNode.Pathfinding(arrival.MyNode, arrival.MyRoom);
            }
            _guestList.Add(arrival);
            arrival.Redraw();
        }

        public void Move_Guest(Form mainform)
        {
            int elcap = elevatorNodes.FirstOrDefault().MySegment.Capacity;

            foreach(Guest currentG in _guestList)
            {
                if (currentG.Moving == true)
                {
                    currentG.Destination_reached();
                    if (currentG.Moving == true && currentG.Path.Any())
                    {
                        currentG.MoveToNode(currentG.MyNode.MyConnections[(int)currentG.Path.First()], currentG.MyNode);
                        currentG.Redraw();                   
                    }
                }
                else
                {
                    currentG.Moving = true;
                }
            }
        }
    }
}
