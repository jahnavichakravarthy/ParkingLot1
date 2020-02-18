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

        public void Parked(int slotno)
        {
            Display.Print("******************************************");
            Display.Print($"the vehicle is parked at slot:{slotno}");
            Display.Print("******************************************");
        }
        public int Input()
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
                    Display.Print("Please check the format of input\n ******************************************");
                    Display.Print("Please enter a valid input");
                }
            }
            return option;
        }
    }
}
