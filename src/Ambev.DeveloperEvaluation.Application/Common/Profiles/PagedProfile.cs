using Ambev.DeveloperEvaluation.Application.Common.Commands;
using Ambev.DeveloperEvaluation.Application.Common.TypeConverters;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Common.Profiles
{
    public class PagedProfile : Profile
    {
        public PagedProfile()
        {
            CreateMap(typeof(PagedCommandResult<>), typeof(PagedCommandResult<>))
                .ConvertUsing(typeof(PaginatedTypeConverter<,>));
        }
    }
}
