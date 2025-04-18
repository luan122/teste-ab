using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.Notifications.CancelOrder
{
    public class CancelOrderNotificationHandler(IOrderRepository orderRepository, ILogger<CancelOrderNotificationHandler> logger) : INotificationHandler<CancelOrderNotification>
    {
        public async Task Handle(CancelOrderNotification notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Order Canceled: {OrderId} by user {UserId}", notification.OrderId, notification.UserId);
            return;
        }
    }
}
