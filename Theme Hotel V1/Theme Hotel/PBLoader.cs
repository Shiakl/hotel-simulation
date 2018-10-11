using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme_Hotel
{
    class PBLoader
    {
        string[] strcords;
        string[] strdims;
        private List<PBLoader> jsonoutput = new List<PBLoader>();      
        public string AreaType { get; set; }
        public string Position
        {
            set
            {
                strcords = value.Split(',');
                this.CordX = Int32.Parse(strcords[0]);
                this.CordY = Int32.Parse(strcords[1]);
            }
        }
        public string Dimension
        {
            set
            {
                strdims = value.Split(',');
                this.DimX = Int32.Parse(strdims[0]);
                this.DimY = Int32.Parse(strdims[1]);
            }
        }
        public string Capacity { get; set; }
        public string Classification { get; set; }
        public int CordX { get; set; }
        public int CordY { get; set; }
        public int DimX { get; set; }
        public int DimY { get; set; }
        public bool Free { get; set; } = true;

        public List<PBLoader> Getjsonoutput
        {
            get { return jsonoutput; }
        }

        public PBLoader()
        {

        }

        public void JsonLoader()
        {
            string layoutcheck = File.ReadAllText(@"C:\Hotel.layout");
            jsonoutput = JsonConvert.DeserializeObject<List<PBLoader>>(layoutcheck);
        }
    }
}
