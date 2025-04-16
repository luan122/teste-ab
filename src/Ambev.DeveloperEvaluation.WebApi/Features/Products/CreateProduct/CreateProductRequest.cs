﻿using Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    public class CreateProductRequest
    {
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public IFormFile Image { get; set; }
        public ProductRateRequest Rating { get; set; }
    }
}
