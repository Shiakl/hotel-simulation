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
