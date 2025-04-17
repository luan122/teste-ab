using Ambev.DeveloperEvaluation.Application.Common.Commands;
using Ambev.DeveloperEvaluation.Application.Common.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct;

/// <summary>
/// Command for retrieving a <see cref="Product"/> by their ID
/// </summary>
public record ListProductCommand : FilterCommandRequest, IRequest<PagedCommandResult<ListProductResult>>
{
    public ListProductCommand(int page, int size)
    {
        Page = page;
        Size = size;
    }
    public ListProductCommand() { }
}
