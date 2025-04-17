using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.ORM.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Categories.DeleteCategory;

/// <summary>
/// Handler for processing DeleteCategoryCommand requests
/// </summary>
public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryResponse>
{
    private readonly ICategoryRepository _categoryRepository;

    /// <summary>
    /// Initializes a new instance of <see cref="DeleteCategoryHandler"/>
    /// </summary>
    /// <param name="categoryRepository">The <see cref="ICategoryRepository"/></param>
    /// <param name="validator">The validator for DeleteCategoryCommand</param>
    public DeleteCategoryHandler(
        ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    /// <summary>
    /// Handles the <see cref="DeleteCategoryCommand"/> request
    /// </summary>
    /// <param name="request">The <see cref="DeleteCategoryCommand"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteCategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteCategoryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        await _categoryRepository.DeleteById(request.Id, cancellationToken);
        var success = (await _categoryRepository.SaveChangesAsync(cancellationToken)) > 0;
        if (!success)
            throw new KeyNotFoundException($"Category with ID {request.Id} not found");

        return new DeleteCategoryResponse { Success = true };
    }
}
