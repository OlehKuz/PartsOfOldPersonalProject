using System;

namespace Common.Helpers
{
    public static class DateTimeExtentions
    {
        private static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime ToUtcDateTime(this long unixTimeStamp) => _unixEpoch.AddSeconds(unixTimeStamp);

        public static DateTime ToLocalDateTime(this long unixTimeStamp) => 
            unixTimeStamp.ToUtcDateTime().ToLocalTime();

        public static long ToUnixTimeStamp(this DateTime utcDateTime)
        {
            return (long)utcDateTime.Subtract(_unixEpoch).TotalSeconds;
        }

        public static DateTime ToUtcDateTime(this DateTime dateTime)=> dateTime.ToUniversalTime();
        
    }
}