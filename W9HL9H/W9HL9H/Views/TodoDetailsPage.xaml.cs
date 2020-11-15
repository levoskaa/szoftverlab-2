using System;
using System.Collections.ObjectModel;
using System.Linq;
using W9HL9H.Models;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace W9HL9H.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TodoDetailsPage : Page
    {
        public TodoItem TodoItem { get; set; } = new TodoItem();
        public ObservableCollection<Priority> Priorities { get; set; } = new ObservableCollection<Priority>(Enum.GetValues(typeof(Priority)).Cast<Priority>());

        public TodoDetailsPage()
        {
            this.InitializeComponent();

            DataContext = this;
        }
    }
}
