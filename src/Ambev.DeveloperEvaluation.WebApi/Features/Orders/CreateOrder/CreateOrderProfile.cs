﻿using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder
{
    public class CreateOrderProfile : Profile
    {
        public CreateOrderProfile()
        {
            CreateMap<CreateOrderRequest, CreateOrderCommand>();
            CreateMap<CreateOrderResult, CreateOrderResponse>();

            CreateMap<CreateOrderItemRequest, CreateOrderItemCommand>();
            CreateMap<CreateOrderItemResult, CreateOrderItemResponse>();

            CreateMap<CreateOrderUserResult, CreateOrderUserResponse>();

            CreateMap<CreateOrderBranchResult, CreateOrderBranchResponse>();
        }
    }
}
