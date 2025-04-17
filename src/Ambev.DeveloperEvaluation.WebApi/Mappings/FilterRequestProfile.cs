using Ambev.DeveloperEvaluation.Application.Categories.ListCategory;
using Ambev.DeveloperEvaluation.Application.Common.Commands;
using Ambev.DeveloperEvaluation.WebApi.Common.Filter;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    /// <summary>
    /// AutoMapper profile that configures mapping between query parameters dictionary and FilterCommandRequest.
    /// </summary>
    /// <remarks>
    /// Uses FilterParamsTypeConverter to transform HTTP query strings into structured filter commands.
    /// </remarks>
    public class FilterRequestProfile : Profile
    {
        public FilterRequestProfile()
        {
            CreateMap<Dictionary<string, string>, FilterCommandRequest>()
                .IncludeAllDerived()
                .ConvertUsing<FilterParamsTypeConverter>();
        }
    }
}
