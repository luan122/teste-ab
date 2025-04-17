using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Images.GetImage
{
    public class GetImageResult
    {
        public bool Success { get; set; }
        public byte[]? FileBytes { get; set; }
        public string ContentType { get; set; }
    }
}
