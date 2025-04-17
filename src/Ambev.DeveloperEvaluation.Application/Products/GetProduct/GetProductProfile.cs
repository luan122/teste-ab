using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Common.TypeConverters;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Profile for mapping between Product entity and GetProductResponse
/// </summary>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProduct operation
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<Product, GetProductResult>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Title))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating));
    }
}
