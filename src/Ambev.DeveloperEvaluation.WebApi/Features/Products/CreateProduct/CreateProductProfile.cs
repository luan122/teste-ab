using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Common.Extensions;
using Ambev.DeveloperEvaluation.Application.Products.Shared;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(
                (src, dest) =>
                {
                    if (src.Image == null)
                        return null;
                    dest.Image ??= new ProductImageCommand();
                    dest.Image.File = src.Image;
                    dest.Image.ImageName = $"{StringSanitizeExtensions.Sanitize(src.Title)}{Path.GetExtension(src.Image.FileName)}";
                    return dest.Image;
                })
            );

        CreateMap<CreateProductResult, CreateProductResponse>();
    }
}
