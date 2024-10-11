using CommunityToolkit.Mvvm.ComponentModel;
using MemoryPack;
using System.Collections.ObjectModel;

namespace ToDoList.Shared.Models
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