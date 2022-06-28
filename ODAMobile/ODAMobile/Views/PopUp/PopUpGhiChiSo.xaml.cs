using ODAMobile.IService;
using ODAMobile.Models;
using ODAMobile.Service;
using ODAMobile.ViewModels.SoGhi;
using ODAMobile.Views.SoGhi;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
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
	public partial class PopUpGhiChiSo
	{
        private readonly SQLiteConnection LocalDb;
        private readonly IContractService contractService;
        private readonly IHistoryService historyService;

        public SoGhiChiSo sgcs;
        public SoGhiChiSo SGCS { get => sgcs; set => sgcs = value; }
        
        public ICommand UpdateCommand { get; private set; }

        public PopUpGhiChiSo ()
		{
			InitializeComponent ();
		}

        public PopUpGhiChiSo(SoGhiChiSo _soghi)
        {
            InitializeComponent();
            this.BindingContext = _soghi;
            SGCS = _soghi;
            LocalDb = TinyIOC.Container.Resolve<SQLiteConnection>();
            contractService = TinyIOC.Container.Resolve<ContractService>();
            historyService = TinyIOC.Container.Resolve<HistoryService>();
        }

        private async void BtnClose_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void BtnCapNhat_Clicked(object sender, EventArgs e)
        {
            SGCS.ChiSoGhiDuoc = int.Parse(txtChiSoGhi.Text);
            if (txtChiSoGhi.Text != "0")
            {
                SGCS.IsUpdated = true;
                SGCS.NgayGhiThucTe = dpNgayGhi.Date;
            }
            contractService.UpdateSoGhi(SGCS);
            HistorySystem _objHs = new HistorySystem();
            _objHs.NguoiDungID = int.Parse(Helpers.Settings.NguoiDungID);
            _objHs.LichSu = Helpers.Settings.Fullname + " đã cập nhật chỉ số nước của hợp đồng " + SGCS.MaHopDong + ", giá trị: " + txtChiSoGhi.Text;
            await historyService.SendLogAsync(_objHs);
            MessagingCenter.Send<App>((App)Xamarin.Forms.Application.Current, "Update");
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}