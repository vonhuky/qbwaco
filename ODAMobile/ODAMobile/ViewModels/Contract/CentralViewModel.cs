using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.TinyMVVM;

namespace ODAMobile.ViewModels.Contract
{
    public class CentralViewModel : TinyViewModel
    {
        public ICommand ViewAllContract { get; private set; }

        public CentralViewModel()
        {
            ViewAllContract = new AwaitCommand(AllContract);
        }

        private async void AllContract(object arg1, TaskCompletionSource<bool> tcs)
        {
            await CoreMethods.PushViewModel<ContractViewModel>();
            tcs.SetResult(true);
        }
    }

}
