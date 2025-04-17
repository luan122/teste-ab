using Ambev.DeveloperEvaluation.Application.Common.Results;
using Ambev.DeveloperEvaluation.WebApi.Common.Paginated;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    /// <summary>
    /// AutoMapper profile that configures mapping between PagedCommandResult and PaginatedList.
    /// </summary>
    /// <remarks>
    /// Uses <see cref="PaginatedTypeConverter{T,T}"/> to handle the conversion between internal and API pagination models.
    /// </remarks>
    public class PaginatedProfile : Profile
    {
        public PaginatedProfile()
        {
            CreateMap(typeof(PagedCommandResult<>), typeof(PaginatedList<>))
                .ConvertUsing(typeof(PaginatedTypeConverter<,>));
        }
    }
}
