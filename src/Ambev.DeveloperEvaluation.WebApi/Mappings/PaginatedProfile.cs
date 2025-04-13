using Ambev.DeveloperEvaluation.Application.Common.Commands;
using Ambev.DeveloperEvaluation.WebApi.Common.Paginated;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class PaginatedProfile : Profile
    {
        public PaginatedProfile()
        {
            CreateMap(typeof(PagedCommandResult<>), typeof(PaginatedList<>))
                .ConvertUsing(typeof(PaginatedTypeConverter<,>));
        }
    }
}
