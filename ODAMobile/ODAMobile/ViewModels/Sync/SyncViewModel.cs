using ODAMobile.IService;
using ODAMobile.Models;
using ODAMobile.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extensions;
using Xamarin.Forms.TinyMVVM;

namespace ODAMobile.ViewModels.Sync
{
    public class SyncViewModel : TinyViewModel
    {
        private readonly IContractService contractService;
        private readonly IHistoryService historyService;

        public string tenkycuoc;
        public string TenKyCuoc { get => tenkycuoc; set => SetProperty(ref tenkycuoc, value); }

        private bool kycuocSelectorVisible;
        public bool KyCuocSelectorVisible { get => kycuocSelectorVisible; set => SetProperty(ref kycuocSelectorVisible, value); }

        public ICommand SyncFromServerCommand { get; private set; }

        public ICommand SyncToServerCommand { get; private set; }

        public ICommand SyncSoGhiFromServerCommand { get; private set; }

        public ICommand SyncSoGhiToServerCommand { get; private set; }

        public ICommand SelectKyCuocCommand { get; private set; }
        public ICommand KyCuocSelectedCommand { get; private set; }

        public SyncViewModel(IContractService contractService)
        {
            this.contractService = contractService;
            historyService = TinyIOC.Container.Resolve<HistoryService>();
            SyncFromServerCommand = new AwaitCommand<Entry>(SyncFromServerTrigger);
            SyncSoGhiFromServerCommand = new AwaitCommand<Entry>(SyncSoGhiFromServerTrigger);
            SyncSoGhiToServerCommand = new AwaitCommand<Entry>(SyncSoGhiToServerTrigger);
            SelectKyCuocCommand = new AwaitCommand(SelectKyCuoc);
            KyCuocSelectedCommand = new AwaitCommand<KyCuoc>(KyCuocSelected);
        }

        private void SyncSoGhiToServerTrigger(Entry arg1, TaskCompletionSource<bool> tcs)
        {
            IsBusy = true;
            DataSync _dataSync = new DataSync();
            _dataSync.NguoiDungID = int.Parse(Helpers.Settings.NguoiDungID);
            List<SoGhiChiSo> lsSoGhi = contractService.GetListSoGhiUpdated();
            _dataSync.ListSoGhi = lsSoGhi;
            Task.Run(async () =>
            {
                return await historyService.SyncSoGhiToServer(_dataSync);
            }).ContinueWith(task => Device.BeginInvokeOnMainThread(() =>
            {
                IsBusy = false;
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    if (task.Result != 0)
                    {
                        CoreMethods.DisplayAlert("Thông báo", "Đã đồng bộ " + task.Result + " chỉ số", "Ok");
                        for (int i = 0; i < _dataSync.ListSoGhi.Count; i++)
                        {
                            SoGhiChiSo _obj = contractService.GetSoGhiByMaHopDong(_dataSync.ListSoGhi[i].MaHopDong);
                            if (_obj != null)
                            {
                                _obj.IsUpdated = false;
                            }
                            contractService.UpdateSoGhi(_obj);
                        }
                    }
                    else
                    {
                        CoreMethods.DisplayAlert("Thông báo", "Không có dữ liệu cần đồng bộ", "Ok");
                    }
                }
                else if (task.IsFaulted)
                {
                    CoreMethods.DisplayAlert(TranslateExtension.GetValue("error"), task.Exception?.GetBaseException().Message, TranslateExtension.GetValue("ok"));
                }
            }));
            tcs.SetResult(true);
        }

        private void SyncSoGhiFromServerTrigger(object sender, TaskCompletionSource<bool> tcs)
        {
            IsBusy = true;

            Task.Run(async () =>
            {
                return await contractService.SyncSoGhiDb(Helpers.Settings.NguoiDungID, TenKyCuoc, Helpers.Settings.ListLoTrinh);
            }).ContinueWith(task => Device.BeginInvokeOnMainThread(() =>
            {
                IsBusy = false;
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    if (task.Result != null)
                    {
                        if (task.Result.ListSoGhi.Count > 0)
                        {
                            contractService.DeleteAllSoGhi();
                            for (int i = 0; i < task.Result.ListSoGhi.Count; i++)
                            {
                                task.Result.ListSoGhi[i].IsUpdated = false;
                                task.Result.ListSoGhi[i].TenKyCuoc = TenKyCuoc;
                                contractService.InsertSoGhi(task.Result.ListSoGhi[i]);
                            }
                            Helpers.Settings.KyCuoc = TenKyCuoc;
                            CoreMethods.DisplayAlert("Đồng bộ", "Đồng bộ sổ ghi thành công", TranslateExtension.GetValue("ok"));
                        }
                    }
                }
                else if (task.IsFaulted)
                {
                    CoreMethods.DisplayAlert(TranslateExtension.GetValue("error"), task.Exception?.GetBaseException().Message, TranslateExtension.GetValue("ok"));
                }
            }));

            tcs.SetResult(true);
        }

        private void KyCuocSelected(KyCuoc sender, TaskCompletionSource<bool> tcs)
        {
            TenKyCuoc = sender.TenKyCuoc;
            kycuocSelectorVisible = false;
            tcs.SetResult(true);
        }

        private void SelectKyCuoc(object sender, TaskCompletionSource<bool> tcs)
        {

            KyCuocSelectorVisible = true;

            tcs.SetResult(true);
        }

        private void SyncFromServerTrigger(object sender, TaskCompletionSource<bool> tcs)
        {
            IsBusy = true;

            Task.Run(async () =>
            {
                return await contractService.SyncDb(Helpers.Settings.NguoiDungID, TenKyCuoc, Helpers.Settings.ListLoTrinh);
            }).ContinueWith(task => Device.BeginInvokeOnMainThread(() =>
            {
                IsBusy = false;
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    if (task.Result != null)
                    {
                        if (task.Result.data.data_postage.Count > 0)
                        {
                            contractService.DeleteAllPostage();
                            for (int i = 0; i < task.Result.data.data_postage.Count; i++)
                            {
                                contractService.InsertPostage(task.Result.data.data_postage[i].Postage);
                            }
                            Helpers.Settings.KyCuoc = TenKyCuoc;
                            CoreMethods.DisplayAlert("Đồng bộ", "Đồng bộ thành công", TranslateExtension.GetValue("ok"));
                        }
                    }
                }
                else if (task.IsFaulted)
                {
                    CoreMethods.DisplayAlert(TranslateExtension.GetValue("error"), task.Exception?.GetBaseException().Message, TranslateExtension.GetValue("ok"));
                }
            }));

            tcs.SetResult(true);
        }
    }
}
