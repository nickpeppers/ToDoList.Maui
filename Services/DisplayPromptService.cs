namespace ToDoList.Maui.Services
{
    /// <summary>
    /// Wrapper for Maui DisplayPromptAsync
    /// </summary>
    public class DisplayPromptService : IDisplayPromptService
    {
        public Task<string> DisplayPromptAsync(string title, string message, string accept, string cancel, string? placeholder, int maxLength, string initialValue)
        {
            return Shell.Current.DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength, initialValue: initialValue);
        }
    }

    public interface IDisplayPromptService
    {
        /// <summary>
        /// Displays entry text prompt
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="accept"></param>
        /// <param name="cancel"></param>
        /// <param name="placeholder"></param>
        /// <param name="maxLength"></param>
        /// <param name="initialValue"></param>
        /// <returns></returns>
        Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel", string? placeholder = null, int maxLength = -1, string initialValue = "");
    }
}