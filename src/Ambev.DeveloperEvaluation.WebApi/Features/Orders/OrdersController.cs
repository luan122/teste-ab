using Ambev.DeveloperEvaluation.Application.Orders.Commands.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.Notifications.CreateOrder;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetOrder;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.Application.Orders.Commands.GetOrder;
using Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrder;
using FluentValidation;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrderItem;
using Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrderItem;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Application.Orders.Notifications.CancelOrder;
using Ambev.DeveloperEvaluation.Application.Orders.Notifications.CancelOrderItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders
{
    /// <summary>
    /// Controller for managing order operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of OrdersController
        /// </summary>
        /// <param name="mediator">The mediator instance</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public OrdersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="request">The order creation request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created order details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateOrderResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateOrderRequestValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var command = _mapper.Map<CreateOrderCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);
            var dataResponse = _mapper.Map<CreateOrderResponse>(response);
            var notificationRequest = _mapper.Map<CreateOrderResponse, CreateOrderNotification>(dataResponse, opt => opt.AfterMap((src, dest) => dest.OrderId = src.Id));
            await _mediator.Publish(notificationRequest, cancellationToken);
            return base.Created(string.Empty, new ApiResponseWithData<CreateOrderResponse>
            {
                Success = true,
                Message = "Order created successfully",
                Data = dataResponse
            });
        }

        /// <summary>
        /// Retrieve a Order by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the Order or the OrderNumber</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The Order details if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetOrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrder(string id, CancellationToken cancellationToken = default)
        {
            GetOrderRequest request;
            if (int.TryParse(id, out var idasInt))
                request = new GetOrderRequest { OrderNumber = idasInt };
            else if(Guid.TryParse(id, out var idasGuid))
                request = new GetOrderRequest { Id = idasGuid };
            else
                return BadRequest("Invalid ID format. Must be a valid Guid or Int");

            var validator = new GetOrderRequestValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var command = _mapper.Map<GetOrderCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);
            var dataResponse = _mapper.Map<GetOrderResponse>(response);
            return Ok(dataResponse, "Order retrieved successfully");
        }


        /// <summary>
        /// Cancels an existing order by its ID or order number
        /// </summary>
        /// <param name="id">The unique identifier or order number of the order to cancel</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the order was cancelled, or an error message if cancellation failed</returns>
        [HttpPut("{id}/Cancel")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelOrder(string id, CancellationToken cancellationToken = default)
        {
            CancelOrderRequest request;
            Enum.TryParse(typeof(UserRole), GetCurrentUserRole(), out var userRole);
            if (userRole == null)
                return BadRequest("Invalid user");

            bool canOverride = false;
            if (userRole is UserRole role)
            {
                canOverride = role == UserRole.Admin || role == UserRole.Manager;
            }

            if (int.TryParse(id, out var idasInt))
                request = new CancelOrderRequest { OrderNumber = idasInt, UserId = GetCurrentUserId(), UserBypass = canOverride };
            else if (Guid.TryParse(id, out var idasGuid))
                request = new CancelOrderRequest { Id = idasGuid, UserId = GetCurrentUserId(), UserBypass = canOverride };
            else
                return BadRequest("Invalid ID format. Must be a valid Guid or Int");

            var validator = new CancelOrderRequestValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var command = _mapper.Map<CancelOrderCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);
            if (response.Success)
            {
                var notificationRequest = new CancelOrderNotification() { OrderId = response.OrderId, UserId = request.UserId };
                await _mediator.Publish(notificationRequest, cancellationToken);
            }

            var resultData = _mapper.Map<ApiResponse>(response);
            return resultData.Success ?
                Ok(resultData.Message) :
                BadRequest(resultData);
        }

        /// <summary>
        /// Cancels an existing order by its ID or order number
        /// </summary>
        /// <param name="orderId">The unique identifier or order number of the order that owns the item</param>
        /// <param name="itemId">The unique identifier of the order item to cancel</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the order item was cancelled, or an error message if cancellation failed</returns>
        [HttpPut("{orderId}/Item/{itemId}/Cancel")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelOrder(string orderId, Guid itemId, CancellationToken cancellationToken = default)
        {
            CancelOrderItemRequest request;
            Enum.TryParse(typeof(UserRole), GetCurrentUserRole(), out var userRole);
            if(userRole == null)
                return BadRequest("Invalid user");

            bool canOverride = false;
            if(userRole is UserRole role)
            {
                canOverride = role == UserRole.Admin || role == UserRole.Manager;
            }

            if (int.TryParse(orderId, out var idasInt))
                request = new CancelOrderItemRequest { OrderNumber = idasInt, OrderItemId = itemId, UserId = GetCurrentUserId(), UserBypass = canOverride };
            else if (Guid.TryParse(orderId, out var idasGuid))
                request = new CancelOrderItemRequest { OrderId = idasGuid, OrderItemId = itemId, UserId = GetCurrentUserId(), UserBypass = canOverride };
            else
                return BadRequest("Invalid OrderID format. Must be a valid Guid or Int");

            var validator = new CancelOrderItemRequestValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var command = _mapper.Map<CancelOrderItemCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            if (response.Success)
            {
                var notificationRequest = new CancelOrderItemNotification() { OrderItemId = response.OrderItemId, UserId = request.UserId };
                await _mediator.Publish(notificationRequest, cancellationToken);
            }

            var resultData = _mapper.Map<ApiResponse>(response);
            return resultData.Success ?
                Ok(resultData.Message) :
                BadRequest(resultData);
        }

    }
}
