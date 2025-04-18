using Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrderItem;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrderItem
{
    public class CancelOrderItemProfile : Profile
    {
        public CancelOrderItemProfile()
        {

            CreateMap<CancelOrderItemResult, ApiResponse>();
            CreateMap<CancelOrderItemRequest,CancelOrderItemCommand>();
        }
    }
}
