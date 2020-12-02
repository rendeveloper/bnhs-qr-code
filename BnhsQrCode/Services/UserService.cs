using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BnhsQrCode.Abstracts;
using BnhsQrCode.Model;
using BnhsQrCode.Model.DTO;
using Microsoft.AspNetCore.Hosting;
using NHibernate;

namespace BnhsQrCode.Services
{
    public class UserService : IUserService
    {
        private readonly ISession _session;
        private ITransaction _transaction;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserImageFileService _userImageFileService;

        public UserService(ISession session, IHostingEnvironment hostingEnvironment, IUserImageFileService userImageFileService)
        {
            _session = session;
            _hostingEnvironment = hostingEnvironment;
            _userImageFileService = userImageFileService;
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
                .FirstOrDefault(c => c.TeacherId.ToLower() == teacherId.ToLower());

            return user;
        }

        public async Task Save(User entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }

        public async Task Delete(User entity)
        {
            await _userImageFileService.DeleteFile(entity.Image);
            await _session.DeleteAsync(entity);
        }



        public bool Upload(ImageDTO imageDto)
        {
            try
            {
                if (imageDto == null || imageDto.DataBytes.Length == 0) return false;

                string basePath = _hostingEnvironment.ContentRootPath + $"{Path.DirectorySeparatorChar}UploadedFiles";

                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath); //Create directory if it doesn't exist
                }

                if (imageDto.Id == 0)
                {
                    //set the image path
                    string attachmentPath = Path.Combine(basePath, imageDto.FileName);

                    byte[] attachmentBytes = imageDto.DataBytes;

                    File.WriteAllBytes(attachmentPath, attachmentBytes);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public User Authenticate(string teacherId)
        {
            if (string.IsNullOrEmpty(teacherId))
                return null;


            var user = _session.Query<User>().FirstOrDefault(c => c.TeacherId.ToLower() == teacherId.ToLower());

            // check if account exists
            if (user == null)
                return null;

            // authentication successful
            return user;
        }
    }
}
