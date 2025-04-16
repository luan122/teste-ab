using Ambev.DeveloperEvaluation.Application.Products.Shared;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared
{
    public class ProductRateProfile : Profile
    {
        public ProductRateProfile()
        {
            CreateMap<ProductRateRequest, ProductRateCommand>();
            CreateMap<ProductRateResult, ProductRateResponse>();
        }
    }
}
