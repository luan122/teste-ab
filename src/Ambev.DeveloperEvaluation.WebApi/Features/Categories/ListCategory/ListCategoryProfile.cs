using Ambev.DeveloperEvaluation.Application.Categories.ListCategory;
using Ambev.DeveloperEvaluation.Application.Common.Commands;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.ListCategory;

/// <summary>
/// Profile for mapping ListCategory feature requests to commands
/// </summary>
public class ListCategoryProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListCategory feature
    /// </summary>
    public ListCategoryProfile()
    {
        CreateMap<ListCategoryResult, ListCategoryResponse>();

        CreateMap<FilterCommandRequest, ListCategoryCommand>();
    }
}
