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
        private Building _myHotel;
        public MainForm()
        {
            InitializeComponent();
            GenerateHotel();


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
