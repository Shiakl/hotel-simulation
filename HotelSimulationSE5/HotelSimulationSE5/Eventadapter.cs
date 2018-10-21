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

        /// <summary>
        /// Return an Entity class object based on the specific ID and Type given from the hotel entity list.
        /// </summary>
        /// <param name="id">Unique ID of a guest or maid</param>
        /// <param name="eType">Guest or Maid</param>
        /// <returns></returns>
        private Entity Select_Entity(int id, Entity.ENTITY_TYPE eType)
        {
            List<Entity> guests = (from entity in _myHotel.entityList
                                   where (entity.ID == id && entity.EType == eType)
                                   select entity).ToList();

                return guests.FirstOrDefault();
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
                    int guestID = Convert.ToInt32(item.Data.First().Value);
                    check_out_list.Add(Select_Entity(guestID,Entity.ENTITY_TYPE.GUEST));

                    foreach (Entity leaver in check_out_list)
                    {
                        leaver.Path = leaver.MyNode.Pathfinding(leaver.MyNode, _myHotel.Reception.MySegment, Building.ID_List.Elevator);
                        leaver.MyRoom.Reserved_room();
                        leaver.MyNode.ColorMe();
                        leaver.Destination_reached();
                    }
                    check_out_list.Clear();
                    break;
                case HotelEventType.CLEANING_EMERGENCY:
                    List<int> event_values = new List<int>();
                    foreach (var value in item.Data)
                    {
                        event_values.Add(Convert.ToInt32(value.Value));
                    }
                    _myHotel.Call_Maid(_myHotel.Reception, event_values.First(), event_values.Last());
                    break;
                case HotelEventType.EVACUATE:
                    foreach (Entity leaver in _myHotel.entityList)
                    {
                        if (leaver.MyNode.MySegment.ID != (int)Building.ID_List.Reception)
                        {
                            leaver.Path = leaver.MyNode.Pathfinding(leaver.MyNode, _myHotel.Reception.MySegment, Building.ID_List.Elevator);
                            leaver.Destination_reached();
                        }
                    }
                    Console.WriteLine("Everyone is evacuating");
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
                    Console.WriteLine("Movie started");
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
