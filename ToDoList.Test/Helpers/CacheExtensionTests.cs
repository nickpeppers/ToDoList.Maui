using Akavache;
using Moq;
using System.Reactive.Linq;
using ToDoList.Shared;
using ToDoList.Shared.Helpers;
using ToDoList.Shared.Models;
using ToDoList.Shared.Services;

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
            var tasksToByteArray = CacheExtensions.Serialize(_tasksToDoFixture.TasksToDoList);

            Assert.NotNull(tasksToByteArray);
            Assert.NotEmpty(tasksToByteArray);
        }

        [Fact]
        void Deserialize()
        {
            var tasksToList = CacheExtensions.Deserialize<List<TasksToDo>>(_tasksToDoFixture.TasksToDoBytes);
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

        //[Fact]
        //async void GetTasksTodo()
        //{
        //    var mockCache = new Mock<ICacheService>();
        //    var mockSecureBlobCache = new Mock<ISecureBlobCache>();
        //    var mockObservable = new Mock<IObservable<byte[]>>();

        //    //mockObservable.ev;
        //    //mockCache.Setup(x => x.Secure).Returns(mockSecureBlobCache.Object);
        //    mockCache.Setup(sec => sec.Secure.Get(Constants.TasksTodo)).Returns(_tasksToDoFixture.TasksToDoBytes);
        //    //var mockSecureStorage = new Mock<ISecureBlobCache>();
        //    //var mockObservable = new Mock<IObservable<byte[]>>();
        //    ////mockObservable.Setup(o => o.
        //    //mockSecureStorage.Setup(get => get.Get(Constants.TasksTodo)).ReturnsAsync(_tasksToDoBytes);
        //    //mockCache.Setup(secStorage => secStorage.Secure).Returns(mockSecureStorage.Object);

        //    var tasksToDo = await mockCache.Object.GetTasksToDo();
        //    Assert.NotNull(tasksToDo);
        //    Assert.NotEmpty(tasksToDo);
        //}
    }
}