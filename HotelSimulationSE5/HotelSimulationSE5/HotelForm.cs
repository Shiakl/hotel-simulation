using HotelEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSimulationSE5
{
    public partial class HotelForm : Form
    {
        public int _refreshrateinterval = 500;
        private Timer _refresh_timer = new Timer();
        bool started = false;

        private Building _myHotel;

        public HotelForm()
        {
            InitializeComponent();
            GenerateHotel();

            GuestButton.Top = _myHotel.maxYcoordinate * _myHotel.segmentSizeY + _myHotel.segmentSizeY;
            PauseButton.Top = _myHotel.maxYcoordinate * _myHotel.segmentSizeY + _myHotel.segmentSizeY;
            EventButton.Top = _myHotel.maxYcoordinate * _myHotel.segmentSizeY + _myHotel.segmentSizeY;
            PathButton.Top = _myHotel.maxYcoordinate * _myHotel.segmentSizeY + _myHotel.segmentSizeY;

            _refresh_timer.Interval = _refreshrateinterval;
            _refresh_timer.Tick += _refresh_timer_Tick;

            Console.WriteLine();
        }

        private void _refresh_timer_Tick(object sender, EventArgs e)
        {
            //Check for events
            _myHotel.Event_Handler();

            //Move guests
            _myHotel.Move_Guest(this);
        }

        public void GenerateHotel()
        {
            _myHotel = new Building();
            _myHotel.CreateHotel(this);
        }

        private void GuestButton_Click(object sender, EventArgs e)
        {
            _myHotel.Create_Guest(_myHotel.elevatorNodes[_myHotel.maxYcoordinate - 1]);
            _refresh_timer.Start();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            _refresh_timer.Stop();
        }

        private void EventButton_Click(object sender, EventArgs e)
        {
            if (started == false)
            {
                _myHotel.events.Register(_myHotel.events);
                _myHotel.events.Start_events();
                started = true;
            }
            else
            {
                _myHotel.events.Stop_Events();
                started = false;
            }
        }

        private void PathButton_Click(object sender, EventArgs e)
        {
            _myHotel.ReloadAvailableRooms();
            foreach (Guest arrival in _myHotel._guestList)
            {
                if (_myHotel.AvailableRooms.Count() > 0)
                {
                    arrival.MyRoom = _myHotel.AssignRoom(_myHotel.AvailableRooms[0].ID);
                    arrival.MyRoom.Reserved = true;
                    arrival.Path = arrival.MyNode.Pathfinding(arrival.MyNode, arrival.MyRoom);
                }
            }
        }
    }
}
