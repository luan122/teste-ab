using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Categories.GetCategory;

/// <summary>
/// Command for retrieving a <see cref="Category"/> by their ID
/// </summary>
public record GetCategoryCommand : IRequest<GetCategoryResult>
{
    /// <summary>
    /// The unique identifier of the <see cref="Category"/> to retrieve
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="GetCategoryCommand"/>
    /// </summary>
    /// <param name="id">The ID of the <see cref="Category"/> to retrieve</param>
    public GetCategoryCommand(Guid id)
    {
        Id = id;
    }
}
