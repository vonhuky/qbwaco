using Android.Widget;

namespace ODAMobile.Droid.Controls.FloatingActionButton
{
    public interface IOnScrollChangedListener
    {
        void OnScrollChanged(ScrollView who, int l, int t, int oldl, int oldt);
    }
}