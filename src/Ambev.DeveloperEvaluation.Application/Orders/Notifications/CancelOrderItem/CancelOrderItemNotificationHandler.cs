using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.Notifications.CancelOrderItem
{
    public class CancelOrderItemNotificationHandler(IOrderRepository orderRepository, ILogger<CancelOrderItemNotificationHandler> logger) : INotificationHandler<CancelOrderItemNotification>
    {
        public async Task Handle(CancelOrderItemNotification notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Order Item Canceled: {OrderItemId} by user {UserId}", notification.OrderItemId, notification.UserId);
            return;
        }
    }
}
