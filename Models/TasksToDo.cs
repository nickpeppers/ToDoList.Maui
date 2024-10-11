using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using MemoryPack;

namespace ToDoList.Maui.Models
{
    /// <summary>
    /// Task with a list of to do items
    /// </summary>
    [MemoryPackable]
    public partial class TasksToDo : ObservableObject
    {
        public string Id = Guid.NewGuid().ToString();

        [ObservableProperty]
        public string _title = string.Empty;

        [ObservableProperty]
        public ObservableCollection<ToDoItem> _toDoItems = new ObservableCollection<ToDoItem>();
    }
}