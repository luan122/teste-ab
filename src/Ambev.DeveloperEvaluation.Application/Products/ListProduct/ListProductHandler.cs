using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Common.Extensions;
using Ambev.DeveloperEvaluation.Application.Common.Results;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct;

/// <summary>
/// Handler for processing <see cref="ListProductHandler"/> requests
/// </summary>
public class ListProductHandler : IRequestHandler<ListProductCommand, PagedCommandResult<ListProductResult>?>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="ListProductHandler"/>
    /// </summary>
    /// <param name="productRepository">The <see cref="IProductRepository"/></param>
    /// <param name="mapper">The <see cref="IMapper"/> instance</param>
    public ListProductHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the <see cref="ListProductCommand"/> request
    /// </summary>
    /// <param name="request">The <see cref="ListProductCommand"/></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The <see cref="PagedCommandResult<ListProductResult>"/> if found.</returns>
    /// <exception cref="ValidationException">If the request is invalid.</exception>
    /// <exception cref="KeyNotFoundException">If the product not found by their unique identifier</exception>
    public async Task<PagedCommandResult<ListProductResult>> Handle(ListProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new ListProductValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Fix: Ensure the generic type matches the expected signature
        var pagedProductResult = await _productRepository.Database.AsQueryable()
            .ApplyFilters(request)
            .GetPagedResultAsync(request, _mapper);
        var pagedCommandResult = _mapper.Map<PagedCommandResult<ListProductResult>>(pagedProductResult);
        return pagedCommandResult;
    }
}
