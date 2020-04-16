using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TwitterBroadcastSystemModel.Extensions
{
    public static class IQueryableExtensions
    {
        public static IOrderedQueryable OrderBy(this IQueryable source, string property)
        {
            return ApplyOrder(source, property, "OrderBy");
        }

        public static IOrderedQueryable OrderByDescending(this IQueryable source, string property)
        {
            return ApplyOrder(source, property, "OrderByDescending");
        }

        public static IOrderedQueryable ThenBy(this IQueryable source, string property)
        {
            return ApplyOrder(source, property, "ThenBy");
        }

        public static IOrderedQueryable ThenByDescending(this IQueryable source, string property)
        {
            return ApplyOrder(source, property, "ThenByDescending");
        }

        static IOrderedQueryable ApplyOrder(IQueryable source, string property, string methodName)
        {
            Type type = source.ElementType;
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            PropertyInfo pi = type.GetProperty(property);

            expr = Expression.Property(expr, pi);
            type = pi.PropertyType;

            Type delegateType = typeof (Func<,>).MakeGenericType(source.ElementType, type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);
            object result = typeof (Queryable).GetMethods()
                .Single(method =>
                    method.Name == methodName &&
                    method.IsGenericMethodDefinition &&
                    method.GetGenericArguments().Length == 2 &&
                    method.GetParameters().Length == 2)
                .MakeGenericMethod(source.ElementType, type)
                .Invoke(null, new object[] {source, lambda});
            return (IOrderedQueryable) result;
        }
    }
}