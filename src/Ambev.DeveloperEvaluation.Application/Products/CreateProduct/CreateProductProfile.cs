using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Extensions;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Profile for mapping between Product entity and CreateProductResponse
/// </summary>
public class CreateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateProduct operation
    /// </summary>
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Product>()
             .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.ImageName))
             .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category() { Title = src.Category }));
        CreateMap<Product, CreateProductResult>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Title))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating));
    }
}
