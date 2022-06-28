using ODAMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ODAMobile.IService
{
    public interface ISettingService
    {
        void InsertCauHinh(SettingModel st);
        void UpdateCauHinh(SettingModel st);
        void DeleteCauHinh();
        SettingModel GetSettings();
    }
}
