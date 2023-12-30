using Domain.Models;

namespace Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
	Task<List<T>> GetAllAsync();

	Task<T> GetByIdAsync(int id);

	Task<T> CreateAsync(T entity);

	Task<T> UpdateAsync(T entity);

	Task<T> DeleteAsync(T entity);
}
