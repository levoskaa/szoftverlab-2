using TodoXaml.TodoServiceApi.Models;
using TodoXaml.ViewModels;
using Windows.UI.Xaml.Controls;

namespace TodoXaml
{
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new MainPageViewModel();
            //NavigationCacheMode = NavigationCacheMode.Required;

            DataContext = ViewModel;
        }

        private void Todos_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedTodo = (TodoItem)e.ClickedItem;
            ViewModel.OpenTodoDetails(clickedTodo.Id);
        }
    }
}
