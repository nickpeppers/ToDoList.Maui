using CommunityToolkit.Mvvm.ComponentModel;
using MemoryPack;

namespace ToDoList.Shared.Models
{
    /// <summary>
    /// List of to do items per task
    /// NOTE: MemoryPackInclude required for serialization to save some values.
    /// </summary>
    [MemoryPackable]
    public partial class ToDoItem : ObservableObject
    {
        public required string ParentId { get; set; }

        public string Id = Guid.NewGuid().ToString();

        [MemoryPackInclude]
        [ObservableProperty]
        string _title = string.Empty;

        [MemoryPackInclude]
        [ObservableProperty]
        string _description = string.Empty;

        [MemoryPackInclude]
        [ObservableProperty]
        bool _completed;
    }
}