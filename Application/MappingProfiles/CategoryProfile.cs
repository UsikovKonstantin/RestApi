using AutoMapper;
using Domain.Models;

namespace Application.MappingProfiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        // TODO: заменить первый класс на CategoryDto
        CreateMap<Category, Category>().ReverseMap();
    }
}
