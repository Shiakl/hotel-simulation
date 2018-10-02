using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace HotelSimulationSE5
{
    class Building
    {
        private string layoutstring;
        private List<TempRoom> temp;

        public Building()
        {
            temp = new List<TempRoom>();
            layoutstring = System.IO.File.ReadAllText(@"..\..\External\Hotel2_reparatieVanVersie1.layout");
            Read_Layout(temp);      

            Console.WriteLine("Checkpoint: 1");
        }

        public void Read_Layout(List<TempRoom> destination)
        {
            destination = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TempRoom>>(layoutstring);
        }

        public void CreateHotel(Form mainform)
        {
            //Test Factory
            Factories.RoomFactory rFac = new Factories.RoomFactory();
            Rooms.IRoom myRoom = rFac.Create("Room") as Rooms.IRoom;

        }


    }
}
