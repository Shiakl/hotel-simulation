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

            _refresh_timer.Interval = _refreshrateinterval;
            _refresh_timer.Tick += _refresh_timer_Tick;
            _refresh_timer.Start();
        }

        private void _refresh_timer_Tick(object sender, EventArgs e)
        {
            _myHotel.Move_Guests();
        }

        public void GenerateHotel()
        {
            this.Invalidate();
            _myHotel = new Building();
            _myHotel.CreateHotel(this);
            this.Refresh();
        }
    }
}
