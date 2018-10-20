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
        public Panel MyPanel { get; set; }
        public PictureBox panelPb;
        public HotelSegments.HSegment MySegment { get; set; }
        private const int _startwaarde = 0;

        public int Distance { get; set; }   
        public DIRECTIONS Previous { get; set; }

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


        /// <summary>
        /// Creates a path for a guest to its designated room
        /// </summary>
        /// <param name="currentroom">The room where the guest is currently at</param>
        /// <param name="targetroom">The room where the guest wants to go</param>
        /// <returns>The generated path in a List with DIRECTIONS</returns>
        public List<DIRECTIONS> Pathfinding(Node currentroom, HotelSegments.HSegment targetroom, Building.ID_List pathtype)
        {
            int ammountcheckedright = _startwaarde; //Used to save how many times the path has gone to the right in case we ever have to reverse the steps and remove them from the list
            int ammountleveled = _startwaarde; //Used to save how many times the path has gone up in case we ever have to reverse the steps and remove them from the list
            bool found = false; //Used to see if the path has been figured out
            bool checkedright = false; //Used to see if the right side of the current node has been checked
            bool checkedtop = false; //Used to see if every level on top of the starting room has been checked
            bool startlevel = true; //Used to see if the path is still on its startinglevel
            Node PathRoom = currentroom; //Used to hop the path generator to the next node
            Node ElevatorOrStaircase = currentroom; //Used to save the elevator- or staircaseNode(the first it has crossed) on the startlevel
            List<DIRECTIONS> path = new List<DIRECTIONS>(); //Used to save the path before returning it to the globl List
            while (found == false) //This will keep looping through until the path has complete
            {
                if (checkedright == true) //If all the rooms on the right of the current level has been checked /PathRoom.MySegment.ID == (int)pathtype
                {
                    if (PathRoom.MySegment is HotelSegments.Elevator || PathRoom.MySegment is HotelSegments.Staircase) //If the current room is an elevator or staircaise 
                    {
                        if (startlevel == true) //If this is the first elevator or staircase of the path then make set the ElevatorOrStaircase to this node
                        {
                            startlevel = false;
                            ElevatorOrStaircase = PathRoom;
                        }

                        if (checkedtop == false && PathRoom.TopNode != null) //If not all the levels above have been checked and there is a level above then go up
                        {
                            path.Add(DIRECTIONS.TOP);
                            PathRoom = PathRoom.TopNode;
                            ElevatorOrStaircase = PathRoom;
                            ammountleveled++;
                            checkedright = false; //Since we went up a level, the right has to be checked again for this level
                        }

                        else if (checkedtop == true && PathRoom.BottomNode != null) //If all the levels above have been checked and there is a level beneath then go down
                        {
                            path.Add(DIRECTIONS.BOTTOM);
                            PathRoom = PathRoom.BottomNode;
                            ElevatorOrStaircase = PathRoom;
                            ammountleveled++;
                            checkedright = false; //Since we went down a level, the right has to be checked again for this level
                        }

                        else if(checkedtop == false && PathRoom.TopNode == null) //If the TopNode is null then this means we are at the top level and can't go any higher
                        {
                            checkedtop = true; //Start going to check beneath the original level
                            for (int ammountremoved = 0; ammountremoved < ammountleveled; ammountremoved++) //We will delete all DIRECTIONS in the List.path until we return to the original ElevatorOrStaircase Node 
                            {
                                ElevatorOrStaircase = ElevatorOrStaircase.BottomNode;
                                path.Remove(path.LastOrDefault());
                            }
                            ammountleveled = _startwaarde;
                            PathRoom = ElevatorOrStaircase;
                        }
                        else if (checkedtop == true && PathRoom.BottomNode == null)
                        {
                            if (PathRoom.LeftNode.MySegment.ID == targetroom.ID)
                            {
                                path.Add(DIRECTIONS.LEFT);
                                found = true;
                            }
                        }

                        else //If everything is false then the target room is not present in the current hotel layout
                        {
                            Console.WriteLine("Room does not exist in the current hotel layout");
                            path.Clear();
                            break;
                        }
                    }

                    else if (PathRoom.LeftNode.MySegment != null) //If there is a room on the left
                    {
                        if (PathRoom.LeftNode.MySegment.ID != targetroom.ID) //If the room on the left is not the target room then move to the left
                        {
                            path.Add(DIRECTIONS.LEFT);
                            PathRoom = PathRoom.LeftNode;
                        }

                        else //If the previous if is false then the room has been found on the left. This will add one more DIRECTIONS to the left so the guests will arrive at his room
                        {
                            path.Add(DIRECTIONS.LEFT);
                            found = true;
                            break;
                        }
                    }

                    else if (PathRoom.LeftNode.MySegment == null) //If there is a room on the left but it has not gotten a segment(sides of a room or hallways) then move one more to the left
                    {
                        path.Add(DIRECTIONS.LEFT);
                        PathRoom = PathRoom.LeftNode;

                    }
                }

                else if (checkedright == false) //If the rooms on the right of the current level haven't been checked
                {
                    if (PathRoom.RightNode != null) //If there is a hotel part on the right
                    {
                        if (PathRoom.RightNode.MySegment != null) //If there is a room on the right
                        {
                            if (PathRoom.RightNode.MySegment.ID != targetroom.ID) //If the room on the right is not the target room then move to the right
                            {
                                path.Add(DIRECTIONS.RIGHT);
                                PathRoom = PathRoom.RightNode;
                                ammountcheckedright++;
                            }
                            else //If the previous if is false then the room has been found on the left. This will add one more DIRECTIONS to the left so the guests will arrive at his room
                            {
                                path.Add(DIRECTIONS.RIGHT);
                                found = true;
                                break;
                            }
                        }
                        else if (PathRoom.RightNode.MySegment == null) //If there is a room on the left but it has not gotten a segment(sides of a room or hallways) then move one more to the left
                        {
                            path.Add(DIRECTIONS.RIGHT);
                            PathRoom = PathRoom.RightNode;
                            ammountcheckedright++;
                        }
                    }

                    else if (PathRoom.RightNode == null) //If there is no part of the hotel on the right then that means it is the end of the level
                    {
                        if (startlevel == true) //If this is the first level that has been checked then return to the original location of the guest
                        {
                            PathRoom = currentroom;
                        }

                        else //If this isn't the first level that has been checked then return to the ElevatorOrStaircase Node
                        {
                            PathRoom = ElevatorOrStaircase;
                        }

                        for (int ammountremoved = 0; ammountremoved < ammountcheckedright; ammountremoved++) //We will delete all DIRECTIONS in the List.path until we return to the original ElevatorOrStaircase Node 
                        {
                            path.Remove(path.LastOrDefault());
                        }

                        checkedright = true; //Start checking left or going up/down a level
                        ammountcheckedright = _startwaarde;
                    }
                }
            }
            return path;
        }
    }
}
