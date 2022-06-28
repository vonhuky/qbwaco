using ODAMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ODAMobile.IService
{
    public interface ICauhinhService
    {
        void InsertCauHinh(SettingModel st);
        void UpdateCauHinh(SettingModel st);
        SettingModel GetSettings();
    }
}
