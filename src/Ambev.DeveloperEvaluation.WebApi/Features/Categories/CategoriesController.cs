using Ambev.DeveloperEvaluation.Application.Categories.CreateCategory;
using Ambev.DeveloperEvaluation.Application.Categories.DeleteCategory;
using Ambev.DeveloperEvaluation.Application.Categories.GetCategory;
using Ambev.DeveloperEvaluation.Application.Categories.ListCategory;
using Ambev.DeveloperEvaluation.Application.Categories.UpdateCategory;
using Ambev.DeveloperEvaluation.Application.Common.Commands;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Common.Filter;
using Ambev.DeveloperEvaluation.WebApi.Common.Paginated;
using Ambev.DeveloperEvaluation.WebApi.Features.Categories.CreateCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Categories.DeleteCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Categories.GetCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Categories.ListCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Categories.UpdateCategory;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories
{
    /// <summary>
    /// Controller for managing categories operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of CategoriesController
        /// </summary>
        /// <param name="mediator">The mediator instance</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public CategoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        /// <summary>
        /// Get a paged list of categorys
        /// </summary>
        /// <param name="filters">Filters sent on query string</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the category was found using filters criteria</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<GetCategoryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories([FromQuery] FilterRequest filters, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<ListCategoryCommand>(_mapper.Map<FilterCommandRequest>(filters));
            var response = await _mediator.Send(command, cancellationToken);
            var result = _mapper.Map<PaginatedList<ListCategoryResponse>>(response);
            return OkPaginated(result, "Categories retrieved successfully");
        }

        /// <summary>
        /// Retrieves a category by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the category</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The category details if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetCategoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCategory(Guid id, CancellationToken cancellationToken = default)
        {
            var request = new GetCategoryRequest { Id = id };
            var validator = new GetCategoryRequestValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var command = _mapper.Map<GetCategoryCommand>(id);
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(_mapper.Map<GetCategoryResponse>(response), "Category retrieved successfully");
        }

        /// <summary>
        /// Creates a new category
        /// </summary>
        /// <param name="request">The category creation request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created category details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateCategoryResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request, CancellationToken cancellationToken = default)
        {
            var validator = new CreateCategoryRequestValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var command = _mapper.Map<CreateCategoryCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateCategoryResponse>
            {
                Success = true,
                Message = "Category created successfully",
                Data = _mapper.Map<CreateCategoryResponse>(response)
            });
        }

        /// <summary>
        /// Update a category by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the category to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the category was deleted</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateCategoryResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCategory([FromRoute] Guid id, [FromBody]UpdateCategoryRequest request, CancellationToken cancellationToken = default)
        {
            var validator = new UpdateCategoryRequestValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var command = _mapper.Map<UpdateCategoryCommand>(request, opt => opt.AfterMap((src, dest) => dest.Id = id));
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(_mapper.Map<UpdateCategoryResponse>(response), "Category updated successfully");
        }

        /// <summary>
        /// Deletes a category by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the category to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the category was deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteCategoryRequest { Id = id };
            var validator = new DeleteCategoryRequestValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var command = _mapper.Map<DeleteCategoryCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);

            return Ok("Category deleted successfully");
        }
    }
}
