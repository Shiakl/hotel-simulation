﻿using HotelEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using HotelSimulationSE5.HotelSegments;

namespace HotelSimulationSE5
{
    public class Eventadapter : HotelEventListener
    {
        public List<HotelEvent> EventList { get; set; } //Lists of queue'd HotelEvents
        private Building _myHotel;  //Hotel the events are sent to
        private float HTE_Value = 5f; //Speed at which the events are generated.

        public Eventadapter(Building hotel)
        {
            HotelEventManager.HTE_Factor = HTE_Value;
            EventList = new List<HotelEvent>();
            _myHotel = hotel;
        }

        /// <summary>
        /// Adaptor Method that calls HotelEvent.Manager.Start();
        /// </summary>
        public void Start_events()
        {
            HotelEventManager.Start();
        }

        /// <summary>
        /// Adaptor Method that calls HotelEvent.Manager.Stop();
        /// </summary>
        public void Stop_Events()
        {
            HotelEventManager.Stop();
        }

        /// <summary>
        /// Adaptor Method that calls HotelEvent.Manager.Pause();
        /// </summary>
        public void Pause_Events()
        {
            HotelEventManager.Pauze();
        }

        /// <summary>
        /// Adaptor Method that calls HotelEvent.Manager.Register();, Registers a HotelEventListener class to the HotelEventManager.
        /// </summary>
        /// <param name="newGuest">HotelEventListener class</param>
        public void Register(Eventadapter newGuest)
        {
            HotelEventManager.Register(newGuest);           
        }

        /// <summary>
        /// Deregisters a HotelEventListener
        /// </summary>
        /// <param name="newGuest">HotelEventListener class</param>
        public void Deregister(Eventadapter newGuest)
        {
            HotelEventManager.Deregister(newGuest);
        }

        private enum FACILITIES
        {
            RESTAURANT,
            FITNESS,
            CINEMA
        }

        /// <summary>
        /// Return a list of all selected facilities present in the hotel.
        /// </summary>
        /// <param name="facility">Facility type</param>
        /// <returns></returns>
        private List<HSegment> Select_Segment(FACILITIES facility)
        {
            List<HSegment> segments;
            switch (facility)
            {
                case FACILITIES.CINEMA:
                    segments = (from entity in _myHotel._nodes
                                where (entity.MySegment is Cinema)
                                select entity.MySegment).ToList();
                    return segments;

                case FACILITIES.FITNESS:
                    segments = (from entity in _myHotel._nodes
                                where (entity.MySegment is Fitness)
                                select entity.MySegment).ToList();
                    return segments;
                case FACILITIES.RESTAURANT:
                    segments = (from entity in _myHotel._nodes
                                where (entity.MySegment is Restaurant)
                                select entity.MySegment).ToList();
                    return segments;
                default:
                    segments = new List<HSegment>();
                    return segments;
            }
        }

        /// <summary>
        /// Sets the path to the nearest facility on the given entities.
        /// </summary>
        /// <param name="unit">Moving entity</param>
        /// <param name="facility">Target Facility</param>
        private void Find_Facility(Entity unit, FACILITIES facility)
        {
            List<Node.DIRECTIONS> route = new List<Node.DIRECTIONS>();
            List<HSegment> found_segs = Select_Segment(facility);
            foreach (HSegment item in found_segs)
            {
                List<Node.DIRECTIONS> temp_route = unit.MyNode.Pathfinding(unit.MyNode, item, Building.ID_List.Elevator);

                if (route.Count() == 0)
                {
                    route = temp_route;
                }
                else if (temp_route.Count() < route.Count())
                {
                    route = temp_route;
                }
            }
            unit.Path = route;
            unit.Destination_reached();
        }

        /// <summary>
        /// Return an Entity class object based on the specific ID and Type given from the hotel entity list.
        /// </summary>
        /// <param name="id">Unique ID of a guest or maid</param>
        /// <param name="eType">Guest or Maid</param>
        /// <returns></returns>
        private Entity Select_Entity(int id, Entity.ENTITY_TYPE eType)
        {
            List<Entity> guests = (from entity in _myHotel.guestList
                                   where (entity.ID == id && entity.EType == eType)
                                   select entity).ToList();

                return guests.FirstOrDefault();
        }

