using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.Notifications.CreateOrder
{
    public class CreateOrderNotification : INotification
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
