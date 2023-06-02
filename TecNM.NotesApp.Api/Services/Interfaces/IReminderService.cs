using TecNM.NotesApp.Core.Dto;

namespace TecNM.NotesApp.Api.Services.Interfaces;

public interface IReminderService
{
    //Save categories method
    Task<ReminderDto>SaveAsync(ReminderDto reminder);
    
    //Update product categories method
    Task<ReminderDto>UpdateAsync(ReminderDto reminder);
    
    //Return reminder list method
    Task<List<ReminderDto>>GetAllAsync(int id);
    
    //Return the id of the reminder that will be deleted method
    Task<bool>ReminderExist(int id);
    
    Task<bool>DeleteAsync(int id);
    
    //Get reminder by id method
    Task<ReminderDto>GetById(int id);
}