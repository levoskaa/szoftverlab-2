using TodoXaml.ViewModels;
using TodoXaml.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TodoXaml
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //NavigationCacheMode = NavigationCacheMode.Required;

            DataContext = new MainPageViewModel();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TodoDetailsPage), null);
        }

        private void Todos_OnItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(TodoDetailsPage), e.ClickedItem);
        }
    }
}
