using Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrder;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrder
{
    public class CancelOrderProfile : Profile
    {
        public CancelOrderProfile()
        {

            CreateMap<CancelOrderResult, ApiResponse>();
            CreateMap<CancelOrderRequest,CancelOrderCommand>();
        }
    }
}
