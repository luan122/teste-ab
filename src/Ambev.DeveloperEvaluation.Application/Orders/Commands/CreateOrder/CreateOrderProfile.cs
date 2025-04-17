using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Extensions;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using Ambev.DeveloperEvaluation.Application.Orders.Notifications.CreateOrder;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CreateOrder;

/// <summary>
/// Profile for mapping between Order entity and CreateOrderResponse
/// </summary>
public class CreateOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateOrder operation
    /// </summary>
    public CreateOrderProfile()
    {
        CreateMap<CreateOrderCommand, Order>();

        CreateMap<CreateOrderItemCommand, OrderItem>();

        CreateMap<Order, CreateOrderResult>()
            .ForMember(dest => dest.TotalOrderPrice, opt => opt.MapFrom(src => src.GetOrderTotalPrice()))
            .ForMember(dest => dest.TotalOrderPriceWithDiscount, opt => opt.MapFrom(src => src.GetOrderTotalPriceWithDiscount()));
        CreateMap<Order, CreateOrderNotification>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id));

        CreateMap<User, CreateOrderUserResult>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Username));

        CreateMap<Branch, CreateOrderBranchResult>();

        CreateMap<OrderItem, CreateOrderItemResult>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Title))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.DiscountedPrice, opt => opt.MapFrom(src => src.GetDiscountedPrice()))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.GetTotalPrice()))
            .ForMember(dest => dest.TotalPriceWithDiscount, opt => opt.MapFrom(src => src.GetITotalPriceWithDiscount()));

        CreateMap<Product, OrderItem>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Description, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        CreateMap<Product, Product>();
    }
}
