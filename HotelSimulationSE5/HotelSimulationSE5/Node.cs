using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace HotelSimulationSE5
{
    public class Node
    {
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
        public Node TopNode { get; set; }
        public Node BottomNode { get; set; }
        public Node[] MyConnections { get; set; }
        public int Distance { get; set; }     
        public Panel MyPanel { get; set; }
        public PictureBox panelPb;
        public HotelSegments.IHSegment MySegment { get; set; }
        private const int _startwaarde = 0;

        public Node(Panel box)
        {
            MyPanel = box;
            panelPb = new PictureBox
            {
                Size = MyPanel.Size
            };
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

        /*
         Search algoritm:
        1. Check right till target found or null, If target not found The room must be on the left.
        2. Check Left till Stairs or target found.
        3. From Stairs go up and check all rooms right. If target is not on this floor its on the floor above or below.
        4. Check all floors above till last floor reached, if target is not found check the floors below the starting floor.
        5. Check all the floors below the starting floor, if target is not found the target room does not exist. 
         */


        private List<DIRECTIONS> Route = new List<DIRECTIONS>();
        private List<DIRECTIONS> tempDirections = new List<DIRECTIONS>();

        public List<DIRECTIONS> Set_route(Node current, HotelSegments.IHSegment target)
        {
            right_check = false;
            if (Find_route(current,target.ID,DIRECTIONS.RIGHT) == true)
            {
                Route = tempDirections;
            }
            else if(Find_route(current, target.ID, DIRECTIONS.LEFT) == true)
            {
                Route = tempDirections;
            }
            return Route;
        }

        private bool right_check;
        public bool Find_route(Node current, int target, DIRECTIONS direction)
        {           
            if (Room_Found(current, target) == true)
            {
                return true;
            }
            else if (current.MyConnections[(int)direction] == null && right_check == false)
            {
                tempDirections.Clear();
                right_check = true;
                return false;
            }
            else if (current.MySegment is HotelSegments.Staircase)
            {
                right_check = false;
                tempDirections.Add(DIRECTIONS.TOP);
                return Find_route(current.MyConnections[(int)direction], target, DIRECTIONS.RIGHT);
            }
            else
            {
                tempDirections.Add(direction);
                return Find_route(current.MyConnections[(int)direction], target, direction);
            }
        }


        private bool Room_Found(Node current, int targetID)
        {
            if (current.MySegment != null)
            {
                if (current.MySegment.ID == targetID)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a path for a guest to its designated room
        /// </summary>
        /// <param name="currentroom">The room where the guest is currently at</param>
        /// <param name="targetroom">The room where the guest wants to go</param>
        /// <returns>The generated path in a List with DIRECTIONS</returns>
        public List<DIRECTIONS> Pathfinding(Node currentroom, HotelSegments.IHSegment targetroom)
        {
            int ammountcheckedright = _startwaarde; //Used to save how many times the path has gone to the right
            int ammountleveled = _startwaarde; //Used to save how many times the path has gone up
            bool found = false; //Used to see if the path has been figured out
            bool checkedright = false; //Used to see if the right side of the current node has been checked
            bool checkedtop = false; //Used to see if every level on top of the starting room has been checked
            bool startlevel = true; //Used to see if the path is still on its startinglevel
            Node PathRoom = currentroom; //Used to hop the path generator to the next node
            Node ElevatorOrStaircase = currentroom; //Used to save the elevator- or staircaseNode(the first it has crossed) on the startlevel
            List<DIRECTIONS> pathfinder = new List<DIRECTIONS>();
            while (found == false)
            {
                if (checkedright == true)
                {
                    if (PathRoom.MySegment is HotelSegments.Elevator || PathRoom.MySegment is HotelSegments.Staircase)
                    {
                        if (startlevel == true)
                        {
                            startlevel = false;
                            ElevatorOrStaircase = PathRoom;
                        }

                        if (checkedtop == false && PathRoom.TopNode != null)
                        {
                            pathfinder.Add(DIRECTIONS.TOP);
                            PathRoom = PathRoom.TopNode;
                            ElevatorOrStaircase = PathRoom;
                            ammountleveled++;
                            checkedright = false;
                        }

                        else if (checkedtop == true && PathRoom.BottomNode != null)
                        {
                            pathfinder.Add(DIRECTIONS.BOTTOM);
                            PathRoom = PathRoom.BottomNode;
                            ElevatorOrStaircase = PathRoom;
                            ammountleveled++;
                            checkedright = false;
                        }

                        else
                        {
                            checkedtop = true;
                            for (int ammountremoved = 0; ammountremoved < ammountleveled; ammountremoved++)
                            {
                                pathfinder.Remove(pathfinder.LastOrDefault());
                            }
                            ammountleveled = _startwaarde;
                            PathRoom = ElevatorOrStaircase;
                        }
                    }

                    else if (PathRoom.LeftNode.MySegment != null)
                    {
                        if (PathRoom.LeftNode.MySegment.ID != targetroom.ID)
                        {
                            pathfinder.Add(DIRECTIONS.LEFT);
                            PathRoom = PathRoom.LeftNode;
                        }

                        else
                        {
                            pathfinder.Add(DIRECTIONS.LEFT);
                            found = true;
                            break;
                        }
                    }

                    else if (PathRoom.LeftNode.MySegment == null)
                    {
                        pathfinder.Add(DIRECTIONS.LEFT);
                        PathRoom = PathRoom.LeftNode;

                    }
                }

                else if (checkedright == false)
                {

                    if (PathRoom.RightNode != null)
                    {
                        if (PathRoom.RightNode.MySegment != null)
                        {
                            if (PathRoom.RightNode.MySegment.ID != targetroom.ID)
                            {
                                pathfinder.Add(DIRECTIONS.RIGHT);
                                PathRoom = PathRoom.RightNode;
                                ammountcheckedright++;
                            }
                            else
                            {
                                pathfinder.Add(DIRECTIONS.RIGHT);
                                found = true;
                                break;
                            }
                        }
                        else if (PathRoom.RightNode.MySegment == null)
                        {
                            pathfinder.Add(DIRECTIONS.RIGHT);
                            PathRoom = PathRoom.RightNode;
                            ammountcheckedright++;
                        }
                    }

                    else if (PathRoom.RightNode == null)
                    {
                        if (startlevel == true)
                        {
                            PathRoom = currentroom;
                        }

                        else
                        {
                            PathRoom = ElevatorOrStaircase;
                        }

                        for (int ammountremoved = 0; ammountremoved < ammountcheckedright; ammountremoved++)
                        {
                            pathfinder.Remove(pathfinder.LastOrDefault());
                        }

                        checkedright = true;
                        ammountcheckedright = _startwaarde;
                    }
                }
            }
            return pathfinder;
        }
    }
}
