using System.Linq.Expressions;

namespace Fintranet.TaxCalculation.Service.Helpers
{
    public static class SetOrder
    {
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string sorts)
        {
            try
            {
                var desc = !string.IsNullOrEmpty(sorts) && sorts.Split('~')[1] == "desc";
                var orderByProperty = !string.IsNullOrEmpty(sorts) ? sorts.Split('~')[0] : string.Empty;

                orderByProperty = char.ToUpper(orderByProperty[0]) + orderByProperty.Substring(1);
                string command = desc ? "OrderByDescending" : "OrderBy";
                var type = typeof(TEntity);
                var property = type.GetProperty(orderByProperty);
                var parameter = Expression.Parameter(type, "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);
                var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                              source.Expression, Expression.Quote(orderByExpression));
                return source.Provider.CreateQuery<TEntity>(resultExpression);
            }
            catch (Exception)
            {
                throw new Exception("نام ستون اشتباه است");
            }
        }
    }
}
