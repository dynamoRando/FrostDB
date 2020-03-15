using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public static class QueryExtensions
    {
        public static string BetweenCharacters(this string item, char item1, char item2)
        {
            int location1 = item.IndexOf(item1) + item.Length;
            int location2 = item.LastIndexOf(item2);

            return item.Substring(location1, location2 - location1);
        }
    }
}
