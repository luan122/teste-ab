using Ambev.DeveloperEvaluation.Application.Common.Commands;
using Ambev.DeveloperEvaluation.Application.Common.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Categories.ListCategory;

/// <summary>
/// Command for retrieving a <see cref="Category"/> by their ID
/// </summary>
public record ListCategoryCommand : FilterCommandRequest, IRequest<PagedCommandResult<ListCategoryResult>>
{
    public ListCategoryCommand(int page, int size)
    {
        Page = page;
        Size = size;
    }
    public ListCategoryCommand() { }
}
