using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrder
{
    public class CancelOrderProfile : Profile
    {
        public CancelOrderProfile()
        {
            CreateMap<bool, CancelOrderResult>()
                .ForMember(dest => dest.Success, opt => opt.MapFrom(src => src));
        }
    }
}
