using Oniria.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Core.Dtos.MembershipCategory.Request
{
    public class UpdateMembershipCategoryRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public StatusEntity Status { get; set; }
    }
}
