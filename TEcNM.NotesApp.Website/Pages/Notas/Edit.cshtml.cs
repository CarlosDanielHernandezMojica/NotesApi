using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecNM.NotesApp.Core.Dto;
using TecNM.NotesApp.Core.Http;
using TEcNM.NotesApp.Website.Services.Interfaces;

namespace TEcNM.NotesApp.Website.Pages.Notas;

public class Edit : PageModel
{
    [TempData]
    public string Title { get; set; }
    [BindProperty] public NoteDto Note { get; set; }

    public int idUsuario;
    public List<string> Errors { get; set; } = new List<string>();

    private readonly INoteService _service;

    public Edit(INoteService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int idUser, int? idNote)
    {
        Title = "Agregar nota";
        Note = new NoteDto();
        if (idNote.HasValue)
        {
            Title = "Editar nota";
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

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Response<NoteDto> response;
        if (Note.Id > 0)
        {
            System.Console.WriteLine("Entro update");
            response = await _service.UpdateAsync(Note);
        }
        else
        {
            
            System.Console.WriteLine("Entro save");
            
            response = await _service.SaveAsync(Note);
        }

        Errors = response.Errors;

        if (Errors.Count > 0)
        {
            return Page();
        }

        Note = response.Data;

        idUsuario = response.Data.idUser;
        return RedirectToPage("/Notas/List", new {id = idUsuario});
    }
}