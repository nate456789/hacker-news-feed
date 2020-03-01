
using System;

namespace HackerNewsFeed.Utility
{
    public static class ExtensionMethods
    {
        public static DateTime EpochToDateTime(this DateTime dt, int value)
        {
            Console.WriteLine("Epoch value ==> Passed Value", dt, value);
            DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(value); 
            return dto.DateTime; 
        }
    }
}