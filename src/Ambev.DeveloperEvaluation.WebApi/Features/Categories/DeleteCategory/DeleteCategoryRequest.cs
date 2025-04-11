namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.DeleteCategory;

/// <summary>
/// Request model for deleting a category
/// </summary>
public class DeleteCategoryRequest
{
    /// <summary>
    /// The unique identifier of the category to delete
    /// </summary>
    public Guid Id { get; set; }
}
