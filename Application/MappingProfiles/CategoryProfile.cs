using Application.Features.Category.Queries.GetAllCategories;
using Application.Features.Category.Queries.GetAllCategoriesDetails;
using AutoMapper;
using Domain.Models;

namespace Application.MappingProfiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryResponse>().ReverseMap();
		CreateMap<Category, CategoryDetailsResponse>().ReverseMap();
	}
}
