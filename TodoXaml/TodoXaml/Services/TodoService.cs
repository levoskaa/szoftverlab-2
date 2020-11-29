using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TodoXaml.Models;

namespace TodoXaml.Services
{
    public class TodoService
    {
        private readonly Uri _serverUrl = new Uri("https://bmetodoservice.azurewebsites.net");

        public async Task<List<TodoItem>> GetTodosAsync()
        {
            return await GetAsync<List<TodoItem>>(new Uri(_serverUrl, "api/Todo"));
        }

        public async Task<TodoItem> GetTodo(int id)
        {
            return await GetAsync<TodoItem>(new Uri(_serverUrl, $"api/Todo/{id}"));
        }

        private async Task<T> GetAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }
    }
}
