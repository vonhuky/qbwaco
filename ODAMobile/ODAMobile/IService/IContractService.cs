using ODAMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ODAMobile.IService
{
    public interface IContractService
    {
        Task<List<HopDong>> GetHopDong();

        void InsertContract(HopDong hd);

        void DeleteAllContract();

        List<HopDong> GetAllHopDong();

        void UpdateContract(HopDong hd);

        HopDong GetContractById(int id);

        Task<List<KyCuoc>> GetListKyCuocAsync();

        List<KyCuoc> GetListKyCuoc();

        void InsertKyCuoc(KyCuoc kc);

        void DeleteAllKyCuoc();

        Task<ResultData> SyncDb(string NguoiDungID, string kycuoc, string lotrinh);

        Task<DataSoGhi> SyncSoGhiDb(string NguoiDungID, string kycuoc, string lotrinh);

        void InsertPostage(Postage po);

        Task<List<Postage>> GetAllPostage(int pageIndex, int pageSize);

        Task<List<Postage>> SearchPostage(int pageIndex, int pageSize, string searchkey);

        int GetCountPostAge();

        int GetCountPostAgeSearch(string searchKey);

        Postage GetPostById(int postID);

        void UpdatePostAge(Postage postage);

        void DeleteAllPostage();

        void InsertSoGhi(SoGhiChiSo sg);

        Task<List<SoGhiChiSo>> GetAllSoGhi(int pageIndex, int pageSize);

        Task<List<SoGhiChiSo>> SearchSoGhi(int pageIndex, int pageSize, string searchkey);

        int GetCountSoGhi();

        int GetCountSoGhiSearch(string searchKey);

        SoGhiChiSo GetSoGhiByMaHopDong(string MaHopDong);

        void UpdateSoGhi(SoGhiChiSo soghi);

        void DeleteAllSoGhi();

        List<SoGhiChiSo> GetListSoGhiUpdated();
    }
}
