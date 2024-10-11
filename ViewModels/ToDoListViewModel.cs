using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ToDoList.Maui.Helpers;
using ToDoList.Maui.Models;
using ToDoList.Maui.Pages;
using ToDoList.Maui.Services;

namespace ToDoList.Maui.ViewModels
{
    public partial class ToDoListViewModel : ObservableObject, IQueryAttributable
    {
        static readonly INavigationService _navigationService = ServiceContainer.Resolve<INavigationService>();
        static readonly ICacheService _cacheService = ServiceContainer.Resolve<ICacheService>();

        [ObservableProperty]
        ObservableCollection<ToDoItem>? _toDoItems = new ObservableCollection<ToDoItem>();

        ObservableCollection<TasksToDo>? _tasksToDoCollection = new ObservableCollection<TasksToDo>();

        string _parentId = string.Empty;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _parentId = query[Constants.ParentId].ToString() ?? string.Empty;
            _tasksToDoCollection = query[Constants.TasksTodo] as ObservableCollection<TasksToDo>;
            ToDoItems = query[Constants.ToDoList] as ObservableCollection<ToDoItem>;
        }

        [RelayCommand]
        async Task Appearing()
        {
            await SaveToCache();
        }

        [RelayCommand]
        async Task CheckChanged()
        {
            await SaveToCache();
        }

        [RelayCommand]
        async Task ModifyToDoItem(ToDoItem todoItem)
        {
            await _navigationService.NavigateToAsync(nameof(DetailsPage), new Dictionary<string, object>
            {
                { Constants.ToDoItem, todoItem }
            });
        }

        [RelayCommand]
        async Task AddToDoItem ()
        {
            var newToDoItem = new ToDoItem() { ParentId = _parentId };
            ToDoItems?.Add(newToDoItem);

            await SaveToCache();

            await _navigationService.NavigateToAsync(nameof(DetailsPage), new Dictionary<string, object>
            {
                { Constants.ToDoItem, newToDoItem }
            });
        }

        [RelayCommand]
        async Task RemoveItem(ToDoItem toDoItem)
        {
            ToDoItems?.Remove(toDoItem);
            await SaveToCache();
        }

        async Task SaveToCache()
        {
            try
            {
                var tasksToDoCollection = _tasksToDoCollection?.ToList();
                if (tasksToDoCollection != null)
                {
                    await _cacheService.SaveTasksToDo(tasksToDoCollection);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to cache: {ex}");
            }
        }
    }
}