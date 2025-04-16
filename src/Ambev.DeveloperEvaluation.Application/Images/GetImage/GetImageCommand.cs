using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Images.GetImage
{
    public class GetImageCommand(string imageName) : IRequest<GetImageResult>
    {
        public string ImageName { get; set; } = imageName!;
    }
}
