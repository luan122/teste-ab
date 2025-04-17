using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Common.Results
{
    /// <summary>
    /// Represents a paginated result set that inherits from List<T>, containing items for the current page
    /// and pagination metadata.
    /// </summary>
    /// <typeparam name="T">The type of elements in the paginated list.</typeparam>
    public class PagedCommandResult<T> : List<T>
    {
        /// <summary>
        /// Gets or sets the current page number of the result set.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total count of items across all pages.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedCommandResult{T}"/> class.
        /// </summary>
        public PagedCommandResult() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedCommandResult{T}"/> class with the specified items.
        /// </summary>
        /// <param name="items">The collection whose elements are copied to the new list.</param>
        public PagedCommandResult(IEnumerable<T> items)
        {
            AddRange(items);
        }
    }
}
