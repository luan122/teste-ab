using Ambev.DeveloperEvaluation.Application.Categories.ListCategory;
using Ambev.DeveloperEvaluation.Application.Common.Commands;
using Ambev.DeveloperEvaluation.WebApi.Common.Filter;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class FilterRequestProfile : Profile
    {
        public FilterRequestProfile()
        {
            CreateMap<Dictionary<string, string>, FilterCommandRequest>()
                .IncludeAllDerived()
                .ConvertUsing<FilterParamsTypeConverter>();

            CreateMap<FilterCommandRequest, ListCategoryCommand>();
        }
    }
}
