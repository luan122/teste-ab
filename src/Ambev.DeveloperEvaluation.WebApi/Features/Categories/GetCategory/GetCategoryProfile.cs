using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Categories.GetCategory;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.GetCategory;

/// <summary>
/// Profile for mapping GetCategory feature requests to commands
/// </summary>
public class GetCategoryProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCategory feature
    /// </summary>
    public GetCategoryProfile()
    {
        CreateMap<Guid, GetCategoryCommand>()
            .ConstructUsing(id => new GetCategoryCommand(id));

        CreateMap<GetCategoryResult, GetCategoryResponse>();
    }
}
