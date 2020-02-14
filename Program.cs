using System;
using System.Collections.Generic;
using ParkingLot.Models;
using ParkingLot.Services;
using ParkingLot.Helper;

namespace ParkingLot
{
    public class Program
    {

        public static void Main(string[] args)
        {

            Program program = new Program();
            Display Display = new Display();
            Functions functions = new Functions();
            List<Ticket> tickets = new List<Ticket>();
            List<Slot> AvailableSlots = new List<Slot>();
            TicketServices ticketServices = new TicketServices();
            ParkingSevices ParkingServices = new ParkingSevices();

            int option;
            Display.Print("WELCOME TO ABCD PARKING LOT");
            Display.Print("******************************************");
            Display.Print("please enter number of slots for 2 wheeler");
            int twoWheeler = functions.Option();
            Display.Print("please enter number of slots for 4 wheeler");
            int fourWheeler = functions.Option();
            Display.Print("please enter number of slots for heavy vehicle");
            int heavyVehicle = functions.Option();
            ParkingServices.CreateSlots(twoWheeler, fourWheeler, heavyVehicle);


            while (true)
            {
                Display.Print("select a option\n\t1.park\n\t2.Un-park");
                option = functions.Option();
                switch (option)
                {
                    case 1:
                        Display.Print("please enter the valid details of the vehicle");
                        Vehicle vehicle = new Vehicle();
                        Display.Print("Choose the type of vehicle\n\t1.2 wheeler\n\t2.4 wheeler\n\t3.heavy vehicle");
                        option = functions.Option();
                        switch (option)
                        {
                            case 1:
                                vehicle.Type = "TwoWheeler";
                                AvailableSlots = ParkingServices.Availability(vehicle.Type);
                                if (AvailableSlots != null)
                                {
                                    while (true)
                                    {
                                        program.DisplayAvailable(AvailableSlots);
                                        Display.Print("Please choose a slot number");
                                        int Number = functions.Option();
                                        Slot SelectedSlot = AvailableSlots.Find(slot => slot.Id == Number);
                                        if (SelectedSlot != null)
                                        {
                                            Ticket ticket = program.Allocate(SelectedSlot, vehicle);
                                            ParkingServices.Park(Number, vehicle.VehicleNumber);
                                            tickets.Add(ticket);
                                            functions.Parked();
                                            break;
                                        }
                                        else
                                        {
                                            functions.Invalid();
                                        }
                                    }
                                }
                                else
                                {
                                    functions.NoSlot();
                                }

                                break;
                            case 2:
                                vehicle.Type = "FourWheeler";
                                AvailableSlots = ParkingServices.Availability(vehicle.Type);
                                if (AvailableSlots != null)
                                {
                                    while (true)
                                    {
                                        program.DisplayAvailable(AvailableSlots);
                                        Display.Print("Please choose a slot number");
                                        int Number = functions.Option();
                                        Slot SelectedSlot = AvailableSlots.Find(slot => slot.Id == Number);
                                        if (SelectedSlot != null)
                                        {
                                            Ticket ticket = program.Allocate(SelectedSlot, vehicle);
                                            ParkingServices.Park(Number, vehicle.VehicleNumber);
                                            tickets.Add(ticket);
                                            functions.Parked();
                                            break;
                                        }
                                        else
                                        {
                                            functions.Invalid();
                                        }
                                    }
                                }
                                else
                                {
                                    functions.NoSlot();
                                }

                                break;
                            case 3:
                                vehicle.Type = "HeavyVehicle";
                                AvailableSlots = ParkingServices.Availability(vehicle.Type);
                                if (AvailableSlots != null)
                                {
                                    while (true)
                                    {
                                        program.DisplayAvailable(AvailableSlots);
                                        Display.Print("Please choose a slot number");
                                        int Number = functions.Option();
                                        Slot SelectedSlot = AvailableSlots.Find(slot => slot.Id == Number);
                                        if (SelectedSlot != null)
                                        {
                                            Ticket ticket = program.Allocate(SelectedSlot, vehicle);
                                            ParkingServices.Park(Number, vehicle.VehicleNumber);
                                            tickets.Add(ticket);
                                            functions.Parked();
                                            break;
                                        }
                                        else
                                        {
                                            functions.Invalid();
                                        }
                                    }
                                }
                                else
                                {
                                    functions.NoSlot();
                                }
                                break;
                        }

                        break;
                    case 2:
                        while (true)
                        {
                            if (tickets != null && tickets.Count != 0)
                            {
                                while (true)
                                {
                                    Display.Print("Please enter the ticket Id");
                                    string Id = Display.Scan();
                                    Ticket SelectedTicket = tickets.Find(ticket => ticket.Id == Id);
                                    if (SelectedTicket != null)
                                    {
                                        Slot slot = ParkingServices.FindSlot(SelectedTicket.SlotNumber);
                                        if (slot.Availability == false)
                                        {
                                            SelectedTicket.OutTime = DateTime.Now;
                                            ParkingServices.UnPark(SelectedTicket.SlotNumber);
                                            Display.Print("******************************************");
                                            Display.Print("The vehicle is unparked");
                                            Console.WriteLine($"Ticket Id:{SelectedTicket.Id}\nVehicle Number:{SelectedTicket.VehicleNumber}\nSlot Number:{SelectedTicket.SlotNumber}\nIn-Time:{SelectedTicket.InTime}\nOut-Time:{SelectedTicket.OutTime}");
                                            Display.Print("******************************************");
                                        }
                                        else
                                        {
                                            Display.Print("the vehicle is already unparked from the slot");
                                            Display.Print("******************************************");
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        Display.Print("enter a valid ticket Id");
                                    }
                                }
                                break;
                            }
                            else
                            {
                                Display.Print("no tickets are generated yet");
                                break;
                            }
                        }
                        break;
                    default:
                        Display.Print("please select a valid option");
                        Display.Print("******************************************");
                        break;
                }
            }
        }
        public Ticket Allocate(Slot SelectedSlot, Vehicle vehicle)
        {
            Validations validations = new Validations();
            Display Display = new Display();
            TicketServices ticketServices = new TicketServices();
            SelectedSlot.ParkedVehicle = vehicle;
            while (true)
            {
                Display.Print("please enter the vehicle number");
                SelectedSlot.ParkedVehicle.VehicleNumber = Display.Scan();
                if (validations.VehicleNumber(SelectedSlot.ParkedVehicle.VehicleNumber) == true)
                {
                    break;
                }
                else
                {
                    Display.Print("Please check the format of the vehicle number");
                    Display.Print("******************************************");
                }
            }
            SelectedSlot.Availability = false;
            Display.Print("******************************************");
            Ticket ticket = ticketServices.GenerateTicket(SelectedSlot.Id, SelectedSlot.ParkedVehicle.VehicleNumber);
            Console.WriteLine($"Ticket Id:{ticket.Id}\nVehicle Number:{SelectedSlot.ParkedVehicle.VehicleNumber}\nSlot Number:{SelectedSlot.Id}\nIn-Time:{ticket.InTime}");
            Display.Print("******************************************");
            return ticket;
        }
        public void DisplayAvailable(List<Slot> AvailableSlots)
        {
            foreach (Slot thisSlot in AvailableSlots)
            {
                Console.WriteLine($"slot-{ thisSlot.Id }");
            }
        }
    }
}
