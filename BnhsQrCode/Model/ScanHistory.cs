using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BnhsQrCode.Model
{
    public class ScanHistory
    {
        public virtual int Id { get; set; }
        public virtual int UserProfileId { get; set; }
        public virtual decimal BodyTemp { get; set; }
        public virtual string TimeStatus { get; set; }
        public virtual DateTime CreatedByDateTime { get; set; }
    }
}
