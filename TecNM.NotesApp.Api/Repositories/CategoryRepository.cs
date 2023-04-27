using Dapper;
using Dapper.Contrib.Extensions;
using TecNM.NotesApp.Api.DataAccess.Interfaces;
using TecNM.NotesApp.Api.Repositories.Interfaces;
using TecNM.NotesApp.Core.Entities;

namespace TecNM.NotesApp.Api.Repositories;

public class CategoryRepository: ICategoryRepository
{
    private readonly IDbContext _dbContext;

    public CategoryRepository(IDbContext context)
    {
        _dbContext = context;
    }   

    public async Task<Category> SaveAsync(Category category)
    {
        category.Id = await _dbContext.Connection.InsertAsync(category);

        return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        await _dbContext.Connection.UpdateAsync(category);

        return category;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Category WHERE IsDeleted = 0";

        var categories = await _dbContext.Connection.QueryAsync<Category>(sql);

        return categories.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await GetById(id);
        if (category == null)
        {
            return false;
        }

        category.IsDeleted = true;

        return await _dbContext.Connection.UpdateAsync(category);
    }

    public async Task<Category> GetById(int id)
    {
        var category = await _dbContext.Connection.GetAsync<Category>(id);

        if (category == null)
            return null;

        return category.IsDeleted == true ? null : category;
    }
}