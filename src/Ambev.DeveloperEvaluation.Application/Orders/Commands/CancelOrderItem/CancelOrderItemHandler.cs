using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrderItem
{
    public class CancelOrderItemHandler : IRequestHandler<CancelOrderItemCommand, CancelOrderItemResult>
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;
        public CancelOrderItemHandler(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }
        public async Task<CancelOrderItemResult> Handle(CancelOrderItemCommand command, CancellationToken cancellationToken)
        {
            var query = _orderItemRepository.Database.Include(oi => oi.Order).AsQueryable();
            if (!command.UserBypass)
                query = query.Where(w => w.Order.Customer.Id == command.UserId);
            OrderItem? orderItem = null;
            bool isOrderNumber = false;

            if (command.OrderId != Guid.Empty)
                orderItem = await query.FirstOrDefaultAsync(o => o.Order.Id == command.OrderId && o.Id == command.OrderItemId && !o.Order.IsCanceled, cancellationToken);
            else if (command.OrderNumber > 0)
            {
                orderItem = await query.FirstOrDefaultAsync(o => o.Order.OrderNumber == command.OrderNumber && !o.Order.IsCanceled, cancellationToken);
                isOrderNumber = true;
            }

            if (orderItem == null) {
                var orderIdentifierMsg = isOrderNumber ? $"orderNumber {command.OrderNumber}" : $"ID {command.OrderId}";
                throw new KeyNotFoundException($"Order item with ID {command.OrderItemId} and order with {orderIdentifierMsg} not found or is already canceled.");
            }

            orderItem.Cancel();
            var result = (await _orderItemRepository.SaveChangesAsync(cancellationToken)) > 0;
            return new CancelOrderItemResult() { Success = result, Message = result ? "Order item cancelled successfully" : "Failed to cancel order item", OrderItemId = orderItem.Id };
        }
    }
}
