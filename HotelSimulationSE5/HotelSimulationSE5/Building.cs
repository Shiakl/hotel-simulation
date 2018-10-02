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

        public enum NODE_TYPES
        {
            Hallway,
            Elevator
        }

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
            Factories.HSegmentFactory rFac = new Factories.HSegmentFactory();
            HotelSegments.IHSegment myRoom = rFac.Create("Room") as HotelSegments.IHSegment;

            /*
             int Count = 0;
        string[] strcords;
        string[] strdims;

        public string Position
        {
            set
            {
                strcords = value.Split(',');

                if (Count == 0)
                {
                    this.CordX = Int32.Parse(strcords[0]);
                    Count = 1;
                }

                if (Count == 1)
                {
                    this.CordY = Int32.Parse(strcords[1]);
                    Count = 0;
                }
            }
        }
             */
        }


    }
}
