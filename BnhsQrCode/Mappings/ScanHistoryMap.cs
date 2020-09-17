using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BnhsQrCode.Model;
using FluentNHibernate.Mapping;

namespace BnhsQrCode.Mappings
{
    public class ScanHistoryMap : ClassMap<ScanHistory>
    {
        public ScanHistoryMap()
        {
            Id(x => x.Id);
            Map(x => x.UserProfileId).Not.Nullable();
            Map(x => x.BodyTemp).Not.Nullable();
            Map(x => x.TimeStatus).Not.Nullable();
            Map(x => x.CreatedByDateTime).Not.Nullable();
            Table("QRCodeHistory");
        }
    }
}
