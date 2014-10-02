using System;

namespace CSharpMultiPartPostHandler.RestClient
{
    public static class DateTimeHelper
    {
        public static DateTime FromTimestamp(Int64 timestamp)
        {
            return FromTimestamp((double)timestamp);
        }

        public static DateTime FromTimestamp(Double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(timestamp).ToLocalTime();
        }

        public static double ToTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}

