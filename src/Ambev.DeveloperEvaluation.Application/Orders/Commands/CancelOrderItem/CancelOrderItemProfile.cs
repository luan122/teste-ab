using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrderItem
{
    public class CancelOrderItemProfile : Profile
    {
        public CancelOrderItemProfile()
        {
            CreateMap<bool, CancelOrderItemResult>()
                .ForMember(dest => dest.Success, opt => opt.MapFrom(src => src));
        }
    }
}
