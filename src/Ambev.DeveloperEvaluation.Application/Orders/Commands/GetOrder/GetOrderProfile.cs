using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Extensions;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.GetOrder;

/// <summary>
/// Profile for mapping between Order entity and GetOrderResponse
/// </summary>
public class GetOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetOrder operation
    /// </summary>
    public GetOrderProfile()
    {
        CreateMap<GetOrderCommand, Order>();

        CreateMap<Order, GetOrderResult>()
            .ForMember(dest => dest.TotalOrderPrice, opt => opt.MapFrom(src => src.GetOrderTotalPrice()))
            .ForMember(dest => dest.TotalOrderPriceWithDiscount, opt => opt.MapFrom(src => src.GetOrderTotalPriceWithDiscount()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.CreatedAt)));

        CreateMap<User, GetOrderUserResult>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Username));

        CreateMap<Branch, GetOrderBranchResult>();

        CreateMap<OrderItem, GetOrderItemResult>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Title))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.DiscountedPrice, opt => opt.MapFrom(src => src.GetDiscountedPrice()))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.GetTotalPrice()))
            .ForMember(dest => dest.TotalPriceWithDiscount, opt => opt.MapFrom(src => src.GetITotalPriceWithDiscount()));
    }
}
