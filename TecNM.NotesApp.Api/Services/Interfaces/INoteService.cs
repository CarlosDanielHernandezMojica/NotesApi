using TecNM.NotesApp.Core.Dto;

namespace TecNM.NotesApp.Api.Services.Interfaces;

public interface INoteService
{
    //Save categories method
    Task<NoteDto>SaveAsync(NoteDto note);
    
    //Update product categories method
    Task<NoteDto>UpdateAsync(NoteDto note);
    
    //Return note list method
    Task<List<NoteDto>>GetAllByIdUserAsync(int idUser);
    
    //Return the id of the note that will be deleted method
    Task<bool>NoteExist(int id);
    
    Task<bool>DeleteAsync(int id);
    
    //Get note by id method
    Task<NoteDto>GetById(int id);
}