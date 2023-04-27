using TecNM.NotesApp.Core.Entities;

namespace TecNM.NotesApp.Core.Dto;

public class CategoryDto: DtoBase
{
    public string Name { get; set; }

    public CategoryDto()
    {
    }

    public CategoryDto(Category category)
    {
        Id = category.Id;
        Name = category.Name;
    }
}