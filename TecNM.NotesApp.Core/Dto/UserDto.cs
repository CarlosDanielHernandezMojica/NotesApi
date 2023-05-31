using TecNM.NotesApp.Core.Entities;

namespace TecNM.NotesApp.Core.Dto;

public class UserDto: DtoBase
{
    public string Username { get; set; }
    public string Password { get; set; }

    public UserDto() {}
    public UserDto(User user)
    {
        Id = user.Id;
        Username = user.Username;
        Password = user.Password;
    }
}