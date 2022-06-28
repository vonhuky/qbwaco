using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ODAMobile.Helpers;
using ODAMobile.IService;
using ODAMobile.Models;
using ODAMobile.Navigation;
using ODAMobile.RestClient;
using ODAMobile.Service;
using ODAMobile.ViewModels.Login;
using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Converters;
using Xamarin.Forms.Extensions;
using Xamarin.Forms.TinyMVVM;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ODAMobile
{
    public partial class App : Application
    {
        public static JsonSerializerSettings DefaultSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new List<JsonConverter>()
            {
                new IgnoreDataTypeConverter(),
                new IgnoreFalseStringConverter()
            }
        };

        public App()
        {
            InitializeComponent();
            // Init Extensions
            ImageResourceExtension.InitImageResourceExtension("AppResources.Assets", typeof(App).GetTypeInfo().Assembly);
            TranslateExtension.InitTranslateExtension("AppResources.Localization.Resources", CultureInfo.CurrentCulture, typeof(App).GetTypeInfo().Assembly);
            RegisterDependency();
            Init();
            if (Settings.LoggedIn)
            {
                Current.MainPage = new MainPage();
            }
            else
            {
                Current.MainPage = new NavigationContainer(ViewModelResolver.ResolveViewModel<LoginViewModel>())
                {
                    BarBackgroundColor = Color.FromHex("#835e7e"),
                    BarTextColor = Color.White
                };
            }
        }

        private void RegisterDependency()
        {
            TinyIOC.Container.Register(DependencyService.Get<ILocalDatabaseService>().GetDatabaseConnection());
            TinyIOC.Container.Register<IRestClient, RestClient.RestClient>();
            TinyIOC.Container.Register<ILoginService, LoginService>();
            TinyIOC.Container.Register<IContractService, ContractService>();
            TinyIOC.Container.Register<ISettingService, SettingService>();
        }

        private void Init()
        {
            //TinyIOC.Container.Resolve<SQLiteConnection>()?.CreateTable<UserData>();
            TinyIOC.Container.Resolve<SQLiteConnection>()?.CreateTable<HomeMenuItem>();
            TinyIOC.Container.Resolve<SQLiteConnection>()?.CreateTable<HopDong>();
            TinyIOC.Container.Resolve<SQLiteConnection>()?.CreateTable<KyCuoc>();
            TinyIOC.Container.Resolve<SQLiteConnection>()?.CreateTable<Postage>();
            TinyIOC.Container.Resolve<SQLiteConnection>()?.CreateTable<SoGhiChiSo>();
            TinyIOC.Container.Resolve<SQLiteConnection>()?.CreateTable<SettingModel>();
            //TinyIOC.Container.Resolve<SQLiteConnection>()?.DropTable<SettingModel>();

            CultureInfo ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
