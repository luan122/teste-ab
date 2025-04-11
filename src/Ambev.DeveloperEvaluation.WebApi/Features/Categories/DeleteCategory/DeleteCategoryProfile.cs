using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Categories.DeleteCategory;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.DeleteCategory;

/// <summary>
/// Profile for mapping DeleteCategory feature requests to commands
/// </summary>
public class DeletecategoryProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteCategory feature
    /// </summary>
    public DeletecategoryProfile()
    {
        CreateMap<Guid, DeleteCategoryCommand>()
            .ConstructUsing(id => new DeleteCategoryCommand(id));
    }
}
