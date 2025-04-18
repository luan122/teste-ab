using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.Notifications.CancelOrder
{
    public class CancelOrderNotification : INotification
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
    }
}
