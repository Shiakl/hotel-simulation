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
                this.Position_X = Int32.Parse(strcords[0]);
                this.Position_Y = Int32.Parse(strcords[1]);
            }
        }
        public string Dimension
        {
            set
            {
                strdims = value.Split(',');
                this.Dimension_X = Int32.Parse(strdims[0]);
                this.Dimension_Y = Int32.Parse(strdims[1]);
            }
        }

        public string ID
        {
            get { return ID; }
            set
            {

                Seg_ID = Int32.Parse(value);
            }
        }

        public int Position_X { get; set; }
        public int Position_Y { get; set; }
        public int Dimension_X { get; set; }
        public int Dimension_Y { get; set; }
        public int Seg_ID { get; set; }


    }
}
