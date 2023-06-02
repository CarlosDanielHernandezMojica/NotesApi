using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecNM.NotesApp.Core.Dto;
using TEcNM.NotesApp.Website.Services.Interfaces;

namespace TEcNM.RemindersApp.Website.Pages.Recordatorios;

public class Delete : PageModel
{
    [TempData]
    public string Title { get; set; }
    [BindProperty] public ReminderDto Reminder { get; set; }

    public int idUsuario;
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IReminderService _service;

    public Delete(IReminderService service)
    {
        _service = service;
    }
    
    public async Task<IActionResult> OnGet(int idUser, int? idReminder)
    {
        Title = "¿Esta seguro de eliminar este elemento?";
        System.Console.WriteLine("Usuario de edit");
        System.Console.WriteLine(idUser);
        this.idUsuario = idUser;
        System.Console.WriteLine(this.idUsuario);
        
        
        Reminder = new ReminderDto();
        if (idReminder.HasValue)
        {
            Title = "Editar nota";
            var response = await _service.GetById(idReminder.Value);
            Reminder = response.Data;
            System.Console.WriteLine(response.Data.Name);
        }
        
        Reminder.idUser = idUser;
        
        if (Reminder == null)
        {
            return RedirectToPage("/Error");
        }

        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {
        await _service.DeleteAsync(Reminder.Id);

        return RedirectToPage("/Recordatorios/List", new {id = Reminder.idUser});
    }
}