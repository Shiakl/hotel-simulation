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

        private List<char> data_value;
        public void Event_Handler(HotelEvent item)
        {
            switch (item.EventType)
            {
                case HotelEventType.CHECK_IN:
                    foreach (var value in item.Data)
                    {
                        data_value = value.Value.SkipWhile(c => !Char.IsDigit(c))
                        .TakeWhile(Char.IsDigit)
                        .ToList();
                    }
                    int classification = Convert.ToInt32(data_value.FirstOrDefault()) - 48;
                    _myHotel.Create_Guest(_myHotel.Reception, classification);
                    break;
                case HotelEventType.CHECK_OUT:
                    List<Entity> check_out_list = new List<Entity>();
                    foreach (var value in item.Data)
                    {
                       List<Entity> guests = (from entity in _myHotel._guestList
                                       where (entity.ID == Convert.ToInt32(value.Value))
                                       select entity).ToList();
                        if (guests.Any())
                        {
                        check_out_list.Add(guests.FirstOrDefault());
                        }
                    }
                    foreach (Entity leaver in check_out_list)
                    {
                        leaver.MyNode.Pathfinding(leaver.MyNode, _myHotel.Reception.MySegment, Building.ID_List.Elevator);
                        leaver.Moving = true;
                    }

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
            Console.WriteLine("");
            EventList.Add(evt);
        }
    }
}
