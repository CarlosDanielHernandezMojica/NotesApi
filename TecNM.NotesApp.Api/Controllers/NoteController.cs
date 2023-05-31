using Microsoft.AspNetCore.Mvc;
using TecNM.NotesApp.Api.Services.Interfaces;
using TecNM.NotesApp.Core.Dto;
using TecNM.NotesApp.Core.Http;

namespace TecNM.NotesApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController: ControllerBase
{
    private readonly INoteService _noteService;
    
    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }
    
    [HttpGet]
    public async Task<ActionResult<Response<List<NoteDto>>>> GetAllByIdUser(int idUser)
    {
        var response = new Response<List<NoteDto>>
        {
            Data = await _noteService.GetAllByIdUserAsync(idUser),
            Message = "Hola"

        };
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<NoteDto>>> Post([FromBody] NoteDto noteDto)
    {
        var response = new Response<NoteDto>()
        {
            Data = await _noteService.SaveAsync(noteDto)
        };
        
        return Created($"/api/[controller]/{response.Data.Id}", response);
    }
    
    [HttpGet]
    [Route("/savenote")]
    public async Task<ActionResult<Response<bool>>> SaveNote(string title, string descripcion, int iduser)
    {
        var noteDto = new NoteDto() 
        {
            Title = title,
            Description = descripcion,
            idUser = iduser,
            idCategory = 1
        };
        
        var response = new Response<NoteDto>()
        {
            Data = await _noteService.SaveAsync(noteDto),
            Message = "Hola"
        };
        
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<NoteDto>>> GetById(int id)
    {
        var response = new Response<NoteDto>();
        
        if (!await _noteService.NoteExist(id))
        {
            response.Errors.Add("Note not found");
            return NotFound(response);
        }

        response.Data = await _noteService.GetById(id);
        
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<NoteDto>>> Update([FromBody] NoteDto noteDto)
    {

        var response = new Response<NoteDto>();
        
        if (!await _noteService.NoteExist(noteDto.Id))
        {
            response.Errors.Add("Note not found");
            return NotFound(response);
        }
        
        response.Data = await _noteService.UpdateAsync(noteDto);
        
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _noteService.DeleteAsync(id);
        response.Data = result;
        
        return Ok(response);
    }
}