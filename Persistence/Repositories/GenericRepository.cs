﻿using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;

namespace Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
	protected readonly ProductDatabaseContext _context;

    public GenericRepository(ProductDatabaseContext context)
    {
        _context = context;
    }


	public async Task<IEnumerable<T>> GetAllAsync()
	{
		return await _context.Set<T>().AsNoTracking().ToListAsync();
	}

	public async Task<T?> GetByIdAsync(int id)
	{
		return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
	}

	public async Task CreateAsync(T entity)
	{
		await _context.AddAsync(entity);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(T entity)
	{
		_context.Entry(entity).State = EntityState.Modified;
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(T entity)
	{
		_context.Remove(entity);
		await _context.SaveChangesAsync();
	}
}
