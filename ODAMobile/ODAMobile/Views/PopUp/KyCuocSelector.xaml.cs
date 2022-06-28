using ODAMobile.Helpers;
using ODAMobile.IService;
using ODAMobile.Models;
using ODAMobile.RestClient;
using ODAMobile.Service;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.TinyMVVM;
using Xamarin.Forms.Xaml;

namespace ODAMobile.Views.PopUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class KyCuocSelector : ContentView
	{
        public static readonly BindableProperty KyCuocSelectedCommandProperty = BindableProperty.Create(nameof(KyCuocSelectedCommand), typeof(ICommand), typeof(KyCuocSelector), default(ICommand));

        private readonly SQLiteConnection LocalDb;
        private readonly IContractService contractService;

        public ICommand KyCuocSelectedCommand
        {
            get { return (ICommand)GetValue(KyCuocSelectedCommandProperty); }
            set { SetValue(KyCuocSelectedCommandProperty, value); }
        }

        public event EventHandler<object> KyCuocSelected;

        public KyCuocSelector ()
		{
			InitializeComponent ();
            LocalDb = TinyIOC.Container.Resolve<SQLiteConnection>();
            contractService = TinyIOC.Container.Resolve<ContractService>();
            PropertyChanged += KyCuocSelector_PropertyChanged;
            kcSearch.TextChanged += kcSearch_TextChanged;
            lvKyCuoc.ItemTapped += LvKyCuoc_ItemTapped;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => IsVisible = false;
            LblClose.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void LvKyCuoc_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (KyCuocSelectedCommand != null && KyCuocSelectedCommand.CanExecute(e.Item))
            {
                KyCuocSelectedCommand.Execute(e.Item);
            }

            KyCuocSelected?.Invoke(this, e.Item);
            IsVisible = false;
        }

        private void kcSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<KyCuoc> lsKyCuoc = contractService.GetListKyCuoc();
            if (string.IsNullOrWhiteSpace(kcSearch.Text))
                lvKyCuoc.ItemsSource = lsKyCuoc;
            else
                lvKyCuoc.ItemsSource = lsKyCuoc.FindAll(x => x.TenKyCuoc.Contains(kcSearch.Text)).ToList();
        }

        private void KyCuocSelector_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == IsVisibleProperty.PropertyName)
            {
                if (IsVisible)
                {
                    kcSearch.Text = "";
                    //LVNationality.ItemsSource = CountryUtil.AllCountries;
                }
            }
        }
    }
}