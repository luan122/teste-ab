using Ambev.DeveloperEvaluation.Application.Common.Commands;
using Ambev.DeveloperEvaluation.Common.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ambev.DeveloperEvaluation.Application.Categories.ListCategory;
using System.Collections.Specialized;
using Microsoft.VisualBasic;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Ambev.DeveloperEvaluation.Application.Common.Results;

namespace Ambev.DeveloperEvaluation.Application.Common.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, FilterCommandRequest? filter)
        {
            if (filter == null || filter == new FilterCommandRequest()) return query;
            if (filter.SearchParams.Count > 0)
                query = query.AddSearchByFilterToQuery(filter.SearchParams);
            if (filter.MinMaxParams.Count > 0)
                query = query.AddMinMaxByFilterToQuery(filter.MinMaxParams);
            if (filter.OrderParams.Count > 0)
                query = query.AddOrderByFilterToQuery(filter.OrderParams);
            return query;
        }
        public static async Task<PagedCommandResult<T>> GetPagedResultAsync<T>(this IQueryable<T> query, FilterCommandRequest filter, IMapper mapper)
        {
            var count = query.Count();
            var result = await query.Skip((filter.Page - 1) * filter.Size)
                                    .Take(filter.Size)
                                    .ProjectTo<T>(mapper.ConfigurationProvider)
                                    .ToListAsync();
            var pagedResult = new PagedCommandResult<T>(result)
            {
                TotalCount = count,
                CurrentPage = filter.Page,
                PageSize = filter.Size
            };
            return pagedResult;
        }
        private static IQueryable<T> AddSearchByFilterToQuery<T>(this IQueryable<T> currentQuery, Dictionary<string, string> filters)
        {
            var validSearchParams = filters.Where(w => typeof(T).GetProperties().Select(s => s.Name.ToLowerInvariant()).Contains(w.Key.ToLowerInvariant())).ToDictionary(kvpk => kvpk.Key.ToLowerInvariant(), kvpv => kvpv.Value);
            foreach (var filter in validSearchParams)
            {
                var property = typeof(T).GetProperty(filter.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null)
                    continue;

                if (property.PropertyType != typeof(string))
                    continue;

                var stringSearchMethod = filter.Value switch
                {
                    { } when filter.Value.StartsWith('*') && filter.Value.EndsWith('*') => typeof(string).GetMethod(nameof(string.Concat), [typeof(string)]),
                    { } when filter.Value.StartsWith('*') => typeof(string).GetMethod(nameof(string.EndsWith), [typeof(string)]),
                    { } when filter.Value.EndsWith('*') => typeof(string).GetMethod(nameof(string.StartsWith), [typeof(string)]),
                    _ => null
                };

                if (stringSearchMethod == null)
                    continue;

                var iswildCardSearch = stringSearchMethod.Name.Contains("Concat");

                
                
                var parameter = Expression.Parameter(typeof(T), "x");
                var getter = Expression.Property(parameter, property);
                var exp = iswildCardSearch ?
                    Expression.Call(
                        typeof(DbFunctionsExtensions),
                        nameof(DbFunctionsExtensions.Like),
                        null,
                        Expression.Constant(EF.Functions),
                        getter,
                        Expression.Add(
                            Expression.Add(
                                Expression.Constant("%"),
                                Expression.Constant(filter.Value.Trim('*').Replace("*", "%")),
                                stringSearchMethod),
                            Expression.Constant("%"),
                            stringSearchMethod))
                    : Expression.Call(
                        getter,
                        stringSearchMethod,
                        [Expression.Constant(filter.Value.Trim('*').Replace("*", "%").Replace("%%","*"), typeof(string)), Expression.Constant(null, typeof(string))]);
                var lambda = Expression.Lambda<Func<T, bool>>(exp, parameter);
                currentQuery = currentQuery.Where(lambda);

            }
            return currentQuery;


        }
        private static IQueryable<T> AddOrderByFilterToQuery<T>(this IQueryable<T> currentQuery, Dictionary<string, string> filters)
        {
            foreach (var item in filters)
            {
                var orderProperties = item.Value.Trim().Split(',').ToDictionary(k =>
                {
                    var paramName = k.Trim().Split(' ').ElementAt(0);
                    return paramName.Trim();
                },
                v =>
                {
                    var shortName = v.Split(' ').ElementAtOrDefault(1)?.ToLowerInvariant() ?? "asc";
                    return shortName == "asc" ? "OrderBy" : "OrderByDescending";
                });
                foreach (var orderBy in orderProperties)
                {
                    var property = typeof(T).GetProperty(orderBy.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (property == null)
                        continue;
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var propertyAccess = Expression.Property(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);
                    var methodCallExpression = Expression.Call(typeof(Queryable), orderBy.Value, new Type[] { typeof(T), property.PropertyType }, currentQuery.Expression, Expression.Quote(orderByExp));
                    currentQuery = currentQuery.Provider.CreateQuery<T>(methodCallExpression);
                }
            }
            return currentQuery;
        }
        private static IQueryable<T> AddMinMaxByFilterToQuery<T>(this IQueryable<T> currentQuery, Dictionary<string, string> filters)
        {
            var validFilters = filters
                .Where(f => typeof(T).GetProperties()
                    .Any(p => p.Name.Equals(f.Key.Replace(FilterAttributesConsts.MIN, string.Empty)
                                                 .Replace(FilterAttributesConsts.MAX, string.Empty), StringComparison.OrdinalIgnoreCase)))
                .ToDictionary(k => k.Key, v => v.Value);

            var groupedFilters = validFilters
                .GroupBy(f => f.Key.Replace(FilterAttributesConsts.MIN, string.Empty)
                                   .Replace(FilterAttributesConsts.MAX, string.Empty), StringComparer.OrdinalIgnoreCase);

            foreach (var group in groupedFilters)
            {
                var propertyName = group.Key;
                var property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null)
                    continue;

                var parameter = Expression.Parameter(typeof(T), "x");
                Expression? combinedExpression = null;

                foreach (var filter in group)
                {
                    var value = filter.Value;
                    if (string.IsNullOrEmpty(value))
                        continue;

                    var convertedValue = Convert.ChangeType(value, property.PropertyType);

                    if (convertedValue is DateTime dateValue)
                    {
                        if (filter.Key.Contains(FilterAttributesConsts.MAX))
                            convertedValue = DateTime.SpecifyKind(dateValue, DateTimeKind.Utc).AddDays(1);
                        else
                            convertedValue = DateTime.SpecifyKind(dateValue, DateTimeKind.Utc);
                    }

                    Expression comparison = filter.Key.Contains(FilterAttributesConsts.MIN)
                        ? Expression.GreaterThanOrEqual(
                            Expression.Property(parameter, property),
                            Expression.Constant(convertedValue))
                        : Expression.LessThan(
                            Expression.Property(parameter, property),
                            Expression.Constant(convertedValue));

                    combinedExpression = combinedExpression == null
                        ? comparison
                        : Expression.AndAlso(combinedExpression, comparison);
                }

                if (combinedExpression != null)
                {
                    var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
                    currentQuery = currentQuery.Where(lambda);
                }
            }

            return currentQuery;
        }
    }
}
