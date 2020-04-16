using System;
using System.Data.Common;

namespace TwitterBroadcastSystemModel.Extensions
{
    public static class DbReaderExtensions
    {
        public static String SafeString(this DbDataReader reader, String columnName)
        {
            Object value = reader[columnName];
            if(value is DBNull)
            {
                return String.Empty;
            }
            return (String) reader[columnName];
        }

        public static Guid SafeGuid(this DbDataReader reader, String columnName)
        {
            Object value = reader[columnName];
            if (value is DBNull)
            {
                return Guid.Empty;
            }
            return (Guid)reader[columnName];
        }

        public static DateTime SafeDateTime(this DbDataReader reader, String columnName)
        {
            Object value = reader[columnName];
            if (value is DBNull)
            {
                return DateTime.MinValue;
            }
            return (DateTime)reader[columnName];
        }

        public static Decimal SafeDecimal(this DbDataReader reader, String columnName)
        {
            Object value = reader[columnName];
            if (value is DBNull)
            {
                return 0;
            }
            return (Decimal)reader[columnName];
        }
    }
}