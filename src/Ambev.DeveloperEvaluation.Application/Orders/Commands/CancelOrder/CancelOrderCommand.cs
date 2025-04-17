using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrder
{

    /// <summary>
    /// Command for cancelling an existing order.
    /// </summary>
    /// <remarks>
    /// This command is used to request the cancellation of an existing order
    /// by specifying either its unique identifier or order number.
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="CancelOrderResult"/> indicating the operation outcome.
    /// 
    /// The data provided in this command is validated using the 
    /// <see cref="CancelOrderCommandValidator"/> which ensures that either a valid ID
    /// or order number is provided, but not both simultaneously.
    /// </remarks>
    public class CancelOrderCommand : IRequest<CancelOrderResult>
    {
        /// <summary>
        /// The unique identifier of the order to cancel
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The order number of the order to cancel
        /// </summary>
        public int OrderNumber { get; set; }
    }
}
