using System;
using System.IO;
using Microsoft.Data.Sqlite;

using ODAMobile.IService;
using SQLite;
using ODAMobile.Droid.Services;


[assembly: Xamarin.Forms.Dependency(typeof(LocalDatabaseService))]
namespace ODAMobile.Droid.Services
{
    public class LocalDatabaseService : ILocalDatabaseService
    {
        public static string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public static string dbName = "ODAs.db";

        public SQLiteConnection GetDatabaseConnection(string dbName)
        {
            return new SQLiteConnection(Path.Combine(dbPath, dbName));
        }

        public SQLiteConnection GetDatabaseConnection()
        {
            return new SQLiteConnection(Path.Combine(dbPath, dbName));
        }
    }
}