using TecNM.NotesApp.Core.Dto;
using TecNM.NotesApp.Core.Http;

namespace TEcNM.NotesApp.Website.Services.Interfaces;

public interface IUserService
{
    Task<Response<List<UserDto>>> GetAllAsync();

    Task<Response<UserDto>> GetById(int id);

    Task<Response<UserDto>> SaveAsync(UserDto user);
    
    Task<Response<UserDto>> UpdateAsync(UserDto user);

    Task<Response<bool>> DeleteAsync(int id);

    Task<Response<UserDto>> Login(UserDto user);
}