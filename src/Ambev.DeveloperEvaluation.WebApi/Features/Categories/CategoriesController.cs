using AgileObjects.ReadableExpressions;
using Ambev.DeveloperEvaluation.Application.Categories.CreateCategory;
using Ambev.DeveloperEvaluation.Application.Categories.DeleteCategory;
using Ambev.DeveloperEvaluation.Application.Categories.GetCategory;
using Ambev.DeveloperEvaluation.Application.Categories.ListCategory;
using Ambev.DeveloperEvaluation.Application.Common.Commands;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Common.Filter;
using Ambev.DeveloperEvaluation.WebApi.Common.Paginated;
using Ambev.DeveloperEvaluation.WebApi.Features.Categories.CreateCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Categories.DeleteCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Categories.GetCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Categories.ListCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        /// <summary>
        /// Get a paged list of categorys
        /// </summary>
        /// <param name="filters">Filters sent on query string<br/>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the category was deleted</returns>
        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] FilterRequest filters, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<ListCategoryCommand>(_mapper.Map<FilterCommandRequest>(filters));
            var response = await _mediator.Send(command, cancellationToken);
            var result = _mapper.Map<PaginatedList<ListCategoryResponse>>(response);
            return OkPaginated(result, "Categories retrieved successfully");
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetCategoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCategory(Guid id, CancellationToken cancellationToken = default)
        {
            var request = new GetCategoryRequest { Id = id };
            var validator = new GetCategoryRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var command = _mapper.Map<GetCategoryCommand>(id);
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(_mapper.Map<GetCategoryResponse>(response), "Category retrieved successfully");
        }
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateCategoryResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request, CancellationToken cancellationToken = default)
        {
            var validator = new CreateCategoryRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

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
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteCategoryCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Category deleted successfully"
            });
        }
    }
}
