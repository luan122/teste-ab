using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Common.Commands;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Common.Filter;
using Ambev.DeveloperEvaluation.WebApi.Common.Paginated;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    /// <summary>
    /// Controller for managing products operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of ProductsController
        /// </summary>
        /// <param name="mediator">The mediator instance</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="request">The product creation request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created product details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromForm]CreateProductRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateProductCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);
            var dataResponse = _mapper.Map<CreateProductResponse>(response);
            return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
            {
                Success = true,
                Message = "Product created successfully",
                Data = dataResponse
            });
        }

    }
}
