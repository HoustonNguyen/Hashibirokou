namespace TwitterBroadcastSystemModel.Extensions
{
    public static class BooleanValueExtensions
    {
        public static bool GetBooleanValue(this byte? value)
        {
            return value == 1;
        }

        public static bool GetBooleanValue(this int? value)
        {
            return value == 1;
        }

        public static bool GetBooleanValue(this bool? value)
        {
            return value == true;
        }
    }
}