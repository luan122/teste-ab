namespace Ambev.DeveloperEvaluation.Application.Categories.CreateCategory;

/// <summary>
/// Represents the response returned after successfully creating a new category.
/// </summary>
/// <remarks>
/// This response contains the newly created category,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateCategoryResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created category.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created category in the system.</value>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the newly created category.
    /// </summary>
    /// <value>A <see cref="string"/> that define the title of the created category in the system</value>
    public string Title { get; set; } = string.Empty!;
}
