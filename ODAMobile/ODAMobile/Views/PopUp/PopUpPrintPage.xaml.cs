using ODAMobile.IDependencyServices;
using ODAMobile.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ODAMobile.Views.PopUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopUpPrintPage
	{
        private readonly IBlueToothService blueToothService;

        private IList<string> _deviceList;
        public IList<string> DeviceList
        {
            get
            {
                if (_deviceList == null)
                    _deviceList = new ObservableCollection<string>();
                return _deviceList;
            }
            set
            {
                _deviceList = value;
            }
        }

        private string _printMessage;
        public string PrintMessage
        {
            get
            {
                return _printMessage;
            }
            set
            {
                _printMessage = value;
            }
        }

        private string _selectedDevice;
        public string SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }
            set
            {
                _selectedDevice = value;
            }
        }
        public ICommand PrintCommand => new Command(async () =>
        {
            PrintMessage += " Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            await blueToothService.Print(SelectedDevice, PrintMessage);
        });
        public PopUpPrintPage ()
		{
            //blueToothService = DependencyService.Get<IBlueToothService>();
            //BindDeviceList();
            InitializeComponent ();
		}

        public PopUpPrintPage(Postage sender)
        {
            this.BindingContext = this;
            blueToothService = DependencyService.Get<IBlueToothService>();
            BindDeviceList();
            InitializeComponent();
        }

        void BindDeviceList()
        {
            var list = blueToothService.GetDeviceList();
            DeviceList.Clear();
            foreach (var item in list)
                DeviceList.Add(item);
        }

        private void BtnPrint_Clicked(object sender, EventArgs e)
        {

        }

        private async void BtnClose_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}