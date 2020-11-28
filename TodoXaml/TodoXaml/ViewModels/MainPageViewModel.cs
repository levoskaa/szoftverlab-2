using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Mvvm;
using TodoXaml.Models;
using TodoXaml.Services;
using Windows.UI.Xaml.Navigation;

namespace TodoXaml.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<TodoItem> Todos { get; set; } = new ObservableCollection<TodoItem>();

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var todos = await new TodoService().GetTodosAsync();
            Todos = new ObservableCollection<TodoItem>(todos);
            await base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
