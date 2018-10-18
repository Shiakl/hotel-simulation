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
        public List<HotelEvents.HotelEvent> EventList { get; set; }

        public Eventadapter()
        {
            HotelEventManager.HTE_Factor = 0.5f;
            EventList = new List<HotelEvent>();
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
            Console.WriteLine(evt.EventType);
            if (evt.Data != null)
            {
                foreach (var item in evt.Data)
                {
                    Console.WriteLine("Key : "+item.Key);
                    Console.WriteLine("Value : " + item.Value);
                }
            }
            EventList.Add(evt);
        }
    }
}
