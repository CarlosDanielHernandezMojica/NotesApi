using TecNM.NotesApp.Api.Repositories.Interfaces;
using TecNM.NotesApp.Api.Services.Interfaces;
using TecNM.NotesApp.Core.Dto;
using TecNM.NotesApp.Core.Entities;

namespace TecNM.NotesApp.Api.Services;

public class ReminderService: IReminderService
{
    private readonly IReminderRepository _reminderRepository;

    public ReminderService(IReminderRepository reminderRepository)
    {
        _reminderRepository = reminderRepository;
    }

    public async Task<ReminderDto> SaveAsync(ReminderDto reminderDto)
    {
        var reminder = new Reminder()
        {
            Name = reminderDto.Name,
            Date = reminderDto.Date,
            idUser = reminderDto.idUser,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };
        
        reminder = await _reminderRepository.SaveAsync(reminder);
        reminderDto.Id = reminder.Id;

        return reminderDto;
    }

    public async Task<ReminderDto> UpdateAsync(ReminderDto reminderDto)
    {
        var reminder = await _reminderRepository.GetById(reminderDto.Id);

        if (reminder == null)
        {
            throw new Exception("Reminder Not Found");
        }

        reminder.Name = reminderDto.Name;
        reminder.Date = reminderDto.Date;
        reminder.UpdatedBy = "";
        reminder.UpdatedDate = DateTime.Now;

        await _reminderRepository.UpdateAsync(reminder);
        
        return reminderDto;
    }

    public async Task<List<ReminderDto>> GetAllAsync(int id)
    {
        var reminders = await _reminderRepository.GetAllAsync(id);
        var remindersDto = reminders.Select(c => new ReminderDto(c)).ToList();
        return remindersDto;
    }

    public async Task<bool> ReminderExist(int id)
    {
        var reminder = await _reminderRepository.GetById(id);

        return (reminder != null);
    }

    public async Task<ReminderDto> GetById(int id)
    {
        var reminder = await _reminderRepository.GetById(id);
        if (reminder == null)
        {
            throw new Exception("Reminder Not Found");
        }

        var reminderDto = new ReminderDto(reminder);

        return reminderDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _reminderRepository.DeleteAsync(id);
    }
}