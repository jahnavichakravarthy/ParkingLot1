using System;
using ParkingLot.Models;

namespace ParkingLot.Services
{
    public class TicketService
    {
        public string GenerateID()
        {
            string Id;
            Id = "TKT" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;//
            return Id;
        }
        public Ticket GenerateTicket(int slotId,string vehiclenumber)
        {
            //generate a ticket
            Ticket ticket = new Ticket(slotId, vehiclenumber)
            {
                Id = GenerateID(),
                InTime = DateTime.Now
            };
            return ticket;
        }
    }   
}
