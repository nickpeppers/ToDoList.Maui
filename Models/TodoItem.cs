using CommunityToolkit.Mvvm.ComponentModel;

namespace ToDoList.Maui.Models
{
    public partial class ToDoItem : ObservableObject
    {
        public string Id = Guid.NewGuid().ToString();

        [ObservableProperty]
        string? _title;

        [ObservableProperty]
        string? _description;

        [ObservableProperty]
        bool _completed;
    }
}