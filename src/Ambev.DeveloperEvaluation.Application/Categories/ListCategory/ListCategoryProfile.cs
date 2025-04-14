using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Common.Commands;

namespace Ambev.DeveloperEvaluation.Application.Categories.ListCategory;

/// <summary>
/// Profile for mapping between Category entity and ListCategoryResponse
/// </summary>
public class ListCategoryProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListCategory operation
    /// </summary>
    public ListCategoryProfile()
    {
        CreateMap<Category, ListCategoryResult>();
        /*CreateMap(typeof(ListCategoryResult), typeof(PagedCommandResult<>))
            .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.TotalCount))*/
    }
}
