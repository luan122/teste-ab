using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.WebApi.Common.Filter
{
    public class FilterRequest : Dictionary<string, string>
    {
    }
}
