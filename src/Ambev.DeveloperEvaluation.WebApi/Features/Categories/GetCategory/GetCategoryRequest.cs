namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.GetCategory
{
    /// <summary>
    /// Request model for getting a category by ID
    /// </summary>
    public class GetCategoryRequest
    {
        /// <summary>
        /// The unique identifier of the category to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}