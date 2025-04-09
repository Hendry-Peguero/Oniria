using Oniria.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Core.Dtos.MembershipAcquisition.Request
{
    public class UpdateMembershipAcquisitionRequest
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string MembershiId { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public StatusEntity Status { get; set; }
    }
}
