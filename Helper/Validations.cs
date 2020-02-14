using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ParkingLot.Helper
{
    public class Validations
    {
        public bool VehicleNumber(string name)
        {
            string strRegex = @"(^[a-z]{2}" + "[0-9]{2}"+"[a-z]{2}"+ "[0-9]{1,}$)";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(name))
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
    }
}
