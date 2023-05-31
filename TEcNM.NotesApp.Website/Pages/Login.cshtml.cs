using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecNM.NotesApp.Core.Dto;
using TecNM.NotesApp.Core.Http;
using TEcNM.NotesApp.Website.Services.Interfaces;

namespace TEcNM.NotesApp.Website.Pages;

public class Login : PageModel
{
    [TempData]
    public string ErrorMessage { get; set; }
    [BindProperty] public UserDto UserDto { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IUserService _service;
    public Login(IUserService service)
    {
        _service = service;
    }
    

    public async Task<IActionResult> OnPost()
    {
        Response<UserDto> response;
        UserDto.Id = 0;
        response = await _service.Login(UserDto);
        System.Console.WriteLine(response.Data.Id);
        if (response.Data.Id <= 0)
        {
            ErrorMessage = "Credenciales inválidas. Por favor, intenta de nuevo.";
            return Page();
        }
        else
        {
            return RedirectToPage("/Index", new { id = response.Data.Id });    
        }
        
    }
}