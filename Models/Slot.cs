using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot.Models
{
    public class Slot
    {
        public int Id { get; set; }
        public string SlotType { get; set; }
        //public enum TypesofSlots { TwoWheeler=1,FourWheeler,HeavyVehicle }
        public bool Availability { get; set; }
        public Vehicle ParkedVehicle { get; set; }
        public Slot(int number, string type)
        {
            Id = number;
            //Type = type;
            Availability = true;
        }

        public Slot() { Availability = true; }

    }
   
}
