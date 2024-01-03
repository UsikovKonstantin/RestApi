using Application.Features.Category.Commands.CreateCategory;
using Application.Features.Category.Commands.UpdateCategory;
using Application.Features.Category.Queries.GetAllCategories;
using Application.Features.Category.Queries.GetAllCategoriesDetails;
using AutoMapper;
using Domain;

namespace Application.MappingProfiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryResponse>();
		CreateMap<Category, CategoryDetailsResponse>();
		CreateMap<CreateCategoryCommand, Category>();
		CreateMap<UpdateCategoryCommand, Category>();
	}
}
