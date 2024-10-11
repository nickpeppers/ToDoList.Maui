using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Shared.Helpers;
using ToDoList.Shared.Models;
using ToDoList.Shared.Services;

namespace ToDoList.Shared.ViewModels
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