using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecNM.NotesApp.Core.Dto;
using TEcNM.NotesApp.Website.Services.Interfaces;

namespace TEcNM.NotesApp.Website.Pages.Notas;

public class View : PageModel
{
    [BindProperty] public NoteDto Note { get; set; }

    public int idUsuario;
    public List<string> Errors { get; set; } = new List<string>();

    private readonly INoteService _service;

    public View(INoteService service)
    {
        _service = service;
    }
    
    public async Task<IActionResult> OnGet(int idUser, int? idNote)
    {
        this.idUsuario = idUser;
        Note = new NoteDto();
        if (idNote.HasValue)
        {
            var response = await _service.GetById(idNote.Value);
            Note = response.Data;
            
        }
        Note.idUser = idUser;
        if (Note == null)
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }
}