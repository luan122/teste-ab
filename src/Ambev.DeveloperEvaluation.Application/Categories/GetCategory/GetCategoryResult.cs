using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Categories.GetCategory;

/// <summary>
/// Response model for GetCategory operation
/// </summary>
public class GetCategoryResult
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
