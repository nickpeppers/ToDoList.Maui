using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ToDoList.Maui.Models
{
    public partial class TasksToDo : ObservableObject
    {
        public string Id = Guid.NewGuid().ToString();

        [ObservableProperty]
        public string? _title;

        [ObservableProperty]
        public ObservableCollection<ToDoItem> _toDoItems = new ObservableCollection<ToDoItem>();
    }
}