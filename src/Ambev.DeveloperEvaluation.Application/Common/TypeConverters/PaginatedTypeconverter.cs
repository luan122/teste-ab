using Ambev.DeveloperEvaluation.Application.Common.Results;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Common.TypeConverters
{
    public class PaginatedTypeConverter<TSource, TDestination> : ITypeConverter<PagedCommandResult<TSource>, PagedCommandResult<TDestination>>
    {
        public PagedCommandResult<TDestination> Convert(
            PagedCommandResult<TSource> source,
            PagedCommandResult<TDestination> destination,
            ResolutionContext context)
        {
            var result = new PagedCommandResult<TDestination>(
                context.Mapper.Map<List<TSource>, List<TDestination>>(source)){ TotalCount = source.TotalCount, CurrentPage = source.CurrentPage, PageSize = source.PageSize };
            return result;
        }
    }
}
