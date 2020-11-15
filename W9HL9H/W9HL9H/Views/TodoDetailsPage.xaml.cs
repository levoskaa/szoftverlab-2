using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using W9HL9H.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace W9HL9H.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TodoDetailsPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TodoItem todoItem = new TodoItem();
        public TodoItem TodoItem
        {
            get { return todoItem; }
            set
            {
                if (todoItem != value)
                {
                    todoItem = value;
                    RaisePropertyChanged("TodoItem");
                }
            }
        }
        public ObservableCollection<Priority> Priorities { get; set; } = new ObservableCollection<Priority>(Enum.GetValues(typeof(Priority)).Cast<Priority>());

        public TodoDetailsPage()
        {
            this.InitializeComponent();
            DataContext = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = (TodoItem)e.Parameter;
            if (parameter != null)
            {
                TodoItem = parameter;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            TodoItem.Id = MainPage.Todos.Max(t => t.Id) + 1;
            MainPage.Todos.Add(TodoItem);
            Frame.Navigate(typeof(MainPage), null);
        }

        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
