using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using MongoDB.Bson;
using Ambev.DeveloperEvaluation.Common.Extensions;
using MongoDB.Bson.Serialization;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.GetOrder;

/// <summary>
/// Handler for processing GetOrderCommand requests
/// </summary>
public class GetOrderHandler : IRequestHandler<GetOrderCommand, GetOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetOrderHandler
    /// </summary>
    /// <param name="orderRepository">The order repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetOrderCommand</param>
    public GetOrderHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves an order by its ID with related customer, branch, and items information
    /// </summary>
    /// <param name="command">The command containing the order ID to retrieve</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The order details if found</returns>
    /// <exception cref="ValidationException">Thrown when the command fails validation</exception>
    /// <exception cref="InvalidOperationException">Thrown when the order cannot be found</exception>
    public async Task<GetOrderResult> Handle(GetOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new GetOrderCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var query = _orderRepository.Database
            .AsNoTracking()
            .Include(o => o.Customer)
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .Include(o => o.Branch);
        Order? order = null;

        if(command.Id != Guid.Empty)
            order = await query.FirstOrDefaultAsync(o => o.Id == command.Id, cancellationToken);
        else if (command.OrderNumber > 0)
            order = await query.FirstOrDefaultAsync(o => o.OrderNumber == command.OrderNumber, cancellationToken);

        if (order == null)
            throw new InvalidOperationException($"Order with ID {command.Id} not found");

        var dataResult = _mapper.Map<GetOrderResult>(order);
        return dataResult;
    }
}
