namespace TecNM.NotesApp.Core.Entities;

public class Note: EntityBase
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int idUser { get; set; }
    public int idCategory { get; set; }
}