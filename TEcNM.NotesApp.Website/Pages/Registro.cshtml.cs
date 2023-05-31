using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecNM.NotesApp.Core.Dto;
using TecNM.NotesApp.Core.Http;
using TEcNM.NotesApp.Website.Services.Interfaces;

namespace TEcNM.NotesApp.Website.Pages;

public class Registro : PageModel
{
    [TempData]
    public string SuccessMessage { get; set; }
    [BindProperty] public UserDto User { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IUserService _service;
    public Registro(IUserService service)
    {
        _service = service;
    }
    public void OnGet()
    {
        
    }
    
    public async Task<IActionResult> OnPost()
    {
        Response<UserDto> response;
        response = await _service.SaveAsync(User);
        SuccessMessage = "Usuario creado correctamente";
        
        return RedirectToPage("/Login");
    }
}