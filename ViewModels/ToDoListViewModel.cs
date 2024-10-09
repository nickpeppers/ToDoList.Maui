using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ToDoList.Maui.Helpers;
using ToDoList.Maui.Models;
using ToDoList.Maui.Pages;
using ToDoList.Maui.Services;

namespace ToDoList.Maui.ViewModels
{
    public partial class ToDoListViewModel : ObservableObject
    {
        static readonly INavigationService _navigationService = ServiceContainer.Resolve<INavigationService>();

        [ObservableProperty]
        ObservableCollection<ToDoItem>? _toDoItems = new ObservableCollection<ToDoItem>();

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
            var newToDoItem = new ToDoItem ();
            ToDoItems?.Add(newToDoItem);

            await _navigationService.NavigateToAsync(nameof(DetailsPage), new Dictionary<string, object>
            {
                { Constants.ToDoItem, newToDoItem }
            });
        }

        [RelayCommand]
        void RemoveItem(ToDoItem toDoItem)
        {
            ToDoItems?.Remove(toDoItem);
        }
    }
}