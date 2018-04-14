using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using static System.Environment;

namespace Remember.Your.Id
{
    public static class DateTimeExtensions
    {
        public static DateTime ToBritishTime(this DateTime source)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var offset = timeZone.GetUtcOffset(source);
            return source + offset;
        }
    }
}