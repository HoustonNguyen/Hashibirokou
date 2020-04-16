using System;

namespace TwitterBroadcastSystemModel.Constants
{
    public class PriorityLevel
    {
        public static readonly Models.PriorityLevel ONCEPERDAY = new Models.PriorityLevel()
        {
            PrimaryKey = new Guid("0E679322-D264-EA11-AFD1-D46D6D54CC97"),
            Description = $"Once per ({ConvertMStoTime(86400000)})",
            Delay = 86400000
        };

        public static readonly Models.PriorityLevel ONCEPER12HOURS = new Models.PriorityLevel()
        { 
            PrimaryKey = new Guid("2E679322-D264-EA11-AFD1-D46D6D54CC97"),
            Description = $"Once per ({ConvertMStoTime(43200000)})", 
            Delay = 43200000 
        };

        public static readonly Models.PriorityLevel ONCEPER6HOURS = new Models.PriorityLevel()
        {
            PrimaryKey = new Guid("3E679322-D264-EA11-AFD1-D46D6D54CC97"),
            Description = $"Once per ({ConvertMStoTime(21600000)})",
            Delay = 21600000
        };

        public static readonly Models.PriorityLevel ONCEPER3HOURS = new Models.PriorityLevel()
        {
            PrimaryKey = new Guid("4E679322-D264-EA11-AFD1-D46D6D54CC97"),
            Description = $"Once per ({ConvertMStoTime(10800000)})",
            Delay = 10800000
        };

        public static readonly Models.PriorityLevel ONCEPER2HOURS = new Models.PriorityLevel()
        {
            PrimaryKey = new Guid("5E679322-D264-EA11-AFD1-D46D6D54CC97"),
            Description = $"Once per ({ConvertMStoTime(7200000)})",
            Delay = 7200000
        };

        public static readonly Models.PriorityLevel ONCEPER1HOUR = new Models.PriorityLevel()
        {
            PrimaryKey = new Guid("6E679322-D264-EA11-AFD1-D46D6D54CC97"),
            Description = $"Once per ({ConvertMStoTime(3600000)})",
            Delay = 3600000
        };

        public static readonly Models.PriorityLevel ONCEPER30MINUTES = new Models.PriorityLevel()
        {
            PrimaryKey = new Guid("7E679322-D264-EA11-AFD1-D46D6D54CC97"),
            Description = $"Once per ({ConvertMStoTime(1800000)})",
            Delay = 1800000
        };

        public static readonly Models.PriorityLevel ONCEPER15MINUTES = new Models.PriorityLevel()
        {
            PrimaryKey = new Guid("8E679322-D264-EA11-AFD1-D46D6D54CC97"),
            Description = $"Once per ({ConvertMStoTime(900000)})",
            Delay = 900000
        };

        public static readonly Models.PriorityLevel ONCEPER5MINUTES = new Models.PriorityLevel()
        {
            PrimaryKey = new Guid("9E679322-D264-EA11-AFD1-D46D6D54CC97"),
            Description = $"Once per ({ConvertMStoTime(300000)})",
            Delay = 300000
        };

        public static readonly Models.PriorityLevel ONCEPER3MINUTES = new Models.PriorityLevel()
        {
            PrimaryKey = new Guid("10679322-D264-EA11-AFD1-D46D6D54CC97"),
            Description = $"Once per ({ConvertMStoTime(180000)})",
            Delay = 180000
        };

        public static readonly Models.PriorityLevel ONCEPER2MINUTES = new Models.PriorityLevel()
        {
            PrimaryKey = new Guid("11679322-D264-EA11-AFD1-D46D6D54CC97"),
            Description = $"Once per ({ConvertMStoTime(120000)})",
            Delay = 120000
        };

        public static readonly Models.PriorityLevel ONCEPER1MINUTE = new Models.PriorityLevel()
        {
            PrimaryKey = new Guid("12679322-D264-EA11-AFD1-D46D6D54CC97"),
            Description = $"Once per ({ConvertMStoTime(60000)})",
            Delay = 60000
        };

        public static readonly Models.PriorityLevel ONCEPER30SECONDS = new Models.PriorityLevel()
        {
            PrimaryKey = new Guid("13679322-D264-EA11-AFD1-D46D6D54CC97"),
            Description = $"Once per ({ConvertMStoTime(30000)})",
            Delay = 30000
        };

        public static readonly Models.PriorityLevel ONCEPER15SECONDS = new Models.PriorityLevel()
        {
            PrimaryKey = new Guid("14679322-D264-EA11-AFD1-D46D6D54CC97"),
            Description = $"Once per ({ConvertMStoTime(15000)})",
            Delay = 15000
        };

        private static string ConvertMStoTime(int ms)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(ms);
            return string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                                    t.Hours,
                                    t.Minutes,
                                    t.Seconds,
                                    t.Milliseconds);
        }
    }
}
