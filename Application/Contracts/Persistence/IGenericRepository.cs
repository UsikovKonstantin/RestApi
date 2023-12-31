﻿using Domain;

namespace Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
	Task<IEnumerable<T>> GetAllAsync();

	Task<T?> GetByIdAsync(int id);

	Task CreateAsync(T entity);

	Task UpdateAsync(T entity);

	Task DeleteAsync(T entity);
}
