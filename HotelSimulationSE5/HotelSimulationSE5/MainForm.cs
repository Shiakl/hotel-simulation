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
    public partial class MainForm : Form
    {
        public int _refreshRateInterval = 500; 
        private Timer _refreshTimer = new Timer();
        private bool started = false;
        private Eventadapter events = new Eventadapter();        
        private Building _myHotel;

        public MainForm()
        {
            InitializeComponent();
            GenerateHotel();
            GuestButton.Top = _myHotel.maxYcoordinate * _myHotel.segmentSizeY + _myHotel.segmentSizeY;
            StopButton.Top = _myHotel.maxYcoordinate * _myHotel.segmentSizeY + _myHotel.segmentSizeY;
            EventButton.Top = _myHotel.maxYcoordinate * _myHotel.segmentSizeY + _myHotel.segmentSizeY;

            _refreshTimer.Interval = _refreshRateInterval;
            _refreshTimer.Tick += _refreshTimerTick;
            _refreshTimer.Start();
            Console.WriteLine();

        }

        private void _refreshTimerTick(object sender, EventArgs e)
        {
            if (events.EventList.Any())
            {
                foreach (var item in events.EventList)
                {
                    switch (item.EventType)
                    {
                        case HotelEventType.CHECK_IN:
                            _myHotel.CreateGuest(_myHotel.GetElevatorNodes[_myHotel.maxYcoordinate-1]);
                            break;
                        case HotelEventType.CHECK_OUT:

                            break;
                        case HotelEventType.CLEANING_EMERGENCY:

                            break;
                        case HotelEventType.EVACUATE:

                            break;
                        case HotelEventType.GODZILLA:

                            break;
                        case HotelEventType.GOTO_CINEMA:

                            break;
                        case HotelEventType.GOTO_FITNESS:

                            break;
                        case HotelEventType.NEED_FOOD:

                            break;
                        case HotelEventType.NONE:

                            break;
                        case HotelEventType.START_CINEMA:

                            break;
                        default:
                            break;
                    }

                }
                events.EventList.Clear();
            }


            //Move guests
            _myHotel.MoveGuest(this);
        }

        public void GenerateHotel()
        {
            _myHotel = new Building();
            _myHotel.CreateHotel(this);
        }

        private void GuestButton_Click(object sender, EventArgs e)
        {
            _myHotel.CreateGuest(_myHotel.GetElevatorNodes[_myHotel.maxYcoordinate-1]);
            _refreshTimer.Start();
        }


        private void Stop_Click(object sender, EventArgs e)
        {
            _refreshTimer.Stop();
        }


        private void EventButton_Click(object sender, EventArgs e)
        {
            if (started == false)
            {
                events.Register(events);
                events.Start_events();
                started = true;
            }
            else
            {
                events.StopEvents();
                started = false;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void button1_Click(object sender, EventArgs e)
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
