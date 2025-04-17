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
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

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
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var command = _mapper.Map<GetOrderCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);
            var dataResponse = _mapper.Map<GetOrderResponse>(response);
            return Ok(dataResponse, "Order retrieved successfully");
        }
    }
}
