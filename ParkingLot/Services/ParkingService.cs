using System;
using System.Collections.Generic;
using ParkingLot.Models;
using System.Linq;

namespace ParkingLot.Services
{
    interface IParkingService
    {
        List<Slot> GetAvailableSlots(VehicleModel type);
        void InitializeSlots(int twoWheeler, int fourWheeler, int heavyVehicles);
        Ticket Park(int slotId, string vehicleNumber);
        void UnPark(int number);
        List<Slot> GetSlots();
        Slot GetSlot(int Number);
       bool VehicleNumberAmbiguity(string newVehicleNo);
    }
    public class ParkingSevice : IParkingService
    {
        public List<Slot> Slots = new List<Slot>();
        public List<Slot> GetAvailableSlots(VehicleModel type)
        {
            //this is to get the slots which are available for parking
            var slots = from slot in Slots
                        where slot.Type == type && slot.Availability ==Status.AVAILABLE
                        select slot;
            List<Slot> AvailableSlots = slots.ToList();
            return (AvailableSlots.Count == 0) ? null : AvailableSlots;
        }
        public void InitializeSlots(int twoWheeler, int fourWheeler, int heavyVehicles)
        {
            //Intialize slots for various types of vehicles
            CreateSlot(twoWheeler, VehicleModel.TwoWheeler);
            CreateSlot(fourWheeler, VehicleModel.FourWheeler);
            CreateSlot(heavyVehicles, VehicleModel.HeavyVehicle);
        }
        public void CreateSlot(int NumberofVehicles, VehicleModel vehicleType)
        { 
            //create slots for each type of  vehicle
            for (int index = 1; index <=NumberofVehicles; index++)
            {
                Slot slot = new Slot(vehicleType, Slots.Count + 1);//if Count==0 =>id=(count+1)=>1
                Slots.Add(slot);
            }
        }
        public Ticket Park(int slotId, string vehicleNumber)
        { 
            //park a vehicle ,generate a tickeT,change status to "OCCUPIED"
            TicketService ticketService = new TicketService();
            Slot SelectedSlot = Slots.Find(slot => slot.Id == slotId);
            Ticket ticket = ticketService.GenerateTicket(slotId, vehicleNumber);
            SelectedSlot.Availability=Status.OCCUPIED;
            SelectedSlot.ParkedVehicle.VehicleNumber = vehicleNumber;
            return ticket;
        }
        public void UnPark(int number)
        {
            //unpark a vehicle ,change the status to available
            Slot ThisSlot = Slots.Find(slot => slot.Id == number);
            ThisSlot.ParkedVehicle = null;
            ThisSlot.Availability = Status.AVAILABLE;
        }
        public List<Slot> GetSlots()
        {
            return Slots;
        }
        public Slot GetSlot(int Number)
        {
            Slot SelectedSlot = Slots.Find(slot => slot.Id == Number);
            return SelectedSlot;
        }
        public bool VehicleNumberAmbiguity(string newVehicleNo)
        { 
            try
            {
                Slot slot = Slots.Find(item => item.ParkedVehicle.VehicleNumber == newVehicleNo);
                return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
            
          
        }

    }
}