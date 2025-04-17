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
            var query = _orderRepository.Database;
            var order = new Order();

            if (command.Id != Guid.Empty)
                order = await query.FirstOrDefaultAsync(o => o.Id == command.Id, cancellationToken);
            else if (command.OrderNumber > 0)
                order = await query.FirstOrDefaultAsync(o => o.OrderNumber == command.OrderNumber, cancellationToken);

            if (order == null)
                throw new KeyNotFoundException($"Order with ID {command.Id} not found.");

            order.Cancel();
            var result = (await _orderRepository.SaveChangesAsync(cancellationToken)) > 0;
            return new CancelOrderResult() { Success = result, Message = result ? "Order cancelled successfully" : "Failed to cancel order" };
        }
    }
}
