using System;
using Android.Support.V4.Content;
using ODAMobile.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(RoundedEffect), "RoundedEffect")]
namespace ODAMobile.Droid.Effects
{
    public class RoundedEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                Control.Background = ContextCompat.GetDrawable(Android.App.Application.Context, Resource.Drawable.RoundedBackground);
                Control.SetPadding(20, 10, 20, 10);
                Control.TextAlignment = Android.Views.TextAlignment.Gravity;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}