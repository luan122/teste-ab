using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.UpdateCategory
{
    /// <summary>
    /// Represents a request to create a new Category in the system.
    /// </summary>
    public class UpdateCategoryRequest
    {
        [BindNever, SwaggerSchema(ReadOnly = true)]
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the Title. Must be unique and contain only valid characters.
        /// </summary>
        public string Title { get; set; } = string.Empty!;
    }
}
