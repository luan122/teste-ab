using Ambev.DeveloperEvaluation.Application.Common.TypeConverters;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.Shared
{
    public class ProductRateProfile : Profile
    {
        public ProductRateProfile()
        {
            CreateMap<ProductRate, ProductRateResult>();
            CreateMap<ProductRateCommand, ProductRate>();
        }
    }
}
