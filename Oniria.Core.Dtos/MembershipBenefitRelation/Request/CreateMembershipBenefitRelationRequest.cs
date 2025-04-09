using Oniria.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Core.Dtos.MembershipBenefitRelation.Request
{
    public class CreateMembershipBenefitRelationRequest
    {
        public bool Available { get; set; }
        public string MembershipId { get; set; }
        public string MembershiBenefitpId { get; set; }
    }
}
