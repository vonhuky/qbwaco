using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ODAMobile.Droid.Implementations;
using ODAMobile.Helpers;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidSQLite))]
namespace ODAMobile.Droid.Implementations
{
    public class AndroidSQLite : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            // Documents folder
            var dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(dbpath, DatabaseHelper.DbFileName);
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}