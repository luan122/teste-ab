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

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CreateOrder;

/// <summary>
/// Handler for processing CreateOrderCommand requests
/// </summary>
public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ICreateOrderDomainService _createOrderDomainService;

    /// <summary>
    /// Initializes a new instance of CreateOrderHandler
    /// </summary>
    /// <param name="orderRepository">The order repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for CreateOrderCommand</param>
    public CreateOrderHandler(IOrderRepository orderRepository, IMapper mapper, ICreateOrderDomainService createOrderDomainService)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _createOrderDomainService = createOrderDomainService;
    }

    /// <summary>
    /// Handles the CreateOrderCommand request
    /// </summary>
    /// <param name="command">The CreateOrder command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created order details</returns>
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateOrderCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var order = _mapper.Map<Order>(command);

        await _orderRepository.AddAsync(order, cancellationToken);
        var saveResult = await _orderRepository.SaveChangesAsync(cancellationToken);
        if(saveResult < 1)
            throw new InvalidOperationException("Error while saving order");
        order = await _orderRepository.Database.AsNoTracking().Include(o => o.Customer).Include(o => o.Items).ThenInclude(i => i.Product).Include(o => o.Branch).FirstAsync(f => f.Id == order.Id, cancellationToken);

        var dataResult = _mapper.Map<CreateOrderResult>(order);
        return dataResult;
    }
}
