using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Common.Commands;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Common.Filter;
using Ambev.DeveloperEvaluation.WebApi.Common.Paginated;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProduct;
using Microsoft.AspNetCore.Authorization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    /// <summary>
    /// Controller for managing products operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        /// <summary>
        /// Update a product by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the product to update</param>
        /// <param name="request">The product update request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the product was deleted</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutProduct([FromRoute] Guid id, [FromForm] UpdateProductRequest request, CancellationToken cancellationToken = default)
        {
            var validator = new UpdateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateProductCommand>(request, opt => opt.AfterMap((src, dest) => dest.Id = id));
            var response = await _mediator.Send(command, cancellationToken);
            var dataResponse = _mapper.Map<UpdateProductResponse>(response);
            return Ok(dataResponse, "Product updated successfully");
        }

        /// <summary>
        /// Delete a product by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the product was deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteProductRequest { Id = id };
            var validator = new DeleteProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteProductCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Product deleted successfully"
            });
        }


        /// <summary>
        /// Retrieve a product by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The product details if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProduct(Guid id, CancellationToken cancellationToken = default)
        {
            var request = new GetProductRequest { Id = id };
            var validator = new GetProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var command = _mapper.Map<GetProductCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);
            var dataResponse = _mapper.Map<GetProductResponse>(response);
            return Ok(dataResponse, "Product retrieved successfully");
        }

        /// <summary>
        /// Retrieve a paged list of products
        /// </summary>
        /// <param name="filters">Filters sent on query string<br/></param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the product was found using filters criteria</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<GetProductResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts([FromQuery] FilterRequest filters, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<ListProductCommand>(_mapper.Map<FilterCommandRequest>(filters));
            var response = await _mediator.Send(command, cancellationToken);
            var dataResponse = _mapper.Map<PaginatedList<ListProductResponse>>(response);
            return OkPaginated(dataResponse, "Products retrieved successfully");
        }
    }
}
