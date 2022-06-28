using Newtonsoft.Json;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ODAMobile.Helpers
{
    public static class Settings
    {
        private static ISettings CrossSettings => Plugin.Settings.CrossSettings.IsSupported ? Plugin.Settings.CrossSettings.Current : null;

        #region LoggedIn

        public static bool LoggedIn
        {
            get => CrossSettings.GetValueOrDefault(nameof(LoggedIn), false);
            set => CrossSettings.AddOrUpdateValue(nameof(LoggedIn), value);
        }

        private static bool IsLoggedInSet => CrossSettings.Contains(nameof(LoggedIn));

        public static void RemoveLoggedIn() => CrossSettings.Remove(nameof(LoggedIn));

        #endregion LoggedIn

        #region Username

        public static string Username
        {
            get => CrossSettings.GetValueOrDefault(nameof(Username), string.Empty);
            set => CrossSettings.AddOrUpdateValue(nameof(Username), value);
        }

        private static bool IsUsernameSet => CrossSettings.Contains(nameof(Username));

        public static void RemoveUsername() => CrossSettings.Remove(nameof(Username));

        #endregion Username

        #region FullName

        public static string Fullname
        {
            get => CrossSettings.GetValueOrDefault(nameof(Username), string.Empty);
            set => CrossSettings.AddOrUpdateValue(nameof(Username), value);
        }

        private static bool IsFullnameSet => CrossSettings.Contains(nameof(Username));

        public static void RemoveFullname() => CrossSettings.Remove(nameof(Username));

        #endregion FullName

        #region Password

        public static string Password
        {
            get => CrossSettings.GetValueOrDefault(nameof(Password), string.Empty);
            set => CrossSettings.AddOrUpdateValue(nameof(Password), value);
        }

        private static bool IsPasswordSet => CrossSettings.Contains(nameof(Password));

        public static void RemovePassword() => CrossSettings.Remove(nameof(Password));

        #endregion Password

        #region NguoiDungID

        public static string NguoiDungID
        {
            get => CrossSettings.GetValueOrDefault(nameof(NguoiDungID), string.Empty);
            set => CrossSettings.AddOrUpdateValue(nameof(NguoiDungID), value);
        }

        private static bool IsNguoiDungIDSet => CrossSettings.Contains(nameof(NguoiDungID));

        public static void RemoveNguoiDungID() => CrossSettings.Remove(nameof(NguoiDungID));

        #endregion NguoiDungID

        #region KyCuoc

        public static string KyCuoc
        {
            get => CrossSettings.GetValueOrDefault(nameof(KyCuoc), string.Empty);
            set => CrossSettings.AddOrUpdateValue(nameof(KyCuoc), value);
        }

        private static bool IsKyCuocSet => CrossSettings.Contains(nameof(KyCuoc));

        public static void RemoveKyCuoc() => CrossSettings.Remove(nameof(KyCuoc));

        #endregion KyCuoc

        #region ListLoTrinh

        public static string ListLoTrinh
        {
            get => CrossSettings.GetValueOrDefault(nameof(ListLoTrinh), string.Empty);
            set => CrossSettings.AddOrUpdateValue(nameof(ListLoTrinh), value);
        }

        private static bool IsListLoTrinhSet => CrossSettings.Contains(nameof(ListLoTrinh));

        public static void RemoveListLoTrinh() => CrossSettings.Remove(nameof(ListLoTrinh));

        #endregion ListLoTrinh

        public static void ClearEverything()
        {
            CrossSettings.Clear();
        }
    }
}
