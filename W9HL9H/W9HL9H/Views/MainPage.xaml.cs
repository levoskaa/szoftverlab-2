using System;
using System.Collections.Generic;
using W9HL9H.Models;
using W9HL9H.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace W9HL9H
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static List<TodoItem> Todos { get; set; } = new List<TodoItem>()
        {
            new TodoItem()
            {
                Id = 1,
                Title = "Tejet venni",
                Description = "Ha van tojás, hozz tizet!",
                Priority = Priority.Normal,
                IsDone = true,
                Deadline = DateTimeOffset.Now + TimeSpan.FromDays(1)
            },
            new TodoItem()
            {
                Id = 2,
                Title = "Megírni a szakdolgozatot",
                Description = "Minimum 40 oldal, szépen kitöltve screenshotokkal",
                Priority = Priority.High,
                IsDone = false,
                Deadline = new DateTime(2017, 12, 08, 12, 00, 00, 00)
            }
        };

        public MainPage()
        {
            this.InitializeComponent();

            DataContext = this;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TodoDetailsPage), null);
        }
    }
}
