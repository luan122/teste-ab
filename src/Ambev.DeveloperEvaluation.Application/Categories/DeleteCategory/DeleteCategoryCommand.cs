using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Categories.DeleteCategory;

/// <summary>
/// Command for deleting a category
/// </summary>
public record DeleteCategoryCommand : IRequest<DeleteCategoryResponse>
{
    /// <summary>
    /// The unique identifier of the category to delete
    /// </summary>
    public Guid Id { get; }
    /// <summary>
    /// Initializes a new instance of <see cref="DeleteCategoryCommand"/>
    /// </summary>
    /// <param name="id">The ID of the <see cref="Category"/> to delete</param>
    public DeleteCategoryCommand(Guid id)
    {
        Id = id;
    }
}
