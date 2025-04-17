using Ambev.DeveloperEvaluation.Application.Common.Commands;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProduct
{
    public class ListProductProfile:Profile
    {
        public ListProductProfile()
        {
            CreateMap<ListProductResult, ListProductResponse>();
            CreateMap<FilterCommandRequest, ListProductCommand>();
        }
    }
}
