using System;

namespace HackerNewsFeed.Utility
{
    public static class ExtensionMethods
    {
        public static DateTime EpochToDateTime(this DateTime dt, int value)
        {
            var dto = DateTimeOffset.FromUnixTimeSeconds(value);
            return dto.LocalDateTime;
        }

        public static string FindElapseTime(this DateTime pub)
        {
            var diff = DateTime.Now - pub;
            if (diff.TotalMinutes < 60)
            {
                return $"{diff.Minutes} minutes ago";
            }
            else if (diff.TotalMinutes >= 60 && diff.TotalMinutes < 120)
            {
                return $"{diff.Hours} hour ago";
            }

            return $"{diff.Hours} hours ago";
        }
    }
}