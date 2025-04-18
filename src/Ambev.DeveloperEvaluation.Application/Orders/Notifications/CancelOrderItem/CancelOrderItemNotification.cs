using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.Notifications.CancelOrderItem
{
    public class CancelOrderItemNotification : INotification
    {
        public Guid OrderItemId { get; set; }
        public Guid UserId { get; set; }
    }
}
