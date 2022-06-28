
using Android.App;
using Android.Widget;
using ODAMobile.Droid.DependencyService;
using ODAMobile.IDependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(ToastAlert))]
namespace ODAMobile.Droid.DependencyService
{
    public class ToastAlert : IToast
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(
                Application.Context,
                message,
                ToastLength.Long
                ).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(
                Application.Context,
                message,
                ToastLength.Short
                ).Show();
        }
    }
}