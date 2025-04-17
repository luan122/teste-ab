using Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductRequest
{
    public string Title { get; set; }
    public decimal? Price { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public IFormFile? Image { get; set; }
    public ProductRateRequest? Rating { get; set; }
}

