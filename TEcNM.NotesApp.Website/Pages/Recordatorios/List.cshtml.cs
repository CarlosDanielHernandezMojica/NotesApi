using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TecNM.NotesApp.Core.Dto;
using TEcNM.NotesApp.Website.Services.Interfaces;

namespace TEcNM.RemindersApp.Website.Pages.Recordatorios;

public class List : PageModel
{
    [BindProperty] public UserDto userDto { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    public List<ReminderDto> Reminders { get; set; }

    public int idReminder;
    
    private readonly IUserService _service;
    private readonly IReminderService _reminderService;
    
    public List(IUserService service, IReminderService reminderService)
    {
        _service = service;
        _reminderService = reminderService;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        var response = await _service.GetById(id);
        userDto = response.Data;
        
        var notesResponse = await _reminderService.GetAllAsync(id);
        System.Console.WriteLine("responde remid");
        System.Console.WriteLine(notesResponse.Data);
        Reminders = notesResponse.Data;
        System.Console.WriteLine("reminder id");
        System.Console.WriteLine(id);
        return Page();
    }
}