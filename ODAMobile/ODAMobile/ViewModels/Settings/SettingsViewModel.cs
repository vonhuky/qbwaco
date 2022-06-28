using ODAMobile.IService;
using ODAMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.TinyMVVM;

namespace ODAMobile.ViewModels.Settings
{
    public class SettingsViewModel : TinyViewModel
    {
        private readonly ISettingService settingService;
        public SettingModel _setting;
        public SettingModel Settings { get => _setting; set => SetProperty(ref _setting, value); }

        public bool _sound;
        public bool sound { get => _sound; set => SetProperty(ref _sound, value); }

        public bool _vibrate;
        public bool vibrate { get => _vibrate; set => SetProperty(ref _vibrate, value); }

        public bool _autosync;
        public bool autosync { get => _autosync; set => SetProperty(ref _autosync, value); }

        public string _host;
        public string host { get => _host; set => SetProperty(ref _host, value); }

        public int _port;
        public int port { get => _port; set => SetProperty(ref _port, value); }

        public ICommand UpdateCommand { get; private set; }
        public SettingsViewModel(ISettingService settingService)
        {
            this.settingService = settingService;
            //settingService.DeleteCauHinh();
            Settings = settingService.GetSettings();
            if (Settings != null)
            {
                host = Settings.host;
                port = Settings.port;
                vibrate = Settings.vibrate;
                sound = Settings.sound;
                autosync = Settings.autosync;
            }
            UpdateCommand = new AwaitCommand(UpdateTrigger);
        }

        private void UpdateTrigger(object sender, TaskCompletionSource<bool> tcs)
        {
            if (Settings == null)
            {
                SettingModel st = new SettingModel
                {
                    autosync = autosync,
                    host = host,
                    port = port,
                    sound = sound,
                    vibrate = vibrate
                };
                settingService.InsertCauHinh(st);
            }
            else
            {
                Settings.port = port;
                Settings.sound = sound;
                Settings.vibrate = vibrate;
                Settings.host = host;
                Settings.autosync = autosync;
                settingService.UpdateCauHinh(Settings);
            }
            CoreMethods.DisplayAlert("Thông báo", "Thiết lập cấu hình thành công", "Ok");
            tcs.SetResult(true);
        }
    }
}
