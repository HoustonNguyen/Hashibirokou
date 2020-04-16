namespace TwitterBroadcastSystemModel.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToCurrencySimple(this decimal value)
        {
            return value.ToString("C").Replace("$", string.Empty).Replace(",", string.Empty);
        }
        public static string ToCurrencyString(this decimal? value)
        {
            if (value.HasValue == false)
            {
                return string.Empty;
            }
            return value.Value.ToString("C");
        }
    }
}