using Ambev.DeveloperEvaluation.Application.Orders.Commands.GetOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetOrder
{
    public class GetOrderProfile : Profile
    {
        public GetOrderProfile()
        {
            CreateMap<GetOrderRequest, GetOrderCommand>();
            CreateMap<GetOrderResult, GetOrderResponse>();

            //CreateMap<GetOrderItemRequest, GetOrderItemCommand>();
            CreateMap<GetOrderItemResult, GetOrderItemResponse>();

            CreateMap<GetOrderUserResult, GetOrderUserResponse>();

            CreateMap<GetOrderBranchResult, GetOrderBranchResponse>();
        }
    }
}
