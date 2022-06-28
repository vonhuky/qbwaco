using System;
using ODAMobile.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("ODAMobile")]
[assembly: ExportEffect(typeof(BorderlessEffect), "BorderlessEffect")]
namespace ODAMobile.Droid.Effects
{
    public class BorderlessEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var plate = Control.FindViewById(Control.Context.Resources.GetIdentifier("android:id/search_plate", null, null));
                if (plate != null)
                {
                    plate.Background = null;
                }

                Control.Background = null;
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