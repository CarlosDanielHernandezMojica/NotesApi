using TecNM.NotesApp.Core.Entities;

namespace TecNM.NotesApp.Core.Dto;

public class NoteDto: DtoBase
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int idUser { get; set; }
    public int idCategory { get; set; }
    public NoteDto(){}
    public NoteDto(Note note)
    {
        Id = note.Id;
        Title = note.Title;
        Description = note.Description;
        idCategory = note.idCategory;
        idUser = note.idUser;
    }
}