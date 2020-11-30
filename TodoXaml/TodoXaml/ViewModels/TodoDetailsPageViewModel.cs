using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using TodoXaml.Services;
using TodoXaml.TodoServiceApi.Models;
using Windows.UI.Xaml.Navigation;
using Priority = TodoXaml.Models.Priority;

namespace TodoXaml.ViewModels
{
    class TodoDetailsPageViewModel : ViewModelBase
    {
        private TodoItem todo;
        public TodoItem Todo
        {
            get
            {
                return todo;
            }
            set
            {
                Set(ref todo, value);
            }
        }
        public IEnumerable<Priority> PriorityValues => Enum.GetValues(typeof(Priority)).Cast<Priority>();
        private DelegateCommand saveCommand;
        public DelegateCommand SaveCommand => saveCommand ?? (saveCommand = new DelegateCommand(Save));
        private bool isCreateOperation;

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (parameter != null)
            {
                Todo = (await new TodoService().GetTodoWithHttpMessagesAsync((int)parameter)).Body;
                isCreateOperation = false;
            }
            else
            {
                Todo = new TodoItem();
                isCreateOperation = true;
            }
            await base.OnNavigatedToAsync(parameter, mode, state);
        }
        private async void Save()
        {
            var todoService = new TodoService();
            if (isCreateOperation)
            {
                await todoService.AddTodoWithHttpMessagesAsync(Todo);
            }
            else
            {
                await todoService.UpdateTodoWithHttpMessagesAsync(Todo.Id, Todo);
            }
            NavigationService.Navigate(typeof(MainPage));
        }
    }
}
