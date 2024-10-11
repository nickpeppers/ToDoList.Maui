using Akavache;
using MemoryPack;
using System.Reactive.Linq;
using ToDoList.Maui.Models;
using ToDoList.Maui.Services;

namespace ToDoList.Maui.Helpers
{
    /// <summary>
    /// Cache extensions to serialize/deserialize data to/from a byte[] for storage using MemoryPack
    /// </summary>
    public static class CacheExtensions
    {
        public static async Task SaveTasksToDo(this ICacheService cacheService, List<TasksToDo> tasksToDo)
        {
            var bytes = Serialize(tasksToDo);
            await cacheService.Secure.Insert(Constants.TasksTodo, bytes);
        }

        public static async Task<List<TasksToDo>> GetTasksToDo(this ICacheService cacheService)
        {
            var bytes = await cacheService.Secure.Get(Constants.TasksTodo);
            var tasksToDo = Deserialize<List<TasksToDo>>(bytes);
            return tasksToDo;
        }

        public static byte[] Serialize<T>(T obj)
        {
            var bytes = MemoryPackSerializer.Serialize(obj);
            return bytes;
        }

        public static T Deserialize<T>(byte[] byteArray) where T : new()
        {
            var obj = MemoryPackSerializer.Deserialize<T>(byteArray);
            return obj ?? new T();
        }
    }
}