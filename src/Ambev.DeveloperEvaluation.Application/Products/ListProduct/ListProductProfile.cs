using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Common.Commands;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct;

/// <summary>
/// Profile for mapping between Product entity and ListProductResponse
/// </summary>
public class ListProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListProduct operation
    /// </summary>
    public ListProductProfile()
    {
        CreateMap<Product, ListProductResult>();
        CreateMap<Product, Product>();
    }
}
