using System.Linq.Expressions;

namespace Business.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderBy)
        {
            var expression = source.Expression;

            string[] orderFieldsArr = orderBy.Split(" ");

            string field = orderFieldsArr[0];

            string order = orderFieldsArr[1];

            var parameter = Expression.Parameter(typeof(T), "x");

            var selector = Expression.PropertyOrField(parameter, field);

            var method = string.Equals(order, "desc", StringComparison.OrdinalIgnoreCase) ?

                "OrderByDescending" : "OrderBy";

            expression = Expression.Call(typeof(Queryable), method,

                new Type[] { source.ElementType, selector.Type },

                expression, Expression.Quote(Expression.Lambda(selector, parameter)));


            return source.Provider.CreateQuery<T>(expression);

        }
    }
}
