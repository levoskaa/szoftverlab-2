using TodoXaml.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TodoXaml.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TodoDetailsPage : Page
    {
        public TodoDetailsPage()
        {
            this.InitializeComponent();
            DataContext = new TodoDetailsPageViewModel();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // MainPage.Todos.Add(Todo);

            Frame.GoBack();
            DataContext = new TodoDetailsPageViewModel();
        }
    }
}
