﻿using Domain;

namespace Application.Contracts.Persistence;

public interface ICategoryRepository : IGenericRepository<Category>
{
	Task<Category?> GetCategoryByNameAsync(string name);
}
