namespace ToDoList.Shared.Services
{
    public class NavigationService : INavigationService
    {
        public Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null)
        {
            return routeParameters != null
               ? Shell.Current.GoToAsync(route, routeParameters)
               : Shell.Current.GoToAsync(route);
        }

        public Task PopAsync(IDictionary<string, object>? routeParameters = null)
        {
            return routeParameters != null
               ? Shell.Current.GoToAsync("..", routeParameters)
               : Shell.Current.GoToAsync("..");
        }
    }

    public interface INavigationService
    {
        Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null);

        Task PopAsync(IDictionary<string, object>? routeParameters = null);
    }
}
