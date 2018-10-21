using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using HotelSimulationSE5.HotelSegments;

namespace HotelSimulationSE5
{
    public class Entity
    {
        public ENTITY_TYPE EType { get; set; }
        public int ID { get; set; }
        public GuestRoom MyRoom { get; set; }        
        public Image MyImage { get; set; }
        public Panel MyPanel { get; set; }
        public List<Node.DIRECTIONS> Path { get; set; }
        public Node MyNode { get; set; }
        public bool Moving { get; set; }
        private PictureBox panelPb;
        private float HTE { get; set; }
        public bool Checked_Out { get; set; }


        public enum ENTITY_TYPE
        {
            GUEST,
            MAID
        }

        public Entity(Node node,int id, GuestRoom room,ENTITY_TYPE etype = ENTITY_TYPE.GUEST)
        {
            MyRoom = room;
            MyNode = node;
            ID = id;
            EType = etype;
            Moving = false; //An entity only moves after it is given a destination.
            Checked_Out = false; //An entity only checks out when when the event is triggered or during evacuation.

            //If the Entity type is a "MAID" it is given the image of a maid, otherwise the default image is that of a guest and the room the guest is assigned to is set to reserved.
            switch (etype)
            {
                case ENTITY_TYPE.MAID:
                    MyImage = Image.FromFile(@"..\..\Images\maid.png");
                    break;
                default:
                    MyImage = Image.FromFile(@"..\..\Images\TempGuest4.png");
                    MyRoom.Reserved_room();
                    break;
            }

            //New picturebox created for the entity to be drawn on.
            panelPb = new PictureBox
            {
                Size = MyImage.Size,
                BackgroundImageLayout = ImageLayout.None,
                Parent = node.panelPb,
                BackColor = Color.Transparent
            };
            node.panelPb.Controls.Add(panelPb);
            panelPb.BackgroundImage = MyImage;
            panelPb.BringToFront();
            Redraw();
        }

        /// <summary>
        /// After an entity moves it needs to be redrawn by the form.
        /// </summary>
        public void Redraw()
        {
            panelPb.Refresh();
        }

        /// <summary>
        /// Check if the entity has anywhere it has to go and set the moving property to true if it does.
        /// If the entity has reached it's destination hide the entity if the entity type is 'GUEST'.
        /// </summary>
        public void Destination_reached()
        {
            if (Path.Any())
            {
                Moving = true;
                panelPb.Visible = true;
            }
            else
            {
                Path.Clear();
                Moving = false;
                if ( EType == ENTITY_TYPE.GUEST || MyNode.MySegment.ID == (int)Building.ID_List.Reception)
                {
                    MyNode.ColorMe();
                    panelPb.Visible = false;
                }
            }            
        }


        public void MoveToNode()
        {
            if (Moving == true && Path.Any())
            {
                MyNode.MyConnections[(int)Path.First()].panelPb.Controls.Add(panelPb);
                MyNode.panelPb.Controls.Remove(panelPb);
                MyNode = MyNode.MyConnections[(int)Path.First()];
                Path.Remove(Path.FirstOrDefault());
                Redraw();
            }
            else
            {
                Destination_reached();
            }
        }

    }
}
