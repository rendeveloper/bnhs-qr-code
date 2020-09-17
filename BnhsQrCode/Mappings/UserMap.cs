using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BnhsQrCode.Model;
using FluentNHibernate.Mapping;

namespace BnhsQrCode.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.TeacherId).Not.Nullable();
            Map(x => x.FirstName).Not.Nullable();
            Map(x => x.LastName).Not.Nullable();
            Map(x => x.MiddleName).Not.Nullable();
            Map(x => x.DateOfBirth).Nullable();
            Map(x => x.Address).Not.Nullable();
            Map(x => x.HealthStatus).Not.Nullable();
            Map(x => x.Department).Not.Nullable();
            Map(x => x.Role).Not.Nullable();
            Map(x => x.Image).Length(int.MaxValue).Not.Nullable();
            Table("UserProfile");
        }
    }
}
