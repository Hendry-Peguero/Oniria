using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniria.Core.Domain.Entities
{
  public class OrganizationEntity : BaseEntity 
    {
        String Name { get; set; }
        String Address { get; set; }
        String PhoneNumber  { get; set; }

        String EmployeeOwnerld { get; set; }
    }
}
