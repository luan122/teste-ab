using Ambev.DeveloperEvaluation.Application.Categories.CreateCategory;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.CreateCategory
{
    public class CreateCategoryProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateCategory feature
        /// </summary>
        public CreateCategoryProfile()
        {
            CreateMap<CreateCategoryRequest, CreateCategoryCommand>();
            CreateMap<CreateCategoryResult, CreateCategoryResponse>();
        }
    }
}
