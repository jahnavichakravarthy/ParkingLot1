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
            while (true)
            {
                Display Display = new Display();
                Functions functions = new Functions();
                List<Ticket> tickets = new List<Ticket>();
                //List<Slot> AvailableSlots = new List<Slot>();
                IParkingService ParkingServices = new ParkingSevice();
                int option;
                Display.Print("WELCOME TO ABCD PARKING LOT");
                Display.Print("******************************************");
                Display.Print("please enter number of slots for 2 wheeler");
                int twoWheeler = functions.Input();
                Display.Print("please enter number of slots for 4 wheeler");
                int fourWheeler = functions.Input();
                Display.Print("please enter number of slots for heavy vehicle");
                int heavyVehicle = functions.Input();
                ParkingServices.InitializeSlots(twoWheeler, fourWheeler, heavyVehicle);
                bool Exit = false;
                do
                {
                    Display.Print("select a option\n\t1.park\n\t2.Un-park\n\t3.Display status of all slots\n\t4.Exit(Re-Initialize)");
                    option = functions.Input();
                    switch (option)
                    {
                        case 1:
                            Display.Print("please enter the valid details of the vehicle");
                            Vehicle vehicle = new Vehicle();
                            Display.Print("Choose the type of vehicle\n\t1.2 wheeler\n\t2.4 wheeler\n\t3.heavy vehicle\n\t");
                            int choice = functions.Input();
                           vehicle.Type = (VehicleModel)choice;
                            List<Slot> AvailableSlots = ParkingServices.GetAvailableSlots(vehicle.Type);
                            if (AvailableSlots != null)
                            {
                                bool validSlot = false;//FOR EXITING THE LOOP
                                do
                                {
                                    foreach (Slot thisSlot in AvailableSlots)
                                    {
                                        Console.WriteLine($"slot-{ thisSlot.Id }");
                                    }
                                    Display.Print("Please choose a slot number");
                                    int slotNumber = functions.Input();
                                    bool validity = false;
                                    do
                                    {
                                        Display.Print("Please enter the vehicle number");
                                        vehicle.VehicleNumber = Display.Scan();
                                        Validations validation = new Validations();
                                        if (validation.VehicleNumber(vehicle.VehicleNumber) == true)
                                        {
                                           bool ambiguity= ParkingServices.VehicleNumberAmbiguity(vehicle.VehicleNumber);
                                            if (ambiguity == false)
                                            {
                                                validity = true;
                                            }
                                            else
                                            {
                                                Display.Print("The Vehicle numberis already parked in another slot");
                                            }
                                            //*********there is still a chance that two vehicles can be parked with same number
                                        }
                                        else
                                        {
                                            Display.Print("Please check the format of the vehicle number");
                                            Display.Print("******************************************");
                                            validity = false;
                                        }
                                    }
                                    while (validity == false);
                                    Slot SelectedSlot = AvailableSlots.Find(slot => slot.Id == slotNumber);
                                    SelectedSlot.ParkedVehicle = vehicle;
                                    if (SelectedSlot != null)
                                    {
                                        Ticket ticket = ParkingServices.Park(slotNumber, vehicle.VehicleNumber);
                                        tickets.Add(ticket);
                                        functions.Parked(SelectedSlot.Id);
                                        Display.Print("******************************************");
                                        Console.WriteLine($"Ticket Id:{ticket.Id}\nVehicle Number:{SelectedSlot.ParkedVehicle.VehicleNumber}\nSlot Number:{SelectedSlot.Id}\nIn-Time:{ticket.InTime}");
                                        Display.Print("******************************************");
                                        validSlot = true;
                                    }
                                    else
                                    {
                                        functions.Invalid();
                                    }
                                }
                                while (validSlot == false);
                            }
                            else
                            {
                                functions.NoSlot();
                            }

                            break;
                        case 2:
                            while (true)
                            {
                                if (tickets != null && tickets.Count != 0)
                                {
                                    bool validSlot = false;
                                    do
                                    {
                                        Display.Print("Please enter the slot Id");
                                        int Id = functions.Input();
                                        Ticket SelectedTicket = tickets.Find(ticket => ticket.SlotNumber == Id);
                                        if (SelectedTicket != null)
                                        {
                                            Slot slot = ParkingServices.GetSlot(SelectedTicket.SlotNumber);
                                            if (slot.Availability == Status.OCCUPIED)
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
                                            validSlot = true;
                                        }
                                        else
                                        {
                                            Display.Print("enter a valid slot id");
                                        }
                                    }
                                    while (validSlot == false);
                                    break;
                                }
                                else
                                {
                                    Display.Print("no tickets are generated yet");
                                    break;
                                }
                            }
                            break;
                        case 3:
                             List<Slot> Slots = ParkingServices.GetSlots();
                            foreach (Slot slot in Slots)
                            {
                                string vehicleNumber = ((slot.ParkedVehicle==null) ? "-----" : slot.ParkedVehicle.VehicleNumber);
                                Console.WriteLine($"slot:{slot.Id}  type:{slot.Type}  status:{Convert.ToString(slot.Availability)} vehiclePArked:{vehicleNumber}");
                            }
                            Display.Print("******************************************");
                            break;
                        case 4:
                            Exit = true;
                        
                            break;
                        default:
                            Display.Print("please select a valid option");
                            Display.Print("******************************************");
                            break;
                    }

                }
                while (Exit == false);
            }
        }
       
    }
}