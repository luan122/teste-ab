namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.ListCategory
{
    public class ListCategoryResponse
    {
        /// <summary>
        /// The unique identifier of the category
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The category title
        /// </summary>
        public string Title { get; set; } = string.Empty;
    }
}
