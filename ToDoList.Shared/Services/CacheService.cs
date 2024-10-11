using Akavache;
using MemoryPack;
using System.Reactive.Linq;
using ToDoList.Shared.Models;

namespace ToDoList.Shared.Services
{
    /// <summary>
    /// Akavache cache implementation to store data
    /// </summary>
    public class CacheService : ICacheService
    {
        public ISecureBlobCache Secure
        {
            get => BlobCache.Secure;
            set => BlobCache.Secure = value;
        }

        public ISecureBlobCache InMemory
        {
            get => BlobCache.InMemory;
            set => BlobCache.InMemory = value;
        }

        public IBlobCache LocalData
        {
            get => BlobCache.LocalMachine;
            set => BlobCache.LocalMachine = value;
        }

        public IBlobCache UserAccount
        {
            get => BlobCache.UserAccount;
            set => BlobCache.UserAccount = value;
        }

        public async Task ClearAll()
        {
            var cachesCleared = Secure.InvalidateAll().Merge(InMemory.InvalidateAll()).Merge(LocalData.InvalidateAll()).Merge(UserAccount.InvalidateAll());
            await cachesCleared;
        }

        public async Task ClearSecure()
        {
            await Secure.InvalidateAll();
        }

        public async Task ClearInMemory()
        {
            await InMemory.InvalidateAll();
        }

        public async Task ClearLocalData()
        {
            await LocalData.InvalidateAll();
        }

        public async Task ClearUserAccount()
        {
            await UserAccount.InvalidateAll();
        }

        public async Task Shutdown()
        {
            await BlobCache.Shutdown();
        }

        public async Task SaveTasksToDo(List<TasksToDo> tasksToDo)
        {
            var bytes = Serialize(tasksToDo);
            await Secure.Insert(Constants.TasksTodo, bytes);
        }

        public async Task<List<TasksToDo>> GetTasksToDo()
        {
            var bytes = await Secure.Get(Constants.TasksTodo);
            var tasksToDo = Deserialize<List<TasksToDo>>(bytes);
            return tasksToDo;
        }

        public byte[] Serialize<T>(T obj)
        {
            var bytes = MemoryPackSerializer.Serialize(obj);
            return bytes;
        }

        public T Deserialize<T>(byte[] byteArray) where T : new()
        {
            var obj = MemoryPackSerializer.Deserialize<T>(byteArray);
            return obj ?? new T();
        }
    }

    public interface ICacheService
    {
        /// <summary>
        /// For saving sensitive data - like credentials.
        /// </summary>
        ISecureBlobCache Secure { get; set; }

        /// <summary>
        /// A database, kept in memory. The data is stored for the lifetime of the app and cleared on app restart
        /// </summary>
        ISecureBlobCache InMemory { get; set; }

        /// <summary>
        /// Local cached app data that can get deleted automatically if running out of space and may get deleted without notification.
        /// </summary>
        IBlobCache LocalData { get; set; }

        /// <summary>
        /// Primarily for user settings and can get automatically backed up to the cloud. Don't save sensitive data here.
        /// </summary>
        IBlobCache UserAccount { get; set; }

        /// <summary>
        /// Clears all cached data including Secure, InMemory, LocalData, and UserAccount
        /// </summary>
        /// <returns></returns>
        Task ClearAll();
        Task ClearSecure();
        Task ClearInMemory();
        Task ClearLocalData();
        Task ClearUserAccount();

        /// <summary>
        /// From docs: Critical to the integrity of your Akavache cache and must be called when your application shuts down
        /// NOTE: Doesn't seem like it actually has to be called data still appears to always persist. May have been changed in later versions.
        /// </summary>
        /// <returns></returns>
        Task Shutdown();

        /// <summary>
        /// Saves TasksToDo to storage
        /// </summary>
        /// <param name="tasksToDo"></param>
        /// <returns></returns>
        Task SaveTasksToDo(List<TasksToDo> tasksToDo);

        /// <summary>
        /// Retrieves list of TasksToDo from storage
        /// </summary>
        /// <returns></returns>
        Task<List<TasksToDo>> GetTasksToDo();

        /// <summary>
        /// Serializes object to byte[]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        byte[] Serialize<T>(T obj);

        /// <summary>
        /// Deserialized byte[] to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        T Deserialize<T>(byte[] byteArray) where T : new();
    }
}