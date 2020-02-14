using System;
using System.Collections.Generic;
using System.Text;
using ParkingLot.Models;

namespace ParkingLot.Services
{
    public class ParkingSevices
    {
        public enum Type { TwoWheeler = 1, FourWheeler, HeavyVehicle }
        public  List<Slot> Slots = new List<Slot>();
        public List<Slot> Availability(string type)
        {
            List<Slot> AvailableSlots = new List<Slot>();
            foreach (Slot slot in Slots)
            {
                if ((slot.SlotType == type)&& (slot.Availability == true))
                {
                     AvailableSlots.Add(slot); 
                }
            }
            return (AvailableSlots.Count == 0) ? null : AvailableSlots;
           // return AvailableSlots;
        }
        public void CreateSlots(int twoWheeler,int fourWheeler,int heavyVehicles)
        {
            int Count = 1;

            do
            { 
                Slot slot = new Slot();
                Slots.Add(slot);
                if (Count <= twoWheeler)
                {
                    slot.Id = Count;
                    slot.SlotType = Convert.ToString(Type.TwoWheeler);
                    Count++;
                }
                else if (Count > twoWheeler && Count <= (twoWheeler + fourWheeler))
                {
                    slot.Id = Count;
                    slot.SlotType = Convert.ToString(Type.FourWheeler);
                    Count++;
                }
                else if((Count > twoWheeler+fourWheeler) && Count <= (twoWheeler + fourWheeler+heavyVehicles))
                {
                    slot.Id = Count;
                    slot.SlotType = Convert.ToString(Type.HeavyVehicle);
                    Count++;
                }
                else
                {
                    break;
                }

            }
            while (true);
                  

        }

        public void Park(int number, string name)
        {
            Ticket ticket = new Ticket();
            Slot ThisSlot = Slots.Find(slot => slot.Id == number);
            //ThisSlot.ParkedVehicle = new Vehicle();
            ticket.InTime = DateTime.Now;
        }
        public void UnPark(int number)
        {
            Slot ThisSlot = Slots.Find(slot => slot.Id == number);
            ThisSlot.ParkedVehicle = null;
            ThisSlot.Availability = true;
          
        }
        public Slot FindSlot(int Number)
        {
            Slot SelectedSlot = Slots.Find(slot => slot.Id == Number);
                return SelectedSlot;
        }

    }
}