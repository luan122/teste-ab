using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Categories.GetCategory;

/// <summary>
/// Handler for processing <see cref="GetCategoryHandler"/> requests
/// </summary>
public class GetCategoryHandler : IRequestHandler<GetCategoryCommand, GetCategoryResult>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="GetCategoryHandler"/>
    /// </summary>
    /// <param name="categoryRepository">The <see cref="ICategoryRepository"/></param>
    /// <param name="mapper">The <see cref="IMapper"/> instance</param>
    public GetCategoryHandler(
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the <see cref="GetCategoryCommand"/> request
    /// </summary>
    /// <param name="request">The <see cref="GetCategoryCommand"/></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The <see cref="GetCategoryResult"/> if found.</returns>
    /// <exception cref="ValidationException">If the request is invalid.</exception>
    /// <exception cref="KeyNotFoundException">If the category not found by their unique identifier</exception>
    public async Task<GetCategoryResult> Handle(GetCategoryCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetCategoryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var category = await _categoryRepository.Database.FirstOrDefaultAsync(fd => fd.Id == request.Id, cancellationToken: cancellationToken);
        return category == null
            ? throw new KeyNotFoundException($"Category with ID {request.Id} not found")
            : _mapper.Map<GetCategoryResult>(category);
    }
}
