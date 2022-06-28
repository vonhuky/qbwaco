using Newtonsoft.Json.Linq;
using ODAMobile.Constants;
using ODAMobile.IService;
using ODAMobile.Models;
using ODAMobile.RestClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ODAMobile.Service
{
    public class HistoryService : IHistoryService
    {
        private readonly IRestClient restClient;

        public HistoryService(IRestClient restClient)
        {
            this.restClient = restClient;
        }
        public async Task<bool> SendLogAsync(HistorySystem _history)
        {
            JObject @params = new JObject()
            {
                new JProperty("NguoiDungID", _history.NguoiDungID),
                new JProperty("LichSu", _history.LichSu)
            };
            bool rs = await restClient.PostAsync<bool, JObject>(ApiURL.URL_MAIN + "/Apis/GhiLichSuHeThong", @params);
            return rs;
        }

        public async Task<int> SyncSoGhiToServer(DataSync _dataSync)
        {
            var vm = new JArray();
            int rs = 0;
            if (_dataSync.ListSoGhi.Count > 0)
            {
                foreach (var item in _dataSync.ListSoGhi)
                {
                    var json = JObject.FromObject(item);
                    vm.Add(json);
                }
                JObject @params = new JObject()
                {
                    new JProperty("NguoiDungID", _dataSync.NguoiDungID),
                    new JProperty("ListSoGhi", vm)
                };
                rs = await restClient.PostAsync<int, JObject>(ApiURL.URL_MAIN + "/Apis/SyncSoGhiToServer", @params);
            }
            
            return rs;
        }
    }
}
