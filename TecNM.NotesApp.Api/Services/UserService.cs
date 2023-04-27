using TecNM.NotesApp.Api.Repositories.Interfaces;
using TecNM.NotesApp.Api.Services.Interfaces;
using TecNM.NotesApp.Core.Dto;
using TecNM.NotesApp.Core.Entities;

namespace TecNM.NotesApp.Api.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> SaveAsync(UserDto userDto)
    {
        var user = new User()
        {
            Username = userDto.Username,
            Password = userDto.Password,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };
        
        user = await _userRepository.SaveAsync(user);
        userDto.Id = user.Id;

        return userDto;
    }

    public async Task<UserDto> UpdateAsync(UserDto userDto)
    {
        var user = await _userRepository.GetById(userDto.Id);

        if (user == null)
        {
            throw new Exception("User Not Found");
        }

        user.Username = userDto.Username;
        user.Password = userDto.Password;
        user.UpdatedBy = "";
        user.UpdatedDate = DateTime.Now;

        await _userRepository.UpdateAsync(user);
        
        return userDto;
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        var usersDto = users.Select(c => new UserDto(c)).ToList();
        return usersDto;
    }

    public async Task<bool> UserExist(int id)
    {
        var user = await _userRepository.GetById(id);

        return (user != null);
    }

    public async Task<UserDto> GetById(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user == null)
        {
            throw new Exception("User Not Found");
        }

        var userDto = new UserDto(user);

        return userDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    public async Task<bool> Login(UserDto userDto)
    {
        var user = new User()
        {
            Username = userDto.Username,
            Password = userDto.Password
        };
        var result = await _userRepository.Login(user);

        return (result != null);
    }
}