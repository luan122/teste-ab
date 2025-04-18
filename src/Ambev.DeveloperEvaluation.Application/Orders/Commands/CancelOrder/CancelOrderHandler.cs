using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrder
{
    public class CancelOrderHandler : IRequestHandler<CancelOrderCommand, CancelOrderResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public CancelOrderHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<CancelOrderResult> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
        {
            var query = _orderRepository.Database.AsQueryable();
            if (!command.UserBypass)
                query = query.Where(w => w.CustomerId == command.UserId);
            Order? order = null;

            if (command.Id != Guid.Empty)
                order = await query.FirstOrDefaultAsync(o => o.Id == command.Id && !o.IsCanceled, cancellationToken);
            else if (command.OrderNumber > 0)
                order = await query.FirstOrDefaultAsync(o => o.OrderNumber == command.OrderNumber && !o.IsCanceled, cancellationToken);

            if (order == null)
                throw new KeyNotFoundException($"Order with ID {command.Id} not found or is already canceled.");

            order.Cancel();
            var result = (await _orderRepository.SaveChangesAsync(cancellationToken)) > 0;
            return new CancelOrderResult() { Success = result, Message = result ? "Order cancelled successfully" : "Failed to cancel order", OrderId = order.Id };
        }
    }
}
