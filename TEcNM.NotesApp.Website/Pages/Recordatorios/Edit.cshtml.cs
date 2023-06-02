using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecNM.NotesApp.Core.Dto;
using TecNM.NotesApp.Core.Http;
using TEcNM.NotesApp.Website.Services.Interfaces;

namespace TEcNM.RemindersApp.Website.Pages.Recordatorios;

public class Edit : PageModel
{
    [TempData]
    public string Title { get; set; }
    [BindProperty] public ReminderDto Reminder { get; set; }

    public int idUsuario;
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IReminderService _service;

    public Edit(IReminderService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int idUser, int? idReminder)
    {
        Title = "Agregar recordatorio";
        System.Console.WriteLine("Usuario de edit");
        System.Console.WriteLine(idUser);
        this.idUsuario = idUser;
        System.Console.WriteLine(this.idUsuario);
        
        
        Reminder = new ReminderDto();
        if (idReminder.HasValue)
        {
            Title = "Editar recordatorio";
            var response = await _service.GetById(idReminder.Value);
            Reminder = response.Data;
            
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
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Response<ReminderDto> response;
        if (Reminder.Id > 0)
        {
            System.Console.WriteLine("Entro update");
            response = await _service.UpdateAsync(Reminder);
        }
        else
        {
            
            System.Console.WriteLine("Entro save");
            
            response = await _service.SaveAsync(Reminder);
        }

        Errors = response.Errors;

        if (Errors.Count > 0)
        {
            return Page();
        }

        Reminder = response.Data;

        idUsuario = response.Data.idUser;
        return RedirectToPage("/Recordatorios/List", new {id = idUsuario});
    }
}