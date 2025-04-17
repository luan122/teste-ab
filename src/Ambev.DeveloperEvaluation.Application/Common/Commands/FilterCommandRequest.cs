using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Common.Commands
{
    /// <summary>
    /// Represents a request for filtering, paging, and ordering data in queries.
    /// </summary>
    public record FilterCommandRequest
    {
        /// <summary>
        /// Gets or sets the current page number for pagination. Default value is 1.
        /// </summary>
        public int Page { get; set; } = 1!;

        /// <summary>
        /// Gets or sets the number of items per page. Default value is 10.
        /// </summary>
        public int Size { get; set; } = 10!;

        /// <summary>
        /// Gets or sets a dictionary of search parameters where the key is the property name
        /// and the value is the search term to filter by.
        /// </summary>
        public Dictionary<string, string> SearchParams { get; set; } = []!;

        /// <summary>
        /// Gets or sets a dictionary of minimum and maximum value parameters for range filtering,
        /// where the key is the property name prefixed with "_min" or "_max" and the value is the threshold value.
        /// </summary>
        public Dictionary<string, string> MinMaxParams { get; set; } = []!;

        /// <summary>
        /// Gets or sets a dictionary of ordering parameters where the key is the property name
        /// and the value contains the direction (asc/desc) for sorting.
        /// </summary>
        public Dictionary<string, string> OrderParams { get; set; } = []!;
    }
}
