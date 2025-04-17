using Ambev.DeveloperEvaluation.Application.Orders.Commands.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.Notifications.CreateOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder
{
    public class CreateOrderProfile : Profile
    {
        public CreateOrderProfile()
        {
            CreateMap<CreateOrderRequest, CreateOrderCommand>();
            CreateMap<CreateOrderResult, CreateOrderResponse>();

            CreateMap<CreateOrderResponse, CreateOrderNotification>();

            CreateMap<CreateOrderItemRequest, CreateOrderItemCommand>();
            CreateMap<CreateOrderItemResult, CreateOrderItemResponse>();

            CreateMap<CreateOrderUserResult, CreateOrderUserResponse>();

            CreateMap<CreateOrderBranchResult, CreateOrderBranchResponse>();
        }
    }
}
