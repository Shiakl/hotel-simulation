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
        public int _refreshrateinterval = 400; 
        private Timer _refresh_timer= new Timer();
        bool started = false;
        Eventadapter events;   

        private Building _myHotel;
        public MainForm()
        {
            InitializeComponent();
            GenerateHotel();
            GuestButton.Top = _myHotel.maxYcoordinate * _myHotel.segmentSizeY + _myHotel.segmentSizeY;
            StopButton.Top = _myHotel.maxYcoordinate * _myHotel.segmentSizeY + _myHotel.segmentSizeY;
            EventButton.Top = _myHotel.maxYcoordinate * _myHotel.segmentSizeY + _myHotel.segmentSizeY;

            _refresh_timer.Interval = _refreshrateinterval;
            _refresh_timer.Tick += _refresh_timer_Tick;
            _refresh_timer.Start();
            Console.WriteLine();

        }

        /// <summary>
        /// At set intervals the EventList in EventAdapter is checked and the events in the list are sent to the Event_Handler method.
        /// Also moves units at these intervals.
        /// </summary>
        private void _refresh_timer_Tick(object sender, EventArgs e)
        {
            if (events.EventList.Count()>0)
            {
            int event_amount = events.EventList.Count();
                for (int counter = 0; counter <event_amount;counter++)
                {
                    events.Event_Handler(events.EventList.First());
                    events.EventList.Remove(events.EventList.FirstOrDefault());
                }
            }

            //Move guests
            _myHotel.Move_Guest(this);
        }

        /// <summary>
        /// Method responsible for generating the hotel and registering the events.
        /// </summary>
        public void GenerateHotel()
        {
            _myHotel = new Building();
            _myHotel.CreateHotel(this);
            events = new Eventadapter(_myHotel);
        }

        private void GuestButton_Click(object sender, EventArgs e)
        {
            _myHotel.Create_Guest(_myHotel.Reception, 2);
            _myHotel.Call_Maid(_myHotel.Reception, 21,5);
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
