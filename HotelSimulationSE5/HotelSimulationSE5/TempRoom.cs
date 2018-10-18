using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5
{
    class TempRoom
    {
        public string Classification { get; set; }
        public string AreaType { get; set; }
        public string Capacity { get; set; }

        private string[] strcords;
        private string[] strdims;
        public string Position
        {
            set
            {
                strcords = value.Split(',');
                this.PositionX = Int32.Parse(strcords[0]);
                this.PositionY = Int32.Parse(strcords[1]);
            }
        }
        public string Dimension
        {
            set
            {
                strdims = value.Split(',');
                this.DimensionX = Int32.Parse(strdims[0]);
                this.DimensionY = Int32.Parse(strdims[1]);
            }
        }

        public string ID
        {
            get { return ID; }
            set
            {

                SegID = Int32.Parse(value);
            }
        }

        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int DimensionX { get; set; }
        public int DimensionY { get; set; }
        public int SegID { get; set; }


    }
}
