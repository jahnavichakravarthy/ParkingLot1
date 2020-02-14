using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot.Models
{
    public class Ticket
    {
        public string Id { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public int SlotNumber { get; set; }
        public string VehicleNumber{ get; set; }

    }
}
