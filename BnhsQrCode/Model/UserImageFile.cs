using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BnhsQrCode.Model
{
    public class UserImageFile
    {
        public virtual int Id { get; set; }
        public virtual string FileName { get; set; }
        public virtual string OriginalFileName { get; set; }
        public virtual int FileSize { get; set; }
        public virtual DateTime UploadDate { get; set; }
        public virtual int UserProfileId { get; set; }
    }
}
