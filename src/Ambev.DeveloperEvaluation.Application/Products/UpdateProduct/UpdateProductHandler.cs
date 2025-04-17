using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using static System.Net.Mime.MediaTypeNames;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Handler for processing UpdateProductCommand requests
/// </summary>
public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly INoSqlRepository<BsonDocumentBackedClass> _noSqlRepository;

    /// <summary>
    /// Initializes a new instance of UpdateProductHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for UpdateProductCommand</param>
    public UpdateProductHandler(IProductRepository productRepository, IMapper mapper, INoSqlRepository<BsonDocumentBackedClass> noSqlRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _noSqlRepository = noSqlRepository;
    }

    /// <summary>
    /// Handles the UpdateProductCommand request
    /// </summary>
    /// <param name="command">The UpdateProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The update product details</returns>
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingProduct = await _productRepository.Database.Include(p => p.Category).Include(p => p.Rating).FirstOrDefaultAsync(fd => fd.Id == command.Id, cancellationToken) ?? throw new InvalidOperationException($"Product with id {command.Id} not found");
        var categoryResult = await _productRepository.UpdateProductCategory(existingProduct, _mapper.Map<Product>(command), cancellationToken);
            switch (categoryResult)
        {
            case ProductUpdateCategoryOperation.Add:
            case ProductUpdateCategoryOperation.Update:
            case ProductUpdateCategoryOperation.Keep:
                command.Category = null;
                break;
        }

        ObjectId imageId = ObjectId.Empty;
        await using (var memoryStream = new MemoryStream())
        {
            if (command.Title != existingProduct.Title)
            {
                if (command.Image != null)
                {
                    await command.Image.File.CopyToAsync(memoryStream, cancellationToken);
                    memoryStream.Position = 0;
                    await _noSqlRepository.DeleteGridFsByFileName(existingProduct.Image, cancellationToken);
                    var id = await _noSqlRepository.GridFsBucket.UploadFromStreamAsync(command.Image.ImageName, memoryStream, cancellationToken: cancellationToken);
                }
                else
                {
                    command.Image = new() { ImageName = $"{StringSanitizeExtensions.Sanitize(command.Title)}.{existingProduct.Image.Split('.').Last()}" };
                    await _noSqlRepository.UpdateGridFsFileByFileName(existingProduct.Image, command.Image.ImageName, cancellationToken);
                }
            }
            else if (command.Image != null)
            {
                imageId = await _noSqlRepository.GridFsBucket.UploadFromStreamAsync(command.Image.ImageName, memoryStream, cancellationToken: cancellationToken);
                if (imageId == ObjectId.Empty)
                    throw new InvalidOperationException("Failed to save image");
            }
        }
        _mapper.Map(command, existingProduct);
        var success = (await _productRepository.SaveChangesAsync(cancellationToken)) > 0;
        if (!success)
            throw new InvalidOperationException($"Failed to update product with id {existingProduct.Id}");
        var result = _mapper.Map<UpdateProductResult>(existingProduct);
        return result;
        //}
    }
}
