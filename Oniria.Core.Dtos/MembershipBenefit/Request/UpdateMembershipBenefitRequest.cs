using Oniria.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Core.Dtos.MembershipBenefit.Request
{
    public class UpdateMembershipBenefitRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public StatusEntity Status { get; set; }
    }
}
