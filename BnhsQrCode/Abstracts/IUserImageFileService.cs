using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BnhsQrCode.Model;

namespace BnhsQrCode.Abstracts
{
    public interface IUserImageFileService
    {
        void BeginTransaction();
        Task Commit();
        Task Rollback();
        void CloseTransaction();
        Task Save(UserImageFile entity);
        Task Delete(UserImageFile entity);
        UserImageFile GetByUserId(int id);

        IQueryable<UserImageFile> UserImageFile { get; }
        Task DeleteFile(string fileName);
    }
}
