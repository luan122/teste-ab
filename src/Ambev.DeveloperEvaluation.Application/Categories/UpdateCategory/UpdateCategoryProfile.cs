using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Categories.UpdateCategory;

/// <summary>
/// Profile for mapping between Category entity and CreateCategoryResponse
/// </summary>
public class UpdateCategoryProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateCategory operation
    /// </summary>
    public UpdateCategoryProfile()
    {
        CreateMap<Category, Category>();
        CreateMap<UpdateCategoryCommand, Category>();
        CreateMap<Category, UpdateCategoryResult>();
    }
}
