using Newtonsoft.Json;
using TecNM.NotesApp.Core.Dto;
using TecNM.NotesApp.Core.Http;
using TEcNM.NotesApp.Website.Services.Interfaces;

namespace TEcNM.NotesApp.Website.Services;

public class NoteService: INoteService
{
    //TODO Change url and endpoint also install newtonsoft
    
    private readonly string _baseUrl = "http://localhost:7257/";
    private readonly string _endPoint = "api/Note";

    public NoteService()
    {
    }

    public async Task<Response<List<NoteDto>>> GetAllAsync(int id)
    {
        var url = $"{_baseUrl}{_endPoint}?idUser={id}";
        
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<List<NoteDto>>>(json);

        return response;
    }

    public async Task<Response<NoteDto>> GetById(int id)
    {
        var url = $"{_baseUrl}{_endPoint}/{id}";

        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<NoteDto>>(json);

        return response;
    }

    public async Task<Response<NoteDto>> SaveAsync(NoteDto note)
    {
        var url = $"{_baseUrl}{_endPoint}";
        System.Console.WriteLine(note.idUser);
        note.idCategory = 1;
        var jsonRequest = JsonConvert.SerializeObject(note);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        
        var client = new HttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<NoteDto>>(json);
        return response;
    }

    public async Task<Response<NoteDto>> UpdateAsync(NoteDto note)
    {
        var url = $"{_baseUrl}{_endPoint}";

        var jsonRequest = JsonConvert.SerializeObject(note);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        
        var client = new HttpClient();
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<Response<NoteDto>>(json);

        return response;
        
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var url = $"{_baseUrl}{_endPoint}/{id}";

        var client = new HttpClient();
        var res = await client.DeleteAsync(url);
        var json = await res.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<Response<bool>>(json);

        return response;
    }
    
}