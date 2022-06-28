using ODAMobile.IService;
using ODAMobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.TinyMVVM;

namespace ODAMobile.Service
{
    public class SettingService : ISettingService
    {

        private readonly SQLiteConnection LocalDb;

        public SettingService()
        {
            LocalDb = TinyIOC.Container.Resolve<SQLiteConnection>();
        }

        public void DeleteCauHinh()
        {
            //LocalDb.DeleteAll<SettingModel>();
            LocalDb.DropTable<SettingModel>();
        }

        public SettingModel GetSettings()
        {
            return LocalDb.Table<SettingModel>().FirstOrDefault();
        }

        public void InsertCauHinh(SettingModel st)
        {
            LocalDb.InsertOrReplace(st);
        }

        public void UpdateCauHinh(SettingModel st)
        {
            LocalDb.Update(st);
        }
    }
}
