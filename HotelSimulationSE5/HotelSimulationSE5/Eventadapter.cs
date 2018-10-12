using HotelEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotelSimulationSE5
{
    class Eventadapter
    {
        HotelEventListener listener;
        public Eventadapter()
        {
            
        }

        public void Start_events()
        {
            HotelEventManager.Start();
        }

        public void Stop_Events()
        {
            HotelEventManager.Stop();
        }

        public void Register()
        {
            HotelEventManager.Register(listener);
        }

        public void Deregister()
        {
            HotelEventManager.Deregister(listener);
        }
    }
}
