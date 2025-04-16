using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Handler for processing DeleteProductCommand requests
/// </summary>
public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
{
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of <see cref="DeleteProductHandler"/>
    /// </summary>
    /// <param name="productRepository">The <see cref="IProductRepository"/></param>
    /// <param name="validator">The validator for DeleteProductCommand</param>
    public DeleteProductHandler(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    /// <summary>
    /// Handles the <see cref="DeleteProductCommand"/> request
    /// </summary>
    /// <param name="request">The <see cref="DeleteProductCommand"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteProductValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        await _productRepository.DeleteById(request.Id, cancellationToken);
        var success = (await _productRepository.SaveChangesAsync(cancellationToken)) > 0;
        if (!success)
            throw new KeyNotFoundException($"Product with ID {request.Id} not found");

        return new DeleteProductResponse { Success = true };
    }
}
