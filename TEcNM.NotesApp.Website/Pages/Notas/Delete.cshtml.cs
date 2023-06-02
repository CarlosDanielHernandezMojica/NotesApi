using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecNM.NotesApp.Core.Dto;
using TEcNM.NotesApp.Website.Services.Interfaces;

namespace TEcNM.NotesApp.Website.Pages.Notas;

public class Delete : PageModel
{
    [TempData]
    public string Title { get; set; }
    [BindProperty] public NoteDto Note { get; set; }

    public int idUsuario;
    public List<string> Errors { get; set; } = new List<string>();

    private readonly INoteService _service;

    public Delete(INoteService service)
    {
        _service = service;
    }
    
    public async Task<IActionResult> OnGet(int idUser, int? idNote)
    {
        Title = "¿Esta seguro de eliminar este elemento?";
        System.Console.WriteLine("Usuario de edit");
        System.Console.WriteLine(idUser);
        this.idUsuario = idUser;
        System.Console.WriteLine(this.idUsuario);
        
        
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
        await _service.DeleteAsync(Note.Id);

        return RedirectToPage("/Notas/List", new {id = Note.idUser});
    }
}