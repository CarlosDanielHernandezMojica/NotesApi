using TecNM.NotesApp.Core.Dto;

namespace TecNM.NotesApp.Api.Services.Interfaces;

public interface IUserService
{
    //Save categories method
    Task<UserDto>SaveAsync(UserDto user);
    
    //Update product categories method
    Task<UserDto>UpdateAsync(UserDto user);
    
    //Return user list method
    Task<List<UserDto>>GetAllAsync();
    
    //Return the id of the user that will be deleted method
    Task<bool>UserExist(int id);
    
    Task<bool>DeleteAsync(int id);
    
    //Get user by id method
    Task<UserDto>GetById(int id);

    Task<bool> Login(UserDto user);
}