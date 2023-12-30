using Application.Contracts.Persistence;
using MediatR;

namespace Application.Features.Category.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
	private readonly ICategoryRepository _categoryRepository;

	public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
	{
		_categoryRepository = categoryRepository;
	}

	public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
	{
		// Получить объект из базы данных
		Domain.Category category = await _categoryRepository.GetByIdAsync(request.Id);

		// TODO: Проверить, что объект существует


		// Удалить объект из базы данных
		await _categoryRepository.DeleteAsync(category);

		// Вернуть Unit
		return Unit.Value;
	}
}
