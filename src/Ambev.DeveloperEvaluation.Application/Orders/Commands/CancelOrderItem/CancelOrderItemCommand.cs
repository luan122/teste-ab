using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrderItem
{

    /// <summary>
    /// Command for cancelling an existing order.
    /// </summary>
    /// <remarks>
    /// This command is used to request the cancellation of an existing order item
    /// by specifying either its unique identifier or order number.
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="CancelOrderItemResult"/> indicating the operation outcome.
    /// 
    /// The data provided in this command is validated using the 
    /// <see cref="CancelOrderItemCommandValidator"/> which ensures that either a valid OrderId
    /// or order number is provided, but not both simultaneously.
    /// </remarks>
    public class CancelOrderItemCommand : IRequest<CancelOrderItemResult>
    {
        /// <summary>
        /// The unique identifier of the order
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// The order number of the order
        /// </summary>
        public int OrderNumber { get; set; }

        /// <summary>
        /// The unique identifier of the order item to cancel
        /// </summary>
        public Guid OrderItemId { get; set; }

        /// <summary>
        /// The unique identifier of the user making the request
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Flag to indicate if the user can bypass rules
        /// </summary>
        public bool UserBypass { get; set; }

    }
}
