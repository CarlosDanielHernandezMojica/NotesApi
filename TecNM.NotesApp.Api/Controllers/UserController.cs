using Microsoft.AspNetCore.Mvc;
using TecNM.NotesApp.Api.Services.Interfaces;
using TecNM.NotesApp.Core.Dto;
using TecNM.NotesApp.Core.Http;

namespace TecNM.NotesApp.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<ActionResult<Response<List<UserDto>>>> GetAll()
    {
        var response = new Response<List<UserDto>>
        {
            Data = await _userService.GetAllAsync()
        };
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<UserDto>>> Post([FromBody] UserDto userDto)
    {

        System.Console.WriteLine(userDto.Username);
        System.Console.WriteLine(userDto.Password);
        var response = new Response<UserDto>()
        {
            Data = await _userService.SaveAsync(userDto)
        };
        
        return Created($"/api/[controller]/{response.Data.Id}", response);
    }
    
    [HttpPost]
    [Route("/login")]
    public async Task<ActionResult<Response<bool>>> Login([FromBody] UserDto userDto)
    {
        var response = new Response<UserDto>();
        
        var result = await _userService.Login(userDto.Username, userDto.Password);
        
        response.Data = result;
        response.Message = "Hola";
        
        return Ok(response);
    }
    
    // [HttpGet]
    // [Route("/registro")]
    // public async Task<ActionResult<Response<bool>>> Registro(string username, string password)
    // {
    //     var userDto = new UserDto()
    //     {
    //         Username = username,
    //         Password = password
    //     };
    //     
    //     var response = new Response<UserDto>()
    //     {
    //         Data = await _userService.SaveAsync(userDto)
    //     };
    //     
    //     return Ok(response);
    // }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<UserDto>>> GetById(int id)
    {
        var response = new Response<UserDto>();
        
        if (!await _userService.UserExist(id))
        {
            response.Errors.Add("User not found");
            return NotFound(response);
        }

        response.Data = await _userService.GetById(id);
        
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<UserDto>>> Update([FromBody] UserDto userDto)
    {

        var response = new Response<UserDto>();
        
        if (!await _userService.UserExist(userDto.Id))
        {
            response.Errors.Add("User not found");
            return NotFound(response);
        }
        
        response.Data = await _userService.UpdateAsync(userDto);
        
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _userService.DeleteAsync(id);
        response.Data = result;
        
        return Ok(response);
    }
}