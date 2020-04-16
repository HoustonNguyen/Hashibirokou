using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Reflection;

namespace TwitterBroadcastSystemModel.Extensions
{
    public static class ObjectExtensions
    {
        public static void SetProperty(this object source, string property, object value)
        {
            var bits = property.Split('.');

            for (var i = 0; i < bits.Length - 1; i++)
            {
                var prop = source.GetType().GetProperty(bits[i]);
                source = prop.GetValue(source, null);
            }

            PropertyInfo propertyToSet;
            if (source is IDictionary)
            {
                string propertyName = bits[bits.Length - 1];
                propertyToSet = source.GetType().GetProperty(propertyName);
                if (propertyToSet != null)
                {
                    propertyToSet.SetValue(source, value, null);
                }
                else
                {
                    // Add to dictionary
                    (source as IDictionary)[propertyName] = value;
                }
            }
            else if (source is IEnumerable)
            {
                foreach (var o in (source as IEnumerable))
                {
                    propertyToSet = o.GetType().GetProperty(bits[bits.Length - 1]);
                    propertyToSet.SetValue(o, value, null);
                    break;
                }
            }
            else
            {
                propertyToSet = source.GetType().GetProperty(bits[bits.Length - 1]);
                propertyToSet.SetValue(source, value, null);
            }
        }

        public static void SetProperty(this object source, string property, object value, DbEntityEntry entityEntry)
        {
            source.SetProperty(property, value);

            if (entityEntry != null)
            {
                entityEntry.Property(property).IsModified = true;
            }
        }

        public static T GetPropertyValue<T>(this object source, string property)
        {
            var bits = property.Split('.');

            for (var i = 0; i < bits.Length - 1; i++)
            {
                var prop = source.GetType().GetProperty(bits[i]);
                source = prop.GetValue(source, null);
            }

            var propertyToGet = source.GetType().GetProperty(bits[bits.Length - 1]);

            return (T)propertyToGet.GetValue(source);
        }
    }
}