using TecNM.NotesApp.Core.Dto;
using TecNM.NotesApp.Core.Http;

namespace TEcNM.NotesApp.Website.Services.Interfaces;

public interface INoteService
{
    Task<Response<List<NoteDto>>> GetAllAsync(int id);

    Task<Response<NoteDto>> GetById(int id);

    Task<Response<NoteDto>> SaveAsync(NoteDto note);
    
    Task<Response<NoteDto>> UpdateAsync(NoteDto note);

    Task<Response<bool>> DeleteAsync(int id);
}