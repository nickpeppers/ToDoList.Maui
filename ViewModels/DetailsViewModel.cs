using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Maui.Helpers;
using ToDoList.Maui.Models;
using ToDoList.Maui.Services;

namespace ToDoList.Maui.ViewModels
{
    public partial class DetailsViewModel : ObservableObject, IQueryAttributable
    {
        static readonly INavigationService _navigationService = ServiceContainer.Resolve<INavigationService>();

        [ObservableProperty]
        ToDoItem? _toDoItem;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            ToDoItem = query[Constants.ToDoItem] as ToDoItem;
        }

        [RelayCommand]
        async Task Close()
        {
            await _navigationService.PopAsync();
        }
    }
}