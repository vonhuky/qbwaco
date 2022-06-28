using ODAMobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ODAMobile.Helpers
{
    public class DatabaseHelper
    {
        static SQLiteConnection sqliteconnection;
        public const string DbFileName = "ODAs.db";

        public DatabaseHelper()
        {
            sqliteconnection = DependencyService.Get<ISQLite>().GetConnection();
            sqliteconnection.CreateTable<UserInfo>();
        }

        public void InsertUser(UserInfo user)
        {
            sqliteconnection.Insert(user);
        }

        public List<UserInfo> GetAllUserData()
        {
            return (from data in sqliteconnection.Table<UserInfo>()
                    select data).ToList();
        }
    }
}
