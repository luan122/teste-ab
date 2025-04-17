using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.Notifications.CreateOrder
{
    public class CreateOrderNotificationHandler(IOrderRepository orderRepository, ILogger<CreateOrderNotificationHandler> logger) : INotificationHandler<CreateOrderNotification>
    {
        public async Task Handle(CreateOrderNotification notification, CancellationToken cancellationToken)
        {
            var order = await orderRepository.Database.AsNoTracking().Include(o => o.Customer).FirstOrDefaultAsync(fd => fd.Id == notification.OrderId);
            if(order == null)
            {
                logger.LogWarning("Order with ID {OrderId} not found", notification.OrderId);
                return;
            }
            logger.LogInformation("Order created: {OrderId} for customer {CustomerId}", order.Id, order.CustomerId);
            return;
        }
    }
}
