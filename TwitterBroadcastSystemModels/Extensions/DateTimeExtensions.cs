using System;

namespace TwitterBroadcastSystemModel.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ConvertUnixToDateTime(this ulong time)
        {
            DateTime startDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime result = startDate.AddSeconds(time);

            return result;
        }

        public static DateTime SqlMax
        {
            get
            {
                return DateTime.Parse("9999-12-31");
            }
        }
    }
}