using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.UpdateCategory
{
    /// <summary>
    /// API response model for CreateCategory operation
    /// </summary>
    public class UpdateCategoryResponse
    {
        /// <summary>
        /// The unique identifier of the created category
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The category's title
        /// </summary>
        public string Title { get; set; } = string.Empty;
    }
}
