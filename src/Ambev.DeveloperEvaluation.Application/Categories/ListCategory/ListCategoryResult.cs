using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Categories.ListCategory;

/// <summary>
/// Response model for ListCategory operation
/// </summary>
public class ListCategoryResult
{
    /// <summary>
    /// The unique identifier of the category
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The category's title
    /// </summary>
    public string Title { get; set; } = string.Empty;
}
