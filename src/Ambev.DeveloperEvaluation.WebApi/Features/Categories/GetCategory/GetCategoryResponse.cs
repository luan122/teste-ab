namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.GetCategory
{
    public class GetCategoryResponse
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
