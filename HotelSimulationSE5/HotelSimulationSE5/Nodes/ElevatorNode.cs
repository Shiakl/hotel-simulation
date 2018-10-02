using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5.Nodes
{
    class ElevatorNode : INode
    {
        public int Capacity { get; set; }
        public int CordX { get; set; }
        public int CordY { get; set; }

        public ElevatorNode()
        {

        }
    }
}
