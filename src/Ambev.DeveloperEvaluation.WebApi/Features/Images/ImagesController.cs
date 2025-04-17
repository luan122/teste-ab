using Ambev.DeveloperEvaluation.Application.Images.GetImage;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Categories.GetCategory;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Images
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ImagesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves an image file by its name.
        /// </summary>
        /// <param name="imageName">The name of the image file to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>
        /// The file content result with the appropriate content type if found;
        /// Otherwise, returns a 404 Not Found response.
        /// </returns>
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("{imageName}")]
        public async Task<IActionResult> GetImage(string imageName, CancellationToken cancellationToken)
        {
            var request = new GetImageCommand(imageName);
            var response = await _mediator.Send(request, cancellationToken);
            if (response.Success)
                return File(response.FileBytes, response.ContentType);
            return NotFound();
        }
    }
}
