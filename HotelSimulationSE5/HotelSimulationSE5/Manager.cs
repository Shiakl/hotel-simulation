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
    public class Manager
    {
        public string _layoutstring; //Hotel layout(blueprint)
        Building _bld = new Building();
        private int guest_id;
        private int guest_amount;
        private int maid_id;

        public Manager(Form mainform)
        {
            guest_amount = 0;
            guest_id = 0;
            _layoutstring = _readLayout();
            _bld.Creater(_layoutstring);
            _bld.BuildHotel(mainform);
        }

        #region Checkers
        public Node[] CheckRooms()
        {
            return _bld.Get_nodes;
        }

        public Node[] CheckElevators()
        {
            return _bld.Get_elevatorNodes;
        }

        public List<Entity> CheckGuests()
        {
            return _bld.Get_guestList;
        }

        public List<Entity> CheckMaids()
        {
            return _bld.Get_maidList;
        }

        public Reception CheckReception()
        {
            return _bld.Get_HotelReception;
        }

        public int CheckSegX()
        {
            return _bld.Get_segmentSizeX;
        }

        public int CheckSegY()
        {
            return _bld.Get_segmentSizeY;
        }

        public int CheckMaxX()
        {
            return _bld.Get_maxXcoordinate;
        }

        public int CheckMaxY()
        {
            return _bld.Get_maxYcoordinate;
        }
        #endregion

        private string _readLayout()
        {
           return System.IO.File.ReadAllText(@"..\..\External\Hotel3.layout"); //Reads the layout file and pushes it into a string       
        }

        public void Move_Guest(Form mainform)
        {
            int elcap = CheckElevators().FirstOrDefault().MySegment.Capacity;

            foreach (Entity currentG in CheckGuests())
            {
                currentG.MoveToNode();
            }

            foreach (Entity currentM in CheckMaids())
            {
                currentM.MoveToNode();
            }

        }
        #region Building
        public void Reception_Queue()
        {
            CheckReception().Assigned_Room(CheckGuests());
        }

        /// <summary>
        /// Create a guest on specified node.
        /// </summary>
        /// <param name="currentNode"> Spawn node of the guest</param>
        public void Create_Guest(Node currentNode, int classification_num)
        {
            Entity arrival;
            _bld.ReloadAvailableRooms(classification_num);
            if (_bld.AvailableRooms.Any())
            {
                guest_amount++;
                guest_id = guest_amount;
                arrival = new Entity(currentNode, guest_id, _bld.AvailableRooms.FirstOrDefault());
                arrival.Path = arrival.MyNode.Pathfinding(arrival.MyNode, arrival.MyRoom, Building.ID_List.Elevator);
                CheckReception().WaitList.Enqueue(arrival);
            }
        }

        public void Call_Maid(Node currentNode, int targetRoom, float hte)
        {
            maid_id = CheckMaids().Count() + 1;
            Entity maid = new Entity(currentNode, maid_id, _bld.AssignRoom(targetRoom), Entity.ENTITY_TYPE.MAID);
            maid.Path = maid.MyNode.Pathfinding(maid.MyNode, maid.MyRoom, Building.ID_List.Elevator);
            CheckMaids().Add(maid);
        }
        #endregion
    }
}
