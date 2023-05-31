using Dapper;
using Dapper.Contrib.Extensions;
using TecNM.NotesApp.Api.DataAccess.Interfaces;
using TecNM.NotesApp.Api.Repositories.Interfaces;
using TecNM.NotesApp.Core.Entities;

namespace TecNM.NotesApp.Api.Repositories;

public class NoteRepository: INoteRepository
{
    private readonly IDbContext _dbContext;

    public NoteRepository(IDbContext context)
    {
        _dbContext = context;
    }   

    public async Task<Note> SaveAsync(Note note)
    {
        note.Id = await _dbContext.Connection.InsertAsync(note);

        return note;
    }

    public async Task<Note> UpdateAsync(Note note)
    {
        await _dbContext.Connection.UpdateAsync(note);

        return note;
    }

    public async Task<List<Note>> GetAllByIdUserAsync(int idUser)
    {
        string sql = "SELECT * FROM Note WHERE IsDeleted = 0 AND IdUser = " + idUser;

        var notes = await _dbContext.Connection.QueryAsync<Note>(sql);

        return notes.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var note = await GetById(id);
        if (note == null)
        {
            return false;
        }

        note.IsDeleted = true;

        return await _dbContext.Connection.UpdateAsync(note);
    }

    public async Task<Note> GetById(int id)
    {
        var note = await _dbContext.Connection.GetAsync<Note>(id);

        if (note == null)
            return null;

        return note.IsDeleted == true ? null : note;
    }
}