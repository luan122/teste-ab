namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.CreateCategory
{
    /// <summary>
    /// Represents a request to create a new Category in the system.
    /// </summary>
    public class CreateCategoryRequest
    {
        /// <summary>
        /// Gets or sets the Title. Must be unique and contain only valid characters.
        /// </summary>
        public string Title { get; set; } = string.Empty!;
    }
}
