using Ambev.DeveloperEvaluation.Application.Common.Commands;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Common.Paginated
{
    public class PaginatedTypeConverter<TSource, TDestination>
    : ITypeConverter<PagedCommandResult<TSource>, PaginatedList<TDestination>>
    {
        public PaginatedList<TDestination> Convert(
            PagedCommandResult<TSource> source,
            PaginatedList<TDestination> destination,
            ResolutionContext context)
        {
            var result = new PaginatedList<TDestination>(
                context.Mapper.Map<List<TSource>, List<TDestination>>(source), source.TotalCount, source.CurrentPage, source.PageSize);
            return result;
        }
    }
}
