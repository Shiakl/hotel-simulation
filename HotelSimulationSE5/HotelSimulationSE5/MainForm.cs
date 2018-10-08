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
        public int _refreshrateinterval = 250; 
        private Timer _refresh_timer= new Timer(); 


        private Building _myHotel;
        public MainForm()
        {
            InitializeComponent();
            GenerateHotel();
            GuestButton.Top = _myHotel.max_y * _myHotel.segmentSize_Y + _myHotel.segmentSize_Y;
            StopButton.Top = _myHotel.max_y * _myHotel.segmentSize_Y + _myHotel.segmentSize_Y;

            _refresh_timer.Interval = _refreshrateinterval;
            _refresh_timer.Tick += _refresh_timer_Tick;
        }

        private void _refresh_timer_Tick(object sender, EventArgs e)
        {
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
            _myHotel.Create_Guest(this);
            this.Refresh();
            _refresh_timer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _myHotel.BreakPoint();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            _refresh_timer.Stop();
        }
    }
}
