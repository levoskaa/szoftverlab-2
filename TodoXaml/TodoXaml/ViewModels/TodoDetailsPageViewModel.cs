using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using TodoXaml.Models;
using TodoXaml.Services;
using Windows.UI.Xaml.Navigation;

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

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Todo = parameter != null ? await new TodoService().GetTodo((int)parameter) : new TodoItem();
            await base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
