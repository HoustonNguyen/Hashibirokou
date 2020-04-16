using System;
using System.Drawing;

namespace TwitterBroadcastSystemModel.Extensions
{
    public static class ColorExtensions
    {
        // http://stackoverflow.com/a/2241471
        public static int PerceivedBrightness(this Color c)
        {
            return (int) Math.Sqrt(
                c.R*c.R*.299 +
                c.G*c.G*.587 +
                c.B*c.B*.114);
        }
        public static string ToARGBHex(string htmlColor)
        {
            if (htmlColor.StartsWith("#") == false)
            {
                htmlColor = ColorTranslator.ToHtml(Color.FromArgb(Color.FromName(htmlColor).ToArgb()));
            }
            htmlColor = "#" + htmlColor.TrimStart('#').PadLeft(6, '0').PadLeft(8, 'F');
            return htmlColor;
        }
        public static string RemoveARGBApha(string htmlColor)
        {
            if (htmlColor.StartsWith("#") == false)
            {
                htmlColor = ColorTranslator.ToHtml(Color.FromArgb(Color.FromName(htmlColor).ToArgb()));
            }
            htmlColor = htmlColor.TrimStart('#').PadLeft(6, '0');
            if(htmlColor.Length == 6)
            {
                return "#" + htmlColor;
            }
            else
            {
                return "#" + htmlColor.Substring(htmlColor.Length - 6, 6);
            }
            
        }
    }
}