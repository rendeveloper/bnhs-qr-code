using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BnhsQrCode.Model;
using BnhsQrCode.Model.DTO;

namespace BnhsQrCode.Abstracts
{
    public interface IUserService
    {
        void BeginTransaction();
        Task Commit();
        Task Rollback();
        void CloseTransaction();
        Task Save(User entity);
        Task Delete(User entity);
        User GetByTeacherId(string teacherId);

        IQueryable<User> Users { get; }
        bool Upload(ImageDTO imageDto);
        User Authenticate(string teacherId);
    }
}
