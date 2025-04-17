using Ambev.DeveloperEvaluation.Application.Products.Shared;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Represents the response returned after successfully creating a new product.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created product,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateProductResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created product.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created product in the system.</value>
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty!;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty!;
    public string Category { get; set; } = string.Empty!;
    public string Image { get; set; } = string.Empty!;
    public ProductRateResult Rating { get; set; }
}
