using Dapper;
using Dapper.Contrib.Extensions;
using TecNM.NotesApp.Api.DataAccess.Interfaces;
using TecNM.NotesApp.Api.Repositories.Interfaces;
using TecNM.NotesApp.Core.Entities;

namespace TecNM.NotesApp.Api.Repositories;

public class ReminderRepository: IReminderRepository
{
    private readonly IDbContext _dbContext;

    public ReminderRepository(IDbContext context)
    {
        _dbContext = context;
    }   

    public async Task<Reminder> SaveAsync(Reminder reminder)
    {
        reminder.Id = await _dbContext.Connection.InsertAsync(reminder);

        return reminder;
    }

    public async Task<Reminder> UpdateAsync(Reminder reminder)
    {
        await _dbContext.Connection.UpdateAsync(reminder);

        return reminder;
    }

    public async Task<List<Reminder>> GetAllAsync(int id)
    {
        string sql = "SELECT * FROM Reminder WHERE IsDeleted = 0 AND idUser =" + id;

        var reminders = await _dbContext.Connection.QueryAsync<Reminder>(sql);

        return reminders.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var reminder = await GetById(id);
        if (reminder == null)
        {
            return false;
        }

        reminder.IsDeleted = true;

        return await _dbContext.Connection.UpdateAsync(reminder);
    }

    public async Task<Reminder> GetById(int id)
    {
        var reminder = await _dbContext.Connection.GetAsync<Reminder>(id);

        if (reminder == null)
            return null;

        return reminder.IsDeleted == true ? null : reminder;
    }
}