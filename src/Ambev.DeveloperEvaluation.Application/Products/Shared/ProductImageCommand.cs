using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.Shared
{
    public class ProductImageCommand
    {
        public string ImageName { get; set; }
        public IFormFile File { get; set; }
    }
}
