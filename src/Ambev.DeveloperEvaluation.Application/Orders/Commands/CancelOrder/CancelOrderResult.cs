using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrder
{
    /// <summary>
    /// Represents the response returned after attempting to cancel an order.
    /// </summary>
    /// <remarks>
    /// This response contains the operation result and a descriptive message about the outcome,
    /// which can be used to determine if the cancellation was successful.
    /// </remarks>
    public class CancelOrderResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the order cancellation operation was successful.
        /// </summary>
        /// <value>A boolean value that is true if the order was successfully cancelled; otherwise, false.</value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets a message providing additional information about the cancellation result.
        /// </summary>
        /// <value>A string containing a descriptive message about the cancellation operation outcome.</value>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// The unique identifier of the canceled order
        /// </summary>
        public Guid OrderId { get; set; }
    }
}
