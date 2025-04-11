using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Categories.CreateCategory;

/// <summary>
/// Profile for mapping between Category entity and CreateCategoryResponse
/// </summary>
public class CreateCategoryProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateCategory operation
    /// </summary>
    public CreateCategoryProfile()
    {
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<Category, CreateCategoryResult>();
    }
}
