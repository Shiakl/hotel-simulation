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

            string layoutstring = System.IO.File.ReadAllText(@"..\..\External\Hotel2_reparatieVanVersie1.layout");
            List<TempRoom> temp = new List<TempRoom>();
            temp = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TempRoom>>(layoutstring);


            

            Console.WriteLine("Checkpoint: 1");

        }
    }
}
