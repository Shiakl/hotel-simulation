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
        public int ID { get; set; }
        public GuestRoom MyRoom { get; set; }        
        public Image MyImage { get; set; }
        public Panel MyPanel { get; set; }
        public List<Node.DIRECTIONS> Path { get; set; }
        public Node MyNode { get; set; }
        public bool Moving { get; set; }
        private PictureBox panelPb;

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

            switch (etype)
            {
                case ENTITY_TYPE.GUEST:
                    MyImage = Image.FromFile(@"..\..\Images\TempGuest4.png");
                    break;
                case ENTITY_TYPE.MAID:
                    MyImage = Image.FromFile(@"..\..\Images\maid.png");
                    break;
                default:
                    break;
            }




            panelPb = new PictureBox();
            panelPb.Size = MyImage.Size;
            panelPb.BackgroundImageLayout = ImageLayout.None;
            panelPb.Parent = node.panelPb;
            panelPb.BackColor = Color.Transparent;
            node.panelPb.Controls.Add(panelPb);
        }


        public void Redraw()
        {
            panelPb.BackgroundImage = MyImage;
            panelPb.BringToFront();
        }

        public void Destination_reached()
        {
            if (MyNode.MySegment != null)
            {
                if (MyNode.MySegment.ID == MyRoom.ID)
                {
                    Moving = false;
                }
                else
                {
                    Moving = true;
                }
            }
            else
            {
                Moving = true;
            }
        }

        public void MoveToNode(Node next,Node current)
        {
            next.panelPb.Controls.Add(panelPb);
            current.panelPb.Controls.Remove(panelPb);
            MyNode = next;
            Path.Remove(Path.FirstOrDefault());
        }

    }
}
