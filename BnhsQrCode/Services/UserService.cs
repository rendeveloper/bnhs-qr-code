using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BnhsQrCode.Abstracts;
using BnhsQrCode.Model;
using NHibernate;

namespace BnhsQrCode.Services
{
    public class UserService : IUserService
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        public UserService(ISession session)
        {
            _session = session;
        }
        public IQueryable<User> Users => _session.Query<User>();

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

        public User GetByTeacherId(string teacherId)
        {
            var user = _session.Query<User>()
                .FirstOrDefault(c => c.TeacherId == teacherId);

            return user;
        }

        public async Task Save(User entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }

        public async Task Delete(User entity)
        {
            await _session.DeleteAsync(entity);
        }
    }
}
