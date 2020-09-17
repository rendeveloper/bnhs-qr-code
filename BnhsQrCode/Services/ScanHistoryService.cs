using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BnhsQrCode.Abstracts;
using BnhsQrCode.Model;
using NHibernate;

namespace BnhsQrCode.Services
{
    public class ScanHistoryService : IScanHistoryService
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        public ScanHistoryService(ISession session)
        {
            _session = session;
        }
        public IQueryable<ScanHistory> ScanHistory => _session.Query<ScanHistory>();

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public ScanHistory GetByUserProfileId(int id)
        {
            var userProfile = _session.Query<ScanHistory>()
                .FirstOrDefault(c => c.UserProfileId == id);

            return userProfile;
        }

        public async Task Save(ScanHistory entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }

        public async Task Delete(ScanHistory entity)
        {
            await _session.DeleteAsync(entity);
        }
    }
}
