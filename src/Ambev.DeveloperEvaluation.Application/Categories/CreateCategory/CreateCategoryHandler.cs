using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Categories.CreateCategory;

/// <summary>
/// Handler for processing CreateCategoryCommand requests
/// </summary>
public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResult>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateCategoryHandler
    /// </summary>
    /// <param name="categoryRepository">The category repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for CreateCategoryCommand</param>
    public CreateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateCategoryCommand request
    /// </summary>
    /// <param name="command">The CreateCategory command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created category details</returns>
    public async Task<CreateCategoryResult> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateCategoryCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingCategory = await _categoryRepository.Database.FirstOrDefaultAsync(fd => fd.Title == command.Title, cancellationToken);
        if (existingCategory != null)
            throw new InvalidOperationException($"Category with title {command.Title} already exists");

        var category = _mapper.Map<Category>(command);

        var createdCategory = await _categoryRepository.AddAsync(category, cancellationToken);
        await _categoryRepository.SaveChangesAsync();
        var result = _mapper.Map<CreateCategoryResult>(createdCategory);
        return result;
    }
}
