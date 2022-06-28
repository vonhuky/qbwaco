using ODAMobile.Helpers;
using ODAMobile.IService;
using ODAMobile.Models;
using ODAMobile.Views.PopUp;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;
using Xamarin.Forms.TinyMVVM;

namespace ODAMobile.ViewModels.SoGhi
{
    public class SoGhiViewModel : TinyViewModel
    {
        private bool _isWorking;
        public bool IsWorking
        {
            get => _isWorking;
            set
            {
                _isWorking = value;
                OnPropertyChanged();
            }
        }
        private const int PageSize = 10;


        private readonly IContractService contractService;

        private string searchKey;
        public string SearchKey { get => searchKey; set => SetProperty(ref searchKey, value); }

        private bool isSearching;
        public bool IsSearching { get => isSearching; set => SetProperty(ref isSearching, value); }

        private string titleSoGhi;
        public string TitleSoGhi { get => titleSoGhi; set => SetProperty(ref titleSoGhi, value); }

        public ICommand StartSearchSoGhiCommand { get; private set; }
        public ICommand StopSearchSoGhiCommand { get; private set; }

        public ICommand GhiChiSoCommand { get; private set; }
        

        private InfiniteScrollCollection<SoGhiChiSo> _items;
        public InfiniteScrollCollection<SoGhiChiSo> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public SoGhiViewModel(IContractService contractService)
        {
            this.contractService = contractService;
            IsBusy = true;
            IsSearching = true;
            this.TitleSoGhi = "Sổ ghi kỳ cước " + Helpers.Settings.KyCuoc;
            StartSearchSoGhiCommand = new AwaitCommand(StartSearchSoGhi);
            StopSearchSoGhiCommand = new AwaitCommand(StopSearchSoGhi);
            GhiChiSoCommand = new AwaitCommand<SoGhiChiSo>(GhiChiSo);
            MessagingCenter.Subscribe<App>((App)Application.Current, "Update", (sender) =>
            {
                Items = new InfiniteScrollCollection<SoGhiChiSo>
                {
                    OnLoadMore = async () =>
                    {
                        IsWorking = true;
                        var page = Items.Count / PageSize;
                        var items = await contractService.GetAllSoGhi(page, PageSize);
                        IsWorking = false;
                        return items;
                    },
                    OnCanLoadMore = () =>
                    {
                        return true;
                    }
                };
                _ = DownloadDataAsync();
            });
            
            Items = new InfiniteScrollCollection<SoGhiChiSo>
            {
                OnLoadMore = async () =>
                {
                    IsWorking = true;
                    var page = Items.Count / PageSize;
                    var items = await contractService.GetAllSoGhi(page, PageSize);
                    IsWorking = false;
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return true;
                }
            };
            _ = DownloadDataAsync();
            IsBusy = false;
        }
        
        private async void GhiChiSo(SoGhiChiSo sender, TaskCompletionSource<bool> tcs)
        {
            await PopupNavigation.Instance.PushAsync(new PopUpGhiChiSo(sender));
            tcs.SetResult(true);
        }

        private async Task DownloadDataAsync()
        {
            var items = await contractService.GetAllSoGhi(pageIndex: 0, pageSize: PageSize);
            Items.AddRange(items);
        }

        private async Task DownloadDataAsyncSearch(string strSearch)
        {
            var items = await contractService.SearchSoGhi(pageIndex: 0, pageSize: PageSize, strSearch);
            Items.AddRange(items);
        }


        public override void OnPageCreated()
        {
            base.OnPageCreated();

            this.WhenAny(OnSearchKeyChanged, vm => vm.SearchKey);
        }
        public override void OnPopped()
        {
            base.OnPopped();
        }
        

        private void StartSearchSoGhi(object sender, TaskCompletionSource<bool> tcs)
        {
            IsSearching = true;
            tcs.SetResult(true);
        }

        private async void StopSearchSoGhi(object sender, TaskCompletionSource<bool> tcs)
        {
            IsSearching = false;
            IsBusy = true;
            Items = new InfiniteScrollCollection<SoGhiChiSo>
            {
                OnLoadMore = async () =>
                {
                    IsWorking = true;
                    var page = Items.Count / PageSize;
                    var items = await contractService.GetAllSoGhi(page, PageSize);
                    IsWorking = false;
                    return items;
                }
            };
            await DownloadDataAsync();
            SearchKey = "";
            IsBusy = false;
            tcs.SetResult(true);
        }

        private void OnSearchKeyChanged(string sender)
        {
            if (!IsSearching)
                return;

            Task.Run(async () =>
            {
                if (string.IsNullOrWhiteSpace(SearchKey))
                {
                    Items = new InfiniteScrollCollection<SoGhiChiSo>
                    {
                        OnLoadMore = async () =>
                        {
                            IsWorking = true;
                            var page = Items.Count / PageSize;
                            var items = await contractService.GetAllSoGhi(page, PageSize);
                            IsWorking = false;
                            return items;
                        }
                    };
                    await DownloadDataAsync();
                }
                else
                {
                    Items = new InfiniteScrollCollection<SoGhiChiSo>
                    {
                        OnLoadMore = async () =>
                        {
                            IsWorking = true;
                            var page = Items.Count / PageSize;
                            var items = await contractService.SearchSoGhi(page, PageSize, searchKey);
                            IsWorking = false;
                            return items;
                        },
                        OnCanLoadMore = () =>
                        {
                            return false;
                        }
                    };
                    await DownloadDataAsyncSearch(searchKey);
                }

            });
        }
    }
}
