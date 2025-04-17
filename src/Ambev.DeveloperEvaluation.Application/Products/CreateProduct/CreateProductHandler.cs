using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using MongoDB.Bson;
using Ambev.DeveloperEvaluation.Common.Extensions;
using MongoDB.Bson.Serialization;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Handler for processing CreateProductCommand requests
/// </summary>
public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly INoSqlRepository<BsonDocumentBackedClass> _noSqlRepository;

    /// <summary>
    /// Initializes a new instance of CreateProductHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for CreateProductCommand</param>
    public CreateProductHandler(IProductRepository productRepository, IMapper mapper, INoSqlRepository<BsonDocumentBackedClass> noSqlRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _noSqlRepository = noSqlRepository;
    }

    /// <summary>
    /// Handles the CreateProductCommand request
    /// </summary>
    /// <param name="command">The CreateProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product details</returns>
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingProduct = await _productRepository.Database.AsNoTracking().FirstOrDefaultAsync(fd => fd.Title == command.Title, cancellationToken);
        if (existingProduct != null) throw new InvalidOperationException($"Product with title {command.Title} already exists");

        var product = _mapper.Map<Domain.Entities.Product>(command);
        await using (var memoryStream = new MemoryStream())
        {
            await command.Image.File.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0;
            var imageId = await _noSqlRepository.GridFsBucket.UploadFromStreamAsync(product.Image, memoryStream, cancellationToken: cancellationToken);
            if(imageId == ObjectId.Empty)
                throw new InvalidOperationException("Failed to save image");
        }

        await _productRepository.AddProduct(product, cancellationToken);
        var success = (await _productRepository.SaveChangesAsync(cancellationToken)) > 0;
        if (!success)
            throw new InvalidOperationException("Failed to create product");
        var result = _mapper.Map<CreateProductResult>(product);
        return result;
    }
}
