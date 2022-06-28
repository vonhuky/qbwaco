using ODAMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ODAMobile.IService
{
    public interface IHistoryService
    {
        Task<bool> SendLogAsync(HistorySystem _history);

        Task<int> SyncSoGhiToServer(DataSync _dataSync);
    }
}
