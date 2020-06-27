using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.ConsoleMessages
{
    public static class Extensions
    {
        public static LocationInfo Convert(this Location location)
        {
            var item = new LocationInfo();
            item.IpAddress = location.IpAddress;
            item.PortNumber = location.PortNumber;
            return item;
        }

        public static string ConvertToString(this Location location)
        {
            return location.IpAddress + ":" + location.PortNumber.ToString();
        }
    }
}
