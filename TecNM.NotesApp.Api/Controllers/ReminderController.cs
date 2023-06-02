using Microsoft.AspNetCore.Mvc;
using TecNM.NotesApp.Api.Services.Interfaces;
using TecNM.NotesApp.Core.Dto;
using TecNM.NotesApp.Core.Http;

namespace TecNM.NotesApp.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ReminderController: ControllerBase
{
    private readonly IReminderService _reminderService;
    
    public ReminderController(IReminderService reminderService)
    {
        _reminderService = reminderService;
    }
    
    [HttpGet]
    public async Task<ActionResult<Response<List<ReminderDto>>>> GetAll(int id)
    {
        var response = new Response<List<ReminderDto>>
        {
            Data = await _reminderService.GetAllAsync(id)
        };
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<ReminderDto>>> Post([FromBody] ReminderDto reminderDto)
    {
        var response = new Response<ReminderDto>()
        {
            Data = await _reminderService.SaveAsync(reminderDto)
        };
        
        return Created($"/api/[controller]/{response.Data.Id}", response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<ReminderDto>>> GetById(int id)
    {
        var response = new Response<ReminderDto>();
        
        if (!await _reminderService.ReminderExist(id))
        {
            response.Errors.Add("Reminder not found");
            return NotFound(response);
        }

        response.Data = await _reminderService.GetById(id);
        
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<ReminderDto>>> Update([FromBody] ReminderDto reminderDto)
    {

        var response = new Response<ReminderDto>();
        
        if (!await _reminderService.ReminderExist(reminderDto.Id))
        {
            response.Errors.Add("Reminder not found");
            return NotFound(response);
        }
        
        response.Data = await _reminderService.UpdateAsync(reminderDto);
        
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _reminderService.DeleteAsync(id);
        response.Data = result;
        
        return Ok(response);
    }
}