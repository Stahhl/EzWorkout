using System;
using System.IO;

namespace EzWorkout.Database
{
    public static class Constants
    {
        public const string DatabaseFilename = "EzWorkoutDb.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
        public static bool DropDb()
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = Path.Combine(basePath, DatabaseFilename);

            if (File.Exists(dbPath) == false)
                return true;
            else
                File.Delete(dbPath);

            return File.Exists(dbPath) == false;
        }
    }
}
