using Ambev.DeveloperEvaluation.Application.Products.Shared;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct;

/// <summary>
/// Response model for ListProduct operation
/// </summary>
public class ListProductResult
{
    /// <summary>
    /// The unique identifier of the product
    /// </summary>
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Product { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public ProductRateResult Rating { get; set; }
}
