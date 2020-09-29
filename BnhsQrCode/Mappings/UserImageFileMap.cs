using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BnhsQrCode.Model;
using FluentNHibernate.Mapping;

namespace BnhsQrCode.Mappings
{
    public class UserImageFileMap : ClassMap<UserImageFile>
    {
        public UserImageFileMap()
        {
            Id(x => x.Id);
            Map(x => x.FileName).Not.Nullable();
            Map(x => x.OriginalFileName).Not.Nullable();
            Map(x => x.FileSize).Not.Nullable();
            Map(x => x.UploadDate).Not.Nullable();
            Map(x => x.UserProfileId).Nullable();
            Table("UserImage");
        }
    }
}
