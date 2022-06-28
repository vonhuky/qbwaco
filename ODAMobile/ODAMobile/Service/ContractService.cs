using Newtonsoft.Json.Linq;
using ODAMobile.Constants;
using ODAMobile.IService;
using ODAMobile.Models;
using ODAMobile.RestClient;
using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.TinyMVVM;

namespace ODAMobile.Service
{
    public class ContractService : IContractService
    {
        private readonly IRestClient restClient;
        private readonly SQLiteConnection LocalDb;

        public ContractService(IRestClient restClient)
        {
            this.restClient = restClient;
            LocalDb = TinyIOC.Container.Resolve<SQLiteConnection>();
        }

        public async Task<List<HopDong>> GetHopDong()
        {
            List<HopDong> rs = await restClient.GetAsync<List<HopDong>>(ApiURL.URL_MAIN + "/HopDong/GetHopDongTest");
            return rs;
        }

        public void DeleteAllContract()
        {
            LocalDb.DeleteAll<HopDong>();
        }
        public void InsertContract(HopDong hd)
        {
            LocalDb.InsertOrReplace(hd);
        }

        public List<HopDong> GetAllHopDong()
        {
            return (from data in LocalDb.Table<HopDong>()
                    select data).ToList();
        }

        public void UpdateContract(HopDong hd)
        {
            LocalDb.Update(hd);
        }

        public HopDong GetContractById(int id)
        {
            return LocalDb.Table<HopDong>().FirstOrDefault(x => x.ID == id);
        }

        public async Task<List<KyCuoc>> GetListKyCuocAsync()
        {
            List<KyCuoc> rs = await restClient.GetAsync<List<KyCuoc>>(ApiURL.URL_MAIN + "/HopDong/GetKyCuoc");
            return rs;
        }

        public List<KyCuoc> GetListKyCuoc()
        {
            return (from data in LocalDb.Table<KyCuoc>()
                    select data).ToList();
        }

        public void InsertKyCuoc(KyCuoc kc)
        {
            LocalDb.InsertOrReplace(kc);
        }

        public void DeleteAllKyCuoc()
        {
            LocalDb.DeleteAll<KyCuoc>();
        }

        public async Task<ResultData> SyncDb(string NguoiDungID, string kycuoc, string lotrinh)
        {
            JObject @params = new JObject()
            {
                new JProperty("NguoiDungID", NguoiDungID),
                new JProperty("kycuoc", kycuoc.Trim()),
                new JProperty("lotrinh", lotrinh.Trim())
            };
            ResultData rs = await restClient.PostAsync<ResultData, JObject>(ApiURL.URL_MAIN + "/Apis/syncDownTest", @params);
            return rs;
        }

        public void InsertPostage(Postage po)
        {
            LocalDb.InsertOrReplace(po);
        }

        public async Task<List<Postage>> GetAllPostageAsync(int pageIndex, int pageSize)
        {
            await Task.Delay(200);
            return (from data in LocalDb.Table<Postage>()
                    select data).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public void DeleteAllPostage()
        {
            LocalDb.DeleteAll<Postage>();
        }

        public int GetCountPostAge()
        {
            return (from data in LocalDb.Table<Postage>()
                    select data).ToList().Count;
        }

        public async Task<List<Postage>> GetAllPostage(int pageIndex, int pageSize)
        {
            await Task.Delay(200);
            return (from data in LocalDb.Table<Postage>()
                    select data).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public async Task<List<Postage>> SearchPostage(int pageIndex, int pageSize, string searchkey)
        {
            await Task.Delay(200);
            var items = (from data in LocalDb.Table<Postage>()
                         where data.ten_kh.ToLower().Contains(searchkey.ToLower())
                         select data).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            return items;
        }

        public int GetCountPostAgeSearch(string searchKey)
        {
            return (from data in LocalDb.Table<Postage>()
                    where data.ten_kh.ToLower().Contains(searchKey.ToLower())
                    select data).ToList().Count;
        }

        public Postage GetPostById(int postID)
        {
            return LocalDb.Table<Postage>().FirstOrDefault(x => x.id_hoadon == postID);
        }

        public void UpdatePostAge(Postage postage)
        {
            LocalDb.Update(postage);
        }

        public async Task<DataSoGhi> SyncSoGhiDb(string NguoiDungID, string kycuoc, string lotrinh)
        {
            JObject @params = new JObject()
            {
                new JProperty("NguoiDungID", NguoiDungID),
                new JProperty("kycuoc", kycuoc.Trim()),
                new JProperty("lotrinh", lotrinh.Trim())
            };
            DataSoGhi rs = await restClient.PostAsync<DataSoGhi, JObject>(ApiURL.URL_MAIN + "/Apis/syncDownSoGhiChiSo", @params);
            return rs;
        }

        public void InsertSoGhi(SoGhiChiSo sg)
        {
            LocalDb.InsertOrReplace(sg);
        }

        public async Task<List<SoGhiChiSo>> GetAllSoGhi(int pageIndex, int pageSize)
        {
            await Task.Delay(200);
            return (from data in LocalDb.Table<SoGhiChiSo>()
                    select data).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public async Task<List<SoGhiChiSo>> SearchSoGhi(int pageIndex, int pageSize, string searchkey)
        {
            await Task.Delay(200);
            var items = (from data in LocalDb.Table<SoGhiChiSo>()
                         where data.HoTen.ToLower().Contains(searchkey.ToLower())
                         select data).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            return items;
        }

        public int GetCountSoGhi()
        {
            return (from data in LocalDb.Table<SoGhiChiSo>()
                    select data).ToList().Count;
        }

        public int GetCountSoGhiSearch(string searchKey)
        {
            return (from data in LocalDb.Table<SoGhiChiSo>()
                    where data.HoTen.ToLower().Contains(searchKey.ToLower())
                    select data).ToList().Count;
        }

        public SoGhiChiSo GetSoGhiByMaHopDong(string MaHopDong)
        {
            return LocalDb.Table<SoGhiChiSo>().FirstOrDefault(x => x.MaHopDong == MaHopDong);
        }

        public void UpdateSoGhi(SoGhiChiSo soghi)
        {
            LocalDb.Update(soghi);
        }

        public void DeleteAllSoGhi()
        {
            LocalDb.DeleteAll<SoGhiChiSo>();
        }

        public List<SoGhiChiSo> GetListSoGhiUpdated()
        {
            return (from data in LocalDb.Table<SoGhiChiSo>()
                    where data.IsUpdated == true
                    select data).ToList();
        }
    }
}
