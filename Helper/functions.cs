using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot.Helper
{
    public class Functions
    {
        Display Display = new Display();
        public void NoSlot()
        {
            
            Display.Print("******************************************");
            Display.Print("no slots are available");
            Display.Print("******************************************");
        } 
        public void Invalid()
        {

            Display.Print("******************************************");
            Display.Print("the slot is invalid");
            Display.Print("******************************************");
        }
    
        public void Parked()
        {
            Display.Print("******************************************");
            Display.Print("the vehicle is parked");
            Display.Print("******************************************");
        }
        public int Option()
        {
            int option;
            while (true)
            {
                try
                {
                   
                    option = int.Parse(Display.Scan());
                    Display.Print("******************************************");
                    break;
                }
                catch (FormatException)
                {
                    Display.Print("******************************************");
                    Display.Print("Please check the format of option\n ******************************************");
                    Display.Print("Please enter a valid option");
                }

            }
            return option;
        }
    }
}
