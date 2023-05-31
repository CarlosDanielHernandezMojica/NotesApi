using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Identity;
using TecNM.NotesApp.Api.DataAccess.Interfaces;
using TecNM.NotesApp.Api.Repositories.Interfaces;
using TecNM.NotesApp.Core.Entities;

namespace TecNM.NotesApp.Api.Repositories;

public class UserRepository: IUserRepository
{
    private readonly IDbContext _dbContext;

    public UserRepository(IDbContext context)
    {
        _dbContext = context;
    }   

    public async Task<User> SaveAsync(User user)
    {
        user.Id = await _dbContext.Connection.InsertAsync(user);
        System.Console.WriteLine(user.Id);
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        await _dbContext.Connection.UpdateAsync(user);

        return user;
    }

    public async Task<List<User>> GetAllAsync()
    {
        const string sql = "SELECT * FROM User WHERE IsDeleted = 0";

        var users = await _dbContext.Connection.QueryAsync<User>(sql);

        return users.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await GetById(id);
        if (user == null)
        {
            return false;
        }

        user.IsDeleted = true;

        return await _dbContext.Connection.UpdateAsync(user);
    }

    public async Task<User> GetById(int id)
    {
        var user = await _dbContext.Connection.GetAsync<User>(id);

        if (user == null)
            return null;

        return user.IsDeleted == true ? null : user;
    }

    public async Task<User> Login(string username, string password)
    {
        string sql = "SELECT * FROM User WHERE Username = '" + username + "' AND Password = '" + password + "' AND IsDeleted = 0;";
        
        var user = await _dbContext.Connection.QuerySingleOrDefaultAsync<User>(sql);
        if (user == null)
        {
            user = new User()
            {
                Id = 0,
                Username = null,
                Password = null
            };
        }
        System.Console.WriteLine(user.Username);
        return user;
    }
    
}