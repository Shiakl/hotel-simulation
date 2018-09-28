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
        public MainForm()
        {
            InitializeComponent();

            string layoutstring = System.IO.File.ReadAllText(@"C:\Users\Sang\Source\Repos\hotel-simulation\HotelSimulationSE5\HotelSimulationSE5\External\Hotel.layout");
            List<Room> temp = new List<Room>();
            temp = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Room>>(layoutstring);

            Console.WriteLine(temp[0]);

        }
    }
}
