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
        public int _refreshrateinterval;
        private Timer _refresh_timer= new Timer();
        bool started;
        Eventadapter events;
        int ybuttondistance;
        private Manager _manageHotel;

        public MainForm()
        {
            InitializeComponent();
            ybuttondistance = 20;
            _refreshrateinterval = 300;
        }

        /// <summary>
        /// At set intervals the EventList in EventAdapter is checked and the events in the list are sent to the Event_Handler method.
        /// Also moves units at these intervals.
        /// </summary>
        private void _refresh_timer_Tick(object sender, EventArgs e)
        {
            _manageHotel.Reception_Queue();

            if (events.EventList.Any())
            {
            int event_amount = events.EventList.Count();
                for (int counter = 0; counter <event_amount;counter++)
                {
                    events.Event_Handler(events.EventList.First());
                    events.EventList.Remove(events.EventList.FirstOrDefault());
                }
            }

            //Move guests
            _manageHotel.Move_Guest(this);
        }

        /// <summary>
        /// Method responsible for generating the hotel and registering the events.
        /// </summary>
        public void GenerateHotel()
        {
            _manageHotel = new Manager(this);
            events = new Eventadapter(_manageHotel);
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            events.Pause_Events();
            if (started == false)
            {
                StopButton.BackgroundImage = Image.FromFile(@"..\..\Images\Pause Button.png");
                _refresh_timer.Start();
                started = true;
            }

            else
            {
                StopButton.BackgroundImage = Image.FromFile(@"..\..\Images\Play Button.jpg");
                _refresh_timer.Stop();
                started = false;
            }
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            started = true;
            BackgroundImage = null;
            StartButton.Visible = false;
            GenerateHotel();
            StopButton.Visible = true;
            ExitButton.Visible = true;
            SpeedUP.Visible = true;
            SpeedDOWN.Visible = true;
            StopButton.Top = _manageHotel.CheckMaxY() * _manageHotel.CheckSegY() + ybuttondistance;
            ExitButton.Top = _manageHotel.CheckMaxY() * _manageHotel.CheckSegY() + ybuttondistance;
            SpeedDOWN.Top = _manageHotel.CheckMaxY() * _manageHotel.CheckSegY() + ybuttondistance;
            SpeedUP.Top = _manageHotel.CheckMaxY() * _manageHotel.CheckSegY() + ybuttondistance;
            _refresh_timer.Interval = _refreshrateinterval;
            _refresh_timer.Tick += _refresh_timer_Tick;
            _refresh_timer.Start();
            events.Register(events);
            events.Start_events();         
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            events.Stop_Events();
            this.Close();
        }

        private void SpeedUP_Click(object sender, EventArgs e)
        {
            if (events.HTE_Value < 10f)
            {
                events.HTE_Value = events.HTE_Value + 1f;
            }

            else
            {
                MessageBox.Show("WE CAN'T GO FASTER!");
            }
        }

        private void SpeedDOWN_Click(object sender, EventArgs e)
        {
            if (events.HTE_Value > 0f)
            {
                events.HTE_Value = events.HTE_Value - 1f;
            }

            else
            {
                MessageBox.Show("WE CAN'T GO SLOWER!");
            }
        }
    }
}
