using System.Collections.ObjectModel;
using ToDoList.Shared.Helpers;
using ToDoList.Shared.Models;

namespace ToDoList.Test
{
    public class TasksToDoFixture : IDisposable
    {
        public byte[] TasksToDoBytes { get; private set; }
        public List<TasksToDo> TasksToDoList { get; private set; } = new List<TasksToDo>()
        {
            new TasksToDo()
            {
                Title = "Task 1", ToDoItems = new ObservableCollection<ToDoItem>(),
            },
            new TasksToDo()
            {
                Title = "Task 2", ToDoItems = new ObservableCollection<ToDoItem>(),
            },
            new TasksToDo()
            {
                Title = "Task 3", ToDoItems = new ObservableCollection<ToDoItem>(),
            },
            new TasksToDo()
            {
                Title = "Task 4", ToDoItems = new ObservableCollection<ToDoItem>(),
            },
        };

        public TasksToDoFixture()
        {
            foreach (var task in TasksToDoList)
            {
                for (var i = 0; i < 2; i++)
                {
                    var num = i + 1;
                    var todoItem = new ToDoItem
                    {
                        ParentId = task.Id,
                        Title = $"Item {num}",
                        Description = $"Description {num}",
                        Completed = i % 2 == 0,
                    };
                    task.ToDoItems.Add(todoItem);
                }
            }

            TasksToDoBytes = CacheExtensions.Serialize(TasksToDoList);
        }

        public void Dispose() { }
    }
}