namespace TecNM.NotesApp.Core.Entities;

public class Reminder: EntityBase
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public int idUser { get; set; }
}