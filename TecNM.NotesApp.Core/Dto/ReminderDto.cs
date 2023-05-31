using TecNM.NotesApp.Core.Entities;

namespace TecNM.NotesApp.Core.Dto;

public class ReminderDto: DtoBase
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public int idUser { get; set; }
    public ReminderDto(){}
    public ReminderDto(Reminder reminder)
    {
        Id = reminder.Id;
        Name = reminder.Name;
        Date = reminder.Date;
        idUser = reminder.idUser;
    }
}