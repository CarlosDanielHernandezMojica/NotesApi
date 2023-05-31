using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecNM.NotesApp.Core.Dto;
using TEcNM.NotesApp.Website.Services.Interfaces;

namespace TEcNM.NotesApp.Website.Pages;

public class IndexModel : PageModel
{ 
    [BindProperty] public UserDto userDto { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IUserService _service;
    
    public IndexModel(IUserService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        // Service call
        var response = await _service.GetById(id);
        userDto = response.Data;

        return Page();
    }
}