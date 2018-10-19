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
        private Building _myHotel;
        private float HTE_Value = 5f;

        public Eventadapter(Building hotel)
        {
            HotelEventManager.HTE_Factor = HTE_Value;
            EventList = new List<HotelEvent>();
            _myHotel = hotel;
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


        public void Event_Handler(HotelEvent item)
        {
            switch (item.EventType)
            {
                case HotelEventType.CHECK_IN:
                    _myHotel.Create_Guest(_myHotel.elevatorNodes.Last());
                    break;
                case HotelEventType.CHECK_OUT:

                    break;
                case HotelEventType.CLEANING_EMERGENCY:

                    break;
                case HotelEventType.EVACUATE:

                    break;
                case HotelEventType.GODZILLA:

                    break;
                case HotelEventType.GOTO_CINEMA:

                    break;
                case HotelEventType.GOTO_FITNESS:

                    break;
                case HotelEventType.NEED_FOOD:

                    break;
                case HotelEventType.NONE:

                    break;
                case HotelEventType.START_CINEMA:

                    break;
                default:
                    break;
            }
        }

        public void Notify(HotelEvents.HotelEvent evt)
        {
            Console.WriteLine("Event type is :" + evt.EventType);
            if (evt.Data != null)
            {
                foreach(var test in evt.Data)
                {
                Console.WriteLine("Key is: " + test.Key);
                Console.WriteLine("Value is: " + test.Value);
                }
            }
            EventList.Add(evt);
        }
    }
}
