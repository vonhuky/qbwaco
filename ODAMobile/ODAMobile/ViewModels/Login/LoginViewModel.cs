using ODAMobile.IService;
using ODAMobile.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extensions;
using Xamarin.Forms.TinyMVVM;

namespace ODAMobile.ViewModels.Login
{
    public class LoginViewModel : TinyViewModel
    {
        private readonly ILoginService loginService;

        public string username;
        public string Username { get => username; set => SetProperty(ref username, value); }

        public string password;
        public string Password { get => password; set => SetProperty(ref password, value); }

        public string validationError;
        public string ValidationError { get => validationError; set => SetProperty(ref validationError, value); }

        public ICommand LoginCommand { get; private set; }

        public LoginViewModel(ILoginService loginService)
        {
            this.loginService = loginService;

            LoginCommand = new AwaitCommand<Entry>(LoginTrigger);
        }

        private void LoginTrigger(object sender, TaskCompletionSource<bool> tcs)
        {
            if (!AllFormValid() || IsBusy)
            {
                tcs.SetResult(true);
                return;
            }
            IsBusy = true;
            Task.Run(async () =>
            {
                return await loginService.Login(Username, Password);
            }).ContinueWith(task => Device.BeginInvokeOnMainThread(() =>
            {
                IsBusy = false;
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    if (task.Result != null)
                    {
                        if (task.Result.Status == 0)
                        {
                            Helpers.Settings.NguoiDungID = task.Result.Id.ToString();
                            Helpers.Settings.LoggedIn = true;
                            Helpers.Settings.Fullname = task.Result.FullName;
                            Helpers.Settings.ListLoTrinh = task.Result.ListLoTrinh;
                            Application.Current.MainPage = new MainPage();
                        }
                        else if (task.Result.Status == -1)
                        {
                            CoreMethods.DisplayAlert(TranslateExtension.GetValue("dialog_title_login"), TranslateExtension.GetValue("dialog_message_login_fail"), TranslateExtension.GetValue("ok"));
                        }
                    }
                    else
                    {
                        CoreMethods.DisplayAlert(TranslateExtension.GetValue("dialog_title_login"), TranslateExtension.GetValue("dialog_message_server_error"), TranslateExtension.GetValue("ok"));
                    }
                }
                else if (task.IsFaulted)
                {
                    CoreMethods.DisplayAlert(TranslateExtension.GetValue("error"), task.Exception?.GetBaseException().Message, TranslateExtension.GetValue("ok"));
                }
            }));
            tcs.SetResult(true);
        }

        private bool AllFormValid()
        {
            return true;
        }
    }
}
