using System;
using System.Xml.Linq;

namespace TwitterBroadcastSystemModel.Extensions
{
    public static class XElementExtensions
    {
        public static DateTime? ToDateTime(this XElement xe)
        {
            if (xe != null)
            {
                if (!string.IsNullOrEmpty(xe.Value))
                {
                    return Convert.ToDateTime(xe.Value);
                }
            }
            return null;
        }

        public static bool? ToBool(this XElement xe)
        {
            if (xe != null)
            {
                if (!string.IsNullOrEmpty(xe.Value))
                {
                    return Convert.ToBoolean(xe.Value);
                }
            }
            return null;
        }
        public static decimal? ToDecimal(this XElement xe)
        {
            if (xe != null)
            {
                if (!string.IsNullOrEmpty(xe.Value))
                {
                    return Convert.ToDecimal(xe.Value);
                }
            }
            return null;
        }

        public static Guid? ToGuid(this XElement xe)
        {
            if (xe != null)
            {
                if (!string.IsNullOrEmpty(xe.Value))
                {
                    return new Guid(xe.Value);
                }
            }
            return null;
        }
        public static int? ToInt(this XElement xe)
        {
            if (xe != null)
            {
                if (!string.IsNullOrEmpty(xe.Value))
                {
                    return Convert.ToInt32(xe.Value);
                }
            }
            return null;
        }
        public static byte? ToByte(this XElement xe)
        {
            if (xe != null)
            {
                if (!string.IsNullOrEmpty(xe.Value))
                {
                    return Convert.ToByte(xe.Value);
                }
            }
            return null;
        }

    }
}