namespace ToDoList.Maui.Services
{
    public class DisplayPromptService : IDisplayPromptService
    {
        public Task<string> DisplayPromptAsync(string title, string message, string accept, string cancel, string? placeholder, int maxLength, string initialValue)
        {
            return Shell.Current.DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength, initialValue: initialValue);
        }
    }

    public interface IDisplayPromptService
    {
        Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel", string? placeholder = null, int maxLength = -1, string initialValue = "");
    }
}