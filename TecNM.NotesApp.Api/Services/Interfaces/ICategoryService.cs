using TecNM.NotesApp.Core.Dto;

namespace TecNM.NotesApp.Api.Services.Interfaces;

public interface ICategoryService
{
    //Save categories method
    Task<CategoryDto>SaveAsync(CategoryDto category);
    
    //Update product categories method
    Task<CategoryDto>UpdateAsync(CategoryDto category);
    
    //Return category list method
    Task<List<CategoryDto>>GetAllAsync();
    
    //Return the id of the category that will be deleted method
    Task<bool>CategoryExist(int id);
    
    Task<bool>DeleteAsync(int id);
    
    //Get category by id method
    Task<CategoryDto>GetById(int id);
}