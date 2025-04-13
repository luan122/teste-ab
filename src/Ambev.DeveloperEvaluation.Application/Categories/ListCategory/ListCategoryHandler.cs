using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Common.Commands;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Common.Extensions;

namespace Ambev.DeveloperEvaluation.Application.Categories.ListCategory;

/// <summary>
/// Handler for processing <see cref="ListCategoryHandler"/> requests
/// </summary>
public class ListCategoryHandler : IRequestHandler<ListCategoryCommand, PagedCommandResult<ListCategoryResult>?>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="ListCategoryHandler"/>
    /// </summary>
    /// <param name="categoryRepository">The <see cref="ICategoryRepository"/></param>
    /// <param name="mapper">The <see cref="IMapper"/> instance</param>
    public ListCategoryHandler(
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the <see cref="ListCategoryCommand"/> request
    /// </summary>
    /// <param name="request">The <see cref="ListCategoryCommand"/></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The <see cref="PagedCommandResult<ListCategoryResult>"/> if found.</returns>
    /// <exception cref="ValidationException">If the request is invalid.</exception>
    /// <exception cref="KeyNotFoundException">If the category not found by their unique identifier</exception>
    public async Task<PagedCommandResult<ListCategoryResult>> Handle(ListCategoryCommand request, CancellationToken cancellationToken)
    {
        var validator = new ListCategoryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Fix: Ensure the generic type matches the expected signature
        var pagedCategoryResult = await _categoryRepository.Database.AsQueryable()
            .ApplyFilters(request)
            .GetPagedResultAsync(request, _mapper);
        var pagedCommandResult = _mapper.Map<PagedCommandResult<ListCategoryResult>>(pagedCategoryResult);
        return pagedCommandResult;
    }
}
