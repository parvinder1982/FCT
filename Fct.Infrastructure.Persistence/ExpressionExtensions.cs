namespace Fct.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Extending SQL LINQ query with by adding multiple requests for including entities referenced by navigation properties
        /// </summary>
        /// <typeparam name="T">Entity type of the DbSet</typeparam>
        /// <param name="dbset">Database table data(set)</param>
        /// <param name="includes">Properties to eager load</param>
        /// <returns></returns>
        public static IQueryable<T> IncludeMultiple<T>(this DbSet<T> dbset, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            var query = dbset.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
