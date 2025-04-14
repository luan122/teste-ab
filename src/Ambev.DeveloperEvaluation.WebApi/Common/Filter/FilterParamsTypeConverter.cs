using Ambev.DeveloperEvaluation.Application.Common.Commands;
using Ambev.DeveloperEvaluation.Common.Consts;
using AutoMapper;
using Microsoft.VisualBasic;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.WebApi.Common.Filter
{
    public class FilterParamsTypeConverter : ITypeConverter<Dictionary<string, string>, FilterCommandRequest>
    {
        public FilterCommandRequest Convert(Dictionary<string, string> source, FilterCommandRequest destination, ResolutionContext context)
        {
            var minMaxValues = source.Where(k => k.Key.StartsWith(FilterAttributesConsts.MIN) || k.Key.StartsWith(FilterAttributesConsts.MAX)).ToDictionary(k => k.Key, v => v.Value);
            var orderValues = source.Where(k => k.Key.StartsWith(FilterAttributesConsts.ORDER)).ToDictionary(k => k.Key, v => v.Value);
            var searchValues = source.Where(k => !FilterAttributesConsts.FILTERS.Contains(k.Key)).ToDictionary(k => k.Key, v => v.Value);

            int page = source.TryGetValue(FilterAttributesConsts.PAGE, out string? pageValue) && int.TryParse(pageValue, out int parsedPage) ? parsedPage : 1;
            int size = source.TryGetValue(FilterAttributesConsts.SIZE, out string? sizeValue) && int.TryParse(sizeValue, out int parsedSize) ? parsedSize : 10;
            destination = new FilterCommandRequest
            {
                Page = page,
                Size = size,
                SearchParams = searchValues,
                MinMaxParams = minMaxValues,
                OrderParams = orderValues
            };


            return destination;
        }
    }
}
