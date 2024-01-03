using Application.Features.Category.Shared;
using MediatR;

namespace Application.Features.Category.Commands.CreateCategory;

public class CreateCategoryCommand : BaseCategoryCommand, IRequest<int>
{

}
