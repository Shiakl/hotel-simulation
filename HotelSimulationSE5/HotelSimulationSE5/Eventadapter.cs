using HotelEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HotelSimulationSE5
{
    class Eventadapter : HotelEvents.HotelEventListener
    {
        List<HotelEventType> eventList;

        public Eventadapter()
        {
            HotelEventManager.HTE_Factor = 1f;
            eventList = new List<HotelEventType>();
        }

        public void Start_events()
        {
            HotelEventManager.Start();
        }

        public void Stop_Events()
        {
            HotelEventManager.Stop();
        }

        public void Register(Eventadapter newGuest)
        {
            HotelEventManager.Register(newGuest);           
        }

        public void Deregister(Eventadapter newGuest)
        {
            HotelEventManager.Deregister(newGuest);
        }

        public void Notify(HotelEvents.HotelEvent evt)
        {
            eventList.Add(evt.EventType);
            Console.WriteLine("Event type is :" + evt.EventType);
            if (evt.Data != null)
            {
                foreach(var test in evt.Data)
                {
                Console.WriteLine("Key is: " + test.Key);
                Console.WriteLine("Value is: " + test.Value);
                }
            }

            
        }


    }
}
