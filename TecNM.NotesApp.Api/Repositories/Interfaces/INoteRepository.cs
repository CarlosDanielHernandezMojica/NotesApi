using TecNM.NotesApp.Core.Entities;

namespace TecNM.NotesApp.Api.Repositories.Interfaces;

public interface INoteRepository
{
    //Save categories method
    Task<Note> SaveAsync(Note note);
    //Update product categories method
    Task<Note> UpdateAsync(Note note);
    //Return note list method
    Task<List<Note>> GetAllByIdUserAsync(int idUser);
    //Return the id of the note that will be deleted method
    Task<bool> DeleteAsync(int id);
    //Get note by id method
    Task<Note> GetById(int id);
}