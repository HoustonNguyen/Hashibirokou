using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System;
using TwitterBroadcastSystemModel.Extensions;

namespace TwitterBroadcastSystemModel.Extensions
{
    public static class TBSEntitiesExtensions
    {
        public static T GetResult<T>(this TwitterBroadcastSystemModel.TBSEntities entities, DbDataReader reader)
        {
            var result = ((IObjectContextAdapter)entities).ObjectContext.Translate<T>(reader);
            return result.FirstOrDefault();
        }

        public static IList<T> GetResults<T>(this TwitterBroadcastSystemModel.TBSEntities entities, DbDataReader reader)
        {
            var result = ((IObjectContextAdapter)entities).ObjectContext.Translate<T>(reader);
            return result.ToList();
        }

        public static IList<T> GetResultsHasSpaces<T>(this TwitterBroadcastSystemModel.TBSEntities entities, DbDataReader reader) where T : new()
        {
            var result = new List<T>();
            while (reader.Read())
            {
                var row = new T();
                for (var fieldCount = 0; fieldCount < reader.FieldCount; fieldCount++)
                {
                    var prop = reader.GetName(fieldCount);
                    reader.SetValue(row, prop);
                }
                result.Add(row);
            }

            return result;
        }

        public static void SetValue(this DbDataReader reader, object row, string name)
        {
            var value = reader[name];

            if (value == null || value == DBNull.Value)
            {
                return;
            }

            row.SetProperty(name.Replace(" ", "_").Replace(",","_"), reader[name]);
            return;
        }

    }

}