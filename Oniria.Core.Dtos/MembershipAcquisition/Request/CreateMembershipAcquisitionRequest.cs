using Oniria.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Core.Dtos.MembershipAcquisition.Request
{
    public class CreateMembershipAcquisitionRequest
    {
        public string OwnerId { get; set; }
        public string MembershipId { get; set; }
        public DateTime AcquisitionDate { get; set; }
    }
}
