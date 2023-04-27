using TecNM.NotesApp.Core.Entities;

namespace TecNM.NotesApp.Api.Repositories.Interfaces;

public interface IReminderRepository
{
    //Save categories method
    Task<Reminder> SaveAsync(Reminder reminder);
    
    //Update product categories method
    Task<Reminder> UpdateAsync(Reminder reminder);
    
    //Return reminder list method
    Task<List<Reminder>> GetAllAsync();
    
    //Return the id of the reminder that will be deleted method
    Task<bool> DeleteAsync(int id);
    
    //Get reminder by id method
    Task<Reminder> GetById(int id);
}