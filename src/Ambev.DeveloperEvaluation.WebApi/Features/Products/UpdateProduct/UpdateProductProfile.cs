using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Common.Commands;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Common.Extensions;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductProfile : Profile
{
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductRequest, UpdateProductCommand>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(
                (src, dest) =>
                {
                    if (src.Image == null)
                        return null;
                    dest.Image ??= new();
                    dest.Image.File = src.Image;
                    dest.Image.ImageName = $"{StringSanitizeExtensions.Sanitize(src.Title)}{Path.GetExtension(src.Image.FileName)}";
                    return dest.Image;
                })
            );

        CreateMap<UpdateProductResult, UpdateProductResponse>();
    }
}
