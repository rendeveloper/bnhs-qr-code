using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BnhsQrCode.Model.DTO
{
    public class UserDTO
    {

        public virtual int Id { get; set; }
        public virtual string TeacherId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string Address { get; set; }
        public virtual string HealthStatus { get; set; }
        public virtual string Department { get; set; }
        public virtual string Role { get; set; }
        public virtual string Image { get; set; }
        public ImageDTO ImageDTO { get; set; }
    }
}
