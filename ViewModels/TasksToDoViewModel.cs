using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ToDoList.Maui.Helpers;
using ToDoList.Maui.Models;
using ToDoList.Maui.Pages;
using ToDoList.Maui.Services;

namespace ToDoList.Maui.ViewModels
{
    public partial class TasksToDoViewModel : ObservableObject
    {
        static readonly INavigationService _navigationService = ServiceContainer.Resolve<INavigationService>();
        static readonly IDisplayPromptService _displayPromptService = ServiceContainer.Resolve<IDisplayPromptService>();
        static readonly ICacheService _cacheService = ServiceContainer.Resolve<ICacheService>();

        [ObservableProperty]
        ObservableCollection<TasksToDo> _tasksToDoCollection = new ObservableCollection<TasksToDo>();

        bool _cacheLoaded = false;

        [RelayCommand]
        async Task Appearing()
        {
            if (_cacheLoaded)
                return;

            await LoadFromCache();
        }

        [RelayCommand]
        async Task ModifyTasksToDoTitle(TasksToDo tasksToDo)
        {
            var newTitle = await _displayPromptService.DisplayPromptAsync("Task", "Please add a title", placeholder: "Title", maxLength: 32, initialValue: tasksToDo.Title ?? "");
            if (!string.IsNullOrEmpty(newTitle))
            {
                tasksToDo.Title = newTitle;
                await SaveToCache();
            }
        }

        [RelayCommand]
        async Task AddTasksToDo()
        {
            var title = await _displayPromptService.DisplayPromptAsync("Task", "Please add a title", placeholder: "Title", maxLength: 32);
            if (!string.IsNullOrEmpty(title))
            {
                var newTasksToDo = new TasksToDo() { Title = title };
                TasksToDoCollection?.Add(newTasksToDo);
                await SaveToCache();
            }
        }

        [RelayCommand]
        async Task ModifyToDoItems(TasksToDo tasksToDo)
        {
            await _navigationService.NavigateToAsync(nameof(ToDoListPage), new Dictionary<string, object>
            {
                { Constants.ParentId, tasksToDo.Id },
                { Constants.TasksTodo, TasksToDoCollection ?? new ObservableCollection<TasksToDo>() },
                { Constants.ToDoList, tasksToDo.ToDoItems }
            });
        }

        [RelayCommand]
        async Task RemoveTasksToDo(TasksToDo tasksToDo)
        {
            TasksToDoCollection?.Remove(tasksToDo);
            await SaveToCache();
        }

        async Task LoadFromCache()
        {
            try
            {
                var tasksToDoList = await _cacheService.GetTasksToDo();
                if (tasksToDoList != null)
                {
                    TasksToDoCollection = new ObservableCollection<TasksToDo>(tasksToDoList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from cache: {ex}");
            }
            finally
            {
                _cacheLoaded = true;
            }
        }

        async Task SaveToCache()
        {
            try
            {
                var tasksToDoList = TasksToDoCollection?.ToList();
                if (tasksToDoList != null)
                {
                    await _cacheService.SaveTasksToDo(tasksToDoList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to cache: {ex}");
            }
        }
    }
}