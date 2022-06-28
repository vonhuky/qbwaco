using ODAMobile.ViewModels.Login;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extensions;
using Xamarin.Forms.TinyMVVM;

namespace ODAMobile.ViewModels
{
    public class HomeViewModel : TinyViewModel
    {
        public ICommand LogOutCommand { get; private set; }

        public HomeViewModel()
        {
            LogOutCommand = new AwaitCommand(LogOut);
        }

        private async void LogOut(object sender, TaskCompletionSource<bool> tcs)
        {
            if (await CoreMethods.DisplayAlert(TranslateExtension.GetValue("dialog_title_logout"), TranslateExtension.GetValue("dialog_message_logout"), TranslateExtension.GetValue("yes"), TranslateExtension.GetValue("no")))
            {
                Helpers.Settings.LoggedIn = false;

                Application.Current.MainPage = new NavigationContainer(ViewModelResolver.ResolveViewModel<LoginViewModel>())
                {
                    BarBackgroundColor = Color.FromHex("#835e7e"),
                    BarTextColor = Color.White
                };
            }

            tcs.SetResult(true);
        }
    }
}