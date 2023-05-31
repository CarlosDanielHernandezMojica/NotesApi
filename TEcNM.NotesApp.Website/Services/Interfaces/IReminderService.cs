using TecNM.NotesApp.Core.Dto;
using TecNM.NotesApp.Core.Http;

namespace TEcNM.NotesApp.Website.Services.Interfaces;

public interface IReminderService
{   
    Task<Response<List<ReminderDto>>> GetAllAsync();

    Task<Response<ReminderDto>> GetById(int id);

    Task<Response<ReminderDto>> SaveAsync(ReminderDto reminder);
    
    Task<Response<ReminderDto>> UpdateAsync(ReminderDto reminder);

    Task<Response<bool>> DeleteAsync(int id);
}