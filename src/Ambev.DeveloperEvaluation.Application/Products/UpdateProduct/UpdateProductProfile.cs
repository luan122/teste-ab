using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Profile for mapping between Product entity and UpdateProductResponse
/// </summary>
public class UpdateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateProduct operation
    /// </summary>
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductCommand, Product>()
             .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category == default ? null : new Category() { Title = src.Category, UpdatedAt = null, IsDeleted = false }))
             .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image == null ? null : src.Image.ImageName))
             .ForAllMembers(o =>
             {
                 o.AllowNull();
                 o.Condition((src, dest, value) => value != null);
             });

        CreateMap<Product, UpdateProductResult>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Title))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating));


    }
}
