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
        public int _refreshrateinterval = 500; 
        private Timer _refresh_timer= new Timer();
        bool started = false;
        Eventadapter events = new Eventadapter();        

        private Building _myHotel;
        public MainForm()
        {
            InitializeComponent();
            GenerateHotel();
            GuestButton.Top = _myHotel.max_y * _myHotel.segmentSize_Y + _myHotel.segmentSize_Y;
            StopButton.Top = _myHotel.max_y * _myHotel.segmentSize_Y + _myHotel.segmentSize_Y;
            EventButton.Top = _myHotel.max_y * _myHotel.segmentSize_Y + _myHotel.segmentSize_Y;

            _refresh_timer.Interval = _refreshrateinterval;
            _refresh_timer.Tick += _refresh_timer_Tick;
            _refresh_timer.Start();
            Console.WriteLine();

        }

        private void _refresh_timer_Tick(object sender, EventArgs e)
        {
            if (events.EventList.Count()>0)
            {
                foreach (var item in events.EventList)
                {
                    switch (item.EventType)
                    {
                        case HotelEventType.CHECK_IN:
                            _myHotel.Create_Guest(_myHotel.elevatorNodes[_myHotel.max_y-1]);
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
            this.Invalidate();
            _myHotel.Move_Guest(this);
            this.Refresh();
        }

        public void GenerateHotel()
        {
            this.Invalidate();
            _myHotel = new Building();
            _myHotel.CreateHotel(this);
            this.Refresh();
        }

        private void GuestButton_Click(object sender, EventArgs e)
        {
            this.Invalidate();
            _myHotel.Create_Guest(_myHotel.elevatorNodes[_myHotel.max_y-1]);
            this.Refresh();
            _refresh_timer.Start();
        }


        private void Stop_Click(object sender, EventArgs e)
        {
            _refresh_timer.Stop();
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
                events.Stop_Events();
                started = false;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}
