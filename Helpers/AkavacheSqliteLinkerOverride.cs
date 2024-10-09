using Akavache.Sqlite3;

namespace ToDoList.Maui.Helpers
{
    [Preserve]
    public static class LinkerPreserve
    {
        static LinkerPreserve()
        {
            var persistentName = typeof(SQLitePersistentBlobCache).FullName;
            var encryptedName = typeof(SQLiteEncryptedBlobCache).FullName;
        }
    }

    public class PreserveAttribute : Attribute
    {
    }
}