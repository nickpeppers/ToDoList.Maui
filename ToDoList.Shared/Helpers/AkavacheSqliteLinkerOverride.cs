using Akavache.Sqlite3;

namespace ToDoList.Shared.Helpers
{
    /// <summary>
    /// Required by Akavache to prevent removal during build
    /// </summary>
    [Preserve]
    public static class LinkerPreserve
    {
        static LinkerPreserve()
        {
            var persistentName = typeof(SQLitePersistentBlobCache).FullName;
            var encryptedName = typeof(SQLiteEncryptedBlobCache).FullName;
        }
    }

    public class PreserveAttribute : Attribute { }
}