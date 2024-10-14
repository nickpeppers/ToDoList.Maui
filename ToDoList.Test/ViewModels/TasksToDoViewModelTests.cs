using Moq;
using System.Collections.ObjectModel;
using ToDoList.Shared.Helpers;
using ToDoList.Shared.Models;
using ToDoList.Shared.Services;
using ToDoList.Shared.ViewModels;

namespace ToDoList.Test.ViewModels
{
    public class TasksToDoViewModelTests
    {
        static readonly List<TasksToDo> _tasksToDoList = new List<TasksToDo>()
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
            }
        };

        Mock<IDisplayPromptService> _displayPromptService = new Mock<IDisplayPromptService>();

        public TasksToDoViewModelTests()
        {
            _displayPromptService.SetupSequence(d => d.DisplayPromptAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(Task.FromResult("new Task"))
                .Returns(Task.FromResult("new Task Name"));

            ServiceContainer.Register<ICacheService>(() => new CacheService());
            ServiceContainer.Register<INavigationService>(() => new NavigationService());
            ServiceContainer.Register(() => _displayPromptService.Object);
        }

        [Fact]
        async Task AddTasksToDoTest()
        {
            var viewModel = new TasksToDoViewModel();
            viewModel.TasksToDoCollection =  new ObservableCollection<TasksToDo>(_tasksToDoList);

            await viewModel.AddTasksToDoCommand.ExecuteAsync(null);
            var lastTask = viewModel.TasksToDoCollection.Last();

            Assert.NotNull(lastTask);
            Assert.Equal("new Task", lastTask.Title);
            Assert.Equal(4, viewModel.TasksToDoCollection.Count());
        }

        [Fact]
        async Task ModifyTasksToDoTitleTest()
        {
            var viewModel = new TasksToDoViewModel();
            viewModel.TasksToDoCollection = new ObservableCollection<TasksToDo>(_tasksToDoList);

            var firstTask = viewModel.TasksToDoCollection.First();
            await viewModel.ModifyTasksToDoTitleCommand.ExecuteAsync(firstTask);
            firstTask = viewModel.TasksToDoCollection.First();

            Assert.NotNull(firstTask);
            Assert.Equal("new Task Name", firstTask.Title);
            Assert.Equal(3, viewModel.TasksToDoCollection.Count());
        }

        [Fact]
        async Task RemoveTasksToDoTest()
        {
            var viewModel = new TasksToDoViewModel();
            viewModel.TasksToDoCollection = new ObservableCollection<TasksToDo>(_tasksToDoList);

            var lastTask = viewModel.TasksToDoCollection.Last();
            await viewModel.RemoveTasksToDoCommand.ExecuteAsync(lastTask);
            lastTask = viewModel.TasksToDoCollection.Last();

            Assert.NotNull(lastTask);
            Assert.Equal("Task 2", lastTask.Title);
            Assert.Equal(2, viewModel.TasksToDoCollection.Count());
        }
    }
}