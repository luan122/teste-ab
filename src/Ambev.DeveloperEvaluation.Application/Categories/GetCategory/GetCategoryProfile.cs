using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Categories.GetCategory;

/// <summary>
/// Profile for mapping between Category entity and GetCategoryResponse
/// </summary>
public class GetCategoryProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCategory operation
    /// </summary>
    public GetCategoryProfile()
    {
        CreateMap<Category, GetCategoryResult>();
    }
}
