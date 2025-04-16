using Ambev.DeveloperEvaluation.Application.Images.GetImage;
using Ambev.DeveloperEvaluation.WebApi.Common;
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
