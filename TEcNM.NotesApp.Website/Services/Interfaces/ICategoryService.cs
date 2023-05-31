using TecNM.NotesApp.Core.Dto;
using TecNM.NotesApp.Core.Http;

namespace TEcNM.NotesApp.Website.Services.Interfaces;

public interface ICategoryInterface
{
    Task<Response<List<CategoryDto>>> GetAllAsync();

    Task<Response<CategoryDto>> GetById(int id);

    Task<Response<CategoryDto>> SaveAsync(CategoryDto category);
    
    Task<Response<CategoryDto>> UpdateAsync(CategoryDto category);

    Task<Response<bool>> DeleteAsync(int id);
}