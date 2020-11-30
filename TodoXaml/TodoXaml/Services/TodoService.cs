using Microsoft.Rest;
using System;
using TodoXaml.TodoServiceApi;

namespace TodoXaml.Services
{
    public class TodoService : TodoServiceApiClient
    {
        public TodoService() : base(new Uri("https://bmetodoservice.azurewebsites.net"), new TokenCredentials("mock"))
        {
        }
    }
}
