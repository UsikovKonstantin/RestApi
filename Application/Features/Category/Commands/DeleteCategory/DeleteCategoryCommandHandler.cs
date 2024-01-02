using Application.Contracts.Persistence;
using Application.Exceptions;
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
		Domain.Category? category = await _categoryRepository.GetByIdAsync(request.Id);

		// Проверить, что объект существует
		if (category == null)
			throw new NotFoundException(nameof(Domain.Category), request.Id);

		// Удалить объект из базы данных
		await _categoryRepository.DeleteAsync(category);

		// Вернуть Unit
		return Unit.Value;
	}
}
