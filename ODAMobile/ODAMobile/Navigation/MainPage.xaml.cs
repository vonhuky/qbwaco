using ODAMobile.Helpers;
using ODAMobile.IService;
using ODAMobile.ViewModels;
using ODAMobile.ViewModels.Contract;
using ODAMobile.ViewModels.Login;
using ODAMobile.ViewModels.Settings;
using ODAMobile.ViewModels.SoGhi;
using ODAMobile.ViewModels.Sync;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Extensions;
using Xamarin.Forms.TinyMVVM;
using Xamarin.Forms.Xaml;

namespace ODAMobile.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            Detail = new NavigationContainer(ViewModelResolver.ResolveViewModel<HomeViewModel>());
        }

        public async Task NavigateFromMenu(HomeMenuItemType type)
        {
            Debug.WriteLine("ITEM SELECTED");

            Page selectedPage = null;

            switch (type)
            {
                case HomeMenuItemType.Home:
                    selectedPage = ViewModelResolver.ResolveViewModel<HomeViewModel>();
                    break;
                case HomeMenuItemType.SyncFromServer:
                    selectedPage = ViewModelResolver.ResolveViewModel<SyncViewModel>();
                    break;
                case HomeMenuItemType.HopDong:
                    selectedPage = ViewModelResolver.ResolveViewModel<CentralViewModel>();
                    break;
                case HomeMenuItemType.SoGhi:
                    selectedPage = ViewModelResolver.ResolveViewModel<SoGhiViewModel>();
                    break;
                case HomeMenuItemType.Settings:
                    selectedPage = ViewModelResolver.ResolveViewModel<SettingsViewModel>();
                    break;
                //case HomeMenuItemType.CheckIn:
                //    selectedPage = string.IsNullOrWhiteSpace(Host.Url) ? selectedPage = ViewModelResolver.ResolveViewModel<CheckInNonFullUserViewModel>() : selectedPage = ViewModelResolver.ResolveViewModel<CheckInViewModel>();
                //    break;

                //case HomeMenuItemType.Bookings:
                //    selectedPage = ViewModelResolver.ResolveViewModel<BookingsViewModel>();
                //    break;

                //case HomeMenuItemType.CheckOut:
                //    selectedPage = ViewModelResolver.ResolveViewModel<CheckOutViewModel>();
                //    break;

                //case HomeMenuItemType.AddBooking:
                //    selectedPage = ViewModelResolver.ResolveViewModel<BookingCalendarViewModel>();
                //    break;

                //case HomeMenuItemType.GuestBook:
                //    selectedPage = ViewModelResolver.ResolveViewModel<GuestBookViewModel>();
                //    break;

                //case HomeMenuItemType.Stats:
                //    selectedPage = ViewModelResolver.ResolveViewModel<StatsViewModel>();
                //    break;

                //case HomeMenuItemType.Settings:
                //    selectedPage = ViewModelResolver.ResolveViewModel<SettingsViewModel>();
                //    break;

                //case HomeMenuItemType.PostsAndReviews:
                //    selectedPage = ViewModelResolver.ResolveViewModel<PostsAndReviewsViewModel>();
                //    break;

                case HomeMenuItemType.Logout:
                    await LogOut();
                    break;

                default:
                    selectedPage = ViewModelResolver.ResolveViewModel<HomeViewModel>();
                    break;
            }

            if (selectedPage == null || type == HomeMenuItemType.Logout)
                return;

            if (Detail is NavigationPage detailPage)
            {
                detailPage.NotifyAllChildrenPopped();
            }

            // Default [Works for iOS]
            Detail = new NavigationContainer(selectedPage)
            {
                BarBackgroundColor = (Color)Application.Current.Resources["colorPrimary"],
                BarTextColor = Color.White
            };

            if (Device.RuntimePlatform == Device.Android)
                await Task.Delay(100);

            // Work around for nav drawer hide lag [Android]
            //var root = Detail.Navigation.NavigationStack[0];
            //Detail.Navigation.InsertPageBefore(page, root);
            //await Detail.Navigation.PopToRootAsync(false);

            IsPresented = false;
        }
        

        private async Task LogOut()
        {
            if (await DisplayAlert("Đăng xuất", "Bạn thật sự muốn đăng xuất?", TranslateExtension.GetValue("yes"), TranslateExtension.GetValue("no")))
            {
                Settings.ClearEverything();

                Application.Current.MainPage = new NavigationContainer(ViewModelResolver.ResolveViewModel<LoginViewModel>())
                {
                    BarBackgroundColor = Color.DodgerBlue,
                    BarTextColor = Color.White
                };
            }
        }
    }
}