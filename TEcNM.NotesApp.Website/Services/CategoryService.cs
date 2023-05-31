using Newtonsoft.Json;
using TecNM.NotesApp.Core.Dto;
using TecNM.NotesApp.Core.Http;
using TEcNM.NotesApp.Website.Services.Interfaces;

namespace TEcNM.NotesApp.Website.Services;

public class CategoryService: ICategoryInterface
{
    //TODO Change url and endpoint also install newtonsoft
      
      private readonly string _baseUrl = "http://localhost:7257/";
      private readonly string _endPoint = "api/Category";

      public CategoryService()
      {
      }

      public async Task<Response<List<CategoryDto>>> GetAllAsync()
      {
          var url = $"{_baseUrl}{_endPoint}";
          
          var client = new HttpClient();
          var res = await client.GetAsync(url);
          var json = await res.Content.ReadAsStringAsync();
          var response = JsonConvert.DeserializeObject<Response<List<CategoryDto>>>(json);

          return response;
      }

      public async Task<Response<CategoryDto>> GetById(int id)
      {
          var url = $"{_baseUrl}{_endPoint}/{id}";

          var client = new HttpClient();
          var res = await client.GetAsync(url);
          var json = await res.Content.ReadAsStringAsync();
          var response = JsonConvert.DeserializeObject<Response<CategoryDto>>(json);

          return response;
      }

      public async Task<Response<CategoryDto>> SaveAsync(CategoryDto category)
      {
          var url = $"{_baseUrl}{_endPoint}";

          var jsonRequest = JsonConvert.SerializeObject(category);
          var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
          
          var client = new HttpClient();
          var res = await client.PostAsync(url, content);
          var json = await res.Content.ReadAsStringAsync();
          
          var response = JsonConvert.DeserializeObject<Response<CategoryDto>>(json);
          return response;
      }

      public async Task<Response<CategoryDto>> UpdateAsync(CategoryDto category)
      {
          var url = $"{_baseUrl}{_endPoint}";

          var jsonRequest = JsonConvert.SerializeObject(category);
          var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
          
          var client = new HttpClient();
          var res = await client.PutAsync(url, content);
          var json = await res.Content.ReadAsStringAsync();

          var response = JsonConvert.DeserializeObject<Response<CategoryDto>>(json);

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