using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BnhsQrCode.Model;

namespace BnhsQrCode.Abstracts
{
    public interface IScanHistoryService
    {
        void BeginTransaction();
        Task Commit();
        Task Rollback();
        void CloseTransaction();
        Task Save(ScanHistory entity);
        Task Delete(ScanHistory entity);
        ScanHistory GetByUserProfileId(int id);

        IQueryable<ScanHistory> ScanHistory { get; }
    }
}