        /// <summary>
        /// Method that processes all the HotelEvents.
        /// </summary>
        /// <param name="item">Provided hotelevent</param>
        public void Event_Handler(HotelEvent item)
        {
            List<Entity> Moving_Enity_List = new List<Entity>();
            int guestID;
            switch (item.EventType)
            {
                case HotelEventType.CHECK_IN:
                    List<char> data_value = new List<char>();
                    foreach (var value in item.Data)
                    {
                        data_value = value.Value.SkipWhile(c => !Char.IsDigit(c))
                        .TakeWhile(Char.IsDigit)
                        .ToList();
                    }
                    int classification = Convert.ToInt32(data_value.FirstOrDefault()) - '0';
                    _myHotel.Create_Guest(_myHotel.elevatorNodes.Last(), classification);
                    break;
                case HotelEventType.CHECK_OUT:
                    guestID = Convert.ToInt32(item.Data.First().Value);
                    Moving_Enity_List.Add(Select_Entity(guestID,Entity.ENTITY_TYPE.GUEST));

                    foreach (Entity leaver in Moving_Enity_List)
                    {
                        leaver.Path = leaver.MyNode.Pathfinding(leaver.MyNode, _myHotel.elevatorNodes.Last().MySegment, Building.ID_List.Elevator);
                        leaver.MyRoom.Reserved_room();
                        leaver.MyNode.ColorMe();
                        leaver.Destination_reached();
                    }
                    Moving_Enity_List.Clear();
                    break;
                case HotelEventType.CLEANING_EMERGENCY:
                    List<int> event_values = new List<int>();
                    foreach (var value in item.Data)
                    {
                        event_values.Add(Convert.ToInt32(value.Value));
                    }
                    _myHotel.Call_Maid(_myHotel.elevatorNodes.Last(), event_values.First(), event_values.Last());
                    break;
                case HotelEventType.EVACUATE:
                    foreach (Entity leaver in _myHotel.guestList)
                    {
                        if (leaver.MyNode.MySegment.ID != (int)Building.ID_List.Reception)
                        {
                            leaver.Path = leaver.MyNode.Pathfinding(leaver.MyNode, _myHotel.elevatorNodes.Last().MySegment, Building.ID_List.Elevator);
                            leaver.Destination_reached();
                        }
                    }
                    foreach (Entity leaver in _myHotel.maidList)
                    {
                        if (leaver.MyNode.MySegment.ID != (int)Building.ID_List.Reception)
                        {
                            leaver.Path = leaver.MyNode.Pathfinding(leaver.MyNode, _myHotel.elevatorNodes.Last().MySegment, Building.ID_List.Elevator);
                            leaver.Destination_reached();
                        }
                    }
                    Console.WriteLine("Everyone is evacuating");
                    break;
                case HotelEventType.GODZILLA:
                    Console.WriteLine("Godzilla has entered the building!");
                    break;
                case HotelEventType.GOTO_CINEMA:
                    guestID = Convert.ToInt32(item.Data.First().Value);
                    Moving_Enity_List.Add(Select_Entity(guestID, Entity.ENTITY_TYPE.GUEST));

                    foreach (Entity unit in Moving_Enity_List)
                    {
                        Find_Facility(unit,FACILITIES.CINEMA);
                    }
                    Moving_Enity_List.Clear();
                    break;
                case HotelEventType.GOTO_FITNESS:
                    guestID = Convert.ToInt32(item.Data.First().Value);
                    Moving_Enity_List.Add(Select_Entity(guestID, Entity.ENTITY_TYPE.GUEST));

                    foreach (Entity unit in Moving_Enity_List)
                    {
                        Find_Facility(unit, FACILITIES.FITNESS);
                    }
                    Moving_Enity_List.Clear();
                    break;
                case HotelEventType.NEED_FOOD:
                    guestID = Convert.ToInt32(item.Data.First().Value);
                    Moving_Enity_List.Add(Select_Entity(guestID, Entity.ENTITY_TYPE.GUEST));

                    foreach (Entity unit in Moving_Enity_List)
                    {
                        Find_Facility(unit, FACILITIES.RESTAURANT);
                    }
                    Moving_Enity_List.Clear();
                    break;
                case HotelEventType.NONE:
                    break;
                case HotelEventType.START_CINEMA:
                    foreach (var data in item.Data)
                    {
                        Console.WriteLine("Movie started in Cinema with ID : " + data.Value);                        
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Adds a Hotelevent to the EventList. Method called by the HotelEventManager.
        /// </summary>
        /// <param name="evt">The generated Hotelevent</param>
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
