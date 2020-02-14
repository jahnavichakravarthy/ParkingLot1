using System;
using System.Collections.Generic;
using System.Text;
using ParkingLot.Models;

namespace ParkingLot.Services
{
    public class TicketServices
    {
        public string GenerateID()
        {
            string Id;
            Id = "TXT" + DateTime.Now.Hour + DateTime.Now.Hour + DateTime.Now.Second;
            return Id;
        }
        public Ticket GenerateTicket(int slotId,string vehiclenumber)
        {
            Ticket ticket = new Ticket();
            ticket.Id = GenerateID();
            ticket.SlotNumber = slotId;
            ticket.VehicleNumber = vehiclenumber;
            ticket.InTime = DateTime.Now;
            return ticket;

        }
    }
}
