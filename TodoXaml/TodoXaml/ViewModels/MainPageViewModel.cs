using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Mvvm;
using TodoXaml.Models;
using TodoXaml.Services;
using TodoXaml.Views;
using Windows.UI.Xaml.Navigation;

namespace TodoXaml.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<TodoItem> todos = new ObservableCollection<TodoItem>();
        public ObservableCollection<TodoItem> Todos
        {
            get
            {
                return todos;
            }
            set
            {
                Set(ref todos, value);
            }
        }
        private DelegateCommand<object> newTodoCommand;
        public DelegateCommand<object> NewTodoCommand => newTodoCommand ?? (newTodoCommand = new DelegateCommand<object>(NavigateToTodoDetailsPage));

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var todos = await new TodoService().GetTodosAsync();
            Todos = new ObservableCollection<TodoItem>(todos);
            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void OpenTodoDetails(int todoId)
        {
            NavigateToTodoDetailsPage(todoId);
        }

        private void NavigateToTodoDetailsPage(object todoId = null)
        {
            NavigationService.Navigate(typeof(TodoDetailsPage), todoId);
        }
    }
}
