using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BnhsQrCode.Abstracts;
using BnhsQrCode.Model;
using Microsoft.AspNetCore.Hosting;
using NHibernate;

namespace BnhsQrCode.Services
{
    public class UserImageFileService : IUserImageFileService
    {
        private readonly ISession _session;
        private ITransaction _transaction;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UserImageFileService(ISession session, IHostingEnvironment hostingEnvironment)
        {
            _session = session;
            _hostingEnvironment = hostingEnvironment;
        }
        public IQueryable<UserImageFile> UserImageFile => _session.Query<UserImageFile>();

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

        public UserImageFile GetByUserId(int id)
        {
            var userImageFile = _session.Query<UserImageFile>()
                .FirstOrDefault(c => c.UserProfileId == id);

            return userImageFile;
        }

        public async Task Save(UserImageFile entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }

        public async Task Delete(UserImageFile entity)
        {
            await _session.DeleteAsync(entity);
        }

        public async Task DeleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;

            var imageFile = _session.Query<UserImageFile>()
                .FirstOrDefault(c => c.FileName.ToLower() == fileName.ToLower());
            
            await Delete(imageFile);

            string basePath = _hostingEnvironment.ContentRootPath + $"{Path.DirectorySeparatorChar}UploadedFiles{Path.DirectorySeparatorChar}" + fileName;

            if (System.IO.File.Exists(basePath))
            {
                System.IO.File.Delete(basePath);
            }
        }
        public async Task UpdateFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;

            var imageFile = _session.Query<UserImageFile>()
                .FirstOrDefault(c => c.FileName.ToLower() == fileName.ToLower());

            await Save(imageFile);

            string basePath = _hostingEnvironment.ContentRootPath + $"{Path.DirectorySeparatorChar}UploadedFiles{Path.DirectorySeparatorChar}" + fileName;

            if (System.IO.File.Exists(basePath))
            {
                System.IO.File.Delete(basePath);
            }
        }
    }
}
