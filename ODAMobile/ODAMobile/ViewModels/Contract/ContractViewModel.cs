using ODAMobile.Helpers;
using ODAMobile.IService;
using ODAMobile.Models;
using ODAMobile.Views.PopUp;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;
using Xamarin.Forms.TinyMVVM;

namespace ODAMobile.ViewModels.Contract
{
    public class ContractViewModel : TinyViewModel
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
        private const int PageSize = 20;


        private readonly IContractService contractService;
        public ICommand GachNoCommand { get; private set; }
        public ICommand PrintCommand { get; private set; }
        private string searchKey;
        public string SearchKey { get => searchKey; set => SetProperty(ref searchKey, value); }

        private bool isSearching;
        public bool IsSearching { get => isSearching; set => SetProperty(ref isSearching, value); }


        public ICommand StartSearchContractCommand { get; private set; }
        public ICommand StopSearchContractCommand { get; private set; }

        private InfiniteScrollCollection<Postage> _items;
        public InfiniteScrollCollection<Postage> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public ContractViewModel(IContractService contractService)
        {
            this.contractService = contractService;
            IsSearching = true;
            StartSearchContractCommand = new AwaitCommand(StartSearchContract);
            StopSearchContractCommand = new AwaitCommand(StopSearchContract);
            IsBusy = true;
            GachNoCommand = new AwaitCommand<Postage>(GachNo);
            PrintCommand = new AwaitCommand<Postage>(Print);
            Items = new InfiniteScrollCollection<Postage>
            {
                OnLoadMore = async () =>
                {
                    IsWorking = true;
                    var page = Items.Count / PageSize;
                    var items = await contractService.GetAllPostage(page, PageSize);
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

        private async void Print(Postage sender, TaskCompletionSource<bool> tcs)
        {
            await PopupNavigation.Instance.PushAsync(new PopUpPrintPage(sender));
            tcs.SetResult(true);
        }

        private async Task DownloadDataAsync()
        {
            var items = await contractService.GetAllPostage(pageIndex: 0, pageSize: PageSize);
            Items.AddRange(items);
        }

        private async Task DownloadDataAsyncSearch(string strSearch)
        {
            var items = await contractService.SearchPostage(pageIndex: 0, pageSize: PageSize, strSearch);
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

        private async void GachNo(Postage sender, TaskCompletionSource<bool> tcs)
        {
            if (IsBusy || !ConnectivityHelper.IsNetworkAvailable())
            {
                return;
            }
            var result = await CoreMethods.DisplayAlert("Thông báo", "Bạn muốn gạch nợ cho khách hàng này?", "Yes", "No");
            if (result)
            {
                Postage objPost = contractService.GetPostById(sender.id_hoadon);
                objPost.status = 1;
                await Task.Run(() =>
                {
                    contractService.UpdatePostAge(objPost);
                }).ContinueWith(task => Device.BeginInvokeOnMainThread(() =>
                {
                    Items = new InfiniteScrollCollection<Postage>
                    {
                        OnLoadMore = async () =>
                        {
                            IsWorking = true;
                            var page = Items.Count / PageSize;
                            var items = await contractService.GetAllPostage(page, PageSize);
                            IsWorking = false;
                            return items;
                        }
                    };
                    _ = DownloadDataAsync();
                    CoreMethods.DisplayAlert("Gạch nợ", "Gạch nợ thành công", "ok");
                }));
            }
            
            tcs.SetResult(true);
        }

        private void StartSearchContract(object sender, TaskCompletionSource<bool> tcs)
        {
            IsSearching = true;
            //if (IsBusy)
            //{
            //    tcs.SetResult(true);
            //    return;
            //}
            
            tcs.SetResult(true);
        }

        private async void StopSearchContract(object sender, TaskCompletionSource<bool> tcs)
        {

            IsSearching = false;
            Items = new InfiniteScrollCollection<Postage>
            {
                OnLoadMore = async () =>
                {
                    IsWorking = true;
                    var page = Items.Count / PageSize;
                    var items = await contractService.GetAllPostage(page, PageSize);
                    IsWorking = false;
                    return items;
                }
            };
            await DownloadDataAsync();
            SearchKey = "";

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
                    Items = new InfiniteScrollCollection<Postage>
                    {
                        OnLoadMore = async () =>
                        {
                            IsWorking = true;
                            var page = Items.Count / PageSize;
                            var items = await contractService.GetAllPostage(page, PageSize);
                            IsWorking = false;
                            return items;
                        }
                    };
                    await DownloadDataAsync();
                }
                else
                {
                    Items = new InfiniteScrollCollection<Postage>
                    {
                        OnLoadMore = async () =>
                        {
                            IsWorking = true;
                            var page = Items.Count / PageSize;
                            var items = await contractService.SearchPostage(page, PageSize, searchKey);
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
