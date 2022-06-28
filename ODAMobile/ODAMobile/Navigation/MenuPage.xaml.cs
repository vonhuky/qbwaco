using ODAMobile.IService;
using ODAMobile.Service;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.TinyMVVM;
using Xamarin.Forms.Xaml;
using ODAMobile.Helpers;
using ODAMobile.Models;

namespace ODAMobile.Navigation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
		public MenuPage ()
		{
			InitializeComponent ();
            BindingContext = new MenuPageViewModel();
        }
        public class MenuPageViewModel : TinyViewModel
        {
            private MainPage RootPage => Application.Current.MainPage as MainPage;

            private readonly SQLiteConnection LocalDb;
            private readonly IContractService contractService;

            public string FullName { get; set; }

            private HomeMenuItem selectedItem;
            public HomeMenuItem SelectedItem { get => selectedItem; set => SetProperty(ref selectedItem, value); }

            public ObservableCollection<HomeMenuItem> MenuItems { get; set; } = new ObservableCollection<HomeMenuItem>();

            public ICommand MenuItemSelectedCommand { get; set; }

            public MenuPageViewModel()
            {
                LocalDb = TinyIOC.Container.Resolve<SQLiteConnection>();
                contractService = TinyIOC.Container.Resolve<ContractService>();

                FullName = Helpers.Settings.Fullname;
                InitLocalMasterMenus();
                InitMasterMenus();
                InitKyCuoc();
                MenuItemSelectedCommand = new Command<HomeMenuItem>(MenuItemSelected);

                //MessagingCenter.Subscribe<ModifyMasterMenuViewModel>(this, MessageKey.HOME_MENUS_CHANGED, OnHomeMenusChanged);
            }

            private async void InitKyCuoc()
            {
                if (ConnectivityHelper.IsNetworkAvailable())
                {
                    List<KyCuoc> ls = await contractService.GetListKyCuocAsync();
                    if (ls.Count > 0)
                    {
                        contractService.DeleteAllKyCuoc();
                        for (int i = 0; i < ls.Count; i++)
                        {
                            contractService.InsertKyCuoc(ls[i]);
                        }
                    }
                }
            }

            private void InitLocalMasterMenus()
            {
                LocalDb.DeleteAll<HomeMenuItem>();
                
                LocalDb.InsertOrReplace(new HomeMenuItem { Id = HomeMenuItemType.Home, Title = "Home", Icon = "ic_menu_home_white" });
                LocalDb.InsertOrReplace(new HomeMenuItem { Id = HomeMenuItemType.ConnectPrinter, Title = "Kết nối máy in", Icon = "ic_post_review" });
                LocalDb.InsertOrReplace(new HomeMenuItem { Id = HomeMenuItemType.HopDong, Title = "Hợp đồng", Icon = "ic_menu_bookings_white" });
                LocalDb.InsertOrReplace(new HomeMenuItem { Id = HomeMenuItemType.SoGhi, Title = "Sổ ghi", Icon = "ic_menu_bookings_white" });
                LocalDb.InsertOrReplace(new HomeMenuItem { Id = HomeMenuItemType.Settings, Title = "Cấu hình", Icon = "ic_settings_white" });
                LocalDb.InsertOrReplace(new HomeMenuItem { Id = HomeMenuItemType.SyncFromServer, Title = "Đồng bộ dữ liệu", Icon = "ic_menu_check_in_white" });
                LocalDb.InsertOrReplace(new HomeMenuItem { Id = HomeMenuItemType.Logout, Title = "Đăng xuất", Icon = "ic_logout_white" });
            }

            private async void MenuItemSelected(HomeMenuItem selected)
            {
                selected.TextColor = (Color)Application.Current.Resources["colorAccent"];

                foreach (var item in MenuItems.Where(m => m.Id != selected.Id))
                {
                    item.TextColor = Color.White;
                }

                await RootPage.NavigateFromMenu(selected.Id);
            }

            //private void OnHomeMenusChanged(ModifyMasterMenuViewModel sender)
            //{
            //    InitMasterMenus();
            //}

            private void InitMasterMenus()
            {
                MenuItems.Clear();

                foreach (var menu in LocalDb.Table<HomeMenuItem>().ToList())
                {
                    if (!menu.Hide)
                    {
                        MenuItems.Add(new HomeMenuItem()
                        {
                            Id = menu.Id,
                            Title = menu.Title,
                            Icon = menu.Icon,
                            Hide = menu.Hide
                        });
                    }
                }

                if (SelectedItem == null)
                {
                    SelectedItem = MenuItems.FirstOrDefault();
                    SelectedItem.TextColor = (Color)Application.Current.Resources["colorAccent"];
                }
                else
                {
                    SelectedItem = MenuItems.FirstOrDefault(s => s.Id == SelectedItem.Id);
                    SelectedItem.TextColor = (Color)Application.Current.Resources["colorAccent"];
                }
            }
        }
    }
}