using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecNM.NotesApp.Core.Dto;
using TEcNM.NotesApp.Website.Services.Interfaces;

namespace TEcNM.NotesApp.Website.Pages.Notas;

public class List : PageModel
{
    [BindProperty] public UserDto userDto { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    public List<NoteDto> Notes { get; set; }

    public int idNote;

    private readonly IUserService _service;
    private readonly INoteService _noteService;
    public List(IUserService service, INoteService noteService)
    {
        _service = service;
        _noteService = noteService;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        var response = await _service.GetById(id);
        userDto = response.Data;
        
        var notesResponse = await _noteService.GetAllAsync(id);
        Notes = notesResponse.Data;
        System.Console.WriteLine(idNote);
        return Page();
    }

    public void GetId(int id)
    {
        idNote = id;
        System.Console.WriteLine("Id List");
        System.Console.WriteLine(id);
    }
    
}

