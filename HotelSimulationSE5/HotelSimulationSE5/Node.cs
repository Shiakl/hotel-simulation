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

        public Node(Panel box)
        {
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


        
        public List<DIRECTIONS> Pathfinding(Node currentroom, HotelSegments.IHSegment targetroom)
        {
            int ammountcheckedright = 0;
            int ammountleveled = 0;
            bool found = false;
            bool checkedright = false;
            bool checkedtop = false;
            bool firstlevel = true;
            Node PathRoom = currentroom;
            Node ElevatorOrStaircase = currentroom;
            List<DIRECTIONS> pathfinder = new List<DIRECTIONS>();
            while (found == false)
            {
                if (checkedright == true)
                {
                    if (PathRoom.MySegment is HotelSegments.Elevator || PathRoom.MySegment is HotelSegments.Staircase)
                    {
                        if (firstlevel == true)
                        {
                            firstlevel = false;
                            ElevatorOrStaircase = PathRoom;
                        }

                        if (checkedtop == false || PathRoom.TopNode != null)
                        {
                            pathfinder.Add(DIRECTIONS.TOP);
                            PathRoom = PathRoom.TopNode;
                            ElevatorOrStaircase = PathRoom.TopNode;
                            ammountleveled++;
                            checkedright = false;
                        }

                        else if (checkedtop == true || PathRoom.BottomNode != null)
                        {
                            pathfinder.Add(DIRECTIONS.BOTTOM);
                            PathRoom = PathRoom.BottomNode;
                            ElevatorOrStaircase = PathRoom.BottomNode;
                            ammountleveled++;
                            checkedright = false;
                        }

                        else
                        {
                            checkedtop = true;
                            for (int i = 0; i < ammountleveled; i++)
                            {
                                pathfinder.Remove(pathfinder.LastOrDefault());
                            }
                            ammountleveled = 0;
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
                        if (firstlevel == true)
                        {
                            PathRoom = currentroom;
                        }

                        else
                        {
                            PathRoom = ElevatorOrStaircase;
                        }
                        checkedright = true;
                        for (int i = 0; i < ammountcheckedright; i++)
                        {
                            pathfinder.Remove(pathfinder.LastOrDefault());
                        }
                        ammountcheckedright = 0;
                    }
                }
            }
            return pathfinder;
        }
    }
}
