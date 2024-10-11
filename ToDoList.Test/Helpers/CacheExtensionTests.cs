using System.Reactive.Linq;
using ToDoList.Shared.Models;

namespace ToDoList.Test.Helpers
{
    public class CacheExtensionTests : IClassFixture<TasksToDoFixture>
    {
        TasksToDoFixture _tasksToDoFixture;

        public CacheExtensionTests(TasksToDoFixture tasksToDoFixture)
        {
            _tasksToDoFixture = tasksToDoFixture;
        }

        [Fact]
        void Serialize()
        {
            var tasksToByteArray = _tasksToDoFixture.CacheService.Serialize(_tasksToDoFixture.TasksToDoList);

            Assert.NotNull(tasksToByteArray);
            Assert.NotEmpty(tasksToByteArray);
        }

        [Fact]
        void Deserialize()
        {
            var tasksToList = _tasksToDoFixture.CacheService.Deserialize<List<TasksToDo>>(_tasksToDoFixture.TasksToDoBytes);
            var firstTask = tasksToList.First();
            var lastTask = tasksToList.Last();
            var count = tasksToList.Count();

            Assert.NotNull(tasksToList);
            Assert.NotEmpty(tasksToList);

            Assert.Equal("Task 1", firstTask.Title);
            Assert.Equal(2, firstTask.ToDoItems.Count());

            Assert.Equal("Task 4", lastTask.Title);
            Assert.Equal(2, lastTask.ToDoItems.Count());

            Assert.Equal(4, count);
        }
    }
}